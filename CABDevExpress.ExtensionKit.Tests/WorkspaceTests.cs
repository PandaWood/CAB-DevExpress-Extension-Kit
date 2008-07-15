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
		public void CanShowAndClose_DockManagerWorkspace()
		{
			// the DockManager must be passed a ContainerControl or the Workspace won't handle it
			// not sure whether I should add something to the workspace to guard this....
			var dockManagerWorkspace = new DockManagerWorkspace(new DockManager(new ContainerControl()));
			dockManagerWorkspace.Show(_smartPart);

			Assert.Equal(1, dockManagerWorkspace.DockPanels.Count);

			dockManagerWorkspace.Close(_smartPart);

			Assert.Equal(0, dockManagerWorkspace.DockPanels.Count);
		}

		[Fact]
		public void CanShow_DockManagerWorkspace_If_PanelName_Is_NotNull()
		{
			var dockManagerControl = new DockManager(new ContainerControl());
			var dockManagerWorkspace = new DockManagerWorkspace(dockManagerControl);
			var smartPartInfo = new DockManagerSmartPartInfo
			                    	{
			                    		ParentPanelName = "PanelBob",
			                    		Name = "Bob",
			                    		Dock = DockingStyle.Bottom
			                    	};
			dockManagerWorkspace.Show(_smartPart, smartPartInfo);

			Assert.Equal(1, dockManagerWorkspace.DockPanels.Count);
			Assert.Equal("Bob", dockManagerControl.Panels[0].Name);

			Assert.Equal(DockingStyle.Bottom, dockManagerControl.Panels[0].Dock);

			dockManagerWorkspace.Close(_smartPart);

			Assert.Equal(0, dockManagerWorkspace.DockPanels.Count);
		}

		[Fact]
		public void CanShowAndClose_XtraTabWorkspace()
		{
			Font tahoma9pt = new Font("Tahoma", 9.75f);
			var smartPartInfo = new XtraTabSmartPartInfo {Text = "text", PageHeaderFont = tahoma9pt};
			var xtrab = new XtraTabWorkspace();
			xtrab.Show(_smartPart, smartPartInfo);

			Assert.Equal(1, xtrab.TabPages.Count);
			Assert.Equal(1, xtrab.SmartParts.Count);
			Assert.Equal("text", xtrab.SelectedTabPage.Text);
			Assert.Equal(tahoma9pt, xtrab.SelectedTabPage.Appearance.Header.Font);

			xtrab.Close(_smartPart);
			Assert.Equal(0, xtrab.TabPages.Count);
			Assert.Equal(0, xtrab.SmartParts.Count);
		}

		[Fact]
		public void CanShowAndCloseAndHide_XtraNavBarWorkspace()
		{
			var navbarWorkspace = new XtraNavBarWorkspace();
			var smartPartInfo = new XtraNavBarGroupSmartPartInfo {Title = "Test Title"};
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
		public void CanMove_AndThen_Activate_Correct_SmartParts()
		{
			var tabWorkspace = new XtraTabWorkspace();
			var smartPart1 = new TestableSmartPart {Name = "smartpart1"};
			var smartPart2 = new TestableSmartPart {Name = "smartpart1"};
			var smartPart3 = new TestableSmartPart {Name = "smartpart1"};

			tabWorkspace.Show(smartPart1);
			tabWorkspace.Show(smartPart2);
			tabWorkspace.Show(smartPart3);

			Assert.Equal(3, tabWorkspace.TabPages.Count);
			Assert.Equal(tabWorkspace.ActiveSmartPart, smartPart3);

			tabWorkspace.TabPages.Move(2, tabWorkspace.TabPages[0]);
		}
	}
}