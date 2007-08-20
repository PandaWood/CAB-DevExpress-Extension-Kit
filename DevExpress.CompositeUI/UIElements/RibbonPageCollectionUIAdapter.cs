using System;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraNavBar;
using Microsoft.Practices.CompositeUI.UIElements;
using Microsoft.Practices.CompositeUI.Utility;

namespace DevExpress.CompositeUI.UIElements
{
    /// <summary>
    /// An adapter that wraps a <see cref="NavGroupCollection"/> for use as an <see cref="IUIElementAdapter"/>.
    /// </summary>
    public class RibbonPageCollectionUIAdapter : UIElementAdapter<RibbonPage>
    {
        private RibbonPageCollection collection;

        /// <summary>
        /// Initializes a new instance of the <see cref="NavBarGroupCollectionUIAdapter"/> class.
        /// </summary>
        /// <param name="collection"></param>
        public RibbonPageCollectionUIAdapter(RibbonPageCollection collection)
        {
            Guard.ArgumentNotNull(collection, "Ribbon Collection");
            this.collection = collection;
        }

        /// <summary>
        /// See <see cref="UIElementAdapter{TUIElement}.Add(TUIElement)"/> for more information.
        /// </summary>
        protected override RibbonPage Add(RibbonPage uiElement)
        {
            if (collection == null)
                throw new InvalidOperationException();

            collection.Insert(GetInsertingIndex(uiElement), uiElement);
            return uiElement;
        }

        /// <summary>
        /// See <see cref="UIElementAdapter{TUIElement}.Remove(TUIElement)"/> for more information.
        /// </summary>
        protected override void Remove(RibbonPage uiElement)
        {
            if (uiElement.Ribbon != null)
                uiElement.Ribbon.Pages.Remove(uiElement);
        }

        /// <summary>
        /// When overridden in a derived class, returns the correct index for the item being added. By default,
        /// it will return the length of the collection.
        /// </summary>
        /// <param name="uiElement"></param>
        /// <returns></returns>
        protected virtual int GetInsertingIndex(object uiElement)
        {
            return collection.Count;
        }

        /// <summary>
        /// Returns the internal collection mananged by the <see cref="NavBarGroupCollectionUIAdapter"/>
        /// </summary>
        protected RibbonPageCollection InternalCollection
        {
            get { return collection; }
            set { collection = value; }
        }
    }
}