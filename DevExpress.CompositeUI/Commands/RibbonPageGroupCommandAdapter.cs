using System.Collections.Generic;
using DevExpress.Utils.Menu;
using Microsoft.Practices.CompositeUI.Commands;
using DevExpress.XtraBars.Ribbon;

namespace DevExpress.CompositeUI.Commands
{
    /// <summary>
    /// An <see cref="EventCommandAdapter{TInvoker}"/> that updates a 
    /// <see cref="DXMenuItem"/> based on the changes to the <see cref="Command.Status"/> property value.
    /// </summary>
    public class RibbonPageGroupCommandAdapter : EventCommandAdapter<RibbonPageGroup>
    {
        #region Constructors

        /// <summary>
        /// Initializes a new <see cref="RibbonPageGroupCommandAdapter"/>
        /// </summary>
        public RibbonPageGroupCommandAdapter()
        { }

        /// <summary>
        /// Initializes  a new <see cref="RibbonPageGroupCommandAdapter"/> with the 
        /// given <see cref="DXMenuItem"/>.
        /// </summary>
        public RibbonPageGroupCommandAdapter(RibbonPageGroup item, string eventName)
            : base(item, eventName)
        { }

        #endregion

        #region Overrides

        ///// <summary>
        ///// 
        ///// </summary>
        //protected override void OnCommandChanged(Command command)
        //{
        //    base.OnCommandChanged(command);

        //    //foreach (KeyValuePair<RibbonPageGroup, List<string>> pair in Invokers)
        //    //{
        //    //    pair.Key.Enabled = (command.Status == CommandStatus.Enabled);
        //    //    pair.Key.Visible = (command.Status != CommandStatus.Unavailable);
        //    //}
        //}

        #endregion
    }
}
