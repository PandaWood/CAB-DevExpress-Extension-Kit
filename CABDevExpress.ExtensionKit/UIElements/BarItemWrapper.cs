using DevExpress.XtraBars;

namespace CABDevExpress.UIElements
{
    public class BarItemWrapper
    {
        public BarItemWrapper(BarItemLinkCollection itemLinkCollection, BarItem item)
        {
            this.item = item;
            this.itemLinks = itemLinkCollection;
        }

        private readonly BarItem item;

        public BarItem Item
        {
            get { return item; }
        }

        private readonly BarItemLinkCollection itemLinks;

        public BarItemLinkCollection ItemLinks
        {
            get { return itemLinks; }
        }
    }
}
