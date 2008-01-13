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

using BankTellerCommon;
using BankTellerModule.WorkItems.BankTeller;
using DevExpress.XtraBars;
using Microsoft.Practices.CompositeUI;
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
			IWorkspace navbarWorkspace = workItem.Workspaces[WorkspacesConstants.SHELL_NAVBARWORKSPACE];
			IWorkspace contentWorkspace = workItem.Workspaces[WorkspacesConstants.SHELL_CONTENTWORKSPACE];

            BankTellerWorkItem bankTellerWorkItem = workItem.WorkItems.AddNew<BankTellerWorkItem>();
            bankTellerWorkItem.Show(navbarWorkspace, contentWorkspace);
		}

		private void AddCustomerMenuItem()
		{
			BarItem customerItem = new BarSubItem();
            customerItem.Caption = "Customer";
			workItem.UIExtensionSites[UIExtensionSites.FILE].Add(customerItem);
			workItem.UIExtensionSites.RegisterSite(Properties.Resources.CustomerMenuExtensionSite, customerItem);
		}
	}
}
