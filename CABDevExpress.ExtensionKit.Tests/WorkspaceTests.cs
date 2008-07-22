using System.Drawing;
using System.Windows.Forms;
using CABDevExpress.SmartPartInfos;
using CABDevExpress.Workspaces;
using DevExpress.XtraBars.Docking;
using DevExpress.XtraNavBar;
using Xunit;
using XunitExt;

namespace CABDevExpress.ExtensionKit.Tests
{
	public class WorkspaceTests
	{
		readonly TestableSmartPart _smartPart = new TestableSmartPart();

		[Fact]
		public void CanShowAndClose_DockManagerWorkspace()
		{
			// the DockManager must be passed a ContainerControl or the Workspace won't handle it
			// perhaps we should add something to the workspace to guard this....
			var dockManagerWorkspace = new DockManagerWorkspace(new DockManager(new ContainerControl()));
			dockManagerWorkspace.Show(_smartPart);

			dockManagerWorkspace.DockPanels.Count.ShouldEqual(1);
			dockManagerWorkspace.Close(_smartPart);
			dockManagerWorkspace.DockPanels.Count.ShouldEqual(0);
		}

		[Fact]
		public void CanShow_DockManagerWorkspace_If_PanelName_Is_NotNull()
		{
			var dockManager = new DockManager(new ContainerControl());
			var dockManagerWorkspace = new DockManagerWorkspace(dockManager);
			var smartPartInfo = new DockManagerSmartPartInfo
			                    	{
			                    		ParentPanelName = "PanelBob",
			                    		Name = "Bob",
			                    		Dock = DockingStyle.Bottom
			                    	};
			dockManagerWorkspace.Show(_smartPart, smartPartInfo);

			dockManagerWorkspace.DockPanels.Count.ShouldEqual(1);
			dockManager.Panels[0].Name.ShouldEqual("Bob");
			dockManager.Panels[0].Dock.ShouldEqual(DockingStyle.Bottom);

			dockManagerWorkspace.Close(_smartPart);

			dockManagerWorkspace.DockPanels.Count.ShouldEqual(0);
		}

		[Fact]
		public void CanShowAndClose_XtraTabWorkspace()
		{
			Font tahoma9ptFont = new Font("Tahoma", 9.75f);
			var smartPartInfo = new XtraTabSmartPartInfo {Text = "text", PageHeaderFont = tahoma9ptFont};
			var xtraTabWorkspace = new XtraTabWorkspace();
			xtraTabWorkspace.Show(_smartPart, smartPartInfo);

			xtraTabWorkspace.TabPages.Count.ShouldEqual(1);
			xtraTabWorkspace.SmartParts.Count.ShouldEqual(1);
			xtraTabWorkspace.SelectedTabPage.Text.ShouldEqual("text");
			xtraTabWorkspace.SelectedTabPage.Appearance.Header.Font.ShouldEqual(tahoma9ptFont);

			xtraTabWorkspace.Close(_smartPart);
			xtraTabWorkspace.TabPages.Count.ShouldEqual(0);
			xtraTabWorkspace.SmartParts.Count.ShouldEqual(0);
		}

		[Fact]
		public void CanShowAndCloseAndHide_XtraNavBarWorkspace()
		{
			var navbarWorkspace = new XtraNavBarWorkspace();
			var smartPartInfo = new XtraNavBarGroupSmartPartInfo {Title = "Test Title"};

			navbarWorkspace.Groups.Count.ShouldEqual(0);

			// show the workspace
			navbarWorkspace.Show(_smartPart, smartPartInfo);
			navbarWorkspace.Groups.Count.ShouldEqual(1);
			navbarWorkspace.Groups[0].GroupStyle.ShouldEqual(NavBarGroupStyle.ControlContainer);

			// hide and the group still exists, but not visible
			navbarWorkspace.Hide(_smartPart);

			navbarWorkspace.Groups.Count.ShouldEqual(1);
			navbarWorkspace.Groups[0].Visible.ShouldBeFalse();

			// close removes
			navbarWorkspace.Close(_smartPart);
			navbarWorkspace.Groups.Count.ShouldEqual(0);
		}

		[Fact]
		public void CanMove_AndThen_Activate_Correct_SmartPart()
		{
			var tabWorkspace = new XtraTabWorkspace();
			var smartPart1 = new TestableSmartPart {Name = "smartpart1"};
			var smartPart2 = new TestableSmartPart {Name = "smartpart2"};
			var smartPart3 = new TestableSmartPart {Name = "smartpart3"};

			tabWorkspace.Show(smartPart1);
			tabWorkspace.Show(smartPart2);
			tabWorkspace.Show(smartPart3);

			tabWorkspace.TabPages.Count.ShouldEqual(3);
			tabWorkspace.ActiveSmartPart.ShouldBeSameAs(smartPart3);
		}
	}
}