using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using iTextSharp.text;
using iTextSharp.text.pdf; 
using System.IO;
using ITUtility;
using System.Threading;

namespace NewBisPA.ReportForm
{
    public partial class frmReportAPPDF : Form
    {
        ProcessResult prLet = new ProcessResult();
        ProcessResult prDoc = new ProcessResult();
        private string src_work = "";
        private string USERID = "";
        REPORT_SELECT_PRINT_Collection _reportSelectColl=new REPORT_SELECT_PRINT_Collection();
        NewBISSvcRef.U_LETTER_SENDOFC uLetterSendOfc = new NewBISSvcRef.U_LETTER_SENDOFC();
        private string dirPath = @"C:\WINDOWS\Temp\NewbizReport";        
        private string reportApFile = "C:\\WINDOWS\\Temp\\NewbizReport\\reportApPDF.pdf";
        private string reportTempFile = "C:\\WINDOWS\\Temp\\NewbizReport\\reportApTempPDF";
        private string reportFinalFile = "C:\\WINDOWS\\Temp\\NewbizReport\\reportApFinalPDF" + DateTime.Now.ToLongTimeString().ToString().Replace(":", "").Replace(" ", "") + ".pdf";
        private string CHANNEL_TYPE = "";

        public frmReportAPPDF(REPORT_SELECT_PRINT_Collection reportSelectColl, string User_id)
        {
            InitializeComponent();
            _reportSelectColl = reportSelectColl;
            USERID = User_id;
            src_work = "PRINT";
        }

        public frmReportAPPDF(string AppNo,long UletterId ,string UserId)
        {
            InitializeComponent();
            REPORT_SELECT_PRINT reportSelect = new REPORT_SELECT_PRINT();
            reportSelect.AppNo = AppNo;
            reportSelect.UletterId = UletterId;
            reportSelect.UserId = UserId;
            USERID = UserId;
            src_work = "PREVIEW";

            using (NewBISSvcRef.NewBISSvcClient client = new NewBISSvcRef.NewBISSvcClient())
            {
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

                if (client.State != System.ServiceModel.CommunicationState.Closed)
                {
                    client.Close();
                }
            }
            _reportSelectColl.Add(reportSelect); 
        }

        private void frmReportAPPDF_Load(object sender, EventArgs e)
        {
            System.IO.DirectoryInfo directoryPath = new System.IO.DirectoryInfo(dirPath);
            if (!directoryPath.Exists)
            {
                System.IO.Directory.CreateDirectory(dirPath);
            }
            System.IO.FileInfo Ap = new System.IO.FileInfo(reportApFile);
            if (Ap.Exists) Ap.Delete();
            //System.IO.FileInfo Temp = new System.IO.FileInfo(reportTempFile);
            //if (Temp.Exists) Temp.Delete();

            string[] filePaths = Directory.GetFiles(dirPath);
            foreach (string filePath in filePaths)
            {
                if (filePath.StartsWith(reportTempFile))
                {
                    File.Delete(filePath);
                }
            }

            //System.IO.FileInfo Final = new System.IO.FileInfo(reportFinalFile);
            //if (Final.Exists) Final.Delete();
            List<string> pdfArraySendOfc = new List<string>();
            List<string> pdfArray = new List<string>();
            NewBISSvcRef.ProcessResult pr = new NewBISSvcRef.ProcessResult();
            NewBISSvcRef.ProcessResult prlistRd = new NewBISSvcRef.ProcessResult();
            NewBISSvcRef.ProcessResult prlistPM = new NewBISSvcRef.ProcessResult();
          //  NewBISSvcRef.REPORT_AP_DETAIL rDetail = new NewBISSvcRef.REPORT_AP_DETAIL();
            NewBISSvcRef.REPORT_AP_DETAIL_Collection RrDetailColl = new NewBISSvcRef.REPORT_AP_DETAIL_Collection();
            NewBISSvcRef.REPORT_AP_DETAILLIST_Collection rDetailRdColl = new NewBISSvcRef.REPORT_AP_DETAILLIST_Collection();
            NewBISSvcRef.REPORT_AP_DETAILLIST_Collection rDetailSpColl = new NewBISSvcRef.REPORT_AP_DETAILLIST_Collection();
            string newfooter = "";

            using (NewBISSvcRef.NewBISSvcClient client = new NewBISSvcRef.NewBISSvcClient())
            {
               // this.Cursor = Cursors.WaitCursor;

                try
                { 

                foreach (REPORT_SELECT_PRINT _reportSelect in _reportSelectColl)
                {
                   // if (Temp.Exists) Temp.Delete(); 
                    try
                    {
                        
                        RrDetailColl = client.GetReportAPDetail(out pr, _reportSelect.AppNo, _reportSelect.UletterId,_reportSelect.sendofc);
                        rDetailRdColl = client.GetReportAPDetaillist(out prlistRd, _reportSelect.AppNo, "RD");
                        rDetailSpColl = client.GetReportAPDetaillist(out prlistPM, _reportSelect.UletterId.ToString(), "PM");
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    if (pr.Successed == true)
                    {
                        if (RrDetailColl != null)
                        {
                            foreach (NewBISSvcRef.REPORT_AP_DETAIL rDetail in RrDetailColl)
                            {
                              //  if (Temp.Exists) Temp.Delete();
                                pdfArraySendOfc.Clear();
                                Int64 sumPrem = 0;
                                Int64 resultPrem = 0;

                                    string CHANNEL_TYPE = client.GetChannelType(out pr, _reportSelect.AppNo, _reportSelect.UletterId);
                                    string firstParagraph = "";
                                    if (CHANNEL_TYPE == "PO")
                                    {
                                        rDetail.Work_group_name = "ส่วนดำเนินงานกรมธรรม์ใหม่สามัญและช่องทางอื่น";
                                        firstParagraph = "										ตามที่ท่านได้กรุณาให้เกียรติ และมอบความไว้วางใจ โดยส่งใบสมัครทำประกันอุบัติเหตุส่วนบุคคลไปยังบริษัทฯ นั้น บริษัทฯ ได้รับแล้วและขอขอบพระคุณยิ่ง แต่บริษัทฯ ยังไม่สามารถรับประกันท่านได้   ในขณะนี้ เนื่องจากท่านยังมิได้ ";
                                    }
                                    else
                                    {
                                        firstParagraph = "										ตามที่ท่านได้กรุณาให้เกียรติ และมอบความไว้วางใจ โดยส่งใบสมัครทำประกันชีวิตไปยังบริษัทฯ นั้น บริษัทฯ ได้รับแล้วและขอขอบพระคุณยิ่ง จากการพิจารณาใคร่ขอเรียนว่าสุขภาพของท่าน อยู่ในระดับมาตรฐาน แต่บริษัทฯ ยังไม่สามารถรับประกันท่านได้ในขณะนี้ เนื่องจากท่านยังมิได้ "; 
                                    }

                                    REPORT_AP_DETAILBindingSource.DataSource = rDetail;
                                REPORT_AP_DETAILLIST_CollectionBindingSource.DataSource = rDetailRdColl;
                                rEPORTAPDETAILLISTCollectionBindingSource.DataSource = rDetailSpColl;

                                foreach (NewBISSvcRef.REPORT_AP_DETAILLIST _sumPrem in rDetailRdColl)
                                {
                                    sumPrem = sumPrem + Convert.ToInt64(_sumPrem.Premium);
                                }
                                resultPrem = sumPrem;
                                if (rDetailSpColl != null)
                                {
                                    if(rDetail.p_mode == 12)
                                    {
                                        resultPrem = resultPrem * 2;
                                    }

                                    foreach (NewBISSvcRef.REPORT_AP_DETAILLIST _sumSuspense in rDetailSpColl)
                                    {
                                        resultPrem = resultPrem - Convert.ToInt64(_sumSuspense.Premium);
                                    }
                                }

                                #region จัด footer ตาม group policy
                                
                                string bbl = "";
                                if (_reportSelect.channelType == "GM")
                                {
                                    if (rDetail.POLICY_HOLDING != null)
                                    {
                                        if (Convert.ToInt64(rDetail.POLICY_HOLDING.Trim()) == 8000029)
                                        {
                                            bbl = "บริษัท กรุงเทพประกันชีวิต จำกัด (มหาชน)";
                                        }
                                    }
                                }

                                #endregion

                                #region เอา footer มาจัด format การวาง
                                
                                if (bbl != "")
                                {
                                    newfooter = bbl;
                                }
                                else if (rDetail.Workgroup == "BNC")
                                {
                                    if (rDetail.Agentuplname.StartsWith("ธนาคาร")) //แบบ 2 บรรทัด(แบบ bank)
                                    {
                                        newfooter = "บมจ." + rDetail.Agentuplname + "\n" + rDetail.Agentname + rDetail.REGION;
                                    }
                                    else
                                    {
                                        newfooter = rDetail.Agentname + " / " + rDetail.Agentuplname + "    (" + rDetail.Agentcode + " / " + rDetail.Agentuplcode + ")\n" + rDetail.Sale_ofc;
                                    }
                                }
                                else //ถ้าไม่ใช่พวก bank
                                {
                                    newfooter = rDetail.Agentname + " / " + rDetail.Agentuplname + "    (" + rDetail.Agentcode + " / " + rDetail.Agentuplcode + ")\n" + rDetail.Sale_ofc;
                                }

                                #endregion

                              

                                reportViewer1.LocalReport.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("region", rDetail.REGION));
                                reportViewer1.LocalReport.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("lastStatus", rDetail.last_status));
                                reportViewer1.LocalReport.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("HeaderExpire", rDetail.header_expire));
                                reportViewer1.LocalReport.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("sumPrem", sumPrem.ToString()));
                                reportViewer1.LocalReport.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("resultPrem", resultPrem.ToString()));
                                reportViewer1.LocalReport.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("answer_limit_dt", Utility.dateTimeToString(Convert.ToDateTime(rDetail.Answer_limit_dt), "d Month yyyy", "BU")));
                                reportViewer1.LocalReport.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("letter_dt", _reportSelect.printDate));//Utility.dateTimeToString(DateTime.Now, "วันที่ d Month yyyy", "BU")));  //apiwat เปลี่ยน letter dt จาก sysdate เป็น sysdate ครั้งแรกที่พิม
                                reportViewer1.LocalReport.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("Workgroup",rDetail.Workgroup));
                                reportViewer1.LocalReport.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("newfooter", newfooter));
                                reportViewer1.LocalReport.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("CHANNEL_TYPE", CHANNEL_TYPE));
                                reportViewer1.LocalReport.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("firstParagraph", firstParagraph));
                                    reportViewer1.RefreshReport();

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
                                    tempWithTime = reportTempFile + DateTime.Now.ToLongTimeString().ToString().Replace(":", "").Replace(" ", "") + ".pdf";
                                    pdfArray.Clear();
                                    fi.CopyTo(tempWithTime);
                                    genReportApPDF(reportApFile);
                                    pdfArray.Add(tempWithTime);
                                    pdfArray.Add(reportApFile);
                                    MergeFiles(reportFinalFile, pdfArray.ToArray());
                                    pdfArraySendOfc.Add(reportApFile); //FILE SEND OFC
                                }
                                else
                                {
                                    genReportApPDF(reportFinalFile);
                                    pdfArraySendOfc.Add(reportFinalFile);  //FILE SEND OFC
                                }

                               // SEND OFC
                                if (_reportSelect.office != null && _reportSelect.office != "" && _reportSelect.office != "สสง" && _reportSelect.office != "สนญ" && src_work == "PRINT" && _reportSelect.sendofc == "Y")
                                {
                                    uploadFileToOfc(client, _reportSelect.AppNo, _reportSelect.office, rDetail.last_status, _reportSelect.UletterId, pdfArraySendOfc.ToArray(),rDetail.CUSTNAME_ONLY);
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
                lfFileControl.DESCRIPTION = status + " ใบคำขอ : " + appno + " (" + custName + ")";
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

        private void genReportApPDF(string _fileName)
        {
            Warning[] warnings;
            string[] streamids;
            string mimeType, encoding, filenameExtension;
            byte[] bytes = reportViewer1.LocalReport.Render("PDF", null, out mimeType, out encoding, out filenameExtension, out streamids, out warnings);
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
