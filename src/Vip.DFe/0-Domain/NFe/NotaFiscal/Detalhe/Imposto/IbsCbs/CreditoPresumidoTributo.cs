using System.ComponentModel;
using Vip.DFe.Attributes;
using Vip.DFe.Enum;
using Vip.DFe.Serializer;

namespace Vip.DFe.NFe.NotaFiscal.Detalhe.Imposto.IbsCbs
{
    /// <summary>
    ///     Grupo de Informações do Crédito Presumido do IBS/CBS
    ///     <br /> pCredPres/vCredPres/vCredPresCondSus - IBS (UB124/UB125/UB126), CBS (UB128/UB129/UB130)
    /// </summary>
    public class CreditoPresumidoTributo : GenericClone<CreditoPresumidoTributo>, INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Properties

        /// <summary>
        ///     Percentual do Crédito Presumido
        ///     <br /> UB124 (IBS), UB128 (CBS)
        /// </summary>
        [DFeElement(TipoCampo.De4, "pCredPres", Min = 5, Max = 10, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal PCredPres { get; set; }

        /// <summary>
        ///     Valor do Crédito Presumido
        ///     <br /> UB125 (IBS), UB129 (CBS)
        /// </summary>
        [DFeElement(TipoCampo.De2, "vCredPres", Min = 3, Max = 15, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public decimal VCredPres { get; set; }

        /// <summary>
        ///     Valor do Crédito Presumido Condição Suspensiva, preencher apenas para cCredPres que possui indicação de Condição
        ///     Suspensiva
        ///     <br /> UB126 (IBS), UB130 (CBS)
        /// </summary>
        [DFeElement(TipoCampo.De2, "vCredPresCondSus", Min = 3, Max = 15, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public decimal VCredPresCondSus { get; set; }

        #endregion
    }
}