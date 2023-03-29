namespace NewBisPA.ReportForm
{
    partial class DlgEditLetter
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle34 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DlgEditLetter));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle35 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle36 = new System.Windows.Forms.DataGridViewCellStyle();
            this.cmbemp = new System.Windows.Forms.ComboBox();
            this.dtgsend = new System.Windows.Forms.DataGridView();
            this.clmremove = new System.Windows.Forms.DataGridViewImageColumn();
            this.clmsendto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmsendby = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmsendtocode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label2 = new System.Windows.Forms.Label();
            this.lblqty = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbsendto = new System.Windows.Forms.ComboBox();
            this.cmbsendby = new System.Windows.Forms.ComboBox();
            this.btnadd = new System.Windows.Forms.Button();
            this.chkaddsend = new System.Windows.Forms.CheckBox();
            this.chksignature = new System.Windows.Forms.CheckBox();
            this.chklimitdate = new System.Windows.Forms.CheckBox();
            this.txtlimitdate = new System.Windows.Forms.TextBox();
            this.ckbReturnPrmNewApp = new System.Windows.Forms.CheckBox();
            this.gbReturnPrmNewApp = new System.Windows.Forms.GroupBox();
            this.pnlnewapp = new System.Windows.Forms.Panel();
            this.txtNewAppSurname = new System.Windows.Forms.TextBox();
            this.txtNewAppName = new System.Windows.Forms.TextBox();
            this.txtNewAppNo = new System.Windows.Forms.TextBox();
            this.txtNewAppTrnDate = new System.Windows.Forms.TextBox();
            this.label243 = new System.Windows.Forms.Label();
            this.label255 = new System.Windows.Forms.Label();
            this.label261 = new System.Windows.Forms.Label();
            this.label260 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dtgsend)).BeginInit();
            this.gbReturnPrmNewApp.SuspendLayout();
            this.pnlnewapp.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmbemp
            // 
            this.cmbemp.Enabled = false;
            this.cmbemp.FormattingEnabled = true;
            this.cmbemp.Location = new System.Drawing.Point(121, 12);
            this.cmbemp.Name = "cmbemp";
            this.cmbemp.Size = new System.Drawing.Size(227, 21);
            this.cmbemp.TabIndex = 1;
            // 
            // dtgsend
            // 
            this.dtgsend.AllowUserToAddRows = false;
            dataGridViewCellStyle34.Alignment = System.Windows.Forms.DataGridViewContentAlignment.BottomCenter;
            dataGridViewCellStyle34.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle34.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            dataGridViewCellStyle34.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle34.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle34.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle34.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dtgsend.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle34;
            this.dtgsend.ColumnHeadersHeight = 20;
            this.dtgsend.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clmremove,
            this.clmsendto,
            this.clmsendby,
            this.clmsendtocode});
            this.dtgsend.Location = new System.Drawing.Point(12, 338);
            this.dtgsend.Name = "dtgsend";
            this.dtgsend.Size = new System.Drawing.Size(397, 114);
            this.dtgsend.TabIndex = 2;
            this.dtgsend.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtgsend_CellClick);
            // 
            // clmremove
            // 
            this.clmremove.Frozen = true;
            this.clmremove.HeaderText = "ยกเลิก";
            this.clmremove.Image = ((System.Drawing.Image)(resources.GetObject("clmremove.Image")));
            this.clmremove.Name = "clmremove";
            this.clmremove.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.clmremove.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.clmremove.Width = 45;
            // 
            // clmsendto
            // 
            this.clmsendto.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridViewCellStyle35.Alignment = System.Windows.Forms.DataGridViewContentAlignment.BottomCenter;
            this.clmsendto.DefaultCellStyle = dataGridViewCellStyle35;
            this.clmsendto.HeaderText = "ส่งให้";
            this.clmsendto.Name = "clmsendto";
            this.clmsendto.Width = 56;
            // 
            // clmsendby
            // 
            this.clmsendby.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridViewCellStyle36.Alignment = System.Windows.Forms.DataGridViewContentAlignment.BottomCenter;
            this.clmsendby.DefaultCellStyle = dataGridViewCellStyle36;
            this.clmsendby.HeaderText = "วิธีการส่ง";
            this.clmsendby.Name = "clmsendby";
            this.clmsendby.Width = 73;
            // 
            // clmsendtocode
            // 
            this.clmsendtocode.HeaderText = "clmsendtocode";
            this.clmsendtocode.Name = "clmsendtocode";
            this.clmsendtocode.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 455);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "จำนวนที่ส่ง";
            // 
            // lblqty
            // 
            this.lblqty.AutoSize = true;
            this.lblqty.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lblqty.ForeColor = System.Drawing.Color.DodgerBlue;
            this.lblqty.Location = new System.Drawing.Point(76, 455);
            this.lblqty.Name = "lblqty";
            this.lblqty.Size = new System.Drawing.Size(0, 13);
            this.lblqty.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(96, 455);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "ฉบับ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(30, 290);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(37, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "ส่งให้ :";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(30, 317);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(54, 13);
            this.label5.TabIndex = 2;
            this.label5.Text = "วิธีการส่ง :";
            // 
            // cmbsendto
            // 
            this.cmbsendto.Enabled = false;
            this.cmbsendto.FormattingEnabled = true;
            this.cmbsendto.Location = new System.Drawing.Point(90, 282);
            this.cmbsendto.Name = "cmbsendto";
            this.cmbsendto.Size = new System.Drawing.Size(227, 21);
            this.cmbsendto.TabIndex = 3;
            // 
            // cmbsendby
            // 
            this.cmbsendby.Enabled = false;
            this.cmbsendby.FormattingEnabled = true;
            this.cmbsendby.Location = new System.Drawing.Point(90, 309);
            this.cmbsendby.Name = "cmbsendby";
            this.cmbsendby.Size = new System.Drawing.Size(227, 21);
            this.cmbsendby.TabIndex = 4;
            // 
            // btnadd
            // 
            this.btnadd.Location = new System.Drawing.Point(313, 57);
            this.btnadd.Name = "btnadd";
            this.btnadd.Size = new System.Drawing.Size(86, 23);
            this.btnadd.TabIndex = 7;
            this.btnadd.Text = "บันทึกข้อมูล";
            this.btnadd.UseVisualStyleBackColor = true;
            this.btnadd.Click += new System.EventHandler(this.btnadd_Click);
            // 
            // chkaddsend
            // 
            this.chkaddsend.AutoSize = true;
            this.chkaddsend.Location = new System.Drawing.Point(14, 259);
            this.chkaddsend.Name = "chkaddsend";
            this.chkaddsend.Size = new System.Drawing.Size(105, 17);
            this.chkaddsend.TabIndex = 8;
            this.chkaddsend.Text = "เพิ่มผู้รับจดหมาย";
            this.chkaddsend.UseVisualStyleBackColor = true;
            this.chkaddsend.CheckedChanged += new System.EventHandler(this.chkaddsend_CheckedChanged);
            // 
            // chksignature
            // 
            this.chksignature.AutoSize = true;
            this.chksignature.Location = new System.Drawing.Point(12, 16);
            this.chksignature.Name = "chksignature";
            this.chksignature.Size = new System.Drawing.Size(103, 17);
            this.chksignature.TabIndex = 9;
            this.chksignature.Text = "ลายเซ็นจดหมาย";
            this.chksignature.UseVisualStyleBackColor = true;
            this.chksignature.CheckedChanged += new System.EventHandler(this.chksignature_CheckedChanged);
            // 
            // chklimitdate
            // 
            this.chklimitdate.AutoSize = true;
            this.chklimitdate.Enabled = false;
            this.chklimitdate.Location = new System.Drawing.Point(12, 39);
            this.chklimitdate.Name = "chklimitdate";
            this.chklimitdate.Size = new System.Drawing.Size(87, 17);
            this.chklimitdate.TabIndex = 10;
            this.chklimitdate.Text = "วันที่ตอบกลับ";
            this.chklimitdate.UseVisualStyleBackColor = true;
            this.chklimitdate.CheckedChanged += new System.EventHandler(this.chklimitdate_CheckedChanged);
            // 
            // txtlimitdate
            // 
            this.txtlimitdate.Enabled = false;
            this.txtlimitdate.Location = new System.Drawing.Point(121, 36);
            this.txtlimitdate.Name = "txtlimitdate";
            this.txtlimitdate.Size = new System.Drawing.Size(126, 20);
            this.txtlimitdate.TabIndex = 11;
            this.txtlimitdate.Leave += new System.EventHandler(this.txtlimitdate_Leave);
            // 
            // ckbReturnPrmNewApp
            // 
            this.ckbReturnPrmNewApp.AutoSize = true;
            this.ckbReturnPrmNewApp.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.ckbReturnPrmNewApp.Location = new System.Drawing.Point(12, 70);
            this.ckbReturnPrmNewApp.Name = "ckbReturnPrmNewApp";
            this.ckbReturnPrmNewApp.Size = new System.Drawing.Size(201, 17);
            this.ckbReturnPrmNewApp.TabIndex = 886;
            this.ckbReturnPrmNewApp.Text = "ต้องการบันทึกโอนเงินเข้า ใบคำขอ อื่น";
            this.ckbReturnPrmNewApp.UseVisualStyleBackColor = true;
            this.ckbReturnPrmNewApp.CheckedChanged += new System.EventHandler(this.ckbReturnPrmNewApp_CheckedChanged);
            // 
            // gbReturnPrmNewApp
            // 
            this.gbReturnPrmNewApp.Controls.Add(this.pnlnewapp);
            this.gbReturnPrmNewApp.Enabled = false;
            this.gbReturnPrmNewApp.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.gbReturnPrmNewApp.Location = new System.Drawing.Point(24, 94);
            this.gbReturnPrmNewApp.Name = "gbReturnPrmNewApp";
            this.gbReturnPrmNewApp.Size = new System.Drawing.Size(375, 159);
            this.gbReturnPrmNewApp.TabIndex = 887;
            this.gbReturnPrmNewApp.TabStop = false;
            this.gbReturnPrmNewApp.Text = "บันทึกโอนเงินเข้า ใบคำขอ อื่น";
            // 
            // pnlnewapp
            // 
            this.pnlnewapp.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.pnlnewapp.Controls.Add(this.txtNewAppSurname);
            this.pnlnewapp.Controls.Add(this.txtNewAppName);
            this.pnlnewapp.Controls.Add(this.txtNewAppNo);
            this.pnlnewapp.Controls.Add(this.txtNewAppTrnDate);
            this.pnlnewapp.Controls.Add(this.label243);
            this.pnlnewapp.Controls.Add(this.label255);
            this.pnlnewapp.Controls.Add(this.label261);
            this.pnlnewapp.Controls.Add(this.label260);
            this.pnlnewapp.Location = new System.Drawing.Point(7, 19);
            this.pnlnewapp.Name = "pnlnewapp";
            this.pnlnewapp.Size = new System.Drawing.Size(360, 129);
            this.pnlnewapp.TabIndex = 886;
            // 
            // txtNewAppSurname
            // 
            this.txtNewAppSurname.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtNewAppSurname.Location = new System.Drawing.Point(132, 91);
            this.txtNewAppSurname.MaxLength = 50;
            this.txtNewAppSurname.Name = "txtNewAppSurname";
            this.txtNewAppSurname.Size = new System.Drawing.Size(176, 22);
            this.txtNewAppSurname.TabIndex = 890;
            // 
            // txtNewAppName
            // 
            this.txtNewAppName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtNewAppName.Location = new System.Drawing.Point(132, 65);
            this.txtNewAppName.MaxLength = 40;
            this.txtNewAppName.Name = "txtNewAppName";
            this.txtNewAppName.Size = new System.Drawing.Size(175, 22);
            this.txtNewAppName.TabIndex = 889;
            // 
            // txtNewAppNo
            // 
            this.txtNewAppNo.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtNewAppNo.Location = new System.Drawing.Point(132, 13);
            this.txtNewAppNo.MaxLength = 7;
            this.txtNewAppNo.Name = "txtNewAppNo";
            this.txtNewAppNo.Size = new System.Drawing.Size(107, 22);
            this.txtNewAppNo.TabIndex = 887;
            this.txtNewAppNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtNewAppTrnDate
            // 
            this.txtNewAppTrnDate.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtNewAppTrnDate.Location = new System.Drawing.Point(132, 39);
            this.txtNewAppTrnDate.MaxLength = 10;
            this.txtNewAppTrnDate.Name = "txtNewAppTrnDate";
            this.txtNewAppTrnDate.Size = new System.Drawing.Size(107, 22);
            this.txtNewAppTrnDate.TabIndex = 888;
            this.txtNewAppTrnDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtNewAppTrnDate.Leave += new System.EventHandler(this.txtNewAppTrnDate_Leave);
            // 
            // label243
            // 
            this.label243.AutoSize = true;
            this.label243.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.label243.Location = new System.Drawing.Point(39, 68);
            this.label243.Name = "label243";
            this.label243.Size = new System.Drawing.Size(84, 13);
            this.label243.TabIndex = 167;
            this.label243.Text = "ชื่อผู้ขอทำประกัน";
            // 
            // label255
            // 
            this.label255.AutoSize = true;
            this.label255.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.label255.Location = new System.Drawing.Point(55, 16);
            this.label255.Name = "label255";
            this.label255.Size = new System.Drawing.Size(68, 13);
            this.label255.TabIndex = 168;
            this.label255.Text = "เลขที่ใบคำขอ";
            // 
            // label261
            // 
            this.label261.AutoSize = true;
            this.label261.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.label261.Location = new System.Drawing.Point(55, 43);
            this.label261.Name = "label261";
            this.label261.Size = new System.Drawing.Size(65, 13);
            this.label261.TabIndex = 170;
            this.label261.Text = "วันที่โอนเงิน";
            // 
            // label260
            // 
            this.label260.AutoSize = true;
            this.label260.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.label260.Location = new System.Drawing.Point(13, 95);
            this.label260.Name = "label260";
            this.label260.Size = new System.Drawing.Size(110, 13);
            this.label260.TabIndex = 169;
            this.label260.Text = "นามสกุลผู้ขอทำประกัน";
            // 
            // DlgEditLetter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(421, 474);
            this.Controls.Add(this.ckbReturnPrmNewApp);
            this.Controls.Add(this.gbReturnPrmNewApp);
            this.Controls.Add(this.txtlimitdate);
            this.Controls.Add(this.chklimitdate);
            this.Controls.Add(this.chksignature);
            this.Controls.Add(this.chkaddsend);
            this.Controls.Add(this.cmbsendby);
            this.Controls.Add(this.btnadd);
            this.Controls.Add(this.cmbsendto);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblqty);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dtgsend);
            this.Controls.Add(this.cmbemp);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "DlgEditLetter";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "แก้ไขรายละเอียดจดหมาย";
            this.Load += new System.EventHandler(this.DlgEditLetter_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtgsend)).EndInit();
            this.gbReturnPrmNewApp.ResumeLayout(false);
            this.pnlnewapp.ResumeLayout(false);
            this.pnlnewapp.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbemp;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblqty;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView dtgsend;
        private System.Windows.Forms.Label lblusername;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbsendby;
        private System.Windows.Forms.ComboBox cmbsendto;
        private System.Windows.Forms.Button btnadd;
        private System.Windows.Forms.CheckBox chkaddsend;
        private System.Windows.Forms.DataGridViewImageColumn clmremove;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmsendto;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmsendby;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmsendtocode;
        private System.Windows.Forms.CheckBox chksignature;
        private System.Windows.Forms.CheckBox chklimitdate;
        private System.Windows.Forms.TextBox txtlimitdate;
        private System.Windows.Forms.CheckBox ckbReturnPrmNewApp;
        private System.Windows.Forms.GroupBox gbReturnPrmNewApp;
        private System.Windows.Forms.Panel pnlnewapp;
        private System.Windows.Forms.TextBox txtNewAppSurname;
        private System.Windows.Forms.TextBox txtNewAppName;
        private System.Windows.Forms.TextBox txtNewAppNo;
        private System.Windows.Forms.TextBox txtNewAppTrnDate;
        private System.Windows.Forms.Label label243;
        private System.Windows.Forms.Label label255;
        private System.Windows.Forms.Label label261;
        private System.Windows.Forms.Label label260;
    }
}