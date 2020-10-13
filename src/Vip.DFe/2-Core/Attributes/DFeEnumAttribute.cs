using System;

namespace Vip.DFe.Attributes
{
    /// <summary>
    ///     Class DFeEnumAttribute. This class cannot be inherited.
    /// </summary>
    /// <seealso cref="System.Attribute" />
    [AttributeUsage(AttributeTargets.Field)]
    public sealed class DFeEnumAttribute : Attribute
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="DFeEnumAttribute" /> class.
        /// </summary>
        /// <param name="value">The value.</param>
        public DFeEnumAttribute(string value)
        {
            Value = value;
        }

        /// <summary>
        ///     Gets or sets the value.
        /// </summary>
        /// <value>The value.</value>
        public string Value { get; set; }
    }
}