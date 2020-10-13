using System.ComponentModel;
using Vip.DFe.Attributes;

namespace Vip.DFe.SAT.Enum
{
    public enum TipoProxy
    {
        [DFeEnum("0")] [Description("Nenhum")] None = 0,

        [DFeEnum("1")] [Description("Proxy Configuração")]
        ProxyConfiguracao = 1,

        [DFeEnum("2")] [Description("Proxy Transparente")]
        ProxyTransparente = 2
    }
}