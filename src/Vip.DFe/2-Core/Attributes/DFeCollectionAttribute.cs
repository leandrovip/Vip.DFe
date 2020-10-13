using System;
using Vip.DFe.Serializer;

namespace Vip.DFe.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public sealed class DFeCollectionAttribute : DFeElementAttribute
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="DFeCollectionAttribute" /> class.
        /// </summary>
        /// <param name="tag">The Name.</param>
        public DFeCollectionAttribute(string tag)
        {
            Name = tag;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="DFeCollectionAttribute" /> class.
        /// </summary>
        /// <param name="tipo">The tipo.</param>
        /// <param name="name">The name.</param>
        public DFeCollectionAttribute(TipoCampo tipo, string name)
        {
            Tipo = tipo;
            Name = name;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// </summary>
        public int MinSize { get; set; }

        /// <summary>
        /// </summary>
        public int MaxSize { get; set; }

        #endregion Properties
    }
}