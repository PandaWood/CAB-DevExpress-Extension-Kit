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
	partial class BankShellRibbonForm
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
            this.barStaticItem1 = new DevExpress.XtraBars.BarStaticItem();
            this.dockManager = new DevExpress.XtraBars.Docking.DockManager(this.components);
            this.navBarWorkspace = new CABDevExpress.Workspaces.XtraNavBarWorkspace();
            this.splitterControl1 = new DevExpress.XtraEditors.SplitterControl();
            this.ribbonControl1 = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.applicationMenu = new DevExpress.XtraBars.Ribbon.ApplicationMenu(this.components);
            this.homePage = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.mainStatusBar = new DevExpress.XtraBars.Ribbon.RibbonStatusBar();
            this.deckWorkspace1 = new Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.navBarWorkspace)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.applicationMenu)).BeginInit();
            this.SuspendLayout();
            // 
            // barStaticItem1
            // 
            this.barStaticItem1.Caption = " 123 ";
            this.barStaticItem1.Id = 2;
            this.barStaticItem1.Name = "barStaticItem1";
            this.barStaticItem1.TextAlignment = System.Drawing.StringAlignment.Near;
            // 
            // dockManager
            // 
            this.dockManager.Form = this;
            this.dockManager.TopZIndexControls.AddRange(new string[] {
            "DevExpress.XtraBars.BarDockControl",
            "System.Windows.Forms.StatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonStatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonControl"});
            this.dockManager.ActivePanelChanged += new DevExpress.XtraBars.Docking.ActivePanelChangedEventHandler(this.dockManager_ActivePanelChanged);
            this.dockManager.Collapsing += new DevExpress.XtraBars.Docking.DockPanelEventHandler(this.dockManager_Collapsing);
            // 
            // navBarWorkspace
            // 
            this.navBarWorkspace.ActiveGroup = null;
            this.navBarWorkspace.ContentButtonHint = null;
            this.navBarWorkspace.Dock = System.Windows.Forms.DockStyle.Left;
            this.navBarWorkspace.Location = new System.Drawing.Point(0, 143);
            this.navBarWorkspace.Name = "navBarWorkspace";
            this.navBarWorkspace.OptionsNavPane.ExpandedWidth = 196;
            this.navBarWorkspace.Size = new System.Drawing.Size(196, 555);
            this.navBarWorkspace.TabIndex = 1;
            this.navBarWorkspace.View = new DevExpress.XtraNavBar.ViewInfo.SkinNavigationPaneViewInfoRegistrator();
            // 
            // splitterControl1
            // 
            this.splitterControl1.Location = new System.Drawing.Point(196, 143);
            this.splitterControl1.Name = "splitterControl1";
            this.splitterControl1.Size = new System.Drawing.Size(5, 555);
            this.splitterControl1.TabIndex = 2;
            this.splitterControl1.TabStop = false;
            // 
            // ribbonControl1
            // 
            this.ribbonControl1.ApplicationButtonDropDownControl = this.applicationMenu;
            this.ribbonControl1.ExpandCollapseItem.Id = 0;
            this.ribbonControl1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbonControl1.ExpandCollapseItem,
            this.barStaticItem1});
            this.ribbonControl1.Location = new System.Drawing.Point(0, 0);
            this.ribbonControl1.MaxItemId = 1;
            this.ribbonControl1.Name = "ribbonControl1";
            this.ribbonControl1.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.homePage});
            this.ribbonControl1.Size = new System.Drawing.Size(861, 143);
            this.ribbonControl1.StatusBar = this.mainStatusBar;
            // 
            // applicationMenu
            // 
            this.applicationMenu.MenuDrawMode = DevExpress.XtraBars.MenuDrawMode.LargeImagesText;
            this.applicationMenu.Name = "applicationMenu";
            this.applicationMenu.Ribbon = this.ribbonControl1;
            // 
            // homePage
            // 
            this.homePage.Name = "homePage";
            this.homePage.Text = "Home";
            // 
            // mainStatusBar
            // 
            this.mainStatusBar.ItemLinks.Add(this.barStaticItem1);
            this.mainStatusBar.Location = new System.Drawing.Point(0, 698);
            this.mainStatusBar.Name = "mainStatusBar";
            this.mainStatusBar.Ribbon = this.ribbonControl1;
            this.mainStatusBar.Size = new System.Drawing.Size(861, 31);
            // 
            // deckWorkspace1
            // 
            this.deckWorkspace1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.deckWorkspace1.Location = new System.Drawing.Point(201, 143);
            this.deckWorkspace1.Name = "deckWorkspace1";
            this.deckWorkspace1.Size = new System.Drawing.Size(660, 555);
            this.deckWorkspace1.TabIndex = 6;
            this.deckWorkspace1.Text = "deckWorkspace1";
            // 
            // BankShellRibbonForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(861, 729);
            this.Controls.Add(this.deckWorkspace1);
            this.Controls.Add(this.splitterControl1);
            this.Controls.Add(this.navBarWorkspace);
            this.Controls.Add(this.mainStatusBar);
            this.Controls.Add(this.ribbonControl1);
            this.IsMdiContainer = true;
            this.Name = "BankShellRibbonForm";
            this.Ribbon = this.ribbonControl1;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.StatusBar = this.mainStatusBar;
            this.Text = "Bank Shell";
            ((System.ComponentModel.ISupportInitialize)(this.dockManager)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.navBarWorkspace)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.applicationMenu)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

        private DevExpress.XtraBars.BarStaticItem barStaticItem1;
        private CABDevExpress.Workspaces.XtraNavBarWorkspace navBarWorkspace;
        private SplitterControl splitterControl1;
		private DevExpress.XtraBars.Docking.DockManager dockManager;
        private DevExpress.XtraBars.Ribbon.RibbonControl ribbonControl1;
        internal DevExpress.XtraBars.Ribbon.RibbonStatusBar mainStatusBar;
        internal DevExpress.XtraBars.Ribbon.RibbonPage homePage;
        internal DevExpress.XtraBars.Ribbon.ApplicationMenu applicationMenu;
        private Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace deckWorkspace1;
    }
}

