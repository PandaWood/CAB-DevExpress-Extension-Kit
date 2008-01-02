using System.Collections.Generic;
using DevExpress.XtraEditors.Repository;
using Microsoft.Practices.CompositeUI.Commands;

namespace CABDevExpress.Commands
{
	/// <summary>
	/// An <see cref="EventCommandAdapter{TInvoker}"/> that updates a <see cref="RepositoryItemHyperLinkEdit"/> based on the changes to 
	/// the <see cref="Command.Status"/> property value.
	/// </summary>
	public class RepositoryItemHyperLinkEditCommandAdapter: EventCommandAdapter<RepositoryItemHyperLinkEdit>
	{
		/// <summary>
		/// Initializes a new <see cref="RepositoryItemHyperLinkEditCommandAdapter"/>
		/// </summary>
		public RepositoryItemHyperLinkEditCommandAdapter() { }

		/// <summary>
		/// Initializes  a new <see cref="RepositoryItemHyperLinkEditCommandAdapter"/> with the 
		/// given <see cref="RepositoryItemHyperLinkEdit"/>.
		/// </summary>
		public RepositoryItemHyperLinkEditCommandAdapter(RepositoryItemHyperLinkEdit item, string eventName) : base(item, eventName) { }

		/// <summary>
		/// Handles the changes in the <see cref="Command"/> by refreshing 
		/// the <see cref="RepositoryItemHyperLinkEdit.Enabled"/> property.
		/// </summary>
		protected override void OnCommandChanged(Command command)
		{
			base.OnCommandChanged(command);

			foreach (KeyValuePair<RepositoryItemHyperLinkEdit, List<string>> pair in Invokers)
			{
				pair.Key.Enabled = (command.Status == CommandStatus.Enabled);
                pair.Key.ReadOnly = (command.Status == CommandStatus.Unavailable);
			}
		}
	}
}