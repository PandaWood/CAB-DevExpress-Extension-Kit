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

// The example companies, organizations, products, domain names, e-mail addresses,
// logos, people, places, and events depicted herein are fictitious.  No association
// with any real company, organization, product, domain name, email address, logo,
// person, places, or events is intended or should be inferred.

using System.Collections;
using BankTellerCommon;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.Utility;

namespace BankTellerModule
{
	// The service that simulates a data provider for customer account business
	// entities (model data). Any class annotated with the [Service] attribute
	// is automatically registered in the root Work Item during module
	// initialization.

	[Service]
	public class CustomerAccountService
	{
		private readonly ListDictionary<int, CustomerAccount> customerAccounts;

		public CustomerAccountService()
		{
			customerAccounts = new ListDictionary<int, CustomerAccount>();

			customerAccounts.Add(1, new CustomerAccount(123456781, "Checking", 1842.75M));
			customerAccounts.Add(1, new CustomerAccount(123456782, "Savings", 9367.92M));

            customerAccounts.Add(2, new CustomerAccount(54491229, "Salary from Employer", 1000M));
            customerAccounts.Add(2, new CustomerAccount(54491243, "House loan", -450000.23M));
            customerAccounts.Add(2, new CustomerAccount(05150004477, "Student loan", -41345.18M));
            
            customerAccounts.Add(3, new CustomerAccount(987654321, "Interest Checking", 2496.44M));
			customerAccounts.Add(3, new CustomerAccount(987654322, "Money Market", 21959.38M));
			customerAccounts.Add(3, new CustomerAccount(987654323, "Car Loan", -19483.95M));
        
        }

		public IEnumerable GetByCustomerID(int customerID)
		{
			return customerAccounts[customerID];
		}
	}
}
