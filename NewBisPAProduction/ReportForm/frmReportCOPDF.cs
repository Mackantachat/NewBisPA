using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using Microsoft.Reporting.WinForms;
using ITUtility;
using System.Threading;

namespace NewBisPA.ReportForm
{
    public partial class frmReportCOPDF : Form
    {
        ProcessResult prLet = new ProcessResult();
        ProcessResult prDoc = new ProcessResult();
        private string src_work = "";
        NewBISSvcRef.ZTB_SIGNATURE[] ztbSignatures;
        NewBISSvcRef.ZTB_SIGNATURE ztbSignature = new NewBISSvcRef.ZTB_SIGNATURE();
        REPORT_SELECT_PRINT_Collection _reportSelectColl = new REPORT_SELECT_PRINT_Collection();
        private string USERID = "";
        NewBISSvcRef.U_LETTER_SENDOFC uLetterSendOfc = new NewBISSvcRef.U_LETTER_SENDOFC();
        private long? sumRider = null;
        private string dirPath = @"C:\WINDOWS\Temp\NewbizReport";
        private string reportCoFile = "C:\\WINDOWS\\Temp\\NewbizReport\\reportCoPDF.pdf";
        private string reportTempFile = "C:\\WINDOWS\\Temp\\NewbizReport\\reportCoTempPDF";
        private string reportFinalFile = "C:\\WINDOWS\\Temp\\NewbizReport\\reportCoFinalPDF" + DateTime.Now.ToLongTimeString().ToString().Replace(":", "").Replace(" ", "") + ".pdf";
        private string CHANNEL_TYPE = "";

        public frmReportCOPDF(REPORT_SELECT_PRINT_Collection reportSelectColl, string User_id)
        {
            InitializeComponent();
            _reportSelectColl = reportSelectColl;
            USERID = User_id;
            src_work = "PRINT";

            #region choice flag
            using (NewBISSvcRef.NewBISSvcClient client = new NewBISSvcRef.NewBISSvcClient())
            {
                string choice_flg = "";
                foreach (REPORT_SELECT_PRINT rep in _reportSelectColl)
                {
                    NewBISSvcRef.ProcessResult prChoice = new NewBISSvcRef.ProcessResult();
                    choice_flg = client.GetCOChoiceFlag(out prChoice, rep.UletterId);
                    if (choice_flg == "C")
                    {
                        rep.choiceFlg = "C";
                    }
                    else
                    {
                        rep.choiceFlg = "";
                    }
                }
                if (client.State != System.ServiceModel.CommunicationState.Closed)
                {
                    client.Close();
                }
            }
            #endregion
        }

        public frmReportCOPDF(string AppNo, long UletterId, string UserId)
        {
            InitializeComponent();
            REPORT_SELECT_PRINT reportSelect = new REPORT_SELECT_PRINT();
            reportSelect.AppNo = AppNo;
            reportSelect.UletterId = UletterId;
            reportSelect.UserId = UserId;
            USERID = UserId;
            src_work = "PREVIEW"; //เปิดหน้าพิจารณา
            using (NewBISSvcRef.NewBISSvcClient client = new NewBISSvcRef.NewBISSvcClient())
            {
                //letter date
                NewBISSvcRef.ProcessResult prLt = new NewBISSvcRef.ProcessResult();
                string _letterdt = client.GetLetterDt(out prLt, (long?)UletterId);
                if (_letterdt != "-")
                {
                    reportSelect.printDate = _letterdt;
                }
                else
                {
                    reportSelect.printDate = Utility.dateTimeToString(DateTime.Now, "d Month yyyy", "BU");
                }

                NewBISSvcRef.ProcessResult prCh = new NewBISSvcRef.ProcessResult();
                string channel_type = client.GetChannelType(out prCh, AppNo, UletterId);
                reportSelect.channelType = channel_type;

                #region choice flag
                NewBISSvcRef.ProcessResult prChoice = new NewBISSvcRef.ProcessResult();
                string choice_flg = client.GetCOChoiceFlag(out prChoice, UletterId);
                if (choice_flg == "C")
                {
                    reportSelect.choiceFlg = "C";
                }
                else
                {
                    reportSelect.choiceFlg = "";
                }
                #endregion

                if (client.State != System.ServiceModel.CommunicationState.Closed)
                {
                    client.Close();
                }
            }
            _reportSelectColl.Add(reportSelect); 
        }

        private void frmReportCOPDF_Load(object sender, EventArgs e)
        {
            System.IO.DirectoryInfo directoryPath = new System.IO.DirectoryInfo(dirPath);
            if (!directoryPath.Exists)
            {
                System.IO.Directory.CreateDirectory(dirPath);
            }
            System.IO.FileInfo Co = new System.IO.FileInfo(reportCoFile);
            if (Co.Exists) Co.Delete(); 

            string[] filePaths = Directory.GetFiles(dirPath);
            foreach (string filePath in filePaths)
            {
                if (filePath.StartsWith(reportTempFile))
                {
                    File.Delete(filePath);
                }
            }
            List<string> pdfArraySendOfc = new List<string>();
            List<string> pdfArray = new List<string>();
            NewBISSvcRef.ProcessResult pr = new NewBISSvcRef.ProcessResult();
            NewBISSvcRef.ProcessResult prPlRd = new NewBISSvcRef.ProcessResult();
            NewBISSvcRef.ProcessResult prExclude = new NewBISSvcRef.ProcessResult();
             
            NewBISSvcRef.REPORT_CO_DETAIL_Collection rDetailColl = new NewBISSvcRef.REPORT_CO_DETAIL_Collection();
            NewBISSvcRef.REPORT_CO_PLRD_Collection rPlRdColl = new NewBISSvcRef.REPORT_CO_PLRD_Collection();
            NewBISSvcRef.REPORT_CO_EXCLUDE_Collection rExColl = new NewBISSvcRef.REPORT_CO_EXCLUDE_Collection();
           
            using (NewBISSvcRef.NewBISSvcClient client = new NewBISSvcRef.NewBISSvcClient())
            { 
                try
                { 
                    string GMfooter = "";
                    string bbl = "";
                    foreach (REPORT_SELECT_PRINT _reportSelect in _reportSelectColl)
                    {
                        if (_reportSelect.channelType == "OD")
                        {
                            rDetailColl = client.GetReportCODetail(out pr, _reportSelect.AppNo, _reportSelect.UletterId,_reportSelect.sendofc);
                        }
                        else 
                        {
                            rDetailColl = client.GetReportCOGMDetail(out pr, _reportSelect.AppNo, _reportSelect.UletterId,_reportSelect.sendofc);
                        }

                        if (pr.Successed == true)
                        {
                            if (rDetailColl != null)
                            {
                                foreach (NewBISSvcRef.REPORT_CO_DETAIL rDetail in rDetailColl)
                                {
                                    if (_reportSelect.channelType == "PO")
                                    {
                                        rDetail.Position = "ส่วนดำเนินงานกรมธรรม์ใหม่สามัญและช่องทางอื่น";
                                    }

                                    pdfArraySendOfc.Clear();
                                  //  rDetail.POLICY_HOLDING = "8000028";
                                    string[] signature_id = { rDetail.Signature_id,rDetail.Userid };
                                    client.GetZTB_SIGNATURE(out ztbSignatures, signature_id);
                                    if (ztbSignatures != null)
                                    {
                                        ztbSignature = ztbSignatures[0];
                                        rDetail.SIGNATURE_IMG = ztbSignature.SIGNATURE;
                                    }

                                    REPORT_CO_DETAILBindingSource.DataSource = rDetail;
                                    try
                                    {
                                        rPlRdColl = client.GetReportCODetailPLRD(out prPlRd, Convert.ToInt32(rDetail.Uapp_id));
                                    }
                                    catch (Exception ex)
                                    {
                                        throw ex;
                                    }

                                    NewBISSvcRef.REPORT_CO_PLRD_Collection rPlRdCollFilter = new NewBISSvcRef.REPORT_CO_PLRD_Collection();
                                    Int64 sumPremium = 0;

                                    if (prPlRd.Successed == true)
                                    {
                                        if (rPlRdColl != null)
                                        {
                                            sumRider = 0;
                                            foreach (NewBISSvcRef.REPORT_CO_PLRD x in rPlRdColl)
                                            {
                                                if (x.Premium > 0 || (x.pl_block == "R" && (x.pl_code == "021" || x.pl_code == "032" || x.pl_code == "041" || x.pl_code == "035" || x.pl_code == "036" || x.pl_code == "042")))
                                                {
                                                    rPlRdCollFilter.Add(x);
                                                    if (x.pl_block == null || x.pl_block != "H")
                                                    {
                                                        sumRider += x.Premium;
                                                    }
                                                }
                                            }

                                            sumPremium = 0;
                                            Int64 premTotal = 0;
                                            REPORT_CO_PLRD_CollectionBindingSource.DataSource = rPlRdCollFilter;// rPlRdColl;
                                            foreach (NewBISSvcRef.REPORT_CO_PLRD plrd in rPlRdColl)
                                            {
                                                if (plrd.Premium != null)
                                                {
                                                    sumPremium = sumPremium + Convert.ToInt64(plrd.Premium);
                                                }

                                                if (_reportSelect.channelType == "GM")
                                                {
                                                    plrd.Title = plrd.Title.Replace("/ปี", " บาท");
                                                    plrd.Title = plrd.Title.Replace("ชั่วคราว", "");
                                                }
                                            }

                                            if (rDetail.P_MODE == "12")
                                            {
                                                premTotal = (sumPremium * 2) - Convert.ToInt64(rDetail.Suspense);
                                            }
                                            else
                                            {
                                                premTotal = sumPremium - Convert.ToInt64(rDetail.Suspense);
                                            }
                                            if (premTotal < 0)
                                            { premTotal = 0; }

                                             
                                            #region จัด footer ตาม group policy
                                            GMfooter = "";
                                            bbl = "";
                                            if (_reportSelect.channelType == "GM")
                                            {
                                                GMfooter = "-กรณีมีชำระเงินเพิ่มเติม กรุณาชำระผ่านระบบ Bill Payment (Service Code:BLAHOME)";
                                                if (rDetail.POLICY_HOLDING != null)
                                                {
                                                    if (Convert.ToInt64(rDetail.POLICY_HOLDING.Trim()) >= 8000026 && Convert.ToInt64(rDetail.POLICY_HOLDING.Trim()) <= 8000062)
                                                    {
                                                        GMfooter = "";
                                                    }

                                                    if (Convert.ToInt64(rDetail.POLICY_HOLDING.Trim()) == 8000029)
                                                    {
                                                        bbl = "บริษัท กรุงเทพประกันชีวิต จำกัด (มหาชน)";
                                                    }
                                                } 
                                              }
                                            else
                                            {
                                                GMfooter = "-กรณีมีชำระเงินเพิ่มเติม กรุณาชำระผ่านระบบ Bill Payment (Service Code:BLAFIRST)";
                                            }
                                            #endregion

                                            #region เอา footer มาจัด format การวาง

                                            string newfooter = "";

                                            if(rDetail.Workgroup == "BNC" && GMfooter != "")
                                            {
                                              if(rDetail.Agentuplname.StartsWith("ธนาคาร")) //แบบ 2 บรรทัด(แบบ bank)
                                              {
                                                newfooter = "บมจ." + rDetail.Agentuplname + "\n" + rDetail.Agentname + rDetail.REGION;
                                              }
                                              else
                                              {
                                                newfooter = rDetail.Agentname + " / " + rDetail.Agentuplname + "    (" + rDetail.Agentcode + " / " + rDetail.Agentuplcode + ")\n" + rDetail.Sale_ofc;
                                              }
                                            }
                                            else if(bbl == "")
                                            {
                                                newfooter = rDetail.Agentname + " / " + rDetail.Agentuplname + "    (" + rDetail.Agentcode + " / " + rDetail.Agentuplcode + ")\n" + rDetail.Sale_ofc;
                                            }
                                            else if(bbl != "")
                                            {
                                                  newfooter = bbl;
                                            }

                                            #endregion


                                            string CHANNEL_TYPE = client.GetChannelType(out pr, _reportSelect.AppNo, _reportSelect.UletterId);


                                            if (_reportSelect.channelType == "GM" && _reportSelect.choiceFlg == "C")
                                            {
                                                reportViewerGM.LocalReport.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("gmfooter", GMfooter));
                                                reportViewerGM.LocalReport.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("region", rDetail.REGION));
                                                reportViewerGM.LocalReport.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("lastStatus", rDetail.last_status));
                                                reportViewerGM.LocalReport.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("HeaderExpire", rDetail.header_expire));
                                                reportViewerGM.LocalReport.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("Paymode", rDetail.Paymode));
                                                reportViewerGM.LocalReport.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("premTotal", premTotal.ToString()));
                                                reportViewerGM.LocalReport.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("sumPremium", sumPremium.ToString()));
                                                reportViewerGM.LocalReport.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("answer_limit_dt", Utility.dateTimeToString(Convert.ToDateTime(rDetail.Answer_limit_dt), "d Month yyyy", "BU")));
                                                reportViewerGM.LocalReport.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("letter_dt", _reportSelect.printDate));//Utility.dateTimeToString(DateTime.Now, "วันที่ d Month yyyy", "BU")));  //apiwat เปลี่ยน letter dt จาก sysdate เป็น sysdate ครั้งแรกที่พิม
                                                reportViewerGM.LocalReport.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("Workgroup", rDetail.Workgroup));
                                                reportViewerGM.LocalReport.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("Channeltype", _reportSelect.channelType));
                                                reportViewerGM.LocalReport.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("bbl", bbl));
                                                reportViewerGM.LocalReport.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("newfooter", newfooter));
                                                reportViewerGM.LocalReport.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("CHANNEL_TYPE", CHANNEL_TYPE));
                                            }
                                            else
                                            {

                                                reportViewer1.LocalReport.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("gmfooter", GMfooter));
                                                reportViewer1.LocalReport.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("region", rDetail.REGION));
                                                reportViewer1.LocalReport.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("lastStatus", rDetail.last_status));
                                                reportViewer1.LocalReport.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("HeaderExpire", rDetail.header_expire));
                                                reportViewer1.LocalReport.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("Paymode", rDetail.Paymode));
                                                reportViewer1.LocalReport.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("premTotal", premTotal.ToString()));
                                                reportViewer1.LocalReport.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("sumPremium", sumPremium.ToString()));
                                                reportViewer1.LocalReport.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("answer_limit_dt", Utility.dateTimeToString(Convert.ToDateTime(rDetail.Answer_limit_dt), "d Month yyyy", "BU")));
                                                reportViewer1.LocalReport.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("letter_dt", _reportSelect.printDate));//Utility.dateTimeToString(DateTime.Now, "วันที่ d Month yyyy", "BU")));  //apiwat เปลี่ยน letter dt จาก sysdate เป็น sysdate ครั้งแรกที่พิม
                                                reportViewer1.LocalReport.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("Workgroup", rDetail.Workgroup));
                                                reportViewer1.LocalReport.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("Channeltype", _reportSelect.channelType));
                                                reportViewer1.LocalReport.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("bbl", bbl));
                                                reportViewer1.LocalReport.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("newfooter", newfooter));
                                                reportViewerGM.LocalReport.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("CHANNEL_TYPE", CHANNEL_TYPE));
                                            }

                                        }
                                    }

                                    try
                                    {
                                       

                                        rExColl = client.GetReportCODetailExclude(out prExclude, Convert.ToInt32(rDetail.Uexclude_id));

                                       // for GM ของใหม่(ของใหม่คือ choice flg เป็น C)
                                        if (_reportSelect.channelType == "GM" && _reportSelect.choiceFlg == "C")
                                        {
                                            if ((sumPremium - rDetail.Suspense) >= 0)
                                            {
                                                string[] excludeLine = new string[3];
                                                string excludeLine1 = "";
                                                string excludeLine2 = "";
                                                string excludeLine3 = "";

                                                int i = 0;
                                                foreach (NewBISSvcRef.REPORT_CO_EXCLUDE ex in rExColl)
                                                {
                                                    excludeLine[i] = ex.Exclude;
                                                    i = i + 1;
                                                }
                                                if (excludeLine[0] != null)
                                                {
                                                    excludeLine1 = "      1.  ชำระเบี้ยประกันเพิ่มจำนวน " + ((long)(sumPremium - rDetail.Suspense)).ToString("#,##0") + " บาท โดยมีรายละเอียดดังนี้";//" + excludeLine[0];
                                                    if (sumPremium - rDetail.Suspense != 0)
                                                    {
                                                        excludeLine2 = "      " + excludeLine[0];//excludeLine[1];
                                                        excludeLine3 = "      " + excludeLine[1];//excludeLine[2];
                                                    }
                                                    else
                                                    {
                                                        excludeLine1 = "      ชำระเบี้ยประกันเพิ่มจำนวน " + ((long)(sumPremium - rDetail.Suspense)).ToString("#,##0") + " บาท โดยมีรายละเอียดดังนี้";//" + excludeLine[0];
                                                    }

                                                    string strPremLebel = "เบี้ยประกันภัยรวม";
                                                    string strPrem = sumPremium.ToString("#,##0"); //rDetail.Suspense==null?"0":((long)rDetail.Suspense).ToString("#,##0");
                                                    strPremLebel += "\nเบี้ยประกันที่ได้ชำระแล้ว";
                                                    strPrem += rDetail.Suspense == null ? "\n0" : "\n" + ((long)rDetail.Suspense).ToString("#,##0");
                                                    strPremLebel += "\nเบี้ยประกันที่ต้องชำระเพิ่ม";
                                                    string strLackPrem = ((long)(sumPremium - rDetail.Suspense)).ToString("#,##0");
                                                    string page2 = "     ข้าพเจ้ายินดีรับข้อเสนอข้อที่....... และขอยืนยันว่าข้อความ        ข้าพเจ้าขอปฏิเสธข้อเสนอตามเงื่อนไขข้างต้น";
                                                    reportViewerGM.LocalReport.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("excludeLine1", excludeLine1));
                                                    reportViewerGM.LocalReport.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("excludeLine2", excludeLine2));
                                                    reportViewerGM.LocalReport.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("excludeLine3", excludeLine3));
                                                    reportViewerGM.LocalReport.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("strPremLebel", strPremLebel));
                                                    reportViewerGM.LocalReport.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("strPrem", strPrem));
                                                    reportViewerGM.LocalReport.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("strLackPrem", strLackPrem));
                                                    reportViewerGM.LocalReport.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("page2", page2));
                                                }
                                            }
                                            else 
                                            {
                                                string[] excludeLine = new string[3];
                                                string excludeLine1 = "";
                                                string excludeLine2 = "";
                                                string excludeLine3 = "";

                                                int i = 0;
                                                foreach (NewBISSvcRef.REPORT_CO_EXCLUDE ex in rExColl)
                                                {
                                                    excludeLine[i] = ex.Exclude;
                                                    i = i + 1;
                                                }
                                                if (excludeLine[0] != null)
                                                {
                                                    excludeLine1 = "      ชำระเบี้ยประกันเพิ่มจำนวน " + ((long)sumRider).ToString("#,##0") + " บาท โดยมีรายละเอียดดังนี้";//" + excludeLine[0];
                                                    excludeLine2 = "";
                                                    excludeLine3 = "";

                                                    string strPremLebel = "เบี้ยประกันภัยรวม";
                                                    string strPrem = sumPremium.ToString("#,##0"); 
                                                    strPremLebel += "\nเบี้ยประกันที่ได้ชำระแล้ว";
                                                    strPrem += rDetail.Suspense == null ? "\n0" : "\n" + ((long)rDetail.Suspense).ToString("#,##0");
                                                    strPremLebel += "\nเบี้ยประกันที่ชำระเกิน";
                                                    string strLackPrem = ((long)(sumPremium - rDetail.Suspense)).ToString("#,##0").Replace("-","");
                                                    string page2 = "                ข้าพเจ้ายินดีรับข้อเสนอและขอยืนยันว่าข้อความ             ข้าพเจ้าขอปฏิเสธข้อเสนอตามเงื่อนไขข้างต้น";
                                                    reportViewerGM.LocalReport.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("excludeLine1", excludeLine1));
                                                    reportViewerGM.LocalReport.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("excludeLine2", excludeLine2));
                                                    reportViewerGM.LocalReport.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("excludeLine3", excludeLine3));
                                                    reportViewerGM.LocalReport.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("strPremLebel", strPremLebel));
                                                    reportViewerGM.LocalReport.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("strPrem", strPrem));
                                                    reportViewerGM.LocalReport.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("strLackPrem", strLackPrem));
                                                    reportViewerGM.LocalReport.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("page2", page2));
                                                }
                                            }
                                        }

                                    }
                                    catch (Exception ex)
                                    {
                                        throw ex;
                                    }
                                    if (prExclude.Successed == true)
                                    {
                                        REPORT_CO_EXCLUDE_CollectionBindingSource.DataSource = rExColl;

                                        if (rExColl != null)
                                        {
                                            bool chkExc = false;
                                            foreach (NewBISSvcRef.REPORT_CO_EXCLUDE rce in rExColl)
                                            {
                                                if (rce.Exclude != null)
                                                {
                                                    chkExc = true;
                                                }
                                            }

                                            if (_reportSelect.channelType == "GM" && _reportSelect.choiceFlg == "C")
                                            {
                                                if (chkExc == true)
                                                {
                                                    reportViewerGM.LocalReport.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("showExclude", "โดยบริษัทฯ"));
                                                }
                                                else
                                                {
                                                    reportViewerGM.LocalReport.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("showExclude", ""));
                                                }
                                            }
                                            else //(_reportSelect.channelType != "GM")
                                            {
                                                if (chkExc == true)
                                                {
                                                    reportViewer1.LocalReport.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("showExclude", "โดยบริษัทฯ"));
                                                }
                                                else
                                                {
                                                    reportViewer1.LocalReport.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("showExclude", ""));
                                                }
                                            }
                                            //else
                                            //{
                                            //    if (chkExc == true)
                                            //    {
                                            //        reportViewerGM.LocalReport.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("showExclude", "โดยบริษัทฯ"));
                                            //    }
                                            //    else
                                            //    {
                                            //        reportViewerGM.LocalReport.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("showExclude", ""));
                                            //    }
                                            //}

                                        }
                                    }

                                    if (_reportSelect.channelType == "GM" && _reportSelect.choiceFlg == "C")
                                    {
                                        reportViewerGM.RefreshReport(); 
                                    }
                                    else
                                    {
                                        reportViewer1.RefreshReport();
                                    }

                                    System.IO.FileInfo fi = new System.IO.FileInfo(reportFinalFile);

                                    filePaths = Directory.GetFiles(dirPath);
                                    foreach (string filePath in filePaths)
                                    {
                                        if (filePath.StartsWith(reportTempFile))
                                        {
                                            System.GC.Collect();
                                            System.GC.WaitForPendingFinalizers();
                                            File.Delete(filePath);
                                        }
                                    }

                                    string tempWithTime = "";
                                    if (fi.Exists)
                                    {
                                        tempWithTime = reportTempFile + DateTime.Now.ToLongTimeString().ToString().Replace(":", "").Replace(" ","") + ".pdf";
                                        pdfArray.Clear();
                                        fi.CopyTo(tempWithTime);
                                        genReportCoPDF(reportCoFile, _reportSelect.channelType,_reportSelect.choiceFlg);
                                        pdfArray.Add(tempWithTime);
                                        pdfArray.Add(reportCoFile);
                                        MergeFiles(reportFinalFile, pdfArray.ToArray());
                                        pdfArraySendOfc.Add(reportCoFile);
                                    }
                                    else
                                    {
                                        genReportCoPDF(reportFinalFile, _reportSelect.channelType, _reportSelect.choiceFlg);
                                        pdfArraySendOfc.Add(reportFinalFile);
                                    }

                                    //SEND OFC
                                    if (_reportSelect.office != null && _reportSelect.office != "" && _reportSelect.office != "สสง" && _reportSelect.office != "สนญ" && src_work == "PRINT" && _reportSelect.sendofc == "Y")
                                    {
                                        uploadFileToOfc(client, _reportSelect.AppNo, _reportSelect.office, rDetail.last_status, _reportSelect.UletterId, pdfArraySendOfc.ToArray(), rDetail.CUSTNAME_ONLY);
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                   throw ex; 
                }
                //finally
                //{
                //    this.Cursor = Cursors.Default;
                //}

                if (client.State != System.ServiceModel.CommunicationState.Closed)
                {
                    client.Close();
                }
            }

            try
            {
                System.Diagnostics.Process.Start(reportFinalFile);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ไม่พบจดหมายนี้ในระบบ : " + ex.Message);
            }
        }

        private void uploadFileToOfc(NewBISSvcRef.NewBISSvcClient cn, string appno, string office, string status, long uLetterId, string[] pdfArr, string custName)
        {
            using (LetterSvcRef.LetterServiceClient lt = new LetterSvcRef.LetterServiceClient())
            {
                #region upload document file

                string filename4upload = appno + "_" + status + "_" + DateTime.Now.ToLongTimeString().ToString().Replace(":", "").Replace(" ", "") + ".PDF";
                string filename4uploadPath = "C:\\WINDOWS\\Temp\\NewbizReport\\" + filename4upload;

                System.IO.FileInfo fup = new System.IO.FileInfo(filename4uploadPath);

                MergeFiles(filename4uploadPath, pdfArr);

                pdfArr = null;
                byte[] f = System.IO.File.ReadAllBytes(filename4uploadPath);
                int? orgid = getOrgId(USERID);
                string filePath = @"\\blanas\letter_service\25\" + office + "\\" + orgid.ToString();

                #endregion

                #region upload file

                LetterSvcRef.LS_FILE_CONTROL lfFileControl = new LetterSvcRef.LS_FILE_CONTROL();
                lfFileControl.APPLICATION_ID = 25; //ระบบพิจารณารับประกัน
                lfFileControl.OFFICE = office;
                lfFileControl.FILE_NAME = filename4upload;
                lfFileControl.CREATE_BY = USERID;
                lfFileControl.ORG_ID = getOrgId(USERID);
                lfFileControl.CONTENT_TYPE = "application/pdf";
                lfFileControl.CREATE_DATE = DateTime.Now;
                lfFileControl.DESCRIPTION = status + " ใบคำขอ : " + appno  +" (" + custName + ")";
                lfFileControl.UPDATE_BY = USERID;

                LetterSvcRef.DocumentFile docFile = new LetterSvcRef.DocumentFile();
                docFile.FileName = filename4upload;
                docFile.ModifyDate = DateTime.Now;
                docFile.FileData = f;//System.IO.File.ReadAllBytes(reportISOFile);

                prLet = lt.UploadFile(lfFileControl, docFile,"Y");
                if (prLet.Successed == false)
                {
                    throw new Exception(prLet.ErrorMessage);
                }
                //upload เสร็จ ลบทิ้ง
                if (fup.Exists) { fup.Delete(); }

                #endregion

                #region write u letter sendofc

                uLetterSendOfc.ULETTER_ID = uLetterId;
                uLetterSendOfc.SEND_BY = USERID;
                uLetterSendOfc.SEND_DT = DateTime.Now;
                uLetterSendOfc.SEND_OFC = office;
                cn.EditU_LETTER_SENDOFC(ref uLetterSendOfc);

                #endregion

                if (lt.State != System.ServiceModel.CommunicationState.Closed)
                {
                    lt.Close();
                }
            }
        }

        private int? getOrgId(string userid)
        {
            CenterSvcRef.ProcessResult pr = new CenterSvcRef.ProcessResult();
            CenterSvcRef.User user = new CenterSvcRef.User();
            using (CenterSvcRef.CenterServiceClient cn = new CenterSvcRef.CenterServiceClient())
            {
                user = cn.getUser(out pr, userid);

                if (cn.State != System.ServiceModel.CommunicationState.Closed)
                {
                    cn.Close();
                }
            }

            return user.OrgID;
        }

        private void genReportCoPDF(string _fileName,string channelType,string choiceFlg)
        {
            Warning[] warnings;
            string[] streamids;
            string mimeType, encoding, filenameExtension;
            byte[] bytes = null;

            if (channelType == "GM" && choiceFlg == "C")
            {
                bytes = reportViewerGM.LocalReport.Render("PDF", null, out mimeType, out encoding, out filenameExtension, out streamids, out warnings);
                
            }
            else
            {
                bytes = reportViewer1.LocalReport.Render("PDF", null, out mimeType, out encoding, out filenameExtension, out streamids, out warnings);
            }

            System.IO.FileStream fs = System.IO.File.Create(_fileName);
            fs.Write(bytes, 0, bytes.Length);
            fs.Close();
        }

        private void MergeFiles(string destinationFile, string[] sourceFiles)
        {
            try
            {
                int f = 0;
                // we create a reader for a certain document			
                PdfReader reader = new PdfReader(sourceFiles[f]);
                // we retrieve the total number of pages			
                int n = reader.NumberOfPages;
                //Console.WriteLine("There are " + n + " pages in the original file.");			
                // step 1: creation of a document-object			
                Document document = new Document(reader.GetPageSizeWithRotation(1));
                // step 2: we create a writer that listens to the document			
                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(destinationFile, FileMode.Create));
                // step 3: we open the document			
                document.Open();
                PdfContentByte cb = writer.DirectContent;
                PdfImportedPage page;
                int rotation;
                // step 4: we add content			
                while (f < sourceFiles.Length)
                {
                    int i = 0;
                    while (i < n)
                    {
                        i++;
                        document.SetPageSize(reader.GetPageSizeWithRotation(i));
                        document.NewPage();
                        page = writer.GetImportedPage(reader, i);
                        rotation = reader.GetPageRotation(i);
                        if (rotation == 90 || rotation == 270)
                        {
                            cb.AddTemplate(page, 0, -1f, 1f, 0, 0, reader.GetPageSizeWithRotation(i).Height);
                        }
                        else
                        {
                            cb.AddTemplate(page, 1f, 0, 0, 1f, 0, 0);
                        }
                        //Console.WriteLine("Processed page " + i);				
                    }

                    f++;
                    if (f < sourceFiles.Length)
                    {
                        reader = new PdfReader(sourceFiles[f]);
                        // we retrieve the total number of pages					
                        n = reader.NumberOfPages;
                        //Console.WriteLine("There are " + n + " pages in the original file.");				
                    }
                }
                // step 5: we close the document			
                document.Close();
            }
            catch (Exception e)
            {
                string strOb = e.Message;
            }
        } 
    }
}
