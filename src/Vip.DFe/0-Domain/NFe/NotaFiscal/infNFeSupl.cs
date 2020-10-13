using System.ComponentModel;
using Vip.DFe.Attributes;
using Vip.DFe.Enum;
using Vip.DFe.Serializer;

namespace Vip.DFe.NFe.NotaFiscal
{
    public sealed class infNFeSupl : GenericClone<infNFeSupl>, INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Properties

        /// <summary>
        ///     ZX02 - Texto com o QR-Code impresso no DANFE NFC-e
        ///     O atributo qrCode deve ser serializado como CDATA, conforme NT2015.002, V141, regra ZX02-22
        /// </summary>
        [DFeElement(TipoCampo.Str, "qrCode", Id = "ZX02", Min = 100, Max = 1000, Ocorrencia = Ocorrencia.Obrigatoria, UseCData = true)]
        public string QrCode { get; set; }

        /// <summary>
        ///     ZX03 - Texto com a URL de consulta por chave de acesso a ser impressa no DANFE NFC-e
        ///     VERSÃO 4.00
        /// </summary>
        [DFeElement(TipoCampo.Str, "urlChave", Id = "ZX03", Min = 21, Max = 85, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string UrlChave { get; set; }

        #endregion
    }
}