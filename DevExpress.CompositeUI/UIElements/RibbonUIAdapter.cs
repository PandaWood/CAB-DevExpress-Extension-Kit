using System;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraBars;

using Microsoft.Practices.CompositeUI.UIElements;
using Microsoft.Practices.CompositeUI.Utility;

namespace DevExpress.CompositeUI.UIElements
{
    /// <summary>
    /// An adapter that wraps a <see cref="Bars"/> for use as an <see cref="IUIElementAdapter"/>.
    /// </summary>
    public class RibbonUIAdapter : UIElementAdapter<RibbonControl>
    {
        private RibbonControl ribbonControl;

        /// <summary>
        /// Initializes a new instance of the <see cref="BarsUIAdapter"/> class.
        /// </summary>
		/// <param name="ribbonControl"></param>
        public RibbonUIAdapter(RibbonControl ribbonControl)
        {
			Guard.ArgumentNotNull(ribbonControl, "RibbonControl");
			this.ribbonControl = ribbonControl;
        }

        /// <summary>
        /// See <see cref="UIElementAdapter{TUIElement}.Add(TUIElement)"/> for more information.
        /// </summary>
        protected override RibbonControl Add(RibbonControl uiElement)
        {
            if (uiElement == null)
                throw new InvalidOperationException();

            ribbonControl = uiElement;
            return uiElement;
        }

        /// <summary>
        /// See <see cref="UIElementAdapter{TUIElement}.Remove(TUIElement)"/> for more information.
        /// </summary>
        protected override void Remove(RibbonControl uiElement)
        {
            ribbonControl = null;
            //int index = bars.IndexOf(uiElement); 
            //if (index  > -1)
            //    bars.RemoveAt(index);
        }

        /// <summary>
        /// When overridden in a derived class, returns the correct index for the item being added. By default,
        /// it will return the length of the bars.
        /// </summary>
        /// <param name="uiElement"></param>
        /// <returns></returns>
        protected virtual int GetInsertingIndex(object uiElement)
        {
            return 1; //bars.Count;
        }

        /// <summary>
        /// Returns the internal bars mananged by the <see cref="BarsUIAdapter"/>
        /// </summary>
        protected RibbonControl InternalCollection
        {
            get
            {
                return ribbonControl;
            }
            set { ribbonControl = value; }
        }
    }
}