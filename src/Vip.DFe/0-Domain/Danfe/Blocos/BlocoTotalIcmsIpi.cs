using Vip.DFe.Danfe.Elementos;
using Vip.DFe.Danfe.Enum;
using Vip.DFe.Danfe.Modelo;
using Vip.DFe.Helpers;

namespace Vip.DFe.Danfe.Blocos
{
    /// <summary>
    ///     Quadro "Total do ICMS / IPI" do leiaute da NT 2026.010.
    ///     As linhas de FCP/DIFAL e de ICMS monofásico são facultativas (item 4.4) e só são impressas quando algum valor da
    ///     linha é maior que zero.
    /// </summary>
    internal class BlocoTotalIcmsIpi : BlocoBase
    {
        #region Constructors

        public BlocoTotalIcmsIpi(DanfeViewModel viewModel, Estilo estilo) : base(viewModel, estilo)
        {
            var m = ViewModel.CalculoImposto;

            AdicionarLinhaCampos()
                .ComCampoNumerico("Base de cálculo do ICMS", m.BaseCalculoIcms)
                .ComCampoNumerico("Valor do ICMS", m.ValorIcms)
                .ComCampoNumerico("Base de cálculo do ICMS ST", m.BaseCalculoIcmsSt)
                .ComCampoNumerico("Valor do ICMS ST", m.ValorIcmsSt)
                .ComCampoNumerico("Valor do IPI", m.ValorIpi)
                .ComLargurasIguais();

            if (AlgumMaiorQueZero(m.ValorFcp, m.ValorFcpSt, m.vICMSUFDest, m.vFCPUFDest))
                AdicionarLinhaCampos()
                    .ComCampoNumerico("Valor do FCP", m.ValorFcp ?? 0)
                    .ComCampoNumerico("Valor do FCP retido por ST", m.ValorFcpSt ?? 0)
                    .ComCampoNumerico("Valor do DIFAL na UF de destino", m.vICMSUFDest ?? 0)
                    .ComCampoNumerico("Valor do FCP na UF de destino", m.vFCPUFDest ?? 0)
                    .ComLargurasIguais();

            if (AlgumMaiorQueZero(m.BaseCalculoIcmsMono, m.ValorIcmsMono, m.BaseCalculoIcmsMonoRetencao, m.ValorIcmsMonoRetencao))
                AdicionarLinhaCampos()
                    .ComCampo("BC do ICMS monofásico", FormatarQuantidade(m.BaseCalculoIcmsMono), AlinhamentoHorizontal.Direita)
                    .ComCampoNumerico("Valor do ICMS monofásico", m.ValorIcmsMono ?? 0)
                    .ComCampo("BC do ICMS monofásico por retenção", FormatarQuantidade(m.BaseCalculoIcmsMonoRetencao), AlinhamentoHorizontal.Direita)
                    .ComCampoNumerico("Valor do ICMS monofásico por retenção", m.ValorIcmsMonoRetencao ?? 0)
                    .ComLargurasIguais();
        }

        #endregion

        #region Properties

        public override PosicaoBloco Posicao => PosicaoBloco.Topo;
        public override string Cabecalho => "Total do ICMS / IPI";

        #endregion

        #region Methods

        internal static bool AlgumMaiorQueZero(params double?[] valores)
        {
            foreach (var valor in valores)
                if (valor.GetValueOrDefault() > 0)
                    return true;

            return false;
        }

        /// <summary>
        ///     qBCMono e qBCMonoReten são quantidades (até 4 casas decimais).
        /// </summary>
        private static string FormatarQuantidade(double? valor) => (valor ?? 0).Formatar();

        #endregion
    }
}