using System.Drawing;
using System.Windows.Forms;
using CABDevExpress.SmartPartInfos;
using CABDevExpress.Workspaces;
using DevExpress.XtraBars.Docking;
using DevExpress.XtraNavBar;
using Xunit;

namespace CABDevExpress.ExtensionKit.Tests
{
	public class WorkspaceTests
	{
		readonly TestableSmartPart _smartPart = new TestableSmartPart();

		[Fact]
		public void CanShowAndCloseDockManagerWorkspace()
		{
			// the DockManager must be passed a ContainerControl or the Workspace won't handle it
			// not sure whether I should add something to the workspace to guard this....
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

		[Fact]
		public void CanShowAndCloseAndHideXtraNavBarWorkspace()
		{
			XtraNavBarWorkspace navbarWorkspace= new XtraNavBarWorkspace();
			XtraNavBarGroupSmartPartInfo smartPartInfo = new XtraNavBarGroupSmartPartInfo();
			smartPartInfo.Title = "Test Title";
			Assert.Equal(0, navbarWorkspace.Groups.Count);

			// show the workspace
			navbarWorkspace.Show(_smartPart, smartPartInfo);
			Assert.Equal(1, navbarWorkspace.Groups.Count);
			Assert.Equal(NavBarGroupStyle.ControlContainer, navbarWorkspace.Groups[0].GroupStyle);

			// hide and the group still exists, but not visible
			navbarWorkspace.Hide(_smartPart);
			Assert.Equal(1, navbarWorkspace.Groups.Count);
			Assert.False(navbarWorkspace.Groups[0].Visible);

			// close removes
			navbarWorkspace.Close(_smartPart);
			Assert.Equal(0, navbarWorkspace.Groups.Count);
		}

		/// <summary>
		/// Item # 8506 - this passes, I've probably misunderstood the issue
		/// </summary>
		[Fact]
		public void CanShowAndMaintainActivateTabStatusAsFalse()
		{
			XtraTabSmartPartInfo info = new XtraTabSmartPartInfo();
			info.ActivateTab = false;

			XtraTabWorkspace xtrab = new XtraTabWorkspace();
			xtrab.Show(_smartPart, info);

			Assert.False(info.ActivateTab);
		}
	}
}