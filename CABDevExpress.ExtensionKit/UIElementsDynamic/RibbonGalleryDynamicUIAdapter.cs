using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.EventBroker;

namespace CABDevExpress.UIElements
{
    /// <summary>
    /// An adapter that wraps a <see cref="RibbonGalleryBarItem"/> for use 
    /// as an <see cref="IUIElementAdapter"/>. Uses Events so that dynamic
    /// GalleryItems can be added.
    /// </summary>
    public class RibbonGalleryDynamicUIAdapter : RibbonGalleryUIAdapter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RibbonGalleryDynamicUIAdapter"/> class.
        /// </summary>
        /// <param name="ribbonGallery">The ribbon gallery.</param>
        /// <param name="rootWorkItem">The root work item.</param>
        public RibbonGalleryDynamicUIAdapter(RibbonGalleryBarItem ribbonGallery, WorkItem workItem)
            : base(ribbonGallery, workItem) { }

        /// <summary>
        /// Fired when any <see cref="GalleryItem"/> contained in this
        /// <see cref="RibbonGalleryBarItem"/> is clicked. If the
        /// GalleryItem.Tag property contains an active CommandName string,
        /// that command is fired.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="GalleryItemClickEventArgs"/>
        /// instance containing the event data.</param>
        protected override void ItemClick(object sender, GalleryItemClickEventArgs e)
        {
            if (e.Item.Tag != null && e.Item.Tag is DynamicCommandEventLink)
            {
                DynamicCommandEventLink eventLink = e.Item.Tag as DynamicCommandEventLink;
                EventTopic eventTopic = WorkItem.EventTopics[eventLink.EventTopicName];
                if (eventTopic != null)
                    eventTopic.Fire(sender, new DynamicEventArgs<DynamicCommandEventLink>(eventLink), 
                        null, PublicationScope.Global);
            }
        }
    }
}
