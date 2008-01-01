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
using BankTellerCommon;
using CABDevExpress.UIElements;
using DevExpress.XtraEditors;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.WinForms;
using CABDevExpress;

namespace BankShell
{
    public class BankShellApplication : XtraFormApplication<WorkItem, BankShellForm>
    {
        [STAThread]
        public static void Main()
        {
            DevExpress.UserSkins.BonusSkins.Register();
        	DevExpress.UserSkins.OfficeSkins.Register();
        	DevExpress.UserSkins.XmasSkins.Register();
        	DevExpress.Skins.SkinManager.EnableFormSkins();

        	BankShellApplication app = new BankShellApplication();
			app.Run();
        }


        // This method is called just after your shell has been created (the root work item
        // also exists). Here, you might want to:
        //   - Attach UIElementManagers
        //   - Register the form with a name.
        //   - Register additional workspaces (e.g. a named WindowWorkspace)
        protected override void AfterShellCreated()
        {
            base.AfterShellCreated();

            RootWorkItem.Items.Add(new MdiWorkspace(Shell), WorkspacesConstants.SHELL_CONTENT);

            RootWorkItem.UIExtensionSites.RegisterSite(UIExtensionConstants.MAINMENU, Shell.bar1);
            RootWorkItem.UIExtensionSites.RegisterSite(UIExtensionConstants.MAINSTATUS, Shell.bar2);
            RootWorkItem.UIExtensionSites.RegisterSite(UIExtensionConstants.FILEDROPDOWN, Shell.barSubItemFile);
            RootWorkItem.UIExtensionSites.RegisterSite(UIExtensionConstants.FILE, new BarItemWrapper(Shell.bar1.ItemLinks, Shell.barSubItemFile));
            RootWorkItem.UIExtensionSites[UIExtensionConstants.MAINMENU].Add(new SkinMenu(Shell.bar1));
            RootWorkItem.UIExtensionSites[UIExtensionConstants.MAINMENU].Add(new WindowMenu(Shell.bar1, Shell.xtraTabbedMdiManager, Shell));

            // Load the menu structure from App.config
            UIElementBuilder.LoadFromConfig(RootWorkItem);
        }

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
    }
}
