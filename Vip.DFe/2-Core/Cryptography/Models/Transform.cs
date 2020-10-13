using Vip.DFe.Attributes;
using Vip.DFe.Enum;
using Vip.DFe.Serializer;

namespace Vip.DFe.Cryptography.Models
{
    public sealed class Transform
    {
        #region Properties

        /// <summary>
        ///     XS13 - Atributos válidos Algorithm do Transform:
        ///     <para>http://www.w3.org/TR/2001/REC-xml-c14n-20010315</para>
        ///     <para>http://www.w3.org/2000/09/xmldsig#enveloped-signature</para>
        /// </summary>
        /// <value>The algorithm.</value>
        [DFeAttribute(TipoCampo.Str, "Algorithm", Id = "XS12", Min = 0, Max = 999, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string Algorithm { get; set; }

        #endregion
    }
}