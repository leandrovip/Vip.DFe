using System.ComponentModel;
using Vip.DFe.Attributes;

namespace Vip.DFe.NFe.Enum
{
    public enum NFeIndEscala
    {
        [DFeEnum("S")] [Description("S - Produzido em Escala Relevante")]
        Sim,

        [DFeEnum("N")] [Description("S - Produzido em Escala NÃO Relevante")]
        Nao
    }
}