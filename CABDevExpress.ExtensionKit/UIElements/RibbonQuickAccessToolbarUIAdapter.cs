using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using Microsoft.Practices.CompositeUI.UIElements;
using Microsoft.Practices.CompositeUI.Utility;

namespace CABDevExpress.UIElements
{
	public class RibbonQuickAccessToolbarUIAdapter : UIElementAdapter<BarItem>
	{
		private readonly RibbonQuickAccessToolbar ribbonQuickAccessToolbar;

		public RibbonQuickAccessToolbarUIAdapter(RibbonQuickAccessToolbar ribbonQuickAccessToolbar)
		{
			Guard.ArgumentNotNull(ribbonQuickAccessToolbar, "RibbonQuickAccessToolbar");
			this.ribbonQuickAccessToolbar = ribbonQuickAccessToolbar;
		}

		protected override BarItem Add(BarItem uiElement)
		{
			Guard.ArgumentNotNull(uiElement, "BarItem");
			ribbonQuickAccessToolbar.ItemLinks.Add(uiElement);
			return uiElement;
		}

		protected override void Remove(BarItem uiElement)
		{
			Guard.ArgumentNotNull(uiElement, "BarItem");
			ribbonQuickAccessToolbar.ItemLinks.Remove(uiElement);
		}
	}
}