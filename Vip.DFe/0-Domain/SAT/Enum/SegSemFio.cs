using Vip.DFe.Attributes;

namespace Vip.DFe.SAT.Enum
{
    public enum SegSemFio
    {
        [DFeEnum("")] None,
        [DFeEnum("WEP")] Wep,
        [DFeEnum("WPA-PERSONAL")] WpaPersonal,
        [DFeEnum("WPA-ENTERPRISE")] WpaEnterprise
    }
}