using System.ComponentModel;
using Vip.DFe.Attributes;
using Vip.DFe.Enum;
using Vip.DFe.Extensions;
using Vip.DFe.Serializer;

namespace Vip.DFe.NFe.NotaFiscal.Emitente
{
    /// <summary>
    ///     Endereço do emitente
    /// </summary>
    public sealed class NFeEmitEndereco : GenericClone<NFeEmitEndereco>, INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Constructor

        public NFeEmitEndereco()
        {
            CodigoPais = 1058;
            Pais = "BRASIL";
            Bairro = "SN";
        }

        #endregion

        #region Properties

        /// <summary>
        ///     C06 - Logradouro
        /// </summary>
        [DFeElement(TipoCampo.Str, "xLgr", Id = "C06", Min = 2, Max = 60, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string Logradouro { get; set; }

        /// <summary>
        ///     C07 - Número
        /// </summary>
        [DFeElement(TipoCampo.Custom, "nro", Id = "C07", Min = 1, Max = 60, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string Numero { get; set; }

        /// <summary>
        ///     C08 - Complemento
        /// </summary>
        [DFeElement(TipoCampo.Str, "xCpl", Id = "C08", Min = 1, Max = 60, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public string Complemento { get; set; }

        /// <summary>
        ///     C09 - Bairro
        /// </summary>
        [DFeElement(TipoCampo.Custom, "xBairro", Id = "C09", Min = 2, Max = 60, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string Bairro { get; set; }

        /// <summary>
        ///     C10 - Código do município
        ///     <para>Código do município (utilizar a tabela do IBGE), informar 9999999 para operações com o exterior.</para>
        /// </summary>
        [DFeElement(TipoCampo.Int, "cMun", Id = "C10", Min = 7, Max = 7, Ocorrencia = Ocorrencia.Obrigatoria)]
        public int CodigoIBGE { get; set; }

        /// <summary>
        ///     C11 - Nome do município, informar EXTERIOR para operações com o exterior.
        /// </summary>
        [DFeElement(TipoCampo.Str, "xMun", Id = "C11", Min = 2, Max = 60, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string Municipio { get; set; }

        /// <summary>
        ///     C12 - Sigla da UF, informar EX para operações com o exterior.
        /// </summary>
        [DFeElement(TipoCampo.Str, "UF", Id = "C12", Min = 2, Max = 2, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string UF { get; set; }

        /// <summary>
        ///     C13 - Código do CEP
        ///     <para>Informar os zeros não significativos. (NT 2011/004)</para>
        /// </summary>

        [DFeElement(TipoCampo.StrNumberFill, "CEP", Id = "C13", Min = 8, Max = 8, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string CEP { get; set; }

        /// <summary>
        ///     C14 - Código do País
        ///     <para>1058 - Brasil</para>
        /// </summary>
        [DFeElement(TipoCampo.Int, "cPais", Id = "C14", Min = 4, Max = 4, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public int? CodigoPais { get; set; }

        /// <summary>
        ///     C15 - Nome do País
        ///     <para>Brasil ou BRASIL</para>
        /// </summary>
        [DFeElement(TipoCampo.Str, "xPais", Id = "C15", Min = 1, Max = 60, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public string Pais { get; set; }

        /// <summary>
        ///     C16 - Telefone
        ///     <para>
        ///         Preencher com o Código DDD + número do telefone. Nas operações com exterior é permitido informar o código do
        ///         país + código da localidade + número do telefone (v.2.0)
        ///     </para>
        /// </summary>
        [DFeElement(TipoCampo.StrNumber, "fone", Id = "C16", Min = 6, Max = 14, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public string Fone { get; set; }

        #endregion

        #region Methods

        private bool ShouldSerializeXCpl() => Complemento.IsNotNullOrEmpty();
        private bool ShouldSerializeCPais() => CodigoPais.HasValue;
        private string SerializeNumero() => Numero.IsNotNullOrEmpty() ? Numero : "SN";
        private object DeserializeNumero(string value) => value;
        private string SerializeBairro() => Bairro.IsNotNullOrEmpty() ? Bairro : "SN";
        private object DeserializeBairro(string value) => value;

        #endregion
    }
}