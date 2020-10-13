using System.ComponentModel;
using Vip.DFe.Attributes;
using Vip.DFe.Enum;
using Vip.DFe.Extensions;
using Vip.DFe.SAT.CupomFiscal;
using Vip.DFe.Serializer;

namespace Vip.DFe.SAT.Eventos
{
    public sealed class CFeCancEmit : GenericClone<CFeCancEmit>, INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion Events

        #region Constructors

        public CFeCancEmit()
        {
            EnderEmit = new CFeEnderEmit();
        }

        #endregion Constructors

        #region Propriedades

        [DFeElement(TipoCampo.StrNumberFill, "CNPJ", Id = "C02", Min = 14, Max = 14, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string CNPJ { get; set; }

        [DFeElement(TipoCampo.Str, "xNome", Id = "C03", Min = 1, Max = 60, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string XNome { get; set; }

        [DFeElement(TipoCampo.Str, "xFant", Id = "C04", Min = 1, Max = 60, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public string XFant { get; set; }

        [DFeElement("enderEmit", Id = "C05", Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public CFeEnderEmit EnderEmit { get; set; }

        [DFeElement(TipoCampo.Str, "IE", Id = "C12", Min = 12, Max = 12, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string IE { get; set; }

        [DFeElement(TipoCampo.Str, "IM", Id = "C13", Min = 1, Max = 15, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public string IM { get; set; }

        #endregion Propriedades

        #region Methods

        private bool ShouldSerializeCNPJ()
        {
            return !CNPJ.IsNullOrEmpty();
        }

        private bool ShouldSerializeXNome()
        {
            return !XNome.IsNullOrEmpty();
        }

        private bool ShouldSerializeXFant()
        {
            return !XFant.IsNullOrEmpty();
        }

        private bool ShouldSerializeEnderEmit()
        {
            return !EnderEmit.CEP.IsNullOrEmpty() ||
                   !EnderEmit.Nro.IsNullOrEmpty() ||
                   !EnderEmit.XBairro.IsNullOrEmpty() ||
                   !EnderEmit.XCpl.IsNullOrEmpty() ||
                   !EnderEmit.XLgr.IsNullOrEmpty() ||
                   !EnderEmit.XMun.IsNullOrEmpty();
        }

        private bool ShouldSerializeIE()
        {
            return !IE.IsNullOrEmpty();
        }

        private bool ShouldSerializeIM()
        {
            return !IM.IsNullOrEmpty();
        }

        #endregion Methods
    }
}