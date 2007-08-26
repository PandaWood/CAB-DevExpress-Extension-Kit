using System;
using DevExpress.XtraEditors;
using Microsoft.Practices.CompositeUI.SmartParts;

namespace BankTellerModule.WorkItems.BankTeller
{
    public partial class StatsBarView : XtraUserControl, ISmartPartInfoProvider
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        public StatsBarView()
        {
            // This call is required by the Windows.Forms Form Designer.
            InitializeComponent();

            // TODO: Add any initialization after the InitForm call
        }

        public Microsoft.Practices.CompositeUI.SmartParts.ISmartPartInfo GetSmartPartInfo(Type smartPartInfoType)
        {
            return infoProvider.GetSmartPartInfo(smartPartInfoType);
        }
    }
}