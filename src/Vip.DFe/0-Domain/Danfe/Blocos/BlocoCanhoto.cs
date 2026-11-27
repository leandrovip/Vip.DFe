using Vip.DFe.Danfe.Elementos;
using Vip.DFe.Danfe.Enum;
using Vip.DFe.Danfe.Modelo;
using Vip.DFe.Helpers;

namespace Vip.DFe.Danfe.Blocos
{
    internal class BlocoCanhoto : BlocoBase
    {
        #region Constructors

        public BlocoCanhoto(DanfeViewModel viewModel, Estilo estilo) : base(viewModel, estilo)
        {
            var textoRecebimento = new TextoSimples(estilo, viewModel.TextoRecebimento) {Height = TextoRecebimentoAltura, TamanhoFonte = 8};
            var nfe = new NumeroNfSerie(estilo, viewModel.NfNumero.ToString(DanfeHelper.FormatoNumeroNF), viewModel.NfSerie.ToString()) {Height = AlturaLinha2 + TextoRecebimentoAltura, Width = 30};

            var campos = new LinhaCampos(Estilo) {Height = AlturaLinha2}
                .ComCampo("Data de Recebimento", null)
                .ComCampo("Identificação e assinatura do recebedor", null)
                .ComLarguras(50, 0);

            var coluna1 = new VerticalStack();
            coluna1.Add(textoRecebimento, campos);

            if (CanhotoNaDireita(viewModel))
            {
                // Paisagem da NT 2026.010: canhoto na borda direita, girado (leitura de baixo para cima),
                // com o número da NF-e no pé da faixa e a linha de corte voltada para o corpo do DANFE.
                var linhaDireita = new FlexibleLine {Height = coluna1.Height}
                    .ComElemento(nfe)
                    .ComElemento(coluna1)
                    .ComLarguras(16, 0);

                MainVerticalStack.Add(new LinhaTracejada(2), linhaDireita);
                return;
            }

            var linha = new FlexibleLine {Height = coluna1.Height}
                .ComElemento(coluna1)
                .ComElemento(nfe)
                .ComLarguras(0, 16);

            MainVerticalStack.Add(linha, new LinhaTracejada(2));
        }

        internal static bool CanhotoNaDireita(DanfeViewModel viewModel) => viewModel.Orientacao == Orientacao.Paisagem && !viewModel.UsarLeiauteAnterior;

        #endregion

        #region Properties

        public const float TextoRecebimentoAltura = 10;
        public const float AlturaLinha2 = 9;
        public override PosicaoBloco Posicao => PosicaoBloco.Topo;

        #endregion
    }
}