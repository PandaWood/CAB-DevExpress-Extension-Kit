using System;
using DevExpress.XtraNavBar;
using Microsoft.Practices.CompositeUI.UIElements;
using Microsoft.Practices.CompositeUI.Utility;

namespace CABDevExpress.UIElements
{
    /// <summary>
    /// An adapter that wraps a <see cref="NavGroupCollection"/> for use as an <see cref="IUIElementAdapter"/>.
    /// </summary>
    public class NavBarGroupCollectionUIAdapter : UIElementAdapter<NavBarGroup>
    {
        private NavGroupCollection collection;

        /// <summary>
        /// Initializes a new instance of the <see cref="NavBarGroupCollectionUIAdapter"/> class.
        /// </summary>
        /// <param name="collection"></param>
        public NavBarGroupCollectionUIAdapter(NavGroupCollection collection)
        {
            Guard.ArgumentNotNull(collection, "collection");
            this.collection = collection;
        }

        /// <summary>
        /// See <see cref="UIElementAdapter{TUIElement}.Add(TUIElement)"/> for more information.
        /// </summary>
        protected override NavBarGroup Add(NavBarGroup uiElement)
        {
            if (collection == null)
                throw new InvalidOperationException();

            collection.Insert(GetInsertingIndex(uiElement), uiElement);
            return uiElement;
        }

        /// <summary>
        /// See <see cref="UIElementAdapter{TUIElement}.Remove(TUIElement)"/> for more information.
        /// </summary>
        protected override void Remove(NavBarGroup uiElement)
        {
            if (uiElement.NavBar != null)
                uiElement.NavBar.Groups.Remove(uiElement);
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
        protected NavGroupCollection InternalCollection
        {
            get { return collection; }
            set { collection = value; }
        }
    }
}