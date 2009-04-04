using System;
using DevExpress.XtraBars;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.EventBroker;
using Microsoft.Practices.CompositeUI.Utility;

namespace CABDevExpress.UIElements
{
    public class BarLinksCollectionDynamicUIAdapter : BarLinksCollectionUIAdapter
    {
        private WorkItem rootWorkItem;

        public BarLinksCollectionDynamicUIAdapter(BarItemLinkCollection linkCollection,
            BarItems itemCollection, WorkItem rootWorkItem)
            : base(linkCollection, itemCollection)
        {
            this.rootWorkItem = rootWorkItem;
        }

        private void ValidateUiElement(BarItem uiElement)
        {
            // this will be called for the Ribbon Gallery - but we handle that separately.
            if (uiElement is RibbonGalleryBarItem)
                return;

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

        /// <summary>
        /// See <see cref="UIElementAdapter{TUIElement}.Add(TUIElement)"/> for more information.
        /// </summary>
        protected override BarItem Add(BarItem uiElement)
        {
            ValidateUiElement(uiElement);
            uiElement.ItemClick += new ItemClickEventHandler(ItemClick);
            return base.Add(uiElement);
        }

        private void ItemClick(object sender, ItemClickEventArgs e)
        {
            if (e.Item.Tag != null && e.Item.Tag is DynamicCommandEventLink)
            {
                DynamicCommandEventLink eventLink = e.Item.Tag as DynamicCommandEventLink;
                EventTopic eventTopic = rootWorkItem.EventTopics[eventLink.EventTopicName];
                if (eventTopic != null)
                    eventTopic.Fire(sender, new DataEventArgs<DynamicCommandEventLink>(eventLink), 
                        null, PublicationScope.Global);
            }
        }
    }
}
