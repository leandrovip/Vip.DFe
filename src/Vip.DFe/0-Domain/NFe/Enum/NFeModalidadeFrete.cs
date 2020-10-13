using System.ComponentModel;
using Vip.DFe.Attributes;

namespace Vip.DFe.NFe.Enum
{
    /// <summary>
    ///     Modalidade do frete
    ///     <para />
    ///     <br />0-Contratação do Frete por conta do Remetente (CIF);
    ///     <br />1-Contratação do Frete por conta do Destinatário(FOB);
    ///     <br />2-Contratação do Frete por conta de Terceiros;
    ///     <br />3-Transporte Próprio por conta do Remetente;
    ///     <br />4-Transporte Próprio por conta do Destinatário;
    ///     <br />9-Sem Ocorrência de Transporte.
    /// </summary>
    public enum NFeModalidadeFrete
    {
        [DFeEnum("0")] [Description("0 - Contratação do Frete por conta do Remetente (CIF)")]
        ContratacaoRemetenteCIF = 0,

        [DFeEnum("1")] [Description("1 - Contratação do Frete por conta do Destinatário (FOB)")]
        ContratacaoDestinatarioFOB = 1,

        [DFeEnum("2")] [Description("2 - Contratação do Frete por conta de Terceiros")]
        ContratacaoTerceiros = 2,

        [DFeEnum("3")] [Description("3 - Transporte Próprio por conta do Remetente")]
        TransporteRemetente = 3,

        [DFeEnum("4")] [Description("4 - Transporte Próprio por conta do Destinatário")]
        TransporteDestinatario = 4,

        [DFeEnum("9")] [Description("9 - Sem Frete")]
        SemFrete = 9
    }
}