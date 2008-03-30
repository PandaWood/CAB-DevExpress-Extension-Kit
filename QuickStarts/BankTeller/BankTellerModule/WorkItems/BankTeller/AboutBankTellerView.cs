using System;
using System.Windows.Forms;
using Microsoft.Practices.CompositeUI.SmartParts;

namespace BankTellerModule.WorkItems.BankTeller
{
	[SmartPart]
	public partial class AboutBankTellerView : DevExpress.XtraEditors.XtraUserControl
	{
		public AboutBankTellerView()
		{
			InitializeComponent();
		}

		protected override void OnLoad(EventArgs e)
		{
			this.Dock = DockStyle.Fill;
		}
	}
}