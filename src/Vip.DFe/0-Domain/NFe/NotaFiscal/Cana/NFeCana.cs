using System.ComponentModel;
using System.Linq;
using Vip.DFe.Attributes;
using Vip.DFe.Document;
using Vip.DFe.Enum;
using Vip.DFe.Serializer;

namespace Vip.DFe.NFe.NotaFiscal.Cana
{
    /// <summary>
    ///     ZC01 - Grupo Cana
    /// </summary>
    public sealed class NFeCana : GenericClone<NFeCana>, INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Constructors

        public NFeCana()
        {
            FornecedimentoDiario = new DFeCollection<NFeCanaFornecimento>();
            DeducaoTaxa = new DFeCollection<NFeCanaDeducao>();
        }

        #endregion

        #region Properties

        /// <summary>
        ///     ZC02 - Identificação da safra
        /// </summary>
        [DFeElement(TipoCampo.Str, "safra", Id = "ZC02", Min = 4, Max = 9, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string Safra { get; set; }

        /// <summary>
        ///     ZC03 - Mês e ano de referência
        /// </summary>
        [DFeElement(TipoCampo.StrNumber, "ref", Id = "ZC03", Min = 7, Max = 7, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string MesAnoRerefencia { get; set; }

        /// <summary>
        ///     ZC04 - Grupo Fornecimento diário de cana
        /// </summary>
        [DFeCollection("forDia", Id = "ZC04", MinSize = 1, MaxSize = 31, Ocorrencia = Ocorrencia.Obrigatoria)]
        public DFeCollection<NFeCanaFornecimento> FornecedimentoDiario { get; set; }

        /// <summary>
        ///     ZC10 - Grupo Deduções – Taxas e Contribuições
        /// </summary>
        [DFeCollection("deduc", Id = "ZC10", MinSize = 0, MaxSize = 10, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public DFeCollection<NFeCanaDeducao> DeducaoTaxa { get; set; }

        #endregion

        #region Methods

        private bool ShouldSerializeDeducaoTaxa()
        {
            return DeducaoTaxa.Any();
        }

        #endregion
    }
}