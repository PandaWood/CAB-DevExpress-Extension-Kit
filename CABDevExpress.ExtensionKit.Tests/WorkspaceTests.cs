using System;
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
			Assert.That(Object.Equals(1, dockManagerWorkspace.DockPanels.Count), Is.True);
			dockManagerWorkspace.Close(_smartPart);
			Assert.That(Object.Equals(0, dockManagerWorkspace.DockPanels.Count), Is.True);
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

			Assert.That(Object.Equals(1, dockManagerWorkspace.DockPanels.Count), Is.True);
			Assert.That(Object.Equals("Bob", dockManager.Panels[0].Name), Is.True);
			Assert.That(Object.Equals(DockingStyle.Bottom, dockManager.Panels[0].Dock), Is.True);
			dockManagerWorkspace.Close(_smartPart);
			Assert.That(Object.Equals(0, dockManagerWorkspace.DockPanels.Count), Is.True);
		}

		 [Test]
		public void CanShowAndClose_XtraTabWorkspace()
		{
			Font tahoma9ptFont = new Font("Tahoma", 9.75f);
			var smartPartInfo = new XtraTabSmartPartInfo {Text = "text", PageHeaderFont = tahoma9ptFont};
			var xtraTabWorkspace = new XtraTabWorkspace();
			xtraTabWorkspace.Show(_smartPart, smartPartInfo);

			Assert.That(Object.Equals(1, xtraTabWorkspace.TabPages.Count), Is.True);
			Assert.That(Object.Equals(1, xtraTabWorkspace.SmartParts.Count), Is.True);
			Assert.That(Object.Equals("text", xtraTabWorkspace.SelectedTabPage.Text), Is.True);
			Assert.That(Object.Equals(tahoma9ptFont, xtraTabWorkspace.SelectedTabPage.Appearance.Header.Font), Is.True);

			xtraTabWorkspace.Close(_smartPart);
			Assert.That(Object.Equals(0, xtraTabWorkspace.TabPages.Count), Is.True);
			Assert.That(Object.Equals(0, xtraTabWorkspace.SmartParts.Count), Is.True);
		}

		 [Test]
		public void CanShowAndCloseAndHide_XtraNavBarWorkspace()
		{
			var navbarWorkspace = new XtraNavBarWorkspace();
			var smartPartInfo = new XtraNavBarGroupSmartPartInfo {Title = "Test Title"};

			Assert.That(Object.Equals(0, navbarWorkspace.Groups.Count), Is.True);

			// show the workspace
			navbarWorkspace.Show(_smartPart, smartPartInfo);
            Assert.That(Object.Equals(1, navbarWorkspace.Groups.Count), Is.True);
			Assert.That(Object.Equals(NavBarGroupStyle.ControlContainer, navbarWorkspace.Groups[0].GroupStyle), Is.True);

			// hide and the group still exists, but not visible
			navbarWorkspace.Hide(_smartPart);

			Assert.That(Object.Equals(1, navbarWorkspace.Groups.Count), Is.True);
			Assert.That(navbarWorkspace.Groups[0].Visible, Is.False);

			// close removes
			navbarWorkspace.Close(_smartPart);
			Assert.That(Object.Equals(0, navbarWorkspace.Groups.Count), Is.True);
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

			Assert.That(Object.Equals(3, tabWorkspace.TabPages.Count), Is.True);
			Assert.That(Object.ReferenceEquals(smartPart3, tabWorkspace.ActiveSmartPart), Is.True);
        }
	}
}