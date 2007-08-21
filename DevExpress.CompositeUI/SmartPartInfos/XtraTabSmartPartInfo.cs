using System;
using System.ComponentModel;
using System.Drawing;
using Microsoft.Practices.CompositeUI.WinForms;

namespace DevExpress.CompositeUI.SmartPartInfos
{
    public class XtraTabSmartPartInfo : TabSmartPartInfo
    {
        //
        // Summary:
        //     Gets or sets the background color of the tab page's client region.
        private Color backColor;

        public Color BackColor
        {
            get { return backColor; }
            set { backColor = value; }
        }

        //
        // Summary:
        //     Gets or sets whether a tab page can be selected.
        private bool enabled;

        public bool Enabled
        {
            get { return enabled; }
            set { enabled = value; }
        }

        //
        // Summary:
        //     Gets or sets the foreground color of the tab page's client region.
        private Color foreColor;

        public Color ForeColor
        {
            get { return foreColor; }
            set { foreColor = value; }
        }

        //
        // Summary:
        //     Gets or sets the image displayed within the tab page's header.
        private Image image;

        public Image Image
        {
            get { return image; }
            set { image = value; }
        }

        //
        // Summary:
        //     Gets or sets the index of the image displayed within the tab page's header.
        [DefaultValue(-1)] [Category("Appearance")] [Description("Gets or sets the index of the image displayed within the tab page's header.")] private int imageIndex = -1;

        public int ImageIndex
        {
            get { return imageIndex; }
            set { imageIndex = value; }
        }

        //
        // Summary:
        //     Gets or sets whether a tab page can be selected.
        private bool pageEnabled = true;

        public bool PageEnabled
        {
            get { return pageEnabled; }
            set { pageEnabled = value; }
        }

        //
        // Summary:
        //     Gets or sets whether the tab page is visible.
        [DefaultValue(true)] [Description("Gets or sets whether the tab page is visible.")] [Category("Behavior")] private bool pageVisible = true;

        public bool PageVisible
        {
            get { return pageVisible; }
            set { pageVisible = value; }
        }

        //
        // Summary:
        //     Gets or sets the control's height and width.
        private Size size;

        public Size Size
        {
            get { return size; }
            set { size = value; }
        }

        //
        // Summary:
        //     Gets or sets the tab order of the control within its container.
        private int tabIndex;

        public int TabIndex
        {
            get { return tabIndex; }
            set { tabIndex = value; }
        }

        //
        // Summary:
        //     Gets or sets a value indicating whether an end user can focus this page using
        //     the TAB key.
        private bool tabStop;

        public bool TabStop
        {
            get { return tabStop; }
            set { tabStop = value; }
        }

        //
        // Summary:
        //     Gets or sets the tab page's caption.
        private string text;

        public string Text
        {
            get { return text; }
            set { text = value; }
        }

        //
        // Summary:
        //     Gets or sets a tooltip for the tab page.
        [DefaultValue("")] [Description("Gets or sets a tooltip for the tab page.")] [Category("Appearance")] [Localizable(true)] public string tooltip = String.Empty;

        public string Tooltip
        {
            get { return tooltip; }
            set { tooltip = value; }
        }

        //
        // Summary:
        //     Gets or sets whether the tab page is visible.
        [DesignerSerializationVisibility(0)] [Browsable(false)] private bool visible;

        public bool Visible
        {
            get { return visible; }
            set { visible = value; }
        }

		/// <summary>
		/// Gets or sets the page header font (CABDevExpress added property)
		/// </summary>
		private Font pageHeaderFont;

		public Font PageHeaderFont
		{
			get { return pageHeaderFont; }
			set { pageHeaderFont = value; }
		}
    }
}