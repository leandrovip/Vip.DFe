using System.ComponentModel;
using Vip.DFe.Attributes;

namespace Vip.DFe.NFe.Enum
{
    /// <summary>
    ///     Inscrição Estadual do Destinatário
    ///     <br />Nota 1: No caso de NFC-e informar indIEDest=9 e não informar a tag IE do destinatário;
    ///     <br />Nota 2: No caso de operação com o Exterior informar indIEDest=9 e não informar a tag IE do destinatário;
    ///     <br />Nota 3: No caso de Contribuinte Isento de Inscrição(indIEDest= 2), não informar a tag IE do destinatário
    /// </summary>
    public enum NFeIndIeDest
    {
        [DFeEnum("1")] [Description("1 - Contribuinte ICMS")]
        Contribuinte = 1,

        [DFeEnum("2")] [Description("2 - Contribuinte Isendo de Inscrição")]
        ContribuinteIsento = 2,

        [DFeEnum("9")] [Description("9 - Não Contribuinte")]
        NaoContribuinte = 9
    }
}