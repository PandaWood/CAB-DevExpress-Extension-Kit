using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Forms;
//using System.Windows.Forms;
using CABDevExpress.SmartPartInfos;
using DevExpress.CodeParser;
using DevExpress.Utils;
using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraBars.Docking2010.Views;
using DevExpress.XtraBars.Docking2010.Views.Tabbed;
using DevExpress.XtraEditors;
using DevExpress.XtraTab;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.SmartParts;
using Microsoft.Practices.CompositeUI.WinForms;

namespace CABDevExpress.Workspaces
{
    /// <summary>
    /// A workspace that displays smart parts within a <see cref="DevExpress.XtraBars.Docking2010.DocumentManager"/>
    /// <remarks>
	/// The code here was copied and adapted from CAB's plain-vanilla <see cref="TabWorkspace"/>
	/// It couldn't be inherited because some property names and features of the XtraTabbedView are different
    /// </remarks>
    /// </summary>
    [Description("XtraTabbedView Workspace")]
    public class XtraTabbedViewWorkspace : XtraUserControl, IComposableWorkspace<System.Windows.Forms.Control, XtraTabSmartPartInfo>
    {
        public DevExpress.XtraBars.Docking2010.Views.Tabbed.TabbedView TabbedView { get; private set; }
        private readonly Dictionary<System.Windows.Forms.Control, DevExpress.XtraBars.Docking2010.Views.BaseDocument> pages = new Dictionary<System.Windows.Forms.Control, DevExpress.XtraBars.Docking2010.Views.BaseDocument>();
        private readonly WorkspaceComposer<System.Windows.Forms.Control, XtraTabSmartPartInfo> composer;
        private bool callComposerActivateOnIndexChange = true;
        private bool populatingPages;
        private bool _bIsTabClosable = true;
        private System.ComponentModel.IContainer components;
        private DevExpress.XtraBars.Docking2010.DocumentManager documentManager;
        /// <summary>
        /// Initializes a new <see cref="XtraTabbedViewWorkspace"/>
        /// </summary>
        public XtraTabbedViewWorkspace()
            : base()
        {
            this.components = new System.ComponentModel.Container();
            documentManager = new DevExpress.XtraBars.Docking2010.DocumentManager(this.components);
            TabbedView = new DevExpress.XtraBars.Docking2010.Views.Tabbed.TabbedView();
            composer = new WorkspaceComposer<System.Windows.Forms.Control, XtraTabSmartPartInfo>(this);
            //TabbedView.DocumentProperties.AllowPin = true;
            //TabbedView.DocumentProperties.ShowPinButton = true;
            //TabbedView.DocumentGroupProperties.HeaderButtons = ((DevExpress.XtraTab.TabButtons)(((DevExpress.XtraTab.TabButtons.Next | DevExpress.XtraTab.TabButtons.Close)
            //            | DevExpress.XtraTab.TabButtons.Default)));
            _bIsTabClosable = true;
            XtraTabbedViewWorkspace.SetTabClosable(TabbedView, _bIsTabClosable);
            TabbedView.AppearancePage.HeaderActive.Font = new System.Drawing.Font(TabbedView.AppearancePage.HeaderActive.Font.Name, (float)Decimal.Round((Decimal)TabbedView.AppearancePage.HeaderActive.Font.Size * (Decimal)1.21), System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline);
            TabbedView.DocumentSelectorProperties.AllowHtmlDraw = DevExpress.Utils.DefaultBoolean.True;
            TabbedView.FloatingDocumentContainer = DevExpress.XtraBars.Docking2010.Views.FloatingDocumentContainer.DocumentsHost;
            TabbedView.DocumentActivated -= DocumentActivated;
            TabbedView.DocumentActivated += DocumentActivated;
            TabbedView.PopupMenuShowing -= TabbedView_PopupMenuShowing;
            TabbedView.PopupMenuShowing += TabbedView_PopupMenuShowing;
            TabbedView.CustomDocumentsHostWindow -= TabbedView_CustomDocumentsHostWindow;
            TabbedView.CustomDocumentsHostWindow += TabbedView_CustomDocumentsHostWindow;
            TabbedView.QueryControl -= TabbedView_QueryControl;
            TabbedView.QueryControl += TabbedView_QueryControl;
            documentManager.ContainerControl = this;
            documentManager.View = TabbedView;
            documentManager.ViewCollection.AddRange(new DevExpress.XtraBars.Docking2010.Views.BaseView[] { TabbedView });
        }

        private static void SetTabClosable(DevExpress.XtraBars.Docking2010.Views.Tabbed.TabbedView TabbedView, bool bClosable)
        {
            TabbedView.DocumentGroupProperties.ClosePageButtonShowMode = DevExpress.XtraTab.ClosePageButtonShowMode.InActiveTabPageHeader;
            TabbedView.DocumentGroupProperties.HeaderButtons = (DevExpress.XtraTab.TabButtons)(DevExpress.XtraTab.TabButtons.Prev | DevExpress.XtraTab.TabButtons.Next);
            TabbedView.DocumentGroupProperties.HeaderButtonsShowMode = TabButtonShowMode.Never;
            if (bClosable)
            {
                TabbedView.DocumentGroupProperties.HeaderButtons = (DevExpress.XtraTab.TabButtons)(TabbedView.DocumentGroupProperties.HeaderButtons | DevExpress.XtraTab.TabButtons.Close);
                TabbedView.DocumentGroupProperties.HeaderButtonsShowMode = TabButtonShowMode.WhenNeeded;
            }
            TabbedView.DocumentProperties.AllowClose = bClosable;
        }

        void TabbedView_CustomDocumentsHostWindow(object sender, DevExpress.XtraBars.Docking2010.CustomDocumentsHostWindowEventArgs e)
        {
            e.Constructor = new DevExpress.XtraBars.Docking2010.DocumentsHostWindowConstructor(CreateCustomHost);
        }
        private CustomDocumentsHost CreateCustomHost()
        {
            return new CustomDocumentsHost();
        }

        void TabbedView_QueryControl(object sender, DevExpress.XtraBars.Docking2010.Views.QueryControlEventArgs e)
        {
            e.Control = new System.Windows.Forms.Control();
        }

        private void TabbedView_PopupMenuShowing(object sender, DevExpress.XtraBars.Docking2010.Views.PopupMenuShowingEventArgs e)
        {
            
            e.Menu.Remove(BaseViewControllerCommand.CloseAll);
            if (!_bIsTabClosable)
            {
                e.Menu.Remove(BaseViewControllerCommand.CloseAllButThis);
                e.Menu.Remove(BaseViewControllerCommand.Close);
            }
        }

        /// <summary>
        /// Fires the <see cref="SmartPartActivated"/> event whenever 
        /// the selected tab index changes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">An <see cref="EventArgs"/> that contains the event data.</param>
        private void DocumentActivated(object sender, DevExpress.XtraBars.Docking2010.Views.DocumentEventArgs e)
        {
            if (callComposerActivateOnIndexChange && TabbedView.Documents.Count != 0)
            {
                //sulla prima pagina del controllo disabilito la possibilità di chiusura del primo TAB aperto
                TabbedView.ActiveDocument.BeginUpdate();

                DefaultBoolean AllowClose = IsTabClosable ? DevExpress.Utils.DefaultBoolean.True : DevExpress.Utils.DefaultBoolean.False;
                if (TabbedView.Documents.Count == 1)
                    AllowClose = DefaultBoolean.False;
                TabbedView.ActiveDocument.Properties.AllowClose = AllowClose;
                // Locate the smart part corresponding to the page.
                TabbedView.ActiveDocument.EndUpdate();
                foreach (KeyValuePair<System.Windows.Forms.Control, DevExpress.XtraBars.Docking2010.Views.BaseDocument> pair in pages)
                {
                    if (pair.Value == TabbedView.ActiveDocument)
                    {
                        Activate(pair.Key);
                        return;
                    }
                }

                // If we got here, we couldn't find a corresponding smart part for the 
                // currently active tab, hence we reset the ActiveSmartPart value.
                composer.SetActiveSmartPart(null);
            }
        }

        //TODO:INIZIO - Eliminare non viene mai invocato e non veniva invocato nemmeno l'override OnCloseButtonClick(object sender, EventArgs e) su XtraTabWorkspace
        //protected void DocumentClosed(object sender, EventArgs e)
        //{
        //    ClosePageButtonEventArgs arg = e as ClosePageButtonEventArgs;
        //    if (pages.ContainsValue((DevExpress.XtraBars.Docking2010.Views.BaseDocument)arg.Page) == true)
        //    {
        //        Control control = GetControlFromPage((DevExpress.XtraBars.Docking2010.Views.BaseDocument)arg.Page);
        //        if (control != null && composer.SmartParts.Contains(control) == true)
        //            Close(control);
        //    }
        //}
        //TODO:FINE

        /// <summary>
        /// Dependency injection setter property to get the <see cref="WorkItem"/> where object is contained.
        /// </summary>
        [ServiceDependency]
        public WorkItem WorkItem
        {
            set { 
                composer.WorkItem = value;
                if (this.Parent != null)
                {
                    if (TabbedView.Manager != null)
                    {
                        TabbedView.Manager.RibbonAndBarsMergeStyle = DevExpress.XtraBars.Docking2010.Views.RibbonAndBarsMergeStyle.Always;
                        TabbedView.Manager.ShowToolTips = DevExpress.Utils.DefaultBoolean.True;
                    }
                }
            }
        }

        /// <summary>
        /// Gets the collection of pages that the tab workspace uses.
        /// </summary>
        public Microsoft.Practices.CompositeUI.Utility.ReadOnlyDictionary<System.Windows.Forms.Control, DevExpress.XtraBars.Docking2010.Views.BaseDocument> Pages
        {
            get { return new Microsoft.Practices.CompositeUI.Utility.ReadOnlyDictionary<System.Windows.Forms.Control, DevExpress.XtraBars.Docking2010.Views.BaseDocument>(pages); }
        }

        private void SetTabProperties(DevExpress.XtraBars.Docking2010.Views.BaseDocument page, XtraTabSmartPartInfo smartPartInfo)
        {
            page.Caption = String.IsNullOrEmpty(smartPartInfo.Title) ? page.Caption : smartPartInfo.Title;

            try
            {
                DevExpress.XtraBars.Docking2010.Views.BaseDocument currentSelection = TabbedView.ActiveDocument;
                callComposerActivateOnIndexChange = false;
                if (smartPartInfo.Position == TabPosition.Beginning)
                {
                    DevExpress.XtraBars.Docking2010.Views.BaseDocument[] tabPages = GetTabPages();
                    TabbedView.Documents.Clear();

                    TabbedView.Documents.Add(page);
                    TabbedView.Documents.AddRange(tabPages);
                }
                else if (TabbedView.Documents.Contains(page) == false)
                {
                    TabbedView.Documents.Add(page);
                }
                TabbedView.BeginUpdate();
                page.Control.BackColor = smartPartInfo.BackColor;
                page.Control.ForeColor = smartPartInfo.ForeColor;
                if (smartPartInfo.Image != null)
                    page.ImageOptions.Image = smartPartInfo.Image;
                //page.ImageIndex = smartPartInfo.ImageIndex;
                page.Control.Enabled = smartPartInfo.PageEnabled;
                page.Control.Visible = smartPartInfo.PageVisible;
                page.Properties.AllowClose = smartPartInfo.ShowCloseButton == DevExpress.Utils.DefaultBoolean.True && _bIsTabClosable ? DevExpress.Utils.DefaultBoolean.True : DevExpress.Utils.DefaultBoolean.False;
                TabbedView.EndUpdate();
                if (smartPartInfo.Text != null || smartPartInfo.Text != null)	// don't apply if not set
                    page.Caption = smartPartInfo.Text ?? smartPartInfo.Title;
                ((DevExpress.XtraBars.Docking2010.Views.Tabbed.Document)page).Tooltip = smartPartInfo.Tooltip;
                //TODO: Aggiunto il tool tip che non è nativo
                //page.Tooltip = smartPartInfo.Tooltip;
                //if (smartPartInfo.PageHeaderFont != null)
                //{
                //    page.Appearance.Header.Font = smartPartInfo.PageHeaderFont;
                //    page.Appearance.Header.Options.UseFont = true;
                //}
                //TODO: Fine
                if (currentSelection?.Control!=null)
                    TabbedView.ActivateDocument(currentSelection.Control);
            }
            finally
            {
                callComposerActivateOnIndexChange = true;
            }
        }

        private DevExpress.XtraBars.Docking2010.Views.BaseDocument[] GetTabPages()
        {
            DevExpress.XtraBars.Docking2010.Views.BaseDocument[] tabPages = new DevExpress.XtraBars.Docking2010.Views.BaseDocument[TabbedView.Documents.Count];
            for (int i = 0; i < tabPages.Length; i++)
            {
                tabPages[i] = TabbedView.Documents[i];
            }

            return tabPages;
        }

        private DevExpress.XtraBars.Docking2010.Views.BaseDocument GetOrCreateTabPage(System.Windows.Forms.Control smartPart)
        {
            DevExpress.XtraBars.Docking2010.Views.BaseDocument page = null;
            if (pages.ContainsKey(smartPart))
                page = pages[smartPart];

            // If the tab was added with the control at design-time, it will have a parent control, 
            // and somewhere up its containment chain we'll find one of our tabs
            if (page == null)
            {
                //XtraForm frm = ((System.Windows.Forms.ContainerControl)smartPart)?.ParentForm as DevExpress.XtraEditors.XtraForm;
                //if (frm!=null)
                //    frm.CloseBox = false;
                page = TabbedView.AddDocument(smartPart);
                ((DevExpress.XtraBars.Docking2010.Views.Tabbed.Document)page).Properties.AllowClose = IsTabClosable ? DevExpress.Utils.DefaultBoolean.True : DevExpress.Utils.DefaultBoolean.False;
                smartPart.Dock = System.Windows.Forms.DockStyle.Fill;
                page.BeginUpdate();
                page.AccessibleName = Guid.NewGuid().ToString();
                page.EndUpdate();
                pages.Add(smartPart, page);
            }
            else if (pages.ContainsKey(smartPart) == false)
            {
                pages.Add(smartPart, page);
            }

            return page;
        }

        private void PopulatePages()
        {
            // If the page count matches, don't bother repopulating the pages collection
            if (!populatingPages && pages.Count != TabbedView.Documents.Count)
            {
                foreach (DevExpress.XtraBars.Docking2010.Views.BaseDocument page in TabbedView.Documents)
                {
                    if (pages.ContainsValue(page) == false)
                    {
                        System.Windows.Forms.Control control = GetControlFromPage(page);
                        if (control != null && composer.SmartParts.Contains(control) == false)
                        {
                            XtraTabSmartPartInfo tabinfo = new XtraTabSmartPartInfo();
                            tabinfo.ActivateTab = false;
                            populatingPages = true;		// Avoid circular calls to this method.
                            try
                            {
                                Show(control, tabinfo);
                            }
                            finally
                            {
                                populatingPages = false;
                            }
                        }
                    }
                }
            }
        }

        private void ControlDisposed(object sender, EventArgs e)
        {
            System.Windows.Forms.Control control = sender as System.Windows.Forms.Control;
            if (control != null && pages.ContainsKey(control))
            {
                composer.ForceClose(control);
            }
        }

        private static System.Windows.Forms.Control GetControlFromPage(DevExpress.XtraBars.Docking2010.Views.BaseDocument page)
        {
            System.Windows.Forms.Control control = null;
            if (page.Control != null)
                control = page.Control;
            return control;
        }

        /// <summary>
        /// Hooks up tab pages added at design-time.
        /// </summary>
        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            PopulatePages();
        }
        private void ActivateSiblingTab(int iPageIndex)
        {
            if (iPageIndex > 0)
            {
                iPageIndex--;
                System.Windows.Forms.Control ctrl = GetControlFromPage(TabbedView.Documents[iPageIndex]);
                TabbedView.ActivateDocument(ctrl);
                composer.SetActiveSmartPart(ctrl);
            }
            else if (iPageIndex < TabbedView.Documents.Count - 1)
            {
                iPageIndex++;
                System.Windows.Forms.Control ctrl = GetControlFromPage(TabbedView.Documents[iPageIndex]);
                TabbedView.ActivateDocument(ctrl);
                composer.SetActiveSmartPart(ctrl);
            }
            else
            {
                composer.SetActiveSmartPart(null);
            }
        }

        private void ResetSelectedIndexIfNoTabs()
        {
            // First control to come in is special. We need to 
            // set the selected index to a non-zero index so we 
            // get the appropriate behavior for activation.
            if (TabbedView.Documents.Count == 0)
            {
                try
                {
                    callComposerActivateOnIndexChange = false;
                }
                finally
                {
                    callComposerActivateOnIndexChange = true;
                }
            }
        }

        private DevExpress.XtraBars.Docking2010.Views.BaseDocument GetTabPageFromName(string name)
        {
            foreach (DevExpress.XtraBars.Docking2010.Views.BaseDocument tabPage in TabbedView.Documents)
            {
                if (tabPage.AccessibleName == name)
                    return tabPage;
            }
            return null;
        }

        /// <summary>
        /// Activates the smart part within the workspace.
        /// </summary>
        /// <param name="smartPart">The smart part to activate</param>
        protected virtual void OnActivate(System.Windows.Forms.Control smartPart)
        {
            PopulatePages();

            string key = pages[smartPart].AccessibleName;

            try
            {
                callComposerActivateOnIndexChange = false;
                DevExpress.XtraBars.Docking2010.Views.BaseDocument page = GetTabPageFromName(key);
                TabbedView.BeginUpdate();
                page.Control.Visible = true;
                DefaultBoolean AllowClose = IsTabClosable ? DevExpress.Utils.DefaultBoolean.True : DevExpress.Utils.DefaultBoolean.False;
                if (TabbedView.Documents.Count == 1)
                    AllowClose = DefaultBoolean.False;
                ((DevExpress.XtraBars.Docking2010.Views.Tabbed.Document)page).Properties.AllowClose = AllowClose;
                TabbedView.EndUpdate();
            }
            finally
            {
                callComposerActivateOnIndexChange = true;
            }
        }

        /// <summary>
        /// Applies the smart part info to the smart part within the workspace.
        /// </summary>
        /// <param name="smartPart">The smart part to which the smart part info should be applied.</param>
        /// <param name="smartPartInfo">The smart part info to apply</param>
        protected virtual void OnApplySmartPartInfo(System.Windows.Forms.Control smartPart, XtraTabSmartPartInfo smartPartInfo)
        {
            PopulatePages();
            string key = pages[smartPart].AccessibleName;
            SetTabProperties(GetTabPageFromName(key), smartPartInfo);
            if (smartPartInfo.ActivateTab)
            {
                Activate(smartPart);
            }
        }

        /// <summary>
        /// Closes/removes the smart part.
        /// </summary>
        protected virtual void OnClose(System.Windows.Forms.Control smartPart)
        {
            PopulatePages();
            if (smartPart != null)
            {
                smartPart.Disposed -= ControlDisposed;
                if (pages.ContainsKey(smartPart))
                {
                    if (TabbedView.Documents.Contains(pages[smartPart]) == true)
                        TabbedView.Documents.Remove(pages[smartPart]);
                    pages.Remove(smartPart);
                }
                if (!smartPart.IsDisposed)
                    smartPart.Dispose();
            }
            //smartPart.Dispose();
        }

        /// <summary>
        /// Hides the smart part.
        /// </summary>
        protected virtual void OnHide(System.Windows.Forms.Control smartPart)
        {
            //Versione Originale rivista il 2019.04.10
            //if (smartPart.Visible)
            //{
            //    PopulatePages();
            //    string key = pages[smartPart].AccessibleName;
            //    GetTabPageFromName(key).Hide();
            //    ActivateSiblingTab();
            //}
            //Versione Originale rivista il 2019.04.10
            PopulatePages();
            string key = pages[smartPart].AccessibleName;
            DevExpress.XtraBars.Docking2010.Views.BaseDocument page = GetTabPageFromName(key);
            int iPageIndex = TabbedView.Documents.IndexOf(page);
            if (page.Control.Visible == true)
            {
                TabbedView.BeginUpdate();
                page.Control.Visible = false;
                TabbedView.EndUpdate();
                ActivateSiblingTab(iPageIndex);
            }
        }

        /// <summary>
        /// Shows the smart part in the workspace.
        /// </summary>
        /// <param name="smartPart">The smart part to show</param>
        /// <param name="smartPartInfo">The associated smart part info for the smart part being shown.</param>
        protected virtual void OnShow(System.Windows.Forms.Control smartPart, XtraTabSmartPartInfo smartPartInfo)
        {
            PopulatePages();
            ResetSelectedIndexIfNoTabs();

            DevExpress.XtraBars.Docking2010.Views.BaseDocument page = GetOrCreateTabPage(smartPart);
            SetTabProperties(page, smartPartInfo);

            if (smartPartInfo.ActivateTab)
            {
                Activate(smartPart);
            }
            smartPart.Disposed -= ControlDisposed;
            smartPart.Disposed += ControlDisposed;
        }

        /// <summary>
        /// Raises the <see cref="SmartPartActivated"/> event.
        /// </summary>
        protected virtual void OnSmartPartActivated(Microsoft.Practices.CompositeUI.SmartParts.WorkspaceEventArgs e)
        {
            if (SmartPartActivated != null)
            {
                SmartPartActivated(this, e);
            }
        }

        /// <summary>
        /// Raises the <see cref="SmartPartClosing"/> event.
        /// </summary>
        protected virtual void OnSmartPartClosing(Microsoft.Practices.CompositeUI.SmartParts.WorkspaceCancelEventArgs e)
        {
            if (SmartPartClosing != null)
            {
                SmartPartClosing(this, e);
            }
        }
        /// <summary>
        /// See <see cref="IWorkspace.ActiveSmartPart"/> for more information.
        /// </summary>
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsTabClosable {
            get { 
                return _bIsTabClosable; 
            } 
            set {
                _bIsTabClosable = value;
                TabbedView.BeginUpdate();
                SetTabClosable(TabbedView, _bIsTabClosable);
                TabbedView.EndUpdate();
            }
        }

        /// <summary>
        /// Converts a smart part information to a compatible one for the workspace.
        /// </summary>
        protected virtual XtraTabSmartPartInfo ConvertFrom(ISmartPartInfo source)
        {
            return SmartPartInfo.ConvertTo<XtraTabSmartPartInfo>(source);
        }

        /// <summary>
        /// See <see cref="IComposableWorkspace{TSmartPart, TSmartPartInfo}.OnActivate"/> for more information.
        /// </summary>
        void IComposableWorkspace<System.Windows.Forms.Control, XtraTabSmartPartInfo>.OnActivate(System.Windows.Forms.Control smartPart)
        {
            OnActivate(smartPart);
        }

        /// <summary>
        /// See <see cref="IComposableWorkspace{TSmartPart, TSmartPartInfo}.OnApplySmartPartInfo"/> for more information.
        /// </summary>
        void IComposableWorkspace<System.Windows.Forms.Control, XtraTabSmartPartInfo>.OnApplySmartPartInfo(System.Windows.Forms.Control smartPart, XtraTabSmartPartInfo smartPartInfo)
        {
            OnApplySmartPartInfo(smartPart, smartPartInfo);
        }

        /// <summary>
        /// See <see cref="IComposableWorkspace{TSmartPart, TSmartPartInfo}.OnShow"/> for more information.
        /// </summary>
        void IComposableWorkspace<System.Windows.Forms.Control, XtraTabSmartPartInfo>.OnShow(System.Windows.Forms.Control smartPart, XtraTabSmartPartInfo smartPartInfo)
        {
            OnShow(smartPart, smartPartInfo);
        }

        /// <summary>
        /// See <see cref="IComposableWorkspace{TSmartPart, TSmartPartInfo}.OnHide"/> for more information.
        /// </summary>
        void IComposableWorkspace<System.Windows.Forms.Control, XtraTabSmartPartInfo>.OnHide(System.Windows.Forms.Control smartPart)
        {
            OnHide(smartPart);
        }

        /// <summary>
        /// See <see cref="IComposableWorkspace{TSmartPart, TSmartPartInfo}.OnClose"/> for more information.
        /// </summary>
        void IComposableWorkspace<System.Windows.Forms.Control, XtraTabSmartPartInfo>.OnClose(System.Windows.Forms.Control smartPart)
        {
            OnClose(smartPart);
        }

        /// <summary>
        /// See <see cref="IComposableWorkspace{TSmartPart, TSmartPartInfo}.RaiseSmartPartActivated"/> for more information.
        /// </summary>
        void IComposableWorkspace<System.Windows.Forms.Control, XtraTabSmartPartInfo>.RaiseSmartPartActivated(Microsoft.Practices.CompositeUI.SmartParts.WorkspaceEventArgs e)
        {
            OnSmartPartActivated(e);
        }

        /// <summary>
        /// See <see cref="IComposableWorkspace{TSmartPart, TSmartPartInfo}.RaiseSmartPartClosing"/> for more information.
        /// </summary>
        void IComposableWorkspace<System.Windows.Forms.Control, XtraTabSmartPartInfo>.RaiseSmartPartClosing(Microsoft.Practices.CompositeUI.SmartParts.WorkspaceCancelEventArgs e)
        {
            OnSmartPartClosing(e);
        }

        /// <summary>
        /// See <see cref="IComposableWorkspace{TSmartPart, TSmartPartInfo}.ConvertFrom"/> for more information.
        /// </summary>
        XtraTabSmartPartInfo IComposableWorkspace<System.Windows.Forms.Control, XtraTabSmartPartInfo>.ConvertFrom(ISmartPartInfo source)
        {
            return SmartPartInfo.ConvertTo<XtraTabSmartPartInfo>(source);
        }

        /// <summary>
        /// See <see cref="IWorkspace.ActiveSmartPart"/> for more information.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public object ActiveSmartPart
        {
            get { return composer.ActiveSmartPart; }
        }

        /// <summary>
        /// See <see cref="IWorkspace.SmartParts"/> for more information.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ReadOnlyCollection<object> SmartParts
        {
            get { return composer.SmartParts; }
        }

        /// <summary>
        /// Activates the specified smart part within the workspace.
        /// </summary>
        /// <param name="smartPart">Smart part to activate</param>
        public void Activate(object smartPart)
        {
            composer.Activate(smartPart);
        }

        /// <summary>
        /// Applies the specified smart part info to the specified smart part within the workspace.
        /// </summary>
        /// <param name="smartPart">Smart part to update</param>
        /// <param name="smartPartInfo">Smart part info to apply to the <paramref name="smartPart"/></param>
        public void ApplySmartPartInfo(object smartPart, ISmartPartInfo smartPartInfo)
        {
            composer.ApplySmartPartInfo(smartPart, smartPartInfo);
        }

        /// <summary>
        /// Shows the smart part within the workspace using the specified smart part info.
        /// </summary>
        /// <param name="smartPart">Smart part that should be displayed</param>
        /// <param name="smartPartInfo">Smart part info to applied to the smart part</param>
        public void Show(object smartPart, ISmartPartInfo smartPartInfo)
        {
            composer.Show(smartPart, smartPartInfo);
        }

        /// <summary>
        /// Shows the specified smart part within the workspace.
        /// </summary>
        /// <param name="smartPart">Smart part to show.</param>
        public void Show(object smartPart)
        {
            composer.Show(smartPart);
        }

        /// <summary>
        /// Hides the specified smart part.
        /// </summary>
        /// <param name="smartPart">Smart part within the workspace that should be hidden.</param>
        public void Hide(object smartPart)
        {
            composer.Hide(smartPart);
        }

        /// <summary>
        /// Closes the specified smart part and removes it from the workspace.
        /// </summary>
        /// <param name="smartPart">Smart part to close and remove.</param>
        public void Close(object smartPart)
        {
            composer.Close(smartPart);
        }

        /// <summary>
        /// See <see cref="IWorkspace.SmartPartClosing"/> for more information.
        /// </summary>
        public event EventHandler<Microsoft.Practices.CompositeUI.SmartParts.WorkspaceCancelEventArgs> SmartPartClosing;

        /// <summary>
        /// See <see cref="IWorkspace.SmartPartActivated"/> for more information.
        /// </summary>
        public event EventHandler<Microsoft.Practices.CompositeUI.SmartParts.WorkspaceEventArgs> SmartPartActivated;
        protected override void Dispose(bool disposing)
        {
            if (disposing && TabbedView!=null) { 
                try {
                    TabbedView.DocumentActivated -= DocumentActivated;
                    TabbedView.PopupMenuShowing -= TabbedView_PopupMenuShowing;
                    TabbedView.CustomDocumentsHostWindow -= TabbedView_CustomDocumentsHostWindow;
                    TabbedView.QueryControl -= TabbedView_QueryControl;
                    if (TabbedView.Documents!=null)
                        TabbedView.Documents.Clear();
                    TabbedView.Dispose(); 
                } catch { }
            }
            TabbedView = null;
        }
    }
}