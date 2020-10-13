using System;

namespace Vip.DFe.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class DFeSignInfoElement : Attribute
    {
        #region Constructors

        /// <summary>
        ///     Inicializa uma nova intância da classe <see cref="DFeSignInfoElement" />.
        /// </summary>
        public DFeSignInfoElement()
        {
            SignElement = string.Empty;
            SignAtribute = "Id";
        }

        /// <summary>
        ///     Inicializa uma nova intância da classe <see cref="DFeSignInfoElement" />.
        /// </summary>
        /// <param name="signElement">O elemento a ser assinado.</param>
        public DFeSignInfoElement(string signElement)
        {
            SignElement = signElement;
            SignAtribute = "Id";
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        ///     Define/retorna o elemento a ser assinado.
        /// </summary>
        /// <value>The sign element.</value>
        public string SignElement { get; set; }

        /// <summary>
        ///     Define/retorna o atributo identificador do elemento a ser assinado.
        /// </summary>
        /// <value>The sign atribute.</value>
        public string SignAtribute { get; set; }

        #endregion Properties
    }
}