using CABDevExpress.Workspaces;
using DevExpress.XtraBars.Docking;
using Xunit;

namespace CABDevExpress.ExtensionKit.Tests
{
	public class WorkspaceTests
	{
		const string TestableSmartPart = "TestableSmartPart";

		[Fact(Skip="Working on this, it's not right yet")]
		public void CanSomethingDockManagerWorkspace()
		{
			TestableWorkItem workItem = new TestableWorkItem();
			TestableSmartPart smartPart = new TestableSmartPart();
			workItem.SmartParts.Add(smartPart, TestableSmartPart);

			DockManagerWorkspace dockManager = new DockManagerWorkspace(new DockManager());
			dockManager.Show(workItem.SmartParts.Get(TestableSmartPart));

			Assert.Equal(1, dockManager.DockPanels.Count);
		}
	}
}