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
using BankTellerModule.Constants;
using CABDevExpress;
using CABDevExpress.UIElements;
using DevExpress.Skins;
using DevExpress.UserSkins;
using DevExpress.XtraEditors;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.WinForms;

namespace BankShell
{
    public class BankShellApplication : XtraFormApplication<WorkItem, BankShellForm>
    {
        [STAThread]
        public static void Main()
        {
            BonusSkins.Register();
        	OfficeSkins.Register();
        	SkinManager.EnableFormSkins();

        	new BankShellApplication().Run();
        }


        // This method is called just after your shell has been created (the root work item
        // also exists). Here, you might want to:
        //   - Attach UIElementManagers
        //   - Register the form with a name.
        //   - Register additional workspaces (e.g. a named WindowWorkspace)
        protected override void AfterShellCreated()
        {
            base.AfterShellCreated();

			RootWorkItem.Items.Add(new MdiWorkspace(Shell), WorkspaceNames.ShellContentWorkspace);
			RootWorkItem.Items.Add(Shell.NavBarWorkspace, WorkspaceNames.ShellNavBarWorkspace);
			RootWorkItem.Items.Add(Shell.DockManagerWorkspace, WorkspaceNames.DockManagerWorkspace);

			RootWorkItem.UIExtensionSites.RegisterSite(ExtensionSiteNames.MainMenu, Shell.mainMenuBar);
			RootWorkItem.UIExtensionSites.RegisterSite(ExtensionSiteNames.MainStatus, Shell.mainStatusBar);
			RootWorkItem.UIExtensionSites.RegisterSite(ExtensionSiteNames.FileDropDown, Shell.barSubItemFile);

			RootWorkItem.UIExtensionSites.RegisterSite(ExtensionSiteNames.File, 
				new BarItemWrapper(Shell.mainMenuBar.ItemLinks, Shell.barSubItemFile));

			RootWorkItem.UIExtensionSites[ExtensionSiteNames.MainMenu].Add(new SkinMenu(Shell.mainMenuBar));

			RootWorkItem.UIExtensionSites[ExtensionSiteNames.MainMenu].Add(
				new WindowMenu(Shell.mainMenuBar, Shell.xtraTabbedMdiManager, Shell));

            UIElementBuilder.LoadFromConfig(RootWorkItem);
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
