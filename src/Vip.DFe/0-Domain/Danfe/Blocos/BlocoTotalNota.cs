using System.Collections.Generic;
using Vip.DFe.Danfe.Elementos;
using Vip.DFe.Danfe.Enum;
using Vip.DFe.Danfe.Modelo;

namespace Vip.DFe.Danfe.Blocos
{
    /// <summary>
    ///     Quadro "Total dos produtos e total da nota" do leiaute da NT 2026.010.
    /// </summary>
    internal class BlocoTotalNota : BlocoBase
    {
        #region Constructors

        public BlocoTotalNota(DanfeViewModel viewModel, Estilo estilo) : base(viewModel, estilo)
        {
            var m = ViewModel.CalculoImposto;
            var paisagem = ViewModel.Orientacao == Orientacao.Paisagem;

            var linha = AdicionarLinhaCampos()
                .ComCampoNumerico("Valor total dos produtos", m.ValorTotalProdutos)
                .ComCampoNumerico("Valor do frete", m.ValorFrete)
                .ComCampoNumerico("Valor do seguro", m.ValorSeguro)
                .ComCampoNumerico("Desconto", m.Desconto)
                .ComCampoNumerico(paisagem ? "Outras despesas acessórias" : "Outras despesas", m.OutrasDespesas);

            // No paisagem a altura é o recurso escasso: o total da nota fica na mesma linha (a NT permite mudar a disposição).
            if (paisagem)
            {
                AdicionarValoresComplementares(linha, null);
                linha.ComCampoNumerico("Valor total da nota", m.ValorTotalNota).ComLargurasIguais();
                return;
            }

            linha.ComLargurasIguais();

            var larguras = new List<float>();
            var linha2 = AdicionarLinhaCampos();
            AdicionarValoresComplementares(linha2, larguras);
            linha2.ComCampoNumerico("Valor total da nota", m.ValorTotalNota);
            larguras.Add(0);
            linha2.ComLarguras(larguras.ToArray());
        }

        /// <summary>
        ///     Valores que compõem o total da nota e não têm quadro próprio no novo leiaute: só aparecem quando informados.
        /// </summary>
        private void AdicionarValoresComplementares(LinhaCampos linha, List<float> larguras)
        {
            var m = ViewModel.CalculoImposto;

            if (m.ValorII > 0)
            {
                linha.ComCampoNumerico("Valor do imposto de importação", m.ValorII);
                larguras?.Add(20);
            }

            if (ViewModel.ExibirPisConfins && m.ValorPis > 0)
            {
                linha.ComCampoNumerico("Valor do PIS", m.ValorPis);
                larguras?.Add(20);
            }

            if (ViewModel.ExibirPisConfins && m.ValorCofins > 0)
            {
                linha.ComCampoNumerico("Valor da COFINS", m.ValorCofins);
                larguras?.Add(20);
            }
        }

        #endregion

        #region Properties

        public override PosicaoBloco Posicao => PosicaoBloco.Topo;
        public override string Cabecalho => "Total dos produtos e total da nota";

        #endregion
    }
}