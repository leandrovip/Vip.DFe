using System;

namespace Vip.DFe.Attributes
{
    /// <summary>
    ///     Class DFeItemAttribute.
    /// </summary>
    /// <seealso cref="System.Attribute" />
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public sealed class DFeItemAttribute : Attribute
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="DFeItemAttribute" /> class.
        /// </summary>
        /// <param name="tipo">Tipo do campo</param>
        /// <param name="name">Nome da tag</param>
        public DFeItemAttribute(Type tipo, string name)
        {
            Tipo = tipo;
            Name = name;
        }

        #endregion Constructors

        #region Propriedades

        public Type Tipo { get; set; }

        public string Name { get; set; }

        public string Namespace { get; set; }

        public bool IsValue { get; set; }

        #endregion Propriedades
    }
}