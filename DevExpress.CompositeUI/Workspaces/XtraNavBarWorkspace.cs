using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Forms;
using CABDevExpress.SmartPartInfos;
using DevExpress.XtraNavBar;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.SmartParts;

namespace CABDevExpress.Workspaces
{
    /// <summary>
	/// A workspace that displays smart parts within an <see cref="NavBarControl"/>
    /// </summary>
    [Description("XtraNavBar Workspace")]
    public class XtraNavBarWorkspace : NavBarControl, IComposableWorkspace<Control, XtraNavBarGroupSmartPartInfo>
    {
        #region Private Members

        private readonly XtraWorkspaceComposer<NavBarGroup, XtraNavBarGroupSmartPartInfo> composer;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new <see cref="XtraNavBarWorkspace"/>
        /// </summary>
        public XtraNavBarWorkspace()
        {
            composer = new XtraWorkspaceComposer<NavBarGroup, XtraNavBarGroupSmartPartInfo>(this, true);
        }

        #endregion

        #region WorkItem

        /// <summary>
        /// Dependency injection setter property to get the <see cref="WorkItem"/> where the object is contained.
        /// </summary>
        [ServiceDependency]
        public WorkItem WorkItem
        {
            set { composer.WorkItem = value; }
        }

        #endregion

        #region Private Methods

        private static void ApplySmartPartInfoHelper(NavBarGroup group, XtraImageSmartPartInfo smartPartInfo)
        {
            group.Caption = smartPartInfo.Title;
            group.Hint = smartPartInfo.Description;

            if (smartPartInfo.SmallImage != null)
                group.SmallImage = smartPartInfo.SmallImage;

            if (smartPartInfo.LargeImage != null)
            {
                group.LargeImage = smartPartInfo.LargeImage;
                group.GroupCaptionUseImage = NavBarImage.Large;
            }
        }

        #endregion

        #region Protected virtual implementations

        /// <summary>
        /// Activates the smart part within the workspace.
        /// </summary>
        /// <param name="smartPart">The smart part to activate</param>
        protected virtual void OnActivate(Control smartPart)
        {
            NavBarGroup group = composer[smartPart];
            group.Visible = true;
            group.Expanded = true;
            smartPart.Focus();
        }

        /// <summary>
        /// Applies the smart part info to the smart part within the workspace.
        /// </summary>
        /// <param name="smartPart">The smart part to which the smart part info should be applied.</param>
        /// <param name="smartPartInfo">The smart part info to apply</param>
        protected virtual void OnApplySmartPartInfo(Control smartPart, XtraNavBarGroupSmartPartInfo smartPartInfo)
        {
            NavBarGroup group = composer[smartPart];
            ApplySmartPartInfoHelper(group, smartPartInfo);
        }

        /// <summary>
        /// Closes/removes the smart part.
        /// </summary>
        protected virtual void OnClose(Control smartPart)
        {
            // find the group we contained the smartpart within
            NavBarGroup group = composer[smartPart];

            // clean up the group<=>smartpart references
            composer.Remove(group, smartPart);

            // at design time, we won't remove the group 
            // if you remove the first control
            if (DesignMode == false)
            {
                // reparent the control
                if (smartPart.Disposing == false && smartPart.IsDisposed == false)
                    smartPart.Parent = null;

                // TODO : Check why group not removeable
                if (Groups.IndexOf(group) > 0)
                {
                    // get rid of the navbar group
                    Groups.Remove(group);
                    group.Dispose();
                }
            }
        }

        /// <summary>
        /// Hides the smart part.
        /// </summary>
        protected virtual void OnHide(Control smartPart)
        {
            NavBarGroup group = composer[smartPart];
            if (group != null)
                group.Visible = false;
        }

        /// <summary>
        /// Shows the smart part in the workspace.
        /// </summary>
        /// <param name="smartPart">The smart part to show</param>
        /// <param name="smartPartInfo">The associated smart part info for the smart part being shown.</param>
        protected virtual void OnShow(Control smartPart, XtraNavBarGroupSmartPartInfo smartPartInfo)
        {
            // Group
            NavBarGroup group = new NavBarGroup();
            group.GroupStyle = NavBarGroupStyle.ControlContainer;
            group.GroupClientHeight = smartPart.Height;
            group.Expanded = true;

            // SmartPartInfo
            ApplySmartPartInfoHelper(group, smartPartInfo);

            // Keep associations between the smart part and group that represents it
            composer.Add(group, smartPart);

            // Store the new group
            Groups.Add(group);

            // Controlcontainer
            NavBarGroupControlContainer controlContainer = new NavBarGroupControlContainer();
            controlContainer.Size = smartPart.Size;
            group.ControlContainer = controlContainer;

            // Add the smart part control
            smartPart.Dock = DockStyle.Fill;
            controlContainer.Controls.Add(smartPart);
        }

        /// <summary>
        /// Raises the <see cref="SmartPartActivated"/> event.
        /// </summary>
        protected virtual void OnSmartPartActivated(WorkspaceEventArgs e)
        {
            if (SmartPartActivated != null) SmartPartActivated(this, e);
        }

        /// <summary>
        /// Raises the <see cref="SmartPartClosing"/> event.
        /// </summary>
        protected virtual void OnSmartPartClosing(WorkspaceCancelEventArgs e)
        {
            if (SmartPartClosing != null) SmartPartClosing(this, e);
        }

        /// <summary>
        /// Converts a smart part information to a compatible one for the workspace.
        /// </summary>
        protected virtual XtraNavBarGroupSmartPartInfo OnConvertFrom(ISmartPartInfo source)
        {
            XtraNavBarGroupSmartPartInfo spi = SmartPartInfo.ConvertTo<XtraNavBarGroupSmartPartInfo>(source);
            if (source is XtraImageSmartPartInfo)
            {
                XtraImageSmartPartInfo imageSmartPartInfo = (XtraImageSmartPartInfo) source;
                spi.SmallImage = imageSmartPartInfo.SmallImage;
                spi.LargeImage = imageSmartPartInfo.LargeImage;
            }

            return spi;
        }

        #endregion

        #region IComposableWorkspace<Control,XtraNavBarGroupSmartPartInfo> Members

        /// <summary>
        /// See <see cref="IComposableWorkspace{TSmartPart, TSmartPartInfo}.OnActivate"/> for more information.
        /// </summary>
        void IComposableWorkspace<Control, XtraNavBarGroupSmartPartInfo>.OnActivate(Control smartPart)
        {
            OnActivate(smartPart);
        }

        /// <summary>
        /// See <see cref="IComposableWorkspace{TSmartPart, TSmartPartInfo}.OnApplySmartPartInfo"/> for more information.
        /// </summary>
        void IComposableWorkspace<Control, XtraNavBarGroupSmartPartInfo>.OnApplySmartPartInfo(Control smartPart, XtraNavBarGroupSmartPartInfo smartPartInfo)
        {
            OnApplySmartPartInfo(smartPart, smartPartInfo);
        }

        /// <summary>
        /// See <see cref="IComposableWorkspace{TSmartPart, TSmartPartInfo}.OnShow"/> for more information.
        /// </summary>
        void IComposableWorkspace<Control, XtraNavBarGroupSmartPartInfo>.OnShow(Control smartPart, XtraNavBarGroupSmartPartInfo smartPartInfo)
        {
            OnShow(smartPart, smartPartInfo);
        }

        /// <summary>
        /// See <see cref="IComposableWorkspace{TSmartPart, TSmartPartInfo}.OnHide"/> for more information.
        /// </summary>
        void IComposableWorkspace<Control, XtraNavBarGroupSmartPartInfo>.OnHide(Control smartPart)
        {
            OnHide(smartPart);
        }

        /// <summary>
        /// See <see cref="IComposableWorkspace{TSmartPart, TSmartPartInfo}.OnClose"/> for more information.
        /// </summary>
        void IComposableWorkspace<Control, XtraNavBarGroupSmartPartInfo>.OnClose(Control smartPart)
        {
            OnClose(smartPart);
        }

        /// <summary>
        /// See <see cref="IComposableWorkspace{TSmartPart, TSmartPartInfo}.RaiseSmartPartActivated"/> for more information.
        /// </summary>
        void IComposableWorkspace<Control, XtraNavBarGroupSmartPartInfo>.RaiseSmartPartActivated(WorkspaceEventArgs e)
        {
            OnSmartPartActivated(e);
        }

        /// <summary>
        /// See <see cref="IComposableWorkspace{TSmartPart, TSmartPartInfo}.RaiseSmartPartClosing"/> for more information.
        /// </summary>
        void IComposableWorkspace<Control, XtraNavBarGroupSmartPartInfo>.RaiseSmartPartClosing(WorkspaceCancelEventArgs e)
        {
            OnSmartPartClosing(e);
        }

        /// <summary>
        /// See <see cref="IComposableWorkspace{TSmartPart, TSmartPartInfo}.ConvertFrom"/> for more information.
        /// </summary>
        XtraNavBarGroupSmartPartInfo IComposableWorkspace<Control, XtraNavBarGroupSmartPartInfo>.ConvertFrom(ISmartPartInfo source)
        {
            return OnConvertFrom(source);
        }

        #endregion

        #region  IWorkspace Members

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
            // Ensure DockStyle.fill for the ControlContainer smartPart
            ((Control) smartPart).Dock = DockStyle.Fill;
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

        #endregion

        #region Events

        /// <summary>
        /// See <see cref="IWorkspace.SmartPartClosing"/> for more information.
        /// </summary>
        public event EventHandler<WorkspaceCancelEventArgs> SmartPartClosing;

        /// <summary>
        /// See <see cref="IWorkspace.SmartPartActivated"/> for more information.
        /// </summary>
        public event EventHandler<WorkspaceEventArgs> SmartPartActivated;

        #endregion
    }
}