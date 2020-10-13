using System.ComponentModel;
using Vip.DFe.Attributes;
using Vip.DFe.Enum;
using Vip.DFe.Serializer;

namespace Vip.DFe.NFe.NotaFiscal.Destinatario
{
    /// <summary>
    ///     Endereço do Destinatário da NF-e
    /// </summary>
    public sealed class NFeDestEndereco : GenericClone<NFeDestEndereco>, INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Constructors

        public NFeDestEndereco()
        {
            CodigoPais = 1058;
            Pais = "BRASIL";
        }

        #endregion

        #region Properties

        /// <summary>
        ///     E06 - Logradouro
        /// </summary>
        [DFeElement(TipoCampo.Str, "xLgr", Id = "E06", Min = 2, Max = 60, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string Logradouro { get; set; }

        /// <summary>
        ///     E07 - Número
        /// </summary>
        [DFeElement(TipoCampo.Str, "nro", Id = "E07", Min = 1, Max = 60, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string Numero { get; set; }

        /// <summary>
        ///     E08 - Complemento
        /// </summary>
        [DFeElement(TipoCampo.Str, "xCpl", Id = "E08", Min = 1, Max = 60, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public string Complemento { get; set; }

        /// <summary>
        ///     E09 - Bairro
        /// </summary>
        [DFeElement(TipoCampo.Str, "xBairro", Id = "E09", Min = 2, Max = 60, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string Bairro { get; set; }

        /// <summary>
        ///     E10 - Código do município
        ///     <para>Código do município (utilizar a tabela do IBGE), informar 9999999 para operações com o exterior.</para>
        /// </summary>
        [DFeElement(TipoCampo.Int, "cMun", Id = "E10", Min = 7, Max = 7, Ocorrencia = Ocorrencia.Obrigatoria)]
        public int CodigoIBGE { get; set; }

        /// <summary>
        ///     E11 - Nome do município, informar EXTERIOR para operações com o exterior.
        /// </summary>
        [DFeElement(TipoCampo.Str, "xMun", Id = "E11", Min = 2, Max = 60, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string Municipio { get; set; }

        /// <summary>
        ///     E12 - Sigla da UF, informar EX para operações com o exterior.
        /// </summary>
        [DFeElement(TipoCampo.Str, "UF", Id = "E12", Min = 2, Max = 2, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string UF { get; set; }

        /// <summary>
        ///     E13 - Código do CEP
        ///     <para>Informar os zeros não significativos. (NT 2011/004)</para>
        /// </summary>
        [DFeElement(TipoCampo.StrNumberFill, "CEP", Id = "E13", Min = 8, Max = 8, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public string CEP { get; set; }

        /// <summary>
        ///     E14 - Código do País
        ///     <para>1058 - Brasil</para>
        /// </summary>
        [DFeElement(TipoCampo.Int, "cPais", Id = "E14", Min = 2, Max = 4, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public int CodigoPais { get; set; }

        /// <summary>
        ///     E15 - Nome do País
        ///     <para>Brasil ou BRASIL</para>
        /// </summary>
        [DFeElement(TipoCampo.Str, "xPais", Id = "E15", Min = 2, Max = 60, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public string Pais { get; set; }

        /// <summary>
        ///     E16 - Telefone
        ///     <para>
        ///         Preencher com o Código DDD + número do telefone. Nas operações com exterior é permitido informar o código do
        ///         país + código da localidade + número do telefone (v.2.0)
        ///     </para>
        /// </summary>
        [DFeElement(TipoCampo.StrNumber, "fone", Id = "E16", Min = 6, Max = 14, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public string Fone { get; set; }

        #endregion
    }
}