using System.ComponentModel;
using Vip.DFe.Attributes;
using Vip.DFe.Enum;
using Vip.DFe.Serializer;

namespace Vip.DFe.NFe.NotaFiscal.Detalhe.Imposto.IbsCbs
{
    /// <summary>
    ///     Grupo de informações da composição do valor do IBS e da CBS em compras governamentais
    /// </summary>
    public class TributacaoCompraGov : GenericClone<TributacaoCompraGov>, INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Properties

        /// <summary>
        ///     UB82b - Alíquota do IBS da UF aplicada
        /// </summary>
        [DFeElement(TipoCampo.De4, "pAliqIBSUF", Id = "UB82b", Min = 5, Max = 10, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal PAliqIbsUf { get; set; }

        /// <summary>
        ///     UB82c - Valor que seria devido a UF, sem aplicação do Art. 473 da LC 214/2025
        /// </summary>
        [DFeElement(TipoCampo.De2, "vTribIBSUF", Id = "UB82c", Min = 3, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal VTribIbsUf { get; set; }

        /// <summary>
        ///     UB82d - Alíquota do IBS do Município aplicada
        /// </summary>
        [DFeElement(TipoCampo.De4, "pAliqIBSMun", Id = "UB82d", Min = 5, Max = 10, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal PAliqIbsMun { get; set; }

        /// <summary>
        ///     UB82e - Valor que seria devido ao município, sem aplicação do Art. 473 da LC 214/2025
        /// </summary>
        [DFeElement(TipoCampo.De2, "vTribIBSMun", Id = "UB82e", Min = 3, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal VTribIbsMun { get; set; }

        /// <summary>
        ///     UB82f - Alíquota da CBS aplicada
        /// </summary>
        [DFeElement(TipoCampo.De4, "pAliqCBS", Id = "UB82f", Min = 5, Max = 10, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal PAliqCbs { get; set; }

        /// <summary>
        ///     UB82g - Valor que seria devido a CBS, sem aplicação do Art. 473 da LC 214/2025
        /// </summary>
        [DFeElement(TipoCampo.De2, "vTribCBS", Id = "UB82g", Min = 3, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal VTribCbs { get; set; }

        #endregion
    }
}