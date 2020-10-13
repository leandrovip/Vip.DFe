using Vip.DFe.Attributes;
using Vip.DFe.Document;
using Vip.DFe.Enum;
using Vip.DFe.Serializer;

namespace Vip.DFe.Cryptography.Models
{
    [DFeRoot("Signature", Namespace = "http://www.w3.org/2000/09/xmldsig#")]
    public sealed class DFeSignature : DFeDocument<DFeSignature>
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="DFeSignature" /> class.
        /// </summary>
        public DFeSignature()
        {
            SignedInfo = new SignedInfo();
            KeyInfo = new KeyInfo();
        }

        #endregion Constructors

        #region Propriedades

        /// <summary>
        ///     XS02 - Grupo da Informação da assinatura
        /// </summary>
        /// <value>The signed information.</value>
        [DFeElement("SignedInfo", Id = "XS02")]
        public SignedInfo SignedInfo { get; set; }

        /// <summary>
        ///     XS18 - Grupo do Signature Value
        /// </summary>
        /// <value>The signature value.</value>
        [DFeElement(TipoCampo.Str, "SignatureValue", Id = "XS18", Min = 0, Max = 999, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string SignatureValue { get; set; }

        /// <summary>
        ///     XS19 - Grupo do KeyInfo
        /// </summary>
        /// <value>The key information.</value>
        [DFeElement("KeyInfo", Id = "XS19")]
        public KeyInfo KeyInfo { get; set; }

        #endregion Propriedades
    }
}