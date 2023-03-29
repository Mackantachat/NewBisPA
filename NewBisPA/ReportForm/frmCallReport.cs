using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NewBisPA.ctDocSvcRef;
using ITUtility;
using System.IO;

namespace NewBisPA.ReportForm
{
    public partial class frmCallReport : Form
    {
        private bool checkPrint;
        string[] planTelSendMail = { "A17901", "A17902", "A17903" };
        private string _UserId;

        public string UserId {
            get { return _UserId; }
            set { _UserId = value; }
        }

        public frmCallReport() {
            InitializeComponent();
        }

        public frmCallReport(string userId) {
            InitializeComponent();
            _UserId = userId;
        }

        private string dirPath = @"C:\WINDOWS\Temp\NewbizReport";

        private void frmCallReport_Load(object sender, EventArgs e) {
            clearFile();
            showHeadCallReport();
        }

        private void showHeadCallReport()
        {
            txtStartDt.Text = DateTime.Now.Day.ToString() + "/" + DateTime.Now.Month.ToString() + "/" + DateTime.Now.ToString("yyyy", System.Globalization.CultureInfo.GetCultureInfo("th-TH"));
            txtEndDt.Text = DateTime.Now.Day.ToString() + "/" + DateTime.Now.Month.ToString() + "/" + DateTime.Now.ToString("yyyy", System.Globalization.CultureInfo.GetCultureInfo("th-TH"));
            txthrbegin.Text = "00"; // DateTime.Now.Hour.ToString().Length == 1 ? "0" + DateTime.Now.Hour.ToString() : DateTime.Now.Hour.ToString();
            txthrend.Text = DateTime.Now.Hour.ToString().Length == 1 ? "0" + DateTime.Now.Hour.ToString() : DateTime.Now.Hour.ToString();
            txtminbegin.Text = "00"; // DateTime.Now.Minute.ToString().Length == 1 ? "0" + DateTime.Now.Minute.ToString() : DateTime.Now.Minute.ToString();
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

            DataTable letterList = new DataTable();
            letterList.Columns.Add(new DataColumn("Display", typeof(string)));
            letterList.Columns.Add(new DataColumn("Id", typeof(string)));
            letterList.Rows.Add(letterList.NewRow());
            letterList.Rows.Add(letterList.NewRow());
            letterList.Rows.Add(letterList.NewRow());
            letterList.Rows.Add(letterList.NewRow());
            letterList.Rows[0][0] = "จดหมายข้อเสนอใหม่ของบริษัทในการรับประกัน (CO)";
            letterList.Rows[0][1] = "CO";
            letterList.Rows[1][0] = "จดหมายขอให้ดำเนินการเพิ่มเติม (MO)";
            letterList.Rows[1][1] = "MO";
            letterList.Rows[2][0] = "จดหมายขอให้ดำเนินการเอกสารเพิ่มเติม (MD)";
            letterList.Rows[2][1] = "MD";
            letterList.Rows[3][0] = "จดหมายการชำระเบี้ยประกันภัย (AP)";
            letterList.Rows[3][1] = "AP";
            cmbLetterType.DataSource = letterList;
            cmbLetterType.DisplayMember = "Display";
            cmbLetterType.ValueMember = "Id";
            cmbLetterType.SelectedValue = "CO";

            NewBISSvcRef.ProcessResult pr = new NewBISSvcRef.ProcessResult();
            NewBISSvcRef.ProcessResult pr2 = new NewBISSvcRef.ProcessResult(); 
            NewBISSvcRef.AUTB_UNDERWRITER_COLLECTION autbColl = new NewBISSvcRef.AUTB_UNDERWRITER_COLLECTION();
            NewBISSvcRef.AUTB_UNDERWRITER autbUnder = new NewBISSvcRef.AUTB_UNDERWRITER();
            NewBISSvcRef.AUTB_CHANNEL_COLLECTION autbChannels = new NewBISSvcRef.AUTB_CHANNEL_COLLECTION();

            using (NewBISSvcRef.NewBISSvcClient client = new NewBISSvcRef.NewBISSvcClient())
            {
                try
                { 
                    autbChannels = client.GetAutbChannel(out pr);
                    autbColl = client.GetUnderWrite(out pr2, autbUnder);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                if (client.State != System.ServiceModel.CommunicationState.Closed)
                {
                    client.Close();
                }
            }
             
            if (pr.Successed == true)
            {
                if (autbChannels != null)
                {
                    NewBISSvcRef.AUTB_CHANNEL blank = new NewBISSvcRef.AUTB_CHANNEL();
                    blank.DESCRIPTION = "ทั้งหมด";
                    blank.CHANNEL_TYPE = "";
                    autbChannels.Add(blank);

                    cmbchannel.DataSource = autbChannels;
                    cmbchannel.DisplayMember = "DESCRIPTION";
                    cmbchannel.ValueMember = "CHANNEL_TYPE";
                    cmbchannel.SelectedValue = "OD";
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
             
            bindingWorkgroup();
             
            cmbchannel.Focus();
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
            NewBISSvcRef.REPORT_SEARCH_LIST_Collection ressearchColl = new NewBISSvcRef.REPORT_SEARCH_LIST_Collection();

            DateTime? stDt = Utility.StringToDateTime(txtStartDt.Text, "BU");
            DateTime? enDt = Utility.StringToDateTime(txtEndDt.Text, "BU");


            if ((txtStartDt.Text == "" && txtEndDt.Text != "") || (txtStartDt.Text != "" && txtEndDt.Text == ""))
            {
                MessageBox.Show("ท่านกรอกวันที่บันทึกไม่ครบถ้วน");
                txtStartDt.Focus();
            }
            else if ((txtStartDt.Text == "" || txtEndDt.Text == "") && (txthrbegin.Text != "" || txthrend.Text != "" || txtminbegin.Text != "" || txtminend.Text != ""))
            {
                MessageBox.Show("โปรดกรอกวันที่บันทึกเริ่มต้นให้ครบถ้วนก่อนกรอกเวลา");
                txtStartDt.Focus();
            }
            else if ((txtStartDt.Text != "" && txtEndDt.Text != "") && stDt == null)
            {
                MessageBox.Show("รูปแบบวันที่บันทึกไม่ถูกต้อง");
                txtStartDt.Focus();
            }
            else if ((txtStartDt.Text != "" && txtEndDt.Text != "") && enDt == null)
            {
                MessageBox.Show("รูปแบบวันที่บันทึกไม่ถูกต้อง");
                txtEndDt.Focus();
            }
            else if ((txtStartDt.Text != "" && txtEndDt.Text != "") && (stDt != null && enDt != null) && (DateTime.Compare((DateTime)stDt, (DateTime)enDt) > 0))
            {
                MessageBox.Show("วันที่บันทึกเริ่มต้นต้องมาก่อน");
                txtStartDt.Focus();
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

        private void GetData(ref NewBISSvcRef.ProcessResult pr, ref NewBISSvcRef.REPORT_SEARCH_LIST_Collection ressearchColl)
        {
            DateTime? sDateTime = null;
            DateTime? eDateTime = null;

            if (txtStartDt.Text != "" && txtEndDt.Text != "")
            {
                DateTime startDateTime = (DateTime)Utility.StringToDateTime(txtStartDt.Text, "BU");
                DateTime endDateTime = (DateTime)Utility.StringToDateTime(txtEndDt.Text, "BU");

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
            using (NewBISSvcRef.NewBISSvcClient client = new NewBISSvcRef.NewBISSvcClient())
            {
                try
                {
                    ressearchColl = client.GetSearchReportColl(out pr, cmbWorkGroup.SelectedValue.ToString(), cmbchannel.SelectedValue.ToString(), cmbLetterType.SelectedValue.ToString(), cmbPrintHis.SelectedValue.ToString(), cmbUnderwriter.SelectedValue.ToString(), sDateTime, eDateTime, txtAppNo.Text);
                }
                catch (Exception ex)
                {
                    throw ex;
                    MessageBox.Show(ex.Message);
                }
                if (client.State != System.ServiceModel.CommunicationState.Closed)
                {
                    client.Close();
                }
            }

            if (pr.Successed == true && ressearchColl != null)
            {
                var realSearchColl = from x in ressearchColl
                                     select x;

                string letterType = cmbLetterType.SelectedValue.ToString().Trim();

                if (txtAppNo.Text == "")
                {
                    realSearchColl = from x in ressearchColl
                                     where x.laststatus != null && x.laststatus == letterType && x.status != "ยกเลิก" //&& !(endStatus.Contains(x.laststatus))
                                     select x;
                } 

                if (dtgCallReport != null)
                {
                    dtgCallReport.DataSource = realSearchColl.ToArray();
                    foreach (DataGridViewRow dr in dtgCallReport.Rows)
                    {
                        if (dr.DataBoundItem is NewBISSvcRef.REPORT_SEARCH_LIST)
                        {
                            NewBISSvcRef.REPORT_SEARCH_LIST obj = (NewBISSvcRef.REPORT_SEARCH_LIST)dr.DataBoundItem;
                            dr.Cells["clmPrintCheck"].Value = obj.status.ToString() == "ยกเลิก" ? "N" : "Y";

                            //for send office
                            if (obj.office != null && obj.office != "" && obj.office != "สสง" && obj.office != "สนญ")
                            {
                                dr.Cells["clmsend"].Value = "Y";
                            }
                            else
                            {
                                dr.Cells["clmsend"].Value = "N";
                            }

                            dr.Cells["clmAppNo"].Value = obj.App_no;
                            dr.Cells["clmReference"].Value = obj.Reference;
                            dr.Cells["clmCustName"].Value = obj.Name_cust;
                            dr.Cells["clmAnswerLimitDT"].Value = Utility.dateTimeToString(Convert.ToDateTime(obj.Answer_limit_dt), "d Month yyyy", "BU");
                            dr.Cells["clmUpdName"].Value = obj.Upd_name;
                            dr.Cells["clmUappID"].Value = obj.Uapp_id;
                            dr.Cells["clmAppID"].Value = obj.App_id;
                            dr.Cells["clmUpdID"].Value = obj.Upd_id;
                            dr.Cells["clmUletterID"].Value = obj.Uletter_id;
                            dr.Cells["clmSummaryID"].Value = obj.Summary_id;
                            dr.Cells["clmLetterTypeID"].Value = obj.LetterType_id;
                            dr.Cells["clmqty"].Value = obj.qty;
                            dr.Cells["clmsignature"].Value = obj.signature;
                            dr.Cells["clmsignature_id"].Value = obj.signature_id;
                            dr.Cells["clmprintby"].Value = obj.printby;
                            dr.Cells["clmprintdate"].Value = obj.printdate;
                            dr.Cells["clmlimitdate"].Value = obj.Answer_limit_dt;
                            dr.Cells["clmchanneltype"].Value = obj.channel_type;
                            dr.Cells["clmpolicyholding"].Value = obj.policy_holding;
                            dr.Cells["clmpolicysend"].Value = obj.ofcsend;
                            dr.Cells["clmoffice"].Value = obj.office;
                            dr.Cells["clmplan"].Value = obj.plan;
                            if ((cmbLetterType.SelectedValue.ToString() == "MO") || (cmbLetterType.SelectedValue.ToString() == "MD"))
                            {
                                dtgCallReport.Columns["clmCountDoc"].Visible = true;
                                if (obj.CountDoc == "0")
                                {
                                    dr.Cells["clmCountDoc"].Value = "ไม่มีเอกสารแนบ";
                                }
                                else
                                {
                                    dr.Cells["clmCountDoc"].Value = "เอกสารแนบ  " + obj.CountDoc + "  ฉบับ";
                                }
                            }
                            else
                            {
                                dtgCallReport.Columns["clmCountDoc"].Visible = false;
                            }
                            dr.Cells["clmLetterStatus"].Value = obj.status;
                        }
                    }
                    lblCount.Text = "จำนวน " + dtgCallReport.RowCount + " Records";
                    lblCount.Visible = true;
                }
                //else
                //{
                //    lblCount.Visible = false;
                //    MessageBox.Show("ไม่พบข้อมูลจดหมายตามเงื่อนไขที่ท่านเลือก");
                //}
            }
            //else
            //{
            //    MessageBox.Show("ไม่พบข้อมูลจดหมายตามเงื่อนไขที่ท่านเลือก");
            //    dtgCallReport.DataSource = null;
            //} 

            if (dtgCallReport.RowCount < 1)
            {
                lblCount.Visible = false;
                MessageBox.Show("ไม่พบข้อมูลจดหมายตามเงื่อนไขที่ท่านเลือก");
            }
        }

        private void newJobToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtStartDt.Text = DateTime.Now.Day.ToString() + "/" + DateTime.Now.Month.ToString() + "/" + DateTime.Now.ToString("yyyy", System.Globalization.CultureInfo.GetCultureInfo("th-TH"));
            txtEndDt.Text = DateTime.Now.Day.ToString() + "/" + DateTime.Now.Month.ToString() + "/" + DateTime.Now.ToString("yyyy", System.Globalization.CultureInfo.GetCultureInfo("th-TH"));
            txthrbegin.Text = "00"; // DateTime.Now.Hour.ToString().Length == 1 ? "0" + DateTime.Now.Hour.ToString() : DateTime.Now.Hour.ToString();
            txthrend.Text = DateTime.Now.Hour.ToString().Length == 1 ? "0" + DateTime.Now.Hour.ToString() : DateTime.Now.Hour.ToString();
            txtminbegin.Text = "00"; // DateTime.Now.Minute.ToString().Length == 1 ? "0" + DateTime.Now.Minute.ToString() : DateTime.Now.Minute.ToString();
            txtminend.Text = DateTime.Now.Minute.ToString().Length == 1 ? "0" + DateTime.Now.Minute.ToString() : DateTime.Now.Minute.ToString(); 

          //  txtStartDt.Text = "";
            txtAppNo.Text = "";
           // txtEndDt.Text = "";
            dtgCallReport.DataSource = null;
            cmbWorkGroup.SelectedValue = "";
            cmbLetterType.SelectedValue = "CO";
            cmbPrintHis.SelectedValue = "";
            cmbUnderwriter.SelectedValue = "";
            lblCount.Visible = false;
            cmbWorkGroup.Focus();
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
                checkPrint = false;
                REPORT_SELECT_PRINT_Collection reportSelectColl = new REPORT_SELECT_PRINT_Collection();
                REPORT_SELECT_PRINT reportSelect;
                if (dtgCallReport.RowCount > 0)
                {
                    dtgCallReport.EndEdit();
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
                            NewBISSvcRef.ProcessResult prLt = new NewBISSvcRef.ProcessResult();
                            checkPrint = true;
                            reportSelect = new REPORT_SELECT_PRINT();
                            reportSelect.AppNo = row.Cells["clmAppNo"].Value.ToString();
                            reportSelect.UletterId = Convert.ToInt64(row.Cells["clmUletterID"].Value);
                            reportSelect.JobId = Convert.ToInt64(row.Cells["clmLetterTypeID"].Value);
                            string printdt = row.Cells["clmprintdate"].Value == null || row.Cells["clmprintdate"].Value == "" || row.Cells["clmprintdate"].Value == "-" ? "" : row.Cells["clmprintdate"].Value.ToString();
                            reportSelect.channelType = row.Cells["clmchanneltype"].Value == null || row.Cells["clmchanneltype"].Value == "" || row.Cells["clmchanneltype"].Value == "-" ? "" : row.Cells["clmchanneltype"].Value.ToString();
                            reportSelect.policyHolding = row.Cells["clmpolicyholding"].Value == null || row.Cells["clmpolicyholding"].Value == "" || row.Cells["clmpolicyholding"].Value == "-" ? "" : row.Cells["clmpolicyholding"].Value.ToString();
                            reportSelect.office = row.Cells["clmoffice"].Value==null? "":row.Cells["clmoffice"].Value.ToString();
                            reportSelect.sendofc = row.Cells["clmsend"].Value.ToString();

                            //if (row.Cells["clmplan"].Value != null)
                            //{
                            //    if (planTelSendMail.Contains(row.Cells["clmplan"].Value.ToString()))//planTelSendMail
                            //    {
                            //        checkSendMail = "Y";
                            //    }
                            //}
                            reportSelect.plan = "";

                            if (row.Cells["clmplan"].Value != null)
                            {
                                if (planTelSendMail.Contains(row.Cells["clmplan"].Value.ToString()))//planTelSendMail
                                {
                                    checkSendMail = "Y";
                                    reportSelect.plan = row.Cells["clmplan"].Value.ToString();
                                }
                            }


                            //reportSelectColl.Add(reportSelect);
                            using (NewBISSvcRef.NewBISSvcClient client = new NewBISSvcRef.NewBISSvcClient())
                            {
                                try
                                {
                                    if (printdt != "" && printdt != "-")
                                    {
                                        string firstPrintdt = client.GetLetterDt(out prLt, reportSelect.UletterId);
                                        if (firstPrintdt != "" || firstPrintdt != "-")
                                        {
                                            reportSelect.printDate = firstPrintdt;
                                        }
                                        else
                                        {
                                            reportSelect.printDate = Utility.dateTimeToString(DateTime.Now, "วันที่ d Month yyyy", "BU");
                                        }
                                    }
                                    else if (printdt == "" || printdt == "-")
                                    {
                                        reportSelect.printDate = Utility.dateTimeToString(DateTime.Now, "วันที่ d Month yyyy", "BU");
                                    }

                                    client.StampPrintFlag(Convert.ToInt64(row.Cells["clmUletterID"].Value.ToString()), _UserId,null);
                                }
                                catch (Exception ex)
                                {
                                    throw ex;
                                }
                                if (client.State != System.ServiceModel.CommunicationState.Closed)
                                {
                                    client.Close();
                                }
                            }

                            reportSelectColl.Add(reportSelect);
                        }
                    }
                    if (checkPrint)
                    {
                        if (checkSendMail == "N")
                        {
                            if ((cmbLetterType.SelectedValue.ToString() == "MO") || cmbLetterType.SelectedValue.ToString() == "MD")
                            {
                                frmReportMOPDF repMo = new frmReportMOPDF(reportSelectColl, UserId);
                                repMo.Show();
                            }
                            else if (cmbLetterType.SelectedValue.ToString() == "AP")
                            {
                                frmReportAPPDF repAp = new frmReportAPPDF(reportSelectColl, UserId);
                                repAp.Show();
                            }
                            else if (cmbLetterType.SelectedValue.ToString() == "CO")
                            {
                                frmReportCOPDF repCo = new frmReportCOPDF(reportSelectColl, UserId);
                                repCo.Show();
                            }
                        }
                        else
                        {
                            //show แบบ lock
                            if ((cmbLetterType.SelectedValue.ToString() == "MO") || cmbLetterType.SelectedValue.ToString() == "MD")
                            {
                                frmReportMOPDF repMo = new frmReportMOPDF(reportSelectColl, UserId);
                                repMo.ShowDialog();
                            }
                            else if (cmbLetterType.SelectedValue.ToString() == "AP")
                            {
                                frmReportAPPDF repAp = new frmReportAPPDF(reportSelectColl, UserId);
                                repAp.ShowDialog();
                            }
                            else if (cmbLetterType.SelectedValue.ToString() == "CO")
                            {
                                frmReportCOPDF repCo = new frmReportCOPDF(reportSelectColl, UserId);
                                repCo.ShowDialog();
                            }


                            //if (MessageBox.Show("ต้องการส่งเมล์แนบจดหมายนี้ไปยังเจ้าหน้าที่ IDBL หรือไม่?", "Send Mail", MessageBoxButtons.YesNo) == DialogResult.Yes)
                            //{
                            //    this.Cursor = Cursors.WaitCursor;
                            //    try
                            //    {
                            //        string checkApp = "";
                            //        foreach (REPORT_SELECT_PRINT rep in reportSelectColl)
                            //        {
                            //            if (checkApp != rep.AppNo && rep.plan != "")
                            //            {
                            //                checkApp = rep.AppNo;

                            //                if ((cmbLetterType.SelectedValue.ToString() == "MO") || cmbLetterType.SelectedValue.ToString() == "MD")
                            //                {
                            //                    frmSendMOLetterByeMail repMo = new frmSendMOLetterByeMail(rep.AppNo, rep.UletterId, UserId);
                            //                    repMo.Show();
                            //                }
                            //                else if (cmbLetterType.SelectedValue.ToString() == "AP")
                            //                {
                            //                    frmSendAPLetterByeMail repAp = new frmSendAPLetterByeMail(rep.AppNo, rep.UletterId, UserId);
                            //                    repAp.Show();
                            //                }
                            //                else if (cmbLetterType.SelectedValue.ToString() == "CO")
                            //                {
                            //                    frmSendCOLetterByeMail repCo = new frmSendCOLetterByeMail(rep.AppNo, rep.UletterId, UserId);
                            //                    repCo.Show();
                            //                }
                            //            }
                            //        }
                            //    }
                            //    finally
                            //    {
                            //        this.Cursor = Cursors.Default;
                            //        MessageBox.Show("ส่ง email ไปยัง IDBL เรียบร้อยแล้ว");
                            //    }
                            //}

                        }
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void dtgCallReport_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || (e.ColumnIndex != dtgCallReport.Columns["clmCountDoc"].Index && (e.ColumnIndex != dtgCallReport.Columns["clmedit"].Index)))
            {
                return;
            }
            else if (e.ColumnIndex == dtgCallReport.Columns["clmCountDoc"].Index)
            {
              showDetailDoc(dtgCallReport["clmSummaryID", e.RowIndex].Value.ToString());
            }
            //else if (e.ColumnIndex == dtgCallReport.Columns["clmedit"].Index)
            //{
            //    try
            //    {
            //        string signature = "";
            //        if(dtgCallReport["clmsignature_id", e.RowIndex].Value == null)
            //        {
            //            signature = "";
            //        }
            //        else
            //        {
            //             signature = dtgCallReport["clmsignature_id", e.RowIndex].Value.ToString();
            //        }
            //        DlgEditLetter dlg = new DlgEditLetter((long)dtgCallReport["clmuletterid", e.RowIndex].Value, signature, _UserId, cmbLetterType.SelectedValue.ToString(), (long)dtgCallReport["clmLetterTypeID", e.RowIndex].Value, Convert.ToDateTime(dtgCallReport["clmlimitdate", e.RowIndex].Value));
            //        dlg.ShowDialog(this);
            //        search();

            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show(ex.Message);
            //    } 
            //}
        }

        private void showDetailDoc(string _summaryID)
        {
            frmReportMODetailDoc detailDoc = new frmReportMODetailDoc(_summaryID);
            detailDoc.ShowDialog();
        }

        private void txtStartDt_Leave(object sender, EventArgs e)
        {
            ChkDateForTextBox(txtStartDt.Text, "Install-date", txtStartDt);
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

        private void txtEndDt_Leave(object sender, EventArgs e)
        {
            ChkDateForTextBox(txtEndDt.Text, "Install-date", txtEndDt);
        }

        private void txthrbegin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < '0' || e.KeyChar > '9') && (e.KeyChar != '\b'))
            {
                e.Handled = true;
                return;
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

        private void txthrend_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < '0' || e.KeyChar > '9') && (e.KeyChar != '\b'))
            {
                e.Handled = true;
                return;
            }
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

        private void txtminend_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < '0' || e.KeyChar > '9') && (e.KeyChar != '\b'))
            {
                e.Handled = true;
                return;
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

        private void cmbchannel_SelectedIndexChanged(object sender, EventArgs e)
        {
            bindingWorkgroup();
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
                    throw ex;
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

        private void frmCallReport_FormClosing(object sender, FormClosingEventArgs e)
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
                    if (filePath.StartsWith("C:\\WINDOWS\\Temp\\NewbizReport\\reportCoFinalPDF") || filePath.StartsWith("C:\\WINDOWS\\Temp\\NewbizReport\\reportMo") || filePath.StartsWith("C:\\WINDOWS\\Temp\\NewbizReport\\reportApFinalPDF"))
                    {
                        File.Delete(filePath);
                    }
                } 
            }
            catch (Exception ex)
            {  }
        }

        private void txtStartDt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                setFormatDate(e, txtStartDt);
                txtEndDt.Focus();
            } 
        }

        private void txtEndDt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                setFormatDate(e, txtEndDt); 
            } 
        }

        private void button1_Click(object sender, EventArgs e)
        {
            NewBISSvcRef.ProcessResult pr = new NewBISSvcRef.ProcessResult();
            NewBISSvcRef.NewBISSvcClient client = new NewBISSvcRef.NewBISSvcClient();
            NewBISSvcRef.RECEIPT_INFORMATION_Collection rc = new NewBISSvcRef.RECEIPT_INFORMATION_Collection();

           rc = client.GetReceiptInfo(out pr, "02367731", "", "OD");
        }
    }
}
