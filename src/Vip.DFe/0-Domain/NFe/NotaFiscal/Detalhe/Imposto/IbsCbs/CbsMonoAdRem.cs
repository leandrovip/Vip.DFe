using System.ComponentModel;
using Vip.DFe.Attributes;
using Vip.DFe.Enum;

namespace Vip.DFe.NFe.NotaFiscal.Detalhe.Imposto.IbsCbs
{
    /// <summary>
    ///     Grupo de informações da monofasia da CBS por alíquota ad rem
    /// </summary>
    public class CbsMonoAdRem : GenericClone<CbsMonoAdRem>, INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Properties

        /// <summary>
        ///     UB96 - Grupo de informações da Tributação Monofásica padrão
        /// </summary>
        [DFeElement("gMonoPadrao", Id = "UB96", Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public CbsAdRemPadrao MonoPadrao { get; set; }

        /// <summary>
        ///     UB97 - Grupo de informações da Tributação Monofásica sujeita a retenção
        /// </summary>
        [DFeElement("gMonoReten", Id = "UB97", Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public CbsAdRemReten MonoReten { get; set; }

        /// <summary>
        ///     UB98 - Grupo de informações da Tributação Monofásica retida anteriormente
        /// </summary>
        [DFeElement("gMonoRet", Id = "UB98", Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public CbsAdRemRet MonoRet { get; set; }

        /// <summary>
        ///     UB99 - Grupo de informações do diferimento do biodiesel na Tributação Monofásica
        /// </summary>
        [DFeElement("gpBioDiferenca", Id = "UB99", Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public AdRemBioDiferenca BioDiferenca { get; set; }

        #endregion
    }
}