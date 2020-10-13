using System.ComponentModel;
using Vip.DFe.Attributes;
using Vip.DFe.Enum;
using Vip.DFe.Extensions;
using Vip.DFe.Serializer;
using Vip.DFe.Shared.Enum;

namespace Vip.DFe.NFe.NotaFiscal.Emitente
{
    /// <summary>
    ///     Identificação do emitente da NF-e
    /// </summary>
    public sealed class NFeEmit : GenericClone<NFeEmit>, INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Constructors

        public NFeEmit()
        {
            Endereco = new NFeEmitEndereco();
        }

        #endregion

        #region Properties

        /// <summary>
        ///     C02 - CNPJ do emitente
        /// </summary>
        [DFeElement(TipoCampo.StrNumberFill, "CNPJ", Id = "C02", Min = 14, Max = 14, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string CNPJ { get; set; }

        /// <summary>
        ///     C02a - CPF do remetente
        /// </summary>
        [DFeElement(TipoCampo.StrNumberFill, "CPF", Id = "C02a", Min = 11, Max = 11, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string CPF { get; set; }

        /// <summary>
        ///     C03 - Razão Social ou Nome do emitente
        /// </summary>
        [DFeElement(TipoCampo.Str, "xNome", Id = "C03", Min = 2, Max = 60, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string XNome { get; set; }

        /// <summary>
        ///     C04 - Nome fantasia
        /// </summary>
        [DFeElement(TipoCampo.Str, "xFant", Id = "C04", Min = 1, Max = 60, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public string XFant { get; set; }

        /// <summary>
        ///     C05 - Grupo do Endereço do emitente
        /// </summary>
        [DFeElement("enderEmit", Ocorrencia = Ocorrencia.Obrigatoria)]
        public NFeEmitEndereco Endereco { get; set; }

        /// <summary>
        ///     C17 - Inscrição Estadual
        ///     <para>Campo de informação obrigatória nos casos de emissão própria (procEmi = 0, 2 ou 3).</para>
        ///     <para>
        ///         A IE deve ser informada apenas com algarismos para destinatários contribuintes do ICMS, sem caracteres de
        ///         formatação (ponto, barra, hífen, etc.);
        ///     </para>
        ///     <para>
        ///         O literal “ISENTO” deve ser informado apenas para contribuintes do ICMS que são isentos de inscrição no
        ///         cadastro de contribuintes do ICMS e estejam emitindo NF-e avulsa;
        ///     </para>
        /// </summary>
        [DFeElement(TipoCampo.Custom, "IE", Id = "C17", Min = 2, Max = 14, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string IE { get; set; }

        /// <summary>
        ///     C18 - IE do Substituto Tributário
        ///     <para>Informar a IE do ST da UF de destino da mercadoria, quando houver a retenção do ICMS ST para a UF de destino.</para>
        /// </summary>
        [DFeElement(TipoCampo.StrNumber, "IEST", Id = "C18", Min = 2, Max = 14, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public string IEST { get; set; }

        /// <summary>
        ///     C19 - Inscrição Municipal
        ///     <para>
        ///         Este campo deve ser informado, quando ocorrer a emissão de NF-e conjugada, com prestação de serviços sujeitos
        ///         ao ISSQN e fornecimento de peças sujeitos ao ICMS.
        ///     </para>
        /// </summary>
        [DFeElement(TipoCampo.Str, "IM", Id = "C19", Min = 1, Max = 15, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public string IM { get; set; }

        /// <summary>
        ///     C20 - CNAE fiscal
        ///     <para>Este campo deve ser informado quando o campo IM (C19) for informado.</para>
        /// </summary>
        [DFeElement(TipoCampo.Int, "CNAE", Id = "C20", Min = 7, Max = 7, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public int CNAE { get; set; }

        /// <summary>
        ///     C21 - Código de Regime Tributário
        /// </summary>
        [DFeElement(TipoCampo.Enum, "CRT", Id = "C21", Min = 1, Max = 1, Ocorrencia = Ocorrencia.Obrigatoria)]
        public RegimeTributario CRT { get; set; }

        #endregion

        #region Methods

        private bool ShouldSerializeCNPJ()
        {
            return CNPJ.IsNotNullOrEmpty();
        }

        private bool ShouldSerializeCPF()
        {
            return CPF.IsNotNullOrEmpty();
        }

        private bool ShouldSerializeXFant()
        {
            return XFant.IsNotNullOrEmpty();
        }

        private bool ShouldSerializeIM()
        {
            return IM.IsNotNullOrEmpty() && !IM.Equals("0");
        }

        private string SerializeIE()
        {
            var ie = IE.TrimVip().ToUpper();
            return ie.Equals(DFeConstantes.Isento) ? ie : ie.OnlyNumbers();
        }

        private object DeserializeIE(string value)
        {
            return value;
        }

        private bool ShouldSerializeCNAE()
        {
            return IM.IsNotNullOrEmpty() && CNAE != 0;
        }

        #endregion
    }
}