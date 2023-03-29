namespace NewBisPA.UserControlsForm
{
    partial class DocumentMemoForm
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
            this.ckbMemoDocument = new System.Windows.Forms.CheckBox();
            this.txtDocumentCode = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // ckbMemoDocument
            // 
            this.ckbMemoDocument.AutoSize = true;
            this.ckbMemoDocument.Location = new System.Drawing.Point(3, 3);
            this.ckbMemoDocument.Name = "ckbMemoDocument";
            this.ckbMemoDocument.Size = new System.Drawing.Size(67, 20);
            this.ckbMemoDocument.TabIndex = 0;
            this.ckbMemoDocument.Text = "เอกสาร";
            this.ckbMemoDocument.UseVisualStyleBackColor = true;
            // 
            // txtDocumentCode
            // 
            this.txtDocumentCode.Location = new System.Drawing.Point(220, 0);
            this.txtDocumentCode.Name = "txtDocumentCode";
            this.txtDocumentCode.ReadOnly = true;
            this.txtDocumentCode.Size = new System.Drawing.Size(20, 23);
            this.txtDocumentCode.TabIndex = 1;
            this.txtDocumentCode.Visible = false;
            // 
            // DocumentMemoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.Controls.Add(this.txtDocumentCode);
            this.Controls.Add(this.ckbMemoDocument);
            this.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "DocumentMemoForm";
            this.Size = new System.Drawing.Size(685, 23);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox ckbMemoDocument;
        private System.Windows.Forms.TextBox txtDocumentCode;
    }
}
