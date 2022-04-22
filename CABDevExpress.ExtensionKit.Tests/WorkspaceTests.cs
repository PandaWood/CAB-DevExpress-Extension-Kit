using System.Drawing;
using System.Windows.Forms;
using CABDevExpress.SmartPartInfos;
using CABDevExpress.Workspaces;
using DevExpress.XtraBars.Docking;
using DevExpress.XtraNavBar;
using NUnit.Framework;

namespace CABDevExpress.ExtensionKit.Tests
{
	/// <summary>
	/// These are pretty basic/fundamental tests at this stage (the project started with no tests)
	/// Would like to grow this suite of tests to give us enough coverage to make changes with confidence
	/// </summary>
	[TestFixture]
	public class WorkspaceTests
	{
		private static TestableSmartPart _smartPart = null;
		[SetUp]
		public void Setup()
		{
			_smartPart = new TestableSmartPart();
		}
		[TearDown]
		public void TearDown()
		{
			_smartPart = null;
		}

		[Test]
		public void CanShowAndClose_DockManagerWorkspace()
		{
			// the DockManager must be passed a ContainerControl or the Workspace won't handle it
			// perhaps we should add something to the workspace to guard this....
			var dockManagerWorkspace = new DockManagerWorkspace(new DockManager(new ContainerControl()));
			dockManagerWorkspace.Show(_smartPart);
			Assert.AreEqual(1, dockManagerWorkspace.DockPanels.Count);
			dockManagerWorkspace.Close(_smartPart);
			Assert.AreEqual(0, dockManagerWorkspace.DockPanels.Count);
		}

		
		/// <summary>
		/// I've taken the DockManagerWorkspace (as already written) and tried to 
		/// understand certain things it does. I found this condition where the ParentPanelName being null
		/// took a different course of action, hence wrote this test to confirm that this code-path works.
		/// This test shouldn't be taken as a good reason for this condition to exist
		/// </summary>
		 [Test]
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

			Assert.AreEqual(1, dockManagerWorkspace.DockPanels.Count);
			Assert.AreEqual("Bob", dockManager.Panels[0].Name);
			Assert.AreEqual(DockingStyle.Bottom, dockManager.Panels[0].Dock);
			dockManagerWorkspace.Close(_smartPart);
			Assert.AreEqual(0, dockManagerWorkspace.DockPanels.Count);
		}

		 [Test]
		public void CanShowAndClose_XtraTabWorkspace()
		{
			Font tahoma9ptFont = new Font("Tahoma", 9.75f);
			var smartPartInfo = new XtraTabSmartPartInfo {Text = "text", PageHeaderFont = tahoma9ptFont};
			var xtraTabWorkspace = new XtraTabWorkspace();
			xtraTabWorkspace.Show(_smartPart, smartPartInfo);

			Assert.AreEqual(1, xtraTabWorkspace.TabPages.Count);
			Assert.AreEqual(1, xtraTabWorkspace.SmartParts.Count);
			Assert.AreEqual("text", xtraTabWorkspace.SelectedTabPage.Text);
			Assert.AreEqual(tahoma9ptFont, xtraTabWorkspace.SelectedTabPage.Appearance.Header.Font);

			xtraTabWorkspace.Close(_smartPart);
			Assert.AreEqual(0, xtraTabWorkspace.TabPages.Count);
			Assert.AreEqual(0, xtraTabWorkspace.SmartParts.Count);
		}

		 [Test]
		public void CanShowAndCloseAndHide_XtraNavBarWorkspace()
		{
			var navbarWorkspace = new XtraNavBarWorkspace();
			var smartPartInfo = new XtraNavBarGroupSmartPartInfo {Title = "Test Title"};

			Assert.AreEqual(0, navbarWorkspace.Groups.Count);

			// show the workspace
			navbarWorkspace.Show(_smartPart, smartPartInfo);
			Assert.AreEqual(1, navbarWorkspace.Groups.Count);
			Assert.AreEqual(NavBarGroupStyle.ControlContainer, navbarWorkspace.Groups[0].GroupStyle);

			// hide and the group still exists, but not visible
			navbarWorkspace.Hide(_smartPart);

			Assert.AreEqual(1, navbarWorkspace.Groups.Count);
			Assert.IsFalse(navbarWorkspace.Groups[0].Visible);

			// close removes
			navbarWorkspace.Close(_smartPart);
			Assert.AreEqual(0, navbarWorkspace.Groups.Count);
		}

		 [Test]
		public void CanMove_AndThen_Activate_Correct_SmartPart()
		{
			var tabWorkspace = new XtraTabWorkspace();
			var smartPart1 = new TestableSmartPart {Name = "smartpart1"};
			var smartPart2 = new TestableSmartPart {Name = "smartpart2"};
			var smartPart3 = new TestableSmartPart {Name = "smartpart3"};

			tabWorkspace.Show(smartPart1);
			tabWorkspace.Show(smartPart2);
			tabWorkspace.Show(smartPart3);

			Assert.AreEqual(3, tabWorkspace.TabPages.Count);
			Assert.AreSame(smartPart3, tabWorkspace.ActiveSmartPart);
		}
	}
}