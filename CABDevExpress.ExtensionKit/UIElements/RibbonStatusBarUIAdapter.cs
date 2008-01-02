using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using Microsoft.Practices.CompositeUI.UIElements;
using Microsoft.Practices.CompositeUI.Utility;

namespace CABDevExpress.UIElements
{
	/// <summary>
	/// An adapter that wraps a <see cref="RibbonStatusBar"/> for use as an <see cref="IUIElementAdapter"/>.
	/// </summary>
	public class RibbonStatusBarUIAdapter : UIElementAdapter<BarItem>
	{
		private readonly RibbonStatusBar ribbonStatusBar;

		/// <summary>
		/// Initializes a new instance of the <see cref="BarsUIAdapter"/> class.
		/// </summary>
		/// <param name="ribbonStatusBar"></param>
		public RibbonStatusBarUIAdapter(RibbonStatusBar ribbonStatusBar)
		{
			Guard.ArgumentNotNull(ribbonStatusBar, "RibbonStatusBar");
			this.ribbonStatusBar = ribbonStatusBar;
		}

		/// <summary>
		/// See <see cref="UIElementAdapter{TUIElement}.Add(TUIElement)"/> for more information.
		/// </summary>
		protected override BarItem Add(BarItem uiElement)
		{
			Guard.ArgumentNotNull(uiElement, "BarItem");

			ribbonStatusBar.Ribbon.Items.Add(uiElement);
			ribbonStatusBar.ItemLinks.Add(uiElement);
			return uiElement;
		}

		/// <summary>
		/// See <see cref="UIElementAdapter{TUIElement}.Remove(TUIElement)"/> for more information.
		/// </summary>
		protected override void Remove(BarItem uiElement)
		{
			Guard.ArgumentNotNull(uiElement, "BarItem");

			ribbonStatusBar.Ribbon.Items.Remove(uiElement);
		}

		/// <summary>
		/// Returns the correct index for the item being added
		/// </summary>
		/// <param name="uiElement"></param>
		/// <returns></returns>
		protected virtual int GetInsertingIndex(object uiElement)
		{
			return ribbonStatusBar.ItemLinks.Count;
		}
	}
}