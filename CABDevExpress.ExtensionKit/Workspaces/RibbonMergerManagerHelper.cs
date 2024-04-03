using DevExpress.XtraBars.Ribbon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CABDevExpress.Workspaces
{
    public static class RibbonMergerManagerHelper
    {
        public static void DoMergeRibbon(object sender, object parentForm, Func<RibbonControl, bool> fnCondition)
        {
            Form form = parentForm as Form;
            if (form?.Owner != null)
                form = form.Owner as Form;
            RibbonForm rf = form as RibbonForm;
            DoMergeRibbon(sender, rf, fnCondition);
        }
        public static void DoMergeRibbon(object sender, RibbonForm rf, Func<RibbonControl, bool> fnCondition)
        {
            if (sender != null && rf != null)
            {
                RibbonControl childRibbon = sender as RibbonControl;
                if (childRibbon!=null && (childRibbon?.Handle?? IntPtr.Zero) != IntPtr.Zero)
                {
                    childRibbon.BeginInvoke(new Action(() =>
                    {
                        if (rf != null)
                        {
                            //RibbonControl childRibbon = FindRibbon(sender);
                            if (childRibbon != null)
                            {
                                if (fnCondition(childRibbon) == true)
                                {
                                    rf.Ribbon.UnMergeRibbon();
                                    rf.Ribbon.MergeRibbon(childRibbon);
                                }
                            }
                        }
                    }));
                }
            }
        }

        //public static RibbonControl FindRibbon(object sender)
        //{
        //    System.Windows.Forms.Control ctrlMaster = sender as System.Windows.Forms.Control;
        //    if (ctrlMaster != null && ctrlMaster.Controls != null)
        //    {
        //        foreach (System.Windows.Forms.Control ctrl in ctrlMaster.Controls)
        //        {
        //            if (ctrl is RibbonControl)
        //                return ctrl as RibbonControl;
        //            if (ctrl.Controls != null)
        //            {
        //                RibbonControl ribbon = FindRibbon(ctrl);
        //                if (ribbon != null)
        //                    return ribbon;
        //            }
        //        }
        //    }
        //    return null;
        //}
    }
}
