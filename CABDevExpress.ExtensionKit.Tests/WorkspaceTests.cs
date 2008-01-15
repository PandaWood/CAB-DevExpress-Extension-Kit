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
		readonly TestableSmartPart _smartPart = new TestableSmartPart();

		[Fact]
		public void CanShowAndCloseDockManagerWorkspace()
		{
			DockManagerWorkspace dockManager = new DockManagerWorkspace(new DockManager(new ContainerControl()));
			dockManager.Show(_smartPart);

			Assert.Equal(1, dockManager.DockPanels.Count);

			dockManager.Close(_smartPart);

			Assert.Equal(0, dockManager.DockPanels.Count);
		}

		[Fact]
		public void CanShowAndCloseXtraTabWorkspace()
		{
			Font tahoma9pt = new Font("Tahoma", 9.75f);

			XtraTabSmartPartInfo info = new XtraTabSmartPartInfo();
			info.Text = "text";
			info.PageHeaderFont = tahoma9pt;

			XtraTabWorkspace xtrab = new XtraTabWorkspace();
			xtrab.Show(_smartPart, info);

			Assert.Equal(1, xtrab.TabPages.Count);
			Assert.Equal(1, xtrab.SmartParts.Count);
			Assert.Equal("text", xtrab.SelectedTabPage.Text);
			Assert.Equal(tahoma9pt, xtrab.SelectedTabPage.Appearance.Header.Font);

			xtrab.Close(_smartPart);
			Assert.Equal(0, xtrab.TabPages.Count);
			Assert.Equal(0, xtrab.SmartParts.Count);
		}
	}
}