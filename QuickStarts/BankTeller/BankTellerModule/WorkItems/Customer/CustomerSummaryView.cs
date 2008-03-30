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
using BankTellerModule.Constants;
using BankTellerModule.WorkItems.Customer;
using DevExpress.XtraEditors;
using Microsoft.Practices.CompositeUI.SmartParts;
using Microsoft.Practices.ObjectBuilder;

namespace BankTellerModule.WorkItems.Customer
{
	[SmartPart]
	public partial class CustomerSummaryView : XtraUserControl
	{
		private CustomerSummaryController controller;

		public CustomerSummaryView()
		{
			InitializeComponent();
		}

		[CreateNew]
		public CustomerSummaryController Controller
		{
			set { controller = value; }
		}

		private void OnSave(object sender, EventArgs e)
		{
			controller.Save();
		}

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);

			controller.WorkItem.UIExtensionSites.RegisterSite(ExtensionSiteNames.CustomerContext, this.customerContextMenu);
		}

		internal void FocusFirstTab()
		{
			this.tabbedWorkspace1.SelectedTabPage = this.tabbedWorkspace1.TabPages[0];
		}
	}
}