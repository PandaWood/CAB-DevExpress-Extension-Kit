using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.XtraBars;

namespace DevExpress.CompositeUI.UIElements
{
    public interface IUIBarItemLinksAdapter
    {
        BarItemLinkCollection InternalCollection { get; }
    }
}