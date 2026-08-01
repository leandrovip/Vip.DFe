using System.ComponentModel;
using Vip.DFe.Attributes;
using Vip.DFe.Enum;

namespace Vip.DFe.NFe.NotaFiscal.Detalhe.Imposto.IbsCbs
{
    /// <summary>
    ///     Grupo de informações da monofasia do IBS por alíquota ad rem
    /// </summary>
    public class IbsMonoAdRem : GenericClone<IbsMonoAdRem>, INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Properties

        /// <summary>
        ///     UB86 - Grupo de informações da Tributação Monofásica padrão
        /// </summary>
        [DFeElement("gMonoPadrao", Id = "UB86", Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public IbsAdRemPadrao MonoPadrao { get; set; }

        /// <summary>
        ///     UB87 - Grupo de informações da Tributação Monofásica sujeita a retenção
        /// </summary>
        [DFeElement("gMonoReten", Id = "UB87", Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public IbsAdRemReten MonoReten { get; set; }

        /// <summary>
        ///     UB88 - Grupo de informações da Tributação Monofásica retida anteriormente
        /// </summary>
        [DFeElement("gMonoRet", Id = "UB88", Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public IbsAdRemRet MonoRet { get; set; }

        /// <summary>
        ///     UB89 - Grupo de informações do diferimento do biodiesel na Tributação Monofásica
        /// </summary>
        [DFeElement("gpBioDiferenca", Id = "UB89", Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public AdRemBioDiferenca BioDiferenca { get; set; }

        #endregion
    }
}