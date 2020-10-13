using Vip.DFe.Attributes;
using Vip.DFe.Enum;
using Vip.DFe.Serializer;

namespace Vip.DFe.Cryptography.Models
{
    public sealed class SignatureMethod
    {
        #region Properties

        /// <summary>
        ///     XS06 - Atributo Algorithm de SignatureMethod: http://www.w3.org/2000/09/xmldsig#rsa-sha1
        /// </summary>
        /// <value>The algorithm.</value>
        [DFeAttribute(TipoCampo.Str, "Algorithm", Id = "XS06", Min = 0, Max = 999, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string Algorithm { get; set; }

        #endregion
    }
}