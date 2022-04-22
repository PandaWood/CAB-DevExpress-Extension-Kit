using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using CABDevExpress.Workspaces;
using Microsoft.Practices.CompositeUI.WinForms;

namespace CABDevExpress.SmartPartInfos
{
    /// <summary>
    /// Provides information to show smartparts in the <see cref="XtraWindowWorkspace"/>.
    /// </summary>
    [ToolboxBitmap(typeof(XtraWindowSmartPartInfo), "XtraWindowSmartPartInfo")]
    public class XtraWindowSmartPartInfo : WindowSmartPartInfo
    {
    	private FormStartPosition startPosition = FormStartPosition.WindowsDefaultLocation;
    	private FormBorderStyle formBorderStyle = FormBorderStyle.Sizable;
        private FormWindowState windowState = default(FormWindowState);

        ///
        /// The start position of the Modal form
        ///
        [DefaultValue(FormStartPosition.WindowsDefaultLocation)]
        [Category("Layout")]
        public FormStartPosition StartPosition
        {
            get { return startPosition; }
            set { startPosition = value; }
        }

    	///
    	/// Whether the form shows in the Windows taskbar
    	///
    	[DefaultValue(false)]
    	[Category("Layout")]
    	public bool ShowInTaskbar { get; set; }

    	///
    	/// Form's Accept Button
    	///
    	[DefaultValue(null)]
    	[Category("Layout")]
    	public IButtonControl AcceptButton { get; set; }

    	///
    	/// Form's Cancel Button
    	///
    	[DefaultValue(null)]
    	[Category("Layout")]
    	public IButtonControl CancelButton { get; set; }

    	///
        /// Form's Border Style
        ///
        [DefaultValue(FormBorderStyle.Sizable)]
        [Category("Layout")]
        public FormBorderStyle FormBorderStyle
        {
            get { return formBorderStyle; }
            set { formBorderStyle = value; }
        }

        ///
        /// Form's Window State
        ///
        [DefaultValue(default(FormBorderStyle))]
        [Category("Layout")]
        public FormWindowState WindowState
        {
            get { return windowState; }
            set { windowState = value; }
        }

    	///
    	/// Form's Size
    	///
    	[Category("Layout")]
    	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    	public Size Size
    	{
    		get { return new Size(Width, Height); }
    		set
    		{
    			Width = value.Width;
    			Height = value.Height;
    		}
    	}
    }
}
