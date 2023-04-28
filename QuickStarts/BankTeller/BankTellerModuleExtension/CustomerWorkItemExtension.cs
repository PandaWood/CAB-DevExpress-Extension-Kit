using BankTellerModule.WorkItems.Customer;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.WinForms;

namespace CustomerMapExtensionModule
{
	[WorkItemExtension(typeof(CustomerWorkItem))]
	public class CustomerWorkItemExtension : WorkItemExtension
	{
		private CustomerMap mapView;

		protected override void OnActivated()
		{
			if (mapView != null) return;

			mapView = WorkItem.Items.AddNew<CustomerMap>();
			var info = new TabSmartPartInfo
			           	{
			           		Title = "Customer Map",
			           		Description = "Map of the customer location"
			           	};
			WorkItem.Workspaces[CustomerWorkItem.CUSTOMERDETAIL_TABWORKSPACE]?.Show(mapView, info);
		}
	}
}
