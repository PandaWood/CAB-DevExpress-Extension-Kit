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
//TODO:2016.10.14
using DevExpress.XtraBars.Ribbon;
//TODO:2016.10.14
namespace CABDevExpress.Workspaces
{
    /// <summary>
    /// A workspace that displays smart parts within a <see cref="XtraTabbedMdiWorkspace"/>
    /// <remarks>
    /// </remarks>
    /// </summary>
    [Description("XtraTabbedMdi Workspace")]
    public class XtraTabbedMdiWorkspace : XtraWindowWorkspace
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
            TabbedMdiManager.AllowDragDrop = DevExpress.Utils.DefaultBoolean.True;
            TabbedMdiManager.ClosePageButtonShowMode = DevExpress.XtraTab.ClosePageButtonShowMode.InActiveTabPageHeader;
            //tabbedMdiManager.Controller = this.barAndDockingController1;
            TabbedMdiManager.HeaderButtons = ((DevExpress.XtraTab.TabButtons)(((DevExpress.XtraTab.TabButtons.Next | DevExpress.XtraTab.TabButtons.Close)
                        | DevExpress.XtraTab.TabButtons.Default)));
            TabbedMdiManager.HeaderButtonsShowMode = DevExpress.XtraTab.TabButtonShowMode.Always;

            tabbedMdiManager.SelectedPageChanged += new System.EventHandler(this.xtraTabbedMdiManager_SelectedPageChanged);
            // tabbedMdiManager.PageRemoved += new DevExpress.XtraTabbedMdi.MdiTabPageEventHandler(this.xtraTabbedMdiManager_PageRemoved);
            tabbedMdiManager.PageAdded += new MdiTabPageEventHandler(tabbedMdiManager_PageAdded);
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

            mdiChild.Show();
            if (mdiMode != MdiMode.Tabbed)
                SetWindowLocation(mdiChild, smartPartInfo);

            mdiChild.BringToFront();
        }

        protected override void OnApplySmartPartInfo(Control smartPart, XtraWindowSmartPartInfo smartPartInfo)
        {
            if (mdiMode != MdiMode.Tabbed)
                base.OnApplySmartPartInfo(smartPart, smartPartInfo);
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
            //TODO:2016.10.14
            e.Page.MdiChild.Activated -= MdiChild_Activated;
            e.Page.MdiChild.Deactivate -= MdiChild_Deactivate;
            //e.Page.MdiChild.SizeChanged -= MdiChild_SizeChanged;
            e.Page.MdiChild.Activated += MdiChild_Activated;
            e.Page.MdiChild.Deactivate += MdiChild_Deactivate;
            //e.Page.MdiChild.SizeChanged += MdiChild_SizeChanged;
            //TODO:2016.10.14
        }

        private void MdiChild_Deactivate(object sender, EventArgs e)
        {
            (sender as Form).Resize -= MdiChild_Resize;
        }

        private void MdiChild_Resize(object sender, EventArgs e)
        {
            DoMergeRibbon(sender);
        }

        //private void MdiChild_SizeChanged(object sender, EventArgs e)
        //{
        //    DoMergeRibbon(sender);
        //}

        //TODO:2016.10.14
        private void MdiChild_Activated(object sender, EventArgs e)
        {
            DoMergeRibbon(sender);
            (sender as Form).Resize -= MdiChild_Resize;
            (sender as Form).Resize += MdiChild_Resize;
        }


        //TODO:2016.10.14

        //TODO:2016.10.14
        private void DoMergeRibbon(object sender)
        {
            (sender as Form).BeginInvoke(new Action(() =>
            {
                RibbonControl childRibbon = FindRibbon(sender);
                if (this.parentMdiForm is RibbonForm && childRibbon != null)
                {
                    if (childRibbon.MdiMergeStyle == RibbonMdiMergeStyle.Always
                        || (childRibbon.MdiMergeStyle == RibbonMdiMergeStyle.OnlyWhenMaximized && mdiMode == MdiMode.Tabbed)
                        || (childRibbon.MdiMergeStyle == RibbonMdiMergeStyle.OnlyWhenMaximized && mdiMode == MdiMode.Windowed && (sender as Form).WindowState == FormWindowState.Maximized))
                        (this.parentMdiForm as RibbonForm).Ribbon.MergeRibbon(childRibbon);
                }
            }));
        }
        //TODO:2016.10.14

        //TODO:2016.10.14
        static RibbonControl FindRibbon(object sender)
        {
            Control ctrlMaster = sender as Control;
            if (ctrlMaster != null && ctrlMaster.Controls != null)
            {
                foreach (Control ctrl in ctrlMaster.Controls)
                {
                    if (ctrl is RibbonControl)
                        return ctrl as RibbonControl;
                    if (ctrl.Controls != null)
                    {
                        RibbonControl ribbon = FindRibbon(ctrl);
                        if (ribbon != null)
                            return ribbon;
                    }
                }
            }
            return null;
        }
        //TODO:2016.10.14

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


    }
}