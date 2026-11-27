using System;
using Vip.DFe.Danfe.Elementos;
using Vip.DFe.Danfe.Enum;
using Vip.DFe.Danfe.Modelo;
using Vip.DFe.Graphics;

namespace Vip.DFe.Danfe.Blocos
{
    /// <summary>
    ///     Define um bloco básico do DANFE.
    /// </summary>
    internal abstract class BlocoBase : ElementoBase
    {
        #region Constructors

        protected BlocoBase(DanfeViewModel viewModel, Estilo estilo) : base(estilo)
        {
            MainVerticalStack = new VerticalStack();
            ViewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));

            if (!string.IsNullOrWhiteSpace(Cabecalho))
            {
                // Leiaute da NT 2026.010: respiro entre o fim do bloco anterior e o título deste.
                if (!ViewModel.UsarLeiauteAnterior) MainVerticalStack.Add(new ElementoVazio {Height = EspacoAntesCabecalho});
                MainVerticalStack.Add(new CabecalhoBloco(estilo, Cabecalho, !ViewModel.UsarLeiauteAnterior));
            }
        }

        #endregion

        #region Properties

        public DanfeViewModel ViewModel { get; private set; }

        public abstract PosicaoBloco Posicao { get; }

        /// <summary>
        ///     Constante de proporção dos campos para o formato retrato A4, porcentagem dividida pela largura desenhável.
        /// </summary>
        public const float Proporcao = 100F / 200F;

        /// <summary>
        ///     Espaço, em mm, antes do título de cada bloco no leiaute da NT 2026.010.
        /// </summary>
        public const float EspacoAntesCabecalho = 1.5F;

        /// <summary>
        ///     Pilha principal.
        /// </summary>
        public VerticalStack MainVerticalStack { get; private set; }

        /// <summary>
        ///     Quando verdadeiro, o bloco é mostrado apenas na primeira página, caso contário é mostrado em todas elas.
        /// </summary>
        public virtual bool VisivelSomentePrimeiraPagina => true;

        public virtual string Cabecalho => null;

        public override float Height
        {
            get => MainVerticalStack.Height;
            set => throw new NotSupportedException();
        }

        public override bool PossuiContono => false;

        #endregion

        #region Methods

        public LinhaCampos AdicionarLinhaCampos()
        {
            var l = new LinhaCampos(Estilo, Width)
            {
                Width = Width,
                Height = DanfeConstantes.CampoAltura
            };
            MainVerticalStack.Add(l);
            return l;
        }

        public override void Draw(Gfx gfx)
        {
            base.Draw(gfx);
            MainVerticalStack.SetPosition(X, Y);
            MainVerticalStack.Width = Width;
            MainVerticalStack.Draw(gfx);
        }

        #endregion
    }
}