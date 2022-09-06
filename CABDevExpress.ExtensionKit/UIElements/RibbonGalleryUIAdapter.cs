using System;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.Commands;
using Microsoft.Practices.CompositeUI.UIElements;
using Microsoft.Practices.CompositeUI.Utility;

namespace CABDevExpress.UIElements
{
	/// <summary>
	/// An adapter that wraps a <see cref="RibbonGalleryBarItem"/> for use
	/// as an <see cref="IUIElementAdapter"/>.
	/// </summary>
	public class RibbonGalleryUIAdapter : UIElementAdapter<GalleryItemGroup>
	{
		private readonly RibbonGalleryBarItem ribbonGallery;
		private readonly WorkItem workItem;

		/// <summary>
		/// Initializes a new instance of the
		/// <see cref="RibbonApplicationMenuUIAdapter"/> class.
		/// </summary>
		/// <param name="ribbonGallery">The application menu.</param>
		/// <param name="workItem">The work item which added the command.
		/// We need to access the Commands property of the work item to
		/// fire the Command associated with the GalleryItem.</param>
		public RibbonGalleryUIAdapter(RibbonGalleryBarItem ribbonGallery, WorkItem workItem)
		{
			Guard.ArgumentNotNull(ribbonGallery, "RibbonGalleryBarItem");
			Guard.ArgumentNotNull(workItem, "workItem");

			this.ribbonGallery = ribbonGallery;
			this.workItem = workItem;

			// Developer Express does not support click events for
			// GalleryItems so we need to handle the ItemClick event here.

			this.ribbonGallery.Gallery.ItemClick += ItemClick;
		}

		/// <summary>
		/// Gets the work item which was passed in at creation.
		/// </summary>
		/// <value>The work item.</value>
		protected WorkItem WorkItem
		{
			get { return workItem; }
		}

		/// <summary>
		/// Fired when any <see cref="GalleryItem"/> contained in this
		/// <see cref="RibbonGalleryBarItem"/> is clicked. If the
		/// GalleryItem.Tag property contains an active CommandName string,
		/// that command is fired.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="GalleryItemClickEventArgs"/>
		/// instance containing the event data.</param>
		protected virtual void ItemClick(object sender, GalleryItemClickEventArgs e)
		{
			if (e.Item.Tag is string && !String.IsNullOrEmpty(e.Item.Tag as string))
			{
				Command command = WorkItem.Commands.Get<Command>(e.Item.Tag as string);

				if (command != null)
					command.Execute();
			}
		}

		/// <summary>
		/// See <see cref="UIElementAdapter{TUIElement}.Add(TUIElement)"/>
		/// for more information.
		/// </summary>
		protected override GalleryItemGroup Add(GalleryItemGroup uiElement)
		{
			Guard.ArgumentNotNull(uiElement, "GalleryItemGroup");
			ribbonGallery.Gallery.Groups.Add(uiElement);
			return uiElement;
		}

		/// <summary>
		/// See <see cref="UIElementAdapter{TUIElement}.Remove(TUIElement)"/>
		/// for more information.
		/// </summary>
		protected override void Remove(GalleryItemGroup uiElement)
		{
			Guard.ArgumentNotNull(uiElement, "GalleryItemGroup");
            if (ribbonGallery.Gallery.Groups.Contains(uiElement))
                ribbonGallery.Gallery.Groups.Remove(uiElement);
		}
	}
}
