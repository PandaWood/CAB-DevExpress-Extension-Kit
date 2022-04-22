using System.Windows.Forms;
using CABDevExpress.Commands;
using CABDevExpress.UIElements;
using DevExpress.Utils.Menu;
using DevExpress.XtraBars;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraNavBar;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.Commands;
using Microsoft.Practices.CompositeUI.UIElements;
using Microsoft.Practices.CompositeUI.WPF;

namespace CABDevExpress
{
    /// <summary>
    /// Defines an abstract CAB application which shows a shell based on a Form that uses DevExpress WinForms components.
    /// </summary>
    /// <typeparam name="TWorkItem">The type of the root application work item.</typeparam>
    /// <typeparam name="TShell">The type of the form for the shell to use. Should be an XtraForm for skin support.</typeparam>
    public abstract class XtraWPFFormApplicationBase<TWorkItem, TShell> : WPFFormShellApplication<TWorkItem, TShell>
        where TWorkItem : WorkItem, new()
        where TShell : Form
    {
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
            var mapService = RootWorkItem.Services.Get<ICommandAdapterMapService>();
            mapService.Register(typeof(BarItem), typeof(BarItemCommandAdapter));
            mapService.Register(typeof(NavBarItem), typeof(NavBarItemCommandAdapter));
            mapService.Register(typeof(DXMenuItem), typeof(DXMenuItemCommandAdapter));
            mapService.Register(typeof(RepositoryItemHyperLinkEdit), typeof(RepositoryItemHyperLinkEditCommandAdapter));
        }

        private void RegisterUIElementAdapterFactories()
        {
            var factoryCatalog = RootWorkItem.Services.Get<IUIElementAdapterFactoryCatalog>();
            factoryCatalog.RegisterFactory(new XtraNavBarUIAdapterFactory());
            factoryCatalog.RegisterFactory(new XtraBarUIAdapterFactory());
            //2016.10.04
            factoryCatalog.RegisterFactory(new XtraAccordionControlUIAdapterFactory());
            //2016.10.04
            factoryCatalog.RegisterFactory(new RibbonUIAdapterFactory());
            factoryCatalog.RegisterFactory(new NavigatorCustomButtonUIAdapterFactory());
            factoryCatalog.RegisterFactory(new EditorButtonCollectionUIAdapterFactory());
        }
        /// <summary>
        /// Used to start a message pump using the specified shell form.
        /// </summary>
    }
}