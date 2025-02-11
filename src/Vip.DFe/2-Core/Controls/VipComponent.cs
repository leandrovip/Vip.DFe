using System;
using System.ComponentModel;
using Vip.DFe.Conveter;
using Vip.DFe.Extensions;

namespace Vip.DFe.Controls
{
    /// <summary>
    ///     Classe base para componentes
    /// </summary>
    [DesignerCategory("Vip.Component")]
    [DesignTimeVisible(true)]
    [TypeConverter(typeof(VipExpandableObjectConverter))]
    public abstract class VipComponent : IComponent
    {
        #region Fields

        private ISite site;

        #endregion Fields

        #region Events

        /// <summary>
        ///     Represents the method that handles the <see cref="E:System.ComponentModel.IComponent.Disposed" /> event of a
        ///     component.
        /// </summary>
        public event EventHandler Disposed;

        #endregion Events

        #region Constructor

        /// <summary>
        ///     Initializes a new instance of the <see cref="VipComponent" /> class.
        /// </summary>
        protected VipComponent()
        {
            OnInitialize();
        }

        /// <summary>
        ///     Finalizes an instance of the <see cref="VipComponent" /> class.
        /// </summary>
        ~VipComponent()
        {
            Dispose(false);
        }

        #endregion Constructor

        #region IComponent

        ISite IComponent.Site
        {
            get => site;
            set => site = value;
        }

        /// <summary>
        /// </summary>
        [Browsable(false)]
        protected virtual bool DesignMode
        {
            get
            {
                var isDesignMode = LicenseManager.UsageMode == LicenseUsageMode.Designtime;

                if (!isDesignMode) isDesignMode = site != null && site.DesignMode;

                return isDesignMode;
            }
        }

        #endregion IComponent

        #region Abstract Methods

        /// <summary>
        ///     Função executada no inicio do componente.
        /// </summary>
        protected abstract void OnInitialize();

        /// <summary>
        ///     Função executa no dispose do componente.
        /// </summary>
        protected abstract void OnDisposing();

        #endregion Abstract Methods

        #region Dispose Methods

        private void Dispose(bool disposing)
        {
            if (disposing) GC.SuppressFinalize(this);

            OnDisposing();
            Disposed.Raise(this, EventArgs.Empty);
        }

        /// <summary>
        ///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
        }

        #endregion Dispose Methods
    }
}