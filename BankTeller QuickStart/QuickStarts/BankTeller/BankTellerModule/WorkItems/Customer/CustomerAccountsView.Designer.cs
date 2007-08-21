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

namespace BankTellerModule
{
	partial class CustomerAccountsView
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.CustomerAccountBindingSource = new System.Windows.Forms.BindingSource(this.components);
			this.gridAccounts = new DevExpress.XtraGrid.GridControl();
			this.gridAccountsView = new DevExpress.XtraGrid.Views.Grid.GridView();
			((System.ComponentModel.ISupportInitialize)(this.CustomerAccountBindingSource)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gridAccounts)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gridAccountsView)).BeginInit();
			this.SuspendLayout();
			// 
			// CustomerAccountBindingSource
			// 
			this.CustomerAccountBindingSource.DataSource = typeof(BankTellerCommon.CustomerAccount);
			// 
			// gridAccounts
			// 
			this.gridAccounts.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gridAccounts.EmbeddedNavigator.Name = "";
			this.gridAccounts.Location = new System.Drawing.Point(0, 0);
			this.gridAccounts.MainView = this.gridAccountsView;
			this.gridAccounts.Name = "gridAccounts";
			this.gridAccounts.Size = new System.Drawing.Size(445, 271);
			this.gridAccounts.TabIndex = 2;
			this.gridAccounts.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridAccountsView});
			// 
			// gridView1
			// 
			this.gridAccountsView.GridControl = this.gridAccounts;
			this.gridAccountsView.Name = "gridView1";
			this.gridAccountsView.OptionsBehavior.Editable = false;
			this.gridAccountsView.OptionsSelection.EnableAppearanceFocusedCell = false;
			// 
			// CustomerAccountsView
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.gridAccounts);
			this.Name = "CustomerAccountsView";
			this.Size = new System.Drawing.Size(445, 271);
			this.Load += new System.EventHandler(this.CustomerAccountsView_Load);
			((System.ComponentModel.ISupportInitialize)(this.CustomerAccountBindingSource)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gridAccounts)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gridAccountsView)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

        private System.Windows.Forms.BindingSource CustomerAccountBindingSource;
        private DevExpress.XtraGrid.GridControl gridAccounts;
        private DevExpress.XtraGrid.Views.Grid.GridView gridAccountsView;



	}
}
