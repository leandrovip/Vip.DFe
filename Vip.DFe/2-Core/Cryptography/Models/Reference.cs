using Vip.DFe.Attributes;
using Vip.DFe.Cryptography;
using Vip.DFe.Cryptography.Models;
using Vip.DFe.Document;
using Vip.DFe.Enum;
using Vip.DFe.Serializer;

namespace Vip.DFe.Cryptography
{
    public sealed class Reference
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Reference"/> class.
        /// </summary>
        public Reference()
        {
            Transforms = new DFeCollection<Transform>();
            DigestMethod = new DigestMethod();
        }

        #endregion Constructors

        #region Propriedades

        /// <summary>
        /// XS08 - Atributo URI da tag Reference
        /// </summary>
        /// <value>The URI.</value>
        [DFeAttribute(TipoCampo.Str, "URI", Id = "XS08", Min = 0, Max = 999, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string URI { get; set; }

        /// <summary>
        /// XS10 - Grupo do algorithm de Transform
        /// </summary>
        /// <value>The transforms.</value>
        [DFeCollection("Transforms", Id = "XS10")]
        [DFeItem(typeof(Transform), "Transform")]
        public DFeCollection<Transform> Transforms { get; set; }

        /// <summary>
        /// XS15 - Grupo do Método de DigestMethod
        /// </summary>
        /// <value>The digest method.</value>
        [DFeElement("DigestMethod", Id = "XS15")]
        public DigestMethod DigestMethod { get; set; }

        /// <summary>
        /// XS17 - Digest Value (Hash SHA-1 – Base64)
        /// </summary>
        /// <value>The digest value.</value>
        [DFeElement(TipoCampo.Str, "DigestValue", Id = "XS17", Min = 0, Max = 999, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string DigestValue { get; set; }

        #endregion Propriedades
    }
}