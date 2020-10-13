using Vip.DFe.Attributes;
using Vip.DFe.Enum;
using Vip.DFe.Serializer;

namespace Vip.DFe.Cryptography.Models
{
    public sealed class CanonicalizationMethod
    {
        #region Properties

        /// <summary>
        ///     XS04 - Atributo Algorithm de CanonicalizationMethod: http://www.w3.org/TR/2001/REC-xml-c14n-20010315
        /// </summary>
        /// <value>The algorithm.</value>
        [DFeAttribute(TipoCampo.Str, "Algorithm", Id = "XS04", Min = 0, Max = 999, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string Algorithm { get; set; }

        #endregion
    }
}