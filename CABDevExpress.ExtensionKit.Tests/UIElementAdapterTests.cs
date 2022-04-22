using System;
using CABDevExpress.UIElements;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraNavBar;
using Microsoft.Practices.CompositeUI.UIElements;
using NUnit.Framework;

namespace CABDevExpress.ExtensionKit.Tests
{
	[TestFixture]
	public class UIElementAdapterTests
	{
		[Test]
		public void XtraBarUIAdapterFactory_CanReturn_CorrectType_For_Bar()
		{
			Bar bar = new Bar {Manager = new BarManager()};

			IUIElementAdapter adapter = new XtraBarUIAdapterFactory().GetAdapter(bar);
			Assert.IsInstanceOf(typeof(BarLinksCollectionUIAdapter), adapter);
		}

		[Test]
		public void XtraBarUIAdapterFactory_CanReturn_CorrectType_For_BarManager()
		{
			var barManager = new BarManager();
			IUIElementAdapter adapter = new XtraBarUIAdapterFactory().GetAdapter(barManager);

			Assert.IsInstanceOf(typeof(BarsUIAdapter), adapter);
		}

		 [Test]
		public void XtraNavBarUIAdapterFactory_CanReturn_CorrectType_For_NavBarControl()
		{
			var navBarControl = new NavBarControl();
			IUIElementAdapter adapter = new XtraNavBarUIAdapterFactory().GetAdapter(navBarControl);

			Assert.IsInstanceOf(typeof(NavBarGroupCollectionUIAdapter), adapter);
		}

		/// <summary>
		/// We're testing both add() and remove() within each test, it's not worth the effort to separate them
		/// </summary>
		 [Test]
		public void CanAddAndRemove_From_NavBarGroupCollectionUIAdapter()
		{
			var navBarControl = new NavBarControl();
			IUIElementAdapter adapter = new XtraNavBarUIAdapterFactory().GetAdapter(navBarControl);

			//add
			var navBarGroup = new NavBarGroup();
			var addedGroup = adapter.Add(navBarGroup);
			Assert.AreEqual(navBarGroup, addedGroup as NavBarGroup);
			Assert.AreEqual(1, navBarControl.Groups.Count);

			//remove
			adapter.Remove(addedGroup);
			Assert.AreEqual(0, navBarControl.Groups.Count);
		}

		 [Test]
		public void CanAddAndRemove_From_BarsUIAdapter()
		{
			var barManager = new BarManager();
			IUIElementAdapter adapter = new XtraBarUIAdapterFactory().GetAdapter(barManager);

			Assert.IsInstanceOf(typeof(BarsUIAdapter), adapter);
			
			//add
			Bar bar = new Bar(barManager);
			var addedBar = adapter.Add(bar);
			Assert.AreEqual(bar, addedBar as Bar);
			Assert.AreEqual(1, barManager.Bars.Count);

			//remove
			adapter.Remove(bar);
			Assert.AreEqual(0, barManager.Bars.Count);

			adapter.Remove(new Bar(barManager));    // ensure that attempting to remove a non-added object, does nothing
			Assert.AreEqual(0, barManager.Bars.Count);
		}

		 [Test]
		public void CanAddAndRemove_From_NavigatorCustomButtonUIAdapter()
		{
			var gridControl = new GridControl();
			NavigatorCustomButtons buttons = gridControl.EmbeddedNavigator.Buttons.CustomButtons;
			IUIElementAdapter adapter = new NavigatorCustomButtonUIAdapterFactory().GetAdapter(buttons);
			Assert.IsInstanceOf(typeof(NavigatorCustomButtonUIAdapter), adapter);

			//add
			var button = new NavigatorCustomButton(0);
			var addedButton = adapter.Add(button);
			Assert.AreEqual(button, addedButton as NavigatorCustomButton);
			Assert.AreEqual(1, gridControl.EmbeddedNavigator.Buttons.CustomButtons.Count);

			//remove
			adapter.Remove(button);
			Assert.AreEqual(0, gridControl.EmbeddedNavigator.Buttons.CustomButtons.Count);
		}

		 [Test]
		public void XtraNavBarUIAdapterFactory_Throws_Exception_WhenGiven_IncorrectType()
		{
			// use the wrong factory and expect an exception
			Assert.Throws<ArgumentException>(() => new XtraNavBarUIAdapterFactory().GetAdapter(new NavigatorCustomButton(0)));
		}

		 [Test]
		public void CanAddAndRemove_From_BarLinksOwnerCollectionUIAdapter()
		{	
			// Looking at the Adapter, the prerequisite setup is that the BarItem passed to the 
			// BarItemWrapper constructor, must have already been added to the BarManager

			Bar bar = new Bar {Manager = new BarManager()};
			BarItem editItem = new BarEditItem(bar.Manager);
			bar.ItemLinks.Add(editItem);	// add before passing to BarItemWrapper

			var barItemWrapper = new BarItemWrapper(bar.ItemLinks, editItem);
			IUIElementAdapter adapter = new XtraBarUIAdapterFactory().GetAdapter(barItemWrapper);
			Assert.IsInstanceOf(typeof(BarLinksOwnerCollectionUIAdapter), adapter);

			//add
			BarItem buttonItem = new BarButtonItem(bar.Manager, "test2");
			var objectAdded = adapter.Add(buttonItem);
			Assert.AreEqual(buttonItem, objectAdded as BarButtonItem);
			Assert.AreEqual(2, barItemWrapper.ItemLinks.Count);

			//remove
			adapter.Remove(buttonItem);
			Assert.AreEqual(1, barItemWrapper.ItemLinks.Count);

			adapter.Remove(editItem);
			Assert.AreEqual(0, barItemWrapper.ItemLinks.Count);
		}
	}
}
