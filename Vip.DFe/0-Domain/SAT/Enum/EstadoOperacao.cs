using System.ComponentModel;

namespace Vip.DFe.SAT.Enum
{
    public enum EstadoOperacao
    {
        [Description("Desbloqueado")] Desbloqueado = 0,
        [Description("Bloqueio SEFAZ")] BloqueioSEFAZ = 1,
        [Description("Bloqueio Contribuinte")] BloqueioContribuinte = 2,
        [Description("Bloqueio Autônomo")] BloqueioAutonomo = 3,
        [Description("Bloquio Desativação")] BloqueioDesativacao = 4
    }
}