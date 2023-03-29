using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ITUtility;

namespace NewBisPA
{
    public partial class FormUAppRemark : Form
    {

        private NewBisPASvcRef.U_APP_REMARK _PAR_U_APP_REMARK;

        public NewBisPASvcRef.U_APP_REMARK PAR_U_APP_REMARK
        {
            get { return _PAR_U_APP_REMARK; }
            set { _PAR_U_APP_REMARK = value; }
        }

        private NewBisPASvcRef.U_APP_REMARK_Collection _PAR_U_APP_REMARK_COLL;

        public NewBisPASvcRef.U_APP_REMARK_Collection PAR_U_APP_REMARK_COLL
        {
            get { return _PAR_U_APP_REMARK_COLL; }
            set { _PAR_U_APP_REMARK_COLL = value; }
        }

        private String _USER_ID;

        public String USER_ID
        {
            get { return _USER_ID; }
            set { _USER_ID = value; }
        }
        private String PAR_ACTION;
        public FormUAppRemark()
        {
            InitializeComponent();
        }

        private void FormUAppRemark_Load(object sender, EventArgs e)
        {
            try
            {
                PAR_ACTION = "INSERT";
                PAR_U_APP_REMARK = new NewBisPASvcRef.U_APP_REMARK();
                var pr = new NewBISSvcRef.ProcessResult();
                NewBISSvcRef.AUTB_DATADIC_DET_COLLECTION problemColl = new NewBISSvcRef.AUTB_DATADIC_DET_COLLECTION();
                using (var client = new NewBISSvcRef.NewBISSvcClient())
                {
                    problemColl = client.GetAutbDataDicDets(out pr, "PROBLEM_CLEARING");
                    if (pr.Successed == false)
                    {
                        throw new Exception(pr.ErrorMessage);
                    }
                }
                if (problemColl != null && problemColl.Count() > 0)
                {
                    var GetData = from getData in problemColl
                                  //where getData.CODE != "X"
                                  select getData;
                    if (GetData != null && GetData.Count() > 0)
                    {
                        problemColl = new NewBISSvcRef.AUTB_DATADIC_DET_COLLECTION();
                        problemColl.AddRange(GetData.ToArray());
                        cmbProblem.DataSource = problemColl;
                        cmbProblem.DisplayMember = "DESCRIPTION";
                        cmbProblem.ValueMember = "CODE";
                        cmbProblem.SelectedValue = "D";
                    }

                }
                txtRemark.Text = "";
                btnAppRemarkSave.Text = "เพิ่มข้อมูล";
                CreateControlsInPanelAppRemark();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnAppRemarkSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtRemark.Text.Trim() == "")
                {
                    txtRemark.Focus();
                    throw new Exception("กรุณาระบุหมายเหตุ");
                }

                if (PAR_ACTION == "INSERT")
                {
                    PAR_U_APP_REMARK = new NewBisPASvcRef.U_APP_REMARK();
                    PAR_U_APP_REMARK.USERID = USER_ID;
                    PAR_U_APP_REMARK.FSYSTEM_DT = DateTime.Now;
                    PAR_U_APP_REMARK.PROBLEM_CLEARING = cmbProblem.SelectedValue == null ? 'D' : Convert.ToChar(cmbProblem.SelectedValue.ToString());
                    PAR_U_APP_REMARK.REMARK = txtRemark.Text.Trim();

                    if (PAR_U_APP_REMARK_COLL == null)
                    {
                        PAR_U_APP_REMARK_COLL = new NewBisPASvcRef.U_APP_REMARK_Collection();
                    }

                    PAR_U_APP_REMARK_COLL.Add(PAR_U_APP_REMARK);
                }
                else if (PAR_ACTION == "UPDATE")
                {
                    PAR_U_APP_REMARK.PROBLEM_CLEARING = cmbProblem.SelectedValue == null ? 'D' : Convert.ToChar(cmbProblem.SelectedValue.ToString());
                    PAR_U_APP_REMARK.REMARK = txtRemark.Text.Trim();
                }

                ClearKeyInput();

                CreateControlsInPanelAppRemark();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ClearKeyInput()
        {
            //set ค่าหลังการบันทึก =============================================
            PAR_ACTION = "INSERT";
            cmbProblem.SelectedValue = "D";
            txtRemark.Text = "";
            btnAppRemarkSave.Text = "เพิ่มข้อมูล";
            //จบ set ค่าหลังการบันทึก ==========================================
        }

        private void CreateControlsInPanelAppRemark()
        {
            //สร้างรายการแสดงข้อมูล ============================================
            if (PAR_U_APP_REMARK_COLL != null && PAR_U_APP_REMARK_COLL.Count() > 0)
            {
                var UAppRemark =
                    from uAppRemark in PAR_U_APP_REMARK_COLL
                    orderby uAppRemark.FSYSTEM_DT descending
                    select uAppRemark;
                if (UAppRemark != null && UAppRemark.Count() > 0)
                {
                    while (panelAppRemark.Controls.Count > 0)
                    {
                        panelAppRemark.Controls.RemoveAt(0);
                    }
                    int i = 0;
                    String userName = "";
                    String problem = "";
                    foreach (var obj in UAppRemark)
                    {

                        CenterSvcRef.ProcessResult pr = new CenterSvcRef.ProcessResult();
                        CenterSvcRef.User userObj = new CenterSvcRef.User();
                        using (CenterSvcRef.CenterServiceClient client = new CenterSvcRef.CenterServiceClient())
                        {
                            userObj = client.getUser(out pr, obj.USERID);
                            if (pr.Successed == false)
                            {
                                throw new Exception(pr.ErrorMessage);
                            }
                        }

                        if (userObj != null && userObj.UserID != null)
                        {
                            userName = "คุณ" + userObj.firstName + "  " + userObj.lastName;
                        }
                        else
                        {
                            userName = "";
                        }

                        problem = "";
                        foreach (NewBISSvcRef.AUTB_DATADIC_DET problemObj in cmbProblem.Items)
                        {
                            if (problemObj.CODE == obj.PROBLEM_CLEARING.ToString())
                            {
                                problem = problemObj.DESCRIPTION;
                                break;
                            }
                        }

                        Panel panelAppRemarkList = new Panel();
                        panelAppRemarkList.Name = "panelAppRemarkList";
                        panelAppRemarkList.Location = new Point(0, 0 + (i * 85));
                        panelAppRemarkList.Size = new Size(755, 85);
                        panelAppRemarkList.BorderStyle = BorderStyle.Fixed3D;
                        panelAppRemarkList.Tag = obj;

                        TextBox txtappRemarkNo = new TextBox();
                        txtappRemarkNo.Name = "txtappRemarkNo";
                        txtappRemarkNo.Location = new Point(2, 2);
                        txtappRemarkNo.Size = new Size(66, 22);
                        txtappRemarkNo.ReadOnly = true;
                        txtappRemarkNo.TextAlign = HorizontalAlignment.Center;
                        txtappRemarkNo.Text = (i + 1).ToString("n0");
                        txtappRemarkNo.Tag = obj;

                        TextBox txtAppRemarkUserID = new TextBox();
                        txtAppRemarkUserID.Name = "txtAppRemarkUserID";
                        txtAppRemarkUserID.Location = new Point(69, 2);
                        txtAppRemarkUserID.Size = new Size(213, 22);
                        txtAppRemarkUserID.ReadOnly = true;
                        txtAppRemarkUserID.TextAlign = HorizontalAlignment.Left;
                        txtAppRemarkUserID.Text = userName;
                        txtAppRemarkUserID.Tag = obj;

                        TextBox txtAppRemarkFSystemDate = new TextBox();
                        txtAppRemarkFSystemDate.Name = "txtAppRemarkFSystemDate";
                        txtAppRemarkFSystemDate.Location = new Point(283, 2);
                        txtAppRemarkFSystemDate.Size = new Size(144, 22);
                        txtAppRemarkFSystemDate.ReadOnly = true;
                        txtAppRemarkFSystemDate.TextAlign = HorizontalAlignment.Center;
                        txtAppRemarkFSystemDate.Text = obj.FSYSTEM_DT == null ? "" : Utility.dateTimeToString(obj.FSYSTEM_DT.Value, "dd/MM/yyyy hh:mi:ss", "BU");
                        txtAppRemarkFSystemDate.Tag = obj;

                        TextBox txtAppRemarkProblem = new TextBox();
                        txtAppRemarkProblem.Name = "txtAppRemarkProblem";
                        txtAppRemarkProblem.Location = new Point(428, 2);
                        txtAppRemarkProblem.Size = new Size(207, 22);
                        txtAppRemarkProblem.ReadOnly = true;
                        txtAppRemarkProblem.TextAlign = HorizontalAlignment.Center;
                        txtAppRemarkProblem.Text = problem;
                        txtAppRemarkProblem.Tag = obj;

                        Label lblAppRemark = new Label();
                        lblAppRemark.Name = "lblAppRemark";
                        lblAppRemark.Location = new Point(12, 39);
                        lblAppRemark.Size = new Size(54, 14);
                        lblAppRemark.Text = "หมายเหตุ";

                        TextBox txtAppRemarkRemark = new TextBox();
                        txtAppRemarkRemark.Name = "txtAppRemarkRemark";
                        txtAppRemarkRemark.Location = new Point(69, 25);
                        txtAppRemarkRemark.Size = new Size(677, 50);
                        txtAppRemarkRemark.Text = obj.REMARK;
                        txtAppRemarkRemark.ReadOnly = true;
                        txtAppRemarkRemark.Multiline = true;
                        txtAppRemarkRemark.ScrollBars = ScrollBars.Vertical;
                        txtAppRemarkRemark.Tag = obj;

                        Button btnAppRemarkEdit = new Button();
                        btnAppRemarkEdit.Name = "btnAppRemarkEdit";
                        btnAppRemarkEdit.Location = new Point(636, 2);
                        btnAppRemarkEdit.Size = new Size(72, 23);
                        btnAppRemarkEdit.Text = "แก้ไขข้อมูล";
                        btnAppRemarkEdit.Tag = obj;
                        btnAppRemarkEdit.Click += new EventHandler(btnAppRemarkEdit_Click);

                        Button btnAppRemarkDelete = new Button();
                        btnAppRemarkDelete.Name = "btnAppRemarkDelete";
                        btnAppRemarkDelete.Location = new Point(709, 2);
                        btnAppRemarkDelete.Size = new Size(39, 23);
                        btnAppRemarkDelete.Text = "ลบ";
                        btnAppRemarkDelete.Tag = obj;
                        btnAppRemarkDelete.Click += new EventHandler(btnAppRemarkDelete_Click);
                        if (obj.APP_ID == null)
                        {
                            btnAppRemarkDelete.Visible = true;
                        }
                        else
                        {
                            btnAppRemarkDelete.Visible = false;
                        }


                        panelAppRemark.Controls.Add(panelAppRemarkList);
                        panelAppRemarkList.Controls.Add(txtappRemarkNo);
                        panelAppRemarkList.Controls.Add(txtAppRemarkUserID);
                        panelAppRemarkList.Controls.Add(txtAppRemarkFSystemDate);
                        panelAppRemarkList.Controls.Add(txtAppRemarkProblem);
                        panelAppRemarkList.Controls.Add(lblAppRemark);
                        panelAppRemarkList.Controls.Add(txtAppRemarkRemark);
                        panelAppRemarkList.Controls.Add(btnAppRemarkEdit);
                        panelAppRemarkList.Controls.Add(btnAppRemarkDelete);

                        i = i + 1;
                    }
                }
            }
            else
            {
                panelAppRemark.Controls.Clear();
            }
            //จบสร้างรายการแสดงข้อมูล ==========================================
        }

        void btnAppRemarkDelete_Click(object sender, EventArgs e)
        {
            try
            {
                var result = MessageBox.Show("คุณต้องการลบข้อมูลนี้ใช่หรือไม่", "คำเตือน", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    Button btnAppRemarkDelete = (Button)sender;
                    PAR_U_APP_REMARK = new NewBisPASvcRef.U_APP_REMARK();
                    PAR_U_APP_REMARK = (NewBisPASvcRef.U_APP_REMARK)btnAppRemarkDelete.Tag;

                    PAR_U_APP_REMARK_COLL.Remove(PAR_U_APP_REMARK);
                    CreateControlsInPanelAppRemark();
                    PAR_ACTION = "INSERT";
                    ClearKeyInput();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void btnAppRemarkEdit_Click(object sender, EventArgs e)
        {
            try
            {
                Button btnAppRemarkEdit = (Button)sender;
                PAR_U_APP_REMARK = new NewBisPASvcRef.U_APP_REMARK();
                PAR_U_APP_REMARK = (NewBisPASvcRef.U_APP_REMARK)btnAppRemarkEdit.Tag;

                txtRemark.Text = PAR_U_APP_REMARK.REMARK;
                cmbProblem.SelectedValue = PAR_U_APP_REMARK.PROBLEM_CLEARING == null ? "D" : PAR_U_APP_REMARK.PROBLEM_CLEARING.Value.ToString();
                btnAppRemarkSave.Text = "แก้ไขข้อมูล";
                PAR_ACTION = "UPDATE";

                SelectRecordInPanelAppRemark();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SelectRecordInPanelAppRemark()
        {
            foreach (Control c in panelAppRemark.Controls)
            {
                if (c.Name == "panelAppRemarkList")
                {
                    if (c.Tag == PAR_U_APP_REMARK)
                    {
                        c.BackColor = Color.Silver;
                    }
                    else
                    {
                        c.BackColor = SystemColors.Control;
                    }
                }
            }
        }

        private void cmbProblem_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {

                if (cmbProblem.SelectedValue != null)
                {
                    if (cmbProblem.SelectedValue.ToString() == "X")
                    {
                        txtRemark.Text = "ใบคำของาน Money Expro";
                    }
                    else if (cmbProblem.SelectedValue.ToString() == "M")
                    {
                        txtRemark.Text = "ใบคำขอโครงการ Mature Policies";
                    }
                    else if (cmbProblem.SelectedValue.ToString() == "Z")
                    {
                        txtRemark.Text = "ใบคำของาน Set in the City";
                    }
                    else if (cmbProblem.SelectedValue.ToString() == "E")
                    {
                        txtRemark.Text = "งาน Event บริษัท";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
