using System.ComponentModel;
using System.Linq;
using Vip.DFe.Attributes;
using Vip.DFe.Document;
using Vip.DFe.Enum;
using Vip.DFe.Extensions;
using Vip.DFe.NFe.Enum;
using Vip.DFe.NFe.Interfaces;
using Vip.DFe.Serializer;

namespace Vip.DFe.NFe.NotaFiscal.Detalhe
{
    public sealed class NFeDetProduto : GenericClone<NFeDetProduto>, INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Fields

        private string _nRecopi;
        private INFeProdutoEspecifico _produtoEspecifico;

        #endregion

        #region Constructors

        public NFeDetProduto()
        {
            NVE = new DFeCollection<string>();
            DeclaracaoImportacao = new DFeCollection<NFeDetImportacao>();
            Exportacao = new DFeCollection<NFeDetExportacao>();
            Rastreabilidade = new DFeCollection<NFeDetRastreabilidade>();
        }

        #endregion

        #region Properties

        /// <summary>
        ///     I02 - Código do produto ou serviço - cProd
        /// </summary>
        [DFeElement(TipoCampo.Str, "cProd", Id = "I02", Min = 1, Max = 60, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string Codigo { get; set; }

        /// <summary>
        ///     I03 - GTIN (Global Trade Item Number) do produto, antigo código EAN ou código de barras - cEAN
        /// </summary>
        [DFeElement(TipoCampo.Custom, "cEAN", Id = "I03", Min = 8, Max = 14, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string CEAN { get; set; }

        /// <summary>
        ///     I04 - Descrição do produto ou serviço
        /// </summary>
        [DFeElement(TipoCampo.Str, "xProd", Id = "I04", Min = 2, Max = 120, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string XProd { get; set; }

        /// <summary>
        ///     I05 - Código NCM (8 posições). Em caso de item de serviço ou item que não tenham produto (Ex. transferência de
        ///     crédito, crédito do ativo imobilizado, etc.), informar o código 00 (zeros) (v2.0)
        /// </summary>
        [DFeElement(TipoCampo.StrNumberFill, "NCM", Id = "I05", Min = 2, Max = 8, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string NCM { get; set; }

        /// <summary>
        ///     105a - Nomenclatura de Valor aduaneio e Estatístico
        ///     <para>Ocorrência: 0-8</para>
        /// </summary>
        [DFeCollection("NVE", Id = "105a", MinSize = 0, MaxSize = 8, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public DFeCollection<string> NVE { get; set; }

        /// <summary>
        ///     I05c - Código CEST
        /// </summary>
        [DFeElement(TipoCampo.StrNumber, "CEST", Id = "I05c", Min = 7, Max = 7, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public string CEST { get; set; }

        /// <summary>
        ///     Versão 4.00
        ///     Indicador de Produção em escala relevante, conforme Cláusula 23 do Convenio ICMS 52/2017 ||||
        ///     Nota: preenchimento obrigatório para produtos com NCM
        ///     relacionado no Anexo XXVII do Convenio 52/2017
        /// </summary>
        [DFeElement(TipoCampo.Enum, "indEscala", Id = "I05d", Min = 1, Max = 1, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public NFeIndEscala? IndEscala { get; set; }

        /// <summary>
        ///     Versão 4.00
        ///     CNPJ do Fabricante da Mercadoria, obrigatório para produto em escala NÃO relevante.
        /// </summary>
        [DFeElement(TipoCampo.StrNumberFill, "CNPJFab", Id = "I05e", Min = 14, Max = 14, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public string CNPJFab { get; set; }

        /// <summary>
        ///     Código de Benefício fiscal utilizado pela UF, aplicado ao item. Obs: Deve ser utilizado o mesmo código adotado na
        ///     EFD e outras declarações, nas UF que o exigem.
        /// </summary>
        [DFeElement(TipoCampo.Str, "cBenef", Min = 10, Max = 10, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public string CBenef { get; set; }

        /// <summary>
        ///     I06 - Código EX TIPI (3 posições)
        /// </summary>
        [DFeElement(TipoCampo.StrNumber, "EXTIPI", Id = "I06", Min = 2, Max = 3, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public string EXTIPI { get; set; }

        /// <summary>
        ///     I08 - Código Fiscal de Operações e Prestações
        /// </summary>
        [DFeElement(TipoCampo.Int, "CFOP", Id = "I08", Min = 4, Max = 4, Ocorrencia = Ocorrencia.Obrigatoria)]
        public int CFOP { get; set; }

        /// <summary>
        ///     I09 - Unidade comercial
        /// </summary>
        [DFeElement(TipoCampo.Str, "uCom", Id = "I09", Min = 1, Max = 6, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string UCom { get; set; }

        /// <summary>
        ///     I10 - Quantidade Comercial  do produto, alterado para aceitar de 0 a 4 casas decimais e 11 inteiros.
        /// </summary>
        [DFeElement(TipoCampo.De4, "qCom", Id = "I10", Min = 5, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal QCom { get; set; }

        /// <summary>
        ///     I10a - Valor Unitário de Comercialização, campos meramente informativo. Para efeitos de cálculo, o valor unitário
        ///     será obtido pela divisão do valor do produto pela quantidade comercial.
        /// </summary>
        [DFeElement(TipoCampo.De10, "vUnCom", Id = "I10a", Ocorrencia = Ocorrencia.MaiorQueZero)]
        public decimal VUnCom { get; set; }

        /// <summary>
        ///     I11 - Valor Total Bruto dos Produtos ou Serviços
        /// </summary>
        [DFeElement(TipoCampo.De2, "vProd", Id = "I11", Min = 3, Max = 15, Ocorrencia = Ocorrencia.MaiorQueZero)]
        public decimal VProd { get; set; }

        /// <summary>
        ///     I12 - GTIN (Global Trade Item Number) do produto, antigo código EAN ou código de barras
        /// </summary>
        [DFeElement(TipoCampo.Custom, "cEANTrib", Id = "I12", Min = 0, Max = 14, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string CEANTrib { get; set; }

        /// <summary>
        ///     I13 - Unidade Tributável
        /// </summary>
        [DFeElement(TipoCampo.Str, "uTrib", Id = "I13", Min = 1, Max = 6, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string UTrib { get; set; }

        /// <summary>
        ///     I14 - Quantidade Tributável
        /// </summary>
        [DFeElement(TipoCampo.De4, "qTrib", Id = "I14", Min = 5, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal QTrib { get; set; }

        /// <summary>
        ///     I14a - Valor Unitário de tributação
        /// </summary>
        [DFeElement(TipoCampo.De10, "vUnTrib", Id = "I14a", Min = 3, Ocorrencia = Ocorrencia.MaiorQueZero)]
        public decimal VUnTrib { get; set; }

        /// <summary>
        ///     I15 - Valor Total do Frete
        /// </summary>
        [DFeElement(TipoCampo.De2, "vFrete", Id = "I15", Min = 3, Max = 15, Ocorrencia = Ocorrencia.MaiorQueZero)]
        public decimal VFrete { get; set; }

        /// <summary>
        ///     I16 - Valor Total do Seguro
        /// </summary>
        [DFeElement(TipoCampo.De2, "vSeg", Id = "I16", Min = 3, Max = 15, Ocorrencia = Ocorrencia.MaiorQueZero)]
        public decimal VSeg { get; set; }

        /// <summary>
        ///     I17 - Valor do Desconto
        /// </summary>
        [DFeElement(TipoCampo.De2, "vDesc", Id = "I17", Min = 3, Max = 15, Ocorrencia = Ocorrencia.MaiorQueZero)]
        public decimal VDesc { get; set; }

        /// <summary>
        ///     I17a - Outras despesas acessórias
        /// </summary>
        [DFeElement(TipoCampo.De2, "vOutro", Id = "I17a", Min = 3, Max = 15, Ocorrencia = Ocorrencia.MaiorQueZero)]
        public decimal VOutro { get; set; }

        /// <summary>
        ///     I17b - Indica se valor do Item (vProd) entra no valor total da NF-e (vProd)
        /// </summary>
        [DFeElement(TipoCampo.Enum, "indTot", Id = "I17b", Min = 1, Max = 1, Ocorrencia = Ocorrencia.Obrigatoria)]
        public NFeIndTotal IndTot { get; set; }

        /// <summary>
        ///     I18 - Declaração de Importação
        /// </summary>
        [DFeCollection("DI", Id = "I18", MinSize = 0, MaxSize = 100, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public DFeCollection<NFeDetImportacao> DeclaracaoImportacao { get; set; }

        /// <summary>
        ///     I50 - Grupo de informações de exportação para o item
        /// </summary>
        [DFeCollection("detExport", Id = "I50", MinSize = 0, MaxSize = 500, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public DFeCollection<NFeDetExportacao> Exportacao { get; set; }

        /// <summary>
        ///     I60 - Número do Pedido de Compra
        /// </summary>
        [DFeElement(TipoCampo.Str, "xPed", Id = "I60", Min = 1, Max = 15, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public string XPed { get; set; }

        /// <summary>
        ///     I61 - Item do Pedido de Compra
        /// </summary>
        [DFeElement(TipoCampo.Int, "nItemPed", Id = "I61", Min = 1, Max = 6, Ocorrencia = Ocorrencia.MaiorQueZero)]
        public int NItemPed { get; set; }

        /// <summary>
        ///     I70 - Número de controle da FCI - Ficha de Conteúdo de Importação
        /// </summary>
        [DFeElement(TipoCampo.Str, "nFCI", Id = "I70", Min = 36, Max = 36, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public string NFCI { get; set; }

        /// <summary>
        ///     I80 - Detalhamento de produto sujeito a rastreabilidade
        ///     Versão 4.00
        /// </summary>
        [DFeCollection("rastro", Id = "I80", MinSize = 1, Max = 500, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public DFeCollection<NFeDetRastreabilidade> Rastreabilidade { get; set; }

        /// <summary>
        ///     <para>129 (veicProd) - Detalhamento de Veículos novos</para>
        ///     <para>K01 (med) - Detalhamento de Medicamentos e de matérias-primas farmacêuticas</para>
        ///     <para>L01 (arma) - Detalhamento de Armamento</para>
        ///     <para>LA01 (comb) - Informações específicas para combustíveis líquidos e lubrificantes</para>
        /// </summary>
        [DFeItem(typeof(NFeDetProdutoVeiculo), "veicProd")]
        [DFeItem(typeof(NFeDetProdutoMed), "med")]
        [DFeItem(typeof(NFeDetProdutoArma), "arma")]
        [DFeItem(typeof(NFeDetProdutoCombustivel), "comb")]
        public INFeProdutoEspecifico ProdutoEspecifico
        {
            get => _produtoEspecifico;
            set
            {
                NRECOPI = null; //ProdutoEspecifico e nRECOPI são mutuamente exclusivos
                _produtoEspecifico = value;
            }
        }

        /// <summary>
        ///     LB01 - Número do RECOPI
        /// </summary>
        [DFeElement(TipoCampo.StrNumber, "nRECOPI", Id = "LB01", Min = 20, Max = 20, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public string NRECOPI
        {
            get => _nRecopi;
            set
            {
                if (value.IsNullOrEmpty()) return;
                ProdutoEspecifico = null; //ProdutoEspecifico e nRECOPI são mutuamente exclusivos
                _nRecopi = value;
            }
        }

        #endregion

        #region Methods

        private bool ShouldSerializeIndEscala()
        {
            return IndEscala.HasValue;
        }

        private bool ShouldSerializeDeclaracaoImportacaoCampo()
        {
            return DeclaracaoImportacao.Any();
        }

        private bool ShouldSerializeNItemPed()
        {
            return XPed.IsNotNullOrEmpty();
        }

        private string SerializeCEAN()
        {
            return GetGtin(CEAN);
        }

        private object DeserializeCEAN(string value)
        {
            return value;
        }

        private string SerializeCEANTrib()
        {
            return GetGtin(CEANTrib);
        }

        private object DeserializeCEANTrib(string value)
        {
            return value;
        }

        private static string GetGtin(string code)
        {
            code = code.OnlyNumbers();
            return code.IsGtin() ? code : "SEM GTIN";
        }

        #endregion
    }
}