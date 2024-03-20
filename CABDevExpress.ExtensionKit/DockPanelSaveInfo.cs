using DevExpress.XtraBars.Docking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CABDevExpress
{
    public class DockPanelSaveInfo
    {
        public System.Drawing.Rectangle Rectangle {  get; set; }
        public DockVisibility Visibility {  get; set; }
        public DockingStyle Dock { get; set; }
        public DockingStyle SavedDock { get; set; }
        public TabsPosition TabsPosition { get; set; }
    }
}
