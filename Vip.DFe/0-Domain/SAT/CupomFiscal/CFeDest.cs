using System.ComponentModel;
using Vip.DFe.Attributes;
using Vip.DFe.Enum;
using Vip.DFe.Extensions;
using Vip.DFe.Serializer;

namespace Vip.DFe.SAT.CupomFiscal
{
    /// <summary>
    ///     Classe CFeDest (dest). Grupo de identificação do Destinatário do CF-e
    /// </summary>
    public sealed class CFeDest : GenericClone<CFeDest>, INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion Events

        #region Propriedades

        /// <summary>
        ///     CPF do destinatário. Informar o CPF do destinatário, preenchendo os zeros não significativos.
        /// </summary>
        [DFeElement(TipoCampo.StrNumberFill, "CPF", Id = "E02", Min = 11, Max = 11, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public string CPF { get; set; }

        /// <summary>
        ///     CNPJ do destinatário. Informar o CNPJ do destinatário, preenchendo os zeros não significativos.
        /// </summary>
        [DFeElement(TipoCampo.StrNumberFill, "CNPJ", Id = "E03", Min = 14, Max = 14, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public string CNPJ { get; set; }

        /// <summary>
        ///     Razão Social ou Nome do destinatário
        /// </summary>
        [DFeElement(TipoCampo.Str, "xNome", Id = "E04", Min = 1, Max = 60, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public string Nome { get; set; }

        #endregion Propriedades

        #region Methods

        private bool ShouldSerializeCPF()
        {
            return CNPJ.IsNullOrEmpty();
        }

        private bool ShouldSerializeCNPJ()
        {
            return CPF.IsNullOrEmpty();
        }

        #endregion Methods
    }
}