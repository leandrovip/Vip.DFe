using Vip.DFe.Danfe.Elementos;
using Vip.DFe.Danfe.Enum;
using Vip.DFe.Danfe.Modelo;
using Vip.DFe.Helpers;

namespace Vip.DFe.Danfe.Blocos
{
    internal class BlocoDestinatarioRemetente : BlocoBase
    {
        #region Constructors

        public BlocoDestinatarioRemetente(DanfeViewModel viewModel, Estilo estilo) : base(viewModel, estilo)
        {
            var destinatario = viewModel.Destinatario;

            AdicionarLinhaCampos()
                .ComCampo(DanfeConstantes.RazaoSocial, destinatario.RazaoSocial)
                .ComCampo(DanfeConstantes.CnpjCpf, DanfeHelper.FormatarCpfCnpj(destinatario.CnpjCpf), AlinhamentoHorizontal.Centro)
                .ComCampo("Data de Emissão", viewModel.DataHoraEmissao.Formatar(), AlinhamentoHorizontal.Centro)
                .ComLarguras(0, 45F * Proporcao, 30F * Proporcao);

            AdicionarLinhaCampos()
                .ComCampo(DanfeConstantes.Endereco, destinatario.EnderecoLinha1)
                .ComCampo(DanfeConstantes.BairroDistrito, destinatario.EnderecoBairro)
                .ComCampo(DanfeConstantes.Cep, DanfeHelper.FormatarCEP(destinatario.EnderecoCep), AlinhamentoHorizontal.Centro)
                .ComCampo("Data Entrada / Saída", ViewModel.DataSaidaEntrada.Formatar(), AlinhamentoHorizontal.Centro)
                .ComLarguras(0, 45F * Proporcao, 25F * Proporcao, 30F * Proporcao);

            AdicionarLinhaCampos()
                .ComCampo(DanfeConstantes.Municipio, destinatario.Municipio)
                .ComCampo(DanfeConstantes.FoneFax, DanfeHelper.FormatarTelefone(destinatario.Telefone), AlinhamentoHorizontal.Centro)
                .ComCampo(DanfeConstantes.UF, destinatario.EnderecoUf, AlinhamentoHorizontal.Centro)
                .ComCampo(DanfeConstantes.InscricaoEstadual, destinatario.Ie, AlinhamentoHorizontal.Centro)
                .ComCampo("Hora Entrada / Saída", ViewModel.HoraSaidaEntrada.Formatar(), AlinhamentoHorizontal.Centro)
                .ComLarguras(0, 35F * Proporcao, 7F * Proporcao, 40F * Proporcao, 30F * Proporcao);
        }

        #endregion

        #region Properties

        public override string Cabecalho => "Destinatário / Remetente";
        public override PosicaoBloco Posicao => PosicaoBloco.Topo;

        #endregion
    }
}