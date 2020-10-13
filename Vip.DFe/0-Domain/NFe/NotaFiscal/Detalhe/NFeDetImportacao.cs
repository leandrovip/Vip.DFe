using System;
using System.ComponentModel;
using Vip.DFe.Attributes;
using Vip.DFe.Document;
using Vip.DFe.Enum;
using Vip.DFe.NFe.Enum;
using Vip.DFe.Serializer;

namespace Vip.DFe.NFe.NotaFiscal.Detalhe
{
    /// <summary>
    ///     Informar dados da importação - DI
    /// </summary>
    public sealed class NFeDetImportacao : GenericClone<NFeDetImportacao>, INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Constructors

        public NFeDetImportacao()
        {
            Adi = new DFeCollection<NFeDetImportacaoAdi>();
        }

        #endregion

        #region Properties

        /// <summary>
        ///     I19 - Número do Documento de Importação (DI, DSI, DIRE, ...)
        /// </summary>
        [DFeElement(TipoCampo.Str, "nDI", Id = "I19", Min = 1, Max = 12, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string NDI { get; set; }

        /// <summary>
        ///     I20 - Data de Registro do documento.
        /// </summary>
        [DFeElement(TipoCampo.Dat, "dDI", Id = "I20", Min = 10, Max = 10, Ocorrencia = Ocorrencia.Obrigatoria)]
        public DateTime DDI { get; set; }

        /// <summary>
        ///     I21 - Local de desembaraço
        /// </summary>
        [DFeElement(TipoCampo.Str, "xLocDesemb", Id = "I21", Min = 1, Max = 60, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string XLocDesemb { get; set; }

        /// <summary>
        ///     I22 - Sigla da UF onde ocorreu o Desembaraço Aduaneiro
        /// </summary>
        [DFeElement(TipoCampo.Str, "UFDesemb", Id = "I22", Min = 2, Max = 2, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string UFDesemb { get; set; }

        /// <summary>
        ///     I23 - Data do Desembaraço Aduaneiro.
        /// </summary>
        [DFeElement(TipoCampo.Dat, "dDesemb", Id = "I23", Min = 10, Max = 10, Ocorrencia = Ocorrencia.Obrigatoria)]
        public DateTime DDesemb { get; set; }

        /// <summary>
        ///     I23a - Via de transporte internacional informada na Declaração de Importação (DI)
        /// </summary>
        [DFeElement(TipoCampo.Enum, "tpViaTransp", Id = "I23a", Min = 2, Max = 2, Ocorrencia = Ocorrencia.Obrigatoria)]
        public NFeTpViaTransp TpViaTransp { get; set; }

        /// <summary>
        ///     I23b - Valor da AFRMM - Adicional ao Frete para Renovação da Marinha Mercante
        /// </summary>
        [DFeElement(TipoCampo.De2, "vAFRMM", Id = "I23b", Min = 3, Max = 15, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public decimal VAFRMM { get; set; }

        /// <summary>
        ///     I23c - Forma de importação quanto a intermediação
        /// </summary>
        [DFeElement(TipoCampo.Enum, "tpIntermedio", Id = "I23c", Min = 1, Max = 1, Ocorrencia = Ocorrencia.Obrigatoria)]
        public NFeTpIntermedio TpIntermedio { get; set; }

        /// <summary>
        ///     I23d - CNPJ do adquirente ou do encomendante
        /// </summary>
        [DFeElement(TipoCampo.StrNumberFill, "CNPJ", Id = "I23d", Min = 14, Max = 14, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public string CNPJ { get; set; }

        /// <summary>
        ///     I23e - Sigla da UF do adquirente ou do encomendante
        /// </summary>
        [DFeElement(TipoCampo.Str, "UFTerceiro", Id = "I23e", Min = 2, Max = 2, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public string UFTerceiro { get; set; }

        /// <summary>
        ///     I24 - Código do Exportador
        /// </summary>
        [DFeElement(TipoCampo.Str, "cExportador", Id = "I24", Min = 1, Max = 60, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string CExportador { get; set; }

        /// <summary>
        ///     I25 - Adições
        /// </summary>

        [DFeCollection("adi", Id = "I25", MinSize = 1, MaxSize = 100)]
        public DFeCollection<NFeDetImportacaoAdi> Adi { get; set; }

        #endregion

        #region Methods

        private bool ShouldSerializeVAFRMM()
        {
            return TpViaTransp == NFeTpViaTransp.Maritima;
        }

        #endregion
    }
}