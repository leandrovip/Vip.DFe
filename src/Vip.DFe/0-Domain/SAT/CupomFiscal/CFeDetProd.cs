using System.ComponentModel;
using System.Globalization;
using Vip.DFe.Attributes;
using Vip.DFe.Document;
using Vip.DFe.Enum;
using Vip.DFe.SAT.Enum;
using Vip.DFe.Serializer;

namespace Vip.DFe.SAT.CupomFiscal
{
    /// <summary>
    ///     TAG de grupo do detalhamento de Produtos e Servi�os do CF-e (prod.)
    /// </summary>
    public sealed class CFeDetProd : DFeParentItem<CFeDetProd, CFe>, INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion Events

        #region Fields

        private bool _seCombustivel;

        #endregion Fields

        #region Constructors

        public CFeDetProd()
        {
            ObsFiscoDet = new DFeCollection<ProdObsFisco>();
            SeCombustivel = false;
        }

        public CFeDetProd(CFe parent) : this()
        {
            Parent = parent;
        }

        #endregion Constructors

        #region Propriedades

        [DFeIgnore]
        public bool SeCombustivel
        {
            get => _seCombustivel;
            set
            {
                if (value == _seCombustivel) return;

                IndRegra = value ? IndRegra.Truncamento : IndRegra.Arredondamento;
                _seCombustivel = value;
            }
        }

        /// <summary>
        ///     C�digo do produto ou servi�o, interno do contribuinte
        /// </summary>
        [DFeElement(TipoCampo.Str, "cProd", Id = "I02", Min = 1, Max = 60, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string CProd { get; set; }

        /// <summary>
        ///     GTIN (Global Trade Item Number) do produto, antigo c�digo EAN ou c�digo de barras
        /// </summary>
        [DFeElement(TipoCampo.Str, "cEAN", Id = "I03", Min = 8, Max = 14, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public string CEAN { get; set; }

        /// <summary>
        ///     Descri��o do produto ou servi�o
        /// </summary>
        [DFeElement(TipoCampo.Str, "xProd", Id = "I04", Min = 1, Max = 120, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string XProd { get; set; }

        /// <summary>
        ///     C�digo NCM com 8 d�gitos ou 2 d�gitos(g�nero)
        /// </summary>
        [DFeElement(TipoCampo.Str, "NCM", Id = "I05", Min = 2, Max = 8, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public string NCM { get; set; }

        /// <summary>
        ///     C�digo Especificador da Substitui��o Tribut�ria
        /// </summary>
        [DFeElement(TipoCampo.Str, "CEST", Id = "I05w", Min = 2, Max = 7, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public string CEST { get; set; }

        /// <summary>
        ///     C�digo Fiscal de Opera��es e Presta��es
        /// </summary>
        [DFeElement(TipoCampo.StrNumberFill, "CFOP", Id = "I06", Min = 4, Max = 4, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string CFOP { get; set; }

        /// <summary>
        ///     Informar a unidade de comercializa��o do produto. No caso de combust�veis, utilizar a unidade de medida da
        ///     codifica��o de produtos do Sistema de Informa��es de Movimenta��o de produtos - SIMP
        /// </summary>
        [DFeElement(TipoCampo.Str, "uCom", Id = "I07", Min = 1, Max = 6, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string UCom { get; set; }

        /// <summary>
        ///     Quantidade Comercial
        /// </summary>
        [DFeElement(TipoCampo.De4, "qCom", Id = "I08", Min = 5, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal QCom { get; set; }

        /// <summary>
        ///     Valor Unit�rio de Comercializa��o
        /// </summary>
        [DFeElement(TipoCampo.Custom, "vUnCom", Id = "I09", Min = 3, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal VUnCom { get; set; }

        /// <summary>
        ///     Valor Bruto dos Produtos ou Servi�os. Valor calculado pelo SAT.
        /// </summary>
        [DFeElement(TipoCampo.De2, "vProd", Id = "I10", Min = 3, Max = 15, Ocorrencia = Ocorrencia.MaiorQueZero)]
        public decimal VProd { get; set; }

        /// <summary>
        ///     Indicador da regra de c�lculo utilizada para Valor Bruto dos Produtos e Servi�os:
        ///     <para>A - Arredondamento</para>
        ///     <para>T - Truncamento</para>
        /// </summary>
        [DFeElement(TipoCampo.Enum, "indRegra", Id = "I11", Min = 1, Max = 1, Ocorrencia = Ocorrencia.Obrigatoria)]
        public IndRegra IndRegra { get; set; }

        /// <summary>
        ///     Valor do desconto incidente sobre o valor do item
        /// </summary>
        [DFeElement(TipoCampo.De2, "vDesc", Id = "I12", Min = 3, Max = 15, Ocorrencia = Ocorrencia.MaiorQueZero)]
        public decimal VDesc { get; set; }

        /// <summary>
        ///     Valor de acr�scimos sobre valor do item
        /// </summary>
        [DFeElement(TipoCampo.De2, "vOutro", Id = "I13", Min = 3, Max = 15, Ocorrencia = Ocorrencia.MaiorQueZero)]
        public decimal VOutro { get; set; }

        /// <summary>
        ///     Valor l�quido do Item. Calculado pelo SAT
        /// </summary>
        [DFeElement(TipoCampo.De2, "vItem", Id = "I14", Min = 3, Max = 15, Ocorrencia = Ocorrencia.MaiorQueZero)]
        public decimal VItem { get; set; }

        /// <summary>
        ///     Rateio do desconto sobre subtotal. Calculado pelo SAT
        /// </summary>
        [DFeElement(TipoCampo.De2, "vRatDesc", Id = "I15", Min = 3, Max = 15, Ocorrencia = Ocorrencia.MaiorQueZero)]
        public decimal VRatDesc { get; set; }

        /// <summary>
        ///     Rateio do acr�scimo sobre subtotal. Calculado pelo SAT
        /// </summary>
        [DFeElement(TipoCampo.De2, "vRatAcr", Id = "I16", Min = 3, Max = 15, Ocorrencia = Ocorrencia.MaiorQueZero)]
        public decimal VRatAcr { get; set; }

        /// <summary>
        ///     Grupo do campo de uso livre do Fisco
        /// </summary>
        [DFeCollection("obsFiscoDet", Id = "I18", MinSize = 0, MaxSize = 500, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public DFeCollection<ProdObsFisco> ObsFiscoDet { get; set; }

        #endregion Propriedades

        #region Methods

        private bool ShouldSerializeCEST()
        {
            return Parent != null && Parent.InfCFe.Versao > 0.08M;
        }

        private string SerializeVUnCom()
        {
            var numberFormat = CultureInfo.InvariantCulture.NumberFormat;
            var format = _seCombustivel ? "{0:0.000}" : "{0:0.00}";
            return string.Format(numberFormat, format, VUnCom);
        }

        private object DeserializeVUnCom(string value)
        {
            var decimais = value.Split('.')[1];
            SeCombustivel = decimais.Length > 2;
            var numberFormat = CultureInfo.InvariantCulture.NumberFormat;
            return decimal.Parse(value, numberFormat);
        }

        #endregion Methods
    }
}