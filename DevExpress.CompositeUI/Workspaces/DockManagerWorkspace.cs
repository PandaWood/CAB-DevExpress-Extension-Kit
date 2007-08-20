using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using DevExpress.CompositeUI.SmartPartInfos;
using DevExpress.XtraBars.Docking;
using Microsoft.Practices.CompositeUI.SmartParts;
using Microsoft.Practices.CompositeUI.Utility;

namespace DevExpress.CompositeUI.Workspaces
{
    /// <summary>
    /// Implements a Workspace that shows smartparts in DockedWindows
    /// </summary>
    public class DockManagerWorkspace : Workspace<Control, DockManagerSmartPartInfo>
    {
        #region Private Members

        private readonly Dictionary<Control, DockPanel> dockPanelDictionary = new Dictionary<Control, DockPanel>();
        private bool fireActivatedFromDockPanel = true; // TODO field never used
        private readonly DockManager dockManager;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes the workspace with no DockManager
        /// windows.
        /// </summary>
        public DockManagerWorkspace()
        {
        }

        /// <summary>
        /// Initializes the workspace with the DockManager which a all new DockPanels are added to. 
        /// DockPanels.
        /// </summary>
        /// <param name="dockManager">The DockManager that new DockPanels are added to</param>
        public DockManagerWorkspace(DockManager dockManager)
        {
            this.dockManager = dockManager;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Read-only view of DockPanelDictionary.
        /// </summary>
        [Browsable(false)]
        public ReadOnlyDictionary<Control, DockPanel> DockPanels
        {
            get { return new ReadOnlyDictionary<Control, DockPanel>(dockPanelDictionary); }
        }

        #endregion

        #region Protected

        /// <summary>
        /// Creates a DockPanel if it does not already exist and adds the given control.
        /// </summary>
        /// <param name="control"></param>
        /// <returns></returns>
        /// <param name="smartPartInfo"></param>
        protected DockPanel GetOrCreateDockPanel(Control control, DockManagerSmartPartInfo smartPartInfo)
        {
            DockPanel dockPanel = null;
            if (dockPanelDictionary.ContainsKey(control))
            {
                dockPanel = dockPanelDictionary[control];
            }
            else
            {
                //Create a new DocPanel
                if (smartPartInfo.ParentPanelName != String.Empty)
                {
                    foreach (DockPanel dockRootPanel in dockManager.RootPanels)
                    {
                        if (dockRootPanel.Name == smartPartInfo.ParentPanelName)
                            //We should probably use the Parents ID instead of the Name. 
                        {
                            //dockPanel = dockRootPanel.AddPanel();
                            //why doesn't the line above work?? I suspect a bug in the DockManager library
                            //The lines below do work, but makes the screen flicker since the panel
                            //is first created outside it's parent container. 

                            dockPanel = dockManager.AddPanel(DockingStyle.Left);
                            //The name and ID will be set later. 
                            dockPanel.DockAsTab(dockRootPanel);
                            break;
                        }
                    }
                    //If the panel is not found should we raise an exception or just create it as a DockingStyle.Float panel?
                    if (dockPanel == null)
                        dockPanel = dockManager.AddPanel(DockingStyle.Float);
                }
                else
                {
                    dockPanel = dockManager.AddPanel(DockingStyle.Float);
                }
                dockPanelDictionary.Add(control, dockPanel);
                dockPanel.Controls.Add(control);
            }
            return dockPanel;
        }

        /// <summary>
        /// Sets specific properties for the given dockpanel. 
        /// </summary>
        protected static void SetDockPanelProperties(DockPanel dockPanel, DockManagerSmartPartInfo info)
        {
            //TODO this code needs serious cleanup!!!

            //dockPanel.ActiveChild = info.ActiveChild;
            //dockPanel.ActiveChildIndex = info.ActiveChildIndex;

            if (info.ParentPanelName == String.Empty)
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

            //////dockPanel.Visibility = info.Visibility;
        }

        /// <summary>
        /// Sets the location information for the given DockPaneø
        /// </summary>
        protected static void SetDockPanelLocation(DockPanel dockPanel, DockManagerSmartPartInfo info)
        {
            //We do not need this!
            //dockPanel.Location = info.Location;
        }

        #endregion

        #region Private

        private void ControlDisposed(object sender, EventArgs e)
        {
            Control control = sender as Control;
            if (control != null && SmartParts.Contains(sender))
            {
                CloseInternal(control);
                dockPanelDictionary[control].Close();
                dockPanelDictionary.Remove(control);
            }
        }

        /* TODO : Unused Code
        private void WireUpDockPanel(DockPanel dockPanel)
        {
            //dockPanel.ClosingPanel. += new EventHandler<WorkspaceCancelEventArgs>(DockPanelClosing);
            //dockPanel.ClosedPanel += new EventHandler<WorkspaceEventArgs>(DockPanelClosed);
            //cokcPanel.WindowFormActivated += new EventHandler<WorkspaceEventArgs>(DockPanelActivated);
        }

        private void DockPanelActivated(object sender, WorkspaceEventArgs e)
        {
            if (fireActivatedFromDockPanel)
            {
                RaiseSmartPartActivated(e.SmartPart);
                SetActiveSmartPart(e.SmartPart);
            }
        }

        private void DockPanelClosed(object sender, WorkspaceEventArgs e)
        {
            RemoveEntry((Control) e.SmartPart);
            InnerSmartParts.Remove((Control) e.SmartPart);
        }

        private void DockPanelClosing(object sender, WorkspaceCancelEventArgs e)
        {
            RaiseSmartPartClosing(e);
        }

        private void CalculateSize(Control smartPart, DockPanel dockPanel)
        {
            //			dockPanel.Size = new Size(smartPart.Size.Width, smartPart.Size.Height + 20);
        }

        private void RemoveEntry(Control spcontrol)
        {
            dockPanelDictionary.Remove(spcontrol);
        }
        */

        private static void ShowDockPanel(DockPanel dockPanel, DockManagerSmartPartInfo smartPartInfo)
        {
            SetDockPanelProperties(dockPanel, smartPartInfo);
        }

        #endregion

        #region Private DockPanel Class

        /// <summary>
        /// DockPanelWindow class
        /// </summary>
        //private class DockPanelWindow : DockPanel
        //{
        //    /// <summary>
        //    /// Fires when DockPanel is closing
        //    /// </summary>
        //    public event EventHandler<WorkspaceCancelEventArgs> DockPanelClosing;

        //    /// <summary>
        //    /// Fires when DockPanel is closed
        //    /// </summary>
        //    public event EventHandler<WorkspaceEventArgs> DockPanelClosed;

        //    /// <summary>
        //    /// Fires when DockPanel is activated
        //    /// </summary>
        //    public event EventHandler<WorkspaceEventArgs> DockPanelActivated;

        //    /// <summary>
        //    /// Handles Activated Event.
        //    /// </summary>
        //    /// <param name="e"></param>
        //    protected override void OnActivated(EventArgs e)
        //    {
        //        if (this.Controls.Count > 0)
        //        {
        //            this.DockPanelActivated(this, new WorkspaceEventArgs(this.Controls[0]));
        //        }

        //        base.OnActivated(e);
        //    }


        //    /// <summary>
        //    /// Handles the Closing Event
        //    /// </summary>
        //    /// <param name="e"></param>
        //    protected override void OnClosing(CancelEventArgs e)
        //    {
        //        if (this.Controls.Count > 0)
        //        {
        //            WorkspaceCancelEventArgs cancelArgs = FireDockPanelClosing(this.Controls[0]);
        //            e.Cancel = cancelArgs.Cancel;

        //            if (cancelArgs.Cancel == false)
        //            {
        //                this.Controls[0].Hide();
        //            }
        //        }

        //        base.OnClosing(e);
        //    }

        //    /// <summary>
        //    /// Handles the Closed Event
        //    /// </summary>
        //    /// <param name="e"></param>
        //    protected override void OnClosed(EventArgs e)
        //    {
        //        if ((this.DockPanelClosed != null) &&
        //            (this.Controls.Count > 0))
        //        {
        //            this.DockPanelClosed(this, new WorkspaceEventArgs(this.Controls[0]));
        //        }

        //        base.OnClosed(e);
        //    }

        //    private WorkspaceCancelEventArgs FireDockPanelClosing(object smartPart)
        //    {
        //        WorkspaceCancelEventArgs cancelArgs = new WorkspaceCancelEventArgs(smartPart);

        //        if (this.DockPanel != null)
        //        {
        //            this.DockPanelClosing(this, cancelArgs);
        //        }

        //        return cancelArgs;
        //    }
        //}

        #endregion

        #region Behavior overrides

        /// <summary>
        /// Shows the DockPanel for the smart part and brings it to the front.
        /// </summary>
        protected override void OnActivate(Control smartPart)
        {
            // Prevent double firing from composer Workspace class and from DockPanel.
            try
            {
                fireActivatedFromDockPanel = false;
                DockPanel dockPanel = dockPanelDictionary[smartPart];
                dockPanel.BringToFront();
                dockPanel.Show();
            }
            finally
            {
                fireActivatedFromDockPanel = true;
            }
        }

        /// <summary>
        /// Sets the properties on the DockPanel based on the information.
        /// </summary>
        protected override void OnApplySmartPartInfo(Control smartPart, DockManagerSmartPartInfo smartPartInfo)
        {
            DockPanel dockPanel = dockPanelDictionary[smartPart];
            SetDockPanelProperties(dockPanel, smartPartInfo);
            SetDockPanelLocation(dockPanel, smartPartInfo);
        }

        /// <summary>
        /// Shows a DockPanel for the smart part and sets its properties.
        /// </summary>
        protected override void OnShow(Control smartPart, DockManagerSmartPartInfo smartPartInfo)
        {
            DockPanel dockPanel = GetOrCreateDockPanel(smartPart, smartPartInfo);
            smartPart.Show();
            ShowDockPanel(dockPanel, smartPartInfo);
        }

        /// <summary>
        /// Hides the DockPanel where the smart part is being shown.
        /// </summary>
        protected override void OnHide(Control smartPart)
        {
            DockPanel dockPanel = dockPanelDictionary[smartPart];
            dockPanel.Hide();
        }

        /// <summary>
        /// Closes the DockPanel where the smart part is being shown.
        /// </summary>
        protected override void OnClose(Control smartPart)
        {
            DockPanel dockPanel = dockPanelDictionary[smartPart];
            smartPart.Disposed -= ControlDisposed;

            //// Remove the smartPart from the DockPanel to avoid disposing it.
            dockPanel.Controls.Remove(smartPart);

            dockPanel.Close();
            dockPanelDictionary.Remove(smartPart);
        }

        #endregion
    }
}