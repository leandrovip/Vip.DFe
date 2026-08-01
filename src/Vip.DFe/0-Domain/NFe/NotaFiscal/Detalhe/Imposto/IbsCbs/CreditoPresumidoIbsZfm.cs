using System.ComponentModel;
using Vip.DFe.Attributes;
using Vip.DFe.Enum;
using Vip.DFe.NFe.Enum;
using Vip.DFe.Serializer;

namespace Vip.DFe.NFe.NotaFiscal.Detalhe.Imposto.IbsCbs
{
    /// <summary>
    ///     Grupo de Informações do crédito presumido de IBS para fornecimentos a partir da ZFM
    /// </summary>
    public class CreditoPresumidoIbsZfm : GenericClone<CreditoPresumidoIbsZfm>, INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Properties

        /// <summary>
        ///     UB132 - Ano e mês referência do período de apuração (AAAA-MM)
        /// </summary>
        [DFeElement(TipoCampo.Str, "competApur", Id = "UB132", Min = 7, Max = 7, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string CompetApur { get; set; }

        /// <summary>
        ///     UB133 - Classificação de acordo com o art. 450, § 1º, da LC 214/25 para o cálculo do crédito presumido na ZFM
        /// </summary>
        [DFeElement(TipoCampo.Enum, "tpCredPresIBSZFM", Id = "UB133", Min = 1, Max = 1, Ocorrencia = Ocorrencia.Obrigatoria)]
        public NFeTipoCredPresIbsZFM TpCredPresIbsZfm { get; set; }

        /// <summary>
        ///     UB134 - Valor do crédito presumido calculado sobre o saldo devedor apurado
        /// </summary>
        [DFeElement(TipoCampo.De2, "vCredPresIBSZFM", Id = "UB134", Min = 3, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal VCredPresIbsZfm { get; set; }

        #endregion
    }
}