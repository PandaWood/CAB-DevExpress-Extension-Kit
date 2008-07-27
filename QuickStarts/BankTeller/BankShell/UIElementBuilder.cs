using System;
using System.Configuration;
using BankShell.Config;
using BankTellerModule.Constants;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
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
                ProcessConfigItem(workItem, menuItem);
            }
        }

#if UseRibbonForm
        private static void ProcessConfigItem(WorkItem workItem, MenuItemElement menuItem)
        {
            BarItem uiMenuItem;
            if (menuItem.Register)
            {
                RibbonPageGroup ribbonGroup = new RibbonPageGroup(menuItem.Label);
                workItem.UIExtensionSites[ExtensionSiteNames.MainMenu].Add<RibbonPageGroup>(ribbonGroup);
                workItem.UIExtensionSites.RegisterSite(menuItem.RegistrationSite, ribbonGroup);
            }
            else
            {
                uiMenuItem = menuItem.ToMenuItem();
                if (!String.IsNullOrEmpty(menuItem.CommandName))
                    workItem.Commands[menuItem.CommandName].AddInvoker(uiMenuItem, "ItemClick");

                foreach (string site in menuItem.Site.Split(new char[] { ';' }))
                {
                    if (workItem.UIExtensionSites.Contains(site))
                        workItem.UIExtensionSites[site].Add(uiMenuItem);
                }
            }
        }
#else
        private static void ProcessConfigItem(WorkItem workItem, MenuItemElement menuItem)
        {
            BarItem uiMenuItem = menuItem.ToMenuItem();

            foreach (string site in menuItem.Site.Split(new char[] { ';' }))
            {
                if (workItem.UIExtensionSites.Contains(site))
                    workItem.UIExtensionSites[site].Add(uiMenuItem);
            }

            if (menuItem.Register)
                workItem.UIExtensionSites.RegisterSite(menuItem.RegistrationSite, uiMenuItem);

            if (!String.IsNullOrEmpty(menuItem.CommandName))
                workItem.Commands[menuItem.CommandName].AddInvoker(uiMenuItem, "ItemClick");
        }
#endif

    }
}
