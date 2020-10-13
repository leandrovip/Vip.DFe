using System.ComponentModel;
using Vip.DFe.Attributes;
using Vip.DFe.Enum;
using Vip.DFe.Extensions;
using Vip.DFe.SAT.Enum;
using Vip.DFe.Serializer;
using Vip.DFe.Shared.Enum;

namespace Vip.DFe.SAT.CupomFiscal
{
    /// <summary>
    ///     Grupo de identifica��o do emitente do CF-e (emit)
    /// </summary>
    public sealed class CFeEmit : GenericClone<CFeEmit>, INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion Events

        #region Constructors

        public CFeEmit()
        {
            EnderEmit = new CFeEnderEmit();
        }

        #endregion Constructors

        #region Propriedades

        /// <summary>
        ///     CNPJ do emitente
        /// </summary>
        [DFeElement(TipoCampo.StrNumberFill, "CNPJ", Id = "C02", Min = 14, Max = 14, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string CNPJ { get; set; }

        /// <summary>
        ///     Raz�o Social do emitente.
        ///     <para />
        ///     <b>Campo preenchido pelo SAT</b>
        /// </summary>
        [DFeElement(TipoCampo.Str, "xNome", Id = "C03", Min = 1, Max = 60, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public string XNome { get; set; }

        /// <summary>
        ///     Nome fantasia
        ///     <para />
        ///     <b>Campo preenchido pelo SAT</b>
        /// </summary>
        [DFeElement(TipoCampo.Str, "xFant", Id = "C04", Min = 1, Max = 60, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public string XFant { get; set; }

        /// <summary>
        ///     Grupo do Endere�o do emitente
        ///     <para />
        ///     <b>Campo preenchido pelo SAT</b>
        /// </summary>
        [DFeElement("enderEmit", Id = "C05", Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public CFeEnderEmit EnderEmit { get; set; }

        /// <summary>
        ///     Inscri��o Estadual
        /// </summary>
        [DFeElement(TipoCampo.Str, "IE", Id = "C12", Min = 2, Max = 14, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public string IE { get; set; }

        /// <summary>
        ///     Inscri��o Municipal. Este campo deve ser informado, quando ocorrer a emiss�o de CF-e conjugada, com presta��o de
        ///     servi�os sujeitos ao ISSQN e fornecimento de pe�as sujeitos ao ICMS.
        /// </summary>
        [DFeElement(TipoCampo.Str, "IM", Id = "C13", Min = 1, Max = 15, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public string IM { get; set; }

        /// <summary>
        ///     Este campo ser� obrigatoriamente preenchido com:
        ///     <para>1 � Simples Nacional</para>
        ///     <para>3 � Regime Normal</para>
        ///     <b>Campo preechido pelo SAT</b>
        /// </summary>
        [DFeElement(TipoCampo.Enum, "cRegTrib", Id = "C14", Min = 1, Max = 1, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public RegimeTributario? CRegTrib { get; set; }

        /// <summary>
        ///     Regime Especial de Tributa��o do ISSQN
        ///     <br />1 - Microempresa Municipal
        ///     <br />2 - Estimativa
        ///     <br />3 - Sociedade de Profissionais
        ///     <br />4 - Cooperativa
        ///     <br />5 - Microempres�rio Individual (MEI);
        /// </summary>
        [DFeElement(TipoCampo.Enum, "cRegTribISSQN", Id = "C15", Min = 1, Max = 1, Ocorrencia = Ocorrencia.Obrigatoria)]
        public RegTribIssqn CRegTribISSQN { get; set; }

        /// <summary>
        ///     Indicador de rateio do Desconto sobre subtotal entre itens sujeitos � tributa��o pelo ISSQN.
        /// </summary>
        [DFeElement(TipoCampo.Enum, "indRatISSQN", Id = "C16", Min = 1, Max = 1, Ocorrencia = Ocorrencia.Obrigatoria)]
        public RatIssqn IndRatISSQN { get; set; }

        #endregion Propriedades

        #region Methods

        private bool ShouldSerializeXNome()
        {
            return !XNome.IsNullOrEmpty();
        }

        private bool ShouldSerializeXFant()
        {
            return !XFant.IsNullOrEmpty();
        }

        private bool ShouldSerializeEnderEmit()
        {
            return !EnderEmit.CEP.IsNullOrEmpty() ||
                   !EnderEmit.Nro.IsNullOrEmpty() ||
                   !EnderEmit.XBairro.IsNullOrEmpty() ||
                   !EnderEmit.XCpl.IsNullOrEmpty() ||
                   !EnderEmit.XLgr.IsNullOrEmpty() ||
                   !EnderEmit.XMun.IsNullOrEmpty();
        }

        private bool ShouldSerializeCRegTrib()
        {
            return CRegTrib != null;
        }

        private bool ShouldSerializeIE()
        {
            return !IE.IsNullOrEmpty();
        }

        private bool ShouldSerializeIM()
        {
            return !IM.IsNullOrEmpty();
        }

        private bool ShouldSerializeCRegTribISSQN()
        {
            return !IM.IsNullOrEmpty() && CRegTribISSQN != RegTribIssqn.Nenhum;
        }

        #endregion Methods
    }
}