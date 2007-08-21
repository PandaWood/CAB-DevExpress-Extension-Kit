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
using DevExpress.Utils;
using DevExpress.Utils.Menu;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Menu;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.SmartParts;

namespace BankTellerModule
{
	[SmartPart]
	public partial class CustomerAccountsView : XtraUserControl
	{
		private Customer customer;
		private CustomerAccountService accountService;
		private WorkItem workItem;

		private readonly DXMenuItem popupMenuItem = new DXMenuItem("Say Hello...");


		// The Customer state is stored in our parent work item
		[State]
		public Customer Customer
		{
			set { customer = value; }
		}

		// Make sure our required CustomerAccountService is available
		[ServiceDependency]
		public CustomerAccountService AccountService
		{
			set { accountService = value; }
		}

		[ServiceDependency]
		public WorkItem ParentWorkItem
		{
			protected get { return workItem; }
			set { workItem = value; }
		}

		public CustomerAccountsView()
		{
			InitializeComponent();

			gridAccountsView.MouseDown += grid_MouseDown;
		}

		void grid_MouseDown(object sender, MouseEventArgs e)
		{
			bool RightMouseClicked = (e.Button & MouseButtons.Right) != 0;
			if (RightMouseClicked)
			{
				GridView view = sender as GridView;
				DXMouseEventArgs mouseEventArgs = e as DXMouseEventArgs;
				if (view == null || mouseEventArgs == null)
					return;

				GridHitInfo hitInfo = view.CalcHitInfo(mouseEventArgs.Location);
				view.FocusedRowHandle = hitInfo.RowHandle;

				CreateAndShowPopupMenu(hitInfo, view);
			}
		}

		protected void CreateAndShowPopupMenu(GridHitInfo hitInfo, GridView view)
		{
			ViewMenu menu = new ViewMenu(view);
			menu.Items.Add(popupMenuItem);
			menu.Show(hitInfo.HitPoint);
		}

		private void CustomerAccountsView_Load(object sender, EventArgs e)
		{
			HookupCabEvents();

			if (customer != null)
			{
				gridAccounts.DataSource = accountService.GetByCustomerID(customer.ID);
			}
		}

		private void HookupCabEvents()
		{
			ParentWorkItem.Commands[CommandConstants.HELLOFROMDXMENU].AddInvoker(popupMenuItem, "Click");
		}
	}
}
