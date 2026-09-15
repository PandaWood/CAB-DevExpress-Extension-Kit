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

using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.SmartParts;
using Microsoft.Practices.ObjectBuilder;
using System;
using System.ComponentModel;

namespace BankTellerModule.WorkItems.Customer
{
	[SmartPart]
	public partial class CustomerDetailView : XtraUserControl
	{
		private BankTellerCommon.Customer customer;
		private WorkItem parentWorkItem;
		private CustomerDetailController controller;

		public CustomerDetailView()
		{
			InitializeComponent();
		}

		[ServiceDependency]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public WorkItem ParentWorkItem
		{
			set { parentWorkItem = value; }
		}

		// The Customer state is stored in our parent work item
		[State]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public BankTellerCommon.Customer Customer
		{
			set { customer = value; }
		}

		// We use our controller so we can show the comments page
		[CreateNew]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public CustomerDetailController Controller
		{
			set { controller = value; }
		}

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);

			if (customer != null)
			{
				customerBindingSource.Add(customer);
			}
		}

		private void OnShowComments(object sender, ItemClickEventArgs e)
		{
			controller.ShowCustomerComments();
		}
	}
}