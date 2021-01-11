using System;
using Vip.DFe.Graphics;

namespace Vip.DFe.Danfe.Elementos
{
    /// <summary>
    ///     Elemento básico no DANFE.
    /// </summary>
    internal abstract class ElementoBase : DrawableBase
    {
        #region Constructors

        protected ElementoBase(Estilo estilo)
        {
            Estilo = estilo ?? throw new ArgumentNullException(nameof(estilo));
        }

        #endregion

        #region Properties

        public Estilo Estilo { get; protected set; }
        public virtual bool PossuiContono => true;

        #endregion

        #region Methods

        public override void Draw(Gfx gfx)
        {
            base.Draw(gfx);
            if (PossuiContono)
                gfx.StrokeRectangle(BoundingBox, 0.25f);
        }

        #endregion
    }
}