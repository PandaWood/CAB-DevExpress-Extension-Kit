using DevExpress.XtraBars.Navigation;
using Microsoft.Practices.CompositeUI.UIElements;
using Microsoft.Practices.CompositeUI.Utility;

namespace CABDevExpress.UIElements
{
    /// <summary>
    /// A <see cref="IUIElementAdapterFactory"/> that produces adapters for XtraAccordionControlUIAdapterFactory-related UI Elements.
    /// </summary
    public class XtraAccordionControlUIAdapterFactory : IUIElementAdapterFactory
    {
        /// <summary>
        /// Returns a <see cref="IUIElementAdapter"/> for the specified uielement.
        /// </summary>
        /// <param name="uiElement">uiElement for which to return a <see cref="IUIElementAdapter"/></param>
        /// <returns>A <see cref="IUIElementAdapter"/> that represents the specified element</returns>
        public IUIElementAdapter GetAdapter(object uiElement)
        {
            Guard.ArgumentNotNull(uiElement, "uiElement");

            if (uiElement is AccordionControl)
                return new AccordionControlGroupCollectionUIAdapter(((AccordionControl)uiElement).Elements);

            if (uiElement is AccordionControlElement && ((AccordionControlElement)uiElement).Style == ElementStyle.Group)
                return new AccordionControlElementCollectionUIAdapter(((AccordionControlElement)uiElement).Elements);

            throw ExceptionFactory.CreateInvalidAdapterElementType(uiElement.GetType(), GetType());
        }

        /// <summary>
        /// Indicates if the specified ui element is supported by the adapter factory.
        /// </summary>
        /// <param name="uiElement">UI Element to evaluate</param>
        /// <returns>Returns true for supported elements, otherwise returns false.</returns>
        //public bool Supports(object uiElement)
        //{
        //	return uiElement is AccordionControl || uiElement is NavBarGroup;
        //}
        //20161004
        public bool Supports(object uiElement)
        {
            return uiElement is AccordionControl || uiElement is AccordionControlElement;
        }
    }
}