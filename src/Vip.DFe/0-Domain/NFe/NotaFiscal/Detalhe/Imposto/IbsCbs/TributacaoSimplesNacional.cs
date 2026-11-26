using System.ComponentModel;
using Vip.DFe.Attributes;
using Vip.DFe.Enum;

namespace Vip.DFe.NFe.NotaFiscal.Detalhe.Imposto.IbsCbs
{
    /// <summary>
    ///     UB115a - Grupo de informações da composição do valor do IBS e da CBS para o Simples Nacional (NT 2026.005)
    /// </summary>
    public class TributacaoSimplesNacional : GenericClone<TributacaoSimplesNacional>, INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Constructors

        public TributacaoSimplesNacional()
        {
            CbsSN = new CbsSimplesNacional();
        }

        #endregion

        #region Properties

        /// <summary>
        ///     UB115b - Grupo de informações da composição do valor do IBS para o Simples Nacional
        /// </summary>
        [DFeElement("gIBSSN", Id = "UB115b", Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public IbsSimplesNacional IbsSN { get; set; }

        /// <summary>
        ///     UB115e - Grupo de informações da composição do valor da CBS para o Simples Nacional
        /// </summary>
        [DFeElement("gCBSSN", Id = "UB115e", Ocorrencia = Ocorrencia.Obrigatoria)]
        public CbsSimplesNacional CbsSN { get; set; }

        #endregion

        #region Methods

        // O serializer instancia grupos ao desserializar; grupos zerados são tratados como não informados (padrão de IbsCbs).
        internal bool PossuiIbsSN => IbsSN != null && (IbsSN.PIbsSN != 0 || IbsSN.VIbsSN != 0);

        internal bool PossuiCbsSN => CbsSN != null && (CbsSN.PCbsSN != 0 || CbsSN.VCbsSN != 0);

        private bool ShouldSerializeIbsSN() => PossuiIbsSN;

        private bool ShouldSerializeCbsSN() => PossuiCbsSN || PossuiIbsSN;

        #endregion
    }
}