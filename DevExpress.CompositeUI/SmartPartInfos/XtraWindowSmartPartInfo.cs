using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.CompositeUI.Workspaces;
using Microsoft.Practices.CompositeUI.WinForms;

namespace DevExpress.CompositeUI.SmartPartInfos
{
    /// <summary>
    /// Provides information to show smartparts in the <see cref="XtraWindowWorkspace"/>.
    /// </summary>
    [ToolboxBitmap(typeof(XtraWindowSmartPartInfo), "XtraWindowSmartPartInfo")]
    public class XtraWindowSmartPartInfo : WindowSmartPartInfo
    {
        private bool showInTaskbar = false;
        private FormStartPosition startPosition = default(FormStartPosition);

        ///
        /// Start position of the Modal form
        ///
        [DefaultValue(false)]
        [Category("Layout")]
        public FormStartPosition StartPosition
        {
            get { return startPosition; }
            set { startPosition = value; }
        }
        //-----

        ///
        /// Whether the forms shows in the taskbar (TODO make work)
        ///
        [DefaultValue(false)]
        [Category("Layout")]
        public bool ShowInTaskbar
        {
            get { return showInTaskbar; }
            set { showInTaskbar = value; }
        }
        //-----
    }
}
