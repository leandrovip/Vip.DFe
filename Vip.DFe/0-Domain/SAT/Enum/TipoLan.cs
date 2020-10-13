using Vip.DFe.Attributes;

namespace Vip.DFe.SAT.Enum
{
    public enum TipoLan
    {
        [DFeEnum("DHCP")] DHCP,
        [DFeEnum("PPPoE")] PPPoE,
        [DFeEnum("IPFIX")] IPFIX
    }
}