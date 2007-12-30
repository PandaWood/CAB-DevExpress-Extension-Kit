using System.Collections.Generic;
using DevExpress.XtraNavBar;
using Microsoft.Practices.CompositeUI.Commands;

namespace CABDevExpress.Commands
{
    /// <summary>
    /// An <see cref="EventCommandAdapter{TInvoker}"/> that updates a <see cref="NavBarItem"/> based on the changes to 
    /// the <see cref="Command.Status"/> property value.
    /// </summary>
    public class NavBarItemCommandAdapter : EventCommandAdapter<NavBarItem>
    {
        /// <summary>
        /// Initializes a new <see cref="NavBarItemCommandAdapter"/>
        /// </summary>
        public NavBarItemCommandAdapter() { }

        /// <summary>
        /// Initializes  a new <see cref="NavBarItemCommandAdapter"/> with the given <see cref="NavBarItem"/>.
        /// </summary>
        public NavBarItemCommandAdapter(NavBarItem item, string eventName) : base(item, eventName) { }

        /// <summary>
        /// Handles the changes in the <see cref="Command"/> by refreshing 
        /// the <see cref="NavBarItem.Enabled"/> property.
        /// </summary>
        protected override void OnCommandChanged(Command command)
        {
            base.OnCommandChanged(command);

            foreach (KeyValuePair<NavBarItem, List<string>> pair in Invokers)
            {
                pair.Key.Enabled = (command.Status == CommandStatus.Enabled);
                pair.Key.Visible = (command.Status != CommandStatus.Unavailable);
            }
        }
    }
}