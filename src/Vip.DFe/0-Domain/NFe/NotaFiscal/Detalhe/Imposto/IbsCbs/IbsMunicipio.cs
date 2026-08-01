using System.ComponentModel;
using Vip.DFe.Attributes;
using Vip.DFe.Enum;
using Vip.DFe.Serializer;

namespace Vip.DFe.NFe.NotaFiscal.Detalhe.Imposto.IbsCbs
{
    /// <summary>
    ///     Grupo de Informações do IBS no Município
    /// </summary>
    public class IbsMunicipio : GenericClone<IbsMunicipio>, INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Properties

        /// <summary>
        ///     UB37 - Alíquota do IBS Municipal (em percentual)
        /// </summary>
        [DFeElement(TipoCampo.De4, "pIBSMun", Id = "UB37", Min = 5, Max = 10, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal PIbsMun { get; set; }

        /// <summary>
        ///     UB40 - Grupo de campos do Diferimento
        /// </summary>
        [DFeElement("gDif", Id = "UB40", Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public Diferimento Diferimento { get; set; }

        /// <summary>
        ///     UB43 - Grupo de Informações da devolução de tributos
        /// </summary>
        [DFeElement("gDevTrib", Id = "UB43", Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public DevolucaoTributo DevolucaoTributo { get; set; }

        /// <summary>
        ///     UB45 - Grupo de campos da redução de alíquota
        /// </summary>
        [DFeElement("gRed", Id = "UB45", Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public ReducaoAliquota ReducaoAliquota { get; set; }

        /// <summary>
        ///     UB54 - Valor do IBS Municipal
        /// </summary>
        [DFeElement(TipoCampo.De2, "vIBSMun", Id = "UB54", Min = 3, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal VIbsMun { get; set; }

        #endregion
    }
}