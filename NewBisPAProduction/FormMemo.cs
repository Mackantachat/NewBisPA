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
    public partial class FormMemo : Form
    {

        private NewBisPASvcRef.U_MEMO_ID_Collection _U_MEMO_ID_Coll;

        public NewBisPASvcRef.U_MEMO_ID_Collection U_MEMO_ID_Coll
        {
            get { return _U_MEMO_ID_Coll; }
            set { _U_MEMO_ID_Coll = value; }
        }

        private NewBisPASvcRef.U_MEMO_ID U_MEMO_ID_OBJ = new NewBisPASvcRef.U_MEMO_ID();
        private NewBisPASvcRef.U_MEMO_DETAIL U_MEMO_DETAIL_OBJ = new NewBisPASvcRef.U_MEMO_DETAIL();


        private NewBisPASvcRef.AUTB_DATADIC_DET_COLLECTION _AUTB_DATADIC_DET_COLL;

        public NewBisPASvcRef.AUTB_DATADIC_DET_COLLECTION AUTB_DATADIC_DET_COLL
        {
            get { return _AUTB_DATADIC_DET_COLL; }
            set { _AUTB_DATADIC_DET_COLL = value; }
        }

        private String _APP_NO;

        public String APP_NO
        {
            get { return _APP_NO; }
            set { _APP_NO = value; }
        }

        private String _USER_ID;

        public String USER_ID
        {
            get { return _USER_ID; }
            set { _USER_ID = value; }
        }

        private NewBisPASvcRef.AUTB_PEND_REQM_COLLECTION _PAR_PEND_REQM_COLL;

        public NewBisPASvcRef.AUTB_PEND_REQM_COLLECTION PAR_PEND_REQM_COLL
        {
            get { return _PAR_PEND_REQM_COLL; }
            set { _PAR_PEND_REQM_COLL = value; }
        }

        private NewBisPASvcRef.AUTB_DOCUMENT_PEND_CODE_COLL _PAR_DOCUMENT_PEND_CODE_COLL;

        public NewBisPASvcRef.AUTB_DOCUMENT_PEND_CODE_COLL PAR_DOCUMENT_PEND_CODE_COLL
        {
            get { return _PAR_DOCUMENT_PEND_CODE_COLL; }
            set { _PAR_DOCUMENT_PEND_CODE_COLL = value; }
        }

        public FormMemo()
        {
            InitializeComponent();
        }


        private void FormMemo_Load(object sender, EventArgs e)
        {
            try
            {

                if (U_MEMO_ID_Coll != null && U_MEMO_ID_Coll.Count() > 0)
                {
                    while (panelMemo.Controls.Count > 0)
                    {
                        panelMemo.Controls.RemoveAt(0);
                    }

                    var GetData = from getData in U_MEMO_ID_Coll
                                  orderby getData.SEQ ascending
                                  select getData;

                    U_MEMO_ID_OBJ = GenericConverter<NewBisPASvcRef.U_MEMO_ID, NewBisPASvcRef.U_MEMO_ID>.Convert(GetData.First());
                    U_MEMO_ID_OBJ.SEQ = 1;

                    int i = 0;

                    foreach (NewBisPASvcRef.U_MEMO_ID obj in GetData)
                    {
                        UserControlsForm.MemoMainForm memoMainForm = new UserControlsForm.MemoMainForm();
                        memoMainForm.Name = "memoMainForm";
                        memoMainForm.Location = new Point(0, 0 + (i * 41));
                        memoMainForm.Size = new Size(820, 41);
                        memoMainForm.Tag = obj;

                        obj.SEQ = (i + 1);
                        memoMainForm.txtMoListNO.Text = obj.SEQ == null ? "" : obj.SEQ.Value.ToString();
                        memoMainForm.txtMoListTrnDate.Text = obj.MEMO_TRN_DT == null ? "" : Utility.dateTimeToString(obj.MEMO_TRN_DT.Value, "dd/MM/yyyy hh:mi:ss", "BU"); ;
                        memoMainForm.txtMoListLimitDate.Text = obj.ANSWER_LIMIT_DT == null ? "" : Utility.dateTimeToString(obj.ANSWER_LIMIT_DT.Value, "dd/MM/yyyy", "BU");
                        memoMainForm.txtMoListLetter.Text = "";

                        if (obj.U_LETTER_ID != null && obj.U_LETTER_ID.ULETTER_ID != null)
                        {
                            memoMainForm.txtMoListLetter.Text = obj.U_LETTER_ID.REFERENCE;
                            memoMainForm.txtMoListLetter.Tag = obj;
                            memoMainForm.txtMoListLetter.ForeColor = Color.Red;
                            memoMainForm.txtMoListLetter.Font = new Font("Tahoma", 10, FontStyle.Underline);
                            memoMainForm.txtMoListLetter.Cursor = Cursors.Hand;
                            ToolTip tt = new ToolTip();
                            tt.SetToolTip(memoMainForm.txtMoListLetter, "คลิกเพื่อดูข้อมูลจดหมาย");
                            memoMainForm.txtMoListLetter.Click += new EventHandler(txtMoListLetter_Click);
                        }

                        memoMainForm.txtMoListProcessEndDesc.Text = "";
                        var GetEndProcess = from getEndProcess in AUTB_DATADIC_DET_COLL
                                            where getEndProcess.COL_NAME == "END_PROCESS" && getEndProcess.CODE == obj.END_PROCESS.Value.ToString()
                                            select getEndProcess;
                        if (GetEndProcess != null && GetEndProcess.Count() > 0)
                        {
                            memoMainForm.txtMoListProcessEndDesc.Text = GetEndProcess.ToArray()[0].DESCRIPTION;
                        }

                        memoMainForm.txtMoListTmnDesc.Text = obj.TMN == 'N' ? "ยังมีผลใช้อยู่" : "ยกเลิกการใช้";                     

                        if (obj.SEQ == U_MEMO_ID_OBJ.SEQ)
                        {
                            memoMainForm.BackColor = Color.FromArgb(128, 128, 255);
                        }
                        else
                        {
                            memoMainForm.BackColor = Color.FromName("InactiveCaptionText");
                        }

                        memoMainForm.btnMoListCancel.Visible = false;
                        memoMainForm.btnMoListEdit.Visible = false;

                        memoMainForm.btnMoListDetail.Tag = obj;
                        memoMainForm.btnMoListDetail.Click += new EventHandler(btnMoListDetail_Click);

                        panelMemo.Controls.Add(memoMainForm);
                        i = i + 1;
                    }

                    if (U_MEMO_ID_OBJ.MEMO_DETAIL_Collection != null && U_MEMO_ID_OBJ.MEMO_DETAIL_Collection.Count() > 0)
                    {
                        DisplayOfPanelMemoDetail(U_MEMO_ID_OBJ.MEMO_DETAIL_Collection);
                    }
                }
                else
                {
                    panelMemo.Controls.Clear();
                    panelMemoDetail.Controls.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void btnMoListDetail_Click(object sender, EventArgs e)
        {
            try
            {
                Button btnMoListDetail = (Button)sender;
                NewBisPASvcRef.U_MEMO_ID uMemoIDObj = new NewBisPASvcRef.U_MEMO_ID();
                uMemoIDObj = (NewBisPASvcRef.U_MEMO_ID)btnMoListDetail.Tag;
                SetBackColorOfPanelMemoList(panelMemo, uMemoIDObj);
                DisplayOfPanelMemoDetail(uMemoIDObj.MEMO_DETAIL_Collection);
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show(ex.Message);
            }
        }

        void txtMoListLetter_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                TextBox txtMoListLetter = (TextBox)sender;
                NewBisPASvcRef.U_MEMO_ID uMemoIDObj = new NewBisPASvcRef.U_MEMO_ID();
                uMemoIDObj = (NewBisPASvcRef.U_MEMO_ID)txtMoListLetter.Tag;
                if (uMemoIDObj != null && uMemoIDObj.UMEMO_ID != null)
                {
                    if (uMemoIDObj.U_LETTER_ID != null && uMemoIDObj.U_LETTER_ID.ULETTER_ID != null)
                    {
                        if (!(string.IsNullOrEmpty(uMemoIDObj.U_LETTER_ID.REFERENCE)))
                        {
                            NewBISSvcRef.ProcessResult pr = new NewBISSvcRef.ProcessResult();
                            NewBISSvcRef.U_LETTER_ID uLetterIDObj = new NewBISSvcRef.U_LETTER_ID();
                            using (NewBISSvcRef.NewBISSvcClient client = new NewBISSvcRef.NewBISSvcClient())
                            {
                                uLetterIDObj = client.GetULetterIDByReference(out pr, uMemoIDObj.U_LETTER_ID.REFERENCE);
                                if (pr.Successed == false)
                                {
                                    throw new Exception(pr.ErrorMessage);
                                }
                            }

                            if (uLetterIDObj != null && uLetterIDObj.ULETTER_ID != null)
                            {
                                ReportForm.frmReportMOPDF appRep = new ReportForm.frmReportMOPDF(APP_NO, uLetterIDObj.ULETTER_ID.Value, USER_ID);
                                appRep.Show();
                            }
                        }
                        else
                        {
                            throw new Exception("ไม่พบข้อมูลจดหมาย");
                        }
                    }
                }

                U_MEMO_ID_OBJ = uMemoIDObj;
                SetBackColorOfPanelMemoList(panelMemo, U_MEMO_ID_OBJ);


                //PAR_U_MEMO_ID = (NewBisPASvcRef.U_MEMO_ID)txtMoListLetter.Tag;
                //ClearControlsOfMemoDetail();
                //DisplayOfPanelMemoDetail(PAR_U_MEMO_ID.MEMO_DETAIL_Collection);
                
                //gbMemoDetail.Enabled = true;
                //PAR_U_MEMO_DETAIL = new NewBisPASvcRef.U_MEMO_DETAIL();
                //SetBackColorOfPanelMemoDetailList(panelMemoDetailList, PAR_U_MEMO_DETAIL);

                
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show(ex.Message);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void SetBackColorOfPanelMemoList(Panel objectPanel, NewBisPASvcRef.U_MEMO_ID objDetail)
        {
            if (objectPanel.Controls.Count > 0)
            {
                foreach (Control c in objectPanel.Controls)
                {
                    if (c.Name == "memoMainForm")
                    {
                        UserControlsForm.MemoMainForm memoMainForm = (UserControlsForm.MemoMainForm)c;
                        NewBisPASvcRef.U_MEMO_ID uMemoIDObj = new NewBisPASvcRef.U_MEMO_ID();
                        uMemoIDObj = (NewBisPASvcRef.U_MEMO_ID)memoMainForm.Tag;
                        if (uMemoIDObj.SEQ == objDetail.SEQ)
                        {
                            memoMainForm.BackColor = Color.FromArgb(128, 128, 255);
                        }
                        else
                        {
                            memoMainForm.BackColor = Color.FromName("InactiveCaptionText");
                        }
                    }
                }
            }
        }

        private void DisplayOfPanelMemoDetail(NewBisPASvcRef.U_MEMO_DETAIL_Collection objectDetail)
        {
            if (objectDetail != null && objectDetail.Count() > 0)
            {
                while (panelMemoDetail.Controls.Count > 0)
                {
                    panelMemoDetail.Controls.RemoveAt(0);
                }

                var GetData = from getData in objectDetail
                              orderby getData.PRINT_SEQ ascending
                              select getData;

                U_MEMO_DETAIL_OBJ = GenericConverter<NewBisPASvcRef.U_MEMO_DETAIL, NewBisPASvcRef.U_MEMO_DETAIL>.Convert(GetData.First());

                if (GetData != null && GetData.Count() > 0)
                {
                    objectDetail = new NewBisPASvcRef.U_MEMO_DETAIL_Collection();
                    objectDetail.AddRange(GetData.ToArray());
                }
                int i = 0;
                foreach (NewBisPASvcRef.U_MEMO_DETAIL obj in objectDetail)
                {
                    UserControlsForm.MemoDetailForm memoDetailForm = new UserControlsForm.MemoDetailForm();
                    memoDetailForm.Name = "memoDetailForm";
                    memoDetailForm.Location = new Point(0, 0 + (i * 97));
                    memoDetailForm.Size = new Size(1045, 97);
                    memoDetailForm.Tag = obj;

                    memoDetailForm.txtMoDetailListNo.Text = obj.PRINT_SEQ == null ? "" : obj.PRINT_SEQ.Value.ToString();
                    memoDetailForm.txtMoDetailListPendCode.Text = obj.PEND_CODE;

                    memoDetailForm.txtMoDetailListPendCodeDesc.Text = "";
                    var GetPendCodeDesc = from getPendCodeDesc in PAR_PEND_REQM_COLL
                                          where getPendCodeDesc.PEND_CODE == obj.PEND_CODE
                                          select getPendCodeDesc;
                    if (GetPendCodeDesc != null && GetPendCodeDesc.Count() > 0)
                    {
                        memoDetailForm.txtMoDetailListPendCodeDesc.Text = GetPendCodeDesc.ToArray()[0].DESCRIPTION;
                    }

                    memoDetailForm.txtMoDetailListPendStatus.Text = "";
                    var GetPenStatus = from getPendStatus in AUTB_DATADIC_DET_COLL
                                       where getPendStatus.COL_NAME == "PEND_STATUS" && getPendStatus.CODE == obj.PEND_STATUS
                                       select getPendStatus;
                    if (GetPenStatus != null && GetPenStatus.Count() > 0)
                    {
                        memoDetailForm.txtMoDetailListPendStatus.Text = GetPenStatus.ToArray()[0].DESCRIPTION;
                    }

                    memoDetailForm.txtMoDetailListPendStatusDate.Text = obj.PEND_STATUS_DT == null ? "" : Utility.dateTimeToString(obj.PEND_STATUS_DT.Value, "dd/MM/yyyy", "BU");

                    if (obj.MEMO_DESCRIPTION != null && (!String.IsNullOrEmpty(obj.MEMO_DESCRIPTION.PEND_CODE)))
                    {
                        memoDetailForm.txtMoDetailListDesc.Text = obj.MEMO_DESCRIPTION.PEND_DESCRIPTION;
                    }

                    memoDetailForm.txtMoDetailListDocument.Text = "";
                    List<string> lLines = new List<string>();
                    if (obj.MEMO_DOCUMENT_Collection != null && obj.MEMO_DOCUMENT_Collection.Count() > 0)
                    {
                        int docNo = 0;
                        foreach (NewBisPASvcRef.U_MEMO_DOCUMENT moDocObj in obj.MEMO_DOCUMENT_Collection)
                        {

                            var GetDoc = from getDoc in PAR_DOCUMENT_PEND_CODE_COLL
                                         where getDoc.DOC_CODE == moDocObj.DOC_CODE
                                         select getDoc;
                            if (GetDoc != null && GetDoc.Count() > 0)
                            {
                                docNo = docNo + 1;
                                lLines.Add(docNo.ToString() + ". " + GetDoc.ToArray()[0].DESCRIPTION);
                            }
                        }
                    }
                    else
                    {
                        lLines = null;
                    }

                    if (lLines != null)
                    {
                        memoDetailForm.txtMoDetailListDocument.Lines = lLines.ToArray();
                    }

                    memoDetailForm.btnMoDetailListCancel.Visible = false;
                    memoDetailForm.btnMoDetailListEdit.Visible = false;

                    memoDetailForm.Tag = obj;
                    memoDetailForm.Click += new EventHandler(memoDetailForm_Click);

                    panelMemoDetail.Controls.Add(memoDetailForm);
                    i = i + 1;

                    if (U_MEMO_DETAIL_OBJ.PRINT_SEQ == obj.PRINT_SEQ)
                    {
                        memoDetailForm.BackColor = Color.FromArgb(128, 128, 255);
                    }
                    else
                    {
                        memoDetailForm.BackColor = Color.FromName("BlanchedAlmond");
                    }
                }
            }
            else
            {
                panelMemoDetail.Controls.Clear();
            }
        }

        void memoDetailForm_Click(object sender, EventArgs e)
        {
            try
            {
                UserControlsForm.MemoDetailForm memoDetailForm = (UserControlsForm.MemoDetailForm)sender;
                NewBisPASvcRef.U_MEMO_DETAIL uMemoDetailObj = new NewBisPASvcRef.U_MEMO_DETAIL();
                //uMemoDetailObj = (NewBisPASvcRef.U_MEMO_DETAIL)memoDetailForm.Tag;
                uMemoDetailObj = GenericConverter<NewBisPASvcRef.U_MEMO_DETAIL, NewBisPASvcRef.U_MEMO_DETAIL>.Convert((NewBisPASvcRef.U_MEMO_DETAIL)memoDetailForm.Tag);
                if (panelMemoDetail.Controls.Count > 0)
                {
                    foreach (Control c in panelMemoDetail.Controls)
                    {
                        if (c.Name == "memoDetailForm")
                        {
                            UserControlsForm.MemoDetailForm memoDetailForm1 = (UserControlsForm.MemoDetailForm)c;
                            NewBisPASvcRef.U_MEMO_DETAIL uMemoDetailObj1 = new NewBisPASvcRef.U_MEMO_DETAIL();
                            uMemoDetailObj1 = (NewBisPASvcRef.U_MEMO_DETAIL)memoDetailForm1.Tag;
                            if (uMemoDetailObj1.PRINT_SEQ == uMemoDetailObj.PRINT_SEQ)
                            {
                                memoDetailForm1.BackColor = Color.FromArgb(128, 128, 255);
                            }
                            else
                            {
                                memoDetailForm1.BackColor = Color.FromName("BlanchedAlmond");
                            }
                            
                        }
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
