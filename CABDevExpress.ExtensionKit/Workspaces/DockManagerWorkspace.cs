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

        /// <summary>
        /// Initializes the workspace with the DockManager which all new DockPanels are added to. 
        /// </summary>
        /// <param name="dockManager">The DockManager that new DockPanels are added to</param>
        public DockManagerWorkspace(DockManager dockManager)
        {
            Guard.ArgumentNotNull(dockManager.Form, "dockManager.Form");
            this._dockManager = dockManager;
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
            }
            dockPanel.ClosingPanel += DockPanel_ClosingPanel;
            dockPanel.ClosedPanel += DockPanel_ClosedPanel;
            return dockPanel;
        }

        private void DockPanel_ClosedPanel(object sender, DockPanelEventArgs e)
        {
            if (sender != null && sender is DockPanel)
                foreach (KeyValuePair<Control, DockPanel> kvp in DockPanels)
                    if (kvp.Value == sender)
                    {
                        kvp.Key.Dispose();
                        break;
                    }
        }

        private void DockPanel_ClosingPanel(object sender, DockPanelCancelEventArgs e)
        {
            if (sender!=null && sender is DockPanel)
                foreach(KeyValuePair<Control, DockPanel> kvp in DockPanels)
                    if (kvp.Value == sender)
                    {
                        WorkspaceCancelEventArgs canc = RaiseSmartPartClosing(kvp.Key);
                        e.Cancel = canc.Cancel;
                        break;
                    }
            RaiseSmartPartClosing(e);
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

    	/// <summary>
		/// Sets  <see cref="DockManagerSmartPartInfo"/> specific properties for the given DockPanel 
        /// </summary>
        protected static void SetDockPanelProperties(DockPanel dockPanel, DockManagerSmartPartInfo info)
        {
            if (string.IsNullOrEmpty(info.ParentPanelName))
                dockPanel.Dock = info.Dock;

            dockPanel.FloatLocation = info.FloatLocation;
            dockPanel.FloatSize = info.FloatSize;
            dockPanel.FloatVertical = info.FloatVertical;
            dockPanel.ID = info.ID;
            dockPanel.ImageIndex = info.ImageIndex;
            dockPanel.Index = info.Index;
            dockPanel.SavedDock = info.SavedDock;
            dockPanel.SavedIndex = info.SavedIndex;
            dockPanel.SavedParent = info.SavedParent;
            dockPanel.SavedTabbed = info.SavedTabbed;
            dockPanel.Tabbed = info.Tabbed;
            dockPanel.TabsPosition = info.TabsPosition;
            dockPanel.TabsScroll = info.TabsScroll;
            dockPanel.TabText = info.TabText;
            dockPanel.Text = info.Title;
            dockPanel.Name = info.Name;
            dockPanel.Visibility = info.Visibility;
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
            if (dockPanel.Dock != DockingStyle.Fill && dockPanel.Dock != DockingStyle.Float)
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
                MethodInfo mi = _dockManager.GetType().GetMethod("SetRedraw", BindingFlags.Instance | BindingFlags.NonPublic);
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
            smartPart.Disposed -= ControlDisposed;
            smartPart.Disposed += ControlDisposed;
        }

        /// <summary>
        /// Hides the DockPanel where the smart part is being shown.
        /// </summary>
        protected override void OnHide(Control smartPart)
        {
        	_dockPanelDictionary[smartPart].Hide();
        }

        /// <summary>
        /// Closes the DockPanel where the smart part is being shown.
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
                    dockPanel.Controls.Remove(smartPart);   // Remove the smartPart from the DockPanel to avoid disposing it
                    _dockManager.RemovePanel(dockPanel);        // changed from dockPanel.Close() but not unit tested
                }
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