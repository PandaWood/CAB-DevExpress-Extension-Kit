namespace BankTellerModule.WorkItems.BankTeller
{
    partial class StatisticsBarView
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
			DevExpress.XtraCharts.XYDiagram xyDiagram1 = new DevExpress.XtraCharts.XYDiagram();
			DevExpress.XtraCharts.SecondaryAxisX secondaryAxisX1 = new DevExpress.XtraCharts.SecondaryAxisX();
			DevExpress.XtraCharts.Series series1 = new DevExpress.XtraCharts.Series();
			DevExpress.XtraCharts.Series series2 = new DevExpress.XtraCharts.Series();
			DevExpress.XtraCharts.ChartTitle chartTitle1 = new DevExpress.XtraCharts.ChartTitle();
			this.infoProvider = new Microsoft.Practices.CompositeUI.SmartParts.SmartPartInfoProvider();
			this.chartControl1 = new DevExpress.XtraCharts.ChartControl();
			((System.ComponentModel.ISupportInitialize)(this.chartControl1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(xyDiagram1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(secondaryAxisX1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(series1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(series2)).BeginInit();
			this.SuspendLayout();
			// 
			// chartControl1
			// 
			xyDiagram1.AxisY.Range.SideMarginsEnabled = true;
			xyDiagram1.AxisX.Range.SideMarginsEnabled = true;
			secondaryAxisX1.Name = "secondaryAxisX1";
			secondaryAxisX1.Range.SideMarginsEnabled = true;
			xyDiagram1.SecondaryAxesX.AddRange(new DevExpress.XtraCharts.SecondaryAxisX[] {
            secondaryAxisX1});
			this.chartControl1.Diagram = xyDiagram1;
			this.chartControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.chartControl1.Location = new System.Drawing.Point(0, 0);
			this.chartControl1.Name = "chartControl1";
			series1.PointOptionsTypeName = "PointOptions";
			series1.Name = "Series 1";
			series2.PointOptionsTypeName = "PointOptions";
			series2.Name = "Series 2";
			this.chartControl1.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series1,
        series2};
			this.chartControl1.SeriesTemplate.PointOptionsTypeName = "PointOptions";
			this.chartControl1.Size = new System.Drawing.Size(167, 230);
			this.chartControl1.TabIndex = 0;
			chartTitle1.Text = "Stats";
			this.chartControl1.Titles.AddRange(new DevExpress.XtraCharts.ChartTitle[] {
            chartTitle1});
			// 
			// StatsBarView
			// 
			this.Controls.Add(this.chartControl1);
			this.Name = "StatsBarView";
			this.Size = new System.Drawing.Size(167, 230);
			((System.ComponentModel.ISupportInitialize)(secondaryAxisX1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(xyDiagram1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(series1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(series2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.chartControl1)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

		private Microsoft.Practices.CompositeUI.SmartParts.SmartPartInfoProvider infoProvider;
		private DevExpress.XtraCharts.ChartControl chartControl1;
    }
}
