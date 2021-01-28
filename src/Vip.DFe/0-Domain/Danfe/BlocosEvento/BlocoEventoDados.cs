using Vip.DFe.Danfe.Elementos;
using Vip.DFe.Danfe.Enum;
using Vip.DFe.Danfe.Modelo;
using Vip.DFe.Extensions;
using Vip.DFe.Helpers;

namespace Vip.DFe.Danfe.BlocosEvento
{
    internal class BlocoEventoDados : BlocoEventoBase
    {
        #region Constructors

        public BlocoEventoDados(DanfeEventoViewModel viewModel, Estilo estilo) : base(viewModel, estilo)
        {
            var ambiente = viewModel.TipoAmbiente == 1 ? "PRODUÇÃO" : "HOMOLOGAÇÃO - SEM VALOR FISCAL";

            AdicionarLinhaCampos()
                .ComCampo("ORGÃO", viewModel.Orgao.ToString())
                .ComCampo("AMBIENTE", ambiente, AlinhamentoHorizontal.Centro)
                .ComCampo("DATA / HORA DO EVENTO", ViewModel.DataHoraEvento.FormatarDataHora(), AlinhamentoHorizontal.Centro)
                .ComLarguras(30F * Proporcao, 0, 45F * Proporcao);

            AdicionarLinhaCampos()
                .ComCampo("EVENTO", viewModel.TipoEvento.GetDFeValue())
                .ComCampo("DESCRIÇÃO DO EVENTO", viewModel.DescricaoEvento)
                .ComCampo("SEQUÊNCIA DO EVENTO", viewModel.SequenciaEvento.ToString(), AlinhamentoHorizontal.Centro)
                .ComLarguras(30F * Proporcao, 0, 45F * Proporcao);

            AdicionarLinhaCampos()
                .ComCampo("STATUS", $"{viewModel.CodigoStatus} - {viewModel.Motivo}")
                .ComCampo("PROTOCOLO", viewModel.Protocolo)
                .ComLarguras(0, 45F * Proporcao);
        }

        #endregion

        #region Properties

        public override string Cabecalho => ViewModel.TituloEvento;
        public override PosicaoBloco Posicao => PosicaoBloco.Topo;

        #endregion
    }
}