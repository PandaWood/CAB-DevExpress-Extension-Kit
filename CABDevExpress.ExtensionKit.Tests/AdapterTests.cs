using CABDevExpress.Adapters;
using DevExpress.XtraBars.Ribbon;
using Xunit;

namespace CABDevExpress.ExtensionKit.Tests
{
	public class AdapterTests
	{
		[Fact]
		public void RibbonUIAdapterCanAdd()
		{
			RibbonControl theRibbon = new RibbonControl();

			RibbonUIAdapter adapter = new RibbonUIAdapter(theRibbon);
			RibbonControl returnedRibbon = (RibbonControl) adapter.Add(theRibbon);
			Assert.Equal(theRibbon, returnedRibbon);
		}

		[Fact]
		public void RibbonUIAdapterCanRemove()
		{
			RibbonControl theRibbon = new RibbonControl();

			RibbonUIAdapter adapter = new RibbonUIAdapter(theRibbon);
			adapter.Remove(theRibbon);		// we would like this test to contain an assert
		}
	}
}
