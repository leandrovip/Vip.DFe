using System;

namespace Vip.DFe.Attributes
{
    /// <summary>
    ///     Class DFeRootAttribute.
    /// </summary>
    /// <seealso cref="System.Attribute" />
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class DFeRootAttribute : Attribute
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="DFeRootAttribute" /> class.
        /// </summary>
        public DFeRootAttribute()
        {
            Name = string.Empty;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="DFeRootAttribute" /> class.
        /// </summary>
        /// <param name="root">The Namespace.</param>
        public DFeRootAttribute(string root)
        {
            Name = root;
        }

        #endregion Constructors

        #region Properties

        public string Name { get; set; }

        public string Namespace { get; set; }

        #endregion Properties
    }
}