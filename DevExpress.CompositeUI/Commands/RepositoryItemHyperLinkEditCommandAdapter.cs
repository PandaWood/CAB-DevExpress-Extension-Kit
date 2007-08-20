using System.Collections.Generic;
using DevExpress.XtraEditors.Repository;
using Microsoft.Practices.CompositeUI.Commands;

namespace DevExpress.CompositeUI.Commands
{
	public class RepositoryItemHyperLinkEditCommandAdapter: EventCommandAdapter<RepositoryItemHyperLinkEdit>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new <see cref="RepositoryItemHyperLinkEditCommandAdapter"/>
		/// </summary>
		public RepositoryItemHyperLinkEditCommandAdapter()
		{ }

		/// <summary>
		/// Initializes  a new <see cref="RepositoryItemHyperLinkEditCommandAdapter"/> with the 
		/// given <see cref="RepositoryItemHyperLinkEdit"/>.
		/// </summary>
		public RepositoryItemHyperLinkEditCommandAdapter(RepositoryItemHyperLinkEdit item, string eventName)
			: base(item, eventName)
		{ }

		#endregion

		#region Overrides

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

		#endregion
	}
}