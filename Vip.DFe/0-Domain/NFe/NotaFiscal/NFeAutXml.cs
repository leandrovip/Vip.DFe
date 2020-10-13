using System.ComponentModel;
using Vip.DFe.Attributes;
using Vip.DFe.Enum;
using Vip.DFe.Extensions;
using Vip.DFe.Serializer;

namespace Vip.DFe.NFe.NotaFiscal
{
    /// <summary>
    ///     Pessoas autorizadas a acessar o XML da NF-e
    /// </summary>
    public sealed class NFeAutXml : GenericClone<NFeAutXml>, INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Properties

        /// <summary>
        ///     GA02 - CNPJ Autorizado
        /// </summary>
        [DFeElement(TipoCampo.StrNumberFill, "CNPJ", Id = "GA02", Min = 14, Max = 14, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string CNPJ { get; set; }

        /// <summary>
        ///     GA03 - CPF Autorizado
        /// </summary>
        [DFeElement(TipoCampo.StrNumberFill, "CPF", Id = "GA03", Min = 11, Max = 11, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string CPF { get; set; }

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

        #endregion
    }
}