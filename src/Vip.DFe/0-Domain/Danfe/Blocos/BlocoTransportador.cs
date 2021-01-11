using Vip.DFe.Danfe.Elementos;
using Vip.DFe.Danfe.Enum;
using Vip.DFe.Danfe.Modelo;
using Vip.DFe.Helpers;

namespace Vip.DFe.Danfe.Blocos
{
    internal class BlocoTransportador : BlocoBase
    {
        #region Constructors

        public BlocoTransportador(DanfeViewModel viewModel, Estilo campoEstilo) : base(viewModel, campoEstilo)
        {
            var transportadora = viewModel.Transportadora;

            AdicionarLinhaCampos()
                .ComCampo(DanfeConstantes.RazaoSocial, transportadora.RazaoSocial)
                .ComCampo("Frete", transportadora.ModalidadeFreteString, AlinhamentoHorizontal.Centro)
                .ComCampo("Código ANTT", transportadora.CodigoAntt, AlinhamentoHorizontal.Centro)
                .ComCampo("Placa do Veículo", transportadora.Placa, AlinhamentoHorizontal.Centro)
                .ComCampo(DanfeConstantes.UF, transportadora.VeiculoUf, AlinhamentoHorizontal.Centro)
                .ComCampo(DanfeConstantes.CnpjCpf, DanfeHelper.FormatarCnpj(transportadora.CnpjCpf), AlinhamentoHorizontal.Centro)
                .ComLarguras(0, LarguraFrete, LarguraCampoCodigoAntt, LarguraCampoPlacaVeiculo, LarguraCampoUf, LarguraCampoCnpj);

            AdicionarLinhaCampos()
                .ComCampo(DanfeConstantes.Endereco, transportadora.EnderecoLogadrouro)
                .ComCampo(DanfeConstantes.Municipio, transportadora.Municipio)
                .ComCampo(DanfeConstantes.UF, transportadora.EnderecoUf, AlinhamentoHorizontal.Centro)
                .ComCampo(DanfeConstantes.InscricaoEstadual, transportadora.Ie, AlinhamentoHorizontal.Centro)
                .ComLarguras(0, LarguraCampoPlacaVeiculo + LarguraCampoCodigoAntt, LarguraCampoUf, LarguraCampoCnpj);

            var l = (LarguraCampoCodigoAntt + LarguraCampoPlacaVeiculo + LarguraCampoUf + LarguraCampoCnpj) / 3F;

            AdicionarLinhaCampos()
                .ComCampoNumerico(DanfeConstantes.Quantidade, transportadora.QuantidadeVolumes, 3)
                .ComCampo("Espécie", transportadora.Especie)
                .ComCampo("Marca", transportadora.Marca)
                .ComCampo("Numeração", transportadora.Numeracao)
                .ComCampoNumerico("Peso Bruto", transportadora.PesoBruto, 3)
                .ComCampoNumerico("Peso Líquido", transportadora.PesoLiquido, 3)
                .ComLarguras(20F / 200F * 100, 0, 0, l, l, l);
        }

        #endregion

        #region Properties

        public const float LarguraCampoPlacaVeiculo = 22F * Proporcao;
        public const float LarguraCampoCodigoAntt = 30F * Proporcao;
        public const float LarguraCampoCnpj = 31F * Proporcao;
        public const float LarguraCampoUf = 7F * Proporcao;
        public const float LarguraFrete = 34F * Proporcao;

        public override PosicaoBloco Posicao => PosicaoBloco.Topo;
        public override string Cabecalho => "Transportador / Volumes Transportados";

        #endregion
    }
}