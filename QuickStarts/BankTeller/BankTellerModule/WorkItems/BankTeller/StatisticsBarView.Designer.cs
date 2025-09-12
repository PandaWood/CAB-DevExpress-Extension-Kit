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
            DevExpress.XtraCharts.SecondaryAxisX secondaryAxisx1 = new DevExpress.XtraCharts.SecondaryAxisX();
            DevExpress.XtraCharts.Series series1 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.Series series2 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.ChartTitle chartTitle1 = new DevExpress.XtraCharts.ChartTitle();
            infoProvider = new Microsoft.Practices.CompositeUI.SmartParts.SmartPartInfoProvider();
            chartControl1 = new DevExpress.XtraCharts.ChartControl();
            ((System.ComponentModel.ISupportInitialize)chartControl1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)xyDiagram1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)secondaryAxisx1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)series1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)series2).BeginInit();
            SuspendLayout();
            // 
            // chartControl1
            // 
            xyDiagram1.AxisX.VisibleInPanesSerializable = "-1";
            xyDiagram1.AxisY.VisibleInPanesSerializable = "-1";
            secondaryAxisx1.AxisID = 0;
            secondaryAxisx1.Name = "secondaryAxisX1";
            secondaryAxisx1.VisibleInPanesSerializable = "-1";
            xyDiagram1.SecondaryAxesX.AddRange(new DevExpress.XtraCharts.SecondaryAxisX[] { secondaryAxisx1 });
            chartControl1.Diagram = xyDiagram1;
            chartControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            chartControl1.Location = new System.Drawing.Point(0, 0);
            chartControl1.Name = "chartControl1";
            series1.Name = "Series 1";
            series1.SeriesID = 0;
            series2.Name = "Series 2";
            series2.SeriesID = 1;
            chartControl1.SeriesSerializable = new DevExpress.XtraCharts.Series[]
    {
    series1,
    series2
    };
            chartControl1.Size = new System.Drawing.Size(745, 230);
            chartControl1.TabIndex = 0;
            chartTitle1.Text = "Stats";
            chartTitle1.TitleID = 0;
            chartControl1.Titles.AddRange(new DevExpress.XtraCharts.ChartTitle[] { chartTitle1 });
            // 
            // StatisticsBarView
            // 
            Controls.Add(chartControl1);
            Name = "StatisticsBarView";
            Size = new System.Drawing.Size(745, 230);
            ((System.ComponentModel.ISupportInitialize)secondaryAxisx1).EndInit();
            ((System.ComponentModel.ISupportInitialize)xyDiagram1).EndInit();
            ((System.ComponentModel.ISupportInitialize)series1).EndInit();
            ((System.ComponentModel.ISupportInitialize)series2).EndInit();
            ((System.ComponentModel.ISupportInitialize)chartControl1).EndInit();
            ResumeLayout(false);

        }

        #endregion

        private Microsoft.Practices.CompositeUI.SmartParts.SmartPartInfoProvider infoProvider;
		private DevExpress.XtraCharts.ChartControl chartControl1;
    }
}
