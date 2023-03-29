namespace NewBisPA
{
    partial class FormCsSuspenseDetail
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormCsSuspenseDetail));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dgvCsSuspense = new System.Windows.Forms.DataGridView();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.txtTotalSuspense = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.CSSNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CSSSuspenseNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CSSSuspensePrem = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CSSSuspenseFee = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CSSSuspenseDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CSSSuspensePayBy = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CSSSuspenseClearing = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CSSSuspenseCancel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCsSuspense)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dgvCsSuspense);
            this.groupBox1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.groupBox1.Location = new System.Drawing.Point(12, 74);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(834, 206);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "ข้อมูลบัญชีพัก";
            // 
            // dgvCsSuspense
            // 
            this.dgvCsSuspense.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvCsSuspense.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvCsSuspense.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCsSuspense.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CSSNo,
            this.CSSSuspenseNo,
            this.CSSSuspensePrem,
            this.CSSSuspenseFee,
            this.CSSSuspenseDate,
            this.CSSSuspensePayBy,
            this.CSSSuspenseClearing,
            this.CSSSuspenseCancel});
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle10.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            dataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvCsSuspense.DefaultCellStyle = dataGridViewCellStyle10;
            this.dgvCsSuspense.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvCsSuspense.Location = new System.Drawing.Point(3, 18);
            this.dgvCsSuspense.Name = "dgvCsSuspense";
            this.dgvCsSuspense.RowHeadersVisible = false;
            this.dgvCsSuspense.RowTemplate.Height = 30;
            this.dgvCsSuspense.Size = new System.Drawing.Size(828, 185);
            this.dgvCsSuspense.TabIndex = 0;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(14, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(832, 67);
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // txtTotalSuspense
            // 
            this.txtTotalSuspense.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtTotalSuspense.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtTotalSuspense.ForeColor = System.Drawing.Color.DarkBlue;
            this.txtTotalSuspense.Location = new System.Drawing.Point(737, 285);
            this.txtTotalSuspense.Name = "txtTotalSuspense";
            this.txtTotalSuspense.Size = new System.Drawing.Size(107, 23);
            this.txtTotalSuspense.TabIndex = 3;
            this.txtTotalSuspense.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label1.ForeColor = System.Drawing.Color.DarkBlue;
            this.label1.Location = new System.Drawing.Point(643, 289);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 16);
            this.label1.TabIndex = 4;
            this.label1.Text = "รวมจำนวนเงิน";
            // 
            // CSSNo
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.CSSNo.DefaultCellStyle = dataGridViewCellStyle2;
            this.CSSNo.HeaderText = "ลำดับที่";
            this.CSSNo.Name = "CSSNo";
            this.CSSNo.ReadOnly = true;
            this.CSSNo.Width = 60;
            // 
            // CSSSuspenseNo
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.CSSSuspenseNo.DefaultCellStyle = dataGridViewCellStyle3;
            this.CSSSuspenseNo.HeaderText = "เลขที่";
            this.CSSSuspenseNo.Name = "CSSSuspenseNo";
            this.CSSSuspenseNo.ReadOnly = true;
            this.CSSSuspenseNo.Width = 150;
            // 
            // CSSSuspensePrem
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle4.NullValue = null;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.CSSSuspensePrem.DefaultCellStyle = dataGridViewCellStyle4;
            this.CSSSuspensePrem.HeaderText = "จำนวนเงิน";
            this.CSSSuspensePrem.Name = "CSSSuspensePrem";
            this.CSSSuspensePrem.ReadOnly = true;
            this.CSSSuspensePrem.Width = 120;
            // 
            // CSSSuspenseFee
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.CSSSuspenseFee.DefaultCellStyle = dataGridViewCellStyle5;
            this.CSSSuspenseFee.HeaderText = "ค่าธรรมเนียม";
            this.CSSSuspenseFee.Name = "CSSSuspenseFee";
            this.CSSSuspenseFee.ReadOnly = true;
            // 
            // CSSSuspenseDate
            // 
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.CSSSuspenseDate.DefaultCellStyle = dataGridViewCellStyle6;
            this.CSSSuspenseDate.HeaderText = "วันที่ชำระ";
            this.CSSSuspenseDate.Name = "CSSSuspenseDate";
            this.CSSSuspenseDate.ReadOnly = true;
            // 
            // CSSSuspensePayBy
            // 
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.CSSSuspensePayBy.DefaultCellStyle = dataGridViewCellStyle7;
            this.CSSSuspensePayBy.HeaderText = "จ่ายโดย";
            this.CSSSuspensePayBy.Name = "CSSSuspensePayBy";
            this.CSSSuspensePayBy.ReadOnly = true;
            this.CSSSuspensePayBy.Width = 150;
            // 
            // CSSSuspenseClearing
            // 
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.CSSSuspenseClearing.DefaultCellStyle = dataGridViewCellStyle8;
            this.CSSSuspenseClearing.HeaderText = "Clearing";
            this.CSSSuspenseClearing.Name = "CSSSuspenseClearing";
            this.CSSSuspenseClearing.ReadOnly = true;
            this.CSSSuspenseClearing.Width = 60;
            // 
            // CSSSuspenseCancel
            // 
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.CSSSuspenseCancel.DefaultCellStyle = dataGridViewCellStyle9;
            this.CSSSuspenseCancel.HeaderText = "สถานะ";
            this.CSSSuspenseCancel.Name = "CSSSuspenseCancel";
            this.CSSSuspenseCancel.ReadOnly = true;
            this.CSSSuspenseCancel.Width = 80;
            // 
            // FormCsSuspenseDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(858, 319);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtTotalSuspense);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.groupBox1);
            this.Name = "FormCsSuspenseDetail";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "รายละเอียดบัญชีพัก";
            this.Load += new System.EventHandler(this.FormCsSuspenseDetail_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCsSuspense)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dgvCsSuspense;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox txtTotalSuspense;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn CSSNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn CSSSuspenseNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn CSSSuspensePrem;
        private System.Windows.Forms.DataGridViewTextBoxColumn CSSSuspenseFee;
        private System.Windows.Forms.DataGridViewTextBoxColumn CSSSuspenseDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn CSSSuspensePayBy;
        private System.Windows.Forms.DataGridViewTextBoxColumn CSSSuspenseClearing;
        private System.Windows.Forms.DataGridViewTextBoxColumn CSSSuspenseCancel;

    }
}