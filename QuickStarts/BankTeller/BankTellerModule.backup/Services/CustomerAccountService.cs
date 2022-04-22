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

// The example companies, organizations, products, domain names, e-mail addresses,
// logos, people, places, and events depicted herein are fictitious.  No association
// with any real company, organization, product, domain name, email address, logo,
// person, places, or events is intended or should be inferred.

using System.Collections;
using BankTellerCommon;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.Utility;

namespace BankTellerModule.Services
{
	[Service]
	public class CustomerAccountService
	{
		private readonly ListDictionary<int, CustomerAccount> customerAccounts;

		public CustomerAccountService()
		{
			customerAccounts = new ListDictionary<int, CustomerAccount>
			                   	{
			                   		{1, new CustomerAccount(123456781, "Checking", 1842.75M)},
			                   		{1, new CustomerAccount(123456782, "Savings", 9367.92M)},
			                   		{2, new CustomerAccount(54491229, "Salary from Employer", 1000M)},
			                   		{2, new CustomerAccount(54491243, "House loan", -450000.23M)},
			                   		{2, new CustomerAccount(05150004477, "Student loan", -41345.18M)},
			                   		{3, new CustomerAccount(987654321, "Interest Checking", 2496.44M)},
			                   		{3, new CustomerAccount(987654322, "Money Market", 21959.38M)},
			                   		{3, new CustomerAccount(987654323, "Car Loan", -19483.95M)}
			                   	};
		}

		public IEnumerable GetByCustomerID(int customerID)
		{
			return customerAccounts[customerID];
		}
	}
}