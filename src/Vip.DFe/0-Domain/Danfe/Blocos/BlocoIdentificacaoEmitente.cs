using System.Drawing;
using org.pdfclown.documents.contents.xObjects;
using Vip.DFe.Danfe.Elementos;
using Vip.DFe.Danfe.Enum;
using Vip.DFe.Danfe.Modelo;
using Vip.DFe.Helpers;

namespace Vip.DFe.Danfe.Blocos
{
    internal class BlocoIdentificacaoEmitente : BlocoBase
    {
        #region Fields

        private readonly NumeroNfSerie2 ifdNfe;
        private readonly IdentificacaoEmitente idEmitente;

        #endregion

        #region Constructors

        public BlocoIdentificacaoEmitente(DanfeViewModel viewModel, Estilo estilo) : base(viewModel, estilo)
        {
            var textoConsulta = new TextoSimples(Estilo, DanfeConstantes.TextoConsulta)
            {
                Height = 8,
                AlinhamentoHorizontal = AlinhamentoHorizontal.Centro,
                AlinhamentoVertical = AlinhamentoVertical.Centro,
                TamanhoFonte = 9
            };

            var campoChaveAcesso = new Campo("Chave de Acesso", DanfeHelper.FormatarChaveAcesso(ViewModel.ChaveAcesso), estilo, AlinhamentoHorizontal.Centro) {Height = DanfeConstantes.CampoAltura};
            var codigoBarras = new Barcode128C(viewModel.ChaveAcesso, Estilo) {Height = AlturaLinha1 - textoConsulta.Height - campoChaveAcesso.Height};

            var coluna3 = new VerticalStack();
            coluna3.Add(codigoBarras, campoChaveAcesso, textoConsulta);

            ifdNfe = new NumeroNfSerie2(estilo, ViewModel);
            idEmitente = new IdentificacaoEmitente(Estilo, ViewModel);

            FlexibleLine fl = new FlexibleLine {Height = coluna3.Height}
                .ComElemento(idEmitente)
                .ComElemento(ifdNfe)
                .ComElemento(coluna3)
                .ComLarguras(0, 15, 46.5F);

            MainVerticalStack.Add(fl);

            AdicionarLinhaCampos()
                .ComCampo("Natureza da operação", ViewModel.NaturezaOperacao)
                .ComCampo("Protocolo de autorização", ViewModel.ProtocoloAutorizacao, AlinhamentoHorizontal.Centro)
                .ComLarguras(0, 46.5F);

            AdicionarLinhaCampos()
                .ComCampo("Inscrição Estadual", ViewModel.Emitente.Ie, AlinhamentoHorizontal.Centro)
                .ComCampo("Inscrição Estadual do Subst. Tributário", ViewModel.Emitente.IeSt, AlinhamentoHorizontal.Centro)
                .ComCampo("Cnpj", DanfeHelper.FormatarCnpj(ViewModel.Emitente.CnpjCpf), AlinhamentoHorizontal.Centro)
                .ComLargurasIguais();
        }

        #endregion

        #region Properties

        public const float LarguraCampoChaveNFe = 93F;
        public const float AlturaLinha1 = 30;
        public RectangleF RetanguloNumeroFolhas => ifdNfe.RetanguloNumeroFolhas;
        public override PosicaoBloco Posicao => PosicaoBloco.Topo;
        public override bool VisivelSomentePrimeiraPagina => false;

        public XObject Logo
        {
            get => idEmitente.Logo;
            set => idEmitente.Logo = value;
        }

        #endregion
    }
}