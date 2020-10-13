using System.ComponentModel;
using Vip.DFe.Attributes;
using Vip.DFe.Enum;
using Vip.DFe.Serializer;

namespace Vip.DFe.SAT.CupomFiscal
{
    /// <summary>
    ///     Grupo de Valores Totais referentes ao ISSQN
    /// </summary>
    public sealed class CFeTotalIssqn : GenericClone<CFeTotalIssqn>, INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion Events

        #region Properties

        /// <summary>
        ///     Valor Total da Base de Cálculo do ISSQN
        ///     <br /><b>Campo preenchido pelo SAT</b>
        /// </summary>
        [DFeElement(TipoCampo.De2, "vBC", Id = "W13", Min = 3, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal VBC { get; set; }

        /// <summary>
        ///     Valor Total do ISS
        ///     <br /><b>Campo preenchido pelo SAT</b>
        /// </summary>
        [DFeElement(TipoCampo.De2, "vISS", Id = "W14", Min = 3, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal VISS { get; set; }

        /// <summary>
        ///     Valor Total do PIS sobre serviços
        ///     <br /><b>Campo preenchido pelo SAT</b>
        /// </summary>
        [DFeElement(TipoCampo.De2, "vPIS", Id = "W15", Min = 3, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal VPIS { get; set; }

        /// <summary>
        ///     Valor Total do COFINS sobre serviços
        ///     <br /><b>Campo preenchido pelo SAT</b>
        /// </summary>
        [DFeElement(TipoCampo.De2, "vCOFINS", Id = "W16", Min = 3, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal VCOFINS { get; set; }

        /// <summary>
        ///     Valor Total do PISST sobre serviços
        ///     <br /><b>Campo preenchido pelo SAT</b>
        /// </summary>
        [DFeElement(TipoCampo.De2, "vPISST", Id = "W17", Min = 3, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal VPISST { get; set; }

        /// <summary>
        ///     Valor Total do COFINS-ST sobre serviços
        ///     <br /><b>Campo preenchido pelo SAT</b>
        /// </summary>
        [DFeElement(TipoCampo.De2, "vCOFINSST", Id = "W18", Min = 3, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal VCOFINSST { get; set; }

        #endregion Properties
    }
}