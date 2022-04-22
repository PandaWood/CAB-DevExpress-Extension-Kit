using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using Microsoft.Practices.CompositeUI.UIElements;
using Microsoft.Practices.CompositeUI.Utility;

namespace CABDevExpress.UIElements
{
	/// <summary>
	/// An adapter that wraps a <see cref="RibbonQuickAccessToolbar"/> for use as an <see cref="IUIElementAdapter"/>.
	/// </summary>
	public class RibbonQuickAccessToolbarUIAdapter : UIElementAdapter<BarItem>
	{
		private readonly RibbonQuickAccessToolbar ribbonQuickAccessToolbar;

		/// <summary>
		/// Initializes a new instance of the <see cref="BarsUIAdapter"/> class.
		/// </summary>
		/// <param name="ribbonQuickAccessToolbar"></param>
		public RibbonQuickAccessToolbarUIAdapter(RibbonQuickAccessToolbar ribbonQuickAccessToolbar)
		{
			Guard.ArgumentNotNull(ribbonQuickAccessToolbar, "RibbonQuickAccessToolbar");
			this.ribbonQuickAccessToolbar = ribbonQuickAccessToolbar;
		}

		/// <summary>
		/// See <see cref="UIElementAdapter{TUIElement}.Add(TUIElement)"/> for more information.
		/// </summary>
		protected override BarItem Add(BarItem uiElement)
		{
			Guard.ArgumentNotNull(uiElement, "BarItem");
			ribbonQuickAccessToolbar.ItemLinks.Add(uiElement);
			return uiElement;
		}

		/// <summary>
		/// See <see cref="UIElementAdapter{TUIElement}.Remove(TUIElement)"/> for more information.
		/// </summary>
		protected override void Remove(BarItem uiElement)
		{
			Guard.ArgumentNotNull(uiElement, "BarItem");
			ribbonQuickAccessToolbar.ItemLinks.Remove(uiElement);
		}
	}
}