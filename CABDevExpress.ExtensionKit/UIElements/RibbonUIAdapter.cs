using DevExpress.XtraBars.Ribbon;
using Microsoft.Practices.CompositeUI.UIElements;
using Microsoft.Practices.CompositeUI.Utility;

namespace CABDevExpress.UIElements
{
	/// <summary>
	/// An adapter that wraps a <see cref="RibbonControl"/> for use as an <see cref="IUIElementAdapter"/>.
	/// </summary>
	public class RibbonUIAdapter : UIElementAdapter<RibbonControl>
	{
		private RibbonControl ribbonControl;

		/// <summary>
		/// Initializes a new instance of the <see cref="BarsUIAdapter"/> class.
		/// </summary>
		/// <param name="ribbonControl"></param>
		public RibbonUIAdapter(RibbonControl ribbonControl)
		{
			Guard.ArgumentNotNull(ribbonControl, "RibbonControl");
			this.ribbonControl = ribbonControl;
		}

		/// <summary>
		/// See <see cref="UIElementAdapter{TUIElement}.Add(TUIElement)"/> for more information.
		/// </summary>
		protected override RibbonControl Add(RibbonControl uiElement)
		{
			Guard.ArgumentNotNull(uiElement, "RibbonControl");

			ribbonControl = uiElement;
			return uiElement;
		}

		/// <summary>
		/// See <see cref="UIElementAdapter{TUIElement}.Remove(TUIElement)"/> for more information.
		/// </summary>
		protected override void Remove(RibbonControl uiElement)
		{
			ribbonControl = null;
		}

		/// <summary>
		/// When overridden in a derived class, returns the correct index for the item being added.
		/// </summary>
		/// <param name="uiElement"></param>
		/// <returns></returns>
		protected virtual int GetInsertingIndex(object uiElement)
		{
			return 1;		// there is only 1 ribbon control
		}
	}
}