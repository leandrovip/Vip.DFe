using System.ComponentModel;
using Vip.DFe.Attributes;
using Vip.DFe.Enum;
using Vip.DFe.Serializer;

namespace Vip.DFe.NFe.NotaFiscal.Total
{
    public sealed class NFeIbsCbsTot : GenericClone<NFeIbsCbsTot>, INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Properties

        /// <summary>
        ///     W35 - Base de Cálculo do IBS/CBS
        /// </summary>
        [DFeElement(TipoCampo.De2, "vBCIBSCBS", Id = "W35", Min = 3, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal VBcIbsCbs { get; set; }

        /// <summary>
        ///     W36 - Grupo de informações do IBS
        /// </summary>
        [DFeElement("gIBS", Id = "W36", Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public NFeIbsTot Ibs { get; set; }

        /// <summary>
        ///     W50 - Grupo de informações da CBS
        /// </summary>
        [DFeElement("gCBS", Id = "W50", Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public NFeCbsTot Cbs { get; set; }

        /// <summary>
        ///     W57 - Grupo de informações da monofasia
        /// </summary>
        [DFeElement("gMono", Id = "W57", Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public NFeMonoTot Mono { get; set; }

        /// <summary>
        ///     W59e - Grupo de informações do estorno de crédito
        /// </summary>
        [DFeElement("gEstornoCred", Id = "W59e", Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public NFeEstornoCreditoTot EstornoCredito { get; set; }

        #endregion
    }
}