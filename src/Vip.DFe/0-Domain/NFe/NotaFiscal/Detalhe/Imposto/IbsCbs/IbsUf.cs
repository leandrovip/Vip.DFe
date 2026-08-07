using System.ComponentModel;
using Vip.DFe.Attributes;
using Vip.DFe.Enum;
using Vip.DFe.Extensions;
using Vip.DFe.Serializer;

namespace Vip.DFe.NFe.NotaFiscal.Detalhe.Imposto.IbsCbs
{
    /// <summary>
    ///     Grupo de informações do IBS na UF
    /// </summary>
    public class IbsUf : GenericClone<IbsUf>, INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Properties

        /// <summary>
        ///     UB18 - Alíquota do IBS de competência das UF (em percentual)
        /// </summary>
        [DFeElement(TipoCampo.De4, "pIBSUF", Id = "UB18", Min = 5, Max = 10, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal PIbsUf { get; set; }

        /// <summary>
        ///     UB21 - Grupo de campos do Diferimento
        /// </summary>
        [DFeElement("gDif", Id = "UB21", Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public Diferimento Diferimento { get; set; }

        /// <summary>
        ///     UB24 - Grupo de Informações da devolução de tributos
        /// </summary>
        [DFeElement("gDevTrib", Id = "UB24", Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public DevolucaoTributo DevolucaoTributo { get; set; }

        /// <summary>
        ///     UB26 - Grupo de campos da redução de alíquota
        /// </summary>
        [DFeElement("gRed", Id = "UB26", Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public ReducaoAliquota ReducaoAliquota { get; set; }

        /// <summary>
        ///     UB35 - Valor do IBS de competência das UF
        /// </summary>
        [DFeElement(TipoCampo.De2, "vIBSUF", Id = "UB35", Min = 3, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal VIbsUf { get; set; }

        #endregion

        #region Methods

        private bool ShouldSerializeDiferimento() => Diferimento.IsNotNull() && (Diferimento.PDif != 0 || Diferimento.VDif != 0);

        private bool ShouldSerializeDevolucaoTributo() => DevolucaoTributo.IsNotNull() && (DevolucaoTributo.PDevTrib != 0 || DevolucaoTributo.VDevTrib != 0);

        private bool ShouldSerializeReducaoAliquota() => ReducaoAliquota.IsNotNull() && (ReducaoAliquota.PAliqEfet != 0 || ReducaoAliquota.PRedAliq != 0);

        #endregion
    }
}