using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.CompositeUI.SmartPartInfos;
using DevExpress.XtraEditors;
using Microsoft.Practices.CompositeUI.SmartParts;
using Microsoft.Practices.CompositeUI.Utility;

namespace DevExpress.CompositeUI.Workspaces
{
    /// <summary>
    /// Implements a Workspace that shows smartparts in windows.
    /// </summary>
    public class XtraWindowWorkspace : Workspace<Control, XtraWindowSmartPartInfo>
    {
        private Dictionary<Control, XtraForm> windowDictionary = new Dictionary<Control, XtraForm>();
        private bool fireActivatedFromForm = true;
        IWin32Window ownerForm;

        /// <summary>
        /// Initializes the workspace with a no-owner form to use to show a new windows
        /// </summary>
        public XtraWindowWorkspace() { }

        /// <summary>
        /// Initializes the workspace with the form to use as the owner of 
        /// all windows shown through the workspace.
        /// </summary>
        /// <param name="ownerForm">The owner of windows shown through the workspace</param>
        public XtraWindowWorkspace(IWin32Window ownerForm)
        {
            this.ownerForm = ownerForm;
        }

        /// <summary>
        /// Read-only view of WindowDictionary.
        /// </summary>
        [Browsable(false)]
        public ReadOnlyDictionary<Control, XtraForm> Windows
        {
            get { return new ReadOnlyDictionary<Control, XtraForm>(windowDictionary); }
        }

        #region Protected

        /// <summary>
        /// Creates a form if it does not already exist and adds the given control.
        /// </summary>
        /// <param name="control"></param>
        /// <returns></returns>
        protected XtraForm GetOrCreateForm(Control control)
        {
            XtraWindowForm form;
            if (windowDictionary.ContainsKey(control))
            {
                form = (XtraWindowForm)windowDictionary[control];
            }
            else
            {
                form = new XtraWindowForm();
                windowDictionary.Add(control, form);
                form.Controls.Add(control);
                CalculateSize(control, form);
                control.Disposed += ControlDisposed;
                WireUpForm(form);
            }

            return form;
        }

        /// <summary>
        /// Sets specific properties for the given form.
        /// </summary>
        protected static void SetWindowProperties(XtraForm form, XtraWindowSmartPartInfo info)
        {
            form.Text = info.Title;
            form.Width = info.Width != 0 ? info.Width : form.Width;
            form.Height = info.Height != 0 ? info.Height : form.Height;
            form.ControlBox = info.ControlBox;
            form.MaximizeBox = info.MaximizeBox;
            form.MinimizeBox = info.MinimizeBox;
            form.Icon = info.Icon;

            form.ShowInTaskbar = info.ShowInTaskbar;
            form.StartPosition = info.StartPosition;
        }

        /// <summary>
        /// Sets the location information for the given form, providing that CenterParent
        /// is not the StartPosition
        /// </summary>
        protected static void SetWindowLocation(XtraForm form, XtraWindowSmartPartInfo info)
        {
            // Without this guard condition, if a centered form has ApplySmartPartInfo()
            // called on it, it will suddenly go to the top-left of the screen (Location 0,0)
            // as that's the default for the Location property but is not where the form will 
            // be if it's FormStartPosition.CenterParent position is set
            if (info.StartPosition != FormStartPosition.CenterParent)
                form.Location = info.Location;
        }

        #endregion

        #region Private

        private void ControlDisposed(object sender, EventArgs e)
        {
            Control control = sender as Control;
            if (control != null && SmartParts.Contains(sender))
            {
                CloseInternal(control);
                //this.windowDictionary[control].Close();
                //this.windowDictionary.Remove(control);
            }
        }

        private void WireUpForm(XtraWindowForm form)
        {
            form.WindowFormClosing += new EventHandler<WorkspaceCancelEventArgs>(WindowFormClosing);
            form.WindowFormClosed += new EventHandler<WorkspaceEventArgs>(WindowFormClosed);
            form.WindowFormActivated += new EventHandler<WorkspaceEventArgs>(WindowFormActivated);
        }

        private void WindowFormActivated(object sender, WorkspaceEventArgs e)
        {
            if (fireActivatedFromForm)
            {
                RaiseSmartPartActivated(e.SmartPart);
                SetActiveSmartPart(e.SmartPart);
            }
        }

        private void WindowFormClosed(object sender, WorkspaceEventArgs e)
        {
            RemoveEntry((Control)e.SmartPart);
            InnerSmartParts.Remove((Control)e.SmartPart);
        }

        private void WindowFormClosing(object sender, WorkspaceCancelEventArgs e)
        {
            RaiseSmartPartClosing(e);
        }

        private static void CalculateSize(Control smartPart, XtraForm form)
        {
            form.Size = new Size(smartPart.Size.Width, smartPart.Size.Height + 20);
        }

        private void RemoveEntry(Control spcontrol)
        {
            windowDictionary.Remove(spcontrol);
        }

        private void ShowForm(XtraForm form, XtraWindowSmartPartInfo smartPartInfo)
        {
            SetWindowProperties(form, smartPartInfo);

            if (smartPartInfo.Modal)
            {
                SetWindowLocation(form, smartPartInfo);
                // Argument can be null. It's the default for the other overload.
                form.ShowDialog(ownerForm);
            }
            else
            {
                // Call changes if no owner is specified.
                if (ownerForm != null)
                {
                    form.Show(ownerForm);
                }
                else
                {
                    form.Show();
                }
                SetWindowLocation(form, smartPartInfo);
                form.BringToFront();
            }
        }

        #endregion

        #region Private Form Class

        /// <summary>
        /// WindowForm class
        /// </summary>
        private class XtraWindowForm : XtraForm
        {
            /// <summary>
            /// Fires when form is closing
            /// </summary>
            public event EventHandler<WorkspaceCancelEventArgs> WindowFormClosing;

            /// <summary>
            /// Fires when form is closed
            /// </summary>
            public event EventHandler<WorkspaceEventArgs> WindowFormClosed;

            /// <summary>
            /// Fires when form is activated
            /// </summary>
            public event EventHandler<WorkspaceEventArgs> WindowFormActivated;

            /// <summary>
            /// Handles Activated Event.
            /// </summary>
            /// <param name="e"></param>
            protected override void OnActivated(EventArgs e)
            {
                if (Controls.Count > 0)
                {
                    WindowFormActivated(this, new WorkspaceEventArgs(Controls[0]));
                }

                base.OnActivated(e);
            }


            /// <summary>
            /// Handles the Closing Event
            /// </summary>
            /// <param name="e"></param>
            protected override void OnClosing(CancelEventArgs e)
            {
                if (Controls.Count > 0)
                {
                    WorkspaceCancelEventArgs cancelArgs = FireWindowFormClosing(Controls[0]);
                    e.Cancel = cancelArgs.Cancel;

                    if (cancelArgs.Cancel == false)
                    {
                        Controls[0].Hide();
                    }
                }

                base.OnClosing(e);
            }

            /// <summary>
            /// Handles the Closed Event
            /// </summary>
            /// <param name="e"></param>
            protected override void OnClosed(EventArgs e)
            {
                if ((WindowFormClosed != null) &&
                    (Controls.Count > 0))
                {
                    WindowFormClosed(this, new WorkspaceEventArgs(Controls[0]));
                }

                base.OnClosed(e);
            }

            private WorkspaceCancelEventArgs FireWindowFormClosing(object smartPart)
            {
                WorkspaceCancelEventArgs cancelArgs = new WorkspaceCancelEventArgs(smartPart);

                if (WindowFormClosing != null)
                {
                    WindowFormClosing(this, cancelArgs);
                }

                return cancelArgs;
            }
        }

        #endregion

        #region Behavior overrides

        /// <summary>
        /// Shows the form for the smart part and brings it to the front.
        /// </summary>
        protected override void OnActivate(Control smartPart)
        {
            // Prevent double firing from composer Workspace class and from form.
            try
            {
                fireActivatedFromForm = false;
                Form form = windowDictionary[smartPart];
                form.BringToFront();
                form.Show();
            }
            finally
            {
                fireActivatedFromForm = true;
            }
        }

        /// <summary>
        /// Hides the form where the smart part is being shown.
        /// </summary>
        protected override void OnHide(Control smartPart)
        {
            XtraForm form = windowDictionary[smartPart];
            form.Hide();
        }

        /// <summary>
        /// Closes the form where the smart part is being shown.
        /// </summary>
        protected override void OnClose(Control smartPart)
        {
            XtraForm form = windowDictionary[smartPart];
            smartPart.Disposed -= ControlDisposed;

            // Remove the smartPart from the form to avoid disposing it.
            form.Controls.Remove(smartPart);

            form.Close();
            windowDictionary.Remove(smartPart);
        }

        #endregion

        /// <summary>
        /// When overridden in a derived class, applies the smartPartInfo
        /// to the smartPart that lives in the workspace.
        /// </summary>
        protected override void OnApplySmartPartInfo(Control smartPart, XtraWindowSmartPartInfo smartPartInfo)
        {
            XtraForm form = windowDictionary[smartPart];
            SetWindowProperties(form, smartPartInfo);
            SetWindowLocation(form, smartPartInfo);
        }

        /// <summary>
        /// When overridden in a derived class, shows the smartPart  on the workspace.
        /// </summary>
        /// <param name="smartPart">The smart part to show.</param>
        /// <param name="smartPartInfo">The information to apply to the smart part.</param>
        protected override void OnShow(Control smartPart, XtraWindowSmartPartInfo smartPartInfo)
        {
            XtraForm form = GetOrCreateForm(smartPart);
            smartPart.Show();
            ShowForm(form, smartPartInfo);
        }
    }
}
