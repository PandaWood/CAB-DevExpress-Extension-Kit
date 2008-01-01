using System;
using DevExpress.XtraBars;
using Microsoft.Practices.CompositeUI.UIElements;
using Microsoft.Practices.CompositeUI.Utility;
using DevExpress.XtraBars.Ribbon;

namespace CABDevExpress.Adapters
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
			Guard.ArgumentNotNull(applicationMenu, "applicationMenu");
			this.applicationMenu = applicationMenu;
		}

		/// <summary>
		/// See <see cref="UIElementAdapter{TUIElement}.Add(TUIElement)"/> for more information.
		/// </summary>
		protected override BarItem Add(BarItem uiElement)
		{
			Guard.ArgumentNotNull(uiElement, "uiElement");

			if (applicationMenu == null)
				throw new InvalidOperationException();

			applicationMenu.AddItem(uiElement);
			return uiElement;
		}

		/// <summary>
		/// See <see cref="UIElementAdapter{TUIElement}.Remove(TUIElement)"/> for more information.
		/// </summary>
		protected override void Remove(BarItem uiElement)
		{
			Guard.ArgumentNotNull(uiElement, "uiElement");
			if (applicationMenu == null || applicationMenu.Ribbon == null)
				throw new InvalidOperationException();

			applicationMenu.Ribbon.Items.Remove(uiElement);
		}
	}
}