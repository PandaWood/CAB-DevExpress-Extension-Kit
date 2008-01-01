using DevExpress.XtraBars.Ribbon;
using Microsoft.Practices.CompositeUI.UIElements;
using Microsoft.Practices.CompositeUI.Utility;

namespace CABDevExpress.Adapters
{
	/// <summary>
	/// An adapter that wraps a <see cref="RibbonPageGroup"/> for use as an <see cref="IUIElementAdapter"/>.
	/// Class it is used to Manage groups added and removed from a ribbon Page
	/// </summary>
	public class RibbonPageGroupCollectionUIAdapter : UIElementAdapter<RibbonPageGroup>
	{
		private readonly RibbonPageGroupCollection ribbonPageGroupCollection;

		/// <summary>
		/// Initializes a new instance of the <see cref="RibbonPageGroupCollectionUIAdapter"/> class.
		/// </summary>
		/// <param name="collection"></param>
		public RibbonPageGroupCollectionUIAdapter(RibbonPageGroupCollection collection)
		{
			Guard.ArgumentNotNull(collection, "RibbonPageGroupCollection");
			ribbonPageGroupCollection = collection;
		}

		/// <summary>
		/// See <see cref="UIElementAdapter{TUIElement}.Add(TUIElement)"/> for more information.
		/// </summary>
		protected override RibbonPageGroup Add(RibbonPageGroup uiElement)
		{
			ribbonPageGroupCollection.Insert(GetInsertingIndex(uiElement), uiElement);
			return uiElement;
		}

		/// <summary>
		/// See <see cref="UIElementAdapter{TUIElement}.Remove(TUIElement)"/> for more information.
		/// </summary>
		protected override void Remove(RibbonPageGroup uiElement)
		{
			if (uiElement.Page != null)
				uiElement.Page.Groups.Remove(uiElement);		
				//TODO I want to know why this doesn't use ribbonPageGroupCollection.Remove(uiElement);
		}

		/// <summary>
		/// When overridden in a derived class, returns the correct index for the item being added. By default,
		/// it will return the length of the ribbonPageGroupCollection.
		/// </summary>
		/// <param name="uiElement"></param>
		/// <returns></returns>
		protected virtual int GetInsertingIndex(object uiElement)
		{
			return ribbonPageGroupCollection.Count;
		}
	}
}