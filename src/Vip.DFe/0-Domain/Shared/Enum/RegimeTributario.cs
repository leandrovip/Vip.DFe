using System.ComponentModel;
using Vip.DFe.Attributes;

namespace Vip.DFe.Shared.Enum
{
    /// <summary>
    ///     Código de Regime Tributário
    ///     <para>1 – Simples Nacional</para>
    ///     <para>2 – Simples Nacional - Ecesso de sublimite de receita bruta</para>
    ///     <para>3 – Regime Normal</para>
    /// </summary>
    public enum RegimeTributario
    {
        [DFeEnum("1")] [Description("1 - Simples Nacional")]
        SimplesNacional = 1,

        [DFeEnum("2")] [Description("2 - Simples Nacional - Excesso de sublimite de receita bruta")]
        SimplesNacionalExcesso = 2,

        [DFeEnum("3")] [Description("3 - Regime Normal")]
        Normal = 3
    }
}