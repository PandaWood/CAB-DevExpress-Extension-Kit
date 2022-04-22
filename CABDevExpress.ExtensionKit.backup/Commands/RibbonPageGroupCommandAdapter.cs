using DevExpress.Utils.Menu;
using DevExpress.XtraBars.Ribbon;
using Microsoft.Practices.CompositeUI.Commands;

namespace CABDevExpress.Commands
{
    /// <summary>
    /// An <see cref="EventCommandAdapter{TInvoker}"/> that updates a 
	/// <see cref="RibbonPageGroup"/> based on the changes to the <see cref="Command.Status"/> property value.
    /// </summary>
    public class RibbonPageGroupCommandAdapter : EventCommandAdapter<RibbonPageGroup>
    {
        /// <summary>
        /// Initializes a new <see cref="RibbonPageGroupCommandAdapter"/>
        /// </summary>
        public RibbonPageGroupCommandAdapter() { }

        /// <summary>
        /// Initializes  a new <see cref="RibbonPageGroupCommandAdapter"/> with the 
        /// given <see cref="DXMenuItem"/>.
        /// </summary>
        public RibbonPageGroupCommandAdapter(RibbonPageGroup item, string eventName): base(item, eventName)
        { }
    }
}
