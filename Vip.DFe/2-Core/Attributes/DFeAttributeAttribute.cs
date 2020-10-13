using System;
using Vip.DFe.Serializer;

namespace Vip.DFe.Attributes
{
    /// <summary>
    ///     Class DFeAttributeAttribute.
    /// </summary>
    /// <seealso cref="System.Attribute" />
    [AttributeUsage(AttributeTargets.Property)]
    public class DFeAttributeAttribute : DFeBaseAttribute
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="DFeElementAttribute" /> class.
        /// </summary>
        public DFeAttributeAttribute() { }

        /// <summary>
        ///     Initializes a new instance of the <see cref="DFeElementAttribute" /> class.
        /// </summary>
        /// <param name="name">The Name.</param>
        public DFeAttributeAttribute(string name) : this()
        {
            Name = name;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="DFeElementAttribute" /> class.
        /// </summary>
        /// <param name="tipo">The tipo.</param>
        /// <param name="name">The name.</param>
        public DFeAttributeAttribute(TipoCampo tipo, string name) : this()
        {
            Tipo = tipo;
            Name = name;
        }

        #endregion Constructors
    }
}