using System.ComponentModel;
using Vip.DFe.Attributes;
using Vip.DFe.Enum;

namespace Vip.DFe.NFe.NotaFiscal.Detalhe.Imposto.IbsCbs
{
    /// <summary>
    ///     Grupo de informações da monofasia da CBS por alíquota ad valorem
    /// </summary>
    public class CbsMonoAdValorem : GenericClone<CbsMonoAdValorem>, INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Properties

        /// <summary>
        ///     UB101 - Grupo de informações da Tributação Monofásica padrão
        /// </summary>
        [DFeElement("gMonoPadrao", Id = "UB101", Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public CbsAdValoremPadrao MonoPadrao { get; set; }

        /// <summary>
        ///     UB102 - Grupo de informações da Tributação Monofásica sujeita a retenção
        /// </summary>
        [DFeElement("gMonoReten", Id = "UB102", Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public CbsAdValoremReten MonoReten { get; set; }

        /// <summary>
        ///     UB103 - Grupo de informações da Tributação Monofásica retida anteriormente
        /// </summary>
        [DFeElement("gMonoRet", Id = "UB103", Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public CbsAdValoremRet MonoRet { get; set; }

        /// <summary>
        ///     UB104 - Grupo de informações do diferimento do biodiesel na Tributação Monofásica
        /// </summary>
        [DFeElement("gpBioDiferenca", Id = "UB104", Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public AdValoremBioDiferenca BioDiferenca { get; set; }

        #endregion
    }
}