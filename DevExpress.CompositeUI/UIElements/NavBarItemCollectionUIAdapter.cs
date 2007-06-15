using System;
using DevExpress.XtraNavBar;
using Microsoft.Practices.CompositeUI.UIElements;
using Microsoft.Practices.CompositeUI.Utility;

namespace DevExpress.CompositeUI.UIElements
{
    /// <summary>
    /// An adapter that wraps a <see cref="NavItemCollection"/> for use as an <see cref="IUIElementAdapter"/>.
    /// </summary>
    public class NavBarItemCollectionUIAdapter : UIElementAdapter<NavBarItem>
    {
        private NavItemCollection collection;
        private NavLinkCollection linkCollection;

        /// <summary>
        /// Initializes a new instance of the <see cref="NavBarItemCollectionUIAdapter"/> class.
        /// </summary>
        /// <param name="linkCollection"></param>
        /// <param name="collection"></param>
        public NavBarItemCollectionUIAdapter(NavLinkCollection linkCollection, NavItemCollection collection)
        {
            Guard.ArgumentNotNull(linkCollection, "linkCollection");
            this.linkCollection = linkCollection;

            Guard.ArgumentNotNull(collection, "collection");
            this.collection = collection;
        }

        /// <summary>
        /// See <see cref="UIElementAdapter{TUIElement}.Add(TUIElement)"/> for more information.
        /// </summary>
        protected override NavBarItem Add(NavBarItem uiElement)
        {
            if (collection == null)
                throw new InvalidOperationException();
            if (linkCollection == null)
                throw new InvalidOperationException();

            collection.Insert(GetInsertingIndex(uiElement), uiElement);
            linkCollection.Add(uiElement);
            return uiElement;
        }

        /// <summary>
        /// See <see cref="UIElementAdapter{TUIElement}.Remove(TUIElement)"/> for more information.
        /// </summary>
        protected override void Remove(NavBarItem uiElement)
        {
            if (uiElement.NavBar != null)
            {
                collection.Remove(uiElement);
                linkCollection.Remove(uiElement);
                //uiElement.NavBar.Items.Remove(uiElement);
                //uiElement.NavBar.GroItems.Remove(uiElement);
            }
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
        /// Returns the internal collection mananged by the <see cref="NavBarItemCollectionUIAdapter"/>
        /// </summary>
        protected NavItemCollection InternalCollection
        {
            get { return collection; }
            set { collection = value; }
        }
    }
}