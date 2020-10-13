using System.ComponentModel;
using Vip.DFe.Attributes;
using Vip.DFe.Enum;
using Vip.DFe.Serializer;

namespace Vip.DFe.NFe.NotaFiscal.Detalhe
{
    public class NFeDetCombustivelEncerrante : GenericClone<NFeDetCombustivelEncerrante>, INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Properties

        /// <summary>
        ///     LA12 - Número de identificação do bico utilizado no abastecimento
        /// </summary>
        [DFeElement(TipoCampo.Int, "nBico", Id = "LA12", Min = 1, Max = 3, Ocorrencia = Ocorrencia.Obrigatoria)]
        public int NBico { get; set; }

        /// <summary>
        ///     LA13 - Número de identificação da bomba ao qual o bico está interligado
        /// </summary>
        [DFeElement(TipoCampo.Int, "nBomba", Id = "LA13", Min = 1, Max = 3, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public int NBomba { get; set; }

        /// <summary>
        ///     LA14 - Número de identificação do tanque ao qual o bico está interligado
        /// </summary>
        [DFeElement(TipoCampo.Int, "nTanque", Id = "LA14", Min = 1, Max = 3, Ocorrencia = Ocorrencia.Obrigatoria)]
        public int NTanque { get; set; }

        /// <summary>
        ///     LA15 - Valor do Encerrante no início do abastecimento
        /// </summary>
        [DFeElement(TipoCampo.De3, "vEncIni", Id = "LA15", Min = 3, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal VEncIni { get; set; }

        /// <summary>
        ///     LA16 - Valor do Encerrante no final do abastecimento
        /// </summary>
        [DFeElement(TipoCampo.De3, "vEncFin", Id = "LA16", Min = 3, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal VEncFin { get; set; }

        #endregion
    }
}