using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using Microsoft.Practices.CompositeUI.UIElements;
using Microsoft.Practices.CompositeUI.Utility;

namespace CABDevExpress.UIElements
{
	/// <summary>
	/// An adapter that wraps a <see cref="RibbonPageHeaderItemLinkCollection"/> for use as an <see cref="IUIElementAdapter"/>.
	/// </summary>
	public class RibbonPageHeaderUIAdapter : UIElementAdapter<BarItem>
	{
		private readonly RibbonPageHeaderItemLinkCollection ribbonPageHeader;

		/// <summary>
		/// Initializes a new instance of the <see cref="RibbonPageHeaderUIAdapter"/> class.
		/// </summary>
		/// <param name="ribbonPageHeader"></param>
		public RibbonPageHeaderUIAdapter(RibbonPageHeaderItemLinkCollection ribbonPageHeader)
		{
			Guard.ArgumentNotNull(ribbonPageHeader, "RibbonPageHeaderItemLinkCollection");
			this.ribbonPageHeader = ribbonPageHeader;
		}

		/// <summary>
		/// See <see cref="UIElementAdapter{TUIElement}.Add(TUIElement)"/> for more information.
		/// </summary>
		protected override BarItem Add(BarItem uiElement)
		{
			Guard.ArgumentNotNull(uiElement, "BarItem");
			ribbonPageHeader.Add(uiElement);
			return uiElement;
		}

		/// <summary>
		/// See <see cref="UIElementAdapter{TUIElement}.Remove(TUIElement)"/> for more information.
		/// </summary>
		protected override void Remove(BarItem uiElement)
		{
			Guard.ArgumentNotNull(uiElement, "BarItem");
			ribbonPageHeader.Remove(uiElement);
		}
	}
}