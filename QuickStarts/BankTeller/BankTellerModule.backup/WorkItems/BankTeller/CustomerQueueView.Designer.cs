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

using DevExpress.XtraEditors;

namespace BankTellerModule.WorkItems.BankTeller
{
	partial class CustomerQueueView
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
			this.panel1 = new DevExpress.XtraEditors.PanelControl();
			this.btnNextCustomer = new DevExpress.XtraEditors.SimpleButton();
			this.listCustomers = new DevExpress.XtraEditors.ListBoxControl();
			this.label1 = new DevExpress.XtraEditors.LabelControl();
			((System.ComponentModel.ISupportInitialize)(this.panel1)).BeginInit();
			this.panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.listCustomers)).BeginInit();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.btnNextCustomer);
			this.panel1.Controls.Add(this.listCustomers);
			this.panel1.Controls.Add(this.label1);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Padding = new System.Windows.Forms.Padding(5);
			this.panel1.Size = new System.Drawing.Size(189, 177);
			this.panel1.TabIndex = 0;
			// 
			// btnNextCustomer
			// 
			this.btnNextCustomer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.btnNextCustomer.Location = new System.Drawing.Point(8, 28);
			this.btnNextCustomer.Name = "btnNextCustomer";
			this.btnNextCustomer.Size = new System.Drawing.Size(173, 26);
			this.btnNextCustomer.TabIndex = 5;
			this.btnNextCustomer.Text = "Accept Customer";
			this.btnNextCustomer.Click += new System.EventHandler(this.OnAcceptCustomer);
			// 
			// listCustomers
			// 
			this.listCustomers.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.listCustomers.DisplayMember = "Count";
			this.listCustomers.Location = new System.Drawing.Point(8, 61);
			this.listCustomers.Name = "listCustomers";
			this.listCustomers.Size = new System.Drawing.Size(173, 104);
			this.listCustomers.TabIndex = 4;
			this.listCustomers.ValueMember = "Count";
			this.listCustomers.SelectedIndexChanged += new System.EventHandler(this.OnCustomerSelectionChanged);
			// 
			// label1
			// 
			this.label1.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Appearance.Options.UseFont = true;
			this.label1.Location = new System.Drawing.Point(8, 5);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(113, 20);
			this.label1.TabIndex = 3;
			this.label1.Text = "My Customers";
			// 
			// CustomerQueueView
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.panel1);
			this.Name = "CustomerQueueView";
			this.Size = new System.Drawing.Size(189, 177);
			((System.ComponentModel.ISupportInitialize)(this.panel1)).EndInit();
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.listCustomers)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private PanelControl panel1;
		private SimpleButton btnNextCustomer;
		private ListBoxControl listCustomers;
		private LabelControl label1;

	}
}