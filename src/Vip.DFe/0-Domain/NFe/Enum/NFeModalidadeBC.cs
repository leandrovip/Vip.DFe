using System.ComponentModel;
using Vip.DFe.Attributes;

namespace Vip.DFe.NFe.Enum
{
    public enum NFeModalidadeBC
    {
        [DFeEnum("0")] [Description("0 - Margem Valor Agregado %")]
        MVA = 0,

        [DFeEnum("1")] [Description("1 - Pauta (Valor)")]
        Pauta = 1,

        [DFeEnum("2")] [Description("2 - Preço Tabelado Max. (Valor)")]
        PrecoTabeladoMax = 2,

        [DFeEnum("3")] [Description("3 - Valor da Operação")]
        ValorOperacao = 3
    }
}