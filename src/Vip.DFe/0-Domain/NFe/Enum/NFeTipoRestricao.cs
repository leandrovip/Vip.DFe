using System.ComponentModel;
using Vip.DFe.Attributes;

namespace Vip.DFe.NFe.Enum
{
    public enum NFeTipoRestricao
    {
        [DFeEnum("0")] [Description("0 - Não há")]
        NaoHa = 0,

        [DFeEnum("1")] [Description("1 - Alienação Fiduciária")]
        AlienacaoFiduciaria = 1,

        [DFeEnum("2")] [Description("2 - Arrendamento Nercantil")]
        ArrendamentoMercantil = 2,

        [DFeEnum("3")] [Description("3 - Reserva de Domínio")]
        ReservaDominio = 3,

        [DFeEnum("4")] [Description("4 - Penhor de Veículos")]
        PenhorVeiculo = 4,

        [DFeEnum("9")] [Description("9 - Outras")]
        Outras = 9
    }
}