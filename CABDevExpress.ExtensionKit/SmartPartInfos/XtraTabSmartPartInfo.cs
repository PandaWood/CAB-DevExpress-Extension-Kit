using System;
using System.ComponentModel;
using System.Drawing;
using Microsoft.Practices.CompositeUI.WinForms;

namespace CABDevExpress.SmartPartInfos
{
	///<summary>
	/// 
	///</summary>
	public class XtraTabSmartPartInfo : TabSmartPartInfo
	{
		private Color backColor;

		/// <summary>
		/// Gets or sets the background color of the tab page's client region.
		/// </summary>
		public Color BackColor
		{
			get { return backColor; }
			set { backColor = value; }
		}

		private bool enabled;

		/// <summary>
		/// Gets or sets whether a tab page can be selected.
		/// </summary>
		public bool Enabled
		{
			get { return enabled; }
			set { enabled = value; }
		}

		private Color foreColor;

		/// <summary>
		/// Gets or sets the foreground color of the tab page's client region.
		/// </summary>
		public Color ForeColor
		{
			get { return foreColor; }
			set { foreColor = value; }
		}

		private Image image;

		/// <summary>
		/// Gets or sets the image displayed within the tab page's header
		/// </summary>
		public Image Image
		{
			get { return image; }
			set { image = value; }
		}

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

		private Size size;

		/// <summary>
		/// Gets or sets the control's height and width.
		/// </summary>
		public Size Size
		{
			get { return size; }
			set { size = value; }
		}

		private int tabIndex;

		/// <summary>
		/// Gets or sets the tab order of the control within its container.
		/// </summary>
		public int TabIndex
		{
			get { return tabIndex; }
			set { tabIndex = value; }
		}

		private bool tabStop;

		/// <summary>
		/// Gets or sets a value indicating whether an end user can focus this page using the TAB key.
		/// </summary>
		public bool TabStop
		{
			get { return tabStop; }
			set { tabStop = value; }
		}

		private string text;

		/// <summary>
		/// Gets or sets the tab page's caption.
		/// </summary>
		public string Text
		{
			get { return text; }
			set { text = value; }
		}

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

		/// <summary>
		/// Gets or sets the page header font (CABDevExpress added property)
		/// </summary>
		private Font pageHeaderFont;

		///<summary>
		///</summary>
		public Font PageHeaderFont
		{
			get { return pageHeaderFont; }
			set { pageHeaderFont = value; }
		}
	}
}