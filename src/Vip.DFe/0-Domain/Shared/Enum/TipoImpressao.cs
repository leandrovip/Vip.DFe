using System.ComponentModel;
using Vip.DFe.Attributes;

namespace Vip.DFe.Shared.Enum
{
    public enum TipoImpressao
    {
        [DFeEnum("0")] [Description("0 - Sem DANFE")]
        SemDanfe = 0,

        [DFeEnum("1")] [Description("1 - DANFE Normal Retrato")]
        NormalRetrato = 1,

        [DFeEnum("2")] [Description("2 - DANFE Normal Paisagem")]
        NormalPaisagem = 2,

        [DFeEnum("3")] [Description("3 - DANFE Simplificado")]
        Simplificado = 3,

        [DFeEnum("4")] [Description("4 - DANFE NFCe")]
        NFCe = 4,

        [DFeEnum("5")] [Description("5 - DANFE NFCe mensagem eletrônica")]
        NFCeMensagemEletronica = 5,

        /// <summary>
        ///     DANFE Simplificado Tipo 2 (Ajuste SINIEF 13/26). Usado em NF-e modelo 55 emitida em operação que normalmente
        ///     seria NFC-e. Exige QR Code v3, ainda não gerado pela lib para o modelo 55.
        /// </summary>
        [DFeEnum("6")] [Description("6 - DANFE Simplificado Tipo 2")]
        SimplificadoTipo2 = 6,
    }
}