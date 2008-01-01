using DevExpress.XtraEditors.Controls;
using DevExpress.XtraNavBar;
using Microsoft.Practices.CompositeUI.UIElements;
using Microsoft.Practices.CompositeUI.Utility;

namespace CABDevExpress.UIElements
{
	/// <summary>
	/// An adapter that wraps a <see cref="NavItemCollection"/> for use as an <see cref="IUIElementAdapter"/>.
	/// </summary>
	public class EditorButtonCollectionUIAdapter : UIElementAdapter<EditorButton>
	{
		private readonly EditorButtonCollection buttonCollection;

		/// <summary>
		/// Initializes a new instance of the <see cref="NavBarItemCollectionUIAdapter"/> class.
		/// </summary>
		/// <param name="collection"></param>
		public EditorButtonCollectionUIAdapter(EditorButtonCollection collection)
		{
			Guard.ArgumentNotNull(collection, "EditorButtonCollection");
			buttonCollection = collection;
		}

		/// <summary>
		/// See <see cref="UIElementAdapter{TUIElement}.Add(TUIElement)"/> for more information.
		/// </summary>
		protected override EditorButton Add(EditorButton uiElement)
		{
			buttonCollection.Add(uiElement);
			return uiElement;
		}

		/// <summary>
		/// See <see cref="UIElementAdapter{TUIElement}.Remove(TUIElement)"/> for more information.
		/// </summary>
		protected override void Remove(EditorButton uiElement)
		{
			int index = buttonCollection.IndexOf(uiElement);
			if (index > -1) 
				buttonCollection.RemoveAt(index);
                          
		}

		/// <summary>
		/// When overridden in a derived class, returns the correct index for the item being added. By default,
		/// it will return the length of the buttonCollection.
		/// </summary>
		/// <param name="uiElement"></param>
		/// <returns></returns>
		protected virtual int GetInsertingIndex(object uiElement)
		{
			return buttonCollection.Count;
		}
	}
}