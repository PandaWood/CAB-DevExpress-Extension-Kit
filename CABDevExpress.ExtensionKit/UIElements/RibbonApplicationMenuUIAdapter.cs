using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using Microsoft.Practices.CompositeUI.UIElements;
using Microsoft.Practices.CompositeUI.Utility;

namespace CABDevExpress.UIElements
{
	/// <summary>
	/// An adapter that wraps an <see cref="ApplicationMenu"/> for use as an <see cref="IUIElementAdapter"/>.
	/// </summary>
	public class RibbonApplicationMenuUIAdapter : UIElementAdapter<BarItem>
	{
		private readonly ApplicationMenu applicationMenu;

		/// <summary>
		/// Initializes a new instance of the <see cref="RibbonApplicationMenuUIAdapter"/> class.
		/// </summary>
		/// <param name="applicationMenu"></param>
		public RibbonApplicationMenuUIAdapter(ApplicationMenu applicationMenu)
		{
			Guard.ArgumentNotNull(applicationMenu, "ApplicationMenu");
			Guard.ArgumentNotNull(applicationMenu.Ribbon, "ApplicationMenu.Ribbon");

			this.applicationMenu = applicationMenu;
		}

		/// <summary>
		/// See <see cref="UIElementAdapter{TUIElement}.Add(TUIElement)"/> for more information.
		/// </summary>
		protected override BarItem Add(BarItem uiElement)
		{
			Guard.ArgumentNotNull(uiElement, "BarItem");

			applicationMenu.AddItem(uiElement);
			return uiElement;
		}

		/// <summary>
		/// See <see cref="UIElementAdapter{TUIElement}.Remove(TUIElement)"/> for more information.
		/// </summary>
		protected override void Remove(BarItem uiElement)
		{
			Guard.ArgumentNotNull(uiElement, "BarItem");
            if (applicationMenu != null && applicationMenu.Ribbon != null)
                applicationMenu.Ribbon.Items.Remove(uiElement);
		}
	}
}