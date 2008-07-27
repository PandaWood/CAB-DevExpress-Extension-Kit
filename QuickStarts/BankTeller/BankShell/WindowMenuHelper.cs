using System;
using System.Windows.Forms;
using DevExpress.XtraTabbedMdi;

namespace BankShell
{
    internal class WindowMenuHelper
    {
        public enum MdiMode
        {
            Tabbed,
            Windowed
        }

        private MdiMode mdiMode = MdiMode.Tabbed;
        private XtraTabbedMdiManager mdiManager;
        private Form shell;

        internal WindowMenuHelper() { }

        internal WindowMenuHelper(XtraTabbedMdiManager mdiManager, Form shell)
        {
            MdiManager = mdiManager;
            Shell = shell;
        }
        
        public XtraTabbedMdiManager MdiManager
        {
            get { return mdiManager; }
            set { mdiManager = value; }
        }

        public Form Shell
        {
            get { return shell; }
            set { shell = value; }
        }

        public void MdiChangeMode(object sender, EventArgs e)
        {
            mdiMode = mdiMode == MdiMode.Tabbed ? MdiMode.Windowed : MdiMode.Tabbed; // Toggle
            SetMdiMode(mdiMode);
        }

        private void SetMdiMode(MdiMode mode)
        {
            MdiManager.MdiParent = mode == MdiMode.Tabbed ? Shell : null;
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
            MdiManager.MdiParent = null;
            Shell.LayoutMdi(layout);
        }
    }
}
