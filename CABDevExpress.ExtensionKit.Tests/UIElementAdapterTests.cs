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
		public void XtraBarUIAdapterFactoryCanReturnCorrectType()
		{
			Bar bar = new Bar();
			bar.Manager = new BarManager();

			XtraBarUIAdapterFactory factory = new XtraBarUIAdapterFactory();
			Assert.IsType(typeof(BarLinksCollectionUIAdapter), factory.GetAdapter(bar));
		}

		[Fact]
		public void XtraNavBarUIAdapterFactoryCanReturnCorrectType()
		{
			NavBarControl navBarControl = new NavBarControl();

			XtraNavBarUIAdapterFactory factory = new XtraNavBarUIAdapterFactory();
			IUIElementAdapter adapter = factory.GetAdapter(navBarControl);
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
	}
}
