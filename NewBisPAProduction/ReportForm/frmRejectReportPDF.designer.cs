namespace NewBisPA.ReportForm
{
    partial class frmRejectReportPDF
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource2 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource3 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource4 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource5 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource6 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource7 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.REPORT_DCBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.REPORT_DC_CollectionBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.reportViewer2 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.reportViewer3 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.reportViewer4 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.reportViewer5 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.reportViewer6 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.reportViewer7 = new Microsoft.Reporting.WinForms.ReportViewer();
            ((System.ComponentModel.ISupportInitialize)(this.REPORT_DCBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.REPORT_DC_CollectionBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // REPORT_DCBindingSource
            // 
            this.REPORT_DCBindingSource.DataSource = typeof(NewBisPA.NewBISSvcRef.REPORT_DC);
            // 
            // REPORT_DC_CollectionBindingSource
            // 
            this.REPORT_DC_CollectionBindingSource.DataSource = typeof(NewBisPA.NewBISSvcRef.REPORT_DC_Collection);
            // 
            // reportViewer1
            // 
            reportDataSource1.Name = "DataSet1";
            reportDataSource1.Value = this.REPORT_DCBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "NewBisPA.Report.dcReport.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(3, 2);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(396, 246);
            this.reportViewer1.TabIndex = 0;
            // 
            // reportViewer2
            // 
            reportDataSource2.Name = "exDataSet";
            reportDataSource2.Value = this.REPORT_DCBindingSource;
            this.reportViewer2.LocalReport.DataSources.Add(reportDataSource2);
            this.reportViewer2.LocalReport.ReportEmbeddedResource = "NewBisPA.Report.exReport.rdlc";
            this.reportViewer2.Location = new System.Drawing.Point(405, 2);
            this.reportViewer2.Name = "reportViewer2";
            this.reportViewer2.Size = new System.Drawing.Size(396, 246);
            this.reportViewer2.TabIndex = 1;
            // 
            // reportViewer3
            // 
            reportDataSource3.Name = "ccDataSet";
            reportDataSource3.Value = this.REPORT_DCBindingSource;
            this.reportViewer3.LocalReport.DataSources.Add(reportDataSource3);
            this.reportViewer3.LocalReport.ReportEmbeddedResource = "NewBisPA.Report.ccReport.rdlc";
            this.reportViewer3.Location = new System.Drawing.Point(3, 254);
            this.reportViewer3.Name = "reportViewer3";
            this.reportViewer3.Size = new System.Drawing.Size(396, 246);
            this.reportViewer3.TabIndex = 2;
            // 
            // reportViewer4
            // 
            reportDataSource4.Name = "ppDataSet";
            reportDataSource4.Value = this.REPORT_DCBindingSource;
            this.reportViewer4.LocalReport.DataSources.Add(reportDataSource4);
            this.reportViewer4.LocalReport.ReportEmbeddedResource = "NewBisPA.Report.ppReport.rdlc";
            this.reportViewer4.Location = new System.Drawing.Point(405, 254);
            this.reportViewer4.Name = "reportViewer4";
            this.reportViewer4.Size = new System.Drawing.Size(396, 246);
            this.reportViewer4.TabIndex = 3;
            // 
            // reportViewer5
            // 
            reportDataSource5.Name = "ntDataSet";
            reportDataSource5.Value = this.REPORT_DCBindingSource;
            this.reportViewer5.LocalReport.DataSources.Add(reportDataSource5);
            this.reportViewer5.LocalReport.ReportEmbeddedResource = "NewBisPA.Report.ntReport.rdlc";
            this.reportViewer5.Location = new System.Drawing.Point(3, 506);
            this.reportViewer5.Name = "reportViewer5";
            this.reportViewer5.Size = new System.Drawing.Size(396, 246);
            this.reportViewer5.TabIndex = 4;
            // 
            // webBrowser1
            // 
            this.webBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser1.Location = new System.Drawing.Point(0, 0);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(978, 661);
            this.webBrowser1.TabIndex = 14;
            // 
            // reportViewer6
            // 
            reportDataSource6.Name = "ifDataSet";
            reportDataSource6.Value = this.REPORT_DCBindingSource;
            this.reportViewer6.LocalReport.DataSources.Add(reportDataSource6);
            this.reportViewer6.LocalReport.ReportEmbeddedResource = "NewBisPA.Report.ifReport.rdlc";
            this.reportViewer6.Location = new System.Drawing.Point(405, 506);
            this.reportViewer6.Name = "reportViewer6";
            this.reportViewer6.Size = new System.Drawing.Size(396, 246);
            this.reportViewer6.TabIndex = 15;
            // 
            // reportViewer7
            // 
            reportDataSource7.Name = "DataSet1";
            reportDataSource7.Value = this.REPORT_DC_CollectionBindingSource;
            this.reportViewer7.LocalReport.DataSources.Add(reportDataSource7);
            this.reportViewer7.LocalReport.ReportEmbeddedResource = "NewBisPA.Report.dcReportKB.rdlc";
            this.reportViewer7.Location = new System.Drawing.Point(291, 207);
            this.reportViewer7.Name = "reportViewer7";
            this.reportViewer7.Size = new System.Drawing.Size(396, 246);
            this.reportViewer7.TabIndex = 16;
            // 
            // frmRejectReportPDF
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(978, 661);
            this.Controls.Add(this.webBrowser1);
            this.Controls.Add(this.reportViewer7);
            this.Controls.Add(this.reportViewer5);
            this.Controls.Add(this.reportViewer4);
            this.Controls.Add(this.reportViewer3);
            this.Controls.Add(this.reportViewer2);
            this.Controls.Add(this.reportViewer1);
            this.Controls.Add(this.reportViewer6);
            this.Name = "frmRejectReportPDF";
            this.Text = "frmReportRejectPDF";
            this.Load += new System.EventHandler(this.frmReportRejectPDF_Load);
            ((System.ComponentModel.ISupportInitialize)(this.REPORT_DCBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.REPORT_DC_CollectionBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer2;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer3;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer4;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer5;
        private System.Windows.Forms.BindingSource REPORT_DCBindingSource;
        private System.Windows.Forms.WebBrowser webBrowser1;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer6;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer7;
        private System.Windows.Forms.BindingSource REPORT_DC_CollectionBindingSource;
    }
}