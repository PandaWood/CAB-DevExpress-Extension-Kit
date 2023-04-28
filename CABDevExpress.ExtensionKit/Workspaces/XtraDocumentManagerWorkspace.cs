using DevExpress.XtraBars.Docking2010.Views.NativeMdi;
using DevExpress.XtraBars.Docking2010.Views;
using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraEditors;
using DevExpress.XtraTab;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CABDevExpress.SmartPartInfos;

namespace CABDevExpress.Workspaces
{
    /// <summary>
    /// A workspace that displays smart parts within a <see cref="XtraDocumentManagerWorkspace"/>
    /// <remarks>
    /// </remarks>
    /// </summary>
    [Description("XtraDocumentManager Workspace")]
    public class XtraDocumentManagerWorkspace : XtraWindowWorkspace, IDisposable
    {
        private readonly DevExpress.XtraBars.Docking2010.DocumentManager _documentManager;
        private DevExpress.XtraBars.Docking2010.Views.BaseView _baseView;
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
        public XtraDocumentManagerWorkspace()
            : base()
        {
            _documentManager = new DocumentManager();
            Initialize(mdiMode);
        }

        /// <summary>
        /// Initializes a new <see cref="XtraTabWorkspace"/>
        /// </summary>
        public XtraDocumentManagerWorkspace(Form parentForm)
            : this(parentForm, new DocumentManager(GetFormIContainer(parentForm)))
        {
        }

        private static System.ComponentModel.IContainer GetFormIContainer(Form parentForm)
        {
            System.ComponentModel.IContainer cont = null;
            if (parentForm != null)
            {
                FieldInfo[] fields = parentForm.GetType().GetFields(
                             BindingFlags.NonPublic |
                             BindingFlags.Instance);
                FieldInfo fi = fields?.FirstOrDefault(w => w.FieldType.Name == typeof(System.ComponentModel.IContainer).Name);
                cont = fi.GetValue(parentForm) as System.ComponentModel.IContainer;
            }
            return cont;
        }

        /// <summary>
        /// Initializes a new <see cref="XtraTabWorkspace"/>
        /// </summary>
        public XtraDocumentManagerWorkspace(Form parentForm, DocumentManager pTabbedMdiManager)
            : base(parentForm)
        {
            _documentManager = pTabbedMdiManager;
            parentMdiForm = parentForm;
            _documentManager.MdiParent = parentMdiForm;
            _documentManager.ContainerControl = parentMdiForm;
            Initialize(mdiMode);
        }

        /// <summary>
        /// Gets the parent MDI form.
        /// </summary>
        public Form ParentMdiForm
        {
            get { return parentMdiForm; }
        }


        void DocumentManagerDocumentAdded(object sender, DevExpress.XtraBars.Docking2010.Views.DocumentEventArgs e)
        {
            //e.Document.ImageOptions.Image = parentMdiForm.Icon.ToBitmap();
            //e.Page.Image = e.Page.MdiChild.Icon.ToBitmap();
        }


        private void DocumentManagerDocumentActivated(object sender, DevExpress.XtraBars.Docking2010.Views.DocumentEventArgs e)
        {
            //e.Document.ImageOptions.Image = parentMdiForm.Icon.ToBitmap();
        }

        protected override void OnClose(System.Windows.Forms.Control smartPart)
        {
            Form mdiChild = this.GetOrCreateForm(smartPart);
            if (mdiChild != null)
            {
                //mdiChild.Activated -= MdiChild_Activated;
                mdiChild.MdiParent = null;
            }
            base.OnClose(smartPart);
        }

        /// <summary>
        /// Shows the form as a child of the specified <see cref="ParentMdiForm"/>.
        /// </summary>
        /// <param name="smartPart">The <see cref="Control"/> to show in the workspace.</param>
        /// <param name="smartPartInfo">The information to use to show the smart part.</param>
        protected override void OnShow(System.Windows.Forms.Control smartPart, XtraWindowSmartPartInfo smartPartInfo)
        {
            Form mdiChild = this.GetOrCreateForm(smartPart);
            SetWindowProperties(mdiChild, smartPartInfo);
            //mdiChild.MdiParent = parentMdiForm;
            //mdiChild.Activated -= MdiChild_Activated;
            //mdiChild.Activated += MdiChild_Activated;
            //mdiChild.Deactivate -= MdiChild_Deactivate;
            //mdiChild.Deactivate += MdiChild_Deactivate;

            _documentManager.MdiParent = parentMdiForm;
            _baseView.BeginUpdate();
            BaseDocument document = _baseView.AddDocument(mdiChild);
            document.ImageOptions.Image = mdiChild.Icon.ToBitmap();
            _baseView.EndUpdate();
            _baseView.Controller.Activate(document);
            //mdiChild.BringToFront();
            //mdiChild.Show();
            DoMergeRibbon(mdiChild);
            if (mdiMode != MdiMode.Tabbed)
                SetWindowLocation(mdiChild, smartPartInfo);
        }

        protected override void OnApplySmartPartInfo(System.Windows.Forms.Control smartPart, XtraWindowSmartPartInfo smartPartInfo)
        {
            if (mdiMode != MdiMode.Tabbed)
                base.OnApplySmartPartInfo(smartPart, smartPartInfo);
            if (smartPart.Parent != null)
                DoMergeRibbon(smartPart.Parent);
        }

        /// <summary>
        /// 
        /// </summary>
        public DocumentManager DocumentManager
        {
            get { return _documentManager; }
        }

        //private void MdiChild_Deactivate(object sender, EventArgs e)
        //{
        //    (sender as Form).Resize -= MdiChild_Resize;
        //}

        //private void MdiChild_Resize(object sender, EventArgs e)
        //{
        //    DoMergeRibbon(sender);
        //}

        private void DoMergeRibbon(object sender)
        {
            if (sender != null)
                (sender as Control)?.BeginInvoke(new Action(() =>
                {
                    if (this.parentMdiForm is RibbonForm)
                    {
                        (this.parentMdiForm as RibbonForm).Ribbon.UnMergeRibbon();
                        RibbonControl childRibbon = FindRibbon(sender);
                        if (this.parentMdiForm is RibbonForm && childRibbon != null)
                        {
                            if (childRibbon.MdiMergeStyle == RibbonMdiMergeStyle.Always
                                || (childRibbon.MdiMergeStyle == RibbonMdiMergeStyle.OnlyWhenMaximized && mdiMode == MdiMode.Tabbed)
                                || (childRibbon.MdiMergeStyle == RibbonMdiMergeStyle.OnlyWhenMaximized && mdiMode == MdiMode.Windowed && (sender as Form).WindowState == FormWindowState.Maximized))
                                (this.parentMdiForm as RibbonForm).Ribbon.MergeRibbon(childRibbon);
                        }
                    }
                }));
        }

        static RibbonControl FindRibbon(object sender)
        {
            System.Windows.Forms.Control ctrlMaster = sender as System.Windows.Forms.Control;
            if (ctrlMaster != null && ctrlMaster.Controls != null)
            {
                foreach (System.Windows.Forms.Control ctrl in ctrlMaster.Controls)
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

        /// <summary>
        /// 
        /// </summary>
        public void MdiChangeMode()
        {
            mdiMode = (mdiMode == MdiMode.Tabbed) ? MdiMode.Windowed : MdiMode.Tabbed; // Toggle
            Initialize(mdiMode);
        }

        private void Initialize(MdiMode mode)
        {
            //DocumentManager.MdiParent = (mode == MdiMode.Tabbed) ? parentMdiForm : null;
            _documentManager.BeginUpdate();
            try
            {
                if (_baseView != null)
                {
                    _baseView.DocumentAdded -= DocumentManagerDocumentAdded;
                    _baseView.DocumentActivated -= DocumentManagerDocumentActivated;
                }
                if (mdiMode == MdiMode.Windowed)
                {
                    _baseView = new NativeMdiView();
                    _documentManager.View = _baseView;
                    _documentManager.ViewCollection.AddRange(new DevExpress.XtraBars.Docking2010.Views.BaseView[] { _baseView });
                    _documentManager.BeginUpdate();
                }
                else
                {
                    _baseView = new DevExpress.XtraBars.Docking2010.Views.Tabbed.TabbedView(GetFormIContainer(parentMdiForm));
                    _documentManager.View = _baseView;
                    _documentManager.ViewCollection.AddRange(new DevExpress.XtraBars.Docking2010.Views.BaseView[] { _baseView });
                    ((DevExpress.XtraBars.Docking2010.Views.Tabbed.TabbedView)_baseView).DocumentProperties.AllowPin = false;
                    ((DevExpress.XtraBars.Docking2010.Views.Tabbed.TabbedView)_baseView).DocumentProperties.ShowPinButton = false;
                    ((DevExpress.XtraBars.Docking2010.Views.Tabbed.TabbedView)_baseView).DocumentGroupProperties.ClosePageButtonShowMode = DevExpress.XtraTab.ClosePageButtonShowMode.InActiveTabPageHeader;
                    ((DevExpress.XtraBars.Docking2010.Views.Tabbed.TabbedView)_baseView).DocumentGroupProperties.HeaderButtons = ((DevExpress.XtraTab.TabButtons)(((DevExpress.XtraTab.TabButtons.Next | DevExpress.XtraTab.TabButtons.Close)
                                | DevExpress.XtraTab.TabButtons.Default)));
                    ((DevExpress.XtraBars.Docking2010.Views.Tabbed.TabbedView)_baseView).DocumentGroupProperties.HeaderButtonsShowMode = TabButtonShowMode.Always;
                    ((DevExpress.XtraBars.Docking2010.Views.Tabbed.TabbedView)_baseView).AppearancePage.HeaderActive.Font = new System.Drawing.Font(((DevExpress.XtraBars.Docking2010.Views.Tabbed.TabbedView)_baseView).AppearancePage.HeaderActive.Font.Name, (float)Decimal.Round((Decimal)((DevExpress.XtraBars.Docking2010.Views.Tabbed.TabbedView)_baseView).AppearancePage.HeaderActive.Font.Size * (Decimal)1.33), System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline);
                    foreach (DevExpress.XtraBars.Docking2010.Views.BaseDocument view in _baseView.Documents)
                        view.ImageOptions.Image = view.Form?.Icon.ToBitmap();
                }
                _documentManager.RibbonAndBarsMergeStyle = DevExpress.XtraBars.Docking2010.Views.RibbonAndBarsMergeStyle.Always;
                _documentManager.ShowToolTips = DevExpress.Utils.DefaultBoolean.True;
                _baseView.DocumentProperties.AllowClose = true;
                _baseView.DocumentSelectorProperties.AllowHtmlDraw = DevExpress.Utils.DefaultBoolean.True;
                _baseView.FloatingDocumentContainer = DevExpress.XtraBars.Docking2010.Views.FloatingDocumentContainer.DocumentsHost;
                _baseView.DocumentAdded += DocumentManagerDocumentAdded;
                _baseView.DocumentActivated += DocumentManagerDocumentActivated;
            }
            finally 
            {
                _documentManager.EndUpdate();
            }
        }

        /// <summary>
        /// Set's Layout of Mdi Mode
        /// </summary>
        /// <param name="layout">one of the values of <see cref="MdiLayout"/> enum</param>
        public void LayoutMdi(MdiLayout layout)
        {
            if (mdiMode == MdiMode.Windowed)
            {
                Initialize(mdiMode);
                parentMdiForm.LayoutMdi(layout);
            }
        }

        protected override void BeforeEntryRemove(XtraForm frm)
        {
            base.BeforeEntryRemove(frm);
            var doc = _baseView.Documents.FirstOrDefault(w => Object.Equals(w.Control, frm) == true);
            if (doc != null)
            {
                _baseView.BeginUpdate();
                _baseView.Documents.Remove(doc);
                _baseView.EndUpdate();
            }

        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing && _baseView != null)
                {
                    _baseView.DocumentAdded -= DocumentManagerDocumentAdded;
                    _baseView.DocumentActivated -= DocumentManagerDocumentActivated;
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
            base.Dispose();
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~XtraDocumentManagerWorkspace() {
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