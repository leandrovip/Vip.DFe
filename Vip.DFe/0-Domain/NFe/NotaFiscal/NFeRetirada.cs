using System.ComponentModel;
using Vip.DFe.Attributes;
using Vip.DFe.Enum;
using Vip.DFe.Extensions;
using Vip.DFe.Serializer;

namespace Vip.DFe.NFe.NotaFiscal
{
    /// <summary>
    ///     F01 - Identificação do Local de retirada, informar somente se diferente do endereço do remetente.
    /// </summary>
    public sealed class NFeRetirada : GenericClone<NFeRetirada>, INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Constructors

        public NFeRetirada()
        {
            CodigoPais = 1058;
            Pais = "BRASIL";
        }

        #endregion

        #region Properties

        /// <summary>
        ///     F02 - CNPJ
        /// </summary>
        [DFeElement(TipoCampo.StrNumberFill, "CNPJ", Id = "F02", Min = 14, Max = 14, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string CNPJ { get; set; }

        /// <summary>
        ///     F02a - CPF
        /// </summary>
        [DFeElement(TipoCampo.StrNumberFill, "CPF", Id = "F02a", Min = 11, Max = 11, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string CPF { get; set; }

        /// <summary>
        ///     F02b - Razão Social ou Nome do expedidor
        /// </summary>
        [DFeElement(TipoCampo.Str, "xNome", Id = "F02b", Min = 2, Max = 60, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public string Nome { get; set; }

        /// <summary>
        ///     F03 - Logradouro
        /// </summary>
        [DFeElement(TipoCampo.Str, "xLgr", Id = "F03", Min = 2, Max = 60, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string Logradouro { get; set; }

        /// <summary>
        ///     F04 - Número
        /// </summary>
        [DFeElement(TipoCampo.Str, "nro", Id = "F04", Min = 1, Max = 60, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string Numero { get; set; }

        /// <summary>
        ///     F05 - Complemento
        /// </summary>
        [DFeElement(TipoCampo.Str, "xCpl", Id = "F05", Min = 1, Max = 60, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public string Complemento { get; set; }

        /// <summary>
        ///     F06 - Bairro
        /// </summary>
        [DFeElement(TipoCampo.Str, "xBairro", Id = "F06", Min = 2, Max = 60, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string Bairro { get; set; }

        /// <summary>
        ///     F07 - Código do município
        ///     <para>Código do município (utilizar a tabela do IBGE), informar 9999999 para operações com o exterior.</para>
        /// </summary>
        [DFeElement(TipoCampo.Int, "cMun", Id = "F07", Min = 7, Max = 7, Ocorrencia = Ocorrencia.Obrigatoria)]
        public int CodigoIBGE { get; set; }

        /// <summary>
        ///     F08 - Nome do município, informar EXTERIOR para operações com o exterior.
        /// </summary>
        [DFeElement(TipoCampo.Str, "xMun", Id = "F08", Min = 2, Max = 60, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string Municipio { get; set; }

        /// <summary>
        ///     F09 - Sigla da UF, informar EX para operações com o exterior.
        /// </summary>
        [DFeElement(TipoCampo.Str, "UF", Id = "F09", Min = 2, Max = 2, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string UF { get; set; }

        /// <summary>
        ///     F10 - Código do CEP, informar zeros não significativos.
        /// </summary>
        [DFeElement(TipoCampo.StrNumberFill, "CEP", Id = "F10", Min = 8, Max = 8, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public string CEP { get; set; }

        /// <summary>
        ///     F11 - Código do País, informar tabela do BACEN.
        /// </summary>
        [DFeElement(TipoCampo.Int, "cPais", Id = "F11", Min = 4, Max = 4, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public int CodigoPais { get; set; }

        /// <summary>
        ///     F12 - Nome do País.
        /// </summary>
        [DFeElement(TipoCampo.Str, "xPais", Id = "F12", Min = 2, Max = 60, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public string Pais { get; set; }

        /// <summary>
        ///     F13 - Telefone. Informar DDD + Telefone. Para exterior usar código do país + código localidade + telefone.
        /// </summary>
        [DFeElement(TipoCampo.StrNumberFill, "fone", Id = "F13", Min = 6, Max = 14, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public string Fone { get; set; }

        /// <summary>
        ///     F14 - E-mail do expedidor.
        /// </summary>
        [DFeElement(TipoCampo.Str, "email", Id = "F14", Min = 1, Max = 60, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public string Email { get; set; }

        /// <summary>
        ///     F15 - I.E. do expedidor, sem caracteres de formatação.
        /// </summary>
        [DFeElement(TipoCampo.StrNumber, "IE", Id = "F15", Min = 2, Max = 14, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public string IE { get; set; }

        #endregion

        #region Methods

        private bool ShouldSerializeCNPJ()
        {
            return CNPJ.IsNotNullOrEmpty();
        }

        private bool ShouldSerializeEmail()
        {
            return Email.IsEmail();
        }

        #endregion
    }
}