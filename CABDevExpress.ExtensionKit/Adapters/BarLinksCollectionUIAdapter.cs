using System;
using DevExpress.XtraBars;
using Microsoft.Practices.CompositeUI.UIElements;
using Microsoft.Practices.CompositeUI.Utility;

namespace CABDevExpress.Adapters
{
	/// <summary>
	/// An adapter that wraps a <see cref="BarItems"/> for use as an <see cref="IUIElementAdapter"/>.
	/// </summary>
	public class BarLinksCollectionUIAdapter : UIElementAdapter<BarItem>
	{
		private readonly BarItems collection;
		private readonly BarItemLinkCollection linkCollection;

		/// <summary>
		/// Initializes a new instance of the <see cref="BarLinksCollectionUIAdapter"/> class.
		/// </summary>
		/// <param name="linkCollection"></param>
		/// <param name="collection"></param>
		public BarLinksCollectionUIAdapter(BarItemLinkCollection linkCollection, BarItems collection)
		{
			Guard.ArgumentNotNull(collection, "collection");
			this.collection = collection;

			Guard.ArgumentNotNull(linkCollection, "linkCollection");
			this.linkCollection = linkCollection;
		}

		/// <summary>
		/// See <see cref="UIElementAdapter{TUIElement}.Add(TUIElement)"/> for more information.
		/// </summary>
		protected override BarItem Add(BarItem uiElement)
		{
			if (collection == null || linkCollection == null)
				throw new InvalidOperationException();

			collection.Add(uiElement);
			linkCollection.Insert(GetInsertingIndex(uiElement), uiElement);

			return uiElement;
		}

		/// <summary>
		/// See <see cref="UIElementAdapter{TUIElement}.Remove(TUIElement)"/> for more information.
		/// </summary>
		protected override void Remove(BarItem uiElement)
		{
			collection.Remove(uiElement);
		}

		/// <summary>
		/// When overridden in a derived class, returns the correct index for the item being added. By default,
		/// it will return the length of the collection.
		/// </summary>
		/// <param name="uiElement"></param>
		/// <returns></returns>
		protected virtual int GetInsertingIndex(object uiElement)
		{
			return linkCollection.Count;
		}

		/// <summary>
		/// Returns the internal collection managed by the <see cref="BarLinksCollectionUIAdapter"/>
		/// </summary>
		protected BarItemLinkCollection InternalCollection
		{
			get { return linkCollection; }
		}
	}
}