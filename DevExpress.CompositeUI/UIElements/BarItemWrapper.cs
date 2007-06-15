using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.XtraBars;

namespace DevExpress.CompositeUI.UIElements
{
    public class BarItemWrapper
    {
        public BarItemWrapper(BarItemLinkCollection itemLinkCollection, BarItem item)
        {
            this.item = item;
            this.itemLinks = itemLinkCollection;
        }

        private BarItem item;

        public BarItem Item
        {
            get { return item; }
        }

        private BarItemLinkCollection itemLinks;

        public BarItemLinkCollection ItemLinks
        {
            get { return itemLinks; }
        }

    }
}
