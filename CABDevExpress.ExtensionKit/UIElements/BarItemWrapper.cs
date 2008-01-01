using DevExpress.XtraBars;

namespace CABDevExpress.UIElements
{
	public class BarItemWrapper
	{
		private readonly BarItem _item;
		private readonly BarItemLinkCollection _itemLinks;

		public BarItemWrapper(BarItemLinkCollection itemLinkCollection, BarItem item)
		{
			_item = item;
			_itemLinks = itemLinkCollection;
		}

		public BarItem Item
		{
			get { return _item; }
		}

		public BarItemLinkCollection ItemLinks
		{
			get { return _itemLinks; }
		}
	}
}