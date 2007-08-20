using System;
using System.Configuration;
using DevExpress.XtraBars;
using Microsoft.Practices.CompositeUI;

namespace BankShell
{
	/// <summary>
	/// This is a temporary implementation that will be replaced with something
	/// richer when we move it into the framework.
	/// </summary>
	public static class UIElementBuilder
	{
		// Loads the menu items from App.config and put them into the menu strip, hooking
		// up the menu URIs for command dispatching.
		public static void LoadFromConfig(WorkItem workItem)
		{
			ShellItemsSection section = (ShellItemsSection)ConfigurationManager.GetSection("shellitems");

            foreach (MenuItemElement menuItem in section.MenuItems)
            {
                BarItem uiMenuItem = menuItem.ToMenuItem();

                workItem.UIExtensionSites[menuItem.Site].Add(uiMenuItem);

                if (menuItem.Register == true)
                    workItem.UIExtensionSites.RegisterSite(menuItem.RegistrationSite, uiMenuItem);

                if (!String.IsNullOrEmpty(menuItem.CommandName))
                    workItem.Commands[menuItem.CommandName].AddInvoker(uiMenuItem, "ItemClick");
            }
		}
	}
}
