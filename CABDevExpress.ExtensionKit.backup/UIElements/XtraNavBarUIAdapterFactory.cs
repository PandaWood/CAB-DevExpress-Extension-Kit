using DevExpress.XtraNavBar;
using Microsoft.Practices.CompositeUI.UIElements;
using Microsoft.Practices.CompositeUI.Utility;

namespace CABDevExpress.UIElements
{
	/// <summary>
	/// A <see cref="IUIElementAdapterFactory"/> that produces adapters for XtraNavBar-related UI Elements.
	/// </summary>
	public class XtraNavBarUIAdapterFactory : IUIElementAdapterFactory
	{
		/// <summary>
		/// Returns a <see cref="IUIElementAdapter"/> for the specified uielement.
		/// </summary>
		/// <param name="uiElement">uiElement for which to return a <see cref="IUIElementAdapter"/></param>
		/// <returns>A <see cref="IUIElementAdapter"/> that represents the specified element</returns>
		public IUIElementAdapter GetAdapter(object uiElement)
		{
			Guard.ArgumentNotNull(uiElement, "uiElement");

			if (uiElement is NavBarControl)
				return new NavBarGroupCollectionUIAdapter(((NavBarControl) uiElement).Groups);

			if (uiElement is NavBarGroup)
				return new NavBarItemCollectionUIAdapter(((NavBarGroup) uiElement).ItemLinks,
				                                         ((NavBarGroup) uiElement).NavBar.Items);

			throw ExceptionFactory.CreateInvalidAdapterElementType(uiElement.GetType(), GetType());
		}

		/// <summary>
		/// Indicates if the specified ui element is supported by the adapter factory.
		/// </summary>
		/// <param name="uiElement">UI Element to evaluate</param>
		/// <returns>Returns true for supported elements, otherwise returns false.</returns>
		public bool Supports(object uiElement)
		{
			return uiElement is NavBarControl || uiElement is NavBarGroup;
		}
	}
}