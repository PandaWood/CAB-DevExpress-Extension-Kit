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
using System.Windows.Forms;
using BankTellerCommon;
using BankTellerModule.Constants;
using BankTellerModule.Properties;
using BankTellerModule.WorkItems.Customer;
using CABDevExpress.SmartPartInfos;
using CABDevExpress.Workspaces;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Docking;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.Commands;
using Microsoft.Practices.CompositeUI.EventBroker;
using Microsoft.Practices.CompositeUI.Services;
using Microsoft.Practices.CompositeUI.SmartParts;

namespace BankTellerModule.WorkItems.BankTeller
{
	public class BankTellerWorkItem : WorkItem, IShowInShell
	{
		private BarItem queueItem;
		private IWorkspace contentWorkspace;

		// The work item uses the state persistence service that's been registered
		// in the shell initialization
		public IStatePersistenceService PersistenceService
		{
			get { return Services.Get<IStatePersistenceService>(); }
		}

		// Here we populate the work item with some of our views and start showing
		// ourselves. The BankTellerMainView has smart part placeholders named
		// UserInfo and CustomerList; these are filled in at runtime with the
		// smart parts that are registered with those names. We chose to put a
		// UserInfoView in the "UserInfo" placeholder, and a CustomerQueueView
		// in the "CustomerList" placeholder.
		//
		// Note that order is important here. When we create the BankTellerMainView,
		// it is going to assume that the smart parts that it needs already exist
		// in the work item. Therefore, we create the smart parts first, and then
		// create the main view that contains them.
		public void Show(IWorkspace navbarWorkspace, IWorkspace content)
		{
			contentWorkspace = content;

			AddMenuItems();
			AddShowNavBarGroups(navbarWorkspace);
			Activate();
		}

		private void AddShowNavBarGroups(IWorkspace navbarWorkspace)
		{
			SmartParts.AddNew<UserInfoView>("UserInfo");	//named because it will be used in a placeholder
			var customerView = SmartParts.AddNew<CustomerView>();
			var statisticsBarView = SmartParts.AddNew<StatisticsBarView>(SmartPartNames.Statistics);

			var customerInfo = new XtraNavBarGroupSmartPartInfo
			                   	{
			                   		Title = "Customers",
			                   		LargeImage = Resources.customersLarge,
			                   		SmallImage = Resources.customersSmall
			                   	};

			var statsInfo = new XtraNavBarGroupSmartPartInfo
			                	{
			                		Title = "Statistics",
			                		LargeImage = Resources.statsLarge,
			                		SmallImage = Resources.statsSmall
			                	};

			navbarWorkspace.Show(customerView, customerInfo);
			navbarWorkspace.Show(statisticsBarView, statsInfo);
		}

		private void AddMenuItems()
		{
			if (queueItem != null) return;

            const string acceptCustomerCaption = "Accept Customer";
            var acceptCustomer = new BarButtonItem
            {
                Caption = acceptCustomerCaption,
                Hint = acceptCustomerCaption,
                Glyph = Resources.AcceptCustomer16,
                LargeGlyph = Resources.AcceptCustomer32,
                ItemShortcut = new BarShortcut(Keys.Control | Keys.A)
            };

            string extensionSite;
#if UseRibbonForm
            extensionSite = ExtensionSiteNames.File;
            // add also the the Quick Access Toolbar and to the ApplicationMenu
            UIExtensionSites[ExtensionSiteNames.RibbonQuickAccessToolbar].Add(acceptCustomer);
            UIExtensionSites[ExtensionSiteNames.RibbonApplicationMenu].Add(acceptCustomer);
#else
            queueItem = new BarSubItem { Caption = "Queue" };

            UIExtensionSites[ExtensionSiteNames.File].Add(queueItem);
            UIExtensionSites.RegisterSite(ExtensionSiteNames.Queue, queueItem);
            extensionSite = ExtensionSiteNames.Queue;
            UIExtensionSites[ExtensionSiteNames.ToolBar].Add(acceptCustomer);
#endif
            UIExtensionSites[extensionSite].Add(acceptCustomer);
			Commands[CommandNames.AcceptCustomer].AddInvoker(acceptCustomer, "ItemClick");
		}

		private bool ShowQueueMenu
		{
			set
			{
				//if (queueItem != null && queueItem.Visibility != value)
				if (queueItem != null && ((queueItem.Visibility == BarItemVisibility.Always ? true : false) != value))
				{
					queueItem.Visibility = value ? BarItemVisibility.Always : BarItemVisibility.Never;
				}
			}
		}

		protected override void OnActivated()
		{
			base.OnActivated();

			ShowQueueMenu = true;
		}

		// When the user clicks on a customer in their customer queue, the
		// CustomerQueueController calls us to tell us to start working with
		// the customer.
		//
		// Editing a customer is self-contained in a work item (the CustomerWorkItem)
		// so we end up with one CustomerWorkItem contained in ourselves for
		// each customer that is being edited.
		public void WorkWithCustomer(BankTellerCommon.Customer customer)
		{
			// Construct a key to register the work item in ourselves
			string key = string.Format("Customer#{0}", customer.ID);

			// Have we already made the work item for this customer?
			// If so, return the existing one.
			var workItem = WorkItems.Get<CustomerWorkItem>(key);

			if (workItem == null)
			{
				workItem = WorkItems.AddNew<CustomerWorkItem>(key);
				//Set ID before setting state.  State will be cleared if a new id is set.
				workItem.ID = key;
				workItem.State[WorkItemStates.Customer] = customer;
				
				// Ask the persistence service if we have a saved version of
				// this work item. If so, load it from persistence.
				if (PersistenceService != null && PersistenceService.Contains(workItem.ID))
					workItem.Load();
			}

			workItem.Show(contentWorkspace);
		}

        /// <summary>
        /// Usage sample for the CABDevExpress.Extension Kit XtraWindowWorkspace and XtraWindowSmartPartInfo
        /// the example shows an 'About Dialog'
        /// </summary>
        private void ShowHelpAbout()
        {
            if (!SmartParts.Contains(SmartPartNames.HelpAbout))
                SmartParts.AddNew<AboutBankTellerView>(SmartPartNames.HelpAbout);

            var smartPartInfo = new XtraWindowSmartPartInfo
                                    {
                                        Modal = true,
                                        StartPosition = FormStartPosition.CenterParent,
                                        FormBorderStyle = FormBorderStyle.FixedDialog,
                                        MinimizeBox = false,
                                        MaximizeBox = false,
                                        Height = 150,
                                        Width = 350,
                                        Title = "About"
                                    };

            // the two properties added by CABDevExpress.ExtensionKit's XtraWindowSmartPartInfo
            var xtraWindow = new XtraWindowWorkspace();
            xtraWindow.Show(SmartParts[SmartPartNames.HelpAbout], smartPartInfo);
        }

		[CommandHandler(CommandNames.HelpAbout)]
		public void OnHelpAbout(object sender, EventArgs e)
		{
            ShowHelpAbout();
        }

        [EventSubscription(EventNames.HelpAbout, ThreadOption.UserInterface)]
        public void OnHelpAboutEvent(object sender, EventArgs e)
        {
            ShowHelpAbout();
        }

		const string DockableStatisticsView = "DockableStatisticsView";

		/// <summary>
		/// Usage sample for the CABDevExpress.Extension Kit DockManagerWorkspace
		/// </summary>
		/// <param name="dockWorkspace"></param>
		public void Show(IWorkspace dockWorkspace)
		{
			var info = new DockManagerSmartPartInfo {Name = "Statistics", Dock = DockingStyle.Right};

			SmartParts.AddNew<StatisticsBarView>(DockableStatisticsView);
			dockWorkspace.Show(SmartParts[DockableStatisticsView], info);
		}
	}
}