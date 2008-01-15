using System.Drawing;
using System.Windows.Forms;
using CABDevExpress.SmartPartInfos;
using CABDevExpress.Workspaces;
using DevExpress.XtraBars.Docking;
using Xunit;

namespace CABDevExpress.ExtensionKit.Tests
{
	public class WorkspaceTests
	{
		readonly TestableWorkItem _workItem = new TestableWorkItem();
		readonly TestableSmartPart _smartPart = new TestableSmartPart();

		const string TestableSmartPart = "TestableSmartPart";

		[Fact]
		public void CanSomethingDockManagerWorkspace()
		{
			//TODO this condition below, won't work so create an exception for the user to know
//			DockManagerWorkspace dockManager = new DockManagerWorkspace(new DockManager());
			DockManagerWorkspace dockManager = new DockManagerWorkspace(new DockManager(new ContainerControl()));
			dockManager.Show(_smartPart);

			Assert.Equal(1, dockManager.DockPanels.Count);
		}

		[Fact]
		public void CanCreateXtraTabWorkspace()
		{
			XtraTabWorkspace xtrab = new XtraTabWorkspace();
			XtraTabSmartPartInfo info = new XtraTabSmartPartInfo();

			info.Text = "text";
			info.PageHeaderFont = new Font("Tahoma", 9.75f);

			xtrab.Show(_smartPart, info);

			Assert.Equal(1, xtrab.TabPages.Count);
			Assert.Equal(1, xtrab.SmartParts.Count);
		}
	}
}