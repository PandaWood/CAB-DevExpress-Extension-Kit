using DevExpress.XtraNavBar;
using Microsoft.Practices.CompositeUI.UIElements;
using Microsoft.Practices.CompositeUI.Utility;

namespace CABDevExpress.UIElements
{
	/// <summary>
	/// An adapter that wraps a <see cref="NavItemCollection"/> for use as an <see cref="IUIElementAdapter"/>.
	/// </summary>
	public class NavBarItemCollectionUIAdapter : UIElementAdapter<NavBarItem>
	{
		private readonly NavItemCollection itemCollection;
		private readonly NavLinkCollection linkCollection;

		/// <summary>
		/// Initializes a new instance of the <see cref="NavBarItemCollectionUIAdapter"/> class.
		/// </summary>
		/// <param name="navBarGroup"></param>
		public NavBarItemCollectionUIAdapter(NavBarGroup navBarGroup)
			: this(navBarGroup.ItemLinks, navBarGroup.NavBar.Items)
		{
			// Do nothing (except chain constructor)
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="NavBarItemCollectionUIAdapter"/> class.
		/// </summary>
		/// <param name="linkCollection"></param>
		/// <param name="itemCollection"></param>
		public NavBarItemCollectionUIAdapter(NavLinkCollection linkCollection, NavItemCollection itemCollection)
		{
			Guard.ArgumentNotNull(linkCollection, "NavLinkCollection");
			this.linkCollection = linkCollection;

			Guard.ArgumentNotNull(itemCollection, "NavItemCollection");
			this.itemCollection = itemCollection;
		}

		/// <summary>
		/// See <see cref="UIElementAdapter{TUIElement}.Add(TUIElement)"/> for more information.
		/// </summary>
		protected override NavBarItem Add(NavBarItem uiElement)
		{
			Guard.ArgumentNotNull(uiElement, "NavBarItem");

			itemCollection.Insert(GetInsertingIndex(uiElement), uiElement);
			linkCollection.Add(uiElement);
			return uiElement;
		}

		/// <summary>
		/// See <see cref="UIElementAdapter{TUIElement}.Remove(TUIElement)"/> for more information.
		/// </summary>
		protected override void Remove(NavBarItem uiElement)
		{
			Guard.ArgumentNotNull(uiElement, "NavBarItem");

			if (uiElement.NavBar != null)
			{
				itemCollection.Remove(uiElement);
				linkCollection.Remove(uiElement);
			}
		}

		/// <summary>
		/// When overridden in a derived class, returns the correct index for the item being added. By default,
		/// it will return the length of the itemCollection.
		/// </summary>
		/// <param name="uiElement"></param>
		/// <returns></returns>
		protected virtual int GetInsertingIndex(object uiElement)
		{
			return itemCollection.Count;
		}
	}
}