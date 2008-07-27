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
using System.Drawing;
using System.Windows.Forms;
using BankTellerModule.Constants;
using BankTellerModule.Properties;
using CABDevExpress.SmartPartInfos;
using DevExpress.XtraBars;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.Commands;
using Microsoft.Practices.CompositeUI.EventBroker;
using Microsoft.Practices.CompositeUI.SmartParts;
using Microsoft.Practices.CompositeUI.Utility;
using Microsoft.Practices.CompositeUI.WinForms;

namespace BankTellerModule.WorkItems.Customer
{
	public class CustomerWorkItem : WorkItem
	{
		public static readonly string CUSTOMERDETAIL_TABWORKSPACE = "tabbedWorkspace1";

		private BarItem editCustomerMenuItem1;
		private CustomerSummaryView customerSummaryView;
		private CustomerCommentsView commentsView;
		private BarStaticItem addressLabel;

		// This event is published to indicate that the module would like to
		// "update status". The only subscriber to this event today is the shell
		// which updates the status bar. These two components don't know anything
		// about one another, because they communicate indirectly via the
		// EventBroker. In reality, you can have any number of publishers and
		// any number of subscribers; in fact, other modules will undoubtedly
		// also publish status update events.
		[EventPublication(EventNames.StatusUpdate, PublicationScope.Global)]
		public event EventHandler<DataEventArgs<string>> UpdateStatusTextEvent;

		public void Show(IWorkspace parentWorkspace)
		{
			customerSummaryView = customerSummaryView ?? Items.AddNew<CustomerSummaryView>();

			AddMenuItems();

			var customer = (BankTellerCommon.Customer)State[WorkItemStates.Customer];

			OnStatusTextUpdate(string.Format("Editing {0}, {1}", customer.LastName, customer.FirstName));
			var smartPartInfo = new WindowSmartPartInfo
			                    	{
			                    		Title = (customer.LastName + " " + customer.FirstName)
			                    	};
			customerSummaryView.Dock = DockStyle.Fill;
			parentWorkspace.Show(customerSummaryView, smartPartInfo);

			UpdateUserAddressLabel(customer);

			Activate();

			// When activating, force focus on the first tab in the view.
			// Extensions may have added stuff at the end of the tab.
			customerSummaryView.FocusFirstTab();
		}

		private void UpdateUserAddressLabel(BankTellerCommon.Customer customer)
		{
			if (addressLabel != null) return;

			addressLabel = new BarStaticItem();
			UIExtensionSites[ExtensionSiteNames.MainStatus].Add(addressLabel);
			addressLabel.Caption = customer.Address1;
		}

		private void AddMenuItems()
		{
			if (editCustomerMenuItem1 != null) return;

            const string editCaption = "Edit";
            editCustomerMenuItem1 = new BarButtonItem 
                                        {
                                            Caption = editCaption,
                                            Hint = editCaption,
                                            Glyph = Resources.Edit16,
                                            LargeGlyph = Resources.Edit32
                                        };
#if UseRibbonForm
            UIExtensionSites[ExtensionSiteNames.File].Add(editCustomerMenuItem1);
#else
			UIExtensionSites[Resources.CustomerMenuExtensionSite].Add(editCustomerMenuItem1);
#endif
		}

		private void SetUIElementVisibility(bool visible)
		{
			if (editCustomerMenuItem1 != null)
				editCustomerMenuItem1.Visibility = visible ? BarItemVisibility.Always : BarItemVisibility.Never;

			if (addressLabel != null)
				addressLabel.Visibility = visible ? BarItemVisibility.Always : BarItemVisibility.Never;
		}

		// We watch for when we are activated (i.e., shown to
		// be worked on), we want to fire a status update event and show ourselves
		// in the provided workspace.
		protected override void OnActivated()
		{
			base.OnActivated();

			SetUIElementVisibility(true);
		}

		protected override void OnDeactivated()
		{
			base.OnDeactivated();

			SetUIElementVisibility(false);
		}

		protected virtual void OnStatusTextUpdate(string newText)
		{
			if (UpdateStatusTextEvent != null)
			{
				UpdateStatusTextEvent(this, new DataEventArgs<string>(newText));
			}
		}

		// This is called by CustomerDetailController when the user has indicated
		// that they want to show the comments. We dynamically create the comments
		// view and add it to our tab workspace.
		public void ShowCustomerComments()
		{
			CreateCommentsView();

			IWorkspace ws = Workspaces[CUSTOMERDETAIL_TABWORKSPACE];
			if (ws != null)
			{
				ws.Show(commentsView);
			}
		}

		private void CreateCommentsView()
		{
			commentsView = commentsView ?? Items.AddNew<CustomerCommentsView>();
			var info = new XtraTabSmartPartInfo
			           	{
			           		Title = "Title",
			           		Text = "Comments",
			           		PageHeaderFont = new Font("Comic Sans MS", 11.25F, FontStyle.Regular)
			           	};
			RegisterSmartPartInfo(commentsView, info);
		}

		//note let this string constant server to inform that it's not defined anywhere else in code
		[CommandHandler("CustomerMouseOver")]
		public void OnCustomerEdit(object sender, EventArgs args)
		{
			if (Status != WorkItemStatus.Active) return;

			Form form = customerSummaryView.ParentForm;
			string tooltipText = "This is customer work item " + ID;
			var toolTip = new ToolTip {IsBalloon = true};
			if (form != null) 
				toolTip.Show(tooltipText, form, form.Size.Width - 30, 30, 3000);
		}
	}
}