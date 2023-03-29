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
using System.Threading;

namespace NewBisPA.ReportForm
{
    public partial class frmReportAppList : Form
    { 
        private string dirPath = @"C:\WINDOWS\Temp\NewbizReport";
        private string reportApFile = "C:\\WINDOWS\\Temp\\NewbizReport\\reportAppListPDF.pdf";
        private string reportTempFile = "C:\\WINDOWS\\Temp\\NewbizReport\\reportAppListTempPDF.pdf";
        private string reportFinalFile = "C:\\WINDOWS\\Temp\\NewbizReport\\reportAppListFinalPDF" + DateTime.Now.ToLongTimeString().ToString().Replace(":", "").Replace(" ", "") + ".pdf";
        NewBISSvcRef.APP_IN_PROCESS[] appInProcesses;

        string condition = "";
        string username = "";
        string sumapp = "";
        string sumsum = "";
        string sumpremium = "";

        public frmReportAppList(NewBISSvcRef.APP_IN_PROCESS[] appInProcessesParam, string _condition, string _username, string _sumapp, string _sumsum, string _sumpremium) //appInProcs,condition,UserID,txtsumapp.Text,txtsumm.Text,txtpremium.Text
        {
            condition = _condition;
            username = _username;
            sumapp = _sumapp;
            sumsum = _sumsum;
            sumpremium = _sumpremium;
            appInProcesses = appInProcessesParam;
            
            foreach (NewBISSvcRef.APP_IN_PROCESS app in appInProcesses)
            {
                app.NEWBIS_DT_DISPLAY = app.NEWBIS_DT==null ? "": ITUtility.Utility.dateTimeToString((DateTime)app.NEWBIS_DT, "dd/MM/yyyy hh:mm", "BU");
                app.APP_OFCRCV_DT_DISPLAY = app.APP_OFCRCV_DT==null? "":ITUtility.Utility.dateTimeToString((DateTime)app.APP_OFCRCV_DT, "dd/MM/yyyy hh:mm", "BU");
            }

            InitializeComponent(); 
            System.IO.DirectoryInfo directoryPath = new System.IO.DirectoryInfo(dirPath);
            if (!directoryPath.Exists)
            {
                System.IO.Directory.CreateDirectory(dirPath);
            }
            System.IO.FileInfo Ap = new System.IO.FileInfo(reportApFile);
            if (Ap.Exists) Ap.Delete();
            System.IO.FileInfo Temp = new System.IO.FileInfo(reportTempFile);
            if (Temp.Exists) Temp.Delete();
           
        }

        private void frmReportAppList_Load(object sender, EventArgs e)
        { 
            List<string> pdfArray = new List<string>();
             
            try
            {
                using (NewBISSvcRef.NewBISSvcClient client = new NewBISSvcRef.NewBISSvcClient())
                {
                    System.IO.FileInfo Temp = new System.IO.FileInfo(reportTempFile);
                    if (Temp.Exists) Temp.Delete();

                    APP_IN_PROCESS_COLLECIONBindingSource.DataSource = appInProcesses;
                    reportViewer1.LocalReport.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("condition", condition.ToString()));
                    reportViewer1.LocalReport.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("userid", username.ToString()));
                    reportViewer1.LocalReport.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("sumapp", sumapp.ToString()));
                    reportViewer1.LocalReport.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("sumsum", sumsum.ToString()));
                    reportViewer1.LocalReport.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("sumpremium", sumpremium.ToString()));
                    reportViewer1.RefreshReport();

                    System.IO.FileInfo fi = new System.IO.FileInfo(reportFinalFile);
                    if (fi.Exists)
                    {
                        pdfArray.Clear();
                        fi.CopyTo(reportTempFile);
                        genReportApPDF(reportApFile);
                        pdfArray.Add(reportTempFile);
                        pdfArray.Add(reportApFile);
                        MergeFiles(reportFinalFile, pdfArray.ToArray());
                    }
                    else
                    {
                        genReportApPDF(reportFinalFile);
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
             
            webBrowser1.Navigate(reportFinalFile); 
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
