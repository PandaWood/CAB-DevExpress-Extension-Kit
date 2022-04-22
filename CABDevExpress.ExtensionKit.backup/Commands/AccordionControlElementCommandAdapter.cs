using System.Collections.Generic;
using DevExpress.XtraBars.Navigation;
using Microsoft.Practices.CompositeUI.Commands;

namespace CABDevExpress.Commands
{
    /// <summary>
    /// An <see cref="EventCommandAdapter{TInvoker}"/> that updates a <see cref="NavBarItem"/> based on the changes to 
    /// the <see cref="Command.Status"/> property value.
    /// </summary>
    public class AccordionControlElementCommandAdapter : EventCommandAdapter<AccordionControlElement>
    {
        /// <summary>
        /// Initializes a new <see cref="AccordionControlElementCommandAdapter"/>
        /// </summary>
        public AccordionControlElementCommandAdapter() { }

        /// <summary>
        /// Initializes  a new <see cref="AccordionControlElementCommandAdapter"/> with the given <see cref="NavBarItem"/>.
        /// </summary>
        public AccordionControlElementCommandAdapter(AccordionControlElement item, string eventName) : base(item, eventName) { }

        /// <summary>
        /// Handles the changes in the <see cref="Command"/> by refreshing 
        /// the <see cref="AccordionControlElement.Enabled"/> property.
        /// </summary>
        protected override void OnCommandChanged(Command command)
        {
            base.OnCommandChanged(command);

            foreach (KeyValuePair<AccordionControlElement, List<string>> pair in Invokers)
            {
                pair.Key.Enabled = (command.Status == CommandStatus.Enabled);
                pair.Key.Visible = (command.Status != CommandStatus.Unavailable);
            }
        }
    }
}