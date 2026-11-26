using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using Vip.DFe.Controls;
using Vip.DFe.Exception;
using Vip.DFe.Extensions;
using Vip.DFe.NFe.Configuration;
using Vip.DFe.NFe.Enum;
using Vip.DFe.NFe.Evento;
using Vip.DFe.NFe.NotaFiscal;
using Vip.DFe.NFe.Protocolo;
using Vip.DFe.NFe.ServAutorizacao;
using Vip.DFe.NFe.ServConsultaProtocolo;
using Vip.DFe.NFe.ServInutilizacao;
using Vip.DFe.NFe.ServRecepcaoEvento;
using Vip.DFe.NFe.ServRetAutorizacao;
using Vip.DFe.NFe.ServStatusServico;

namespace Vip.DFe.NFe
{
    public sealed class NFeService : VipComponent
    {
        #region Fields

        private SecurityProtocolType _securityProtocol;
        private NFeStatus _status;
        private X509Certificate2 _certificado;

        #endregion Fields

        #region Events

        /// <summary>
        ///     Evento disparado quando o status do componente muda.
        /// </summary>
        public event EventHandler<EventArgs> StatusChanged;

        #endregion Events

        #region Propriedades

        /// <summary>
        ///     Retorna as configurações
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public NFeConfig Configuracoes { get; private set; }

        /// <summary>
        ///     Retorna a lista de NFes para processamento
        /// </summary>
        public NFeCollection Documentos { get; private set; }

        /// <summary>
        ///     Retorna a situação do componente
        /// </summary>
        public NFeStatus Status
        {
            get => _status;
            internal set
            {
                if (_status == value) return;

                _status = value;
                StatusChanged.Raise(this, EventArgs.Empty);
            }
        }

        #endregion Propriedades

        #region Public Methods

        /// TODO: Gerar método para compactar a mensagem
        /// <summary>
        ///     Método para enviar a(s) NFe(s) carregada(s) na coleção - AutorizacaoLote
        /// </summary>
        public NFeAutorizacaoResposta AutorizacaoLote(int loteId)
        {
            return AutorizacaoLote(loteId.ToString());
        }

        /// <summary>
        ///     Método para enviar a(s) NFe(s) carregada(s) na coleção - AutorizacaoLote
        /// </summary>
        public NFeAutorizacaoResposta AutorizacaoLote(string loteId = "")
        {
            return Executar(NFeStatus.Autorizacao, certificado =>
            {
                using var service = new NFeServAutorizacao(Configuracoes, certificado);
                return service.AutorizacaoLote(Documentos.NFe, loteId);
            }, AssinarEValidar);
        }

        /// <summary>
        ///     Método para enviar a primeira NFe carregada na coleção (padrão para NFCe) - Autorizacao
        /// </summary>
        public NFeAutorizacaoResposta Autorizacao()
        {
            var autorizacao = Executar(NFeStatus.Autorizacao, certificado =>
            {
                using var service = new NFeServAutorizacao(Configuracoes, certificado);
                return service.Autorizacao(Documentos.NFe.FirstOrDefault());
            }, AssinarEValidar);

            if (Configuracoes.EnviarModoSincrono) GerarNFeProc(autorizacao);
            return autorizacao;
        }

        /// <summary>
        ///     Método para consultar se NFe foi aprovada/processada - RetAutorizacao
        /// </summary>
        public NFeRetAutorizacaoResposta ConsultaAutorizacao(string recibo)
        {
            var retAutorizacao = Executar(NFeStatus.RetAutorizacao, certificado =>
            {
                using var service = new NFeServRetAutorizacao(Configuracoes, certificado);
                var tentativas = 0;
                var intervaloTentativas = Math.Max(Configuracoes.Webservices.IntervaloTentativas, 1000);
                NFeRetAutorizacaoResposta resposta;

                do
                {
                    resposta = service.RetAutorizacao(recibo);
                    tentativas++;
                    Thread.Sleep(intervaloTentativas);
                } while (resposta.Resultado.CStat != 104 && tentativas < Configuracoes.Webservices.NumeroTentativas);

                return resposta;
            });

            GerarNFeProc(retAutorizacao);
            return retAutorizacao;
        }

        /// <summary>
        ///     Metodo para checar a situação do serviço - StatusServicoNF
        /// </summary>
        public NFeStatusServicoResposta ConsultaSituacaoServico()
        {
            return Executar(NFeStatus.StatusServico, certificado =>
            {
                using var service = new NFeServStatusServico(Configuracoes, certificado);
                return service.StatusServico();
            });
        }

        /// <summary>
        ///     Método para checar a situação da NFe pela chave - ConsultaProtocolo (nfeConsultaNF)
        /// </summary>
        public NFeConsultaProtocoloResposta Consultar(string chave)
        {
            chave = chave.SomenteAlfanumerico();
            Guard.Against<ArgumentException>(chave.IsNullOrEmpty(), "ERRO: Nenhuma chave informada!");

            return Executar(NFeStatus.Consulta, certificado =>
            {
                using var service = new NFeServConsultaProtocolo(Configuracoes, certificado);
                var retornoConsulta = service.Consulta(chave);

                GerarNFeProc(retornoConsulta);

                return retornoConsulta;
            });
        }

        /// <summary>
        ///     Método para inutilizar uma faixa de numeração NFe.
        /// </summary>
        public NFeInutilizacaoResposta Inutilizar(string cnpj, string justificativa, NFeModelo modelo, int serie, int numeroInicial, int numeroFinal)
        {
            return Executar(NFeStatus.Inutilizacao, certificado =>
            {
                using var service = new NFeServInutilizacao(Configuracoes, certificado);
                return service.Inutilizar(cnpj, justificativa, modelo, serie, numeroInicial, numeroFinal);
            });
        }

        /// <summary>
        ///     Método para cancelar uma NFe
        /// </summary>
        public NFeRecepcaoEventoResposta Cancelar(string cnpj, string chave, string numeroProtocolo, int sequencialEvento, string justificativa)
        {
            #region Validação

            cnpj = cnpj.SomenteAlfanumerico();
            numeroProtocolo = numeroProtocolo.OnlyNumbers();

            Guard.Against<ArgumentException>(cnpj.IsNullOrEmpty(), "ERRO: Informe o CNPJ ou CPF");
            Guard.Against<ArgumentNullException>(chave.IsNullOrEmpty(), "ERRO: Informe a Chave");
            Guard.Against<ArgumentNullException>(numeroProtocolo.IsNullOrEmpty(), "ERRO: Informe o número do protocolo");
            Guard.Against<ArgumentNullException>(justificativa.IsNullOrEmpty(), "ERRO: Informe a justificativa do cancelamento");
            Guard.Against<ArgumentException>(justificativa.Length < 15, "ERRO: Justificativa deve ter mais de 15 caracteres");

            #endregion

            var evento = new NFeEvento
            {
                Versao = NFeVersao.v100,
                InfEvento = new NFeInfEvento
                {
                    COrgao = Configuracoes.Webservices.UF,
                    TpAmb = Configuracoes.Ambiente,
                    Documento = cnpj,
                    Chave = chave.TrimVip(),
                    DhEvento = DateTimeOffset.Now,
                    TpEvento = NFeTipoEvento.Cancelamento,
                    NSeqEvento = sequencialEvento,
                    VerEvento = NFeVersao.v100,
                    DetEvento = new NFeDetEvento
                    {
                        Versao = NFeVersao.v100,
                        DescEvento = DFeConstantes.Cancelamento,
                        NProt = numeroProtocolo,
                        XJust = justificativa
                    }
                }
            };

            return RecepcaoEvento(evento);
        }

        /// <summary>
        ///     Método para cancelar uma NFe por substituição
        /// </summary>
        public NFeRecepcaoEventoResposta CancelarSubstituicao(string cnpj, string chave, string numeroProtocolo, int sequencialEvento, string justificativa, string chaveReferenciada, string versaoAplicacao)
        {
            #region Validação

            cnpj = cnpj.SomenteAlfanumerico();
            numeroProtocolo = numeroProtocolo.OnlyNumbers();
            versaoAplicacao = versaoAplicacao.IsNotNullOrEmpty() ? versaoAplicacao.TrimVip() : "1.0";

            Guard.Against<ArgumentException>(cnpj.IsNullOrEmpty(), "ERRO: Informe o CNPJ ou CPF");
            Guard.Against<ArgumentNullException>(chave.IsNullOrEmpty(), "ERRO: Informe a Chave");
            Guard.Against<ArgumentNullException>(chaveReferenciada.IsNullOrEmpty(), "ERRO: Informe a Chave Referenciada, substituta à cancelada");
            Guard.Against<ArgumentNullException>(numeroProtocolo.IsNullOrEmpty(), "ERRO: Informe o número do protocolo");
            Guard.Against<ArgumentNullException>(justificativa.IsNullOrEmpty(), "ERRO: Informe a justificativa do cancelamento");
            Guard.Against<ArgumentException>(justificativa.Length < 15, "ERRO: Justificativa deve ter mais de 15 caracteres");
            Guard.Against<ArgumentException>(justificativa.Length > 255, "ERRO: Justificativa deve ter menos de 255 caracteres caracteres");

            #endregion

            var evento = new NFeEvento
            {
                Versao = NFeVersao.v100,
                InfEvento = new NFeInfEvento
                {
                    VerEvento = NFeVersao.v100,
                    COrgao = Configuracoes.Webservices.UF,
                    TpAmb = Configuracoes.Ambiente,
                    Documento = cnpj,
                    Chave = chave.TrimVip(),
                    DhEvento = DateTimeOffset.Now,
                    TpEvento = NFeTipoEvento.CancelamentoST,
                    NSeqEvento = sequencialEvento,
                    DetEvento = new NFeDetEvento
                    {
                        Versao = NFeVersao.v100,
                        DescEvento = DFeConstantes.CancelamentoSubstituicao,
                        COrgaoAutor = Configuracoes.Webservices.UF,
                        TipoAutor = NFeTipoAutor.EmpresaEmitente,
                        VerAplic = versaoAplicacao,
                        NProt = numeroProtocolo,
                        XJust = justificativa,
                        ChaveRef = chaveReferenciada
                    }
                }
            };

            return RecepcaoEvento(evento);
        }

        /// <summary>
        ///     Método para gerar um CCe
        /// </summary>
        public NFeRecepcaoEventoResposta CartaCorrecao(string cnpj, string chave, int sequencialEvento, string correcao)
        {
            #region Validação

            cnpj = cnpj.SomenteAlfanumerico();
            chave = chave.SomenteAlfanumerico();

            Guard.Against<ArgumentException>(cnpj.IsNullOrEmpty(), "ERRO: Informe o CNPJ ou CPF");
            Guard.Against<ArgumentNullException>(chave.IsNullOrEmpty(), "ERRO: Informe a Chave");
            Guard.Against<ArgumentNullException>(correcao.IsNullOrEmpty(), "ERRO: Informe o texto da correção da CC-e");
            Guard.Against<ArgumentException>(correcao.Length < 15, "ERRO: A correção da CC-e deve ter mais de 15 caracteres");
            Guard.Against<ArgumentException>(correcao.Length > 1000, "ERRO: A correção da CC-e deve ter menos de 1000 caracteres");

            #endregion

            var evento = new NFeEvento
            {
                Versao = NFeVersao.v100,
                InfEvento = new NFeInfEvento
                {
                    COrgao = Configuracoes.Webservices.UF,
                    TpAmb = Configuracoes.Ambiente,
                    Documento = cnpj,
                    Chave = chave.TrimVip(),
                    DhEvento = DateTimeOffset.Now,
                    TpEvento = NFeTipoEvento.CartaCorrecao,
                    NSeqEvento = sequencialEvento,
                    VerEvento = NFeVersao.v100,
                    DetEvento = new NFeDetEvento
                    {
                        Versao = NFeVersao.v100,
                        DescEvento = DFeConstantes.CartaCorrecao,
                        XCorrecao = correcao.TrimVip(),
                        XCondUso = DFeConstantes.CondicaoUso
                    }
                }
            };

            return RecepcaoEvento(evento);
        }

        /// <summary>
        ///     Assina os documentos adicionados na biblioteca
        /// </summary>
        /// <returns>VipException - Caso haja validações</returns>
        public void Assinar(X509Certificate2 certificado = null)
        {
            if (certificado.IsNull()) certificado = Configuracoes.Certificado.ObterCertificado();
            Documentos.Assinar(certificado, Configuracoes.ObterOptions());
        }

        #endregion Methods

        #region Private Methods

        /// <summary>
        ///     Valida os documentos adicionados através do Schema.
        /// </summary>
        /// <returns>VipException - Caso haja validações</returns>
        public void Validar()
        {
            Documentos.Validar();
        }

        /// <summary>
        ///     Executa uma operação de serviço: valida configurações, ajusta o protocolo de segurança,
        ///     obtém o certificado, altera o status e restaura tudo ao final.
        /// </summary>
        /// <param name="status">Status a ser aplicado durante a operação (nulo mantém o atual).</param>
        /// <param name="acao">Operação a ser executada com o certificado.</param>
        /// <param name="preparar">Ação executada após obter o certificado e antes do bloco protegido.</param>
        private T Executar<T>(NFeStatus? status, Func<X509Certificate2, T> acao, Action<X509Certificate2> preparar = null)
        {
            Configuracoes.Validar();

            var oldProtocol = ServicePointManager.SecurityProtocol;
            ServicePointManager.SecurityProtocol = _securityProtocol;
            _certificado = Configuracoes.Certificado.ObterCertificado();

            preparar?.Invoke(_certificado);

            try
            {
                if (status.HasValue) Status = status.Value;
                return acao(_certificado);
            }
            finally
            {
                LiberarCacheCertificado();
                ServicePointManager.SecurityProtocol = oldProtocol;
                Status = NFeStatus.EmEspera;
            }
        }

        private void AssinarEValidar(X509Certificate2 certificado)
        {
            Assinar(certificado);
            Validar();
        }

        /// <summary>
        ///     Confere o DigestValue do documento com o retornado no protocolo.
        /// </summary>
        private void ValidarDigest(NFeProtNFe protNFe, NotaFiscal.NFe nfe)
        {
            if (!Configuracoes.ValidarDigest) return;
            Guard.Against<VipException>(protNFe.InfProt.DigVal.IsNotNullOrEmpty() && protNFe.InfProt.DigVal != nfe.Signature.SignedInfo.Reference.DigestValue, $"DigestValue do documento {nfe.InfNFe.Id} não confere.");
        }

        /// <summary>
        ///     Metodo para enviar eventos da NFe.
        /// </summary>
        private NFeRecepcaoEventoResposta RecepcaoEvento(NFeEvento evento)
        {
            Guard.Against<ArgumentNullException>(evento == null, "ERRO: Evento não informado. Por favor, tente novamente");

            NFeStatus? status = null;
            switch (evento.InfEvento.TpEvento)
            {
                case NFeTipoEvento.Cancelamento:
                    status = NFeStatus.Cancelamento;
                    break;
                case NFeTipoEvento.CancelamentoST:
                    status = NFeStatus.CancelamentoST;
                    break;
                case NFeTipoEvento.CartaCorrecao:
                    status = NFeStatus.CCe;
                    break;
            }

            var recepcaoEventoResposta = Executar(status, certificado =>
            {
                using var service = new NFeServRecepcaoEvento(Configuracoes, certificado);
                return service.RecepcaoEvento(evento);
            });

            GerarProcEvento(recepcaoEventoResposta, evento);
            return recepcaoEventoResposta;
        }

        /// <summary>
        ///     Gera classe procNFe via RetAutorizacao / Lote
        /// </summary>
        private void GerarNFeProc(NFeRetAutorizacaoResposta retAutorizacao)
        {
            if (retAutorizacao.Resultado.CStat != 104 || !retAutorizacao.Resultado.ProtNFe.Any()) return;

            var autorizadas = new List<NFeProc>();
            foreach (var protNFe in retAutorizacao.Resultado.ProtNFe)
            {
                var nfe = Documentos.NFe.SingleOrDefault(x => x.InfNFe.Id.Substring(3) == protNFe.InfProt.ChNFe);
                if (nfe.IsNull()) continue;

                ValidarDigest(protNFe, nfe);

                var nfeProc = new NFeProc {Versao = nfe.InfNFe.Versao, NFe = nfe, ProtNFe = protNFe};
                if (nfeProc.Processado)
                {
                    autorizadas.Add(nfeProc);
                    nfeProc.Gravar(Configuracoes);
                }
            }

            retAutorizacao.NFeAutorizadas = autorizadas.ToArray();
        }

        /// <summary>
        ///     Gera classe procNFe via autorizacao / síncrono
        /// </summary>
        private void GerarNFeProc(NFeAutorizacaoResposta autorizacao)
        {
            if (autorizacao.Resultado.CStat != 104 || autorizacao.Resultado.ProtNFe.IsNull()) return;

            var protNFe = autorizacao.Resultado.ProtNFe;

            var nfe = Documentos.NFe.FirstOrDefault();
            if (nfe.IsNull()) return;

            ValidarDigest(protNFe, nfe);

            var nfeProc = new NFeProc {Versao = nfe.InfNFe.Versao, NFe = nfe, ProtNFe = protNFe};
            if (nfeProc.Processado)
            {
                autorizacao.NFeAutorizada = nfeProc;
                nfeProc.Gravar(Configuracoes);
            }
        }

        /// <summary>
        ///     Gera classe procNFe via consulta de protocolo
        /// </summary>
        private void GerarNFeProc(NFeConsultaProtocoloResposta consulta)
        {
            if (consulta.Resultado.ProtNFe.IsNull()) return;
            if (consulta.Resultado.CStat != 100 && consulta.Resultado.CStat != 150) return;

            var protNFe = consulta.Resultado.ProtNFe;

            var nfe = Documentos.NFe.FirstOrDefault();
            if (nfe.IsNull()) return;

            ValidarDigest(protNFe, nfe);

            var nfeProc = new NFeProc {Versao = nfe.InfNFe.Versao, NFe = nfe, ProtNFe = protNFe};
            if (nfeProc.Processado)
            {
                consulta.NFeAutorizada = nfeProc;
                nfeProc.Gravar(Configuracoes);
            }
        }

        /// <summary>
        ///     Gera classe procEvento
        /// </summary>
        private void GerarProcEvento(NFeRecepcaoEventoResposta recepcaoEventoResposta, NFeEvento evento)
        {
            if (recepcaoEventoResposta.Resultado.CStat != 128) return;

            var procEvento = new NFeProcEvento
            {
                Versao = recepcaoEventoResposta.Resultado.Versao,
                RetEvento = recepcaoEventoResposta.Resultado.RetEvento.FirstOrDefault(),
                Evento = evento
            };
            recepcaoEventoResposta.ProcEvento = procEvento;
            if (procEvento.Processado) procEvento.Gravar(Configuracoes);
        }

        private void LiberarCacheCertificado()
        {
            if (Configuracoes.Certificado.ManterEmCache) return;
            _certificado?.Reset();
        }

        #endregion

        #region Override

        protected override void OnInitialize()
        {
            Status = NFeStatus.EmEspera;
            _securityProtocol = SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
            Configuracoes = new NFeConfig(this);
            Documentos = new NFeCollection(this);
        }

        protected override void OnDisposing()
        {
            _certificado?.Reset();
        }

        #endregion
    }
}