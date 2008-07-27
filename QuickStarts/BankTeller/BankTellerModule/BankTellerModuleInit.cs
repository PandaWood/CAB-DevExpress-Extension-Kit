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

using BankTellerModule.Constants;
using BankTellerModule.Properties;
using BankTellerModule.WorkItems.BankTeller;
using CABDevExpress.UIElements;
using DevExpress.LookAndFeel;
using DevExpress.XtraBars;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.EventBroker;
using Microsoft.Practices.CompositeUI.SmartParts;
using Microsoft.Practices.ObjectBuilder;

namespace BankTellerModule
{
	// This is the initialization class for the Module. Any classes in the assembly that
	// derive from Microsoft.Practices.CompositeUI.ModuleInit will automatically
	// be created and called for initialization.

	public class BankTellerModuleInit : ModuleInit
	{
		private readonly WorkItem workItem;
		
		[InjectionConstructor]
		public BankTellerModuleInit([ServiceDependency] WorkItem workItem)
		{
			this.workItem = workItem;
		}

		public override void Load()
		{
			AddCustomerMenuItem();

			//Retrieve well known workspaces
			IWorkspace navbarWorkspace = workItem.Workspaces[WorkspaceNames.ShellNavBarWorkspace];
			IWorkspace contentWorkspace = workItem.Workspaces[WorkspaceNames.ShellContentWorkspace];

            BankTellerWorkItem bankTellerWorkItem = workItem.WorkItems.AddNew<BankTellerWorkItem>();
            bankTellerWorkItem.Show(navbarWorkspace, contentWorkspace);

			IWorkspace dockWorkspace = workItem.Workspaces[WorkspaceNames.DockManagerWorkspace];
			bankTellerWorkItem.Show(dockWorkspace);
		}

		private void AddCustomerMenuItem()
		{
#if !UseRibbonForm
			BarItem customerItem = new BarSubItem();
            customerItem.Caption = "Customer";
			workItem.UIExtensionSites[ExtensionSiteNames.File].Add(customerItem);
			workItem.UIExtensionSites.RegisterSite(Resources.CustomerMenuExtensionSite, customerItem);
#endif
		}

#if UseRibbonForm
        [EventSubscription(EventNames.RibbonSkinChange, ThreadOption.UserInterface)]
        public void ModifyLookAndFeel(object sender, DynamicEventArgs<DynamicCommandEventLink> e)
        {
            UserLookAndFeel.Default.SetSkinStyle((string)e.Data.Data);
        }
#endif
	}
}
