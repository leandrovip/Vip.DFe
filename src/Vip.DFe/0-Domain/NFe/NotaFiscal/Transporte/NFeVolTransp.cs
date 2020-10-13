using System.ComponentModel;
using Vip.DFe.Attributes;
using Vip.DFe.Document;
using Vip.DFe.Enum;
using Vip.DFe.Serializer;

namespace Vip.DFe.NFe.NotaFiscal.Transporte
{
    public sealed class NFeVolTransp : GenericClone<NFeVolTransp>, INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Constructors

        public NFeVolTransp()
        {
            Lacres = new DFeCollection<NFeLacresTransp>();
        }

        #endregion

        #region Properties

        /// <summary>
        ///     X27 - Quantidade de volumes transportados
        /// </summary>
        [DFeElement(TipoCampo.Int, "qVol", Id = "X27", Min = 1, Max = 15, Ocorrencia = Ocorrencia.MaiorQueZero)]
        public int QVol { get; set; }

        /// <summary>
        ///     X28 - Espécie dos volumes transportados
        /// </summary>
        [DFeElement(TipoCampo.Str, "esp", Id = "X28", Min = 1, Max = 60, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public string Especie { get; set; }

        /// <summary>
        ///     X29 - Marca dos volumes transportados
        /// </summary>
        [DFeElement(TipoCampo.Str, "marca", Id = "X29", Min = 1, Max = 60, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public string Marca { get; set; }

        /// <summary>
        ///     X30 - Numeração dos volumes transportados
        /// </summary>
        [DFeElement(TipoCampo.Str, "nVol", Id = "X30", Min = 1, Max = 60, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public string NVol { get; set; }

        /// <summary>
        ///     X31 - Peso Líquido (em kg)
        /// </summary>
        [DFeElement(TipoCampo.De3, "pesoL", Id = "X31", Min = 4, Max = 15, Ocorrencia = Ocorrencia.MaiorQueZero)]
        public decimal PesoL { get; set; }

        /// <summary>
        ///     X32 - Peso Bruto (em kg)
        /// </summary>
        [DFeElement(TipoCampo.De3, "pesoB", Id = "X32", Min = 4, Max = 15, Ocorrencia = Ocorrencia.MaiorQueZero)]
        public decimal PesoB { get; set; }

        /// <summary>
        ///     X33 - Grupo Lacres
        ///     <para>Ocorrência: 0-5000</para>
        /// </summary>
        [DFeCollection("lacres", Id = "X33", MinSize = 0, MaxSize = 5000, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public DFeCollection<NFeLacresTransp> Lacres { get; set; }

        #endregion
    }
}