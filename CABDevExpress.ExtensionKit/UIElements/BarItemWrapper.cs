using DevExpress.XtraBars;

namespace CABDevExpress.UIElements
{
	///<summary>
	/// A wrapper around the BarItem and BarItemLinkCollection combination of classes
	///</summary>
	public class BarItemWrapper
	{
		private readonly BarItem barItem;
		private readonly BarItemLinkCollection barItemLinks;

		///<summary>
		/// constructor
		///</summary>
		public BarItemWrapper(BarItemLinkCollection itemLinkCollection, BarItem barItem)
		{
			this.barItem = barItem;
			barItemLinks = itemLinkCollection;
		}

		///<summary>
		/// 
		///</summary>
		public BarItem Item
		{
			get { return barItem; }
		}

		///<summary>
		/// 
		///</summary>
		public BarItemLinkCollection ItemLinks
		{
			get { return barItemLinks; }
		}
	}
}