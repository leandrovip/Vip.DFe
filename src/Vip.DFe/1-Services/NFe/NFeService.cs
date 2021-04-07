using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using Vip.DFe.Control;
using Vip.DFe.Exception;
using Vip.DFe.Extensions;
using Vip.DFe.NFe.Configuration;
using Vip.DFe.NFe.Enum;
using Vip.DFe.NFe.Evento;
using Vip.DFe.NFe.NotaFiscal;
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

        #region Methods

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
            Configuracoes.Validar();

            var oldProtocol = ServicePointManager.SecurityProtocol;
            ServicePointManager.SecurityProtocol = _securityProtocol;
            _certificado = Configuracoes.Certificado.ObterCertificado();

            Assinar(_certificado);
            Validar();

            NFeAutorizacaoResposta autorizacao;

            try
            {
                Status = NFeStatus.Autorizacao;
                using var service = new NFeServAutorizacao(Configuracoes, _certificado);
                autorizacao = service.AutorizacaoLote(Documentos.NFe, loteId);
            }
            finally
            {
                LiberarCacheCertificado();
                ServicePointManager.SecurityProtocol = oldProtocol;
                Status = NFeStatus.EmEspera;
            }

            return autorizacao;
        }

        /// <summary>
        ///     Método para consultar se NFe foi aprovada/processada - RetAutorizacao
        /// </summary>
        public NFeRetAutorizacaoResposta ConsultaAutorizacao(string recibo)
        {
            Configuracoes.Validar();

            var oldProtocol = ServicePointManager.SecurityProtocol;
            ServicePointManager.SecurityProtocol = _securityProtocol;
            _certificado = Configuracoes.Certificado.ObterCertificado();

            NFeRetAutorizacaoResposta retAutorizacao;

            try
            {
                Status = NFeStatus.RetAutorizacao;
                using var service = new NFeServRetAutorizacao(Configuracoes, _certificado);
                var tentativas = 0;
                var intervaloTentativas = Math.Max(Configuracoes.Webservices.IntervaloTentativas, 1000);

                do
                {
                    retAutorizacao = service.RetAutorizacao(recibo);
                    tentativas++;
                    Thread.Sleep(intervaloTentativas);
                } while (retAutorizacao.Resultado.CStat != 104 && tentativas < Configuracoes.Webservices.NumeroTentativas);
            }
            finally
            {
                LiberarCacheCertificado();
                ServicePointManager.SecurityProtocol = oldProtocol;
                Status = NFeStatus.EmEspera;
            }

            GerarNFeProc(retAutorizacao);
            return retAutorizacao;
        }

        /// <summary>
        ///     Metodo para checar a situação do serviço - StatusServicoNF
        /// </summary>
        public NFeStatusServicoResposta ConsultaSituacaoServico()
        {
            Configuracoes.Validar();

            var oldProtocol = ServicePointManager.SecurityProtocol;
            ServicePointManager.SecurityProtocol = _securityProtocol;
            _certificado = Configuracoes.Certificado.ObterCertificado();

            try
            {
                Status = NFeStatus.StatusServico;
                using var service = new NFeServStatusServico(Configuracoes, _certificado);
                return service.StatusServico();
            }
            finally
            {
                LiberarCacheCertificado();
                ServicePointManager.SecurityProtocol = oldProtocol;
                Status = NFeStatus.EmEspera;
            }
        }

        /// <summary>
        ///     Método para checar a situação da NFe pela chave - ConsultaProtocolo (nfeConsultaNF)
        /// </summary>
        public NFeConsultaProtocoloResposta Consultar(string chave)
        {
            chave = chave.OnlyNumbers();
            Guard.Against<ArgumentException>(chave.IsNullOrEmpty(), "ERRO: Nenhuma chave informada!");

            Configuracoes.Validar();

            var oldProtocol = ServicePointManager.SecurityProtocol;
            ServicePointManager.SecurityProtocol = _securityProtocol;
            _certificado = Configuracoes.Certificado.ObterCertificado();

            try
            {
                Status = NFeStatus.Consulta;
                using var service = new NFeServConsultaProtocolo(Configuracoes, _certificado);
                return service.Consulta(chave);
            }
            finally
            {
                LiberarCacheCertificado();
                ServicePointManager.SecurityProtocol = oldProtocol;
                Status = NFeStatus.EmEspera;
            }
        }

        /// <summary>
        ///     Método para inutilizar uma faixa de numeração NFe.
        /// </summary>
        public NFeInutilizacaoResposta Inutilizar(string cnpj, string justificativa, NFeModelo modelo, int serie, int numeroInicial, int numeroFinal)
        {
            Configuracoes.Validar();

            var oldProtocol = ServicePointManager.SecurityProtocol;
            ServicePointManager.SecurityProtocol = _securityProtocol;
            _certificado = Configuracoes.Certificado.ObterCertificado();

            try
            {
                Status = NFeStatus.Inutilizacao;
                using var service = new NFeServInutilizacao(Configuracoes, _certificado);
                return service.Inutilizar(cnpj, justificativa, modelo, serie, numeroInicial, numeroFinal);
            }
            finally
            {
                LiberarCacheCertificado();
                ServicePointManager.SecurityProtocol = oldProtocol;
                Status = NFeStatus.EmEspera;
            }
        }

        /// <summary>
        ///     Método para cancelar uma NFe
        /// </summary>
        public NFeRecepcaoEventoResposta Cancelar(string cnpj, string chave, string numeroProtocolo, int sequencialEvento, string justificativa)
        {
            #region Validação

            cnpj = cnpj.OnlyNumbers();
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
        ///     Método para gerar um CCe
        /// </summary>
        public NFeRecepcaoEventoResposta CartaoCorrecao(string cnpj, string chave, int sequencialEvento, string correcao)
        {
            #region Validação

            cnpj = cnpj.OnlyNumbers();
            chave = chave.OnlyNumbers();

            Guard.Against<ArgumentException>(cnpj.IsNullOrEmpty(), "ERRO: Informe o CNPJ ou CPF");
            Guard.Against<ArgumentNullException>(chave.IsNullOrEmpty(), "ERRO: Informe a Chave");
            Guard.Against<ArgumentNullException>(correcao.IsNullOrEmpty(), "ERRO: Informe a justificativa do cancelamento");
            Guard.Against<ArgumentException>(correcao.Length < 15, "ERRO: Justificativa deve ter mais de 15 caracteres");
            Guard.Against<ArgumentException>(correcao.Length > 1000, "ERRO: Justificativa deve ter menos de 1000 caracteres");

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
                        DescEvento = DFeConstantes.CartaoCorecao,
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

        /// <summary>
        ///     Valida os documentos adicionados através do Schema.
        /// </summary>
        /// <returns>VipException - Caso haja validações</returns>
        public void Validar()
        {
            Documentos.Validar();
        }

        /// <summary>
        ///     Metodo para enviar eventos da NFe.
        /// </summary>
        private NFeRecepcaoEventoResposta RecepcaoEvento(NFeEvento evento)
        {
            Guard.Against<ArgumentNullException>(evento == null, "ERRO: Evento não informado. Por favor, tente novamente");

            Configuracoes.Validar();

            var oldProtocol = ServicePointManager.SecurityProtocol;

            ServicePointManager.SecurityProtocol = _securityProtocol;
            _certificado = Configuracoes.Certificado.ObterCertificado();

            NFeRecepcaoEventoResposta recepcaoEventoResposta;

            try
            {
                switch (evento.InfEvento.TpEvento)
                {
                    case NFeTipoEvento.Cancelamento:
                        Status = NFeStatus.Cancelamento;
                        break;
                    case NFeTipoEvento.CartaCorrecao:
                        Status = NFeStatus.CCe;
                        break;
                }

                using var service = new NFeServRecepcaoEvento(Configuracoes, _certificado);
                recepcaoEventoResposta = service.RecepcaoEvento(evento);
            }
            finally
            {
                LiberarCacheCertificado();
                ServicePointManager.SecurityProtocol = oldProtocol;
                Status = NFeStatus.EmEspera;
            }

            GerarProcEvento(recepcaoEventoResposta, evento);
            return recepcaoEventoResposta;
        }

        /// <summary>
        ///     Gera classe procNFe
        /// </summary>
        private void GerarNFeProc(NFeRetAutorizacaoResposta retAutorizacao)
        {
            if (retAutorizacao.Resultado.CStat != 104 || !retAutorizacao.Resultado.ProtNFe.Any()) return;

            var autorizadas = new List<NFeProc>();
            foreach (var protNFe in retAutorizacao.Resultado.ProtNFe)
            {
                var nfe = Documentos.NFe.SingleOrDefault(x => x.InfNFe.Id.Substring(3) == protNFe.InfProt.ChNFe);
                if (nfe.IsNull()) continue;

                if (Configuracoes.ValidarDigest)
                    Guard.Against<VipException>(protNFe.InfProt.DigVal.IsNotNullOrEmpty() && protNFe.InfProt.DigVal != nfe.Signature.SignedInfo.Reference.DigestValue, $"DigestValue do documento {nfe.InfNFe.Id} não confere.");

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

        #endregion Methods

        #region Override

        protected override void OnInitialize()
        {
            Status = NFeStatus.EmEspera;
            _securityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
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