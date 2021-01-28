using Vip.DFe.Danfe.Elementos;
using Vip.DFe.Danfe.Enum;
using Vip.DFe.Danfe.Modelo;

namespace Vip.DFe.Danfe.BlocosEvento
{
    internal class BlocoEventoCartaCorrecao : BlocoEventoBase
    {
        #region Constructors

        public BlocoEventoCartaCorrecao(DanfeEventoViewModel viewModel, Estilo estilo) : base(viewModel, estilo)
        {
            var correcao = new CampoMultilinha("", viewModel.Correcao, estilo, AlinhamentoHorizontal.Esquerda, true) {Height = AlturaCorrecao};
            var linha = new FlexibleLine {Height = AlturaCorrecao};
            linha.ComElemento(correcao).ComLargurasIguais();

            MainVerticalStack.Add(linha);
        }

        #endregion

        #region Properties

        public const float AlturaCorrecao = DanfeConstantes.A4Altura - 132;

        public override string Cabecalho => "CORREÇÃO A SER CONSIDERADA";
        public override PosicaoBloco Posicao => PosicaoBloco.Topo;

        #endregion
    }
}