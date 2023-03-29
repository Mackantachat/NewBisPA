using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;
using ITUtility;
using System.IO;

namespace NewBisPA.ReportForm
{
    public partial class frmRejectionReport : Form
    {
        private bool checkPrint;
        string[] ProvinceArr;

        REPORT_SELECT_PRINT_Collection RepSelectPrntColl = new REPORT_SELECT_PRINT_Collection();
        string[] planTelSendMail = { "A17901","A17902","A17903" };
        private string _UserID;

        public string UserID
        {
            get { return _UserID; }
            set { _UserID = value; }
        }

        public frmRejectionReport()
        {
            InitializeComponent(); 
        }

        public frmRejectionReport(string user_id)
        {
            InitializeComponent();
            UserID = user_id;
        }

        private void frmRejectionReport_Load(object sender, EventArgs e)
        {
            clearFile();
            showHeadCallReport(); 
        }

        private string dirPath = @"C:\WINDOWS\Temp\NewbizReport";
        NewBISSvcRef.REPORT_REJECTION_SEARCH_LIST_Collection ResultColl = new NewBISSvcRef.REPORT_REJECTION_SEARCH_LIST_Collection();

        private void showHeadCallReport()
        {
            txtdatebegin.Text = DateTime.Now.Day.ToString() + "/" + DateTime.Now.Month.ToString() + "/" + DateTime.Now.ToString("yyyy", System.Globalization.CultureInfo.GetCultureInfo("th-TH"));
            txtdateend.Text = DateTime.Now.Day.ToString() + "/" + DateTime.Now.Month.ToString() + "/" + DateTime.Now.ToString("yyyy", System.Globalization.CultureInfo.GetCultureInfo("th-TH"));
            txthrbegin.Text = "00"; //DateTime.Now.Hour.ToString().Length == 1 ? "0" + DateTime.Now.Hour.ToString() : DateTime.Now.Hour.ToString();
            txthrend.Text = DateTime.Now.Hour.ToString().Length == 1 ? "0" + DateTime.Now.Hour.ToString() : DateTime.Now.Hour.ToString();
            txtminbegin.Text = "00"; //DateTime.Now.Minute.ToString().Length == 1 ? "0" + DateTime.Now.Minute.ToString() : DateTime.Now.Minute.ToString();
            txtminend.Text = DateTime.Now.Minute.ToString().Length == 1 ? "0" + DateTime.Now.Minute.ToString() : DateTime.Now.Minute.ToString(); 

            DataTable printHisist = new DataTable();
            printHisist.Columns.Add(new DataColumn("Display", typeof(string)));
            printHisist.Columns.Add(new DataColumn("Id", typeof(string)));
            printHisist.Rows.Add(printHisist.NewRow());
            printHisist.Rows.Add(printHisist.NewRow());
            printHisist.Rows.Add(printHisist.NewRow());
            printHisist.Rows[0][0] = "พิมพ์แล้ว";
            printHisist.Rows[0][1] = "Y";
            printHisist.Rows[1][0] = "ยังไม่พิมพ์";
            printHisist.Rows[1][1] = "N";
            printHisist.Rows[2][0] = "ทั้งหมด";
            printHisist.Rows[2][1] = "";

            cmbPrintHis.DataSource = printHisist;
            cmbPrintHis.DisplayMember = "Display";
            cmbPrintHis.ValueMember = "Id";
            cmbPrintHis.SelectedValue = "";

            //cmbreturnprm
            DataTable returnprm = new DataTable();
            returnprm.Columns.Add(new DataColumn("Display", typeof(string)));
            returnprm.Columns.Add(new DataColumn("Id", typeof(string)));
            returnprm.Rows.Add(returnprm.NewRow());
            returnprm.Rows.Add(returnprm.NewRow());
            returnprm.Rows.Add(returnprm.NewRow());
            returnprm.Rows[0][0] = "มีเงินคืน";
            returnprm.Rows[0][1] = "Y";
            returnprm.Rows[1][0] = "ไม่มีเงินคืน";
            returnprm.Rows[1][1] = "N";
            returnprm.Rows[2][0] = "แสดงทั้งหมด";
            returnprm.Rows[2][1] = "";

            cmbreturnprm.DataSource = returnprm;
            cmbreturnprm.DisplayMember = "Display";
            cmbreturnprm.ValueMember = "Id";
            cmbreturnprm.SelectedValue = "";
             
            //cmbreturnprm
            DataTable account = new DataTable();
            account.Columns.Add(new DataColumn("Display", typeof(string)));
            account.Columns.Add(new DataColumn("Id", typeof(string)));
            account.Rows.Add(account.NewRow());
            account.Rows.Add(account.NewRow());
           // account.Rows.Add(account.NewRow());
            account.Rows[0][0] = "ทำเงินคืนแล้ว";
            account.Rows[0][1] = "Y";
            account.Rows[1][0] = "ยังไม่ทำเงินคืน";
            account.Rows[1][1] = "N";
            //account.Rows[2][0] = "ทั้งหมด";
            //account.Rows[2][1] = "";

            cmbaccount.DataSource = account;
            cmbaccount.DisplayMember = "Display";
            cmbaccount.ValueMember = "Id";
            cmbaccount.SelectedValue = "Y";

            DataTable letterList = new DataTable();
            letterList.Columns.Add(new DataColumn("Display", typeof(string)));
            letterList.Columns.Add(new DataColumn("Id", typeof(string)));
            letterList.Rows.Add(letterList.NewRow());
            letterList.Rows.Add(letterList.NewRow());
            letterList.Rows.Add(letterList.NewRow());
            letterList.Rows.Add(letterList.NewRow());
            letterList.Rows.Add(letterList.NewRow());
            letterList.Rows.Add(letterList.NewRow());
            letterList.Rows.Add(letterList.NewRow());
            letterList.Rows.Add(letterList.NewRow());
            letterList.Rows.Add(letterList.NewRow());
            letterList.Rows[0][0] = "จดหมายบริษัทปฏิเสธการรับประกัน (DC)";
            letterList.Rows[0][1] = "DC";
            letterList.Rows[1][0] = "จดหมายเลื่อนการพิจารณารับประกัน (PP)";
            letterList.Rows[1][1] = "PP";
            letterList.Rows[2][0] = "จดหมายผู้เอาประกันขอยกเลิกการสมัครประกันชีวิต (NT)";
            letterList.Rows[2][1] = "NT";
            letterList.Rows[3][0] = "จดหมายยกเลิกใบคำขอโดยไม่ได้รับการติดต่อ (CC)";
            letterList.Rows[3][1] = "CC";
            letterList.Rows[4][0] = "จดหมายยกเลิกใบคำขอโดยครบกำหนดชำระเบี้ย (EX)";
            letterList.Rows[4][1] = "EX";
            letterList.Rows[5][0] = "จดหมายนำส่งค่าเบี้ยประกันส่วนเกิน (IF)";
            letterList.Rows[5][1] = "IF";
            letterList.Rows[6][0] = "จดหมายแจ้งผู้เอาประกันกรณีไม่อนุมัติบัญชี SMART";
            letterList.Rows[6][1] = "SM";
            letterList.Rows[7][0] = "จดหมายแจ้งผู้เอาประกันกรณีไม่อนุมัติการโอนสิทธิ์";
            letterList.Rows[7][1] = "AS";
            letterList.Rows[8][0] = "ทั้งหมด";
            letterList.Rows[8][1] = "";
            cmbLetterType.DataSource = letterList;
            cmbLetterType.DisplayMember = "Display";
            cmbLetterType.ValueMember = "Id";
            cmbLetterType.SelectedValue = "";
             
            NewBISSvcRef.ProcessResult pr2 = new NewBISSvcRef.ProcessResult();
            NewBISSvcRef.ProcessResult pr3 = new NewBISSvcRef.ProcessResult();
            NewBISSvcRef.ProcessResult pr4 = new NewBISSvcRef.ProcessResult();
            NewBISSvcRef.ProcessResult pr5 = new NewBISSvcRef.ProcessResult();
             
            NewBISSvcRef.AUTB_UNDERWRITER_COLLECTION autbColl = new NewBISSvcRef.AUTB_UNDERWRITER_COLLECTION();
            NewBISSvcRef.AUTB_UNDERWRITER autbUnder = new NewBISSvcRef.AUTB_UNDERWRITER();
            NewBISSvcRef.AUTB_CHANNEL_COLLECTION autbChannels = new NewBISSvcRef.AUTB_CHANNEL_COLLECTION();

            using (NewBISSvcRef.NewBISSvcClient client = new NewBISSvcRef.NewBISSvcClient())
            {
                try
                { 
                    autbColl = client.GetUnderWrite(out pr2, autbUnder);
                    autbChannels = client.GetAutbChannel(out pr5);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                if (client.State != System.ServiceModel.CommunicationState.Closed)
                {
                    client.Close();
                }
            }
         

            if (pr2.Successed == true)
            {
                if (autbColl != null)
                {
                    NewBISSvcRef.AUTB_UNDERWRITER blank = new NewBISSvcRef.AUTB_UNDERWRITER();
                    blank.UND_NAME = "ทั้งหมด";
                    blank.UND_ID = "";
                    autbColl.Add(blank);

                    cmbUnderwriter.DataSource = autbColl;
                    cmbUnderwriter.DisplayMember = "UND_NAME";
                    cmbUnderwriter.ValueMember = "UND_ID";
                    cmbUnderwriter.SelectedValue = "";
                }
            }
             
            if (pr5.Successed == true)
            {
                if (autbChannels != null)
                {
                    //NewBISSvcRef.AUTB_CHANNEL blank = new NewBISSvcRef.AUTB_CHANNEL();
                    //blank.DESCRIPTION = "ทั้งหมด";
                    //blank.CHANNEL_TYPE = "";
                    //autbChannels.Add(blank);

                    cmbchannel.DataSource = autbChannels;
                    cmbchannel.DisplayMember = "DESCRIPTION";
                    cmbchannel.ValueMember = "CHANNEL_TYPE";
                    cmbchannel.SelectedValue = "OD";
                }
            }

            bindingWorkgroup();

            cmbchannel.Focus();
        }

        private void bindingWorkgroup()
        {
            NewBISSvcRef.ProcessResult pr = new NewBISSvcRef.ProcessResult();
            NewBISSvcRef.AUTB_DATADIC_DET_COLLECTION autbDataDicDets = new NewBISSvcRef.AUTB_DATADIC_DET_COLLECTION();
            using (NewBISSvcRef.NewBISSvcClient client = new NewBISSvcRef.NewBISSvcClient())
            {
                try
                {
                    autbDataDicDets = client.GetWorkGroup(out pr, cmbchannel.SelectedValue.ToString());
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                if (client.State != System.ServiceModel.CommunicationState.Closed)
                {
                    client.Close();
                }
            }

            if (pr.Successed == true)
            {
                if (autbDataDicDets != null)
                {
                    NewBISSvcRef.AUTB_DATADIC_DET blank = new NewBISSvcRef.AUTB_DATADIC_DET();
                    blank.DESCRIPTION = "ทั้งหมด";
                    blank.CODE = "";
                    autbDataDicDets.Add(blank);

                    cmbWorkGroup.DataSource = autbDataDicDets;
                    cmbWorkGroup.DisplayMember = "DESCRIPTION";
                    cmbWorkGroup.ValueMember = "CODE";
                    cmbWorkGroup.SelectedValue = "";
                }
            }
        }

        private void searchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            try
            { 
                search();   
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }


        private void search()
        {
            dtgCallReport.DataSource = null;
            dtgCallReport.AutoGenerateColumns = false;
            NewBISSvcRef.ProcessResult pr = new NewBISSvcRef.ProcessResult();
            NewBISSvcRef.REPORT_REJECTION_SEARCH_LIST_Collection ressearchColl = new NewBISSvcRef.REPORT_REJECTION_SEARCH_LIST_Collection();

            DateTime? sd = Utility.StringToDateTime(txtdatebegin.Text, "BU");
            DateTime? ed = Utility.StringToDateTime(txtdateend.Text, "BU");

            if ((txtdatebegin.Text == "" && txtdateend.Text != "") || (txtdatebegin.Text != "" && txtdateend.Text == ""))
            {
                MessageBox.Show("ท่านกรอกวันที่บันทึกไม่ครบถ้วน");
                txtdatebegin.Focus();
            }
            else if ((txtdatebegin.Text == "" || txtdateend.Text == "") && (txthrbegin.Text != "" || txthrend.Text != "" || txtminbegin.Text != "" || txtminend.Text != ""))
            {
                MessageBox.Show("โปรดกรอกวันที่บันทึกเริ่มต้นให้ครบถ้วนก่อนกรอกเวลา");
                txtdatebegin.Focus();
            }
            else if ((txtdatebegin.Text != "" && txtdateend.Text != "") && sd == null)
            {
                MessageBox.Show("รูปแบบวันที่บันทึกไม่ถูกต้อง");
                txtdatebegin.Focus();
            }
            else if ((txtdatebegin.Text != "" && txtdateend.Text != "") && ed == null)
            {
                MessageBox.Show("รูปแบบวันที่บันทึกไม่ถูกต้อง");
                txtdateend.Focus();
            }
            else if ((txtdatebegin.Text != "" && txtdateend.Text != "") && (sd != null && ed != null) && (DateTime.Compare((DateTime)sd, (DateTime)ed) > 0))
            {
                MessageBox.Show("วันที่บันทึกเริ่มต้นต้องมาก่อน");
                txtdatebegin.Focus();
            }
            else if (txthrbegin.Text != "" && txthrend.Text != "" && txtminbegin.Text != "" && txtminend.Text != "")
            {
                GetData(ref pr, ref ressearchColl);
            }
            else if (txthrbegin.Text == "" && txthrend.Text == "" && txtminbegin.Text == "" && txtminend.Text == "")
            {
                GetData(ref pr, ref ressearchColl);
            }
            else if (txthrbegin.Text == "" || txthrend.Text != "" || txtminbegin.Text != "" || txtminend.Text != "")
            {
                MessageBox.Show("ท่านกรอกเวลาที่บันทึกไม่ครบถ้วน");
                txthrbegin.Focus();
            }
        }

        private void GetData(ref NewBISSvcRef.ProcessResult pr, ref NewBISSvcRef.REPORT_REJECTION_SEARCH_LIST_Collection ressearchColl)
        {
            DateTime? sDateTime = null;
            DateTime? eDateTime = null;

            if (txtdatebegin.Text != "" && txtdateend.Text != "")
            {
                DateTime startDateTime = (DateTime)Utility.StringToDateTime(txtdatebegin.Text, "BU");
                DateTime endDateTime = (DateTime)Utility.StringToDateTime(txtdateend.Text, "BU");

                if (txthrbegin.Text != "" && txthrend.Text != "" && txtminbegin.Text != "" && txtminend.Text != "")
                {
                    startDateTime = startDateTime.ChangeTime(Convert.ToInt32(txthrbegin.Text), Convert.ToInt32(txtminbegin.Text), 0, 0);
                    endDateTime = endDateTime.ChangeTime(Convert.ToInt32(txthrend.Text), Convert.ToInt32(txtminend.Text), 0, 0);
                }
                else if (txthrbegin.Text == "" && txthrend.Text == "" && txtminbegin.Text == "" && txtminend.Text == "")
                {
                    startDateTime = startDateTime.ChangeTime(0, 0, 0, 0);
                    endDateTime = endDateTime.ChangeTime(0, 0, 0, 0);
                }

                sDateTime = startDateTime;
                eDateTime = endDateTime;
            }

            string appnobegin = "";
            string appnoend = "";

            if (txtappnobegin.Text != "" && txtappnoend.Text != "")
            {
                appnobegin = txtappnobegin.Text.Trim().ToUpper();
                appnoend = txtappnoend.Text.Trim().ToUpper();
            }
            else if (txtappnobegin.Text != "" && txtappnoend.Text == "")
            {
                appnobegin = txtappnobegin.Text.Trim().ToUpper();
                appnoend = txtappnobegin.Text.Trim().ToUpper();
            }
            else if (txtappnobegin.Text == "" && txtappnoend.Text != "")
            {
                appnobegin = txtappnoend.Text.Trim().ToUpper();
                appnoend = txtappnoend.Text.Trim().ToUpper();
            }

            string accflag="";
            if (cmbaccount.SelectedValue == null)
            {
                accflag = "";
            }
            else
            {
                accflag = cmbaccount.SelectedValue.ToString();
            }

            using (NewBISSvcRef.NewBISSvcClient client = new NewBISSvcRef.NewBISSvcClient())
            {
                try
                {
                    if (cmbWorkGroup.SelectedValue.ToString() != "OD1" && cmbWorkGroup.SelectedValue.ToString() != "OD2")
                    {
                        ProvinceArr = null;
                    }

                    ResultColl = new NewBISSvcRef.REPORT_REJECTION_SEARCH_LIST_Collection();
                    ressearchColl = client.GetSearchReportRejectColl(out pr, cmbWorkGroup.SelectedValue.ToString(), cmbLetterType.SelectedValue.ToString(), cmbPrintHis.SelectedValue.ToString(), cmbUnderwriter.SelectedValue.ToString(), sDateTime, eDateTime, appnobegin, appnoend, cmbchannel.SelectedValue.ToString(), cmbreturnprm.SelectedValue.ToString(), accflag,ProvinceArr,txtpolicy.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                if (client.State != System.ServiceModel.CommunicationState.Closed)
                {
                    client.Close();
                }
            }

            if (pr.Successed == true)
            {
                if (ressearchColl != null)
                {
                    ITUtility.ProcessResult prAcc = new ITUtility.ProcessResult();
                    NewBISSvcRef.U_PAYMENT_REFERENCE[] upayrefs = null;
                    NewBISSvcRef.U_PAYMENT_REFERENCE upayref = new NewBISSvcRef.U_PAYMENT_REFERENCE();

                    foreach (NewBISSvcRef.REPORT_REJECTION_SEARCH_LIST r in ressearchColl)
                    {
                        r.real_approved = "";

                        if (r.approved == "อนุมัติแล้ว")
                        {
                            using (NewBISSvcRef.NewBISSvcClient client = new NewBISSvcRef.NewBISSvcClient())
                            {
                                long[] letter = { (long)r.Uletter_id };
                                client.GetCompositeDataU_PAYMENT_REFERENCE(out upayrefs, null, letter);
                            }

                            if (upayrefs != null)
                            {
                                r.real_approved = "ยังไม่อนุมัติ";

                                for (int m = 0; m < upayrefs.Length;m++ )
                                {
                                    upayref = upayrefs[m];  

                                using (AccountSvcRef.AccountServiceClient accClient = new AccountSvcRef.AccountServiceClient())
                                {
                                    string outPayOption = "";
                                    string outBank = "";
                                    string outBranch = "";
                                    string outAccNo = "";
                                    string outPayName = "";
                                    DateTime? outPayDate = null;

                                    prAcc = accClient.GET_AC_OPTPAY_BYREFID(out outPayOption, out outBank, out outBranch, out outAccNo, out outPayName, out outPayDate, "J70", upayref.PAYREF_ID);
                                    if (outAccNo != "" && outAccNo != null)
                                    {
                                        r.real_approved = "อนุมัติแล้ว";
                                    }
                                }
                                    //else
                                    //{
                                    //    r.real_approved = "ยังไม่อนุมัติ";
                                    //}
                                }
                            }
                        }
                        else
                        {
                            r.real_approved = "ยังไม่อนุมัติ";
                        }
                        if (r.newapp != "-")
                        { r.real_approved = "อนุมัติแล้ว"; }
                    }

                    if (cmbreturnprm.SelectedValue.ToString() == "Y" && cmbaccount.SelectedValue.ToString() == "Y") //มีเงินคืน-อนุมัต
                    {
                        var result =
                               from res in ressearchColl
                               where res.real_approved == "อนุมัติแล้ว" && res.ret != "ไม่มีเงินคืน"
                               select res;
                        ResultColl.AddRange(result.ToArray());
                        // ressearchColl.AddRange(result.ToArray());
                    }
                    else if (cmbreturnprm.SelectedValue.ToString() == "Y" && cmbaccount.SelectedValue.ToString() == "N") //มีเงินคืน - ยังไม่อนุมัติ
                    {
                        var result =
                            from res in ressearchColl
                            where res.real_approved == "ยังไม่อนุมัติ" && res.ret != "ไม่มีเงินคืน"
                            select res;
                        ResultColl.AddRange(result.ToArray());
                        // ressearchColl.AddRange(result.ToArray());
                    }
                    else if (cmbreturnprm.SelectedValue.ToString() == "N" || cmbreturnprm.SelectedValue.ToString() == "")
                    {
                        ResultColl = ressearchColl;
                    }

                    ressearchColl = new NewBISSvcRef.REPORT_REJECTION_SEARCH_LIST_Collection();
                    ressearchColl = ResultColl;

                    //if (pr.Successed == true)
                    //{
                    if (dtgCallReport != null)
                    {
                        dtgCallReport.DataSource = ressearchColl;
                        if (cmbLetterType.SelectedValue.ToString() != "SM" && cmbLetterType.SelectedValue.ToString() != "AS")
                        {
                            foreach (DataGridViewRow dr in dtgCallReport.Rows)
                            {
                                if (dr.DataBoundItem is NewBISSvcRef.REPORT_REJECTION_SEARCH_LIST)
                                {
                                    NewBISSvcRef.REPORT_REJECTION_SEARCH_LIST obj = (NewBISSvcRef.REPORT_REJECTION_SEARCH_LIST)dr.DataBoundItem;

                                    #region " check ว่าอนุมัติยัง"

                                    //string approved = "";
                                    //if (obj.approved == "อนุมัติแล้ว")
                                    //{
                                    //    using (NewBISSvcRef.NewBISSvcClient client = new NewBISSvcRef.NewBISSvcClient())
                                    //    {
                                    //        long[] letter = { (long)obj.Uletter_id };
                                    //        client.GetCompositeDataU_PAYMENT_REFERENCE(out upayrefs, null, letter);
                                    //    }

                                    //    if (upayrefs != null)
                                    //    {
                                    //        upayref = upayrefs[0];


                                    //        using (AccountSvcRef.AccountServiceClient accClient = new AccountSvcRef.AccountServiceClient())
                                    //        {
                                    //            string outPayOption = "";
                                    //            string outBank = "";
                                    //            string outBranch = "";
                                    //            string outAccNo = "";
                                    //            string outPayName = "";
                                    //            DateTime? outPayDate = null;

                                    //            prAcc = accClient.GET_AC_OPTPAY_BYREFID(out outPayOption, out outBank, out outBranch, out outAccNo, out outPayName, out outPayDate, "J70", upayref.PAYREF_ID);
                                    //            if (outPayOption != "")
                                    //            {
                                    //                approved = "อนุมัติแล้ว";
                                    //            }
                                    //            else
                                    //            {
                                    //                approved = "ยังไม่อนุมัติ";
                                    //            }
                                    //        }
                                    //    }
                                    //}
                                    //else
                                    //{
                                    //    approved = "ยังไม่อนุมัติ";
                                    //}

                                    #endregion

                                    dr.Cells["clmPrintCheck"].Value = "Y";
                                    dr.Cells["clmAppNo"].Value = obj.App_no;
                                    dr.Cells["clmCustName"].Value = obj.Custname;
                                    dr.Cells["clmUpdName"].Value = obj.Upd_name;
                                    dr.Cells["clmUappID"].Value = obj.Uapp_id;
                                    dr.Cells["clmAppID"].Value = obj.App_id;
                                    dr.Cells["clmUpdID"].Value = obj.Upd_id;
                                    dr.Cells["clmUletterID"].Value = obj.Uletter_id;
                                    dr.Cells["clmstatus"].Value = obj.status;
                                    dr.Cells["clmsubstatus"].Value = obj.substatus;
                                    dr.Cells["clmprintdate"].Value = obj.printdate;
                                    dr.Cells["clmprintby"].Value = obj.printby;
                                    dr.Cells["clmqty"].Value = obj.qty;
                                    dr.Cells["clmsignature"].Value = obj.signature;
                                    //dr.Cells["clmedit"].Value = "แก้ไข";
                                    dr.Cells["clmsignature_id"].Value = obj.signature_id;
                                    dr.Cells["clmmoney"].Value = obj.newapp == "-" ? obj.ret : obj.newapp;
                                    dr.Cells["clmok"].Value = dr.Cells["clmmoney"].Value.ToString() == "มีเงินคืน" ? obj.real_approved : "";
                                    dr.Cells["clmchanneltype"].Value = obj.channel_type;
                                    dr.Cells["clmpolicyholding"].Value = obj.policy_holding;
                                    dr.Cells["clmreference"].Value = obj.reference;
                                    dr.Cells["clmsendofc"].Value = obj.send_ofc;
                                    dr.Cells["clmplan"].Value = obj.plan;
                                }
                            }
                        }
                        else
                        {
                            foreach (DataGridViewRow dr in dtgCallReport.Rows)
                            {
                                if (dr.DataBoundItem is NewBISSvcRef.REPORT_REJECTION_SEARCH_LIST)
                                {
                                    NewBISSvcRef.REPORT_REJECTION_SEARCH_LIST obj = (NewBISSvcRef.REPORT_REJECTION_SEARCH_LIST)dr.DataBoundItem;

                                    #region " check ว่าอนุมัติยัง"

                                    //string approved = "";
                                    //if (obj.approved == "อนุมัติแล้ว")
                                    //{
                                    //    using (NewBISSvcRef.NewBISSvcClient client = new NewBISSvcRef.NewBISSvcClient())
                                    //    {
                                    //        long[] letter = { (long)obj.Uletter_id };
                                    //        client.GetCompositeDataU_PAYMENT_REFERENCE(out upayrefs, null, letter);
                                    //    }

                                    //    if (upayrefs != null)
                                    //    {
                                    //        upayref = upayrefs[0];


                                    //        using (AccountSvcRef.AccountServiceClient accClient = new AccountSvcRef.AccountServiceClient())
                                    //        {
                                    //            string outPayOption = "";
                                    //            string outBank = "";
                                    //            string outBranch = "";
                                    //            string outAccNo = "";
                                    //            string outPayName = "";
                                    //            DateTime? outPayDate = null;

                                    //            prAcc = accClient.GET_AC_OPTPAY_BYREFID(out outPayOption, out outBank, out outBranch, out outAccNo, out outPayName, out outPayDate, "J70", upayref.PAYREF_ID);
                                    //            if (outPayOption != "")
                                    //            {
                                    //                approved = "อนุมัติแล้ว";
                                    //            }
                                    //            else
                                    //            {
                                    //                approved = "ยังไม่อนุมัติ";
                                    //            }
                                    //        }
                                    //    }
                                    //}
                                    //else
                                    //{
                                    //    approved = "ยังไม่อนุมัติ";
                                    //}

                                    #endregion

                                    dr.Cells["clmPrintCheck"].Value = "Y";
                                    dr.Cells["clmAppNo"].Value = obj.App_no;
                                    dr.Cells["clmCustName"].Value = obj.Custname;
                                    dr.Cells["clmUpdName"].Value = obj.Upd_name;
                                    dr.Cells["clmUappID"].Value = obj.Uapp_id;
                                    dr.Cells["clmAppID"].Value = obj.App_id;
                                    dr.Cells["clmUpdID"].Value = obj.Upd_id;
                                    dr.Cells["clmUletterID"].Value = obj.Uletter_id;
                                    dr.Cells["clmstatus"].Value = obj.status;
                                    dr.Cells["clmsubstatus"].Value = obj.substatus;
                                    dr.Cells["clmprintdate"].Value = obj.printdate;
                                    dr.Cells["clmprintby"].Value = obj.printby;
                                    dr.Cells["clmqty"].Value = obj.qty;
                                    dr.Cells["clmsignature"].Value = obj.signature;
                                    //dr.Cells["clmedit"].Value = "แก้ไข";
                                    dr.Cells["clmsignature_id"].Value = obj.signature_id;
                                    dr.Cells["clmmoney"].Value = "-";
                                    dr.Cells["clmok"].Value = "-";
                                    dr.Cells["clmchanneltype"].Value = obj.channel_type;
                                    dr.Cells["clmpolicyholding"].Value = "-";
                                    dr.Cells["clmreference"].Value = obj.reference;
                                }
                            }
                        }

                        lblCount.Text = "จำนวน " + dtgCallReport.RowCount + " Records";
                        lblCount.Visible = true;
                    }
                    else
                    {
                        lblCount.Visible = false;
                        MessageBox.Show("ไม่พบข้อมูลจดหมายตามเงื่อนไขที่ท่านเลือก");
                    }
                }
                }
                else
                {
                    MessageBox.Show(pr.ErrorMessage);
                    dtgCallReport.DataSource = null;
                }  
            if (dtgCallReport.RowCount < 1)
            {
                lblCount.Visible = false;
                MessageBox.Show("ไม่พบข้อมูลจดหมายตามเงื่อนไขที่ท่านเลือก");
            }
        } 

        private void newJobToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //dtpStartDt.Value = DateTime.Now;
            //dtpEndDt.Value = DateTime.Now;
            pnliso.Visible = false;

            txtdatebegin.Text = DateTime.Now.Day.ToString() + "/" + DateTime.Now.Month.ToString() + "/" + DateTime.Now.ToString("yyyy", System.Globalization.CultureInfo.GetCultureInfo("th-TH"));
            txtdateend.Text = DateTime.Now.Day.ToString() + "/" + DateTime.Now.Month.ToString() + "/" + DateTime.Now.ToString("yyyy", System.Globalization.CultureInfo.GetCultureInfo("th-TH"));
            txthrbegin.Text = "00";// DateTime.Now.Hour.ToString().Length == 1 ? "0" + DateTime.Now.Hour.ToString() : DateTime.Now.Hour.ToString();
            txthrend.Text = DateTime.Now.Hour.ToString().Length == 1 ? "0" + DateTime.Now.Hour.ToString() : DateTime.Now.Hour.ToString();
            txtminbegin.Text = "00"; // DateTime.Now.Minute.ToString().Length == 1 ? "0" + DateTime.Now.Minute.ToString() : DateTime.Now.Minute.ToString();
            txtminend.Text = DateTime.Now.Minute.ToString().Length == 1 ? "0" + DateTime.Now.Minute.ToString() : DateTime.Now.Minute.ToString(); 

            dtgCallReport.DataSource = null;
            cmbWorkGroup.SelectedValue = "";
            cmbLetterType.SelectedValue = "";
            cmbPrintHis.SelectedValue = "";
            cmbUnderwriter.SelectedValue = "";
            cmbchannel.SelectedValue = "OD"; 
            txtappnobegin.Text = "";
            txtappnoend.Text = "";
            lblCount.Visible = false;
            cmbWorkGroup.Focus();
            cmbaccount.SelectedValue = "";
            cmbreturnprm.SelectedValue = "";
            lblaccount.Visible = false;
            cmbaccount.Visible = false;
            pnlpolicy.Visible = false;
        }

        private void clearFile()
        {
            System.IO.DirectoryInfo directoryPath = new System.IO.DirectoryInfo(dirPath);
            if (!directoryPath.Exists)
            {
                System.IO.Directory.CreateDirectory(dirPath);
            }

            string[] filePaths = Directory.GetFiles(dirPath);

            try
            {
                foreach (string filePath in filePaths)
                {
                    if (filePath.StartsWith("C:\\WINDOWS\\Temp\\NewbizReport\\"))
                    {
                        File.Delete(filePath);
                    }
                }
            }
            catch (Exception ex)
            { }
        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                clearFile();
                if (cmbLetterType.SelectedValue.ToString() != "SM" && cmbLetterType.SelectedValue.ToString() != "AS")
                {
                    checkPrint = false;
                    List<string> appNoArray = new List<string>();
                    appNoArray.Clear();
                    if (dtgCallReport.RowCount > 0)
                    {
                        dtgCallReport.EndEdit();
                        RepSelectPrntColl.Clear();
                        string checkSendMail = "N";

                        foreach (DataGridViewRow row in dtgCallReport.Rows)
                        {
                            string check = "";
                            if (row.Cells["clmPrintCheck"].Value == null)
                            {
                                check = "N";
                            }
                            else
                            {
                                check = row.Cells["clmPrintCheck"].Value.ToString();
                            }
                            
                            if (check == "Y")
                            {
                                checkPrint = true;
                                REPORT_SELECT_PRINT RepSelectPrnt = new REPORT_SELECT_PRINT();
                                RepSelectPrnt.AppNo = row.Cells["clmAppNo"].Value.ToString();
                                RepSelectPrnt.UletterId = Convert.ToInt64(row.Cells["clmUletterID"].Value.ToString());
                                RepSelectPrnt.UserId = UserID;
                                RepSelectPrnt.approved = row.Cells["clmok"].Value == null ? "Y" : (row.Cells["clmok"].Value.ToString() == "ยังไม่อนุมัติ" ? "N" : "Y");
                                RepSelectPrnt.printDate = row.Cells["clmprintdate"].Value == null || row.Cells["clmprintdate"].Value == "" || row.Cells["clmprintdate"].Value == "-" ? "" : row.Cells["clmprintdate"].Value.ToString();
                                RepSelectPrnt.channelType = row.Cells["clmchanneltype"].Value == null || row.Cells["clmchanneltype"].Value == "" || row.Cells["clmchanneltype"].Value == "-" ? "" : row.Cells["clmchanneltype"].Value.ToString();
                                RepSelectPrnt.policyHolding = row.Cells["clmpolicyholding"].Value == null || row.Cells["clmpolicyholding"].Value == "" || row.Cells["clmpolicyholding"].Value == "-" ? "" : row.Cells["clmpolicyholding"].Value.ToString();
                                RepSelectPrnt.plan = "";

                                if(row.Cells["clmplan"].Value != null)
                                {
                                    if (planTelSendMail.Contains(row.Cells["clmplan"].Value.ToString()))//planTelSendMail
                                    {
                                        checkSendMail = "Y";
                                        RepSelectPrnt.plan = row.Cells["clmplan"].Value.ToString();
                                    }
                                }

                                RepSelectPrntColl.Add(RepSelectPrnt);
                            }
                        }

                        if (checkPrint)
                        {
                            if (checkSendMail == "N")
                            {
                                frmRejectReportPDF repRej = new frmRejectReportPDF(RepSelectPrntColl);
                                repRej.Show();
                            }
                            //else 
                            //{
                            //    //show แบบ lock
                            //    frmRejectReportPDF repRej = new frmRejectReportPDF(RepSelectPrntColl);
                            //    repRej.ShowDialog();
                                 
                            //    if (MessageBox.Show("ต้องการส่งเมล์แนบจดหมายนี้ไปยังเจ้าหน้าที่ IDBL หรือไม่?", "Send Mail", MessageBoxButtons.YesNo) == DialogResult.Yes)
                            //    {
                            //        this.Cursor = Cursors.WaitCursor;
                            //        try
                            //        {
                            //            string checkApp = "";
                            //            foreach (REPORT_SELECT_PRINT rep in RepSelectPrntColl)
                            //            {
                            //                if (checkApp != rep.AppNo && rep.plan != "")
                            //                {
                            //                    checkApp = rep.AppNo;
                            //                    frmSendRejectLetterByeMail appRep = new frmSendRejectLetterByeMail(rep.AppNo, (long)rep.UletterId, UserID);
                            //                    appRep.Show();
                            //                }
                            //            }
                            //        }
                            //        finally
                            //        {
                            //            this.Cursor = Cursors.Default;
                            //            MessageBox.Show("ส่ง email ไปยัง IDBL เรียบร้อยแล้ว");
                            //        }
                            //    }

                            //}

                        }
                        else
                        {
                            MessageBox.Show("กรุณาเลือกข้อมูลที่ต้องการพิมพ์              ", "ระบบพิจารณารับประกัน", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        MessageBox.Show("กรุณาดึงข้อมูลก่อน              ", "ระบบพิจารณารับประกัน", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else if (cmbLetterType.SelectedValue.ToString() == "SM")
                { 
                    ////////////////////////
                    checkPrint = false;  
                    if (dtgCallReport.RowCount > 0)
                    {
                        dtgCallReport.EndEdit();
                        RepSelectPrntColl.Clear();
                        List<string> letterReference = new List<string>();
                        List<string> letterids = new List<string>();

                        foreach (DataGridViewRow row in dtgCallReport.Rows)
                        {
                            string check = "";
                            if (row.Cells["clmPrintCheck"].Value == null)
                            {
                                check = "N";
                            }
                            else
                            {
                                check = row.Cells["clmPrintCheck"].Value.ToString();
                            }
                            
                            if (check == "Y")
                            {
                                checkPrint = true;
                                letterReference.Add(row.Cells["clmreference"].Value.ToString());
                                letterids.Add(row.Cells["clmUletterID"].Value.ToString());
                            }
                        }

                        if (checkPrint)
                        {
                            using (PolicySvcRef.PolicySvcClient client = new PolicySvcRef.PolicySvcClient())
                            {
                                try
                                {
                                    client.StampSmartPrintFlag(letterids.ToArray(), UserID);
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.ToString());
                                }

                                if (client.State != System.ServiceModel.CommunicationState.Closed)
                                {
                                    client.Close();
                                }
                            }

                            //frmViewReport repRej = new frmViewReport(letterReference);
                            //repRej.Show();
                        }
                        else
                        {
                            MessageBox.Show("กรุณาเลือกข้อมูลที่ต้องการพิมพ์              ", "ระบบพิจารณารับประกัน", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        MessageBox.Show("กรุณาดึงข้อมูลก่อน              ", "ระบบพิจารณารับประกัน", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    ///////////////////////////
                }
                else if (cmbLetterType.SelectedValue.ToString() == "AS")
                {
                    ////////////////////////
                    checkPrint = false;
                    if (dtgCallReport.RowCount > 0)
                    {
                        dtgCallReport.EndEdit();
                        RepSelectPrntColl.Clear();
                        List<string> letterReference = new List<string>();
                        List<string> letterids = new List<string>();

                        foreach (DataGridViewRow row in dtgCallReport.Rows)
                        {
                            string check = "";
                            if (row.Cells["clmPrintCheck"].Value == null)
                            {
                                check = "N";
                            }
                            else
                            {
                                check = row.Cells["clmPrintCheck"].Value.ToString();
                            }

                            if (check == "Y")
                            {
                                checkPrint = true;
                                letterReference.Add(row.Cells["clmreference"].Value.ToString());
                                letterids.Add(row.Cells["clmUletterID"].Value.ToString());
                            }
                        }

                        //if (checkPrint)
                        //{
                        //    using (PolicySvcRef.PolicySvcClient client = new PolicySvcRef.PolicySvcClient())
                        //    {
                        //        try
                        //        {
                        //            client.StampSmartPrintFlag(letterids.ToArray(), UserID);
                        //        }
                        //        catch (Exception ex)
                        //        {
                        //            MessageBox.Show(ex.ToString());
                        //        }

                        //        if (client.State != System.ServiceModel.CommunicationState.Closed)
                        //        {
                        //            client.Close();
                        //        }
                        //    }

                        //    frmViewReportAS repRej = new frmViewReportAS(letterReference);
                        //    repRej.Show();
                        //}
                        //else
                        //{
                        //    MessageBox.Show("กรุณาเลือกข้อมูลที่ต้องการพิมพ์              ", "ระบบพิจารณารับประกัน", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //}
                    }
                    else
                    {
                        MessageBox.Show("กรุณาดึงข้อมูลก่อน              ", "ระบบพิจารณารับประกัน", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    ///////////////////////////
                }
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        //private void cmbchannel_SelectedIndexChanged(object sender, EventArgs e)
        //{

        //}

        private void txtdatebegin_Leave(object sender, EventArgs e)
        {
            ChkDateForTextBox(txtdatebegin.Text, "วันที่บันทึก", txtdatebegin);
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

        private void txtdateend_Leave(object sender, EventArgs e)
        {
            ChkDateForTextBox(txtdateend.Text, "วันที่บันทึก", txtdateend);
        } 

        private void txthrend_Leave(object sender, EventArgs e)
        {
            if (txthrend.Text != "")
            {
                if (Convert.ToInt32(txthrend.Text) > 24)
                {
                    MessageBox.Show("กรอกเวลาไม่ถูกต้อง");
                    txthrend.Focus();
                }
            }
        }

        private void txthrbegin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < '0' || e.KeyChar > '9') && (e.KeyChar != '\b'))
            {
                e.Handled = true;
                return;
            }
        }

        private void txthrend_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < '0' || e.KeyChar > '9') && (e.KeyChar != '\b'))
            {
                e.Handled = true;
                return;
            }
        }

        private void txtminbegin_Leave(object sender, EventArgs e)
        {
            if (txtminbegin.Text != "")
            {
                if (Convert.ToInt32(txtminbegin.Text) > 60)
                {
                    MessageBox.Show("กรอกเวลาไม่ถูกต้อง");
                    txtminbegin.Focus();
                }
            }
        }

        private void txtminend_Leave(object sender, EventArgs e)
        {
            if (txtminend.Text != "")
            {
                if (Convert.ToInt32(txtminend.Text) > 60)
                {
                    MessageBox.Show("กรอกเวลาไม่ถูกต้อง");
                    txtminend.Focus();
                }
            }
        }

        private void txthrbegin_Leave(object sender, EventArgs e)
        {
            if (txthrbegin.Text != "")
            {
                if (Convert.ToInt32(txthrbegin.Text) > 24)
                {
                    MessageBox.Show("กรอกเวลาไม่ถูกต้อง");
                    txthrbegin.Focus();
                }
            }
        }

        private void txtminbegin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < '0' || e.KeyChar > '9') && (e.KeyChar != '\b'))
            {
                e.Handled = true;
                return;
            }
        }

        private void txtminend_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < '0' || e.KeyChar > '9') && (e.KeyChar != '\b'))
            {
                e.Handled = true;
                return;
            }
        }

        private void dtgCallReport_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || (e.ColumnIndex != dtgCallReport.Columns["clmedit"].Index))
            {
                return;
            }
            //else if (e.ColumnIndex == dtgCallReport.Columns["clmedit"].Index)
            //{
            //    if (cmbLetterType.SelectedValue.ToString() != "SM")
            //    {
            //        try
            //        {
            //            string signature = "";
            //            if (dtgCallReport["clmsignature_id", e.RowIndex].Value == null)
            //            {
            //                signature = "";
            //            }
            //            else
            //            {
            //                signature = dtgCallReport["clmsignature_id", e.RowIndex].Value.ToString();
            //            }
            //            DlgEditLetter dlg = new DlgEditLetter((long)dtgCallReport["clmuletterid", e.RowIndex].Value, signature, UserID, dtgCallReport["clmstatus", e.RowIndex].Value.ToString(), 0, null);
            //            dlg.ShowDialog(this);
            //            search();

            //        }
            //        catch (Exception ex)
            //        {
            //            MessageBox.Show(ex.Message);
            //        }
            //    }
            //    else
            //    {
            //        MessageBox.Show("จดหมาย Smart ไม่มีการใช้งานในส่วนนี้");
            //    }
            //}
        }

        private void frmRejectionReport_FormClosing(object sender, FormClosingEventArgs e)
        {
            System.IO.DirectoryInfo directoryPath = new System.IO.DirectoryInfo(dirPath);
            if (!directoryPath.Exists)
            {
                System.IO.Directory.CreateDirectory(dirPath);
            }

            string[] filePaths = Directory.GetFiles(dirPath);

            try
            {
                foreach (string filePath in filePaths)
                {
                    if (filePath.StartsWith("C:\\WINDOWS\\Temp\\NewbizReport\\reportRejectFinalPDF"))
                    {
                        File.Delete(filePath);
                    }
                }
            }
            catch (Exception ex)
            { }
        }

        private void cmbreturnprm_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cmbreturnprm.SelectedValue.ToString() == "Y")
            {
                lblaccount.Visible = true;
                cmbaccount.Visible = true;
            }
            else if (cmbreturnprm.SelectedValue.ToString() == "N" || cmbreturnprm.SelectedValue.ToString() == "")
            {
                lblaccount.Visible = false;
                cmbaccount.Visible = false;
            }
        }

        private void txtdatebegin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                setFormatDate(e, txtdatebegin);
                txthrbegin.Focus();
            } 
        }

        private void setFormatDate(KeyEventArgs e, TextBox t)
        {
            if ((t.Text.Length < 8 || t.Text.Length > 8) && !(t.Text.Contains("/")))
            {
                MessageBox.Show(new Form() { TopMost = true }, "โปรดกรอกวันที่ให้ตรงตามรูปแบบ");
            }
            else if (!t.Text.Contains("/"))
            {
                t.Text = t.Text.Substring(0, 2) + "/" + t.Text.Substring(2, 2) + "/" + t.Text.Substring(4, 4);
            }
        }

        private void txtdateend_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                setFormatDate(e, txtdateend);
                txtdateend.Focus();
            } 
        }

        private void cmbchannel_SelectionChangeCommitted(object sender, EventArgs e)
        {
            bindingWorkgroup();
        }

        private void cmbLetterType_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cmbLetterType.SelectedValue.ToString() == "SM" || cmbLetterType.SelectedValue.ToString() == "AS")
            {
                pnlpolicy.Visible = true;
            }
            else if ((cmbLetterType.SelectedValue.ToString() == "SM") && (cmbWorkGroup.SelectedValue.ToString() == "OD1" || cmbWorkGroup.SelectedValue.ToString() == "OD2"))
            {
                pnliso.Visible = true;
                cmbiso.DataSource = null;

                DataTable iso = new DataTable();
                iso.Columns.Add(new DataColumn("Display", typeof(string)));
                iso.Columns.Add(new DataColumn("Id", typeof(string)));
                iso.Rows.Add(iso.NewRow());
                iso.Rows[0][0] = "ไม่ระบุสาขา";
                iso.Rows[0][1] = "";
                iso.Rows.Add(iso.NewRow());
                iso.Rows[1][0] = "สาขา ISO";
                iso.Rows[1][1] = "Y";
                iso.Rows.Add(iso.NewRow());
                iso.Rows[2][0] = "สาขาไม่มี ISO";
                iso.Rows[2][1] = "N";

                cmbiso.DataSource = iso;
                cmbiso.DisplayMember = "Display";
                cmbiso.ValueMember = "Id";
                cmbiso.SelectedValue = "";

                 pnlpolicy.Visible = true;
            } 
            else
            {
                pnlpolicy.Visible = false;
                pnliso.Visible = false;
            }
        }

        private void cmbiso_SelectionChangeCommitted(object sender, EventArgs e)
        {
            ProvinceArr = null;
            if (cmbiso.SelectedValue.ToString() == "Y" || cmbiso.SelectedValue.ToString() == "N")
            {
                ProvinceArr = getAllOfficeByISO();
            }
        }

        private string[] getAllOfficeByISO()
        {
            List<string> provinceTemp = new List<string>();
            CenterSvcRef.Office[] officeArr;
            CenterSvcRef.ProcessResult prOffice = new CenterSvcRef.ProcessResult();
            using (CenterSvcRef.CenterServiceClient centerClient = new CenterSvcRef.CenterServiceClient())
            { 
                string workgroup = cmbWorkGroup.SelectedValue.ToString();
                if (workgroup == "OD1" || workgroup == "OD2")
                {
                    workgroup = "";
                }
                officeArr = centerClient.GetOfficeByIsoRegion(out prOffice, cmbiso.SelectedValue.ToString(), workgroup);
                if (prOffice.Successed == false)
                {
                    throw new Exception(prOffice.ErrorMessage);
                }
                if (centerClient.State != System.ServiceModel.CommunicationState.Closed)
                {
                    centerClient.Close();
                }
            }

            if (prOffice.Successed == true)
            {
                foreach (CenterSvcRef.Office x in officeArr)
                {
                    provinceTemp.Add(x.Code);
                }
            }

            return provinceTemp.ToArray();
        }

        private void cmbWorkGroup_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if ((cmbLetterType.SelectedValue.ToString() == "SM") && (cmbWorkGroup.SelectedValue.ToString() == "OD1" || cmbWorkGroup.SelectedValue.ToString() == "OD2"))
            {
                pnliso.Visible = true;
                cmbiso.DataSource = null;

                DataTable iso = new DataTable();
                iso.Columns.Add(new DataColumn("Display", typeof(string)));
                iso.Columns.Add(new DataColumn("Id", typeof(string)));
                iso.Rows.Add(iso.NewRow());
                iso.Rows[0][0] = "ไม่ระบุสาขา";
                iso.Rows[0][1] = "";
                iso.Rows.Add(iso.NewRow());
                iso.Rows[1][0] = "สาขา ISO";
                iso.Rows[1][1] = "Y";
                iso.Rows.Add(iso.NewRow());
                iso.Rows[2][0] = "สาขาไม่มี ISO";
                iso.Rows[2][1] = "N";

                cmbiso.DataSource = iso;
                cmbiso.DisplayMember = "Display";
                cmbiso.ValueMember = "Id";
                cmbiso.SelectedValue = "";
            }
            else
            {
                pnliso.Visible = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        { 
            NewBISSvcRef.NewBISSvcClient client = new NewBISSvcRef.NewBISSvcClient();
            client.AcknowledgeDocFromISIS(53826, "050D104D429E148CE054001517713782", "ชร", "000593", "065FD08658A170F3E054001517713782");
        
        }

        //private void button1_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        NewBISSvcRef.NewBISSvcClient c = new NewBISSvcRef.NewBISSvcClient();
        //        NewBISSvcRef.ProcessResult pr = new NewBISSvcRef.ProcessResult();
        //        pr = c.TransferSmartToOncesmart(2929, "002705");
        //        if (pr.Successed == false)
        //        {
        //            throw new Exception(pr.ErrorMessage);

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.ToString());
        //    }
        //}

        //private void button1_Click(object sender, EventArgs e)
        //{
        //    NewBISSvcRef.NewBISSvcClient client = new NewBISSvcRef.NewBISSvcClient();
        //    client.AcknowledgeDocFromISIS(10663, "DB90474EF6E50B52E044001517713782", "พจ2", "002760", "DDC480828FB031F4E044001517713782");
        //}
 
    }

    public static class DateModule  //Extension Method for changedate ind datetime variable
    {
        public static DateTime ChangeTime(this DateTime dateTime, int hours, int minutes, int seconds, int milliseconds)
        {
            return new DateTime(
                dateTime.Year,
                dateTime.Month,
                dateTime.Day,
                hours,
                minutes,
                seconds,
                milliseconds,
                dateTime.Kind);
        }
    }
}
