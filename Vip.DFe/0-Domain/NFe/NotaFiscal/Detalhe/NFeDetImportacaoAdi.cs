using System.ComponentModel;
using Vip.DFe.Attributes;
using Vip.DFe.Enum;
using Vip.DFe.Serializer;

namespace Vip.DFe.NFe.NotaFiscal.Detalhe
{
    /// <summary>
    ///     Adições
    /// </summary>
    public sealed class NFeDetImportacaoAdi : GenericClone<NFeDetImportacaoAdi>, INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Properties

        /// <summary>
        ///     I26 - Numero da Adição
        /// </summary>
        [DFeElement(TipoCampo.Int, "nAdicao", Id = "I26", Min = 1, Max = 3, Ocorrencia = Ocorrencia.Obrigatoria)]
        public int NAdicao { get; set; }

        /// <summary>
        ///     I27 - Numero sequencial do item dentro da Adição
        /// </summary>
        [DFeElement(TipoCampo.Int, "nSeqAdic", Id = "I27", Min = 1, Max = 3, Ocorrencia = Ocorrencia.Obrigatoria)]
        public int NSeqAdic { get; set; }

        /// <summary>
        ///     I28 - Código do fabricante estrangeiro
        /// </summary>
        [DFeElement(TipoCampo.Str, "cFabricante", Id = "I28", Min = 1, Max = 60, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string CFabricante { get; set; }

        /// <summary>
        ///     I29 - Valor do desconto do item da DI – Adição
        /// </summary>
        [DFeElement(TipoCampo.De2, "vDescDI", Id = "I29", Min = 3, Max = 15, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public decimal? VDescDI { get; set; }

        /// <summary>
        ///     I29a - Número do ato concessório de Drawback
        /// </summary>
        [DFeElement(TipoCampo.StrNumber, "nDraw", Id = "I29a", Min = 0, Max = 11, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public string NDraw { get; set; }

        #endregion

        #region Methods

        private bool ShouldSerializeVDescDI()
        {
            return VDescDI.HasValue;
        }

        #endregion
    }
}