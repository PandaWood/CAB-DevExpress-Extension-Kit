using System.Drawing;
using System.Windows.Forms;
using BankShell.Properties;
using DevExpress.XtraBars;
using DevExpress.XtraTabbedMdi;

namespace BankShell
{
    public class WindowMenu : BarSubItem
    {
    	private Bar bar;
        private readonly XtraTabbedMdiManager mdiManager;
        private readonly Form shell;
        private WindowMenuHelper menuHelper;

    	public WindowMenu(Bar bar, XtraTabbedMdiManager mdiManager, Form shell)
        {
            this.bar = bar;
            this.mdiManager = mdiManager;
            this.shell = shell;
            Manager = bar.Manager;
            menuHelper = new WindowMenuHelper(mdiManager, shell);

            Caption = "Window";
            AddAllMenuItems();
        }

        private void AddAllMenuItems()
        {
            BarButtonItem bbiTabbed = AddBarItem("&Use Tabbed MDI", null, null, menuHelper.MdiChangeMode);
            bbiTabbed.ButtonStyle = BarButtonStyle.Check;
            bbiTabbed.Down = true;

            AddBarItem("&Cascade", Resources.WindowCascade16, Resources.WindowCascade32, menuHelper.MdiLayoutCascade);
            AddBarItem("Tile &Horizontally", Resources.WindowsTileHoriz16, Resources.WindowsTileHoriz32, menuHelper.MdiLayoutTileHorizontal);
            AddBarItem("Tile &Vertically", Resources.WindowsTileVert16, Resources.WindowsTileVert32, menuHelper.MdiLayoutTileVertical);

            BarSubItem bsiWindows = new BarSubItem(Manager, "&Windows");
            AddItem(bsiWindows);
            bsiWindows.AddItem(new BarMdiChildrenListItem());
        }

        private BarButtonItem AddBarItem(string caption, Image glyph, Image largeGlyph,
            ItemClickEventHandler itemClickEventHandler)
        {
            BarButtonItem item = new BarButtonItem(Manager, caption);
            item.Glyph = glyph;
            item.LargeGlyph = largeGlyph;
            item.ItemClick += itemClickEventHandler;
            AddItem(item);
            return item;
        }
    }
}