using System.ComponentModel;
using Vip.DFe.Attributes;
using Vip.DFe.Enum;
using Vip.DFe.Serializer;

namespace Vip.DFe.NFe.NotaFiscal.Cobranca
{
    public sealed class NFeCobrFatura : GenericClone<NFeCobrFatura>, INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Properties

        /// <summary>
        ///     Y03 - Número da Fatura
        /// </summary>
        [DFeElement(TipoCampo.Str, "nFat", Id = "Y03", Min = 1, Max = 60, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public string NFat { get; set; }

        /// <summary>
        ///     Y04 - Valor Original da Fatura
        /// </summary>
        [DFeElement(TipoCampo.De2, "vOrig", Id = "Y04", Min = 3, Max = 15, Ocorrencia = Ocorrencia.MaiorQueZero)]
        public decimal VOrig { get; set; }

        /// <summary>
        ///     Y05 - Valor do desconto
        /// </summary>
        [DFeElement(TipoCampo.De2, "vDesc", Id = "Y05", Min = 3, Max = 15, Ocorrencia = Ocorrencia.MaiorQueZero)]
        public decimal VDesc { get; set; }

        /// <summary>
        ///     Y06 - Valor Líquido da Fatura
        /// </summary>
        [DFeElement(TipoCampo.De2, "vLiq", Id = "Y06", Min = 3, Max = 15, Ocorrencia = Ocorrencia.MaiorQueZero)]
        public decimal VLiq { get; set; }

        #endregion
    }
}