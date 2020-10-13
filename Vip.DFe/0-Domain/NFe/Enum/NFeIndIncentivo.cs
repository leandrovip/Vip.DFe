using Vip.DFe.Attributes;

namespace Vip.DFe.NFe.Enum
{
    /// <summary>
    ///     Indicador de incentivo Fiscal
    /// </summary>
    public enum NFeIndIncentivo
    {
        [DFeEnum("1")] Sim = 1,
        [DFeEnum("2")] Nao = 2
    }
}