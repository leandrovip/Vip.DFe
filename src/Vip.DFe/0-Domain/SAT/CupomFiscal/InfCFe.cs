using System.ComponentModel;
using System.Linq;
using Vip.DFe.Attributes;
using Vip.DFe.Document;
using Vip.DFe.Enum;
using Vip.DFe.Extensions;
using Vip.DFe.Serializer;

namespace Vip.DFe.SAT.CupomFiscal
{
    /// <summary>
    ///     Grupo das informações do CF-e
    /// </summary>
    public sealed class InfCFe : DFeParentItem<InfCFe, CFe>, INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion Events

        #region Fields

        private CFeDetCollection det;
        private CFeInfAdic infAdic;

        #endregion Fields

        #region Constructor

        public InfCFe()
        {
            Ide = new CFeIde();
            Emit = new CFeEmit();
            Dest = new CFeDest();
            Entrega = new CFeEntrega();
            Det = new CFeDetCollection();
            Total = new CFeTotal();
            Pagto = new CFePgto();
            InfAdic = new CFeInfAdic();
            ObsFisco = new DFeCollection<CFeObsFisco>();
        }

        public InfCFe(CFe parent) : this()
        {
            Parent = parent;
            Det = new CFeDetCollection(Parent);
            InfAdic = new CFeInfAdic(Parent);
        }

        #endregion Constructor

        #region Propriedades

        /// <summary>
        ///     Identifiador da TAG a ser assinada
        ///     <br /><b>Campo preenchido pelo SAT</b>
        /// </summary>
        [DFeAttribute(TipoCampo.Str, "Id")]
        public string Id { get; set; }

        /// <summary>
        ///     Versão do leiaute do CF-e
        ///     <br /><b>Campo preenchido pelo SAT</b>
        /// </summary>
        [DFeAttribute(TipoCampo.De2, "versao", Min = 4, Max = 9, Ocorrencia = Ocorrencia.MaiorQueZero)]
        public decimal Versao { get; set; }

        /// <summary>
        ///     Versão do leiaute do arquivo de dados do AC
        /// </summary>
        [DFeAttribute(TipoCampo.De2, "versaoDadosEnt", Min = 4, Max = 9, Ocorrencia = Ocorrencia.MaiorQueZero)]
        public decimal VersaoDadosEnt { get; set; }

        /// <summary>
        ///     Versão do Software Básico do SAT
        ///     <br /><b>Campo preenchido pelo SAT</b>
        /// </summary>
        [DFeAttribute(TipoCampo.De2, "versaoSB", Ocorrencia = Ocorrencia.MaiorQueZero)]
        public decimal VersaoSb { get; set; }

        /// <summary>
        ///     Grupo das informações de identificação do CF-e
        /// </summary>
        [DFeElement("ide", Id = "B01", Ocorrencia = Ocorrencia.Obrigatoria)]
        public CFeIde Ide { get; set; }

        /// <summary>
        ///     Grupo de identificação do emitente do CF-e
        /// </summary>
        [DFeElement("emit", Id = "C01", Ocorrencia = Ocorrencia.Obrigatoria)]
        public CFeEmit Emit { get; set; }

        /// <summary>
        ///     Grupo de identificação do Destinatário do CF-e
        /// </summary>
        [DFeElement("dest", Id = "E01", Ocorrencia = Ocorrencia.Obrigatoria)]
        public CFeDest Dest { get; set; }

        /// <summary>
        ///     Grupo de identificação do Local de entrega
        /// </summary>
        [DFeElement("entrega", Id = "G01", Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public CFeEntrega Entrega { get; set; }

        /// <summary>
        ///     Grupo do detalhamento de Produtos e Serviços do CF-e
        /// </summary>
        [DFeCollection("det", Id = "H01", MinSize = 1, MaxSize = 500)]
        public CFeDetCollection Det
        {
            get => det;
            set
            {
                det = value;
                if (det != null && det.Parent != Parent)
                    det.Parent = Parent;
            }
        }

        /// <summary>
        ///     Grupo de Valores Totais do CF-e
        /// </summary>
        [DFeElement("total", Id = "W01", Ocorrencia = Ocorrencia.Obrigatoria)]
        public CFeTotal Total { get; set; }

        /// <summary>
        ///     Grupo de informações sobre Pagamento do CF-e
        /// </summary>
        [DFeElement("pgto", Id = "WA01", Ocorrencia = Ocorrencia.Obrigatoria)]
        public CFePgto Pagto { get; set; }

        /// <summary>
        ///     Grupo de Informações Adicionais
        /// </summary>
        [DFeElement("infAdic", Id = "Z01", Ocorrencia = Ocorrencia.Obrigatoria)]
        public CFeInfAdic InfAdic
        {
            get => infAdic;
            set
            {
                if (infAdic == value) return;

                infAdic = value;
                if (value != null && value.Parent != Parent)
                    value.Parent = Parent;
            }
        }

        /// <summary>
        ///     Grupo do campo de uso livre do Fisco
        /// </summary>
        [DFeCollection("obsFisco", Id = "Z03", MinSize = 0, MaxSize = 10, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public DFeCollection<CFeObsFisco> ObsFisco { get; set; }

        #endregion Propriedades

        #region Methods

        private bool ShouldSerializeId()
        {
            return !Id.IsNullOrEmpty();
        }

        private bool ShouldSerializeEntrega()
        {
            return !Entrega.XLgr.IsNullOrEmpty() ||
                   !Entrega.Nro.IsNullOrEmpty() ||
                   !Entrega.XCpl.IsNullOrEmpty() ||
                   !Entrega.XBairro.IsNullOrEmpty() ||
                   !Entrega.XMun.IsNullOrEmpty() ||
                   !Entrega.UF.IsNullOrEmpty();
        }

        private bool ShouldSerializeInfAdic()
        {
            return !InfAdic.InfCpl.IsNullOrEmpty() || InfAdic.ObsFisco.Any() && Versao < 0.08M;
        }

        private bool ShouldSerializeObsFisco()
        {
            return Versao > 0.07M && ObsFisco.Any();
        }

        protected override void OnParentChanged()
        {
            Det.Parent = Parent;
            InfAdic.Parent = Parent;
        }

        #endregion Methods
    }
}