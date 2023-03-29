namespace NewBisPA.ReportForm
{
    partial class frmReportAPPDF
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmReportAPPDF));
            this.REPORT_AP_DETAILBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.REPORT_AP_DETAILLIST_CollectionBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.rEPORTAPDETAILLISTCollectionBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            ((System.ComponentModel.ISupportInitialize)(this.REPORT_AP_DETAILBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.REPORT_AP_DETAILLIST_CollectionBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rEPORTAPDETAILLISTCollectionBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // REPORT_AP_DETAILBindingSource
            // 
            this.REPORT_AP_DETAILBindingSource.DataSource = typeof(NewBisPA.NewBISSvcRef.REPORT_AP_DETAIL);
            // 
            // REPORT_AP_DETAILLIST_CollectionBindingSource
            // 
            this.REPORT_AP_DETAILLIST_CollectionBindingSource.DataSource = typeof(NewBisPA.NewBISSvcRef.REPORT_AP_DETAILLIST_Collection);
            // 
            // rEPORTAPDETAILLISTCollectionBindingSource
            // 
            this.rEPORTAPDETAILLISTCollectionBindingSource.DataSource = typeof(NewBisPA.NewBISSvcRef.REPORT_AP_DETAILLIST_Collection);
            // 
            // reportViewer1
            // 
            this.reportViewer1.DocumentMapWidth = 93;
            reportDataSource1.Name = "apDetailDataset";
            reportDataSource1.Value = this.REPORT_AP_DETAILBindingSource;
            reportDataSource2.Name = "apDetaillistRDDataset";
            reportDataSource2.Value = this.REPORT_AP_DETAILLIST_CollectionBindingSource;
            reportDataSource3.Name = "apDetaillistPMDataset";
            reportDataSource3.Value = this.rEPORTAPDETAILLISTCollectionBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource2);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource3);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "NewBisPA.Report.apReport.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(12, 12);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(873, 592);
            this.reportViewer1.TabIndex = 0;
            this.reportViewer1.Visible = false;
            // 
            // frmReportAPPDF
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(908, 644);
            this.Controls.Add(this.reportViewer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmReportAPPDF";
            this.Text = "NewBisPA :  จดหมายการชำระเบี้ยประกันภัย";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmReportAPPDF_Load);
            ((System.ComponentModel.ISupportInitialize)(this.REPORT_AP_DETAILBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.REPORT_AP_DETAILLIST_CollectionBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rEPORTAPDETAILLISTCollectionBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource REPORT_AP_DETAILBindingSource;
        private System.Windows.Forms.BindingSource REPORT_AP_DETAILLIST_CollectionBindingSource;
        private System.Windows.Forms.BindingSource rEPORTAPDETAILLISTCollectionBindingSource;
    }
}