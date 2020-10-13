using System.ComponentModel;
using Vip.DFe.Attributes;
using Vip.DFe.Document;
using Vip.DFe.Enum;
using Vip.DFe.Extensions;
using Vip.DFe.Serializer;

namespace Vip.DFe.SAT.Eventos
{
    public sealed class CFeCancDest : DFeParentItem<CFeCancDest, CancInfCFe>, INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion Events

        #region Propriedades

        [DFeElement(TipoCampo.StrNumberFill, "CPF", Id = "E02", Min = 11, Max = 11, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public string CPF { get; set; }

        [DFeElement(TipoCampo.StrNumberFill, "CNPJ", Id = "E03", Min = 14, Max = 14, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public string CNPJ { get; set; }

        #endregion Propriedades

        #region Methods

        private bool ShouldSerializeCPF()
        {
            return Parent.Versao < 0.07M && CNPJ.IsNullOrEmpty();
        }

        private bool ShouldSerializeCNPJ()
        {
            return Parent.Versao < 0.07M && CPF.IsNullOrEmpty();
        }

        #endregion Methods
    }
}