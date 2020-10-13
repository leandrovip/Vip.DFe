using System.ComponentModel;
using Vip.DFe.Attributes;
using Vip.DFe.Document;
using Vip.DFe.Enum;
using Vip.DFe.Serializer;

namespace Vip.DFe.SAT.CupomFiscal
{
    /// <summary>
    ///     Classe CFeDet (det). Grupo do detalhamento de Produtos e Serviços do CF-e
    /// </summary>
    public sealed class CFeDet : DFeParentItem<CFeDet, CFe>, INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion Events

        #region Fields

        private CFeDetProd prod;

        #endregion Fields

        #region Constructors

        public CFeDet()
        {
            Imposto = new CFeDetImposto();
            Prod = new CFeDetProd();
        }

        public CFeDet(CFe parent) : this()
        {
            Parent = parent;
            Prod = new CFeDetProd(Parent);
        }

        #endregion Constructors

        #region Propriedades

        /// <summary>
        ///     Número do item (1-500)
        /// </summary>
        [DFeAttribute(TipoCampo.Int, "nItem", Id = "H02", Min = 1, Max = 3, Ocorrencia = Ocorrencia.Obrigatoria)]
        public int NItem { get; set; }

        /// <summary>
        ///     TAG de grupo do detalhamento de Produtos e Serviços do CF-e (prod).
        /// </summary>
        [DFeElement("prod", Id = "I01", Ocorrencia = Ocorrencia.Obrigatoria)]
        public CFeDetProd Prod
        {
            get => prod;
            set
            {
                prod = value;
                if (prod.Parent != Parent)
                    prod.Parent = Parent;
            }
        }

        /// <summary>
        ///     Grupo de Tributos incidentes no Produto ou Serviço (imposto)
        /// </summary>
        [DFeElement("imposto", Id = "M01", Ocorrencia = Ocorrencia.Obrigatoria)]
        public CFeDetImposto Imposto { get; set; }

        /// <summary>
        ///     Informações Adicionais do Produto
        /// </summary>
        [DFeElement(TipoCampo.Str, "infAdProd", Id = "V01", Min = 1, Max = 500, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public string InfAdProd { get; set; }

        #endregion Propriedades

        #region Methods

        protected override void OnParentChanged()
        {
            Prod.Parent = Parent;
        }

        #endregion Methods
    }
}