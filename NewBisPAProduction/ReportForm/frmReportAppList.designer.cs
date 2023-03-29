namespace NewBisPA.ReportForm
{
    partial class frmReportAppList
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
            this.APP_IN_PROCESS_COLLECIONBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            ((System.ComponentModel.ISupportInitialize)(this.APP_IN_PROCESS_COLLECIONBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // APP_IN_PROCESS_COLLECIONBindingSource
            // 
            this.APP_IN_PROCESS_COLLECIONBindingSource.DataSource = typeof(NewBisPA.NewBISSvcRef.APP_IN_PROCESS_COLLECION);
            // 
            // reportViewer1
            // 
            reportDataSource1.Name = "appDataSet";
            reportDataSource1.Value = this.APP_IN_PROCESS_COLLECIONBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "NewBisPA.Report.appListReport.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(491, 277);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(396, 246);
            this.reportViewer1.TabIndex = 0;
            // 
            // webBrowser1
            // 
            this.webBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser1.Location = new System.Drawing.Point(0, 0);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(962, 623);
            this.webBrowser1.TabIndex = 15;
            // 
            // frmReportAppList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(962, 623);
            this.Controls.Add(this.webBrowser1);
            this.Controls.Add(this.reportViewer1);
            this.Name = "frmReportAppList";
            this.Text = "frmReportAppList";
            this.Load += new System.EventHandler(this.frmReportAppList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.APP_IN_PROCESS_COLLECIONBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource APP_IN_PROCESS_COLLECIONBindingSource;
        private System.Windows.Forms.WebBrowser webBrowser1;
    }
}