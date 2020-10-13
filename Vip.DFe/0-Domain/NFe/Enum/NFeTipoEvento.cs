using System.ComponentModel;
using Vip.DFe.Attributes;

namespace Vip.DFe.NFe.Enum
{
    public enum NFeTipoEvento
    {
        /// <summary>
        ///     110110 - Carta de Correção
        /// </summary>
        [DFeEnum("110110")] [Description("CCe")]
        CartaCorrecao = 110110,

        /// <summary>
        ///     110111 - Cancelamento
        /// </summary>
        [DFeEnum("110111")] [Description("Cancelamento")]
        Cancelamento = 110111,

        /// <summary>
        ///     110112 - Cancelamento Substituição
        /// </summary>
        [DFeEnum("110112")] [Description("Cancelamento ST")]
        CancelamentoST = 110112,

        /// <summary>
        ///     110113 - EPEC
        /// </summary>
        [DFeEnum("110113")] [Description("EPEC")]
        EPEC = 110113,
    }
}