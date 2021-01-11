using System;
using Vip.DFe.Danfe.Enum;
using Vip.DFe.Graphics;

namespace Vip.DFe.Danfe.Elementos
{
    /// <summary>
    ///     Campo multilinha.
    /// </summary>
    internal class CampoMultilinha : Campo
    {
        #region Fields

        private readonly TextBlock _tbConteudo;

        #endregion

        #region Constructors

        public CampoMultilinha(string cabecalho, string conteudo, Estilo estilo, AlinhamentoHorizontal alinhamentoHorizontalConteudo = AlinhamentoHorizontal.Esquerda)
            : base(cabecalho, conteudo, estilo, alinhamentoHorizontalConteudo)
        {
            _tbConteudo = new TextBlock(conteudo, estilo.FonteCampoConteudo);
            IsConteudoNegrito = false;
        }

        #endregion

        #region Properties

        public override float Height
        {
            get => Math.Max(_tbConteudo.Height + Estilo.FonteCampoCabecalho.AlturaLinha + Estilo.PaddingSuperior + 2 * Estilo.PaddingInferior, base.Height);
            set => base.Height = value;
        }

        public override string Conteudo
        {
            get => base.Conteudo;
            set => base.Conteudo = value;
        }

        public override float Width
        {
            get => base.Width;
            set
            {
                base.Width = value;
                _tbConteudo.Width = value - 2 * Estilo.PaddingHorizontal;
            }
        }

        #endregion

        #region Methods

        protected override void DesenharConteudo(Gfx gfx)
        {
            if (!string.IsNullOrWhiteSpace(Conteudo))
            {
                _tbConteudo.SetPosition(RetanguloDesenhvael.X, RetanguloDesenhvael.Y + Estilo.FonteCampoCabecalho.AlturaLinha + Estilo.PaddingInferior);
                _tbConteudo.Draw(gfx);
            }
        }

        #endregion
    }
}