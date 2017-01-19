using System;
using System.ComponentModel;
using System.Drawing;
using Microsoft.Practices.CompositeUI.WinForms;
using DevExpress.Utils;

namespace CABDevExpress.SmartPartInfos
{
	///<summary>
	/// 
	///</summary>
	public class XtraTabSmartPartInfo : TabSmartPartInfo
	{
		/// <summary>
		/// Gets or sets the background color of the tab page's client region.
		/// </summary>
		public Color BackColor { get; set; }

		/// <summary>
		/// Gets or sets whether a tab page can be selected.
		/// </summary>
		public bool Enabled { get; set; }

		/// <summary>
		/// Gets or sets the foreground color of the tab page's client region.
		/// </summary>
		public Color ForeColor { get; set; }

		/// <summary>
		/// Gets or sets the image displayed within the tab page's header
		/// </summary>
		public Image Image { get; set; }

		[DefaultValue(-1)] 
		[Category("Appearance")] 
		[Description("Gets or sets the index of the image displayed within the tab page's header.")] 
		private int imageIndex = -1;

		/// <summary>
		/// Gets or sets the index of the image displayed within the tab page's header.
		/// </summary>
		public int ImageIndex
		{
			get { return imageIndex; }
			set { imageIndex = value; }
		}

		//
		// Summary:
		//     Gets or sets whether a tab page can be selected.
		private bool pageEnabled = true;

		///<summary>
		///</summary>
		public bool PageEnabled
		{
			get { return pageEnabled; }
			set { pageEnabled = value; }
		}

		[DefaultValue(true)] 
		[Description("Gets or sets whether the tab page is visible.")] 
		[Category("Behavior")] 
		private bool pageVisible = true;

		/// <summary>
		///  Gets or sets whether the tab page is visible.
		/// </summary>
		public bool PageVisible
		{
			get { return pageVisible; }
			set { pageVisible = value; }
		}


        [DefaultValue(true)]
        [Description("Gets or sets whether the ShowCloseButton is visible.")]
        [Category("Behavior")]
        private DefaultBoolean bShowCloseButton = DefaultBoolean.True;

        /// <summary>
        ///  Gets or sets whether the tab page is visible.
        /// </summary>
        public DefaultBoolean ShowCloseButton
        {
            get { return bShowCloseButton; }
            set { bShowCloseButton = value; }
        }


        /// <summary>
        /// Gets or sets the control's height and width.
        /// </summary>
        public Size Size { get; set; }

		/// <summary>
		/// Gets or sets the tab order of the control within its container.
		/// </summary>
		public int TabIndex { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether an end user can focus this page using the TAB key.
		/// </summary>
		public bool TabStop { get; set; }

		/// <summary>
		/// Gets or sets the tab page's caption.
		/// </summary>
		public string Text { get; set; }

		[DefaultValue("")] 
		[Description("Gets or sets a tooltip for the tab page.")] 
		[Category("Appearance")] 
		[Localizable(true)] 
		public string tooltip = String.Empty;

		/// <summary>
		/// Gets or sets a tooltip for the tab page.
		/// </summary>
		public string Tooltip
		{
			get { return tooltip; }
			set { tooltip = value; }
		}

		[DesignerSerializationVisibility(0)] 
		[Browsable(false)] 
		private bool visible;

		/// <summary>
		/// Gets or sets whether the tab page is visible
		/// </summary>
		public bool Visible
		{
			get { return visible; }
			set { visible = value; }
		}

		public Font PageHeaderFont { get; set; }
	}
}