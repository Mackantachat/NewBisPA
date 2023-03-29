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
    public partial class frmReportMOPDF : Form 
    {    
        ProcessResult prLet = new ProcessResult();
        ProcessResult prDoc = new ProcessResult();
        string src_work = "";
        private string dirPath = @"C:\WINDOWS\Temp\NewbizReport";
        private string USERID = "";
        REPORT_SELECT_PRINT_Collection _reportSelectColl = new REPORT_SELECT_PRINT_Collection();
        NewBISSvcRef.U_LETTER_SENDOFC uLetterSendOfc = new NewBISSvcRef.U_LETTER_SENDOFC();
        private string CHANNEL_TYPE = "";
        private string reportMoFile = "C:\\WINDOWS\\Temp\\NewbizReport\\reportMoPDF.pdf";
        private string reportISOFile = "C:\\WINDOWS\\Temp\\NewbizReport\\reportMoISOPDF.pdf";
        private string reportTempFile = "C:\\WINDOWS\\Temp\\NewbizReport\\reportMoTempPDF";
        private string reportFinalFile = "C:\\WINDOWS\\Temp\\NewbizReport\\reportMoFinalPDF" + DateTime.Now.ToLongTimeString().ToString().Replace(":", "").Replace(" ", "") + ".pdf";
        //private string fileISOIntranet = @"\\172.16.10.116\Life insurance\FORM\";  
        private string fileISOIntranet =@"\\172.16.10.116\Life insurance\printform\";
         
        public frmReportMOPDF(REPORT_SELECT_PRINT_Collection reportSelectColl, string User_id)
        {
            InitializeComponent();
            _reportSelectColl = reportSelectColl;
            USERID = User_id;
            src_work = "PRINT";
        }

        public frmReportMOPDF(string AppNo, long UletterId,string UserId)
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

        private void frmReportMOPDF_Load(object sender, EventArgs e)
        {
            System.IO.DirectoryInfo directoryPath = new System.IO.DirectoryInfo(dirPath);
            if (!directoryPath.Exists)
            {
                System.IO.Directory.CreateDirectory(dirPath);
            }
            System.IO.FileInfo Mo = new System.IO.FileInfo(reportMoFile);
            if (Mo.Exists) Mo.Delete();
            System.IO.FileInfo ISO = new System.IO.FileInfo(reportISOFile);
            if (ISO.Exists) ISO.Delete();
            //System.IO.FileInfo Temp = new System.IO.FileInfo(reportTempFile);
            //if (Temp.Exists) Temp.Delete();

            System.GC.Collect();
            System.GC.WaitForPendingFinalizers();

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

            List<string> pdfArray = new List<string>();
            List<string> pdfArraySendOfc = new List<string>();
            NewBISSvcRef.ProcessResult pr = new NewBISSvcRef.ProcessResult();
            NewBISSvcRef.ProcessResult prlist = new NewBISSvcRef.ProcessResult();
            // NewBISSvcRef.REPORT_MEMO_DETAIL rDetail = new NewBISSvcRef.REPORT_MEMO_DETAIL();
            NewBISSvcRef.REPORT_MEMO_DETAIL_Collection repMemoDetColl = new NewBISSvcRef.REPORT_MEMO_DETAIL_Collection();
            NewBISSvcRef.REPORT_MEMO_DETAILLIST_Collection rDetailColl = new NewBISSvcRef.REPORT_MEMO_DETAILLIST_Collection();

           // this.Cursor = Cursors.WaitCursor;

            try
            {
                
                using (NewBISSvcRef.NewBISSvcClient client = new NewBISSvcRef.NewBISSvcClient())
                {
                    foreach (REPORT_SELECT_PRINT _reportSelect in _reportSelectColl)
                    {
                        pdfArray.Clear();

                        if (ISO.Exists) ISO.Delete();
                        //  if (Temp.Exists) Temp.Delete();
                        string newfooter = ""; 
                        try
                        {
                            
                            repMemoDetColl = client.GetReportMEMODetail(out pr, _reportSelect.AppNo, _reportSelect.UletterId, _reportSelect.sendofc);
                            rDetailColl = client.GetReportMEMODetaillist(out prlist, _reportSelect.AppNo,_reportSelect.UletterId);

                            //rDetailColl = new NewBISSvcRef.REPORT_MEMO_DETAILLIST_Collection();
                            //for (int i = 0; i < 15; i++)
                            //{
                            //    NewBISSvcRef.REPORT_MEMO_DETAILLIST x = new NewBISSvcRef.REPORT_MEMO_DETAILLIST();
                            //    x.Description = "XXXXXXXXXX";
                            //    rDetailColl.Add(x);
                            //}

                            //if (rDetailColl.Count > 8)
                            //{
                            //    for (int i = 0; i < 7; i++)
                            //    {
                            //        NewBISSvcRef.REPORT_MEMO_DETAILLIST x = new NewBISSvcRef.REPORT_MEMO_DETAILLIST();
                            //        x.Description = " ";
                            //        rDetailColl.Add(x);
                            //    }
                            //} 
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                        if (pr.Successed == true)
                        {
                            // if (rDetail != null)
                            if (repMemoDetColl != null)
                            {
                                string zipcode = "";
                                string newAddress2 = "";

                                foreach (NewBISSvcRef.REPORT_MEMO_DETAIL rDetail in repMemoDetColl)
                                {
                                   string[] aa = rDetail.Address2.Split(' ');
                                   if (aa != null && aa.Any())
                                   {
                                      zipcode =  aa.Last();
                                      newAddress2 = rDetail.Address2.Replace(zipcode, "");
                                   }
                                   // rDetail.Letter_dt = _reportSelect.printDate;  
                                    pdfArray.Clear();
                                    pdfArraySendOfc.Clear();
                                    REPORT_MEMO_DETAILBindingSource.DataSource = rDetail;
                                    REPORT_MEMO_DETAILLISTBindingSource.DataSource = rDetailColl;

                                    if (rDetail.last_status == "MO" || rDetail.last_status == "MD")
                                    {
                                        rDetail.last_status = "MO";
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
                                     
                                    if(bbl != "")
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

                                    string CHANNEL_TYPE = client.GetChannelType(out pr, _reportSelect.AppNo, _reportSelect.UletterId);

                                    if (CHANNEL_TYPE == "PO")
                                    {
                                        rDetail.Work_group_name = "ส่วนดำเนินงานกรมธรรม์ใหม่สามัญและช่องทางอื่น"; 
                                    }
                                    //if (_reportSelect.channelType != "GM")
                                    //{
                                        reportViewer1.LocalReport.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("region", rDetail.REGION));
                                        reportViewer1.LocalReport.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("lastStatus", rDetail.last_status));
                                        reportViewer1.LocalReport.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("HeaderExpire", rDetail.header_expire));
                                        reportViewer1.LocalReport.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("letter_dt", _reportSelect.printDate));//Utility.dateTimeToString(DateTime.Now, "วันที่ d Month yyyy", "BU")));  //apiwat เปลี่ยน letter dt จาก sysdate เป็น sysdate ครั้งแรกที่พิม
                                        reportViewer1.LocalReport.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("answer_limit_dt", Utility.dateTimeToString(Convert.ToDateTime(rDetail.Answer_limit_dt), "d Month yyyy", "BU")));
                                        reportViewer1.LocalReport.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("Work_group_name", rDetail.Work_group_name));
                                        reportViewer1.LocalReport.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("Workgroup", rDetail.Workgroup));
                                        reportViewer1.LocalReport.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("newfooter", newfooter));
                                        reportViewer1.LocalReport.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("CHANNEL_TYPE", CHANNEL_TYPE));
                                        reportViewer1.LocalReport.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("zipcode", zipcode));
                                        reportViewer1.LocalReport.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("newAddress2", newAddress2));
                                       

                                        reportViewer1.RefreshReport();
                                    //}
                                    //else if (_reportSelect.channelType == "GM")
                                    //{
                                    //    reportViewer2.LocalReport.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("region", rDetail.REGION));
                                    //    reportViewer2.LocalReport.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("lastStatus", rDetail.last_status));
                                    //    reportViewer2.LocalReport.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("HeaderExpire", rDetail.header_expire));
                                    //    reportViewer2.LocalReport.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("letter_dt", _reportSelect.printDate));//Utility.dateTimeToString(DateTime.Now, "วันที่ d Month yyyy", "BU")));  //apiwat เปลี่ยน letter dt จาก sysdate เป็น sysdate ครั้งแรกที่พิม
                                    //    reportViewer2.LocalReport.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("answer_limit_dt", Utility.dateTimeToString(Convert.ToDateTime(rDetail.Answer_limit_dt), "d Month yyyy", "BU")));
                                    //    reportViewer2.LocalReport.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("Work_group_name", rDetail.Work_group_name));
                                    //    reportViewer2.LocalReport.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("Workgroup", rDetail.Workgroup));
                                    //    reportViewer2.LocalReport.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("newfooter", newfooter));
                                    //    reportViewer1.LocalReport.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("CHANNEL_TYPE", CHANNEL_TYPE));

                                    //    reportViewer2.RefreshReport();
                                    //}

                                        genReportMoPDF(CHANNEL_TYPE);
                                    genISOPDF(_reportSelect.AppNo, client, _reportSelect.UletterId);

                                    System.IO.FileInfo fi = new System.IO.FileInfo(reportFinalFile);

                                    filePaths = Directory.GetFiles(dirPath);
                                    try
                                    {
                                        foreach (string filePath in filePaths)
                                        { 
                                            if (filePath.StartsWith(reportTempFile))
                                            {
                                                System.GC.Collect();
                                                System.GC.WaitForPendingFinalizers();
                                                File.Delete(filePath);
                                            } 
                                        }
                                    }
                                    catch (Exception ex)
                                    { }

                                    string tempWithTime = "";
                                    if (fi.Exists)
                                    {
                                        tempWithTime = reportTempFile + DateTime.Now.ToLongTimeString().ToString().Replace(":", "").Replace(" ", "") + ".pdf";
                                        //if (Temp.Exists) Temp.Delete();
                                        fi.CopyTo(tempWithTime);
                                        pdfArray.Add(tempWithTime); 
                                    }
                                    pdfArray.Add(reportMoFile);
                                    pdfArraySendOfc.Clear();
                                    pdfArraySendOfc.Add(reportMoFile);
                                    System.IO.FileInfo fiISO = new System.IO.FileInfo(reportISOFile);
                                    if (fiISO.Exists)
                                    {
                                        pdfArray.Add(reportISOFile);
                                        pdfArraySendOfc.Add(reportISOFile);
                                    } 

                                    MergeFiles(reportFinalFile, pdfArray.ToArray());

                                    //SEND OFC
                                    if (_reportSelect.office != null && _reportSelect.office != "" && _reportSelect.office != "สสง" && _reportSelect.office != "สนญ" && src_work == "PRINT" && _reportSelect.sendofc == "Y")
                                    {
                                        uploadFileToOfc(client, _reportSelect.AppNo, _reportSelect.office, rDetail.last_status, _reportSelect.UletterId, pdfArraySendOfc.ToArray(),rDetail.CUSTNAME_ONLY);
                                    }

                                    try
                                    {
                                        if (fiISO.Exists) fiISO.Delete();
                                    }
                                    catch (Exception ex)
                                    { }
                                }
                            }
                        }
                    }
                    if (client.State != System.ServiceModel.CommunicationState.Closed)
                    {
                        client.Close();
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            //finally
            //{
            //    this.Cursor = Cursors.Default;
            //}

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

        private void genISOPDF(string aNo, NewBISSvcRef.NewBISSvcClient client, long uletterId)
        {
            List<string> appNoArray = new List<string>();
            appNoArray.Clear();
              
            NewBISSvcRef.ProcessResult pr = new NewBISSvcRef.ProcessResult();
            NewBISSvcRef.AUTB_DOCUMENT_COLLECTION autbDoc = new NewBISSvcRef.AUTB_DOCUMENT_COLLECTION();
            try
            {
                autbDoc = client.GetIsoDocumentColl(out pr, aNo, uletterId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            if (pr.Successed == true)
            {
                if (autbDoc != null)
                {
                    foreach (NewBISSvcRef.AUTB_DOCUMENT autb in autbDoc)
                    {
                        DirectoryInfo di = new DirectoryInfo(fileISOIntranet);
                        FileInfo fiLatest = di.GetFiles(autb.ISO_CODE + "*.pdf").OrderByDescending(fi => fi.FullName).First(); 
                        appNoArray.Add(fileISOIntranet + Path.GetFileName(fiLatest.FullName));
                    } 
                    MergeFiles(reportISOFile, appNoArray.ToArray());
                }
            }
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
                
               // writer.Flush();
               // writer.Close();
               // writer.Dispose();
                 
                document.Close();
                document.Dispose();

                reader.Close();
                reader.Dispose();		
            }
            catch (Exception e)
            {
                string strOb = e.Message;
            }
        }

        private void genReportMoPDF(string ch)
        {
            try
            { 
                System.GC.Collect();
                System.GC.WaitForPendingFinalizers();

                Warning[] warnings;
                string[] streamids;
                string mimeType, encoding, filenameExtension;
                byte[] bytes = null;

                if (ch != "GM")
                {
                    bytes = reportViewer1.LocalReport.Render("PDF", null, out mimeType, out encoding, out filenameExtension, out streamids, out warnings);
                }
                else if (ch == "GM")
                {
                    bytes = reportViewer2.LocalReport.Render("PDF", null, out mimeType, out encoding, out filenameExtension, out streamids, out warnings);
                }
                using (System.IO.FileStream fs = System.IO.File.Create(reportMoFile))
                {
                    fs.Write(bytes, 0, bytes.Length);
                    fs.Close();
                    fs.Dispose();
                }
            }
            catch (Exception ex)
            {

            }
        }

         
    }
}
