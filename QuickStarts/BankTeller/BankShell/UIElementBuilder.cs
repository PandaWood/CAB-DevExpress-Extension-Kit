using System;
using System.Configuration;
using BankShell.Config;
using BankTellerModule.Constants;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraEditors;
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
        private static string InstantiateNonBarItemUiElement(WorkItem workItem, 
            MenuItemElement menuItem, string siteAndType, ref SimpleButton uiButton)
        {
            string site;
            site = siteAndType.Substring(0, siteAndType.IndexOf('='));
            if (workItem.UIExtensionSites.Contains(site))
            {
                string type = siteAndType.Substring(siteAndType.IndexOf('=') + 1);
                if (type != "SimpleButton")
                    throw new Exception("Unknown UI element type in configuration file.");
                if (uiButton == null)
                {
                    uiButton = new SimpleButton();
                    // OK. I'm abusing the system here to change a command into
                    // an event. I would not do this in anything other than a demo.
                    uiButton.Tag = menuItem.CommandName + "Event";
                    uiButton.Text = menuItem.Label;
                    uiButton.Image = MenuItemElement.GetGlyph(menuItem.Glyph);
                }
                workItem.UIExtensionSites[site].Add(uiButton);
            }
            return site;
        }

        private static void InstantiateBarItemUiElement(WorkItem workItem, 
            MenuItemElement menuItem, ref BarItem uiMenuItem, string site)
        {
            if (uiMenuItem == null)
            {
                uiMenuItem = menuItem.ToMenuItem();
                if (!String.IsNullOrEmpty(menuItem.CommandName))
                    workItem.Commands[menuItem.CommandName].AddInvoker(uiMenuItem, "ItemClick");
            }
            workItem.UIExtensionSites[site].Add(uiMenuItem);
        }

        private static void ProcessConfigItem(WorkItem workItem, MenuItemElement menuItem)
        {
            if (menuItem.Register)
            {
                RibbonPageGroup ribbonGroup = new RibbonPageGroup(menuItem.Label);
                workItem.UIExtensionSites[ExtensionSiteNames.MainMenu].Add<RibbonPageGroup>(ribbonGroup);
                workItem.UIExtensionSites.RegisterSite(menuItem.RegistrationSite, ribbonGroup);
            }
            else
            {
                foreach (string siteAndType in menuItem.Site.Split(new char[] { ';' }))
                {
                    BarItem uiMenuItem = null;
                    SimpleButton uiButton = null;
                    string site = siteAndType;
                    if (siteAndType.Contains("="))
                        site = InstantiateNonBarItemUiElement(workItem, menuItem, siteAndType, ref uiButton);
                    else if (workItem.UIExtensionSites.Contains(site))
                        InstantiateBarItemUiElement(workItem, menuItem, ref uiMenuItem, site);
                }
            }
        }
#else
        private static void ProcessConfigItem(WorkItem workItem, MenuItemElement menuItem)
        {
            BarItem uiMenuItem = menuItem.ToMenuItem();

            foreach (string site in menuItem.Site.Split(new char[] { ';' }))
            {
                // this has not been modified to handle different uiElement types
                // if a siteType contains a different uiElement type the conditional
                // will fail and the uiElement item won't be added to the system.
                if (workItem.UIExtensionSites.Contains(site))
                {
                    workItem.UIExtensionSites[site].Add(uiMenuItem);
                }
            }

            if (menuItem.Register)
                workItem.UIExtensionSites.RegisterSite(menuItem.RegistrationSite, uiMenuItem);

            if (!String.IsNullOrEmpty(menuItem.CommandName))
                workItem.Commands[menuItem.CommandName].AddInvoker(uiMenuItem, "ItemClick");
        }
#endif

    }
}
