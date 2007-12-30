using System.Collections.Generic;
using DevExpress.Utils.Menu;
using Microsoft.Practices.CompositeUI.Commands;

namespace CABDevExpress.Commands
{
    /// <summary>
    /// An <see cref="EventCommandAdapter{TInvoker}"/> that updates a 
    /// <see cref="DXMenuItem"/> based on the changes to the <see cref="Command.Status"/> property value.
    /// </summary>
    public class DXMenuItemCommandAdapter : EventCommandAdapter<DXMenuItem>
    {
        /// <summary>
        /// Initializes a new <see cref="DXMenuItemCommandAdapter"/>
        /// </summary>
        public DXMenuItemCommandAdapter() { }

        /// <summary>
        /// Initializes  a new <see cref="DXMenuItemCommandAdapter"/> with the 
        /// given <see cref="DXMenuItem"/>.
        /// </summary>
        public DXMenuItemCommandAdapter(DXMenuItem item, string eventName) : base(item, eventName) { }

        /// <summary>
        /// Handles the changes in the <see cref="Command"/> by refreshing 
        /// the <see cref="DXMenuItem.Enabled"/> property.
        /// </summary>
        protected override void OnCommandChanged(Command command)
        {
            base.OnCommandChanged(command);

            foreach (KeyValuePair<DXMenuItem, List<string>> pair in Invokers)
            {
                pair.Key.Enabled = (command.Status == CommandStatus.Enabled);
                pair.Key.Visible = (command.Status != CommandStatus.Unavailable);
            }
        }
    }
}
