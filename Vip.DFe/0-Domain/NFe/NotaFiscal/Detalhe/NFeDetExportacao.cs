using System.ComponentModel;
using Vip.DFe.Attributes;
using Vip.DFe.Enum;
using Vip.DFe.Extensions;
using Vip.DFe.Serializer;

namespace Vip.DFe.NFe.NotaFiscal.Detalhe
{
    public sealed class NFeDetExportacao : GenericClone<NFeDetExportacao>, INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Constructors

        public NFeDetExportacao()
        {
            ExportacaoIndireta = new NFeDetExportacaoIndireta();
        }

        #endregion

        #region Properties

        /// <summary>
        ///     I51 - Número do ato concessório de Drawback
        /// </summary>
        [DFeElement(TipoCampo.StrNumber, "nDraw", Id = "I51", Min = 0, Max = 11, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public string NDraw { get; set; }

        /// <summary>
        ///     I52 - Grupo sobre exportação indireta
        /// </summary>
        [DFeElement("exportInd", Id = "I52", Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public NFeDetExportacaoIndireta ExportacaoIndireta { get; set; }

        #endregion

        #region Methods

        private bool ShouldSerializeExportacaoIndireta()
        {
            return ExportacaoIndireta.NRE.IsNotNullOrEmpty() || ExportacaoIndireta.ChNFe.IsNotNullOrEmpty();
        }

        #endregion
    }
}