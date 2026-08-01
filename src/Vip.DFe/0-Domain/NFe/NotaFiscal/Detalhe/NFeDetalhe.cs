using System.ComponentModel;
using Vip.DFe.Attributes;
using Vip.DFe.Document;
using Vip.DFe.Enum;
using Vip.DFe.Extensions;
using Vip.DFe.Serializer;

namespace Vip.DFe.NFe.NotaFiscal.Detalhe
{
    public sealed class NFeDetalhe : DFeParentItem<NFeDetalhe, NFe>, INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Fields

        private NFeDetProduto _produto;

        #endregion

        #region Constructors

        public NFeDetalhe()
        {
            _produto = new NFeDetProduto(this);
            Imposto = new NFeDetImposto();
            ImpostoDevol = new NFeDetImpostoDevol();
        }

        public NFeDetalhe(NFe parent) : this()
        {
            Parent = parent;
        }

        #endregion

        #region Properties

        /// <summary>
        ///     H02 - Número do item do NF
        /// </summary>
        [DFeAttribute(TipoCampo.Int, "nItem", Id = "H02", Min = 1, Max = 3, Ocorrencia = Ocorrencia.Obrigatoria)]
        public int NItem { get; set; }

        /// <summary>
        ///     I01 - Detalhamento de Produtos e Serviços
        /// </summary>
        [DFeElement("prod", Id = "I01", Ocorrencia = Ocorrencia.Obrigatoria)]
        public NFeDetProduto Produto
        {
            get => _produto;
            set
            {
                if (_produto == value) return;
                _produto = value;
                if (_produto.Parent != this)
                    _produto.Parent = this;
            }
        }

        /// <summary>
        ///     M01 - Tributos incidentes no Produto ou Serviço
        /// </summary>
        [DFeElement("imposto", Id = "M01", Ocorrencia = Ocorrencia.Obrigatoria)]
        public NFeDetImposto Imposto { get; set; }

        /// <summary>
        ///     UA01 - Informação do Imposto devolvido
        /// </summary>

        [DFeElement("impostoDevol", Id = "UA01", Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public NFeDetImpostoDevol ImpostoDevol { get; set; }

        /// <summary>
        ///     V01 - Informações Adicionais do Produto
        /// </summary>
        [DFeElement(TipoCampo.Custom, "infAdProd", Id = "V01", Min = 1, Max = 500, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public string InfAdProd { get; set; }

        /// <summary>
        ///     VA01 - Grupo de observações de uso livre para o item da NF-e
        /// </summary>
        [DFeElement("obsItem", Id = "VA01", Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public NFeDetObsItem ObsItem { get; set; }

        /// <summary>
        ///     VB01 - Valor total do Item, correspondente à sua participação no total da nota
        /// </summary>
        [DFeElement(TipoCampo.De2, "vItem", Id = "VB01", Min = 3, Max = 15, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public decimal VItem { get; set; }

        /// <summary>
        ///     VC01 - Grupo de Documento Fiscal Referenciado no item
        /// </summary>
        [DFeElement("DFeReferenciado", Id = "VC01", Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public NFeDFeReferenciado DFeReferenciado { get; set; }

        #endregion

        #region Methods

        private bool ShouldSerializeImpostoDevol()
        {
            return ImpostoDevol.PDevol > 0 || ImpostoDevol.Ipi.VIpiDevol > 0;
        }

        private bool ShouldSerializeObsItem()
        {
            return ObsItem?.ObsCont != null || ObsItem?.ObsFisco != null;
        }

        private string SerializeInfAdProd()
        {
            return InfAdProd.Truncate(500).TrimVip().RemoveBreakline();
        }

        private bool ShouldSerializeDFeReferenciado()
        {
            return DFeReferenciado?.ChaveAcesso.IsNotNullOrEmpty() == true;
        }

        private object DeserializeInfAdProd(string value) => value;

        #endregion
    }
}