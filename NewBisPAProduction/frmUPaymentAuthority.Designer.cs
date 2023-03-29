namespace NewBisPA
{
    partial class frmUPaymentAuthority
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmUPaymentAuthority));
            this.txtPasswordAppr = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.userFullname = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtPasswordAppr
            // 
            this.txtPasswordAppr.Location = new System.Drawing.Point(115, 60);
            this.txtPasswordAppr.Name = "txtPasswordAppr";
            this.txtPasswordAppr.PasswordChar = '*';
            this.txtPasswordAppr.Size = new System.Drawing.Size(174, 20);
            this.txtPasswordAppr.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(63, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "รหัสผ่าน :";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(154, 86);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 4;
            this.btnOK.Text = "ตกลง";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(67, 35);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "ผู้อนุมัติ :";
            // 
            // userFullname
            // 
            this.userFullname.AutoSize = true;
            this.userFullname.Location = new System.Drawing.Point(112, 35);
            this.userFullname.Name = "userFullname";
            this.userFullname.Size = new System.Drawing.Size(42, 13);
            this.userFullname.TabIndex = 7;
            this.userFullname.Text = "ผู้อนุมัติ";
            // 
            // frmUPaymentAuthority
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(383, 119);
            this.Controls.Add(this.userFullname);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtPasswordAppr);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmUPaymentAuthority";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ยืนยันสิทธิ์การอนุมัติ";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmUPaymentAuthority_FormClosed);
            this.Load += new System.EventHandler(this.frmUPaymentAuthority_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtPasswordAppr;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label userFullname;
    }
}