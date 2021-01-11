using System;
using System.Collections.Generic;
using System.Linq;
using Vip.DFe.Attributes;
using Vip.DFe.Graphics;

namespace Vip.DFe.Danfe.Elementos
{
    /// <summary>
    ///     Define uma pilha vertical de elementos, de forma que todos eles fiquem com a mesma largura.
    /// </summary>
    [AlturaFixa]
    internal class VerticalStack : DrawableBase
    {
        #region Constructors

        public VerticalStack()
        {
            Drawables = new List<DrawableBase>();
        }

        public VerticalStack(float width) : this()
        {
            Width = width;
        }

        #endregion

        #region Properties

        public List<DrawableBase> Drawables { get; private set; }

        /// <summary>
        ///     Soma das alturas de todos os elementos.
        /// </summary>
        public override float Height
        {
            get => Drawables.Sum(x => x.Height);
            set => throw new NotSupportedException();
        }

        #endregion

        #region Methods

        public void Add(params DrawableBase[] db)
        {
            foreach (var item in db)
            {
                if (item == this) throw new InvalidOperationException();

                Drawables.Add(item);
            }
        }

        public override void Draw(Gfx gfx)
        {
            base.Draw(gfx);

            float x = X, y = Y;

            for (int i = 0; i < Drawables.Count; i++)
            {
                var db = Drawables[i];
                db.Width = Width;
                db.SetPosition(x, y);
                db.Draw(gfx);
                y += db.Height;
            }
        }

        /// <summary>
        ///     Somente é possível mudar a largura.
        /// </summary>
        public override void SetSize(float w, float h) => throw new NotSupportedException();

        #endregion
    }
}