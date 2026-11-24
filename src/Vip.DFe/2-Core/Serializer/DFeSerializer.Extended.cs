using System.IO;

namespace Vip.DFe.Serializer
{
    /// <summary>
    ///     Class DFeSerializer. This class cannot be inherited.
    /// </summary>
    /// <typeparam name="TClass"></typeparam>
    public sealed class DFeSerializer<TClass> : DFeSerializer where TClass : class
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="DFeSerializer{TClass}" /> class.
        /// </summary>
        internal DFeSerializer() : base(typeof(TClass)) { }

        #endregion Constructors

        #region Create

        /// <summary>
        ///     Creates the serializer.
        /// </summary>
        /// <typeparam name="TCreate"></typeparam>
        /// <returns>DFeSerializer.</returns>
        public static DFeSerializer<TCreate> CreateSerializer<TCreate>() where TCreate : class
        {
            return new DFeSerializer<TCreate>();
        }

        #endregion Create

        #region Methods

        #region Serialize

        /// <summary>
        ///     Serializes the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="path">The xml.</param>
        public bool Serialize(TClass item, string path)
        {
            return base.Serialize(item, path);
        }

        /// <summary>
        ///     Serializes the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="stream">The stream.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool Serialize(TClass item, Stream stream)
        {
            return base.Serialize(item, stream);
        }

        #endregion Serialize

        #region Deserialize

        /// <summary>
        ///     Deserializes the specified xml.
        /// </summary>
        /// <param name="path">The xml.</param>
        /// <returns>T.</returns>
        public new TClass Deserialize(string path)
        {
            return (TClass) base.Deserialize(path);
        }

        /// <summary>
        ///     Deserializes the specified xml.
        /// </summary>
        /// <param name="stream">The xml.</param>
        /// <returns>T.</returns>
        public new TClass Deserialize(Stream stream)
        {
            return (TClass) base.Deserialize(stream);
        }

        #endregion Deserialize

        #endregion Methods
    }
}