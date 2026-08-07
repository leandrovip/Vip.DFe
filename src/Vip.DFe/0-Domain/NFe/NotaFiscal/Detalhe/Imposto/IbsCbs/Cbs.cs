using System.ComponentModel;
using Vip.DFe.Attributes;
using Vip.DFe.Enum;
using Vip.DFe.Extensions;
using Vip.DFe.Serializer;

namespace Vip.DFe.NFe.NotaFiscal.Detalhe.Imposto.IbsCbs
{
    /// <summary>
    ///     Grupo de Tributação da CBS
    /// </summary>
    public class Cbs : GenericClone<Cbs>, INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Properties

        /// <summary>
        ///     UB56 - Alíquota da CBS (em percentual)
        /// </summary>
        [DFeElement(TipoCampo.De4, "pCBS", Id = "UB56", Min = 5, Max = 10, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal PCbs { get; set; }

        /// <summary>
        ///     UB59 - Grupo de campos do Diferimento
        /// </summary>
        [DFeElement("gDif", Id = "UB59", Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public Diferimento Diferimento { get; set; }

        /// <summary>
        ///     UB62 - Grupo de Informações da devolução de tributos
        /// </summary>
        [DFeElement("gDevTrib", Id = "UB62", Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public DevolucaoTributo DevolucaoTributo { get; set; }

        /// <summary>
        ///     UB64 - Grupo de campos da redução de alíquota
        /// </summary>
        [DFeElement("gRed", Id = "UB64", Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public ReducaoAliquota ReducaoAliquota { get; set; }

        /// <summary>
        ///     UB66a - Grupo de operações em áreas incentivadas (ALC/ZFM) - CBS (alíquota zero)
        /// </summary>
        [DFeElement("gALCZFMCBS", Id = "UB66a", Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public AlcZfmCbs AlcZfmCbs { get; set; }

        /// <summary>
        ///     UB67 - Valor da CBS
        /// </summary>
        [DFeElement(TipoCampo.De2, "vCBS", Id = "UB67", Min = 3, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal VCbs { get; set; }

        #endregion

        #region Métodos Privados

        private bool ShouldSerializeDiferimento() => Diferimento.IsNotNull() && (Diferimento.PDif != 0 || Diferimento.VDif != 0);

        private bool ShouldSerializeDevolucaoTributo() => DevolucaoTributo.IsNotNull() && (DevolucaoTributo.PDevTrib != 0 || DevolucaoTributo.VDevTrib != 0);

        private bool ShouldSerializeReducaoAliquota() => ReducaoAliquota.IsNotNull() && (ReducaoAliquota.PAliqEfet != 0 || ReducaoAliquota.PRedAliq != 0);

        private bool ShouldSerializeAlcZfmCbs() => AlcZfmCbs?.TpAlcZfmCbs != 0;

        #endregion
    }
}