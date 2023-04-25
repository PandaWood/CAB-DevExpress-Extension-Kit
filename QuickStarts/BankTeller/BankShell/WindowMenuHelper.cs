using System;
using System.Windows.Forms;
using CABDevExpress.Workspaces;
using DevExpress.XtraTabbedMdi;

namespace BankShell
{
    internal class WindowMenuHelper
    {
        //public enum MdiMode
        //{
        //    Tabbed,
        //    Windowed
        //}

        //private MdiMode mdiMode = MdiMode.Tabbed;
        //private XtraTabbedMdiManager mdiManager;
        private XtraDocumentManagerWorkspace mdiWorkspace;
        //private Form shell;

        internal WindowMenuHelper() { }

        internal WindowMenuHelper(XtraDocumentManagerWorkspace mdiManager)
        {
            mdiWorkspace = mdiManager;
            //Shell = shell;
        }

        public XtraDocumentManagerWorkspace MdiWorkSpace
        {
            get { return mdiWorkspace; }
            set { mdiWorkspace = value; }
        }

        //public Form Shell
        //{
        //    get { return shell; }
        //    set { shell = value; }
        //}

        public void MdiChangeMode(object sender, EventArgs e)
        {
            //mdiMode = mdiMode == MdiMode.Tabbed ? MdiMode.Windowed : MdiMode.Tabbed; // Toggle
            //SetMdiMode(mdiMode);
            mdiWorkspace.MdiChangeMode();
        }

        //private void SetMdiMode(MdiMode mode)
        //{
        //    MdiWorkSpace.MdiParent = mode == MdiMode.Tabbed ? Shell : null;
        //}

        public void MdiLayoutCascade(object sender, EventArgs e)
        {
           // LayoutMdi(MdiLayout.Cascade);
            mdiWorkspace.LayoutMdi(MdiLayout.Cascade);
        }

        public void MdiLayoutTileHorizontal(object sender, EventArgs e)
        {
            //LayoutMdi(MdiLayout.TileHorizontal);
            mdiWorkspace.LayoutMdi(MdiLayout.TileHorizontal);
        }

        public void MdiLayoutTileVertical(object sender, EventArgs e)
        {
            //LayoutMdi(MdiLayout.TileVertical);
            mdiWorkspace.LayoutMdi(MdiLayout.TileVertical);
        }

        //private void LayoutMdi(MdiLayout layout)
        //{
        //    SetMdiMode(MdiMode.Windowed);
        //    MdiWorkSpace.MdiParent = null;
        //    Shell.LayoutMdi(layout);
        //}
    }
}
