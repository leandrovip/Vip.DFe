using System.ComponentModel;
using System.Linq;
using Vip.DFe.Attributes;
using Vip.DFe.Document;
using Vip.DFe.Enum;
using Vip.DFe.Extensions;
using Vip.DFe.NFe.Enum;
using Vip.DFe.NFe.NotaFiscal.Cana;
using Vip.DFe.NFe.NotaFiscal.Cobranca;
using Vip.DFe.NFe.NotaFiscal.Destinatario;
using Vip.DFe.NFe.NotaFiscal.Detalhe;
using Vip.DFe.NFe.NotaFiscal.Emitente;
using Vip.DFe.NFe.NotaFiscal.Identificacao;
using Vip.DFe.NFe.NotaFiscal.InformacaoAdicional;
using Vip.DFe.NFe.NotaFiscal.Pagamento;
using Vip.DFe.NFe.NotaFiscal.Total;
using Vip.DFe.NFe.NotaFiscal.Transporte;
using Vip.DFe.Serializer;

namespace Vip.DFe.NFe.NotaFiscal
{
    public sealed class infNFe : DFeParentItem<infNFe, NFe>, INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Fields

        private NFeIde _ide;
        private NFeDetCollection _detalhe;
        private NFeDest _destinatario;

        #endregion

        #region Constructors

        public infNFe()
        {
            Versao = NFeVersao.v400;
            _ide = new NFeIde();
            Emitente = new NFeEmit();
            Avulsa = new NFeAvulsa();
            _destinatario = new NFeDest(this);
            Retirada = new NFeRetirada();
            Entrega = new NFeEntrega();
            AutXML = new DFeCollection<NFeAutXml>();
            Detalhe = new NFeDetCollection();
            Total = new NFeTotal();
            Transporte = new NFeTransporte();
            Cobranca = new NFeCobranca();
            Pagamento = new NFePagamento();
            Intermediador = new NFeIntermediador();
            InformacaoAdicional = new NFeInformacaoAdicional();
            Exporta = new NFeExporta();
            Compra = new NFeCompra();
            Cana = new NFeCana();
            ResponsavelTecnico = new NFeResponsavelTecnico();
        }

        public infNFe(NFe parent) : this()
        {
            Parent = parent;
            _detalhe = new NFeDetCollection(Parent);
        }

        #endregion

        #region Properties

        /// <summary>
        ///     A02 - Versão do leiaute da NFe (3.1, 4.0, etc)
        /// </summary>
        [DFeAttribute(TipoCampo.Enum, "versao", Id = "A02", Min = 4, Max = 4, Ocorrencia = Ocorrencia.Obrigatoria)]
        public NFeVersao Versao { get; set; }

        /// <summary>
        ///     A03 - Identificador da TAG a ser assinada
        ///     <para>informar a chave de acesso da NF-e precedida do literal "NFe", acrescentada a validação do formato (v2.0).</para>
        /// </summary>
        [DFeAttribute(TipoCampo.Str, "Id", Id = "A03", Min = 44, Max = 47, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string Id { get; set; }

        /// <summary>
        ///     B01 - Identificação da NF-e
        /// </summary>
        [DFeElement("ide", Id = "B01", Ocorrencia = Ocorrencia.Obrigatoria)]
        public NFeIde Ide
        {
            get => _ide;
            set
            {
                if (_ide == value) return;
                _ide = value;
                if (_ide.Parent != this)
                    _ide.Parent = this;
            }
        }

        /// <summary>
        ///     C01 - Grupo de identificação do emitente da NF-e
        /// </summary>
        [DFeElement("emit", Id = "C01", Ocorrencia = Ocorrencia.Obrigatoria)]
        public NFeEmit Emitente { get; set; }

        /// <summary>
        ///     D01 - Informações do fisco emitente (uso exclusivo do fisco)
        /// </summary>
        [DFeElement("avulsa", Id = "D01", Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public NFeAvulsa Avulsa { get; set; }

        /// <summary>
        ///     E01 - Grupo de identificação do Destinatário da NF-e
        /// </summary>
        [DFeElement("dest", Ocorrencia = Ocorrencia.Obrigatoria)]
        public NFeDest Destinatario
        {
            get => _destinatario;
            set
            {
                if (_destinatario == value) return;
                _destinatario = value;
                if (_destinatario?.Parent != this)
                    _destinatario.Parent = this;
            }
        }

        /// <summary>
        ///     F01 - Identificação do Local de retirada
        /// </summary>
        [DFeElement("retirada", Id = "F01", Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public NFeRetirada Retirada { get; set; }

        /// <summary>
        ///     G01 - Identificação do Local de entrega
        /// </summary>
        [DFeElement("entrega", Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public NFeEntrega Entrega { get; set; }

        /// <summary>
        ///     GA01 - Pessoas autorizadas a acessar o XML da NF-e
        ///     <para>Ocorrência: 0-10</para>
        /// </summary>
        [DFeCollection("autXML", Id = "GA01", MinSize = 0, MaxSize = 10, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public DFeCollection<NFeAutXml> AutXML { get; set; }

        /// <summary>
        ///     H01 - Dados dos detalhes da NF-e
        ///     <para>Ocorrência: 1-990</para>
        /// </summary>
        [DFeCollection("det", Id = "H01", MinSize = 1, MaxSize = 999, Ocorrencia = Ocorrencia.Obrigatoria)]
        public NFeDetCollection Detalhe
        {
            get => _detalhe;
            set
            {
                if (_detalhe == value) return;
                _detalhe = value;
                if (_detalhe?.Parent != Parent)
                    _detalhe.Parent = Parent;
            }
        }

        /// <summary>
        ///     W01 - Grupo Totais da NF-e
        /// </summary>
        [DFeElement("total", Id = "W01", Ocorrencia = Ocorrencia.Obrigatoria)]
        public NFeTotal Total { get; set; }

        /// <summary>
        ///     X01 - Grupo Informações do Transporte
        /// </summary>
        [DFeElement("transp", Id = "X01", Ocorrencia = Ocorrencia.Obrigatoria)]
        public NFeTransporte Transporte { get; set; }

        /// <summary>
        ///     Y01 - Grupo Cobrança
        /// </summary>
        [DFeElement("cobr", Id = "Y01", Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public NFeCobranca Cobranca { get; set; }

        /// <summary>
        ///     YA01 - Grupo de Formas de Pagamento
        ///     <para>Ocorrência: 0-100</para>
        /// </summary>
        [DFeElement("pag", Id = "YA01", Ocorrencia = Ocorrencia.Obrigatoria)]
        public NFePagamento Pagamento { get; set; }

        /// <summary>
        ///     YB01 - Grupo de Informações do Intermediador da Transação
        /// </summary>
        [DFeElement("infIntermed", Id = "YB01", Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public NFeIntermediador Intermediador { get; set; }

        /// <summary>
        ///     Z01 - Grupo de Informações Adicionais
        /// </summary>
        [DFeElement("infAdic", Id = "Z01", Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public NFeInformacaoAdicional InformacaoAdicional { get; set; }

        /// <summary>
        ///     ZA01 - Grupo Exportação
        /// </summary>
        [DFeElement("exporta", Id = "ZA01", Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public NFeExporta Exporta { get; set; }

        /// <summary>
        ///     ZB01 - Grupo Compra
        /// </summary>
        [DFeElement("compra", Id = "ZB01", Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public NFeCompra Compra { get; set; }

        /// <summary>
        ///     ZC01 - Grupo Cana
        /// </summary>
        [DFeElement("cana", Id = "ZC01", Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public NFeCana Cana { get; set; }

        /// <summary>
        ///     ZD01 - Informações do Responsável Técnico pela Emissão do DF-e
        /// </summary>
        [DFeElement("infRespTec", Id = "ZD01", Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public NFeResponsavelTecnico ResponsavelTecnico { get; set; }

        #endregion

        #region Methods

        private bool ShouldSerializeAvulsa()
        {
            return Avulsa.CNPJ.IsNotNullOrEmpty() || Avulsa.XOrgao.IsNotNullOrEmpty() || Avulsa.Matr.IsNotNullOrEmpty() || Avulsa.XAgente.IsNotNullOrEmpty();
        }

        private bool ShouldSerializeDestinatario()
        {
            return Destinatario.CNPJ.IsNotNullOrEmpty() || Destinatario.CPF.IsNotNullOrEmpty() || Destinatario.Nome.IsNotNullOrEmpty();
        }

        private bool ShouldSerializeRetirada()
        {
            return Retirada.CNPJ.IsNotNullOrEmpty() || Retirada.CPF.IsNotNullOrEmpty() || Retirada.Logradouro.IsNotNullOrEmpty() || Retirada.Municipio.IsNotNullOrEmpty();
        }

        private bool ShouldSerializeEntrega()
        {
            return Entrega.CNPJ.IsNotNullOrEmpty() || Entrega.CPF.IsNotNullOrEmpty() || Entrega.Logradouro.IsNotNullOrEmpty();
        }

        private bool ShouldSerializeInformacaoAdicional()
        {
            return InformacaoAdicional.InformacaoComplementar.IsNotNullOrEmpty() ||
                   InformacaoAdicional.InformacaoFisco.IsNotNullOrEmpty() ||
                   InformacaoAdicional.ObsCont.Any() ||
                   InformacaoAdicional.ObsFisco.Any() ||
                   InformacaoAdicional.ProcRef.Any();
        }

        private bool ShouldSerializeExporta()
        {
            return Exporta.UFSaidaPais.IsNotNullOrEmpty() || Exporta.XLocExporta.IsNotNullOrEmpty();
        }

        private bool ShouldSerializeCompra()
        {
            return Compra.XNEmp.IsNotNullOrEmpty() || Compra.XPed.IsNotNullOrEmpty() || Compra.XCont.IsNotNullOrEmpty();
        }

        private bool ShouldSerializeCana()
        {
            return Cana.FornecedimentoDiario.Any() || Cana.Safra.IsNotNullOrEmpty() || Cana.MesAnoRerefencia.IsNotNullOrEmpty();
        }

        private bool ShouldSerializeIntermediador()
        {
            return Intermediador.Cnpj.IsNotNullOrEmpty() || Intermediador.IdCadIntTran.IsNotNullOrEmpty();
        }

        private bool ShouldSerializeResponsavelTecnico()
        {
            return ResponsavelTecnico.Cnpj.IsNotNullOrEmpty() || ResponsavelTecnico.XContato.IsNotNullOrEmpty() || ResponsavelTecnico.Email.IsNotNullOrEmpty() || ResponsavelTecnico.Fone.IsNotNullOrEmpty();
        }

        protected override void OnParentChanged()
        {
            _detalhe.Parent = Parent;
        }

        #endregion
    }
}