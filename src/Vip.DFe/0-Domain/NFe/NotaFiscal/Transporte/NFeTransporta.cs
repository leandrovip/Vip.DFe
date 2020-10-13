using System.ComponentModel;
using Vip.DFe.Attributes;
using Vip.DFe.Enum;
using Vip.DFe.Extensions;
using Vip.DFe.Serializer;

namespace Vip.DFe.NFe.NotaFiscal.Transporte
{
    public sealed class NFeTransporta : GenericClone<NFeTransporta>, INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Properties

        /// <summary>
        ///     X04 - CNPJ do Transportador
        /// </summary>
        [DFeElement(TipoCampo.StrNumberFill, "CNPJ", Id = "X04", Min = 14, Max = 14, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public string Cnpj { get; set; }

        /// <summary>
        ///     X05 - CPF do Transportador
        /// </summary>
        [DFeElement(TipoCampo.StrNumberFill, "CPF", Id = "X05", Min = 11, Max = 11, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public string Cpf { get; set; }

        /// <summary>
        ///     X06 - Razão Social ou nome
        /// </summary>
        [DFeElement(TipoCampo.Str, "xNome", Id = "X06", Min = 2, Max = 60, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public string XNome { get; set; }

        /// <summary>
        ///     X07 - Inscrição Estadual do Transportador
        /// </summary>
        [DFeElement(TipoCampo.Custom, "IE", Id = "X07", Min = 2, Max = 14, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public string IE { get; set; }

        /// <summary>
        ///     X08 - Endereço Completo
        /// </summary>
        [DFeElement(TipoCampo.Str, "xEnder", Id = "X08", Min = 1, Max = 60, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public string XEnder { get; set; }

        /// <summary>
        ///     X09 - Nome do município
        /// </summary>
        [DFeElement(TipoCampo.Str, "xMun", Id = "X09", Min = 1, Max = 60, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public string XMun { get; set; }

        /// <summary>
        ///     X10 - Sigla da UF
        /// </summary>
        [DFeElement(TipoCampo.Str, "UF", Id = "X10", Min = 2, Max = 2, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public string UF { get; set; }

        #endregion

        #region Methods

        private bool ShouldSerializeCnpj()
        {
            return Cnpj.IsNotNullOrEmpty();
        }

        private bool ShouldSerializeCpf()
        {
            return Cpf.IsNotNullOrEmpty();
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