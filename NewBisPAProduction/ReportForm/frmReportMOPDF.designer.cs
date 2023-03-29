namespace NewBisPA.ReportForm
{
    partial class frmReportMOPDF
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmReportMOPDF));
            this.REPORT_MEMO_DETAILBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.REPORT_MEMO_DETAILLISTBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.reportViewer2 = new Microsoft.Reporting.WinForms.ReportViewer();
            ((System.ComponentModel.ISupportInitialize)(this.REPORT_MEMO_DETAILBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.REPORT_MEMO_DETAILLISTBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // REPORT_MEMO_DETAILBindingSource
            // 
            this.REPORT_MEMO_DETAILBindingSource.DataSource = typeof(NewBisPA.NewBISSvcRef.REPORT_MEMO_DETAIL);
            // 
            // REPORT_MEMO_DETAILLISTBindingSource
            // 
            this.REPORT_MEMO_DETAILLISTBindingSource.DataSource = typeof(NewBisPA.NewBISSvcRef.REPORT_MEMO_DETAILLIST);
            // 
            // reportViewer1
            // 
            this.reportViewer1.DocumentMapWidth = 80;
            reportDataSource1.Name = "memoDetailDataSet";
            reportDataSource1.Value = this.REPORT_MEMO_DETAILBindingSource;
            reportDataSource2.Name = "memoDetaillistDataSet";
            reportDataSource2.Value = this.REPORT_MEMO_DETAILLISTBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource2);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "NewBisPA.Report.memoReport.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(1, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(377, 480);
            this.reportViewer1.TabIndex = 0;
            this.reportViewer1.Visible = false;
            // 
            // reportViewer2
            // 
            this.reportViewer2.DocumentMapWidth = 80;
            reportDataSource3.Name = "memoDetailDataSet";
            reportDataSource3.Value = this.REPORT_MEMO_DETAILBindingSource;
            reportDataSource4.Name = "memoDetaillistDataSet";
            reportDataSource4.Value = this.REPORT_MEMO_DETAILLISTBindingSource;
            this.reportViewer2.LocalReport.DataSources.Add(reportDataSource3);
            this.reportViewer2.LocalReport.DataSources.Add(reportDataSource4);
            this.reportViewer2.LocalReport.ReportEmbeddedResource = "NewBisPA.Report.memoReportGM.rdlc";
            this.reportViewer2.Location = new System.Drawing.Point(462, 0);
            this.reportViewer2.Name = "reportViewer2";
            this.reportViewer2.Size = new System.Drawing.Size(377, 480);
            this.reportViewer2.TabIndex = 1;
            this.reportViewer2.Visible = false;
            // 
            // frmReportMOPDF
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(883, 477);
            this.Controls.Add(this.reportViewer2);
            this.Controls.Add(this.reportViewer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmReportMOPDF";
            this.Text = "NewBisPA :  จดหมายขอให้ดำเนินการเพิ่มเติม";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmReportMOPDF_Load);
            ((System.ComponentModel.ISupportInitialize)(this.REPORT_MEMO_DETAILBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.REPORT_MEMO_DETAILLISTBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource REPORT_MEMO_DETAILBindingSource;
        private System.Windows.Forms.BindingSource REPORT_MEMO_DETAILLISTBindingSource;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer2;
    }
}