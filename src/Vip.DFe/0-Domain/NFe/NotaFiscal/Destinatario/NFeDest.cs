using System.ComponentModel;
using Vip.DFe.Attributes;
using Vip.DFe.Document;
using Vip.DFe.Enum;
using Vip.DFe.Extensions;
using Vip.DFe.NFe.Enum;
using Vip.DFe.Serializer;
using Vip.DFe.Shared.Enum;

namespace Vip.DFe.NFe.NotaFiscal.Destinatario
{
    public sealed class NFeDest : DFeParentItem<NFeDest, infNFe>, INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Constructors

        public NFeDest()
        {
            Endereco = new NFeDestEndereco();
        }

        public NFeDest(infNFe parent) : this()
        {
            Parent = parent;
        }

        #endregion

        #region Properties

        /// <summary>
        ///     E02 - CNPJ do destinatário
        /// </summary>
        [DFeElement(TipoCampo.StrNumberFill, "CNPJ", Id = "E02", Min = 14, Max = 14, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string CNPJ { get; set; }

        /// <summary>
        ///     E03 - CPF do destinatário
        /// </summary>
        [DFeElement(TipoCampo.StrNumberFill, "CPF", Id = "E03", Min = 11, Max = 11, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string CPF { get; set; }

        /// <summary>
        ///     E03a - Identificador do destinatário, em caso de comprador estrangeiro
        /// </summary>
        [DFeElement(TipoCampo.Str, "idEstrangeiro", Id = "E03a", Min = 0, Max = 20, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string IdEstrangeiro { get; set; }

        /// <summary>
        ///     E04 - Razão Social ou nome do destinatário
        /// </summary>
        [DFeElement(TipoCampo.Custom, "xNome", Id = "E04", Min = 2, Max = 60, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public string Nome { get; set; }

        /// <summary>
        ///     E05 - Endereço do Destinatário da NF-e
        /// </summary>
        [DFeElement("enderDest", Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public NFeDestEndereco Endereco { get; set; }

        /// <summary>
        ///     E16a - Indicador da IE do destinatário:
        /// </summary>
        [DFeElement(TipoCampo.Enum, "indIEDest", Id = "E16a", Min = 1, Max = 1, Ocorrencia = Ocorrencia.Obrigatoria)]
        public NFeIndIeDest IndIEDest { get; set; }

        /// <summary>
        ///     E17 - Inscrição Estadual
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
        [DFeElement(TipoCampo.Custom, "IE", Id = "E17", Min = 2, Max = 14, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public string IE { get; set; }

        /// <summary>
        ///     E18 - Inscrição na SUFRAMA (Obrigatório nas operações com as áreas com benefícios de incentivos fiscais sob
        ///     controle da SUFRAMA)
        /// </summary>
        [DFeElement(TipoCampo.StrNumber, "ISUF", Id = "E18", Min = 8, Max = 9, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public string ISUF { get; set; }

        /// <summary>
        ///     E18a - Inscrição Municipal
        ///     <para>
        ///         Este campo deve ser informado, quando ocorrer a emissão de NF-e conjugada, com prestação de serviços sujeitos
        ///         ao ISSQN e fornecimento de peças sujeitos ao ICMS.
        ///     </para>
        /// </summary>
        [DFeElement(TipoCampo.Str, "IM", Id = "E18a", Min = 1, Max = 15, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public string IM { get; set; }

        /// <summary>
        ///     E19 - Informar o e-mail do destinatário. O campo pode ser utilizado para informar o e-mail de recepção da NF-e
        ///     indicada pelo destinatário
        /// </summary>
        [DFeElement(TipoCampo.Str, "email", Id = "E19", Min = 1, Max = 60, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public string Email { get; set; }

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

        private bool ShouldSerializeIdEstrangeiro()
        {
            return IdEstrangeiro.IsNotNullOrEmpty();
        }

        private bool ShouldSerializeEmail()
        {
            return Email.IsEmail();
        }

        private string SerializeNome()
        {
            return Parent?.Ide.TpAmb == TipoAmbiente.Homologacao ? DFeConstantes.NFeHomologacao : Nome;
        }

        private object DeserializeNome(string value)
        {
            return value;
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

        #endregion
    }
}