namespace NewBisPA.UserControlsForm
{
    partial class HistoryForm
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtSeq = new System.Windows.Forms.TextBox();
            this.txtStatus = new System.Windows.Forms.TextBox();
            this.txtSubStatus = new System.Windows.Forms.TextBox();
            this.txtStatusCause = new System.Windows.Forms.TextBox();
            this.txtUndID = new System.Windows.Forms.TextBox();
            this.txtFSystemDate = new System.Windows.Forms.TextBox();
            this.txtUpdID = new System.Windows.Forms.TextBox();
            this.txtLetterNo = new System.Windows.Forms.TextBox();
            this.btnDetail = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtSeq
            // 
            this.txtSeq.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtSeq.Location = new System.Drawing.Point(2, 2);
            this.txtSeq.Name = "txtSeq";
            this.txtSeq.ReadOnly = true;
            this.txtSeq.Size = new System.Drawing.Size(50, 23);
            this.txtSeq.TabIndex = 0;
            this.txtSeq.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtStatus
            // 
            this.txtStatus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtStatus.Location = new System.Drawing.Point(53, 2);
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.ReadOnly = true;
            this.txtStatus.Size = new System.Drawing.Size(180, 23);
            this.txtStatus.TabIndex = 1;
            // 
            // txtSubStatus
            // 
            this.txtSubStatus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtSubStatus.Location = new System.Drawing.Point(234, 2);
            this.txtSubStatus.Name = "txtSubStatus";
            this.txtSubStatus.ReadOnly = true;
            this.txtSubStatus.Size = new System.Drawing.Size(180, 23);
            this.txtSubStatus.TabIndex = 2;
            // 
            // txtStatusCause
            // 
            this.txtStatusCause.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtStatusCause.Location = new System.Drawing.Point(415, 2);
            this.txtStatusCause.Name = "txtStatusCause";
            this.txtStatusCause.ReadOnly = true;
            this.txtStatusCause.Size = new System.Drawing.Size(180, 23);
            this.txtStatusCause.TabIndex = 3;
            // 
            // txtUndID
            // 
            this.txtUndID.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtUndID.Location = new System.Drawing.Point(596, 2);
            this.txtUndID.Name = "txtUndID";
            this.txtUndID.ReadOnly = true;
            this.txtUndID.Size = new System.Drawing.Size(150, 23);
            this.txtUndID.TabIndex = 4;
            // 
            // txtFSystemDate
            // 
            this.txtFSystemDate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtFSystemDate.Location = new System.Drawing.Point(747, 2);
            this.txtFSystemDate.Name = "txtFSystemDate";
            this.txtFSystemDate.ReadOnly = true;
            this.txtFSystemDate.Size = new System.Drawing.Size(147, 23);
            this.txtFSystemDate.TabIndex = 5;
            this.txtFSystemDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtUpdID
            // 
            this.txtUpdID.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtUpdID.Location = new System.Drawing.Point(895, 2);
            this.txtUpdID.Name = "txtUpdID";
            this.txtUpdID.ReadOnly = true;
            this.txtUpdID.Size = new System.Drawing.Size(150, 23);
            this.txtUpdID.TabIndex = 6;
            // 
            // txtLetterNo
            // 
            this.txtLetterNo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtLetterNo.Location = new System.Drawing.Point(1046, 2);
            this.txtLetterNo.Name = "txtLetterNo";
            this.txtLetterNo.ReadOnly = true;
            this.txtLetterNo.Size = new System.Drawing.Size(128, 23);
            this.txtLetterNo.TabIndex = 7;
            this.txtLetterNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnDetail
            // 
            this.btnDetail.Location = new System.Drawing.Point(1178, 2);
            this.btnDetail.Name = "btnDetail";
            this.btnDetail.Size = new System.Drawing.Size(82, 23);
            this.btnDetail.TabIndex = 8;
            this.btnDetail.Text = "รายละเอียด";
            this.btnDetail.UseVisualStyleBackColor = true;
            // 
            // HistoryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.btnDetail);
            this.Controls.Add(this.txtLetterNo);
            this.Controls.Add(this.txtUpdID);
            this.Controls.Add(this.txtFSystemDate);
            this.Controls.Add(this.txtUndID);
            this.Controls.Add(this.txtStatusCause);
            this.Controls.Add(this.txtSubStatus);
            this.Controls.Add(this.txtStatus);
            this.Controls.Add(this.txtSeq);
            this.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "HistoryForm";
            this.Size = new System.Drawing.Size(1263, 27);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtSeq;
        private System.Windows.Forms.TextBox txtStatus;
        private System.Windows.Forms.TextBox txtSubStatus;
        private System.Windows.Forms.TextBox txtStatusCause;
        private System.Windows.Forms.TextBox txtUndID;
        private System.Windows.Forms.TextBox txtFSystemDate;
        private System.Windows.Forms.TextBox txtUpdID;
        private System.Windows.Forms.TextBox txtLetterNo;
        private System.Windows.Forms.Button btnDetail;
    }
}
