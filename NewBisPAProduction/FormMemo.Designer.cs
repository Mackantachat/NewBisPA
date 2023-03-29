namespace NewBisPA
{
    partial class FormMemo
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
            this.panelMemo = new System.Windows.Forms.Panel();
            this.memoMainForm1 = new NewBisPA.UserControlsForm.MemoMainForm();
            this.panelMemoDetail = new System.Windows.Forms.Panel();
            this.memoDetailForm1 = new NewBisPA.UserControlsForm.MemoDetailForm();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.panelMemo.SuspendLayout();
            this.panelMemoDetail.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMemo
            // 
            this.panelMemo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.panelMemo.Controls.Add(this.memoMainForm1);
            this.panelMemo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMemo.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.panelMemo.Location = new System.Drawing.Point(3, 19);
            this.panelMemo.Name = "panelMemo";
            this.panelMemo.Size = new System.Drawing.Size(1157, 180);
            this.panelMemo.TabIndex = 0;
            // 
            // memoMainForm1
            // 
            this.memoMainForm1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.memoMainForm1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.memoMainForm1.Location = new System.Drawing.Point(1, 1);
            this.memoMainForm1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.memoMainForm1.Name = "memoMainForm1";
            this.memoMainForm1.Size = new System.Drawing.Size(820, 41);
            this.memoMainForm1.TabIndex = 0;
            // 
            // panelMemoDetail
            // 
            this.panelMemoDetail.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.panelMemoDetail.Controls.Add(this.memoDetailForm1);
            this.panelMemoDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMemoDetail.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.panelMemoDetail.Location = new System.Drawing.Point(3, 19);
            this.panelMemoDetail.Name = "panelMemoDetail";
            this.panelMemoDetail.Size = new System.Drawing.Size(1157, 414);
            this.panelMemoDetail.TabIndex = 1;
            // 
            // memoDetailForm1
            // 
            this.memoDetailForm1.BackColor = System.Drawing.Color.BlanchedAlmond;
            this.memoDetailForm1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.memoDetailForm1.Location = new System.Drawing.Point(1, 1);
            this.memoDetailForm1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.memoDetailForm1.Name = "memoDetailForm1";
            this.memoDetailForm1.Size = new System.Drawing.Size(1046, 100);
            this.memoDetailForm1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.panelMemo);
            this.groupBox1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1163, 202);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "ข้อมูลการขอเอกสารเพิ่ม";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.panelMemoDetail);
            this.groupBox2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.groupBox2.Location = new System.Drawing.Point(12, 218);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1163, 436);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "รายละเอียดการขอเอกสารเพิ่ม";
            // 
            // FormMemo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1187, 666);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "FormMemo";
            this.Text = "ข้อมูลเอกสารเพิ่ม";
            this.Load += new System.EventHandler(this.FormMemo_Load);
            this.panelMemo.ResumeLayout(false);
            this.panelMemoDetail.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelMemo;
        private UserControlsForm.MemoMainForm memoMainForm1;
        private System.Windows.Forms.Panel panelMemoDetail;
        private UserControlsForm.MemoDetailForm memoDetailForm1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
    }
}