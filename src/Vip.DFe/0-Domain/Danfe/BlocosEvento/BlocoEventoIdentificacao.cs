﻿using Vip.DFe.Danfe.Elementos;
using Vip.DFe.Danfe.Enum;
using Vip.DFe.Danfe.Modelo;
using Vip.DFe.Helpers;

namespace Vip.DFe.Danfe.BlocosEvento
{
    internal class BlocoEventoIdentificacao : BlocoEventoBase
    {
        #region Constructors

        public BlocoEventoIdentificacao(DanfeEventoViewModel viewModel, Estilo estilo) : base(viewModel, estilo)
        {
            #region Propriedades

            var chaveAcesso = DanfeHelper.FormatarChaveAcesso(viewModel.ChaveAcesso);
            var modelo = viewModel.ChaveAcesso.Substring(20, 2);
            var serie = viewModel.ChaveAcesso.Substring(22, 3);
            var numero = DanfeHelper.FormatarNumeroNF(viewModel.ChaveAcesso.Substring(25, 9));
            var ano = viewModel.ChaveAcesso.Substring(2, 2);
            var mes = viewModel.ChaveAcesso.Substring(4, 2);

            #endregion

            var codigoBarras = new Barcode128C(viewModel.ChaveAcesso, Estilo) {Height = AlturaCodigo};

            var coluna = new VerticalStack();
            var linha1 = new LinhaCampos(estilo) {Height = AlturaLinhaCampo}
                .ComCampo("MODELO", modelo)
                .ComCampo("SÉRIE", serie)
                .ComCampo("NÚMERO", numero)
                .ComCampo("MÊS / ANO DA EMISSÃO", $"{mes} / {ano}")
                .ComLargurasIguais();

            var linha2 = new Campo("CHAVE DE ACESSO", chaveAcesso, estilo) {Height = AlturaLinhaCampo};

            coluna.Add(linha1, linha2);

            var fl = new FlexibleLine {Height = codigoBarras.Height}
                .ComElemento(coluna)
                .ComElemento(codigoBarras)
                .ComLargurasIguais();

            MainVerticalStack.Add(fl);
        }

        #endregion

        #region Properties

        public const float AlturaCodigo = 15;
        public const float AlturaLinhaCampo = 7.5f;

        public override string Cabecalho => "IDENTIFICAÇÃO";
        public override PosicaoBloco Posicao => PosicaoBloco.Topo;

        #endregion
    }
}