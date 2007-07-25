using System;
using DevExpress.XtraNavBar;
using Microsoft.Practices.CompositeUI.UIElements;
using Microsoft.Practices.CompositeUI.Utility;
using DevExpress.XtraEditors.Controls;

namespace DevExpress.CompositeUI.UIElements
{
    /// <summary>
    /// An adapter that wraps a <see cref="NavItemCollection"/> for use as an <see cref="IUIElementAdapter"/>.
    /// </summary>
    public class EditorButtonCollectionUIAdapter : UIElementAdapter<EditorButton>
    {
        private EditorButtonCollection collection;

        /// <summary>
        /// Initializes a new instance of the <see cref="NavBarItemCollectionUIAdapter"/> class.
        /// </summary>
        /// <param name="linkCollection"></param>
        /// <param name="collection"></param>
        public EditorButtonCollectionUIAdapter(EditorButtonCollection collection)
        {
            Guard.ArgumentNotNull(collection, "collection");
            this.collection = collection;
        }

        /// <summary>
        /// See <see cref="UIElementAdapter{TUIElement}.Add(TUIElement)"/> for more information.
        /// </summary>
        protected override EditorButton Add(EditorButton uiElement)
        {
            if (collection == null)
                throw new InvalidOperationException();
            
            collection.Add(uiElement);
            return uiElement;
        }

        /// <summary>
        /// See <see cref="UIElementAdapter{TUIElement}.Remove(TUIElement)"/> for more information.
        /// </summary>
        protected override void Remove(EditorButton uiElement)
        {
            int index = collection.IndexOf(uiElement);
            if (index > -1) 
                collection.RemoveAt(index);
                          
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
        protected EditorButtonCollection InternalCollection
        {
            get { return collection; }
            set { collection = value; }
        }
    }
}