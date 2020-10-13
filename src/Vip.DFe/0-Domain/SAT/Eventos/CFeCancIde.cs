using System;
using System.ComponentModel;
using Vip.DFe.Attributes;
using Vip.DFe.Enum;
using Vip.DFe.Extensions;
using Vip.DFe.SAT.CupomFiscal;
using Vip.DFe.Serializer;
using Vip.DFe.Shared.Enum;

namespace Vip.DFe.SAT.Eventos
{
    public sealed class CFeCancIde : GenericClone<CFeCancIde>, INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion Events

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="CFeIde" /> class.
        /// </summary>
        public CFeCancIde()
        {
            DhEmissao = null;
            SignAC = string.Empty;
        }

        #endregion Constructors

        #region Propriedades

        [DFeElement(TipoCampo.Enum, "cUF", Id = "B02", Min = 2, Max = 2, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public CodigoUF? UF { get; set; }

        [DFeElement(TipoCampo.Int, "cNF", Id = "B03", Min = 6, Max = 6, Ocorrencia = Ocorrencia.MaiorQueZero)]
        public int CNf { get; set; }

        [DFeElement(TipoCampo.Int, "mod", Id = "B04", Min = 2, Max = 2, Ocorrencia = Ocorrencia.MaiorQueZero)]
        public int Modelo { get; set; }

        [DFeElement(TipoCampo.Int, "nserieSAT", Id = "B05", Min = 9, Max = 9, Ocorrencia = Ocorrencia.MaiorQueZero)]
        public int NSerieSAT { get; set; }

        [DFeElement(TipoCampo.Int, "nCFe", Id = "B06", Min = 6, Max = 6, Ocorrencia = Ocorrencia.MaiorQueZero)]
        public int NCFe { get; set; }

        [DFeIgnore] public DateTime? DhEmissao { get; set; }

        [DFeElement(TipoCampo.DatCFe, "dEmi", Id = "B07", Min = 8, Max = 8, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public DateTime DEmi
        {
            get => DhEmissao ?? DateTime.MinValue;
            set => DhEmissao = value.Date + (DhEmissao?.TimeOfDay ?? DateTime.MinValue.TimeOfDay);
        }

        [DFeElement(TipoCampo.HorCFe, "hEmi", Id = "B08", Min = 6, Max = 6, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public DateTime HEmi
        {
            get => DhEmissao ?? DateTime.MinValue;
            set => DhEmissao = (DhEmissao?.Date ?? DateTime.MinValue.Date) + value.TimeOfDay;
        }

        [DFeElement(TipoCampo.Int, "cDV", Id = "B09", Min = 1, Max = 1, Ocorrencia = Ocorrencia.MaiorQueZero)]
        public int CDv { get; set; }

        [DFeElement(TipoCampo.StrNumberFill, "CNPJ", Id = "B11", Min = 14, Max = 14, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string CNPJ { get; set; }

        [DFeElement(TipoCampo.Str, "signAC", Id = "B12", Min = 1, Max = 344, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string SignAC { get; set; }

        [DFeElement(TipoCampo.Str, "assinaturaQRCODE", Id = "B13", Min = 344, Max = 344, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string AssinaturaQrcode { get; set; }

        [DFeElement(TipoCampo.Int, "numeroCaixa", Id = "B14", Min = 3, Max = 3, Ocorrencia = Ocorrencia.Obrigatoria)]
        public int NumeroCaixa { get; set; }

        #endregion Propriedades

        #region Methods

        private bool ShouldSerializeDEmi()
        {
            return DhEmissao.HasValue;
        }

        private bool ShouldSerializeHEmi()
        {
            return DhEmissao.HasValue;
        }

        private bool ShouldSerializeAssinaturaQrcode()
        {
            return !AssinaturaQrcode.IsNullOrEmpty();
        }

        #endregion Methods
    }
}