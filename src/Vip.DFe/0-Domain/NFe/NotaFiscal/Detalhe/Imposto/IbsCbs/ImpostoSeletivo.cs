using System.ComponentModel;
using Vip.DFe.Attributes;
using Vip.DFe.Enum;
using Vip.DFe.Extensions;
using Vip.DFe.Serializer;

namespace Vip.DFe.NFe.NotaFiscal.Detalhe.Imposto.IbsCbs
{
    /// <summary>
    ///     Grupo de informações do Imposto Seletivo
    /// </summary>
    public class ImpostoSeletivo : GenericClone<ImpostoSeletivo>, INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Properties

        /// <summary>
        ///     UB02 - Código de Situação Tributária do Imposto Seletivo
        /// </summary>
        [DFeElement(TipoCampo.StrNumberFill, "CSTIS", Id = "UB02", Min = 3, Max = 3, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string CstIS { get; set; }

        /// <summary>
        ///     UB03 - Código de Classificação Tributária do Imposto Seletivo
        /// </summary>
        [DFeElement(TipoCampo.StrNumberFill, "cClassTribIS", Id = "UB03", Min = 6, Max = 6, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string CClassTribIS { get; set; }

        /// <summary>
        ///     UB05 - Valor da Base de Cálculo do Imposto Seletivo
        /// </summary>
        [DFeElement(TipoCampo.De2, "vBCIS", Id = "UB05", Min = 3, Max = 15, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public decimal VBcIS { get; set; }

        /// <summary>
        ///     UB06 - Alíquota do Imposto Seletivo (percentual)
        /// </summary>
        [DFeElement(TipoCampo.De4, "pIS", Id = "UB06", Min = 5, Max = 10, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public decimal PIs { get; set; }

        /// <summary>
        ///     UB07 - Alíquota do Imposto Seletivo (por valor)
        /// </summary>
        [DFeElement(TipoCampo.De4, "adRemIS", Id = "UB07", Min = 5, Max = 10, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public decimal AdRemIS { get; set; }

        /// <summary>
        ///     UB09 - Unidade de medida apropriada especificada em Lei Ordinária para fins de apuração do Imposto Seletivo
        /// </summary>
        [DFeElement(TipoCampo.Str, "uTrib", Id = "UB09", Min = 1, Max = 6, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public string UTrib { get; set; }

        /// <summary>
        ///     UB10 - Quantidade com base no campo uTrib informado
        /// </summary>
        [DFeElement(TipoCampo.De4, "qTrib", Id = "UB10", Min = 5, Max = 16, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public decimal QTrib { get; set; }

        /// <summary>
        ///     UB11 - Valor do Imposto Seletivo calculado
        /// </summary>
        [DFeElement(TipoCampo.De2, "vIS", Id = "UB11", Min = 3, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal VIS { get; set; }

        #endregion

        #region Methods

        private bool ShouldSerializeVBcIS()
        {
            return VBcIS > 0;
        }

        private bool ShouldSerializePIs()
        {
            return PIs > 0;
        }

        private bool ShouldSerializeAdRemIS()
        {
            return AdRemIS > 0;
        }

        private bool ShouldSerializeUTrib()
        {
            return UTrib.IsNotNullOrEmpty();
        }

        private bool ShouldSerializeQTrib()
        {
            return QTrib > 0;
        }

        #endregion
    }
}