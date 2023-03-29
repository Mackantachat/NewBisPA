namespace NewBisPA
{
    partial class ShowOldAdrressByNameIDForm
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
            this.panelOldAddress = new System.Windows.Forms.Panel();
            this.lblTotalAmount = new System.Windows.Forms.Label();
            this.oldAddressDataFormItem = new NewBisPA.UserControlsForm.OldAddressDataForm();
            this.panelOldAddress.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelOldAddress
            // 
            this.panelOldAddress.AutoScroll = true;
            this.panelOldAddress.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.panelOldAddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelOldAddress.Controls.Add(this.oldAddressDataFormItem);
            this.panelOldAddress.Location = new System.Drawing.Point(6, 29);
            this.panelOldAddress.Name = "panelOldAddress";
            this.panelOldAddress.Size = new System.Drawing.Size(755, 290);
            this.panelOldAddress.TabIndex = 0;
            // 
            // lblTotalAmount
            // 
            this.lblTotalAmount.AutoSize = true;
            this.lblTotalAmount.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lblTotalAmount.ForeColor = System.Drawing.Color.Red;
            this.lblTotalAmount.Location = new System.Drawing.Point(25, 7);
            this.lblTotalAmount.Name = "lblTotalAmount";
            this.lblTotalAmount.Size = new System.Drawing.Size(46, 16);
            this.lblTotalAmount.TabIndex = 1;
            this.lblTotalAmount.Text = "label1";
            // 
            // oldAddressDataFormItem
            // 
            this.oldAddressDataFormItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.oldAddressDataFormItem.Location = new System.Drawing.Point(3, 2);
            this.oldAddressDataFormItem.Name = "oldAddressDataFormItem";
            this.oldAddressDataFormItem.Size = new System.Drawing.Size(730, 140);
            this.oldAddressDataFormItem.TabIndex = 0;
            // 
            // ShowOldAdrressByNameIDForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SkyBlue;
            this.ClientSize = new System.Drawing.Size(768, 326);
            this.Controls.Add(this.lblTotalAmount);
            this.Controls.Add(this.panelOldAddress);
            this.Name = "ShowOldAdrressByNameIDForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "รายการข้อมูลเดิมของลูกค้า";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ShowOldAdrressByNameIDForm_FormClosing);
            this.Load += new System.EventHandler(this.ShowOldAdrressByNameIDForm_Load);
            this.panelOldAddress.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelOldAddress;
        private UserControlsForm.OldAddressDataForm oldAddressDataFormItem;
        private System.Windows.Forms.Label lblTotalAmount;
    }
}