using System;
using Vip.DFe.Danfe.Elementos;
using Vip.DFe.Danfe.Enum;
using Vip.DFe.Danfe.Modelo;
using Vip.DFe.Helpers;

namespace Vip.DFe.Danfe.Blocos
{
    internal abstract class BlocoLocalEntregaRetirada : BlocoBase
    {
        #region Constructors

        protected BlocoLocalEntregaRetirada(DanfeViewModel viewModel, Estilo estilo, LocalEntregaRetiradaViewModel localModel) : base(viewModel, estilo)
        {
            Model = localModel ?? throw new ArgumentNullException(nameof(localModel));

            AdicionarLinhaCampos()
                .ComCampo(DanfeConstantes.NomeRazaoSocial, Model.NomeRazaoSocial)
                .ComCampo(DanfeConstantes.CnpjCpf, DanfeHelper.FormatarCpfCnpj(Model.CnpjCpf), AlinhamentoHorizontal.Centro)
                .ComCampo(DanfeConstantes.InscricaoEstadual, Model.InscricaoEstadual, AlinhamentoHorizontal.Centro)
                .ComLarguras(0, 45F * Proporcao, 30F * Proporcao);

            AdicionarLinhaCampos()
                .ComCampo(DanfeConstantes.Endereco, Model.Endereco)
                .ComCampo(DanfeConstantes.BairroDistrito, Model.Bairro)
                .ComCampo(DanfeConstantes.Cep, DanfeHelper.FormatarCEP(Model.Cep), AlinhamentoHorizontal.Centro)
                .ComLarguras(0, 45F * Proporcao, 30F * Proporcao);

            AdicionarLinhaCampos()
                .ComCampo(DanfeConstantes.Municipio, Model.Municipio)
                .ComCampo(DanfeConstantes.UF, Model.Uf, AlinhamentoHorizontal.Centro)
                .ComCampo(DanfeConstantes.FoneFax, DanfeHelper.FormatarTelefone(Model.Telefone), AlinhamentoHorizontal.Centro)
                .ComLarguras(0, 7F * Proporcao, 30F * Proporcao);
        }

        #endregion

        #region Properties

        public LocalEntregaRetiradaViewModel Model { get; private set; }
        public override PosicaoBloco Posicao => PosicaoBloco.Topo;

        #endregion
    }
}