using System.ComponentModel;
using Vip.DFe.Attributes;
using Vip.DFe.Enum;
using Vip.DFe.Extensions;
using Vip.DFe.Serializer;

namespace Vip.DFe.NFe.NotaFiscal
{
    /// <summary>
    ///     Identificação do Local de entrega, informar somente se diferente do endereço destinatário
    /// </summary>
    public sealed class NFeEntrega : GenericClone<NFeEntrega>, INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Constructors

        public NFeEntrega()
        {
            CodigoPais = 1058;
            Pais = "BRASIL";
        }

        #endregion

        #region Properties

        /// <summary>
        ///     G02 - CNPJ
        /// </summary>
        [DFeElement(TipoCampo.StrNumberFill, "CNPJ", Id = "G02", Min = 14, Max = 14, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string CNPJ { get; set; }

        /// <summary>
        ///     G02a - CPF
        /// </summary>
        [DFeElement(TipoCampo.StrNumberFill, "CPF", Id = "G02a", Min = 11, Max = 11, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string CPF { get; set; }

        /// <summary>
        ///     G02b - Razão Social ou Nome do recebedor
        /// </summary>
        [DFeElement(TipoCampo.Str, "xNome", Id = "G02b", Min = 2, Max = 60, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public string Nome { get; set; }

        /// <summary>
        ///     G03 - Logradouro
        /// </summary>
        [DFeElement(TipoCampo.Str, "xLgr", Id = "G03", Min = 2, Max = 60, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string Logradouro { get; set; }

        /// <summary>
        ///     G04 - Número
        /// </summary>
        [DFeElement(TipoCampo.Str, "nro", Id = "G04", Min = 1, Max = 60, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string Numero { get; set; }

        /// <summary>
        ///     G05 - Complemento
        /// </summary>
        [DFeElement(TipoCampo.Str, "xCpl", Id = "G05", Min = 1, Max = 60, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public string Complemento { get; set; }

        /// <summary>
        ///     G06 - Bairro
        /// </summary>
        [DFeElement(TipoCampo.Str, "xBairro", Id = "G06", Min = 2, Max = 60, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string xBairro { get; set; }

        /// <summary>
        ///     G07 - Código do município
        ///     <para>Código do município (utilizar a tabela do IBGE), informar 9999999 para operações com o exterior.</para>
        /// </summary>
        [DFeElement(TipoCampo.Int, "cMun", Id = "G07", Min = 7, Max = 7, Ocorrencia = Ocorrencia.Obrigatoria)]
        public int CodigoIBGE { get; set; }

        /// <summary>
        ///     G08 - Nome do município, informar EXTERIOR para operações com o exterior.
        /// </summary>
        [DFeElement(TipoCampo.Str, "xMun", Id = "G08", Min = 2, Max = 60, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string Municipio { get; set; }

        /// <summary>
        ///     G09 - Sigla da UF, informar EX para operações com o exterior.
        /// </summary>
        [DFeElement(TipoCampo.Str, "UF", Id = "G09", Min = 2, Max = 2, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string UF { get; set; }

        /// <summary>
        ///     G10 - Código do CEP, informar zeros não significativos.
        /// </summary>
        [DFeElement(TipoCampo.StrNumberFill, "CEP", Id = "G10", Min = 8, Max = 8, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public string CEP { get; set; }

        /// <summary>
        ///     G11 - Código do País, informar tabela do BACEN.
        /// </summary>
        [DFeElement(TipoCampo.Int, "cPais", Id = "G11", Min = 4, Max = 4, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public int CodigoPais { get; set; }

        /// <summary>
        ///     G12 - Nome do País.
        /// </summary>
        [DFeElement(TipoCampo.Str, "xPais", Id = "G12", Min = 2, Max = 60, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public string Pais { get; set; }

        /// <summary>
        ///     G13 - Telefone. Informar DDD + Telefone. Para exterior usar código do país + código localidade + telefone.
        /// </summary>
        [DFeElement(TipoCampo.StrNumberFill, "fone", Id = "G13", Min = 6, Max = 14, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public string Fone { get; set; }

        /// <summary>
        ///     G14 - E-mail do recebedor.
        /// </summary>
        [DFeElement(TipoCampo.Str, "email", Id = "G14", Min = 1, Max = 60, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public string Email { get; set; }

        /// <summary>
        ///     G15 - I.E. do recebedor, sem caracteres de formatação.
        /// </summary>
        [DFeElement(TipoCampo.StrNumber, "IE", Id = "G15", Min = 2, Max = 14, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public string IE { get; set; }

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

        private bool ShouldSerializeEmail()
        {
            return Email.IsEmail();
        }

        #endregion
    }
}