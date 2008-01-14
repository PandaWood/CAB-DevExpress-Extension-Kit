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

namespace CABDevExpress.Workspaces
{
    /// <summary>
    /// A workspace that displays smart parts within a <see cref="XtraTabControl"/>
    /// <remarks>
	/// The code here was copied and adapted from CAB's plain-vanilla <see cref="TabWorkspace"/>
	/// It couldn't be inherited because some property names and features of the XtraTab are different
    /// </remarks>
    /// </summary>
    [Description("XtraTab Workspace")]
    public class XtraTabWorkspace : XtraTabControl, IComposableWorkspace<Control, XtraTabSmartPartInfo>
    {
        private readonly Dictionary<Control, XtraTabPage> pages = new Dictionary<Control, XtraTabPage>();
        private readonly WorkspaceComposer<Control, XtraTabSmartPartInfo> composer;
        private bool callComposerActivateOnIndexChange = true;
        private bool populatingPages = false;

        /// <summary>
        /// Initializes a new <see cref="XtraTabWorkspace"/>
        /// </summary>
        public XtraTabWorkspace()
        {
            composer = new WorkspaceComposer<Control, XtraTabSmartPartInfo>(this);
        }

        /// <summary>
        /// Dependency injection setter property to get the <see cref="WorkItem"/> where object is contained.
        /// </summary>
        [ServiceDependency]
        public WorkItem WorkItem
        {
            set { composer.WorkItem = value; }
        }

        /// <summary>
        /// Gets the collection of pages that the tab workspace uses.
        /// </summary>
        public ReadOnlyDictionary<Control, XtraTabPage> Pages
        {
            get { return new ReadOnlyDictionary<Control, XtraTabPage>(pages); }
        }

        private void SetTabProperties(XtraTabPage page, XtraTabSmartPartInfo smartPartInfo)
        {
            page.Text = String.IsNullOrEmpty(smartPartInfo.Title) ? page.Text : smartPartInfo.Title;

            try
            {
                XtraTabPage currentSelection = SelectedTabPage;
                callComposerActivateOnIndexChange = false;
                if (smartPartInfo.Position == TabPosition.Beginning)
                {
                    XtraTabPage[] tabPages = GetTabPages();
                    TabPages.Clear();

                    TabPages.Add(page);
                    TabPages.AddRange(tabPages);
                }
                else if (TabPages.Contains(page) == false)
                {
                    TabPages.Add(page);
                }

                page.BackColor = smartPartInfo.BackColor;
                page.ForeColor = smartPartInfo.ForeColor;
                page.Image = smartPartInfo.Image;
                page.ImageIndex = smartPartInfo.ImageIndex;
                page.PageEnabled = smartPartInfo.PageEnabled;
                page.PageVisible = smartPartInfo.PageVisible;

				if (smartPartInfo.Text != null || smartPartInfo.Text != null)	// don't apply if not set
                    page.Text = smartPartInfo.Text ?? smartPartInfo.Title;

                page.Tooltip = smartPartInfo.Tooltip;
				
				if (smartPartInfo.PageHeaderFont != null)
				{
					page.Appearance.Header.Font = smartPartInfo.PageHeaderFont;
					page.Appearance.Header.Options.UseFont = true;
				}

				SelectedTabPage = currentSelection;		// preserve selection through the operation
            }
            finally
            {
                callComposerActivateOnIndexChange = true;
            }
        }

        private XtraTabPage[] GetTabPages()
        {
            XtraTabPage[] tabPages = new XtraTabPage[TabPages.Count];
            for (int i = 0; i < tabPages.Length; i++)
            {
                tabPages[i] = TabPages[i];
            }

            return tabPages;
        }

        private XtraTabPage GetOrCreateTabPage(Control smartPart)
        {
            XtraTabPage page = null;

            // If the tab was added with the control at design-time, it will have a parent control, 
            // and somewhere up its containment chain we'll find one of our tabs
            Control current = smartPart;
            while (current != null && page == null)
            {
                current = current.Parent;
                page = current as XtraTabPage;
            }

            if (page == null)
            {
                page = new XtraTabPage();
                page.Controls.Add(smartPart);
                smartPart.Dock = DockStyle.Fill;
                page.Name = Guid.NewGuid().ToString();

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
            if (!populatingPages && pages.Count != TabPages.Count)
            {
                foreach (XtraTabPage page in TabPages)
                {
                    if (pages.ContainsValue(page) == false)
                    {
                        Control control = GetControlFromPage(page);
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
            Control control = sender as Control;
            if (control != null && pages.ContainsKey(control))
            {
                composer.ForceClose(control);
            }
        }

        private static Control GetControlFromPage(Control page)
        {
            Control control = null;
            if (page.Controls.Count > 0)
            {
                control = page.Controls[0];
            }

            return control;
        }

        /// <summary>
        /// Fires the <see cref="SmartPartActivated"/> event whenever 
        /// the selected tab index changes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">An <see cref="EventArgs"/> that contains the event data.</param>
        protected override void OnSelectedPageChanged(object sender, ViewInfoTabPageChangedEventArgs e)
        {
            base.OnSelectedPageChanged(sender, e);
            if (callComposerActivateOnIndexChange && TabPages.Count != 0)
            {
                // Locate the smart part corresponding to the page.
                foreach (KeyValuePair<Control, XtraTabPage> pair in pages)
                {
                    if (pair.Value == SelectedTabPage)
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

        /// <summary>
        /// Hooks up tab pages added at design-time.
        /// </summary>
        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            PopulatePages();
        }

        private void ActivateSiblingTab()
        {
            if (SelectedTabPageIndex > 0)
            {
                SelectedTabPageIndex -= 1;
            }
            else if (SelectedTabPageIndex < TabPages.Count - 1)
            {
                SelectedTabPageIndex += 1;
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
            if (TabPages.Count == 0)
            {
                try
                {
                    callComposerActivateOnIndexChange = false;
                    SelectedTabPageIndex = -1;
                }
                finally
                {
                    callComposerActivateOnIndexChange = true;
                }
            }
        }

        private XtraTabPage GetTabPageFromName(string name)
        {
            foreach (XtraTabPage tabPage in TabPages)
            {
            	if (tabPage.Name == name)
            		return tabPage;
            }
            return null;
        }

        /// <summary>
        /// Activates the smart part within the workspace.
        /// </summary>
        /// <param name="smartPart">The smart part to activate</param>
        protected virtual void OnActivate(Control smartPart)
        {
            PopulatePages();

            string key = pages[smartPart].Name;

            try
            {
                callComposerActivateOnIndexChange = false;
                SelectedTabPage = GetTabPageFromName(key);
                SelectedTabPage.Show();
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
        protected virtual void OnApplySmartPartInfo(Control smartPart, XtraTabSmartPartInfo smartPartInfo)
        {
            PopulatePages();
            string key = pages[smartPart].Name;
            SetTabProperties(GetTabPageFromName(key), smartPartInfo);
            if (smartPartInfo.ActivateTab)
            {
                Activate(smartPart);
            }
        }

        /// <summary>
        /// Closes/removes the smart part.
        /// </summary>
        protected virtual void OnClose(Control smartPart)
        {
            PopulatePages();
            TabPages.Remove(pages[smartPart]);
            pages.Remove(smartPart);

            smartPart.Disposed -= ControlDisposed;
            //smartPart.Dispose();
        }

        /// <summary>
        /// Hides the smart part.
        /// </summary>
        protected virtual void OnHide(Control smartPart)
        {
            if (smartPart.Visible)
            {
                PopulatePages();
                string key = pages[smartPart].Name;
                //TabPages[key].Hide();
                GetTabPageFromName(key).Hide();
                ActivateSiblingTab();
            }
        }

        /// <summary>
        /// Shows the smart part in the workspace.
        /// </summary>
        /// <param name="smartPart">The smart part to show</param>
        /// <param name="smartPartInfo">The associated smart part info for the smart part being shown.</param>
        protected virtual void OnShow(Control smartPart, XtraTabSmartPartInfo smartPartInfo)
        {
            PopulatePages();
            ResetSelectedIndexIfNoTabs();

            XtraTabPage page = GetOrCreateTabPage(smartPart);
            SetTabProperties(page, smartPartInfo);

            if (smartPartInfo.ActivateTab)
            {
                Activate(smartPart);
            }

            smartPart.Disposed += ControlDisposed;
        }

        /// <summary>
        /// Raises the <see cref="SmartPartActivated"/> event.
        /// </summary>
        protected virtual void OnSmartPartActivated(WorkspaceEventArgs e)
        {
            if (SmartPartActivated != null)
            {
                SmartPartActivated(this, e);
            }
        }

        /// <summary>
        /// Raises the <see cref="SmartPartClosing"/> event.
        /// </summary>
        protected virtual void OnSmartPartClosing(WorkspaceCancelEventArgs e)
        {
            if (SmartPartClosing != null)
            {
                SmartPartClosing(this, e);
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
        void IComposableWorkspace<Control, XtraTabSmartPartInfo>.OnActivate(Control smartPart)
        {
            OnActivate(smartPart);
        }

        /// <summary>
        /// See <see cref="IComposableWorkspace{TSmartPart, TSmartPartInfo}.OnApplySmartPartInfo"/> for more information.
        /// </summary>
        void IComposableWorkspace<Control, XtraTabSmartPartInfo>.OnApplySmartPartInfo(Control smartPart, XtraTabSmartPartInfo smartPartInfo)
        {
            OnApplySmartPartInfo(smartPart, smartPartInfo);
        }

        /// <summary>
        /// See <see cref="IComposableWorkspace{TSmartPart, TSmartPartInfo}.OnShow"/> for more information.
        /// </summary>
        void IComposableWorkspace<Control, XtraTabSmartPartInfo>.OnShow(Control smartPart, XtraTabSmartPartInfo smartPartInfo)
        {
            OnShow(smartPart, smartPartInfo);
        }

        /// <summary>
        /// See <see cref="IComposableWorkspace{TSmartPart, TSmartPartInfo}.OnHide"/> for more information.
        /// </summary>
        void IComposableWorkspace<Control, XtraTabSmartPartInfo>.OnHide(Control smartPart)
        {
            OnHide(smartPart);
        }

        /// <summary>
        /// See <see cref="IComposableWorkspace{TSmartPart, TSmartPartInfo}.OnClose"/> for more information.
        /// </summary>
        void IComposableWorkspace<Control, XtraTabSmartPartInfo>.OnClose(Control smartPart)
        {
            OnClose(smartPart);
        }

        /// <summary>
        /// See <see cref="IComposableWorkspace{TSmartPart, TSmartPartInfo}.RaiseSmartPartActivated"/> for more information.
        /// </summary>
        void IComposableWorkspace<Control, XtraTabSmartPartInfo>.RaiseSmartPartActivated(WorkspaceEventArgs e)
        {
            OnSmartPartActivated(e);
        }

        /// <summary>
        /// See <see cref="IComposableWorkspace{TSmartPart, TSmartPartInfo}.RaiseSmartPartClosing"/> for more information.
        /// </summary>
        void IComposableWorkspace<Control, XtraTabSmartPartInfo>.RaiseSmartPartClosing(WorkspaceCancelEventArgs e)
        {
            OnSmartPartClosing(e);
        }

        /// <summary>
        /// See <see cref="IComposableWorkspace{TSmartPart, TSmartPartInfo}.ConvertFrom"/> for more information.
        /// </summary>
        XtraTabSmartPartInfo IComposableWorkspace<Control, XtraTabSmartPartInfo>.ConvertFrom(ISmartPartInfo source)
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
        public event EventHandler<WorkspaceCancelEventArgs> SmartPartClosing;

        /// <summary>
        /// See <see cref="IWorkspace.SmartPartActivated"/> for more information.
        /// </summary>
        public event EventHandler<WorkspaceEventArgs> SmartPartActivated;
    }
}