using System;
using CABDevExpress.UIElements;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraNavBar;
using Microsoft.Practices.CompositeUI.UIElements;
using Xunit;

namespace CABDevExpress.ExtensionKit.Tests
{
	public class UIElementAdapterTests
	{
		[Fact]
		public void XtraBarUIAdapterFactoryCanReturnCorrectTypeForBar()
		{
			Bar bar = new Bar();
			bar.Manager = new BarManager();

			IUIElementAdapter adapter = new XtraBarUIAdapterFactory().GetAdapter(bar);
			Assert.IsType(typeof(BarLinksCollectionUIAdapter), adapter);
		}

		[Fact]
		public void XtraBarUIAdapterFactoryCanReturnCorrectTypeForBarManager()
		{
			BarManager barManager = new BarManager();
			IUIElementAdapter adapter = new XtraBarUIAdapterFactory().GetAdapter(barManager);

			Assert.IsType(typeof(BarsUIAdapter), adapter);
		}

		[Fact]
		public void XtraNavBarUIAdapterFactoryCanReturnCorrectTypeForNavBarControl()
		{
			NavBarControl navBarControl = new NavBarControl();

			IUIElementAdapter adapter = new XtraNavBarUIAdapterFactory().GetAdapter(navBarControl);
			Assert.IsType(typeof(NavBarGroupCollectionUIAdapter), adapter);
		}

		/// <summary>
		/// We're testing both add() and remove() within each test, it's not worth the effort to separate them
		/// </summary>
		[Fact]
		public void CanAddAndRemoveFromNavBarGroupCollectionUIAdapter()
		{
			NavBarControl navBarControl = new NavBarControl();
			IUIElementAdapter adapter = new XtraNavBarUIAdapterFactory().GetAdapter(navBarControl);

			//add
			NavBarGroup navBarGroup = new NavBarGroup();
			object addedGroup = adapter.Add(navBarGroup);
			Assert.Equal(navBarGroup, addedGroup as NavBarGroup);
			Assert.Equal(1, navBarControl.Groups.Count);

			//remove
			adapter.Remove(addedGroup);
			Assert.Equal(0, navBarControl.Groups.Count);
		}

		[Fact]
		public void CanAddAndRemoveFromBarsUIAdapter()
		{
			BarManager barManager = new BarManager();
			IUIElementAdapter adapter = new XtraBarUIAdapterFactory().GetAdapter(barManager);

			Assert.IsType(typeof(BarsUIAdapter), adapter);
			
			//add
			Bar bar = new Bar(barManager);
			object addedBar = adapter.Add(bar);
			Assert.Equal(bar, addedBar as Bar);
			Assert.Equal(1, barManager.Bars.Count);

			//remove
			adapter.Remove(bar);
			Assert.Equal(0, barManager.Bars.Count);

			adapter.Remove(new Bar(barManager));	// ensure that attempting to remove a non-added object, does nothing
			Assert.Equal(0, barManager.Bars.Count);
		}

		[Fact]
		public void CanAddAndRemoveFromNavigatorCustomButtonUIAdapter()
		{
			GridControl grid = new GridControl();
			NavigatorCustomButtons buttons = grid .EmbeddedNavigator.Buttons.CustomButtons;
			IUIElementAdapter adapter = new NavigatorCustomButtonUIAdapterFactory().GetAdapter(buttons);
			Assert.IsType(typeof(NavigatorCustomButtonUIAdapter), adapter);

			//add
			NavigatorCustomButton button = new NavigatorCustomButton(0);
			object addedButton = adapter.Add(button);
			Assert.Equal(button, addedButton as NavigatorCustomButton);
			Assert.Equal(1, grid.EmbeddedNavigator.Buttons.CustomButtons.Count);

			//remove
			adapter.Remove(button);
			Assert.Equal(0, grid.EmbeddedNavigator.Buttons.CustomButtons.Count);
		}

		[Fact]
		public void XtraNavBarUIAdapterFactoryThrowsExceptionWithUnsupported()
		{
			Assert.Throws<ArgumentException>(delegate
			                                 	{	// use the wrong factory and expect an exception
			                                 		new XtraNavBarUIAdapterFactory().GetAdapter(new NavigatorCustomButton(0));
			                                 	});
		}

		[Fact]
		public void CanAddAndRemoveFromBarLinksOwnerCollectionUIAdapter()
		{	
			// Looking at the Adapter, the prerequisite setup is that the BarItem passed to the 
			// BarItemWrapper constructor, must have already been added to the BarManager

			Bar bar = new Bar();
			bar.Manager = new BarManager();
			BarItem editItem = new BarEditItem(bar.Manager);
			bar.ItemLinks.Add(editItem);	// add before passing to BarItemWrapper

			BarItemWrapper wrapper = new BarItemWrapper(bar.ItemLinks, editItem);
			IUIElementAdapter adapter = new XtraBarUIAdapterFactory().GetAdapter(wrapper);
			Assert.IsType(typeof(BarLinksOwnerCollectionUIAdapter), adapter);

			//add
			BarItem buttonItem = new BarButtonItem(bar.Manager, "test2");
			object objectAdded = adapter.Add(buttonItem);
			Assert.Equal<BarItem>(buttonItem, objectAdded as BarButtonItem);
			Assert.Equal(2, wrapper.ItemLinks.Count);

			//remove
			adapter.Remove(buttonItem);
			Assert.Equal(1, wrapper.ItemLinks.Count);

			adapter.Remove(editItem);
			Assert.Equal(0, wrapper.ItemLinks.Count);
		}
	}
}
