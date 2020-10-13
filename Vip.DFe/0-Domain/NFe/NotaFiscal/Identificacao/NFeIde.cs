using System;
using System.ComponentModel;
using Vip.DFe.Attributes;
using Vip.DFe.Document;
using Vip.DFe.Enum;
using Vip.DFe.Extensions;
using Vip.DFe.NFe.Enum;
using Vip.DFe.Serializer;
using Vip.DFe.Shared.Enum;

namespace Vip.DFe.NFe.NotaFiscal.Identificacao
{
    public sealed class NFeIde : DFeParentItem<NFeIde, infNFe>, INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Constructors

        public NFeIde()
        {
            ProcEmi = ProcessoEmissao.AplicativoContribuinte;
            NFref = new DFeCollection<NFeNfRef>();
            TipoEmissao = TipoEmissao.Normal;
        }

        #endregion

        #region Properties

        /// <summary>
        ///     B02 - Código da UF do emitente do Documento Fiscal. Utilizar a Tabela do IBGE.
        /// </summary>
        [DFeElement(TipoCampo.Enum, "cUF", Id = "B02", Min = 2, Max = 2, Ocorrencia = Ocorrencia.Obrigatoria)]
        public CodigoUF CodigoUF { get; set; }

        /// <summary>
        ///     B03 - Código numérico que compõe a Chave de Acesso. Número aleatório gerado pelo emitente para cada NF-e.
        /// </summary>
        [DFeElement(TipoCampo.StrNumber, "cNF", Id = "B03", Min = 8, Max = 8, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string CNf { get; set; }

        /// <summary>
        ///     B04 - Descrição da Natureza da Operação
        /// </summary>
        [DFeElement(TipoCampo.Str, "natOp", Id = "B04", Min = 1, Max = 60, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string NatOp { get; set; }

        /// <summary>
        ///     B06 - Modelo do Documento Fiscal
        /// </summary>
        [DFeElement(TipoCampo.Enum, "mod", Id = "B06", Min = 2, Max = 2, Ocorrencia = Ocorrencia.Obrigatoria)]
        public NFeModelo Modelo { get; set; }

        /// <summary>
        ///     B07 - Série do Documento Fiscal
        ///     <para>série normal 0-889</para>
        ///     <para>Avulsa Fisco 890-899</para>
        ///     <para>SCAN 900-999</para>
        /// </summary>
        [DFeElement(TipoCampo.Int, "serie", Id = "B07", Min = 1, Max = 3, Ocorrencia = Ocorrencia.Obrigatoria)]
        public int Serie { get; set; }

        /// <summary>
        ///     B08 - Número do Documento Fiscal (nNF)
        /// </summary>
        [DFeElement(TipoCampo.Int, "nNF", Id = "B08", Min = 1, Max = 9, Ocorrencia = Ocorrencia.Obrigatoria)]
        public long NumeroNFe { get; set; }

        /// <summary>
        ///     B09 - Data de emissão do Documento Fiscal - Informar a data de emissão do Documento Fiscal no padrão UTC
        /// </summary>
        [DFeElement(TipoCampo.DatHorTz, "dhEmi", Id = "B09", Min = 25, Max = 25, Ocorrencia = Ocorrencia.Obrigatoria)]
        public DateTimeOffset DhEmi { get; set; }

        /// <summary>
        ///     B10 - Data de Saída ou da Entrada da Mercadoria/Produto - Informar a data e hora de Saída ou da Entrada da
        ///     Mercadoria/Produto no padrão UTC
        /// </summary>
        [DFeElement(TipoCampo.DatHorTz, "dhSaiEnt", Id = "B10", Min = 25, Max = 25, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public DateTimeOffset? DhSaiEnt { get; set; }

        /// <summary>
        ///     B11 - Tipo do Documento Fiscal
        ///     <br />0 - Entrada
        ///     <br />1 - Saída
        /// </summary>
        [DFeElement(TipoCampo.Enum, "tpNF", Id = "B11", Min = 1, Max = 1, Ocorrencia = Ocorrencia.Obrigatoria)]
        public NFeTipo TipoNFe { get; set; }

        /// <summary>
        ///     B11a - Identificador de Local de destino da operação
        /// </summary>
        [DFeElement(TipoCampo.Enum, "idDest", Id = "B11a", Min = 1, Max = 1, Ocorrencia = Ocorrencia.Obrigatoria)]
        public NFeDestinoOperacao IdDest { get; set; }

        /// <summary>
        ///     B12 - Código do Município de Ocorrência do Fato Gerador (utilizar a tabela do IBGE)
        /// </summary>
        [DFeElement(TipoCampo.Int, "cMunFG", Id = "B12", Min = 7, Max = 7, Ocorrencia = Ocorrencia.Obrigatoria)]
        public int CMunFG { get; set; }

        /// <summary>
        ///     B21 - Formato de impressão do DANFE
        /// </summary>
        [DFeElement(TipoCampo.Enum, "tpImp", Id = "B21", Min = 1, Max = 1, Ocorrencia = Ocorrencia.Obrigatoria)]
        public TipoImpressao TipoImpressao { get; set; }

        /// <summary>
        ///     B22 - Tipo de Emissão da NF-e
        /// </summary>
        [DFeElement(TipoCampo.Enum, "tpEmis", Id = "B22", Min = 1, Max = 1, Ocorrencia = Ocorrencia.Obrigatoria)]
        public TipoEmissao TipoEmissao { get; set; }

        /// <summary>
        ///     B23 - Digito Verificador da Chave de Acesso da NF-e
        /// </summary>
        [DFeElement(TipoCampo.Int, "cDV", Id = "B23", Min = 1, Max = 1, Ocorrencia = Ocorrencia.Obrigatoria)]
        public int CDV { get; set; }

        /// <summary>
        ///     B24 - Identificação do Ambiente
        /// </summary>
        [DFeElement(TipoCampo.Enum, "tpAmb", Id = "B24", Min = 1, Max = 1, Ocorrencia = Ocorrencia.Obrigatoria)]
        public TipoAmbiente TpAmb { get; set; }

        /// <summary>
        ///     B25a - Finalidade da emissão da NF-e
        /// </summary>
        [DFeElement(TipoCampo.Enum, "finNFe", Id = "B25", Min = 1, Max = 1, Ocorrencia = Ocorrencia.Obrigatoria)]
        public NFeFinalidade FinNFe { get; set; }

        /// <summary>
        ///     B25a - Indica operação com consumidor final
        /// </summary>
        [DFeElement(TipoCampo.Enum, "indFinal", Id = "B25a", Min = 1, Max = 1, Ocorrencia = Ocorrencia.Obrigatoria)]
        public NFeConsumidorFinal IndFinal { get; set; }

        /// <summary>
        ///     B25b - Indicador de presença do comprador no estabelecimento comercial no momento da operação
        /// </summary>
        [DFeElement(TipoCampo.Enum, "indPres", Id = "B25b", Min = 1, Max = 1, Ocorrencia = Ocorrencia.Obrigatoria)]
        public NFePresencaComprador IndPres { get; set; }

        /// <summary>
        ///     B26 - Processo de emissão utilizado com a seguinte codificação:
        /// </summary>
        [DFeElement(TipoCampo.Enum, "procEmi", Id = "B26", Min = 1, Max = 1, Ocorrencia = Ocorrencia.Obrigatoria)]
        public ProcessoEmissao ProcEmi { get; set; }

        /// <summary>
        ///     B27 - versão do aplicativo utilizado no processo de emissão
        /// </summary>
        [DFeElement(TipoCampo.Custom, "verProc", Id = "B27", Min = 1, Max = 20, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string VerProc { get; set; }

        /// <summary>
        ///     B28 - Informar apenas para tpEmis diferente de 1
        ///     <para>
        ///         Informar a data e hora de entrada em contingência
        ///     </para>
        /// </summary>
        [DFeElement(TipoCampo.DatHorTz, "dhCont", Id = "B28", Min = 1, Max = 25, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public DateTime DhCont { get; set; }

        /// <summary>
        ///     B29 - Informar a Justificativa da entrada
        /// </summary>
        [DFeElement(TipoCampo.Str, "xJust", Id = "B29", Min = 15, Max = 256, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public string XJust { get; set; }

        /// <summary>
        ///     BA01 - Informação de Documentos Fiscais referenciados
        /// </summary>
        [DFeCollection("NFref", Id = "BA01", MinSize = 0, MaxSize = 500, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public DFeCollection<NFeNfRef> NFref { get; set; }

        //#endregion

        #endregion

        #region Methods

        public bool ShouldSerializeDhSaiEnt()
        {
            return DhSaiEnt.HasValue;
        }

        public bool ShouldSerializeDhCont()
        {
            return TipoEmissao != TipoEmissao.Normal;
        }

        public bool ShouldSerializeXJust()
        {
            return TipoEmissao != TipoEmissao.Normal;
        }

        private string SerializeVerProc()
        {
            return VerProc.IsNullOrEmpty() ? "4.00" : VerProc;
        }

        private object DeserializeVerProc(string value)
        {
            return value;
        }

        #endregion
    }
}