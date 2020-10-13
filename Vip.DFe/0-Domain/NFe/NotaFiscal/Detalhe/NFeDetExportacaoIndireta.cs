using System.ComponentModel;
using Vip.DFe.Attributes;
using Vip.DFe.Enum;
using Vip.DFe.Serializer;

namespace Vip.DFe.NFe.NotaFiscal.Detalhe
{
    public sealed class NFeDetExportacaoIndireta : GenericClone<NFeDetExportacaoIndireta>, INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Properties

        /// <summary>
        ///     I53 - Número do Registro de Exportação
        /// </summary>
        [DFeElement(TipoCampo.StrNumber, "nRE", Id = "I53", Min = 12, Max = 12, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string NRE { get; set; }

        /// <summary>
        ///     I54 - Chave de Acesso da NF-e recebida para exportação
        /// </summary>
        [DFeElement(TipoCampo.StrNumberFill, "chNFe", Id = "I54", Min = 44, Max = 44, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string ChNFe { get; set; }

        /// <summary>
        ///     I55 - Quantidade do item realmente exportado
        /// </summary>

        [DFeElement(TipoCampo.De4, "qExport", Id = "I55", Min = 5, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal QExport { get; set; }

        #endregion
    }
}