using System;
using System.Collections.Generic;
using System.Linq;
using Vip.DFe.Document;
using Vip.DFe.Extensions;
using Vip.DFe.NFe.Enum;
using Vip.DFe.NFe.NotaFiscal.Cobranca;
using Vip.DFe.NFe.NotaFiscal.Destinatario;
using Vip.DFe.NFe.NotaFiscal.Detalhe;
using Vip.DFe.NFe.NotaFiscal.Emitente;
using Vip.DFe.NFe.NotaFiscal.Identificacao;
using Vip.DFe.NFe.NotaFiscal.InformacaoAdicional;
using Vip.DFe.NFe.NotaFiscal.Pagamento;
using Vip.DFe.NFe.NotaFiscal.Total;
using Vip.DFe.NFe.NotaFiscal.Transporte;
using Vip.DFe.Shared.Enum;

namespace Vip.DFe.Demo
{
    public class DadosNFe
    {
        private readonly int numeroNF;
        public readonly int serie;
        private readonly TipoAmbiente ambiente;
        private readonly NFeModelo modeloDoc;
        private readonly NFeFinalidade finalidade;

        public DadosNFe(TipoAmbiente ambiente, NFeModelo modeloDoc, NFeFinalidade finalidade, int serie, int numeroNF)
        {
            this.serie = serie;
            this.numeroNF = numeroNF;
            this.ambiente = ambiente;
            this.modeloDoc = modeloDoc;
            this.finalidade = finalidade;
        }

        private string GerarCNF()
        {
            int seed = numeroNF + serie;
            Random random = new Random(seed);
            string result = random.Next().ToString();
            if (result.Length > 8) result = result.Substring(0, 8);
            return result;
        }

        public NFeIde GetIdentificacao()
        {
            /*
             * ATENÇÃO:
             * 
             * cNF DEVE SER GERADO RANDOMICAMENTE!!!RS
             * */

            var ide = new NFeIde
            {
                CodigoUF = CodigoUF.SP,
                NatOp = "VENDA DE MERCADORIAS",
                Modelo = modeloDoc, //NF-e / NFC-e
                Serie = serie,
                NumeroNFe = numeroNF,
                TipoNFe = NFeTipo.Saida, // Entrada / Saida
                CMunFG = 3556800, // Viradouro
                TipoEmissao = TipoEmissao.Normal,
                CNf = GerarCNF(),
                TpAmb = TipoAmbiente.Homologacao,
                FinNFe = NFeFinalidade.Normal,
                VerProc = "4.000",
                DhEmi = DateTime.Now, //data da EMISSAO da NF
                //data da SAIDA da NF do estabelecimento
                IdDest = NFeDestinoOperacao.Interna,
                ProcEmi = ProcessoEmissao.AplicativoContribuinte,
                IndFinal = NFeConsumidorFinal.Sim,
            };

            //APENAS NFCe!!!
            //if (ide.tpEmis != TipoEmissao.teNormal)
            //{
            //    ide.DhCont = ide.dhEmi; //data da CONTINGENCIA
            //    ide.xJust = "TESTE DE CONTIGÊNCIA PARA NFe/NFCe";
            //}

            //if (ide.mod == ModeloDocumento.NFCe)
            //{
            //    ide.tpImp = TipoImpressao.tiNFCe;
            //    ide.indPres = PresencaComprador.pcPresencial; //NFCe: deve ser 1 ou 4
            //}
            //else
            //{
            ide.TipoImpressao = TipoImpressao.NormalRetrato;
            ide.IndPres = NFePresencaComprador.Nao;
            ide.DhSaiEnt = DateTime.Now;
            //}

            return ide;
        }

        public NFeEmit GetEmitente()
        {
            var emit = new NFeEmit();
            emit.XNome = "EMPRESA TESTE LTDA";
            emit.XFant = "EMPRESA TESTE";
            emit.CNPJ = "12332134000199";
            emit.CRT = RegimeTributario.SimplesNacional;
            emit.IE = "715025640119";
            emit.Endereco = new NFeEmitEndereco
            {
                Logradouro = "AV SAO JOAO",
                Numero = "10",
                Bairro = "BAIRRO",
                CodigoIBGE = 3556800,
                Municipio = "VIRADOURO",
                UF = CodigoUF.SP.GetDescription(),
                CEP = "14740000",
                CodigoPais = 1058,
                Pais = "BRASIL",
                Fone = "0000000000"
            };

            return emit;
        }

        internal NFeCobranca GetFinanceiroNota(decimal vNF, decimal vDescTotalNF)
        {
            var cobr = new NFeCobranca();
            cobr.Fatura = new NFeCobrFatura();
            cobr.Duplicata = new DFeCollection<NFeCobrDuplicata>();

            cobr.Fatura.VOrig = vNF;
            cobr.Fatura.VLiq = vNF - vDescTotalNF;
            cobr.Fatura.NFat = "1";
            if (vDescTotalNF > 0)
                cobr.Fatura.VDesc = vDescTotalNF;

            const int nParc = 2;

            for (int i = 0; i < nParc; i++)
                cobr.Duplicata.Add(new NFeCobrDuplicata
                {
                    NDup = nParc.ToString().PadLeft(3, '0'),
                    DVenc = new DateTime(2019, 08, 30),
                    VDup = cobr.Fatura.VLiq / nParc
                });

            return cobr;
        }

        internal NFeInformacaoAdicional GetInformacoesComplementares()
        {
            return new NFeInformacaoAdicional
            {
                InformacaoComplementar = "PRAZO PARA TROCA DE ATÉ 7 DIAS. OBRIGADO PELA PREFERENCIA."
            };
        }

        public NFeTotal GetTotal(List<NFeDetalhe> produtos)
        {
            decimal totalItens = produtos.Where(p => p.Produto.IndTot == NFeIndTotal.ValorItemCompoeTotalNota).Sum(p => p.Produto.VProd);
            decimal totalDesconto = produtos.Where(p => p.Produto.IndTot == NFeIndTotal.ValorItemCompoeTotalNota).Sum(p => p.Produto.VDesc);
            decimal totalOutros = produtos.Where(p => p.Produto.IndTot == NFeIndTotal.ValorItemCompoeTotalNota).Sum(e => e.Produto.VOutro);
            decimal totalTrib = produtos.Where(p => p.Produto.IndTot == NFeIndTotal.ValorItemCompoeTotalNota).Sum(p => p.Imposto.VTotTrib);

            //Totalizadores de impostos deverão vir da camada de dados do sistema
            decimal ValorFCPST = 0;
            decimal ValorIcmsST = 0;
            decimal ValorIPI = 0;
            decimal ValorFrete = 0;
            decimal ValorOutro = 0;
            decimal ValorSeguro = 0;
            decimal ValorPIS = 0;
            decimal ValorCOFINS = 0;
            decimal ValorFCP = 0;
            decimal ValorFCPSTRet = 0;

            var icmsTot = new NFeIcmsTot
            {
                VBcSt = 0,
                VSt = 0,
                VBc = 0,
                VIcms = 0,
                VProd = 100,
                VNf = totalItens - totalDesconto + ValorFCPST + ValorIcmsST + ValorIPI + ValorFrete + ValorOutro + ValorSeguro,
                VDesc = totalDesconto,
                VTotTrib = totalTrib,
                VPis = ValorPIS,
                VCofins = ValorCOFINS,
                VIpi = ValorIPI,
                VSeg = ValorSeguro,
                VOutro = ValorOutro,
                VFcp = ValorFCP,
                VFcpSt = ValorFCPST,
                VFcpStRet = ValorFCPSTRet,
                VIpiDevol = 0,
                VFrete = ValorFrete,
            };

            icmsTot.VIcmsDeson = 0;

            var total = new NFeTotal {IcmsTot = icmsTot};
            return total;
        }

        public NFeDest GetDestinatario()
        {
            bool pessoaFisica = true;
            bool clienteTemEmail = false;

            var dest = new NFeDest();
            if (pessoaFisica)
            {
                dest.CPF = "33974986863";
            }
            else
            {
                dest.CNPJ = "12332134000199";
                dest.IE = "715025640119";
            }

            dest.Nome = "LEANDRO FERREIRA";
            //dest.Nome = ambiente == TipoAmbiente.Homologacao
            //    ? "NF-E EMITIDA EM AMBIENTE DE HOMOLOGACAO - SEM VALOR FISCAL"
            //    : "Nome do cliente";

            dest.Endereco = new NFeDestEndereco
            {
                Logradouro = "RUA DE EXEMPLO",
                Numero = "10",
                Bairro = "JARDIM PRIMAVERA",
                CodigoIBGE = 3556800,
                Municipio = "VIRADOURO",
                UF = "SP",
                CEP = "14740000",
                CodigoPais = 1058,
                Pais = "BRASIL",
                Fone = "0000000000"
            };

            dest.IndIEDest = NFeIndIeDest.NaoContribuinte;

            if (clienteTemEmail)
                dest.Email = "email@cliente.com";

            return dest;
        }

        public NFeTransporte GetTransporte()
        {
            if (modeloDoc == NFeModelo.NFCe)
                return new NFeTransporte {ModFrete = NFeModalidadeFrete.SemFrete};

            return GetDadosTransporte();
        }

        private NFeTransporte GetDadosTransporte()
        {
            var transp = new NFeTransporte();
            transp.ModFrete = NFeModalidadeFrete.ContratacaoDestinatarioFOB;

            if (transp.ModFrete != NFeModalidadeFrete.SemFrete)
                transp.Transporta = new NFeTransporta
                {
                    XNome = "NOME DA TRANSPORTADORA",
                    Cnpj = "12332134000199",
                    //CPF = "124548784",
                    //IE = "124584",
                    UF = "SP",
                    XEnder = "ENDERECO TRANSPORTADORA",
                    XMun = "VIRADOURO"
                };
            /*transp.retTransp = new retTransp()
                {
                    CFOP = 5545,
                    cMunFG = 3306305,
                    pICMSRet = 0,
                    vBCRet = 0,
                    vICMSRet = 0,
                    vServ = 50
                };*/

            //daqui pra baixo, TUDO VEM DO BANCO
            transp.Vol = new DFeCollection<NFeVolTransp>();

            var vol = new NFeVolTransp();
            vol.Especie = "ESPECIE";
            vol.Marca = "MARCA";
            vol.NVol = "001";
            vol.QVol = 2;

            //vol.Lacres = new List<lacres>();
            //vol.Lacres.Add(new lacres
            //{
            //    nLacre = "ATPC-78855"
            //});

            vol.PesoB = 6.498m;
            vol.PesoL = 6.275M;
            transp.Vol.Add(vol);

            return transp;
        }

        public List<NFeNfRef> GetNotasReferenciadas()
        {
            var notasRefs = new List<NFeNfRef>();

            //notasRefs.Add(new NFeNfRef
            //{
            //    RefNFe = "chave de acesso da NF referenciada para devolução"
            //});

            //notasRefs.Add(new NFeNfRef
            //{
            //    RefNFe = "outra chave de acesso de uma outra NF referenciada para devolução"
            //});

            return notasRefs;
        }

        public DFeCollection<NFeDetalhe> GetDetalhesProdutosNF()
        {
            var produto = new DadosProduto(ambiente, modeloDoc);
            var listaDetalhes = new DFeCollection<NFeDetalhe>();

            /*
             * Em um cenário real, aqui é feito um
             * foreach nos itens vindos do banco para preencher os
             * objetos de detalhe e imposto
             * */

            var itemDetalhe = new NFeDetalhe
            {
                NItem = 1,
                Produto = produto.GetProduto(),
                Imposto = produto.GetImposto()
            };

            if (modeloDoc == NFeModelo.NFCe)
            {
                decimal federal = 15; //deverá buscar da tabela IBPT, com base no NCM do item
                decimal estadual = 17; //deverá buscar da tabela IBPT, com base no NCM do item
                decimal municipal = 0; //deverá buscar da tabela IBPT, com base no NCM do item

                decimal vProd = itemDetalhe.Produto.VProd;
                itemDetalhe.Imposto.VTotTrib = vProd * (federal + estadual + municipal) / 100;
            }

            listaDetalhes.Add(itemDetalhe);
            return listaDetalhes;
        }

        internal NFePagamento GetPagamentos()
        {
            var pag = new NFePagamento();
            pag.DetPag = new DFeCollection<NFeDetPag>();

            if (modeloDoc == NFeModelo.NFe && finalidade == NFeFinalidade.Devolucao)
            {
                pag.DetPag.Add(new NFeDetPag {TPag = MeioPagamento.SemPagamento}); //NAO SE FAZ PAGAMENTO PARA NF DE DEVOLUÇÃO!!!!
            }
            else
            {
                var detalhesPag = GetDetalhesPagamento();
                pag.DetPag.Add(detalhesPag);
            }

            return pag;
        }

        private NFeDetPag GetDetalhesPagamento()
        {
            decimal valorPagamento = 100; //vindo do banco
            decimal valorOutro = 0;

            valorPagamento -= valorOutro; //ValorOutros deve ser subtraido do valor do pagamento, senão dá problema de troco

            var pag = new NFeDetPag();
            var TipoPagamento = "DINHEIRO"; //vindo do banco;

            switch (TipoPagamento)
            {
                case "DINHEIRO":
                    pag.TPag = MeioPagamento.Dinheiro;
                    pag.VPag = valorPagamento; //DEVE SER SUBTRAIDO O TROCO NESTE VALOR!!!

                    if (modeloDoc == NFeModelo.NFe)
                        pag.IndPag = NFeIndFormaPagamento.PagamentoVista;
                    break;

                case "CARTAO":
                    pag.TPag = MeioPagamento.CartaodeCredito;
                    pag.VPag = valorPagamento;

                    if (modeloDoc == NFeModelo.NFe)
                        pag.IndPag = NFeIndFormaPagamento.PagamentoPrazo;

                    pag.Card = new NFeCardPag
                    {
                        TpIntegra = NFeTipoIntegracaoPagamento.PagamentoNaoIntegradoPOS
                    };
                    break;

                case "CREDITO":
                    pag.TPag = MeioPagamento.CreditoLoja;
                    pag.VPag = valorPagamento;

                    if (modeloDoc == NFeModelo.NFe)
                        pag.IndPag = NFeIndFormaPagamento.PagamentoVista;
                    break;

                case "PRAZO": //o famoso "fiado"
                    pag.TPag = MeioPagamento.Outros;
                    pag.VPag = valorPagamento;

                    if (modeloDoc == NFeModelo.NFe)
                        pag.IndPag = NFeIndFormaPagamento.PagamentoPrazo;
                    break;

                case "CHEQUE":
                    pag.TPag = MeioPagamento.Cheque;
                    pag.VPag = valorPagamento;

                    if (modeloDoc == NFeModelo.NFe)
                        pag.IndPag = NFeIndFormaPagamento.PagamentoPrazo;
                    break;
            }

            return pag;
        }
    }
}