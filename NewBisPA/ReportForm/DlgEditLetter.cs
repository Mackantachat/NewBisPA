using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ITUtility;

namespace NewBisPA.ReportForm
{
    public partial class DlgEditLetter : Form
    {
        private string _SIGNATURE_ID;

        public string SIGNATURE_ID
        {
            get { return _SIGNATURE_ID; }
            set { _SIGNATURE_ID = value; }
        }

        private DateTime? _LIMIT_DATE;

        public DateTime? LIMIT_DATE
        {
            get { return _LIMIT_DATE; }
            set { _LIMIT_DATE = value; }
        }

        NewBISSvcRef.ProcessResult pr = new NewBISSvcRef.ProcessResult();

        NewBISSvcRef.ZTB_USER[] ztbUsers;
        NewBISSvcRef.ZTB_USER ztbUser = new NewBISSvcRef.ZTB_USER();
        NewBISSvcRef.AUTB_UNDERWRITER autbUnderWriter = new NewBISSvcRef.AUTB_UNDERWRITER();
        NewBISSvcRef.U_LETTER_SENDTO[] uletterSendtos;
        NewBISSvcRef.U_LETTER_SENDTO uletterSendto = new NewBISSvcRef.U_LETTER_SENDTO();
        NewBISSvcRef.U_LETTER_ID uLetter = new NewBISSvcRef.U_LETTER_ID();
        NewBISSvcRef.U_LETTER_ID[] uLetterArr;


        private string[] Department = { "POS" };
        private long uletterid;
        private string status = "";
        private string UserId = "";
        private long key;

        public DlgEditLetter(long _uletter_id, string _signature_id, string _UserId,string _status,long _key,DateTime? _limitdate)
        {
            uletterid = _uletter_id;
            SIGNATURE_ID = _signature_id;
            UserId = _UserId;
            status = _status;
            key = _key;
            LIMIT_DATE = _limitdate;
            InitializeComponent(); 
        }

        private void DlgEditLetter_Load(object sender, EventArgs e)
        {
            bindingControl(SIGNATURE_ID, LIMIT_DATE);
            if (status == "MD" || status == "MO" || status == "AP" || status == "NT")
            {
                chksignature.Enabled = false;
            }
        }

        private void bindingControl(string signature_id,DateTime? limitdate)
        {
            using (NewBISSvcRef.NewBISSvcClient client = new NewBISSvcRef.NewBISSvcClient())
            {
                try
                {

                    if (limitdate != null)
                    {
                        txtlimitdate.Text = Utility.dateTimeToString((DateTime)limitdate,"dd/MM/yyyy","BU"); //String.Format("{0:dd/MM/yyyy}", obj.BIRTH_DT);
                    }

                    if (status == "EX" || status == "NT" || status =="PP" || status == "DC" || status =="CC")
                    {
                        chklimitdate.Enabled = false;
                    }
                    else
                    {
                        chklimitdate.Enabled = true;
                    }

                    pr = client.GetZTB_USER(out ztbUsers, (string[])null, Department);

                    if (pr.Successed == true)
                    {
                        if (ztbUsers != null)
                        {
                            cmbemp.DataSource = ztbUsers;
                            cmbemp.DisplayMember = "FULLNAME";
                            cmbemp.ValueMember = "N_USERID";
                            cmbemp.SelectedValue = signature_id;
                        }
                    }

                    //sendto

                    NewBISSvcRef.AUTB_DATADIC_DET_COLLECTION autbDatadicDetColl = new NewBISSvcRef.AUTB_DATADIC_DET_COLLECTION();
                    NewBISSvcRef.AUTB_DATADIC_DET_COLLECTION sendLetterToColl = new NewBISSvcRef.AUTB_DATADIC_DET_COLLECTION();
                    NewBISSvcRef.AUTB_DATADIC_DET_COLLECTION sendByColl = new NewBISSvcRef.AUTB_DATADIC_DET_COLLECTION();
                    autbDatadicDetColl = client.GetAutbDataDicDetAll(out pr);

                    var sendLetterTo = from s in autbDatadicDetColl
                                       where s.COL_NAME == "SEND_LETTER_TO"
                                       select s;

                    if (sendLetterTo != null && sendLetterTo.Count() > 0)
                    {
                        sendLetterToColl.AddRange(sendLetterTo.ToArray());
                    }

                    cmbsendto.DataSource = sendLetterToColl;
                    cmbsendto.DisplayMember = "DESCRIPTION";
                    cmbsendto.ValueMember = "CODE";
                    cmbsendto.SelectedValue = "C";

                    //sendby 

                    var sendBy = from s in autbDatadicDetColl
                                       where s.COL_NAME == "SEND_BY"
                                       select s;

                    if (sendBy != null && sendBy.Count() > 0)
                    {
                        sendByColl.AddRange(sendBy.ToArray());
                    }

                    cmbsendby.DataSource = sendByColl;
                    cmbsendby.DisplayMember = "DESCRIPTION";
                    cmbsendby.ValueMember = "CODE";
                    cmbsendby.SelectedValue = "CS";

                    long[] uletterids = { uletterid };
                    pr = client.GetU_LETTER_SENDTO(out uletterSendtos, null, uletterids);

                    if (pr.Successed == true)
                    {
                        string sendby = "";
                        dtgsend.AutoGenerateColumns = false;
                        dtgsend.DataSource = uletterSendtos;

                        foreach (DataGridViewRow dr in dtgsend.Rows)
                        {
                            if (dr.DataBoundItem is NewBISSvcRef.U_LETTER_SENDTO)
                            {
                                NewBISSvcRef.U_LETTER_SENDTO obj = (NewBISSvcRef.U_LETTER_SENDTO)dr.DataBoundItem;
                                dr.Cells["clmsendto"].Value = obj.SEND_LETTER_TO == "C" ? "ลูกค้า" : "ตัวแทน";

                                switch (obj.SEND_BY)
                                {
                                    case "MD":
                                        sendby = "ส่งผ่าน MDO";
                                        break;
                                    case "CS":
                                        sendby = "ส่งตรงผู้เอาประกัน";
                                        break;
                                    case "PB":
                                        sendby = "ส่งผ่านสาขาธนาคาร";
                                        break;
                                    case "PO":
                                        sendby = "ส่งผ่านสาขาบริษัทฯ";
                                        break;
                                    case "AG":
                                        sendby = "ส่งผ่านตัวแทน";
                                        break;
                                }

                                dr.Cells["clmsendby"].Value = sendby;
                                dr.Cells["clmsendtocode"].Value = obj.SEND_LETTER_TO;
                            }
                        }

                        if (dtgsend.RowCount == 0)
                        {
                            MessageBox.Show("ยังไม่มีข้อมูล");
                        }
                    }

                    lblqty.Text = dtgsend.RowCount.ToString();

                    //ดึงโอน app เก่าๆ
                    NewBISSvcRef.U_RETURN_PRMNEWAPP[] uReturnPrmNewApps = null;
                    NewBISSvcRef.U_RETURN_PRMNEWAPP uReturnPrmNewApp = new NewBISSvcRef.U_RETURN_PRMNEWAPP();
                    pr = client.GetU_RETURN_PRMNEWAPP(out uReturnPrmNewApps, uletterids);

                    if (pr.Successed == true && uReturnPrmNewApps != null)
                    {
                        uReturnPrmNewApp = uReturnPrmNewApps[0];
                        txtNewAppNo.Text = uReturnPrmNewApp.NEW_APP_NO;
                        txtNewAppName.Text = uReturnPrmNewApp.NEW_APP_NAME;
                        txtNewAppSurname.Text = uReturnPrmNewApp.NEW_APP_SURNAME;
                        txtNewAppTrnDate.Text = uReturnPrmNewApp.NEW_APP_TRN_DT == null ? "" : Utility.dateTimeToString((DateTime)uReturnPrmNewApp.NEW_APP_TRN_DT, "dd/MM/yyyy", "BU");
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                if (client.State != System.ServiceModel.CommunicationState.Closed)
                {
                    client.Close();
                }
            }
        }

        private void btnadd_Click(object sender, EventArgs e)
        {
            DateTime? ld = Utility.StringToDateTime(txtlimitdate.Text, "BU");

            if (chkaddsend.Checked == false && chklimitdate.Checked == false && chksignature.Checked == false && ckbReturnPrmNewApp.Checked == false)
            {
                MessageBox.Show("โปรดเลือกรายการที่ต้องการแก้ไขก่อนบันทึกค่า");
            }
            else
            {
                if ((chklimitdate.Checked == true && ld != null) || (chklimitdate.Checked == false))
                {
                    add();
                }
                else
                {
                    MessageBox.Show("รูปแบบวันที่ตอบกลับไม่ถูกต้อง");
                    txtlimitdate.Focus();
                }
            } 
        }

        private void add()
        {
          //  throw new Exception("user =" + UserId);
            long[] uletterids = { uletterid };
            bool dupFlg = true;

            using (NewBISSvcRef.NewBISSvcClient client = new NewBISSvcRef.NewBISSvcClient())
            {
                try
                {
                    if (MessageBox.Show("ท่านต้องการบันทึกการเปลี่ยนแปลง?", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        if (chkaddsend.Checked == true)
                        {
                            pr = client.GetU_LETTER_SENDTO(out uletterSendtos, null, uletterids);

                            if (pr.Successed == true)
                            {
                                if (uletterSendtos != null)
                                {
                                    for (int i = 0; i < uletterSendtos.Length; i++)
                                    {
                                        uletterSendto = uletterSendtos[i];
                                        if (uletterSendto.SEND_LETTER_TO.ToString() == cmbsendto.SelectedValue.ToString())
                                        {
                                            dupFlg = false;
                                        }
                                    }

                                    if (dupFlg)
                                    {
                                        NewBISSvcRef.U_LETTER_SENDTO addUlettersendto = new NewBISSvcRef.U_LETTER_SENDTO();
                                        addUlettersendto.ULETTER_ID = uletterid;
                                        addUlettersendto.SEND_LETTER_TO = cmbsendto.SelectedValue.ToString();
                                        addUlettersendto.SEND_BY = cmbsendby.SelectedValue.ToString();
                                        client.AddU_LETTER_SENDTO(ref addUlettersendto);
                                    }
                                    else
                                    {
                                        MessageBox.Show("ผู้รับจดหมายประเภทนี้มีอยู่แล้ว รบกวนยกเลิกของเดิมก่อน");
                                    }
                                }
                                else
                                {
                                    NewBISSvcRef.U_LETTER_SENDTO addUlettersendto = new NewBISSvcRef.U_LETTER_SENDTO();
                                    addUlettersendto.ULETTER_ID = uletterid;
                                    addUlettersendto.SEND_LETTER_TO = cmbsendto.SelectedValue.ToString();
                                    addUlettersendto.SEND_BY = cmbsendby.SelectedValue.ToString();
                                    client.AddU_LETTER_SENDTO(ref addUlettersendto);
                                }
                            }
                        }

                        if (chksignature.Checked == true)
                        {
                            client.GetU_LETTER_ID(out uLetterArr, uletterids, null, null);

                            uLetter = uLetterArr[0];
                            uLetter.SIGNATURE_ID = cmbemp.SelectedValue.ToString();
                            uLetter.UPD_ID = UserId;
                            client.EditU_LETTER_ID_ONLY(ref uLetter);

                            SIGNATURE_ID = uLetter.SIGNATURE_ID;
                        }

                        if (chklimitdate.Checked == true)
                        {
                            switch (status)
                            {
                                case "CO":
                                    {
                                        NewBISSvcRef.U_EXCLUSION_ID uExclusionId = new NewBISSvcRef.U_EXCLUSION_ID();
                                        NewBISSvcRef.U_EXCLUSION_ID[] uExclusionIds;
                                        long[] keys = { (long)key };
                                        pr = client.GetU_EXCLUSION_ID(out uExclusionIds, keys, null);
                                        if (pr.Successed == true && uExclusionIds != null)
                                        {
                                            uExclusionId = uExclusionIds[0];
                                            uExclusionId.ANSWER_LIMIT_DT = Utility.StringToDateTime(txtlimitdate.Text, "BU");
                                            uExclusionId.UPD_ID = UserId;
                                            client.EditU_EXCLUSION_ID(ref uExclusionId);
                                        }
                                        break;
                                    }
                                case "AP":
                                    {
                                        NewBISSvcRef.U_AP_ID uApId = new NewBISSvcRef.U_AP_ID();
                                        NewBISSvcRef.U_AP_ID[] uApIds;
                                        long[] keys = { (long)key };
                                        pr = client.GetU_AP_ID(out uApIds, keys, null);
                                        if (pr.Successed == true && uApIds != null)
                                        {
                                            uApId = uApIds[0];
                                            uApId.ANSWER_LIMIT_DT = Utility.StringToDateTime(txtlimitdate.Text, "BU");
                                            uApId.UPD_ID = UserId;
                                            client.EditU_AP_ID(ref uApId);
                                        }
                                        break;
                                    }
                                case "MO":
                                    {
                                        NewBISSvcRef.U_MEMO_ID uMemoId = new NewBISSvcRef.U_MEMO_ID();
                                        NewBISSvcRef.U_MEMO_ID[] uMemoIds;
                                        long[] keys = { (long)key };
                                        pr = client.GetU_MEMO_ID(out uMemoIds, keys, null);
                                        if (pr.Successed == true && uMemoIds != null)
                                        {
                                            uMemoId = uMemoIds[0];
                                            uMemoId.ANSWER_LIMIT_DT = Utility.StringToDateTime(txtlimitdate.Text, "BU");
                                            uMemoId.UPD_ID = UserId;
                                            client.UpdateLimitDateMO(uMemoIds[0].UMEMO_ID, uMemoId.ANSWER_LIMIT_DT, UserId);
                                          //  client.EditU_MEMO_ID(ref uMemoId);
                                        }
                                        break;
                                    }
                                case "MD":
                                    {
                                        NewBISSvcRef.U_MEMO_ID uMemoId = new NewBISSvcRef.U_MEMO_ID();
                                        NewBISSvcRef.U_MEMO_ID[] uMemoIds;
                                        long[] keys = { (long)key };
                                        pr = client.GetU_MEMO_ID(out uMemoIds, keys, null);
                                        if (pr.Successed == true && uMemoIds != null)
                                        {
                                            uMemoId = uMemoIds[0];
                                            uMemoId.ANSWER_LIMIT_DT = Utility.StringToDateTime(txtlimitdate.Text, "BU");
                                            uMemoId.UPD_ID = UserId;
                                        //    client.EditU_MEMO_ID(ref uMemoId);
                                           pr =  client.UpdateLimitDateMO(uMemoIds[0].UMEMO_ID, uMemoId.ANSWER_LIMIT_DT, UserId);
                                            if (pr.Successed == false)
                                            {
                                                throw new Exception(pr.ErrorMessage);
                                            }
                                        }
                                        break;
                                    }
                                default:
                                    break;
                            }

                            LIMIT_DATE = Utility.StringToDateTime(txtlimitdate.Text, "BU");
                        }

                        if (ckbReturnPrmNewApp.Checked == true)
                        {
                            if (txtNewAppNo.Text != "" && txtNewAppName.Text != "" && txtNewAppNo.Text != "" && txtNewAppSurname.Text != "")
                            {
                                DateTime? NewAppTrnDate = Utility.StringToDateTime(txtNewAppTrnDate.Text.Trim(), "BU");
                                if (NewAppTrnDate != null)
                                {  
                                    NewBISSvcRef.U_RETURN_PRMNEWAPP uReturnPrmnewapp = new NewBISSvcRef.U_RETURN_PRMNEWAPP();
                                    NewBISSvcRef.U_RETURN_PRM[] uReturnPrms;
                                    long[] uletter_ids = { uletterid };
                                    client.GetU_RETURN_PRM(out uReturnPrms, uletter_ids);

                                    if (uReturnPrms != null)
                                    {
                                        uReturnPrmnewapp.ULETTER_ID = uletterid;
                                        uReturnPrmnewapp.NEW_APP_NO = txtNewAppNo.Text.Trim().ToUpper();
                                        uReturnPrmnewapp.NEW_APP_TRN_DT = NewAppTrnDate;
                                        uReturnPrmnewapp.NEW_APP_NAME = txtNewAppName.Text.Trim();
                                        uReturnPrmnewapp.NEW_APP_SURNAME = txtNewAppSurname.Text.Trim();
                                        //client.AddU_RETURN_PRMNEWAPP(ref uReturnPrmnewapp);
                                        client.EditU_RETURN_PRMNEWAPP(ref uReturnPrmnewapp);

                                        txtNewAppNo.Text = "";
                                        txtNewAppTrnDate.Text = "";
                                        txtNewAppName.Text = "";
                                        txtNewAppSurname.Text = "";
                                    }
                                    else
                                    {
                                        MessageBox.Show("ไม่สามารถโอนเงินไปใบคำขออื่นได้เนื่องจากไม่มีเงินเกิน");
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("รูปแบบวันที่โอนเงินไม่ถูกต้อง");
                                    txtNewAppTrnDate.Focus();
                                }
                            }
                            else
                            {
                                MessageBox.Show("กรอกข้อมูลใบคำขออื่นไม่ครบถ้วน");
                            }
                              
                        }

                        bindingControl(SIGNATURE_ID, LIMIT_DATE);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                if (client.State != System.ServiceModel.CommunicationState.Closed)
                {
                    client.Close();
                }
            }
        } 

        private void chkaddsend_CheckedChanged(object sender, EventArgs e)
        {
            if (chkaddsend.Checked == true)
            {
                cmbsendby.Enabled = true;
                cmbsendto.Enabled = true;
            }
            else
            {
                cmbsendby.Enabled = false;
                cmbsendto.Enabled = false; 
            }
        }

        private void dtgsend_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex < 0 || (e.ColumnIndex != dtgsend.Columns["clmremove"].Index))
            {
                return;
            }
            else if (e.ColumnIndex == dtgsend.Columns["clmremove"].Index)
            {
                if (MessageBox.Show("ท่านต้องการยกเลิกการส่งจดหมายให้  " + dtgsend["clmsendto", e.RowIndex].Value.ToString() + "?", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    try
                    {
                        using (NewBISSvcRef.NewBISSvcClient client = new NewBISSvcRef.NewBISSvcClient())
                        {
                            NewBISSvcRef.U_LETTER_SENDTO removeUletterSendto = new NewBISSvcRef.U_LETTER_SENDTO();
                            NewBISSvcRef.U_LETTER_SENDTO[] removeUletterSendtos;
                            long[] uletterids = { uletterid };
                            client.GetU_LETTER_SENDTO(out removeUletterSendtos,null,uletterids);

                            if (removeUletterSendtos.Length == 1)
                            {
                                MessageBox.Show("ไม่อนุญาติให้ลบเนื่องจากเหลือวิธีการส่งจดหมายเพียงวิธีเดียว");
                            }
                            else
                            {
                                for (int i = 0; i < removeUletterSendtos.Length; i++)
                                {
                                    removeUletterSendto = removeUletterSendtos[i];
                                    if (removeUletterSendto.SEND_LETTER_TO.ToString() == dtgsend["clmsendtocode", e.RowIndex].Value.ToString())
                                    {
                                        client.RemoveU_LETTER_SENDTO(removeUletterSendto);
                                        MessageBox.Show("การส่งจดหมายให้ " + dtgsend["clmsendto", e.RowIndex].Value.ToString() + " ถูกยกเลิกแล้ว");
                                    }
                                }
                            }

                            if (client.State != System.ServiceModel.CommunicationState.Closed)
                            {
                                client.Close();
                            }
                            bindingControl(SIGNATURE_ID,LIMIT_DATE);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        private void chksignature_CheckedChanged(object sender, EventArgs e)
        {
            if (chksignature.Checked == true)
            {
                cmbemp.Enabled = true;
            }
            else
            {
                cmbemp.Enabled = false;
            }
        }

        private void chklimitdate_CheckedChanged(object sender, EventArgs e)
        {
            if (chklimitdate.Checked == true)
            {
                txtlimitdate.Enabled = true;
            }
            else
            {
                txtlimitdate.Enabled = false;
            }
        }

        private void txtlimitdate_Leave(object sender, EventArgs e)
        {
            ChkDateForTextBox(txtlimitdate.Text, "วันที่ตอบกลับ", txtlimitdate);
        } 

        private void ChkDateForTextBox(String strDate, String msg, Control textBox)
        {
            if (strDate != "")
            {
                DateTime? d = Utility.StringToDateTime(strDate, "BU");
                if (d == null)
                {
                    textBox.Text = "";
                    MessageBox.Show("รูปแบบวันที่ " + msg + " ไม่ถูกต้อง");
                    textBox.Focus();
                    return;
                }
            }
        }

        private void ckbReturnPrmNewApp_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbReturnPrmNewApp.Checked == true)
            {
                gbReturnPrmNewApp.Enabled = true;
            }
            else
            {
                gbReturnPrmNewApp.Enabled = false;
                txtNewAppNo.Text = "";
                txtNewAppTrnDate.Text = "";
                txtNewAppName.Text = "";
                txtNewAppSurname.Text = "";
            }
        }

        private void txtNewAppTrnDate_Leave(object sender, EventArgs e)
        {
            ChkDateForTextBox(txtNewAppTrnDate.Text, "วันที่โอนเงิน", txtNewAppTrnDate);
        }

        //private void ChkDateForTextBox(String strDate, String msg, Control textBox)
        //{
        //    if (strDate != "")
        //    {
        //        DateTime? d = Utility.StringToDateTime(strDate, "BU");
        //        if (d == null)
        //        {
        //            textBox.Text = "";
        //            MessageBox.Show("รูปแบบวันที่ " + msg + " ไม่ถูกต้อง");
        //            textBox.Focus();
        //            return;
        //        }
        //    }
        //}
    }
}
