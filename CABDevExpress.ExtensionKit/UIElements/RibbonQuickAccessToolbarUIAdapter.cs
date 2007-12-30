using System;
using Microsoft.Practices.CompositeUI.UIElements;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using Microsoft.Practices.CompositeUI.Utility;

namespace CABDevExpress.UIElements
{
    public class RibbonQuickAccessToolbarUIAdapter : UIElementAdapter<BarItem>
    {
        private readonly RibbonQuickAccessToolbar ribbonQuickAccessToolbar;

        public RibbonQuickAccessToolbarUIAdapter(RibbonQuickAccessToolbar ribbonQuickAccessToolbar)
        {
            Guard.ArgumentNotNull(ribbonQuickAccessToolbar, "ribbonQuickAccessToolbar");
            this.ribbonQuickAccessToolbar = ribbonQuickAccessToolbar;
        }

        protected override BarItem Add(BarItem uiElement)
        {
            Guard.ArgumentNotNull(uiElement, "uiElement");

            if (ribbonQuickAccessToolbar == null)
                throw new InvalidOperationException();

            ribbonQuickAccessToolbar.ItemLinks.Add(uiElement);
            return uiElement;
        }

        protected override void Remove(BarItem uiElement)
        {
            Guard.ArgumentNotNull(uiElement, "uiElement");

            if (ribbonQuickAccessToolbar == null)
                throw new InvalidOperationException();

            ribbonQuickAccessToolbar.ItemLinks.Remove(uiElement);
        }
    }
}
