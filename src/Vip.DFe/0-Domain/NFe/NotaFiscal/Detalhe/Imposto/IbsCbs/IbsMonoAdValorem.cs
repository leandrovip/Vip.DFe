using System.ComponentModel;
using Vip.DFe.Attributes;
using Vip.DFe.Enum;

namespace Vip.DFe.NFe.NotaFiscal.Detalhe.Imposto.IbsCbs
{
    /// <summary>
    ///     Grupo de informações da monofasia do IBS por alíquota ad valorem
    /// </summary>
    public class IbsMonoAdValorem : GenericClone<IbsMonoAdValorem>, INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Properties

        /// <summary>
        ///     UB91 - Grupo de informações da Tributação Monofásica padrão
        /// </summary>
        [DFeElement("gMonoPadrao", Id = "UB91", Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public IbsAdValoremPadrao MonoPadrao { get; set; }

        /// <summary>
        ///     UB92 - Grupo de informações da Tributação Monofásica sujeita a retenção
        /// </summary>
        [DFeElement("gMonoReten", Id = "UB92", Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public IbsAdValoremReten MonoReten { get; set; }

        /// <summary>
        ///     UB93 - Grupo de informações da Tributação Monofásica retida anteriormente
        /// </summary>
        [DFeElement("gMonoRet", Id = "UB93", Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public IbsAdValoremRet MonoRet { get; set; }

        /// <summary>
        ///     UB94 - Grupo de informações do diferimento do biodiesel na Tributação Monofásica
        /// </summary>
        [DFeElement("gpBioDiferenca", Id = "UB94", Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public AdValoremBioDiferenca BioDiferenca { get; set; }

        #endregion
    }
}