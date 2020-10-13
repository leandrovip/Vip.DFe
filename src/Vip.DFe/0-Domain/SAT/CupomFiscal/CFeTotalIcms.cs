using System.ComponentModel;
using Vip.DFe.Attributes;
using Vip.DFe.Enum;
using Vip.DFe.Serializer;

namespace Vip.DFe.SAT.CupomFiscal
{
    /// <summary>
    ///     Grupo de Valores Totais referentes ao ICMS (ICMSTot)
    /// </summary>
    public sealed class CFeTotalIcms : GenericClone<CFeTotalIcms>, INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion Events

        #region Properties

        /// <summary>
        ///     Valor Total do ICMS
        ///     <br /><b>Campo preenchido pelo SAT</b>
        /// </summary>
        [DFeElement(TipoCampo.De2, "vICMS", Id = "W03", Min = 3, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal VICMS { get; set; }

        /// <summary>
        ///     Valor total dos produtos e serviços sujeitos ao ICMS.
        ///     <br /><b>Campo preenchido pelo SAT</b>
        /// </summary>
        [DFeElement(TipoCampo.De2, "vProd", Id = "W04", Min = 3, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal VProd { get; set; }

        /// <summary>
        ///     Valor Total dos Descontos sobre Item
        ///     <br /><b>Campo preenchido pelo SAT</b>
        /// </summary>
        [DFeElement(TipoCampo.De2, "vDesc", Id = "W05", Min = 3, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal VDesc { get; set; }

        /// <summary>
        ///     Valor Total do PIS
        ///     <br /><b>Campo preenchido pelo SAT</b>
        /// </summary>
        [DFeElement(TipoCampo.De2, "vPIS", Id = "W06", Min = 3, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal VPIS { get; set; }

        /// <summary>
        ///     Valor Total do COFINS
        ///     <br /><b>Campo preenchido pelo SAT</b>
        /// </summary>
        [DFeElement(TipoCampo.De2, "vCOFINS", Id = "W07", Min = 3, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal VCOFINS { get; set; }

        /// <summary>
        ///     Valor Total do PISST
        ///     <br /><b>Campo preenchido pelo SAT</b>
        /// </summary>
        [DFeElement(TipoCampo.De2, "vPISST", Id = "W08", Min = 3, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal VPISST { get; set; }

        /// <summary>
        ///     Valor Total do COFINS-ST
        ///     <br /><b>Campo preenchido pelo SAT</b>
        /// </summary>
        [DFeElement(TipoCampo.De2, "vCOFINSST", Id = "W09", Min = 3, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal VCOFINSST { get; set; }

        /// <summary>
        ///     Valor Total de Outras Despesas acessórias sobre Item
        ///     <br /><b>Campo preenchido pelo SAT</b>
        /// </summary>
        [DFeElement(TipoCampo.De2, "vOutro", Id = "W10", Min = 3, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal VOutro { get; set; }

        #endregion Properties
    }
}