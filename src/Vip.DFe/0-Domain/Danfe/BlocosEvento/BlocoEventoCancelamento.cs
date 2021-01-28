using Vip.DFe.Danfe.Elementos;
using Vip.DFe.Danfe.Enum;
using Vip.DFe.Danfe.Modelo;

namespace Vip.DFe.Danfe.BlocosEvento
{
    internal class BlocoEventoCancelamento : BlocoEventoBase
    {
        #region Constructors

        public BlocoEventoCancelamento(DanfeEventoViewModel viewModel, Estilo estilo) : base(viewModel, estilo)
        {
            var campoJustificativa = new CampoMultilinha("", viewModel.Justificativa, estilo, AlinhamentoHorizontal.Esquerda, true) {Height = AlturaCampo};
            var linha = new FlexibleLine {Height = AlturaCampo}
                .ComElemento(campoJustificativa)
                .ComLargurasIguais();

            MainVerticalStack.Add(linha);
        }

        #endregion

        #region Properties

        public const float AlturaCampo = DanfeConstantes.A4Altura - 99;
        public override string Cabecalho => "JUSTIFICATIVA";
        public override PosicaoBloco Posicao => PosicaoBloco.Topo;

        #endregion
    }
}