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
using Microsoft.Practices.CompositeUI;
namespace BankTellerModule.WorkItems.Customer
{
    partial class TabbedViewCustomerSummaryView
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
            if (controller.WorkItem.Status != WorkItemStatus.Terminated)
                controller.WorkItem.Terminate();
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
            components = new System.ComponentModel.Container();
            customerAccountsView1 = new CustomerAccountsView();
            customerDetailView1 = new CustomerDetailView();
            tabbedWorkspace1 = new CABDevExpress.Workspaces.XtraTabbedViewWorkspace();
            SaveButton = new SimpleButton();
            customerContextMenu = new System.Windows.Forms.ContextMenuStrip(components);
            infoProvider = new Microsoft.Practices.CompositeUI.SmartParts.SmartPartInfoProvider();
            barManager1 = new DevExpress.XtraBars.BarManager(components);
            barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            documentGroup1 = new DevExpress.XtraBars.Docking2010.Views.Tabbed.DocumentGroup(components);
            tabAccounts = new DevExpress.XtraBars.Docking2010.Views.Tabbed.Document(components);
            tabSummary = new DevExpress.XtraBars.Docking2010.Views.Tabbed.Document(components);
            customerHeaderView1 = new CustomerHeaderView();
            ((System.ComponentModel.ISupportInitialize)barManager1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)documentGroup1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)tabAccounts).BeginInit();
            ((System.ComponentModel.ISupportInitialize)tabSummary).BeginInit();
            SuspendLayout();
            // 
            // customerAccountsView1
            // 
            customerAccountsView1.Location = new System.Drawing.Point(0, 0);
            customerAccountsView1.Name = "customerAccountsView1";
            customerAccountsView1.Size = new System.Drawing.Size(445, 271);
            customerAccountsView1.TabIndex = 0;
            // 
            // customerDetailView1
            // 
            customerDetailView1.AutoScroll = true;
            customerDetailView1.Location = new System.Drawing.Point(0, 0);
            customerDetailView1.Name = "customerDetailView1";
            customerDetailView1.Size = new System.Drawing.Size(383, 289);
            customerDetailView1.TabIndex = 0;
            // 
            // tabbedWorkspace1
            // 
            tabbedWorkspace1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            tabbedWorkspace1.Location = new System.Drawing.Point(3, 84);
            tabbedWorkspace1.Name = "tabbedWorkspace1";
            tabbedWorkspace1.Size = new System.Drawing.Size(688, 293);
            tabbedWorkspace1.TabIndex = 7;
            // 
            // SaveButton
            // 
            SaveButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            SaveButton.Location = new System.Drawing.Point(616, 383);
            SaveButton.Name = "SaveButton";
            SaveButton.Size = new System.Drawing.Size(75, 23);
            SaveButton.TabIndex = 2;
            SaveButton.Text = "Save";
            SaveButton.Click += OnSave;
            // 
            // customerContextMenu
            // 
            customerContextMenu.Name = "customerContextMenu";
            customerContextMenu.Size = new System.Drawing.Size(61, 4);
            // 
            // barManager1
            // 
            barManager1.DockControls.Add(barDockControlTop);
            barManager1.DockControls.Add(barDockControlBottom);
            barManager1.DockControls.Add(barDockControlLeft);
            barManager1.DockControls.Add(barDockControlRight);
            barManager1.Form = this;
            // 
            // barDockControlTop
            // 
            barDockControlTop.CausesValidation = false;
            barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            barDockControlTop.Location = new System.Drawing.Point(0, 0);
            barDockControlTop.Manager = barManager1;
            barDockControlTop.Size = new System.Drawing.Size(694, 0);
            // 
            // barDockControlBottom
            // 
            barDockControlBottom.CausesValidation = false;
            barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            barDockControlBottom.Location = new System.Drawing.Point(0, 411);
            barDockControlBottom.Manager = barManager1;
            barDockControlBottom.Size = new System.Drawing.Size(694, 0);
            // 
            // barDockControlLeft
            // 
            barDockControlLeft.CausesValidation = false;
            barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            barDockControlLeft.Manager = barManager1;
            barDockControlLeft.Size = new System.Drawing.Size(0, 411);
            // 
            // barDockControlRight
            // 
            barDockControlRight.CausesValidation = false;
            barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            barDockControlRight.Location = new System.Drawing.Point(694, 0);
            barDockControlRight.Manager = barManager1;
            barDockControlRight.Size = new System.Drawing.Size(0, 411);
            // 
            // documentGroup1
            // 
            documentGroup1.Items.AddRange(new DevExpress.XtraBars.Docking2010.Views.Tabbed.Document[] { tabAccounts });
            // 
            // tabAccounts
            // 
            tabAccounts.Caption = "Account";
            tabAccounts.ControlName = "tabAccounts";
            // 
            // tabSummary
            // 
            tabSummary.Caption = "Summary";
            tabSummary.ControlName = "tabSummary";
            // 
            // customerHeaderView1
            // 
            customerHeaderView1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            customerHeaderView1.Location = new System.Drawing.Point(3, 3);
            customerHeaderView1.Name = "customerHeaderView1";
            customerHeaderView1.Size = new System.Drawing.Size(688, 85);
            customerHeaderView1.TabIndex = 8;
            // 
            // TabbedViewCustomerSummaryView
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ContextMenuStrip = customerContextMenu;
            Controls.Add(customerHeaderView1);
            Controls.Add(tabbedWorkspace1);
            Controls.Add(SaveButton);
            Controls.Add(barDockControlLeft);
            Controls.Add(barDockControlRight);
            Controls.Add(barDockControlBottom);
            Controls.Add(barDockControlTop);
            Name = "TabbedViewCustomerSummaryView";
            Size = new System.Drawing.Size(694, 411);
            Load += TabbedViewCustomerSummaryView_Load;
            ((System.ComponentModel.ISupportInitialize)barManager1).EndInit();
            ((System.ComponentModel.ISupportInitialize)documentGroup1).EndInit();
            ((System.ComponentModel.ISupportInitialize)tabAccounts).EndInit();
            ((System.ComponentModel.ISupportInitialize)tabSummary).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private CABDevExpress.Workspaces.XtraTabbedViewWorkspace tabbedWorkspace1;
        private CustomerDetailView customerDetailView1;
        private CustomerAccountsView customerAccountsView1;
        private CustomerHeaderView customerHeaderView1;
        private DevExpress.XtraEditors.SimpleButton SaveButton;
        private System.Windows.Forms.ContextMenuStrip customerContextMenu;
        private Microsoft.Practices.CompositeUI.SmartParts.SmartPartInfoProvider infoProvider;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.Docking2010.Views.Tabbed.DocumentGroup documentGroup1;
        private DevExpress.XtraBars.Docking2010.Views.Tabbed.Document tabAccounts;
        private DevExpress.XtraBars.Docking2010.Views.Tabbed.Document tabSummary;
    }
}