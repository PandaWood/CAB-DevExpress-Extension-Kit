using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Forms;
using CABDevExpress.SmartPartInfos;
using DevExpress.XtraTab;
using DevExpress.XtraTab.ViewInfo;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.SmartParts;
using Microsoft.Practices.CompositeUI.Utility;
using Microsoft.Practices.CompositeUI.WinForms;
using DevExpress.XtraTabbedMdi;
using DevExpress.XtraBars.Ribbon;

namespace CABDevExpress.Workspaces
{
    /// <summary>
    /// A workspace that displays smart parts within a <see cref="XtraTabbedMdiWorkspace"/>
    /// <remarks>
    /// </remarks>
    /// </summary>
    [Description("XtraTabbedMdi Workspace")]
    public class XtraTabbedMdiWorkspace : XtraWindowWorkspace, IDisposable
    {
        private readonly XtraTabbedMdiManager tabbedMdiManager;
        private MdiMode mdiMode = MdiMode.Tabbed;
        private readonly Form parentMdiForm;

        public enum MdiMode
        {
            Tabbed,
            Windowed
        }

        /// <summary>
        /// Initializes a new <see cref="XtraTabWorkspace"/>
        /// </summary>
        public XtraTabbedMdiWorkspace()
            : base()
        {
            tabbedMdiManager = new XtraTabbedMdiManager();
            Initialize();
        }

        /// <summary>
        /// Initializes a new <see cref="XtraTabWorkspace"/>
        /// </summary>
        public XtraTabbedMdiWorkspace(Form parentForm)
            : this(parentForm, new XtraTabbedMdiManager())
        {
        }


        /// <summary>
        /// Initializes a new <see cref="XtraTabWorkspace"/>
        /// </summary>
        public XtraTabbedMdiWorkspace(Form parentForm, XtraTabbedMdiManager pTabbedMdiManager)
            : base(parentForm)
        {
            tabbedMdiManager = pTabbedMdiManager;
            tabbedMdiManager.MdiParent = parentForm;
            parentMdiForm = parentForm;
            parentMdiForm.IsMdiContainer = true;
            Initialize();
            SetMdiMode();
            if (parentForm != null)
                parentForm.FontChanged += ParentMdiForm_FontChanged;
        }
        private void ParentMdiForm_FontChanged(object sender, EventArgs e)
        {
            if (tabbedMdiManager!=null)
                tabbedMdiManager.AppearancePage.HeaderActive.Font = new System.Drawing.Font(tabbedMdiManager.AppearancePage.HeaderActive.Font.Name, (float)Decimal.Round((Decimal)DevExpress.XtraEditors.WindowsFormsSettings.DefaultFont.Size * (Decimal)1.33), System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline);
        }

        /// <summary>
        /// Gets the parent MDI form.
        /// </summary>
        public Form ParentMdiForm
        {
            get { return parentMdiForm; }
        }


        public void Initialize()
        {
            // 
            // tabbedMdiManager
            // 
            TabbedMdiManager.AllowDragDrop = DevExpress.Utils.DefaultBoolean.False;
            TabbedMdiManager.ClosePageButtonShowMode = DevExpress.XtraTab.ClosePageButtonShowMode.InActiveTabPageHeader;
            //tabbedMdiManager.Controller = this.barAndDockingController1;
            TabbedMdiManager.HeaderButtons = ((DevExpress.XtraTab.TabButtons)(((DevExpress.XtraTab.TabButtons.Next | DevExpress.XtraTab.TabButtons.Close)
                        | DevExpress.XtraTab.TabButtons.Default)));
            TabbedMdiManager.HeaderButtonsShowMode = DevExpress.XtraTab.TabButtonShowMode.Always;

            tabbedMdiManager.SelectedPageChanged += new System.EventHandler(this.xtraTabbedMdiManager_SelectedPageChanged);
            // tabbedMdiManager.PageRemoved += new DevExpress.XtraTabbedMdi.MdiTabPageEventHandler(this.xtraTabbedMdiManager_PageRemoved);
            tabbedMdiManager.PageAdded += new MdiTabPageEventHandler(tabbedMdiManager_PageAdded);
            //2021.10.27 aggiunto per ottenere evidenza del TAB Attivo
            tabbedMdiManager.AppearancePage.HeaderActive.Font = new System.Drawing.Font(tabbedMdiManager.AppearancePage.HeaderActive.Font.Name, (float)Decimal.Round((Decimal)DevExpress.XtraEditors.WindowsFormsSettings.DefaultFont.Size * (Decimal)1.33), System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline);
        }

        protected override void OnClose(Control smartPart)
        {
            Form mdiChild = this.GetOrCreateForm(smartPart);
            if (mdiChild != null)
            {
                mdiChild.Activated -= MdiChild_Activated;
                mdiChild.MdiParent = null;
            }
            base.OnClose(smartPart);
        }

        /// <summary>
        /// Shows the form as a child of the specified <see cref="ParentMdiForm"/>.
        /// </summary>
        /// <param name="smartPart">The <see cref="Control"/> to show in the workspace.</param>
        /// <param name="smartPartInfo">The information to use to show the smart part.</param>
        protected override void OnShow(Control smartPart, XtraWindowSmartPartInfo smartPartInfo)
        {
            Form mdiChild = this.GetOrCreateForm(smartPart);
            SetWindowProperties(mdiChild, smartPartInfo);
            mdiChild.MdiParent = parentMdiForm;
            mdiChild.Activated -= MdiChild_Activated;
            mdiChild.Activated += MdiChild_Activated;
            //mdiChild.Deactivate -= MdiChild_Deactivate;
            //mdiChild.Deactivate += MdiChild_Deactivate;
            mdiChild.Show();
            if (mdiMode != MdiMode.Tabbed)
                SetWindowLocation(mdiChild, smartPartInfo);

            mdiChild.BringToFront();
        }

        protected override void OnApplySmartPartInfo(Control smartPart, XtraWindowSmartPartInfo smartPartInfo)
        {
            if (mdiMode != MdiMode.Tabbed)
                base.OnApplySmartPartInfo(smartPart, smartPartInfo);
            //if (smartPart.Parent != null)
            //    RibonMergerManagerHelper.DoMergeRibbon(smartPart.Parent, this.parentMdiForm,
            //        (x) => x.MdiMergeStyle == RibbonMdiMergeStyle.Always
            //            || (x.MdiMergeStyle == RibbonMdiMergeStyle.OnlyWhenMaximized && mdiMode == MdiMode.Tabbed)
            //            || (x.MdiMergeStyle == RibbonMdiMergeStyle.OnlyWhenMaximized && mdiMode == MdiMode.Windowed && (smartPart.Parent as Form)?.WindowState == FormWindowState.Maximized)
            //        );
        }

        /// <summary>
        /// 
        /// </summary>
        public XtraTabbedMdiManager TabbedMdiManager
        {
            get { return tabbedMdiManager; }
        }

        private void xtraTabbedMdiManager_SelectedPageChanged(object sender, EventArgs e)
        {
            XtraMdiTabPage page = tabbedMdiManager.SelectedPage;
            if (page != null)
            {
                page.Image = page.MdiChild.Icon.ToBitmap();
            }
        }

        void tabbedMdiManager_PageAdded(object sender, MdiTabPageEventArgs e)
        {
            e.Page.Image = e.Page.MdiChild.Icon.ToBitmap();
        }

        //private void MdiChild_Deactivate(object sender, EventArgs e)
        //{
        //    (sender as Form).Resize -= MdiChild_Resize;
        //}

        //private void MdiChild_Resize(object sender, EventArgs e)
        //{
        //    DoMergeRibbon(sender);
        //}

        private void MdiChild_Activated(object sender, EventArgs e)
        {
            //(sender as Form).Resize -= MdiChild_Resize;
            //(sender as Form).Resize += MdiChild_Resize;
            //DoMergeRibbon(sender);
            //RibonMergerManagerHelper.DoMergeRibbon(sender, this.parentMdiForm,
            //    (x) => x.MdiMergeStyle == RibbonMdiMergeStyle.Always
            //        || (x.MdiMergeStyle == RibbonMdiMergeStyle.OnlyWhenMaximized && mdiMode == MdiMode.Tabbed)
            //        || (x.MdiMergeStyle == RibbonMdiMergeStyle.OnlyWhenMaximized && mdiMode == MdiMode.Windowed && (sender as Form)?.WindowState == FormWindowState.Maximized)
            //    );
        }

        //private void DoMergeRibbon(object sender)
        //{
        //    (sender as Form).BeginInvoke(new Action(() =>
        //    {
        //        if (this.parentMdiForm is RibbonForm)
        //        {
        //            (this.parentMdiForm as RibbonForm).Ribbon.UnMergeRibbon();
        //            RibbonControl childRibbon = FindRibbon(sender);
        //            if (this.parentMdiForm is RibbonForm && childRibbon != null)
        //            {
        //                if (childRibbon.MdiMergeStyle == RibbonMdiMergeStyle.Always
        //                    || (childRibbon.MdiMergeStyle == RibbonMdiMergeStyle.OnlyWhenMaximized && mdiMode == MdiMode.Tabbed)
        //                    || (childRibbon.MdiMergeStyle == RibbonMdiMergeStyle.OnlyWhenMaximized && mdiMode == MdiMode.Windowed && (sender as Form).WindowState == FormWindowState.Maximized))
        //                    (this.parentMdiForm as RibbonForm).Ribbon.MergeRibbon(childRibbon);
        //            }
        //        }
        //    }));
        //}

        //static RibbonControl FindRibbon(object sender)
        //{
        //    Control ctrlMaster = sender as Control;
        //    if (ctrlMaster != null && ctrlMaster.Controls != null)
        //    {
        //        foreach (Control ctrl in ctrlMaster.Controls)
        //        {
        //            if (ctrl is RibbonControl)
        //                return ctrl as RibbonControl;
        //            if (ctrl.Controls != null)
        //            {
        //                RibbonControl ribbon = FindRibbon(ctrl);
        //                if (ribbon != null)
        //                    return ribbon;
        //            }
        //        }
        //    }
        //    return null;
        //}

        /// <summary>
        /// 
        /// </summary>
        public void MdiChangeMode()
        {
            mdiMode = (mdiMode == MdiMode.Tabbed) ? MdiMode.Windowed : MdiMode.Tabbed; // Toggle
            SetMdiMode(mdiMode);
        }

        private void SetMdiMode()
        {
            SetMdiMode(mdiMode);
        }

        private void SetMdiMode(MdiMode mode)
        {
            TabbedMdiManager.MdiParent = (mode == MdiMode.Tabbed) ? parentMdiForm : null;
        }

        /// <summary>
        /// Set's Layout of Mdi Mode
        /// </summary>
        /// <param name="layout">one of the values of <see cref="MdiLayout"/> enum</param>
        public void LayoutMdi(MdiLayout layout)
        {
            SetMdiMode(MdiMode.Windowed);
            TabbedMdiManager.MdiParent = null;
            parentMdiForm.LayoutMdi(layout);
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    tabbedMdiManager.SelectedPageChanged -= this.xtraTabbedMdiManager_SelectedPageChanged;
                    tabbedMdiManager.PageAdded -= tabbedMdiManager_PageAdded;
                    if (parentMdiForm != null)
                        parentMdiForm.FontChanged -= ParentMdiForm_FontChanged;
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
            base.Dispose();
        }


        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~XtraTabbedMdiWorkspace() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}