using System.ComponentModel;
using Vip.DFe.Attributes;
using Vip.DFe.Enum;
using Vip.DFe.Serializer;

namespace Vip.DFe.NFe.NotaFiscal.Detalhe.Imposto.IbsCbs
{
    /// <summary>
    ///     Grupo de informações da Tributação Monofásica sujeita a retenção da CBS (ad rem)
    /// </summary>
    public class CbsAdRemReten : GenericClone<CbsAdRemReten>, INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Properties

        /// <summary>
        ///     UB97a - Quantidade tributada sujeita a retenção
        /// </summary>
        [DFeElement(TipoCampo.De4, "qBCMonoReten", Id = "UB97a", Min = 5, Max = 16, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal QbcMonoReten { get; set; }

        /// <summary>
        ///     UB97b - Alíquota ad rem da CBS sujeita a retenção
        /// </summary>
        [DFeElement(TipoCampo.De4, "adRemCBSReten", Id = "UB97b", Min = 5, Max = 10, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal AdRemCbsReten { get; set; }

        /// <summary>
        ///     UB97c - Valor da CBS monofásica sujeita a retenção
        /// </summary>
        [DFeElement(TipoCampo.De2, "vCBSMonoReten", Id = "UB97c", Min = 3, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal VCbsMonoReten { get; set; }

        #endregion
    }
}