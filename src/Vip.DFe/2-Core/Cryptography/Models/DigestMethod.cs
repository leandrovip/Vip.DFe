using Vip.DFe.Attributes;
using Vip.DFe.Enum;
using Vip.DFe.Serializer;

namespace Vip.DFe.Cryptography.Models
{
    public sealed class DigestMethod
    {
        #region Properties

        /// <summary>
        ///     XS16 - Atributo Algorithm de DigestMethod: http://www.w3.org/2000/09/xmldsig#sha1
        /// </summary>
        /// <value>The algorithm.</value>
        [DFeAttribute(TipoCampo.Str, "Algorithm", Id = "XS16", Min = 0, Max = 999, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string Algorithm { get; set; }

        #endregion
    }
}