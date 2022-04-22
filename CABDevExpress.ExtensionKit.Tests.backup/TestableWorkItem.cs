using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.ObjectBuilder;

namespace CABDevExpress.ExtensionKit.Tests
{
	public class TestableWorkItem : WorkItem
	{
		public TestableWorkItem()
		{
			InitializeRootWorkItem(new Builder());
		}
	}
}