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
    public partial class OldCustomerDataForm : Form
    {

        private long? _NameID;
        public long? NameID
        {
            get { return _NameID; }
            set { _NameID = value; }
        }

        private long? _ParentID;

        public long? ParentID
        {
            get { return _ParentID; }
            set { _ParentID = value; }
        }

        private NewBisPASvcRef.P_NAME_ID _P_NAME_ID_OBJ;
        public NewBisPASvcRef.P_NAME_ID P_NAME_ID_OBJ
        {
            get { return _P_NAME_ID_OBJ; }
            set { _P_NAME_ID_OBJ = value; }
        }

        private NewBisPASvcRef.P_NAME_ID_COLLECTION _P_NAME_ID_COLL;
        public NewBisPASvcRef.P_NAME_ID_COLLECTION P_NAME_ID_COLL
        {
            get { return _P_NAME_ID_COLL; }
            set { _P_NAME_ID_COLL = value; }
        }

        private NewBisPASvcRef.P_PARENT_ID _P_PARENT_ID_OBJ;

        public NewBisPASvcRef.P_PARENT_ID P_PARENT_ID_OBJ
        {
            get { return _P_PARENT_ID_OBJ; }
            set { _P_PARENT_ID_OBJ = value; }
        }

        private NewBisPASvcRef.P_PARENT_ID_COLLECTION _P_PARENT_ID_COLL;

        public NewBisPASvcRef.P_PARENT_ID_COLLECTION P_PARENT_ID_COLL
        {
            get { return _P_PARENT_ID_COLL; }
            set { _P_PARENT_ID_COLL = value; }
        }

        public OldCustomerDataForm()
        {
            InitializeComponent();
        }

        private void OldCustomerDataForm_Load(object sender, EventArgs e)
        {
            try
            {
                //ส่วนของข้อมูลลูกค้า ==============================================================
                if (NameID != null)
                {
                    ckbOldCustomer.Checked = true;
                    ckbNewCustomer.Checked = false;
                }
                else
                {
                    ckbOldCustomer.Checked = false;
                    ckbNewCustomer.Checked = false;
                }

                if (P_NAME_ID_COLL != null && P_NAME_ID_COLL.Count() > 0)
                {
                    dgvPNameID.EndEdit();
                    dgvPNameID.AutoGenerateColumns = false;
                    dgvPNameID.DataSource = P_NAME_ID_COLL;
                    int rowNo = 0;


                    foreach (DataGridViewRow dr in dgvPNameID.Rows)
                    {
                        if (dr.IsNewRow == false)
                        {
                            if (dr.DataBoundItem is NewBisPASvcRef.P_NAME_ID)
                            {
                                NewBisPASvcRef.P_NAME_ID obj = (NewBisPASvcRef.P_NAME_ID)dr.DataBoundItem;
                                rowNo = rowNo + 1;
                                if (obj.NAME_ID == NameID)
                                {
                                    dr.Cells["CustomerSel"].Value = "Y";
                                }
                                else
                                {
                                    dr.Cells["CustomerSel"].Value = "N";
                                }
                                dr.Cells["CustomerNo"].Value = rowNo.ToString();
                                dr.Cells["CustomerName"].Value = obj.PRENAME + " " + obj.NAME + "  " + obj.SURNAME;
                                dr.Cells["CustomerBirthDate"].Value = obj.BIRTH_DT.Value == null ? null : Utility.dateTimeToString(obj.BIRTH_DT.Value, "dd/MM/yyyy", "BU");
                                dr.Cells["CustomerSex"].Value = obj.SEX == null ? null : Utility.GetSex(obj.SEX);
                                dr.Cells["CustomerIdcardNo"].Value = obj.IDCARD_NO == null ? null : Utility.patternIDCard(obj.IDCARD_NO);
                                dr.Cells["CustomerPassport"].Value = obj.PASSPORT;
                                dr.Cells["CustomerPhone"].Value = obj.MB_PHONE;
                                dr.Cells["CustomerEmail"].Value = obj.EMAIL;
                                dr.Cells["CustomerNationality"].Value = obj.NATIONALITY;

                                dr.Cells["CustomerNameID"].Value = obj.NAME_ID;
                                dr.Cells["CustomerPrename"].Value = obj.PRENAME;
                                dr.Cells["CustomerFirstName"].Value = obj.NAME;
                                dr.Cells["CustomerLastName"].Value = obj.SURNAME;
                                dr.Cells["CustomerSexCode"].Value = obj.SEX;
                                dr.Cells["CustomerIdcardCode"].Value = obj.IDCARD_NO;
                            }

                        }


                    }

                    lblAmountCustomer.Text = "จำนวนข้อมูล " + rowNo.ToString() + " ราย";
                }
                else
                {
                    dgvPNameID.AutoGenerateColumns = false;
                    dgvPNameID.DataSource = null;
                    ckbOldCustomer.Checked = false;
                    ckbNewCustomer.Checked = true;
                    lblAmountCustomer.Text = "";
                }

                //จบส่วนของข้อมูลลูกค้า ==============================================================

                //ส่วนของข้อมูลผู้ปกครอง ==============================================================
                if (ParentID != null)
                {
                    ckbOldParent.Checked = true;
                    ckbNewParent.Checked = false;
                }
                else
                {
                    ckbOldParent.Checked = false;
                    ckbNewParent.Checked = false;
                }

                if (P_PARENT_ID_COLL != null && P_PARENT_ID_COLL.Count() > 0)
                {
                    dgvPParentID.EndEdit();
                    dgvPParentID.AutoGenerateColumns = false;
                    dgvPParentID.DataSource = P_PARENT_ID_COLL;
                    int rowNo = 0;

                    foreach (DataGridViewRow dr in dgvPParentID.Rows)
                    {
                        if (dr.IsNewRow == false)
                        {
                            if (dr.DataBoundItem is NewBisPASvcRef.P_PARENT_ID)
                            {
                                NewBisPASvcRef.P_PARENT_ID obj = (NewBisPASvcRef.P_PARENT_ID)dr.DataBoundItem;
                                rowNo = rowNo + 1;
                                if (obj.PARENT_ID == ParentID)
                                {
                                    dr.Cells["ParentSel"].Value = "Y";
                                }
                                else
                                {
                                    dr.Cells["ParentSel"].Value = "N";
                                }
                                dr.Cells["ParentNo"].Value = rowNo.ToString();
                                dr.Cells["ParentName"].Value = obj.PRENAME + " " + obj.NAME + "  " + obj.SURNAME;
                                dr.Cells["ParentBirthDate"].Value = obj.BIRTH_DT == null ? null : Utility.dateTimeToString(obj.BIRTH_DT.Value, "dd/MM/yyyy", "BU");
                                dr.Cells["ParentSex"].Value = obj.SEX == null ? null : Utility.GetSex(obj.SEX);
                                dr.Cells["ParentIdcardNo"].Value = obj.IDCARD_NO == null ? null : Utility.patternIDCard(obj.IDCARD_NO);
                                dr.Cells["ParentPassport"].Value = obj.PASSPORT;
                                dr.Cells["ParentPhone"].Value = obj.MB_PHONE;
                                dr.Cells["ParentEmail"].Value = null;
                                dr.Cells["ParentNationality"].Value = obj.NATIONALITY;

                                dr.Cells["ParentParentID"].Value = obj.PARENT_ID;
                                dr.Cells["ParentPrename"].Value = obj.PRENAME;
                                dr.Cells["ParentFirstName"].Value = obj.NAME;
                                dr.Cells["ParentLastName"].Value = obj.SURNAME;
                                dr.Cells["ParentSexCode"].Value = obj.SEX;
                                dr.Cells["ParentIdcardCode"].Value = obj.IDCARD_NO;
                            }
                        }
                    }

                    lblAmountParent.Text = "จำนวนข้อมูล " + rowNo.ToString() + " ราย";
                }
                else
                {
                    dgvPParentID.AutoGenerateColumns = false;
                    dgvPParentID.DataSource = null;
                    ckbOldParent.Checked = false;
                    ckbNewParent.Checked = true;
                    lblAmountParent.Text = "";
                }
                //จบส่วนของข้อมูลผู้ปกครอง ============================================================
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        private void ckbNewCustomer_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (ckbNewCustomer.Checked == true)
                {
                    ckbOldCustomer.Checked = false;
                    if (dgvPNameID.Rows.Count > 0)
                    {
                        foreach (DataGridViewRow dr in dgvPNameID.Rows)
                        {
                            if (dr.IsNewRow == false)
                            {
                                dr.Cells["CustomerSel"].Value = "N";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void dgvPNameID_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if ((e.RowIndex > -1) && (dgvPNameID.Rows[e.RowIndex].Cells["CustomerSel"].ColumnIndex == e.ColumnIndex))
                {
                    if (dgvPNameID.Rows.Count > 0)
                    {
                        int rowNoChk = e.RowIndex;
                        foreach (DataGridViewRow dr in dgvPNameID.Rows)
                        {
                            if (dr.IsNewRow == false)
                            {
                                if (dr.Index == rowNoChk)
                                {
                                    if (dr.Cells["CustomerSel"].Value.ToString() == "Y")
                                    {
                                        dr.Cells["CustomerSel"].Value = "N";
                                    }
                                    else
                                    {
                                        dr.Cells["CustomerSel"].Value = "Y";
                                    }
                                }
                                else
                                {
                                    dr.Cells["CustomerSel"].Value = "N";
                                }
                            }
                        }

                        //dgvPNameID.EndEdit();
                        int chkData = 0;
                        foreach (DataGridViewRow dr in dgvPNameID.Rows)
                        {
                            if (dr.IsNewRow == false)
                            {
                                if (dr.Cells["CustomerSel"].Value.ToString() == "Y")
                                {
                                    chkData = chkData + 1;
                                }
                            }
                        }

                        if (chkData > 0)
                        {
                            ckbOldCustomer.Checked = true;
                            ckbNewCustomer.Checked = false;
                        }
                        else
                        {
                            ckbOldCustomer.Checked = false;
                            ckbNewCustomer.Checked = false;
                        }

                    }

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void OldCustomerDataForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (ckbOldCustomer.Checked == false && ckbNewCustomer.Checked == false)
                {
                    MessageBox.Show("กรุณาระบุว่าลูกค้าเป็น ลูกค้าเดิมหรือลูค้าใหม่ ก่อน");
                    ckbNewCustomer.Focus();
                    e.Cancel = true;
                    return;
                }

                if (ckbOldParent.Checked == false && ckbNewParent.Checked == false)
                {
                    MessageBox.Show("กรุณาระบุว่าผู้ปกครองเป็น ลูกค้าเดิมหรือลูค้าใหม่ ก่อน");
                    ckbNewParent.Focus();
                    e.Cancel = true;
                    return;
                }

                if (ckbOldCustomer.Checked == true)
                {
                    int chkData = 0;
                    dgvPNameID.EndEdit();
                    if (dgvPNameID.Rows.Count > 0)
                    {
                        foreach (DataGridViewRow dr in dgvPNameID.Rows)
                        {
                            if (dr.IsNewRow == false)
                            {
                                if (dr.Cells["CustomerSel"].Value.ToString() == "Y")
                                {
                                    chkData = chkData + 1;
                                }
                            }
                        }
                    }
                    if (chkData == 0)
                    {
                        MessageBox.Show("ท่านเลือกเป็นลูกค้าเดิม แต่ท่านไม่ได้เลือกลูกค้าในตารางข้อมูลเดิมของผู้เอาประกัน");
                        e.Cancel = true;
                        return;
                    }
                }

                if (ckbOldParent.Checked == true)
                {
                    int chkData = 0;
                    dgvPParentID.EndEdit();
                    if (dgvPParentID.Rows.Count > 0)
                    {
                        foreach (DataGridViewRow dr in dgvPParentID.Rows)
                        {
                            if (dr.IsNewRow == false)
                            {
                                if (dr.Cells["ParentSel"].Value.ToString() == "Y")
                                {
                                    chkData = chkData + 1;
                                }
                            }
                        }
                    }
                    if (chkData == 0)
                    {
                        MessageBox.Show("ท่านเลือกเป็นลูกค้าเดิม แต่ท่านไม่ได้เลือกลูกค้าในตารางข้อมูลเดิมของผู้ปกครอง");
                        e.Cancel = true;
                        return;
                    }
                }


                if (dgvPNameID.Rows.Count > 0)
                {
                    foreach (DataGridViewRow dr in dgvPNameID.Rows)
                    {
                        if (dr.IsNewRow == false)
                        {
                            if (dr.Cells["CustomerSel"].Value.ToString() == "Y")
                            {
                                P_NAME_ID_OBJ = new NewBisPASvcRef.P_NAME_ID();
                                P_NAME_ID_OBJ.NAME_ID = dr.Cells["CustomerNameID"].Value == null ? (long?)null : Convert.ToInt64(dr.Cells["CustomerNameID"].Value);
                                P_NAME_ID_OBJ.PRENAME = dr.Cells["CustomerPrename"].Value == null ? "" : dr.Cells["CustomerPrename"].Value.ToString();
                                P_NAME_ID_OBJ.NAME = dr.Cells["CustomerFirstName"].Value == null ? "" : dr.Cells["CustomerFirstName"].Value.ToString();
                                P_NAME_ID_OBJ.SURNAME = dr.Cells["CustomerLastName"].Value == null ? "" : dr.Cells["CustomerLastName"].Value.ToString();
                                P_NAME_ID_OBJ.SEX = dr.Cells["CustomerSexCode"].Value == null ? "" : dr.Cells["CustomerSexCode"].Value.ToString();
                                P_NAME_ID_OBJ.BIRTH_DT = dr.Cells["CustomerBirthDate"].Value == null ? (DateTime?)null : Utility.StringToDateTime(dr.Cells["CustomerBirthDate"].Value.ToString(), "BU");
                                P_NAME_ID_OBJ.IDCARD_NO = dr.Cells["CustomerIdcardCode"].Value == null ? "" : dr.Cells["CustomerIdcardCode"].Value.ToString();
                                P_NAME_ID_OBJ.PASSPORT = dr.Cells["CustomerPassport"].Value == null ? "" : dr.Cells["CustomerPassport"].Value.ToString();
                                P_NAME_ID_OBJ.MB_PHONE = dr.Cells["CustomerPhone"].Value == null ? "" : dr.Cells["CustomerPhone"].Value.ToString();
                                P_NAME_ID_OBJ.EMAIL = dr.Cells["CustomerEmail"].Value == null ? "" : dr.Cells["CustomerEmail"].Value.ToString();
                                P_NAME_ID_OBJ.NATIONALITY = dr.Cells["CustomerNationality"].Value == null ? "" : dr.Cells["CustomerNationality"].Value.ToString();
                                break;
                            }
                        }
                    }
                }

                if (dgvPParentID.Rows.Count > 0)
                {
                    foreach (DataGridViewRow dr in dgvPParentID.Rows)
                    {
                        if (dr.IsNewRow == false)
                        {
                            if (dr.Cells["ParentSel"].Value.ToString() == "Y")
                            {
                                P_PARENT_ID_OBJ = new NewBisPASvcRef.P_PARENT_ID();
                                P_PARENT_ID_OBJ.PARENT_ID = dr.Cells["ParentParentID"].Value == null ? (long?)null : Convert.ToInt64(dr.Cells["ParentParentID"].Value);
                                P_PARENT_ID_OBJ.PRENAME = dr.Cells["ParentPrename"].Value == null ? "" : dr.Cells["ParentPrename"].Value.ToString();
                                P_PARENT_ID_OBJ.NAME = dr.Cells["ParentFirstName"].Value == null ? "" : dr.Cells["ParentFirstName"].Value.ToString();
                                P_PARENT_ID_OBJ.SURNAME = dr.Cells["ParentLastName"].Value == null ? "" : dr.Cells["ParentLastName"].Value.ToString();
                                P_PARENT_ID_OBJ.SEX = dr.Cells["ParentSexCode"].Value == null ? "" : dr.Cells["ParentSexCode"].Value.ToString();
                                P_PARENT_ID_OBJ.BIRTH_DT = dr.Cells["ParentBirthDate"].Value == null ? (DateTime?)null : Utility.StringToDateTime(dr.Cells["ParentBirthDate"].Value.ToString(), "BU");
                                P_PARENT_ID_OBJ.IDCARD_NO = dr.Cells["ParentIdcardCode"].Value == null ? "" : dr.Cells["ParentIdcardCode"].Value.ToString();
                                P_PARENT_ID_OBJ.PASSPORT = dr.Cells["ParentPassport"].Value == null ? "" : dr.Cells["ParentPassport"].Value.ToString();
                                P_PARENT_ID_OBJ.MB_PHONE = dr.Cells["ParentPhone"].Value == null ? "" : dr.Cells["ParentPhone"].Value.ToString();
                                P_PARENT_ID_OBJ.NATIONALITY = dr.Cells["ParentNationality"].Value == null ? "" : dr.Cells["ParentNationality"].Value.ToString();
                                break;
                            }
                        }
                    }
                }

                e.Cancel = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());

            }
        }

        private void ckbNewParent_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (ckbNewParent.Checked == true)
                {
                    ckbOldParent.Checked = false;
                    if (dgvPParentID.Rows.Count > 0)
                    {
                        foreach (DataGridViewRow dr in dgvPParentID.Rows)
                        {
                            if (dr.IsNewRow == false)
                            {
                                dr.Cells["ParentSel"].Value = "N";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void dgvPParentID_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if ((e.RowIndex > -1) && (dgvPParentID.Rows[e.RowIndex].Cells["ParentSel"].ColumnIndex == e.ColumnIndex))
                {
                    if (dgvPParentID.Rows.Count > 0)
                    {
                        int rowNoChk = e.RowIndex;
                        foreach (DataGridViewRow dr in dgvPParentID.Rows)
                        {
                            if (dr.IsNewRow == false)
                            {
                                if (dr.Index == rowNoChk)
                                {
                                    if (dr.Cells["ParentSel"].Value.ToString() == "Y")
                                    {
                                        dr.Cells["ParentSel"].Value = "N";
                                    }
                                    else
                                    {
                                        dr.Cells["ParentSel"].Value = "Y";
                                    }
                                }
                                else
                                {
                                    dr.Cells["ParentSel"].Value = "N";
                                }
                            }
                        }

                        //dgvPNameID.EndEdit();
                        int chkData = 0;
                        foreach (DataGridViewRow dr in dgvPParentID.Rows)
                        {
                            if (dr.IsNewRow == false)
                            {
                                if (dr.Cells["ParentSel"].Value.ToString() == "Y")
                                {
                                    chkData = chkData + 1;
                                }
                            }
                        }

                        if (chkData > 0)
                        {
                            ckbOldParent.Checked = true;
                            ckbNewParent.Checked = false;
                        }
                        else
                        {
                            ckbOldParent.Checked = false;
                            ckbNewParent.Checked = false;
                        }

                    }

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }



    }
}
