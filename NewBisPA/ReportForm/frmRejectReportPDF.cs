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
    public partial class frmRejectReportPDF : Form
    {
        private string _APPROVED;

        public string APPROVED
        {
            get { return _APPROVED; }
            set { _APPROVED = value; }
        }

        private string dirPath = @"C:\WINDOWS\Temp\NewbizReport";
        private string reportRejectFile = "C:\\WINDOWS\\Temp\\NewbizReport\\reportRejectPDF.pdf";
        private string reportTempFile = "C:\\WINDOWS\\Temp\\NewbizReport\\reportRejectTempPDF.pdf";
        private string reportFinalFile = "C:\\WINDOWS\\Temp\\NewbizReport\\reportRejectFinalPDF" + DateTime.Now.ToLongTimeString().ToString().Replace(":", "").Replace(" ", "") + ".pdf"; 
        REPORT_SELECT_PRINT_Collection RepSelectPrntColl = new REPORT_SELECT_PRINT_Collection();
        REPORT_SELECT_PRINT RepSelectPrnt = new REPORT_SELECT_PRINT();
        private string callCenterPhoneNo = "";
        private string paymentChannel = "";
        private string _letter_dt = "";
        NewBISSvcRef.U_LETTER_PRINT[] uLetterPrints = null;
        NewBISSvcRef.U_LETTER_PRINT uLetterPrint = new NewBISSvcRef.U_LETTER_PRINT();
        NewBISSvcRef.ProcessResult prLt = new NewBISSvcRef.ProcessResult();
        NewBISSvcRef.ProcessResult prCnt = new NewBISSvcRef.ProcessResult();
        private string userid = "";
        private string branchSending = "";
        private string sourceFlg = ""; // UND=หน้าพิจารณา LET=หน้าพิมพ์จดหมาย
        private string deptype = "";
        private string deptypeContinue = "";

        public frmRejectReportPDF(string _appNo, long _uletterid, string _UserId)
        {
            InitializeComponent();
             
            RepSelectPrnt.AppNo = _appNo;
            RepSelectPrnt.UletterId = _uletterid;
            RepSelectPrnt.UserId = _UserId;
            userid = _UserId;
            RepSelectPrnt.UserId = "";
             
            System.IO.DirectoryInfo directoryPath = new System.IO.DirectoryInfo(dirPath);
            if (!directoryPath.Exists)
            {
                System.IO.Directory.CreateDirectory(dirPath);
            }
            System.IO.FileInfo Ap = new System.IO.FileInfo(reportRejectFile);
            if (Ap.Exists) Ap.Delete();
            System.IO.FileInfo Temp = new System.IO.FileInfo(reportTempFile);
            if (Temp.Exists) Temp.Delete();

            NewBISSvcRef.ProcessResult prch = new NewBISSvcRef.ProcessResult();
            using (NewBISSvcRef.NewBISSvcClient client = new NewBISSvcRef.NewBISSvcClient())
            {
                try
                {
                    if (RepSelectPrnt.channelType == null || RepSelectPrnt.channelType == "")
                    {
                        RepSelectPrnt.channelType = client.GetChannelType(out prch , RepSelectPrnt.AppNo, RepSelectPrnt.UletterId);
                    }


                   long[] uletter_ids = { _uletterid };
                   RepSelectPrnt.approved = "";

                   NewBISSvcRef.ProcessResult prNa = new NewBISSvcRef.ProcessResult();
                   string detail = client.GetNewAppDetail(out prNa, _appNo, "");
                   if (detail == "")
                   {
                       using (AccountSvcRef.AccountServiceClient accClient = new AccountSvcRef.AccountServiceClient())
                       {
                           NewBISSvcRef.U_PAYMENT_REFERENCE[] ups = null;
                           client.GetCompositeDataU_PAYMENT_REFERENCE(out ups, null, uletter_ids);
                           if (ups != null && ups.Count() > 0)
                           {
                               RepSelectPrnt.approved = "N";
                               
                               foreach (NewBISSvcRef.U_PAYMENT_REFERENCE upr in ups)
                               {
                                   string outPayOption = "";
                                   string outBank = "";
                                   string outBranch = "";
                                   string outAccNo = "";
                                   string outPayName = "";
                                   DateTime? outPayDate = null;
                                   ProcessResult prAcc = new ProcessResult();
                                   prAcc = accClient.GET_AC_OPTPAY_BYREFID(out outPayOption, out outBank, out outBranch, out outAccNo, out outPayName, out outPayDate, "J70",upr.PAYREF_ID);
                                   if (outAccNo != "" && outAccNo != null)
                                   {
                                       RepSelectPrnt.approved = "";
                                   }

                               } 
                               //else
                               //{
                               //    RepSelectPrnt.approved = "N";
                               //}
                           }

                       }
                   }

                  // client.GetU_LETTER_PRINT(out uLetterPrints, uletter_ids, null);
                   string _letterdt = client.GetLetterDt(out prLt, (long?)_uletterid);
                   if (_letterdt !="-")
                   {
                        //uLetterPrint = uLetterPrints[0];
                       //_letter_dt = uLetterPrint.PRINT_DT == null ? Utility.dateTimeToString(DateTime.Now, "d Month yyyy", "BU") : Utility.dateTimeToString((DateTime)uLetterPrint.PRINT_DT, "d Month yyyy", "BU");
                       RepSelectPrnt.printDate = _letterdt;//uLetterPrint.PRINT_DT == null ? Utility.dateTimeToString(DateTime.Now, "d Month yyyy", "BU") : Utility.dateTimeToString((DateTime)uLetterPrint.PRINT_DT, "d Month yyyy", "BU");
                   }
                   else
                   {
                       //_letter_dt = "";// Utility.dateTimeToString(DateTime.Now, "d Month yyyy", "BU");
                       RepSelectPrnt.printDate = Utility.dateTimeToString(DateTime.Now, "d Month yyyy", "BU");
                   }

                   

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

            RepSelectPrntColl.Add(RepSelectPrnt);
            //System.IO.FileInfo Final = new System.IO.FileInfo(reportFinalFile);
            //if (Final.Exists) Final.Delete(); 
        }

        public frmRejectReportPDF(REPORT_SELECT_PRINT_Collection _repSelectPrntColl)   //(string[] appNoArray) //UserId
        {
            InitializeComponent();
            sourceFlg = "LET";
            //_letter_dt = Utility.dateTimeToString(DateTime.Now, "d Month yyyy", "BU");
            RepSelectPrntColl = _repSelectPrntColl; 
            System.IO.DirectoryInfo directoryPath = new System.IO.DirectoryInfo(dirPath);
            if (!directoryPath.Exists)
            {
                System.IO.Directory.CreateDirectory(dirPath);
            }
            System.IO.FileInfo Ap = new System.IO.FileInfo(reportRejectFile);
            if (Ap.Exists) Ap.Delete();
            System.IO.FileInfo Temp = new System.IO.FileInfo(reportTempFile);
            if (Temp.Exists) Temp.Delete();
            //System.IO.FileInfo Final = new System.IO.FileInfo(reportFinalFile);
            //if (Final.Exists) Final.Delete();
             
            using (NewBISSvcRef.NewBISSvcClient client = new NewBISSvcRef.NewBISSvcClient())
            {
                try
                {
                   foreach(REPORT_SELECT_PRINT r in RepSelectPrntColl)
                   {
                       userid = r.UserId;

                       if (r.approved != "N" && (r.printDate == "-" || r.printDate == "")) //ถ้าอนุมัติเงินแล้ว  แต่ยังไม่เคยพิม -> stamp,letter_dt= sysdate
                       {
                           client.StampPrintFlag(r.UletterId, r.UserId, null);
                           r.printDate = Utility.dateTimeToString(DateTime.Now, "d Month yyyy", "BU"); // คือวันเดวกับที่ stamp
                           //long[] uletter_ids = { r.UletterId };
                           //client.GetU_LETTER_PRINT(out uLetterPrints, uletter_ids, null);
                           //if (uLetterPrints != null && uLetterPrints.Count() > 0)
                           //{
                           //    uLetterPrint = uLetterPrints[0];
                           //   // _letter_dt = uLetterPrint.PRINT_DT == null ? Utility.dateTimeToString(DateTime.Now, "d Month yyyy", "BU") : Utility.dateTimeToString((DateTime)uLetterPrint.PRINT_DT, "d Month yyyy", "BU");
                           //    r.printDate = uLetterPrint.PRINT_DT == null ? "": Utility.dateTimeToString((DateTime)uLetterPrint.PRINT_DT, "d Month yyyy", "BU");
                           //}
                           //else
                           //{
                           //    client.StampPrintFlag(r.UletterId, r.UserId);
                           //    //ถ้าเป็นการพิมครั้งแรก stamp วันที่และใช้วันที่นั้นๆ(sysdate) เป็น letter_dt
                           //   // _letter_dt = Utility.dateTimeToString(DateTime.Now, "d Month yyyy", "BU");
                           //    r.printDate = Utility.dateTimeToString(DateTime.Now, "d Month yyyy", "BU");
                           //}
                       }
                       else if (r.approved != "N" && r.printDate != "-" && r.printDate != "") //ถ้าอนุมัติเงินแล้ว  และเคยพิม -> stamp,letter_dt= ดึงครั้งแรกที่ stamp
                       {
                           client.StampPrintFlag(r.UletterId, r.UserId, null);
                           string _letterdt = client.GetLetterDt(out prLt, (long?)r.UletterId);
                           r.printDate = _letterdt=="-"?"":_letterdt;
                       }
                   }
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
        }

        public frmRejectReportPDF(string _appNo, long _uletterid, string _channelType, string _UserId)
        {
            InitializeComponent();
            string policyHolding = "";
            sourceFlg = "UND";

            using (NewBISSvcRef.NewBISSvcClient client = new NewBISSvcRef.NewBISSvcClient())
            {
                 
            if (_channelType == "GM")
            {
                NewBISSvcRef.ProcessResult prHolding = new NewBISSvcRef.ProcessResult();
                policyHolding = client.GetPolicyHolding(out prHolding, _appNo, _channelType); 
            }

            RepSelectPrnt.AppNo = _appNo;
            RepSelectPrnt.UletterId = _uletterid;
            RepSelectPrnt.UserId = _UserId;
            RepSelectPrnt.UserId = "";
            RepSelectPrnt.policyHolding = policyHolding;

            System.IO.DirectoryInfo directoryPath = new System.IO.DirectoryInfo(dirPath);
            if (!directoryPath.Exists)
            {
                System.IO.Directory.CreateDirectory(dirPath);
            }
            System.IO.FileInfo Ap = new System.IO.FileInfo(reportRejectFile);
            if (Ap.Exists) Ap.Delete();
            System.IO.FileInfo Temp = new System.IO.FileInfo(reportTempFile);
            if (Temp.Exists) Temp.Delete();


          
                try
                {
                    long[] uletter_ids = { _uletterid }; 
                    string _letterdt = client.GetLetterDt(out prLt, (long?)_uletterid);
                    if (_letterdt != "-")
                    { 
                        RepSelectPrnt.printDate = _letterdt;
                    }
                    else
                    { 
                        RepSelectPrnt.printDate = Utility.dateTimeToString(DateTime.Now, "d Month yyyy", "BU");
                    }

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

            RepSelectPrntColl.Add(RepSelectPrnt); 
        }

        private void frmReportRejectPDF_Load(object sender, EventArgs e)
        { 
           // this.Cursor = Cursors.WaitCursor;
            List<string> pdfArray = new List<string>();
            NewBISSvcRef.ProcessResult pr = new NewBISSvcRef.ProcessResult();
            NewBISSvcRef.ProcessResult pr2 = new NewBISSvcRef.ProcessResult(); 
            ProcessResult pr3 = new ProcessResult();
            NewBISSvcRef.ProcessResult pr4 = new NewBISSvcRef.ProcessResult();
            NewBISSvcRef.ProcessResult pr5 = new NewBISSvcRef.ProcessResult();
            NewBISSvcRef.ProcessResult pr6 = new NewBISSvcRef.ProcessResult();
            NewBISSvcRef.ProcessResult pr7 = new NewBISSvcRef.ProcessResult();

            NewBISSvcRef.REPORT_DC_Collection repDCs = new NewBISSvcRef.REPORT_DC_Collection();
            NewBISSvcRef.U_RETURN_PRMNEWAPP[] uRetPrmNewApps;
            NewBISSvcRef.U_RETURN_PRMNEWAPP uRetPrmNewApp = new NewBISSvcRef.U_RETURN_PRMNEWAPP();
            NewBISSvcRef.ZTB_SIGNATURE[] ztbSignatures; 
            NewBISSvcRef.ZTB_SIGNATURE ztbSignature = new NewBISSvcRef.ZTB_SIGNATURE();
            NewBISSvcRef.U_PAYMENT_REFERENCE[] uPaymentReferences; 
            NewBISSvcRef.U_PAYMENT_REFERENCE uPaymentReference = new NewBISSvcRef.U_PAYMENT_REFERENCE();
            NewBISSvcRef.ZTB_OFFICE[] ztbOffices; 
            NewBISSvcRef.ZTB_OFFICE ztbOffice = new NewBISSvcRef.ZTB_OFFICE(); 
            NewBISSvcRef.GBBL_STRUCT[] gbblStructs; 
            NewBISSvcRef.GBBL_STRUCT gbblStruct = new NewBISSvcRef.GBBL_STRUCT();
             
            using (NewBISSvcRef.NewBISSvcClient client = new NewBISSvcRef.NewBISSvcClient())
            { 
                 try
                {
                    callCenterPhoneNo = client.getTelephoneNo(out pr7, "CALL CENTER");

                    foreach (REPORT_SELECT_PRINT rsp in RepSelectPrntColl)
                    { 
                        repDCs = client.GetRejectReportData(out pr, rsp.AppNo, rsp.UletterId, rsp.channelType); 
                        if (pr.Successed == true)
                        {
                            if (repDCs != null)
                            { 
                                foreach (NewBISSvcRef.REPORT_DC repDC in repDCs)
                                {
                                    if (repDC.CHANNEL_TYPE == "KB")
                                    {
                                        if (repDC.ACC_DEPOSIT_TYPE == "S")
                                        {
                                            deptype = "สะสมทรัพย์";
                                            deptypeContinue = "ปกติ";
                                            repDC.TITLE = "เงินฝากขวัญบัวหลวง ประเภทสะสมทรัพย์";
                                        }
                                        else if (repDC.ACC_DEPOSIT_TYPE == "F")
                                        {
                                            deptype = "ประจำ";
                                            deptypeContinue = "ตามระยะเวลาฝากเดิม";
                                            repDC.TITLE = "เงินฝากขวัญบัวหลวง ประเภทประจำ";
                                        }
                                    }
                                     
                                    if (rsp.approved == "N")
                                    {
                                        repDC.APPROVED = "ใบคำขอนี้ ยังไม่มีการอนุมัติเงินคืนจากฝ่ายบัญชี";
                                    }
                                    else
                                    {
                                        repDC.APPROVED = "";
                                    }

                                    if (repDC.CHANNEL_TYPE == "KB")
                                    {
                                        repDC.ACC_NO = String.Format("{0:###-#-#####-#}", Convert.ToInt64(repDC.ACC_NO));
                                        if (repDC.RECEIVE_DT != null)
                                        {
                                            if (repDC.RECEIVE_DT.StartsWith("0"))
                                            {
                                                repDC.RECEIVE_DT = repDC.RECEIVE_DT.Substring(1, repDC.RECEIVE_DT.Length - 1).Replace("     ", " ");
                                            }
                                        }

                                        //repDC.SIG_POSITION_LINE1 = "ผู้บริหารส่วน";
                                        //repDC.SIG_POSITION_LINE2= "ส่วนกรมธรรม์ BANCASSURANCE";
                                    }
                                    
                                    System.IO.FileInfo Temp = new System.IO.FileInfo(reportTempFile);
                                    if (Temp.Exists) Temp.Delete();

                                    //check return newapp
                                    string newAppDes = "";
                                    long[] uletter_id = { (long)repDC.ULETTER_ID };
                                    client.GetU_RETURN_PRMNEWAPP(out uRetPrmNewApps, uletter_id);
                                    if (uRetPrmNewApps != null)
                                    {
                                        uRetPrmNewApp = uRetPrmNewApps[0];
                                        repDC.NEW_APP_NO = uRetPrmNewApp.NEW_APP_NO;
                                        repDC.NEW_APP_NAME = uRetPrmNewApp.NEW_APP_NAME;
                                        repDC.NEW_APP_SURNAME = uRetPrmNewApp.NEW_APP_SURNAME;
                                        repDC.NEW_APP_TRN_DT = uRetPrmNewApp.NEW_APP_TRN_DT;
                                        newAppDes = "- โอนไปชำระเบี้ยประกันตามใบคำขอเลขที่ " + repDC.NEW_APP_NO + " ของ คุณ " + repDC.NEW_APP_NAME + " " + repDC.NEW_APP_SURNAME;
                                    }
                                    else
                                    {
                                        repDC.NEW_APP_NO = "0";
                                        newAppDes = "";
                                    }

                                    //check new return newapp
                                    if (repDC.NEW_APP_NO == "0")
                                    {
                                        NewBISSvcRef.ProcessResult prNa = new NewBISSvcRef.ProcessResult();
                                        string detail = client.GetNewAppDetail(out prNa, repDC.APP_NO, repDC.CHANNEL_TYPE);
                                        if (prNa.Successed == false)
                                        {
                                            throw new Exception(prNa.ErrorMessage);
                                        }
                                        if (detail != "")
                                        {
                                            newAppDes = "- โอนไป " + detail;
                                        }
                                    }

                                    //check match signature
                                    //830044	อมลวรรณ	กุลวิรัชติวงศ์	ผู้บริหารส่วนดำเนินงานกรมธรรม์ใหม่
                                    string firstParagraph = "";
                                    if (repDC.REASON == "เหตุผลทางการแพทย์")
                                    {
                                        repDC.SIGNATURE_ID = "930731";
                                        repDC.USERID = "930731";
                                        repDC.SIG_NAME = "นางโสมอุษา	ชิดชนกนารถ";
                                        repDC.SIG_POSITION = "ผู้อำนวยการฝ่ายรับประกันและสินไหม";
                                        firstParagraph = "อุบัติเหตุส่วนบุคคล";
                                        repDC.WORKGROUP = "ส่วนดำเนินงานกรมธรรม์ใหม่สามัญและช่องทางอื่น";
                                    }
                                    else
                                    {
                                        repDC.SIGNATURE_ID = "830044";
                                        repDC.USERID = "830044";
                                        repDC.SIG_NAME = "นางสาวอมลวรรณ	กุลวิรัชติวงศ์";
                                        repDC.SIG_POSITION = "ผู้บริหารส่วนดำเนินงานกรมธรรม์ใหม่";
                                        firstParagraph = "ชีวิต";
                                    }
                                     
                                    //check match signature
                                    if (repDC.CHANNEL_TYPE == "PO")
                                    {
                                        repDC.SIGNATURE_ID = "930731";
                                        repDC.USERID = "930731";
                                        repDC.SIG_NAME = "นางโสมอุษา	ชิดชนกนารถ";
                                        repDC.SIG_POSITION = "ผู้อำนวยการฝ่ายรับประกันและสินไหม";
                                        firstParagraph = "อุบัติเหตุส่วนบุคคล";
                                        repDC.WORKGROUP = "ส่วนดำเนินงานกรมธรรม์ใหม่สามัญและช่องทางอื่น";
                                    }

                                    if (repDC.CHANNEL_TYPE == "PN")
                                    {
                                        repDC.SIGNATURE_ID = "930731";
                                        repDC.USERID = "930731";
                                        repDC.SIG_NAME = "นางโสมอุษา	ชิดชนกนารถ";
                                        repDC.SIG_POSITION = "ผู้อำนวยการฝ่ายรับประกันและสินไหม";
                                        firstParagraph = "ชีวิต";
                                        repDC.WORKGROUP = "ส่วนดำเนินงานกรมธรรม์ใหม่สามัญและช่องทางอื่น";
                                    }


                                    string[] signature_id = { repDC.SIGNATURE_ID, repDC.USERID };
                                    client.GetZTB_SIGNATURE(out ztbSignatures, signature_id);
                                    if (ztbSignatures != null)
                                    {
                                        ztbSignature = ztbSignatures[0]; 
                                        repDC.SIGNATURE = ztbSignature.SIGNATURE;
                                    } 

                                    //get barcode
                                    if (repDC.REFERENCE != "" && repDC.REFERENCE != null)
                                    {
                                        string eRefNo = client.GetERefNo(out pr2, repDC.REFERENCE); // get reference in english
                                        System.Drawing.Image barCode = CreateBarcode(eRefNo);
                                        repDC.BARCODE = imageToByteArray(barCode);
                                        repDC.BARCODETEXT = eRefNo.Replace("*", "").Trim();
                                    }

                                    if (repDC.NEW_APP_NO == "0")
                                    {
                                        #region "GET PAYMENT TYPE"
                                        //get payment type 
                                        long[] uletterid = { (long)repDC.ULETTER_ID };
                                        client.GetCompositeDataU_PAYMENT_REFERENCE(out uPaymentReferences, null, uletterid);
                                        
                                        //count payment option
                                        bool returnByOptionFlg = false; //false คือไม่ต้องแจกแจงว่าแต่ละ option คืนเท่าไร
                                        int? cntPay = client.CountPayOption(out prCnt, repDC.ULETTER_ID);
                                        if (cntPay != null && cntPay > 1)
                                        {
                                            returnByOptionFlg = true;
                                        }

                                        repDC.PAYMENT_DES = "";
                                        repDC.FOOTER = "";
                                        paymentChannel = "";

                                        if (uPaymentReferences != null)
                                        {
                                            for (int i = 0; i < uPaymentReferences.Length; i++)  //loop for จ่ายเงินหลายวิธี
                                            {
                                                uPaymentReference = uPaymentReferences[i];
                                                using (AccountSvcRef.AccountServiceClient accClient = new AccountSvcRef.AccountServiceClient())
                                                {
                                                    string outPayOption = "";
                                                    string outBank = "";
                                                    string outBranch = "";
                                                    string outAccNo = "";
                                                    string outPayName = "";
                                                    DateTime? outPayDate = null;
                                                    decimal? outPay = null;

                                                    //pr3 = accClient.GET_AC_OPTPAY_BYREFID(out outPayOption, out outBank, out outBranch, out outAccNo, out outPayName, out outPayDate, "J70", uPaymentReference.PAYREF_ID); //jobcode เอา uletterid ไป get มา
                                                    pr3 = accClient.GET_AC_OPTPAY_BYREFID_V2(out outPayOption, out outBank, out outBranch, out outAccNo, out outPayName, out outPayDate, out outPay, "J70", uPaymentReference.PAYREF_ID);

                                                    if (pr3.Successed == true)
                                                    {
                                                        string bank = client.GetBankName(out pr4, outBank);
                                                        string branch = client.GetBranchName(out pr5, outBank, outBranch);
                                                        branchSending = branch;
                                                        if (pr4.Successed == true)
                                                        {
                                                            if (bank != "")
                                                            {
                                                                repDC.BANK = bank;
                                                            }
                                                            else
                                                            {
                                                                repDC.BANK = "";
                                                            }
                                                        }
                                                        else
                                                        {
                                                            MessageBox.Show(pr4.ErrorMessage);
                                                        }

                                                        if (pr5.Successed == true)
                                                        {
                                                            if (branch != "")
                                                            {
                                                                repDC.Branch = branch.Replace("ธนาคารกรุงเทพ ", "");
                                                            }
                                                            else
                                                            {
                                                                repDC.Branch = "";
                                                            }
                                                        }
                                                        else
                                                        {
                                                            MessageBox.Show(pr5.ErrorMessage);
                                                        }

                                                        repDC.PAY_OPTION = outPayOption;
                                                        repDC.ACCNO = outAccNo;
                                                        repDC.PAY_NAME = outPayName;
                                                        repDC.PAY = outPay;

                                                        if (outPayDate != null)
                                                        {
                                                            repDC.PAY_DATE = Utility.dateTimeToString((DateTime)outPayDate, "dd/MM/yyyy", "BU");//String.Format("{0:dd/MM/yyyy}", outPayDate);
                                                        }
                                                        string mbphone = "";

                                                        if (repDC.MBPHONE != null)
                                                        {
                                                            mbphone = repDC.MBPHONE;
                                                        } 

                                                        string[] appofc = { repDC.APP_OFC };

                                                        if (repDC.WORKGROUPCODE == "OD2" && repDC.SEND_OFC != null && repDC.SEND_OFC != "")
                                                        { 
                                                            appofc[0] = repDC.SEND_OFC;
                                                        }

                                                        string adroffice = "";

                                                        if (repDC.APP_OFC.Trim() != "สนญ" && repDC.CHANNEL_TYPE != "PO")
                                                        {
                                                            pr6 = client.GetZTB_OFFICE(out ztbOffices, appofc);
                                                            if (pr6.Successed == true)
                                                            {
                                                                if (ztbOffices != null)
                                                                {
                                                                    ztbOffice = ztbOffices[0];
                                                                    adroffice = "โดยผ่านทาง บริษัท กรุงเทพประกันชีวิต จำกัด (มหาชน) " + ztbOffice.DESCRIPTION + "\n"
                                                                        + "         เลขที่ " + ztbOffice.ADR1 + "\n          " + ztbOffice.ADR2.Trim() + "\n         " + ztbOffice.ADR3.Trim() + " " + ztbOffice.POST + " โทร. " + ztbOffice.TELEPHONE;

                                                                }
                                                            }
                                                            else
                                                            {
                                                                MessageBox.Show(pr6.ErrorMessage);
                                                            }
                                                        }
                                                        else if (repDC.APP_OFC.Trim() == "สนญ" && (repDC.WORKGROUPCODE == "OD2"))
                                                        {
                                                            pr6 = client.GetZTB_OFFICE(out ztbOffices, appofc);
                                                            if (pr6.Successed == true)
                                                            {
                                                                if (ztbOffices != null)
                                                                {
                                                                    ztbOffice = ztbOffices[0];
                                                                    adroffice = "โดยผ่านทาง บริษัท กรุงเทพประกันชีวิต จำกัด (มหาชน) " + ztbOffice.DESCRIPTION + "\n"
                                                                        + "         เลขที่ " + ztbOffice.ADR1 + "\n          " + ztbOffice.ADR2.Trim() + "\n         " + ztbOffice.ADR3.Trim() + " " + ztbOffice.POST + " โทร. " + ztbOffice.TELEPHONE;

                                                                }
                                                            }
                                                            else
                                                            {
                                                                MessageBox.Show(pr6.ErrorMessage);
                                                            }
                                                        }
                                                        else if (repDC.CHANNEL_TYPE == "PO")
                                                        {
                                                            long[] uappids = { (long)repDC.UAPP_ID };
                                                            NewBISSvcRef.U_POLICY_SENDING[] uPolSends = null;
                                                            client.GetU_POLICY_SENDING(out uPolSends, uappids);
                                                            string[] newOfc = { uPolSends[0].OFFICE };
                                                            pr6 = client.GetZTB_OFFICE(out ztbOffices, newOfc);
                                                            if (pr6.Successed == true)
                                                            {
                                                                if (ztbOffices != null)
                                                                {
                                                                    ztbOffice = ztbOffices[0];
                                                                    adroffice = "โดยผ่านทาง บริษัท กรุงเทพประกันชีวิต จำกัด (มหาชน) " + ztbOffice.DESCRIPTION + "\n"
                                                                        + "         เลขที่ " + ztbOffice.ADR1 + "\n          " + ztbOffice.ADR2.Trim() + "\n         " + ztbOffice.ADR3.Trim() + " " + ztbOffice.POST + " โทร. " + ztbOffice.TELEPHONE;

                                                                }
                                                            }
                                                            else
                                                            {
                                                                MessageBox.Show(pr6.ErrorMessage);
                                                            }
                                                        }
                                                        else
                                                        {
                                                            //adroffice = "โดยผ่าน คุณ" + repDC.SALE_AGENT_NAME + " โทรศัพท์ " + mbphone;
                                                            adroffice = "โดยผ่าน " + repDC.SALE_AGENT_NAME + " โทรศัพท์ " + mbphone;
                                                        }

                                                        string[] agentcode = { repDC.SALE_AGENT_CODE };
                                                        string adrbankbranch = "";
                                                        pr6 = client.GetGBBL_STRUCT(out gbblStructs, agentcode);
                                                        if (pr6.Successed == true)
                                                        {
                                                            if (gbblStructs != null)
                                                            {
                                                                gbblStruct = gbblStructs[0];
                                                                adrbankbranch = "โดยผ่านทาง ธนาคารกรุงเทพ จำกัด (มหาชน) " + gbblStruct.DESCRIPTION + "\n"
                                                                    + "         เลขที่ " + gbblStruct.ADR1 + "\n         " + gbblStruct.ADR2 + "\n         " + gbblStruct.ADR3 + " " + gbblStruct.POST + " โทร. " + gbblStruct.TELEPHONE;
                                                                branchSending = gbblStruct.DESCRIPTION;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            MessageBox.Show(pr6.ErrorMessage);
                                                        }

                                                        decimal? fee = 0;
                                                        decimal? charge = 0;
                                                        decimal? commission = 0;
                                                        string msg = "";
                                                        decimal? bankfee = 0;

                                                        if (repDC.SEND_BY == null || repDC.SEND_BY == "") //จดหมาย IF send_by เป็น null
                                                        {
                                                            if (repDC.WORKGROUPCODE == "BNC")
                                                            {
                                                                repDC.SEND_BY = "PB"; //ส่งไป bank    
                                                            }
                                                            else
                                                            {
                                                                repDC.SEND_BY = "PO"; //ส่ง office <ไม่แน่ใจสะสมเงินเดืนอว่าตัวแทนมารับเองหรือไม่>
                                                            }
                                                        }

                                                        switch (repDC.PAY_OPTION)
                                                        {
                                                            #region TF 
                                                            case "TF":
                                                                if (repDC.PAYMENT_DES != "") { repDC.PAYMENT_DES += "\n"; }

                                                                if (returnByOptionFlg == false)
                                                                {
                                                                    repDC.PAYMENT_DES += "- โอนเงินเข้าบัญชี " + repDC.BANK + " " + repDC.Branch + " เลขที่ " + repDC.ACCNO + " วันที่ " + repDC.PAY_DATE;
                                                                }
                                                                else if (returnByOptionFlg == true)
                                                                {
                                                                    repDC.PAYMENT_DES += "- โอนเงินเข้าบัญชี " + repDC.BANK + " " + repDC.Branch + " เลขที่ " + repDC.ACCNO + " วันที่ " + repDC.PAY_DATE + " เป็นจำนวนเงิน " + String.Format("{0:n}", repDC.PAY) + " บาท";
                                                                } 

                                                                repDC.FOOTER = "";

                                                                #region ค่าธรรมเนียมการโอนเงิน
                                                                if (repDC.STATUS == "NT" && repDC.SUBSTATUS == "NT" && rsp.channelType != "GM") 
                                                                {
                                                                    NewBISSvcRef.ProcessResult prBankFee = new NewBISSvcRef.ProcessResult();
                                                                    // decimal? bankfee = client.GetBankFee(out prBankFee, "J70", uPaymentReference.PAYREF_ID,"TF");
                                                                    fee = 0;
                                                                    charge = 0;
                                                                    commission = 0; 
                                                                    msg = "";
                                                                    bankfee = 0;

                                                                    prBankFee = client.GET_BANK_FEE(out fee, out charge, out commission, out msg, "J70", uPaymentReference.PAYREF_ID);

                                                                    if (prBankFee.Successed == false)
                                                                    {
                                                                        throw new Exception(prBankFee.ErrorMessage);
                                                                    }
                                                                    if (msg == "N")
                                                                    {
                                                                        throw new Exception("Error : Get Bank Fee Process");
                                                                    }

                                                                    bankfee = fee + charge + commission;

                                                                    if (bankfee != 0)
                                                                    {
                                                                        repDC.NT_BANKFEE = "";
                                                                        repDC.NT_BANKFEE = "หมายเหตุ ทั้งนี้ บริษัทฯ ได้หักค่าธรรมเนียมในการโอนเงินเข้าบัญชีเป็นจำนวนเงิน " + bankfee.Value.ToString() + " บาท เรียบร้อยแล้ว";
                                                                        repDC.RETURN_PREMIUM = repDC.RETURN_PREMIUM - bankfee;
                                                                    }
                                                                }
#endregion

                                                                break;
                                                            #endregion

                                                            #region CQ  
                                                            case "CQ":
                                                                {
                                                                    if (repDC.PAYMENT_DES != "") { repDC.PAYMENT_DES += "\n"; }

                                                                    if (returnByOptionFlg == false)
                                                                    {
                                                                        repDC.PAYMENT_DES += "- เช็ค " + repDC.BANK + " เลขที่ " + repDC.ACCNO + " วันที่ " + repDC.PAY_DATE + " จำนวน 1 ฉบับ";
                                                                    }
                                                                    else if (returnByOptionFlg == true)
                                                                    {
                                                                        repDC.PAYMENT_DES += "- เช็ค " + repDC.BANK + " เลขที่ " + repDC.ACCNO + " วันที่ " + repDC.PAY_DATE + " จำนวน 1 ฉบับ\nเป็นจำนวนเงิน " + String.Format("{0:n}", repDC.PAY) + " บาท";
                                                                    } 

                                                                    if (paymentChannel != "") { paymentChannel += "\n"; } 
                                                                    switch (repDC.SEND_BY)
                                                                    {
                                                                        case "PB":
                                                                            //   repDC.PAYMENT_DES += "\n " + adrbankbranch;
                                                                            if (paymentChannel.Trim() != adrbankbranch.Trim())
                                                                            paymentChannel += adrbankbranch;
                                                                            break;
                                                                        case "PO":
                                                                            //  repDC.PAYMENT_DES += "\n " + adroffice;
                                                                            if (paymentChannel.Trim() != adroffice.Trim())
                                                                            paymentChannel += adroffice;
                                                                            break;
                                                                        case "AG":
                                                                            // repDC.PAYMENT_DES += "\n " + "โดยผ่าน " + repDC.SALE_AGENT_NAME + " โทรศัพท์ " + mbphone;
                                                                            if (paymentChannel.Trim() != "โดยผ่าน " + repDC.SALE_AGENT_NAME + " โทรศัพท์ " + mbphone)
                                                                            paymentChannel += "โดยผ่าน " + repDC.SALE_AGENT_NAME + " โทรศัพท์ " + mbphone;
                                                                            break;
                                                                        default:
                                                                            // repDC.SEND_BY_ADDRESS = ""; 
                                                                            break;
                                                                    }

                                                                    //add 16/8/2556 
                                                                    repDC.PAYMENT_DES += "\n" + paymentChannel.TrimEnd();

                                                                    #region ค่าธรรมเนียมการออกเช็ค
                                                                    if (repDC.STATUS == "NT" && repDC.SUBSTATUS == "NT" && rsp.channelType != "GM")
                                                                    {
                                                                        NewBISSvcRef.ProcessResult prBankFee = new NewBISSvcRef.ProcessResult();
                                                                        fee = 0;
                                                                        charge = 0;
                                                                        commission = 0;
                                                                        msg = "";
                                                                        bankfee = 0;

                                                                        prBankFee = client.GET_BANK_FEE(out fee, out charge, out commission, out msg, "J70", uPaymentReference.PAYREF_ID);

                                                                        //decimal? bankfee = client.GetBankFee(out prBankFee, "J70", uPaymentReference.PAYREF_ID, "CQ");

                                                                        if (prBankFee.Successed == false)
                                                                        {
                                                                            throw new Exception(prBankFee.ErrorMessage);
                                                                        }

                                                                        if (msg == "N")
                                                                        {
                                                                            throw new Exception("Error : Get Bank Fee Process");
                                                                        }

                                                                        bankfee = fee + charge + commission;

                                                                        if (bankfee != 0)
                                                                        {
                                                                            repDC.NT_BANKFEE = "";
                                                                            repDC.NT_BANKFEE = "หมายเหตุ ทั้งนี้ บริษัทฯ ได้หักค่าธรรมเนียมในการออกเช็คเป็นจำนวนเงิน " + bankfee.Value.ToString() + " บาท เรียบร้อยแล้ว";
                                                                            repDC.RETURN_PREMIUM = repDC.RETURN_PREMIUM - bankfee;
                                                                        }
                                                                    }
                                                                    #endregion

                                                                    break;
                                                                }
                                                            #endregion

                                                            #region DF

                                                            case "DF":
                                                                if (repDC.PAYMENT_DES != "") { repDC.PAYMENT_DES += "\n"; }

                                                                if (returnByOptionFlg == false)
                                                                {
                                                                    repDC.PAYMENT_DES += "- ตั๋วแลกเงิน " + repDC.BANK + " เลขที่ " + repDC.ACCNO + " วันที่ " + repDC.PAY_DATE + " จำนวน 1 ฉบับ";
                                                                }
                                                                else if (returnByOptionFlg == true)
                                                                {
                                                                    repDC.PAYMENT_DES += "- ตั๋วแลกเงิน " + repDC.BANK + " เลขที่ " + repDC.ACCNO + " วันที่ " + repDC.PAY_DATE + " จำนวน 1 ฉบับ\nเป็นจำนวนเงิน " + String.Format("{0:n}", repDC.PAY) + " บาท";
                                                                }

                                                                if (paymentChannel != "") { paymentChannel += "\n"; }
                                                                switch (repDC.SEND_BY)
                                                                {
                                                                    case "PB":
                                                                        //  repDC.PAYMENT_DES += "\n " + adrbankbranch;
                                                                        if (paymentChannel.Trim() != adrbankbranch.Trim())
                                                                        paymentChannel += adrbankbranch;
                                                                        // repDC.FOOTER = "สำเนาเรียน ผู้จัดการธนาคารกรุงเทพ จำกัด (มหาชน) " + gbblStruct.DESCRIPTION;
                                                                        repDC.FOOTER = "";
                                                                        break;
                                                                    case "PO":
                                                                        //  repDC.PAYMENT_DES += "\n " + adroffice;
                                                                        if (paymentChannel.Trim() != adroffice.Trim())
                                                                        paymentChannel = adroffice;
                                                                        break;
                                                                    case "AG":
                                                                        // repDC.PAYMENT_DES += "\n " + "โดยผ่าน คุณ" + repDC.SALE_AGENT_NAME + " โทรศัพท์ " + mbphone;
                                                                        if (paymentChannel.Trim() != "โดยผ่าน " + repDC.SALE_AGENT_NAME + " โทรศัพท์ " + mbphone)
                                                                        paymentChannel = "โดยผ่าน " + repDC.SALE_AGENT_NAME + " โทรศัพท์ " + mbphone;
                                                                        break;
                                                                    default:
                                                                        //repDC.SEND_BY_ADDRESS = "";
                                                                        break;
                                                                }
                                                                //add 16/8/2556  
                                                                repDC.PAYMENT_DES += "\n" + paymentChannel.TrimEnd();

                                                                if (repDC.STATUS == "NT" && repDC.SUBSTATUS == "NT" && rsp.channelType != "GM")
                                                                {
                                                                    NewBISSvcRef.ProcessResult prBankFee = new NewBISSvcRef.ProcessResult();
                                                                    //   decimal? bankfee = client.GetBankFee(out prBankFee, "J70", uPaymentReference.PAYREF_ID,"DF");
                                                                    fee = 0;
                                                                    charge = 0;
                                                                    commission = 0;
                                                                    msg = "";
                                                                    bankfee = 0;

                                                                    prBankFee = client.GET_BANK_FEE(out fee, out charge, out commission, out msg, "J70", uPaymentReference.PAYREF_ID);

                                                                    if (prBankFee.Successed == false)
                                                                    {
                                                                        throw new Exception(prBankFee.ErrorMessage);
                                                                    }
                                                                    if (msg == "N")
                                                                    {
                                                                        throw new Exception("Error : Get Bank Fee Process");
                                                                    }

                                                                    bankfee = fee + charge + commission;

                                                                    if (bankfee != 0)
                                                                    {
                                                                        repDC.NT_BANKFEE = "";
                                                                        repDC.NT_BANKFEE = "หมายเหตุ ทั้งนี้ บริษัทฯ ได้หักค่าธรรมเนียมในการออกตั๋วแลกเงินเป็นจำนวนเงิน " + bankfee.Value.ToString() + " บาท เรียบร้อยแล้ว";
                                                                        repDC.RETURN_PREMIUM = repDC.RETURN_PREMIUM - bankfee;
                                                                    }
                                                                }

                                                                break;
                                                            #endregion

                                                            #region CC
                                                            case "CC":
                                                                if (repDC.PAYMENT_DES != "") { repDC.PAYMENT_DES += "\n"; }
                                                                // repDC.PAYMENT_DES += "- บัตรเครดิต เลขที่ " + repDC.ACCNO + " วันที่ " + repDC.PAY_DATE;
                                                                String bran1 = repDC.ACCNO == null ? "" : repDC.ACCNO.Substring(0, 4);
                                                                String bran2 = repDC.ACCNO == null ? "" : repDC.ACCNO.Substring(4, 2);
                                                                String bran3 = repDC.ACCNO == null ? "" : repDC.ACCNO.Substring(6, 2);
                                                                NewBISSvcRef.ProcessResult prCc = new NewBISSvcRef.ProcessResult();
                                                                NewBISSvcRef.CDC_DESCRIPTION obj = new NewBISSvcRef.CDC_DESCRIPTION();
                                                                obj = client.GetCreditCardDesc(out prCc, bran1, bran2, bran3);
                                                                String CCBank = "";
                                                                if (obj == null || obj.CCDC_THAI_DESC == null)
                                                                {
                                                                    CCBank = "";
                                                                }
                                                                else
                                                                {
                                                                    CCBank = obj.CCDC_THAI_DESC;
                                                                }

                                                                if (returnByOptionFlg == false)
                                                                {
                                                                    repDC.PAYMENT_DES += "- ยกเลิกรายการเรียกเก็บเงินค่าเบี้ยประกันจากบัตรเครดิตกับ" + CCBank + " วันที่ " + repDC.PAY_DATE + " \n" + "   (หากถูกเรียกเก็บจากธนาคารแล้ว จะได้รับคืนในงวดถัดไป) ";
                                                                }
                                                                else if (returnByOptionFlg == true)
                                                                {
                                                                    repDC.PAYMENT_DES += "- ยกเลิกรายการเรียกเก็บเงินค่าเบี้ยประกันจากบัตรเครดิตกับ" + CCBank + " วันที่ " + repDC.PAY_DATE + " \n" + "   (หากถูกเรียกเก็บจากธนาคารแล้ว จะได้รับคืนในงวดถัดไป) เป็นจำนวนเงิน " + String.Format("{0:n}", repDC.PAY) + " บาท";
                                                                }

                                                                repDC.FOOTER = "";
                                                                break;
                                                            #endregion

                                                            #region DA
                                                            case "DA":
                                                                if (repDC.PAYMENT_DES != "") { repDC.PAYMENT_DES += "\n"; }

                                                                if (returnByOptionFlg == false)
                                                                {
                                                                    repDC.PAYMENT_DES += "- โอนไปชำระเบี้ยประกันตามใบคำขอ " + repDC.PAY_NAME;
                                                                }
                                                                else if (returnByOptionFlg == true)
                                                                {
                                                                    repDC.PAYMENT_DES += "- โอนไปชำระเบี้ยประกันตามใบคำขอ " + repDC.PAY_NAME + " เป็นจำนวนเงิน " + String.Format("{0:n}", repDC.PAY) + " บาท";
                                                                }
                                                                break;
                                                            #endregion
                                                        }
                                                    }  //close get service
                                                    else
                                                    {
                                                        MessageBox.Show(pr3.ErrorMessage);
                                                    }

                                                    if (accClient.State != System.ServiceModel.CommunicationState.Closed)
                                                    {
                                                        accClient.Close();
                                                    }
                                                }
                                            }
                                        }
                                        else//ถ้าไม่มีเงินคืน
                                        {
                                            string[] agentcode = { repDC.SALE_AGENT_CODE };
                                            string adrbankbranch = "";
                                            pr6 = client.GetGBBL_STRUCT(out gbblStructs, agentcode);
                                            if (pr6.Successed == true)
                                            {
                                                if (gbblStructs != null)
                                                {
                                                    gbblStruct = gbblStructs[0];
                                                    branchSending = gbblStruct.DESCRIPTION;
                                                }
                                            }
                                            else
                                            {
                                                MessageBox.Show(pr6.ErrorMessage);
                                            }
                                        }
                                        #endregion
                                    }
                                    else  //ถ้าโอนแอป
                                    {
                                        string[] agentcode = { repDC.SALE_AGENT_CODE };
                                        string adrbankbranch = "";
                                        pr6 = client.GetGBBL_STRUCT(out gbblStructs, agentcode);
                                        if (pr6.Successed == true)
                                        {
                                            if (gbblStructs != null)
                                            {
                                                gbblStruct = gbblStructs[0];
                                                branchSending = gbblStruct.DESCRIPTION;
                                            }
                                        }
                                        else
                                        {
                                            MessageBox.Show(pr6.ErrorMessage);
                                        }
                                    }

                                    paymentChannel = paymentChannel.TrimEnd();
                                    paymentChannel = "";
                                    //ไม่มีการจ่ายแบบอื่นๆแล้วมาเช็ค newapp
                                    if ((repDC.PAYMENT_DES == "" || repDC.PAYMENT_DES == null) && newAppDes != "")
                                    {
                                        repDC.PAYMENT_DES = newAppDes;
                                        repDC.FOOTER = "";
                                    }

                                    //ต่อ string แต่ง จดหมาย pp
                                    if (repDC.PP_UNTIL != null && repDC.PP_UNTIL != "")
                                    {
                                        repDC.PP_UNTIL = " จึงจำเป็นต้อง" + repDC.PP_UNTIL;
                                    }
                                      
                                    repDC.FOOTER = repDC.WORKGROUPCODE;
                                    repDC.LETTER_DT = rsp.printDate=="-"? "":rsp.printDate;//_letter_dt;

                                    //repDC.POLICY_HOLDING = "(Cert." + rsp.policyHolding + ")" ;

                                    if (repDC.WORKGROUPCODE == "BNC")
                                    {
                                        repDC.BNK_BRANCH = branchSending == "" ? repDC.BNK_BRANCH : branchSending;
                                    }

                                    if (repDC.CHANNEL_TYPE == "GM")
                                    {
                                        repDC.POLICY = Convert.ToInt64(repDC.POLICY).ToString().PadLeft(6,'0');
                                    }

                                   // repDC.LETTER_DT = Utility.dateTimeToString(DateTime.Now, "dd Month yyyy", "BU");
                                    repDC.BBL = "";
                                    if (repDC.WORKGROUPCODE == "BNC")
                                    {
                                        if (repDC.SALE_AGENT_NAME != null && repDC.SALE_AGENT_NAME.StartsWith("ธนาคาร"))
                                        {
                                            repDC.NEWFOOTER = "BANK"; //วาง footer แบบ bank
                                        }
                                        else if (repDC.SALE_AGENT_NAME != null && repDC.SALE_AGENT_NAME.StartsWith("บริษัท"))
                                        {
                                            repDC.NEWFOOTER = "BANK"; //วาง footer แบบ bank
                                            repDC.BBL = "บริษัท กรุงเทพประกันชีวิต จำกัด (มหาชน)";
                                        }
                                    }
                                    else
                                    {
                                        repDC.NEWFOOTER = "HUMAN"; //วาง footer แบบ คน
                                    }

                                    if (repDC.RETURN_PREMIUM != 0 && repDC.RETURN_PREMIUM != null)
                                    {
                                        repDC.RETURN_PREMIUM_THI = ThaiBaht(repDC.RETURN_PREMIUM.ToString());
                                    }

                                    if (repDC.REASON == "ล้มละลาย" && sourceFlg == "LET")
                                    {
                                        client.StampPrintFlag((long)repDC.ULETTER_ID, userid, null); 
                                        repDC.LETTER_DT = client.GetLetterDt(out prLt, (long?)repDC.ULETTER_ID); 
                                    }

                                    if (repDC != null)
                                    {
                                        if (repDC.STATUS == "IF")
                                        { 
                                            if (repDC.CHANNEL_TYPE == "PO")
                                            {
                                                firstParagraph = "อุบัติเหตุส่วนบุคคล";
                                            }
                                            else
                                            {
                                                firstParagraph = "ชีวิตคุ้มครองเครดิต";
                                            }
                                        }

                                        string SecondParagraph = "";
                                        if (repDC.STATUS == "CC")
                                        {
                                            if (repDC.CHANNEL_TYPE == "PO")
                                            {
                                                SecondParagraph = "อุบัติเหตุส่วนบุคคล";
                                            }
                                            else
                                            {
                                                SecondParagraph = "ภัย";
                                            }
                                        }
                                        else if (repDC.STATUS == "EX")
                                        {
                                            if (repDC.CHANNEL_TYPE == "PO")
                                            {
                                                SecondParagraph = "อุบัติเหตุส่วนบุคคล";
                                            }
                                            else
                                            {
                                                SecondParagraph = "";
                                            }
                                        }

                                        REPORT_DCBindingSource.DataSource = repDC;
                                        REPORT_DC_CollectionBindingSource.DataSource = repDC; 
                                        reportViewer1.LocalReport.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("paymentChannel", paymentChannel));
                                        reportViewer2.LocalReport.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("paymentChannel", paymentChannel));
                                        reportViewer3.LocalReport.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("paymentChannel", paymentChannel));
                                        reportViewer4.LocalReport.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("paymentChannel", paymentChannel));
                                        reportViewer5.LocalReport.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("paymentChannel", paymentChannel));
                                        reportViewer6.LocalReport.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("paymentChannel", paymentChannel));
                                        reportViewer7.LocalReport.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("paymentChannel", paymentChannel));
                                        reportViewer7.LocalReport.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("depType", deptype));
                                        reportViewer7.LocalReport.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("deptypeContinue", deptypeContinue));
                                         
                                        reportViewer1.LocalReport.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("callCenterPhoneNo", callCenterPhoneNo));
                                        reportViewer2.LocalReport.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("callCenterPhoneNo", callCenterPhoneNo));
                                        reportViewer3.LocalReport.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("callCenterPhoneNo", callCenterPhoneNo));
                                        reportViewer4.LocalReport.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("callCenterPhoneNo", callCenterPhoneNo));
                                        reportViewer5.LocalReport.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("callCenterPhoneNo", callCenterPhoneNo));
                                        reportViewer6.LocalReport.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("callCenterPhoneNo", callCenterPhoneNo));
                                        reportViewer7.LocalReport.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("callCenterPhoneNo", callCenterPhoneNo));

                                        reportViewer1.LocalReport.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("firstParagraph", firstParagraph));
                                        reportViewer2.LocalReport.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("firstParagraph", firstParagraph));
                                        reportViewer3.LocalReport.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("firstParagraph", firstParagraph));
                                        reportViewer4.LocalReport.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("firstParagraph", firstParagraph));
                                        reportViewer5.LocalReport.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("firstParagraph", firstParagraph));
                                        reportViewer6.LocalReport.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("firstParagraph", firstParagraph));

                                        reportViewer1.LocalReport.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("SecondParagraph", SecondParagraph));
                                        reportViewer2.LocalReport.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("SecondParagraph", SecondParagraph));
                                        reportViewer3.LocalReport.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("SecondParagraph", SecondParagraph));
                                        reportViewer4.LocalReport.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("SecondParagraph", SecondParagraph));
                                        reportViewer5.LocalReport.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("SecondParagraph", SecondParagraph));
                                        reportViewer6.LocalReport.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("SecondParagraph", SecondParagraph));
                                        //  reportViewer7.LocalReport.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("firstParagraph", firstParagraph));


                                        this.reportViewer1.RefreshReport();
                                        this.reportViewer2.RefreshReport();
                                        this.reportViewer3.RefreshReport();
                                        this.reportViewer4.RefreshReport();
                                        this.reportViewer5.RefreshReport();
                                        this.reportViewer6.RefreshReport();
                                        this.reportViewer7.RefreshReport();

                                        System.IO.FileInfo fi = new System.IO.FileInfo(reportFinalFile);
                                        if (fi.Exists)
                                        {
                                            fi.CopyTo(reportTempFile);
                                            genReportRejectPDF(reportRejectFile, repDC.STATUS, repDC.CHANNEL_TYPE);
                                            pdfArray.Clear();
                                            pdfArray.Add(reportTempFile);
                                            pdfArray.Add(reportRejectFile);
                                            MergeFiles(reportFinalFile, pdfArray.ToArray());
                                        }
                                        else
                                        {
                                            genReportRejectPDF(reportFinalFile, repDC.STATUS, repDC.CHANNEL_TYPE);
                                        }
                                    } 

                                }  //foreach
                            }
                        } //close pr.success == true
                        else
                        {
                            MessageBox.Show(pr.ErrorMessage);
                        }

                    } 
                }
                 catch (Exception ex)
                 {
                     MessageBox.Show(ex.Message);
                 }
                //finally
                // {
                //     this.Cursor = Cursors.Default;
                // }

                if (client.State != System.ServiceModel.CommunicationState.Closed)
                {
                    client.Close();
                }

            }
           // this.Cursor = Cursors.Default;
            //webBrowser1.Navigate(reportFinalFile);
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

        public static string ThaiBaht(string txt)
        {
            string bahtTxt, n, bahtTH = "";
            double amount;
            try { amount = Convert.ToDouble(txt); }
            catch { amount = 0; }
            bahtTxt = amount.ToString("####.00");
            string[] num = { "ศูนย์", "หนึ่ง", "สอง", "สาม", "สี่", "ห้า", "หก", "เจ็ด", "แปด", "เก้า", "สิบ" };
            string[] rank = { "", "สิบ", "ร้อย", "พัน", "หมื่น", "แสน", "ล้าน" };
            string[] temp = bahtTxt.Split('.');
            string intVal = temp[0];
            string decVal = temp[1];
            if (Convert.ToDouble(bahtTxt) == 0)
                bahtTH = "ศูนย์บาทถ้วน";
            else
            {
                for (int i = 0; i < intVal.Length; i++)
                {
                    n = intVal.Substring(i, 1);
                    if (n != "0")
                    {
                        if ((i == (intVal.Length - 1)) && (n == "1"))
                            bahtTH += "เอ็ด";
                        else if ((i == (intVal.Length - 2)) && (n == "2"))
                            bahtTH += "ยี่";
                        else if ((i == (intVal.Length - 2)) && (n == "1"))
                            bahtTH += "";
                        else
                            bahtTH += num[Convert.ToInt32(n)];
                        bahtTH += rank[(intVal.Length - i) - 1];
                    }
                }
                bahtTH += "บาท";
                if (decVal == "00")
                    bahtTH += "ถ้วน";
                else
                {
                    for (int i = 0; i < decVal.Length; i++)
                    {
                        n = decVal.Substring(i, 1);
                        if (n != "0")
                        {
                            if ((i == decVal.Length - 1) && (n == "1"))
                                bahtTH += "เอ็ด";
                            else if ((i == (decVal.Length - 2)) && (n == "2"))
                                bahtTH += "ยี่";
                            else if ((i == (decVal.Length - 2)) && (n == "1"))
                                bahtTH += "";
                            else
                                bahtTH += num[Convert.ToInt32(n)];
                            bahtTH += rank[(decVal.Length - i) - 1];
                        }
                    }
                    bahtTH += "สตางค์";
                }
            }
            return bahtTH;
        }
         
        private System.Drawing.Image CreateBarcode(string data)
        {
            Bitmap barCode = new Bitmap(1, 1);

            System.Drawing.Font threeOfNine = new System.Drawing.Font("Free 3 of 9", 60,
                                        System.Drawing.FontStyle.Regular,
                                        System.Drawing.GraphicsUnit.Point);

            Graphics graphics = Graphics.FromImage(barCode);

            SizeF dataSize = graphics.MeasureString(data, threeOfNine);

            barCode = new Bitmap(barCode, dataSize.ToSize());

            graphics = Graphics.FromImage(barCode);

            graphics.Clear(Color.White);

            graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SingleBitPerPixel;

            graphics.DrawString(data, threeOfNine,
                    new SolidBrush(Color.Black), 0, 0);

            graphics.Flush();

            threeOfNine.Dispose();
            graphics.Dispose();

            return (System.Drawing.Image)barCode;
        } 

        public byte[] imageToByteArray(System.Drawing.Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
            return ms.ToArray();
        }

        private void genReportRejectPDF(string _fileName, string rejectStatus, string channelType)
        {
            Warning[] warnings;
            string[] streamids;
            string mimeType, encoding, filenameExtension;
            byte[] bytes = null;

            switch (rejectStatus)
            {
                case "DC":
                    {
                        if (channelType == "KB")
                        {
                            bytes = reportViewer7.LocalReport.Render("PDF", null, out mimeType, out encoding, out filenameExtension, out streamids, out warnings);                       
                        }
                        else
                        {
                            bytes = reportViewer1.LocalReport.Render("PDF", null, out mimeType, out encoding, out filenameExtension, out streamids, out warnings);
                        }
                    }
                    break;
                case "EX":
                    bytes = reportViewer2.LocalReport.Render("PDF", null, out mimeType, out encoding, out filenameExtension, out streamids, out warnings);
                    break;
                case "CC":
                    bytes = reportViewer3.LocalReport.Render("PDF", null, out mimeType, out encoding, out filenameExtension, out streamids, out warnings);
                    break;
                case "PP":
                    bytes = reportViewer4.LocalReport.Render("PDF", null, out mimeType, out encoding, out filenameExtension, out streamids, out warnings);
                    break;
                case "NT":
                    bytes = reportViewer5.LocalReport.Render("PDF", null, out mimeType, out encoding, out filenameExtension, out streamids, out warnings);
                    break;
                case "IF":
                    bytes = reportViewer6.LocalReport.Render("PDF", null, out mimeType, out encoding, out filenameExtension, out streamids, out warnings);
                    break;
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
