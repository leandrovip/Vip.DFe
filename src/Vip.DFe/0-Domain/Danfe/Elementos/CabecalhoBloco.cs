using System;
using Vip.DFe.Danfe.Enum;
using Vip.DFe.Graphics;

namespace Vip.DFe.Danfe.Elementos
{
    /// <summary>
    ///     Cabeçalho do bloco, normalmente um texto em caixa alta.
    /// </summary>
    internal class CabecalhoBloco : ElementoBase
    {
        #region Constructors

        public CabecalhoBloco(Estilo estilo, string cabecalho) : base(estilo)
        {
            Cabecalho = cabecalho ?? throw new ArgumentNullException(cabecalho);
        }

        #endregion

        #region Properties

        public const float MargemSuperior = 0.8F;
        public string Cabecalho { get; set; }

        public override float Height
        {
            get => MargemSuperior + Estilo.FonteBlocoCabecalho.AlturaLinha;
            set => throw new NotSupportedException();
        }

        public override bool PossuiContono => false;

        #endregion

        #region Methods

        public override void Draw(Gfx gfx)
        {
            base.Draw(gfx);
            gfx.DrawString(Cabecalho.ToUpper(), BoundingBox, Estilo.FonteBlocoCabecalho,
                AlinhamentoHorizontal.Esquerda, AlinhamentoVertical.Base);
        }

        #endregion
    }
}