using System.ComponentModel;
using Vip.DFe.Attributes;

namespace Vip.DFe.SAT.Enum
{
    /// <summary>
    ///     Regime Especial de Tributação do ISSQN
    ///     <para />
    ///     <br />1 - Microempresa Municipal
    ///     <br />2 - Estimativa
    ///     <br />3 - Sociedade de Profissionais
    ///     <br />4 - Cooperativa
    ///     <br />5 - Microempresário Individual (MEI);
    ///     <br />6 - Microempresário Empresa Pequeno Porte (MEEPP);
    /// </summary>
    public enum RegTribIssqn
    {
        [DFeEnum("0")] [Description("Nenhum")] Nenhum,

        [DFeEnum("1")] [Description("MicroEmpresa Municipal")]
        MicroempresaMunicipal,

        [DFeEnum("2")] [Description("Estimativa")]
        Estimativa,

        [DFeEnum("3")] [Description("Sociedade de Profissionais")]
        SociedadeProfissionais,

        [DFeEnum("4")] [Description("Cooperativa")]
        Cooperativa,

        [DFeEnum("5")] [Description("MicroEmpresário Individual (MEI)")]
        MEI,

        [DFeEnum("6")] [Description("MicroEmpresário Empresa Pequeno Porte (MEEPP)")]
        MEEPP
    }
}