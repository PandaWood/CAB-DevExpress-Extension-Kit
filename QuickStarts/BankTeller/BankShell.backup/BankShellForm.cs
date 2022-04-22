//===============================================================================
// Microsoft patterns & practices
// CompositeUI Application Block
//===============================================================================
// Copyright � Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
//===============================================================================

using System;
using System.Windows.Forms;
using BankTellerModule.Constants;
using CABDevExpress.Workspaces;
using DevExpress.XtraEditors;
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
    // Core menu items (like Exit and About) are handled in the shell. We
    // ask for them to be dispatched on the user interface thread so that
    // we can directly call Form methods without using Invoke.
    //
    // We listen for status update events. Modules can fire status update
    // events to tell us to change the status bar.
    public partial class BankShellForm : XtraForm
    {
        private readonly WorkItem workItem;
        private IWorkItemTypeCatalogService workItemTypeCatalog;
    	private readonly DockManagerWorkspace dockManagerWorkspace;

        public BankShellForm()
        {
            InitializeComponent();
            barStaticItem1.Caption = String.Empty;
            barManager.ForceInitialize();

			dockManagerWorkspace = new DockManagerWorkspace(dockManager);
        }

        /// <summary>
        /// This constructor will be called by ObjectBuilder when the Form is created
        /// by calling WorkItem.Items.AddNew.
        /// </summary>
        [InjectionConstructor]
        public BankShellForm(WorkItem workItem, IWorkItemTypeCatalogService workItemTypeCatalog) : this()
        {
            this.workItem = workItem;
            this.workItemTypeCatalog = workItemTypeCatalog;
        }

        [CommandHandler(CommandNames.Exit)]
        public void OnFileExit(object sender, EventArgs e)
        {
            Application.Exit();
        }

        [EventSubscription(EventNames.StatusUpdate, Thread = ThreadOption.UserInterface)]
        public void OnStatusUpdate(object sender, DataEventArgs<string> e)
        {
            barStaticItem1.Caption = e.Data;
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