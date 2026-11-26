using System.ComponentModel;

namespace Vip.DFe.NFe.Enum
{
    /// <summary>
    ///     Versão do QR Code da NFC-e (Manual de Especificações Técnicas do DANFE NFC-e e QR Code, itens 4.4.1 e 4.4.2)
    /// </summary>
    public enum NFeVersaoQrCode
    {
        /// <summary>
        ///     Versão 2: exige CSC e idCSC (hash SHA-1)
        /// </summary>
        [Description("2 - Com CSC")]
        V2 = 2,

        /// <summary>
        ///     Versão 3: sem CSC; na contingência off-line leva a assinatura digital (RSA SHA-1) do certificado do emitente
        /// </summary>
        [Description("3 - Sem CSC")]
        V3 = 3
    }
}