using System;
using DevExpress.XtraEditors;
using Microsoft.Practices.CompositeUI.SmartParts;

namespace BankTellerModule.WorkItems.BankTeller
{
	[SmartPart]
	public partial class CustomerView : XtraUserControl, ISmartPartInfoProvider
	{
		public CustomerView()
		{
			InitializeComponent();
		}

		public ISmartPartInfo GetSmartPartInfo(Type smartPartInfoType)
		{
			return infoProvider.GetSmartPartInfo(smartPartInfoType);
		}
	}
}