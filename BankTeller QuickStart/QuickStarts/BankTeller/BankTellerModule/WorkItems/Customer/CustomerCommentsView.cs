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
using DevExpress.XtraEditors;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.SmartParts;

namespace BankTellerModule.WorkItems.Customer
{
	[SmartPart]
	public partial class CustomerCommentsView : XtraUserControl
	{
		// The Customer state is stored in our parent work item

		private BankTellerCommon.Customer customer = null;

		[State]
		public BankTellerCommon.Customer Customer
		{
			set { customer = value; }
		}

		public CustomerCommentsView()
		{
			InitializeComponent();
		}

		private void CustomerCommentsView_Load(object sender, EventArgs e)
		{
			if (!DesignMode)
			{
				customerBindingSource.Add(customer);
			}
		}
	}
}