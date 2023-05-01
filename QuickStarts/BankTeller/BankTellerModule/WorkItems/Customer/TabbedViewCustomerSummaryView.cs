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
using BankTellerModule.Constants;
using DevExpress.Mvvm.POCO;
using DevExpress.XtraBars.Docking2010.Views.Tabbed;
using DevExpress.XtraBars.Docking2010.Views;
using DevExpress.XtraEditors;
using Microsoft.Practices.CompositeUI.SmartParts;
using Microsoft.Practices.ObjectBuilder;
using System.Windows.Automation;
using System.Windows.Forms;
using System.Linq;
using System.Drawing;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using System.Xml.Serialization;

namespace BankTellerModule.WorkItems.Customer
{
    [SmartPart]
    public partial class TabbedViewCustomerSummaryView : XtraUserControl
    {
        private TabbedViewCustomerSummaryController controller;

        public TabbedViewCustomerSummaryView()
        {
            InitializeComponent();
        }


        [CreateNew]
        public TabbedViewCustomerSummaryController Controller
        {
            set { controller = value; }
        }

        private void OnSave(object sender, EventArgs e)
        {
            controller.Save();
            ;
            //if (bAccountShowed)
            //    controller.WorkItem.Workspaces[CustomerWorkItem.CUSTOMERDETAIL_TABWORKSPACE].Hide(account);
            //else
            //    controller.WorkItem.Workspaces[CustomerWorkItem.CUSTOMERDETAIL_TABWORKSPACE].Show(account);
            //bAccountShowed = !bAccountShowed;

            //CABDevExpress.Workspaces.XtraTabbedViewWorkspace tabWks = controller.WorkItem.Workspaces[CustomerWorkItem.CUSTOMERDETAIL_TABWORKSPACE] as CABDevExpress.Workspaces.XtraTabbedViewWorkspace;
            ////TODO: Eliminare
            //String strLayout = null;
            //MemoryStream stream = new MemoryStream();
            //tabWks.SaveLayoutToStream(stream, false);
            //stream.Seek(0, System.IO.SeekOrigin.Begin);
            //using (StreamReader reader = new StreamReader(stream))
            //{
            //    strLayout = reader.ReadToEnd();
            //    ;
            //}
            //FileInfo fi = new FileInfo(@"C:\Temp\SamplePersist.xml");
            //if (fi.Directory != null && !fi.Directory.Exists)
            //    fi.Directory.Create();
            //System.IO.FileStream isoStream;
            //using (isoStream = new System.IO.FileStream(fi.FullName, FileMode.Create, System.IO.FileAccess.Write))
            //{
            //    try
            //    {
            //        XmlSerializer ser = new XmlSerializer(typeof(String));
            //        ser.Serialize(isoStream, strLayout);
            //    }
            //    catch (Exception ex)
            //    {
            //        ;
            //    }
            //}

            //TODO: Eliminare
        }
        CustomerDetailView detail = null;
        CustomerAccountsView account = null;
        //bool bAccountShowed = true;
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (detail == null)
            {
                //var detail = (Control)Activator.CreateInstance(typeof(CustomerDetailView));
                detail = controller.WorkItem.Items.AddNew<CustomerDetailView>();
                var smartPartInfo = new CABDevExpress.SmartPartInfos.XtraTabSmartPartInfo { Title = "Summary", PageHeaderFont = new Font("Tahoma", 9.75f) };
                tabbedWorkspace1.Show(detail, smartPartInfo);
            }
            if (tabbedWorkspace1.TabbedView.Documents.FirstOrDefault(f => String.Compare(f.Caption, "Accounts", true) == 0) == null)
            {
                //var account = (Control)Activator.CreateInstance(typeof(CustomerAccountsView));
                account = controller.WorkItem.Items.AddNew<CustomerAccountsView>();
                var smartPartInfo = new CABDevExpress.SmartPartInfos.XtraTabSmartPartInfo { Title = "Accounts", PageHeaderFont = new Font("Tahoma", 9.75f) };
                tabbedWorkspace1.Show(account, smartPartInfo);
            }
            if (!controller.WorkItem.UIExtensionSites.Contains("CustomerContext"))
                controller.WorkItem.UIExtensionSites.RegisterSite(ExtensionSiteNames.CustomerContext, customerContextMenu);
        }

        internal void FocusFirstTab()
        {
            if (tabbedWorkspace1.TabbedView.Documents.Count > 0)
                ;//tabbedWorkspace1.SelectedTabPage = tabbedWorkspace1.TabPages[0];
        }

        private void TabbedViewCustomerSummaryView_Load(object sender, EventArgs e)
        {
            ;
        }
    }
}