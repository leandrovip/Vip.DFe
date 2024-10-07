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

        #region Constructors

        public NFeDetalhe()
        {
            Produto = new NFeDetProduto();
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
        public NFeDetProduto Produto { get; set; }

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

        #endregion

        #region Methods

        private bool ShouldSerializeImpostoDevol()
        {
            return ImpostoDevol.PDevol > 0 || ImpostoDevol.Ipi.VIpiDevol > 0;
        }

        private string SerializeInfAdProd()
        {
            return InfAdProd.RemoveBreakline();
        }

        private object DeserializeInfAdProd(string value) => value;

        #endregion
    }
}