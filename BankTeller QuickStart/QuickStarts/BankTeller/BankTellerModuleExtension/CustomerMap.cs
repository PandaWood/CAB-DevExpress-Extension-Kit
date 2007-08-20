using System;
using BankTellerCommon;
using DevExpress.XtraEditors;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.SmartParts;

namespace CustomerMapExtensionModule
{
	[SmartPart]
	public partial class CustomerMap : XtraUserControl
	{
		private Customer customer;
		private bool mapLoaded = false;

		const string mapUrlFormat = "http://maps.msn.com/home.aspx?strt1={0}&city1={2}&stnm1={3}&zipc1={4}";

		[State(StateConstants.CUSTOMER)]
		public Customer Customer
		{
			set { customer = value; }
		}

		public CustomerMap()
		{
			InitializeComponent();
		}

		protected override void OnVisibleChanged(EventArgs e)
		{
			base.OnVisibleChanged(e);
			
			if (this.Visible && mapLoaded == false)
			{
				LoadMap();
				mapLoaded = true;
			}
		}
		
		private void LoadMap()
		{
			if (customer != null)
			{
				// does anybody realise that customer.Address2 below is not actually used?
				string url = String.Format(mapUrlFormat, customer.Address1, customer.Address2, customer.City, customer.State, customer.ZipCode);				
				browser.Navigate(Uri.EscapeUriString(url));
			}
		}
	}
}
