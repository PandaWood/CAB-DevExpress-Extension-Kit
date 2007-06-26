namespace BankTellerModule.WorkItems.BankTeller
{
    partial class StatsBarView
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.xtraNavBarGroupSmartPartInfo1 = new DevExpress.CompositeUI.SmartPartInfos.XtraNavBarGroupSmartPartInfo();
            this.infoProvider = new Microsoft.Practices.CompositeUI.SmartParts.SmartPartInfoProvider();
            this.SuspendLayout();
            // 
            // xtraNavBarGroupSmartPartInfo1
            // 
            this.xtraNavBarGroupSmartPartInfo1.Description = "";
            this.xtraNavBarGroupSmartPartInfo1.LargeImage = global::BankTellerModule.Properties.Resources.statsLarge;
            this.xtraNavBarGroupSmartPartInfo1.SmallImage = global::BankTellerModule.Properties.Resources.statsSmall;
            this.xtraNavBarGroupSmartPartInfo1.Title = "Statistics";
            this.infoProvider.Items.Add(this.xtraNavBarGroupSmartPartInfo1);
            // 
            // StatsBarView
            // 
            this.Name = "StatsBarView";
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.CompositeUI.SmartPartInfos.XtraNavBarGroupSmartPartInfo xtraNavBarGroupSmartPartInfo1;
        private Microsoft.Practices.CompositeUI.SmartParts.SmartPartInfoProvider infoProvider;
    }
}
