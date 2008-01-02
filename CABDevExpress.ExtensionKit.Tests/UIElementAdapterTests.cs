using CABDevExpress.UIElements;
using DevExpress.XtraBars;
using Xunit;

namespace CABDevExpress.ExtensionKit.Tests
{
	public class UIElementAdapterTests
	{
		[Fact]
		public void XtraBarUIAdapterFactoryCanReturnCorrectType()
		{
			Bar navBar = new Bar();
			navBar.Manager = new BarManager();

			XtraBarUIAdapterFactory factory = new XtraBarUIAdapterFactory();
			Assert.IsType(typeof(BarLinksCollectionUIAdapter), factory.GetAdapter(navBar));
		}
	}
}
