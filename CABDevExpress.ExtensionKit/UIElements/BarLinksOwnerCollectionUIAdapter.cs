using System;
using DevExpress.XtraBars;

namespace CABDevExpress.UIElements
{
    class BarLinksOwnerCollectionUIAdapter : BarLinksCollectionUIAdapter
    {
	/// <summary>
	/// Provides an adapter for BarItems where new items will be added to the item's owner collection, 
	/// after the item to which the adapter is attached.
	/// </summary>
	readonly BarItem item;

		/// <summary>
        /// Initializes a new instance of the <see cref="BarLinksOwnerCollectionUIAdapter"/> using the
		/// specified item.
		/// </summary>
		/// <param name="item"></param>
        public BarLinksOwnerCollectionUIAdapter(BarItem item, BarItemLinkCollection linkCollection)
			: base(linkCollection, item.Manager.Items)
		{

            this.item = item;
			//item.OwnerChanged += new EventHandler(item_OwnerChanged);
		}

		/// <summary>
        /// Returns the index immediately after the <see cref="BarItemLink"/> that
		/// was provided to the constructor.
		/// </summary>
		/// <param name="uiElement"></param>
		/// <returns></returns>
		protected override int GetInsertingIndex(object uiElement)
		{
			int index  = 0;
            foreach (BarItemLink barItemLink in linkCollection) 
            {
                if (barItemLink.Item == this.item)
                    break;
                index++;
            }
			return  ++index;
		}

		//TODO remove this or implement?
		private void item_OwnerChanged(object sender, EventArgs e)
		{
            //if (item.Owner == null)
            //    InternalCollection = null;
            //else
            //    InternalCollection = item.Owner.Items;
		}
    }
}

