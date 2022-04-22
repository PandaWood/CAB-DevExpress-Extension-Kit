using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Reflection;

namespace CABDevExpress
{
    public static class ControlExtension
    {
        public static void SetStyle(this Control ctrl, ControlStyles cs, bool b)
        {
            MethodInfo mi = ctrl.GetType().GetMethod("SetStyle", BindingFlags.Instance | BindingFlags.NonPublic);
            if (mi != null)
            {
                mi.Invoke(ctrl, new object[] { cs, b });
            }
        }
    }
}
