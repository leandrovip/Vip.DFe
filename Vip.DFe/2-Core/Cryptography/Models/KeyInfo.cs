using Vip.DFe.Attributes;

namespace Vip.DFe.Cryptography.Models
{
    public sealed class KeyInfo
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="KeyInfo" /> class.
        /// </summary>
        public KeyInfo()
        {
            X509Data = new X509Data();
        }

        #endregion Constructors

        #region Propriedades

        /// <summary>
        ///     XS20 - Grupo X509
        /// </summary>
        /// <value>The X509 data.</value>
        [DFeElement("X509Data", Id = "XS20")]
        public X509Data X509Data { get; set; }

        #endregion Propriedades
    }
}