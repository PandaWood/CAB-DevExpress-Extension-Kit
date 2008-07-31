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
using System.Windows.Forms;
using BankTellerModule.Constants;
using CABDevExpress.UIElements;
using CABDevExpress.Workspaces;
using DevExpress.LookAndFeel;
using DevExpress.Utils;
using DevExpress.XtraBars.Ribbon;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.Commands;
using Microsoft.Practices.CompositeUI.EventBroker;
using Microsoft.Practices.CompositeUI.Services;
using Microsoft.Practices.CompositeUI.Utility;
using Microsoft.Practices.ObjectBuilder;

namespace BankShell
{
    // The shell represents the main window of the application. The shell
    // provides a menu, status bar, and a single workspace for the rest of
    // the window. Modules will use the workspace to display their role-
    // specific user interface.
    //
    // Core menu items (like Exit) are handled in the shell. We
    // ask for them to be dispatched on the user interface thread so that
    // we can directly call Form methods without using Invoke.
    //
    // We listen for status update events. Modules can fire status update
    // events to tell us to change the status bar.
    public partial class BankShellRibbonForm : RibbonForm
    {
        private readonly WorkItem workItem;
        private IWorkItemTypeCatalogService workItemTypeCatalog;
    	private readonly DockManagerWorkspace dockManagerWorkspace;

        public BankShellRibbonForm()
        {
            InitializeComponent();
            barStaticItem1.Caption = String.Empty;
			dockManagerWorkspace = new DockManagerWorkspace(dockManager);
            AllowFormGlass = DefaultBoolean.False;
        }

        /// <summary>
        /// This constructor will be called by ObjectBuilder when the Form is created
        /// by calling WorkItem.Items.AddNew.
        /// </summary>
        [InjectionConstructor]
        public BankShellRibbonForm(WorkItem workItem, IWorkItemTypeCatalogService workItemTypeCatalog) : this()
        {
            this.workItem = workItem;
            this.workItemTypeCatalog = workItemTypeCatalog;
        }

        [CommandHandler(CommandNames.Exit)]
        public void OnFileExit(object sender, EventArgs e)
        {
            Application.Exit();
        }

        [EventSubscription(EventNames.Exit, ThreadOption.UserInterface)]
        public void OnFileExitEvent(object sender, EventArgs e)
        {
            Application.Exit();
        }

        [EventSubscription(EventNames.StatusUpdate, Thread = ThreadOption.UserInterface)]
        public void OnStatusUpdate(object sender, DataEventArgs<string> e)
        {
            barStaticItem1.Caption = e.Data;
        }

        [EventSubscription(EventNames.RibbonSkinChange, ThreadOption.UserInterface)]
        public void ModifyLookAndFeel(object sender, DynamicEventArgs<DynamicCommandEventLink> e)
        {
            UserLookAndFeel.Default.SetSkinStyle((string)e.Data.Data);
        }
        
        public XtraNavBarWorkspace NavBarWorkspace
		{
			get { return navBarWorkspace; }
		}

		public DockManagerWorkspace DockManagerWorkspace
    	{
			get { return dockManagerWorkspace; }
    	}
    }
}