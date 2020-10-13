using System.ComponentModel;
using System.Globalization;
using Vip.DFe.Attributes;
using Vip.DFe.Cryptography.Models;
using Vip.DFe.Document;
using Vip.DFe.Enum;
using Vip.DFe.Extensions;

namespace Vip.DFe.SAT.CupomFiscal
{
    [DFeRoot("CFe")]
    public sealed class CFe : DFeDocument<CFe>, INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion Events

        #region Fields

        private InfCFe infCFe;

        #endregion Fields

        #region Constructors

        public CFe()
        {
            infCFe = new InfCFe(this);
            Signature = new DFeSignature();
        }

        #endregion Constructors

        #region Properties

        [DFeElement("infCFe", Ocorrencia = Ocorrencia.Obrigatoria)]
        public InfCFe InfCFe
        {
            get => infCFe;
            set
            {
                if (infCFe == value) return;

                infCFe = value;
                if (value != null && value.Parent != this)
                    value.Parent = this;
            }
        }

        public DFeSignature Signature { get; set; }

        #endregion Properties

        #region Methods

        /// <summary>
        ///     Retorna o valor do QrCode
        /// </summary>
        /// <returns>Código QrCode</returns>
        public string GetQRCode()
        {
            var documento = InfCFe.Dest.CNPJ.IsNullOrEmpty() ? InfCFe.Dest.CPF.OnlyNumbers() : InfCFe.Dest.CNPJ.OnlyNumbers();
            return $"{InfCFe.Id.OnlyNumbers()}|{InfCFe.Ide.DhEmissao:yyyyMMddHHmmss}|{InfCFe.Total.VCFe.ToString("0.00", CultureInfo.InvariantCulture)}|{documento}|{InfCFe.Ide.AssinaturaQrcode}";
        }

        /// <summary>
        ///     Função para preencher o número do item da lista de itens da CFe.
        /// </summary>
        public void PreencherNItem()
        {
            for (var i = 0; i < InfCFe.Det.Count; i++)
            {
                var det = InfCFe.Det[i];
                det.NItem = i + 1;
            }
        }

        private bool ShouldSerializeSignature()
        {
            return !Signature.SignatureValue.IsNullOrEmpty() &&
                   !Signature.SignedInfo.Reference.DigestValue.IsNullOrEmpty() &&
                   !Signature.KeyInfo.X509Data.X509Certificate.IsNullOrEmpty();
        }

        #endregion Methods
    }
}