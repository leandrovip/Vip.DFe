using System.ComponentModel;
using Vip.DFe.Attributes;

namespace Vip.DFe.NFe.Enum
{
    public enum NFeModalidadeBCST
    {
        /// <summary>
        ///     0 - Preço tabelado ou máximo sugerido
        /// </summary>
        [DFeEnum("0")] [Description("0 - Preço Tabelado ou Máximo Sugerido")]
        PrecoTabeladoOuMaximoSugerido = 0,

        /// <summary>
        ///     1 - Lista Negativa (valor);
        /// </summary>
        [DFeEnum("1")] [Description("1 - Lista Negativa (Valor)")]
        ListaNegativa = 1,

        /// <summary>
        ///     2 - Lista Positiva (valor)
        /// </summary>
        [DFeEnum("2")] [Description("2 - Lista Positiva (Valor)")]
        ListaPositiva = 2,

        /// <summary>
        ///     3 - Lista Neutra (valor)
        /// </summary>
        [DFeEnum("3")] [Description("3 - Lista Neutra (Valor)")]
        ListaNeutra = 3,

        /// <summary>
        ///     4 - Margem Valor Agregado (%)
        /// </summary>
        [DFeEnum("4")] [Description("4 - Margem Valor Agregado %")]
        MVA = 4,

        /// <summary>
        ///     5 - Pauta (valor)
        /// </summary>
        [DFeEnum("5")] [Description("5 - Pauta (Valor)")]
        Pauta = 5
    }
}