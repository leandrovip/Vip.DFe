using System;
using System.ComponentModel;
using System.Security.Cryptography.X509Certificates;
using Vip.DFe.Attributes;
using Vip.DFe.Cryptography.Models;
using Vip.DFe.Document;
using Vip.DFe.Enum;
using Vip.DFe.Exception;
using Vip.DFe.Extensions;
using Vip.DFe.NFe.Configuration;
using Vip.DFe.NFe.Enum;
using Vip.DFe.Shared.Enum;

namespace Vip.DFe.NFe.NotaFiscal
{
    [DFeSignInfoElement("infNFe")]
    [DFeRoot("NFe", Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public sealed class NFe : DFeSignDocument<NFe>, INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Fields

        private infNFe _infNFe;

        #endregion;

        #region Constructor

        public NFe()
        {
            _infNFe = new infNFe(this);
            InfNFeSupl = new infNFeSupl();
            Signature = new DFeSignature();
        }

        #endregion

        #region Properties

        /// <summary>
        ///     A01 - Informações da Nota Fiscal Eletrônica
        /// </summary>
        [DFeElement("infNFe", Id = "A01", Ocorrencia = Ocorrencia.Obrigatoria)]
        public infNFe InfNFe
        {
            get => _infNFe;
            set
            {
                if (_infNFe == value) return;

                _infNFe = value;
                if (_infNFe != null && _infNFe.Parent != this)
                    _infNFe.Parent = this;
            }
        }

        /// <summary>
        ///     ZX01 - Informações suplementares da Nota Fiscal
        /// </summary>
        [DFeElement("infNFeSupl", Id = "ZX01", Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public infNFeSupl InfNFeSupl { get; set; }

        [DFeIgnore] public bool Assinado => ShouldSerializeSignature();

        #endregion

        #region Methods

        /// <summary>
        ///     Assina a NFe
        /// </summary>
        /// <param name="certificado"> Certificado Digital</param>
        /// <param name="saveOptions"> Opções </param>
        public void Assinar(X509Certificate2 certificado, SaveOptions saveOptions)
        {
            Guard.Against<ArgumentNullException>(certificado == null, "Certificado não pode ser nulo.");

            if (InfNFe.Id.IsNullOrEmpty() || InfNFe.Id.Length < 44)
            {
                var chave = ChaveDFe.Gerar(
                    InfNFe.Ide.CodigoUF,
                    InfNFe.Ide.DhEmi.DateTime,
                    InfNFe.Emitente.CNPJ,
                    (int) InfNFe.Ide.Modelo,
                    InfNFe.Ide.Serie,
                    InfNFe.Ide.NumeroNFe,
                    InfNFe.Ide.TipoEmissao,
                    InfNFe.Ide.CNf.ToInt32()
                );

                InfNFe.Id = $"NFe{chave.Chave}";
                InfNFe.Ide.CDV = chave.Digito;
            }

            AssinarDocumento(certificado, saveOptions, false, SignDigest.SHA1);
        }

        /// <summary>
        ///     Valida a assinatura da NFe
        /// </summary>
        /// <param name="gerarXml"></param>
        /// <returns></returns>
        public bool ValidarAssinatura(bool gerarXml = true)
        {
            Guard.Against<VipException>(!Assinado, "Documento não esta assinado.");
            return ValidarAssinaturaDocumento(gerarXml);
        }

        /// <summary>
        ///     Retorna Nome do XML da NFe
        /// </summary>
        /// <returns></returns>
        public string ObterNomeXml()
        {
            return $"{InfNFe.Id.OnlyNumbers()}-nfe.xml";
        }

        /// <summary>
        ///     Gera a URL para o QR-Code versão 2.0 com o tratamento de parâmetro query no final da url
        /// </summary>
        /// <returns></returns>
        public void GerarQrCode(NFeConfig configuracao)
        {
            if (InfNFe.Ide.Modelo != NFeModelo.NFCe) return;

            Guard.Against<VipException>(configuracao.NFCeIdToken.IsNullOrEmpty(), "ID do token para montagem do QRCode não encontrado");
            Guard.Against<VipException>(configuracao.NFCeCSC.IsNullOrEmpty(), "Token para montagem do QRCode não encontrado");

            var enderecoQrCode = configuracao.Webservices.ObterUrlQrCode();
            Guard.Against<VipException>(enderecoQrCode.IsNullOrEmpty(), "URL do QrCode não encontrada");

            var urlConsultaChave = configuracao.Webservices.ObterUrlChave();
            Guard.Against<VipException>(urlConsultaChave.IsNullOrEmpty(), "URL de consulta da chave não encontrada");

            #region Gerar URL QRCODE

            const string interrogacao = "?";
            const string parametro = "p=";

            if (!enderecoQrCode.EndsWith(interrogacao))
                enderecoQrCode += interrogacao;
            if (!enderecoQrCode.EndsWith(parametro))
                enderecoQrCode += parametro;

            const string pipe = "|";

            //Chave de Acesso da NFC-e 
            var chave = InfNFe.Id.Substring(3);

            //Identificação do Ambiente (1 – Produção, 2 – Homologação) 
            var ambiente = (int) InfNFe.Ide.TpAmb;

            //Identificador do CSC (Código de Segurança do Contribuinte no Banco de Dados da SEFAZ). Informar sem os zeros não significativos
            var idCsc = configuracao.NFCeIdToken.ToInt32(1);

            const int versaoQrCode = 2;

            string dadosBase;

            if (InfNFe.Ide.TipoEmissao == TipoEmissao.OffLine)
            {
                var diaEmi = InfNFe.Ide.DhEmi.Day.ToString("D2");
                var valorNfce = InfNFe.Total.IcmsTot.VNf.ToString("0.00").Replace(',', '.');
                var digVal = Signature.SignedInfo.Reference.DigestValue.ObterHex();
                dadosBase = string.Concat(chave, pipe, versaoQrCode, pipe, ambiente, pipe, diaEmi, pipe, valorNfce, pipe, digVal, pipe, idCsc);
            }
            else
            {
                dadosBase = string.Concat(chave, pipe, versaoQrCode, pipe, ambiente, pipe, idCsc);
            }

            var dadosSha1 = string.Concat(dadosBase, configuracao.NFCeCSC).ObterHexSha1();
            var urlQrCode = string.Concat(enderecoQrCode, dadosBase, pipe, dadosSha1);

            #endregion

            InfNFeSupl.QrCode = urlQrCode;
            InfNFeSupl.UrlChave = urlConsultaChave;
        }

        private bool ShouldSerializeInfNFeSupl()
        {
            return InfNFeSupl.QrCode.IsNotNullOrEmpty() || InfNFeSupl.UrlChave.IsNotNullOrEmpty();
        }

        #endregion
    }
}