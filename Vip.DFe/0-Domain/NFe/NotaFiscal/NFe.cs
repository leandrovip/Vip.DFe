using System;
using System.ComponentModel;
using System.Security.Cryptography.X509Certificates;
using Vip.DFe.Attributes;
using Vip.DFe.Cryptography.Models;
using Vip.DFe.Document;
using Vip.DFe.Enum;
using Vip.DFe.Exception;
using Vip.DFe.Extensions;

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
                var chave = ChaveDFe.Gerar(InfNFe.Ide.CodigoUF, InfNFe.Ide.DhEmi.DateTime,
                    InfNFe.Emitente.CNPJ, (int) InfNFe.Ide.Modelo, InfNFe.Ide.Serie,
                    InfNFe.Ide.NumeroNFe, InfNFe.Ide.TipoEmissao, InfNFe.Ide.CNf.ToInt32());

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

        private bool ShouldSerializeInfNFeSupl()
        {
            return InfNFeSupl.QrCode.IsNotNullOrEmpty() || InfNFeSupl.UrlChave.IsNotNullOrEmpty();
        }

        #endregion
    }
}