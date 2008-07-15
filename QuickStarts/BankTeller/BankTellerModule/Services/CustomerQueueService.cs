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

using BankTellerCommon;
using Microsoft.Practices.CompositeUI;

namespace BankTellerModule.Services
{
	[Service]
	public class CustomerQueueService
	{
		private readonly Customer[] customers;
		private int idx;

		public CustomerQueueService()
		{
			customers = new[]
				{
					new Customer(1, "Panda", "Wood", "16074 NE 36th Way", "", "Sydney", "NSW", "2000", "spurrymoses@gmail.com",
					             "425-555-0100", "", "August 25, 2007 - Updated the CAB DevExpress Extension Kit BankTeller app"),
					new Customer(2, "Espen", "Schaathun", "Via Clemente", "", "Carlsbad", "CA", "98002",
					             "espen underscore schaathun at hotmail dot com", "858-999-8765", "858-123-4567",
					             "April 2006 - Been working on CAB DevExpress Extension Kit"),
					new Customer(4, "Vincent", "Guerci", "Rue du chat qui pêche", "", "Paris", "TX", "75005",
					             "puy0 _at_ hotmail _dot_ com", "123-456-7890", "123-456-7890",
					             "April 2006 - Been working on CAB DevExpress Extension Kit"),
					new Customer(3, "David", "Jones", "1 Microsoft Way", "", "Redmond", "WA", "98052", "", "425-555-0101",
					             "425-555-0102", "")
				};
		}

		public bool HasMore
		{
			get
			{
				return idx < customers.Length;
			}
		}

		public Customer GetNext()
		{
			if (!HasMore)
				return null;

			return customers[idx++];
		}
	}
}