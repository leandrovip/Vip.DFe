using System;
using Vip.DFe.Enum;

namespace Vip.DFe.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class DFeDictionaryAttribute : Attribute
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="DFeCollectionAttribute" /> class.
        /// </summary>
        public DFeDictionaryAttribute()
        {
            Id = string.Empty;
            Name = string.Empty;
            ItemName = string.Empty;
            Descricao = string.Empty;
            MinSize = 0;
            MaxSize = 0;
            Ocorrencia = Ocorrencia.NaoObrigatoria;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="DFeCollectionAttribute" /> class.
        /// </summary>
        /// <param name="tag">The Name.</param>
        public DFeDictionaryAttribute(string tag) : this()
        {
            Name = tag;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="DFeCollectionAttribute" /> class.
        /// </summary>
        /// <param name="tag">The Name.</param>
        /// <param name="itemName">The Name.</param>
        public DFeDictionaryAttribute(string tag, string itemName) : this()
        {
            Name = tag;
            ItemName = itemName;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        ///     Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public string Id { get; set; }

        /// <summary>
        ///     Gets or sets the descricao.
        /// </summary>
        /// <value>The descricao.</value>
        public string Descricao { get; set; }

        /// <summary>
        ///     Gets or sets the Name.
        /// </summary>
        /// <value>The Name.</value>
        public string Name { get; set; }

        /// <summary>
        ///     Gets or sets the Name.
        /// </summary>
        /// <value>The Name.</value>
        public string ItemName { get; set; }

        /// <summary>
        ///     Gets or sets the name space.
        /// </summary>
        /// <value>The name space.</value>
        public string Namespace { get; set; }

        /// <summary>
        ///     Gets or sets the ocorrencias.
        /// </summary>
        /// <value>The ocorrencias.</value>
        public Ocorrencia Ocorrencia { get; set; }

        /// <summary>
        /// </summary>
        public int MinSize { get; set; }

        /// <summary>
        /// </summary>
        public int MaxSize { get; set; }

        #endregion Properties
    }
}