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
            this.skinsBar = new DevExpress.XtraBars.Bar();
            this.mainBar = new DevExpress.XtraBars.Bar();
            this.windowToolBar = new DevExpress.XtraBars.Bar();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.dockManager = new DevExpress.XtraBars.Docking.DockManager(this.components);
            this.navBarWorkspace = new CABDevExpress.Workspaces.XtraNavBarWorkspace();
            this.xtraTabbedMdiManager = new DevExpress.XtraTabbedMdi.XtraTabbedMdiManager(this.components);
            this.splitterControl1 = new DevExpress.XtraEditors.SplitterControl();
            ((System.ComponentModel.ISupportInitialize)(this.barManager)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.navBarWorkspace)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabbedMdiManager)).BeginInit();
            this.SuspendLayout();
            // 
            // barManager
            // 
            this.barManager.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.mainMenuBar,
            this.mainStatusBar,
            this.skinsBar,
            this.mainBar,
            this.windowToolBar});
            this.barManager.DockControls.Add(this.barDockControlTop);
            this.barManager.DockControls.Add(this.barDockControlBottom);
            this.barManager.DockControls.Add(this.barDockControlLeft);
            this.barManager.DockControls.Add(this.barDockControlRight);
            this.barManager.DockManager = this.dockManager;
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
            this.mainMenuBar.BarName = "Main Menu";
            this.mainMenuBar.DockCol = 0;
            this.mainMenuBar.DockRow = 0;
            this.mainMenuBar.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.mainMenuBar.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barSubItemFile)});
            this.mainMenuBar.OptionsBar.MultiLine = true;
            this.mainMenuBar.OptionsBar.UseWholeRow = true;
            this.mainMenuBar.Text = "Main Menu";
            // 
            // barSubItemFile
            // 
            this.barSubItemFile.Caption = "File";
            this.barSubItemFile.Id = 0;
            this.barSubItemFile.Name = "barSubItemFile";
            // 
            // mainStatusBar
            // 
            this.mainStatusBar.BarName = "Status Bar";
            this.mainStatusBar.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Bottom;
            this.mainStatusBar.DockCol = 0;
            this.mainStatusBar.DockRow = 0;
            this.mainStatusBar.DockStyle = DevExpress.XtraBars.BarDockStyle.Bottom;
            this.mainStatusBar.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barStaticItem1)});
            this.mainStatusBar.OptionsBar.AllowQuickCustomization = false;
            this.mainStatusBar.OptionsBar.DrawDragBorder = false;
            this.mainStatusBar.OptionsBar.UseWholeRow = true;
            this.mainStatusBar.Text = "Status Bar";
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
            // skinsBar
            // 
            this.skinsBar.BarName = "Skins";
            this.skinsBar.DockCol = 2;
            this.skinsBar.DockRow = 1;
            this.skinsBar.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.skinsBar.Offset = 46;
            this.skinsBar.Text = "Skins";
            this.skinsBar.Visible = false;
            // 
            // mainBar
            // 
            this.mainBar.BarName = "Tools";
            this.mainBar.DockCol = 0;
            this.mainBar.DockRow = 1;
            this.mainBar.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.mainBar.Text = "Tools";
            // 
            // windowToolBar
            // 
            this.windowToolBar.BarName = "Windows";
            this.windowToolBar.DockCol = 1;
            this.windowToolBar.DockRow = 1;
            this.windowToolBar.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.windowToolBar.Offset = 43;
            this.windowToolBar.Text = "Windows";
            // 
            // dockManager
            // 
            this.dockManager.Form = this;
            this.dockManager.TopZIndexControls.AddRange(new string[] {
            "DevExpress.XtraBars.BarDockControl",
            "System.Windows.Forms.StatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonStatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonControl"});
            // 
            // navBarWorkspace
            // 
            this.navBarWorkspace.ActiveGroup = null;
            this.navBarWorkspace.ContentButtonHint = null;
            this.navBarWorkspace.Dock = System.Windows.Forms.DockStyle.Left;
            this.navBarWorkspace.Location = new System.Drawing.Point(0, 50);
            this.navBarWorkspace.Name = "navBarWorkspace";
            this.navBarWorkspace.OptionsNavPane.ExpandedWidth = 196;
            this.navBarWorkspace.Size = new System.Drawing.Size(196, 434);
            this.navBarWorkspace.TabIndex = 1;
            this.navBarWorkspace.View = new DevExpress.XtraNavBar.ViewInfo.SkinNavigationPaneViewInfoRegistrator();
            // 
            // xtraTabbedMdiManager
            // 
            this.xtraTabbedMdiManager.MdiParent = this;
            // 
            // splitterControl1
            // 
            this.splitterControl1.Location = new System.Drawing.Point(196, 50);
            this.splitterControl1.Name = "splitterControl1";
            this.splitterControl1.Size = new System.Drawing.Size(6, 434);
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
            ((System.ComponentModel.ISupportInitialize)(this.dockManager)).EndInit();
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
        private DevExpress.XtraBars.Docking.DockManager dockManager;
        public DevExpress.XtraBars.Bar skinsBar;
        public DevExpress.XtraBars.Bar mainBar;
        public DevExpress.XtraBars.Bar windowToolBar;
	}
}

