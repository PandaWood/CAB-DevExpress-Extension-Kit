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

using BankTellerModule.Services;
using Microsoft.Practices.CompositeUI;

namespace BankTellerModule.WorkItems.BankTeller
{
	public class CustomerQueueController : Controller
	{
		// We depend on the customer queue service to tell us which customer is next
		private CustomerQueueService customerQueueService;

		[ServiceDependency]
		public CustomerQueueService CustomerQueueService
		{
			set { customerQueueService = value; }
		}

		public new BankTellerWorkItem WorkItem
		{
			get { return base.WorkItem as BankTellerWorkItem; }
		}

		public BankTellerCommon.Customer GetNextCustomerInQueue()
		{
			return customerQueueService.GetNext();
		}

		public void WorkWithCustomer(BankTellerCommon.Customer customer)
		{
			WorkItem.WorkWithCustomer(customer);
		}
	}
}