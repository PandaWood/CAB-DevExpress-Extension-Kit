using DevExpress.CompositeUI.Commands;
using DevExpress.CompositeUI.UIElements;
using DevExpress.XtraBars;
using DevExpress.XtraNavBar;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.Commands;
using Microsoft.Practices.CompositeUI.UIElements;
using Microsoft.Practices.CompositeUI.WinForms;
using DevExpress.Utils.Menu;

namespace DevExpress.CompositeUI
{
    /// <summary>
    /// Defines an abstract cab application which shows a shell based on a Form that uses DevExpress WinForms components.
    /// </summary>
    /// <typeparam name="TWorkItem">The type of the root application work item.</typeparam>
    /// <typeparam name="TShell">The type of the form for the shell to use. Should be an XtraForm for skin support.</typeparam>
    public abstract class XtraFormApplicationBase<TWorkItem, TShell> : WindowsFormsApplication<TWorkItem, TShell>
        where TWorkItem : WorkItem, new()
    {
        #region Constructor

        /// <summary>
        /// Initializes a new <see cref="XtraFormApplicationBase{TWorkItem,TShell}"/>
        /// </summary>
        protected XtraFormApplicationBase() : base()
        {
        }

        #endregion

        #region Overrides

        /// <summary>
        /// See <see cref="CabShellApplication{T,S}.AfterShellCreated"/>
        /// </summary>
        protected override void AfterShellCreated()
        {
            base.AfterShellCreated();
            RegisterCommandAdapters();
            RegisterUIElementAdapterFactories();
        }

        private void RegisterCommandAdapters()
        {
            ICommandAdapterMapService mapService = RootWorkItem.Services.Get<ICommandAdapterMapService>();
            mapService.Register(typeof (BarItem), typeof (BarItemCommandAdapter));
            mapService.Register(typeof (NavBarItem), typeof (NavBarItemCommandAdapter));
            mapService.Register(typeof(DXMenuItem), typeof(MenuItemCommandAdapter));
        }

        private void RegisterUIElementAdapterFactories()
        {
            IUIElementAdapterFactoryCatalog catalog = RootWorkItem.Services.Get<IUIElementAdapterFactoryCatalog>();
            catalog.RegisterFactory(new XtraNavBarUIAdapterFactory());
            catalog.RegisterFactory(new XtraBarUIAdapterFactory());
            catalog.RegisterFactory(new XtraRibbonBarUIAdapterFactory());
            catalog.RegisterFactory(new NavigatorCustomButtonUIAdapterFactory());
            catalog.RegisterFactory(new EditorButtonCollectionUIAdapterFactory());
        }

        #endregion
    }
}