namespace NewBisPA.ReportForm
{
    partial class frmReportMODetailDoc
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmReportMODetailDoc));
            this.dtgDocshow = new System.Windows.Forms.DataGridView();
            this.clmDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmPrint_seq = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmPrint_seq2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmIso_code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dtgDocshow)).BeginInit();
            this.SuspendLayout();
            // 
            // dtgDocshow
            // 
            this.dtgDocshow.AllowUserToAddRows = false;
            this.dtgDocshow.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dtgDocshow.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dtgDocshow.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dtgDocshow.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clmDescription,
            this.clmPrint_seq,
            this.clmPrint_seq2,
            this.clmIso_code});
            this.dtgDocshow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtgDocshow.Location = new System.Drawing.Point(0, 0);
            this.dtgDocshow.Name = "dtgDocshow";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dtgDocshow.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dtgDocshow.RowHeadersVisible = false;
            this.dtgDocshow.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToDisplayedHeaders;
            this.dtgDocshow.Size = new System.Drawing.Size(743, 443);
            this.dtgDocshow.TabIndex = 0;
            // 
            // clmDescription
            // 
            this.clmDescription.HeaderText = "รายละเอียดจดหมายดำเนินการเพิ่มเติม และเอกสาร";
            this.clmDescription.Name = "clmDescription";
            this.clmDescription.ReadOnly = true;
            this.clmDescription.Width = 700;
            // 
            // clmPrint_seq
            // 
            this.clmPrint_seq.HeaderText = "Column1";
            this.clmPrint_seq.Name = "clmPrint_seq";
            this.clmPrint_seq.Visible = false;
            // 
            // clmPrint_seq2
            // 
            this.clmPrint_seq2.HeaderText = "Column1";
            this.clmPrint_seq2.Name = "clmPrint_seq2";
            this.clmPrint_seq2.Visible = false;
            // 
            // clmIso_code
            // 
            this.clmIso_code.HeaderText = "Column1";
            this.clmIso_code.Name = "clmIso_code";
            this.clmIso_code.Visible = false;
            // 
            // frmReportMODetailDoc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(743, 443);
            this.Controls.Add(this.dtgDocshow);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmReportMODetailDoc";
            this.Text = "รายละเอียดเอกสารแนบ";
            this.Load += new System.EventHandler(this.frmReportMODetailDoc_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtgDocshow)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dtgDocshow;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmDescription;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmPrint_seq;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmPrint_seq2;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmIso_code;
    }
}