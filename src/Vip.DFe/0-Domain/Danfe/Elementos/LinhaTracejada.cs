using System;
using System.Drawing;
using org.pdfclown.documents.contents;
using Vip.DFe.Extensions;
using Vip.DFe.Graphics;

namespace Vip.DFe.Danfe.Elementos
{
    internal class LinhaTracejada : DrawableBase
    {
        #region Constructors

        public LinhaTracejada(float margin)
        {
            Margin = margin;
        }

        #endregion

        #region Properties

        public float Margin { get; set; }
        public double[] DashPattern { get; set; }

        public override float Height
        {
            get => 2 * Margin;
            set => throw new NotSupportedException();
        }

        #endregion

        #region Methods

        public override void Draw(Gfx gfx)
        {
            base.Draw(gfx);

            gfx.PrimitiveComposer.BeginLocalState();
            gfx.PrimitiveComposer.SetLineDash(new LineDash(new double[] {3, 2}));
            gfx.PrimitiveComposer.DrawLine(new PointF(BoundingBox.Left, Y + Margin).ToPointMeasure(), new PointF(BoundingBox.Right, Y + Margin).ToPointMeasure());
            gfx.PrimitiveComposer.Stroke();
            gfx.PrimitiveComposer.End();
        }

        #endregion
    }
}