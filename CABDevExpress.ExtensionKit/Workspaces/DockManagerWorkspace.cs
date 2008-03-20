using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using CABDevExpress.SmartPartInfos;
using DevExpress.XtraBars.Docking;
using Microsoft.Practices.CompositeUI.SmartParts;
using Microsoft.Practices.CompositeUI.Utility;

namespace CABDevExpress.Workspaces
{
    /// <summary>
    /// Implements a Workspace that shows smartparts in DockedWindows
    /// </summary>
    public class DockManagerWorkspace : Workspace<Control, DockManagerSmartPartInfo>
    {
        private readonly Dictionary<Control, DockPanel> dockPanelDictionary = new Dictionary<Control, DockPanel>();
        private readonly DockManager dockManager;

    	/// <summary>
        /// Initializes the workspace with no DockManager windows.
        /// </summary>
        public DockManagerWorkspace() { }

        /// <summary>
        /// Initializes the workspace with the DockManager which a all new DockPanels are added to. 
        /// </summary>
        /// <param name="dockManager">The DockManager that new DockPanels are added to</param>
        public DockManagerWorkspace(DockManager dockManager)
        {
            this.dockManager = dockManager;
        }

        /// <summary>
        /// Read-only view of DockPanelDictionary.
        /// </summary>
        [Browsable(false)]
        public ReadOnlyDictionary<Control, DockPanel> DockPanels
        {
            get { return new ReadOnlyDictionary<Control, DockPanel>(dockPanelDictionary); }
        }

        /// <summary>
        /// Creates a DockPanel if it does not already exist and adds the given control.
        /// </summary>
        protected DockPanel GetOrCreateDockPanel(Control control, DockManagerSmartPartInfo smartPartInfo)
        {
            DockPanel dockPanel = null;
            if (dockPanelDictionary.ContainsKey(control))
            {
                dockPanel = dockPanelDictionary[control];
            }
			else
            {	
                dockPanel = CreateDockPanel(control, smartPartInfo, dockPanel);
            }
            return dockPanel;
        }

    	private DockPanel CreateDockPanel(Control control, DockManagerSmartPartInfo smartPartInfo, DockPanel dockPanel)
    	{
    		if (!string.IsNullOrEmpty(smartPartInfo.ParentPanelName))
    		{
    			foreach (DockPanel dockRootPanel in dockManager.RootPanels)
    			{
    				if (dockRootPanel.Name == smartPartInfo.ParentPanelName)	// could we use Parents ID instead of Name?
    				{
    					//dockPanel = dockRootPanel.AddPanel(); //why doesn't this work? - a bug in the DockManager library?
    					//The lines below do work, but make the screen flicker because the panel
    					//is created outside it's parent container

    					dockPanel = dockManager.AddPanel(DockingStyle.Left);	//The name and ID will be set later. 
    					dockPanel.DockAsTab(dockRootPanel);
    					break;
    				}
    			}

    			if (dockPanel == null)
					dockPanel = dockManager.AddPanel(DockingStyle.Float);	//If the panel is not found, just create one
    		}
    		else
    		{
    			dockPanel = dockManager.AddPanel(DockingStyle.Float);
    		}
    		dockPanelDictionary.Add(control, dockPanel);
    		dockPanel.Controls.Add(control);
    		return dockPanel;
    	}

    	/// <summary>
		/// Sets  <see cref="DockManagerSmartPartInfo"/> specific properties for the given dockpanel. 
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
        }

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

        private static void ShowDockPanel(DockPanel dockPanel, DockManagerSmartPartInfo smartPartInfo)
        {
            SetDockPanelProperties(dockPanel, smartPartInfo);
        }

        /// <summary>
        /// Shows the DockPanel for the smart part and brings it to the front.
        /// </summary>
        protected override void OnActivate(Control smartPart)
        {
        	DockPanel dockPanel = dockPanelDictionary[smartPart];
        	dockPanel.BringToFront();
        	dockPanel.Show();
        }

        /// <summary>
        /// Sets the properties on the DockPanel based on the information.
        /// </summary>
        protected override void OnApplySmartPartInfo(Control smartPart, DockManagerSmartPartInfo smartPartInfo)
        {
            DockPanel dockPanel = dockPanelDictionary[smartPart];
            SetDockPanelProperties(dockPanel, smartPartInfo);
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
        	dockPanelDictionary[smartPart].Hide();
        }

        /// <summary>
        /// Closes the DockPanel where the smart part is being shown.
        /// </summary>
        protected override void OnClose(Control smartPart)
        {
            DockPanel dockPanel = dockPanelDictionary[smartPart];
            smartPart.Disposed -= ControlDisposed;

			dockPanel.Controls.Remove(smartPart);	// Remove the smartPart from the DockPanel to avoid disposing it

            dockPanel.Close();
            dockPanelDictionary.Remove(smartPart);
        }
    }
}