using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using CABDevExpress.SmartPartInfos;
using DevExpress.XtraEditors;
using Microsoft.Practices.CompositeUI.SmartParts;
using Microsoft.Practices.CompositeUI.Utility;

namespace CABDevExpress.Workspaces
{
    /// <summary>
    /// Implements a Workspace that shows smartparts in windows.
    /// <remarks>Uses DevExpress XtraForm to enable form-skinning</remarks>
    /// </summary>
    public class XtraWindowWorkspace : Workspace<Control, XtraWindowSmartPartInfo>, IDisposable
    {
        private readonly Dictionary<Control, XtraForm> windowDictionary = new Dictionary<Control, XtraForm>();
        private bool fireActivatedFromForm = true;
        protected readonly IWin32Window ownerForm;

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

        /// <summary>
        /// Creates a form if it does not already exist and adds the given control.
        /// </summary>
        /// <param name="control"></param>
        /// <returns></returns>
        protected Form GetOrCreateForm(Control control)
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
                // reversed the order of these following two lines for issue 11166
                CalculateSize(control, form);
                form.Controls.Add(control);
                control.Dock = DockStyle.Fill;
                control.Disposed += ControlDisposed;
                WireUpForm(form);
            }

            return form;
        }

        /// <summary>
        /// Sets specific properties for the given form.
        /// </summary>
        protected static void SetWindowProperties(Form form, XtraWindowSmartPartInfo info)
        {
            form.WindowState = info.WindowState;
            form.Text = info.Title;
            form.Width = info.Width != 0 ? info.Width : form.Width;
            form.Height = info.Height != 0 ? info.Height : form.Height;
            form.ControlBox = info.ControlBox;
            form.MaximizeBox = info.MaximizeBox;
            form.MinimizeBox = info.MinimizeBox;
            form.Icon = info.Icon;
            form.AcceptButton = info.AcceptButton;
            form.CancelButton = info.CancelButton;
            form.FormBorderStyle = info.FormBorderStyle;
            form.StartPosition = info.StartPosition;
            form.ShowInTaskbar = info.ShowInTaskbar;
        }

        /// <summary>
        /// Sets the location information for the given form, providing that CenterParent
        /// is not the StartPosition
        /// </summary>
        protected static void SetWindowLocation(Form form, XtraWindowSmartPartInfo info)
        {
            // Without this guard condition, if a centered form has ApplySmartPartInfo()
            // called on it, it will suddenly go to the top-left of the screen (Location 0,0)
            // as that's the default for the Location property but is not where the form will 
            // be if it's FormStartPosition.CenterParent position is set
            if (info.StartPosition != FormStartPosition.CenterParent)
                form.Location = info.Location;
        }

        private void ControlDisposed(object sender, EventArgs e)
        {
            Control control = sender as Control;
            if (control != null && SmartParts.Contains(sender))
            {
                CloseInternal(control);
            }
        }

        private void WireUpForm(XtraWindowForm form)
        {
            form.WindowFormClosing += WindowFormClosing;
            form.WindowFormClosed += WindowFormClosed;
            form.WindowFormActivated += WindowFormActivated;
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

        private static void CalculateSize(Control smartPart, Form form)
        {
            form.ClientSize = smartPart.Size;
        }

        private void RemoveEntry(Control spcontrol)
        {
            windowDictionary.Remove(spcontrol);
        }

        private void ShowForm(Form form, XtraWindowSmartPartInfo smartPartInfo)
        {
            SetWindowProperties(form, smartPartInfo);

            if (smartPartInfo.Modal)
            {
                SetWindowLocation(form, smartPartInfo); // Argument can be null. It's the default for the other overload.
                form.ShowDialog(ownerForm);
            }
            else
            {
                if (ownerForm != null) // Call changes if no owner is specified.
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
                    WindowFormActivated(this, new WorkspaceEventArgs(Controls[0]));

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

                    if (cancelArgs.Cancel == false && Controls.Count > 0)
                        Controls[0].Hide();
                }

                base.OnClosing(e);
            }

            /// <summary>
            /// Handles the Closed Event
            /// </summary>
            /// <param name="e"></param>
            protected override void OnClosed(EventArgs e)
            {
                if (WindowFormClosed != null && Controls.Count > 0)
                    WindowFormClosed(this, new WorkspaceEventArgs(Controls[0]));

                base.OnClosed(e);
            }

            private WorkspaceCancelEventArgs FireWindowFormClosing(object smartPart)
            {
                var cancelArgs = new WorkspaceCancelEventArgs(smartPart);

                if (WindowFormClosing != null)
                    WindowFormClosing(this, cancelArgs);

                return cancelArgs;
            }
        }

        /// <summary>
        /// Shows the form for the smart part and brings it to the front.
        /// </summary>
        protected override void OnActivate(Control smartPart)
        {
            try
            {
                fireActivatedFromForm = false;	// Prevent double firing from composer Workspace class and form
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
            Form form = windowDictionary[smartPart];
            form.Hide();
        }

        /// <summary>
        /// Closes the form where the smart part is being shown.
        /// </summary>
        protected override void OnClose(Control smartPart)
        {
            Form form = windowDictionary[smartPart];
            smartPart.Disposed -= ControlDisposed;
            if (form != null)
            {
                ((XtraWindowForm)form).WindowFormClosing -= WindowFormClosing;
                ((XtraWindowForm)form).WindowFormClosed -= WindowFormClosed;
                ((XtraWindowForm)form).WindowFormActivated -= WindowFormActivated;
                form.Controls.Remove(smartPart);	// Remove the smartPart from the form to avoid disposing it.
                form.Close();
                form.Dispose();
            }
            form = null;
            windowDictionary.Remove(smartPart);
        }

        /// <summary>
        /// When overridden in a derived class, applies the smartPartInfo
        /// to the smartPart that lives in the workspace.
        /// </summary>
        protected override void OnApplySmartPartInfo(Control smartPart, XtraWindowSmartPartInfo smartPartInfo)
        {
            Form form = windowDictionary[smartPart];
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
            Form form = GetOrCreateForm(smartPart);
            smartPart.Show();
            ShowForm(form, smartPartInfo);
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    if (windowDictionary != null)
                    {
                        foreach (Control ctrl in windowDictionary.Values)
                        {
                            ctrl.Disposed -= ControlDisposed;
                            XtraWindowForm form = (XtraWindowForm)windowDictionary[ctrl];
                            if (form != null)
                            {
                                form.WindowFormClosing -= WindowFormClosing;
                                form.WindowFormClosed -= WindowFormClosed;
                                form.WindowFormActivated -= WindowFormActivated;
                            }
                        }
                        windowDictionary.Clear();
                    }
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~XtraWindowWorkspace() {
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
