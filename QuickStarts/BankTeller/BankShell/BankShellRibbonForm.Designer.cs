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
            components = new System.ComponentModel.Container();
            barStaticItem1 = new DevExpress.XtraBars.BarStaticItem();
            dockManager = new DevExpress.XtraBars.Docking.DockManager(components);
            navBarWorkspace = new CABDevExpress.Workspaces.XtraNavBarWorkspace();
            splitterControl1 = new SplitterControl();
            ribbonControl1 = new DevExpress.XtraBars.Ribbon.RibbonControl();
            applicationMenu = new DevExpress.XtraBars.Ribbon.ApplicationMenu(components);
            homePage = new DevExpress.XtraBars.Ribbon.RibbonPage();
            mainStatusBar = new DevExpress.XtraBars.Ribbon.RibbonStatusBar();
            deckWorkspace1 = new Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace();
            ribbonPage1 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            ribbonPageGroup1 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            ((System.ComponentModel.ISupportInitialize)dockManager).BeginInit();
            ((System.ComponentModel.ISupportInitialize)navBarWorkspace).BeginInit();
            ((System.ComponentModel.ISupportInitialize)ribbonControl1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)applicationMenu).BeginInit();
            SuspendLayout();
            // 
            // barStaticItem1
            // 
            barStaticItem1.Caption = " 123 ";
            barStaticItem1.Id = 2;
            barStaticItem1.Name = "barStaticItem1";
            // 
            // dockManager
            // 
            dockManager.Form = this;
            dockManager.TopZIndexControls.AddRange(new string[] { "DevExpress.XtraBars.BarDockControl", "System.Windows.Forms.StatusBar", "DevExpress.XtraBars.Ribbon.RibbonStatusBar", "DevExpress.XtraBars.Ribbon.RibbonControl" });
            dockManager.ActivePanelChanged += dockManager_ActivePanelChanged;
            dockManager.Collapsing += dockManager_Collapsing;
            // 
            // navBarWorkspace
            // 
            navBarWorkspace.ContentButtonHint = null;
            navBarWorkspace.Dock = System.Windows.Forms.DockStyle.Left;
            navBarWorkspace.Location = new System.Drawing.Point(0, 143);
            navBarWorkspace.Name = "navBarWorkspace";
            navBarWorkspace.OptionsNavPane.ExpandedWidth = 196;
            navBarWorkspace.Size = new System.Drawing.Size(196, 555);
            navBarWorkspace.TabIndex = 1;
            navBarWorkspace.View = new DevExpress.XtraNavBar.ViewInfo.SkinNavigationPaneViewInfoRegistrator();
            // 
            // splitterControl1
            // 
            splitterControl1.Location = new System.Drawing.Point(196, 143);
            splitterControl1.Name = "splitterControl1";
            splitterControl1.Size = new System.Drawing.Size(5, 555);
            splitterControl1.TabIndex = 2;
            splitterControl1.TabStop = false;
            // 
            // ribbonControl1
            // 
            ribbonControl1.ApplicationButtonDropDownControl = applicationMenu;
            ribbonControl1.ExpandCollapseItem.Id = 0;
            ribbonControl1.Items.AddRange(new DevExpress.XtraBars.BarItem[] { ribbonControl1.ExpandCollapseItem, barStaticItem1, ribbonControl1.SearchEditItem });
            ribbonControl1.Location = new System.Drawing.Point(0, 0);
            ribbonControl1.MaxItemId = 1;
            ribbonControl1.Name = "ribbonControl1";
            ribbonControl1.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] { homePage, ribbonPage1 });
            ribbonControl1.Size = new System.Drawing.Size(861, 143);
            ribbonControl1.StatusBar = mainStatusBar;
            // 
            // applicationMenu
            // 
            applicationMenu.MenuDrawMode = DevExpress.XtraBars.MenuDrawMode.LargeImagesText;
            applicationMenu.Name = "applicationMenu";
            applicationMenu.Ribbon = ribbonControl1;
            // 
            // homePage
            // 
            homePage.Name = "homePage";
            homePage.Text = "Home";
            // 
            // mainStatusBar
            // 
            mainStatusBar.ItemLinks.Add(barStaticItem1);
            mainStatusBar.Location = new System.Drawing.Point(0, 698);
            mainStatusBar.Name = "mainStatusBar";
            mainStatusBar.Ribbon = ribbonControl1;
            mainStatusBar.Size = new System.Drawing.Size(861, 31);
            // 
            // deckWorkspace1
            // 
            deckWorkspace1.Dock = System.Windows.Forms.DockStyle.Fill;
            deckWorkspace1.Location = new System.Drawing.Point(201, 143);
            deckWorkspace1.Name = "deckWorkspace1";
            deckWorkspace1.Size = new System.Drawing.Size(660, 555);
            deckWorkspace1.TabIndex = 6;
            deckWorkspace1.Text = "deckWorkspace1";
            // 
            // ribbonPage1
            // 
            ribbonPage1.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] { ribbonPageGroup1 });
            ribbonPage1.Name = "ribbonPage1";
            ribbonPage1.Text = "ribbonPage1";
            // 
            // ribbonPageGroup1
            // 
            ribbonPageGroup1.Name = "ribbonPageGroup1";
            ribbonPageGroup1.Text = "ribbonPageGroup1";
            // 
            // BankShellRibbonForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(861, 729);
            Controls.Add(deckWorkspace1);
            Controls.Add(splitterControl1);
            Controls.Add(navBarWorkspace);
            Controls.Add(mainStatusBar);
            Controls.Add(ribbonControl1);
            IsMdiContainer = true;
            Name = "BankShellRibbonForm";
            Ribbon = ribbonControl1;
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            StatusBar = mainStatusBar;
            Text = "Bank Shell";
            ((System.ComponentModel.ISupportInitialize)dockManager).EndInit();
            ((System.ComponentModel.ISupportInitialize)navBarWorkspace).EndInit();
            ((System.ComponentModel.ISupportInitialize)ribbonControl1).EndInit();
            ((System.ComponentModel.ISupportInitialize)applicationMenu).EndInit();
            ResumeLayout(false);
            PerformLayout();
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
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage1;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup1;
    }
}

