using DevExpress.XtraBars;
using Microsoft.Practices.CompositeUI.UIElements;
using Microsoft.Practices.CompositeUI.Utility;

namespace CABDevExpress.UIElements
{
	/// <summary>
	/// An adapter that wraps a <see cref="BarItems"/> for use as an <see cref="IUIElementAdapter"/>.
	/// </summary>
	public class BarLinksCollectionUIAdapter : UIElementAdapter<BarItem>
	{
		private readonly BarItems itemCollection;
		private readonly BarItemLinkCollection linkCollection;

		/// <summary>
		/// Initializes a new instance of the <see cref="BarLinksCollectionUIAdapter"/> class.
		/// </summary>
		/// <param name="linkCollection"></param>
		/// <param name="itemCollection"></param>
		public BarLinksCollectionUIAdapter(BarItemLinkCollection linkCollection, BarItems itemCollection)
		{
			Guard.ArgumentNotNull(itemCollection, "BarItems");
			this.itemCollection = itemCollection;

			Guard.ArgumentNotNull(linkCollection, "BarItemLinkCollection");
			this.linkCollection = linkCollection;
		}

		/// <summary>
		/// See <see cref="UIElementAdapter{TUIElement}.Add(TUIElement)"/> for more information.
		/// </summary>
		protected override BarItem Add(BarItem uiElement)
		{
			itemCollection.Add(uiElement);
			linkCollection.Insert(GetInsertingIndex(uiElement), uiElement);
			return uiElement;
		}

		/// <summary>
		/// See <see cref="UIElementAdapter{TUIElement}.Remove(TUIElement)"/> for more information.
		/// </summary>
		protected override void Remove(BarItem uiElement)
		{
			itemCollection.Remove(uiElement);
		}

		/// <summary>
		/// When overridden in a derived class, returns the correct index for the item being added. By default,
		/// it will return the length of the itemCollection.
		/// </summary>
		/// <param name="uiElement"></param>
		/// <returns></returns>
		protected virtual int GetInsertingIndex(object uiElement)
		{
			return linkCollection.Count;
		}

		/// <summary>
		/// Returns the internal itemCollection managed by the <see cref="BarLinksCollectionUIAdapter"/>
		/// </summary>
		protected BarItemLinkCollection InternalCollection
		{
			get { return linkCollection; }
		}
	}
}