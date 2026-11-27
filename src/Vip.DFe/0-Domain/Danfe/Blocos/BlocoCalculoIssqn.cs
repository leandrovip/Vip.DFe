using Vip.DFe.Danfe.Elementos;
using Vip.DFe.Danfe.Enum;
using Vip.DFe.Danfe.Modelo;

namespace Vip.DFe.Danfe.Blocos
{
    internal class BlocoCalculoIssqn : BlocoBase
    {
        #region Constructors

        public BlocoCalculoIssqn(DanfeViewModel viewModel, Estilo estilo) : base(viewModel, estilo)
        {
            var m = viewModel.CalculoIssqn;

            AdicionarLinhaCampos()
                .ComCampo("INSCRIÇÃO MUNICIPAL", m.InscricaoMunicipal, AlinhamentoHorizontal.Centro)
                .ComCampoNumerico("VALOR TOTAL DOS SERVIÇOS", m.ValorTotalServicos)
                .ComCampoNumerico("BASE DE CÁLCULO DO ISSQN", m.BaseIssqn)
                .ComCampoNumerico("VALOR TOTAL DO ISSQN", m.ValorIssqn)
                .ComLargurasIguais();
        }

        #endregion

        #region Properties

        /// <summary>
        ///     No leiaute da NT 2026.010 o quadro fica logo após os totais (antes do transportador); no anterior, acima dos dados
        ///     adicionais.
        /// </summary>
        public override PosicaoBloco Posicao => ViewModel.UsarLeiauteAnterior ? PosicaoBloco.Base : PosicaoBloco.Topo;

        public override string Cabecalho => "CÁLCULO DO ISSQN";

        #endregion
    }
}