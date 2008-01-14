using System;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraTabbedMdi;

namespace BankShell
{
    public class WindowMenu : BarSubItem
    {
    	private Bar bar;
        private readonly XtraTabbedMdiManager mdiManager;
        private readonly Form shell;

        private MdiMode mdiMode = MdiMode.Tabbed;

    	public WindowMenu(Bar bar, XtraTabbedMdiManager mdiManager, Form shell)
        {
            this.bar = bar;
            this.mdiManager = mdiManager;
            this.shell = shell;
            Manager = bar.Manager;

            AddAllMenuItems();
        }

    	private void AddAllMenuItems()
        {
			Caption = "Window";

            BarButtonItem bbiTabbed = AddBarItem("&Use Tabbed MDI", MdiChangeMode);
            bbiTabbed.ButtonStyle = BarButtonStyle.Check;
            bbiTabbed.Down = true;

            AddBarItem("&Cascade", MdiLayoutCascade);
            AddBarItem("Tile &Horizontally", MdiLayoutTileHorizontal);
            AddBarItem("Tile &Vertically", MdiLayoutTileVertical);

            BarSubItem bsiWindows = new BarSubItem(Manager, "&Windows");
            AddItem(bsiWindows);
            bsiWindows.AddItem(new BarMdiChildrenListItem());
        }

		private BarButtonItem AddBarItem(string caption, ItemClickEventHandler itemClickEventHandler)
        {
            BarButtonItem item = new BarButtonItem(Manager, caption);
            item.ItemClick += itemClickEventHandler;
            AddItem(item);
            return item;
        }

    	public enum MdiMode
        {
            Tabbed,
            Windowed
        }

        public void MdiChangeMode(object sender, EventArgs e)
        {
            mdiMode = mdiMode == MdiMode.Tabbed ? MdiMode.Windowed : MdiMode.Tabbed; // Toggle
            SetMdiMode(mdiMode);
        }

        private void SetMdiMode(MdiMode mode)
        {
            mdiManager.MdiParent = mode == MdiMode.Tabbed ? shell : null;
        }

        public void MdiLayoutCascade(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        public void MdiLayoutTileHorizontal(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        public void MdiLayoutTileVertical(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void LayoutMdi(MdiLayout layout)
        {
            SetMdiMode(MdiMode.Windowed);
            mdiManager.MdiParent = null;
            shell.LayoutMdi(layout);
        }
    }
}