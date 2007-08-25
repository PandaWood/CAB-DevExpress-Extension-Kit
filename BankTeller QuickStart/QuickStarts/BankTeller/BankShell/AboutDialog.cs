using System.Windows.Forms;
using Microsoft.Practices.CompositeUI.SmartParts;

namespace BankShell
{
	[SmartPart]
	public partial class AboutDialog : DevExpress.XtraEditors.XtraUserControl
	{
		public AboutDialog()
		{
			InitializeComponent();

			// this property is not visible in the VisualStudio Properties editor, so set here
			this.Dock = DockStyle.Fill;
		}
	}
}
