namespace NewBisPA.ReportForm
{
    partial class frmReportCOPDF
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
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource8 = new Microsoft.Reporting.WinForms.ReportDataSource();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmReportCOPDF));
            this.REPORT_CO_DETAILBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.REPORT_CO_PLRD_CollectionBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.REPORT_CO_EXCLUDE_CollectionBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.reportViewerGM = new Microsoft.Reporting.WinForms.ReportViewer();
            ((System.ComponentModel.ISupportInitialize)(this.REPORT_CO_DETAILBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.REPORT_CO_PLRD_CollectionBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.REPORT_CO_EXCLUDE_CollectionBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // REPORT_CO_DETAILBindingSource
            // 
            this.REPORT_CO_DETAILBindingSource.DataSource = typeof(NewBisPA.NewBISSvcRef.REPORT_CO_DETAIL);
            // 
            // REPORT_CO_PLRD_CollectionBindingSource
            // 
            this.REPORT_CO_PLRD_CollectionBindingSource.DataSource = typeof(NewBisPA.NewBISSvcRef.REPORT_CO_PLRD_Collection);
            // 
            // REPORT_CO_EXCLUDE_CollectionBindingSource
            // 
            this.REPORT_CO_EXCLUDE_CollectionBindingSource.DataSource = typeof(NewBisPA.NewBISSvcRef.REPORT_CO_EXCLUDE_Collection);
            // 
            // reportViewer1
            // 
            reportDataSource1.Name = "apDetailDataset";
            reportDataSource1.Value = this.REPORT_CO_DETAILBindingSource;
            reportDataSource2.Name = "coDetailPolicyRider";
            reportDataSource2.Value = this.REPORT_CO_PLRD_CollectionBindingSource;
            reportDataSource3.Name = "coDetailExclude";
            reportDataSource3.Value = this.REPORT_CO_EXCLUDE_CollectionBindingSource;
            reportDataSource4.Name = "coDataSet";
            reportDataSource4.Value = this.REPORT_CO_DETAILBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource2);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource3);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource4);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "NewBisPA.Report.coReport.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(12, 12);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(331, 637);
            this.reportViewer1.TabIndex = 0;
            this.reportViewer1.Visible = false;
            // 
            // reportViewerGM
            // 
            reportDataSource5.Name = "apDetailDataset";
            reportDataSource5.Value = this.REPORT_CO_DETAILBindingSource;
            reportDataSource6.Name = "coDetailPolicyRider";
            reportDataSource6.Value = this.REPORT_CO_PLRD_CollectionBindingSource;
            reportDataSource7.Name = "coDetailExclude";
            reportDataSource7.Value = this.REPORT_CO_EXCLUDE_CollectionBindingSource;
            reportDataSource8.Name = "coDataSet";
            reportDataSource8.Value = this.REPORT_CO_DETAILBindingSource;
            this.reportViewerGM.LocalReport.DataSources.Add(reportDataSource5);
            this.reportViewerGM.LocalReport.DataSources.Add(reportDataSource6);
            this.reportViewerGM.LocalReport.DataSources.Add(reportDataSource7);
            this.reportViewerGM.LocalReport.DataSources.Add(reportDataSource8);
            this.reportViewerGM.LocalReport.ReportEmbeddedResource = "NewBisPA.Report.coReportGM.rdlc";
            this.reportViewerGM.Location = new System.Drawing.Point(479, 12);
            this.reportViewerGM.Name = "reportViewerGM";
            this.reportViewerGM.Size = new System.Drawing.Size(331, 637);
            this.reportViewerGM.TabIndex = 1;
            this.reportViewerGM.Visible = false;
            // 
            // frmReportCOPDF
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(978, 661);
            this.Controls.Add(this.reportViewerGM);
            this.Controls.Add(this.reportViewer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmReportCOPDF";
            this.Text = "NewBisPA :  จดหมายข้อเสนอใหม่ของบริษัท ในการรับประกัน ";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmReportCOPDF_Load);
            ((System.ComponentModel.ISupportInitialize)(this.REPORT_CO_DETAILBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.REPORT_CO_PLRD_CollectionBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.REPORT_CO_EXCLUDE_CollectionBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource REPORT_CO_DETAILBindingSource;
        private System.Windows.Forms.BindingSource REPORT_CO_PLRD_CollectionBindingSource;
        private System.Windows.Forms.BindingSource REPORT_CO_EXCLUDE_CollectionBindingSource;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewerGM;
    }
}