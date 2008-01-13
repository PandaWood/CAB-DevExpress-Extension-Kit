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

namespace BankShell
{
	partial class BankShellForm
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

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.barManager = new DevExpress.XtraBars.BarManager(this.components);
			this.mainMenuBar = new DevExpress.XtraBars.Bar();
			this.barSubItemFile = new DevExpress.XtraBars.BarSubItem();
			this.mainStatusBar = new DevExpress.XtraBars.Bar();
			this.barStaticItem1 = new DevExpress.XtraBars.BarStaticItem();
			this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
			this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
			this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
			this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
			this.navBarWorkspace = new CABDevExpress.Workspaces.XtraNavBarWorkspace();
			this.xtraTabbedMdiManager = new DevExpress.XtraTabbedMdi.XtraTabbedMdiManager(this.components);
			this.splitterControl1 = new DevExpress.XtraEditors.SplitterControl();
			((System.ComponentModel.ISupportInitialize)(this.barManager)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.navBarWorkspace)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.xtraTabbedMdiManager)).BeginInit();
			this.SuspendLayout();
			// 
			// barManager
			// 
			this.barManager.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.mainMenuBar,
            this.mainStatusBar});
			this.barManager.DockControls.Add(this.barDockControlTop);
			this.barManager.DockControls.Add(this.barDockControlBottom);
			this.barManager.DockControls.Add(this.barDockControlLeft);
			this.barManager.DockControls.Add(this.barDockControlRight);
			this.barManager.Form = this;
			this.barManager.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barSubItemFile,
            this.barStaticItem1});
			this.barManager.MainMenu = this.mainMenuBar;
			this.barManager.MaxItemId = 3;
			this.barManager.StatusBar = this.mainStatusBar;
			// 
			// mainMenuBar
			// 
			this.mainMenuBar.BarName = "Custom 1";
			this.mainMenuBar.DockCol = 0;
			this.mainMenuBar.DockRow = 0;
			this.mainMenuBar.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
			this.mainMenuBar.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barSubItemFile)});
			this.mainMenuBar.OptionsBar.MultiLine = true;
			this.mainMenuBar.OptionsBar.UseWholeRow = true;
			this.mainMenuBar.Text = "Custom 1";
			// 
			// barSubItemFile
			// 
			this.barSubItemFile.Caption = "File";
			this.barSubItemFile.Id = 0;
			this.barSubItemFile.Name = "barSubItemFile";
			// 
			// mainStatusBar
			// 
			this.mainStatusBar.BarName = "Custom 2";
			this.mainStatusBar.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Bottom;
			this.mainStatusBar.DockCol = 0;
			this.mainStatusBar.DockRow = 0;
			this.mainStatusBar.DockStyle = DevExpress.XtraBars.BarDockStyle.Bottom;
			this.mainStatusBar.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barStaticItem1)});
			this.mainStatusBar.OptionsBar.AllowQuickCustomization = false;
			this.mainStatusBar.OptionsBar.DrawDragBorder = false;
			this.mainStatusBar.OptionsBar.UseWholeRow = true;
			this.mainStatusBar.Text = "Custom 2";
			// 
			// barStaticItem1
			// 
			this.barStaticItem1.AutoSize = DevExpress.XtraBars.BarStaticItemSize.None;
			this.barStaticItem1.Caption = " 123 ";
			this.barStaticItem1.Id = 2;
			this.barStaticItem1.Name = "barStaticItem1";
			this.barStaticItem1.TextAlignment = System.Drawing.StringAlignment.Near;
			this.barStaticItem1.Width = 150;
			// 
			// navBarWorkspace
			// 
			this.navBarWorkspace.ActiveGroup = null;
			this.navBarWorkspace.ContentButtonHint = null;
			this.navBarWorkspace.Dock = System.Windows.Forms.DockStyle.Left;
			this.navBarWorkspace.Location = new System.Drawing.Point(0, 25);
			this.navBarWorkspace.Name = "navBarWorkspace";
			this.navBarWorkspace.OptionsNavPane.ExpandedWidth = 196;
			this.navBarWorkspace.Size = new System.Drawing.Size(196, 459);
			this.navBarWorkspace.TabIndex = 1;
			this.navBarWorkspace.View = new DevExpress.XtraNavBar.ViewInfo.SkinNavigationPaneViewInfoRegistrator();
			// 
			// xtraTabbedMdiManager
			// 
			this.xtraTabbedMdiManager.MdiParent = this;
			// 
			// splitterControl1
			// 
			this.splitterControl1.Location = new System.Drawing.Point(196, 25);
			this.splitterControl1.Name = "splitterControl1";
			this.splitterControl1.Size = new System.Drawing.Size(6, 459);
			this.splitterControl1.TabIndex = 2;
			this.splitterControl1.TabStop = false;
			// 
			// BankShellForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(657, 510);
			this.Controls.Add(this.splitterControl1);
			this.Controls.Add(this.navBarWorkspace);
			this.Controls.Add(this.barDockControlLeft);
			this.Controls.Add(this.barDockControlRight);
			this.Controls.Add(this.barDockControlBottom);
			this.Controls.Add(this.barDockControlTop);
			this.IsMdiContainer = true;
			this.LookAndFeel.SkinName = "Lilian";
			this.Name = "BankShellForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Bank Shell";
			((System.ComponentModel.ISupportInitialize)(this.barManager)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.navBarWorkspace)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.xtraTabbedMdiManager)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        public DevExpress.XtraBars.BarManager barManager;
        public DevExpress.XtraBars.Bar mainMenuBar;
        public DevExpress.XtraBars.BarSubItem barSubItemFile;
        public DevExpress.XtraBars.Bar mainStatusBar;
        private DevExpress.XtraBars.BarStaticItem barStaticItem1;
        private CABDevExpress.Workspaces.XtraNavBarWorkspace navBarWorkspace;
        public DevExpress.XtraTabbedMdi.XtraTabbedMdiManager xtraTabbedMdiManager;
        private SplitterControl splitterControl1;
	}
}

