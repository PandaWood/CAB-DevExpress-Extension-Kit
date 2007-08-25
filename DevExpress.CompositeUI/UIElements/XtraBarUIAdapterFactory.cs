using DevExpress.XtraBars;
using Microsoft.Practices.CompositeUI.UIElements;
using Microsoft.Practices.CompositeUI.Utility;

namespace CABDevExpress.UIElements
{
    /// <summary>
    /// A <see cref="IUIElementAdapterFactory"/> that produces adapters for XtraBar-related UI Elements.
    /// </summary>
    public class XtraBarUIAdapterFactory : IUIElementAdapterFactory
    {
        /// <summary>
        /// Returns a <see cref="IUIElementAdapter"/> for the specified uielement.
        /// </summary>
        /// <param name="uiElement">uiElement for which to return a <see cref="IUIElementAdapter"/></param>
        /// <returns>A <see cref="IUIElementAdapter"/> that represents the specified element</returns>
        public IUIElementAdapter GetAdapter(object uiElement)
        {
            Guard.ArgumentNotNull(uiElement, "uiElement");

            if (uiElement is BarManager)
                return new BarsUIAdapter(((BarManager)uiElement).Bars);

            if (uiElement is Bar)
                return new BarLinksCollectionUIAdapter(((Bar)uiElement).ItemLinks, ((Bar)uiElement).Manager.Items);

            if (uiElement is BarSubItem)
                return
                    new BarLinksCollectionUIAdapter(((BarSubItem)uiElement).ItemLinks,
                                          ((BarSubItem)uiElement).Manager.Items);
            if (uiElement is BarItemWrapper)
                return
                    new BarLinksOwnerCollectionUIAdapter(((BarItemWrapper)uiElement).Item, ((BarItemWrapper)uiElement).ItemLinks);

            throw ExceptionFactory.CreateInvalidAdapterElementType(uiElement.GetType(), GetType());
        }

        /// <summary>
        /// Indicates if the specified ui element is supported by the adapter factory.
        /// </summary>
        /// <param name="uiElement">UI Element to evaluate</param>
        /// <returns>Returns true for supported elements, otherwise returns false.</returns>
        public bool Supports(object uiElement)
        {
            return uiElement is BarManager ||
                   uiElement is Bar ||
                   uiElement is BarSubItem ||
                   uiElement is BarItemWrapper;
        }
    }
}