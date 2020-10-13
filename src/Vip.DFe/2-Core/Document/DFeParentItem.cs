using Vip.DFe.Attributes;

namespace Vip.DFe.Document
{
    public abstract class DFeParentItem<TTipo, TParent> : GenericClone<DFeParentItem<TTipo, TParent>>
        where TParent : class
        where TTipo : class
    {
        #region Fields

        protected TParent parent;

        #endregion Fields

        #region Properties

        [DFeIgnore]
        public TParent Parent
        {
            get => parent;
            set
            {
                if (value == parent) return;

                parent = value;
                OnParentChanged();
            }
        }

        #endregion Properties

        #region Methods

        protected virtual void OnParentChanged()
        {
            //
        }

        #endregion Methods
    }
}