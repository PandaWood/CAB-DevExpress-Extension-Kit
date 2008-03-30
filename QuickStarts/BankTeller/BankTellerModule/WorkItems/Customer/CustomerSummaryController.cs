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
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.Commands;
using Microsoft.Practices.CompositeUI.WinForms;

namespace BankTellerModule.WorkItems.Customer
{
	public class CustomerSummaryController : Controller
	{
		// The CustomerSummaryController is the controller used by the CustomerSummaryView.
		// The summary view contains the pieces of the other views to display a customer,
		// and includes the Save button for the user to save their changes. The save
		// request is forwarded up to the work item.

		public new CustomerWorkItem WorkItem
		{
			get { return base.WorkItem as CustomerWorkItem; }
		}

		public void Save()
		{
			WorkItem.Save();
		}

		[CommandHandler(CommandNames.EditCustomer)]
		public void OnCustomerEdit(object sender, EventArgs args)
		{
			if (WorkItem.Status == WorkItemStatus.Active)
			{
				TabWorkspace tabWS = WorkItem.Workspaces[CustomerWorkItem.CUSTOMERDETAIL_TABWORKSPACE] as TabWorkspace;

				if (tabWS != null)
				{
					tabWS.SelectedIndex = 0;
				}
			}
		}
		
	}
}