using System;
using DevExpress.XtraNavBar;
using Microsoft.Practices.CompositeUI.UIElements;
using Microsoft.Practices.CompositeUI.Utility;
using DevExpress.XtraEditors;

namespace DevExpress.CompositeUI.UIElements
{
    /// <summary>
    /// An adapter that wraps a <see cref="NavItemCollection"/> for use as an <see cref="IUIElementAdapter"/>.
    /// </summary>
    public class NavigatorCustomButtonUIAdapter : UIElementAdapter<NavigatorCustomButton>
    {
        private NavigatorCustomButtons collection;

        /// <summary>
        /// Initializes a new instance of the <see cref="NavBarItemCollectionUIAdapter"/> class.
        /// </summary>
        /// <param name="collection"></param>
        public NavigatorCustomButtonUIAdapter(NavigatorCustomButtons collection)
        {
            Guard.ArgumentNotNull(collection, "collection");
            this.collection = collection;
        }

        /// <summary>
        /// See <see cref="UIElementAdapter{TUIElement}.Add(TUIElement)"/> for more information.
        /// </summary>
        protected override NavigatorCustomButton Add(NavigatorCustomButton uiElement)
        {
            if (collection == null)
                throw new InvalidOperationException();

            collection.AddRange(new DevExpress.XtraEditors.NavigatorCustomButton[] { uiElement });
            return uiElement;
        }

        /// <summary>
        /// See <see cref="UIElementAdapter{TUIElement}.Remove(TUIElement)"/> for more information.
        /// </summary>
        protected override void Remove(NavigatorCustomButton uiElement)
        {
            int index = -1;
            foreach (object o in collection)
            {
                index++;
                if (o == uiElement)
                    break;
            }

            if (index > -1)
            {
                collection.RemoveAt(index);
            }
            //should we raise an exception if we're instructed to remove an item that does not exist? 
        }

        /// <summary>
        /// Returns the correct index for the item being added. By default,
        /// it will return the length of the collection.
        /// </summary>
        /// <param name="uiElement"></param>
        /// <returns></returns>
        protected virtual int GetInsertingIndex(object uiElement)
        {
            return collection.Count;
        }

        /// <summary>
        /// Returns the internal collection mananged by the <see cref="NavBarItemCollectionUIAdapter"/>
        /// </summary>
        protected NavigatorCustomButtons InternalCollection
        {
            get { return collection; }
            set { collection = value; }
        }
    }
}