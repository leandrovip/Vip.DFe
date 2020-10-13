using System.ComponentModel;
using Vip.DFe.Attributes;

namespace Vip.DFe.NFe.Enum
{
    public enum NFeVersao
    {
        [DFeEnum("1.00")]
        [Description("1.00")]
        v100,
        [DFeEnum("3.10")]
        [Description("3.10")]
        v310,
        [DFeEnum("4.00")]
        [Description("4.00")]
        v400
    }
}