using System.ComponentModel;
using Vip.DFe.Attributes;
using Vip.DFe.Enum;
using Vip.DFe.Serializer;

namespace Vip.DFe.NFe.NotaFiscal.Detalhe.Imposto.IbsCbs
{
    /// <summary>
    ///     Grupo de informações do IBS e da CBS
    /// </summary>
    public class GrupoIbsCbs : GenericClone<GrupoIbsCbs>, INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Constructors

        public GrupoIbsCbs()
        {
            IbsUf = new IbsUf();
            IbsMunicipio = new IbsMunicipio();
            Cbs = new Cbs();
        }

        #endregion

        #region Properties

        /// <summary>
        ///     UB16 - Valor da Base de Cálculo do IBS/CBS
        /// </summary>
        [DFeElement(TipoCampo.De2, "vBC", Id = "UB16", Min = 3, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal VBc { get; set; }

        /// <summary>
        ///     UB17 - Grupo de informações do IBS na UF
        /// </summary>
        [DFeElement("gIBSUF", Id = "UB17", Ocorrencia = Ocorrencia.Obrigatoria)]
        public IbsUf IbsUf { get; set; }

        /// <summary>
        ///     UB36 - Grupo de Informações do IBS no Município
        /// </summary>
        [DFeElement("gIBSMun", Id = "UB36", Ocorrencia = Ocorrencia.Obrigatoria)]
        public IbsMunicipio IbsMunicipio { get; set; }

        /// <summary>
        ///     UB54a - Valor do IBS
        /// </summary>
        [DFeElement(TipoCampo.De2, "vIBS", Id = "UB54a", Min = 3, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal VIbs { get; set; }

        /// <summary>
        ///     UB55 - Grupo de Tributação da CBS
        /// </summary>
        [DFeElement("gCBS", Id = "UB55", Ocorrencia = Ocorrencia.Obrigatoria)]
        public Cbs Cbs { get; set; }

        /// <summary>
        ///     UB68 - Grupo de informações da Tributação Regular.
        ///     <br /> Informar como seria a tributação caso não cumprida a condição resolutória/suspensiva.
        /// </summary>
        [DFeElement("gTribRegular", Id = "UB68", Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public TributacaoRegular TributacaoRegular { get; set; }

        /// <summary>
        ///     UB82a - Grupo de informações da composição do valor do IBS e da CBS em compras governamentais
        /// </summary>
        [DFeElement("gTribCompraGov", Id = "UB82a", Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public TributacaoCompraGov TributacaoCompraGov { get; set; }

        #endregion
    }
}