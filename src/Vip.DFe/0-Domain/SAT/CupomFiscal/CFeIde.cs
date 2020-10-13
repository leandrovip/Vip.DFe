using System;
using System.ComponentModel;
using Vip.DFe.Attributes;
using Vip.DFe.Enum;
using Vip.DFe.Extensions;
using Vip.DFe.Serializer;
using Vip.DFe.Shared.Enum;

namespace Vip.DFe.SAT.CupomFiscal
{
    /// <summary>
    ///     Grupo das informações de identificação do CF-e
    /// </summary>
    public sealed class CFeIde : GenericClone<CFeIde>, INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion Events

        #region Constructors

        public CFeIde()
        {
            DhEmissao = null;
            SignAC = string.Empty;
        }

        #endregion Constructors

        #region Propriedades

        /// <summary>
        ///     Código da UF do emitente do Documento Fiscal
        ///     <br /><b>Campo preenchido pelo SAT</b>
        /// </summary>
        [DFeElement(TipoCampo.Enum, "cUF", Id = "B02", Min = 2, Max = 2, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public CodigoUF? UF { get; set; }

        /// <summary>
        ///     Código Numérico que compõe a Chave de Acesso
        ///     <br /><b>Campo preenchido pelo SAT</b>
        /// </summary>
        [DFeElement(TipoCampo.Int, "cNF", Id = "B03", Min = 6, Max = 6, Ocorrencia = Ocorrencia.MaiorQueZero)]
        public int CNf { get; set; }

        /// <summary>
        ///     Código do Modelo do Documento Fiscal
        ///     <br /><b>Campo preenchido pelo SAT</b>
        /// </summary>
        [DFeElement(TipoCampo.Int, "mod", Id = "B04", Min = 2, Max = 2, Ocorrencia = Ocorrencia.MaiorQueZero)]
        public int Modelo { get; set; }

        /// <summary>
        ///     Número de Série do equipamento SAT
        ///     <br /><b>Campo preenchido pelo SAT</b>
        /// </summary>
        [DFeElement(TipoCampo.Int, "nserieSAT", Id = "B05", Min = 9, Max = 9, Ocorrencia = Ocorrencia.MaiorQueZero)]
        public int NSerieSAT { get; set; }

        /// <summary>
        ///     Número do Cupom Fiscal Eletronico
        ///     <br /><b>Campo preenchido pelo SAT</b>
        /// </summary>
        [DFeElement(TipoCampo.Int, "nCFe", Id = "B06", Min = 6, Max = 6, Ocorrencia = Ocorrencia.MaiorQueZero)]
        public int NCFe { get; set; }

        [DFeIgnore] public DateTime? DhEmissao { get; set; }

        /// <summary>
        ///     Data de emissão do Cupom Fiscal
        ///     <br /><b>Campo preenchido pelo SAT</b>
        /// </summary>
        [DFeElement(TipoCampo.DatCFe, "dEmi", Id = "B07", Min = 8, Max = 8, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public DateTime DEmi
        {
            get => DhEmissao ?? DateTime.MinValue;
            set => DhEmissao = value.Date + (DhEmissao?.TimeOfDay ?? DateTime.MinValue.TimeOfDay);
        }

        /// <summary>
        ///     Hora de emissão do Cupom Fiscal
        ///     <br /><b>Campo preenchido pelo SAT</b>
        /// </summary>
        [DFeElement(TipoCampo.HorCFe, "hEmi", Id = "B08", Min = 6, Max = 6, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public DateTime HEmi
        {
            get => DhEmissao ?? DateTime.MinValue;
            set => DhEmissao = (DhEmissao?.Date ?? DateTime.MinValue.Date) + value.TimeOfDay;
        }

        /// <summary>
        ///     Dígito Verificador da Chave de Acesso do CF-e
        ///     <br /><b>Campo preenchido pelo SAT</b>
        /// </summary>
        [DFeElement(TipoCampo.Int, "cDV", Id = "B09", Min = 1, Max = 1, Ocorrencia = Ocorrencia.MaiorQueZero)]
        public int CDv { get; set; }

        /// <summary>
        ///     Identificação do Ambiente
        ///     <br /><b>Campo preenchido pelo SAT</b>
        /// </summary>
        [DFeElement(TipoCampo.Enum, "tpAmb", Id = "B10", Min = 1, Max = 1, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public TipoAmbiente? TpAmb { get; set; }

        /// <summary>
        ///     CNPJ Software House
        /// </summary>
        [DFeElement(TipoCampo.StrNumberFill, "CNPJ", Id = "B11", Min = 14, Max = 14, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string CNPJ { get; set; }

        /// <summary>
        ///     Assinatura do Aplicativo Comercial
        /// </summary>
        [DFeElement(TipoCampo.Str, "signAC", Id = "B12", Min = 1, Max = 344, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string SignAC { get; set; }

        /// <summary>
        ///     Assinatura Digital para uso em QRCODE
        ///     <br /><b>Campo preenchido pelo SAT</b>
        /// </summary>
        [DFeElement(TipoCampo.Str, "assinaturaQRCODE", Id = "B13", Min = 344, Max = 344, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string AssinaturaQrcode { get; set; }

        /// <summary>
        ///     Número do Caixa ao qual o SAT está conectado
        /// </summary>
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