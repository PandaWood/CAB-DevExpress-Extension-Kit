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

namespace BankTellerCommon
{
	[Serializable]
	public class Customer
	{
		private readonly int id;

		public Customer(int id, string firstName, string lastName, string address1, string address2,
		                string city, string state, string zipCode, string emailAddress, string phone1, string phone2,
		                string comments)
		{
			this.id = id;
			FirstName = firstName;
			LastName = lastName;
			Address1 = address1;
			Address2 = address2;
			City = city;
			State = state;
			ZipCode = zipCode;
			EmailAddress = emailAddress;
			Phone1 = phone1;
			Phone2 = phone2;
			Comments = comments;
		}

		public int ID
		{
			get { return id; }
		}

		public string Address1 { get; set; }
		public string Address2 { get; set; }
		public string City { get; set; }
		public string Comments { get; set; }
		public string EmailAddress { get; set; }
		public string FirstName { get; set; }
		
		public string LastName { get; set; }
		public string Phone1 { get; set; }
		public string Phone2 { get; set; }
		public string State { get; set; }
		public string ZipCode { get; set; }

		public override string ToString()
		{
			return string.Format("{0}, {1}", LastName, FirstName);
		}
	}
}