namespace BankShell
{
	partial class AboutDialog
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
			this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
			this.lblMessage = new DevExpress.XtraEditors.LabelControl();
			this.lblBankTeller = new DevExpress.XtraEditors.LabelControl();
			this.urlCABDevExpress = new DevExpress.XtraEditors.HyperLinkEdit();
			((System.ComponentModel.ISupportInitialize)(this.urlCABDevExpress.Properties)).BeginInit();
			this.SuspendLayout();
			// 
			// simpleButton1
			// 
			this.simpleButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.simpleButton1.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.simpleButton1.Location = new System.Drawing.Point(242, 95);
			this.simpleButton1.Name = "simpleButton1";
			this.simpleButton1.Size = new System.Drawing.Size(75, 23);
			this.simpleButton1.TabIndex = 0;
			this.simpleButton1.Text = "Close";
			// 
			// lblMessage
			// 
			this.lblMessage.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblMessage.Appearance.Options.UseFont = true;
			this.lblMessage.Location = new System.Drawing.Point(12, 11);
			this.lblMessage.Name = "lblMessage";
			this.lblMessage.Size = new System.Drawing.Size(182, 16);
			this.lblMessage.TabIndex = 1;
			this.lblMessage.Text = "CAB DevExpress Extension Kit";
			// 
			// lblBankTeller
			// 
			this.lblBankTeller.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.lblBankTeller.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblBankTeller.Appearance.Options.UseFont = true;
			this.lblBankTeller.Location = new System.Drawing.Point(88, 64);
			this.lblBankTeller.Name = "lblBankTeller";
			this.lblBankTeller.Size = new System.Drawing.Size(160, 16);
			this.lblBankTeller.TabIndex = 2;
			this.lblBankTeller.Text = "Banker Teller Application";
			// 
			// urlCABDevExpress
			// 
			this.urlCABDevExpress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.urlCABDevExpress.EditValue = "http://www.codeplex.com/CABDevExpress";
			this.urlCABDevExpress.Location = new System.Drawing.Point(12, 31);
			this.urlCABDevExpress.Name = "urlCABDevExpress";
			this.urlCABDevExpress.Properties.AllowFocused = false;
			this.urlCABDevExpress.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
			this.urlCABDevExpress.Properties.Appearance.Options.UseBackColor = true;
			this.urlCABDevExpress.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
			this.urlCABDevExpress.Properties.ReadOnly = true;
			this.urlCABDevExpress.Size = new System.Drawing.Size(307, 18);
			this.urlCABDevExpress.TabIndex = 3;
			// 
			// AboutDialog
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.urlCABDevExpress);
			this.Controls.Add(this.lblBankTeller);
			this.Controls.Add(this.lblMessage);
			this.Controls.Add(this.simpleButton1);
			this.MinimumSize = new System.Drawing.Size(320, 100);
			this.Name = "AboutDialog";
			this.Size = new System.Drawing.Size(320, 121);
			((System.ComponentModel.ISupportInitialize)(this.urlCABDevExpress.Properties)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private DevExpress.XtraEditors.SimpleButton simpleButton1;
		private DevExpress.XtraEditors.LabelControl lblMessage;
		private DevExpress.XtraEditors.LabelControl lblBankTeller;
		private DevExpress.XtraEditors.HyperLinkEdit urlCABDevExpress;
	}
}
