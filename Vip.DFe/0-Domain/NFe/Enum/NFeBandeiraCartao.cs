using System.ComponentModel;
using Vip.DFe.Attributes;

namespace Vip.DFe.NFe.Enum
{
    /// <summary>
    ///     Bandeira da operadora de cartão de crédito e/ou débito
    ///     <para />
    ///     <br /> 01=Visa
    ///     <br /> 02=Mastercard
    ///     <br /> 03=American Express
    ///     <br /> 04=Sorocred
    ///     <br /> 05=Diners Club
    ///     <br /> 06=Elo
    ///     <br /> 07=Hipercard
    ///     <br /> 08=Aura
    ///     <br /> 09=Cabal
    ///     <br /> 99=Outros
    /// </summary>
    public enum NFeBandeiraCartao
    {
        [DFeEnum("01")] [Description("01 - Visa")]
        Visa = 1,

        [DFeEnum("02")] [Description("02 - Mastercard")]
        Mastercard = 2,

        [DFeEnum("03")] [Description("03 - American Express")]
        AmericanExpress = 3,

        [DFeEnum("04")] [Description("04 - Sorocred")]
        Sorocred = 4,

        [DFeEnum("05")] [Description("05 - Diners Club")]
        Diners = 5,

        [DFeEnum("06")] [Description("06 - Elo")]
        Elo = 6,

        [DFeEnum("07")] [Description("07 - Hipercard")]
        Hipercard = 7,

        [DFeEnum("08")] [Description("08 - Aura")]
        Aura = 8,

        [DFeEnum("09")] [Description("09 - Cabal")]
        Cabal = 9,

        [DFeEnum("99")] [Description("99 - Outros")]
        Outros = 99
    }
}