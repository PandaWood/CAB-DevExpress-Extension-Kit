using Microsoft.Practices.CompositeUI.SmartParts;

namespace BankTellerModule.WorkItems.BankTeller
{
	partial class CustomerView
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
			this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
			this.smartPartPlaceholder1 = new Microsoft.Practices.CompositeUI.WinForms.SmartPartPlaceholder();
			this.customerQueueView1 = new BankTellerModule.WorkItems.BankTeller.CustomerQueueView();
			this.infoProvider = new Microsoft.Practices.CompositeUI.SmartParts.SmartPartInfoProvider();
			((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
			this.splitContainerControl1.SuspendLayout();
			this.SuspendLayout();
			// 
			// splitContainerControl1
			// 
			this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainerControl1.Horizontal = false;
			this.splitContainerControl1.Location = new System.Drawing.Point(0, 0);
			this.splitContainerControl1.Name = "splitContainerControl1";
			this.splitContainerControl1.Panel1.Controls.Add(this.smartPartPlaceholder1);
			this.splitContainerControl1.Panel1.Text = "splitContainerControl1_Panel1";
			this.splitContainerControl1.Panel2.Controls.Add(this.customerQueueView1);
			this.splitContainerControl1.Panel2.Text = "splitContainerControl1_Panel2";
			this.splitContainerControl1.Size = new System.Drawing.Size(200, 263);
			this.splitContainerControl1.SplitterPosition = 47;
			this.splitContainerControl1.TabIndex = 1;
			this.splitContainerControl1.Text = "splitContainerControl1";
			// 
			// smartPartPlaceholder1
			// 
			this.smartPartPlaceholder1.BackColor = System.Drawing.Color.Transparent;
			this.smartPartPlaceholder1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.smartPartPlaceholder1.Location = new System.Drawing.Point(0, 0);
			this.smartPartPlaceholder1.Name = "smartPartPlaceholder1";
			this.smartPartPlaceholder1.Size = new System.Drawing.Size(196, 43);
			this.smartPartPlaceholder1.SmartPartName = "UserInfo";
			this.smartPartPlaceholder1.TabIndex = 0;
			this.smartPartPlaceholder1.Text = "smartPartPlaceholder1";
			// 
			// customerQueueView1
			// 
			this.customerQueueView1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.customerQueueView1.Location = new System.Drawing.Point(0, 0);
			this.customerQueueView1.Name = "customerQueueView1";
			this.customerQueueView1.Size = new System.Drawing.Size(196, 206);
			this.customerQueueView1.TabIndex = 0;
			// 
			// CustomerView
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.splitContainerControl1);
			this.Name = "CustomerView";
			this.Size = new System.Drawing.Size(200, 263);
			((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
			this.splitContainerControl1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private Microsoft.Practices.CompositeUI.WinForms.SmartPartPlaceholder smartPartPlaceholder1;
		private CustomerQueueView customerQueueView1;
		private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
		private SmartPartInfoProvider infoProvider;
	}
}