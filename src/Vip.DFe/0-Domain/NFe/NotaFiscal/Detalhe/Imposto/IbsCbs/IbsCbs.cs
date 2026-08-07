using System.ComponentModel;
using Vip.DFe.Attributes;
using Vip.DFe.Enum;
using Vip.DFe.Extensions;
using Vip.DFe.Serializer;

namespace Vip.DFe.NFe.NotaFiscal.Detalhe.Imposto.IbsCbs
{
    /// <summary>
    ///     Grupo de informações dos tributos IBS, CBS e Imposto Seletivo
    /// </summary>
    public class IbsCbs : GenericClone<IbsCbs>, INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Properties

        /// <summary>
        ///     UB13 - Código de Situação Tributária do IBS/CBS
        /// </summary>
        [DFeElement(TipoCampo.StrNumberFill, "CST", Id = "UB13", Min = 3, Max = 3, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string Cst { get; set; }

        /// <summary>
        ///     UB14 - Código de Classificação Tributária do IBS e da CBS
        /// </summary>
        [DFeElement(TipoCampo.StrNumberFill, "cClassTrib", Id = "UB14", Min = 6, Max = 6, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string CClassTrib { get; set; }

        /// <summary>
        ///     UB14a - Indica se a operação é de doação, informar 1 quando doação.
        /// </summary>
        [DFeElement(TipoCampo.Int, "indDoacao", Id = "UB14a", Min = 1, Max = 1, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public int IndDoacao { get; set; }

        /// <summary>
        ///     UB15 - Grupo de informações do IBS e da CBS
        /// </summary>
        [DFeElement("gIBSCBS", Id = "UB15", Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public GrupoIbsCbs GrupoIbsCbs { get; set; }

        /// <summary>
        ///     UB84 - Grupo de informações da monofasia do IBS e da CBS
        /// </summary>
        [DFeElement("gIBSCBSMono", Id = "UB84", Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public IbsCbsMono IbsCbsMono { get; set; }

        /// <summary>
        ///     UB106 - Grupo de Transferência de Crédito
        /// </summary>
        [DFeElement("gTransfCred", Id = "UB106", Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public TransferenciaCredito TransferenciaCredito { get; set; }

        /// <summary>
        ///     UB112 - Grupo de Ajuste de Competência
        /// </summary>
        [DFeElement("gAjusteCompet", Id = "UB112", Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public AjusteCompetencia AjusteCompetencia { get; set; }

        /// <summary>
        ///     UB116 - Grupo do Estorno de Crédito
        ///     <br /> Informado conforme indicador no cClassTrib
        /// </summary>
        [DFeElement("gEstornoCred", Id = "UB116", Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public EstornoCredito EstornoCredito { get; set; }

        /// <summary>
        ///     UB120 - Grupo do Crédito Presumido da Operação
        ///     <br /> Informado conforme indicador no cClassTrib
        /// </summary>
        [DFeElement("gCredPresOper", Id = "UB120", Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public CreditoPresumidoOperacao CreditoPresumidoOperacao { get; set; }

        /// <summary>
        ///     UB131 - Grupo do Crédito Presumido de IBS para fornecimentos a partir da ZFM
        ///     <br /> Classificação de acordo com o art. 450, § 1º, da LC 214/25 para o cálculo do crédito presumido na ZFM.
        ///     Informado conforme indicador no cClassTrib.
        /// </summary>
        [DFeElement("gCredPresIBSZFM", Id = "UB131", Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public CreditoPresumidoIbsZfm CreditoPresumidoIbsZfm { get; set; }

        #endregion

        #region Methods

        private bool ShouldSerializeIndDoacao()
        {
            return IndDoacao == 1;
        }

        private bool ShouldSerializeGrupoIbsCbs() => GrupoIbsCbs.IsNotNull() && GrupoIbsCbs.VBc != 0;

        private bool ShouldSerializeIbsCbsMono() => IbsCbsMono.IsNotNull() && (IbsCbsMono.VTotCbsMonoItem != 0 || IbsCbsMono.VTotIbsMonoItem != 0);

        private bool ShouldSerializeTransferenciaCredito() => TransferenciaCredito.IsNotNull() && (TransferenciaCredito.VCbs != 0 || TransferenciaCredito.VIbs != 0);

        private bool ShouldSerializeAjusteCompetencia() => AjusteCompetencia.IsNotNull() && (AjusteCompetencia.VIbs != 0 || AjusteCompetencia.VCbs != 0);

        private bool ShouldSerializeEstornoCredito() => EstornoCredito.IsNotNull() && (EstornoCredito.VCbsEstCred != 0 || EstornoCredito.VIbsEstCred != 0);

        private bool ShouldSerializeCreditoPresumidoOperacao() => CreditoPresumidoOperacao.IsNotNull() && CreditoPresumidoOperacao.CCredPres.IsNotNullOrEmpty();

        private bool ShouldSerializeCreditoPresumidoIbsZfm() => CreditoPresumidoIbsZfm.IsNotNull() && CreditoPresumidoIbsZfm.CompetApur.IsNotNullOrEmpty();

        #endregion
    }
}