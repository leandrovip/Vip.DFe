using System.ComponentModel;
using Vip.DFe.Attributes;

namespace Vip.DFe.NFe.Enum
{
    /// <summary>
    ///     N28b - Indicador de dedução do ICMS desonerado (indDeduzDeson) - NT 2023.004.
    ///     Indica se o valor do ICMS desonerado (vICMSDeson) deduz do valor do item (vProd) / total da NF-e.
    ///     Obrigatório informar 1 quando motDesICMS = 7 (Suframa), rejeição 652.
    /// </summary>
    public enum NFeIndDeduzDeson
    {
        /// <summary>
        ///     0 - O valor do ICMS desonerado (vICMSDeson) não deduz do valor do item (vProd) / total da NF-e
        /// </summary>
        [DFeEnum("0")] [Description("0 - Não deduz")]
        NaoDeduz = 0,

        /// <summary>
        ///     1 - O valor do ICMS desonerado (vICMSDeson) deduz do valor do item (vProd) / total da NF-e
        /// </summary>
        [DFeEnum("1")] [Description("1 - Deduz")]
        Deduz = 1
    }
}
