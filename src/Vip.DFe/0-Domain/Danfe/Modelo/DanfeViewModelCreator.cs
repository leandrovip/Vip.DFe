using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using Vip.DFe.Danfe.Enum;
using Vip.DFe.Extensions;
using Vip.DFe.Helpers;
using Vip.DFe.NFe.Enum;
using Vip.DFe.NFe.Interfaces;
using Vip.DFe.NFe.NotaFiscal;
using Vip.DFe.NFe.NotaFiscal.Destinatario;
using Vip.DFe.NFe.NotaFiscal.Detalhe.Imposto.Estadual;
using Vip.DFe.NFe.NotaFiscal.Detalhe.Imposto.Federal;
using Vip.DFe.NFe.NotaFiscal.Emitente;
using Vip.DFe.NFe.NotaFiscal.Total;
using Vip.DFe.Shared.Enum;

namespace Vip.DFe.Danfe.Modelo
{
    public static class DanfeViewModelCreator
    {
        #region Fields

        public static readonly IEnumerable<TipoEmissao> FormasEmissaoSuportadas = new[]
        {
            TipoEmissao.Normal,
            TipoEmissao.SVCAN,
            TipoEmissao.SVCRS
        };

        #endregion

        #region Methods

        internal static DanfeViewModel CreateFromXmlStream(Stream stream)
        {
            try
            {
                var nfe = NFeProc.Load(stream);
                return CreateFromXml(nfe);
            }
            catch (System.Exception ex)
            {
                if (ex.InnerException is XmlException e) throw new System.Exception($"Não foi possível interpretar o Xml. Linha {e.LineNumber} Posição {e.LinePosition}.");

                throw new XmlException("O Xml não parece ser uma NF-e processada.", ex);
            }
        }

        internal static DanfeViewModel CreateFromXmlString(string xml)
        {
            try
            {
                var nfe = NFeProc.Load(xml);
                return CreateFromXml(nfe);
            }
            catch (System.Exception ex)
            {
                throw new System.Exception("Não foi possível interpretar o texto Xml.", ex);
            }
        }

        /// <summary>
        ///     Cria o modelo a partir de uma string xml.
        /// </summary>
        public static DanfeViewModel CriarDeStringXml(string str)
        {
            if (str == null) throw new ArgumentNullException(nameof(str));
            return CreateFromXmlString(str);
        }

        /// <summary>
        ///     Cria o modelo a partir de um arquivo xml.
        /// </summary>
        /// <param name="caminho"></param>
        /// <returns></returns>
        public static DanfeViewModel CriarDeArquivoXml(string caminho)
        {
            using var sr = new StreamReader(caminho, true);
            return CreateFromXmlStream(sr.BaseStream);
        }

        /// <summary>
        ///     Cria o modelo a partir de um arquivo xml contido num stream.
        /// </summary>
        /// <param name="stream"></param>
        /// <returns>Modelo</returns>
        public static DanfeViewModel CriarDeArquivoXml(Stream stream)
        {
            if (stream == null) throw new ArgumentNullException(nameof(stream));
            return CreateFromXmlStream(stream);
        }

        public static DanfeViewModel CreateFromXml(NFeProc procNfe)
        {
            var model = new DanfeViewModel();

            var nfe = procNfe.NFe;
            var infNfe = nfe.InfNFe;
            var ide = infNfe.Ide;

            model.TipoEmissao = ide.TipoEmissao;

            if (ide.Modelo != NFeModelo.NFe)
                throw new NotSupportedException("Somente o mod==55 está implementado.");

            if (!FormasEmissaoSuportadas.Contains(model.TipoEmissao))
                throw new NotSupportedException($"O tpEmis {ide.TipoEmissao} não é suportado.");

            model.Orientacao = ide.TipoImpressao == TipoImpressao.NormalRetrato ? Orientacao.Retrato : Orientacao.Paisagem;

            var infProt = procNfe.ProtNFe.InfProt;
            model.CodigoStatusReposta = infProt.CStat;
            model.DescricaoStatusReposta = infProt.Motivo;
            model.TipoAmbiente = (int) ide.TpAmb;
            model.NfNumero = (int) ide.NumeroNFe;
            model.NfSerie = ide.Serie;
            model.NaturezaOperacao = ide.NatOp;
            model.ChaveAcesso = procNfe.NFe.InfNFe.Id.Substring(3);
            model.TipoNF = (int) ide.TipoNFe;

            model.Emitente = ObterEmitente(infNfe.Emitente);
            model.Destinatario = ObterDestinatario(infNfe.Destinatario);

            #region Compra / Entrega / Retirada

            if (infNfe.Retirada != null)
                model.LocalRetirada = ObterLocalRetirada(infNfe.Retirada);

            if (infNfe.Entrega != null)
                model.LocalEntrega = ObterLocalEntrega(infNfe.Entrega);

            model.NotasFiscaisReferenciadas = ide.NFref.Select(x => x.ToString()).ToList();

            if (infNfe.Compra != null)
            {
                model.Contrato = infNfe.Compra.XCont;
                model.NotaEmpenho = infNfe.Compra.XNEmp;
                model.Pedido = infNfe.Compra.XPed;
            }

            #endregion

            #region Detalhes

            foreach (var det in infNfe.Detalhe)
            {
                var produto = new ProdutoViewModel
                {
                    Codigo = det.Produto.Codigo,
                    Descricao = det.Produto.XProd,
                    Ncm = det.Produto.NCM,
                    Cfop = det.Produto.CFOP,
                    Unidade = det.Produto.UCom,
                    Quantidade = (double) det.Produto.QCom,
                    ValorUnitario = (double) det.Produto.VUnCom,
                    ValorTotal = (double) det.Produto.VProd,
                    InformacoesAdicionais = det.InfAdProd
                };

                var imposto = det.Imposto;
                if (imposto != null)
                {
                    var icms = imposto.Imposto;
                    if (icms != null)
                    {
                        var modelIcms = ObterIcms(icms);

                        produto.ValorIcms = modelIcms.VIcms;
                        produto.BaseIcms = modelIcms.VBc;
                        produto.AliquotaIcms = modelIcms.PIcms;
                        produto.OCst = modelIcms.OrigemMercadoria.GetDFeValue() + modelIcms.Cst + modelIcms.Csosn;
                    }

                    if (imposto.Ipi.Imposto is IpiTrib ipi)
                    {
                        produto.ValorIpi = (double?) ipi.VIpi;
                        produto.AliquotaIpi = (double?) ipi.PIpi;
                    }
                }

                model.Produtos.Add(produto);
            }

            #endregion

            #region Cobrança

            if (infNfe.Cobranca != null)
            {
                var duplicatas = infNfe.Cobranca.Duplicata.Select(x => new DuplicataViewModel
                {
                    Numero = x.NDup,
                    Valor = (double?) x.VDup,
                    Vecimento = x.DVenc
                });
                model.Duplicatas = duplicatas.ToList();
            }

            #endregion

            #region Total

            model.CalculoImposto = ObterTotalImposto(infNfe.Total.IcmsTot);

            var issqnTotal = infNfe.Total.IssqnTot;
            if (issqnTotal != null)
            {
                var c = model.CalculoIssqn;
                c.InscricaoMunicipal = infNfe.Emitente.IM;
                c.BaseIssqn = (double?) issqnTotal.VBc;
                c.ValorTotalServicos = (double?) issqnTotal.VServ;
                c.ValorIssqn = (double?) issqnTotal.VIss;
                c.Mostrar = true;
            }

            #endregion

            #region Transportadora

            var transp = infNfe.Transporte;
            var transportadora = transp.Transporta;
            var transportadoraModel = model.Transportadora;

            transportadoraModel.ModalidadeFrete = (int) transp.ModFrete;

            if (transp.VeicTransp != null)
            {
                transportadoraModel.VeiculoUf = transp.VeicTransp.UF;
                transportadoraModel.CodigoAntt = transp.VeicTransp.RNTC;
                transportadoraModel.Placa = transp.VeicTransp.Placa;
            }

            if (transportadora != null)
            {
                transportadoraModel.RazaoSocial = transportadora.XNome;
                transportadoraModel.EnderecoUf = transportadora.UF;
                transportadoraModel.CnpjCpf = !string.IsNullOrWhiteSpace(transportadora.Cnpj) ? transportadora.Cnpj : transportadora.Cpf;
                transportadoraModel.EnderecoLogadrouro = transportadora.XEnder;
                transportadoraModel.Municipio = transportadora.XMun;
                transportadoraModel.Ie = transportadora.IE;
            }

            var vol = transp.Vol.FirstOrDefault();
            if (vol != null)
            {
                transportadoraModel.QuantidadeVolumes = vol.QVol;
                transportadoraModel.Especie = vol.Especie;
                transportadoraModel.Marca = vol.Marca;
                transportadoraModel.Numeracao = vol.NVol;
                transportadoraModel.PesoBruto = (double?) vol.PesoB;
                transportadoraModel.PesoLiquido = (double?) vol.PesoL;
            }

            #endregion

            #region Informação Adicional

            var infAdic = infNfe.InformacaoAdicional;
            if (infAdic != null)
            {
                model.InformacoesComplementares = procNfe.NFe.InfNFe.InformacaoAdicional.InformacaoComplementar;
                model.InformacoesAdicionaisFisco = procNfe.NFe.InfNFe.InformacaoAdicional.InformacaoFisco;
            }

            #endregion

            var infoProt = procNfe.ProtNFe.InfProt;
            model.ProtocoloAutorizacao = string.Format(DanfeHelper.Cultura, "{0} - {1}", infoProt.NProt, infoProt.DhRecbto.DateTime);
            model.DataHoraEmissao = ide.DhEmi.DateTime;
            model.DataSaidaEntrada = ide.DhSaiEnt?.DateTime;

            if (model.DataSaidaEntrada.HasValue)
                model.HoraSaidaEntrada = model.DataSaidaEntrada?.TimeOfDay;

            // Contingência SVC-AN e SVC-RS
            if (model.TipoEmissao == TipoEmissao.SVCAN || model.TipoEmissao == TipoEmissao.SVCRS)
            {
                model.ContingenciaDataHora = ide.DhCont;
                model.ContingenciaJustificativa = ide.XJust;
            }

            return model;
        }

        #endregion

        #region Private methods

        private static EmpresaViewModel ObterEmitente(NFeEmit emit)
        {
            var model = new EmpresaViewModel
            {
                RazaoSocial = emit.XNome,
                CnpjCpf = !string.IsNullOrWhiteSpace(emit.CNPJ) ? emit.CNPJ : emit.CPF,
                Ie = emit.IE,
                IeSt = emit.IEST,
                IM = emit.IM,
                CRT = emit.CRT.GetDFeValue(),
                NomeFantasia = emit.XFant
            };

            var end = emit.Endereco;
            if (end != null)
            {
                model.EnderecoLogadrouro = end.Logradouro;
                model.EnderecoNumero = end.Numero;
                model.EnderecoBairro = end.Bairro;
                model.Municipio = end.Municipio;
                model.EnderecoUf = end.UF;
                model.EnderecoCep = end.CEP;
                model.Telefone = end.Fone;
                model.EnderecoComplemento = end.Complemento;
            }

            return model;
        }

        private static EmpresaViewModel ObterDestinatario(NFeDest dest)
        {
            var model = new EmpresaViewModel
            {
                RazaoSocial = dest.Nome,
                CnpjCpf = !string.IsNullOrWhiteSpace(dest.CNPJ) ? dest.CNPJ : dest.CPF,
                Ie = dest.IE,
                Email = dest.Email
            };

            var end = dest.Endereco;
            if (end != null)
            {
                model.EnderecoLogadrouro = end.Logradouro;
                model.EnderecoNumero = end.Numero;
                model.EnderecoBairro = end.Bairro;
                model.Municipio = end.Municipio;
                model.EnderecoUf = end.UF;
                model.EnderecoCep = end.CEP;
                model.Telefone = end.Fone;
                model.EnderecoComplemento = end.Complemento;
            }

            return model;
        }

        private static LocalEntregaRetiradaViewModel ObterLocalEntrega(NFeEntrega local)
        {
            var m = new LocalEntregaRetiradaViewModel
            {
                NomeRazaoSocial = local.Nome,
                CnpjCpf = !string.IsNullOrWhiteSpace(local.CNPJ) ? local.CNPJ : local.CPF,
                InscricaoEstadual = local.IE,
                Bairro = local.xBairro,
                Municipio = local.Municipio,
                Uf = local.UF,
                Cep = local.CEP,
                Telefone = local.Fone
            };

            var sb = new StringBuilder();
            sb.Append(local.Logradouro);

            if (!string.IsNullOrWhiteSpace(local.Numero))
                sb.Append(", ").Append(local.Numero);

            if (!string.IsNullOrWhiteSpace(local.Complemento))
                sb.Append(" - ").Append(local.Complemento);

            m.Endereco = sb.ToString();

            return m;
        }

        private static LocalEntregaRetiradaViewModel ObterLocalRetirada(NFeRetirada local)
        {
            var m = new LocalEntregaRetiradaViewModel
            {
                NomeRazaoSocial = local.Nome,
                CnpjCpf = !string.IsNullOrWhiteSpace(local.CNPJ) ? local.CNPJ : local.CPF,
                InscricaoEstadual = local.IE,
                Bairro = local.Bairro,
                Municipio = local.Municipio,
                Uf = local.UF,
                Cep = local.CEP,
                Telefone = local.Fone
            };

            var sb = new StringBuilder();
            sb.Append(local.Logradouro);

            if (!string.IsNullOrWhiteSpace(local.Numero))
                sb.Append(", ").Append(local.Numero);

            if (!string.IsNullOrWhiteSpace(local.Complemento))
                sb.Append(" - ").Append(local.Complemento);

            m.Endereco = sb.ToString();

            return m;
        }

        private static CalculoImpostoViewModel ObterTotalImposto(NFeIcmsTot total)
        {
            var model = new CalculoImpostoViewModel
            {
                ValorAproximadoTributos = (double?) total.VTotTrib,
                BaseCalculoIcms = (double) total.VBc,
                ValorIcms = (double) total.VIcms,
                BaseCalculoIcmsSt = (double) total.VBcSt,
                ValorIcmsSt = (double) total.VSt,
                ValorTotalProdutos = (double?) total.VProd,
                ValorFrete = (double) total.VFrete,
                ValorSeguro = (double) total.VSeg,
                Desconto = (double) total.VDesc,
                ValorII = (double) total.VII,
                ValorIpi = (double) total.VIpi,
                ValorPis = (double) total.VPis,
                ValorCofins = (double) total.VCofins,
                OutrasDespesas = (double) total.VOutro,
                ValorTotalNota = (double) total.VNf,
                vFCPUFDest = (double?) total.VFcpUfDest,
                vICMSUFDest = (double?) total.VIcmsUfDest,
                vICMSUFRemet = (double?) total.VIcmsUfRemet
            };

            return model;
        }

        private static ImpostoViewModel ObterIcms(INFeImposto icms)
        {
            var model = new ImpostoViewModel();

            try
            {
                var objeto = ((Icms) icms).Tipo;
                foreach (var property in objeto.GetType().GetProperties())
                {
                    var ok = property.Name;
                    switch (ok.ToLower())
                    {
                        case "origem":
                            model.OrigemMercadoria = (OrigemMercadoria) property.GetValue(objeto);
                            break;
                        case "cst":
                            model.Cst = property.GetValue(objeto).ToString();
                            break;
                        case "csosn":
                            model.Csosn = property.GetValue(objeto).ToString();
                            break;
                        case "vbc":
                            double.TryParse(property.GetValue(objeto).ToString(), out var vBc);
                            model.VBc = vBc;
                            break;
                        case "picms":
                            double.TryParse(property.GetValue(objeto).ToString(), out var pIcms);
                            model.PIcms = pIcms;
                            break;
                        case "vicms":
                            double.TryParse(property.GetValue(objeto).ToString(), out var vIcms);
                            model.VIcms = vIcms;
                            break;
                    }
                }

                return model;
            }
            catch
            {
                return model;
            }
        }

        #endregion
    }
}