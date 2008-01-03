using DevExpress.XtraBars;

namespace CABDevExpress.UIElements
{
	public class BarItemWrapper
	{
		private readonly BarItem barItem;
		private readonly BarItemLinkCollection barItemLinks;

		public BarItemWrapper(BarItemLinkCollection itemLinkCollection, BarItem barItem)
		{
			this.barItem = barItem;
			barItemLinks = itemLinkCollection;
		}

		public BarItem Item
		{
			get { return barItem; }
		}

		public BarItemLinkCollection ItemLinks
		{
			get { return barItemLinks; }
		}
	}
}