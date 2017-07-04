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
using BankTellerModule.Constants;
using BankTellerModule.Services;
using DevExpress.Utils;
using DevExpress.Utils.Menu;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Menu;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.SmartParts;

namespace BankTellerModule.WorkItems.Customer
{
	[SmartPart]
	public partial class CustomerAccountsView : XtraUserControl
	{
		private BankTellerCommon.Customer customer;
		private CustomerAccountService accountService;

		private readonly DXMenuItem popupMenuItem = new DXMenuItem("Say Hello...");


		// The Customer state is stored in our parent work item
		[State]
		public BankTellerCommon.Customer Customer
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
		public WorkItem ParentWorkItem { protected get; set; }

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
				var view = sender as GridView;
				var mouseEventArgs = e as DXMouseEventArgs;
				if (view == null || mouseEventArgs == null)
					return;

				GridHitInfo hitInfo = view.CalcHitInfo(mouseEventArgs.Location);
				view.FocusedRowHandle = hitInfo.RowHandle;

				CreateAndShowPopupMenu(hitInfo, view);
			}
		}

		protected void CreateAndShowPopupMenu(GridHitInfo hitInfo, GridView view)
		{
			var menu = new ViewMenu(view);
			menu.Items.Add(popupMenuItem);
			menu.Show(hitInfo.HitPoint);
		}

		private void CustomerAccountsView_Load(object sender, EventArgs e)
		{
			HookupCabEvents();

			if (customer != null)
				gridAccounts.DataSource = accountService.GetByCustomerID(customer.ID);
		}

		private void HookupCabEvents()
		{
            if (ParentWorkItem != null)
                ParentWorkItem.Commands[CommandNames.DxMenuSamplePopup].AddInvoker(popupMenuItem, "Click");
		}

        private void gridAccounts_DragDrop(object sender, DragEventArgs e)
        {
            MessageBox.Show("DEVEXPRESS-gridAccounts_DragDrop event!");
        }

        private void gridAccounts_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = System.Windows.Forms.DragDropEffects.Link;
            if (e.Data.GetDataPresent("FileGroupDescriptor"))
                e.Effect = System.Windows.Forms.DragDropEffects.Copy;
        }

        private void CustomerAccountsView_DragEnter(object sender, DragEventArgs e)
        {
            MessageBox.Show("DEVEXPRESS-CustomerAccountsView_DragEnter event!");
        }
    }
}