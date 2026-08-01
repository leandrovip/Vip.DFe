using System.ComponentModel;
using Vip.DFe.Attributes;
using Vip.DFe.Enum;
using Vip.DFe.Serializer;

namespace Vip.DFe.NFe.NotaFiscal.Total
{
    public sealed class NFeIbsTot : GenericClone<NFeIbsTot>, INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Constructors

        public NFeIbsTot()
        {
            IbsUf = new NFeIbsUfTot();
            IbsMunicipio = new NFeIbsMunicipioTot();
        }

        #endregion

        #region Properties

        /// <summary>
        ///     W37 - Grupo de informações do IBS na UF
        /// </summary>
        [DFeElement("gIBSUF", Id = "W37", Ocorrencia = Ocorrencia.Obrigatoria)]
        public NFeIbsUfTot IbsUf { get; set; }

        /// <summary>
        ///     W42 - Grupo de informações do IBS no Município
        /// </summary>
        [DFeElement("gIBSMun", Id = "W42", Ocorrencia = Ocorrencia.Obrigatoria)]
        public NFeIbsMunicipioTot IbsMunicipio { get; set; }

        /// <summary>
        ///     W47 - Valor do IBS
        /// </summary>
        [DFeElement(TipoCampo.De2, "vIBS", Id = "W47", Min = 3, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal VIbs { get; set; }

        /// <summary>
        ///     W48 - Valor do Crédito Presumido do IBS
        /// </summary>
        [DFeElement(TipoCampo.De2, "vCredPres", Id = "W48", Min = 3, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal VCredPres { get; set; }

        /// <summary>
        ///     W49 - Valor do Crédito Presumido do IBS com Condição Suspensiva
        /// </summary>
        [DFeElement(TipoCampo.De2, "vCredPresCondSus", Id = "W49", Min = 3, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal VCredPresCondSus { get; set; }

        #endregion
    }
}