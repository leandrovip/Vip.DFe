using Vip.DFe.Attributes;

namespace Vip.DFe.Cryptography.Models
{
    public sealed class SignedInfo
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="DFeSignature" /> class.
        /// </summary>
        public SignedInfo()
        {
            CanonicalizationMethod = new CanonicalizationMethod();
            SignatureMethod = new SignatureMethod();
            Reference = new Reference();
        }

        #endregion Constructors

        #region Propriedades

        /// <summary>
        ///     XS03 - Grupo do Método de Canonicalização
        /// </summary>
        /// <value>The canonicalization method.</value>
        [DFeElement("CanonicalizationMethod", Id = "XS03")]
        public CanonicalizationMethod CanonicalizationMethod { get; set; }

        /// <summary>
        ///     XS05 - Grupo do Método de Assinatura
        /// </summary>
        /// <value>The signature method.</value>
        [DFeElement("SignatureMethod", Id = "XS05")]
        public SignatureMethod SignatureMethod { get; set; }

        /// <summary>
        ///     XS07 - Grupo Reference
        /// </summary>
        /// <value>The reference.</value>
        [DFeElement("Reference", Id = "XS07")]
        public Reference Reference { get; set; }

        #endregion Propriedades
    }
}