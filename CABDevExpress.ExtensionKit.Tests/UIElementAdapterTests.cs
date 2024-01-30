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
			Assert.That(adapter?.GetType()==typeof(BarLinksCollectionUIAdapter), Is.True);
		}

		[Test]
		public void XtraBarUIAdapterFactory_CanReturn_CorrectType_For_BarManager()
		{
			var barManager = new BarManager();
			IUIElementAdapter adapter = new XtraBarUIAdapterFactory().GetAdapter(barManager);

			Assert.That(adapter?.GetType() == typeof(BarsUIAdapter), Is.True);
		}

		 [Test]
		public void XtraNavBarUIAdapterFactory_CanReturn_CorrectType_For_NavBarControl()
		{
			var navBarControl = new NavBarControl();
			IUIElementAdapter adapter = new XtraNavBarUIAdapterFactory().GetAdapter(navBarControl);

            Assert.That(adapter?.GetType() == typeof(NavBarGroupCollectionUIAdapter), Is.True);
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
			Assert.That(Object.Equals(navBarGroup, addedGroup as NavBarGroup), Is.True);
			Assert.That(Object.Equals(1, navBarControl.Groups.Count), Is.True);

			//remove
			adapter.Remove(addedGroup);
			Assert.That(Object.Equals(0, navBarControl.Groups.Count), Is.True);
		}

		 [Test]
		public void CanAddAndRemove_From_BarsUIAdapter()
		{
			var barManager = new BarManager();
			IUIElementAdapter adapter = new XtraBarUIAdapterFactory().GetAdapter(barManager);

            Assert.That(adapter?.GetType() == typeof(BarsUIAdapter), Is.True);
			
			//add
			Bar bar = new Bar(barManager);
			var addedBar = adapter.Add(bar);
			Assert.That(Object.Equals(bar, addedBar as Bar), Is.True);
			Assert.That(Object.Equals(1, barManager.Bars.Count), Is.True);

			//remove
			adapter.Remove(bar);
			Assert.That(Object.Equals(0, barManager.Bars.Count), Is.True);

			adapter.Remove(new Bar(barManager));    // ensure that attempting to remove a non-added object, does nothing
			Assert.That(Object.Equals(0, barManager.Bars.Count), Is.True);
		}

		 [Test]
		public void CanAddAndRemove_From_NavigatorCustomButtonUIAdapter()
		{
			var gridControl = new GridControl();
			NavigatorCustomButtons buttons = gridControl.EmbeddedNavigator.Buttons.CustomButtons;
			IUIElementAdapter adapter = new NavigatorCustomButtonUIAdapterFactory().GetAdapter(buttons);
            Assert.That(adapter?.GetType() == typeof(NavigatorCustomButtonUIAdapter), Is.True);

			//add
			var button = new NavigatorCustomButton(0);
			var addedButton = adapter.Add(button);
			Assert.That(Object.Equals(button, addedButton as NavigatorCustomButton), Is.True);
			Assert.That(Object.Equals(1, gridControl.EmbeddedNavigator.Buttons.CustomButtons.Count), Is.True);

			//remove
			adapter.Remove(button);
			Assert.That(Object.Equals(0, gridControl.EmbeddedNavigator.Buttons.CustomButtons.Count), Is.True);
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
			Assert.That(adapter?.GetType() == typeof(BarLinksOwnerCollectionUIAdapter), Is.True);

			//add
			BarItem buttonItem = new BarButtonItem(bar.Manager, "test2");
			var objectAdded = adapter.Add(buttonItem);
			Assert.That(Object.Equals(buttonItem, objectAdded as BarButtonItem), Is.True);
			Assert.That(Object.Equals(2, barItemWrapper.ItemLinks.Count), Is.True);

			//remove
			adapter.Remove(buttonItem);
			Assert.That(Object.Equals(1, barItemWrapper.ItemLinks.Count), Is.True);

			adapter.Remove(editItem);
			Assert.That(Object.Equals(0, barItemWrapper.ItemLinks.Count), Is.True);
		}
	}
}
