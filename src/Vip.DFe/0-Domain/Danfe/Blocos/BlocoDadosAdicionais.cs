using Vip.DFe.Danfe.Elementos;
using Vip.DFe.Danfe.Enum;
using Vip.DFe.Danfe.Modelo;
using Vip.DFe.Graphics;

namespace Vip.DFe.Danfe.Blocos
{
    internal class BlocoDadosAdicionais : BlocoBase
    {
        #region Fields

        private readonly CampoMultilinha _cInfComplementares;
        private readonly FlexibleLine _Linha;

        #endregion

        #region Constructors

        public BlocoDadosAdicionais(DanfeViewModel viewModel, Estilo estilo) : base(viewModel, estilo)
        {
            _cInfComplementares = new CampoMultilinha("Informações Complementares", ViewModel.TextoAdicional(), estilo);
            Campo cReservadoFisco = new CampoMultilinha("Reservado ao fisco", ViewModel.TextoAdicionalFisco(), estilo);

            _Linha = new FlexibleLine {Height = _cInfComplementares.Height}
                .ComElemento(_cInfComplementares)
                .ComElemento(cReservadoFisco)
                .ComLarguras(InfComplementaresLarguraPorcentagem, 0);

            MainVerticalStack.Add(_Linha);
        }

        #endregion

        #region Properties

        public const float AlturaMinima = 25;

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
                    _cInfComplementares.Height = AlturaMinima;
                    _Linha.Height = _cInfComplementares.Height;
                }
            }
        }

        public override PosicaoBloco Posicao => PosicaoBloco.Base;
        public override string Cabecalho => "Dados adicionais";

        #endregion

        #region Methods

        public override void Draw(Gfx gfx)
        {
            base.Draw(gfx);
        }

        #endregion
    }
}