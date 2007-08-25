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
			if (mapView == null)
			{
				mapView = WorkItem.Items.AddNew<CustomerMap>();

				TabSmartPartInfo info = new TabSmartPartInfo();
				info.Title = "Customer Map";
				info.Description = "Map of the customer location";
				WorkItem.Workspaces[CustomerWorkItem.CUSTOMERDETAIL_TABWORKSPACE].Show(mapView, info);
			}
		}
	}
}
