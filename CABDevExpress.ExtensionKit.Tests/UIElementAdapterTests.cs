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

		[Fact]
		public void CanAddAndRemoveFromNavBarGroupCollectionUIAdapter()
		{
			NavBarControl navBarControl = new NavBarControl();
			IUIElementAdapter adapter = new XtraNavBarUIAdapterFactory().GetAdapter(navBarControl);

			NavBarGroup navBarGroup = new NavBarGroup();
			object addedGroup = adapter.Add(navBarGroup);
			Assert.Equal(navBarGroup, addedGroup as NavBarGroup);
			Assert.Equal(1, navBarControl.Groups.Count);

			adapter.Remove(addedGroup);
			Assert.Equal(0, navBarControl.Groups.Count);
		}

		[Fact]
		public void CanAddAndRemoveFromBarsUIAdapter()
		{
			BarManager barManager = new BarManager();
			IUIElementAdapter adapter = new XtraBarUIAdapterFactory().GetAdapter(barManager);

			Assert.IsType(typeof(BarsUIAdapter), adapter);
					
			Bar bar = new Bar(barManager);
			object addedBar = adapter.Add(bar);
			Assert.Equal(bar, addedBar as Bar);
			Assert.Equal(1, barManager.Bars.Count);

			adapter.Remove(bar);
			Assert.Equal(0, barManager.Bars.Count);

			adapter.Remove(new Bar(barManager));
			Assert.Equal(0, barManager.Bars.Count);
		}

		[Fact]
		public void NavigatorCustomButtonUIAdapterWorks()
		{
			
			GridControl grid = new GridControl();
			NavigatorCustomButtons buttons = grid .EmbeddedNavigator.Buttons.CustomButtons;
			IUIElementAdapter adapter = new NavigatorCustomButtonUIAdapterFactory().GetAdapter(buttons);

			Assert.IsType(typeof(NavigatorCustomButtonUIAdapter), adapter);

			NavigatorCustomButton button = new NavigatorCustomButton(0);

			object addedButton = adapter.Add(button);
			Assert.Equal(button, addedButton as NavigatorCustomButton);
			Assert.Equal(1, grid.EmbeddedNavigator.Buttons.CustomButtons.Count);

			adapter.Remove(button);
			Assert.Equal(0, grid.EmbeddedNavigator.Buttons.CustomButtons.Count);
		}

		[Fact]
		public void XtraNavBarUIAdapterFactoryThrowsExceptionWithUnsupported()
		{
			Assert.Throws<ArgumentException>(delegate
			                                 	{
			                                 		new XtraNavBarUIAdapterFactory().GetAdapter(new NavigatorCustomButton(0));
			                                 	});
		}
	}
}
