using System;
using System.Globalization;
using System.IO;
using System.Text;

namespace Vip.DFe.Writer
{
    /// <summary>
    ///     Classe derivada da StringWriter que aceita a mudança de enconding e usa UTF8 como padrão.
    /// </summary>
    public sealed class VipStringWriter : StringWriter
    {
        #region Constructors

        /// <summary>
        ///     Inicializar uma nova instancida da classe <see cref="VipStringWriter" />.
        /// </summary>
        public VipStringWriter() : this(Encoding.UTF8, new StringBuilder(), CultureInfo.CurrentCulture) { }

        /// <summary>
        ///     Inicializar uma nova instancida da classe <see cref="VipStringWriter" />.
        /// </summary>
        /// <param name="encoding"></param>
        public VipStringWriter(Encoding encoding) : this(encoding, new StringBuilder(), CultureInfo.CurrentCulture) { }

        /// <summary>
        ///     Inicializar uma nova instancida da classe <see cref="VipStringWriter" />.
        /// </summary>
        /// <param name="encoding"></param>
        /// <param name="formatProvider"></param>
        public VipStringWriter(Encoding encoding, IFormatProvider formatProvider)
            : this(encoding, new StringBuilder(), formatProvider) { }

        /// <summary>
        ///     Inicializar uma nova instancida da classe <see cref="VipStringWriter" />.
        /// </summary>
        /// <param name="formatProvider"></param>
        public VipStringWriter(IFormatProvider formatProvider)
            : this(Encoding.UTF8, new StringBuilder(), formatProvider) { }

        /// <summary>
        ///     Inicializar uma nova instancida da classe <see cref="VipStringWriter" />.
        /// </summary>
        /// <param name="encoding"></param>
        /// <param name="sb"></param>
        public VipStringWriter(Encoding encoding, StringBuilder sb) : this(encoding, sb, CultureInfo.CurrentCulture) { }

        /// <summary>
        ///     Inicializar uma nova instancida da classe <see cref="VipStringWriter" />.
        /// </summary>
        /// <param name="sb"></param>
        public VipStringWriter(StringBuilder sb) : this(Encoding.UTF8, sb, CultureInfo.CurrentCulture) { }

        /// <summary>
        ///     Inicializar uma nova instancida da classe <see cref="VipStringWriter" />.
        /// </summary>
        /// <param name="encoding"></param>
        /// <param name="sb"></param>
        /// <param name="formatProvider"></param>
        public VipStringWriter(Encoding encoding, StringBuilder sb, IFormatProvider formatProvider) : base(sb, formatProvider)
        {
            Encoding = encoding;
        }

        #endregion Constructors

        #region Propriedades

        /// <summary>Gets the <see cref="T:System.Text.Encoding" /> in which the output is written.</summary>
        /// <returns>The Encoding in which the output is written.</returns>
        /// <filterpriority>1</filterpriority>
        public override Encoding Encoding { get; }

        #endregion Propriedades
    }
}