using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using CABDevExpress.SmartPartInfos;
using DevExpress.XtraBars.Docking;
using Microsoft.Practices.CompositeUI.SmartParts;
using Microsoft.Practices.CompositeUI.Utility;
using System.Reflection;
using Microsoft.Practices.CompositeUI;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraEditors;
using System.Linq;

namespace CABDevExpress.Workspaces
{
    /// <summary>
    /// Implements a Workspace that shows smartparts in DockedWindows
    /// </summary>
    public class DockManagerWorkspace : Workspace<Control, DockManagerSmartPartInfo>, IDisposable
    {
        private readonly Dictionary<Control, DockPanel> _dockPanelDictionary = new Dictionary<Control, DockPanel>();
        private readonly DockManager _dockManager;

    	/// <summary>
        /// Initializes the workspace with no DockManager windows.
        /// </summary>
        public DockManagerWorkspace() { }
        private WorkItem WorkItem;
        string WorkSpaceName;
        private bool disposedValue;
        //private Form _parentform;
        /// <summary>
        /// Initializes the workspace with the DockManager which all new DockPanels are added to. 
        /// </summary>
        /// <param name="dockManager">The DockManager that new DockPanels are added to</param>
        public DockManagerWorkspace(DockManager dockManager)
        {
            Guard.ArgumentNotNull(dockManager.Form, "dockManager.Form");
            this._dockManager = dockManager;
            this._dockManager.UnregisterDockPanel -= dockManagerUnregisterDockPanel;
            this._dockManager.UnregisterDockPanel += dockManagerUnregisterDockPanel;
            this._dockManager.DockingOptions.AllowDockToCenter = DevExpress.Utils.DefaultBoolean.True;
        }

        private void FormClosed(object sender, FormClosedEventArgs e)
        {
            if (_dockPanelDictionary != null)
                foreach (DockPanel panel in _dockPanelDictionary.Values)
                    panel.Close();
        }

        private void FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_dockPanelDictionary != null)
                foreach (DockPanel panel in _dockPanelDictionary.Values)
                    panel.Close();
        }

        private void dockManagerUnregisterDockPanel(object sender, DockPanelEventArgs e)
        {
            if (_dockPanelDictionary!=null)
                foreach(DockPanel panel in _dockPanelDictionary.Values)
                {
                    if (object.Equals(panel, e.Panel) == true)
                        panel.Close();
                }
        }

        public DockManagerWorkspace(DockManager dockManager, WorkItem workItem, String workSpaceName) 
            : this(dockManager)
        {
            WorkItem = workItem;
            WorkSpaceName = workSpaceName;
            if (WorkItem.Workspaces[WorkSpaceName] != null)
            {
                IWorkspace dockWorkspace = WorkItem.Workspaces[WorkSpaceName];
                WorkItem.Workspaces.Remove(dockWorkspace);
                WorkItem.Items.Remove(this);
            }
            if (WorkItem.Workspaces[WorkSpaceName] == null)
            {
                WorkItem.Items.Add(this, WorkSpaceName);
                IWorkspace dockWorkspace = WorkItem.Workspaces[WorkSpaceName];
                WorkItem.Workspaces.Add(dockWorkspace);
            }

        }

        /// <summary>
        /// Read-only view of DockPanelDictionary.
        /// </summary>
        [Browsable(false)]
        public ReadOnlyDictionary<Control, DockPanel> DockPanels
        {
            get { return new ReadOnlyDictionary<Control, DockPanel>(_dockPanelDictionary); }
        }

        /// <summary>
        /// Creates a DockPanel if it does not already exist and adds the given control.
        /// </summary>
        protected DockPanel GetOrCreateDockPanel(Control control, DockManagerSmartPartInfo smartPartInfo)
        {
            DockPanel dockPanel = null;
            if (_dockPanelDictionary.ContainsKey(control))
            {
                dockPanel = _dockPanelDictionary[control];
            }
			else
            {	
                dockPanel = CreateDockPanel(control, smartPartInfo, dockPanel);
                CalculateSize(control, smartPartInfo, dockPanel);
                control.Disposed -= ControlDisposed;
                control.Disposed += ControlDisposed;
                WireUpPanel(dockPanel);
            }
            return dockPanel;
        }

        private void DockPanelClosedPanel(object sender, DockPanelEventArgs e)
        {
            Control smartpart = _dockPanelDictionary?.FirstOrDefault(w => Object.ReferenceEquals(w.Value, e?.Panel) == true).Key;
            //if (sender != null && sender is DockPanel)
            //    foreach (KeyValuePair<Control, DockPanel> kvp in _dockPanelDictionary)
            //        if (kvp.Value == sender)
            //        {
            //            kvp.Key.Dispose();
            //            break;
            //        }
            //TODO: riattivare la dispose
            if (smartpart != null)
            {
                RemoveEntry(smartpart);
                smartpart.Dispose();
            }
            InnerSmartParts.Remove(smartpart);
        }

        private void WireUpPanel(DockPanel dockPanel)
        {
            dockPanel.ClosingPanel -= DockPanelClosingPanel;
            dockPanel.ClosedPanel -= DockPanelClosedPanel;
            dockPanel.ClosingPanel += DockPanelClosingPanel;
            dockPanel.ClosedPanel += DockPanelClosedPanel;
            //20230525 disabilitata la sottoscrizione dell'evento FormClosed, non viene mai richiamato, indagare
            //if (_parentform == null)
            //{
            //    _parentform = this._dockManager.Form as Form;
            //    if (_parentform == null)
            //    {
            //        UserControl ctrl = this._dockManager.Form as UserControl;
            //        if (ctrl?.ParentForm != null)
            //            _parentform = ctrl?.ParentForm as Form;
            //    }
            //    if (_parentform != null)
            //    {
            //        _parentform.FormClosing -= FormClosing;
            //        _parentform.FormClosing += FormClosing;
            //        _parentform.FormClosed -= FormClosed;
            //        _parentform.FormClosed += FormClosed;
            //    }
            //}
        }

        protected virtual void BeforeEntryRemove(DockPanel frm) { }
        private void RemoveEntry(Control spcontrol)
        {
            DockPanel panel = null;
            if (_dockPanelDictionary != null)
                panel = _dockPanelDictionary[spcontrol];
            BeforeEntryRemove(panel);
            _dockPanelDictionary.Remove(spcontrol);
        }

        private void PushDockPanetPropertyOnSmartPart(Control smartpart, DockPanel dockPanel)
        {
            DockPanelSaveInfo dockPanelSaveInfo = null;
            if (dockPanel.Dock == DevExpress.XtraBars.Docking.DockingStyle.Float)
                dockPanelSaveInfo = new DockPanelSaveInfo()
                {
                    Rectangle = new System.Drawing.Rectangle(dockPanel.FloatFormRestoreBounds.X, dockPanel.FloatFormRestoreBounds.Y, dockPanel.FloatFormRestoreBounds.Width, dockPanel.FloatFormRestoreBounds.Height),
                    Visibility = dockPanel.Visibility,
                    Dock = dockPanel.Dock,
                    SavedDock = dockPanel.SavedDock,
                    TabsPosition = dockPanel.TabsPosition,
                };
            else
                dockPanelSaveInfo = new DockPanelSaveInfo()
                {
                    Rectangle = new System.Drawing.Rectangle(dockPanel.ClientRectangle.X, dockPanel.ClientRectangle.Y, dockPanel.ClientRectangle.Width, dockPanel.ClientRectangle.Height),
                    Visibility = dockPanel.Visibility,
                    Dock = dockPanel.Dock,
                    SavedDock = dockPanel.SavedDock,
                    TabsPosition = dockPanel.TabsPosition,
                };
            //https://supportcenter.devexpress.com/ticket/details/cq57302/how-can-i-determine-that-a-docking-panel-in-autohide-mode-is-made-visible-slides-out
            //Workaround, non si sa perchè in fase di chiusura della main form dockPanel.Visibility vale sempre DockVisibility.Visible
            if (dockPanelSaveInfo.Visibility == DockVisibility.Visible && dockPanel?.ControlContainer != null && dockPanel.ControlContainer.Visible == false)
                dockPanelSaveInfo.Visibility = DockVisibility.AutoHide;
            smartpart.Tag = dockPanelSaveInfo;

        }

        private void DockPanelClosingPanel(object sender, DockPanelCancelEventArgs e)
        {
            //if (sender!=null && sender is DockPanel)
            //    foreach(KeyValuePair<Control, DockPanel> kvp in _dockPanelDictionary)
            //        if (kvp.Value == sender)
            //        {
            //            WorkspaceCancelEventArgs canc = RaiseSmartPartClosing(kvp.Key);
            //            e.Cancel = canc.Cancel;
            //            break;
            //        }
            //RaiseSmartPartClosing(e);
            Control smartpart = _dockPanelDictionary?.FirstOrDefault(w => Object.ReferenceEquals(w.Value, e?.Panel) == true).Key;
            if (smartpart != null)
            {
                PushDockPanetPropertyOnSmartPart(smartpart, e.Panel);
                RaiseSmartPartClosing(smartpart);
            }
        }

        private DockPanel CreateDockPanel(Control control, DockManagerSmartPartInfo smartPartInfo, DockPanel dockPanel)
    	{
    		if (string.IsNullOrEmpty(smartPartInfo.ParentPanelName))
    		{
    			dockPanel = _dockManager.AddPanel(smartPartInfo.Dock);
    		}
    		else
    		{
    			foreach (DockPanel dockRootPanel in _dockManager.RootPanels)
    			{
    				if (dockRootPanel.Name != smartPartInfo.ParentPanelName) continue;

    				dockPanel = _dockManager.AddPanel(smartPartInfo.Dock);
    				dockPanel.DockAsTab(dockRootPanel);
    				break;
    			}

    			if (dockPanel == null)
    				dockPanel = _dockManager.AddPanel(smartPartInfo.Dock); //If the panel is not found, just create one
    		}

    		control.Dock = DockStyle.Fill;
    		_dockPanelDictionary.Add(control, dockPanel);
    		dockPanel.Controls.Add(control);
    		return dockPanel;
    	}

        private static void CalculateSize(Control smartPart, DockManagerSmartPartInfo smartPartInfo, DockPanel panel)
        {
            panel.ClientSize = smartPart.Size;
            if (smartPartInfo != null && smartPartInfo.FloatSize.Width != 0 && smartPartInfo.FloatSize.Height != 0)
                panel.ClientSize = new System.Drawing.Size(smartPartInfo.FloatSize.Width, smartPartInfo.FloatSize.Height);
            DockPanel existingPanel = panel.DockManager.RootPanels?.FirstOrDefault(w=>w.Dock == smartPartInfo.Dock);
            if (existingPanel!=null)
                panel.ClientSize = existingPanel.ClientSize;
        }
        /// <summary>
        /// Sets  <see cref="DockManagerSmartPartInfo"/> specific properties for the given DockPanel 
        /// </summary>
        protected static void SetDockPanelProperties(DockPanel dockPanel, DockManagerSmartPartInfo info)
        {
            //if (string.IsNullOrEmpty(info.ParentPanelName))
            if (!info.DoNotTouchDockStyleAndVisibility)
            {
                dockPanel.Dock = info.Dock;
                dockPanel.FloatLocation = info.FloatLocation;
                dockPanel.FloatSize = info.FloatSize;
                dockPanel.FloatVertical = info.FloatVertical;
                dockPanel.SavedDock = info.SavedDock;
                dockPanel.SavedIndex = info.SavedIndex;
                dockPanel.SavedParent = info.SavedParent;
                dockPanel.SavedTabbed = info.SavedTabbed;
                dockPanel.Visibility = info.Visibility;
            }

            dockPanel.ID = info.ID;
            dockPanel.ImageIndex = info.ImageIndex;
            dockPanel.Index = info.Index;
            dockPanel.Tabbed = info.Tabbed;
            dockPanel.TabsPosition = info.TabsPosition;
            dockPanel.TabsScroll = info.TabsScroll;
            dockPanel.TabText = info.TabText;
            dockPanel.Text = info.Title;
            dockPanel.Name = info.Name;
            dockPanel.Options.ShowCloseButton    = info.ShowCloseButton ;
            dockPanel.Options.ShowAutoHideButton = info.ShowAutoHideButton;
            dockPanel.Options.ShowMaximizeButton = info.ShowMaximizeButton;
            dockPanel.Options.ShowMinimizeButton = info.ShowMinimizeButton;
        }

        private void ControlDisposed(object sender, EventArgs e)
        {
            Control control = sender as Control;
            if (control != null && SmartParts.Contains(sender))
            {
                CloseInternal(control);
                if (_dockPanelDictionary.ContainsKey(control))
                    _dockPanelDictionary[control].Close();
                _dockPanelDictionary.Remove(control);
            }
        }

        private static void ShowDockPanel(DockPanel dockPanel, DockManagerSmartPartInfo smartPartInfo)
        {
            SetDockPanelProperties(dockPanel, smartPartInfo);
        }

        /// <summary>
        /// Shows the DockPanel for the smart part and brings it to the front.
        /// </summary>
        protected override void OnActivate(Control smartPart)
        {
        	DockPanel dockPanel = _dockPanelDictionary[smartPart];
            dockPanel.BringToFront();
        	dockPanel.Show();
        }

        /// <summary>
        /// If DockPanel already exist in the same position, doc new dockPanel on new tabbed panel
        /// </summary>
        //TODO:2016.11.17 new features to be tested
        protected void EvaluateOpenOnTab(DockPanel dockPanel)
        {
            //if (dockPanel.Dock != DockingStyle.Fill && dockPanel.Dock != DockingStyle.Float)
            if (dockPanel.Dock != DockingStyle.Float)
            {
                foreach (DockPanel currPanel in _dockPanelDictionary.Values)
                {
                    DockPanel currTabbedToDocAsTab = GetTabbedPanel(currPanel, dockPanel);
                    if (currTabbedToDocAsTab != null)
                        currTabbedToDocAsTab.DockAsTab(dockPanel);
                }
            }
        }
        //TODO:2016.11.17 new features to be tested


        /// <summary>
        /// Get existing DockPanel to anchor new panel
        /// </summary>
        protected DockPanel GetTabbedPanel(DockPanel currPanel, DockPanel dockPanel)
        {
            DockPanel destPanel = null;
            if (currPanel.ParentPanel != null && currPanel.ParentPanel.Tabbed)
                destPanel = GetTabbedPanel(currPanel.ParentPanel, dockPanel);
            else if (currPanel.Visible == true && currPanel.Dock == dockPanel.Dock && currPanel != dockPanel)
                destPanel = currPanel;
            return destPanel;
        }

        /// <summary>
        /// Sets the properties on the DockPanel based on the information.
        /// </summary>
        protected override void OnApplySmartPartInfo(Control smartPart, DockManagerSmartPartInfo smartPartInfo)
        {
            DockPanel dockPanel = _dockPanelDictionary[smartPart];
            SetDockPanelProperties(dockPanel, smartPartInfo);
            //RibonMergerManagerHelper.DoMergeRibbon(smartPart, this._dockManager.Form.TopLevelControl,
            //    (x) => x.MdiMergeStyle == RibbonMdiMergeStyle.Always);
        }

        /// <summary>
        /// Shows a DockPanel for the smart part and sets its properties.
        /// </summary>
        protected override void OnShow(Control smartPart, DockManagerSmartPartInfo smartPartInfo)
        {
            Guard.ArgumentNotNull(smartPart, "smartPart");
            try
            { 
                _dockManager.BeginUpdate();
                MethodInfo mi = _dockManager.GetType().GetMethod("SetRedrawNew", BindingFlags.Instance | BindingFlags.NonPublic);
                if (mi != null) mi.Invoke(_dockManager, new object[] { _dockManager.Form, false });
                DockPanel dockPanel = GetOrCreateDockPanel(smartPart, smartPartInfo);
                //TODO:2016.11.17 new features to be tested
                EvaluateOpenOnTab(dockPanel);
                //TODO:2016.11.17 new features to be tested
                smartPart.Show();
                ShowDockPanel(dockPanel, smartPartInfo);
                if (mi != null) mi.Invoke(_dockManager, new object[] { _dockManager.Form, true });
            }
            catch (Exception ex) { throw; }
            finally
            {
                _dockManager.EndUpdate();
                //RibonMergerManagerHelper.DoMergeRibbon(smartPart, this._dockManager.Form.TopLevelControl,
                //    (x) => x.MdiMergeStyle == RibbonMdiMergeStyle.Always);
            }
            //smartPart.Disposed -= ControlDisposed;
            //smartPart.Disposed += ControlDisposed;
        }

        /// <summary>
        /// Hides the DockPanel where the smart part is being shown.
        /// </summary>
        protected override void OnHide(Control smartPart)
        {
        	_dockPanelDictionary[smartPart].Hide();
        }

        /// <summary>
        /// Closes the DockPanel where the smart part is being shown. this methos is implementation of (Microsoft.Practices.CompositeUI.SmartParts=>protected abstract void OnClose(TSmartPart smartPart));
        /// </summary>
        protected override void OnClose(Control smartPart)
        {
            DockPanel dockPanel = null;
            if (smartPart != null)
            {
                smartPart.Disposed -= ControlDisposed;
                if (_dockPanelDictionary.ContainsKey(smartPart))
                {
                    dockPanel = _dockPanelDictionary[smartPart];
                    _dockPanelDictionary.Remove(smartPart);
                }
                if (dockPanel != null)
                {
                    dockPanel.ClosingPanel -= DockPanelClosingPanel;
                    dockPanel.ClosedPanel -= DockPanelClosedPanel;
                    PushDockPanetPropertyOnSmartPart(smartPart, dockPanel);
                    dockPanel.Controls.Remove(smartPart);   // Remove the smartPart from the DockPanel to avoid disposing it
                    _dockManager.RemovePanel(dockPanel);    // changed from dockPanel.Close() but not unit tested
                }
                if (!smartPart.IsDisposed)
                    smartPart.Dispose();
            }
        }
        public void SaveLayoutToStream(System.IO.Stream stream)
        {
            _dockManager?.SaveLayoutToStream(stream);
        }
        public void RestoreLayoutFromStream(System.IO.Stream stream)
        {
            _dockManager?.RestoreLayoutFromStream(stream);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    //if (_parentform != null)
                    //{
                    //    _parentform.FormClosing -= FormClosing;
                    //    _parentform.FormClosed -= FormClosed;
                    //}
                    this._dockManager.UnregisterDockPanel -= dockManagerUnregisterDockPanel;
                    if (WorkItem?.Workspaces[WorkSpaceName] != null)
                    {
                        IWorkspace dockWorkspace = WorkItem.Workspaces[WorkSpaceName];
                        WorkItem.Workspaces.Remove(dockWorkspace);
                        WorkItem.Items.Remove(this);
                        _dockPanelDictionary?.Clear();
                    }
                }

                WorkItem = null;

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~DockManagerWorkspace()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}