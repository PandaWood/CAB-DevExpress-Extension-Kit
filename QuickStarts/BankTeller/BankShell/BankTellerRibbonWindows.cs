using System.Drawing;
using System.Windows.Forms;
using BankShell.Properties;
using BankTellerModule.Constants;
using CABDevExpress.Workspaces;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraTabbedMdi;
using Microsoft.Practices.CompositeUI;

namespace BankShell
{
    internal static class BankTellerRibbonWindows
    {
        private static WindowMenuHelper windowMenuHelper = new WindowMenuHelper();

        internal static void Add(WorkItem workItem, XtraDocumentManagerWorkspace mdiManager)
        {
            RibbonPageGroup ribbonGroup = new RibbonPageGroup(ExtensionSiteNames.RibbonWindows);
            workItem.UIExtensionSites[ExtensionSiteNames.MainMenu].Add<RibbonPageGroup>(ribbonGroup);
            workItem.UIExtensionSites.RegisterSite(ExtensionSiteNames.RibbonWindows, ribbonGroup);

            windowMenuHelper.MdiWorkSpace = mdiManager;
            windowMenuHelper.WorkItem = workItem;
            //windowMenuHelper.Shell = shell;

            AddAllRibbonItems(workItem); 
        }

        private static void AddAllRibbonItems(WorkItem workItem)
        {
            AddBarItem(workItem, "&Cascade", Resources.WindowCascade16, Resources.WindowCascade32, windowMenuHelper.MdiLayoutCascade);
            AddBarItem(workItem, "Tile &Horizontally", Resources.WindowsTileHoriz16, Resources.WindowsTileHoriz32, windowMenuHelper.MdiLayoutTileHorizontal);
            AddBarItem(workItem, "Tile &Vertically", Resources.WindowsTileVert16, Resources.WindowsTileVert32, windowMenuHelper.MdiLayoutTileVertical);

            BarCheckItem bbiTabbed = AddCheckItem(workItem, "&Use Tabbed MDI", windowMenuHelper.MdiChangeMode);
            bbiTabbed.Checked = true;

            workItem.RootWorkItem.State["UseXtraTabbedView"] = true;
            BarCheckItem bbiXtraTabbedView = AddCheckItem(workItem, "&Use XtraTabbedView", windowMenuHelper.UseXtraTabbedView);
            bbiXtraTabbedView.Checked = true;

            BarSubItem bsiWindows = new BarSubItem();
            bsiWindows.Caption = "&Windows";
            AddItem(bsiWindows, workItem);
            bsiWindows.AddItem(new BarMdiChildrenListItem());
        }

        private static BarCheckItem AddCheckItem(WorkItem workItem, string caption, ItemClickEventHandler itemClickEventHandler)
        {
            BarCheckItem item = new BarCheckItem();
            item.Caption = caption;
            item.ItemClick += itemClickEventHandler;
            AddItem(item, workItem);
            return item;
        }

        private static BarButtonItem AddBarItem(WorkItem workItem, string caption, Image glyph, Image largeGlyph,
            ItemClickEventHandler itemClickEventHandler)
        {
            BarButtonItem item = new BarButtonItem();
            item.RibbonStyle = RibbonItemStyles.SmallWithText;
            item.Caption = caption;
            item.Glyph = glyph;
            item.LargeGlyph = largeGlyph;
            // here we bypass the command and event broker of the CAB and set the
            // event handler directly. We could have used events and commands but
            // this is simpler and there is no advantage to using CAB commands
            // (other than it utilizes better decoupling).
            item.ItemClick += itemClickEventHandler;
            AddItem(item, workItem);
            return item;
        }

        private static void AddItem(BarItem item, WorkItem workItem)
        {
            workItem.UIExtensionSites[ExtensionSiteNames.RibbonWindows].Add<BarItem>(item);
        }
    }
}
