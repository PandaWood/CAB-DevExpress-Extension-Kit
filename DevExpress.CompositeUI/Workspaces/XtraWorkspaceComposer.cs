using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Microsoft.Practices.CompositeUI.SmartParts;
using Microsoft.Practices.CompositeUI.Utility;

namespace DevExpress.CompositeUI
{
    /// <summary>
    /// Helper class for dealing with a composable workspace where the smart parts are controls.
    /// </summary>
    /// <typeparam name="TSmartPartInfo">Type of the smart part info for the workspace</typeparam>
    /// <typeparam name="TWorkspaceItem">Type of the item that represents the smart parts in the workspace.</typeparam>
    internal class XtraWorkspaceComposer<TWorkspaceItem, TSmartPartInfo> : WorkspaceComposer<Control, TSmartPartInfo>
        where TWorkspaceItem : class
        where TSmartPartInfo : ISmartPartInfo, new()
    {
        #region Private Members

        private Dictionary<Control, TWorkspaceItem> smartParts = new Dictionary<Control, TWorkspaceItem>();
        private Dictionary<TWorkspaceItem, Control> items = new Dictionary<TWorkspaceItem, Control>();

        private Control smartPartBeingActivated = null;
        private bool hookControlEnter = false;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new <see cref="XtraWorkspaceComposer"/>
        /// </summary>
        /// <param name="composedWorkspace">ComposableWorkspace that will be delegated to.</param>
        /// <param name="activateSmartPartOnControlEnter">Boolean indicating whether to automatically activate the smart part when the control's Enter event is invoked.</param>
        public XtraWorkspaceComposer(IComposableWorkspace<Control, TSmartPartInfo> composedWorkspace,
                                     bool activateSmartPartOnControlEnter)
            : base(composedWorkspace)
        {
            hookControlEnter = activateSmartPartOnControlEnter;
        }

        #endregion

        #region Public Properties

        public TWorkspaceItem this[Control smartPart]
        {
            get
            {
                TWorkspaceItem item;
                TryGetItem(smartPart, out item);
                return item;
            }
        }

        public Control this[TWorkspaceItem item]
        {
            get
            {
                Control smartPart;
                TryGetSmartPart(item, out smartPart);
                return smartPart;
            }
        }

        #endregion

        #region Public Methods

        public void Add(TWorkspaceItem item, Control smartPart)
        {
            Guard.ArgumentNotNull(item, "item");
            Guard.ArgumentNotNull(smartPart, "smartPart");

            if (smartPart.IsDisposed)
                throw new InvalidOperationException();

            items.Add(item, smartPart);
            smartParts.Add(smartPart, item);

            smartPart.Disposed += new EventHandler(OnSmartPartControlDisposed);

            if (hookControlEnter)
                smartPart.Enter += new EventHandler(OnSmartPartControlEnter);
        }

        public bool ContainsItem(TWorkspaceItem item)
        {
            return items.ContainsKey(item);
        }

        public bool ContainsSmartPart(Control smartPart)
        {
            return smartParts.ContainsKey(smartPart);
        }

        public void Remove(TWorkspaceItem item, Control smartPart)
        {
            Guard.ArgumentNotNull(item, "item");
            Guard.ArgumentNotNull(smartPart, "smartPart");

            if (items[item] != smartPart ||
                smartParts[smartPart] != item)
                throw new InvalidOperationException();

            items.Remove(item);
            smartParts.Remove(smartPart);

            smartPart.Disposed -= new EventHandler(OnSmartPartControlDisposed);

            if (hookControlEnter)
                smartPart.Enter -= new EventHandler(OnSmartPartControlEnter);
        }

        public bool TryGetItem(Control smartPart, out TWorkspaceItem item)
        {
            return smartParts.TryGetValue(smartPart, out item);
        }

        public bool TryGetSmartPart(TWorkspaceItem item, out Control smartPart)
        {
            return items.TryGetValue(item, out smartPart);
        }

        public void VerifyActiveItem(TWorkspaceItem item)
        {
            Control smartPart = null != item ? this[item] : null;
            VerifyActiveSmartPart(smartPart);
        }

        public void VerifyActiveSmartPart(Control smartPart)
        {
            if (ActiveSmartPart != smartPart)
            {
                SetActiveSmartPart(smartPart);
                RaiseSmartPartActivated(smartPart);
            }
        }

        #endregion

        #region Private Methods

        private void OnSmartPartControlDisposed(object sender, EventArgs e)
        {
            Control ctrl = sender as Control;
            if (ContainsSmartPart(ctrl))
                ForceClose(ctrl);
        }

        private void OnSmartPartControlEnter(object sender, EventArgs e)
        {
            VerifyActiveSmartPart(sender as Control);
        }

        #endregion

        #region Overrides

        /// <summary>
        /// Calls <see cref="IComposableWorkspace{TSmartPart, TSmartPartInfo}.OnActivate"/> 
        /// on the composed workspace.
        /// </summary>
        protected override void OnActivate(Control smartPart)
        {
            if (smartPart == smartPartBeingActivated)
                return;

            Control previousSmartPart = smartPartBeingActivated;

            try
            {
                smartPartBeingActivated = smartPart;
                base.OnActivate(smartPart);
            }
            finally
            {
                smartPartBeingActivated = previousSmartPart;
            }
        }

        #endregion
    }
}