using System;
using System.ComponentModel;
using Vip.DFe.Attributes;
using Vip.DFe.Enum;
using Vip.DFe.Extensions;
using Vip.DFe.Serializer;

namespace Vip.DFe.SAT.Eventos
{
    public sealed class CancInfCFe : INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion Events

        #region Fields

        private CFeCancDest dest;

        #endregion Fields

        #region Constructors

        public CancInfCFe()
        {
            Ide = new CFeCancIde();
            Emit = new CFeCancEmit();
            Dest = new CFeCancDest();
            Total = new CFeCancTotal();
            InfAdic = new CFeCancInfAdic();
        }

        #endregion Constructors

        #region Propriedades

        [DFeAttribute(TipoCampo.Str, "Id", Id = "A04", Ocorrencia = Ocorrencia.Obrigatoria)]
        public string Id { get; set; }

        [DFeAttribute(TipoCampo.De2, "versao", Id = "A05", Min = 4, Max = 9, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal Versao { get; set; }

        [DFeAttribute(TipoCampo.Str, "chCanc", Id = "A06", Ocorrencia = Ocorrencia.Obrigatoria)]
        public string ChCanc { get; set; }

        [DFeIgnore] public DateTime? DhEmissao { get; set; }

        [DFeElement(TipoCampo.DatCFe, "dEmi", Id = "A07", Min = 8, Max = 8, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public DateTime DEmi
        {
            get => DhEmissao ?? DateTime.MinValue;
            set => DhEmissao = value.Date + (DhEmissao?.TimeOfDay ?? DateTime.MinValue.TimeOfDay);
        }

        [DFeElement(TipoCampo.HorCFe, "hEmi", Id = "A08", Min = 6, Max = 6, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public DateTime HEmi
        {
            get => DhEmissao ?? DateTime.MinValue;
            set => DhEmissao = (DhEmissao?.Date ?? DateTime.MinValue.Date) + value.TimeOfDay;
        }

        [DFeElement("ide", Id = "B01", Ocorrencia = Ocorrencia.Obrigatoria)]
        public CFeCancIde Ide { get; set; }

        [DFeElement("emit", Id = "C01", Ocorrencia = Ocorrencia.Obrigatoria)]
        public CFeCancEmit Emit { get; set; }

        [DFeElement("dest", Id = "E01", Ocorrencia = Ocorrencia.Obrigatoria)]
        public CFeCancDest Dest
        {
            get => dest;
            set
            {
                dest = value;
                if (dest.Parent != this)
                    dest.Parent = this;
            }
        }

        [DFeElement("total", Id = "W01", Ocorrencia = Ocorrencia.Obrigatoria)]
        public CFeCancTotal Total { get; set; }

        [DFeElement("infAdic", Id = "Z01", Ocorrencia = Ocorrencia.Obrigatoria)]
        public CFeCancInfAdic InfAdic { get; set; }

        #endregion Propriedades

        #region Methods

        private bool ShouldSerializeId()
        {
            return !Id.IsNullOrEmpty();
        }

        private bool ShouldSerializeVersao()
        {
            return !Id.IsNullOrEmpty();
        }

        private bool ShouldSerializeDEmi()
        {
            return DhEmissao.HasValue;
        }

        private bool ShouldSerializeHEmi()
        {
            return DhEmissao.HasValue;
        }

        #endregion Methods
    }
}