using System;
using DevExpress.XtraBars.Ribbon;

namespace CABDevExpress.UIElements
{
    public class RibbonGalleryGroupDynamicUIAdapter : RibbonGalleryGroupUIAdapter
    {
        public RibbonGalleryGroupDynamicUIAdapter(GalleryItemGroup ribbonGalleryGroup) 
            : base(ribbonGalleryGroup) { }

        protected override void ValidateUiElement(GalleryItem uiElement)
        {
            if (uiElement == null)
                throw new ArgumentException("uiElement.Tag cannot null. It must contain a DynamicCommandEventLink.");
            if (uiElement.Tag is DynamicCommandEventLink)
            {
                if (String.IsNullOrEmpty((uiElement.Tag as DynamicCommandEventLink).EventTopicName))
                    throw new ArgumentException("uiElement.Tag EventTopicName cannot be empty. It must contain the Event to be fired.");
            }
            else
                throw new ArgumentException("uiElement.Tag must be a DynamicCommandEventLink and must contain the Event to be fired.");
        }
    }
}
