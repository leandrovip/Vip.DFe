using System;
using System.ComponentModel;
using Vip.DFe.Attributes;
using Vip.DFe.Enum;
using Vip.DFe.Serializer;

namespace Vip.DFe.NFe.NotaFiscal.Cobranca
{
    public sealed class NFeCobrDuplicata : GenericClone<NFeCobrDuplicata>, INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Properties

        /// <summary>
        ///     Y08 - Número da Duplicata
        /// </summary>
        [DFeElement(TipoCampo.Str, "nDup", Id = "Y08", Min = 1, Max = 60, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public string NDup { get; set; }

        /// <summary>
        ///     Y09 - Data de vencimento
        /// </summary>
        [DFeElement(TipoCampo.Dat, "dVenc", Id = "Y09", Min = 10, Max = 10, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public DateTime DVenc { get; set; }

        /// <summary>
        ///     Y10 - Valor da duplicata
        /// </summary>
        [DFeElement(TipoCampo.De2, "vDup", Id = "Y10", Min = 3, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal VDup { get; set; }

        #endregion
    }
}