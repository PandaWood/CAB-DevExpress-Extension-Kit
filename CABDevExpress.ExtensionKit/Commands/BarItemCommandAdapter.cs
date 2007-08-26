using System.Collections.Generic;
using DevExpress.XtraBars;
using Microsoft.Practices.CompositeUI.Commands;

namespace CABDevExpress.Commands
{
    /// <summary>
    /// An <see cref="EventCommandAdapter{TInvoker}"/> that updates a <see cref="BarItem"/> based on the changes to 
    /// the <see cref="Command.Status"/> property value.
    /// </summary>
    public class BarItemCommandAdapter : EventCommandAdapter<BarItem>
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="BarItemCommandAdapter"/> class
        /// </summary>
        public BarItemCommandAdapter()
        {
        }

        /// <summary>
        /// Initializes the adapter with the given <see cref="BarItem"/>.
        /// </summary>
        public BarItemCommandAdapter(BarItem item, string eventName)
            : base(item, eventName)
        {
        }

        #endregion

        #region Overrides

        /// <summary>
        /// Handles the changes in the <see cref="Command"/> by refreshing 
        /// the <see cref="BarItem.Enabled"/> property.
        /// </summary>
        protected override void OnCommandChanged(Command command)
        {
            base.OnCommandChanged(command);

            foreach (KeyValuePair<BarItem, List<string>> pair in Invokers)
            {
                pair.Key.Enabled = (command.Status == CommandStatus.Enabled);
                pair.Key.Visibility = (command.Status != CommandStatus.Unavailable)
                                          ? BarItemVisibility.Always
                                          : BarItemVisibility.Never;
            }
        }

        #endregion
    }
}