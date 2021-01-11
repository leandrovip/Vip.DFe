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

            if (!string.IsNullOrWhiteSpace(Cabecalho)) MainVerticalStack.Add(new CabecalhoBloco(estilo, Cabecalho));
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