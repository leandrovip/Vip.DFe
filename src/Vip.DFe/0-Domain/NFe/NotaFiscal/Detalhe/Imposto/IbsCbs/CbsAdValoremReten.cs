using System.ComponentModel;
using Vip.DFe.Attributes;
using Vip.DFe.Enum;
using Vip.DFe.Serializer;

namespace Vip.DFe.NFe.NotaFiscal.Detalhe.Imposto.IbsCbs
{
    /// <summary>
    ///     Grupo de informações da Tributação Monofásica sujeita a retenção da CBS (ad valorem)
    /// </summary>
    public class CbsAdValoremReten : GenericClone<CbsAdValoremReten>, INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Properties

        /// <summary>
        ///     UB102a - Valor da Base de Cálculo sujeita a retenção
        /// </summary>
        [DFeElement(TipoCampo.De2, "vBCMonoReten", Id = "UB102a", Min = 3, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal VBcMonoReten { get; set; }

        /// <summary>
        ///     UB102b - Alíquota ad valorem da CBS sujeita a retenção
        /// </summary>
        [DFeElement(TipoCampo.De4, "pCBSMonoReten", Id = "UB102b", Min = 5, Max = 10, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal PCbsMonoReten { get; set; }

        /// <summary>
        ///     UB102c - Valor da CBS monofásica sujeita a retenção
        /// </summary>
        [DFeElement(TipoCampo.De2, "vCBSMonoReten", Id = "UB102c", Min = 3, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal VCbsMonoReten { get; set; }

        #endregion
    }
}