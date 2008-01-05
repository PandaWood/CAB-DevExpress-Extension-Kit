using CABDevExpress.UIElements;
using DevExpress.XtraBars;
using DevExpress.XtraNavBar;
using Microsoft.Practices.CompositeUI.UIElements;
using Xunit;

namespace CABDevExpress.ExtensionKit.Tests
{
	public class UIElementAdapterTests
	{
		[Fact]
		public void XtraBarUIAdapterFactoryCanReturnCorrectTypeForBar()
		{
			Bar bar = new Bar();
			bar.Manager = new BarManager();

			IUIElementAdapter adapter = new XtraBarUIAdapterFactory().GetAdapter(bar);
			Assert.IsType(typeof(BarLinksCollectionUIAdapter), adapter);
		}

		[Fact]
		public void XtraBarUIAdapterFactoryCanReturnCorrectTypeForBarManager()
		{
			BarManager barManager = new BarManager();
			IUIElementAdapter adapter = new XtraBarUIAdapterFactory().GetAdapter(barManager);

			Assert.IsType(typeof(BarsUIAdapter), adapter);
		}

		[Fact]
		public void XtraNavBarUIAdapterFactoryCanReturnCorrectTypeForNavBarControl()
		{
			NavBarControl navBarControl = new NavBarControl();

			IUIElementAdapter adapter = new XtraNavBarUIAdapterFactory().GetAdapter(navBarControl);
			Assert.IsType(typeof(NavBarGroupCollectionUIAdapter), adapter);
		}

		[Fact]
		public void CanAddAndRemoveFromNavBarGroupCollectionUIAdapter()
		{
			NavBarControl navBarControl = new NavBarControl();
			IUIElementAdapter adapter = new XtraNavBarUIAdapterFactory().GetAdapter(navBarControl);

			NavBarGroup navBarGroup = new NavBarGroup();
			object addedGroup = adapter.Add(navBarGroup);
			Assert.Equal(navBarGroup, addedGroup as NavBarGroup);
			adapter.Remove(addedGroup);
		}

		[Fact]
		public void CanAddAndRemoveFromBarsUIAdapter()
		{
			BarManager barManager = new BarManager();
			IUIElementAdapter adapter = new XtraBarUIAdapterFactory().GetAdapter(barManager);

			Assert.IsType(typeof(BarsUIAdapter), adapter);
					
			Bar bar = new Bar(barManager);
			object addedBar = adapter.Add(bar);
			Assert.Equal(bar, addedBar as Bar);
			adapter.Remove(bar);		// TODO we would like to confirm it was actually removed from the underlying collection
		}
	}
}
