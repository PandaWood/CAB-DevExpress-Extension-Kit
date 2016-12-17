using DevExpress.XtraBars.Navigation;
using Microsoft.Practices.CompositeUI.UIElements;
using Microsoft.Practices.CompositeUI.Utility;

namespace CABDevExpress.UIElements
{
    /// <summary>
    /// An adapter that wraps a <see cref="AccordionControlElementCollection"/> for use as an <see cref="IUIElementAdapter"/>.
    /// </summary>
    public class AccordionControlElementCollectionUIAdapter : UIElementAdapter<AccordionControlElement>
    {
        private readonly AccordionControlElementCollection itemCollection;

        /// <summary>
        /// Initializes a new instance of the <see cref="AccordionControlElementCollectionUIAdapter"/> class.
        /// </summary>
        /// <param name="navBarGroup"></param>
        public AccordionControlElementCollectionUIAdapter(AccordionControlElementCollection navBarGroup)
        {
            itemCollection = navBarGroup;
            // Do nothing (except chain constructor)
        }

        /// <summary>
        /// See <see cref="UIElementAdapter{TUIElement}.Add(TUIElement)"/> for more information.
        /// </summary>
        protected override AccordionControlElement Add(AccordionControlElement uiElement)
        {
            Guard.ArgumentNotNull(uiElement, "AccordionControlElement");

            itemCollection.Insert(GetInsertingIndex(uiElement), uiElement);
            return uiElement;
        }

        /// <summary>
        /// See <see cref="UIElementAdapter{TUIElement}.Remove(TUIElement)"/> for more information.
        /// </summary>
        protected override void Remove(AccordionControlElement uiElement)
        {
            Guard.ArgumentNotNull(uiElement, "AccordionControlElement");

            if (uiElement.AccordionControl != null)
            {
                itemCollection.Remove(uiElement);
            }
        }

        /// <summary>
        /// When overridden in a derived class, returns the correct index for the item being added. By default,
        /// it will return the length of the itemCollection.
        /// </summary>
        /// <param name="uiElement"></param>
        /// <returns></returns>
        protected virtual int GetInsertingIndex(AccordionControlElement uiElement)
        {
            //AccordionControlElement itemCollection[1];
            int iIndex = 0;
            for (iIndex = 0; iIndex < itemCollection.Count; iIndex++)
                if (string.Compare(uiElement.Text,itemCollection[iIndex].Text)<0)
                    break;
            return iIndex;
            //return itemCollection.Count;
        }
    }
}