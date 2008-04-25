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
		public void Can_ShowAndClose_DockManagerWorkspace()
		{
			// the DockManager must be passed a ContainerControl or the Workspace won't handle it
			// not sure whether I should add something to the workspace to guard this....
			DockManagerWorkspace dockManagerWorkspace = new DockManagerWorkspace(new DockManager(new ContainerControl()));
			dockManagerWorkspace.Show(_smartPart);

			Assert.Equal(1, dockManagerWorkspace.DockPanels.Count);

			dockManagerWorkspace.Close(_smartPart);

			Assert.Equal(0, dockManagerWorkspace.DockPanels.Count);
		}

		[Fact]
		public void Can_Show_DockManagerWorkspace_If_PanelName_Is_NotNull()
		{
			DockManager dockManagerControl = new DockManager(new ContainerControl());
			DockManagerWorkspace dockManagerWorkspace = new DockManagerWorkspace(dockManagerControl);
			DockManagerSmartPartInfo info = new DockManagerSmartPartInfo();

			info.ParentPanelName = "PanelBob";		
			info.Name = "Bob";
			info.Dock = DockingStyle.Bottom;
			dockManagerWorkspace.Show(_smartPart, info);

			Assert.Equal(1, dockManagerWorkspace.DockPanels.Count);
			Assert.Equal("Bob", dockManagerControl.Panels[0].Name);

			Assert.Equal(DockingStyle.Bottom, dockManagerControl.Panels[0].Dock);

			dockManagerWorkspace.Close(_smartPart);

			Assert.Equal(0, dockManagerWorkspace.DockPanels.Count);
		}

		[Fact]
		public void Can_ShowAndClose_XtraTabWorkspace()
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
		public void Can_ShowAndCloseAndHide_XtraNavBarWorkspace()
		{
			XtraNavBarWorkspace navbarWorkspace = new XtraNavBarWorkspace();
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

		[Fact]
		public void Can_Move_AndThen_Activate_Correct_SmartParts()
		{
			XtraTabWorkspace tabWorkspace = new XtraTabWorkspace();

			TestableSmartPart smartPart1 = new TestableSmartPart();
			smartPart1.Name = "smartpart1";
			TestableSmartPart smartPart2 = new TestableSmartPart();
			smartPart2.Name = "smartpart1";
			TestableSmartPart smartPart3 = new TestableSmartPart();
			smartPart3.Name = "smartpart1";

			tabWorkspace.Show(smartPart1);
			tabWorkspace.Show(smartPart2);
			tabWorkspace.Show(smartPart3);

			Assert.Equal(3, tabWorkspace.TabPages.Count);
			Assert.Equal<object>(tabWorkspace.ActiveSmartPart, smartPart3);

			tabWorkspace.TabPages.Move(2, tabWorkspace.TabPages[0]);
		}
	}
}