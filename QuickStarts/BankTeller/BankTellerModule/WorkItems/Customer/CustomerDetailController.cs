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

using Microsoft.Practices.CompositeUI;

namespace BankTellerModule.WorkItems.Customer
{
	public class CustomerDetailController : Controller
	{
		public new CustomerWorkItem WorkItem
		{
			get { return base.WorkItem as CustomerWorkItem; }
		}

		public void ShowCustomerComments()
		{
			WorkItem.ShowCustomerComments();
		}
	}
}