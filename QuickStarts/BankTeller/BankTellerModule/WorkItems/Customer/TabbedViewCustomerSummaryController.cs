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
using System.IO;
using BankTellerModule.Constants;
using CABDevExpress.Workspaces;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.Commands;
using Microsoft.Practices.CompositeUI.WinForms;

namespace BankTellerModule.WorkItems.Customer
{
	public class TabbedViewCustomerSummaryController : Controller
	{
        // The TabbedViewCustomerSummaryController is the controller used by the TabbedViewCustomerSummaryView.
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
			if (WorkItem.Status != WorkItemStatus.Active) return;

			var tabWorkspace = WorkItem.Workspaces[CustomerWorkItem.CUSTOMERDETAIL_TABWORKSPACE] as XtraTabbedViewWorkspace;
		}
        String _streamString;
        [CommandHandler(CommandNames.SaveLayout)]
        public void OnSaveLayout(object sender, EventArgs args)
        {
            if (WorkItem.Status != WorkItemStatus.Active) return;

            XtraTabbedViewWorkspace tabWorkspace = WorkItem.Workspaces[CustomerWorkItem.CUSTOMERDETAIL_TABWORKSPACE] as XtraTabbedViewWorkspace;
            MemoryStream layoutStream = new MemoryStream();
            tabWorkspace.SaveLayoutToStream(layoutStream, false);
            layoutStream.Seek(0, System.IO.SeekOrigin.Begin);
            using (StreamReader reader = new StreamReader(layoutStream, System.Text.Encoding.ASCII))
            {
                _streamString = reader.ReadToEnd();
            }
        }

        [CommandHandler(CommandNames.RestoreLayout)]
        public void OnRestoreLayout(object sender, EventArgs args)
        {
            if (WorkItem.Status != WorkItemStatus.Active) return;

            XtraTabbedViewWorkspace tabWorkspace = WorkItem.Workspaces[CustomerWorkItem.CUSTOMERDETAIL_TABWORKSPACE] as XtraTabbedViewWorkspace;
            if (tabWorkspace != null && !String.IsNullOrEmpty(_streamString))
            {
                byte[] layoutData = System.Text.Encoding.ASCII.GetBytes(_streamString);
                MemoryStream layoutStream = new System.IO.MemoryStream(layoutData);
                layoutStream.Seek(0, SeekOrigin.Begin);
                tabWorkspace.RestoreLayoutFromStream(layoutStream, false);
            }
        }
        

    }
}