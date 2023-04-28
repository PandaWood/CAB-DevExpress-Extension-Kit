using DevExpress.Utils;
using DevExpress.XtraBars.Docking2010.Views.Tabbed;
using DevExpress.XtraBars.Docking2010.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CABDevExpress.DocumentHost
{
    internal class TabbedViewToolTipClient : IToolTipControlClient
    {
        TabbedView viewCore;
        public TabbedViewToolTipClient(TabbedView view)
        {
            viewCore = view;
        }
        public TabbedView View
        {
            get { return viewCore; }
        }
        ToolTipControlInfo IToolTipControlClient.GetObjectInfo(System.Drawing.Point point)
        {
            ToolTipControlInfo info = new ToolTipControlInfo();
            BaseViewHitInfo hitInfo = View.Manager.CalcHitInfo(point);
            if (!hitInfo.IsEmpty)
            {
                IDocumentInfo documentInfo = hitInfo.HitElement as IDocumentInfo;
                if (documentInfo != null)
                {
                    Document document = documentInfo.Document;
                    SuperToolTip superToolTip = new SuperToolTip();
                    superToolTip.Items.Add(document.AccessibleDescription);
                    info.ToolTipType = ToolTipType.SuperTip;
                    info.SuperTip = superToolTip;
                    info.Object = document;
                }
            }
            return info;
        }
        bool IToolTipControlClient.ShowToolTips
        {
            get { return true; }
        }
    }
}
