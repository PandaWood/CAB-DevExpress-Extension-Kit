//===============================================================================
// Microsoft patterns & practices
// CompositeUI Application Block
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
//===============================================================================

using System;
using BankShell.Properties;
using BankTellerModule.Constants;
using CABDevExpress;
using CABDevExpress.UIElements;
using CABDevExpress.Workspaces;
using DevExpress.LookAndFeel;
using DevExpress.Skins;
using DevExpress.UserSkins;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraEditors;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.WinForms;

namespace BankShell
{
#if UseRibbonForm
    public class BankShellApplication : XtraFormApplication<WorkItem, BankShellRibbonForm>
#else
    public class BankShellApplication : XtraFormApplication<WorkItem, BankShellForm>
#endif
    {
        [STAThread]
        public static void Main()
        {
            BonusSkins.Register();
            SkinManager.EnableFormSkins();
            if (String.IsNullOrEmpty(Settings.Default.AppSkin))
                UserLookAndFeel.Default.SetSkinStyle("DevExpress Style");
            else
                UserLookAndFeel.Default.SetSkinStyle(Settings.Default.AppSkin);

            new BankShellApplication().Run();
            if (!String.IsNullOrEmpty(UserLookAndFeel.Default?.ActiveSkinName))
            {
                Settings.Default.AppSkin = UserLookAndFeel.Default.ActiveSkinName;
                Settings.Default.Save();
            }
        }

        // This method is called just after your shell has been created (the root work item
        // also exists). Here, you might want to:
        //   - Attach UIElementManagers
        //   - Register the form with a name.
        //   - Register additional workspaces (e.g. a named WindowWorkspace)
        protected override void AfterShellCreated()
        {
            base.AfterShellCreated();

            XtraDocumentManagerWorkspace ContentWorkspace = new XtraDocumentManagerWorkspace(Shell);
            RootWorkItem.Items.Add(ContentWorkspace, WorkspaceNames.ShellContentWorkspace);
         
            //RootWorkItem.Items.Add(new MdiWorkspace(Shell), WorkspaceNames.ShellContentWorkspace);
            RootWorkItem.Items.Add(Shell.NavBarWorkspace, WorkspaceNames.ShellNavBarWorkspace);
            RootWorkItem.Items.Add(Shell.DockManagerWorkspace, WorkspaceNames.DockManagerWorkspace);
            RootWorkItem.Items.Add(Shell.DeckWorkspace, WorkspaceNames.DeckWorkspace);
            RootWorkItem.Workspaces[WorkspaceNames.ShellContentWorkspace].SmartPartActivated += BankShellApplication_SmartPartActivated;
            RootWorkItem.Workspaces[WorkspaceNames.ShellContentWorkspace].SmartPartClosing += BankShellApplication_SmartPartClosing;
#if UseRibbonForm
            RootWorkItem.UIExtensionSites.RegisterSite(ExtensionSiteNames.Ribbon, Shell.Ribbon);
            RootWorkItem.UIExtensionSites.RegisterSite(ExtensionSiteNames.MainMenu, Shell.homePage);
            RootWorkItem.UIExtensionSites.RegisterSite(ExtensionSiteNames.MainStatus, Shell.mainStatusBar);
            RootWorkItem.UIExtensionSites.RegisterSite(ExtensionSiteNames.RibbonApplicationMenu, new
                RibbonApplicationMenuUIAdapter(Shell.applicationMenu));

            RibbonPageGroup ribbonGroup = new RibbonPageGroup(ExtensionSiteNames.File);
            RootWorkItem.UIExtensionSites[ExtensionSiteNames.MainMenu].Add<RibbonPageGroup>(ribbonGroup);
            RootWorkItem.UIExtensionSites.RegisterSite(ExtensionSiteNames.File, ribbonGroup);
            RootWorkItem.UIExtensionSites.RegisterSite(ExtensionSiteNames.RibbonPageHeader, new
                RibbonPageHeaderUIAdapter(Shell.Ribbon.PageHeaderItemLinks));
            RootWorkItem.UIExtensionSites.RegisterSite(ExtensionSiteNames.RibbonQuickAccessToolbar, new
                RibbonQuickAccessToolbarUIAdapter(Shell.Ribbon.Toolbar));
            RootWorkItem.UIExtensionSites.RegisterSite(ExtensionSiteNames.RibbonAppMenuBottomPane, new
                RibbonAppMenuBottomPaneSimpleButtonUIAdapter(RootWorkItem, Shell.applicationMenu));

            BankTellerRibbonSkins.Add(RootWorkItem);
            //BankTellerRibbonWindows.Add(RootWorkItem, Shell.xtraTabbedMdiManager, Shell);
            BankTellerRibbonWindows.Add(RootWorkItem, ContentWorkspace);
#else
			RootWorkItem.UIExtensionSites.RegisterSite(ExtensionSiteNames.MainMenu, Shell.mainMenuBar);
			RootWorkItem.UIExtensionSites.RegisterSite(ExtensionSiteNames.MainStatus, Shell.mainStatusBar);
			RootWorkItem.UIExtensionSites.RegisterSite(ExtensionSiteNames.FileDropDown, Shell.barSubItemFile);
			RootWorkItem.UIExtensionSites.RegisterSite(ExtensionSiteNames.File, new BarItemWrapper(Shell.mainMenuBar.ItemLinks, Shell.barSubItemFile));
            RootWorkItem.UIExtensionSites.RegisterSite(ExtensionSiteNames.SkinsToolBar, Shell.skinsBar);
            RootWorkItem.UIExtensionSites.RegisterSite(ExtensionSiteNames.ToolBar, Shell.mainBar);
            RootWorkItem.UIExtensionSites.RegisterSite(ExtensionSiteNames.WindowToolBar, Shell.windowToolBar);
            
            // There are two ways to add skins to the BankTeller demo.
            // 1) SkinMenu: Bypasses the CAB framework and directly adds and
            //    manipulates the menu items. The SkinMenu is standard .NET 
            //    functionality and is more efficient and easier to understand.
            // 2) BankTellerDynamicSkins: Uses the event broker to handle both the
            //    menu Popup event and the handling of the menu item click. This
            //    allows both dynamic menu item creation as well as more adherence
            //    to the CAB decoupled philosophy. It does not provide any
            //    additional functionality.
            //
            // SkinMenu and BankTellerDynamicSkins are functionally equivalent.
            // The two methods give the developer a choice on how closely to
            // follow the CAB philosophy.
            //
            // To change between the two swap the comments on the following two
            // lines of code. The two lines can also both be uncommented and two
            // working skin menus will appear.
            RootWorkItem.UIExtensionSites[ExtensionSiteNames.MainMenu].Add(new SkinMenu(Shell.mainMenuBar));
            //BankTellerDynamicSkins.Skins.AddSkins(RootWorkItem);
            RootWorkItem.UIExtensionSites[ExtensionSiteNames.MainMenu].Add(new WindowMenu(Shell.mainMenuBar, ContentWorkspace,  RootWorkItem));
#endif
            UIElementBuilder.LoadFromConfig(RootWorkItem);
        }

        private void BankShellApplication_SmartPartClosing(object sender, Microsoft.Practices.CompositeUI.SmartParts.WorkspaceCancelEventArgs e)
        {
            CABDevExpress.Workspaces.XtraDocumentManagerWorkspace wks = sender as CABDevExpress.Workspaces.XtraDocumentManagerWorkspace;
            if (wks!=null && wks.SmartParts.Count==1)
                Shell.DeckWorkspaceBringToFront();
        }

        private void BankShellApplication_SmartPartActivated(object sender, Microsoft.Practices.CompositeUI.SmartParts.WorkspaceEventArgs e)
        {
            Shell.DeckWorkspaceSendToBack();
        }

        #region HandleException

        public override void OnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception ex = e.ExceptionObject as Exception;

            if (ex != null)
            {
                XtraMessageBox.Show(BuildExceptionString(ex));
            }
            else
            {
                XtraMessageBox.Show("An Exception has occured, unable to get details");
            }

            Environment.Exit(0);
        }

        private static string BuildExceptionString(Exception exception)
        {
            string errMessage = string.Empty;

            errMessage += exception.Message + Environment.NewLine + exception.StackTrace;

            while (exception.InnerException != null)
            {
                errMessage += BuildInnerExceptionString(exception.InnerException);
                exception = exception.InnerException;
            }

            return errMessage;
        }

        private static string BuildInnerExceptionString(Exception innerException)
        {
            string errMessage = string.Empty;

            errMessage += Environment.NewLine + " InnerException ";
            errMessage += Environment.NewLine + innerException.Message + Environment.NewLine + innerException.StackTrace;

            return errMessage;
        }

        #endregion
    }
}
