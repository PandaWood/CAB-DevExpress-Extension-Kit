using System;
using DevExpress.XtraEditors;
using Microsoft.Practices.CompositeUI.SmartParts;

namespace BankTellerModule.WorkItems.BankTeller
{
	[SmartPart]
    public partial class StatisticsBarView : XtraUserControl, ISmartPartInfoProvider
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        public StatisticsBarView()
        {
            // This call is required by the Windows.Forms Form Designer.
            InitializeComponent();
        }

        public ISmartPartInfo GetSmartPartInfo(Type smartPartInfoType)
        {
            return infoProvider.GetSmartPartInfo(smartPartInfoType);
        }
    }
}