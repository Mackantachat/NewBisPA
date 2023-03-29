namespace NewBisPA
{
    partial class PlanSearchForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PlanSearchForm));
            this.TxtKeyword = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSearch = new System.Windows.Forms.Button();
            this.dgvPlanSearch = new System.Windows.Forms.DataGridView();
            this.planRowNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.planCommand = new System.Windows.Forms.DataGridViewButtonColumn();
            this.planBlock = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.planCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.planCode2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.planTitle = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.planAgeMinMax = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.planEffDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.planSingleDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.planFree = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.planPlType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.planSingleMode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPlanSearch)).BeginInit();
            this.SuspendLayout();
            // 
            // TxtKeyword
            // 
            this.TxtKeyword.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.TxtKeyword.Location = new System.Drawing.Point(196, 23);
            this.TxtKeyword.Name = "TxtKeyword";
            this.TxtKeyword.Size = new System.Drawing.Size(335, 22);
            this.TxtKeyword.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label1.Location = new System.Drawing.Point(116, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 14);
            this.label1.TabIndex = 1;
            this.label1.Text = "ชื่อแบบประกัน";
            // 
            // btnSearch
            // 
            this.btnSearch.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnSearch.Location = new System.Drawing.Point(535, 23);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(57, 25);
            this.btnSearch.TabIndex = 2;
            this.btnSearch.Text = "ค้นหา";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // dgvPlanSearch
            // 
            this.dgvPlanSearch.AllowUserToAddRows = false;
            this.dgvPlanSearch.AllowUserToDeleteRows = false;
            this.dgvPlanSearch.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPlanSearch.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.planRowNo,
            this.planCommand,
            this.planBlock,
            this.planCode,
            this.planCode2,
            this.planTitle,
            this.planAgeMinMax,
            this.planEffDate,
            this.planSingleDescription,
            this.planFree,
            this.planPlType,
            this.planSingleMode});
            this.dgvPlanSearch.Location = new System.Drawing.Point(12, 62);
            this.dgvPlanSearch.Name = "dgvPlanSearch";
            this.dgvPlanSearch.ReadOnly = true;
            this.dgvPlanSearch.RowHeadersVisible = false;
            this.dgvPlanSearch.RowHeadersWidth = 60;
            this.dgvPlanSearch.Size = new System.Drawing.Size(918, 359);
            this.dgvPlanSearch.TabIndex = 3;
            this.dgvPlanSearch.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPlanSearch_CellClick);
            // 
            // planRowNo
            // 
            this.planRowNo.HeaderText = "ลำดับ";
            this.planRowNo.Name = "planRowNo";
            this.planRowNo.ReadOnly = true;
            this.planRowNo.Width = 60;
            // 
            // planCommand
            // 
            this.planCommand.HeaderText = "คำสั่ง";
            this.planCommand.Name = "planCommand";
            this.planCommand.ReadOnly = true;
            this.planCommand.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.planCommand.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.planCommand.Text = "เลือก";
            this.planCommand.UseColumnTextForButtonValue = true;
            this.planCommand.Width = 60;
            // 
            // planBlock
            // 
            this.planBlock.DataPropertyName = "PL_BLOCK";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.planBlock.DefaultCellStyle = dataGridViewCellStyle1;
            this.planBlock.HeaderText = "PL BLOCK";
            this.planBlock.Name = "planBlock";
            this.planBlock.ReadOnly = true;
            this.planBlock.Width = 90;
            // 
            // planCode
            // 
            this.planCode.DataPropertyName = "PL_CODE";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.planCode.DefaultCellStyle = dataGridViewCellStyle2;
            this.planCode.HeaderText = "CODE";
            this.planCode.Name = "planCode";
            this.planCode.ReadOnly = true;
            this.planCode.Width = 50;
            // 
            // planCode2
            // 
            this.planCode2.DataPropertyName = "PL_CODE2";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.planCode2.DefaultCellStyle = dataGridViewCellStyle3;
            this.planCode2.HeaderText = "CODE2";
            this.planCode2.Name = "planCode2";
            this.planCode2.ReadOnly = true;
            this.planCode2.Width = 60;
            // 
            // planTitle
            // 
            this.planTitle.DataPropertyName = "BLA_TITLE";
            this.planTitle.HeaderText = "ชื่อแบบประกัน";
            this.planTitle.Name = "planTitle";
            this.planTitle.ReadOnly = true;
            this.planTitle.Width = 300;
            // 
            // planAgeMinMax
            // 
            this.planAgeMinMax.DataPropertyName = "AGE_MIN_MAX";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.planAgeMinMax.DefaultCellStyle = dataGridViewCellStyle4;
            this.planAgeMinMax.HeaderText = "อายุรับประกัน";
            this.planAgeMinMax.Name = "planAgeMinMax";
            this.planAgeMinMax.ReadOnly = true;
            // 
            // planEffDate
            // 
            this.planEffDate.DataPropertyName = "EFF_DT";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.Format = "d";
            dataGridViewCellStyle5.NullValue = null;
            this.planEffDate.DefaultCellStyle = dataGridViewCellStyle5;
            this.planEffDate.HeaderText = "วันที่ EFF DATE";
            this.planEffDate.Name = "planEffDate";
            this.planEffDate.ReadOnly = true;
            this.planEffDate.Width = 120;
            // 
            // planSingleDescription
            // 
            this.planSingleDescription.DataPropertyName = "SINGLE_DESCRIPTION";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.planSingleDescription.DefaultCellStyle = dataGridViewCellStyle6;
            this.planSingleDescription.HeaderText = "การชำระเบี้ย";
            this.planSingleDescription.Name = "planSingleDescription";
            this.planSingleDescription.ReadOnly = true;
            this.planSingleDescription.Width = 200;
            // 
            // planFree
            // 
            this.planFree.DataPropertyName = "FREE_FLG";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.planFree.DefaultCellStyle = dataGridViewCellStyle7;
            this.planFree.HeaderText = "แถมฟรี";
            this.planFree.Name = "planFree";
            this.planFree.ReadOnly = true;
            this.planFree.Width = 70;
            // 
            // planPlType
            // 
            this.planPlType.DataPropertyName = "PL_TYPE";
            this.planPlType.HeaderText = "PL_TYPE";
            this.planPlType.Name = "planPlType";
            this.planPlType.ReadOnly = true;
            this.planPlType.Visible = false;
            // 
            // planSingleMode
            // 
            this.planSingleMode.DataPropertyName = "SINGLE_MODE";
            this.planSingleMode.HeaderText = "SingleMode";
            this.planSingleMode.Name = "planSingleMode";
            this.planSingleMode.ReadOnly = true;
            this.planSingleMode.Visible = false;
            // 
            // PlanSearchForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(942, 433);
            this.Controls.Add(this.dgvPlanSearch);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.TxtKeyword);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "PlanSearchForm";
            this.Text = "ค้นหาแบบประกัน";
            this.Load += new System.EventHandler(this.PlanSearchForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPlanSearch)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox TxtKeyword;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.DataGridView dgvPlanSearch;
        private System.Windows.Forms.DataGridViewTextBoxColumn planRowNo;
        private System.Windows.Forms.DataGridViewButtonColumn planCommand;
        private System.Windows.Forms.DataGridViewTextBoxColumn planBlock;
        private System.Windows.Forms.DataGridViewTextBoxColumn planCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn planCode2;
        private System.Windows.Forms.DataGridViewTextBoxColumn planTitle;
        private System.Windows.Forms.DataGridViewTextBoxColumn planAgeMinMax;
        private System.Windows.Forms.DataGridViewTextBoxColumn planEffDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn planSingleDescription;
        private System.Windows.Forms.DataGridViewTextBoxColumn planFree;
        private System.Windows.Forms.DataGridViewTextBoxColumn planPlType;
        private System.Windows.Forms.DataGridViewTextBoxColumn planSingleMode;
    }
}