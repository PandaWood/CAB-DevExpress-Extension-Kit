using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Utils;
using DevExpress.XtraBars.Ribbon;
using Microsoft.Practices.CompositeUI.UIElements;

namespace CABDevExpress.UIElements
{
    /// <summary>
    /// Ribbon Page Category UI Adapter
    /// </summary>
    public class RibbonPageCategoryCollectionUIAdapter : UIElementAdapter<RibbonPageCategory>
    {
        private readonly RibbonPageCategoryCollection ribbonPageCategoryCollection;

        public RibbonPageCategoryCollectionUIAdapter( RibbonPageCategoryCollection ribbonPageCategoryCollection)
        {
            Guard.ArgumentNotNull(ribbonPageCategoryCollection, "RibbonPageCategoryCollection");
            this.ribbonPageCategoryCollection = ribbonPageCategoryCollection;
        }
        
        protected override RibbonPageCategory Add(RibbonPageCategory uiElement)
        {
            Guard.ArgumentNotNull(uiElement, "RibbonPage");
            ribbonPageCategoryCollection.Insert(GetInsertingIndex(uiElement), uiElement);
            return uiElement;

        }

        protected override void Remove(RibbonPageCategory uiElement)
        {
            Guard.ArgumentNotNull(uiElement, "RibbonPageCategory");

            if (uiElement.Ribbon != null)
                ribbonPageCategoryCollection.Remove(uiElement);
        }

        /// <summary>
		/// When overridden in a derived class, returns the correct index for the item being added. By default,
		/// it will return the length of the ribbonPageCollection.
		/// </summary>
		/// <param name="uiElement"></param>
		/// <returns></returns>
		protected virtual int GetInsertingIndex(object uiElement)
        {
            return ribbonPageCategoryCollection.Count;
        }
    }
}
