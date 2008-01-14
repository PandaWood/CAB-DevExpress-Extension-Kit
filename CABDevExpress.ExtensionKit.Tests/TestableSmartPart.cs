using DevExpress.XtraEditors;
using Microsoft.Practices.CompositeUI.SmartParts;

namespace CABDevExpress.ExtensionKit.Tests
{
	[SmartPart]
	public class TestableSmartPart : XtraUserControl
	{
		private string _description;
		private string _title;

		public string Description
		{
			get { return _description; }
			set { _description = value; }
		}

		public string Title
		{
			get { return _title; }
			set { _title = value; }
		}
	}
}