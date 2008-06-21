using System;
using DevExpress.XtraBars.Ribbon;
using Microsoft.Practices.CompositeUI.UIElements;
using Microsoft.Practices.CompositeUI.Utility;

namespace CABDevExpress.UIElements
{
	/// <summary>
	/// An adapter that wraps a <see cref="GalleryItemGroup"/> for use
	/// as an <see cref="IUIElementAdapter"/>.
	/// </summary>
	public class RibbonGalleryGroupUIAdapter : UIElementAdapter<GalleryItem>
	{
		private readonly GalleryItemGroup ribbonGalleryGroup;

		/// <summary>
		/// Initializes a new instance of the
		/// <see cref="RibbonGalleryGroupUIAdapter"/> class.
		/// </summary>
		/// <param name="ribbonGalleryGroup"></param>
		public RibbonGalleryGroupUIAdapter(GalleryItemGroup ribbonGalleryGroup)
		{
			Guard.ArgumentNotNull(ribbonGalleryGroup, "GalleryItemGroup");
			this.ribbonGalleryGroup = ribbonGalleryGroup;
		}

		/// <summary>
		/// See <see cref="UIElementAdapter{TUIElement}.Add(TUIElement)"/> for
		/// more information.
		/// </summary>
		protected override GalleryItem Add(GalleryItem uiElement)
		{
			ValidateUiElement(uiElement);
			ribbonGalleryGroup.Items.Add(uiElement);
			return uiElement;
		}

		/// <summary>
		/// Validates a UIElement to ensure that the GalleryItem.Tag property
		/// contains a non-empty string value. This value is assumed to be a
		/// CommandName associated with an instantiated CAB Command.
		/// </summary>
		/// <param name="uiElement">The UIElement to be validated.</param>
		protected virtual void ValidateUiElement(GalleryItem uiElement)
		{
			// not using Guard here so that we can supply the additional
			// information in the message.

			if (uiElement == null)
				throw new ArgumentNullException("uiElement.Tag cannot null."
				                                + " It must contain the CommandName to be fired.");

			if (uiElement.Tag is string)
			{
				if (String.IsNullOrEmpty(uiElement.Tag as string))
					throw new ArgumentException("uiElement.Tag cannot be empty."
					                            + " It must contain the CommandName to be fired.");
			}
			else
			{
				throw new ArgumentException("uiElement.Tag must be a string and"
				                            + " must contain the CommandName to be fired.");
			}
		}

		/// <summary>
		/// See <see cref="UIElementAdapter{TUIElement}.Remove(TUIElement)"/> for
		/// more information.
		/// </summary>
		protected override void Remove(GalleryItem uiElement)
		{
			Guard.ArgumentNotNull(uiElement, "GalleryItem");
			ribbonGalleryGroup.Items.Remove(uiElement);
		}
	}
}
