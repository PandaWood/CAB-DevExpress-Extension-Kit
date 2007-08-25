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

namespace BankTellerCommon
{
	public class CustomerAccount
	{
		private readonly long accountNumber;
		private readonly string accountType;
		private readonly decimal currentBalance;

		public CustomerAccount(long accountNumber, string accountType, decimal currentBalance)
		{
			this.accountNumber = accountNumber;
			this.accountType = accountType;
			this.currentBalance = currentBalance;
		}

		public long AccountNumber
		{
			get { return accountNumber; }
		}

		public string AccountType
		{
			get { return accountType; }
		}

		public decimal CurrentBalance
		{
			get { return currentBalance; }
		}

	}
}