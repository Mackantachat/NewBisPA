namespace NewBisPA
{
    partial class FormUnlockApp
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormUnlockApp));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnunlockall = new System.Windows.Forms.Button();
            this.dtgapplockdetail = new System.Windows.Forms.DataGridView();
            this.clmunlock = new System.Windows.Forms.DataGridViewImageColumn();
            this.clmappno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmusername = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmdate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmtelephone = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmuappid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmupdid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmupdateflg = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgapplockdetail)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(547, 181);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnunlockall);
            this.panel1.Controls.Add(this.dtgapplockdetail);
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(541, 175);
            this.panel1.TabIndex = 0;
            // 
            // btnunlockall
            // 
            this.btnunlockall.Enabled = false;
            this.btnunlockall.Location = new System.Drawing.Point(9, 9);
            this.btnunlockall.Name = "btnunlockall";
            this.btnunlockall.Size = new System.Drawing.Size(62, 13);
            this.btnunlockall.TabIndex = 1;
            this.btnunlockall.Text = "ปลดล็อคใบคำขอ";
            this.btnunlockall.UseVisualStyleBackColor = true;
            this.btnunlockall.Visible = false;
            this.btnunlockall.Click += new System.EventHandler(this.btnunlockall_Click);
            // 
            // dtgapplockdetail
            // 
            this.dtgapplockdetail.AllowUserToAddRows = false;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.BottomCenter;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dtgapplockdetail.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle8;
            this.dtgapplockdetail.ColumnHeadersHeight = 25;
            this.dtgapplockdetail.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clmunlock,
            this.clmappno,
            this.clmusername,
            this.clmdate,
            this.clmtelephone,
            this.clmuappid,
            this.clmupdid,
            this.clmupdateflg});
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle13.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            dataGridViewCellStyle13.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle13.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle13.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle13.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dtgapplockdetail.DefaultCellStyle = dataGridViewCellStyle13;
            this.dtgapplockdetail.Location = new System.Drawing.Point(3, 9);
            this.dtgapplockdetail.Name = "dtgapplockdetail";
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle14.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle14.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            dataGridViewCellStyle14.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle14.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle14.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle14.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dtgapplockdetail.RowHeadersDefaultCellStyle = dataGridViewCellStyle14;
            this.dtgapplockdetail.RowHeadersVisible = false;
            this.dtgapplockdetail.Size = new System.Drawing.Size(645, 367);
            this.dtgapplockdetail.TabIndex = 0;
            this.dtgapplockdetail.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtgapplockdetail_CellClick);
            // 
            // clmunlock
            // 
            this.clmunlock.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.clmunlock.HeaderText = "ปลดล็อคใบคำขอ";
            this.clmunlock.Image = ((System.Drawing.Image)(resources.GetObject("clmunlock.Image")));
            this.clmunlock.Name = "clmunlock";
            this.clmunlock.Width = 90;
            // 
            // clmappno
            // 
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.BottomCenter;
            this.clmappno.DefaultCellStyle = dataGridViewCellStyle9;
            this.clmappno.HeaderText = "ใบคำขอ";
            this.clmappno.Name = "clmappno";
            this.clmappno.Width = 75;
            // 
            // clmusername
            // 
            this.clmusername.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.BottomLeft;
            this.clmusername.DefaultCellStyle = dataGridViewCellStyle10;
            this.clmusername.HeaderText = "ผู้ล็อคใบคำขอ";
            this.clmusername.Name = "clmusername";
            this.clmusername.Width = 95;
            // 
            // clmdate
            // 
            this.clmdate.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.BottomCenter;
            this.clmdate.DefaultCellStyle = dataGridViewCellStyle11;
            this.clmdate.HeaderText = "วัน-เวลาที่ล็อค";
            this.clmdate.Name = "clmdate";
            this.clmdate.Width = 97;
            // 
            // clmtelephone
            // 
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.BottomCenter;
            this.clmtelephone.DefaultCellStyle = dataGridViewCellStyle12;
            this.clmtelephone.HeaderText = "เบอร์โทรศัพท์";
            this.clmtelephone.Name = "clmtelephone";
            // 
            // clmuappid
            // 
            this.clmuappid.HeaderText = "uappid";
            this.clmuappid.Name = "clmuappid";
            this.clmuappid.Visible = false;
            // 
            // clmupdid
            // 
            this.clmupdid.HeaderText = "updid";
            this.clmupdid.Name = "clmupdid";
            this.clmupdid.Visible = false;
            // 
            // clmupdateflg
            // 
            this.clmupdateflg.HeaderText = "updateflg";
            this.clmupdateflg.Name = "clmupdateflg";
            this.clmupdateflg.Visible = false;
            // 
            // FormUnlockApp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(547, 181);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormUnlockApp";
            this.Text = "NewBis : ปลดล็อคใบคำขอ";
            this.Load += new System.EventHandler(this.FormUnlockApp_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtgapplockdetail)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dtgapplockdetail;
        private System.Windows.Forms.Button btnunlockall;
        private System.Windows.Forms.DataGridViewImageColumn clmunlock;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmappno;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmusername;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmdate;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmtelephone;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmuappid;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmupdid;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmupdateflg;
    }
}