using Vip.DFe.Danfe.Elementos;
using Vip.DFe.Danfe.Enum;
using Vip.DFe.Danfe.Modelo;

namespace Vip.DFe.Danfe.Blocos
{
    /// <summary>
    ///     Quadro "Total do IBS / CBS / IS" do leiaute da NT 2026.010 (item 4.1).
    ///     A primeira linha é sempre impressa; a linha da monofasia é facultativa e só aparece quando algum valor é maior que
    ///     zero.
    /// </summary>
    internal class BlocoTotalIbsCbsIs : BlocoBase
    {
        #region Constructors

        public BlocoTotalIbsCbsIs(DanfeViewModel viewModel, Estilo estilo) : base(viewModel, estilo)
        {
            var m = ViewModel.CalculoImposto;

            AdicionarLinhaCampos()
                .ComCampoNumerico("Valor da CBS", m.ValorCbs ?? 0)
                .ComCampoNumerico("Valor do IBS UF", m.ValorIbsUf ?? 0)
                .ComCampoNumerico("Valor do IBS Município", m.ValorIbsMunicipio ?? 0)
                .ComCampoNumerico("Valor do Imposto Seletivo", m.ValorIs ?? 0)
                .ComLargurasIguais();

            if (BlocoTotalIcmsIpi.AlgumMaiorQueZero(m.ValorIbsMono, m.ValorCbsMono, m.ValorIbsMonoRetencao, m.ValorCbsMonoRetencao))
                AdicionarLinhaCampos()
                    .ComCampoNumerico("Valor do IBS monofásico", m.ValorIbsMono ?? 0)
                    .ComCampoNumerico("Valor da CBS monofásica", m.ValorCbsMono ?? 0)
                    .ComCampoNumerico("Valor do IBS monofásico por retenção", m.ValorIbsMonoRetencao ?? 0)
                    .ComCampoNumerico("Valor da CBS monofásica por retenção", m.ValorCbsMonoRetencao ?? 0)
                    .ComLargurasIguais();
        }

        #endregion

        #region Properties

        public override PosicaoBloco Posicao => PosicaoBloco.Topo;
        public override string Cabecalho => "Total do IBS / CBS / IS";

        #endregion
    }
}