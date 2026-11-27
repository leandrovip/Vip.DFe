using System;
using Vip.DFe.Danfe.Elementos;
using Vip.DFe.Danfe.Enum;
using Vip.DFe.Danfe.Modelo;

namespace Vip.DFe.Danfe.Blocos
{
    internal class BlocoDadosAdicionais : BlocoBase
    {
        #region Fields

        private readonly CampoMultilinha _cInfComplementares;
        private readonly CampoMultilinha _cReservadoFisco;
        private readonly FlexibleLine _Linha;

        #endregion

        #region Constructors

        public BlocoDadosAdicionais(DanfeViewModel viewModel, Estilo estilo) : base(viewModel, estilo)
        {
            _cInfComplementares = new CampoMultilinha("Informações Complementares", ViewModel.TextoAdicional(), estilo);
            _cReservadoFisco = new CampoMultilinha("Reservado ao fisco", ViewModel.TextoAdicionalFisco(), estilo);

            _Linha = new FlexibleLine {Height = _cInfComplementares.Height}
                .ComElemento(_cInfComplementares)
                .ComElemento(_cReservadoFisco)
                .ComLarguras(InfComplementaresLarguraPorcentagem, 0);

            MainVerticalStack.Add(_Linha);
        }

        #endregion

        #region Properties

        public const float AlturaMinima = 25;

        /// <summary>
        ///     Altura mínima no leiaute da NT 2026.010, em retrato e paisagem: o quadro acompanha o conteúdo
        ///     (o Manual do DANFE não fixa altura mínima para este quadro) e o espaço restante fica para os itens.
        /// </summary>
        public const float AlturaMinimaRtc = 12;

        public const float InfComplementaresLarguraPorcentagem = 75;

        public override float Width
        {
            get => base.Width;
            set
            {
                base.Width = value;
                // Força o ajuste da altura do InfComplementares
                if (_cInfComplementares != null && _Linha != null)
                {
                    _Linha.Width = value;
                    _Linha.Posicionar();

                    if (ViewModel.UsarLeiauteAnterior)
                    {
                        _cInfComplementares.Height = AlturaMinima;
                        _Linha.Height = _cInfComplementares.Height;
                        return;
                    }

                    // Leiaute novo: altura do conteúdo (o maior entre os dois campos), com mínimo pequeno.
                    _cInfComplementares.Height = AlturaMinimaRtc;
                    _cReservadoFisco.Height = AlturaMinimaRtc;
                    _Linha.Height = Math.Max(_cInfComplementares.Height, _cReservadoFisco.Height);
                }
            }
        }

        public override PosicaoBloco Posicao => PosicaoBloco.Base;
        public override string Cabecalho => "Dados adicionais";

        #endregion
    }
}