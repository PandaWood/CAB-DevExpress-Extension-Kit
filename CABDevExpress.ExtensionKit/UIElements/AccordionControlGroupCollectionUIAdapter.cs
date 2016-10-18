using DevExpress.XtraBars.Navigation;
using Microsoft.Practices.CompositeUI.UIElements;
using Microsoft.Practices.CompositeUI.Utility;

namespace CABDevExpress.UIElements
{
    /// <summary>
    /// An adapter that wraps a <see cref="AccordionControlElementCollection"/> for use as an <see cref="IUIElementAdapter"/>.
    /// </summary>
    public class AccordionControlGroupCollectionUIAdapter : UIElementAdapter<AccordionControlElement>
    {
        private readonly AccordionControlElementCollection navGroupCollection;

        /// <summary>
        /// Initializes a new instance of the <see cref="AccordionControlGroupCollectionUIAdapter"/> class.
        /// </summary>
        /// <param name="collection"></param>
        public AccordionControlGroupCollectionUIAdapter(AccordionControlElementCollection collection)
        {
            Guard.ArgumentNotNull(collection, "AccordionControlElementCollection");
            navGroupCollection = collection;
        }

        /// <summary>
        /// See <see cref="UIElementAdapter{TUIElement}.Add(TUIElement)"/> for more information.
        /// </summary>
        protected override AccordionControlElement Add(AccordionControlElement uiElement)
        {
            Guard.ArgumentNotNull(uiElement, "AccordionControlElementCollection");
            navGroupCollection.Insert(GetInsertingIndex(uiElement), uiElement);
            return uiElement;
        }

        /// <summary>
        /// See <see cref="UIElementAdapter{TUIElement}.Remove(TUIElement)"/> for more information.
        /// </summary>
        protected override void Remove(AccordionControlElement uiElement)
        {
            Guard.ArgumentNotNull(uiElement, "AccordionControlElementCollection");

            if (uiElement.AccordionControl != null)
                navGroupCollection.Remove(uiElement);
        }

        /// <summary>
        /// When overridden in a derived class, returns the correct index for the item being added. By default,
        /// it will return the length of the navGroupCollection.
        /// </summary>
        /// <param name="uiElement"></param>
        /// <returns></returns>
        protected virtual int GetInsertingIndex(object uiElement)
        {
            return navGroupCollection.Count;
        }
    }
}