using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Collections;
using ITUtility;
using System.Diagnostics;
using NewBisPA.Library;
using NewBisPA.Library.Resource;

namespace NewBisPA
{
    public partial class ApplicationForm : Form
    {
        private NewBisPASvcRef.PA_APPLICATION_ID PAR_PA_APPLICATION_ID;
        private NewBisPASvcRef.W_UNDERWRITE_ID_Collection PAR_W_UNDERWRITE_ID_COLL = new NewBisPASvcRef.W_UNDERWRITE_ID_Collection();
        private NewBisPASvcRef.W_UNDERWRITE_ID_Collection PAR_W_UNDERWRITE_ID_TMP_COLL = new NewBisPASvcRef.W_UNDERWRITE_ID_Collection();
        private NewBisPASvcRef.U_ADDRESS_ID[] PAR_U_ADDRESS_ID_COLL;
        private NewBisPASvcRef.U_SPOUSE_ID[] PAR_U_SPOUSE_ID_COLL;
        private NewBisPASvcRef.U_LIFE_ID PAR_PLAN_FREE = new NewBisPASvcRef.U_LIFE_ID();
        private NewBisPASvcRef.U_LIFE_ID PAR_PLAN_PAID = new NewBisPASvcRef.U_LIFE_ID();
        private NewBisPASvcRef.U_BENEFIT_ID_COLLECTION PAR_BENEFIT_ID_COLL = new NewBisPASvcRef.U_BENEFIT_ID_COLLECTION();
        private NewBisPASvcRef.U_BENEFIT_ID_COLLECTION PAR_BENEFIT_ID_COLL_TMP = new NewBisPASvcRef.U_BENEFIT_ID_COLLECTION();
        private NewBisPASvcRef.U_BENEFIT_ID PAR_BENEFIT_ID_OBJ = new NewBisPASvcRef.U_BENEFIT_ID();
        public bool IsAutoCheckOldAddressData = true;
        public NewBisPASvcRef.AUTB_DATADIC_DET_COLLECTION PaymentDescColl { get; set; }

        private NewBisPASvcRef.P_NAME_ID_COLLECTION PAR_P_NAME_ID_COLL = new NewBisPASvcRef.P_NAME_ID_COLLECTION();
        private NewBisPASvcRef.P_PARENT_ID_COLLECTION PAR_P_PARENT_ID_COLL = new NewBisPASvcRef.P_PARENT_ID_COLLECTION();


        private List<NewBisPASvcRef.U_ACCOUNT_ID> uAccountIDColl { get; set; }
        private NewBisPASvcRef.P_NAME_ID_COLLECTION PAR_PARENT_P_NAME_ID_COLL = new NewBisPASvcRef.P_NAME_ID_COLLECTION();
        private NewBisPASvcRef.P_PARENT_ID_COLLECTION PAR_PARENT_P_PARENT_ID_COLL = new NewBisPASvcRef.P_PARENT_ID_COLLECTION();



        //เก็บค่าตัวแปร U_APP_REMARK =====================================================
        private NewBisPASvcRef.U_APP_REMARK_Collection U_APP_REMARK_COLL { get; set; }


        private NewBisPASvcRef.P_ADDRESS_ID_COLLECTION PAR_ADDRESS_ID_COLL = new NewBisPASvcRef.P_ADDRESS_ID_COLLECTION();
        private NewBisPASvcRef.W_SUMMARY PAR_W_SUMMARY = new NewBisPASvcRef.W_SUMMARY();
        private NewBisPASvcRef.U_MEMO_ID_Collection PAR_U_MEMO_ID_COLL_TMP = new NewBisPASvcRef.U_MEMO_ID_Collection();
        private NewBisPASvcRef.U_MEMO_ID PAR_U_MEMO_ID = new NewBisPASvcRef.U_MEMO_ID();
        private NewBisPASvcRef.U_MEMO_DETAIL PAR_U_MEMO_DETAIL = new NewBisPASvcRef.U_MEMO_DETAIL();
        private NewBisPASvcRef.AUTB_APPNAME_ID PAR_AUTB_APPNAME_ID = new NewBisPASvcRef.AUTB_APPNAME_ID();
        private NewBisPASvcRef.U_OADDRESS PAR_U_OADDRESS = new NewBisPASvcRef.U_OADDRESS();
        private NewBisPASvcRef.U_ADDRESS_ID PAR_U_ADDRESS_ID_OLD = new NewBisPASvcRef.U_ADDRESS_ID();
        private NewBisPASvcRef.U_APPLICATION_NAME PAR_U_APPLICATION_NAME = new NewBisPASvcRef.U_APPLICATION_NAME();
        private NewBisPASvcRef.QUESTION_DATA_COLL questionColl = new NewBisPASvcRef.QUESTION_DATA_COLL();
        private NewBisPASvcRef.U_ACCOUNT_ID[] PAR_U_ACCOUNT_ID_COLL;
        private NewBisPASvcRef.U_APPLICATION_LOCK PAR_U_APPLICATION_LOCK = new NewBisPASvcRef.U_APPLICATION_LOCK();
        private NewBisPASvcRef.CHECK_DATA_CUSTOMER PAR_CHECK_DATA_CUSTOMER = new NewBisPASvcRef.CHECK_DATA_CUSTOMER();
        private NewBisPASvcRef.W_SUMMARY_DETAIL_Collection PAR_W_SUMMARY_DETAIL_COLL = new NewBisPASvcRef.W_SUMMARY_DETAIL_Collection();
        private NewBisPASvcRef.W_SUMMARY_DETAIL PAR_W_SUMMARY_DETAIL = new NewBisPASvcRef.W_SUMMARY_DETAIL();
        private NewBisPASvcRef.U_CAL_ERROR PAR_U_CAL_ERROR = new NewBisPASvcRef.U_CAL_ERROR();
        private NewBisPASvcRef.CHECK_DATA_CUSTOMER PAR_CHECK_DATA_OTHER = new NewBisPASvcRef.CHECK_DATA_CUSTOMER();

        private NewBisPASvcRef.P_POS_ID pPosID = new NewBisPASvcRef.P_POS_ID();
        private NewBisPASvcRef.U_LETTER_ID ULetterIDObj = new NewBisPASvcRef.U_LETTER_ID();
        private long? PK_SMART_ID { get; set; }
        private long? PK_SMART_ACCOUNT_ID { get; set; }
        public long? PK_UAP_ID { get; set; }

        public DateTime? SuspenseePayDate { get; set; }
        private DateTime? UAP_TRN_DT { get; set; }

        public ApplicationForm()
        {
            InitializeComponent();






            //            ReceiptSvcRef.ServiceClient repClient = new ReceiptSvcRef.ServiceClient();



            //            repClient.GenerateReceipt("2C08EB64430C47DEE0540021284F4F36"
            //                                    , "PN"
            //                                    , "PN0000000055",
            //                                    "",
            //                                     ITUtility.Utility.StringToDateTime("04/02/2559", "BU"),
            //                                     402888, "003062");

            //            repClient.GenerateReceipt("2C08EB64431E47DEE0540021284F4F36"
            //                        , "PN"
            //                        , "PN0000000056",
            //                        "",
            //                         ITUtility.Utility.StringToDateTime("05/02/2559", "BU"),
            //                         402896, "003062");

            //            repClient.GenerateReceipt("2C08EB64430E47DEE0540021284F4F36"
            //                , "PN"
            //                , "PN0000000057",
            //                "",
            //                 ITUtility.Utility.StringToDateTime("05/02/2559", "BU"),
            //                 402889, "003062");


            //            repClient.GenerateReceipt("2B032422216A0219E0540021284F4F36"
            //       , "PN"
            //       , "PO0000000051",
            //       "",
            //        ITUtility.Utility.StringToDateTime("12/02/2559", "BU"),
            //        402923, "003062");


            //            repClient.GenerateReceipt("2B032422216B0219E0540021284F4F36"
            //, "PN"
            //, "PO0000000052",
            //"",
            //ITUtility.Utility.StringToDateTime("11/02/2559", "BU"),
            //402925, "003062");

            //            repClient.GenerateReceipt("2C08EB64430A47DEE0540021284F4F36"
            //, "PN"
            //, "PN0000000061",
            //"",
            //ITUtility.Utility.StringToDateTime("5/02/2559", "BU"),
            //402887, "003062");












        }
        public ApplicationForm(String userID)
        {
            InitializeComponent();
            UserID = userID;
        }

        private List<Form> childForm;

        public List<Form> ChildForm
        {
            get { return childForm; }
            set { childForm = value; }
        }

        private FormApplicationSelector _ApplicationSelectorForm;

        public FormApplicationSelector ApplicationSelectorForm
        {
            get { return _ApplicationSelectorForm; }
            set { _ApplicationSelectorForm = value; }
        }

        private FormApplicationViewer _ApplicationViewerForm;

        public FormApplicationViewer ApplicationViewerForm
        {
            get { return _ApplicationViewerForm; }
            set { _ApplicationViewerForm = value; }
        }

        private FormCustomerData _formCustomerData;

        public FormCustomerData formCustomerData
        {
            get { return _formCustomerData; }
            set { _formCustomerData = value; }
        }

        private string _FORMAPP;

        public string FORMAPP
        {
            get { return _FORMAPP; }
            set { _FORMAPP = value; }
        }

        public bool IsChannelTypeFreePolicy(string channelType)
        {

            if (channelType == "PF")
            {
                return true;
            }
            return false;


        }

        private void ApplicationForm_Load(object sender, EventArgs e)
        {

            String chkSystem = Utility.AppSettings("chkSystem");
            if (chkSystem == "UAT")
            {
                this.Text = "NewBIS : Personal Accident Insurance System(รุ่นทดสอบ)";
                row1panel.BackColor = Color.FromArgb(255, 224, 192);
            }
            else
            {
                this.Text = "NewBIS : Personal Accident Insurance System";
                row1panel.BackColor = System.Drawing.SystemColors.Control;
            }

            getRegistryValue();
            childForm = new List<Form>();
            btnHeadSave.Enabled = false;
            btnScan.Enabled = false;
            btnCustomerData.Enabled = false;
            PAR_PA_APPLICATION_ID = new NewBisPASvcRef.PA_APPLICATION_ID();
            PAR_PLAN_FREE = new NewBisPASvcRef.U_LIFE_ID();
            PAR_PLAN_PAID = new NewBisPASvcRef.U_LIFE_ID();
            tabMain.SelectTab("tabCustomerData");
            panelBenefitList.Controls.Clear();
            CreateBaseControlsDat();
            DisplayPolicyHolding();

        }

        private void DisplayPolicyHolding()
        {
            var channelType = cmbChannelType.SelectedValue.ToString();
            if (!string.IsNullOrEmpty(channelType))
            {
                if (channelType == "PD")
                {
                    txtPolicyHolding.Visible = true;
                    txtPolicyHolding.AutoCompleteCustomSource = null;
                    if (PAR_ZTB_PM_POLICY_COLL != null && PAR_ZTB_PM_POLICY_COLL.Count() > 0)
                    {
                        var GetData = from getData in PAR_ZTB_PM_POLICY_COLL
                                      where getData.PL_BLOCK == 'Q'
                                      orderby getData.POLICY_HOLDING ascending
                                      select getData;
                        if (GetData != null && GetData.Count() > 0)
                        {
                            AutoCompleteStringCollection data = new AutoCompleteStringCollection();

                            foreach (NewBisPASvcRef.ZTB_PM_POLICY item in GetData)
                            {
                                data.Add(item.POLICY_HOLDING);
                            }
                            txtPolicyHolding.AutoCompleteMode = AutoCompleteMode.Suggest;
                            txtPolicyHolding.AutoCompleteSource = AutoCompleteSource.CustomSource;
                            txtPolicyHolding.AutoCompleteCustomSource = data;
                        }

                    }
                }
                else if (channelType == "PF")
                {
                    txtPolicyHolding.Visible = true;
                    txtPolicyHolding.AutoCompleteCustomSource = null;
                    if (PAR_ZTB_PM_POLICY_COLL != null && PAR_ZTB_PM_POLICY_COLL.Count() > 0)
                    {
                        var GetData = from getData in PAR_ZTB_PM_POLICY_COLL
                                      where getData.PL_BLOCK == 'F'
                                      orderby getData.POLICY_HOLDING ascending
                                      select getData;
                        if (GetData != null && GetData.Count() > 0)
                        {
                            AutoCompleteStringCollection data = new AutoCompleteStringCollection();

                            foreach (NewBisPASvcRef.ZTB_PM_POLICY item in GetData)
                            {
                                data.Add(item.POLICY_HOLDING);
                            }
                            txtPolicyHolding.AutoCompleteMode = AutoCompleteMode.Suggest;
                            txtPolicyHolding.AutoCompleteSource = AutoCompleteSource.CustomSource;
                            txtPolicyHolding.AutoCompleteCustomSource = data;
                        }

                    }
                }
                else if (cmbChannelType.SelectedValue.ToString() == "KB")
                {
                    txtPolicyHolding.Visible = true;
                    txtPolicyHolding.AutoCompleteCustomSource = null;
                    if (PAR_ZTB_PM_POLICY_COLL != null && PAR_ZTB_PM_POLICY_COLL.Count() > 0)
                    {
                        var GetData = from getData in PAR_ZTB_PM_POLICY_COLL
                                      where getData.PL_BLOCK == 'K'
                                      orderby getData.POLICY_HOLDING ascending
                                      select getData;
                        if (GetData != null && GetData.Count() > 0)
                        {
                            AutoCompleteStringCollection data = new AutoCompleteStringCollection();

                            foreach (NewBisPASvcRef.ZTB_PM_POLICY item in GetData)
                            {
                                data.Add(item.POLICY_HOLDING);
                            }
                            txtPolicyHolding.AutoCompleteMode = AutoCompleteMode.Suggest;
                            txtPolicyHolding.AutoCompleteSource = AutoCompleteSource.CustomSource;
                            txtPolicyHolding.AutoCompleteCustomSource = data;
                        }

                    }
                }
                else
                {
                    txtPolicyHolding.Visible = false;
                }
            }
        }

        private void cmbChannelType_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                txtPolicyHolding.Text = "";
                if (cmbChannelType.SelectedValue != null)
                {
                    if (cmbChannelType.SelectedValue.ToString() == "")
                    {
                        NewBisPASvcRef.AUTB_CHANNEL_BLOCK_COLLECTION channelBlocks = new NewBisPASvcRef.AUTB_CHANNEL_BLOCK_COLLECTION();
                        NewBisPASvcRef.AUTB_CHANNEL_BLOCK channelBlockObj = new NewBisPASvcRef.AUTB_CHANNEL_BLOCK();
                        channelBlockObj.WORK_GROUP = "";
                        channelBlockObj.PL_BLOCK = "";
                        channelBlockObj.CHANNEL_TYPE = "";
                        channelBlockObj.WORK_GROUP_NAME = "กรุณาระบุกลุ่มงานที่ต้องการ";
                        channelBlocks.Add(channelBlockObj);
                        cmbWorkGroup.DataSource = channelBlocks;
                        cmbWorkGroup.DisplayMember = "WORK_GROUP_NAME";
                        cmbWorkGroup.ValueMember = "WORK_GROUP";
                        cmbWorkGroup.SelectedValue = "";

                        NewBisPASvcRef.AUTB_APPNAME_ID_COLLECTION appNameIDs = new NewBisPASvcRef.AUTB_APPNAME_ID_COLLECTION();
                        NewBisPASvcRef.AUTB_APPNAME_ID appNameIDObj = new NewBisPASvcRef.AUTB_APPNAME_ID();
                        appNameIDObj.APPNAME_ID = 0;
                        appNameIDObj.TITLE = "กรุณาระบุเอกสารชุดใบคำขอที่ต้องการ";
                        appNameIDObj.CHANNEL_TYPE = "";
                        appNameIDObj.WORK_GROUP = "";
                        appNameIDs.Add(appNameIDObj);
                        cmbAppNameID.DataSource = appNameIDs;
                        cmbAppNameID.DisplayMember = "TITLE";
                        cmbAppNameID.ValueMember = "APPNAME_ID";
                    }
                    else
                    {
                        SetControlByChannelType(cmbChannelType.SelectedValue.ToString());
                        if (PAR_AUTB_CHANNEL_BLOCK_COLLECTION != null && PAR_AUTB_CHANNEL_BLOCK_COLLECTION.Count() > 0)
                        {
                            var WorkGroupLinq = from workGroupLinq in PAR_AUTB_CHANNEL_BLOCK_COLLECTION
                                                where workGroupLinq.CHANNEL_TYPE == cmbChannelType.SelectedValue.ToString()
                                                select workGroupLinq;
                            if (WorkGroupLinq != null && WorkGroupLinq.Count() > 0)
                            {

                                NewBisPASvcRef.AUTB_CHANNEL_BLOCK_COLLECTION channelBlocks = new NewBisPASvcRef.AUTB_CHANNEL_BLOCK_COLLECTION();
                                NewBisPASvcRef.AUTB_CHANNEL_BLOCK channelBlockObj = new NewBisPASvcRef.AUTB_CHANNEL_BLOCK();
                                channelBlockObj.WORK_GROUP = "";
                                channelBlockObj.PL_BLOCK = "";
                                channelBlockObj.CHANNEL_TYPE = "";
                                channelBlockObj.WORK_GROUP_NAME = "กรุณาระบุกลุ่มงานที่ต้องการ";
                                channelBlocks.Add(channelBlockObj);
                                channelBlocks.AddRange(WorkGroupLinq.ToArray());
                                cmbWorkGroup.DataSource = channelBlocks;
                                cmbWorkGroup.DisplayMember = "WORK_GROUP_NAME";
                                cmbWorkGroup.ValueMember = "WORK_GROUP";
                                cmbWorkGroup.SelectedValue = WorkGroupLinq.ToArray()[0].WORK_GROUP;

                                NewBisPASvcRef.AUTB_CHANNEL_BLOCK_COLLECTION plBlockS = new NewBisPASvcRef.AUTB_CHANNEL_BLOCK_COLLECTION();
                                plBlockS.AddRange(WorkGroupLinq.ToArray());
                                cmbFreePlBlock.DataSource = plBlockS;
                                cmbFreePlBlock.DisplayMember = "PL_BLOCK";
                                cmbFreePlBlock.ValueMember = "PL_BLOCK";
                                cmbFreePlBlock.SelectedIndex = 0;

                                NewBisPASvcRef.AUTB_CHANNEL_BLOCK[] paidPlBlockS = new NewBisPASvcRef.AUTB_CHANNEL_BLOCK[plBlockS.Count()];
                                plBlockS.CopyTo(paidPlBlockS);
                                cmbPaidPlBlock.DataSource = plBlockS;
                                cmbPaidPlBlock.DisplayMember = "PL_BLOCK";
                                cmbPaidPlBlock.ValueMember = "PL_BLOCK";
                                cmbPaidPlBlock.SelectedIndex = 0;
                            }
                        }

                        if (PAR_AUTB_APPNAME_ID_COLLECTION != null && PAR_AUTB_APPNAME_ID_COLLECTION.Count() > 0)
                        {
                            var GetAppName = from getAppName in PAR_AUTB_APPNAME_ID_COLLECTION
                                             where getAppName.CHANNEL_TYPE == cmbChannelType.SelectedValue.ToString()
                                             select getAppName;
                            if (GetAppName != null && GetAppName.Count() > 0)
                            {
                                NewBisPASvcRef.AUTB_APPNAME_ID_COLLECTION appNameIDs = new NewBisPASvcRef.AUTB_APPNAME_ID_COLLECTION();
                                NewBisPASvcRef.AUTB_APPNAME_ID appNameIDObj = new NewBisPASvcRef.AUTB_APPNAME_ID();
                                appNameIDObj.APPNAME_ID = 0;
                                appNameIDObj.TITLE = "กรุณาระบุเอกสารชุดใบคำขอที่ต้องการ";
                                appNameIDObj.CHANNEL_TYPE = "";
                                appNameIDObj.WORK_GROUP = "";
                                appNameIDs.Add(appNameIDObj);
                                appNameIDs.AddRange(GetAppName.ToArray());
                                cmbAppNameID.DataSource = appNameIDs;
                                cmbAppNameID.DisplayMember = "TITLE";
                                cmbAppNameID.ValueMember = "APPNAME_ID";
                                cmbAppNameID.SelectedValue = GetAppName.ToArray()[0].APPNAME_ID;
                            }
                            else
                            {
                                NewBisPASvcRef.AUTB_APPNAME_ID_COLLECTION appNameIDs = new NewBisPASvcRef.AUTB_APPNAME_ID_COLLECTION();
                                NewBisPASvcRef.AUTB_APPNAME_ID appNameIDObj = new NewBisPASvcRef.AUTB_APPNAME_ID();
                                appNameIDObj.APPNAME_ID = 0;
                                appNameIDObj.TITLE = "กรุณาระบุเอกสารชุดใบคำขอที่ต้องการ";
                                appNameIDObj.CHANNEL_TYPE = "";
                                appNameIDObj.WORK_GROUP = "";
                                appNameIDs.Add(appNameIDObj);
                                cmbAppNameID.DataSource = appNameIDs;
                                cmbAppNameID.DisplayMember = "TITLE";
                                cmbAppNameID.ValueMember = "APPNAME_ID";
                            }
                        }
                    }


                    DisplayPolicyHolding();

                }

                if (cmbChannelType.SelectedValue.ToString() == "KB")
                {
                    txtPaidPremium.ReadOnly = false;
                }
                else
                {
                    txtPaidPremium.ReadOnly = true;
                }
                if (cmbChannelType.SelectedValue.ToString() == "PF")
                {
                    lblEpolicyDocType.Visible = lblEpolicySendType.Visible = true;
                    cmbEPolicySending.Visible = cmbPolicyDocmentType.Visible = true;
                    gbPlanPaid.Enabled = false;
                    ckbLifeFree.Checked = true;

                    txtAppOfc.Text = "สนญ";
                    txtAppDt.Text = Utility.dateTimeToString(DateTime.Now, "dd/MM/yyyy", "BU");
                    cmbSendTo.SelectedValue = "C";
                    cmbDocumentFlag.SelectedValue = "Y";
                    cmbUnderWriteFlag.SelectedValue = "C";

                }
                else
                {
                    lblEpolicyDocType.Visible = lblEpolicySendType.Visible = false;
                    cmbEPolicySending.Visible = cmbPolicyDocmentType.Visible = false;
                    cmbEPolicySending.SelectedValue = cmbPolicyDocmentType.SelectedValue = "";
                    cmbPolicyDocmentType.SelectedValue = "";
                    cmbEPolicySending.SelectedValue = "";
                }
            }
            catch (Exception ex)
            {
                SetMsgException(ex);
            }
        }

        private void cmbWorkGroup_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                if (cmbWorkGroup.SelectedValue != null)
                {

                }
            }
            catch (Exception ex)
            {
                SetMsgException(ex);
            }
        }

        private void btnHeadClear_Click(object sender, EventArgs e)
        {
            try
            {
                //ปลดล็อคของ U_APPLICATION_LOCK ===============================================================

                if (APP_LOCK_ERROR == false && PAR_END_PROCESS == 'N')
                {
                    if (PAR_PA_APPLICATION_ID != null && PAR_PA_APPLICATION_ID.APP_ID != null)
                    {
                        NewBisPASvcRef.ProcessResult pr = new NewBisPASvcRef.ProcessResult();
                        using (NewBisPASvcRef.NewBisPASvcClient client = new NewBisPASvcRef.NewBisPASvcClient())
                        {
                            NewBisPASvcRef.U_APPLICATION_LOCK uApplicationLockObj = new NewBisPASvcRef.U_APPLICATION_LOCK();
                            pr = client.GetUapplicationLockByAppID(out uApplicationLockObj, PAR_PA_APPLICATION_ID.APP_ID);
                            if (pr.Successed == false)
                            {
                                throw new Exception(pr.ErrorMessage);
                            }

                            uApplicationLockObj.TRANSACTION_FLG = 'E';
                            pr = client.AdjustUApplicationLock(ref uApplicationLockObj, "UPDATE");
                            if (pr.Successed == false)
                            {
                                // throw new Exception(pr.ErrorMessage);
                            }
                        }
                    }
                }

                //END ปลดล็อคของ U_APPLICATION_LOCK ===========================================================

                if (childForm != null)
                {
                    foreach (Form item in childForm)
                    {
                        item.Close();
                    }
                }

                txtCertNo.ReadOnly = false;
                txtAppNo.Text = "";
                txtAppNo.ReadOnly = false;
                txtAppNo.Text = "";
                txtAppNo.Enabled = true;

                txtCertNo.Enabled = true;
                txtCertNo.ReadOnly = false;


                PK_UAP_ID = null;
                UAP_TRN_DT = null;

                ApplicationBeginParameter();



            }
            catch (Exception ex)
            {
                SetMsgException(ex);
            }
        }

        private void txtAppNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                try
                {
                    if (!(string.IsNullOrEmpty(FORMAPP)))
                    {
                        return;
                    }

                    if (txtAppNo.ReadOnly == true)
                    {
                        return;
                    }

                    this.Cursor = Cursors.WaitCursor;

                    PAR_PA_APPLICATION_ID = new NewBisPASvcRef.PA_APPLICATION_ID();

                    String ChannelType = cmbChannelType.SelectedValue == null ? "" : cmbChannelType.SelectedValue.ToString();
                    String WorkGroup = cmbWorkGroup.SelectedValue == null ? "" : cmbWorkGroup.SelectedValue.ToString();

                    if (txtAppNo.Text == "")
                    {
                        txtAppNo.Focus();
                        throw new Exception("กรุณาระบุเลขที่ใบคำขอ");
                    }
                    else if (ChannelType == "")
                    {
                        cmbChannelType.Focus();
                        throw new Exception("กรุณาระบุช่องทางการขายที่ท่านต้องการ");
                    }
                    else
                    {
                        //เคลียร์ค่าต่างๆในโปรแกรมก่อน=======================
                        String appNo = txtAppNo.Text.ToUpper();
                        txtAppNo.Text = appNo;
                        txtCertNo.Text = "";
                        ApplicationBeginParameter();
                        //END เคลียร์ค่าต่างๆในโปรแกรมก่อน===================

                        //ค้นหาข้อมูล ==================================
                        cmbChannelType.Enabled = false;
                        cmbWorkGroup.Enabled = false;
                        cmbAppNameID.Enabled = false;
                        txtAppNo.Enabled = false;
                        txtAppNo.ReadOnly = true;
                        btnHeadSave.Enabled = true;
                        btnScan.Enabled = true;
                        btnCustomerData.Enabled = true;
                        SearchData();
                        //END ค้นหาข้อมูล ==============================
                    }
                    //ตรวจสอบและทำการ LOCK เลขใบคำขอหนี้เพื่อไม่ให้คนอื่นเข้ามาทำงานซ้อน ===============================
                    APP_LOCK_ERROR = false;
                    if (PAR_END_PROCESS == 'N')
                    {
                        if (PAR_PA_APPLICATION_ID != null && PAR_PA_APPLICATION_ID.APP_ID != null)
                        {
                            if (PAR_U_APPLICATION_LOCK != null && PAR_U_APPLICATION_LOCK.UAPP_ID != null)
                            {
                                if (PAR_U_APPLICATION_LOCK.TRANSACTION_FLG == 'E')
                                {
                                    NewBisPASvcRef.U_APPLICATION_LOCK uApplicationLockObj = new NewBisPASvcRef.U_APPLICATION_LOCK();
                                    uApplicationLockObj.UAPP_ID = PAR_PA_APPLICATION_ID.APPLICATION_Collection[0].UAPP_ID;
                                    uApplicationLockObj.FSYSTEM_DT = DateTime.Now;
                                    uApplicationLockObj.UPD_ID = UserID;
                                    uApplicationLockObj.TRANSACTION_FLG = 'U';
                                    uApplicationLockObj.UPDATE_FLG = 'N';
                                    uApplicationLockObj.UPDATE_DT = null;

                                    NewBisPASvcRef.ProcessResult pr = new NewBisPASvcRef.ProcessResult();
                                    using (NewBisPASvcRef.NewBisPASvcClient client = new NewBisPASvcRef.NewBisPASvcClient())
                                    {
                                        pr = client.AdjustUApplicationLock(ref uApplicationLockObj, "INSERT");
                                        if (pr.Successed == false)
                                        {
                                            btnHeadSave.Visible = false;
                                            throw new Exception(pr.ErrorMessage);
                                        }
                                    }
                                }
                                else
                                {
                                    APP_LOCK_ERROR = true;
                                    btnHeadSave.Visible = false;
                                    throw new Exception("เลที่ใบคำขอ " + txtAppNo.Text + " กำลังพิจารณาโดย รหัสพนักงาน " + PAR_U_APPLICATION_LOCK.UPD_ID);
                                }
                            }
                            else
                            {
                                NewBisPASvcRef.U_APPLICATION_LOCK uApplicationLockObj = new NewBisPASvcRef.U_APPLICATION_LOCK();
                                uApplicationLockObj.UAPP_ID = PAR_PA_APPLICATION_ID.APPLICATION_Collection[0].UAPP_ID;
                                uApplicationLockObj.FSYSTEM_DT = DateTime.Now;
                                uApplicationLockObj.UPD_ID = UserID;
                                uApplicationLockObj.TRANSACTION_FLG = 'U';
                                uApplicationLockObj.UPDATE_FLG = 'N';
                                uApplicationLockObj.UPDATE_DT = null;

                                NewBisPASvcRef.ProcessResult pr = new NewBisPASvcRef.ProcessResult();
                                using (NewBisPASvcRef.NewBisPASvcClient client = new NewBisPASvcRef.NewBisPASvcClient())
                                {
                                    pr = client.AdjustUApplicationLock(ref uApplicationLockObj, "INSERT");
                                    if (pr.Successed == false)
                                    {
                                        APP_LOCK_ERROR = true;
                                        btnHeadSave.Visible = false;
                                        throw new Exception(pr.ErrorMessage);
                                    }
                                }
                            }
                        }
                    }
                    //END ตรวจสอบและทำการ LOCK เลขใบคำขอหนี้เพื่อไม่ให้คนอื่นเข้ามาทำงานซ้อน ===========================


                    //ดึงข้อมูลเงินเกิน 







                }
                catch (Exception ex)
                {
                    //  btnHeadSave.Enabled = false;
                    SetMsgException(ex);
                }
                finally
                {
                    this.Cursor = Cursors.Default;
                }
            }
        }


        private void InitUSmart()
        {
            if (PAR_U_ACCOUNT_ID_COLL != null && PAR_U_ACCOUNT_ID_COLL.Any())
            {
                var selAccSmart =
                    from accSmart in PAR_U_ACCOUNT_ID_COLL
                    where accSmart.SMART_ID != null && accSmart.SMART_ID.USMART_ID != null
                    select accSmart;
                if (selAccSmart != null && selAccSmart.Count() > 0)
                {
                    var accountSmart = selAccSmart.First();
                    PK_SMART_ACCOUNT_ID = accountSmart.UACCOUNT_ID;
                    txtSmartBankCode.Text = accountSmart.ACC_BANK;
                    txtSmartBranchCode.Text = accountSmart.ACC_BRANCH;

                    NewBISSvcRef.ProcessResult pr = new NewBISSvcRef.ProcessResult();

                    using (NewBISSvcRef.NewBISSvcClient client = new NewBISSvcRef.NewBISSvcClient())
                    {
                        txtSmartBankName.Text = client.GetBankName(out pr, txtSmartBankCode.Text);
                        if (pr.Successed == false)
                        {
                            throw new Exception(pr.ErrorMessage);
                        }
                        txtSmartBranchName.Text = client.GetBranchName(out pr, txtSmartBankCode.Text, txtSmartBranchCode.Text);
                        if (pr.Successed == false)
                        {
                            throw new Exception(pr.ErrorMessage);
                        }

                    }

                    txtSmartAccName.Text = accountSmart.ACC_NAME;
                    txtSmartAccNo.Text = accountSmart.ACC_NO;
                    cmbSmartDepositType.SelectedValue = accountSmart.ACC_DEPOSIT_TYPE == null ? "" : accountSmart.ACC_DEPOSIT_TYPE;

                    chbSmartIDTmn.Checked = accountSmart.SMART_ID.TMN == 'Y' ? true : false;
                    chbSmartIDApproved.Checked = accountSmart.SMART_ID.APPROVE_FLG == 'Y' ? true : false;

                    if (chbSmartIDTmn.Checked == true)
                    {
                        cmbSmartRemark.Visible = true;
                    }
                    else
                    {
                        cmbSmartRemark.Visible = false;
                    }

                    cmbSmartRemark.SelectedValue = accountSmart.SMART_ID.FOLLOW_SMART_CODE == null ? "" : accountSmart.SMART_ID.FOLLOW_SMART_CODE;

                    if (accountSmart.ACCOUNT_PERSONAL != null)
                    {
                        if ((accountSmart.ACCOUNT_PERSONAL.IDCARD_NO != null) && (accountSmart.ACCOUNT_PERSONAL.IDCARD_NO == txtIdcardNo.Text.Trim()))
                        {
                            cmbSmartAccOwner.SelectedValue = "C";
                        }
                        if ((accountSmart.ACCOUNT_PERSONAL.IDCARD_NO != null) && (accountSmart.ACCOUNT_PERSONAL.IDCARD_NO == txtParentIdcardNo.Text.Trim()))
                        {
                            cmbSmartAccOwner.SelectedValue = "P";
                        }
                        if ((accountSmart.ACCOUNT_PERSONAL.PASSPORT != null) && (accountSmart.ACCOUNT_PERSONAL.PASSPORT == txtIdcardNo.Text.Trim()))
                        {
                            cmbSmartAccOwner.SelectedValue = "C";
                        }
                        if ((accountSmart.ACCOUNT_PERSONAL.PASSPORT != null) && (accountSmart.ACCOUNT_PERSONAL.PASSPORT == txtParentIdcardNo.Text.Trim()))
                        {
                            cmbSmartAccOwner.SelectedValue = "P";
                        }
                    }


                }
            }
        }

        private void InitTempAccount()
        {
            if (txtAppNo.Text != "")
            {
                String channelType = cmbChannelType.SelectedValue.ToString();
                String msg = "";
                ITUtility.ProcessResult pr = new ProcessResult();
                PolicySvcRef.TEMP_ACCOUNT[] tempAccounts = null;

                using (PolicySvcRef.PolicySvcClient client = new PolicySvcRef.PolicySvcClient())
                {
                    pr = client.GET_ONCESMART_INFO(out tempAccounts, out msg, "", "", txtAppNo.Text.Trim(), channelType, 6, UserID);
                }
                if (pr.Successed == false)
                {
                    throw new Exception(pr.ErrorMessage);
                }

                txtTmpFlag.Text = msg;

                if (tempAccounts != null && tempAccounts.Count() > 0)
                {
                    txtTmpBankCode.Text = tempAccounts[0].ACC_BANK;
                    txtTmpBankName.Text = tempAccounts[0].BANK_NAME;
                    txtTmpBranchCode.Text = tempAccounts[0].ACC_BRANCH;
                    txtTmpBranchName.Text = tempAccounts[0].BRANCH_NAME;
                    txtTmpAccountName.Text = tempAccounts[0].ACC_NAME;
                    txtTmpAccountNo.Text = tempAccounts[0].ACC_NO;
                    txtTmpAccountType.Text = tempAccounts[0].ACC_DEPOSIT_TYPE_DETAIL;

                    if (txtTmpAccountName.Text.Replace(" ", String.Empty).Contains(txtCustomerName.Text.Replace(" ", String.Empty))) //ถ้ามีชื่อผู้เอาประกันอยู่ในชื่อบัญชี
                    {
                        //label diable
                        lbltempaccountwarning.Visible = false;
                    }
                    else
                    {
                        //show warning
                        lbltempaccountwarning.Visible = true;
                    }
                }

                using (NewBISSvcRef.NewBISSvcClient cnb = new NewBISSvcRef.NewBISSvcClient())
                {
                    string[] appnoarr = { txtAppNo.Text.Trim() };
                    string[] OutappnoArr = null;
                    string aprOnceSmart = "";
                    NewBISSvcRef.ProcessResult prOnce = new NewBISSvcRef.ProcessResult();
                    aprOnceSmart = cnb.CheckOnceSmartApprove(out OutappnoArr, out prOnce, appnoarr, channelType);
                    if (aprOnceSmart == "N")
                    {
                        lblchecktemp.Visible = true;
                    }
                    else
                    {
                        lblchecktemp.Visible = false;
                    }
                }
            }

        }


        private void txtSaleAgent_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {

                SetAgentRelate();
            }
        }

        private void txtAppDt_Leave(object sender, EventArgs e)
        {
            try
            {
                ChkDateForTextBox(txtAppDt.Text, "ใบคำขอประจำวันที่", txtAppDt);
            }
            catch (Exception ex)
            {
                SetMsgException(ex);
            }
        }

        private void txtAppDt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                try
                {
                    ChkDateForTextBox(txtAppDt.Text, "ใบคำขอประจำวันที่", txtAppDt);
                }
                catch (Exception ex)
                {
                    SetMsgException(ex);
                }
            }
        }

        private void txtAppSignDt_Leave(object sender, EventArgs e)
        {
            try
            {
                ChkDateForTextBox(txtAppSignDt.Text, "วันที่ลงนาม", txtAppSignDt);
            }
            catch (Exception ex)
            {
                SetMsgException(ex);
            }
        }

        private void txtAppSignDt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                try
                {
                    ChkDateForTextBox(txtAppSignDt.Text, "วันที่ลงนาม", txtAppSignDt);
                }
                catch (Exception ex)
                {
                    SetMsgException(ex);
                }
            }
        }

        private void txtStrDt_Leave(object sender, EventArgs e)
        {
            try
            {
                ChkDateForTextBox(txtStrDt.Text, "วันที่คิดผลงาน", txtStrDt);
            }
            catch (Exception ex)
            {
                SetMsgException(ex);
            }
        }

        private void txtStrDt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                try
                {
                    ChkDateForTextBox(txtStrDt.Text, "วันที่คิดผลงาน", txtStrDt);
                }
                catch (Exception ex)
                {
                    SetMsgException(ex);
                }
            }
        }

        private void cmbSendTo_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cmbSendTo.SelectedValue != null)
            {
                txtSendOfc.Text = "";
                if (cmbSendTo.SelectedValue.ToString() == "O")
                {
                    lblSendAt.Visible = true;
                    txtSendOfc.Visible = true;
                    txtSendOfc.Text = txtAppOfc.Text;
                }
                else
                {
                    lblSendAt.Visible = false;
                    txtSendOfc.Visible = false;
                }
            }
        }

        private void txtBirthDt_Leave(object sender, EventArgs e)
        {
            try
            {
                ChkDateForTextBox(txtBirthDt.Text, "วันเกิด", txtBirthDt);
            }
            catch (Exception ex)
            {
                SetMsgException(ex);
            }
        }



        private void txtIdcardNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                try
                {
                    this.Cursor = Cursors.WaitCursor;
                    CheckOldCustomer();
                }
                catch (Exception ex)
                {
                    SetMsgException(ex);
                }
                finally
                {
                    this.Cursor = Cursors.Default;
                }
            }
        }
        private void txtBirthDt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                try
                {
                    this.Cursor = Cursors.WaitCursor;
                    if (txtBirthDt.Text.Trim() != "")
                    {
                        DateTime? d = Utility.StringToDateTime(txtBirthDt.Text.Trim(), "BU");
                        if (d == null)
                        {
                            txtBirthDt.Text = "";
                            txtBirthDt.Focus();
                            throw new Exception("รูปแบบวันที่ ของวันเกิดลูกค้า ไม่ถูกต้อง");
                        }
                    }
                    VerifyFlag = true;
                    CheckOldCustomer();
                }
                catch (Exception ex)
                {
                    SetMsgException(ex);
                }
                finally
                {
                    this.Cursor = Cursors.Default;
                }
            }
        }

        private void CompareAndSetAddressInfo(NewBisPASvcRef.P_ADDRESS_ID pAddressIDObj)
        {

            if (txtAddressName.Text.Trim() != (pAddressIDObj.ADDRESS_NAME ?? ""))
            {
                txtAddressName.BackColor = Color.FromArgb(bgRed[0], bgRed[1], bgRed[2]);
            }
            else
            {
                txtAddressName.BackColor = Color.White;
            }

            if (txtAddressNumber.Text.Trim() != (pAddressIDObj.ADDRESS_NUMBER ?? ""))
            {
                txtAddressNumber.BackColor = Color.FromArgb(bgRed[0], bgRed[1], bgRed[2]);
            }
            else
            {
                txtAddressNumber.BackColor = Color.White;
            }

            if (txtMooh.Text.Trim() != (pAddressIDObj.MOOH ?? ""))
            {
                txtMooh.BackColor = Color.FromArgb(bgRed[0], bgRed[1], bgRed[2]);
            }
            else
            {
                txtMooh.BackColor = Color.White;
            }

            if (txtSoi.Text.Trim() != (pAddressIDObj.SOI ?? ""))
            {
                txtSoi.BackColor = Color.FromArgb(bgRed[0], bgRed[1], bgRed[2]);
            }
            else
            {
                txtSoi.BackColor = Color.White;
            }

            if (txtRoad.Text.Trim() != (pAddressIDObj.ROAD ?? ""))
            {
                txtRoad.BackColor = Color.FromArgb(bgRed[0], bgRed[1], bgRed[2]);
            }
            else
            {
                txtRoad.BackColor = Color.White;
            }
            String province = pAddressIDObj.PROVINCE == "" ? "ระบุจังหวัดที่ต้องการ" : pAddressIDObj.PROVINCE;
            if (cmbProvince.Text.Trim() == "ระบุจังหวัดที่ต้องการ" || string.IsNullOrEmpty(cmbProvince.Text))
            {
                cmbProvince.Text = pAddressIDObj.PROVINCE;
            }
            else
            {
                if (cmbProvince.Text.Trim() != (pAddressIDObj.PROVINCE ?? ""))
                {
                    cmbProvince.BackColor = Color.FromArgb(bgRed[0], bgRed[1], bgRed[2]);
                }
                else
                {
                    cmbProvince.BackColor = Color.White;
                }
            }

            String amphur = pAddressIDObj.AMPHUR == "" ? "ระบุอำเภอที่ต้องการ" : pAddressIDObj.AMPHUR;
            if (cmbAmphur.Text.Trim() == "ระบุอำเภอที่ต้องการ" || string.IsNullOrEmpty(cmbAmphur.Text))
            {
                cmbAmphur.Text = pAddressIDObj.AMPHUR;
            }
            else
            {
                if (cmbAmphur.Text.Trim() != (pAddressIDObj.AMPHUR ?? ""))
                {
                    cmbAmphur.BackColor = Color.FromArgb(bgRed[0], bgRed[1], bgRed[2]);
                }
                else
                {
                    cmbAmphur.BackColor = Color.White;
                }
            }

            String tambol = pAddressIDObj.TAMBOL == "" ? "ระบุตำบลที่ต้องการ" : pAddressIDObj.TAMBOL;
            if (cmbTambol.Text.Trim() == "ระบุตำบลที่ต้องการ" || string.IsNullOrEmpty(cmbTambol.Text))
            {
                cmbTambol.Text = pAddressIDObj.TAMBOL;
            }
            else
            {
                if (cmbTambol.Text.Trim() != (pAddressIDObj.TAMBOL ?? ""))
                {
                    cmbTambol.BackColor = Color.FromArgb(bgRed[0], bgRed[1], bgRed[2]);
                }
                else
                {
                    cmbTambol.BackColor = Color.White;
                }
            }

            if (txtZipcode.Text.Trim() != (pAddressIDObj.ZIP_CODE ?? ""))
            {
                txtZipcode.BackColor = Color.FromArgb(bgRed[0], bgRed[1], bgRed[2]);
            }
            else
            {
                txtZipcode.BackColor = Color.White;
            }

            if (txtPhoneNumber.Text.Trim() != (pAddressIDObj.PHONE_NUMBER ?? ""))
            {
                txtPhoneNumber.BackColor = Color.FromArgb(bgRed[0], bgRed[1], bgRed[2]);
            }
            else
            {
                txtPhoneNumber.BackColor = Color.White;
            }
        }
        private void ChkOldCusDataWithNewCusData(NewBisPASvcRef.P_NAME_ID pNameIDObj)
        {
            if (txtIdcardNo.Text.Trim() == "")
            {
                if (cmbCardType.SelectedValue.ToString() == "1")
                {
                    txtIdcardNo.Text = pNameIDObj.IDCARD_NO;
                }
                else if (cmbCardType.SelectedValue.ToString() == "2")
                {
                    txtIdcardNo.Text = pNameIDObj.PASSPORT;
                }
            }
            else
            {
                if (cmbCardType.SelectedValue.ToString() == "1")
                {
                    if (txtIdcardNo.Text.Trim() != pNameIDObj.IDCARD_NO)
                    {
                        txtIdcardNo.BackColor = Color.FromArgb(bgRed[0], bgRed[1], bgRed[2]);
                    }
                    else
                    {
                        txtIdcardNo.BackColor = Color.FromArgb(bgYellow[0], bgYellow[1], bgYellow[2]);
                    }
                }
                else if (cmbCardType.SelectedValue.ToString() == "2")
                {
                    if (txtIdcardNo.Text.Trim() != pNameIDObj.PASSPORT)
                    {
                        txtIdcardNo.BackColor = Color.FromArgb(bgRed[0], bgRed[1], bgRed[2]);
                    }
                    else
                    {
                        txtIdcardNo.BackColor = Color.FromArgb(bgYellow[0], bgYellow[1], bgYellow[2]);
                    }
                }
            }

            if (txtPrename.Text.Trim() == "")
            {
                txtPrename.Text = pNameIDObj.PRENAME;
            }
            else
            {
                if (txtPrename.Text.Trim() != pNameIDObj.PRENAME)
                {
                    txtPrename.BackColor = Color.FromArgb(bgRed[0], bgRed[1], bgRed[2]);
                }
                else
                {
                    txtPrename.BackColor = Color.White;
                }
            }

            if (txtName.Text.Trim() == "")
            {
                txtName.Text = pNameIDObj.NAME;
            }
            else
            {
                if (txtName.Text.Trim() != pNameIDObj.NAME)
                {
                    txtName.BackColor = Color.FromArgb(bgRed[0], bgRed[1], bgRed[2]);
                }
                else
                {
                    txtName.BackColor = Color.White;
                }
            }

            if (txtSurname.Text.Trim() == "")
            {
                txtSurname.Text = pNameIDObj.SURNAME;
            }
            else
            {
                if (txtSurname.Text.Trim() != pNameIDObj.SURNAME)
                {
                    txtSurname.BackColor = Color.FromArgb(bgRed[0], bgRed[1], bgRed[2]);
                }
                else
                {
                    txtSurname.BackColor = Color.White;
                }
            }

            if (cmbSex.SelectedValue.ToString() == "")
            {
                cmbSex.SelectedValue = pNameIDObj.SEX;
            }
            else
            {
                if (cmbSex.SelectedValue.ToString() != pNameIDObj.SEX)
                {
                    cmbSex.BackColor = Color.FromArgb(bgRed[0], bgRed[1], bgRed[2]);
                }
                else
                {
                    cmbSex.BackColor = Color.White;
                }
            }

            String cusBirthDate = pNameIDObj.BIRTH_DT == null ? "" : Utility.dateTimeToString(pNameIDObj.BIRTH_DT.Value, "dd/MM/yyyy", "BU");
            if (txtBirthDt.Text.Trim() == "")
            {
                txtBirthDt.Text = cusBirthDate;
            }
            else
            {
                if (txtBirthDt.Text.Trim() != cusBirthDate)
                {
                    txtBirthDt.BackColor = Color.FromArgb(bgRed[0], bgRed[1], bgRed[2]);
                }
                else
                {
                    txtBirthDt.BackColor = Color.FromArgb(bgYellow[0], bgYellow[1], bgYellow[2]);
                }
            }

            String cusNationality = "";
            cusNationality = pNameIDObj.NATIONALITY == null ? "" : pNameIDObj.NATIONALITY;
            if (cmbNationality.SelectedValue.ToString() == "")
            {
                cmbNationality.SelectedValue = cusNationality;
            }
            else
            {
                if (cmbNationality.SelectedValue.ToString() != cusNationality)
                {
                    cmbNationality.BackColor = Color.FromArgb(bgRed[0], bgRed[1], bgRed[2]);
                }
                else
                {
                    cmbNationality.BackColor = Color.White;
                }
            }

            if (txtMbPhone.Text.Trim() == "")
            {
                txtMbPhone.Text = pNameIDObj.MB_PHONE;
            }
            else
            {
                if (txtMbPhone.Text.Trim() != pNameIDObj.MB_PHONE)
                {
                    txtMbPhone.BackColor = Color.FromArgb(bgRed[0], bgRed[1], bgRed[2]);
                }
                else
                {
                    txtMbPhone.BackColor = Color.White;
                }
            }

            if (txtEmail.Text.Trim() == "")
            {
                txtEmail.Text = pNameIDObj.EMAIL;
            }
            else
            {
                if (txtEmail.Text.Trim() != pNameIDObj.EMAIL)
                {
                    txtEmail.BackColor = Color.FromArgb(bgRed[0], bgRed[1], bgRed[2]);
                }
                else
                {
                    txtEmail.BackColor = Color.White;
                }
            }
        }


        private void ChkOldParentDataWithNewCusParent(NewBisPASvcRef.P_NAME_ID pNameIDObj)
        {
            if (txtParentIdcardNo.Text.Trim() == "")
            {
                if (cmbParentCardType.SelectedValue.ToString() == "1")
                {
                    txtParentIdcardNo.Text = pNameIDObj.IDCARD_NO;
                }
                else if (cmbParentCardType.SelectedValue.ToString() == "2")
                {
                    txtParentIdcardNo.Text = pNameIDObj.PASSPORT;
                }
            }
            else
            {
                if (cmbParentCardType.SelectedValue.ToString() == "1")
                {
                    if (txtParentIdcardNo.Text.Trim() != pNameIDObj.IDCARD_NO)
                    {
                        txtParentIdcardNo.BackColor = Color.FromArgb(bgRed[0], bgRed[1], bgRed[2]);
                    }
                    else
                    {
                        txtParentIdcardNo.BackColor = Color.FromArgb(bgYellow[0], bgYellow[1], bgYellow[2]);
                    }
                }
                else if (cmbParentCardType.SelectedValue.ToString() == "2")
                {
                    if (txtParentIdcardNo.Text.Trim() != pNameIDObj.PASSPORT)
                    {
                        txtParentIdcardNo.BackColor = Color.FromArgb(bgRed[0], bgRed[1], bgRed[2]);
                    }
                    else
                    {
                        txtParentIdcardNo.BackColor = Color.FromArgb(bgYellow[0], bgYellow[1], bgYellow[2]);
                    }
                }
            }

            if (txtParentPrename.Text.Trim() == "")
            {
                txtParentPrename.Text = pNameIDObj.PRENAME;
            }
            else
            {
                if (txtParentPrename.Text.Trim() != pNameIDObj.PRENAME)
                {
                    txtParentPrename.BackColor = Color.FromArgb(bgRed[0], bgRed[1], bgRed[2]);
                }
                else
                {
                    txtParentPrename.BackColor = Color.White;
                }
            }

            if (txtParentName.Text.Trim() == "")
            {
                txtParentName.Text = pNameIDObj.NAME;
            }
            else
            {
                if (txtParentName.Text.Trim() != pNameIDObj.NAME)
                {
                    txtParentName.BackColor = Color.FromArgb(bgRed[0], bgRed[1], bgRed[2]);
                }
                else
                {
                    txtParentName.BackColor = Color.White;
                }
            }

            if (txtParentSurname.Text.Trim() == "")
            {
                txtParentSurname.Text = pNameIDObj.SURNAME;
            }
            else
            {
                if (txtParentSurname.Text.Trim() != pNameIDObj.SURNAME)
                {
                    txtParentSurname.BackColor = Color.FromArgb(bgRed[0], bgRed[1], bgRed[2]);
                }
                else
                {
                    txtParentSurname.BackColor = Color.White;
                }
            }

            if (cmbParentGender.SelectedValue.ToString() == "")
            {
                cmbParentGender.SelectedValue = pNameIDObj.SEX;
            }
            else
            {
                if (cmbParentGender.SelectedValue.ToString() != pNameIDObj.SEX)
                {
                    cmbParentGender.BackColor = Color.FromArgb(bgRed[0], bgRed[1], bgRed[2]);
                }
                else
                {
                    cmbParentGender.BackColor = Color.White;
                }
            }

            String cusBirthDate = pNameIDObj.BIRTH_DT == null ? "" : Utility.dateTimeToString(pNameIDObj.BIRTH_DT.Value, "dd/MM/yyyy", "BU");
            if (txtParentBirthDt.Text.Trim() == "")
            {
                txtParentBirthDt.Text = cusBirthDate;
            }
            else
            {
                if (txtParentBirthDt.Text.Trim() != cusBirthDate)
                {
                    txtParentBirthDt.BackColor = Color.FromArgb(bgRed[0], bgRed[1], bgRed[2]);
                }
                else
                {
                    txtParentBirthDt.BackColor = Color.FromArgb(bgYellow[0], bgYellow[1], bgYellow[2]);
                }
            }

            String cusNationality = "";
            cusNationality = pNameIDObj.NATIONALITY == null ? "" : pNameIDObj.NATIONALITY;
            if (cmbParentNationality.SelectedValue.ToString() == "")
            {
                cmbParentNationality.SelectedValue = cusNationality;
            }
            else
            {
                if (cmbParentNationality.SelectedValue.ToString() != cusNationality)
                {
                    cmbParentNationality.BackColor = Color.FromArgb(bgRed[0], bgRed[1], bgRed[2]);
                }
                else
                {
                    cmbParentNationality.BackColor = Color.White;
                }
            }

            if (txtParentMbPhone.Text.Trim() == "")
            {
                txtParentMbPhone.Text = pNameIDObj.MB_PHONE;
            }
            else
            {
                if (txtParentMbPhone.Text.Trim() != pNameIDObj.MB_PHONE)
                {
                    txtParentMbPhone.BackColor = Color.FromArgb(bgRed[0], bgRed[1], bgRed[2]);
                }
                else
                {
                    txtParentMbPhone.BackColor = Color.White;
                }
            }

            if (txtParentEmail.Text.Trim() == "")
            {
                txtParentEmail.Text = pNameIDObj.EMAIL;
            }
            else
            {
                if (txtParentEmail.Text.Trim() != pNameIDObj.EMAIL)
                {
                    txtParentEmail.BackColor = Color.FromArgb(bgRed[0], bgRed[1], bgRed[2]);
                }
                else
                {
                    txtParentEmail.BackColor = Color.White;
                }
            }
        }


        private void ChkOldParentDataWithParentNewCusData(NewBisPASvcRef.P_PARENT_ID pParentIDObj)
        {
            if (txtParentIdcardNo.Text == "")
            {
                txtParentIdcardNo.Text = pParentIDObj.IDCARD_NO;
            }

            if (txtParentPrename.Text == "")
            {
                txtParentPrename.Text = pParentIDObj.PRENAME;
            }

            if (txtParentName.Text == "")
            {
                txtParentName.Text = pParentIDObj.NAME;
            }

            if (txtParentSurname.Text == "")
            {
                txtParentSurname.Text = pParentIDObj.SURNAME;
            }

            if (cmbParentGender.SelectedValue.ToString() == "")
            {
                cmbParentGender.SelectedValue = pParentIDObj.SEX;
            }

            String parentBirthDate = pParentIDObj.BIRTH_DT == null ? "" : Utility.dateTimeToString(pParentIDObj.BIRTH_DT.Value, "dd/MM/yyyy", "BU");
            if (txtParentBirthDt.Text == "")
            {
                txtParentBirthDt.Text = parentBirthDate;
            }

            if (cmbParentNationality.SelectedValue.ToString() == "")
            {
                cmbParentNationality.SelectedValue = pParentIDObj.NATIONALITY;
            }

            if (txtParentMbPhone.Text == "")
            {
                txtParentMbPhone.Text = pParentIDObj.MB_PHONE;
            }
        }

        private void ChkOldParentDataWithNewCusData(NewBisPASvcRef.P_PARENT_ID pParentIDObj)
        {
            if (txtIdcardNo.Text == "")
            {
                txtIdcardNo.Text = pParentIDObj.IDCARD_NO;
            }

            if (txtPrename.Text == "")
            {
                txtPrename.Text = pParentIDObj.PRENAME;
            }

            if (txtName.Text == "")
            {
                txtName.Text = pParentIDObj.NAME;
            }

            if (txtSurname.Text == "")
            {
                txtSurname.Text = pParentIDObj.SURNAME;
            }

            if (cmbSex.SelectedValue.ToString() == "")
            {
                cmbSex.SelectedValue = pParentIDObj.SEX;
            }

            String parentBirthDate = pParentIDObj.BIRTH_DT == null ? "" : Utility.dateTimeToString(pParentIDObj.BIRTH_DT.Value, "dd/MM/yyyy", "BU");
            if (txtBirthDt.Text == "")
            {
                txtBirthDt.Text = parentBirthDate;
            }

            if (cmbNationality.SelectedValue.ToString() == "")
            {
                cmbNationality.SelectedValue = pParentIDObj.NATIONALITY;
            }

            if (txtMbPhone.Text == "")
            {
                txtMbPhone.Text = pParentIDObj.MB_PHONE;
            }
        }

        private void cmbProvince_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbProvince.SelectedValue != null)
                {
                    NewBisPASvcRef.GET_LIST_COLLECTION amphurS = new NewBisPASvcRef.GET_LIST_COLLECTION();
                    if (cmbProvince.SelectedValue.ToString() == "")
                    {
                        NewBisPASvcRef.GET_LIST amphurObj = new NewBisPASvcRef.GET_LIST();
                        amphurObj.CODE = "";
                        amphurObj.DESCRIPTION = "ระบุอำเภอที่ต้องการ";
                        amphurS.Add(amphurObj);
                        cmbAmphur.DataSource = amphurS;
                        cmbAmphur.DisplayMember = "DESCRIPTION";
                        cmbAmphur.ValueMember = "CODE";
                        cmbAmphur.SelectedValue = "";
                    }
                    else
                    {
                        if (PAR_ZTB_POST_SUBDISTRICT_COLLECTION != null && PAR_ZTB_POST_SUBDISTRICT_COLLECTION.Count() > 0)
                        {
                            var AmphurLinq = (from amphurLinq in PAR_ZTB_POST_SUBDISTRICT_COLLECTION
                                              where amphurLinq.PROVINCE == cmbProvince.SelectedValue.ToString()
                                              orderby amphurLinq.DISTRICT ascending
                                              select amphurLinq.DISTRICT).Distinct();
                            if (AmphurLinq != null && AmphurLinq.Count() > 0)
                            {
                                NewBisPASvcRef.GET_LIST amphurObj = new NewBisPASvcRef.GET_LIST();
                                amphurObj.CODE = "";
                                amphurObj.DESCRIPTION = "ระบุอำเภอที่ต้องการ";
                                amphurS.Add(amphurObj);

                                foreach (var item in AmphurLinq)
                                {
                                    NewBisPASvcRef.GET_LIST obj = new NewBisPASvcRef.GET_LIST();
                                    obj.CODE = item.ToString();
                                    obj.DESCRIPTION = item.ToString();
                                    amphurS.Add(obj);
                                }
                                cmbAmphur.DataSource = amphurS;
                                cmbAmphur.DisplayMember = "DESCRIPTION";
                                cmbAmphur.ValueMember = "CODE";
                                cmbAmphur.SelectedValue = "";

                                AutoCompleteStringCollection data = new AutoCompleteStringCollection();
                                foreach (NewBisPASvcRef.GET_LIST item in amphurS)
                                {
                                    data.Add(item.DESCRIPTION);
                                }
                                cmbAmphur.AutoCompleteMode = AutoCompleteMode.Suggest;
                                cmbAmphur.AutoCompleteSource = AutoCompleteSource.CustomSource;
                                cmbAmphur.AutoCompleteCustomSource = data;
                            }
                            else
                            {
                                NewBisPASvcRef.GET_LIST amphurObj = new NewBisPASvcRef.GET_LIST();
                                amphurObj.CODE = "";
                                amphurObj.DESCRIPTION = "ระบุอำเภอที่ต้องการ";
                                amphurS.Add(amphurObj);
                                cmbAmphur.DataSource = amphurS;
                                cmbAmphur.DisplayMember = "DESCRIPTION";
                                cmbAmphur.ValueMember = "CODE";
                                cmbAmphur.SelectedValue = "";
                            }
                        }
                    }


                }
            }
            catch (Exception ex)
            {
                SetMsgException(ex);
            }
        }

        private void cmbAmphur_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbAmphur.SelectedValue != null)
                {
                    NewBisPASvcRef.GET_LIST_COLLECTION tambolS = new NewBisPASvcRef.GET_LIST_COLLECTION();
                    if (cmbAmphur.SelectedValue.ToString() == "")
                    {
                        NewBisPASvcRef.GET_LIST tambolObj = new NewBisPASvcRef.GET_LIST();
                        tambolObj.CODE = "";
                        tambolObj.DESCRIPTION = "ระบุตำบลที่ต้องการ";
                        tambolS.Add(tambolObj);
                        cmbTambol.DataSource = tambolS;
                        cmbTambol.DisplayMember = "DESCRIPTION";
                        cmbTambol.ValueMember = "CODE";
                        cmbTambol.SelectedValue = "";
                    }
                    else
                    {
                        if (PAR_ZTB_POST_SUBDISTRICT_COLLECTION != null && PAR_ZTB_POST_SUBDISTRICT_COLLECTION.Count() > 0)
                        {
                            var TambolLing = (from tambolLing in PAR_ZTB_POST_SUBDISTRICT_COLLECTION
                                              where tambolLing.PROVINCE == cmbProvince.SelectedValue.ToString()
                                              && tambolLing.DISTRICT == cmbAmphur.SelectedValue.ToString()
                                              orderby tambolLing.SUBDISTRICT ascending
                                              select tambolLing.SUBDISTRICT).Distinct();
                            if (TambolLing != null && TambolLing.Count() > 0)
                            {
                                NewBisPASvcRef.GET_LIST tambolObj = new NewBisPASvcRef.GET_LIST();
                                tambolObj.CODE = "";
                                tambolObj.DESCRIPTION = "ระบุตำบลที่ต้องการ";
                                tambolS.Add(tambolObj);

                                foreach (var item in TambolLing)
                                {
                                    NewBisPASvcRef.GET_LIST obj = new NewBisPASvcRef.GET_LIST();
                                    obj.CODE = item.ToString();
                                    obj.DESCRIPTION = item.ToString();
                                    tambolS.Add(obj);
                                }
                                cmbTambol.DataSource = tambolS;
                                cmbTambol.DisplayMember = "DESCRIPTION";
                                cmbTambol.ValueMember = "CODE";
                                cmbTambol.SelectedValue = "";
                                AutoCompleteStringCollection data = new AutoCompleteStringCollection();
                                foreach (NewBisPASvcRef.GET_LIST item in tambolS)
                                {
                                    data.Add(item.DESCRIPTION);
                                }
                                cmbTambol.AutoCompleteMode = AutoCompleteMode.Suggest;
                                cmbTambol.AutoCompleteSource = AutoCompleteSource.CustomSource;
                                cmbTambol.AutoCompleteCustomSource = data;
                            }
                            else
                            {
                                NewBisPASvcRef.GET_LIST tambolObj = new NewBisPASvcRef.GET_LIST();
                                tambolObj.CODE = "";
                                tambolObj.DESCRIPTION = "ระบุตำบลที่ต้องการ";
                                tambolS.Add(tambolObj);
                                cmbTambol.DataSource = tambolS;
                                cmbTambol.DisplayMember = "DESCRIPTION";
                                cmbTambol.ValueMember = "CODE";
                                cmbTambol.SelectedValue = "";
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                SetMsgException(ex);
            }
        }

        private void cmbTambol_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbTambol.SelectedValue != null)
                {
                    if (cmbTambol.SelectedValue.ToString() == "")
                    {
                        txtZipcode.Text = "";
                    }
                    else
                    {
                        if (PAR_ZTB_POST_SUBDISTRICT_COLLECTION != null && PAR_ZTB_POST_SUBDISTRICT_COLLECTION.Count() > 0)
                        {
                            var ZipcodeLinq = from zipcodeLinq in PAR_ZTB_POST_SUBDISTRICT_COLLECTION
                                              where zipcodeLinq.PROVINCE == cmbProvince.SelectedValue.ToString()
                                              && zipcodeLinq.DISTRICT == cmbAmphur.SelectedValue.ToString()
                                              && zipcodeLinq.SUBDISTRICT == cmbTambol.SelectedValue.ToString()
                                              select zipcodeLinq;
                            if (ZipcodeLinq != null && ZipcodeLinq.Count() > 0)
                            {
                                if (ZipcodeLinq.Count() > 1)
                                {
                                    txtZipcode.Text = "";
                                }
                                else
                                {
                                    txtZipcode.Text = ZipcodeLinq.ToArray()[0].POSTCODE;
                                }
                            }
                            else
                            {
                                txtZipcode.Text = "";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                SetMsgException(ex);
            }
        }

        private void txtZipcode_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtZipcode.Text != "")
                {
                    ChkIntForTextBox(txtZipcode.Text.Replace(",", ""), "รหัสไปรษณีย์", txtZipcode);
                }
            }
            catch (Exception ex)
            {
                SetMsgException(ex);
            }

        }

        private void cmbAddressType_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                if (cmbAddressType.SelectedValue != null && IsAutoCheckOldAddressData)
                {
                    if (cmbAddressType.SelectedValue.ToString() != "" && NAME_ID != null)
                    {
                        this.Cursor = Cursors.WaitCursor;

                        SetColorItemBeginCustomerAddress();

                        NewBisPASvcRef.ProcessResult pr = new NewBisPASvcRef.ProcessResult();
                        NewBisPASvcRef.P_ADDRESS_ID_COLLECTION addressIDColl = new NewBisPASvcRef.P_ADDRESS_ID_COLLECTION();
                        using (NewBisPASvcRef.NewBisPASvcClient client = new NewBisPASvcRef.NewBisPASvcClient())
                        {
                            pr = client.GetOldAddressByNameID(out addressIDColl, NAME_ID);
                            if (pr.Successed == false)
                            {
                                throw new Exception(pr.ErrorMessage);
                            }
                        }

                        if (addressIDColl != null && addressIDColl.Count() > 0)
                        {
                            ShowOldAdrressByNameIDForm ShowOldAdrressByNameIDForm = new ShowOldAdrressByNameIDForm();
                            ShowOldAdrressByNameIDForm.P_ADDRESS_ID_COLL = new NewBisPASvcRef.P_ADDRESS_ID_COLLECTION();
                            ShowOldAdrressByNameIDForm.P_ADDRESS_ID_COLL = addressIDColl;
                            ShowOldAdrressByNameIDForm.ADDRESS_ID = ADDRESS_ID;
                            ShowOldAdrressByNameIDForm.ShowDialog();

                            if (ShowOldAdrressByNameIDForm.P_ADDRESS_ID_OBJ != null && ShowOldAdrressByNameIDForm.P_ADDRESS_ID_OBJ.ADDRESS_ID != null)
                            {
                                ADDRESS_ID = ShowOldAdrressByNameIDForm.P_ADDRESS_ID_OBJ.ADDRESS_ID;
                                cmbAdrType.SelectedValue = "1";
                                cmbAdrType.BackColor = Color.FromArgb(bgRed[0], bgRed[1], bgRed[2]);
                                ChkOldAddressWithNewAddress(ShowOldAdrressByNameIDForm.P_ADDRESS_ID_OBJ);
                            }
                            else
                            {
                                ADDRESS_ID = null;
                                cmbAdrType.SelectedValue = "2";
                                cmbAdrType.BackColor = Color.White;
                            }

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                SetMsgException(ex);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void ChkOldAddressWithNewAddress(NewBisPASvcRef.P_ADDRESS_ID pAddressIDObj)
        {
            if (txtAddressName.Text.Trim() == "")
            {
                txtAddressName.Text = pAddressIDObj.ADDRESS_NAME;
            }
            else
            {
                if (txtAddressName.Text.Trim() != pAddressIDObj.ADDRESS_NAME)
                {
                    txtAddressName.BackColor = Color.FromArgb(bgRed[0], bgRed[1], bgRed[2]);
                }
                else
                {
                    txtAddressName.BackColor = Color.White;
                }
            }

            if (txtAddressNumber.Text.Trim() == "")
            {
                txtAddressNumber.Text = pAddressIDObj.ADDRESS_NUMBER;
            }
            else
            {
                if (txtAddressNumber.Text.Trim() != pAddressIDObj.ADDRESS_NUMBER)
                {
                    txtAddressNumber.BackColor = Color.FromArgb(bgRed[0], bgRed[1], bgRed[2]);
                }
                else
                {
                    txtAddressNumber.BackColor = Color.White;
                }
            }

            if (txtMooh.Text.Trim() == "")
            {
                txtMooh.Text = pAddressIDObj.MOOH;
            }
            else
            {
                if (txtMooh.Text.Trim() != pAddressIDObj.MOOH)
                {
                    txtMooh.BackColor = Color.FromArgb(bgRed[0], bgRed[1], bgRed[2]);
                }
                else
                {
                    txtMooh.BackColor = Color.White;
                }
            }

            if (txtSoi.Text.Trim() == "")
            {
                txtSoi.Text = pAddressIDObj.SOI;
            }
            else
            {
                if (txtSoi.Text.Trim() != pAddressIDObj.SOI)
                {
                    txtSoi.BackColor = Color.FromArgb(bgRed[0], bgRed[1], bgRed[2]);
                }
                else
                {
                    txtSoi.BackColor = Color.White;
                }
            }

            if (txtRoad.Text.Trim() == "")
            {
                txtRoad.Text = pAddressIDObj.ROAD;
            }
            else
            {
                if (txtRoad.Text.Trim() != pAddressIDObj.ROAD)
                {
                    txtRoad.BackColor = Color.FromArgb(bgRed[0], bgRed[1], bgRed[2]);
                }
                else
                {
                    txtRoad.BackColor = Color.White;
                }
            }

            String province = pAddressIDObj.PROVINCE == "" ? "ระบุจังหวัดที่ต้องการ" : pAddressIDObj.PROVINCE;
            if (cmbProvince.Text.Trim() == "ระบุจังหวัดที่ต้องการ" || string.IsNullOrEmpty(cmbProvince.Text))
            {
                cmbProvince.Text = pAddressIDObj.PROVINCE;
            }
            else
            {
                if (cmbProvince.Text.Trim() != pAddressIDObj.PROVINCE)
                {
                    cmbProvince.BackColor = Color.FromArgb(bgRed[0], bgRed[1], bgRed[2]);
                }
                else
                {
                    cmbProvince.BackColor = Color.White;
                }
            }

            String amphur = pAddressIDObj.AMPHUR == "" ? "ระบุอำเภอที่ต้องการ" : pAddressIDObj.AMPHUR;
            if (cmbAmphur.Text.Trim() == "ระบุอำเภอที่ต้องการ" || string.IsNullOrEmpty(cmbAmphur.Text))
            {
                cmbAmphur.Text = pAddressIDObj.AMPHUR;
            }
            else
            {
                if (cmbAmphur.Text.Trim() != pAddressIDObj.AMPHUR)
                {
                    cmbAmphur.BackColor = Color.FromArgb(bgRed[0], bgRed[1], bgRed[2]);
                }
                else
                {
                    cmbAmphur.BackColor = Color.White;
                }
            }

            String tambol = pAddressIDObj.TAMBOL == "" ? "ระบุตำบลที่ต้องการ" : pAddressIDObj.TAMBOL;
            if (cmbTambol.Text.Trim() == "ระบุตำบลที่ต้องการ" || string.IsNullOrEmpty(cmbTambol.Text))
            {
                cmbTambol.Text = pAddressIDObj.TAMBOL;
            }
            else
            {
                if (cmbTambol.Text.Trim() != pAddressIDObj.TAMBOL)
                {
                    cmbTambol.BackColor = Color.FromArgb(bgRed[0], bgRed[1], bgRed[2]);
                }
                else
                {
                    cmbTambol.BackColor = Color.White;
                }
            }

            if (txtZipcode.Text.Trim() == "")
            {
                txtZipcode.Text = pAddressIDObj.ZIP_CODE;
            }
            else
            {
                if (txtZipcode.Text.Trim() != pAddressIDObj.ZIP_CODE)
                {
                    txtZipcode.BackColor = Color.FromArgb(bgRed[0], bgRed[1], bgRed[2]);
                }
                else
                {
                    txtZipcode.BackColor = Color.White;
                }
            }

            if (txtPhoneNumber.Text.Trim() == "")
            {
                txtPhoneNumber.Text = pAddressIDObj.PHONE_NUMBER;
            }
            else
            {
                if (txtPhoneNumber.Text.Trim() != pAddressIDObj.PHONE_NUMBER)
                {
                    txtPhoneNumber.BackColor = Color.FromArgb(bgRed[0], bgRed[1], bgRed[2]);
                }
                else
                {
                    txtPhoneNumber.BackColor = Color.White;
                }
            }
        }

        private void tabMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                // init Plain
                if (ClickTabPlanData == false)
                {
                    if (PAR_PA_APPLICATION_ID != null && PAR_PA_APPLICATION_ID.APP_ID != null)
                    {
                        PlanPDParameter();
                        CreditCardParameter();
                        Decimal? totalPremium = 0;
                        Decimal? suspensePremium = null;
                        Decimal overPremium = 0;
                        DateTime? suspenseDate = null;
                        string channelType = cmbChannelType.SelectedValue == null ? "" : cmbChannelType.SelectedValue.ToString();
                        if (PAR_PA_APPLICATION_ID.APPLICATION_Collection != null && PAR_PA_APPLICATION_ID.APPLICATION_Collection.Count() > 0)
                        {
                            foreach (NewBisPASvcRef.U_APPLICATION obj in PAR_PA_APPLICATION_ID.APPLICATION_Collection)
                            {
                                if (obj.LIFE_ID == null) continue;
                                totalPremium = totalPremium + (obj.LIFE_ID.PREMIUM == null ? 0 : obj.LIFE_ID.PREMIUM.Value);
                                if (obj.LIFE_ID.FREE_FLG == "Y" || IsChannelTypeFreePolicy(channelType))
                                {
                                    GetULifeIDFree(obj.LIFE_ID);
                                }
                                else if (obj.LIFE_ID.FREE_FLG == "N")
                                {
                                    GetULifeIDPaid(obj.LIFE_ID);
                                    if (obj.LIFE_ID.APP_SPOUSE_Collection != null && obj.LIFE_ID.APP_SPOUSE_Collection.Count() > 0)
                                    {
                                        var GetData = from getData in obj.LIFE_ID.APP_SPOUSE_Collection
                                                      where getData.TMN == 'N'
                                                      select getData;
                                        if (GetData != null && GetData.Count() > 0)
                                        {
                                            GetUSpouseID(obj.LIFE_ID.APP_SPOUSE_Collection, PAR_U_SPOUSE_ID_COLL);
                                        }
                                    }

                                    bool haveData = false;

                                    if (PAR_SUSPENSE_CHECK == true)
                                    {
                                        haveData = PAR_SUSPENSE_DETAIL;
                                        suspensePremium = PAR_PREMIUM_SUSPENSE;
                                        suspenseDate = PAR_PAYIN_DATE;
                                    }
                                    else
                                    {
                                        if (cmbChannelType.SelectedValue.ToString() == "PO" || cmbChannelType.SelectedValue.ToString() == "PN")
                                        {
                                            GetSuspenseData(out haveData, out suspensePremium, out suspenseDate, totalPremium);
                                        }

                                    }


                                    lblSuspense.ForeColor = Color.Black;
                                    txtSuspensePremium.Cursor = Cursors.Default;
                                    if (haveData == true)
                                    {
                                        lblSuspense.ForeColor = Color.Red;
                                        txtSuspensePremium.Cursor = Cursors.Hand;
                                    }

                                }
                            }

                            txtLifePremium.Text = (totalPremium ?? 0).ToString("n0");
                            // checkmode  
                            if (cmbChannelType.SelectedValue.ToString() == "PO" || cmbChannelType.SelectedValue.ToString() == "PN")
                            {
                                var repo = new RenewalRepository();
                                if (!repo.IsRenewApplication(txtAppNo.Text.Trim(), cmbChannelType.SelectedValue.ToString()))
                                {
                                    totalPremium = (PAR_PA_APPLICATION_ID.APPLICATION_Collection.First().LIFE_ID.P_MODE == 12 ? (totalPremium ?? 0) * 2 : (totalPremium ?? 0));
                                }
                            }
                            txtTotalPremium.Text = totalPremium.Value.ToString("n0");
                            txtSuspensePremium.Text = suspensePremium == null ? "0" : suspensePremium.Value.ToString("n0");
                            SuspenseePayDate = suspenseDate;
                            overPremium = (Convert.ToDecimal(txtSuspensePremium.Text.Replace(",", "")) - Convert.ToDecimal(txtTotalPremium.Text.Replace(",", "")));

                            if (overPremium > 0)
                            {
                                txtOverPremium.Text = "+" + overPremium.ToString("n0");
                            }
                            else if (overPremium < 0)
                            {
                                txtOverPremium.Text = overPremium.ToString("n0");
                            }
                            else
                            {
                                txtOverPremium.Text = overPremium.ToString("n0");
                            }


                            if (PAR_PA_APPLICATION_ID.APPLICATION_Collection[0].CREDIT_CARD_Collection != null && PAR_PA_APPLICATION_ID.APPLICATION_Collection[0].CREDIT_CARD_Collection.Count() > 0)
                            {
                                GetUCreditCard(PAR_PA_APPLICATION_ID.APPLICATION_Collection[0].CREDIT_CARD_Collection[0]);
                            }


                        }
                    }

                    ClickTabPlanData = true;
                }

                if (tabMain.SelectedTab.Name == "tabCustomerData")
                {

                }
                else if (tabMain.SelectedTab.Name == "tabParaentData")
                {

                    int? cusAge = txtStAge.Text.Trim() == "" ? (int?)null : Convert.ToInt16(txtStAge.Text.Trim());
                    if (IsChildApplicationAndAlertMessage(cusAge))
                    {
                        ParentOccpuPanel.Visible = ParentInfoPanel.Visible = ParentAddressPanel.Visible = true;
                    }
                    else
                    {

                        ParentOccpuPanel.Visible = ParentInfoPanel.Visible = ParentAddressPanel.Visible = false;
                    }


                }

                //else if (tabMain.SelectedTab.Name == "tabPlanData")
                //{

                //}
                else if (tabMain.SelectedTab.Name == "tabBenefit")
                {
                    ClearControlsBenefitAdd();
                    SetItemBnfPerson();
                    if (ClickTabBenefit == false)
                    {
                        String newBisFlag = "";
                        String channelType = cmbChannelType.SelectedValue.ToString();
                        if (PAR_PA_APPLICATION_ID != null && PAR_PA_APPLICATION_ID.APP_ID != null)
                        {
                            if (PAR_PA_APPLICATION_ID.APP_ISIS != null && PAR_PA_APPLICATION_ID.APP_ISIS.APP_ID != null)
                            {
                                newBisFlag = PAR_PA_APPLICATION_ID.APP_ISIS.NEWBIS == null ? "" : PAR_PA_APPLICATION_ID.APP_ISIS.NEWBIS.Value.ToString();
                            }
                        }

                        if ((channelType == "PD" || channelType == "PF") && newBisFlag == "N")
                        {
                            if (PAR_BENEFIT_ID_COLL != null && PAR_BENEFIT_ID_COLL.Count() > 0)
                            {
                                PAR_BENEFIT_ID_COLL_TMP = new NewBisPASvcRef.U_BENEFIT_ID_COLLECTION();

                                var GetData = from getData in PAR_BENEFIT_ID_COLL
                                              where !string.IsNullOrEmpty(getData.BENEFIT_MESSAGE?.MESSAGE) || !string.IsNullOrEmpty(getData.BENEFIT_PERSON?.NAME)
                                              orderby getData.SPOUSE_FLG == null ? 'N' : getData.SPOUSE_FLG ascending, getData.APP_BENEFIT.SEQ descending
                                              select getData;
                                if (GetData != null && GetData.Count() > 0)
                                {
                                    PAR_BENEFIT_ID_COLL_TMP.AddRange(GetData.ToArray());

                                    for (int i = 0; i < PAR_BENEFIT_ID_COLL_TMP.Count(); i++)
                                    {
                                        PAR_BENEFIT_ID_COLL_TMP[i].APP_BENEFIT.SEQ = i;
                                    }

                                    DisplayBenefitPDPanal();
                                }


                            }
                        }
                        else
                        {
                            if (PAR_BENEFIT_ID_COLL != null && PAR_BENEFIT_ID_COLL.Count() > 0)
                            {
                                PAR_BENEFIT_ID_COLL_TMP = new NewBisPASvcRef.U_BENEFIT_ID_COLLECTION();

                                var GetData = from getData in PAR_BENEFIT_ID_COLL
                                              orderby getData.APP_BENEFIT.SEQ ascending
                                              select getData;
                                if (GetData != null && GetData.Count() > 0)
                                {
                                    PAR_BENEFIT_ID_COLL_TMP.AddRange(GetData.ToArray());
                                    DisplayBenefitPanal();
                                }


                            }
                        }


                        ClickTabBenefit = true;
                    }

                }
                else if (tabMain.SelectedTab.Name == "tabUnderWriteData" || tabMain.SelectedTab.Name == "tabSmart")
                {
                    txtNowSuspensePremium.Cursor = Cursors.Hand;
                    tabUnderWriteMain.SelectTab("tabUnderWrite");

                    //Get ค่าเบี้บ ค่าทุน เงินบัญชีพัก ==========================================================
                    if (ClickTabUnderWriteMain == false)
                    {
                        GetValueForUnderWrite();
                    }
                    else
                    {
                        if (PAR_CAL_SUMM == true)
                        {
                            GetValueForUnderWrite();
                            PAR_CAL_SUMM = false;
                        }
                    }
                    //END Get ค่าเบี้บ ค่าทุน เงินบัญชีพัก =======================================================

                    if (ClickTabUnderWriteMain == false)
                    {
                        string channelType = cmbChannelType.SelectedValue.ToString();
                        var channelList = new string[] { "PO", "PN" };
                        if (channelList.Contains(channelType))
                        {
                            if (PAR_END_PROCESS == 'Y')
                            {
                                btnSaveSummaryDetail.Visible = true;
                            }
                            /* var wUnderwrite = PAR_W_SUMMARY.SUMMARY_UNDERWRITER_Collection;
                             if (wUnderwrite != null && wUnderwrite.Any())
                             {
                                 var lastUnderwrite = wUnderwrite.OrderByDescending(i => i.SUMMARY_ID).First();
                                 cmbUnderWriteID.SelectedValue = lastUnderwrite.UND_ID == null ? UserID : lastUnderwrite.UND_ID;
                             }*/
                        }
                        else
                        {
                            btnSaveSummaryDetail.Visible = false;
                        }

                        if (PAR_W_UNDERWRITE_ID_COLL != null && PAR_W_UNDERWRITE_ID_COLL.Count() > 0)
                        {
                            GetWUnderwriteID(PAR_W_UNDERWRITE_ID_COLL.First());

                            txtSummaryDetailSeq.Text = "";
                            txtSummaryDetail.Text = "";
                            btnSummaryDetail.Text = "เพิ่มข้อมูล";
                            DisplayOfPanelWSummaryDetail(PAR_W_SUMMARY_DETAIL_COLL);
                            GetUAPIDDocumentFromBranch(PAR_W_UNDERWRITE_ID_COLL.First().SUBUNDERWRITE_ID_Collection.First().SUMMARY_Collection.First());
                        }

                        ClickTabUnderWriteMain = true;
                        ClickTabUnderWrite = true;
                    }
                    //ตรวจสอบเงินเกิน เพื่อทำการเก็บค่าการจ่ายคืน ======================================================
                    if (tabMain.SelectedTab.Name == "tabSmart" && cmbSummryStatus.SelectedValue != null)
                    {

                        Decimal returnPremiumPayment = txtPayMentReturnPrem.Text == "" ? 0 : Convert.ToDecimal(txtPayMentReturnPrem.Text.Replace(",", ""));
                        Decimal returnPremium = string.IsNullOrEmpty(txtOverPremium.Text) ? 0 : Convert.ToDecimal(txtOverPremium.Text);
                        if (cmbSummryStatus.SelectedValue.ToString() == "IF")
                        {
                            if (returnPremiumPayment != returnPremium)
                            {
                                //**if (returnPremium >= 100)
                                //{
                                txtPayMentReturnPrem.Text = returnPremium.ToString("n2");
                                txtPayMentPremium.Text = txtTotalPremium.Text;
                                txtPayMentSuspense.Text = txtSuspensePremium.Text;
                                gbPayMentReturnPrm.Visible = true;
                                //  }
                            }
                        }
                        else if (cmbSummryStatus.SelectedValue.ToString() == "CC" || cmbSummryStatus.SelectedValue.ToString() == "DC" || cmbSummryStatus.SelectedValue.ToString() == "PP" || cmbSummryStatus.SelectedValue.ToString() == "EX" || cmbSummryStatus.SelectedValue.ToString() == "NT")
                        {
                            txtPayMentReturnPrem.Text = txtSuspensePremium.Text; ;
                            txtPayMentPremium.Text = txtTotalPremium.Text;
                            txtPayMentSuspense.Text = txtSuspensePremium.Text; ;
                            gbPayMentReturnPrm.Visible = true;
                        }
                    }

                    //จบตรวจสอบเงินเกิน เพื่อทำการเก็บค่าการจ่ายคืน ======================================================

                }
                else if (tabMain.SelectedTab.Name == "tabAppName")
                {
                    if (ClickTabAppName == false)
                    {
                        long? AppNameID = cmbAppNameID.SelectedValue == null || cmbAppNameID.SelectedValue.ToString() == "" ? 0 : Convert.ToInt64(cmbAppNameID.SelectedValue.ToString());
                        if (AppNameID != 0)
                        {
                            NewBisPASvcRef.ProcessResult pr = new NewBisPASvcRef.ProcessResult();
                            questionColl = new NewBisPASvcRef.QUESTION_DATA_COLL();
                            using (NewBisPASvcRef.NewBisPASvcClient client = new NewBisPASvcRef.NewBisPASvcClient())
                            {
                                questionColl = client.GetQuestionPA(out pr, AppNameID.Value);

                                if (pr.Successed == false)
                                {
                                    throw new Exception(pr.ErrorMessage);
                                }
                            }
                            if (questionColl != null && questionColl.Count() > 0)
                            {

                                if (!(PAR_U_APPLICATION_NAME != null && PAR_U_APPLICATION_NAME.UAPPNAME_ID != null))
                                {
                                    PAR_U_APPLICATION_NAME = new NewBisPASvcRef.U_APPLICATION_NAME();
                                    PAR_U_APPLICATION_NAME.APPNAME_ID = Convert.ToInt16(AppNameID.Value);
                                    PAR_U_APPLICATION_NAME.APPLICATION_QUESTIONAIRE_Collection = new NewBisPASvcRef.U_APPLICATION_QUESTIONAIRE_Collection();

                                    foreach (NewBisPASvcRef.QUESTION_DATA questionObj in questionColl)
                                    {
                                        NewBisPASvcRef.U_APPLICATION_QUESTIONAIRE uQuestionObj = new NewBisPASvcRef.U_APPLICATION_QUESTIONAIRE();
                                        uQuestionObj.QUESTION_ID = Convert.ToInt16(questionObj.QUESTION_ID.Value);

                                        var GetData = from getData in questionObj.ANSWER_COLL
                                                      where getData.CLEAN == "Y"
                                                      select getData;

                                        NewBisPASvcRef.AUTB_ANSWER_ID answerIDObj = new NewBisPASvcRef.AUTB_ANSWER_ID();
                                        if (GetData != null && GetData.Count() > 0)
                                        {
                                            answerIDObj = GetData.ToArray()[0];
                                        }

                                        uQuestionObj.ANSWER_ID = Convert.ToChar(answerIDObj.ANSWER_ID);
                                        uQuestionObj.CLEAN_FLG = Convert.ToChar(answerIDObj.CLEAN);
                                        PAR_U_APPLICATION_NAME.APPLICATION_QUESTIONAIRE_Collection.Add(uQuestionObj);
                                    }
                                }

                                while (panelAppName.Controls.Count > 0)
                                {
                                    panelAppName.Controls.RemoveAt(0);
                                }

                                int i = 0;

                                var GetDataJoin = from a in PAR_U_APPLICATION_NAME.APPLICATION_QUESTIONAIRE_Collection
                                                  join b in questionColl
                                                  on a.QUESTION_ID equals b.QUESTION_ID
                                                  orderby b.SEQ ascending
                                                  select a;

                                if (GetDataJoin != null && GetDataJoin.Count() > 0)
                                {
                                    PAR_U_APPLICATION_NAME.APPLICATION_QUESTIONAIRE_Collection = new NewBisPASvcRef.U_APPLICATION_QUESTIONAIRE_Collection();
                                    PAR_U_APPLICATION_NAME.APPLICATION_QUESTIONAIRE_Collection.AddRange(GetDataJoin.ToArray());
                                }

                                foreach (NewBisPASvcRef.U_APPLICATION_QUESTIONAIRE obj in PAR_U_APPLICATION_NAME.APPLICATION_QUESTIONAIRE_Collection)
                                {
                                    UserControlsForm.AppNameForm appNameForm = new UserControlsForm.AppNameForm();
                                    appNameForm.Name = "appNameForm";
                                    appNameForm.Location = new Point(0, 0 + (i * 46));
                                    appNameForm.Size = new Size(1100, 46);
                                    appNameForm.Tag = obj;

                                    var GetData = from getData in questionColl
                                                  where getData.QUESTION_ID == obj.QUESTION_ID
                                                  select getData;

                                    NewBisPASvcRef.QUESTION_DATA questionDataObj = new NewBisPASvcRef.QUESTION_DATA();
                                    questionDataObj = GetData.ToArray()[0];

                                    appNameForm.txtAppNameQuestion.Text = questionDataObj.APP_SEQ + " " + questionDataObj.DESCRIPTION_QUESTION;

                                    appNameForm.cmbAppNameAnswer.DataSource = questionDataObj.ANSWER_COLL;
                                    appNameForm.cmbAppNameAnswer.DisplayMember = "DESCRIPTION";
                                    appNameForm.cmbAppNameAnswer.ValueMember = "ANSWER_ID";
                                    appNameForm.cmbAppNameAnswer.SelectedValue = obj.ANSWER_ID.Value.ToString();

                                    String cleanFlag = (from getClean in questionDataObj.ANSWER_COLL where getClean.ANSWER_ID == obj.ANSWER_ID.Value.ToString() && getClean.CLEAN == obj.CLEAN_FLG.Value.ToString() select getClean.CLEAN).First();

                                    if (cleanFlag == "Y")
                                    {
                                        appNameForm.txtAppNameQuestion.BackColor = Color.FromArgb(255, 224, 192);
                                        appNameForm.cmbAppNameAnswer.BackColor = Color.FromArgb(255, 224, 192);
                                        appNameForm.BackColor = Color.FromArgb(255, 224, 192);
                                    }
                                    else
                                    {
                                        appNameForm.txtAppNameQuestion.BackColor = Color.FromArgb(255, 192, 192);
                                        appNameForm.cmbAppNameAnswer.BackColor = Color.FromArgb(255, 192, 192);
                                        appNameForm.BackColor = Color.FromArgb(255, 192, 192);
                                    }

                                    appNameForm.cmbAppNameAnswer.Tag = obj;
                                    appNameForm.cmbAppNameAnswer.SelectionChangeCommitted += new EventHandler(cmbAppNameAnswer_SelectionChangeCommitted);

                                    appNameForm.txtAppNameQuestion.Tag = obj;
                                    appNameForm.txtAppNameQuestion.DoubleClick += new EventHandler(txtAppNameQuestion_DoubleClick);

                                    panelAppName.Controls.Add(appNameForm);
                                    i = i + 1;
                                }


                            }
                            else
                            {
                                panelAppName.Controls.Clear();
                            }
                        }
                        else
                        {
                            var GetData = from getData in PAR_AUTB_APPNAME_ID_COLLECTION
                                          where getData.CHANNEL_TYPE == cmbChannelType.SelectedValue.ToString()
                                          select getData;
                            if (GetData != null && GetData.Count() > 0)
                            {
                                tabMain.SelectTab("tabCustomerData");
                                cmbAppNameID.Focus();
                                throw new Exception("กรุณาระบุเอกสารชุดใบคำขอที่ต้องการ");
                            }
                            else
                            {
                                panelAppName.Controls.Clear();
                            }
                        }



                        ClickTabAppName = true;
                    }
                }



            }
            catch (Exception ex)
            {
                SetMsgException(ex);
            }
        }


        private void GetUAPIDDocumentFromBranch(NewBisPASvcRef.W_SUMMARY wSummaryObj)
        {
            if (wSummaryObj.AP_ID_Collection != null && wSummaryObj.AP_ID_Collection.Count() > 0)
            {
                gbAPDate.Visible = true;
                ckbAPTmn.Checked = false;
                var selUAPID =
                    from uAPID in wSummaryObj.AP_ID_Collection
                    where uAPID.TMN == 'N'
                    select uAPID;
                if (selUAPID != null && selUAPID.Count() > 0)
                {
                    var uAPIDObj = new NewBisPASvcRef.U_AP_ID();
                    uAPIDObj = selUAPID.ToArray()[0];
                    ckbAPTmn.Visible = true;
                    txtAPAnswerLimitDate.Text = Utility.dateTimeToString(uAPIDObj.ANSWER_LIMIT_DT.Value, "dd/MM/yyyy", "BU");

                    PK_UAP_ID = uAPIDObj.UAP_ID;
                    UAP_TRN_DT = uAPIDObj.AP_TRN_DT;
                }
                else
                {
                    ckbAPTmn.Visible = false;
                    txtAPAnswerLimitDate.Text = "";
                    PK_UAP_ID = null;
                    UAP_TRN_DT = null;
                }

            }
        }

        private void GetValueForUnderWrite()
        {
            Decimal totalSumm = 0;
            Decimal totalSummOld = 0;
            Decimal nowSumm = 0;
            Decimal nowPremium = 0;

            Decimal totalSummFree = 0;
            Decimal totalSummOldFree = 0;
            Decimal nowSummFree = 0;

            if (ClickTabPlanData == false)
            {
                nowSumm = OLD_SUMM_PAID == "" || OLD_SUMM_PAID == null ? 0 : Convert.ToDecimal(OLD_SUMM_PAID.Replace(",", ""));
                nowSummFree = OLD_SUMM_FREE == "" || OLD_SUMM_FREE == null ? 0 : Convert.ToDecimal(OLD_SUMM_FREE.Replace(",", ""));
                nowPremium = OLD_PREMIUM_PAID == "" || OLD_PREMIUM_PAID == null ? 0 : Convert.ToDecimal(OLD_PREMIUM_PAID.Replace(",", ""));

                if (!(String.IsNullOrEmpty(OLD_ISU_DT_FREE)))
                {
                    txtUWIsuDate.Text = OLD_ISU_DT_FREE;
                }
                if (!(String.IsNullOrEmpty(OLD_ISU_DT_PAID)))
                {
                    txtUWIsuDate.Text = OLD_ISU_DT_PAID;
                }
            }
            else
            {
                nowSumm = txtPaidSumm.Text.Trim() == "" ? 0 : Convert.ToDecimal(txtPaidSumm.Text.Trim().Replace(",", ""));
                nowSummFree = txtFreeSumm.Text.Trim() == "" ? 0 : Convert.ToDecimal(txtFreeSumm.Text.Trim().Replace(",", ""));
                nowPremium = txtTotalPremium.Text.Trim() == "" ? 0 : Convert.ToDecimal(txtTotalPremium.Text.Trim().Replace(",", ""));

                if (!(String.IsNullOrEmpty(txtPaidIsuDate.Text)))
                {
                    txtUWIsuDate.Text = txtPaidIsuDate.Text;
                }
                if (!(String.IsNullOrEmpty(txtFreeIsuDate.Text)))
                {
                    txtUWIsuDate.Text = txtFreeIsuDate.Text;
                }
            }


            NewBisPASvcRef.ProcessResult pr = new NewBisPASvcRef.ProcessResult();
            decimal? totalSummBuyOutPut = 0;
            decimal? totalSummFreeOutPut = 0;
            DateTime? birthDate = Utility.StringToDateTime(txtBirthDt.Text.Trim(), "BU");
            using (NewBisPASvcRef.NewBisPASvcClient client = new NewBisPASvcRef.NewBisPASvcClient())
            {
                pr = client.GetTotalSumm(out totalSummBuyOutPut, txtIdcardNo.Text.Trim(), txtName.Text.Trim(), txtSurname.Text.Trim(), cmbSex.SelectedValue.ToString(), birthDate, txtAppNo.Text.Trim(), cmbChannelType.SelectedValue.ToString(), "N");
                if (pr.Successed == false)
                {
                    throw new Exception(pr.ErrorMessage);
                }
                totalSummOld = totalSummBuyOutPut == null ? 0 : totalSummBuyOutPut.Value;

                //สำหรับงาน PA บัตรเครดิต ====================================================
                var channelType = cmbChannelType.SelectedValue.ToString();
                if (channelType == "PD" || channelType == "PF")
                {
                    pr = client.GetTotalSumm(out totalSummFreeOutPut, txtIdcardNo.Text.Trim(), txtName.Text.Trim(), txtSurname.Text.Trim(), cmbSex.SelectedValue.ToString(), birthDate, txtAppNo.Text.Trim(), cmbChannelType.SelectedValue.ToString(), "Y");
                    if (pr.Successed == false)
                    {
                        throw new Exception(pr.ErrorMessage);
                    }
                    totalSummOldFree = totalSummFreeOutPut == null ? 0 : totalSummFreeOutPut.Value;
                }
                //END สำหรับงาน PA บัตรเครดิต ================================================
            }

            totalSumm = (nowSumm + totalSummOld);
            totalSummFree = (nowSummFree + totalSummOldFree);

            txtNowPremium.Text = nowPremium.ToString("n0");

            txtNowSumm.Text = nowSumm.ToString("n0");
            txtTotalSummOld.Text = totalSummOld.ToString("n0");
            txtTotalSumm.Text = totalSumm.ToString("n0");

            txtNowSummFree.Text = nowSummFree.ToString("n0");
            txtTotalSummOldFree.Text = totalSummOldFree.ToString("n0");
            txtTotalSummFree.Text = totalSummFree.ToString("n0");


            bool haveData = false;
            Decimal? suspensePremium = null;
            DateTime? suspenseDate = null;

            if (PAR_SUSPENSE_CHECK == true)
            {
                haveData = PAR_SUSPENSE_DETAIL;
                suspensePremium = PAR_PREMIUM_SUSPENSE;
                suspenseDate = PAR_PAYIN_DATE;
            }
            else
            {
                if (PAR_PA_APPLICATION_ID != null && PAR_PA_APPLICATION_ID.APP_ID != null)
                {
                    if (cmbChannelType.SelectedValue.ToString() == "PO" || cmbChannelType.SelectedValue.ToString() == "PN")
                    {
                        GetSuspenseData(out haveData, out suspensePremium, out suspenseDate, nowPremium);
                    }

                }

            }

            txtNowSuspensePremium.Text = suspensePremium == null ? "0" : suspensePremium.Value.ToString("n0");
            txtSuspenseDate.Text = suspenseDate == null ? "" : Utility.dateTimeToString(suspenseDate.Value, "dd/MM/yyyy", "BU");

        }

        private void GetSuspenseData(out bool haveData, out Decimal? suspensePremium, out DateTime? suspenseDate, Decimal? premium)
        {
            NewBisPASvcRef.ProcessResult pr = new NewBisPASvcRef.ProcessResult();
            String status = "";
            status = "WT";
            if (PAR_PA_APPLICATION_ID != null && PAR_PA_APPLICATION_ID.APP_ID != null)
            {
                if (PAR_PA_APPLICATION_ID.APPLICATION_Collection != null && PAR_PA_APPLICATION_ID.APPLICATION_Collection.Count() > 0)
                {
                    if (PAR_PA_APPLICATION_ID.APPLICATION_Collection[0].STATUS_ID_Collection != null && PAR_PA_APPLICATION_ID.APPLICATION_Collection[0].STATUS_ID_Collection.Count() > 0)
                    {
                        status = PAR_PA_APPLICATION_ID.APPLICATION_Collection[0].STATUS_ID_Collection[0].STATUS;
                    }
                }
            }

            var renewRepository = new RenewalRepository();

            var appNo = txtAppNo.Text.Trim();
            var channelType = cmbChannelType.SelectedValue.ToString();
            if (renewRepository.IsRenewApplication(appNo, channelType))
            {
                var isEndProcess = PAR_END_PROCESS == 'Y' ? true : false;
                var transection = renewRepository.GetRenewalPaymentTransection(appNo, channelType, isEndProcess, NewBISSvcRef.ERenewStatusCase.RewnewOrAllowToGrant);
                if (transection != null)
                {
                    haveData = true;
                    PAR_PREMIUM_SUSPENSE = suspensePremium = transection.PremiumReceive;
                    PAR_PAYIN_DATE = suspenseDate = transection.Transections.Max(item => item.PAY_DT).Value;

                }
                else
                {
                    haveData = false;
                    PAR_PREMIUM_SUSPENSE = suspensePremium = 0;
                    PAR_PAYIN_DATE = suspenseDate = null;
                }

            }
            else
            {
                using (NewBisPASvcRef.NewBisPASvcClient client = new NewBisPASvcRef.NewBisPASvcClient())
                {
                    pr = client.GetSuspenseData(out haveData, out suspensePremium, out suspenseDate, txtAppNo.Text.Trim(), cmbChannelType.SelectedValue.ToString(), status, premium);
                    if (pr.Successed == false)
                    {
                        throw new Exception(pr.ErrorMessage);
                    }

                    PAR_SUSPENSE_CHECK = true;
                    PAR_SUSPENSE_DETAIL = haveData;
                    PAR_PREMIUM_SUSPENSE = suspensePremium;
                    PAR_PAYIN_DATE = suspenseDate;
                }
            }
            //hard Code
            //haveData = true;
            //PAR_PREMIUM_SUSPENSE = suspensePremium = 7000;
            //PAR_PAYIN_DATE = suspenseDate = DateTime.Now;                                                                                                    
        }

        void txtAppNameQuestion_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                TextBox txtAppNameQuestion = (TextBox)sender;
                NewBisPASvcRef.U_APPLICATION_QUESTIONAIRE obj = (NewBisPASvcRef.U_APPLICATION_QUESTIONAIRE)txtAppNameQuestion.Tag;
                MessageBox.Show(obj.ANSWER_ID.Value.ToString() + "/Clean Flag = " + obj.CLEAN_FLG.Value.ToString());
            }
            catch (Exception ex)
            {
                SetMsgException(ex);
            }
        }

        void cmbAppNameAnswer_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                ComboBox cmbAppNameAnswer = (ComboBox)sender;
                NewBisPASvcRef.U_APPLICATION_QUESTIONAIRE obj = (NewBisPASvcRef.U_APPLICATION_QUESTIONAIRE)cmbAppNameAnswer.Tag;


                var GetData = from getData in questionColl
                              where getData.QUESTION_ID == obj.QUESTION_ID
                              select getData;

                NewBisPASvcRef.AUTB_ANSWER_ID_COLLECTION answerColl = new NewBisPASvcRef.AUTB_ANSWER_ID_COLLECTION();
                answerColl.AddRange(GetData.ToArray()[0].ANSWER_COLL.ToArray());

                var GetAnswer = from getAnswer in answerColl
                                where getAnswer.ANSWER_ID == cmbAppNameAnswer.SelectedValue.ToString()
                                select getAnswer;

                NewBisPASvcRef.AUTB_ANSWER_ID answerObj = new NewBisPASvcRef.AUTB_ANSWER_ID();
                answerObj = GetAnswer.ToArray()[0];

                obj.ANSWER_ID = Convert.ToChar(answerObj.ANSWER_ID);
                obj.CLEAN_FLG = Convert.ToChar(answerObj.CLEAN);

                if (panelAppName.Controls.Count > 0)
                {
                    foreach (Control c in panelAppName.Controls)
                    {
                        if (c.Name == "appNameForm")
                        {
                            UserControlsForm.AppNameForm appNameForm = (UserControlsForm.AppNameForm)c;
                            NewBisPASvcRef.U_APPLICATION_QUESTIONAIRE questionaireObj = new NewBisPASvcRef.U_APPLICATION_QUESTIONAIRE();
                            questionaireObj = (NewBisPASvcRef.U_APPLICATION_QUESTIONAIRE)appNameForm.Tag;
                            if (questionaireObj.QUESTION_ID == obj.QUESTION_ID)
                            {
                                if (answerObj.CLEAN == "Y")
                                {
                                    appNameForm.txtAppNameQuestion.BackColor = Color.FromArgb(255, 224, 192);
                                    appNameForm.cmbAppNameAnswer.BackColor = Color.FromArgb(255, 224, 192);
                                    appNameForm.BackColor = Color.FromArgb(255, 224, 192);
                                }
                                else
                                {
                                    appNameForm.txtAppNameQuestion.BackColor = Color.FromArgb(255, 192, 192);
                                    appNameForm.cmbAppNameAnswer.BackColor = Color.FromArgb(255, 192, 192);
                                    appNameForm.BackColor = Color.FromArgb(255, 192, 192);
                                }
                            }

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                SetMsgException(ex);
            }
        }

        private void txtFreePlcode2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                FindAndSetPlanInfo();
            }
        }

        private void txtPaidPlcode2_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyValue == 13)
                {
                    String plBlock = cmbPaidPlBlock.SelectedValue == null ? "" : cmbPaidPlBlock.SelectedValue.ToString();

                    if (plBlock == "")
                    {
                        cmbPaidPlBlock.Focus();
                        throw new Exception("กรุณาระบุชนิดแบบประกัน");
                    }
                    if (txtPaidPlcode.Text.Trim() == "")
                    {
                        txtPaidPlcode.Focus();
                        throw new Exception("กรุณาระบุรหัสแบบประกัน");
                    }
                    if (txtPaidPlcode2.Text.Trim() == "")
                    {
                        txtPaidPlcode2.Focus();
                        throw new Exception("กรุณาระบุรหัสแบบประกัน");
                    }
                    //if (isuDate == null)
                    //{
                    //    txtPaidIsuDate.Focus();
                    //    throw new Exception("กรุณาระบุวันเริ่มคุ้มครอง");
                    //}
                    if (txtPaidIsuDate.Text.Trim() == "")
                    {
                        txtPaidIsuDate.Text = txtAppSignDt.Text.Trim();
                    }
                    if (cmbChannelType.SelectedValue == null || cmbChannelType.SelectedValue.ToString() == "")
                    {
                        cmbChannelType.Focus();
                        throw new Exception("กรุณาระบุช่องทางการขายที่ท่านต้องการ");
                    }
                    String channelType = cmbChannelType.SelectedValue.ToString();

                    DateTime? isuDate = txtPaidIsuDate.Text.Trim() == "" ? null : Utility.StringToDateTime(txtPaidIsuDate.Text.Trim(), "BU");

                    NewBisPASvcRef.ProcessResult pr = new NewBisPASvcRef.ProcessResult();
                    NewBisPASvcRef.ZTB_PLAN_PM_COLLECTION planColl = new NewBisPASvcRef.ZTB_PLAN_PM_COLLECTION();
                    using (NewBisPASvcRef.NewBisPASvcClient client = new NewBisPASvcRef.NewBisPASvcClient())
                    {
                        pr = client.GetZtbPlanOfPM(out planColl, plBlock, txtPaidPlcode.Text.Trim(), txtPaidPlcode2.Text.Trim(), isuDate, channelType);
                        if (pr.Successed == false)
                        {
                            throw new Exception(pr.ErrorMessage);
                        }
                    }

                    if (planColl != null && planColl.Count() > 0)
                    {
                        NewBisPASvcRef.ZTB_PLAN_PM planObj = new NewBisPASvcRef.ZTB_PLAN_PM();
                        planObj = planColl[0];

                        cmbPaidCtmType.SelectedValue = planObj.CTM_TYPE;
                        cmbPaidPlBlock.SelectedValue = planObj.PL_BLOCK;
                        txtPaidPlcode.Text = planObj.PL_CODE;
                        txtPaidPlcode2.Text = planObj.PL_CODE2;
                        txtPaidPlanName.Text = planObj.BLA_TITLE;
                        cmbPaidPmode.SelectedValue = planObj.P_MODE == null ? "" : planObj.P_MODE.Value.ToString();
                        if (planObj.PL_BLOCK != "K")
                        {
                            txtPaidSumm.Text = planObj.SUM_MAX == null ? "0" : planObj.SUM_MAX.Value.ToString("n0");
                        }
                        txtPaidMarketingType.Text = planObj.MARKETING_TYPE;
                        txtPaidPolicyHolding.Text = planObj.POLICY_HOLDING;
                        txtPolicyHolding.Text = planObj.POLICY_HOLDING;
                        txtPaidSpouseFlg.Text = planObj.OPL_CODE2;
                        if (planObj.FREE_FLG != "N")
                        {
                            txtFreePlcode2.Focus();
                            throw new Exception("แบบประกันไม่ถูกต้องต้องเป็นแบบซื้อเพิ่ม");
                        }
                        ckbLifeBuy.Checked = true;


                        var ulifePlan = PAR_PA_APPLICATION_ID?.APPLICATION_Collection?.Where(item => item.LIFE_ID != null && item.LIFE_ID.UAPP_ID != null).FirstOrDefault();
                        if (ulifePlan == null && channelType == "KB")
                        {
                            int cusAge = txtStAge.Text.Trim() == "" ? 0 : Convert.ToInt32(txtStAge.Text.Trim());
                            FindQuestion(planObj.PL_BLOCK, planObj.PL_CODE, planObj.PL_CODE2, cusAge);
                        }
                    }
                    else
                    {
                        txtPaidPlcode2.Focus();
                        throw new Exception("ไม่พบแบบประกันที่ท่านต้องการ");
                    }
                    if (string.IsNullOrEmpty(txtFreePlcode.Text.Trim()) && string.IsNullOrEmpty(txtFreePlcode2.Text.Trim()))
                    {
                        if (PAR_PA_APPLICATION_ID.APP_ID != null && channelType == "PD")
                        {
                            AutoPlanFreeInfo();
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                PLAN_ERROR = true;
                txtPaidPlanName.Text = "";
                SetMsgException(ex);
            }
        }


        private void FindAndSetPlanInfo()
        {
            try
            {
                String plBlock = cmbFreePlBlock.SelectedValue == null ? "" : cmbFreePlBlock.SelectedValue.ToString();

                if (plBlock == "")
                {
                    cmbFreePlBlock.Focus();
                    throw new Exception("กรุณาระบุชนิดแบบประกัน");
                }
                if (txtFreePlcode.Text.Trim() == "")
                {
                    txtFreePlcode.Focus();
                    throw new Exception("กรุณาระบุรหัสแบบประกัน");
                }
                if (txtFreePlcode2.Text.Trim() == "")
                {
                    txtFreePlcode2.Focus();
                    throw new Exception("กรุณาระบุรหัสแบบประกัน");
                }
                //if (isuDate == null)
                //{
                //    txtFreeIsuDate.Focus();
                //    throw new Exception("กรุณาระบุวันเริ่มคุ้มครอง");
                //}
                if (cmbChannelType.SelectedValue == null || cmbChannelType.SelectedValue.ToString() == "")
                {
                    cmbChannelType.Focus();
                    throw new Exception("กรุณาระบุช่องทางการขายที่ท่านต้องการ");
                }

                txtFreeIsuDate.Text = txtAppSignDt.Text.Trim();

                DateTime? isuDate = txtFreeIsuDate.Text.Trim() == "" ? null : Utility.StringToDateTime(txtFreeIsuDate.Text.Trim(), "BU");
                String channelType = cmbChannelType.SelectedValue.ToString();
                NewBisPASvcRef.ProcessResult pr = new NewBisPASvcRef.ProcessResult();
                NewBisPASvcRef.ZTB_PLAN_PM_COLLECTION planColl = new NewBisPASvcRef.ZTB_PLAN_PM_COLLECTION();
                using (NewBisPASvcRef.NewBisPASvcClient client = new NewBisPASvcRef.NewBisPASvcClient())
                {
                    pr = client.GetZtbPlanOfPM(out planColl, plBlock, txtFreePlcode.Text.Trim(), txtFreePlcode2.Text.Trim(), isuDate, channelType);
                    if (pr.Successed == false)
                    {
                        throw new Exception(pr.ErrorMessage);
                    }
                }

                if (planColl != null && planColl.Count() > 0)
                {
                    NewBisPASvcRef.ZTB_PLAN_PM planObj = new NewBisPASvcRef.ZTB_PLAN_PM();
                    planObj = planColl[0];

                    cmbFreeCtmType.SelectedValue = planObj.CTM_TYPE;
                    cmbFreePlBlock.SelectedValue = planObj.PL_BLOCK;
                    txtFreePlcode.Text = planObj.PL_CODE;
                    txtFreePlcode2.Text = planObj.PL_CODE2;
                    txtFreePlanName.Text = planObj.BLA_TITLE;
                    cmbFreePmode.SelectedValue = planObj.P_MODE == null ? "" : planObj.P_MODE.Value.ToString();
                    txtFreeSumm.Text = planObj.SUM_MAX == null ? "0" : planObj.SUM_MAX.Value.ToString("n0");
                    txtFreeMarketingType.Text = planObj.MARKETING_TYPE;
                    txtFreePolicyHolding.Text = planObj.POLICY_HOLDING;
                    txtPolicyHolding.Text = planObj.POLICY_HOLDING;
                    txtFreeSpouseFlg.Text = planObj.OPL_CODE2;


                    if (!IsChannelTypeFreePolicy(channelType))
                    {
                        if (planObj.FREE_FLG != "Y")
                        {
                            txtFreePlcode2.Focus();
                            throw new Exception("แบบประกันไม่ถูกต้องต้องเป็นแบบแถมฟรี");
                        }
                    }
                    ckbLifeFree.Checked = true;
                }
                else
                {
                    txtFreePlcode2.Focus();
                    throw new Exception("ไม่พบแบบประกันที่ท่านต้องการ");
                }

            }
            catch (Exception ex)
            {
                PLAN_ERROR = true;
                txtFreePlanName.Text = "";
                SetMsgException(ex);
            }
        }
        private void AutoPlanFreeInfo()
        {
            txtFreePlcode.Text = "018";
            txtFreePlcode2.Text = "00";
            FindAndSetPlanInfo();
        }
        private void btnCalPremium_Click(object sender, EventArgs e)
        {
            var channelType = cmbChannelType.SelectedValue.ToString();
            if (channelType == "PD" || channelType == "PF")
            {
                CalPlanPACreditCard();
            }
            else if (channelType == "PO" || channelType == "PN")
            {
                CalPlanOrdinaryPA();
            }
            else if (channelType == "KB")
            {
                CalPlanKB();
            }

        }

        private void CalPlanOrdinaryPA()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                PLAN_ERROR = false;

                if (cmbPaidCtmType.SelectedValue.ToString() == "")
                {
                    cmbPaidCtmType.Focus();
                    throw new Exception("ระบุ ประเภทลูกค้า ของแบบประกันซื้อเพิ่ม");
                }

                if (cmbSex.SelectedValue.ToString() == "")
                {
                    cmbSex.Focus();
                    throw new Exception("ระบุ เพศผู้เอาประกัน");
                }

                if (cmbPaidPlBlock.SelectedValue.ToString() == "")
                {
                    cmbPaidPlBlock.Focus();
                    throw new Exception("ระบุ ชนิดแบบประกัน ของแบบประกันซื้อเพิ่ม");
                }
                if (txtPaidPlcode.Text.Trim() == "")
                {
                    txtPaidPlcode.Focus();
                    throw new Exception("ระบุ รหัสแบบประกัน ของแบบประกันซื้อเพิ่ม");
                }
                if (txtPaidPlcode2.Text.Trim() == "")
                {
                    txtPaidPlcode2.Focus();
                    throw new Exception("ระบุ รหัสแบบประกัน ของแบบประกันซื้อเพิ่ม");
                }
                if (txtPaidPlanName.Text.Trim() == "")
                {
                    txtPaidPlcode2.Focus();
                    throw new Exception("ระบุ ชื่อแบบประกัน ของแบบประกันซื้อเพิ่ม");
                }
                if (cmbPaidPmode.SelectedValue.ToString() == "")
                {
                    cmbPaidPmode.Focus();
                    throw new Exception("ระบุ งาดการชำระเบี้ย ของแบบประกันซื้อเพิ่ม");
                }
                if (txtPaidSumm.Text.Trim() == "")
                {
                    txtPaidSumm.Focus();
                    throw new Exception("ระบุ ทุนประกัน ของแบบประกันซื้อเพิ่ม");
                }
                if (txtPaidIsuDate.Text.Trim() == "")
                {
                    txtPaidIsuDate.Focus();
                    throw new Exception("ระบุ วันที่เริ่มคุ้มครอง ของแบบประกันซื้อเพิ่ม");
                }



                PAR_PLAN_PAID = new NewBisPASvcRef.U_LIFE_ID();
                String channelType = cmbChannelType.SelectedValue == null ? "" : cmbChannelType.SelectedValue.ToString();
                String appNo = txtAppNo.Text.Trim();
                PolicySvcRef.P_NAME_ID pNameIDObj = new PolicySvcRef.P_NAME_ID();

                pNameIDObj.NAME_ID = NAME_ID;
                pNameIDObj.PRENAME = txtPrename.Text.Trim();
                pNameIDObj.NAME = txtName.Text.Trim();
                pNameIDObj.SURNAME = txtSurname.Text.Trim();
                if (cmbCardType.SelectedValue.ToString() == "1")
                {
                    pNameIDObj.IDCARD_NO = txtIdcardNo.Text.Trim();
                    pNameIDObj.PASSPORT = null;
                }
                else
                {
                    pNameIDObj.IDCARD_NO = null;
                    pNameIDObj.PASSPORT = txtIdcardNo.Text.Trim();
                }
                pNameIDObj.BIRTH_DT = txtBirthDt.Text.Trim() == "" ? (DateTime?)null : Utility.StringToDateTime(txtBirthDt.Text.Trim(), "BU");
                pNameIDObj.SEX = cmbSex.SelectedValue.ToString() == "" ? (char?)null : Convert.ToChar(cmbSex.SelectedValue.ToString());
                pNameIDObj.ST_AGE = txtStAge.Text == "" ? (Decimal?)null : Convert.ToDecimal(txtStAge.Text);

                PolicySvcRef.P_LIFE_ID pLifeIDPaidObj = new PolicySvcRef.P_LIFE_ID();
                pLifeIDPaidObj.PL_BLOCK = cmbPaidPlBlock.SelectedValue.ToString() == "" ? (char?)null : Convert.ToChar(cmbPaidPlBlock.SelectedValue.ToString());
                pLifeIDPaidObj.PL_CODE = txtPaidPlcode.Text.Trim();
                pLifeIDPaidObj.PL_CODE2 = txtPaidPlcode2.Text.Trim();
                pLifeIDPaidObj.SUMM = txtPaidSumm.Text.Trim() == "" ? (long?)null : Convert.ToInt64(txtPaidSumm.Text.Trim().Replace(",", ""));
                pLifeIDPaidObj.P_MODE = cmbPaidPmode.SelectedValue.ToString() == "" ? (int?)null : Convert.ToInt32(cmbPaidPmode.SelectedValue.ToString());
                pLifeIDPaidObj.ISU_DT = txtPaidIsuDate.Text.Trim() == "" ? (DateTime?)null : Utility.StringToDateTime(txtPaidIsuDate.Text.Trim(), "BU");
                pLifeIDPaidObj.FREE_FLG = "N";
                pLifeIDPaidObj.CTM_TYPE = cmbPaidCtmType.SelectedValue.ToString() == "" ? (char?)null : Convert.ToChar(cmbPaidCtmType.SelectedValue.ToString());
                pLifeIDPaidObj.OCP_CLASS = txtOcpClass.Text == "" ? ' ' : Convert.ToChar(txtOcpClass.Text);



                var repo = new Repository();

                if (repo.IsRiskOfOccupation(txtOcpType.Text, txtOcpClass.Text, pLifeIDPaidObj.PL_BLOCK.ToString(), pLifeIDPaidObj.PL_CODE, pLifeIDPaidObj.PL_CODE2))
                {
                    txtPaidIsuDate.Focus();
                    throw new Exception("อาชีพมีความเสี่ยงไม่สามารถซื้อแบบประกันนี้ได้");
                }



                ITUtility.ProcessResult pr = new ProcessResult();
                String msgError = "";
                Decimal? totalSumm = 0;
                using (PolicySvcRef.PolicySvcClient client = new PolicySvcRef.PolicySvcClient())
                {
                    pr = client.CalPremiumPlanPO(ref pNameIDObj, ref pLifeIDPaidObj, ref channelType, ref appNo, out msgError, out totalSumm);
                    if (pr.Successed == false)
                    {
                        throw new Exception(pr.ErrorMessage);
                    }

                    PAR_CAL_SUMM = true;
                    PAR_TOTAL_SUMM_FREE = 0;
                    PAR_TOTAL_SUMM_BUY = totalSumm;

                    if (String.IsNullOrEmpty(msgError))
                    {
                        if (PAR_U_CAL_ERROR != null)
                        {
                            if (PAR_U_CAL_ERROR.APP_ID == null)
                            {
                                PAR_U_CAL_ERROR = new NewBisPASvcRef.U_CAL_ERROR();
                            }
                            else
                            {
                                PAR_U_CAL_ERROR.TMN = 'Y';
                                PAR_U_CAL_ERROR.TMN_ID = UserID;
                                PAR_U_CAL_ERROR.TMN_DT = DateTime.Now;
                            }
                        }
                    }
                    else
                    {
                        PAR_U_CAL_ERROR = new NewBisPASvcRef.U_CAL_ERROR();
                        PAR_U_CAL_ERROR.APP_ID = null;
                        PAR_U_CAL_ERROR.ERROR_DT = DateTime.Now;
                        PAR_U_CAL_ERROR.ERROR_ID = UserID;
                        PAR_U_CAL_ERROR.TMN = 'N';
                        PAR_U_CAL_ERROR.U_CAL_ERROR_DETAIL_Coll = new NewBisPASvcRef.U_CAL_ERROR_DETAIL_Collection();
                        String[] msqErrorArr = msgError.Split(',');
                        if (msqErrorArr != null && msqErrorArr.Count() > 0)
                        {
                            foreach (String obj in msqErrorArr.Distinct())
                            {
                                if (!(String.IsNullOrEmpty(obj)))
                                {
                                    NewBisPASvcRef.U_CAL_ERROR_DETAIL uCalErrDetailObj = new NewBisPASvcRef.U_CAL_ERROR_DETAIL();
                                    uCalErrDetailObj.U_CAL_ERROR_ID = null;
                                    uCalErrDetailObj.ERROR_CODE = obj;
                                    PAR_U_CAL_ERROR.U_CAL_ERROR_DETAIL_Coll.Add(uCalErrDetailObj);
                                }


                            }
                        }

                    }
                }

                if (PAR_U_CAL_ERROR != null && PAR_U_CAL_ERROR.ERROR_DT != null)
                {
                    if (PAR_U_CAL_ERROR.TMN == 'N')
                    {
                        String strError = "";
                        if (PAR_U_CAL_ERROR.U_CAL_ERROR_DETAIL_Coll != null && PAR_U_CAL_ERROR.U_CAL_ERROR_DETAIL_Coll.Count() > 0)
                        {
                            int i = 0;
                            foreach (NewBisPASvcRef.U_CAL_ERROR_DETAIL obj in PAR_U_CAL_ERROR.U_CAL_ERROR_DETAIL_Coll)
                            {
                                var GetData = from getData in PAR_AUTB_DATADIC_DET_COLLECTION
                                              where getData.COL_NAME == "ERROR_CODE"
                                              && getData.CODE == obj.ERROR_CODE
                                              select getData;
                                if (GetData != null && GetData.Count() > 0)
                                {
                                    i = i + 1;
                                    strError = strError + i.ToString() + ". " + GetData.ToArray()[0].DESCRIPTION + "\n";
                                }
                            }
                        }

                        MessageBox.Show("มีข้อผิดพลาดในการคำนวนเบี้ยประกันมีรายละเอียดดังนี้\n" + strError);
                    }
                }

                if (cmbDataCus.SelectedValue.ToString() == "2")
                {
                    if (pNameIDObj != null && pNameIDObj.NAME_ID != null)
                    {
                        throw new Exception("ไม่สามารถคำนวนเบี้ยประกันได้เนื่องจากลูกค้ามีข้อมูลเก่าแต่ท่านเลือกประเภทลูกค้าเป็นลูกค้าใหม่");
                    }
                }

                Decimal? totalPremium = 0;
                Decimal? suspensePremium = 0;
                Decimal? overPremium = 0;

                if (pLifeIDPaidObj == null)
                {
                    throw new Exception("ไม่สามารถคำนวนเบี้ยประกันได้กรุณาติกต่อ IT Tel 8518");
                }
                if (String.IsNullOrEmpty(pLifeIDPaidObj.PL_CODE))
                {
                    throw new Exception("ไม่สามารถคำนวนเบี้ยประกันได้กรุณาติกต่อ IT Tel 8518");
                }
                txtStAge.Text = pLifeIDPaidObj.ST_AGE == null ? "" : pLifeIDPaidObj.ST_AGE.Value.ToString();
                txtPaidSumm.Text = pLifeIDPaidObj.SUMM == null ? "" : pLifeIDPaidObj.SUMM.Value.ToString("n0");
                txtPaidPolYr.Text = pLifeIDPaidObj.POL_YR == null ? "" : pLifeIDPaidObj.POL_YR.Value.ToString();
                txtPaidPolLt.Text = pLifeIDPaidObj.POL_LT == null ? "" : pLifeIDPaidObj.POL_LT.Value.ToString();
                txtPaidPayTerm.Text = pLifeIDPaidObj.PAY_TERM == null ? "" : pLifeIDPaidObj.PAY_TERM.Value.ToString();
                txtPaidLastPayDate.Text = pLifeIDPaidObj.LASTPAY_DT == null ? "" : Utility.dateTimeToString(pLifeIDPaidObj.LASTPAY_DT.Value, "dd/MM/yyyy", "BU");
                txtPaidAssTerm.Text = pLifeIDPaidObj.ASS_TERM == null ? "" : pLifeIDPaidObj.ASS_TERM.Value.ToString();
                txtPaidAssDate.Text = pLifeIDPaidObj.ASS_DT == null ? "" : Utility.dateTimeToString(pLifeIDPaidObj.ASS_DT.Value, "dd/MM/yyyy", "BU");
                txtPaidMatTerm.Text = pLifeIDPaidObj.MAT_TERM == null ? "" : pLifeIDPaidObj.MAT_TERM.Value.ToString();
                txtPaidMatDate.Text = pLifeIDPaidObj.MAT_DT == null ? "" : Utility.dateTimeToString(pLifeIDPaidObj.MAT_DT.Value, "dd/MM/yyyy", "BU");
                txtPaidNxtDueDate.Text = pLifeIDPaidObj.NXTDUE_DT == null ? "" : Utility.dateTimeToString(pLifeIDPaidObj.NXTDUE_DT.Value, "dd/MM/yyyy", "BU");
                txtPaidPremium.Text = pLifeIDPaidObj.PREMIUM == null ? "0" : pLifeIDPaidObj.PREMIUM.Value.ToString("n0");
                txtPaidSingle.Text = pLifeIDPaidObj.SINGLE == null ? "" : pLifeIDPaidObj.SINGLE.Value.ToString();
                txtPaidProtect.Text = pLifeIDPaidObj.PROTECT;
                txtPaidMedical.Text = pLifeIDPaidObj.MEDICAL == null ? "" : pLifeIDPaidObj.MEDICAL.Value.ToString();
                txtPaidStandard.Text = pLifeIDPaidObj.STANDARD == null ? "" : pLifeIDPaidObj.STANDARD.Value.ToString();
                txtPaidReinsure.Text = pLifeIDPaidObj.REINSURE == null ? "" : pLifeIDPaidObj.REINSURE.Value.ToString();
                txtPaidExcludeTpd.Text = pLifeIDPaidObj.EXCLUDE_TPD;
                txtPaidDepositInst.Text = pLifeIDPaidObj.DEPOSIT_INST == null ? "" : pLifeIDPaidObj.DEPOSIT_INST.Value.ToString("n0");
                txtPaidPensionDate.Text = pLifeIDPaidObj.PENSION_DT == null ? "" : Utility.dateTimeToString(pLifeIDPaidObj.PENSION_DT.Value, "dd/MM/yyyy", "BU");
                txtPaidPolicyHolding.Text = pLifeIDPaidObj.POLICY_HOLDING;
                txtPaidMarketingType.Text = pLifeIDPaidObj.MARKETING_TYPE;

                if (pLifeIDPaidObj.PREMIUM != null)
                {
                    totalPremium = totalPremium + pLifeIDPaidObj.PREMIUM.Value;
                }
                PAR_PLAN_PAID.PL_BLOCK = pLifeIDPaidObj.PL_BLOCK == null ? "" : pLifeIDPaidObj.PL_BLOCK.Value.ToString();
                PAR_PLAN_PAID.PL_CODE = pLifeIDPaidObj.PL_CODE;
                PAR_PLAN_PAID.PL_CODE2 = pLifeIDPaidObj.PL_CODE2;
                PAR_PLAN_PAID.SUMM = pLifeIDPaidObj.SUMM;
                PAR_PLAN_PAID.INFSUMM = pLifeIDPaidObj.INFSUMM;
                PAR_PLAN_PAID.PREMIUM = pLifeIDPaidObj.PREMIUM;
                PAR_PLAN_PAID.P_MODE = pLifeIDPaidObj.P_MODE;
                PAR_PLAN_PAID.ASS_TERM = pLifeIDPaidObj.ASS_TERM;
                PAR_PLAN_PAID.MAT_TERM = pLifeIDPaidObj.MAT_TERM;
                PAR_PLAN_PAID.PAY_TERM = pLifeIDPaidObj.PAY_TERM;
                PAR_PLAN_PAID.POL_YR = pLifeIDPaidObj.POL_YR;
                PAR_PLAN_PAID.POL_LT = pLifeIDPaidObj.POL_LT;
                PAR_PLAN_PAID.POL_LF_YR = pLifeIDPaidObj.POL_YR;
                PAR_PLAN_PAID.POL_LF_LT = pLifeIDPaidObj.POL_LT;
                PAR_PLAN_PAID.ISU_DT = pLifeIDPaidObj.ISU_DT;
                PAR_PLAN_PAID.ASS_DT = pLifeIDPaidObj.ASS_DT;
                PAR_PLAN_PAID.MAT_DT = pLifeIDPaidObj.MAT_DT;
                PAR_PLAN_PAID.PENSION_DT = pLifeIDPaidObj.PENSION_DT;
                PAR_PLAN_PAID.LASTPAY_DT = pLifeIDPaidObj.LASTPAY_DT;
                PAR_PLAN_PAID.NXTDUE_DT = pLifeIDPaidObj.NXTDUE_DT;
                PAR_PLAN_PAID.DEPOSIT_INST = pLifeIDPaidObj.DEPOSIT_INST;
                PAR_PLAN_PAID.CTM_TYPE = pLifeIDPaidObj.CTM_TYPE == null ? "" : pLifeIDPaidObj.CTM_TYPE.Value.ToString();
                PAR_PLAN_PAID.SINGLE = pLifeIDPaidObj.SINGLE == null ? "" : pLifeIDPaidObj.SINGLE.Value.ToString();
                PAR_PLAN_PAID.PROTECT = pLifeIDPaidObj.PROTECT;
                PAR_PLAN_PAID.MEDICAL = pLifeIDPaidObj.MEDICAL == null ? "" : pLifeIDPaidObj.MEDICAL.Value.ToString();
                PAR_PLAN_PAID.STANDARD = pLifeIDPaidObj.STANDARD == null ? "" : pLifeIDPaidObj.STANDARD.Value.ToString();
                PAR_PLAN_PAID.REINSURE = pLifeIDPaidObj.REINSURE == null ? "" : pLifeIDPaidObj.REINSURE.Value.ToString();
                PAR_PLAN_PAID.EXCLUDE_TPD = pLifeIDPaidObj.EXCLUDE_TPD == null || pLifeIDPaidObj.EXCLUDE_TPD == "" ? (char?)null : Convert.ToChar(pLifeIDPaidObj.EXCLUDE_TPD); ;
                PAR_PLAN_PAID.FREE_FLG = pLifeIDPaidObj.FREE_FLG;
                PAR_PLAN_PAID.POLICY_HOLDING = pLifeIDPaidObj.POLICY_HOLDING;
                PAR_PLAN_PAID.MARKETING_TYPE = pLifeIDPaidObj.MARKETING_TYPE;
                PAR_PLAN_PAID.BLA_TITLE = txtPaidPlanName.Text.Trim();



                bool haveData = false;
                DateTime? suspenseDate = null;

                if (PAR_SUSPENSE_CHECK == true)
                {
                    haveData = PAR_SUSPENSE_DETAIL;
                    suspensePremium = PAR_PREMIUM_SUSPENSE;
                    suspenseDate = PAR_PAYIN_DATE;
                }
                else
                {
                    if (cmbChannelType.SelectedValue.ToString() == "PO" || cmbChannelType.SelectedValue.ToString() == "PN")
                    {
                        GetSuspenseData(out haveData, out suspensePremium, out suspenseDate, totalPremium);
                    }

                }


                lblSuspense.ForeColor = Color.Black;
                txtSuspensePremium.Cursor = Cursors.Default;
                if (haveData == true)
                {
                    lblSuspense.ForeColor = Color.Red;
                    txtSuspensePremium.Cursor = Cursors.Hand;
                }


                txtLifePremium.Text = (totalPremium ?? 0).ToString("n0");
                // checkmode  
                var renewRepo = new RenewalRepository();
                if (!renewRepo.IsRenewApplication(appNo, channelType))
                {
                    totalPremium = (PAR_PA_APPLICATION_ID.APPLICATION_Collection.First().LIFE_ID.P_MODE == 12 ? (totalPremium ?? 0) * 2 : (totalPremium ?? 0));
                }
                txtTotalPremium.Text = totalPremium.Value.ToString("n0");
                txtSuspensePremium.Text = suspensePremium == null ? "0" : suspensePremium.Value.ToString("n0");
                SuspenseePayDate = suspenseDate;
                overPremium = ((suspensePremium ?? 0) - totalPremium);
                txtOverPremium.Text = overPremium.Value.ToString("n0");

                //ตัวแปรที่มีผลต่อการคำนวนเบี้ย =================
                OLD_BIRTH_DATE = txtBirthDt.Text.Trim();
                OLD_PLAN_PAID = ckbLifeBuy.Checked;
                OLD_PLAN_CODE_PAID = txtPaidPlcode.Text.Trim() + txtPaidPlcode2.Text.Trim();
                OLD_MODE_PAID = cmbPaidPmode.SelectedValue.ToString();
                OLD_SUMM_PAID = txtPaidSumm.Text.Trim();
                OLD_ISU_DT_PAID = txtPaidIsuDate.Text.Trim();
                //จบตัวแปรที่มีผลต่อการคำนวนเบี้ย ===============



                //ตรวจสอบเงินเกิน เพื่อทำการเก็บค่าการจ่ายคืน ======================================================
                if (cmbSummryStatus.SelectedValue != null)
                {
                    VerifyandDisplayOverPay();
                }

                //จบตรวจสอบเงินเกิน เพื่อทำการเก็บค่าการจ่ายคืน ======================================================


                MessageBox.Show("คำนวนเบี้ยประกันเสร็จเรียบร้อยแล้ว");
            }
            catch (Exception ex)
            {
                PLAN_ERROR = true;
                txtTotalPremium.Text = "";
                txtLifePremium.Text = "";
                SetMsgException(ex);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }


        public void VerifyandDisplayOverPay()
        {

            //ตรวจสอบเงินเกิน เพื่อทำการเก็บค่าการจ่ายคืน ======================================================
            if (cmbSummryStatus.SelectedValue != null)
            {
                Decimal returnPremiumPayment = txtPayMentReturnPrem.Text == "" ? 0 : Convert.ToDecimal(txtPayMentReturnPrem.Text.Replace(",", ""));
                Decimal returnPremium = string.IsNullOrEmpty(txtOverPremium.Text) ? 0 : Convert.ToDecimal(txtOverPremium.Text);

                if (cmbSummryStatus.SelectedValue.ToString() == "IF")
                {
                    if (returnPremiumPayment != returnPremium)
                    {
                        if (returnPremium >= 100)
                        {
                            txtPayMentReturnPrem.Text = returnPremium.ToString("n2");
                            txtPayMentPremium.Text = txtTotalPremium.Text;
                            txtPayMentSuspense.Text = txtSuspensePremium.Text;
                            gbPayMentReturnPrm.Visible = true;
                            dgvPayMent.Rows.Clear();
                            bool chkError = false;
                            String errorMsg = "";
                            chkError = GenPaymentOfReturnPremium(out errorMsg);
                            if (chkError == true)
                            {
                                throw new Exception(errorMsg);
                            }
                        }
                        else
                        {
                            //gbPayMentReturnPrm.Visible = false;
                            txtPayMentSuspense.Text = "";
                            txtPayMentPremium.Text = "";
                            txtPayMentReturnPrem.Text = "";
                            dgvPayMent.Rows.Clear();
                        }
                    }
                }
                else if (cmbSummryStatus.SelectedValue.ToString() == "CC" || cmbSummryStatus.SelectedValue.ToString() == "DC" || cmbSummryStatus.SelectedValue.ToString() == "PP" || cmbSummryStatus.SelectedValue.ToString() == "EX" || cmbSummryStatus.SelectedValue.ToString() == "NT")
                {
                    txtPayMentReturnPrem.Text = txtSuspensePremium.Text; ;
                    txtPayMentPremium.Text = txtTotalPremium.Text;
                    txtPayMentSuspense.Text = txtSuspensePremium.Text; ;
                    gbPayMentReturnPrm.Visible = true;
                }
                else
                {
                    //gbPayMentReturnPrm.Visible = false;
                    txtPayMentSuspense.Text = "";
                    txtPayMentPremium.Text = "";
                    txtPayMentReturnPrem.Text = "";
                    dgvPayMent.Rows.Clear();
                }
            }

        }
        private void CalPlanKB()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                PLAN_ERROR = false;

                if (cmbPaidCtmType.SelectedValue.ToString() == "")
                {
                    cmbPaidCtmType.Focus();
                    throw new Exception("ระบุ ประเภทลูกค้า ของแบบประกันซื้อเพิ่ม");
                }
                if (cmbPaidPlBlock.SelectedValue.ToString() == "")
                {
                    cmbPaidPlBlock.Focus();
                    throw new Exception("ระบุ ชนิดแบบประกัน ของแบบประกันซื้อเพิ่ม");
                }
                if (txtPaidPlcode.Text.Trim() == "")
                {
                    txtPaidPlcode.Focus();
                    throw new Exception("ระบุ รหัสแบบประกัน ของแบบประกันซื้อเพิ่ม");
                }
                if (txtPaidPlcode2.Text.Trim() == "")
                {
                    txtPaidPlcode2.Focus();
                    throw new Exception("ระบุ รหัสแบบประกัน ของแบบประกันซื้อเพิ่ม");
                }
                if (txtPaidPlanName.Text.Trim() == "")
                {
                    txtPaidPlcode2.Focus();
                    throw new Exception("ระบุ ชื่อแบบประกัน ของแบบประกันซื้อเพิ่ม");
                }
                /*
                if (cmbPaidPmode.SelectedValue.ToString() == "")
                {
                    cmbPaidPmode.Focus();
                    throw new Exception("ระบุ งาดการชำระเบี้ย ของแบบประกันซื้อเพิ่ม");
                }

                if (txtPaidSumm.Text.Trim() == "")
                {
                    txtPaidSumm.Focus();
                    throw new Exception("ระบุ ทุนประกัน ของแบบประกันซื้อเพิ่ม");
                }

                if (txtPaidPremium.Text.Trim() == "" || txtPaidPremium.Text == "0")
                {
                    txtPaidPremium.Focus();
                    throw new Exception("ระบุ เบี้ยประกันชีวิต ของแบบประกันซื้อเพิ่ม");
                }
                */
                if (txtPaidIsuDate.Text.Trim() == "")
                {
                    txtPaidIsuDate.Focus();
                    throw new Exception("ระบุ วันที่เริ่มคุ้มครอง ของแบบประกันซื้อเพิ่ม");
                }


                PAR_PLAN_PAID = new NewBisPASvcRef.U_LIFE_ID();
                String channelType = cmbChannelType.SelectedValue == null ? "" : cmbChannelType.SelectedValue.ToString();
                String appNo = txtAppNo.Text.Trim();
                PolicySvcRef.P_NAME_ID pNameIDObj = new PolicySvcRef.P_NAME_ID();

                pNameIDObj.NAME_ID = NAME_ID;
                pNameIDObj.PRENAME = txtPrename.Text.Trim();
                pNameIDObj.NAME = txtName.Text.Trim();
                pNameIDObj.SURNAME = txtSurname.Text.Trim();
                if (cmbCardType.SelectedValue.ToString() == "1")
                {
                    pNameIDObj.IDCARD_NO = txtIdcardNo.Text.Trim();
                    pNameIDObj.PASSPORT = null;
                }
                else
                {
                    pNameIDObj.IDCARD_NO = null;
                    pNameIDObj.PASSPORT = txtIdcardNo.Text.Trim();
                }
                pNameIDObj.BIRTH_DT = txtBirthDt.Text.Trim() == "" ? (DateTime?)null : Utility.StringToDateTime(txtBirthDt.Text.Trim(), "BU");
                pNameIDObj.SEX = cmbSex.SelectedValue.ToString() == "" ? (char?)null : Convert.ToChar(cmbSex.SelectedValue.ToString());
                pNameIDObj.ST_AGE = txtStAge.Text == "" ? (Decimal?)null : Convert.ToDecimal(txtStAge.Text);

                PolicySvcRef.P_LIFE_ID pLifeIDPaidObj = new PolicySvcRef.P_LIFE_ID();
                pLifeIDPaidObj.PL_BLOCK = cmbPaidPlBlock.SelectedValue.ToString() == "" ? (char?)null : Convert.ToChar(cmbPaidPlBlock.SelectedValue.ToString());
                pLifeIDPaidObj.PL_CODE = txtPaidPlcode.Text.Trim();
                pLifeIDPaidObj.PL_CODE2 = txtPaidPlcode2.Text.Trim();
                //pLifeIDPaidObj.SUMM = txtPaidSumm.Text.Trim() == "" ? (long?)null : Convert.ToInt64(txtPaidSumm.Text.Trim().Replace(",", ""));
                pLifeIDPaidObj.SUMM = txtPaidSumm.Text.Trim() == "" ? 0 : Convert.ToInt64(txtPaidSumm.Text.Trim().Replace(",", ""));
                pLifeIDPaidObj.P_MODE = cmbPaidPmode.SelectedValue.ToString() == "" ? (int?)null : Convert.ToInt32(cmbPaidPmode.SelectedValue.ToString());
                pLifeIDPaidObj.ISU_DT = txtPaidIsuDate.Text.Trim() == "" ? (DateTime?)null : Utility.StringToDateTime(txtPaidIsuDate.Text.Trim(), "BU");
                pLifeIDPaidObj.FREE_FLG = "N";
                pLifeIDPaidObj.CTM_TYPE = cmbPaidCtmType.SelectedValue.ToString() == "" ? (char?)null : Convert.ToChar(cmbPaidCtmType.SelectedValue.ToString());
                //pLifeIDPaidObj.PREMIUM = txtPaidPremium.Text.Trim() == "" ? (Int64?)null : Convert.ToInt64(txtPaidPremium.Text.Trim().Replace(",", ""));
                pLifeIDPaidObj.PREMIUM = txtPaidPremium.Text.Trim() == "" ? 0 : Convert.ToInt64(txtPaidPremium.Text.Trim().Replace(",", ""));

                ITUtility.ProcessResult pr = new ProcessResult();
                String msgError = "";
                Decimal? totalSumm = 0;
                using (PolicySvcRef.PolicySvcClient client = new PolicySvcRef.PolicySvcClient())
                {
                    pr = client.CalPremiumPlanKB(ref pNameIDObj, ref pLifeIDPaidObj, ref channelType, ref appNo, out msgError, out totalSumm);
                    if (pr.Successed == false)
                    {
                        throw new Exception(pr.ErrorMessage);
                    }

                    PAR_CAL_SUMM = true;
                    PAR_TOTAL_SUMM_FREE = 0;
                    PAR_TOTAL_SUMM_BUY = totalSumm;

                    if (String.IsNullOrEmpty(msgError))
                    {
                        if (PAR_U_CAL_ERROR != null)
                        {
                            if (PAR_U_CAL_ERROR.APP_ID == null)
                            {
                                PAR_U_CAL_ERROR = new NewBisPASvcRef.U_CAL_ERROR();
                            }
                            else
                            {
                                PAR_U_CAL_ERROR.TMN = 'Y';
                                PAR_U_CAL_ERROR.TMN_ID = UserID;
                                PAR_U_CAL_ERROR.TMN_DT = DateTime.Now;
                            }
                        }
                    }
                    else
                    {
                        PAR_U_CAL_ERROR = new NewBisPASvcRef.U_CAL_ERROR();
                        PAR_U_CAL_ERROR.APP_ID = null;
                        PAR_U_CAL_ERROR.ERROR_DT = DateTime.Now;
                        PAR_U_CAL_ERROR.ERROR_ID = UserID;
                        PAR_U_CAL_ERROR.TMN = 'N';
                        PAR_U_CAL_ERROR.U_CAL_ERROR_DETAIL_Coll = new NewBisPASvcRef.U_CAL_ERROR_DETAIL_Collection();
                        String[] msqErrorArr = msgError.Split(',');
                        if (msqErrorArr != null && msqErrorArr.Count() > 0)
                        {
                            foreach (String obj in msqErrorArr.Distinct())
                            {
                                if (!(String.IsNullOrEmpty(obj)))
                                {
                                    NewBisPASvcRef.U_CAL_ERROR_DETAIL uCalErrDetailObj = new NewBisPASvcRef.U_CAL_ERROR_DETAIL();
                                    uCalErrDetailObj.U_CAL_ERROR_ID = null;
                                    uCalErrDetailObj.ERROR_CODE = obj;
                                    PAR_U_CAL_ERROR.U_CAL_ERROR_DETAIL_Coll.Add(uCalErrDetailObj);
                                }


                            }
                        }

                    }
                }

                if (PAR_U_CAL_ERROR != null && PAR_U_CAL_ERROR.ERROR_DT != null)
                {
                    if (PAR_U_CAL_ERROR.TMN == 'N')
                    {
                        String strError = "";
                        if (PAR_U_CAL_ERROR.U_CAL_ERROR_DETAIL_Coll != null && PAR_U_CAL_ERROR.U_CAL_ERROR_DETAIL_Coll.Count() > 0)
                        {
                            int i = 0;
                            foreach (NewBisPASvcRef.U_CAL_ERROR_DETAIL obj in PAR_U_CAL_ERROR.U_CAL_ERROR_DETAIL_Coll)
                            {
                                var GetData = from getData in PAR_AUTB_DATADIC_DET_COLLECTION
                                              where getData.COL_NAME == "ERROR_CODE"
                                              && getData.CODE == obj.ERROR_CODE
                                              select getData;
                                if (GetData != null && GetData.Count() > 0)
                                {
                                    i = i + 1;
                                    strError = strError + i.ToString() + ". " + GetData.ToArray()[0].DESCRIPTION + "\n";
                                }
                            }
                        }

                        MessageBox.Show("มีข้อผิดพลาดในการคำนวนเบี้ยประกันมีรายละเอียดดังนี้\n" + strError);
                    }
                }

                if (cmbDataCus.SelectedValue.ToString() == "2")
                {
                    if (pNameIDObj != null && pNameIDObj.NAME_ID != null)
                    {
                        throw new Exception("ไม่สามารถคำนวนเบี้ยประกันได้เนื่องจากลูกค้ามีข้อมูลเก่าแต่ท่านเลือกประเภทลูกค้าเป็นลูกค้าใหม่");
                    }
                }

                Decimal? totalPremium = 0;
                Decimal? suspensePremium = 0;
                Decimal? overPremium = 0;

                if (pLifeIDPaidObj == null)
                {
                    throw new Exception("ไม่สามารถคำนวนเบี้ยประกันได้กรุณาติกต่อ IT Tel 8518");
                }
                if (String.IsNullOrEmpty(pLifeIDPaidObj.PL_CODE))
                {
                    throw new Exception("ไม่สามารถคำนวนเบี้ยประกันได้กรุณาติกต่อ IT Tel 8518");
                }
                txtStAge.Text = pLifeIDPaidObj.ST_AGE == null ? "" : pLifeIDPaidObj.ST_AGE.Value.ToString();
                txtPaidSumm.Text = pLifeIDPaidObj.SUMM == null ? "0" : pLifeIDPaidObj.SUMM.Value.ToString("n0");
                txtPaidPolYr.Text = pLifeIDPaidObj.POL_YR == null ? "" : pLifeIDPaidObj.POL_YR.Value.ToString();
                txtPaidPolLt.Text = pLifeIDPaidObj.POL_LT == null ? "" : pLifeIDPaidObj.POL_LT.Value.ToString();
                txtPaidPayTerm.Text = pLifeIDPaidObj.PAY_TERM == null ? "" : pLifeIDPaidObj.PAY_TERM.Value.ToString();
                txtPaidLastPayDate.Text = pLifeIDPaidObj.LASTPAY_DT == null ? "" : Utility.dateTimeToString(pLifeIDPaidObj.LASTPAY_DT.Value, "dd/MM/yyyy", "BU");
                txtPaidAssTerm.Text = pLifeIDPaidObj.ASS_TERM == null ? "" : pLifeIDPaidObj.ASS_TERM.Value.ToString();
                txtPaidAssDate.Text = pLifeIDPaidObj.ASS_DT == null ? "" : Utility.dateTimeToString(pLifeIDPaidObj.ASS_DT.Value, "dd/MM/yyyy", "BU");
                txtPaidMatTerm.Text = pLifeIDPaidObj.MAT_TERM == null ? "" : pLifeIDPaidObj.MAT_TERM.Value.ToString();
                txtPaidMatDate.Text = pLifeIDPaidObj.MAT_DT == null ? "" : Utility.dateTimeToString(pLifeIDPaidObj.MAT_DT.Value, "dd/MM/yyyy", "BU");
                txtPaidNxtDueDate.Text = pLifeIDPaidObj.NXTDUE_DT == null ? "" : Utility.dateTimeToString(pLifeIDPaidObj.NXTDUE_DT.Value, "dd/MM/yyyy", "BU");
                txtPaidPremium.Text = pLifeIDPaidObj.PREMIUM == null ? "0" : pLifeIDPaidObj.PREMIUM.Value.ToString("n0");
                txtPaidSingle.Text = pLifeIDPaidObj.SINGLE == null ? "" : pLifeIDPaidObj.SINGLE.Value.ToString();
                txtPaidProtect.Text = pLifeIDPaidObj.PROTECT;
                txtPaidMedical.Text = pLifeIDPaidObj.MEDICAL == null ? "" : pLifeIDPaidObj.MEDICAL.Value.ToString();
                txtPaidStandard.Text = pLifeIDPaidObj.STANDARD == null ? "" : pLifeIDPaidObj.STANDARD.Value.ToString();
                txtPaidReinsure.Text = pLifeIDPaidObj.REINSURE == null ? "" : pLifeIDPaidObj.REINSURE.Value.ToString();
                txtPaidExcludeTpd.Text = pLifeIDPaidObj.EXCLUDE_TPD;
                txtPaidDepositInst.Text = pLifeIDPaidObj.DEPOSIT_INST == null ? "" : pLifeIDPaidObj.DEPOSIT_INST.Value.ToString("n0");
                txtPaidPensionDate.Text = pLifeIDPaidObj.PENSION_DT == null ? "" : Utility.dateTimeToString(pLifeIDPaidObj.PENSION_DT.Value, "dd/MM/yyyy", "BU");
                txtPaidPolicyHolding.Text = pLifeIDPaidObj.POLICY_HOLDING;
                txtPaidMarketingType.Text = pLifeIDPaidObj.MARKETING_TYPE;

                if (pLifeIDPaidObj.PREMIUM != null)
                {
                    totalPremium = totalPremium + pLifeIDPaidObj.PREMIUM.Value;
                }
                PAR_PLAN_PAID.PL_BLOCK = pLifeIDPaidObj.PL_BLOCK == null ? "" : pLifeIDPaidObj.PL_BLOCK.Value.ToString();
                PAR_PLAN_PAID.PL_CODE = pLifeIDPaidObj.PL_CODE;
                PAR_PLAN_PAID.PL_CODE2 = pLifeIDPaidObj.PL_CODE2;
                PAR_PLAN_PAID.SUMM = pLifeIDPaidObj.SUMM == null ? 0 : pLifeIDPaidObj.SUMM;
                PAR_PLAN_PAID.INFSUMM = pLifeIDPaidObj.INFSUMM;
                PAR_PLAN_PAID.PREMIUM = pLifeIDPaidObj.PREMIUM == null ? 0 : pLifeIDPaidObj.PREMIUM;
                PAR_PLAN_PAID.P_MODE = pLifeIDPaidObj.P_MODE;
                PAR_PLAN_PAID.ASS_TERM = pLifeIDPaidObj.ASS_TERM;
                PAR_PLAN_PAID.MAT_TERM = pLifeIDPaidObj.MAT_TERM;
                PAR_PLAN_PAID.PAY_TERM = pLifeIDPaidObj.PAY_TERM;
                PAR_PLAN_PAID.POL_YR = pLifeIDPaidObj.POL_YR;
                PAR_PLAN_PAID.POL_LT = pLifeIDPaidObj.POL_LT;
                PAR_PLAN_PAID.POL_LF_YR = pLifeIDPaidObj.POL_YR;
                PAR_PLAN_PAID.POL_LF_LT = pLifeIDPaidObj.POL_LT;
                PAR_PLAN_PAID.ISU_DT = pLifeIDPaidObj.ISU_DT;
                PAR_PLAN_PAID.ASS_DT = pLifeIDPaidObj.ASS_DT;
                PAR_PLAN_PAID.MAT_DT = pLifeIDPaidObj.MAT_DT;
                PAR_PLAN_PAID.PENSION_DT = pLifeIDPaidObj.PENSION_DT;
                PAR_PLAN_PAID.LASTPAY_DT = pLifeIDPaidObj.LASTPAY_DT;
                PAR_PLAN_PAID.NXTDUE_DT = pLifeIDPaidObj.NXTDUE_DT;
                PAR_PLAN_PAID.DEPOSIT_INST = pLifeIDPaidObj.DEPOSIT_INST;
                PAR_PLAN_PAID.CTM_TYPE = pLifeIDPaidObj.CTM_TYPE == null ? "" : pLifeIDPaidObj.CTM_TYPE.Value.ToString();
                PAR_PLAN_PAID.SINGLE = pLifeIDPaidObj.SINGLE == null ? "" : pLifeIDPaidObj.SINGLE.Value.ToString();
                PAR_PLAN_PAID.PROTECT = pLifeIDPaidObj.PROTECT;
                PAR_PLAN_PAID.MEDICAL = pLifeIDPaidObj.MEDICAL == null ? "" : pLifeIDPaidObj.MEDICAL.Value.ToString();
                PAR_PLAN_PAID.STANDARD = pLifeIDPaidObj.STANDARD == null ? "" : pLifeIDPaidObj.STANDARD.Value.ToString();
                PAR_PLAN_PAID.REINSURE = pLifeIDPaidObj.REINSURE == null ? "" : pLifeIDPaidObj.REINSURE.Value.ToString();
                PAR_PLAN_PAID.EXCLUDE_TPD = pLifeIDPaidObj.EXCLUDE_TPD == null || pLifeIDPaidObj.EXCLUDE_TPD == "" ? (char?)null : Convert.ToChar(pLifeIDPaidObj.EXCLUDE_TPD); ;
                PAR_PLAN_PAID.FREE_FLG = pLifeIDPaidObj.FREE_FLG;
                PAR_PLAN_PAID.POLICY_HOLDING = pLifeIDPaidObj.POLICY_HOLDING;
                PAR_PLAN_PAID.MARKETING_TYPE = pLifeIDPaidObj.MARKETING_TYPE;
                PAR_PLAN_PAID.BLA_TITLE = txtPaidPlanName.Text.Trim();


                txtLifePremium.Text = (totalPremium ?? 0).ToString("n0");
                // checkmode  
                //totalPremium = (PAR_PA_APPLICATION_ID.APPLICATION_Collection.First().LIFE_ID.P_MODE == 12 ? (totalPremium ?? 0) * 2 : (totalPremium ?? 0));
                txtTotalPremium.Text = totalPremium.Value.ToString("n0");

                txtSuspensePremium.Text = suspensePremium.Value.ToString("n0");
                overPremium = (suspensePremium - totalPremium);
                txtOverPremium.Text = overPremium.Value.ToString("n0");

                //ตัวแปรที่มีผลต่อการคำนวนเบี้ย =================
                OLD_BIRTH_DATE = txtBirthDt.Text.Trim();
                OLD_PLAN_PAID = ckbLifeBuy.Checked;
                OLD_PLAN_CODE_PAID = txtPaidPlcode.Text.Trim() + txtPaidPlcode2.Text.Trim();
                OLD_MODE_PAID = cmbPaidPmode.SelectedValue.ToString();
                OLD_SUMM_PAID = txtPaidSumm.Text.Trim();
                OLD_ISU_DT_PAID = txtPaidIsuDate.Text.Trim();
                //จบตัวแปรที่มีผลต่อการคำนวนเบี้ย ===============
                MessageBox.Show("คำนวนเบี้ยประกันเสร็จเรียบร้อยแล้ว");
            }
            catch (Exception ex)
            {
                PLAN_ERROR = true;
                txtTotalPremium.Text = "";
                txtLifePremium.Text = "";
                SetMsgException(ex);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
        private void CalPlanPACreditCard()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                if (ckbLifeFree.Checked == true)
                {
                    if (cmbFreeCtmType.SelectedValue.ToString() == "")
                    {
                        cmbFreeCtmType.Focus();
                        throw new Exception("ระบุ ประเภทลูกค้า ของแบบประกันแถมฟรี");
                    }
                    if (cmbFreePlBlock.SelectedValue.ToString() == "")
                    {
                        cmbFreePlBlock.Focus();
                        throw new Exception("ระบุ ชนิดแบบประกัน ของแบบประกันแถมฟรี");
                    }
                    if (txtFreePlcode.Text.Trim() == "")
                    {
                        txtFreePlcode.Focus();
                        throw new Exception("ระบุ รหัสแบบประกัน ของแบบประกันแถมฟรี");
                    }
                    if (txtFreePlcode2.Text.Trim() == "")
                    {
                        txtFreePlcode2.Focus();
                        throw new Exception("ระบุ รหัสแบบประกัน ของแบบประกันแถมฟรี");
                    }
                    if (txtFreePlanName.Text.Trim() == "")
                    {
                        txtFreePlcode2.Focus();
                        throw new Exception("ระบุ ชื่อแบบประกัน ของแบบประกันแถมฟรี");
                    }
                    if (cmbFreePmode.SelectedValue.ToString() == "")
                    {
                        cmbFreePmode.Focus();
                        throw new Exception("ระบุ งาดการชำระเบี้ย ของแบบประกันแถมฟรี");
                    }
                    if (txtFreeSumm.Text.Trim() == "")
                    {
                        txtFreeSumm.Focus();
                        throw new Exception("ระบุ ทุนประกัน ของแบบประกันแถมฟรี");
                    }
                    if (txtFreeIsuDate.Text.Trim() == "")
                    {
                        txtFreeIsuDate.Focus();
                        throw new Exception("ระบุ วันที่เริ่มคุ้มครอง ของแบบประกันแถมฟรี");
                    }
                }

                if (ckbLifeBuy.Checked == true)
                {
                    if (cmbPaidCtmType.SelectedValue.ToString() == "")
                    {
                        cmbPaidCtmType.Focus();
                        throw new Exception("ระบุ ประเภทลูกค้า ของแบบประกันซื้อเพิ่ม");
                    }
                    if (cmbPaidPlBlock.SelectedValue.ToString() == "")
                    {
                        cmbPaidPlBlock.Focus();
                        throw new Exception("ระบุ ชนิดแบบประกัน ของแบบประกันซื้อเพิ่ม");
                    }
                    if (txtPaidPlcode.Text.Trim() == "")
                    {
                        txtPaidPlcode.Focus();
                        throw new Exception("ระบุ รหัสแบบประกัน ของแบบประกันซื้อเพิ่ม");
                    }
                    if (txtPaidPlcode2.Text.Trim() == "")
                    {
                        txtPaidPlcode2.Focus();
                        throw new Exception("ระบุ รหัสแบบประกัน ของแบบประกันซื้อเพิ่ม");
                    }
                    if (txtPaidPlanName.Text.Trim() == "")
                    {
                        txtPaidPlcode2.Focus();
                        throw new Exception("ระบุ ชื่อแบบประกัน ของแบบประกันซื้อเพิ่ม");
                    }
                    if (cmbPaidPmode.SelectedValue.ToString() == "")
                    {
                        cmbPaidPmode.Focus();
                        throw new Exception("ระบุ งาดการชำระเบี้ย ของแบบประกันซื้อเพิ่ม");
                    }
                    if (txtPaidSumm.Text.Trim() == "")
                    {
                        txtPaidSumm.Focus();
                        throw new Exception("ระบุ ทุนประกัน ของแบบประกันซื้อเพิ่ม");
                    }
                    if (txtPaidIsuDate.Text.Trim() == "")
                    {
                        txtPaidIsuDate.Focus();
                        throw new Exception("ระบุ วันที่เริ่มคุ้มครอง ของแบบประกันซื้อเพิ่ม");
                    }
                }

                if (ckbSpouse.Checked == true)
                {
                    //if (cmbSpCardType.SelectedValue.ToString() == "")
                    //{
                    //    cmbSpCardType.Focus();
                    //    throw new Exception("ระบุ ประเภทบัตร ของคู่สมรส");
                    //}
                    //if (txtSpIDcardNo.Text.Trim() == "")
                    //{
                    //    txtSpIDcardNo.Focus();
                    //    throw new Exception("ระบุ เลขบัตรประชาชนหรือหนังสือเดินทาง ของคู่สมรส");
                    //}
                    if (txtSpIDcardNo.Text.Trim() != "" && cmbSpCardType.SelectedValue.ToString() == "")
                    {
                        cmbSpCardType.Focus();
                        throw new Exception("ระบุ ประเภทบัตร ของคู่สมรส");
                    }
                    if (cmbSpCardType.SelectedValue.ToString() == "1" && txtSpIDcardNo.Text.Trim() != "")
                    {
                        if (Utility.ValidateIDCard(txtSpIDcardNo.Text) == false)
                        {
                            txtSpIDcardNo.Focus();
                            throw new Exception("รูปแบบของเลขที่บัตรประชาชนไม่ถูกต้องของคู่สมรส");
                        }
                    }
                    if (txtSpPrename.Text.Trim() == "")
                    {
                        txtSpPrename.Focus();
                        throw new Exception("ระบุ คำนำหน้าชื่อ ของคู่สมรส");
                    }
                    if (txtSpName.Text.Trim() == "")
                    {
                        txtSpName.Focus();
                        throw new Exception("ระบุ ชื่อ ของคู่สมรส");
                    }
                    if (cmbSpSex.SelectedValue.ToString() == "")
                    {
                        cmbSpSex.Focus();
                        throw new Exception("ระบุ เพศ ของคู่สมรส");
                    }
                    if (txtSpBirthDate.Text.Trim() == "")
                    {
                        txtSpBirthDate.Focus();
                        throw new Exception("ระบุ วันเกิด ของคู่สมรส");
                    }
                    if (txtSpAge.Text.Trim() == "")
                    {
                        txtSpAge.Focus();
                        throw new Exception("ระบุ อายุ ของคู่สมรส");
                    }
                    if (cmbSpNationality.SelectedValue.ToString() == "")
                    {
                        cmbSpNationality.Focus();
                        throw new Exception("ระบุ สัญชาติ ของคู่สมรส");
                    }
                }
                string channelType = cmbChannelType.SelectedValue == null ? "" : cmbChannelType.SelectedValue.ToString();
                if (channelType == "PF" && NAME_ID != null)
                {
                    using (var client = new NewBisPASvcRef.NewBisPASvcClient())
                    {
                        var hasData = false;
                        var plBlock = cmbFreePlBlock.SelectedValue.ToString();
                        var plCode = txtFreePlcode.Text.Trim();
                        var plCode2 = txtFreePlcode2.Text.Trim();
                        var prCheck = client.HasAlreadyLifePlanPolicy(out hasData, NAME_ID.Value, channelType, plBlock, plCode, plCode2);
                        if (!prCheck.Successed)
                        {
                            throw new Exception("HasAlreadyLifePlanPolicy : ไม่สามารถตรวจสอบแบบประกันได้ ");
                        }

                        if (hasData)
                        {
                            throw new Exception("ผู้เอาประกันรายนี้ได้ออก กธ. แบบประกันนี้ไปแล้วไม่สามารถออก กธ. ซ้ำได้ ");
                        }
                    }
                }

                PLAN_ERROR = false;
                PAR_PLAN_FREE = new NewBisPASvcRef.U_LIFE_ID();
                PAR_PLAN_PAID = new NewBisPASvcRef.U_LIFE_ID();
                String appNo = txtAppNo.Text.Trim();

                PolicySvcRef.P_NAME_ID pNameIDObj = new PolicySvcRef.P_NAME_ID();
                pNameIDObj.NAME_ID = NAME_ID;
                pNameIDObj.PRENAME = txtPrename.Text.Trim();
                pNameIDObj.NAME = txtName.Text.Trim();
                pNameIDObj.SURNAME = txtSurname.Text.Trim();
                if (cmbCardType.SelectedValue.ToString() == "1")
                {
                    pNameIDObj.IDCARD_NO = txtIdcardNo.Text.Trim();
                    pNameIDObj.PASSPORT = null;
                }
                else
                {
                    pNameIDObj.IDCARD_NO = null;
                    pNameIDObj.PASSPORT = txtIdcardNo.Text.Trim();
                }
                pNameIDObj.BIRTH_DT = txtBirthDt.Text.Trim() == "" ? (DateTime?)null : Utility.StringToDateTime(txtBirthDt.Text.Trim(), "BU");
                pNameIDObj.SEX = cmbSex.SelectedValue.ToString() == "" ? (char?)null : Convert.ToChar(cmbSex.SelectedValue.ToString());
                pNameIDObj.ST_AGE = txtStAge.Text == "" ? (Decimal?)null : Convert.ToDecimal(txtStAge.Text);

                PolicySvcRef.P_LIFE_ID_Collection pLifeIDColl = new PolicySvcRef.P_LIFE_ID_Collection();
                Decimal? ST_AGE_SPOUSE = null;

                if (ckbLifeFree.Checked == true)
                {
                    PolicySvcRef.P_LIFE_ID pLifeIDFreeObj = new PolicySvcRef.P_LIFE_ID();
                    pLifeIDFreeObj.PL_BLOCK = cmbFreePlBlock.SelectedValue.ToString() == "" ? (char?)null : Convert.ToChar(cmbFreePlBlock.SelectedValue.ToString());
                    pLifeIDFreeObj.PL_CODE = txtFreePlcode.Text.Trim();
                    pLifeIDFreeObj.PL_CODE2 = txtFreePlcode2.Text.Trim();
                    pLifeIDFreeObj.SUMM = txtFreeSumm.Text.Trim() == "" ? (long?)null : Convert.ToInt64(txtFreeSumm.Text.Trim().Replace(",", ""));
                    pLifeIDFreeObj.P_MODE = cmbFreePmode.SelectedValue.ToString() == "" ? (int?)null : Convert.ToInt32(cmbFreePmode.SelectedValue.ToString());
                    pLifeIDFreeObj.ISU_DT = txtFreeIsuDate.Text.Trim() == "" ? (DateTime?)null : Utility.StringToDateTime(txtFreeIsuDate.Text.Trim(), "BU");
                    pLifeIDFreeObj.FREE_FLG = "Y";
                    pLifeIDFreeObj.SPOUSE_FLG = "N";
                    pLifeIDFreeObj.CTM_TYPE = cmbFreeCtmType.SelectedValue.ToString() == "" ? (char?)null : Convert.ToChar(cmbFreeCtmType.SelectedValue.ToString());
                    pLifeIDColl.Add(pLifeIDFreeObj);
                }

                if (ckbLifeBuy.Checked == true)
                {

                    if (txtPaidSpouseFlg.Text == "T" && ckbSpouse.Checked == false)
                    {
                        throw new Exception("เป็นแบบประกันแบบมีคู่สมรสแต่ไม่ได้เลือกใส่ข้อมูลคู่สมรส");
                    }

                    PolicySvcRef.P_LIFE_ID pLifeIDPaidObj = new PolicySvcRef.P_LIFE_ID();
                    pLifeIDPaidObj.PL_BLOCK = cmbPaidPlBlock.SelectedValue.ToString() == "" ? (char?)null : Convert.ToChar(cmbPaidPlBlock.SelectedValue.ToString());
                    pLifeIDPaidObj.PL_CODE = txtPaidPlcode.Text.Trim();
                    pLifeIDPaidObj.PL_CODE2 = txtPaidPlcode2.Text.Trim();
                    pLifeIDPaidObj.SUMM = txtPaidSumm.Text.Trim() == "" ? (long?)null : Convert.ToInt64(txtPaidSumm.Text.Trim().Replace(",", ""));
                    pLifeIDPaidObj.P_MODE = cmbPaidPmode.SelectedValue.ToString() == "" ? (int?)null : Convert.ToInt32(cmbPaidPmode.SelectedValue.ToString());
                    pLifeIDPaidObj.ISU_DT = txtPaidIsuDate.Text.Trim() == "" ? (DateTime?)null : Utility.StringToDateTime(txtPaidIsuDate.Text.Trim(), "BU");
                    pLifeIDPaidObj.FREE_FLG = "N";
                    if (ckbSpouse.Checked == true)
                    {
                        pLifeIDPaidObj.SPOUSE_FLG = "Y";
                        ST_AGE_SPOUSE = txtSpAge.Text.Trim() == "" ? (Decimal?)null : Convert.ToDecimal(txtSpAge.Text.Trim());
                    }
                    else
                    {
                        pLifeIDPaidObj.SPOUSE_FLG = "N";
                        ST_AGE_SPOUSE = null;
                    }

                    pLifeIDPaidObj.CTM_TYPE = cmbPaidCtmType.SelectedValue.ToString() == "" ? (char?)null : Convert.ToChar(cmbPaidCtmType.SelectedValue.ToString());
                    pLifeIDColl.Add(pLifeIDPaidObj);
                }

                ITUtility.ProcessResult pr = new ProcessResult();
                decimal? stAge = txtStAge.Text.Trim() == "" ? (decimal?)null : Convert.ToDecimal(txtStAge.Text.Trim());
                DateTime? birthDateSpouse = txtSpBirthDate.Text.Trim() == "" ? (DateTime?)null : Utility.StringToDateTime(txtSpBirthDate.Text.Trim(), "BU");
                int? SP_ASS_TERM = null;
                DateTime? SP_ASS_DATE = null;
                String msgError = "";
                Decimal? totalSummFree = 0;
                Decimal? totalSummBuy = 0;
                using (PolicySvcRef.PolicySvcClient client = new PolicySvcRef.PolicySvcClient())
                {
                    pr = client.CalPremiumPlanPM(ref pNameIDObj, ref pLifeIDColl, ref channelType, ref appNo, ref ST_AGE_SPOUSE, ref stAge, birthDateSpouse, ref SP_ASS_TERM, ref SP_ASS_DATE, out msgError, out totalSummFree, out totalSummBuy);
                    if (pr.Successed == false)
                    {
                        throw new Exception(pr.ErrorMessage);
                    }
                    PAR_CAL_SUMM = true;
                    PAR_TOTAL_SUMM_FREE = totalSummFree;
                    PAR_TOTAL_SUMM_BUY = totalSummBuy;

                    //if (String.IsNullOrEmpty(txtExpireMM.Text) || String.IsNullOrEmpty(txtExpireMM.Text))
                    //{
                    //    msgError = msgError + ",ER006";
                    //}


                    if (String.IsNullOrEmpty(msgError))
                    {
                        if (PAR_U_CAL_ERROR != null)
                        {
                            if (PAR_U_CAL_ERROR.APP_ID == null)
                            {
                                PAR_U_CAL_ERROR = new NewBisPASvcRef.U_CAL_ERROR();
                            }
                            else
                            {
                                PAR_U_CAL_ERROR.TMN = 'Y';
                                PAR_U_CAL_ERROR.TMN_ID = UserID;
                                PAR_U_CAL_ERROR.TMN_DT = DateTime.Now;
                            }
                        }
                    }
                    else
                    {
                        PAR_U_CAL_ERROR = new NewBisPASvcRef.U_CAL_ERROR();
                        PAR_U_CAL_ERROR.APP_ID = null;
                        PAR_U_CAL_ERROR.ERROR_DT = DateTime.Now;
                        PAR_U_CAL_ERROR.ERROR_ID = UserID;
                        PAR_U_CAL_ERROR.TMN = 'N';
                        PAR_U_CAL_ERROR.U_CAL_ERROR_DETAIL_Coll = new NewBisPASvcRef.U_CAL_ERROR_DETAIL_Collection();
                        String[] msqErrorArr = msgError.Split(',');
                        if (msqErrorArr != null && msqErrorArr.Count() > 0)
                        {
                            foreach (String obj in msqErrorArr.Distinct())
                            {
                                if (!(String.IsNullOrEmpty(obj)))
                                {
                                    NewBisPASvcRef.U_CAL_ERROR_DETAIL uCalErrDetailObj = new NewBisPASvcRef.U_CAL_ERROR_DETAIL();
                                    uCalErrDetailObj.U_CAL_ERROR_ID = null;
                                    uCalErrDetailObj.ERROR_CODE = obj;
                                    PAR_U_CAL_ERROR.U_CAL_ERROR_DETAIL_Coll.Add(uCalErrDetailObj);
                                }


                            }
                        }

                    }
                }

                if (PAR_U_CAL_ERROR != null && PAR_U_CAL_ERROR.ERROR_DT != null)
                {
                    if (PAR_U_CAL_ERROR.TMN == 'N')
                    {
                        String strError = "";
                        if (PAR_U_CAL_ERROR.U_CAL_ERROR_DETAIL_Coll != null && PAR_U_CAL_ERROR.U_CAL_ERROR_DETAIL_Coll.Count() > 0)
                        {
                            int i = 0;
                            foreach (NewBisPASvcRef.U_CAL_ERROR_DETAIL obj in PAR_U_CAL_ERROR.U_CAL_ERROR_DETAIL_Coll)
                            {
                                var GetData = from getData in PAR_AUTB_DATADIC_DET_COLLECTION
                                              where getData.COL_NAME == "ERROR_CODE"
                                              && getData.CODE == obj.ERROR_CODE
                                              select getData;
                                if (GetData != null && GetData.Count() > 0)
                                {
                                    i = i + 1;
                                    strError = strError + i.ToString() + ". " + GetData.ToArray()[0].DESCRIPTION + "\n";
                                }
                            }
                        }

                        MessageBox.Show("มีข้อผิดพลาดในการคำนวนเบี้ยประกันมีรายละเอียดดังนี้\n" + strError);
                    }
                }

                if (cmbDataCus.SelectedValue.ToString() == "2")
                {
                    if (pNameIDObj != null && pNameIDObj.NAME_ID != null)
                    {
                        throw new Exception("ไม่สามารถคำนวนเบี้ยประกันได้เนื่องจากลูกค้ามีข้อมูลเก่าแต่ท่านเลือกประเภทลูกค้าเป็นลูกค้าใหม่");
                    }
                }

                Decimal? totalPremium = 0;
                Decimal? suspensePremium = 0;
                Decimal? overPremium = 0;

                if (pLifeIDColl != null && pLifeIDColl.Count() > 0)
                {
                    txtStAge.Text = stAge == null ? "" : stAge.Value.ToString("n0");
                    txtSpAge.Text = ST_AGE_SPOUSE == null ? "" : ST_AGE_SPOUSE.Value.ToString("n0");

                    PolicySvcRef.P_LIFE_ID pLifeIDFreeObj = new PolicySvcRef.P_LIFE_ID();
                    PolicySvcRef.P_LIFE_ID pLifeIDPaidObj = new PolicySvcRef.P_LIFE_ID();
                    foreach (PolicySvcRef.P_LIFE_ID obj in pLifeIDColl)
                    {
                        if (obj.FREE_FLG == "Y" || IsChannelTypeFreePolicy(channelType))
                        {
                            pLifeIDFreeObj = obj;
                        }
                        else
                        {
                            pLifeIDPaidObj = obj;
                        }
                    }

                    if (pLifeIDFreeObj != null && (!String.IsNullOrEmpty(pLifeIDFreeObj.PL_CODE)))
                    {
                        txtFreeSumm.Text = pLifeIDFreeObj.SUMM == null ? "" : pLifeIDFreeObj.SUMM.Value.ToString("n0");
                        txtFreePolYr.Text = pLifeIDFreeObj.POL_YR == null ? "" : pLifeIDFreeObj.POL_YR.Value.ToString();
                        txtFreePolLt.Text = pLifeIDFreeObj.POL_LT == null ? "" : pLifeIDFreeObj.POL_LT.Value.ToString();
                        txtFreePayTerm.Text = pLifeIDFreeObj.PAY_TERM == null ? "" : pLifeIDFreeObj.PAY_TERM.Value.ToString();
                        txtFreeLastPayDate.Text = pLifeIDFreeObj.LASTPAY_DT == null ? "" : Utility.dateTimeToString(pLifeIDFreeObj.LASTPAY_DT.Value, "dd/MM/yyyy", "BU");
                        txtFreeAssTerm.Text = pLifeIDFreeObj.ASS_TERM == null ? "" : pLifeIDFreeObj.ASS_TERM.Value.ToString();
                        txtFreeAssDate.Text = pLifeIDFreeObj.ASS_DT == null ? "" : Utility.dateTimeToString(pLifeIDFreeObj.ASS_DT.Value, "dd/MM/yyyy", "BU");
                        txtFreeMatTerm.Text = pLifeIDFreeObj.MAT_TERM == null ? "" : pLifeIDFreeObj.MAT_TERM.Value.ToString();
                        txtFreeMatDate.Text = pLifeIDFreeObj.MAT_DT == null ? "" : Utility.dateTimeToString(pLifeIDFreeObj.MAT_DT.Value, "dd/MM/yyyy", "BU");
                        txtFreeNxtDueDate.Text = pLifeIDFreeObj.NXTDUE_DT == null ? "" : Utility.dateTimeToString(pLifeIDFreeObj.NXTDUE_DT.Value, "dd/MM/yyyy", "BU");
                        txtFreePremium.Text = pLifeIDFreeObj.PREMIUM == null ? "0" : pLifeIDFreeObj.PREMIUM.Value.ToString("n0");
                        txtFreeSingle.Text = pLifeIDFreeObj.SINGLE == null ? "" : pLifeIDFreeObj.SINGLE.Value.ToString();
                        txtFreeProtect.Text = pLifeIDFreeObj.PROTECT;
                        txtFreeMedical.Text = pLifeIDFreeObj.MEDICAL == null ? "" : pLifeIDFreeObj.MEDICAL.Value.ToString();
                        txtFreeStandard.Text = pLifeIDFreeObj.STANDARD == null ? "" : pLifeIDFreeObj.STANDARD.Value.ToString();
                        txtFreeReinsure.Text = pLifeIDFreeObj.REINSURE == null ? "" : pLifeIDFreeObj.REINSURE.Value.ToString();
                        txtFreeExcludeTpd.Text = pLifeIDFreeObj.EXCLUDE_TPD;
                        txtFreeDepositInst.Text = pLifeIDFreeObj.DEPOSIT_INST == null ? "" : pLifeIDFreeObj.DEPOSIT_INST.Value.ToString("n0");
                        txtFreePensionDate.Text = pLifeIDFreeObj.PENSION_DT == null ? "" : Utility.dateTimeToString(pLifeIDFreeObj.PENSION_DT.Value, "dd/MM/yyyy", "BU");
                        txtFreePolicyHolding.Text = pLifeIDFreeObj.POLICY_HOLDING;
                        txtFreeMarketingType.Text = pLifeIDFreeObj.MARKETING_TYPE;
                        if (pLifeIDFreeObj.PREMIUM != null)
                        {
                            totalPremium = totalPremium + pLifeIDFreeObj.PREMIUM.Value;
                        }

                        //เก็บค่า แบบประกันฟรี ใน Object สาธารณะ =======================================================================
                        PAR_PLAN_FREE.PL_BLOCK = pLifeIDFreeObj.PL_BLOCK == null ? "" : pLifeIDFreeObj.PL_BLOCK.Value.ToString();
                        PAR_PLAN_FREE.PL_CODE = pLifeIDFreeObj.PL_CODE;
                        PAR_PLAN_FREE.PL_CODE2 = pLifeIDFreeObj.PL_CODE2;
                        PAR_PLAN_FREE.SUMM = pLifeIDFreeObj.SUMM;
                        PAR_PLAN_FREE.INFSUMM = pLifeIDFreeObj.INFSUMM;
                        PAR_PLAN_FREE.PREMIUM = pLifeIDFreeObj.PREMIUM;
                        PAR_PLAN_FREE.P_MODE = pLifeIDFreeObj.P_MODE;
                        PAR_PLAN_FREE.ASS_TERM = pLifeIDFreeObj.ASS_TERM;
                        PAR_PLAN_FREE.MAT_TERM = pLifeIDFreeObj.MAT_TERM;
                        PAR_PLAN_FREE.PAY_TERM = pLifeIDFreeObj.PAY_TERM;
                        PAR_PLAN_FREE.POL_YR = pLifeIDFreeObj.POL_YR;
                        PAR_PLAN_FREE.POL_LT = pLifeIDFreeObj.POL_LT;
                        PAR_PLAN_FREE.POL_LF_YR = pLifeIDFreeObj.POL_YR;
                        PAR_PLAN_FREE.POL_LF_LT = pLifeIDFreeObj.POL_LT;
                        PAR_PLAN_FREE.ISU_DT = pLifeIDFreeObj.ISU_DT;
                        PAR_PLAN_FREE.ASS_DT = pLifeIDFreeObj.ASS_DT;
                        PAR_PLAN_FREE.MAT_DT = pLifeIDFreeObj.MAT_DT;
                        PAR_PLAN_FREE.PENSION_DT = pLifeIDFreeObj.PENSION_DT;
                        PAR_PLAN_FREE.LASTPAY_DT = pLifeIDFreeObj.LASTPAY_DT;
                        PAR_PLAN_FREE.NXTDUE_DT = pLifeIDFreeObj.NXTDUE_DT;
                        PAR_PLAN_FREE.BLA_TITLE = txtFreePlanName.Text;
                        PAR_PLAN_FREE.DEPOSIT_INST = pLifeIDFreeObj.DEPOSIT_INST;
                        PAR_PLAN_FREE.CTM_TYPE = pLifeIDFreeObj.CTM_TYPE == null ? "" : pLifeIDFreeObj.CTM_TYPE.Value.ToString();
                        PAR_PLAN_FREE.SINGLE = pLifeIDFreeObj.SINGLE == null ? "" : pLifeIDFreeObj.SINGLE.Value.ToString();
                        PAR_PLAN_FREE.PROTECT = pLifeIDFreeObj.PROTECT;
                        PAR_PLAN_FREE.MEDICAL = pLifeIDFreeObj.MEDICAL == null ? "" : pLifeIDFreeObj.MEDICAL.Value.ToString();
                        PAR_PLAN_FREE.STANDARD = pLifeIDFreeObj.STANDARD == null ? "" : pLifeIDFreeObj.STANDARD.Value.ToString();
                        PAR_PLAN_FREE.REINSURE = pLifeIDFreeObj.REINSURE == null ? "" : pLifeIDFreeObj.REINSURE.Value.ToString();
                        PAR_PLAN_FREE.EXCLUDE_TPD = pLifeIDFreeObj.EXCLUDE_TPD == null || pLifeIDFreeObj.EXCLUDE_TPD == "" ? (char?)null : Convert.ToChar(pLifeIDFreeObj.EXCLUDE_TPD); ;
                        PAR_PLAN_FREE.FREE_FLG = pLifeIDFreeObj.FREE_FLG;
                        PAR_PLAN_FREE.POLICY_HOLDING = pLifeIDFreeObj.POLICY_HOLDING;
                        PAR_PLAN_FREE.MARKETING_TYPE = pLifeIDFreeObj.MARKETING_TYPE;
                        PAR_PLAN_FREE.BLA_TITLE = txtFreePlanName.Text.Trim();

                        //END เก็บค่า แบบประกันฟรี ใน Object สาธารณะ ====================================================================
                    }
                    if (pLifeIDPaidObj != null && (!String.IsNullOrEmpty(pLifeIDPaidObj.PL_CODE)))
                    {
                        txtPaidSumm.Text = pLifeIDPaidObj.SUMM == null ? "" : pLifeIDPaidObj.SUMM.Value.ToString("n0");
                        txtPaidPolYr.Text = pLifeIDPaidObj.POL_YR == null ? "" : pLifeIDPaidObj.POL_YR.Value.ToString();
                        txtPaidPolLt.Text = pLifeIDPaidObj.POL_LT == null ? "" : pLifeIDPaidObj.POL_LT.Value.ToString();
                        txtPaidPayTerm.Text = pLifeIDPaidObj.PAY_TERM == null ? "" : pLifeIDPaidObj.PAY_TERM.Value.ToString();
                        txtPaidLastPayDate.Text = pLifeIDPaidObj.LASTPAY_DT == null ? "" : Utility.dateTimeToString(pLifeIDPaidObj.LASTPAY_DT.Value, "dd/MM/yyyy", "BU");
                        txtPaidAssTerm.Text = pLifeIDPaidObj.ASS_TERM == null ? "" : pLifeIDPaidObj.ASS_TERM.Value.ToString();
                        txtPaidAssDate.Text = pLifeIDPaidObj.ASS_DT == null ? "" : Utility.dateTimeToString(pLifeIDPaidObj.ASS_DT.Value, "dd/MM/yyyy", "BU");
                        txtPaidMatTerm.Text = pLifeIDPaidObj.MAT_TERM == null ? "" : pLifeIDPaidObj.MAT_TERM.Value.ToString();
                        txtPaidMatDate.Text = pLifeIDPaidObj.MAT_DT == null ? "" : Utility.dateTimeToString(pLifeIDPaidObj.MAT_DT.Value, "dd/MM/yyyy", "BU");
                        txtPaidNxtDueDate.Text = pLifeIDPaidObj.NXTDUE_DT == null ? "" : Utility.dateTimeToString(pLifeIDPaidObj.NXTDUE_DT.Value, "dd/MM/yyyy", "BU");
                        txtPaidPremium.Text = pLifeIDPaidObj.PREMIUM == null ? "0" : pLifeIDPaidObj.PREMIUM.Value.ToString("n0");
                        txtPaidSingle.Text = pLifeIDPaidObj.SINGLE == null ? "" : pLifeIDPaidObj.SINGLE.Value.ToString();
                        txtPaidProtect.Text = pLifeIDPaidObj.PROTECT;
                        txtPaidMedical.Text = pLifeIDPaidObj.MEDICAL == null ? "" : pLifeIDPaidObj.MEDICAL.Value.ToString();
                        txtPaidStandard.Text = pLifeIDPaidObj.STANDARD == null ? "" : pLifeIDPaidObj.STANDARD.Value.ToString();
                        txtPaidReinsure.Text = pLifeIDPaidObj.REINSURE == null ? "" : pLifeIDPaidObj.REINSURE.Value.ToString();
                        txtPaidExcludeTpd.Text = pLifeIDPaidObj.EXCLUDE_TPD;
                        txtPaidDepositInst.Text = pLifeIDPaidObj.DEPOSIT_INST == null ? "" : pLifeIDPaidObj.DEPOSIT_INST.Value.ToString("n0");
                        txtPaidPensionDate.Text = pLifeIDPaidObj.PENSION_DT == null ? "" : Utility.dateTimeToString(pLifeIDPaidObj.PENSION_DT.Value, "dd/MM/yyyy", "BU");
                        txtPaidPolicyHolding.Text = pLifeIDPaidObj.POLICY_HOLDING;
                        txtPaidMarketingType.Text = pLifeIDPaidObj.MARKETING_TYPE;
                        txtSpAssTerm.Text = SP_ASS_TERM == null ? "" : SP_ASS_TERM.Value.ToString();
                        txtSpAssDate.Text = SP_ASS_DATE == null ? "" : Utility.dateTimeToString(SP_ASS_DATE.Value, "dd/MM/yyyy", "BU");

                        pLifeIDPaidObj.SPOUSE_FLG = txtPaidSpouseFlg.Text;

                        if (pLifeIDPaidObj.PREMIUM != null)
                        {
                            totalPremium = totalPremium + pLifeIDPaidObj.PREMIUM.Value;
                        }
                        //เก็บค่า แบบประกันซื้อ ใน Object สาธารณะ =======================================================================

                        if (pLifeIDPaidObj.SPOUSE_FLG == "T")
                        {
                            if (cmbSpCardType.SelectedValue.ToString() == "")
                            {
                                cmbSpCardType.Focus();
                                throw new Exception("ระบุ ประเภทบัตร ของคู่สมรส");
                            }
                            if (txtSpIDcardNo.Text.Trim() == "")
                            {
                                txtSpIDcardNo.Focus();
                                throw new Exception("ระบุ เลขบัตรประชาชนหรือหนังสือเดินทาง ของคู่สมรส");
                            }
                            if (cmbSpCardType.SelectedValue.ToString() == "1" && txtSpIDcardNo.Text.Trim() != "")
                            {
                                if (Utility.ValidateIDCard(txtSpIDcardNo.Text) == false)
                                {
                                    txtSpIDcardNo.Focus();
                                    throw new Exception("รูปแบบของเลขที่บัตรประชาชนไม่ถูกต้องของคู่สมรส");
                                }
                            }
                            if (txtSpPrename.Text.Trim() == "")
                            {
                                txtSpPrename.Focus();
                                throw new Exception("ระบุ คำนำหน้าชื่อ ของคู่สมรส");
                            }
                            if (txtSpName.Text.Trim() == "")
                            {
                                txtSpName.Focus();
                                throw new Exception("ระบุ ชื่อ ของคู่สมรส");
                            }
                            if (cmbSpSex.SelectedValue.ToString() == "")
                            {
                                cmbSpSex.Focus();
                                throw new Exception("ระบุ เพศ ของคู่สมรส");
                            }
                            if (txtSpBirthDate.Text.Trim() == "")
                            {
                                txtSpBirthDate.Focus();
                                throw new Exception("ระบุ วันเกิด ของคู่สมรส");
                            }
                            if (txtSpAge.Text.Trim() == "")
                            {
                                txtSpAge.Focus();
                                throw new Exception("ระบุ อายุ ของคู่สมรส");
                            }
                            if (cmbSpNationality.SelectedValue.ToString() == "")
                            {
                                cmbSpNationality.Focus();
                                throw new Exception("ระบุ สัญชาติ ของคู่สมรส");
                            }
                            if (txtSpAssTerm.Text.Trim() == "")
                            {
                                txtSpAssTerm.Focus();
                                throw new Exception("ไม่มีข้อมูลระยะคุ้มครองของคู่สมรส");
                            }
                            if (txtSpAssDate.Text.Trim() == "")
                            {
                                txtSpAssDate.Focus();
                                throw new Exception("ไม่มีข้อมูลวันที่สิ้นสุดคุ้มครองของคู่สมรส");
                            }
                        }

                        PAR_PLAN_PAID.PL_BLOCK = pLifeIDPaidObj.PL_BLOCK == null ? "" : pLifeIDPaidObj.PL_BLOCK.Value.ToString();
                        PAR_PLAN_PAID.PL_CODE = pLifeIDPaidObj.PL_CODE;
                        PAR_PLAN_PAID.PL_CODE2 = pLifeIDPaidObj.PL_CODE2;
                        PAR_PLAN_PAID.SUMM = pLifeIDPaidObj.SUMM;
                        PAR_PLAN_PAID.INFSUMM = pLifeIDPaidObj.INFSUMM;
                        PAR_PLAN_PAID.PREMIUM = pLifeIDPaidObj.PREMIUM;
                        PAR_PLAN_PAID.P_MODE = pLifeIDPaidObj.P_MODE;
                        PAR_PLAN_PAID.ASS_TERM = pLifeIDPaidObj.ASS_TERM;
                        PAR_PLAN_PAID.MAT_TERM = pLifeIDPaidObj.MAT_TERM;
                        PAR_PLAN_PAID.PAY_TERM = pLifeIDPaidObj.PAY_TERM;
                        PAR_PLAN_PAID.POL_YR = pLifeIDPaidObj.POL_YR;
                        PAR_PLAN_PAID.POL_LT = pLifeIDPaidObj.POL_LT;
                        PAR_PLAN_PAID.POL_LF_YR = pLifeIDPaidObj.POL_YR;
                        PAR_PLAN_PAID.POL_LF_LT = pLifeIDPaidObj.POL_LT;
                        PAR_PLAN_PAID.ISU_DT = pLifeIDPaidObj.ISU_DT;
                        PAR_PLAN_PAID.ASS_DT = pLifeIDPaidObj.ASS_DT;
                        PAR_PLAN_PAID.MAT_DT = pLifeIDPaidObj.MAT_DT;
                        PAR_PLAN_PAID.PENSION_DT = pLifeIDPaidObj.PENSION_DT;
                        PAR_PLAN_PAID.LASTPAY_DT = pLifeIDPaidObj.LASTPAY_DT;
                        PAR_PLAN_PAID.NXTDUE_DT = pLifeIDPaidObj.NXTDUE_DT;
                        PAR_PLAN_PAID.DEPOSIT_INST = pLifeIDPaidObj.DEPOSIT_INST;
                        PAR_PLAN_PAID.CTM_TYPE = pLifeIDPaidObj.CTM_TYPE == null ? "" : pLifeIDPaidObj.CTM_TYPE.Value.ToString();
                        PAR_PLAN_PAID.SINGLE = pLifeIDPaidObj.SINGLE == null ? "" : pLifeIDPaidObj.SINGLE.Value.ToString();
                        PAR_PLAN_PAID.PROTECT = pLifeIDPaidObj.PROTECT;
                        PAR_PLAN_PAID.MEDICAL = pLifeIDPaidObj.MEDICAL == null ? "" : pLifeIDPaidObj.MEDICAL.Value.ToString();
                        PAR_PLAN_PAID.STANDARD = pLifeIDPaidObj.STANDARD == null ? "" : pLifeIDPaidObj.STANDARD.Value.ToString();
                        PAR_PLAN_PAID.REINSURE = pLifeIDPaidObj.REINSURE == null ? "" : pLifeIDPaidObj.REINSURE.Value.ToString();
                        PAR_PLAN_PAID.EXCLUDE_TPD = pLifeIDPaidObj.EXCLUDE_TPD == null || pLifeIDPaidObj.EXCLUDE_TPD == "" ? (char?)null : Convert.ToChar(pLifeIDPaidObj.EXCLUDE_TPD); ;
                        PAR_PLAN_PAID.FREE_FLG = pLifeIDPaidObj.FREE_FLG;
                        PAR_PLAN_PAID.POLICY_HOLDING = pLifeIDPaidObj.POLICY_HOLDING;
                        PAR_PLAN_PAID.MARKETING_TYPE = pLifeIDPaidObj.MARKETING_TYPE;
                        PAR_PLAN_PAID.BLA_TITLE = txtPaidPlanName.Text.Trim();
                        PAR_PLAN_PAID.SPOUSE_FLG = pLifeIDPaidObj.SPOUSE_FLG;


                        //END เก็บค่า แบบประกันซื้อ ใน Object สาธารณะ ====================================================================
                    }

                    NewBisPASvcRef.U_APPLICATION_Collection uApplicationRemoveObjColl = new NewBisPASvcRef.U_APPLICATION_Collection();
                    if (PAR_PA_APPLICATION_ID != null && PAR_PA_APPLICATION_ID.APP_ID != null)
                    {
                        if (PAR_PA_APPLICATION_ID.APPLICATION_Collection != null && PAR_PA_APPLICATION_ID.APPLICATION_Collection.Count() > 0)
                        {
                            foreach (NewBisPASvcRef.U_APPLICATION obj in PAR_PA_APPLICATION_ID.APPLICATION_Collection)
                            {
                                if (obj.LIFE_ID != null && obj.LIFE_ID.UAPP_ID != null)
                                {
                                    if (obj.LIFE_ID.FREE_FLG == "N")
                                    {
                                        if (ckbLifeBuy.Checked == false)
                                        {
                                            uApplicationRemoveObjColl.Add(obj);
                                        }
                                    }
                                    if (obj.LIFE_ID.FREE_FLG == "Y" || IsChannelTypeFreePolicy(channelType))
                                    {
                                        if (ckbLifeFree.Checked == false)
                                        {
                                            uApplicationRemoveObjColl.Add(obj);
                                        }
                                    }
                                }
                            }
                        }
                    }

                    if (uApplicationRemoveObjColl != null && uApplicationRemoveObjColl.Count() > 0)
                    {
                        foreach (NewBisPASvcRef.U_APPLICATION obj in uApplicationRemoveObjColl)
                        {
                            if (PAR_PA_APPLICATION_ID.APPLICATION_Collection != null && PAR_PA_APPLICATION_ID.APPLICATION_Collection.Count() > 0)
                            {
                                PAR_PA_APPLICATION_ID.APPLICATION_Collection.Remove(obj);
                            }
                        }
                    }

                }

                txtLifePremium.Text = (totalPremium ?? 0).ToString("n0");
                // checkmode  
                //totalPremium = (PAR_PA_APPLICATION_ID.APPLICATION_Collection.First( i => i.).LIFE_ID.P_MODE == 12 ? (totalPremium ?? 0) * 2 : (totalPremium ?? 0));
                txtTotalPremium.Text = totalPremium.Value.ToString("n0");


                txtSuspensePremium.Text = suspensePremium.Value.ToString("n0");
                overPremium = (suspensePremium - totalPremium);
                txtOverPremium.Text = overPremium.Value.ToString("n0");

                //ตัวแปรที่มีผลต่อการคำนวนเบี้ย =================
                OLD_BIRTH_DATE = txtBirthDt.Text.Trim();
                OLD_BIRTH_DATE_SPOUSE = txtSpBirthDate.Text.Trim();
                OLD_PLAN_FREE = ckbLifeFree.Checked;
                OLD_PLAN_PAID = ckbLifeBuy.Checked;
                OLD_SPOUSE = ckbSpouse.Checked;
                OLD_PLAN_CODE_FREE = txtFreePlcode.Text.Trim() + txtFreePlcode2.Text.Trim();
                OLD_MODE_FREE = cmbFreePmode.SelectedValue.ToString();
                OLD_SUMM_FREE = txtFreeSumm.Text.Trim();
                OLD_ISU_DT_FREE = txtFreeIsuDate.Text.Trim();
                OLD_PLAN_CODE_PAID = txtPaidPlcode.Text.Trim() + txtPaidPlcode2.Text.Trim();
                OLD_MODE_PAID = cmbPaidPmode.SelectedValue.ToString();
                OLD_SUMM_PAID = txtPaidSumm.Text.Trim();
                OLD_ISU_DT_PAID = txtPaidIsuDate.Text.Trim();
                //จบตัวแปรที่มีผลต่อการคำนวนเบี้ย ===============
                MessageBox.Show("คำนวนเบี้ยประกันเสร็จเรียบร้อยแล้ว");
            }
            catch (Exception ex)
            {
                PLAN_ERROR = true;
                txtTotalPremium.Text = "";
                txtLifePremium.Text = "";
                SetMsgException(ex);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void btnHeadSave_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                if (childForm != null)
                {
                    foreach (Form item in childForm)
                    {
                        item.Close();
                    }
                }

                ValidateParameter();

                try
                {
                    EditData();

                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                SetMsgException(ex);
            }
            finally
            {
                this.Cursor = Cursors.Default;

            }

        }
        private void txtSpBirthDate_Leave(object sender, EventArgs e)
        {
            try
            {
                ChkDateForTextBox(txtSpBirthDate.Text, "วันเกิดคู่สมรส", txtSpBirthDate);
            }
            catch (Exception ex)
            {
                SetMsgException(ex);
            }
        }

        private void txtSpBirthDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                try
                {
                    if (txtSpBirthDate.Text.Trim() != "")
                    {
                        DateTime? d = Utility.StringToDateTime(txtSpBirthDate.Text.Trim(), "BU");
                        if (d == null)
                        {
                            txtSpBirthDate.Text = "";
                            txtSpBirthDate.Focus();
                            throw new Exception("รูปแบบวันที่ วันเกิดคู่สมรส ไม่ถูกต้อง");
                        }
                    }
                    long? ageCal = null;
                    NewBisPASvcRef.ProcessResult pr = new NewBisPASvcRef.ProcessResult();
                    using (NewBisPASvcRef.NewBisPASvcClient clientAge = new NewBisPASvcRef.NewBisPASvcClient())
                    {
                        if (cmbChannelType.SelectedValue == null || cmbChannelType.SelectedValue.ToString() == "")
                        {
                            cmbChannelType.Focus();
                            throw new Exception("กรุณาระบุช่องทางการขายที่ท่านต้องการ");
                        }
                        String channelType = cmbChannelType.SelectedValue.ToString();
                        DateTime? dateCal = null;
                        dateCal = Utility.StringToDateTime(txtAppSignDt.Text, "BU");
                        DateTime? birthDateCal = Utility.StringToDateTime(txtSpBirthDate.Text, "BU");
                        ageCal = clientAge.GetAge(out pr, birthDateCal, dateCal, dateCal, channelType);
                        if (pr.Successed == false)
                        {
                            throw new Exception(pr.ErrorMessage);
                        }
                    }
                    txtSpAge.Text = ageCal == null ? "" : ageCal.Value.ToString();
                }
                catch (Exception ex)
                {
                    SetMsgException(ex);
                }
            }
        }

        private void btnBenefitSave_Click(object sender, EventArgs e)
        {
            try
            {
                //if (cmbBenefitType.SelectedValue.ToString() == "")
                //{
                //    cmbBenefitType.Focus();
                //    throw new Exception("กรุณาระบุประเภทของผู้รับผลประโยชน์");
                //}

                if (cmbBenefitType.SelectedValue.ToString() == "1")
                {
                    if (txtBenefitPrename.Text.Trim() == "")
                    {
                        txtBenefitPrename.Focus();
                        throw new Exception("กรุณาระบุคำนำหน้าชื่อของผู้รับผลประโยชน์");
                    }

                    if (txtBenefitName.Text.Trim() == "")
                    {
                        txtBenefitName.Focus();
                        throw new Exception("กรุณาระบุชื่อของผู้รับผลประโยชน์");
                    }

                    if (txtBenefitSurname.Text.Trim() == "")
                    {
                        txtBenefitSurname.Focus();
                        throw new Exception("กรุณาระบุนามสกุลของผู้รับผลประโยชน์");
                    }

                    if (txtBenefitRelaton.Text.Trim() == "")
                    {
                        txtBenefitRelaton.Focus();
                        throw new Exception("กรุณาระบุความสัมพันธ์ของผู้รับผลประโยชน์");
                    }

                    if (txtBenefitGainPercent.Text.Trim() == "")
                    {
                        txtBenefitGainPercent.Focus();
                        throw new Exception("กรุณาระบุส่วนแบ่งของผู้รับผลประโยชน์");
                    }
                    if (txtBenefitGainPercent.Text.Trim() != "")
                    {
                        Decimal i = 0;
                        if (!Decimal.TryParse(txtBenefitGainPercent.Text.Trim(), out i))
                        {
                            txtBenefitGainPercent.Focus();
                            throw new Exception("กรุณาระบุส่วนแบ่งของผู้รับผลประโยชน์เป็นตัวเลขเท่านั้น");
                        }

                        Decimal? gainPercent = txtBenefitGainPercent.Text.Trim() == "" ? 0 : Convert.ToDecimal(txtBenefitGainPercent.Text.Trim());
                        if (gainPercent <= 0)
                        {
                            txtBenefitGainPercent.Focus();
                            throw new Exception("กรุณาระบุส่วนแบ่งของผู้รับผลประโยชน์ต้องมีค่ามากกว่า 0");
                        }
                    }

                    //if (cmbBenefitClearFlag.SelectedValue.ToString() == "")
                    //{
                    //    cmbBenefitClearFlag.Focus();
                    //    throw new Exception("กรุณาระบุการตรวจสอบของผู้รับผลประโยชน์");
                    //}

                }

                if (cmbBenefitType.SelectedValue.ToString() == "2")
                {
                    if (txtBenefitMessage.Text.Trim() == "")
                    {
                        txtBenefitMessage.Focus();
                        throw new Exception("กรุณาข้อความของผู้รับผลประโยชน์");
                    }
                }

                if (txtBenefitNo.Text == "")
                {
                    //Insert U_BENEFIT_ID ==================================================
                    int? rowNo = 0;
                    if (!(PAR_BENEFIT_ID_COLL_TMP != null && PAR_BENEFIT_ID_COLL_TMP.Count() > 0))
                    {
                        PAR_BENEFIT_ID_COLL_TMP = new NewBisPASvcRef.U_BENEFIT_ID_COLLECTION();
                        rowNo = 1;
                    }
                    else
                    {
                        rowNo = (PAR_BENEFIT_ID_COLL_TMP.Count() + 1);
                    }

                    NewBisPASvcRef.U_BENEFIT_ID uBenefitIDObj = new NewBisPASvcRef.U_BENEFIT_ID();
                    uBenefitIDObj.CLEAR_FLG = Convert.ToChar(cmbBenefitClearFlag.SelectedValue.ToString());
                    uBenefitIDObj.TMN = ckbBenefitTmn.Checked == true ? 'Y' : 'N';
                    uBenefitIDObj.SPOUSE_FLG = cmbSpouseFlg.SelectedValue.ToString() == "" ? (char?)null : Convert.ToChar(cmbSpouseFlg.SelectedValue.ToString());

                    uBenefitIDObj.APP_BENEFIT = new NewBisPASvcRef.U_APP_BENEFIT();
                    uBenefitIDObj.APP_BENEFIT.SEQ = rowNo;
                    uBenefitIDObj.BENEFIT_TYPE = cmbBenefitType.SelectedValue.ToString();

                    uBenefitIDObj.BENEFIT_PERSON = new NewBisPASvcRef.U_BENEFIT_PERSON();
                    uBenefitIDObj.BENEFIT_MESSAGE = new NewBisPASvcRef.U_BENEFIT_MESSAGE();

                    if (cmbBenefitType.SelectedValue.ToString() == "1")
                    {
                        uBenefitIDObj.BENEFIT_PERSON.PRENAME = txtBenefitPrename.Text.Trim();
                        uBenefitIDObj.BENEFIT_PERSON.NAME = txtBenefitName.Text.Trim();
                        uBenefitIDObj.BENEFIT_PERSON.SURNAME = txtBenefitSurname.Text.Trim();
                        uBenefitIDObj.BENEFIT_PERSON.RELATION = txtBenefitRelaton.Text.Trim();
                        uBenefitIDObj.BENEFIT_PERSON.GAIN_PERCENT = txtBenefitGainPercent.Text.Trim() == "" ? 0 : Convert.ToDecimal(txtBenefitGainPercent.Text.Trim());

                        if (PAR_BENEFIT_ID_COLL_TMP != null && PAR_BENEFIT_ID_COLL_TMP.Count() > 0)
                        {

                            if (uBenefitIDObj.SPOUSE_FLG == 'Y')
                            {
                                var GetDataSp = from getData in PAR_BENEFIT_ID_COLL_TMP
                                                where getData.SPOUSE_FLG == 'Y'
                                                && getData.TMN == 'N'
                                                select getData;
                                decimal? totalGainPercent = 0;
                                if (GetDataSp != null && GetDataSp.Count() > 0)
                                {

                                    foreach (NewBisPASvcRef.U_BENEFIT_ID objDetail in GetDataSp.ToArray())
                                    {
                                        if (objDetail.BENEFIT_PERSON != null && objDetail.BENEFIT_PERSON.GAIN_PERCENT != null)
                                        {
                                            totalGainPercent = totalGainPercent + objDetail.BENEFIT_PERSON.GAIN_PERCENT;
                                        }
                                    }
                                }
                                if (uBenefitIDObj.TMN == 'N')
                                {
                                    totalGainPercent = totalGainPercent + uBenefitIDObj.BENEFIT_PERSON.GAIN_PERCENT;
                                }

                                if (totalGainPercent > 100)
                                {
                                    txtBenefitGainPercent.Focus();
                                    throw new Exception("ผู้รับผลประโยชน์ของคู่สมรสมีส่วนแบ่งร่วมเกิน 100%");
                                }
                            }
                            else
                            {
                                var GetDataCus = from getData in PAR_BENEFIT_ID_COLL_TMP
                                                 where getData.SPOUSE_FLG != 'Y'
                                                 && getData.TMN == 'N'
                                                 select getData;
                                decimal? totalGainPercent = 0;
                                if (GetDataCus != null && GetDataCus.Count() > 0)
                                {
                                    foreach (NewBisPASvcRef.U_BENEFIT_ID objDetail in GetDataCus.ToArray())
                                    {
                                        if (objDetail.BENEFIT_PERSON != null && objDetail.BENEFIT_PERSON.GAIN_PERCENT != null)
                                        {
                                            totalGainPercent = totalGainPercent + objDetail.BENEFIT_PERSON.GAIN_PERCENT;
                                        }
                                    }
                                }

                                if (uBenefitIDObj.TMN == 'N')
                                {
                                    totalGainPercent = totalGainPercent + uBenefitIDObj.BENEFIT_PERSON.GAIN_PERCENT;
                                }

                                if (totalGainPercent > 100)
                                {
                                    txtBenefitGainPercent.Focus();
                                    throw new Exception("ผู้รับผลประโยชน์ของผู้เอาประกันมีส่วนแบ่งร่วมเกิน 100%");
                                }
                            }
                        }
                    }
                    else if (cmbBenefitType.SelectedValue.ToString() == "2")
                    {
                        uBenefitIDObj.BENEFIT_MESSAGE.MESSAGE = txtBenefitMessage.Text.Trim();
                    }



                    PAR_BENEFIT_ID_COLL_TMP.Add(uBenefitIDObj);
                    //END Insert U_BENEFIT_ID ==============================================
                }
                else
                {
                    //Update U_BENEFIT_ID ==================================================
                    if (cmbBenefitType.SelectedValue.ToString() == "1")
                    {
                        PAR_BENEFIT_ID_OBJ.BENEFIT_PERSON.PRENAME = txtBenefitPrename.Text.Trim();
                        PAR_BENEFIT_ID_OBJ.BENEFIT_PERSON.NAME = txtBenefitName.Text.Trim();
                        PAR_BENEFIT_ID_OBJ.BENEFIT_PERSON.SURNAME = txtBenefitSurname.Text.Trim();
                        PAR_BENEFIT_ID_OBJ.BENEFIT_PERSON.RELATION = txtBenefitRelaton.Text.Trim();
                        PAR_BENEFIT_ID_OBJ.BENEFIT_PERSON.GAIN_PERCENT = txtBenefitGainPercent.Text.Trim() == "" ? (decimal?)null : Convert.ToDecimal(txtBenefitGainPercent.Text.Trim());

                        if (PAR_BENEFIT_ID_COLL_TMP != null && PAR_BENEFIT_ID_COLL_TMP.Count() > 0)
                        {
                            var GetDataCus = from getData in PAR_BENEFIT_ID_COLL_TMP
                                             where getData.SPOUSE_FLG != 'Y'
                                             && getData.TMN == 'N'
                                             && getData.APP_BENEFIT.SEQ != PAR_BENEFIT_ID_OBJ.APP_BENEFIT.SEQ
                                             select getData;
                            decimal? totalGainPercentCus = 0;
                            if (GetDataCus != null && GetDataCus.Count() > 0)
                            {
                                foreach (NewBisPASvcRef.U_BENEFIT_ID objDetail in GetDataCus.ToArray())
                                {
                                    if (objDetail.BENEFIT_PERSON != null && objDetail.BENEFIT_PERSON.GAIN_PERCENT != null)
                                    {
                                        totalGainPercentCus = totalGainPercentCus + objDetail.BENEFIT_PERSON.GAIN_PERCENT;
                                    }
                                }
                            }

                            if (PAR_BENEFIT_ID_OBJ.SPOUSE_FLG != 'Y' && PAR_BENEFIT_ID_OBJ.TMN == 'N')
                            {
                                if (PAR_BENEFIT_ID_OBJ.BENEFIT_PERSON != null && PAR_BENEFIT_ID_OBJ.BENEFIT_PERSON.GAIN_PERCENT != null)
                                {
                                    totalGainPercentCus = totalGainPercentCus + PAR_BENEFIT_ID_OBJ.BENEFIT_PERSON.GAIN_PERCENT;
                                }
                            }


                            if (totalGainPercentCus > 100)
                            {
                                txtBenefitGainPercent.Focus();
                                throw new Exception("ผู้รับผลประโยชน์ของผู้เอาประกันมีส่วนแบ่งร่วมเกิน 100%");
                            }


                            var GetDataSp = from getData in PAR_BENEFIT_ID_COLL_TMP
                                            where getData.SPOUSE_FLG == 'Y'
                                            && getData.TMN == 'N'
                                            && getData.APP_BENEFIT.SEQ != PAR_BENEFIT_ID_OBJ.APP_BENEFIT.SEQ
                                            select getData;
                            decimal? totalGainPercentSp = 0;
                            if (GetDataSp != null && GetDataSp.Count() > 0)
                            {

                                foreach (NewBisPASvcRef.U_BENEFIT_ID objDetail in GetDataSp.ToArray())
                                {
                                    if (objDetail.BENEFIT_PERSON != null && objDetail.BENEFIT_PERSON.GAIN_PERCENT != null)
                                    {
                                        totalGainPercentSp = totalGainPercentSp + objDetail.BENEFIT_PERSON.GAIN_PERCENT;
                                    }
                                }

                            }
                            if (PAR_BENEFIT_ID_OBJ.SPOUSE_FLG == 'Y' && PAR_BENEFIT_ID_OBJ.TMN == 'N')
                            {
                                if (PAR_BENEFIT_ID_OBJ.BENEFIT_PERSON != null && PAR_BENEFIT_ID_OBJ.BENEFIT_PERSON.GAIN_PERCENT != null)
                                {
                                    totalGainPercentSp = totalGainPercentSp + PAR_BENEFIT_ID_OBJ.BENEFIT_PERSON.GAIN_PERCENT;
                                }
                            }

                            if (totalGainPercentSp > 100)
                            {
                                txtBenefitGainPercent.Focus();
                                throw new Exception("ผู้รับผลประโยชน์ของคู่สมรสมีส่วนแบ่งร่วมเกิน 100%");
                            }
                        }
                    }
                    else if (cmbBenefitType.SelectedValue.ToString() == "2")
                    {
                        PAR_BENEFIT_ID_OBJ.BENEFIT_MESSAGE.MESSAGE = txtBenefitMessage.Text.Trim();
                    }

                    PAR_BENEFIT_ID_OBJ.APP_BENEFIT.SEQ = txtBenefitNo.Text.Trim() == "" ? (int?)null : Convert.ToInt16(txtBenefitNo.Text.Trim());
                    PAR_BENEFIT_ID_OBJ.UBENEFIT_ID = txtBenefitID.Text.Trim() == "" ? (long?)null : Convert.ToInt64(txtBenefitID.Text.Trim());
                    PAR_BENEFIT_ID_OBJ.CLEAR_FLG = Convert.ToChar(cmbBenefitClearFlag.SelectedValue.ToString());
                    PAR_BENEFIT_ID_OBJ.TMN = ckbBenefitTmn.Checked == true ? 'Y' : 'N';
                    PAR_BENEFIT_ID_OBJ.SPOUSE_FLG = cmbSpouseFlg.SelectedValue.ToString() == "" ? (char?)null : Convert.ToChar(cmbSpouseFlg.SelectedValue.ToString());

                    //END Update U_BENEFIT_ID ==============================================
                }

                //ส่วนบันทึกข้อมูล ==============================================================

                ClearControlsBenefitAdd();
                DisplayBenefitPanal();

                //END ส่วนบันทึกข้อมูล ==========================================================
            }
            catch (Exception ex)
            {
                SetMsgException(ex);
            }
        }

        private void ClearControlsBenefitAdd()
        {
            txtBenefitID.Text = "";
            txtBenefitNo.Text = "";
            cmbBenefitType.SelectedValue = "1";
            txtBenefitPrename.Text = "";
            txtBenefitName.Text = "";
            txtBenefitSurname.Text = "";
            txtBenefitRelaton.Text = "";
            txtBenefitGainPercent.Text = "";
            cmbBenefitClearFlag.SelectedValue = "Y";
            ckbBenefitTmn.Checked = false;
            txtBenefitMessage.Text = "";

            txtBenefitPrename.ReadOnly = false;
            txtBenefitName.ReadOnly = false;
            txtBenefitSurname.ReadOnly = false;
            txtBenefitRelaton.ReadOnly = false;
            txtBenefitGainPercent.ReadOnly = false;
            txtBenefitMessage.ReadOnly = false;
            cmbBenefitType.Enabled = true;
            btnBenefitSave.Text = "บันทึก";
            cmbSpouseFlg.SelectedValue = "";
            if (cmbChannelType.SelectedValue.ToString() != "PD" && cmbChannelType.SelectedValue.ToString() != "PF")
            {
                cmbSpouseFlg.Enabled = false;
            }
            else
            {
                cmbSpouseFlg.Enabled = true;
            }

        }
        private void DisplayBenefitPDPanal()
        {
            if (PAR_BENEFIT_ID_COLL_TMP != null && PAR_BENEFIT_ID_COLL_TMP.Count() > 0)
            {
                while (panelBenefitList.Controls.Count > 0)
                {
                    panelBenefitList.Controls.RemoveAt(0);
                }
                int i = 0;

                foreach (NewBisPASvcRef.U_BENEFIT_ID obj in PAR_BENEFIT_ID_COLL_TMP)
                {
                    UserControlsForm.BenefitControl benefitControlList = new UserControlsForm.BenefitControl();
                    benefitControlList.Name = "benefitControlList";
                    benefitControlList.Location = new Point(0, 0 + (i * 86));
                    benefitControlList.Size = new Size(1227, 86);
                    benefitControlList.Tag = obj;

                    benefitControlList.txtBnfNo.Text = i.ToString();
                    if (obj.BENEFIT_TYPE == "1")
                    {
                        benefitControlList.txtBnfTypeCode.Text = obj.BENEFIT_TYPE;
                        benefitControlList.txtBnfPrename.Text = obj.BENEFIT_PERSON.PRENAME;
                        benefitControlList.txtBnfName.Text = obj.BENEFIT_PERSON.NAME;
                        benefitControlList.txtBnfSurname.Text = obj.BENEFIT_PERSON.SURNAME;
                        benefitControlList.txtBnfRelation.Text = obj.BENEFIT_PERSON.RELATION;
                        benefitControlList.txtBnfGainPercent.Text = obj.BENEFIT_PERSON.GAIN_PERCENT == null ? "" : obj.BENEFIT_PERSON.GAIN_PERCENT.Value.ToString("n2");
                        benefitControlList.txtBnfMessage.Text = "";
                    }
                    else if (obj.BENEFIT_TYPE == "2")
                    {
                        benefitControlList.txtBnfTypeCode.Text = obj.BENEFIT_TYPE;
                        benefitControlList.txtBnfTypeCode.Text = "1";
                        benefitControlList.txtBnfPrename.Text = "";
                        benefitControlList.txtBnfName.Text = "";
                        benefitControlList.txtBnfSurname.Text = "";
                        benefitControlList.txtBnfRelation.Text = "";
                        benefitControlList.txtBnfGainPercent.Text = "";
                        benefitControlList.txtBnfMessage.Text = obj.BENEFIT_MESSAGE.MESSAGE;
                    }
                    var bnfTyp = (from getBenefitType in benefitTypeArr where getBenefitType.CODE == obj.BENEFIT_TYPE select getBenefitType.DESCRIPTION).FirstOrDefault();

                    benefitControlList.txtBnfType.Text = bnfTyp;
                    benefitControlList.txtBnfClearFlag.Text = obj.CLEAR_FLG.Value.ToString();
                    benefitControlList.txtBnfClear.Text = (from getClear in benefitClearArr where getClear.CODE == benefitControlList.txtBnfClearFlag.Text select getClear.DESCRIPTION).First();
                    benefitControlList.txtBnfStatus.Text = obj.TMN.Value == 'N' ? "ใช้งาน" : "ยกเลิก";
                    benefitControlList.txtBnfTmn.Text = obj.TMN == null ? "" : obj.TMN.Value.ToString();
                    benefitControlList.txtBnfID.Text = obj.UBENEFIT_ID == null ? "" : obj.UBENEFIT_ID.Value.ToString();

                    if (obj.SPOUSE_FLG == 'Y')
                    {
                        benefitControlList.txtSpFlg.Text = "คู่สมรส";
                        benefitControlList.BackColor = Color.FromArgb(192, 255, 192);
                    }
                    else
                    {
                        benefitControlList.txtSpFlg.Text = "ผู้เอาประกัน";
                        benefitControlList.BackColor = Color.FromArgb(255, 224, 192);
                    }

                    if (obj.CLEAR_FLG == 'Y')
                    {
                        benefitControlList.txtBnfClear.BackColor = SystemColors.Control;
                    }
                    else
                    {
                        benefitControlList.txtBnfClear.BackColor = Color.FromArgb(bgRed[0], bgRed[1], bgRed[2]);
                    }

                    benefitControlList.btnBnfEdit.Tag = obj;
                    benefitControlList.btnBnfEdit.Click += new EventHandler(btnBnfEdit_Click);

                    panelBenefitList.Controls.Add(benefitControlList);
                    i = i + 1;
                }
            }
            else
            {
                panelBenefitList.Controls.Clear();
            }
        }
        private void DisplayBenefitPanal()
        {
            if (PAR_BENEFIT_ID_COLL_TMP != null && PAR_BENEFIT_ID_COLL_TMP.Count() > 0)
            {
                while (panelBenefitList.Controls.Count > 0)
                {
                    panelBenefitList.Controls.RemoveAt(0);
                }
                int i = 0;

                foreach (NewBisPASvcRef.U_BENEFIT_ID obj in PAR_BENEFIT_ID_COLL_TMP)
                {
                    UserControlsForm.BenefitControl benefitControlList = new UserControlsForm.BenefitControl();
                    benefitControlList.Name = "benefitControlList";
                    benefitControlList.Location = new Point(0, 0 + (i * 86));
                    benefitControlList.Size = new Size(1227, 86);
                    benefitControlList.Tag = obj;

                    benefitControlList.txtBnfNo.Text = obj.APP_BENEFIT.SEQ == null ? "" : obj.APP_BENEFIT.SEQ.Value.ToString();
                    if (obj.BENEFIT_TYPE == "1")
                    {
                        benefitControlList.txtBnfTypeCode.Text = obj.BENEFIT_TYPE;
                        benefitControlList.txtBnfPrename.Text = obj.BENEFIT_PERSON.PRENAME;
                        benefitControlList.txtBnfName.Text = obj.BENEFIT_PERSON.NAME;
                        benefitControlList.txtBnfSurname.Text = obj.BENEFIT_PERSON.SURNAME;
                        benefitControlList.txtBnfRelation.Text = obj.BENEFIT_PERSON.RELATION;
                        benefitControlList.txtBnfGainPercent.Text = obj.BENEFIT_PERSON.GAIN_PERCENT == null ? "" : obj.BENEFIT_PERSON.GAIN_PERCENT.Value.ToString("n2");
                        benefitControlList.txtBnfMessage.Text = "";
                    }
                    else if (obj.BENEFIT_TYPE == "2")
                    {
                        benefitControlList.txtBnfTypeCode.Text = obj.BENEFIT_TYPE;
                        benefitControlList.txtBnfTypeCode.Text = "1";
                        benefitControlList.txtBnfPrename.Text = "";
                        benefitControlList.txtBnfName.Text = "";
                        benefitControlList.txtBnfSurname.Text = "";
                        benefitControlList.txtBnfRelation.Text = "";
                        benefitControlList.txtBnfGainPercent.Text = "";
                        benefitControlList.txtBnfMessage.Text = obj.BENEFIT_MESSAGE.MESSAGE;
                    }

                    benefitControlList.txtBnfType.Text = (from getBenefitType in benefitTypeArr where getBenefitType.CODE == obj.BENEFIT_TYPE select getBenefitType.DESCRIPTION).First();
                    benefitControlList.txtBnfClearFlag.Text = obj.CLEAR_FLG.Value.ToString();
                    benefitControlList.txtBnfClear.Text = (from getClear in benefitClearArr where getClear.CODE == benefitControlList.txtBnfClearFlag.Text select getClear.DESCRIPTION).First();
                    benefitControlList.txtBnfStatus.Text = obj.TMN.Value == 'N' ? "ใช้งาน" : "ยกเลิก";
                    benefitControlList.txtBnfTmn.Text = obj.TMN == null ? "" : obj.TMN.Value.ToString();
                    benefitControlList.txtBnfID.Text = obj.UBENEFIT_ID == null ? "" : obj.UBENEFIT_ID.Value.ToString();

                    if (obj.SPOUSE_FLG == 'Y')
                    {
                        benefitControlList.txtSpFlg.Text = "คู่สมรส";
                        benefitControlList.BackColor = Color.FromArgb(192, 255, 192);
                    }
                    else
                    {
                        benefitControlList.txtSpFlg.Text = "ผู้เอาประกัน";
                        benefitControlList.BackColor = Color.FromArgb(255, 224, 192);
                    }

                    if (obj.CLEAR_FLG == 'Y')
                    {
                        benefitControlList.txtBnfClear.BackColor = SystemColors.Control;
                    }
                    else
                    {
                        benefitControlList.txtBnfClear.BackColor = Color.FromArgb(bgRed[0], bgRed[1], bgRed[2]);
                    }

                    benefitControlList.btnBnfEdit.Tag = obj;
                    benefitControlList.btnBnfEdit.Click += new EventHandler(btnBnfEdit_Click);

                    panelBenefitList.Controls.Add(benefitControlList);
                    i = i + 1;
                }
            }
            else
            {
                panelBenefitList.Controls.Clear();
            }
        }

        void btnBnfEdit_Click(object sender, EventArgs e)
        {
            try
            {
                Button btnBnfEdit = (Button)sender;
                NewBisPASvcRef.U_BENEFIT_ID obj = new NewBisPASvcRef.U_BENEFIT_ID();
                obj = (NewBisPASvcRef.U_BENEFIT_ID)btnBnfEdit.Tag;

                cmbBenefitType.Enabled = false;
                txtBenefitNo.Text = obj.APP_BENEFIT.SEQ.Value.ToString();
                cmbBenefitType.SelectedValue = obj.BENEFIT_TYPE;
                cmbBenefitClearFlag.SelectedValue = obj.CLEAR_FLG.Value.ToString();
                ckbBenefitTmn.Checked = obj.TMN == 'Y' ? true : false;
                cmbSpouseFlg.SelectedValue = obj.SPOUSE_FLG == null ? "" : obj.SPOUSE_FLG.Value.ToString();
                txtBenefitID.Text = obj.UBENEFIT_ID == null ? "" : obj.UBENEFIT_ID.Value.ToString();
                if (obj.BENEFIT_TYPE == "1")
                {
                    SetItemBnfPerson();
                    txtBenefitPrename.Text = obj.BENEFIT_PERSON.PRENAME;
                    txtBenefitName.Text = obj.BENEFIT_PERSON.NAME;
                    txtBenefitSurname.Text = obj.BENEFIT_PERSON.SURNAME;
                    txtBenefitRelaton.Text = obj.BENEFIT_PERSON.RELATION;
                    txtBenefitGainPercent.Text = obj.BENEFIT_PERSON.GAIN_PERCENT.Value.ToString();
                    txtBenefitMessage.Text = "";
                }
                else
                {
                    SetItemBnfMessage();
                    txtBenefitPrename.Text = "";
                    txtBenefitName.Text = "";
                    txtBenefitSurname.Text = "";
                    txtBenefitRelaton.Text = "";
                    txtBenefitGainPercent.Text = "";
                    txtBenefitMessage.Text = obj.BENEFIT_MESSAGE.MESSAGE;
                }

                PAR_BENEFIT_ID_OBJ = new NewBisPASvcRef.U_BENEFIT_ID();
                PAR_BENEFIT_ID_OBJ = obj;
                btnBenefitSave.Text = "แก้ไข";
            }
            catch (Exception ex)
            {
                SetMsgException(ex);
            }
        }
        private void SetItemBnfPerson()
        {
            txtBenefitPrename.ReadOnly = false;
            txtBenefitName.ReadOnly = false;
            txtBenefitSurname.ReadOnly = false;
            txtBenefitRelaton.ReadOnly = false;
            txtBenefitGainPercent.ReadOnly = false;
            txtBenefitMessage.ReadOnly = true;
        }
        private void SetItemBnfMessage()
        {
            txtBenefitPrename.ReadOnly = true;
            txtBenefitName.ReadOnly = true;
            txtBenefitSurname.ReadOnly = true;
            txtBenefitRelaton.ReadOnly = true;
            txtBenefitGainPercent.ReadOnly = true;
            txtBenefitMessage.ReadOnly = false;
        }
        private void txtBenefitGainPercent_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtBenefitGainPercent.Text.Trim() != "")
                {
                    ChkIntForTextBox(txtBenefitGainPercent.Text.Trim(), "ส่วนแบ่งผู้รับผลประโยชน์", txtBenefitGainPercent);
                }

            }
            catch (Exception ex)
            {
                SetMsgException(ex);
            }
        }

        private void cmbBenefitType_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                if (cmbBenefitType.SelectedValue != null)
                {
                    if (cmbBenefitType.SelectedValue.ToString() == "1")
                    {
                        SetItemBnfPerson();
                    }
                    else
                    {
                        SetItemBnfMessage();
                    }
                }
            }
            catch (Exception ex)
            {
                SetMsgException(ex);
            }
        }

        private void txtSpIDcardNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                try
                {
                    //if (cmbSpCardType.SelectedValue.ToString() == "")
                    //{
                    //    cmbSpCardType.Focus();
                    //    throw new Exception("ระบุประเภทของบัตรของคู่สมรส");
                    //}
                    //if (txtSpIDcardNo.Text.Trim() == "")
                    //{
                    //    txtSpIDcardNo.Focus();
                    //    throw new Exception("ระบุเลขบัตรประชาชนหรือหนังสือเดินทางของคู่สมรส");
                    //}
                    if (txtSpIDcardNo.Text.Trim() != "" && cmbSpCardType.SelectedValue.ToString() == "")
                    {
                        cmbSpCardType.Focus();
                        throw new Exception("ระบุประเภทของบัตรของคู่สมรส");
                    }
                    if (cmbSpCardType.SelectedValue.ToString() == "1" && txtSpIDcardNo.Text.Trim() != "")
                    {
                        if (Utility.ValidateIDCard(txtSpIDcardNo.Text) == false)
                        {
                            txtSpIDcardNo.Focus();
                            throw new Exception("รูปแบบของเลขที่บัตรประชาชนไม่ถูกต้องของคู่สมรส");
                        }
                    }
                }
                catch (Exception ex)
                {
                    SetMsgException(ex);
                }
            }
        }

        private void txtFreeSumm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                try
                {
                    ChkIntForTextBox(txtFreeSumm.Text.Trim(), "ทุนประกันของแบบประกันแถมฟรี", txtFreeSumm);
                }
                catch (Exception ex)
                {
                    SetMsgException(ex);
                }
            }
        }

        private void txtFreeSumm_Leave(object sender, EventArgs e)
        {
            try
            {
                ChkIntForTextBox(txtFreeSumm.Text.Trim(), "ทุนประกันของแบบประกันแถมฟรี", txtFreeSumm);
            }
            catch (Exception ex)
            {
                SetMsgException(ex);
            }
        }

        private void txtFreeIsuDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                try
                {
                    ChkDateForTextBox(txtFreeIsuDate.Text.Trim(), "วันที่เริ่มคุ้มครองของแบบประกันแถมฟรี", txtFreeIsuDate);
                    if (txtBirthDt.Text.Trim() != "")
                    {
                        Int64? ageCal = null;
                        NewBisPASvcRef.ProcessResult pr = new NewBisPASvcRef.ProcessResult();
                        using (NewBisPASvcRef.NewBisPASvcClient clientAge = new NewBisPASvcRef.NewBisPASvcClient())
                        {
                            DateTime? dateCal = null;
                            dateCal = Utility.StringToDateTime(txtFreeIsuDate.Text, "BU");
                            String channelType = cmbChannelType.SelectedValue.ToString();
                            DateTime? birthDateCal = Utility.StringToDateTime(txtBirthDt.Text, "BU");
                            ageCal = clientAge.GetAge(out pr, birthDateCal, dateCal, dateCal, channelType);
                            if (pr.Successed == false)
                            {
                                txtStAge.Text = "";
                                throw new Exception(pr.ErrorMessage);
                            }
                            txtStAge.Text = ageCal == null ? "" : ageCal.Value.ToString();
                        }
                    }

                    txtPaidIsuDate.Text = txtFreeIsuDate.Text;
                }
                catch (Exception ex)
                {
                    SetMsgException(ex);
                }
            }
        }

        private void txtFreeIsuDate_Leave(object sender, EventArgs e)
        {
            try
            {
                ChkDateForTextBox(txtFreeIsuDate.Text.Trim(), "วันที่เริ่มคุ้มครองของแบบประกันแถมฟรี", txtFreeIsuDate);
            }
            catch (Exception ex)
            {
                SetMsgException(ex);
            }
        }

        private void txtPaidSumm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                try
                {
                    ChkIntForTextBox(txtPaidSumm.Text.Trim(), "ทุนประกันของแบบประกันแบบซื้อ", txtPaidSumm);
                }
                catch (Exception ex)
                {
                    SetMsgException(ex);
                }
            }
        }

        private void txtPaidSumm_Leave(object sender, EventArgs e)
        {
            try
            {
                ChkIntForTextBox(txtPaidSumm.Text.Trim(), "ทุนประกันของแบบประกันแบบซื้อ", txtPaidSumm);
            }
            catch (Exception ex)
            {
                SetMsgException(ex);
            }
        }

        private void txtPaidIsuDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                try
                {
                    ChkDateForTextBox(txtPaidIsuDate.Text.Trim(), "วันที่เริ่มคุ้มครองของแบบประกันแบบซื้อ", txtPaidIsuDate);

                    if (txtBirthDt.Text.Trim() != "")
                    {
                        Int64? ageCal = null;
                        NewBisPASvcRef.ProcessResult pr = new NewBisPASvcRef.ProcessResult();
                        using (NewBisPASvcRef.NewBisPASvcClient clientAge = new NewBisPASvcRef.NewBisPASvcClient())
                        {
                            DateTime? dateCal = null;
                            dateCal = Utility.StringToDateTime(txtPaidIsuDate.Text, "BU");
                            String channelType = cmbChannelType.SelectedValue.ToString();
                            DateTime? birthDateCal = Utility.StringToDateTime(txtBirthDt.Text, "BU");
                            ageCal = clientAge.GetAge(out pr, birthDateCal, dateCal, dateCal, channelType);
                            if (pr.Successed == false)
                            {
                                txtStAge.Text = "";
                                throw new Exception(pr.ErrorMessage);
                            }
                            txtStAge.Text = ageCal == null ? "" : ageCal.Value.ToString();
                        }
                    }

                    txtFreeIsuDate.Text = txtPaidIsuDate.Text;

                }
                catch (Exception ex)
                {
                    SetMsgException(ex);
                }
            }
        }

        private void txtPaidIsuDate_Leave(object sender, EventArgs e)
        {
            try
            {
                ChkDateForTextBox(txtPaidIsuDate.Text.Trim(), "วันที่เริ่มคุ้มครองของแบบประกันแบบซื้อ", txtPaidIsuDate);
            }
            catch (Exception ex)
            {
                SetMsgException(ex);
            }
        }

        private void txtBenefitRelaton_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtBenefitRelaton.Text.Trim() != "")
                {
                    if (PAR_RELATION_COLL != null && PAR_RELATION_COLL.Count() > 0)
                    {
                        var GetData = from getData in PAR_RELATION_COLL
                                      where getData.RELATION == txtBenefitRelaton.Text.Trim()
                                      select getData;
                        if (GetData != null && GetData.Count() > 0)
                        {
                            String ClearFlag = GetData.ToArray()[0].CLEAN_FLG;
                            if (ClearFlag == "Y")
                            {
                                cmbBenefitClearFlag.SelectedValue = "Y";
                                cmbBenefitClearFlag.BackColor = Color.White;
                            }
                            else if (ClearFlag == "N")
                            {
                                cmbBenefitClearFlag.SelectedValue = "N";
                                cmbBenefitClearFlag.BackColor = Color.FromArgb(bgRed[0], bgRed[1], bgRed[2]);
                            }
                        }
                        else
                        {

                        }
                    }

                }
            }
            catch (Exception ex)
            {
                SetMsgException(ex);
            }
        }


        private void txtCardNo4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                try
                {
                    String cardNo = txtCardNo1.Text.Trim() + txtCardNo2.Text.Trim() + txtCardNo3.Text.Trim() + txtCardNo4.Text.Trim();

                    if (cardNo != "")
                    {
                        Decimal i = 0;
                        if (!Decimal.TryParse(cardNo, out i))
                        {
                            txtCardNo1.Focus();
                            txtCardNo1.Text = "";
                            txtCardNo2.Text = "";
                            txtCardNo3.Text = "";
                            txtCardNo4.Text = "";
                            throw new Exception("กรุณาระบุบัตรเครดิตเป็นตัวเลขเท่านั้น");
                        }
                    }

                    if (cardNo.Length < 16)
                    {
                        txtCardNo1.Focus();
                        throw new Exception("กรุณาระบุบัตรเครดิตให้ครบ 16 หลัก");
                    }
                    else
                    {
                        String chkCard1 = "";
                        chkCard1 = txtCardNo1.Text;

                        String[] cardUse = { "4546", "4730", "5444", "4505", "6223", "5237", "5229", "8803" };

                        if (!cardUse.Contains(chkCard1))
                        {
                            //txtCardNo1.Focus();
                            //txtCardNo1.Text = "";
                            //txtCardNo2.Text = "";
                            //txtCardNo3.Text = "";
                            //txtCardNo4.Text = "";
                            //throw new Exception("เลขที่บัตร 4 ตัว หน้าไม่อยู่ในกลุ่ม 4546,4730,5444");
                            MessageBox.Show("เลขที่บัตร 4 ตัว หน้าไม่อยู่ในกลุ่ม 4546,4730,5444,4505,6223,5237,5229,8803");
                        }

                        String bran1 = txtCardNo2.Text.Substring(0, 2);
                        String bran2 = txtCardNo2.Text.Substring(2, 2);
                        NewBisPASvcRef.ProcessResult pr = new NewBisPASvcRef.ProcessResult();
                        NewBisPASvcRef.CDC_DESCRIPTION objDetail = new NewBisPASvcRef.CDC_DESCRIPTION();
                        using (NewBisPASvcRef.NewBisPASvcClient client = new NewBisPASvcRef.NewBisPASvcClient())
                        {
                            objDetail = client.GetCreditCardData(out pr, txtCardNo1.Text.Trim(), bran1, bran2);
                            if (pr.Successed == false)
                            {
                                throw new Exception(pr.ErrorMessage);
                            }
                        }

                        if (objDetail != null)
                        {
                            txtCreditCardName.Text = objDetail.CCDC_THAI_DESC;
                            txtCreditCardTypeName.Text = objDetail.CARD_TYPE_NAME;
                            txtCreditCardType.Text = objDetail.CCDC_CARD_TYPE;
                            txtFinCode.Text = objDetail.CCDC_FIN_CODE;
                        }
                        else
                        {
                            txtCreditCardName.Text = "";
                            txtCreditCardTypeName.Text = "";
                            txtCreditCardType.Text = "";
                            txtFinCode.Text = "";
                            throw new Exception("เลขที่บัตรเครดิตไม่ถูกต้อง");
                        }

                    }
                }
                catch (Exception ex)
                {
                    SetMsgException(ex);
                }
            }
        }

        private void txtExpireMM_Leave(object sender, EventArgs e)
        {
            try
            {
                ChkIntForTextBox(txtExpireMM.Text, "เดือนหมดอายุ", txtExpireMM);
            }
            catch (Exception ex)
            {
                SetMsgException(ex);
            }
        }

        private void txtExpireYY_Leave(object sender, EventArgs e)
        {
            try
            {
                ChkIntForTextBox(txtExpireYY.Text, "ปีหมดอายุ", txtExpireYY);
            }
            catch (Exception ex)
            {
                SetMsgException(ex);
            }
        }

        private void txtCardNo1_Leave(object sender, EventArgs e)
        {
            try
            {
                ChkIntForTextBox(txtCardNo1.Text, "หมายเลขบัตรเครดิต", txtCardNo1);
            }
            catch (Exception ex)
            {
                SetMsgException(ex);
            }
        }

        private void txtCardNo2_Leave(object sender, EventArgs e)
        {
            try
            {
                ChkIntForTextBox(txtCardNo2.Text, "หมายเลขบัตรเครดิต", txtCardNo2);
            }
            catch (Exception ex)
            {
                SetMsgException(ex);
            }
        }

        private void txtCardNo3_Leave(object sender, EventArgs e)
        {
            try
            {
                ChkIntForTextBox(txtCardNo3.Text, "หมายเลขบัตรเครดิต", txtCardNo3);
            }
            catch (Exception ex)
            {
                SetMsgException(ex);
            }
        }

        private void txtCardNo4_Leave(object sender, EventArgs e)
        {
            try
            {
                ChkIntForTextBox(txtCardNo4.Text, "หมายเลขบัตรเครดิต", txtCardNo4);
            }
            catch (Exception ex)
            {
                SetMsgException(ex);
            }
        }

        private void cmbSummryStatus_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {

                String ChannelType = cmbChannelType.SelectedValue == null ? "" : cmbChannelType.SelectedValue.ToString();
                string workGroup = cmbWorkGroup.SelectedValue == null ? "" : cmbWorkGroup.SelectedValue.ToString();

                if (cmbSummryStatus.SelectedValue != null)
                {
                    var summaryStatus = cmbSummryStatus.SelectedValue.ToString();
                    if ((ChannelType == "PO" || ChannelType == "PN") && (PAR_W_SUMMARY == null || string.IsNullOrEmpty(PAR_W_SUMMARY.STATUS)))
                    {
                        if ((PAR_W_SUMMARY == null && (summaryStatus != "WT" && !string.IsNullOrEmpty(summaryStatus))))
                        {
                            throw new Exception("ไม่สามารปรับสถานะเป็น " + cmbSummryStatus.SelectedValue.ToString() + " เนื่องจากต้องผ่านการคัดเคสก่อน");
                        }

                        else if (!(PAR_W_SUMMARY != null && (PAR_W_SUMMARY.STATUS == "WT")))
                        {
                            throw new Exception("ไม่สามารปรับสถานะเป็น " + cmbSummryStatus.SelectedValue.ToString() + " เนื่องจากต้องผ่านการคัดเคสก่อน");
                        }
                    }

                    if (U_APP_REMARK_COLL != null && U_APP_REMARK_COLL.Any() && summaryStatus == "IF")
                    {
                        if (U_APP_REMARK_COLL.Where(i => i.PROBLEM_CLEARING == 'N').Any())
                        {
                            throw new Exception("ไม่สามารปรับสถานะเป็น " + summaryStatus + " เนื่องจากยังไม่ clear ปัญหาในหมายเหตุ");
                        }
                    }
                    String WorkGroup = cmbWorkGroup.SelectedValue == null ? "" : cmbWorkGroup.SelectedValue.ToString();
                    Decimal? premium = txtTotalPremium.Text == "" ? (Decimal?)null : Convert.ToDecimal(txtTotalPremium.Text.Trim().Replace(",", ""));
                    //String statusCode = cmbSStatus.SelectedValue == null ? "WT" : cmbSStatus.SelectedValue.ToString();
                    //calPremium = client.GetCalPremiumByAppNo(out prCalPremium, txtAppNo.Text.Trim(), ChannelType, premium, statusCode);
                    var calPremium = PAR_PREMIUM_SUSPENSE == null ? 0 : PAR_PREMIUM_SUSPENSE.Value;
                    //if (prCalPremium.Successed == false)
                    //{
                    //    cmbSStatus.SelectedValue = PAR_OLD_STATUS == null ? "" : PAR_OLD_STATUS;
                    //    subSubStatusError = PAR_OLD_SUB_STATUS == null ? "" : PAR_OLD_SUB_STATUS;
                    //    ChkError = true;
                    //    throw new Exception(prCalPremium.ErrorMessage);
                    //}


                    Decimal? overPremium = (calPremium - premium);
                    //ในกรณีเป็น IF ตรวจสอบเงินบัญชีพัก===============================                    

                    if ((ChannelType == "PO" || ChannelType == "PN") && cmbSummryStatus.SelectedValue.ToString() == "IF" && (txtNowSuspensePremium.Text == "" || txtNowSuspensePremium.Text == "0" || overPremium < 0 || Convert.ToInt64(txtNowSuspensePremium.Text.Replace(",", "").Replace("-", "")) < 0))
                    {

                        MessageBox.Show("ข้อความเตือน : ข้อมูลนี้มีจำนวนเงินขาดเป็นจำนวนเงิน " + overPremium.Value.ToString("n0"));

                        string user_temp = UserID;
                        string User_Login = UserID;
                        decimal aut_premium = 0;
                        // decimal aut_prem_temp = aut_premium;
                        frmUPaymentAuthority frm1 = new frmUPaymentAuthority(UserID, premium ?? 0, User_Login);
                        frm1.ShowDialog();
                        //UserAppr = frm1.chkUserAppr;
                        aut_premium = frm1.PremiumApprove;
                        if (aut_premium > 0 && frm1.isApproved) //((rdApprProcess.Checked == true) && (aut_premium > 0))
                        {
                            //saveToolStripMenuItem.Enabled = true;
                            cmbSummryStatus.SelectedValue = "IF";
                            GetControlsUnderStatus("IF", "IF");
                        }
                        else
                        {
                            cmbSummryStatus.SelectedValue = "AP";
                            GetControlsUnderStatus("AP", "AS");
                        }

                        //cmbSummryStatus.SelectedValue = "AP";
                        // GetControlsUnderStatus("AP", "AS");
                    }
                    else
                    {
                        GetControlsUnderStatus(cmbSummryStatus.SelectedValue.ToString(), "");
                    }

                    //GetControlsUnderStatus(cmbSummryStatus.SelectedValue.ToString(), "");
                    //END ในกรณีเป็น IF ตรวจสอบเงินบัญชีพัก===========================
                    // ทำการ คัดเคสเป็น WT ก่อนการปรับเป็นสถานะอื่นๆ 


                    //ในกรณีเป็น IF ตรวจสอบว่ามีการเกิด ERROR จากการคำนวนเบี้ยหรือไม่ =======
                    if (cmbSummryStatus.SelectedValue.ToString() == "IF")
                    {
                        bool errorFree = false;
                        if (PAR_U_CAL_ERROR != null && PAR_U_CAL_ERROR.ERROR_DT != null)
                        {
                            if (PAR_U_CAL_ERROR.TMN == 'N')
                            {
                                String strError = "";
                                if (PAR_U_CAL_ERROR.U_CAL_ERROR_DETAIL_Coll != null && PAR_U_CAL_ERROR.U_CAL_ERROR_DETAIL_Coll.Count() > 0)
                                {
                                    if (PAR_U_CAL_ERROR.U_CAL_ERROR_DETAIL_Coll.Count() == 1)
                                    {
                                        var GetDataErrorSummFree = from getData in PAR_U_CAL_ERROR.U_CAL_ERROR_DETAIL_Coll
                                                                   where getData.ERROR_CODE == "ER003"
                                                                   select getData;
                                        if (GetDataErrorSummFree != null && GetDataErrorSummFree.Count() > 0)
                                        {
                                            errorFree = true;
                                            MessageBox.Show("ทุนรวมของแบบประกันแถมฟรี เกิน 300,000 ถ้าต้องการออกกรมธรรม์โดยยกเลิกแบบประกันแถมฟรี ให้เลือกถานะหลักเป็น ออกกรมธรรม์ และสถานะย่อยเป็น (CC) ออกกรมธรรม์โดยยกเลิกกรมธรรม์แถมฟรีของ PA บัตรเครดิต");
                                        }
                                        else
                                        {
                                            int i = 0;
                                            foreach (NewBisPASvcRef.U_CAL_ERROR_DETAIL obj in PAR_U_CAL_ERROR.U_CAL_ERROR_DETAIL_Coll)
                                            {
                                                var GetData = from getData in PAR_AUTB_DATADIC_DET_COLLECTION
                                                              where getData.COL_NAME == "ERROR_CODE"
                                                              && getData.CODE == obj.ERROR_CODE
                                                              select getData;
                                                if (GetData != null && GetData.Count() > 0)
                                                {
                                                    i = i + 1;
                                                    strError = strError + i.ToString() + ". " + GetData.ToArray()[0].DESCRIPTION + "\n";
                                                }
                                            }
                                            throw new Exception("มีข้อผิดพลาดในการคำนวนเบี้ยประกันมีรายละเอียดดังนี้\n" + strError);
                                        }
                                    }
                                    else
                                    {
                                        int i = 0;
                                        foreach (NewBisPASvcRef.U_CAL_ERROR_DETAIL obj in PAR_U_CAL_ERROR.U_CAL_ERROR_DETAIL_Coll)
                                        {
                                            var GetData = from getData in PAR_AUTB_DATADIC_DET_COLLECTION
                                                          where getData.COL_NAME == "ERROR_CODE"
                                                          && getData.CODE == obj.ERROR_CODE
                                                          select getData;
                                            if (GetData != null && GetData.Count() > 0)
                                            {
                                                i = i + 1;
                                                strError = strError + i.ToString() + ". " + GetData.ToArray()[0].DESCRIPTION + "\n";
                                            }
                                        }
                                        throw new Exception("มีข้อผิดพลาดในการคำนวนเบี้ยประกันมีรายละเอียดดังนี้\n" + strError);
                                    }

                                }

                            }
                        }


                        // ตรวจสอบสถานะ เพื่อ auto  checked จัดเรียงเอกสาร 
                        NewBISSvcRef.U_STATUS_ID[] uStatusIDArr;
                        if (PAR_PA_APPLICATION_ID != null && PAR_PA_APPLICATION_ID.APPLICATION_Collection != null && PAR_PA_APPLICATION_ID.APPLICATION_Collection.Any())
                        {
                            long[] uAppIDs = { PAR_PA_APPLICATION_ID.APPLICATION_Collection.First().UAPP_ID.Value };
                            using (NewBISSvcRef.NewBISSvcClient clientDate = new NewBISSvcRef.NewBISSvcClient())
                            {
                                var prDate = clientDate.GetU_STATUS_ID(out uStatusIDArr, uAppIDs, null);
                                if (prDate.Successed == false)
                                {
                                    throw new Exception(prDate.ErrorMessage);
                                }
                            }
                            if (uStatusIDArr != null && uStatusIDArr.Count() > 0)
                            {
                                foreach (NewBISSvcRef.U_STATUS_ID obj in uStatusIDArr)
                                {
                                    if (obj.STATUS == "MO" || obj.STATUS == "MD" || (obj.STATUS == "CO" && obj.SUBSTATUS == "CO"))
                                    {
                                        chbDmsChange.Checked = true;
                                        break;
                                    }
                                }
                            }
                        }


                        if (cmbCardType.SelectedValue.ToString() == "")
                        {
                            cmbCardType.Focus();
                            throw new Exception("ไม่สามารถออกกรมธรรม์ได้ กรุณาระบุประเภทของบัตรประชาชนหรือหนังสือเดินทาง");
                        }

                        if (txtIdcardNo.Text.Trim() == "")
                        {
                            txtIdcardNo.Focus();
                            throw new Exception("ไม่มีข้อมูลบัตรประชาชนหรือหนังสือเดินทาง ไม่สามารถออกกรมธรรม์ได้");
                        }

                        if (cmbCardType.SelectedValue.ToString() == "1" && txtIdcardNo.Text.Trim() != "")
                        {
                            if (Utility.ValidateIDCard(txtIdcardNo.Text) == false)
                            {
                                txtIdcardNo.Focus();
                                throw new Exception("ไม่สามารถออกกรมธรรม์ได้ รูปแบบของเลขที่บัตรประชาชนไม่ถูกต้อง");
                            }
                        }

                        if (ChannelType == "PD" || ChannelType == "PF")
                        {
                            Decimal summBuy = txtNowSumm.Text.Trim() == "" ? 0 : Convert.ToDecimal(txtNowSumm.Text.Trim().Replace(",", ""));
                            Decimal summBuyTotal = txtTotalSumm.Text.Trim() == "" ? 0 : Convert.ToDecimal(txtTotalSumm.Text.Trim().Replace(",", ""));
                            if (summBuy > 0 && summBuyTotal > 3000000)
                            {
                                throw new Exception("ไม่สามารถออกกรมธรรม์ได้ เนื่องจากแบบประกันซื้อมีทุนรวมมากกว่า 3,000,000");
                            }

                            Decimal summFree = txtNowSummFree.Text.Trim() == "" ? 0 : Convert.ToDecimal(txtNowSummFree.Text.Trim().Replace(",", ""));
                            Decimal summFreeTotal = txtTotalSummFree.Text.Trim() == "" ? 0 : Convert.ToDecimal(txtTotalSummFree.Text.Trim().Replace(",", ""));
                            if (summFree > 0 && summFreeTotal > 300000 && errorFree == false)
                            {
                                throw new Exception("ไม่สามารถออกกรมธรรม์ได้ เนื่องจากแบบประกันแถมมีทุนรวมมากกว่า 300,000");
                            }

                            if (ClickTabPlanData == true)
                            {
                                if (ckbSpouse.Checked == true)
                                {
                                    if (cmbSpCardType.SelectedValue.ToString() == "")
                                    {
                                        cmbSpCardType.Focus();
                                        throw new Exception("ไม่สามารถออกกรมธรรม์ได้ กรุณาระบุประเภทของบัตรประชาชนหรือหนังสือเดินทางของคู่สมรส");
                                    }
                                    if (txtSpIDcardNo.Text.Trim() == "")
                                    {
                                        txtSpIDcardNo.Focus();
                                        throw new Exception("ไม่สามารถออกกรมธรรม์ได้ ระบุเลขบัตรประชาชนหรือหนังสือเดินทางของคู่สมรส");
                                    }
                                    if (cmbSpCardType.SelectedValue.ToString() == "1" && txtSpIDcardNo.Text.Trim() != "")
                                    {
                                        if (Utility.ValidateIDCard(txtSpIDcardNo.Text) == false)
                                        {
                                            txtSpIDcardNo.Focus();
                                            throw new Exception("ไม่สามารถออกกรมธรรม์ได้ รูปแบบของเลขที่บัตรประชาชนไม่ถูกต้องของคู่สมรส");
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (PAR_U_SPOUSE_ID_COLL != null && PAR_U_SPOUSE_ID_COLL.Count() > 0)
                                {
                                    NewBisPASvcRef.U_SPOUSE_ID obj = new NewBisPASvcRef.U_SPOUSE_ID();
                                    obj = PAR_U_SPOUSE_ID_COLL.ToArray()[0];
                                    if (String.IsNullOrEmpty(obj.IDCARD_NO) && String.IsNullOrEmpty(obj.PASSPORT))
                                    {
                                        tabMain.SelectTab("tabPlanData");
                                        txtSpIDcardNo.Focus();
                                        throw new Exception("ไม่สามารถออกกรมธรรม์ได้ ระบุ ข้อมูลบัตรประชาชนหรือหนังสือเดินทาง ของคู่สมรส");
                                    }

                                    if (!String.IsNullOrEmpty(obj.IDCARD_NO))
                                    {
                                        if (Utility.ValidateIDCard(obj.IDCARD_NO) == false)
                                        {
                                            tabMain.SelectTab("tabPlanData");
                                            txtSpIDcardNo.Focus();
                                            throw new Exception("ไม่สามารถออกกรมธรรม์ได้ รูปแบบของเลขที่บัตรประชาชนของคู่สมรสไม่ถูกต้อง");
                                        }
                                    }
                                }
                            }
                        }

                        #region "ตรวจสอบข้อมูลลูกค้า IDCARD_NO SEX BIRTH_DATE"
                        if (PAR_P_NAME_ID_COLL != null && PAR_P_NAME_ID_COLL.Count() > 0)
                        {
                            if (NAME_ID != null)
                            {
                                NewBisPASvcRef.P_NAME_ID oldPNameIDObj = new NewBisPASvcRef.P_NAME_ID();
                                var GetData = from getData in PAR_P_NAME_ID_COLL
                                              where getData.NAME_ID == NAME_ID
                                              select getData;
                                if (GetData != null && GetData.Count() > 0)
                                {
                                    oldPNameIDObj = GetData.ToArray()[0];

                                    String idCardNoOld = "";
                                    String sexOld = "";
                                    String birthDateOld = "";

                                    if (cmbCardType.SelectedValue.ToString() == "1")
                                    {
                                        idCardNoOld = oldPNameIDObj.IDCARD_NO == null ? "" : oldPNameIDObj.IDCARD_NO;
                                    }
                                    else if (cmbCardType.SelectedValue.ToString() == "2")
                                    {
                                        idCardNoOld = oldPNameIDObj.PASSPORT == null ? "" : oldPNameIDObj.PASSPORT;
                                    }

                                    sexOld = oldPNameIDObj.SEX == null ? "" : oldPNameIDObj.SEX;
                                    birthDateOld = oldPNameIDObj.BIRTH_DT == null ? "" : Utility.dateTimeToString(oldPNameIDObj.BIRTH_DT.Value, "dd/MM/yyyy", "BU");

                                    if (idCardNoOld != "")
                                    {
                                        if (txtIdcardNo.Text.Trim() != idCardNoOld.Trim())
                                        {
                                            throw new Exception("เลขที่บัตรประชาชนของลูกค้าไม่ตรงกับเลขที่บัตรประชาชนเก่าของลูกค้า เลขที่เก่าคือ " + idCardNoOld.Trim());
                                        }
                                    }

                                    if (cmbSex.SelectedValue.ToString() != sexOld)
                                    {
                                        throw new Exception("เพศลูกค้าไม่ตรงกับเพศเก่าของลูกค้า");
                                    }

                                    if (txtBirthDt.Text.Trim() != birthDateOld)
                                    {
                                        throw new Exception("วันเกิดลูกค้าไม่ตรงกับวันเกิดเก่าของลูกค้า");
                                    }
                                }
                                else
                                {
                                    throw new Exception("ไม่พบข้อมูลลูกค้าเก่าของลูกค้า");
                                }
                            }
                            else
                            {
                                throw new Exception("ข้อมูลลูกค้าเป็นลูกค้าเก่าของบริษัท แต่ท่านไม่เลือกลูกค้าเป็นลูกค้าเก่า");
                            }
                        }

                        if (PAR_P_PARENT_ID_COLL != null && PAR_P_PARENT_ID_COLL.Count() > 0)
                        {
                            if (PARENT_ID == null)
                            {
                                throw new Exception("ข้อมูลลูกค้าเป็นผู้ปกครองเก่าของบริษัท แต่ท่านไม่เลือกผู้ปกครองเป็นลูกค้าเก่า");
                            }
                        }

                        #endregion

                        // check clean  uapplciation Remark
                        var ChkProblem = true;
                        if (U_APP_REMARK_COLL != null && U_APP_REMARK_COLL.Count() > 0)
                        {
                            foreach (var obj in U_APP_REMARK_COLL)
                            {
                                if (obj.PROBLEM_CLEARING == 'N')
                                {
                                    ChkProblem = false;
                                    break;
                                }
                            }

                            if (ChkProblem == false)
                            {

                                // cmbSStatus.SelectedValue = PAR_OLD_STATUS == null ? "" : PAR_OLD_STATUS;
                                //subSubStatusError = PAR_OLD_SUB_STATUS == null ? "" : PAR_OLD_SUB_STATUS;
                                //isError = true;
                                //throw new Exception("มีรายละเอียดของหมายเหตุที่มีสถานะเป็น ยังไม่ clear ปัญหา(ออกกรมธรรม์ไม่ได้)");
                            }
                        }
                        var ekycChannelList = new string[] { "PN", "PO" };
                        if (ekycChannelList.Contains(ChannelType) && workGroup == "TEL" && chk_ekyc_wait_verify.Checked)
                        {
                            MessageBox.Show("กรุณาตรวจสอบเอกสาร ekyc ของผู้เอาประกันก่อนการ ออกกธ.");
                            return;
                        }
                    }





                    //END ในกรณีเป็น IF ตรวจสอบว่ามีการเกิด ERROR จากการคำนวนเบี้ยหรือไม่ ===

                    
                    string subSubStatusError = "";
                    DateTime? policyDate = null;
                    DateTime? strDate = null;
                    DateTime? mktDate = null;
                    DateTime? InstallDate = DateTime.Now;
                    var pr = new NewBISSvcRef.NewBISSvcClient();
                    using (var client = new NewBISSvcRef.NewBISSvcClient())
                    {
                        if (cmbSummryStatus.SelectedValue.ToString() == "IF")
                        {
                            DateTime? IsuDate = null;

                            if (ClickTabPlanData == false)
                            {
                                if (OLD_ISU_DT_FREE != "")
                                {
                                    IsuDate = Utility.StringToDateTime(OLD_ISU_DT_FREE, "BU");
                                }

                                if (OLD_ISU_DT_PAID != "")
                                {
                                    IsuDate = Utility.StringToDateTime(OLD_ISU_DT_PAID, "BU");
                                }
                            }
                            else
                            {
                                if (txtFreeIsuDate.Text.Trim() != "")
                                {
                                    IsuDate = Utility.StringToDateTime(txtFreeIsuDate.Text.Trim(), "BU");
                                }

                                if (txtPaidIsuDate.Text.Trim() != "")
                                {
                                    IsuDate = Utility.StringToDateTime(txtPaidIsuDate.Text.Trim(), "BU");
                                }
                            }



                            if (IsuDate == null)
                            {
                                throw new Exception("ไม่มีข้อมูลวันเริ่มคุ้มครอง");
                            }

                            //ตรวจวันที่เริ่มคุ้มครองกับวันที่บัญชีพักล่าสุด ก่อนออกกรมธรรม์ ==========================================
                            DateTime? suspaenseDate = null;
                            if (txtSuspenseDate.Text.Trim() != "")
                            {
                                suspaenseDate = Utility.StringToDateTime(txtSuspenseDate.Text.Trim(), "BU");
                            }

                            if (suspaenseDate != null)
                            {
                                if (suspaenseDate > IsuDate)
                                {
                                    var channelType = cmbChannelType.SelectedValue.ToString();
                                    var repo = new RenewalRepository();
                                    if (!repo.IsRenewApplication(txtAppNo.Text.Trim(), ChannelType))
                                    {
                                        IsuDate = suspaenseDate;
                                        txtPaidIsuDate.Text = txtSuspenseDate.Text.Trim();
                                        txtUWIsuDate.Text = txtPaidIsuDate.Text;
                                    }
                                    if (channelType == "PD" || channelType == "PF")
                                    {
                                        CalPlanPACreditCard();
                                    }
                                    else if (channelType == "PO" || channelType == "PN")
                                    {
                                        CalPlanOrdinaryPA();
                                    }
                                    else if (channelType == "KB")
                                    {
                                        CalPlanKB();
                                    }


                                    // MessageBox.Show("เนื่องจากวันที่เงินเข้าล่าสุดมากกว่าวันที่เริ่มคุ้มครอง ทำให้วันที่เริ่มคุ้มครองเปลี่ยน กรุณาทำการคำนวนเบี้ยใหม่ก่อนทำการบันทึกข้อมูล");
                                    //cmbSummryStatus.SelectedValue = "";
                                    //GetControlsUnderStatus("", "");
                                    //tabMain.SelectTab("tabPlanData");
                                }
                            }

                            //END ตรวจวันที่เริ่มคุ้มครองกับวันที่บัญชีพักล่าสุด ก่อนออกกรมธรรม์ ======================================


                            DateTime? AppSignDt = Utility.StringToDateTime(txtAppSignDt.Text.Trim(), "BU");

                            var client1 = new NewBisPASvcRef.NewBisPASvcClient();

                            var pr1 = client1.GetDateOfStatusInforceForPA(out policyDate, out strDate, out mktDate, ChannelType, WorkGroup, IsuDate, AppSignDt, InstallDate, SuspenseePayDate);

                            if (pr1.Successed == false)
                            {
                                throw new Exception(pr1.ErrorMessage);
                            }
                        }
                    }

                    if (cmbSummryStatus.SelectedValue.ToString() != "MO" && cmbSummryStatus.SelectedValue.ToString() != "MD")
                    {
                        if (PAR_U_MEMO_ID_COLL_TMP != null && PAR_U_MEMO_ID_COLL_TMP.Count() > 0)
                        {
                            foreach (NewBisPASvcRef.U_MEMO_ID uMemoIDObj in PAR_U_MEMO_ID_COLL_TMP)
                            {
                                ValidateUMemoIDData(uMemoIDObj, cmbSummryStatus.SelectedValue.ToString());
                            }
                        }
                    }


                    String[] statusEnd = { "IF", "CC", "DC", "EX", "NT", "PP" };
                    if (statusEnd.Contains(cmbSummryStatus.SelectedValue.ToString()))
                    {
                        txtApprovedDt.Text = Utility.dateTimeToString(InstallDate.Value, "dd/MM/yyyy", "BU");
                        txtInstallDt.Text = Utility.dateTimeToString(InstallDate.Value, "dd/MM/yyyy", "BU");
                        if (cmbSummryStatus.SelectedValue.ToString() == "IF")
                        {
                            txtPolicyDt.Text = Utility.dateTimeToString(policyDate.Value, "dd/MM/yyyy", "BU");
                            txtStrDt.Text = Utility.dateTimeToString(strDate.Value, "dd/MM/yyyy", "BU");
                            txtMktDate.Text = Utility.dateTimeToString(mktDate.Value, "dd/MM/yyyy", "BU");

                            if (ChannelType == "PF")
                            {
                                txtFreeIsuDate.Text = txtAppSignDt.Text;
                                OLD_ISU_DT_FREE = txtAppSignDt.Text;
                            }
                            if (ChannelType == "PD")
                            {
                                txtUWIsuDate.Text = Utility.dateTimeToString(InstallDate.Value, "dd/MM/yyyy", "BU");
                                tabMain.SelectTab("tabPlanData");
                                if (ckbLifeFree.Checked == true)
                                {
                                    txtFreeIsuDate.Text = txtUWIsuDate.Text;
                                    OLD_ISU_DT_FREE = txtUWIsuDate.Text;
                                }

                                if (ckbLifeBuy.Checked == true)
                                {
                                    txtPaidIsuDate.Text = txtUWIsuDate.Text;
                                    OLD_ISU_DT_PAID = txtUWIsuDate.Text;
                                }

                                tabMain.SelectTab("tabUnderWriteData");
                            }
                        }
                        var wSummarystatus = cmbSummryStatus.SelectedValue.ToString();

                        SetReturnPayData(wSummarystatus, overPremium, ChannelType, calPremium);

                        String agentCode = "";
                        if (workGroup == "BNC")
                        {
                            if (ChannelType == "GM")
                            {
                                Decimal policyHold = txtPolicyHolding.Text == "" ? 0 : Convert.ToDecimal(txtPolicyHolding.Text);
                                if (policyHold >= 8000026)
                                {
                                    agentCode = txtSaleAgent.Text.Trim();
                                }
                                else
                                {
                                    agentCode = txtSaleAgentUpl.Text.Trim();
                                }
                            }
                            else if (ChannelType == "OD")
                            {
                                agentCode = txtSaleAgentUpl.Text.Trim();
                            }
                            else if (ChannelType == "HL")
                            {
                                agentCode = txtSaleAgentUpl.Text.Trim();
                            }

                        }
                        else
                        {
                            agentCode = txtSaleAgent.Text.Trim();
                        }
                        var chkAgent = "";
                        using (NewBISSvcRef.NewBISSvcClient client = new NewBISSvcRef.NewBISSvcClient())
                        {
                            NewBISSvcRef.ProcessResult prCalPremium;
                            chkAgent = client.GetLicenseValidate(out prCalPremium, agentCode);
                            if (prCalPremium.Successed == false)
                            {
                                cmbSummryStatus.SelectedValue = PAR_W_SUMMARY.STATUS == null ? "" : PAR_W_SUMMARY.STATUS;
                                subSubStatusError = PAR_W_SUMMARY.SUBSTATUS == null ? "" : PAR_W_SUMMARY.SUBSTATUS;
                                // isError = true;
                                throw new Exception(prCalPremium.ErrorMessage);
                            }
                        }

                        if (ChannelType == "PO" || ChannelType == "PN")
                        {
                            if (chkAgent == "X")
                            {
                                cmbSummryStatus.SelectedValue = "AE";
                                subSubStatusError = "AG";
                                throw new Exception("ตัวแทนไม่มีบัตรตัวแทน ต้องปรับสถานะเป็น AE");
                            }
                            if (chkAgent == "N")
                            {
                                cmbSummryStatus.SelectedValue = "AE";
                                subSubStatusError = "AG";
                                throw new Exception("ตัวแทนบัตรหมดอายุ ต้องปรับสถานะเป็น AE");
                            }
                        }

                    }


                }


            }
            catch (Exception ex)
            {
                if (PAR_W_SUMMARY != null && PAR_W_SUMMARY.SUMMARY_ID != null)
                {
                    cmbSummryStatus.SelectedValue = PAR_W_SUMMARY.STATUS == null ? "" : PAR_W_SUMMARY.STATUS;
                    GetControlsUnderStatus(cmbSummryStatus.SelectedValue.ToString(), PAR_W_SUMMARY.SUBSTATUS);
                    if (!(String.IsNullOrEmpty(PAR_W_SUMMARY.STATUS_CAUSE)))
                    {
                        cmbSummaryStatusCause.SelectedValue = PAR_W_SUMMARY.STATUS_CAUSE == null ? "" : PAR_W_SUMMARY.STATUS_CAUSE;
                    }
                }
                else
                {
                    cmbSummryStatus.SelectedValue = "WT";
                    GetControlsUnderStatus("WT", "WT");
                }
                SetMsgException(ex);
            }
        }

        private void SetReturnPayData(string wSummarystatus, decimal? overPremium, string ChannelType, decimal calPremium, bool isAppendData = false)
        {

            if (wSummarystatus == "IF")
            {
                // ตรวจสอบเงินเกิน

                String chkAgent = "";
                NewBISSvcRef.ProcessResult prCalPremium = new NewBISSvcRef.ProcessResult();
                //bool ChkProblem = true;



                // String ChannelType = ((NewBISSvcRef.AUTB_CHANNEL)cmbChannelType.SelectedItem).CHANNEL_TYPE;
                // Decimal? premium = txtTotalPremium.Text == "" ? (Decimal?)null : Convert.ToDecimal(txtTotalPremium.Text.Trim().Replace(",", ""));
                //String statusCode = cmbSStatus.SelectedValue == null ? "WT" : cmbSStatus.SelectedValue.ToString();
                //calPremium = client.GetCalPremiumByAppNo(out prCalPremium, txtAppNo.Text.Trim(), ChannelType, premium, statusCode);
                //var calPremium = PAR_PREMIUM_SUSPENSE == null ? 0 : PAR_PREMIUM_SUSPENSE.Value;
                //if (prCalPremium.Successed == false)
                //{
                //    cmbSStatus.SelectedValue = PAR_OLD_STATUS == null ? "" : PAR_OLD_STATUS;
                //    subSubStatusError = PAR_OLD_SUB_STATUS == null ? "" : PAR_OLD_SUB_STATUS;
                //    ChkError = true;
                //    throw new Exception(prCalPremium.ErrorMessage);
                //}

                //Decimal? overPremium = (calPremium - premium);

                if (overPremium > 0)
                {
                    MessageBox.Show("ข้อความเตือน : ข้อมูลนี้มีจำนวนเงินเกินเป็นจำนวนเงิน " + overPremium.Value.ToString("n0"));
                }

                if ((ChannelType == "PO" || ChannelType == "PN") && overPremium >= 1)
                {
                    txtPayMentReturnPrem.Text = overPremium.Value.ToString("n2");
                    gbPayMentReturnPrm.Visible = true;

                    if (!isAppendData)
                    {
                        dgvPayMent.Rows.Clear();
                    }

                    bool chkError = false;
                    String errorMsg = "";
                    chkError = GenPaymentOfReturnPremium(out errorMsg);
                    if (chkError == true)
                    {
                        cmbSummryStatus.SelectedValue = PAR_W_SUMMARY.STATUS == null ? "" : PAR_W_SUMMARY.STATUS;
                        // subSubStatusError = PAR_W_SUMMARY.SUBSTATUS == null ? "" : PAR_W_SUMMARY.SUBSTATUS;
                        //  isError = true;
                        throw new Exception(errorMsg);
                    }
                }
                else
                {
                    txtPayMentReturnPrem.Text = "";
                    //  gbPayMentReturnPrm.Visible = false;
                }


                txtPayMentPremium.Text = txtTotalPremium.Text;
                txtPayMentSuspense.Text = calPremium.ToString("n2");
            }
            else if (wSummarystatus == "CC" || wSummarystatus == "DC" || wSummarystatus == "PP" || wSummarystatus == "EX" || wSummarystatus == "NT")
            {


                NewBISSvcRef.ProcessResult prCalPremium = new NewBISSvcRef.ProcessResult();
                using (NewBISSvcRef.NewBISSvcClient client = new NewBISSvcRef.NewBISSvcClient())
                {
                    string channelType = cmbChannelType.SelectedValue.ToString();
                    Decimal? premiumLife = txtTotalPremium.Text == "" ? (Decimal?)null : Convert.ToDecimal(txtTotalPremium.Text.Trim().Replace(",", ""));
                    String statusCode = wSummarystatus ?? "WT";
                    //calPremium = client.GetCalPremiumByAppNo(out prCalPremium, txtAppNo.Text.Trim(), ChannelType, premium, statusCode);
                    calPremium = PAR_PREMIUM_SUSPENSE == null ? 0 : PAR_PREMIUM_SUSPENSE.Value;
                    //if (prCalPremium.Successed == false)
                    //{
                    //    cmbSStatus.SelectedValue = PAR_OLD_STATUS == null ? "" : PAR_OLD_STATUS;
                    //    subSubStatusError = PAR_OLD_SUB_STATUS == null ? "" : PAR_OLD_SUB_STATUS;
                    //    ChkError = true;
                    //    throw new Exception(prCalPremium.ErrorMessage);
                    //}
                    if ((channelType == "PO" || channelType == "PN") && calPremium >= 100)
                    {
                        txtPayMentPremium.Text = txtTotalPremium.Text;
                        txtPayMentSuspense.Text = calPremium.ToString("n2");
                        txtPayMentReturnPrem.Text = txtPayMentSuspense.Text;

                        gbPayMentReturnPrm.Visible = true;

                        dgvPayMent.Rows.Clear();

                        bool chkError = false;

                        String errorMsg = "";
                        chkError = GenPaymentOfReturnPremium(out errorMsg);
                        if (chkError == true)
                        {
                            cmbSummryStatus.SelectedValue = PAR_W_SUMMARY.STATUS == null ? "" : PAR_W_SUMMARY.STATUS;
                            //subSubStatusError = PAR_W_SUMMARY.SUBSTATUS == null ? "" : PAR_W_SUMMARY.SUBSTATUS;
                            //  isError = true;
                            throw new Exception(errorMsg);
                        }
                    }
                    else
                    {
                        gbPayMentReturnPrm.Visible = false;
                        txtPayMentReturnPrem.Text = "";
                    }
                }

            }
        }

        private void cmbStatusCause_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                if (cmbSummaryStatusCause.SelectedValue != null)
                {
                    var GetData = from getData in PAR_LETTER_SCRIPT_COLL
                                  where getData.STATUS_CAUSE == cmbSummaryStatusCause.SelectedValue.ToString()
                                  select getData;
                    if (GetData != null && GetData.Count() > 0)
                    {
                        lblPPUntil.Visible = true;
                        cmbPPUntil.Visible = true;
                        NewBisPASvcRef.AUTB_LETTER_SCRIPT_COLLECTION letterScriptColl = new NewBisPASvcRef.AUTB_LETTER_SCRIPT_COLLECTION();
                        letterScriptColl.AddRange(GetData.ToArray());
                        cmbPPUntil.DataSource = letterScriptColl;
                        cmbPPUntil.DisplayMember = "DESCRIPTION";
                        cmbPPUntil.ValueMember = "SCRIPT_CODE";
                        cmbPPUntil.SelectedIndex = 0;
                    }
                    else
                    {
                        lblPPUntil.Visible = false;
                        cmbPPUntil.Visible = false;
                        cmbPPUntil.DataSource = null;
                    }
                }
            }
            catch (Exception ex)
            {
                SetMsgException(ex);
            }
        }
        private void tabUnderWriteMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (tabUnderWriteMain.SelectedTab.Name == "tabUnderWrite")
                {
                    txtNowSuspensePremium.Cursor = Cursors.Hand;

                    //if (ClickTabUnderWrite == false)
                    //{
                    //    GetValueForUnderWrite();
                    //}
                    //else
                    //{
                    //    if (PAR_CAL_SUMM == true)
                    //    {
                    //        GetValueForUnderWrite();
                    //    }
                    //}


                    if (ClickTabUnderWrite == false)
                    {
                        ClickTabUnderWrite = true;
                    }

                }
                else if (tabUnderWriteMain.SelectedTab.Name == "tabMemo")
                {
                    if (ClickTabMemo == false)
                    {
                        if (PAR_U_MEMO_ID_COLL_TMP != null && PAR_U_MEMO_ID_COLL_TMP.Count() > 0)
                        {
                            var GetData = from getData in PAR_U_MEMO_ID_COLL_TMP
                                          orderby getData.MEMO_TRN_DT ascending
                                          select getData;
                            if (GetData != null && GetData.Count() > 0)
                            {
                                PAR_U_MEMO_ID_COLL_TMP = new NewBisPASvcRef.U_MEMO_ID_Collection();
                                PAR_U_MEMO_ID_COLL_TMP.AddRange(GetData.ToArray());
                                for (int i = 0; i < PAR_U_MEMO_ID_COLL_TMP.Count(); i++)
                                {
                                    PAR_U_MEMO_ID_COLL_TMP[i].SEQ = i + 1;
                                }
                            }

                            txtMemoIDLimitDate.Focus();
                            ClearControlsOfMemoID();
                            ClearControlsOfMemoDetail();
                            DisplayOfPanelMemoID(PAR_U_MEMO_ID_COLL_TMP);

                            PAR_U_MEMO_ID = new NewBisPASvcRef.U_MEMO_ID();
                            PAR_U_MEMO_ID = PAR_U_MEMO_ID_COLL_TMP.FirstOrDefault(item => item.TMN == 'N') ?? PAR_U_MEMO_ID_COLL_TMP.First();
                            SetBackColorOfPanelMemoList(panelMemoList, PAR_U_MEMO_ID);

                            DisplayOfPanelMemoDetail(PAR_U_MEMO_ID.MEMO_DETAIL_Collection);
                            PAR_U_MEMO_DETAIL = new NewBisPASvcRef.U_MEMO_DETAIL();
                            SetBackColorOfPanelMemoDetailList(panelMemoDetailList, PAR_U_MEMO_DETAIL);
                            gbMemoDetail.Enabled = true;

                        }
                        else
                        {
                            MemoParameter();
                        }
                        ClickTabMemo = true;
                    }
                }
                else if (tabUnderWriteMain.SelectedTab.Name == "tabHistory")
                {
                    if (ClickTabHistory == false)
                    {
                        if (PAR_PA_APPLICATION_ID != null && PAR_PA_APPLICATION_ID.APP_ID != null)
                        {
                            if (PAR_W_UNDERWRITE_ID_COLL != null && PAR_W_UNDERWRITE_ID_COLL.Count() > 0)
                            {
                                NewBisPASvcRef.ProcessResult pr = new NewBisPASvcRef.ProcessResult();
                                NewBisPASvcRef.W_UNDERWRITE_ID wUnderWriteIDObj = new NewBisPASvcRef.W_UNDERWRITE_ID();
                                NewBisPASvcRef.U_LETTER_ID_Collection uLetterIDColl = new NewBisPASvcRef.U_LETTER_ID_Collection();
                                using (NewBisPASvcRef.NewBisPASvcClient client = new NewBisPASvcRef.NewBisPASvcClient())
                                {
                                    pr = client.GetWSummaryHistory(out wUnderWriteIDObj, out uLetterIDColl, PAR_W_UNDERWRITE_ID_COLL.First().UNDERWRITE_ID);
                                    if (pr.Successed == false)
                                    {
                                        throw new Exception(pr.ErrorMessage);
                                    }
                                }

                                if (wUnderWriteIDObj != null && wUnderWriteIDObj.UNDERWRITE_ID != null)
                                {
                                    if (wUnderWriteIDObj.SUBUNDERWRITE_ID_Collection != null && wUnderWriteIDObj.SUBUNDERWRITE_ID_Collection.Count() > 0)
                                    {
                                        NewBisPASvcRef.W_SUBUNDERWRITE_ID wSubUnderwriteIDObj = new NewBisPASvcRef.W_SUBUNDERWRITE_ID();
                                        wSubUnderwriteIDObj = wUnderWriteIDObj.SUBUNDERWRITE_ID_Collection[0];
                                        if (wSubUnderwriteIDObj.SUMMARY_Collection != null && wSubUnderwriteIDObj.SUMMARY_Collection.Count() > 0)
                                        {
                                            while (panelHistory.Controls.Count > 0)
                                            {
                                                panelHistory.Controls.RemoveAt(0);
                                            }

                                            var GetHistory = from getData in wSubUnderwriteIDObj.SUMMARY_Collection
                                                             orderby getData.FSYSTEM_DT descending
                                                             select getData;
                                            if (GetHistory != null && GetHistory.Count() > 0)
                                            {
                                                var historyOrder = GetHistory.OrderBy(item => item.FSYSTEM_DT.Value);
                                                int i = 0;
                                                foreach (NewBisPASvcRef.W_SUMMARY obj in historyOrder)
                                                {
                                                    UserControlsForm.HistoryForm historyForm = new UserControlsForm.HistoryForm();
                                                    historyForm.Name = "historyForm";
                                                    historyForm.Location = new Point(0, 0 + (i * 29));
                                                    historyForm.Size = new Size(1268, 29);
                                                    historyForm.Tag = obj;

                                                    historyForm.txtHisSeq.Text = (i + 1).ToString();

                                                    var GetStatus = from getData in PAR_AUTB_STATUS_COLLECTION
                                                                    where getData.STATUS == obj.STATUS
                                                                    select getData;
                                                    if (GetStatus != null && GetStatus.Count() > 0)
                                                    {
                                                        historyForm.txtHisStatus.Text = GetStatus.First().DESCRIPTION;
                                                    }

                                                    var GetSubStatus = from getData in PAR_AUTB_SUBSTATUS_COLLECTION
                                                                       where getData.STATUS == obj.STATUS
                                                                       && getData.SUBSTATUS == obj.SUBSTATUS
                                                                       select getData;
                                                    if (GetSubStatus != null && GetSubStatus.Count() > 0)
                                                    {
                                                        historyForm.txtHisSubStatus.Text = GetSubStatus.First().DESCRIPTION;
                                                    }

                                                    var GetStatusCause = from getData in PAR_STATUS_CAUSE_COLL
                                                                         where getData.STATUS == obj.STATUS
                                                                         && getData.STATUS_CAUSE == obj.STATUS_CAUSE
                                                                         select getData;
                                                    if (GetStatusCause != null && GetStatusCause.Count() > 0)
                                                    {
                                                        historyForm.txtHisStatusCause.Text = GetStatusCause.First().DESCRIPTION;
                                                    }

                                                    if (obj.ZTB_USER_OBJ != null && !(string.IsNullOrEmpty(obj.ZTB_USER_OBJ.N_USERID)))
                                                    {
                                                        historyForm.txtHisUpdID.Text = obj.ZTB_USER_OBJ.NAME + "  " + obj.ZTB_USER_OBJ.SURNAME;
                                                    }

                                                    historyForm.txtHisFSystemDate.Text = obj.FSYSTEM_DT == null ? "" : Utility.dateTimeToString(obj.FSYSTEM_DT.Value, "dd/MM/yyyy", "BU");

                                                    if (obj.SUMMARY_UNDERWRITER_Collection != null && obj.SUMMARY_UNDERWRITER_Collection.Count() > 0)
                                                    {
                                                        var GetUnderwrite = from getData in obj.SUMMARY_UNDERWRITER_Collection
                                                                            orderby getData.FSYSTEM_DT descending
                                                                            select getData;
                                                        if (GetUnderwrite != null && GetUnderwrite.Count() > 0)
                                                        {
                                                            if (GetUnderwrite.First().ZTB_USER_OBJ != null && !(String.IsNullOrEmpty(GetUnderwrite.First().ZTB_USER_OBJ.N_USERID)))
                                                            {
                                                                historyForm.txtHisUndID.Text = GetUnderwrite.First().ZTB_USER_OBJ.NAME + "  " + GetUnderwrite.First().ZTB_USER_OBJ.SURNAME;
                                                            }
                                                        }
                                                    }

                                                    historyForm.txtHisLetterNo.Cursor = Cursors.Default;
                                                    if (obj.SUMMARY_LETTER_Collection != null && obj.SUMMARY_LETTER_Collection.Count() > 0)
                                                    {
                                                        if (obj.STATUS != "MO" && obj.STATUS != "MD")
                                                        {
                                                            if (uLetterIDColl != null && uLetterIDColl.Count() > 0)
                                                            {
                                                                var GetLetter = from getData in uLetterIDColl
                                                                                from getData1 in obj.SUMMARY_LETTER_Collection
                                                                                where getData.ULETTER_ID == getData1.ULETTER_ID
                                                                                && getData.STATUS == obj.STATUS
                                                                                && getData.SUBSTATUS == obj.SUBSTATUS
                                                                                && getData.LETTER_DT.Value.ToString("dd/MM/yyyy") == obj.FSYSTEM_DT.Value.ToString("dd/MM/yyyy")
                                                                                && getData.TMN == 'N'
                                                                                select getData;
                                                                if (GetLetter != null && GetLetter.Count() > 0)
                                                                {
                                                                    historyForm.txtHisLetterNo.Tag = GetLetter.First();
                                                                    historyForm.txtHisLetterNo.Text = GetLetter.First().REFERENCE;
                                                                    historyForm.txtHisLetterNo.ForeColor = Color.Red;
                                                                    historyForm.txtHisLetterNo.Font = new Font("Tahoma", 10, FontStyle.Underline);
                                                                    historyForm.txtHisLetterNo.Cursor = Cursors.Hand;
                                                                    ToolTip tt = new ToolTip();
                                                                    tt.SetToolTip(historyForm.txtHisLetterNo, "คลิกเพื่อดูข้อมูลจดหมาย");
                                                                    //historyForm.txtHisLetterNo.DoubleClick += new EventHandler(txtHisLetterNo_DoubleClick);
                                                                    historyForm.txtHisLetterNo.Click += new EventHandler(txtHisLetterNo_Click);
                                                                }

                                                            }
                                                        }
                                                    }

                                                    //historyForm.Click += new EventHandler(historyForm_Click);
                                                    //historyForm.txtHisStatus.Tag = obj;
                                                    //historyForm.txtHisStatus.ForeColor = Color.Red;
                                                    //historyForm.txtHisStatus.Font = new Font("Tahoma", 10, FontStyle.Underline);
                                                    //historyForm.txtHisStatus.Cursor = Cursors.Hand;
                                                    //ToolTip tt1 = new ToolTip();
                                                    //tt1.SetToolTip(historyForm.txtHisLetterNo, "คลิกเพื่อดูรายละเอียดการพิจารณา");
                                                    //historyForm.txtHisStatus.Click += new EventHandler(txtHisStatus_Click);

                                                    historyForm.btnHisDetail.Visible = false;
                                                    if (obj.MEMO_ID_Collection != null && obj.MEMO_ID_Collection.Count() > 0)
                                                    {
                                                        historyForm.btnHisDetail.Tag = obj.MEMO_ID_Collection;
                                                        historyForm.btnHisDetail.Visible = true;
                                                        historyForm.btnHisDetail.Click += new EventHandler(btnHisDetail_Click);
                                                    }

                                                    panelHistory.Controls.Add(historyForm);

                                                    i = i + 1;
                                                }
                                            }

                                        }
                                        else
                                        {
                                            panelHistory.Controls.Clear();

                                        }
                                    }
                                }
                            }
                        }

                        ClickTabHistory = true;
                    }
                }
            }
            catch (Exception ex)
            {
                SetMsgException(ex);
            }
        }

        void btnHisDetail_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                Button btnHisDetail = (Button)sender;
                NewBisPASvcRef.U_MEMO_ID_Collection uMemoIDColl = new NewBisPASvcRef.U_MEMO_ID_Collection();
                uMemoIDColl = (NewBisPASvcRef.U_MEMO_ID_Collection)btnHisDetail.Tag;
                FormMemo formMemo = new FormMemo();
                formMemo.U_MEMO_ID_Coll = new NewBisPASvcRef.U_MEMO_ID_Collection();
                formMemo.U_MEMO_ID_Coll = uMemoIDColl;
                formMemo.AUTB_DATADIC_DET_COLL = new NewBisPASvcRef.AUTB_DATADIC_DET_COLLECTION();
                formMemo.AUTB_DATADIC_DET_COLL = PAR_AUTB_DATADIC_DET_COLLECTION;
                formMemo.PAR_PEND_REQM_COLL = new NewBisPASvcRef.AUTB_PEND_REQM_COLLECTION();
                formMemo.PAR_PEND_REQM_COLL = PAR_PEND_REQM_COLL;
                formMemo.PAR_DOCUMENT_PEND_CODE_COLL = new NewBisPASvcRef.AUTB_DOCUMENT_PEND_CODE_COLL();
                formMemo.PAR_DOCUMENT_PEND_CODE_COLL = PAR_DOCUMENT_PEND_CODE_COLL;

                formMemo.APP_NO = txtAppNo.Text;
                formMemo.USER_ID = UserID;
                formMemo.ShowDialog();
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                SetMsgException(ex);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        void txtHisLetterNo_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                TextBox txtHisLetterNo = (TextBox)sender;
                NewBisPASvcRef.U_LETTER_ID uLetterID = new NewBisPASvcRef.U_LETTER_ID();
                uLetterID = (NewBisPASvcRef.U_LETTER_ID)txtHisLetterNo.Tag;
                if (uLetterID != null && uLetterID.ULETTER_ID != null)
                {
                    if (String.IsNullOrEmpty(uLetterID.REFERENCE))
                    {
                        throw new Exception("ไม่พบข้อมูลจดหมาย");
                    }
                    else
                    {
                        NewBISSvcRef.ProcessResult pr = new NewBISSvcRef.ProcessResult();
                        NewBISSvcRef.U_LETTER_ID uLetterIDObj = new NewBISSvcRef.U_LETTER_ID();
                        using (NewBISSvcRef.NewBISSvcClient client = new NewBISSvcRef.NewBISSvcClient())
                        {
                            uLetterIDObj = client.GetULetterIDByReference(out pr, uLetterID.REFERENCE);
                            if (pr.Successed == false)
                            {
                                throw new Exception(pr.ErrorMessage);
                            }
                        }

                        if (uLetterIDObj != null && uLetterIDObj.ULETTER_ID != null)
                        {
                            if (uLetterIDObj.STATUS == "AP")
                            {
                                ReportForm.frmReportAPPDF appRep = new ReportForm.frmReportAPPDF(txtAppNo.Text.Trim(), uLetterIDObj.ULETTER_ID.Value, UserID);
                                appRep.Show();
                            }
                            else if (uLetterIDObj.STATUS == "MO" || uLetterIDObj.STATUS == "MD")
                            {
                                ReportForm.frmReportMOPDF appRep = new ReportForm.frmReportMOPDF(txtAppNo.Text.Trim(), uLetterIDObj.ULETTER_ID.Value, UserID);
                                appRep.Show();
                            }
                            else if (uLetterIDObj.STATUS == "CO")
                            {
                                ReportForm.frmReportCOPDF appRep = new ReportForm.frmReportCOPDF(txtAppNo.Text.Trim(), uLetterIDObj.ULETTER_ID.Value, UserID);
                                appRep.Show();
                            }
                            else
                            {
                                ReportForm.frmRejectReportPDF appRep = new ReportForm.frmRejectReportPDF(txtAppNo.Text.Trim(), uLetterIDObj.ULETTER_ID.Value, UserID);
                                appRep.Show();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                SetMsgException(ex);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        void txtHisStatus_Click(object sender, EventArgs e)
        {
            try
            {
                TextBox txtHisStatus = (TextBox)sender;
                NewBisPASvcRef.W_SUMMARY wSummaryObj = new NewBisPASvcRef.W_SUMMARY();
                wSummaryObj = (NewBisPASvcRef.W_SUMMARY)txtHisStatus.Tag;

                foreach (Control c in panelHistory.Controls)
                {
                    if (c.Name == "historyForm")
                    {
                        UserControlsForm.HistoryForm historyForm1 = (UserControlsForm.HistoryForm)c;
                        NewBisPASvcRef.W_SUMMARY wSummaryObj1 = new NewBisPASvcRef.W_SUMMARY();
                        wSummaryObj1 = (NewBisPASvcRef.W_SUMMARY)historyForm1.Tag;
                        if (wSummaryObj.SUMMARY_ID == wSummaryObj1.SUMMARY_ID)
                        {
                            historyForm1.txtHisSeq.BackColor = Color.FromArgb(255, 192, 192);
                            historyForm1.txtHisStatus.BackColor = Color.FromArgb(255, 192, 192);
                            historyForm1.txtHisSubStatus.BackColor = Color.FromArgb(255, 192, 192);
                            historyForm1.txtHisStatusCause.BackColor = Color.FromArgb(255, 192, 192);
                            historyForm1.txtHisUndID.BackColor = Color.FromArgb(255, 192, 192);
                            historyForm1.txtHisUpdID.BackColor = Color.FromArgb(255, 192, 192);
                            historyForm1.txtHisFSystemDate.BackColor = Color.FromArgb(255, 192, 192);
                            historyForm1.txtHisLetterNo.BackColor = Color.FromArgb(255, 192, 192);
                        }
                        else
                        {
                            historyForm1.txtHisSeq.BackColor = Color.FromArgb(255, 255, 192);
                            historyForm1.txtHisStatus.BackColor = Color.FromArgb(255, 255, 192);
                            historyForm1.txtHisSubStatus.BackColor = Color.FromArgb(255, 255, 192);
                            historyForm1.txtHisStatusCause.BackColor = Color.FromArgb(255, 255, 192);
                            historyForm1.txtHisUndID.BackColor = Color.FromArgb(255, 255, 192);
                            historyForm1.txtHisUpdID.BackColor = Color.FromArgb(255, 255, 192);
                            historyForm1.txtHisFSystemDate.BackColor = Color.FromArgb(255, 255, 192);
                            historyForm1.txtHisLetterNo.BackColor = Color.FromArgb(255, 255, 192);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                SetMsgException(ex);
            }
        }

        void historyForm_Click(object sender, EventArgs e)
        {
            try
            {
                UserControlsForm.HistoryForm historyForm = (UserControlsForm.HistoryForm)sender;
                NewBisPASvcRef.W_SUMMARY wSummaryObj = new NewBisPASvcRef.W_SUMMARY();
                wSummaryObj = (NewBisPASvcRef.W_SUMMARY)historyForm.Tag;

                foreach (Control c in panelHistory.Controls)
                {
                    if (c.Name == "historyForm")
                    {
                        UserControlsForm.HistoryForm historyForm1 = (UserControlsForm.HistoryForm)c;
                        NewBisPASvcRef.W_SUMMARY wSummaryObj1 = new NewBisPASvcRef.W_SUMMARY();
                        wSummaryObj1 = (NewBisPASvcRef.W_SUMMARY)historyForm1.Tag;
                        if (wSummaryObj.SUMMARY_ID == wSummaryObj1.SUMMARY_ID)
                        {
                            historyForm1.txtHisSeq.BackColor = Color.FromArgb(255, 192, 192);
                        }
                        else
                        {
                            historyForm1.txtHisSeq.BackColor = Color.FromArgb(255, 255, 192);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                SetMsgException(ex);
            }
        }

        void txtHisLetterNo_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                TextBox txtHisLetterNo = (TextBox)sender;
                NewBisPASvcRef.U_LETTER_ID uLetterID = new NewBisPASvcRef.U_LETTER_ID();
                uLetterID = (NewBisPASvcRef.U_LETTER_ID)txtHisLetterNo.Tag;
                if (uLetterID != null && uLetterID.ULETTER_ID != null)
                {
                    if (String.IsNullOrEmpty(uLetterID.REFERENCE))
                    {
                        throw new Exception("ไม่พบข้อมูลจดหมาย");
                    }
                    else
                    {
                        NewBISSvcRef.ProcessResult pr = new NewBISSvcRef.ProcessResult();
                        NewBISSvcRef.U_LETTER_ID uLetterIDObj = new NewBISSvcRef.U_LETTER_ID();
                        using (NewBISSvcRef.NewBISSvcClient client = new NewBISSvcRef.NewBISSvcClient())
                        {
                            uLetterIDObj = client.GetULetterIDByReference(out pr, uLetterID.REFERENCE);
                            if (pr.Successed == false)
                            {
                                throw new Exception(pr.ErrorMessage);
                            }
                        }

                        if (uLetterIDObj != null && uLetterIDObj.ULETTER_ID != null)
                        {
                            if (uLetterIDObj.STATUS == "AP")
                            {
                                ReportForm.frmReportAPPDF appRep = new ReportForm.frmReportAPPDF(txtAppNo.Text.Trim(), uLetterIDObj.ULETTER_ID.Value, UserID);
                                appRep.Show();
                            }
                            else if (uLetterIDObj.STATUS == "MO" || uLetterIDObj.STATUS == "MD")
                            {
                                ReportForm.frmReportMOPDF appRep = new ReportForm.frmReportMOPDF(txtAppNo.Text.Trim(), uLetterIDObj.ULETTER_ID.Value, UserID);
                                appRep.Show();
                            }
                            else if (uLetterIDObj.STATUS == "CO")
                            {
                                ReportForm.frmReportCOPDF appRep = new ReportForm.frmReportCOPDF(txtAppNo.Text.Trim(), uLetterIDObj.ULETTER_ID.Value, UserID);
                                appRep.Show();
                            }
                            else
                            {
                                ReportForm.frmRejectReportPDF appRep = new ReportForm.frmRejectReportPDF(txtAppNo.Text.Trim(), uLetterIDObj.ULETTER_ID.Value, UserID);
                                appRep.Show();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                SetMsgException(ex);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        #region "MEMO"
        private void btnMemoIDSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!(cmbSummryStatus.SelectedValue.ToString() == "MO" || cmbSummryStatus.SelectedValue.ToString() == "MD"))
                {
                    btnMemoIDSave.Focus();
                    throw new Exception("ไม่สามารถบันทึกข้อมูลได้ใบคำขอต้องมีสถานะ MO และ MD เท่านั้น");
                }

                if (PAR_MEMO_SAVE_SUSCESS == false)
                {
                    txtMemoDetailPendCode.Focus();
                    throw new Exception("กรุณาระบุรายละเอียดของหัวข้อย่อยก่อนเพิ่มหัวข้อหลักใหม่");
                }

                if (txtMemoIDLimitDate.Text.Trim() == "")
                {
                    txtMemoIDLimitDate.Focus();
                    throw new Exception("กรุณาระบุวันที่จำกัดการรอคอยการตอบกลับ");
                }

                if (txtMemoIDLimitDate.Text.Trim() != "")
                {
                    DateTime? d = Utility.StringToDateTime(txtMemoIDLimitDate.Text.Trim(), "BU");
                    if (d == null)
                    {
                        txtMemoIDLimitDate.Focus();
                        throw new Exception("กรุณาระบุรูปแบบวันที่จำกัดการรอคอยการตอบกลับให้ถูกต้อง");
                    }
                }

                if (cmbMemoIDEndProcess.SelectedValue.ToString() == "")
                {
                    cmbMemoIDEndProcess.Focus();
                    throw new Exception("กรุณาระบุขั้นตอนดำเนินการ");
                }


                //Add ข้อมูลในส่วน U_MEMO_ID =======================================================
                if (txtMemoIDNO.Text.Trim() == "")
                {
                    int? rowNo = 0;
                    if (!(PAR_U_MEMO_ID_COLL_TMP != null && PAR_U_MEMO_ID_COLL_TMP.Count() > 0))
                    {
                        PAR_U_MEMO_ID_COLL_TMP = new NewBisPASvcRef.U_MEMO_ID_Collection();
                        rowNo = 1;
                    }
                    else
                    {
                        rowNo = (PAR_U_MEMO_ID_COLL_TMP.Count() + 1);
                    }

                    PAR_U_MEMO_ID = new NewBisPASvcRef.U_MEMO_ID();
                    PAR_U_MEMO_ID.SEQ = rowNo;
                    PAR_U_MEMO_ID.MEMO_TRN_DT = DateTime.Now;
                    PAR_U_MEMO_ID.ANSWER_LIMIT_DT = Utility.StringToDateTime(txtMemoIDLimitDate.Text.Trim(), "BU");
                    PAR_U_MEMO_ID.END_PROCESS = cmbMemoIDEndProcess.SelectedValue == null || cmbMemoIDEndProcess.SelectedValue.ToString() == "" ? (char?)null : Convert.ToChar(cmbMemoIDEndProcess.SelectedValue.ToString());
                    PAR_U_MEMO_ID.TMN = ckbMemoIDTmn.Checked == false ? 'N' : 'Y';
                    PAR_U_MEMO_ID.UPD_ID = UserID;
                    PAR_U_MEMO_ID_COLL_TMP.Add(PAR_U_MEMO_ID);
                    PAR_MEMO_SAVE_SUSCESS = false;
                }
                //END Add ข้อมูลในส่วน U_MEMO_ID ===================================================
                //Update ข้อมูลในส่วน U_MEMO_ID ====================================================
                else
                {
                    PAR_U_MEMO_ID.SEQ = txtMemoIDNO.Text.Trim() == "" ? (int?)null : Convert.ToInt16(txtMemoIDNO.Text.Trim());
                    PAR_U_MEMO_ID.MEMO_TRN_DT = txtMemoIDTrnDate.Text.Trim() == "" ? null : Utility.StringToDateTime(txtMemoIDTrnDate.Text.Trim(), "BU");
                    PAR_U_MEMO_ID.ANSWER_LIMIT_DT = Utility.StringToDateTime(txtMemoIDLimitDate.Text.Trim(), "BU");
                    PAR_U_MEMO_ID.END_PROCESS = cmbMemoIDEndProcess.SelectedValue == null || cmbMemoIDEndProcess.SelectedValue.ToString() == "" ? (char?)null : Convert.ToChar(cmbMemoIDEndProcess.SelectedValue.ToString());
                    PAR_U_MEMO_ID.TMN = ckbMemoIDTmn.Checked == false ? 'N' : 'Y';
                    PAR_U_MEMO_ID.UPD_ID = UserID;
                }
                //END Update ข้อมูลในส่วน U_MEMO_ID ================================================

                if (PAR_U_MEMO_ID != null && PAR_U_MEMO_ID.MEMO_TRN_DT != null)
                {
                    gbMemoDetail.Enabled = true;
                }
                else
                {
                    gbMemoDetail.Enabled = false;
                }

                ClearControlsOfMemoID();
                ClearControlsOfMemoDetail();
                txtMemoDetailPendCode.Focus();
                DisplayOfPanelMemoID(PAR_U_MEMO_ID_COLL_TMP);
                DisplayOfPanelMemoDetail(PAR_U_MEMO_ID.MEMO_DETAIL_Collection);
                PAR_U_MEMO_DETAIL = new NewBisPASvcRef.U_MEMO_DETAIL();
                SetBackColorOfPanelMemoDetailList(panelMemoDetailList, PAR_U_MEMO_DETAIL);
            }
            catch (Exception ex)
            {
                SetMsgException(ex);
            }
        }
        private void DisplayOfPanelMemoID(NewBisPASvcRef.U_MEMO_ID_Collection objectDetail)
        {
            if (objectDetail != null && objectDetail.Count() > 0)
            {
                while (panelMemoList.Controls.Count > 0)
                {
                    panelMemoList.Controls.RemoveAt(0);
                }

                var GetData = from getData in objectDetail
                              orderby getData.SEQ ascending
                              select getData;
                if (GetData != null && GetData.Count() > 0)
                {
                    objectDetail = new NewBisPASvcRef.U_MEMO_ID_Collection();
                    objectDetail.AddRange(GetData.ToArray());
                }

                int i = 0;
                foreach (NewBisPASvcRef.U_MEMO_ID obj in objectDetail)
                {
                    UserControlsForm.MemoMainForm memoMainForm = new UserControlsForm.MemoMainForm();
                    memoMainForm.Name = "memoMainForm";
                    memoMainForm.Location = new Point(0, 0 + (i * 39));
                    memoMainForm.Size = new Size(1193, 39);
                    memoMainForm.Tag = obj;

                    memoMainForm.txtMoListNO.Text = obj.SEQ == null ? "" : obj.SEQ.Value.ToString();
                    memoMainForm.txtMoListTrnDate.Text = obj.MEMO_TRN_DT == null ? "" : Utility.dateTimeToString(obj.MEMO_TRN_DT.Value, "dd/MM/yyyy hh:mi:ss", "BU"); ;
                    memoMainForm.txtMoListLimitDate.Text = obj.ANSWER_LIMIT_DT == null ? "" : Utility.dateTimeToString(obj.ANSWER_LIMIT_DT.Value, "dd/MM/yyyy", "BU");
                    memoMainForm.txtMoListLetter.Text = "";

                    if (obj.U_LETTER_ID != null && obj.U_LETTER_ID.ULETTER_ID != null)
                    {
                        memoMainForm.txtMoListLetter.Text = obj.U_LETTER_ID.REFERENCE;
                        memoMainForm.txtMoListLetter.Tag = obj;
                        memoMainForm.txtMoListLetter.Font = new Font("Tahoma", 10, FontStyle.Underline);
                        memoMainForm.txtMoListLetter.Cursor = Cursors.Hand;
                        //  memoMainForm.txtMoListLetter.ForeColor = Color.Red;
                        ToolTip tt = new ToolTip();
                        tt.SetToolTip(memoMainForm.txtMoListLetter, "คลิกเพื่อดูข้อมูลจดหมาย");
                        memoMainForm.txtMoListLetter.Click += new EventHandler(txtMoListLetter_Click);
                    }

                    memoMainForm.txtMoListProcessEndDesc.Text = "";
                    var GetEndProcess = from getEndProcess in PAR_AUTB_DATADIC_DET_COLLECTION
                                        where getEndProcess.COL_NAME == "END_PROCESS" && getEndProcess.CODE == obj.END_PROCESS.Value.ToString()
                                        select getEndProcess;
                    if (GetEndProcess != null && GetEndProcess.Count() > 0)
                    {
                        memoMainForm.txtMoListProcessEndDesc.Text = GetEndProcess.ToArray()[0].DESCRIPTION;
                    }

                    if (obj.END_PROCESS == 'Y')
                    {
                        memoMainForm.txtMoListProcessEndDesc.BackColor = Color.FromArgb(192, 255, 192);
                    }
                    else
                    {
                        memoMainForm.txtMoListProcessEndDesc.BackColor = SystemColors.Control;
                    }

                    memoMainForm.txtMoListTmnDesc.Text = obj.TMN == 'N' ? "ยังมีผลใช้อยู่" : "ยกเลิกการใช้";

                    if (obj.SEQ == PAR_U_MEMO_ID.SEQ)
                    {
                        memoMainForm.BackColor = Color.FromArgb(128, 128, 255);
                    }
                    else
                    {
                        memoMainForm.BackColor = Color.FromName("InactiveCaptionText");
                    }

                    //COMMAND =========================================================
                    //Edit ============================================================
                    memoMainForm.btnMoListEdit.Tag = obj;
                    memoMainForm.btnMoListEdit.Click += new EventHandler(btnMoListEdit_Click);
                    //END Edit ========================================================

                    //Detail ==========================================================
                    memoMainForm.btnMoListDetail.Tag = obj;
                    memoMainForm.btnMoListDetail.Click += new EventHandler(btnMoListDetail_Click);
                    //END Detail ======================================================

                    //Cancel ==========================================================
                    memoMainForm.btnMoListCancel.Tag = obj;
                    memoMainForm.btnMoListCancel.Click += new EventHandler(btnMoListCancel_Click);
                    //END Cancel ======================================================
                    //END COMMAND =====================================================


                    //Cancel ==========================================================
                    memoMainForm.ButtonRequestDocumentFromBranch.Tag = obj;
                    memoMainForm.ButtonRequestDocumentFromBranch.Click += new EventHandler(btnRequestDocumentMemoFromBranch_Click);
                    //END Cancel ======================================================
                    //END COMMAND =====================================================

                    panelMemoList.Controls.Add(memoMainForm);
                    i = i + 1;
                }
            }
            else
            {
                panelMemoList.Controls.Clear();
                gbMemoDetail.Enabled = false;
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
                                ReportForm.frmReportMOPDF appRep = new ReportForm.frmReportMOPDF(txtAppNo.Text.Trim(), uLetterIDObj.ULETTER_ID.Value, UserID);
                                appRep.Show();
                            }
                        }
                        else
                        {
                            throw new Exception("ไม่พบข้อมูลจดหมาย");
                        }
                    }
                }

                PAR_U_MEMO_ID = (NewBisPASvcRef.U_MEMO_ID)txtMoListLetter.Tag;
                ClearControlsOfMemoDetail();
                DisplayOfPanelMemoDetail(PAR_U_MEMO_ID.MEMO_DETAIL_Collection);
                SetBackColorOfPanelMemoList(panelMemoList, PAR_U_MEMO_ID);
                gbMemoDetail.Enabled = true;
                PAR_U_MEMO_DETAIL = new NewBisPASvcRef.U_MEMO_DETAIL();
                SetBackColorOfPanelMemoDetailList(panelMemoDetailList, PAR_U_MEMO_DETAIL);
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                SetMsgException(ex);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
        void btnMoListEdit_Click(object sender, EventArgs e)
        {
            try
            {
                Button btnMoListEdit = (Button)sender;
                PAR_U_MEMO_ID = (NewBisPASvcRef.U_MEMO_ID)btnMoListEdit.Tag;

                txtMemoIDNO.Text = PAR_U_MEMO_ID.SEQ == null ? "" : PAR_U_MEMO_ID.SEQ.Value.ToString();
                txtMemoIDTrnDate.Text = PAR_U_MEMO_ID.MEMO_TRN_DT == null ? "" : Utility.dateTimeToString(PAR_U_MEMO_ID.MEMO_TRN_DT.Value, "dd/MM/yyyy hh:mi:ss", "BU");
                txtMemoIDLimitDate.Text = PAR_U_MEMO_ID.ANSWER_LIMIT_DT == null ? "" : Utility.dateTimeToString(PAR_U_MEMO_ID.ANSWER_LIMIT_DT.Value, "dd/MM/yyyy", "BU");
                cmbMemoIDEndProcess.SelectedValue = PAR_U_MEMO_ID.END_PROCESS == null ? "" : PAR_U_MEMO_ID.END_PROCESS.Value.ToString();
                ckbMemoIDTmn.Checked = PAR_U_MEMO_ID.TMN == 'Y' ? true : false;
                btnMemoIDSave.Text = "แก้ไขข้อมูล";

                ClearControlsOfMemoDetail();
                DisplayOfPanelMemoDetail(PAR_U_MEMO_ID.MEMO_DETAIL_Collection);
                SetBackColorOfPanelMemoList(panelMemoList, PAR_U_MEMO_ID);
                gbMemoDetail.Enabled = true;
                PAR_U_MEMO_DETAIL = new NewBisPASvcRef.U_MEMO_DETAIL();
                SetBackColorOfPanelMemoDetailList(panelMemoDetailList, PAR_U_MEMO_DETAIL);

            }
            catch (Exception ex)
            {
                SetMsgException(ex);
            }
        }
        void btnMoListCancel_Click(object sender, EventArgs e)
        {
            try
            {
                var result = MessageBox.Show("คุณต้องการยกเเลิกข้อมูลนี้ใช่หรือไม่", "คำเตือน", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    Button btnMoListCancel = (Button)sender;
                    PAR_U_MEMO_ID = (NewBisPASvcRef.U_MEMO_ID)btnMoListCancel.Tag;

                    if (PAR_U_MEMO_ID.UMEMO_ID == null)
                    {
                        if (PAR_U_MEMO_ID_COLL_TMP != null && PAR_U_MEMO_ID_COLL_TMP.Count() > 0)
                        {
                            PAR_U_MEMO_ID_COLL_TMP.Remove(PAR_U_MEMO_ID);
                        }
                    }
                    else
                    {
                        PAR_U_MEMO_ID.TMN = 'Y';
                    }

                    if (PAR_U_MEMO_ID_COLL_TMP != null && PAR_U_MEMO_ID_COLL_TMP.Count() > 0)
                    {
                        var GetData = from getData in PAR_U_MEMO_ID_COLL_TMP
                                      orderby getData.SEQ ascending
                                      select getData;
                        if (GetData != null && GetData.Count() > 0)
                        {
                            PAR_U_MEMO_ID_COLL_TMP = new NewBisPASvcRef.U_MEMO_ID_Collection();
                            PAR_U_MEMO_ID_COLL_TMP.AddRange(GetData.ToArray());
                            for (int i = 0; i < PAR_U_MEMO_ID_COLL_TMP.Count(); i++)
                            {
                                PAR_U_MEMO_ID_COLL_TMP[i].SEQ = i + 1;
                            }
                        }
                    }

                    ClearControlsOfMemoID();
                    ClearControlsOfMemoDetail();
                    DisplayOfPanelMemoID(PAR_U_MEMO_ID_COLL_TMP);

                    if (PAR_U_MEMO_ID_COLL_TMP != null && PAR_U_MEMO_ID_COLL_TMP.Count() > 0)
                    {
                        PAR_U_MEMO_ID = PAR_U_MEMO_ID_COLL_TMP.First();
                    }
                    else
                    {
                        PAR_U_MEMO_ID = new NewBisPASvcRef.U_MEMO_ID();
                        gbMemoDetail.Enabled = false;
                    }

                    DisplayOfPanelMemoDetail(PAR_U_MEMO_ID.MEMO_DETAIL_Collection);
                    SetBackColorOfPanelMemoList(panelMemoList, PAR_U_MEMO_ID);
                    PAR_U_MEMO_DETAIL = new NewBisPASvcRef.U_MEMO_DETAIL();
                    SetBackColorOfPanelMemoDetailList(panelMemoDetailList, PAR_U_MEMO_DETAIL);
                }
            }
            catch (Exception ex)
            {
                SetMsgException(ex);
            }
        }

        void btnRequestDocumentMemoFromBranch_Click(object sender, EventArgs e)
        {
            try
            {
                var result = MessageBox.Show("คุณต้องการตามเอกสารเพิ่มจากสาขาใช่หรือไม่", "คำเตือน", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    Button btnMemoIDScan = (Button)sender;
                    var uMemoIDObj = new NewBisPASvcRef.U_MEMO_ID();
                    uMemoIDObj = (NewBisPASvcRef.U_MEMO_ID)btnMemoIDScan.Tag;

                    String guID = "";
                    int gracePeriod = 0;
                    long? isisDocID = null;
                    double? diffDate = null;
                    String detail = "";
                    DateTime? statusDate = DateTime.Now;

                    NewBISSvcRef.ProcessResult pr = new NewBISSvcRef.ProcessResult();
                    NewBISSvcRef.U_STATUS_ID uStatusIDObj = new NewBISSvcRef.U_STATUS_ID();
                    NewBISSvcRef.U_STATUS_ID[] uStatusIDArr;
                    long[] uAppIDs = { PAR_PA_APPLICATION_ID.APPLICATION_Collection.First().UAPP_ID.Value };
                    using (NewBISSvcRef.NewBISSvcClient client = new NewBISSvcRef.NewBISSvcClient())
                    {
                        pr = client.GetCompositeDataU_STATUS_ID(out uStatusIDArr, uAppIDs, null);
                        if (pr.Successed == false)
                        {
                            throw new Exception(pr.ErrorMessage);
                        }
                        if (uStatusIDArr != null && uStatusIDArr.Count() > 0)
                        {
                            var SelStatusID =
                                from selStatusID in uStatusIDArr
                                where selStatusID.TMN == "N"
                                select selStatusID;
                            if (SelStatusID != null && SelStatusID.Count() > 0)
                            {
                                uStatusIDObj = SelStatusID.ToArray()[0];
                                statusDate = uStatusIDObj.STATUS_DT;
                            }
                        }


                        pr = client.RequestDocFromISIS(out guID, out isisDocID, out detail, PAR_PA_APPLICATION_ID.APP_ID.Value, cmbSummryStatus.SelectedValue.ToString(), cmbSummrySubStatus.SelectedValue.ToString(), uMemoIDObj.ANSWER_LIMIT_DT.Value, statusDate.Value, UserID);
                        if (pr.Successed == false)
                        {
                            throw new Exception(pr.ErrorMessage);
                        }
                    }
                    if (guID != null && guID != "")
                    {
                        try
                        {
                            diffDate = Utility.differenceDay(uMemoIDObj.MEMO_TRN_DT, uMemoIDObj.ANSWER_LIMIT_DT);
                            gracePeriod = diffDate == null ? 0 : Convert.ToInt16(Math.Ceiling(diffDate.Value));
                            using (IsisAppTransportService.AppTransportServiceContractClient clientIsis = new IsisAppTransportService.AppTransportServiceContractClient())
                            {
                                clientIsis.ChangeAppFormStatus(guID, cmbSummryStatus.SelectedValue.ToString(), cmbSummrySubStatus.SelectedValue.ToString(), detail, DateTime.Now, gracePeriod, isisDocID.Value);
                            }
                        }
                        catch (Exception ex)
                        {

                            throw new Exception("Error Function clientIsis.ChangeAppFormStatus LN:20625  " + ex);
                        }

                        MessageBox.Show("ส่งข้อมูลเรียบร้อยแล้ว");
                    }
                }

            }
            catch (Exception ex)
            {
                SetMsgException(ex);
            }
        }
        void btnMoListDetail_Click(object sender, EventArgs e)
        {
            try
            {
                Button btnMoListDetail = (Button)sender;
                PAR_U_MEMO_ID = (NewBisPASvcRef.U_MEMO_ID)btnMoListDetail.Tag;

                ClearControlsOfMemoID();
                ClearControlsOfMemoDetail();
                DisplayOfPanelMemoDetail(PAR_U_MEMO_ID.MEMO_DETAIL_Collection);
                SetBackColorOfPanelMemoList(panelMemoList, PAR_U_MEMO_ID);
                gbMemoDetail.Enabled = true;
                PAR_U_MEMO_DETAIL = new NewBisPASvcRef.U_MEMO_DETAIL();
                SetBackColorOfPanelMemoDetailList(panelMemoDetailList, PAR_U_MEMO_DETAIL);
            }
            catch (Exception ex)
            {
                SetMsgException(ex);
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
        private void ClearControlsOfMemoID()
        {
            txtMemoIDNO.Text = "";
            txtMemoIDTrnDate.Text = "";
            txtMemoIDLimitDate.Text = "";
            cmbMemoIDEndProcess.SelectedValue = "";
            ckbMemoIDTmn.Checked = false;
            btnMemoIDSave.Text = "เพิ่มข้อมูล";

            DateTime? AnswerLimitDate = DateTime.Now.AddDays(20);
            txtMemoIDLimitDate.Text = Utility.dateTimeToString(AnswerLimitDate.Value, "dd/MM/yyyy", "BU");
            cmbMemoIDEndProcess.SelectedValue = "N";


        }
        private void ClearControlsOfMemoDetail()
        {
            txtMemoDetailPendCode.Enabled = true;
            cmbMemoDetailPendCodeDesc.Enabled = true;
            txtMemoDetailNO.Text = "";
            txtMemoDetailPendCode.Text = "";
            cmbMemoDetailPendCodeDesc.SelectedValue = "";
            cmbMemoDetailPendStatus.SelectedValue = "";
            txtMemoDetailPendStatusDate.Text = "";
            btnMemoDetailSave.Text = "เพิ่มข้อมูล";
            txtMemoDescription.Text = "";
            panelMemoDocument.Controls.Clear();
        }
        private void btnMemoDetailSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtMemoDetailPendCode.Text == "")
                {
                    txtMemoDetailPendCode.Focus();
                    throw new Exception("กรุณาระบุรหัสการขอเอกสารเพิ่ม");
                }
                if (cmbMemoDetailPendCodeDesc.SelectedValue.ToString() == "")
                {
                    cmbMemoDetailPendCodeDesc.Focus();
                    throw new Exception("กรุณาระบุรายละเอียดของรหัสการขอเอกสารเพิ่ม");
                }
                if (cmbMemoDetailPendStatus.SelectedValue.ToString() == "")
                {
                    cmbMemoDetailPendStatus.Focus();
                    throw new Exception("กรุณาระบุสถานะ");
                }
                if (cmbMemoDetailPendStatus.SelectedValue.ToString() != "W")
                {
                    if (txtMemoDetailPendStatusDate.Text == "")
                    {
                        txtMemoDetailPendStatusDate.Focus();
                        throw new Exception("กรุณาระบุวันที่ตอบรับ");
                    }
                }
                if (txtMemoDetailPendStatusDate.Text.Trim() != "")
                {
                    DateTime? d = Utility.StringToDateTime(txtMemoDetailPendStatusDate.Text.Trim(), "BU");
                    if (d == null)
                    {
                        txtMemoDetailPendStatusDate.Focus();
                        throw new Exception("กรุณาระบุรูปแบบวันที่ตอบรับให้ถูกต้อง");
                    }
                }
                //Add U_MEMO_DETAIL ======================================================================
                if (txtMemoDetailNO.Text == "")
                {
                    int? rowNo = 0;
                    if (PAR_U_MEMO_ID.MEMO_DETAIL_Collection != null && PAR_U_MEMO_ID.MEMO_DETAIL_Collection.Count() > 0)
                    {
                        rowNo = PAR_U_MEMO_ID.MEMO_DETAIL_Collection.Count() + 1;
                    }
                    else
                    {
                        PAR_U_MEMO_ID.MEMO_DETAIL_Collection = new NewBisPASvcRef.U_MEMO_DETAIL_Collection();
                        rowNo = 1;
                    }

                    PAR_U_MEMO_DETAIL = new NewBisPASvcRef.U_MEMO_DETAIL();
                    PAR_U_MEMO_DETAIL.PRINT_SEQ = rowNo;
                    PAR_U_MEMO_DETAIL.PEND_CODE = txtMemoDetailPendCode.Text.Trim();
                    PAR_U_MEMO_DETAIL.PEND_STATUS = cmbMemoDetailPendStatus.SelectedValue == null ? "" : cmbMemoDetailPendStatus.SelectedValue.ToString();
                    PAR_U_MEMO_DETAIL.PEND_STATUS_DT = txtMemoDetailPendStatusDate.Text == "" ? null : Utility.StringToDateTime(txtMemoDetailPendStatusDate.Text.Trim(), "BU");

                    if (txtMemoDescription.Text.Trim() != "")
                    {
                        PAR_U_MEMO_DETAIL.MEMO_DESCRIPTION = new NewBisPASvcRef.U_MEMO_DESCRIPTION();
                        PAR_U_MEMO_DETAIL.MEMO_DESCRIPTION.UMEMO_ID = PAR_U_MEMO_DETAIL.UMEMO_ID;
                        PAR_U_MEMO_DETAIL.MEMO_DESCRIPTION.PEND_CODE = PAR_U_MEMO_DETAIL.PEND_CODE;
                        PAR_U_MEMO_DETAIL.MEMO_DESCRIPTION.PEND_DESCRIPTION = txtMemoDescription.Text.Trim();
                    }


                    PAR_U_MEMO_DETAIL.MEMO_DOCUMENT_Collection = new NewBisPASvcRef.U_MEMO_DOCUMENT_Collection();
                    AddObjectOfDocumentInPanelMemoDocument(panelMemoDocument, PAR_U_MEMO_DETAIL.MEMO_DOCUMENT_Collection);

                    if (PAR_U_MEMO_ID.MEMO_DETAIL_Collection != null && PAR_U_MEMO_ID.MEMO_DETAIL_Collection.Count() > 0)
                    {
                        foreach (NewBisPASvcRef.U_MEMO_DETAIL obj in PAR_U_MEMO_ID.MEMO_DETAIL_Collection)
                        {
                            if (txtMemoDetailPendCode.Text.Trim() == obj.PEND_CODE)
                            {
                                txtMemoDetailPendCode.Focus();
                                throw new Exception("มีรหัส PEND CODE นี้แล้วไม่สามารถเพิ่มข้อมูลได้");
                            }
                        }
                    }

                    PAR_U_MEMO_ID.MEMO_DETAIL_Collection.Add(PAR_U_MEMO_DETAIL);
                    PAR_MEMO_SAVE_SUSCESS = true;
                }
                //END Add U_MEMO_DETAIL ==================================================================
                //UPDATE U_MEMO_DETAIL ===================================================================
                else
                {
                    PAR_U_MEMO_DETAIL.PRINT_SEQ = txtMemoDetailNO.Text.Trim() == "" ? (int?)null : Convert.ToInt16(txtMemoDetailNO.Text.Trim());
                    PAR_U_MEMO_DETAIL.PEND_CODE = txtMemoDetailPendCode.Text.Trim();
                    PAR_U_MEMO_DETAIL.PEND_STATUS = cmbMemoDetailPendStatus.SelectedValue == null ? "" : cmbMemoDetailPendStatus.SelectedValue.ToString();
                    PAR_U_MEMO_DETAIL.PEND_STATUS_DT = txtMemoDetailPendStatusDate.Text == "" ? null : Utility.StringToDateTime(txtMemoDetailPendStatusDate.Text.Trim(), "BU");

                    if (txtMemoDescription.Text.Trim() != "")
                    {
                        PAR_U_MEMO_DETAIL.MEMO_DESCRIPTION = new NewBisPASvcRef.U_MEMO_DESCRIPTION();
                        PAR_U_MEMO_DETAIL.MEMO_DESCRIPTION.UMEMO_ID = PAR_U_MEMO_DETAIL.UMEMO_ID;
                        PAR_U_MEMO_DETAIL.MEMO_DESCRIPTION.PEND_CODE = PAR_U_MEMO_DETAIL.PEND_CODE;
                        PAR_U_MEMO_DETAIL.MEMO_DESCRIPTION.PEND_DESCRIPTION = txtMemoDescription.Text.Trim();
                    }
                    else
                    {
                        PAR_U_MEMO_DETAIL.MEMO_DESCRIPTION = new NewBisPASvcRef.U_MEMO_DESCRIPTION();
                    }

                    if (PAR_U_MEMO_ID.MEMO_DETAIL_Collection != null && PAR_U_MEMO_ID.MEMO_DETAIL_Collection.Count() > 0)
                    {
                        foreach (NewBisPASvcRef.U_MEMO_DETAIL obj in PAR_U_MEMO_ID.MEMO_DETAIL_Collection)
                        {
                            if (obj.PRINT_SEQ != PAR_U_MEMO_DETAIL.PRINT_SEQ)
                            {
                                if (txtMemoDetailPendCode.Text.Trim() == obj.PEND_CODE)
                                {
                                    txtMemoDetailPendCode.Focus();
                                    throw new Exception("มีรหัส PEND CODE นี้แล้วไม่สามารถเพิ่มข้อมูลได้");
                                }
                            }

                        }
                    }

                    PAR_U_MEMO_DETAIL.MEMO_DOCUMENT_Collection = new NewBisPASvcRef.U_MEMO_DOCUMENT_Collection();
                    AddObjectOfDocumentInPanelMemoDocument(panelMemoDocument, PAR_U_MEMO_DETAIL.MEMO_DOCUMENT_Collection);
                }
                //END UPDATE U_MEMO_DETAIL ================================================================

                ClearControlsOfMemoDetail();
                DisplayOfPanelMemoDetail(PAR_U_MEMO_ID.MEMO_DETAIL_Collection);
                txtMemoDetailPendCode.Focus();

                if (PAR_U_MEMO_ID.MEMO_DETAIL_Collection != null && PAR_U_MEMO_ID.MEMO_DETAIL_Collection.Count() > 0)
                {
                    int chkEndProcess = 0;
                    foreach (NewBisPASvcRef.U_MEMO_DETAIL uMemoDetailObj in PAR_U_MEMO_ID.MEMO_DETAIL_Collection)
                    {
                        if (uMemoDetailObj.PEND_STATUS == "W")
                        {
                            chkEndProcess = chkEndProcess + 1;
                        }
                    }

                    if (chkEndProcess > 0)
                    {
                        PAR_U_MEMO_ID.END_PROCESS = 'N';
                    }
                    else
                    {
                        PAR_U_MEMO_ID.END_PROCESS = 'Y';
                    }

                    DisplayOfPanelMemoID(PAR_U_MEMO_ID_COLL_TMP);
                }
            }
            catch (Exception ex)
            {
                SetMsgException(ex);
            }
        }
        private void txtMemoIDLimitDate_Leave(object sender, EventArgs e)
        {
            try
            {
                ChkDateForTextBox(txtMemoIDLimitDate.Text.Trim(), "วันที่จำกัดการรอคอยการตอบกลับ", txtMemoIDLimitDate);
            }
            catch (Exception ex)
            {
                SetMsgException(ex);
            }
        }
        private void txtMemoDetailPendStatusDate_Leave(object sender, EventArgs e)
        {
            try
            {
                ChkDateForTextBox(txtMemoDetailPendStatusDate.Text.Trim(), "วันที่ตอบกลับ", txtMemoDetailPendStatusDate);
            }
            catch (Exception ex)
            {
                SetMsgException(ex);
            }
        }
        private void txtMemoDetailPendCode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyValue == 13)
                {
                    if (txtMemoDetailPendCode.Text.Trim() == "")
                    {
                        cmbMemoDetailPendCodeDesc.SelectedValue = "";
                        panelMemoDocument.Controls.Clear();
                    }
                    else
                    {
                        txtMemoDetailPendCode.Text = txtMemoDetailPendCode.Text.Trim().ToUpper();
                        var GetData = from getData in PAR_PEND_REQM_COLL
                                      where getData.PEND_CODE == txtMemoDetailPendCode.Text.Trim()
                                      select getData;
                        if (GetData != null && GetData.Count() > 0)
                        {
                            cmbMemoDetailPendCodeDesc.SelectedValue = txtMemoDetailPendCode.Text.Trim();
                            cmbMemoDetailPendStatus.SelectedValue = "W";

                            var GetDoc = from getDoc in PAR_DOCUMENT_PEND_CODE_COLL
                                         where getDoc.PEND_CODE == txtMemoDetailPendCode.Text.Trim()
                                         select getDoc;
                            if (GetDoc != null && GetDoc.Count() > 0)
                            {
                                NewBisPASvcRef.AUTB_DOCUMENT_PEND_CODE_COLL docColl = new NewBisPASvcRef.AUTB_DOCUMENT_PEND_CODE_COLL();
                                docColl.AddRange(GetDoc.ToArray());
                                DisplayOfPanelMemoDocument(docColl, null, "INSERT");
                            }
                            else
                            {
                                panelMemoDocument.Controls.Clear();
                            }
                        }
                        else
                        {
                            txtMemoDetailPendCode.Text = "";
                            cmbMemoDetailPendCodeDesc.SelectedValue = "";
                            panelMemoDocument.Controls.Clear();
                            txtMemoDetailPendCode.Focus();
                            cmbMemoDetailPendStatus.SelectedValue = "";
                            throw new Exception("ไม่พบรหัสการขอเอกสารเพิ่ม");
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                SetMsgException(ex);
            }
        }
        private void cmbMemoDetailPendCodeDesc_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                if (cmbMemoDetailPendCodeDesc.SelectedValue == null || cmbMemoDetailPendCodeDesc.SelectedValue.ToString() == "")
                {
                    txtMemoDetailPendCode.Text = "";
                    cmbMemoDetailPendCodeDesc.SelectedValue = "";
                    panelMemoDocument.Controls.Clear();
                    txtMemoDetailPendCode.Focus();
                    cmbMemoDetailPendStatus.SelectedValue = "";
                }
                else
                {
                    txtMemoDetailPendCode.Text = cmbMemoDetailPendCodeDesc.SelectedValue.ToString();
                    cmbMemoDetailPendStatus.SelectedValue = "W";
                    var GetDoc = from getDoc in PAR_DOCUMENT_PEND_CODE_COLL
                                 where getDoc.PEND_CODE == txtMemoDetailPendCode.Text.Trim()
                                 select getDoc;
                    if (GetDoc != null && GetDoc.Count() > 0)
                    {
                        NewBisPASvcRef.AUTB_DOCUMENT_PEND_CODE_COLL docColl = new NewBisPASvcRef.AUTB_DOCUMENT_PEND_CODE_COLL();
                        docColl.AddRange(GetDoc.ToArray());
                        DisplayOfPanelMemoDocument(docColl, null, "INSERT");
                    }
                    else
                    {
                        panelMemoDocument.Controls.Clear();
                    }
                }
            }
            catch (Exception ex)
            {
                SetMsgException(ex);
            }
        }
        private void cmbMemoDetailPendStatus_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                if (cmbMemoDetailPendStatus.SelectedValue != null)
                {
                    if (cmbMemoDetailPendStatus.SelectedValue.ToString() != "W" && cmbMemoDetailPendStatus.SelectedValue.ToString() != "")
                    {
                        if (txtMemoDetailPendStatusDate.Text.Trim() == "")
                        {
                            txtMemoDetailPendStatusDate.Text = Utility.dateTimeToString(DateTime.Now, "dd/MM/yyyy", "BU");
                        }
                    }
                    else
                    {
                        txtMemoDetailPendStatusDate.Text = "";
                    }
                }
            }
            catch (Exception ex)
            {
                SetMsgException(ex);
            }
        }
        private void DisplayOfPanelMemoDocument(NewBisPASvcRef.AUTB_DOCUMENT_PEND_CODE_COLL objectDetail, NewBisPASvcRef.U_MEMO_DOCUMENT_Collection memoDocumentColl, String action)
        {
            if (objectDetail != null && objectDetail.Count() > 0)
            {
                while (panelMemoDocument.Controls.Count > 0)
                {
                    panelMemoDocument.Controls.RemoveAt(0);
                }

                int i = 0;
                foreach (NewBisPASvcRef.AUTB_DOCUMENT_PEND_CODE obj in objectDetail)
                {
                    UserControlsForm.DocumentMemoForm documentMemoForm = new UserControlsForm.DocumentMemoForm();
                    documentMemoForm.Name = "documentMemoForm";
                    documentMemoForm.Location = new Point(0, 0 + (i * 23));
                    documentMemoForm.Size = new Size(336, 23);
                    documentMemoForm.Tag = obj;

                    if (memoDocumentColl != null && memoDocumentColl.Count() > 0)
                    {
                        var GetData = from getData in memoDocumentColl
                                      where getData.DOC_CODE == obj.DOC_CODE
                                      select getData;
                        if (GetData != null && GetData.Count() > 0)
                        {
                            documentMemoForm.ckbMoDocument.Checked = true;
                        }
                        else
                        {
                            documentMemoForm.ckbMoDocument.Checked = false;
                        }
                    }
                    else
                    {
                        if (action == "INSERT")
                        {
                            documentMemoForm.ckbMoDocument.Checked = true;
                        }
                        else if (action == "UPDATE")
                        {
                            documentMemoForm.ckbMoDocument.Checked = false;
                        }
                    }


                    documentMemoForm.ckbMoDocument.Text = obj.DESCRIPTION;
                    documentMemoForm.ckbMoDocument.Tag = obj;
                    documentMemoForm.txtDocCode.Text = obj.DOC_CODE;

                    ToolTip toolTipDoc = new ToolTip();
                    toolTipDoc.SetToolTip(documentMemoForm.ckbMoDocument, obj.DESCRIPTION);
                    documentMemoForm.txtDocCode.Tag = obj;

                    panelMemoDocument.Controls.Add(documentMemoForm);
                    i = i + 1;
                }
            }
            else
            {
                panelMemoDocument.Controls.Clear();
            }
        }
        private void AddObjectOfDocumentInPanelMemoDocument(Panel panelMemoDocument, NewBisPASvcRef.U_MEMO_DOCUMENT_Collection objectDetail)
        {
            if (panelMemoDocument.Controls.Count > 0)
            {
                foreach (Control c in panelMemoDocument.Controls)
                {
                    if (c.Name == "documentMemoForm")
                    {
                        foreach (Control c1 in c.Controls)
                        {
                            if (c1.Name == "ckbMemoDocument")
                            {
                                CheckBox ckb = (CheckBox)c1;
                                if (ckb.Checked == true)
                                {
                                    NewBisPASvcRef.AUTB_DOCUMENT_PEND_CODE obj = new NewBisPASvcRef.AUTB_DOCUMENT_PEND_CODE();
                                    obj = (NewBisPASvcRef.AUTB_DOCUMENT_PEND_CODE)ckb.Tag;

                                    NewBisPASvcRef.U_MEMO_DOCUMENT uMemoDocObj = new NewBisPASvcRef.U_MEMO_DOCUMENT();
                                    uMemoDocObj.PEND_CODE = obj.PEND_CODE;
                                    uMemoDocObj.DOC_CODE = obj.DOC_CODE;
                                    objectDetail.Add(uMemoDocObj);
                                }
                            }
                        }
                    }
                }
            }
        }
        private void DisplayOfPanelMemoDetail(NewBisPASvcRef.U_MEMO_DETAIL_Collection objectDetail)
        {
            if (objectDetail != null && objectDetail.Count() > 0)
            {
                while (panelMemoDetailList.Controls.Count > 0)
                {
                    panelMemoDetailList.Controls.RemoveAt(0);
                }

                var GetData = from getData in objectDetail
                              orderby getData.PRINT_SEQ ascending
                              select getData;
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
                    var GetPenStatus = from getPendStatus in PAR_AUTB_DATADIC_DET_COLLECTION
                                       where getPendStatus.COL_NAME == "PEND_STATUS" && getPendStatus.CODE == obj.PEND_STATUS
                                       select getPendStatus;
                    if (GetPenStatus != null && GetPenStatus.Count() > 0)
                    {
                        memoDetailForm.txtMoDetailListPendStatus.Text = GetPenStatus.ToArray()[0].DESCRIPTION;
                    }

                    if (obj.PEND_STATUS == "W")
                    {
                        memoDetailForm.txtMoDetailListPendStatus.BackColor = SystemColors.Control;
                    }
                    else
                    {
                        memoDetailForm.txtMoDetailListPendStatus.BackColor = Color.FromArgb(192, 255, 192);
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

                    if (obj.UMEMO_ID == null)
                    {
                        memoDetailForm.btnMoDetailListCancel.Enabled = true;
                    }
                    else
                    {
                        memoDetailForm.btnMoDetailListCancel.Enabled = false;
                    }

                    //COMMAND ===========================================================

                    //Edit ==============================================================
                    memoDetailForm.btnMoDetailListEdit.Tag = obj;
                    memoDetailForm.btnMoDetailListEdit.Click += new EventHandler(btnMoDetailListEdit_Click);
                    //END Edit ==========================================================

                    //Cancel ============================================================
                    //END Cancel ========================================================
                    memoDetailForm.btnMoDetailListCancel.Tag = obj;
                    memoDetailForm.btnMoDetailListCancel.Click += new EventHandler(btnMoDetailListCancel_Click);
                    //END COMMAND =======================================================

                    panelMemoDetailList.Controls.Add(memoDetailForm);
                    i = i + 1;

                    if (PAR_U_MEMO_DETAIL.PRINT_SEQ == obj.PRINT_SEQ)
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
                panelMemoDetailList.Controls.Clear();
            }
        }
        void btnMoDetailListEdit_Click(object sender, EventArgs e)
        {
            try
            {
                Button btnMoDetailListEdit = (Button)sender;
                PAR_U_MEMO_DETAIL = (NewBisPASvcRef.U_MEMO_DETAIL)btnMoDetailListEdit.Tag;

                txtMemoDetailNO.Text = PAR_U_MEMO_DETAIL.PRINT_SEQ == null ? "" : PAR_U_MEMO_DETAIL.PRINT_SEQ.Value.ToString();
                txtMemoDetailPendCode.Text = PAR_U_MEMO_DETAIL.PEND_CODE;
                cmbMemoDetailPendCodeDesc.SelectedValue = txtMemoDetailPendCode.Text;
                cmbMemoDetailPendStatus.SelectedValue = PAR_U_MEMO_DETAIL.PEND_STATUS;
                txtMemoDetailPendStatusDate.Text = PAR_U_MEMO_DETAIL.PEND_STATUS_DT == null ? "" : Utility.dateTimeToString(PAR_U_MEMO_DETAIL.PEND_STATUS_DT.Value, "dd/MM/yyyy", "BU");

                if (PAR_U_MEMO_DETAIL.UMEMO_ID == null)
                {
                    txtMemoDetailPendCode.Enabled = true;
                    cmbMemoDetailPendCodeDesc.Enabled = true;
                }
                else
                {
                    txtMemoDetailPendCode.Enabled = false;
                    cmbMemoDetailPendCodeDesc.Enabled = false;
                }


                if (PAR_U_MEMO_DETAIL.MEMO_DESCRIPTION != null && (!String.IsNullOrEmpty(PAR_U_MEMO_DETAIL.MEMO_DESCRIPTION.PEND_DESCRIPTION)))
                {
                    txtMemoDescription.Text = PAR_U_MEMO_DETAIL.MEMO_DESCRIPTION.PEND_DESCRIPTION;
                }

                var GetDoc = from getDoc in PAR_DOCUMENT_PEND_CODE_COLL
                             where getDoc.PEND_CODE == txtMemoDetailPendCode.Text.Trim()
                             select getDoc;
                if (GetDoc != null && GetDoc.Count() > 0)
                {
                    NewBisPASvcRef.AUTB_DOCUMENT_PEND_CODE_COLL docColl = new NewBisPASvcRef.AUTB_DOCUMENT_PEND_CODE_COLL();
                    docColl.AddRange(GetDoc.ToArray());
                    DisplayOfPanelMemoDocument(docColl, PAR_U_MEMO_DETAIL.MEMO_DOCUMENT_Collection, "UPDATE");

                }
                else
                {
                    panelMemoDocument.Controls.Clear();
                }

                btnMemoDetailSave.Text = "แก้ไขข้อมูล";

                SetBackColorOfPanelMemoDetailList(panelMemoDetailList, PAR_U_MEMO_DETAIL);
            }
            catch (Exception ex)
            {
                SetMsgException(ex);
            }
        }
        void btnMoDetailListCancel_Click(object sender, EventArgs e)
        {
            try
            {
                var result = MessageBox.Show("คุณต้องการยกเเลิกข้อมูลนี้ใช่หรือไม่", "คำเตือน", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    Button btnMoDetailListCancel = (Button)sender;
                    PAR_U_MEMO_DETAIL = (NewBisPASvcRef.U_MEMO_DETAIL)btnMoDetailListCancel.Tag;

                    if (PAR_U_MEMO_ID.MEMO_DETAIL_Collection != null && PAR_U_MEMO_ID.MEMO_DETAIL_Collection.Count() > 0)
                    {
                        PAR_U_MEMO_ID.MEMO_DETAIL_Collection.Remove(PAR_U_MEMO_DETAIL);
                        PAR_U_MEMO_DETAIL = new NewBisPASvcRef.U_MEMO_DETAIL();
                    }

                    if (PAR_U_MEMO_ID.MEMO_DETAIL_Collection != null && PAR_U_MEMO_ID.MEMO_DETAIL_Collection.Count() > 0)
                    {
                        var GetData = from getData in PAR_U_MEMO_ID.MEMO_DETAIL_Collection
                                      orderby getData.PRINT_SEQ ascending
                                      select getData;
                        if (GetData != null && GetData.Count() > 0)
                        {
                            PAR_U_MEMO_ID.MEMO_DETAIL_Collection = new NewBisPASvcRef.U_MEMO_DETAIL_Collection();
                            PAR_U_MEMO_ID.MEMO_DETAIL_Collection.AddRange(GetData.ToArray());
                            for (int i = 0; i < PAR_U_MEMO_ID.MEMO_DETAIL_Collection.Count(); i++)
                            {
                                PAR_U_MEMO_ID.MEMO_DETAIL_Collection[i].PRINT_SEQ = i + 1;
                            }
                        }
                    }
                    else
                    {
                        PAR_MEMO_SAVE_SUSCESS = false;
                    }

                    ClearControlsOfMemoDetail();
                    DisplayOfPanelMemoDetail(PAR_U_MEMO_ID.MEMO_DETAIL_Collection);
                    txtMemoDetailPendCode.Focus();
                    SetBackColorOfPanelMemoDetailList(panelMemoDetailList, PAR_U_MEMO_DETAIL);


                }


            }
            catch (Exception ex)
            {
                SetMsgException(ex);
            }
        }
        private void SetBackColorOfPanelMemoDetailList(Panel objectPanel, NewBisPASvcRef.U_MEMO_DETAIL objDetail)
        {
            if (objectPanel.Controls.Count > 0)
            {
                foreach (Control c in objectPanel.Controls)
                {
                    if (c.Name == "memoDetailForm")
                    {
                        UserControlsForm.MemoDetailForm memoDetailForm = (UserControlsForm.MemoDetailForm)c;
                        NewBisPASvcRef.U_MEMO_DETAIL uMemoDetailObj = new NewBisPASvcRef.U_MEMO_DETAIL();
                        uMemoDetailObj = (NewBisPASvcRef.U_MEMO_DETAIL)memoDetailForm.Tag;
                        if (uMemoDetailObj.PRINT_SEQ == objDetail.PRINT_SEQ)
                        {
                            memoDetailForm.BackColor = Color.FromArgb(128, 128, 255);
                        }
                        else
                        {
                            memoDetailForm.BackColor = Color.FromName("BlanchedAlmond");
                        }
                    }
                }
            }
        }
        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                NewBisPASvcRef.ISIS_APPLICATION isisApplicationObj = new NewBisPASvcRef.ISIS_APPLICATION();

                isisApplicationObj.UPD_BY = UserID;

                isisApplicationObj.APP_NO = "K700001";
                isisApplicationObj.CHANNEL_TYPE = cmbChannelType.SelectedValue.ToString();
                isisApplicationObj.WORK_GROUP = cmbWorkGroup.SelectedValue.ToString();
                isisApplicationObj.REGISTER_DT = DateTime.Now;
                isisApplicationObj.REGISTER_BY = UserID;

                isisApplicationObj.ISIS_RIGHT = 'Y';
                isisApplicationObj.ISIS_CLEAN = 'Y';
                isisApplicationObj.ISIS_GUID = "TESTPD1234567890";

                isisApplicationObj.APP_OFC = "สนญ";
                isisApplicationObj.APP_OFCRCV_DT = DateTime.Now;
                isisApplicationObj.APP_SIGN_DT = DateTime.Now;
                isisApplicationObj.ENTRY_OFC = "สนญ";

                isisApplicationObj.SEND_POLICY_CHANNEL = 1;
                isisApplicationObj.SendPolicyBranchCode = "สนญ";


                if (ckbSpouse.Checked == true)
                {
                    isisApplicationObj.U_SPOUSE_ID = new NewBisPASvcRef.U_SPOUSE_ID();
                    isisApplicationObj.U_SPOUSE_ID.PRENAME = txtSpPrename.Text.Trim();
                    isisApplicationObj.U_SPOUSE_ID.NAME = txtSpName.Text.Trim();
                    isisApplicationObj.U_SPOUSE_ID.SURNAME = txtSpSurname.Text.Trim();
                    isisApplicationObj.U_SPOUSE_ID.IDCARD_NO = txtSpIDcardNo.Text.Trim();
                    isisApplicationObj.U_SPOUSE_ID.BIRTH_DT = Utility.StringToDateTime(txtSpBirthDate.Text.Trim(), "BU");
                    isisApplicationObj.U_SPOUSE_ID.SEX = Convert.ToChar(cmbSpSex.SelectedValue.ToString());
                    isisApplicationObj.U_SPOUSE_ID.MB_PHONE = txtSpMbPhone.Text.Trim();
                    isisApplicationObj.U_SPOUSE_ID.NATIONALITY = cmbSpNationality.SelectedValue.ToString();
                }

                isisApplicationObj.SALE_AGENT = txtSaleAgent.Text.Trim();

                String creditCardNo = txtCardNo1.Text.Trim() + txtCardNo2.Text.Trim() + txtCardNo3.Text.Trim() + txtCardNo4.Text.Trim();

                if (!(String.IsNullOrEmpty(creditCardNo)))
                {
                    isisApplicationObj.PAY_OPTION = cmbPayOption.SelectedValue.ToString();
                    isisApplicationObj.CARD_NO = creditCardNo;
                    isisApplicationObj.EXPIRE_MM = 12;
                    isisApplicationObj.EXPIRE_Y4 = 2560;
                }

                isisApplicationObj.PRENAME = txtPrename.Text.Trim();
                isisApplicationObj.NAME = txtName.Text.Trim();
                isisApplicationObj.SURNAME = txtSurname.Text.Trim();
                isisApplicationObj.IDCARD_NO = txtIdcardNo.Text.Trim();
                isisApplicationObj.BIRTH_DT = Utility.StringToDateTime(txtBirthDt.Text.Trim(), "BU");
                isisApplicationObj.SEX = cmbSex.SelectedValue.ToString();
                isisApplicationObj.MB_PHONE = txtMbPhone.Text.Trim();
                isisApplicationObj.NATIONALITY = cmbNationality.SelectedValue.ToString();
                isisApplicationObj.ST_AGE = Convert.ToInt16(txtStAge.Text.Trim());
                isisApplicationObj.MARITAL_STATUS = "O";
                isisApplicationObj.RELIGION = "B";

                if (!(String.IsNullOrEmpty(txtEmail.Text.Trim())))
                {
                    isisApplicationObj.EMAIL = txtEmail.Text.Trim();
                }

                isisApplicationObj.CUSTOMER_ADDRESS = new NewBisPASvcRef.ISIS_CUSTOMER_ADDRESS();
                isisApplicationObj.CUSTOMER_ADDRESS.ADDRESS_TYPE = 1;
                isisApplicationObj.CUSTOMER_ADDRESS.ADDRESS_NUMBER = txtAddressNumber.Text.Trim();
                isisApplicationObj.CUSTOMER_ADDRESS.ADDRESS_NAME = txtAddressName.Text.Trim();
                isisApplicationObj.CUSTOMER_ADDRESS.MOOH = txtMooh.Text.Trim();
                isisApplicationObj.CUSTOMER_ADDRESS.SOI = txtSoi.Text.Trim();
                isisApplicationObj.CUSTOMER_ADDRESS.ROAD = txtRoad.Text.Trim();
                isisApplicationObj.CUSTOMER_ADDRESS.TAMBOL = cmbTambol.Text;
                isisApplicationObj.CUSTOMER_ADDRESS.AMPHUR = cmbAmphur.Text;
                isisApplicationObj.CUSTOMER_ADDRESS.PROVINCE = cmbProvince.Text;
                isisApplicationObj.CUSTOMER_ADDRESS.ZIP_CODE = txtZipcode.Text.Trim();
                isisApplicationObj.CUSTOMER_ADDRESS.PHONE_NUMBER = txtPhoneNumber.Text.Trim();

                if (PAR_BENEFIT_ID_COLL_TMP != null && PAR_BENEFIT_ID_COLL_TMP.Count() > 0)
                {
                    isisApplicationObj.BENEFIT_Collection = new NewBisPASvcRef.ISIS_BENEFIT_Collection();
                    foreach (NewBisPASvcRef.U_BENEFIT_ID obj in PAR_BENEFIT_ID_COLL_TMP)
                    {
                        NewBisPASvcRef.ISIS_BENEFIT isisBenefitObj = new NewBisPASvcRef.ISIS_BENEFIT();
                        if (obj.BENEFIT_PERSON != null && obj.BENEFIT_PERSON.GAIN_PERCENT != null)
                        {
                            isisBenefitObj.PRENAME = obj.BENEFIT_PERSON.PRENAME;
                            isisBenefitObj.NAME = obj.BENEFIT_PERSON.NAME;
                            isisBenefitObj.SURNAME = obj.BENEFIT_PERSON.SURNAME;
                            isisBenefitObj.RELATION = obj.BENEFIT_PERSON.RELATION;
                            isisBenefitObj.GAIN_PERCENT = obj.BENEFIT_PERSON.GAIN_PERCENT;
                            isisBenefitObj.SEQ = obj.APP_BENEFIT.SEQ;
                            isisApplicationObj.BENEFIT_Collection.Add(isisBenefitObj);
                        }
                    }
                }

                #region "บัญชีขวัญบัวหลวง"
                isisApplicationObj.ACC_DEPOSIT_TYPE = "J";
                isisApplicationObj.ACC_NO = "8899858500";
                isisApplicationObj.ACC_NAME_TYPE = "P";
                isisApplicationObj.ACC_NAME = "Test  Test";
                isisApplicationObj.ACC_BRANCH = "0511";
                isisApplicationObj.ACC_BANK = "002";
                isisApplicationObj.UPD_BY = "001957";
                #endregion


                NewBisPASvcRef.U_LIFE_ID[] uLifeIDArr = new NewBisPASvcRef.U_LIFE_ID[1];

                //uLifeIDArr[0] = new NewBisPASvcRef.U_LIFE_ID();
                //uLifeIDArr[0] = PAR_PLAN_FREE;
                //uLifeIDArr[1] = new NewBisPASvcRef.U_LIFE_ID();
                //uLifeIDArr[1] = PAR_PLAN_PAID;

                uLifeIDArr[0] = new NewBisPASvcRef.U_LIFE_ID();
                uLifeIDArr[0] = PAR_PLAN_PAID;

                if (ckbSpouse.Checked == true)
                {
                    uLifeIDArr[1].APP_SPOUSE_Collection = new NewBisPASvcRef.U_APP_SPOUSE_Collection();
                    NewBisPASvcRef.U_APP_SPOUSE uAppSpouseObj = new NewBisPASvcRef.U_APP_SPOUSE();
                    uAppSpouseObj.ST_AGE = Convert.ToInt16(txtSpAge.Text.Trim());
                    uAppSpouseObj.ASS_TERM = uLifeIDArr[1].ASS_TERM;
                    uAppSpouseObj.ISU_DT = uLifeIDArr[1].ISU_DT;
                    uAppSpouseObj.ASS_DT = uLifeIDArr[1].ASS_DT;
                    uAppSpouseObj.TMN = 'N';
                    uLifeIDArr[1].APP_SPOUSE_Collection.Add(uAppSpouseObj);
                }

                NewBisPASvcRef.ProcessResult pr = new NewBisPASvcRef.ProcessResult();
                using (NewBisPASvcRef.NewBisPASvcClient client = new NewBisPASvcRef.NewBisPASvcClient())
                {
                    pr = client.AddISISapplicationForPA1(isisApplicationObj, uLifeIDArr);
                    if (pr.Successed == false)
                    {
                        throw new Exception(pr.ErrorMessage);
                    }
                }

                MessageBox.Show("Save OK");
            }
            catch (Exception ex)
            {
                SetMsgException(ex);
            }
        }

        private void txtHeight_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyValue == 13)
                {
                    ChkIntForTextBox(txtHeight.Text, "ส่วนสูง", txtHeight);
                }
            }
            catch (Exception ex)
            {
                SetMsgException(ex);
            }
        }

        private void txtHeight_Leave(object sender, EventArgs e)
        {
            try
            {
                ChkIntForTextBox(txtHeight.Text, "ส่วนสูง", txtHeight);
            }
            catch (Exception ex)
            {
                SetMsgException(ex);
            }
        }

        private void txtWeight_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyValue == 13)
                {
                    ChkIntForTextBox(txtWeight.Text, "น้ำหนัก", txtWeight);

                    decimal? weight = txtWeight.Text.Trim() == "" ? (decimal?)null : Convert.ToDecimal(txtWeight.Text.Trim());
                    decimal? height = txtHeight.Text.Trim() == "" ? (decimal?)null : Convert.ToDecimal(txtHeight.Text.Trim());
                    ITUtility.ProcessResult pr = new ProcessResult();
                    decimal? bmiValue = null;
                    using (PolicySvcRef.PolicySvcClient polClient = new PolicySvcRef.PolicySvcClient())
                    {
                        bmiValue = polClient.FFD_BMI(out pr, weight, height);
                        if (pr.Successed == false)
                        {
                            throw new Exception(pr.ErrorMessage);
                        }

                        if (bmiValue != null)
                        {

                            txtBmi.Text = bmiValue.Value.ToString("n2");

                            decimal ageCustomer = txtStAge.Text == "" ? 0 : Convert.ToDecimal(txtStAge.Text.Replace(",", ""));
                            if (ageCustomer < 12)
                            {
                                if (bmiValue.Value >= 13 && bmiValue.Value <= 21)
                                {
                                    txtBmi.BackColor = Color.FromArgb(192, 255, 192);
                                }
                                else
                                {
                                    txtBmi.BackColor = Color.FromArgb(255, 192, 192);
                                }
                            }
                            else
                            {
                                if (bmiValue.Value >= 17 && bmiValue.Value <= 32)
                                {
                                    txtBmi.BackColor = Color.FromArgb(192, 255, 192);
                                }
                                else
                                {
                                    txtBmi.BackColor = Color.FromArgb(255, 192, 192);
                                }
                            }
                        }
                        else
                        {
                            txtBmi.Text = "";
                            txtBmi.BackColor = Color.FromArgb(192, 255, 192);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                SetMsgException(ex);
            }
        }

        private void txtWeight_Leave(object sender, EventArgs e)
        {
            try
            {
                ChkIntForTextBox(txtWeight.Text, "น้ำหนัก", txtWeight);
            }
            catch (Exception ex)
            {
                SetMsgException(ex);
            }
        }

        private void txtIncomePerYear_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyValue == 13)
                {
                    ChkIntForTextBox(txtIncomePerYear.Text, "รายได้ประจำปี", txtIncomePerYear);
                }
            }
            catch (Exception ex)
            {
                SetMsgException(ex);
            }
        }

        private void txtIncomePerYear_Leave(object sender, EventArgs e)
        {
            try
            {
                ChkIntForTextBox(txtIncomePerYear.Text, "รายได้ประจำปี", txtIncomePerYear);
            }
            catch (Exception ex)
            {
                SetMsgException(ex);
            }
        }

        private void txtOcpClass_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyValue == 13)
                {


                    if (txtOcpType.Text != "" && txtOcpClass.Text != "")
                    {

                        txtOcpType.Text = txtOcpType.Text.ToUpper();
                        if (PAR_OCCUPATION_COLL != null && PAR_OCCUPATION_COLL.Count() > 0)
                        {
                            var GetData = from getData in PAR_OCCUPATION_COLL
                                          where getData.OCP_TYPE == txtOcpType.Text
                                          && getData.OCP_CLASS == txtOcpClass.Text
                                          && getData.MOTERCYCLE_USED == (ckbMotercycleUsed.Checked ? "Y" : "N")
                                          select getData;
                            if (GetData != null && GetData.Count() > 0)
                            {
                                cmbocpDesc.SelectedValue = GetData.ToArray()[0].CODE;
                            }
                            else
                            {
                                txtOcpType.Focus();
                                cmbocpDesc.SelectedValue = "";
                                throw new Exception("ไม่พบข้อมูลชั้นอาชีพ");
                            }
                        }
                        else
                        {
                            txtOcpType.Focus();
                            throw new Exception("ไม่พบข้อมูลชั้นอาชีพ");
                        }
                    }
                    else
                    {
                        txtOcpType.Focus();
                        throw new Exception("กรุณาระบุ รหัสของชั้นอาชีพให้ครบ");
                    }
                }

            }
            catch (Exception ex)
            {
                SetMsgException(ex);
            }
        }

        private void cmbocpDesc_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbocpDesc.SelectedValue != null)
                {
                    if (cmbocpDesc.SelectedValue.ToString() != "")
                    {
                        String code = cmbocpDesc.SelectedValue.ToString();
                        String[] val = code.Split('/');

                        if (val != null && val.Count() > 1)
                        {
                            txtOcpType.Text = val[0] == null ? "" : val[0].ToString();
                            txtOcpClass.Text = val[1] == null ? "" : val[1].ToString();



                        }
                        else
                        {
                            txtOcpType.Text = "";
                            txtOcpClass.Text = "";
                        }
                    }

                }
                else
                {
                    txtOcpType.Focus();
                    txtOcpType.Text = "";
                    txtOcpClass.Text = "";
                    throw new Exception("ไม่พบข้อมูลชั้นอาชีพ");
                }
            }
            catch (Exception ex)
            {
                SetMsgException(ex);
            }
        }

        private void cmbocpDesc_Leave(object sender, EventArgs e)
        {
            try
            {
                if (cmbocpDesc.Text.Trim() != "" && cmbocpDesc.Text.Trim() != "ระบุชั้นอาชีพ")
                {
                    var GetData = from getData in PAR_OCCUPATION_COLL
                                  where getData.OCP_TYPE_NAME == cmbocpDesc.Text.Trim()
                                  select getData;
                    if (!(GetData != null && GetData.Count() > 0))
                    {
                        txtOcpType.Text = "";
                        txtOcpClass.Text = "";
                        throw new Exception("ไม่พบข้อมูลชั้นอาชีพ");
                    }
                }
                else if (cmbocpDesc.Text.Trim() == "ระบุชั้นอาชีพ")
                {
                    txtOcpType.Text = "";
                    txtOcpClass.Text = "";
                }
                else
                {
                    txtOcpType.Text = "";
                    txtOcpClass.Text = "";
                    throw new Exception("ไม่พบข้อมูลชั้นอาชีพ");
                }
            }
            catch (Exception ex)
            {
                SetMsgException(ex);
            }
        }

        private void txtBranchCode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyValue == 13)
                {
                    if (txtBranchCode.Text.Trim() != "")
                    {
                        NewBisPASvcRef.ProcessResult pr = new NewBisPASvcRef.ProcessResult();

                        using (NewBisPASvcRef.NewBisPASvcClient client = new NewBisPASvcRef.NewBisPASvcClient())
                        {
                            txtBranchName.Text = client.GetBranchNamePA(out pr, txtBankCode.Text.Trim(), txtBranchCode.Text.Trim());
                            if (pr.Successed == false)
                            {
                                throw new Exception(pr.ErrorMessage);
                            }
                        }

                        if (txtBranchName.Text.Trim() == "")
                        {
                            txtBranchCode.Text = "";
                            throw new Exception("ไม่พบข้อมูลของสาขาธนาคารกรุงเทพที่ท่านต้องการ");
                        }
                    }
                    else
                    {
                        txtBranchCode.Focus();
                        txtBranchCode.Text = "";
                        txtBranchName.Text = "";
                        MessageBox.Show("กรุณาระบุรหัสสาขาที่ท่านต้องการ");
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                SetMsgException(ex);
            }
        }

        private void txtAccNo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyValue == 13)
                {
                    if (txtAccNo.Text.Trim() != "")
                    {
                        NewBisPASvcRef.ProcessResult pr = new NewBisPASvcRef.ProcessResult();
                        String depositType = "";
                        using (NewBisPASvcRef.NewBisPASvcClient client = new NewBisPASvcRef.NewBisPASvcClient())
                        {
                            pr = client.GetDepositType(out depositType, txtBankCode.Text.Trim(), txtAccNo.Text.Trim());
                            if (pr.Successed == false)
                            {
                                throw new Exception(pr.ErrorMessage);
                            }
                        }
                        if (!string.IsNullOrEmpty(depositType))
                        {
                            cmbDepositType.SelectedValue = depositType == null ? "" : depositType;
                        }
                    }
                    else
                    {
                        txtAccNo.Focus();
                        cmbDepositType.SelectedValue = "";
                        MessageBox.Show("กรุณาระบุเลขที่บัญชีที่ท่านต้องการ");
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                SetMsgException(ex);
            }
        }

        private void btnScan_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (txtAppNo.Text.Trim() == "")
                {
                    txtAppNo.Focus();
                    throw new Exception("กรุณาระบุเลขที่ใบคำขอที่ท่านต้องการ");
                }
                else
                {
                    if (PAR_PA_APPLICATION_ID.APP_ID == null)
                    {
                        throw new Exception("ยังไม่มีข้อมูลเอกสาร");
                    }
                    else
                    {
                        string channelType = cmbChannelType.SelectedValue.ToString();
                        ViewDocumentForm vdf = new ViewDocumentForm(PAR_PA_APPLICATION_ID.APP_ID.Value, txtAppNo.Text.Trim(), channelType, UserID);
                        //ViewDocumentForm vdf = new ViewDocumentForm(59850);
                        childForm.Add(vdf);
                        vdf.Show();
                    }

                }

            }
            catch (Exception ex)
            {
                SetMsgException(ex);
                this.Cursor = Cursors.Default;
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void ApplicationForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (childForm != null)
                {
                    foreach (Form item in childForm)
                    {
                        item.Close();
                    }
                }

                if (FORMAPP == "SELECT")
                {
                    this.Hide();
                    ApplicationSelectorForm.Show();
                    e.Cancel = true;
                }
                else if (FORMAPP == "VIEW")
                {
                    this.Hide();
                    ApplicationViewerForm.Show();
                    e.Cancel = true;
                }
                else
                {
                    if (APP_LOCK_ERROR == false && PAR_END_PROCESS == 'N')
                    {
                        if (PAR_PA_APPLICATION_ID != null && PAR_PA_APPLICATION_ID.APP_ID != null)
                        {
                            NewBisPASvcRef.ProcessResult pr = new NewBisPASvcRef.ProcessResult();
                            using (NewBisPASvcRef.NewBisPASvcClient client = new NewBisPASvcRef.NewBisPASvcClient())
                            {
                                NewBisPASvcRef.U_APPLICATION_LOCK uApplicationLockObj = new NewBisPASvcRef.U_APPLICATION_LOCK();
                                pr = client.GetUapplicationLockByAppID(out uApplicationLockObj, PAR_PA_APPLICATION_ID.APP_ID);
                                if (pr.Successed == false)
                                {
                                    throw new Exception(pr.ErrorMessage);
                                }

                                uApplicationLockObj.TRANSACTION_FLG = 'E';
                                pr = client.AdjustUApplicationLock(ref uApplicationLockObj, "UPDATE");
                                if (pr.Successed == false)
                                {
                                    throw new Exception(pr.ErrorMessage);
                                }
                            }
                        }
                    }
                    e.Cancel = false;
                }

                txtCertNo.ReadOnly = false;
                txtAppNo.Text = "";
                txtAppNo.ReadOnly = false;
                txtAppNo.Text = "";
                txtAppNo.Enabled = true;

                txtCertNo.Enabled = true;
                txtCertNo.ReadOnly = false;


                PK_UAP_ID = null;
                UAP_TRN_DT = null;

                ApplicationBeginParameter();


                ckbMedical.Checked = false;
                ckbCounterOffer.Checked = false;
                ckbchgConditionApp.Checked = false;
                ckbApsolutePlan.Checked = false;

            }
            catch (Exception ex)
            {
                e.Cancel = true;
                SetMsgException(ex);
            }
        }

        private void btnCustomerData_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                NewBISSvcRef.U_NAME_ID customerData = new NewBISSvcRef.U_NAME_ID();

                if (cmbCardType.SelectedValue.ToString() == "1")
                {
                    customerData.IDCARD_NO = txtIdcardNo.Text.Trim();
                }
                else if (cmbCardType.SelectedValue.ToString() == "2")
                {
                    customerData.PASSPORT = txtIdcardNo.Text.Trim();
                }

                customerData.NAME = txtName.Text.Trim();
                customerData.SURNAME = txtSurname.Text.Trim();
                customerData.SEX = cmbSex.SelectedValue.ToString() == "" ? (char?)null : Convert.ToChar(cmbSex.SelectedValue.ToString());
                customerData.BIRTH_DT = txtBirthDt.Text.Trim() == "" ? (DateTime?)null : Utility.StringToDateTime(txtBirthDt.Text.Trim(), "BU");

                NewBISSvcRef.U_NAME_ID parentData = new NewBISSvcRef.U_NAME_ID();
                if (ClickTabPlanData == true)
                {
                    if (ckbSpouse.Checked == true)
                    {
                        if (cmbSpCardType.SelectedValue.ToString() == "1")
                        {
                            parentData.IDCARD_NO = txtSpIDcardNo.Text.Trim();
                        }
                        else if (cmbSpCardType.SelectedValue.ToString() == "2")
                        {
                            parentData.PASSPORT = txtSpIDcardNo.Text.Trim();
                        }
                        parentData.NAME = txtSpName.Text.Trim();
                        parentData.SURNAME = txtSpSurname.Text.Trim();
                        parentData.SEX = cmbSpSex.SelectedValue.ToString() == "" ? (char?)null : Convert.ToChar(cmbSpSex.SelectedValue.ToString());
                        parentData.BIRTH_DT = txtSpBirthDate.Text.Trim() == "" ? (DateTime?)null : Utility.StringToDateTime(txtSpBirthDate.Text.Trim(), "BU");
                    }
                }
                else
                {
                    if (PAR_U_SPOUSE_ID_COLL != null && PAR_U_SPOUSE_ID_COLL.Count() > 0)
                    {
                        parentData.IDCARD_NO = PAR_U_SPOUSE_ID_COLL[0].IDCARD_NO;
                        parentData.PASSPORT = PAR_U_SPOUSE_ID_COLL[0].PASSPORT;
                        parentData.NAME = PAR_U_SPOUSE_ID_COLL[0].NAME;
                        parentData.SURNAME = PAR_U_SPOUSE_ID_COLL[0].SURNAME;
                        parentData.SEX = PAR_U_SPOUSE_ID_COLL[0].SEX;
                        parentData.BIRTH_DT = PAR_U_SPOUSE_ID_COLL[0].BIRTH_DT;
                    }
                }

                formCustomerData = new FormCustomerData();

                formCustomerData.APP_NO = txtAppNo.Text.Trim();
                formCustomerData.USERID = UserID;
                formCustomerData.FromControl = "btnCustomerData";
                formCustomerData.CUSTOMER_DATA = new NewBISSvcRef.U_NAME_ID();
                formCustomerData.CUSTOMER_DATA = customerData;
                formCustomerData.PARENT_DATA = new NewBISSvcRef.U_NAME_ID();
                formCustomerData.PARENT_DATA = parentData;
                formCustomerData.CUSTOMER_TYPE = cmbUnderWrite.SelectedValue.ToString();
                formCustomerData.UAPP_ID = null;

                if (PAR_PA_APPLICATION_ID != null && PAR_PA_APPLICATION_ID.APP_ID != null)
                {
                    if (PAR_PA_APPLICATION_ID.APPLICATION_Collection != null && PAR_PA_APPLICATION_ID.APPLICATION_Collection.Count() > 0)
                    {
                        formCustomerData.UAPP_ID = PAR_PA_APPLICATION_ID.APPLICATION_Collection[0].UAPP_ID;
                    }
                }

                formCustomerData.CLICK_FORM = PAR_CLICK_CUSFORM;

                childForm.Add(formCustomerData);
                formCustomerData.ShowDialog();

                if (formCustomerData.CHK_HAVE_DATA == 0)
                {
                    btnCustomerData.ForeColor = Color.Black;
                    btnCustomerData.Font = new Font("Tahoma", 9, FontStyle.Regular);
                }
                else
                {
                    btnCustomerData.ForeColor = Color.Red;
                    btnCustomerData.Font = new Font("Tahoma", 9, FontStyle.Bold);
                }

                PAR_CLICK_CUSFORM = formCustomerData.CLICK_FORM;

                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                SetMsgException(ex);
                this.Cursor = Cursors.Default;
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void btnSummaryDetail_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbSummryStatus.SelectedValue.ToString() == "")
                {
                    cmbSummryStatus.Focus();
                    throw new Exception("กรุณาระบุสถานะ");
                }
                if (txtSummaryDetail.Text.Trim() == "")
                {
                    txtSummaryDetail.Focus();
                    throw new Exception("กรุณาระบุรายละเอียดการพิจารณา");
                }

                int? seq = 0;
                if (!(PAR_W_SUMMARY_DETAIL_COLL != null && PAR_W_SUMMARY_DETAIL_COLL.Count() > 0))
                {
                    PAR_W_SUMMARY_DETAIL_COLL = new NewBisPASvcRef.W_SUMMARY_DETAIL_Collection();
                    seq = 1;
                }
                else
                {
                    seq = PAR_W_SUMMARY_DETAIL_COLL.Count() + 1;
                }

                if (txtSummaryDetailSeq.Text.Trim() == "")
                {
                    PAR_W_SUMMARY_DETAIL = new NewBisPASvcRef.W_SUMMARY_DETAIL();
                    String[] statusEnd = { "IF", "CC", "DC", "EX", "NT", "PP" };

                    if (statusEnd.Contains(PAR_W_SUMMARY.STATUS))
                    {
                        PAR_W_SUMMARY_DETAIL.SUMMARY_ID = PAR_W_SUMMARY.SUMMARY_ID;
                    }
                    else
                    {
                        PAR_W_SUMMARY_DETAIL.SUMMARY_ID = null;

                    }

                    PAR_W_SUMMARY_DETAIL.SEQ = seq;
                    PAR_W_SUMMARY_DETAIL.DETAIL = txtSummaryDetail.Text.Trim();
                    PAR_W_SUMMARY_DETAIL.FSYSTEM_DT = DateTime.Now;
                    PAR_W_SUMMARY_DETAIL.STATUS = cmbSummryStatus.SelectedValue.ToString();
                    PAR_W_SUMMARY_DETAIL.SUBSTATUS = cmbSummrySubStatus.SelectedValue.ToString();
                    PAR_W_SUMMARY_DETAIL_COLL.Add(PAR_W_SUMMARY_DETAIL);
                }
                else
                {
                    PAR_W_SUMMARY_DETAIL.DETAIL = txtSummaryDetail.Text.Trim();
                }

                txtSummaryDetailSeq.Text = "";
                txtSummaryDetail.Text = "";
                btnSummaryDetail.Text = "เพิ่มข้อมูล";
                DisplayOfPanelWSummaryDetail(PAR_W_SUMMARY_DETAIL_COLL);
            }
            catch (Exception ex)
            {
                SetMsgException(ex);
            }
        }

        private void DisplayOfPanelWSummaryDetail(NewBisPASvcRef.W_SUMMARY_DETAIL_Collection objectDetail)
        {
            if (objectDetail != null && objectDetail.Count() > 0)
            {
                while (panelSummaryDetail.Controls.Count > 0)
                {
                    panelSummaryDetail.Controls.RemoveAt(0);
                }

                IEnumerable<NewBisPASvcRef.W_SUMMARY_DETAIL> GetData = null;
                string channelType = cmbChannelType.SelectedValue.ToString();
                var channelList = new string[] { "PO", "PN" };
                if (channelList.Contains(channelType))
                {
                    GetData = from getData in objectDetail
                              orderby getData.FSYSTEM_DT ascending, getData.SEQ ascending
                              select getData;
                }
                else
                {
                    GetData = from getData in objectDetail
                              orderby getData.FSYSTEM_DT descending, getData.SEQ descending
                              select getData;
                }
                if (GetData != null && GetData.Count() > 0)
                {
                    objectDetail = new NewBisPASvcRef.W_SUMMARY_DETAIL_Collection();
                    objectDetail.AddRange(GetData);
                }
                int i = 0;
                int j = objectDetail.Count();
                foreach (NewBisPASvcRef.W_SUMMARY_DETAIL obj in objectDetail)
                {

                    UserControlsForm.SummaryDetailForm summaryDetailForm = new UserControlsForm.SummaryDetailForm();
                    summaryDetailForm.Name = "summaryDetailForm";
                    summaryDetailForm.Location = new Point(0, 0 + (i * 93));
                    summaryDetailForm.Size = new Size(680, 93);
                    summaryDetailForm.Tag = obj;

                    summaryDetailForm.txtSummaryDetailListSeq.Text = obj.SEQ != null ? obj.SEQ.ToString() : "";  // j.ToString();
                    summaryDetailForm.txtSummaryDetailList.Text = obj.DETAIL;

                    String status = "";
                    String substatus = "";
                    if (obj.SUMMARY_ID == null || channelList.Contains(channelType))
                    {

                        status = cmbSummryStatus.SelectedValue.ToString();
                        substatus = cmbSummrySubStatus.SelectedValue.ToString();
                        String[] statusEnd = { "IF", "CC", "DC", "EX", "NT", "PP" };

                        if (statusEnd.Contains(status))
                        {
                            summaryDetailForm.btnSummaryDetailListEdit.Enabled = false;
                            summaryDetailForm.btnSummaryDetailListDelete.Enabled = false;
                        }
                        else
                        {
                            summaryDetailForm.btnSummaryDetailListEdit.Enabled = true;
                            summaryDetailForm.btnSummaryDetailListDelete.Enabled = true;
                        }
                        summaryDetailForm.txtSummaryUpdID.Text = "";
                    }
                    else
                    {
                        status = obj.STATUS;
                        substatus = obj.SUBSTATUS;
                        summaryDetailForm.btnSummaryDetailListEdit.Enabled = false;
                        summaryDetailForm.btnSummaryDetailListDelete.Enabled = false;
                        summaryDetailForm.txtSummaryUpdID.Text = "ผู้บันทึก : " + obj.NAME;
                    }

                    var GetStatus = from getData in PAR_AUTB_STATUS_COLLECTION
                                    where getData.STATUS == status
                                    select getData;

                    String statusDesc = "";
                    if (GetStatus != null && GetStatus.Count() > 0)
                    {
                        statusDesc = GetStatus.First().DESCRIPTION;
                    }

                    var GetSubStatus = from getData in PAR_AUTB_SUBSTATUS_COLLECTION
                                       where getData.STATUS == status && getData.SUBSTATUS == substatus
                                       select getData;
                    String subStatusDesc = "";
                    if (GetSubStatus != null && GetSubStatus.Count() > 0)
                    {
                        subStatusDesc = GetSubStatus.First().DESCRIPTION;
                    }

                    summaryDetailForm.txtSummaryDetailListStatus.Text = "สถานะ : " + statusDesc + " / " + subStatusDesc;
                    summaryDetailForm.btnSummaryDetailListEdit.Tag = obj;
                    summaryDetailForm.btnSummaryDetailListEdit.Click += new EventHandler(btnSummaryDetailListEdit_Click);
                    summaryDetailForm.btnSummaryDetailListDelete.Tag = obj;
                    summaryDetailForm.btnSummaryDetailListDelete.Click += new EventHandler(btnSummaryDetailListDelete_Click);

                    panelSummaryDetail.Controls.Add(summaryDetailForm);
                    i = i + 1;
                    //  j = j - 1;
                }
            }
            else
            {
                panelSummaryDetail.Controls.Clear();
            }
        }

        void btnSummaryDetailListEdit_Click(object sender, EventArgs e)
        {
            try
            {
                Button btnSummaryDetailListEdit = (Button)sender;
                PAR_W_SUMMARY_DETAIL = new NewBisPASvcRef.W_SUMMARY_DETAIL();
                PAR_W_SUMMARY_DETAIL = (NewBisPASvcRef.W_SUMMARY_DETAIL)btnSummaryDetailListEdit.Tag;

                txtSummaryDetailSeq.Text = PAR_W_SUMMARY_DETAIL.SEQ.ToString();
                txtSummaryDetail.Text = PAR_W_SUMMARY_DETAIL.DETAIL;
                btnSummaryDetail.Text = "แก้ไขข้อมูล";
            }
            catch (Exception ex)
            {
                SetMsgException(ex);
            }
        }
        void btnSummaryDetailListDelete_Click(object sender, EventArgs e)
        {
            try
            {
                var result = MessageBox.Show("คุณต้องการยกเเลิกข้อมูลนี้ใช่หรือไม่", "คำเตือน", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    Button btnSummaryDetailListDelete = (Button)sender;
                    PAR_W_SUMMARY_DETAIL = new NewBisPASvcRef.W_SUMMARY_DETAIL();
                    PAR_W_SUMMARY_DETAIL = (NewBisPASvcRef.W_SUMMARY_DETAIL)btnSummaryDetailListDelete.Tag;

                    if (PAR_W_SUMMARY_DETAIL_COLL != null && PAR_W_SUMMARY_DETAIL_COLL.Count() > 0)
                    {
                        PAR_W_SUMMARY_DETAIL_COLL.Remove(PAR_W_SUMMARY_DETAIL);
                    }

                    txtSummaryDetailSeq.Text = "";
                    txtSummaryDetail.Text = "";
                    btnSummaryDetail.Text = "เพิ่มข้อมูล";
                    DisplayOfPanelWSummaryDetail(PAR_W_SUMMARY_DETAIL_COLL);
                }
            }
            catch (Exception ex)
            {
                SetMsgException(ex);
            }
        }

        private void txtSuspensePremium_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                String ChannelType = cmbChannelType.SelectedValue == null ? "" : cmbChannelType.SelectedValue.ToString();
                var isEndProcess = PAR_END_PROCESS == 'Y' ? true : false;
                FormCsSuspenseDetail formCsSuspenseDetail = new FormCsSuspenseDetail();
                formCsSuspenseDetail.APP_NO = txtAppNo.Text.Trim();
                formCsSuspenseDetail.CHANNEL_TYPE = ChannelType;
                formCsSuspenseDetail.IsEndProcess = isEndProcess;
                Decimal? premium = txtSuspensePremium.Text.Trim() == "" ? (decimal?)null : Convert.ToDecimal(txtSuspensePremium.Text.Trim().Replace(",", ""));
                formCsSuspenseDetail.PREMIUM = premium;
                String status = "";
                status = "WT";
                if (PAR_PA_APPLICATION_ID != null && PAR_PA_APPLICATION_ID.APP_ID != null)
                {
                    if (PAR_PA_APPLICATION_ID.APPLICATION_Collection != null && PAR_PA_APPLICATION_ID.APPLICATION_Collection.Count() > 0)
                    {
                        if (PAR_PA_APPLICATION_ID.APPLICATION_Collection[0].STATUS_ID_Collection != null && PAR_PA_APPLICATION_ID.APPLICATION_Collection[0].STATUS_ID_Collection.Count() > 0)
                        {
                            status = PAR_PA_APPLICATION_ID.APPLICATION_Collection[0].STATUS_ID_Collection[0].STATUS;
                        }
                    }
                }
                formCsSuspenseDetail.STATUS = status;
                formCsSuspenseDetail.ShowDialog();
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                SetMsgException(ex);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void txtNowSuspensePremium_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                String ChannelType = cmbChannelType.SelectedValue == null ? "" : cmbChannelType.SelectedValue.ToString();
                var isEndProcess = PAR_END_PROCESS == 'Y' ? true : false;
                FormCsSuspenseDetail formCsSuspenseDetail = new FormCsSuspenseDetail();
                formCsSuspenseDetail.APP_NO = txtAppNo.Text.Trim();
                formCsSuspenseDetail.CHANNEL_TYPE = ChannelType;
                formCsSuspenseDetail.IsEndProcess = isEndProcess;
                Decimal? premium = txtSuspensePremium.Text.Trim() == "" ? (decimal?)null : Convert.ToDecimal(txtSuspensePremium.Text.Trim().Replace(",", ""));
                formCsSuspenseDetail.PREMIUM = premium;
                String status = "";
                status = "WT";
                if (PAR_PA_APPLICATION_ID != null && PAR_PA_APPLICATION_ID.APP_ID != null)
                {
                    if (PAR_PA_APPLICATION_ID.APPLICATION_Collection != null && PAR_PA_APPLICATION_ID.APPLICATION_Collection.Count() > 0)
                    {
                        if (PAR_PA_APPLICATION_ID.APPLICATION_Collection[0].STATUS_ID_Collection != null && PAR_PA_APPLICATION_ID.APPLICATION_Collection[0].STATUS_ID_Collection.Count() > 0)
                        {
                            status = PAR_PA_APPLICATION_ID.APPLICATION_Collection[0].STATUS_ID_Collection[0].STATUS;
                        }
                    }
                }
                formCsSuspenseDetail.STATUS = status;
                formCsSuspenseDetail.ShowDialog();
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                SetMsgException(ex);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void txtUWIsuDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                try
                {
                    ChkDateForTextBox(txtUWIsuDate.Text.Trim(), "วันที่เริ่มคุ้มครอง", txtUWIsuDate);
                    tabMain.SelectTab("tabPlanData");
                    txtPaidIsuDate.Text = txtUWIsuDate.Text;
                    txtFreeIsuDate.Text = txtUWIsuDate.Text;
                    tabMain.SelectTab("tabUnderWriteData");
                }
                catch (Exception ex)
                {
                    SetMsgException(ex);
                }
            }
        }

        private void txtUWIsuDate_Leave(object sender, EventArgs e)
        {
            try
            {
                ChkDateForTextBox(txtUWIsuDate.Text.Trim(), "วันที่เริ่มคุ้มครอง", txtUWIsuDate);
            }
            catch (Exception ex)
            {
                SetMsgException(ex);
            }
        }

        private void txtCertNo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyValue == 13)
                {
                    if (!(string.IsNullOrEmpty(FORMAPP)))
                    {
                        return;
                    }
                    if (txtAppNo.ReadOnly == true)
                    {
                        return;
                    }
                    if (txtCertNo.ReadOnly == true)
                    {
                        return;
                    }

                    this.Cursor = Cursors.WaitCursor;

                    PAR_PA_APPLICATION_ID = new NewBisPASvcRef.PA_APPLICATION_ID();

                    String ChannelType = cmbChannelType.SelectedValue == null ? "" : cmbChannelType.SelectedValue.ToString();
                    String WorkGroup = cmbWorkGroup.SelectedValue == null ? "" : cmbWorkGroup.SelectedValue.ToString();

                    if (txtCertNo.Text == "")
                    {
                        txtCertNo.Focus();
                        throw new Exception("กรุณาระบุเลขที่ กรมธรรม์ หรือ Cert No.");
                    }

                    if (ChannelType == "KB" || ChannelType == "PD" || ChannelType == "PF")
                    {
                        if (txtPolicyHolding.Text == "")
                        {
                            txtPolicyHolding.Focus();
                            throw new Exception("กรุณาระบุเลขที่ Policy Holding");
                        }
                    }

                    String policyNo = txtCertNo.Text.PadLeft(8, '0').ToUpper();
                    NewBisPASvcRef.ProcessResult pr = new NewBisPASvcRef.ProcessResult();
                    String appNo = "";
                    using (NewBisPASvcRef.NewBisPASvcClient client = new NewBisPASvcRef.NewBisPASvcClient())
                    {
                        pr = client.GetAppNoByPolicyNoPA(out appNo, policyNo, txtPolicyHolding.Text, ChannelType);
                        if (pr.Successed == false)
                        {
                            throw new Exception(pr.ErrorMessage);
                        }
                    }

                    if (String.IsNullOrEmpty(appNo))
                    {
                        MessageBox.Show("ไม่พบข้อมูลที่ท่านต้องการ");
                    }
                    else
                    {
                        //เคลียร์ค่าต่างๆในโปรแกรมก่อน=======================
                        txtAppNo.Text = appNo.ToUpper();
                        ApplicationBeginParameter();
                        //END เคลียร์ค่าต่างๆในโปรแกรมก่อน===================

                        //ค้นหาข้อมูล ==================================
                        cmbChannelType.Enabled = false;
                        cmbWorkGroup.Enabled = false;
                        cmbAppNameID.Enabled = false;
                        txtAppNo.Enabled = false;
                        txtAppNo.ReadOnly = true;
                        //txtCertNo.Enabled = false;
                        txtCertNo.ReadOnly = true;
                        //txtPolicyHolding.Enabled = false;
                        txtPolicyHolding.ReadOnly = true;
                        btnScan.Enabled = true;
                        btnCustomerData.Enabled = true;
                        SearchData();
                        //END ค้นหาข้อมูล ==============================

                        //บันทึกข้อมูลครั้งแรก ===============================================================
                        if (PAR_PA_APPLICATION_ID.APP_ID == null)
                        {
                            VerifyFlag = false;
                            PLAN_ERROR = true;
                            if (WorkGroup == "")
                            {
                                txtAppDt.Text = "";
                                cmbWorkGroup.Focus();
                                throw new Exception("กรุณาระบุกลุ่มงานที่ท่านต้องการ");
                            }

                            NewBisPASvcRef.ProcessResult prSearch = new NewBisPASvcRef.ProcessResult();
                            DateTime? AppDate = null;
                            using (NewBisPASvcRef.NewBisPASvcClient clientSearch = new NewBisPASvcRef.NewBisPASvcClient())
                            {
                                if (WorkGroup == "BNC")
                                {
                                    prSearch = clientSearch.GetValidatePA(out AppDate, ChannelType, WorkGroup, DateTime.Now, null);
                                }
                                else if (WorkGroup == "SAL")
                                {
                                    prSearch = clientSearch.GetValidatePA(out AppDate, ChannelType, WorkGroup, DateTime.Now, null);
                                }

                                if (prSearch.Successed == false)
                                {
                                    txtAppDt.Text = "";
                                    txtAppNo.Focus();
                                    throw new Exception(prSearch.ErrorMessage);
                                }
                            }

                            if (AppDate == null)
                            {
                                txtAppDt.Text = "";
                                txtAppNo.Focus();
                                throw new Exception("ไม่พบข้อมูลวันที่ใบคำขอประจำเดือนกรุณาติดต่อฝ่าย IT");
                            }

                            //วันที่ใบคำขอประจำเดือน ===================================================
                            if (txtAppDt.Text.Trim() == "")
                            {
                                txtAppDt.Text = Utility.dateTimeToString(AppDate.Value, "dd/MM/yyyy", "BU");

                            }
                            //END วันที่ใบคำขอประจำเดือน ===============================================
                            //สาขาส่งใบคำขอ =========================================================
                            if (txtAppOfc.Text.Trim() == "")
                            {
                                if (WorkGroup == "SAL")
                                {
                                    txtAppOfc.Text = "สสง";
                                }
                                else
                                {
                                    if (PAR_USER != null && !string.IsNullOrEmpty(PAR_USER.UserID))
                                    {
                                        txtAppOfc.Text = PAR_USER.OfficeCode;
                                    }
                                }
                            }
                            //END สาขาส่งใบคำขอ ======================================================


                        }
                        else
                        {




                        }
                        //END บันทึกข้อมูลครั้งแรก ============================================================
                    }
                }
            }
            catch (Exception ex)
            {
                SetMsgException(ex);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void cmbMIB_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                if (cmbMIB.SelectedValue != null)
                {
                    if (cmbMIB.SelectedValue.ToString() != "")
                    {
                        String prename = "";
                        String firstName = "";
                        String LastName = "";
                        String birthDate = "";
                        String idcard = "";
                        String sex = "";

                        if (cmbMIB.SelectedValue.ToString() == "C")
                        {
                            prename = txtPrename.Text;
                            firstName = txtName.Text;
                            LastName = txtSurname.Text;
                            birthDate = txtBirthDt.Text;
                            idcard = txtIdcardNo.Text;
                            sex = cmbSex.SelectedValue == null ? "" : cmbSex.SelectedValue.ToString();
                        }
                        else
                        {

                            if (ClickTabPlanData == true)
                            {
                                prename = txtSpPrename.Text;
                                firstName = txtSpName.Text;
                                LastName = txtSpSurname.Text;
                                birthDate = txtSpBirthDate.Text;
                                idcard = txtSpIDcardNo.Text;
                                sex = cmbSpSex.SelectedValue == null ? "" : cmbSpSex.SelectedValue.ToString();
                            }
                            else
                            {
                                if (PAR_PA_APPLICATION_ID != null && PAR_PA_APPLICATION_ID.APP_ID != null)
                                {
                                    if (PAR_PA_APPLICATION_ID.APPLICATION_Collection != null && PAR_PA_APPLICATION_ID.APPLICATION_Collection.Count() > 0)
                                    {
                                        foreach (NewBisPASvcRef.U_APPLICATION uApplicationObj in PAR_PA_APPLICATION_ID.APPLICATION_Collection)
                                        {
                                            if (uApplicationObj.LIFE_ID != null && uApplicationObj.LIFE_ID.UAPP_ID != null)
                                            {
                                                if (uApplicationObj.LIFE_ID.APP_SPOUSE_Collection != null && uApplicationObj.LIFE_ID.APP_SPOUSE_Collection.Count() > 0)
                                                {
                                                    NewBisPASvcRef.U_APP_SPOUSE uAppSpouseObj = new NewBisPASvcRef.U_APP_SPOUSE();
                                                    uAppSpouseObj = uApplicationObj.LIFE_ID.APP_SPOUSE_Collection[0];
                                                    if (uAppSpouseObj.TMN == 'N')
                                                    {
                                                        if (PAR_U_SPOUSE_ID_COLL != null && PAR_U_SPOUSE_ID_COLL.Count() > 0)
                                                        {
                                                            var GetData = from getData in PAR_U_SPOUSE_ID_COLL
                                                                          where getData.SPOUSE_ID == uAppSpouseObj.SPOUSE_ID
                                                                          select getData;
                                                            if (GetData != null && GetData.Count() > 0)
                                                            {
                                                                NewBisPASvcRef.U_SPOUSE_ID uSpouseIDObj = new NewBisPASvcRef.U_SPOUSE_ID();
                                                                uSpouseIDObj = GetData.ToArray()[0];
                                                                prename = uSpouseIDObj.PRENAME;
                                                                firstName = uSpouseIDObj.NAME;
                                                                LastName = uSpouseIDObj.SURNAME;
                                                                birthDate = uSpouseIDObj.BIRTH_DT == null ? "" : Utility.dateTimeToString(uSpouseIDObj.BIRTH_DT.Value, "dd/MM/yyyy", "BU");
                                                                idcard = uSpouseIDObj.IDCARD_NO;
                                                                sex = uSpouseIDObj.SEX == null ? "" : uSpouseIDObj.SEX.Value.ToString();

                                                            }
                                                        }
                                                    }

                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }

                        if (String.IsNullOrEmpty(prename))
                        {
                            if (cmbMIB.SelectedValue.ToString() == "C")
                            {
                                throw new Exception("ไม่มีข้อมูลชื่อผู้เอาประกัน");
                            }
                            else if (cmbMIB.SelectedValue.ToString() == "S")
                            {
                                throw new Exception("ไม่มีข้อมูลชื่อคู่สมรส");
                            }
                        }

                        string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.StartMenu);
                        System.IO.DirectoryInfo appDataDirInfo = new System.IO.DirectoryInfo(appDataPath);
                        System.IO.DirectoryInfo startMenuDir = null;
                        startMenuDir = new System.IO.DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.StartMenu));
                        System.IO.FileInfo[] files = startMenuDir.GetFiles("Medical Information Bureau System(MIB).appref-ms", System.IO.SearchOption.AllDirectories);

                        if (files.Length == 0)
                        {
                            throw new Exception("กรุณาติดตั้ง โปรแกรม Medical Information Bureau System(MIB) ก่อน");
                        }
                        else
                        {
                            RegistryKey localMachineReg;
                            RegistryKey softwareReg;
                            RegistryKey bangkokLifeAssuranceReg;
                            GetRegistry(out localMachineReg, out softwareReg, out bangkokLifeAssuranceReg);

                            RegistryKey blaWinAppMenuReg = bangkokLifeAssuranceReg.OpenSubKey("BlaWinAppMenu", true);

                            blaWinAppMenuReg.SetValue("UserID", UserID, RegistryValueKind.String);
                            blaWinAppMenuReg.SetValue("FormAction", "FORMSAVE", RegistryValueKind.String);
                            blaWinAppMenuReg.SetValue("AppDate", txtAppDt.Text.Trim(), RegistryValueKind.String);

                            blaWinAppMenuReg.SetValue("Prename", prename, RegistryValueKind.String);
                            blaWinAppMenuReg.SetValue("FirstName", firstName, RegistryValueKind.String);
                            blaWinAppMenuReg.SetValue("LastName", LastName, RegistryValueKind.String);
                            blaWinAppMenuReg.SetValue("BirthDate", birthDate, RegistryValueKind.String);
                            blaWinAppMenuReg.SetValue("IDCard", idcard, RegistryValueKind.String);
                            blaWinAppMenuReg.SetValue("Sex", sex, RegistryValueKind.String);

                            blaWinAppMenuReg.Close();
                            bangkokLifeAssuranceReg.Close();
                            softwareReg.Close();
                            localMachineReg.Close();
                            Process.Start(files[0].FullName);


                        }
                    }
                }
            }
            catch (Exception ex)
            {
                SetMsgException(ex);
            }
        }

        private static void GetRegistry(out RegistryKey localMachineReg, out RegistryKey softwareReg, out RegistryKey bangkokLifeAssuranceReg)
        {
            localMachineReg = Registry.LocalMachine;
            softwareReg = localMachineReg.OpenSubKey("Software", true);
            bangkokLifeAssuranceReg = softwareReg.OpenSubKey("Bangkok Life Assurance", true);
            if (bangkokLifeAssuranceReg == null)
            {
                RegistryKey a = softwareReg.OpenSubKey("Wow6432Node", true);

                bangkokLifeAssuranceReg = a.OpenSubKey("Bangkok Life Assurance", true);
                if (bangkokLifeAssuranceReg == null)
                    throw new Exception("กรุณาเข้าใช้ระบบผ่านทางเมนูระบบงาน !");
            }
        }

        private void cmbSpouseFlg_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //if (PAR_PA_APPLICATION_ID.APPLICATION_Collection != null && PAR_PA_APPLICATION_ID.APPLICATION_Collection.Count() > 0)
                //{
                //    String spouseFlg = "";
                //    foreach (NewBisPASvcRef.U_APPLICATION uAppObj in PAR_PA_APPLICATION_ID.APPLICATION_Collection)
                //    {
                //        if (uAppObj.LIFE_ID != null && uAppObj.LIFE_ID.UAPP_ID != null)
                //        {
                //            if (uAppObj.LIFE_ID.SPOUSE_FLG == "T")
                //            {
                //                spouseFlg = "Y";
                //                break;
                //            }
                //        }
                //    }

                //    if (spouseFlg == "")
                //    {
                //        String spouseValue = cmbSpouseFlg.SelectedValue == null ? "" : cmbSpouseFlg.SelectedValue.ToString();
                //        if (spouseValue == "Y" && txtBenefitNo.Text.Trim() == "")
                //        {
                //            MessageBox.Show("ท่านไม่สามรถเลือกข้อมูลเป็นของคู่สมรสได้เพราะเป็นแบบประกันแบบไม่มีคู่สมรส");
                //            cmbSpouseFlg.SelectedValue = "";
                //        }
                //    }
                //}

                String spouseFlg = "";

                if (ckbLifeBuy.Checked == true && txtPaidSpouseFlg.Text.Trim() == "T")
                {
                    spouseFlg = "Y";
                }

                if (spouseFlg == "")
                {
                    String spouseValue = cmbSpouseFlg.SelectedValue == null ? "" : cmbSpouseFlg.SelectedValue.ToString();
                    if (spouseValue == "Y" && txtBenefitNo.Text.Trim() == "")
                    {
                        MessageBox.Show("ท่านไม่สามรถเลือกข้อมูลเป็นของคู่สมรสได้เพราะเป็นแบบประกันแบบไม่มีคู่สมรส");
                        cmbSpouseFlg.SelectedValue = "";
                    }
                }
            }
            catch (Exception ex)
            {
                SetMsgException(ex);
            }
        }

        private void label172_Click(object sender, EventArgs e)
        {

        }

        private void label137_Click(object sender, EventArgs e)
        {

        }

        private void cmbParentProvince_SelectedIndexChanged(object sender, EventArgs e)
        {

            try
            {
                if (cmbParentProvince.SelectedValue != null)
                {
                    NewBisPASvcRef.GET_LIST_COLLECTION amphurS = new NewBisPASvcRef.GET_LIST_COLLECTION();
                    if (cmbParentProvince.SelectedValue.ToString() == "")
                    {
                        NewBisPASvcRef.GET_LIST amphurObj = new NewBisPASvcRef.GET_LIST();
                        amphurObj.CODE = "";
                        amphurObj.DESCRIPTION = "ระบุอำเภอที่ต้องการ";
                        amphurS.Add(amphurObj);
                        cmbParentAmphur.DataSource = amphurS;
                        cmbParentAmphur.DisplayMember = "DESCRIPTION";
                        cmbParentAmphur.ValueMember = "CODE";
                        cmbParentAmphur.SelectedValue = "";
                    }
                    else
                    {
                        if (PAR_ZTB_POST_SUBDISTRICT_COLLECTION != null && PAR_ZTB_POST_SUBDISTRICT_COLLECTION.Count() > 0)
                        {
                            var AmphurLinq = (from amphurLinq in PAR_ZTB_POST_SUBDISTRICT_COLLECTION
                                              where amphurLinq.PROVINCE == cmbParentProvince.SelectedValue.ToString()
                                              orderby amphurLinq.DISTRICT ascending
                                              select amphurLinq.DISTRICT).Distinct();
                            if (AmphurLinq != null && AmphurLinq.Count() > 0)
                            {
                                NewBisPASvcRef.GET_LIST amphurObj = new NewBisPASvcRef.GET_LIST();
                                amphurObj.CODE = "";
                                amphurObj.DESCRIPTION = "ระบุอำเภอที่ต้องการ";
                                amphurS.Add(amphurObj);

                                foreach (var item in AmphurLinq)
                                {
                                    NewBisPASvcRef.GET_LIST obj = new NewBisPASvcRef.GET_LIST();
                                    obj.CODE = item.ToString();
                                    obj.DESCRIPTION = item.ToString();
                                    amphurS.Add(obj);
                                }
                                cmbParentAmphur.DataSource = amphurS;
                                cmbParentAmphur.DisplayMember = "DESCRIPTION";
                                cmbParentAmphur.ValueMember = "CODE";
                                cmbParentAmphur.SelectedValue = "";

                                AutoCompleteStringCollection data = new AutoCompleteStringCollection();
                                foreach (NewBisPASvcRef.GET_LIST item in amphurS)
                                {
                                    data.Add(item.DESCRIPTION);
                                }
                                cmbParentAmphur.AutoCompleteMode = AutoCompleteMode.Suggest;
                                cmbParentAmphur.AutoCompleteSource = AutoCompleteSource.CustomSource;
                                cmbParentAmphur.AutoCompleteCustomSource = data;
                            }
                            else
                            {
                                NewBisPASvcRef.GET_LIST amphurObj = new NewBisPASvcRef.GET_LIST();
                                amphurObj.CODE = "";
                                amphurObj.DESCRIPTION = "ระบุอำเภอที่ต้องการ";
                                amphurS.Add(amphurObj);
                                cmbParentAmphur.DataSource = amphurS;
                                cmbParentAmphur.DisplayMember = "DESCRIPTION";
                                cmbParentAmphur.ValueMember = "CODE";
                                cmbParentAmphur.SelectedValue = "";
                            }
                        }
                    }


                }
            }
            catch (Exception ex)
            {
                SetMsgException(ex);
            }

        }

        private void cmbParentAmphur_SelectedIndexChanged(object sender, EventArgs e)
        {

            try
            {
                if (cmbParentAmphur.SelectedValue != null)
                {
                    NewBisPASvcRef.GET_LIST_COLLECTION tambolS = new NewBisPASvcRef.GET_LIST_COLLECTION();
                    if (cmbParentAmphur.SelectedValue.ToString() == "")
                    {
                        NewBisPASvcRef.GET_LIST tambolObj = new NewBisPASvcRef.GET_LIST();
                        tambolObj.CODE = "";
                        tambolObj.DESCRIPTION = "ระบุตำบลที่ต้องการ";
                        tambolS.Add(tambolObj);
                        cmbParentTambol.DataSource = tambolS;
                        cmbParentTambol.DisplayMember = "DESCRIPTION";
                        cmbParentTambol.ValueMember = "CODE";
                        cmbParentTambol.SelectedValue = "";
                    }
                    else
                    {
                        if (PAR_ZTB_POST_SUBDISTRICT_COLLECTION != null && PAR_ZTB_POST_SUBDISTRICT_COLLECTION.Count() > 0)
                        {
                            var TambolLing = (from tambolLing in PAR_ZTB_POST_SUBDISTRICT_COLLECTION
                                              where tambolLing.PROVINCE == cmbParentProvince.SelectedValue.ToString()
                                              && tambolLing.DISTRICT == cmbParentAmphur.SelectedValue.ToString()
                                              orderby tambolLing.SUBDISTRICT ascending
                                              select tambolLing.SUBDISTRICT).Distinct();
                            if (TambolLing != null && TambolLing.Count() > 0)
                            {
                                NewBisPASvcRef.GET_LIST tambolObj = new NewBisPASvcRef.GET_LIST();
                                tambolObj.CODE = "";
                                tambolObj.DESCRIPTION = "ระบุตำบลที่ต้องการ";
                                tambolS.Add(tambolObj);

                                foreach (var item in TambolLing)
                                {
                                    NewBisPASvcRef.GET_LIST obj = new NewBisPASvcRef.GET_LIST();
                                    obj.CODE = item.ToString();
                                    obj.DESCRIPTION = item.ToString();
                                    tambolS.Add(obj);
                                }
                                cmbParentTambol.DataSource = tambolS;
                                cmbParentTambol.DisplayMember = "DESCRIPTION";
                                cmbParentTambol.ValueMember = "CODE";
                                cmbParentTambol.SelectedValue = "";
                                AutoCompleteStringCollection data = new AutoCompleteStringCollection();
                                foreach (NewBisPASvcRef.GET_LIST item in tambolS)
                                {
                                    data.Add(item.DESCRIPTION);
                                }
                                cmbParentTambol.AutoCompleteMode = AutoCompleteMode.Suggest;
                                cmbParentTambol.AutoCompleteSource = AutoCompleteSource.CustomSource;
                                cmbParentTambol.AutoCompleteCustomSource = data;
                            }
                            else
                            {
                                NewBisPASvcRef.GET_LIST tambolObj = new NewBisPASvcRef.GET_LIST();
                                tambolObj.CODE = "";
                                tambolObj.DESCRIPTION = "ระบุตำบลที่ต้องการ";
                                tambolS.Add(tambolObj);
                                cmbParentTambol.DataSource = tambolS;
                                cmbParentTambol.DisplayMember = "DESCRIPTION";
                                cmbParentTambol.ValueMember = "CODE";
                                cmbParentTambol.SelectedValue = "";
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                SetMsgException(ex);
            }

        }



        private bool IsChildApplication(int? cusAge)
        {
            string errorMessage;
            return CheckChildApplication(out errorMessage, cusAge);
        }

        private bool CheckChildApplication(out string errorMessage, int? cusAge)
        {
            errorMessage = "";
            string AdultFlag = "";
            if ((NewBisPASvcRef.AUTB_APPNAME_ID)cmbAppNameID.SelectedItem != null)
            {
                AdultFlag = ((NewBisPASvcRef.AUTB_APPNAME_ID)cmbAppNameID.SelectedItem).ADULT == null ? "" : ((NewBisPASvcRef.AUTB_APPNAME_ID)cmbAppNameID.SelectedItem).ADULT.Value.ToString();
            }
            if (cusAge == null)
            {
                errorMessage = "กรุณาระบุอายุผู้เอาประกัน";
                txtStAge.Focus();
                tabMain.SelectTab(0);
                return false;
            }

            var channelType = cmbChannelType.SelectedValue.ToString();
            var channelList = new string[] { "PO", "PN", "PF" };
            if (channelList.Contains(channelType))
            {
                if ((channelType == "PN" && cusAge >= 15) || (channelType == "PO" && cusAge >= 15) || (channelType == "PF" && cusAge >= 15))
                {
                    errorMessage = "ใบคำขอผู้ใหญ่";
                    return false;
                }
            }
            else
            {
                errorMessage = "ไม่มีใบคำขอผู้ใหญ่";
                return false;
            }

            if (AdultFlag == "Y" || AdultFlag == "")
            {
                //errorMessage = "เอกสารชุดใบคำขอไม่ใช่สำหรับผู้เยาว์";
                //MessageBox.Show("เอกสารชุดใบคำขอไม่ใช่สำหรับผู้เยาว์");
                //tabMain.SelectTab(0);
                //  return;
                //return false;
            }

            return true;

        }


        private bool IsChildApplicationAndAlertMessage(int? cusAge)
        {

            string errorMessage;
            if (CheckChildApplication(out errorMessage, cusAge))
            {
                return true;
            }
            else
            {
                MessageBox.Show(errorMessage);
                return false;
            }
        }

        private void txtParentIdcardNo_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyValue == 13)
            {
                try
                {
                    this.Cursor = Cursors.WaitCursor;
                    VerifyFlag = true;

                    //if (cmbCardType.SelectedValue.ToString() == "")
                    //{
                    //    cmbCardType.Focus();
                    //    throw new Exception("ระบุประเภทของบัตร");
                    //}

                    //if (txtIdcardNo.Text.Trim() == "")
                    //{
                    //    txtIdcardNo.Focus();
                    //    throw new Exception("ระบุเลขบัตรประชาชนหรือหนังสือเดินทาง");
                    //}

                    if (txtParentIdcardNo.Text.Trim() != "" && cmbParentCardType.SelectedValue.ToString() == "")
                    {
                        cmbParentCardType.Focus();
                        throw new Exception("ระบุประเภทของบัตร");
                    }

                    if (cmbParentCardType.SelectedValue.ToString() == "1" && txtParentIdcardNo.Text.Trim() != "")
                    {
                        if (Utility.ValidateIDCard(txtParentIdcardNo.Text) == false)
                        {
                            txtParentIdcardNo.Focus();
                            throw new Exception("รูปแบบของเลขที่บัตรประชาชนไม่ถูกต้อง");
                        }
                    }

                    NewBisPASvcRef.ProcessResult pr = new NewBisPASvcRef.ProcessResult();
                    NewBisPASvcRef.P_NAME_ID_COLLECTION pNameIDColl = new NewBisPASvcRef.P_NAME_ID_COLLECTION();
                    NewBisPASvcRef.P_PARENT_ID_COLLECTION pParentIDColl = new NewBisPASvcRef.P_PARENT_ID_COLLECTION();
                    DateTime? customerBirthDate = Utility.StringToDateTime(txtParentBirthDt.Text, "BU");
                    using (NewBisPASvcRef.NewBisPASvcClient client = new NewBisPASvcRef.NewBisPASvcClient())
                    {
                        pr = client.GetPNameIDByCustomerData(out pNameIDColl, out pParentIDColl, txtParentIdcardNo.Text.Trim(), txtParentName.Text.Trim(), txtParentSurname.Text.Trim(), cmbParentGender.SelectedValue.ToString(), customerBirthDate);
                        if (pr.Successed == false)
                        {
                            throw new Exception(pr.ErrorMessage);
                        }

                        PAR_PARENT_P_NAME_ID_COLL = new NewBisPASvcRef.P_NAME_ID_COLLECTION();
                        if (pNameIDColl != null && pNameIDColl.Count() > 0)
                        {
                            PAR_PARENT_P_NAME_ID_COLL.AddRange(pNameIDColl.ToArray());
                        }

                        PAR_PARENT_P_PARENT_ID_COLL = new NewBisPASvcRef.P_PARENT_ID_COLLECTION();
                        if (pParentIDColl != null && pParentIDColl.Count() > 0)
                        {
                            PAR_PARENT_P_PARENT_ID_COLL.AddRange(pParentIDColl.ToArray());
                        }

                    }

                    SetColorItemBeginParentName();

                    if ((pNameIDColl != null && pNameIDColl.Count() > 0) || (pParentIDColl != null && pParentIDColl.Count() > 0))
                    {
                        OldCustomerDataForm oldCusDataForm = new OldCustomerDataForm();
                        oldCusDataForm.NameID = PARENT_PA_NAME_ID;
                        oldCusDataForm.P_NAME_ID_COLL = new NewBisPASvcRef.P_NAME_ID_COLLECTION();
                        if (pNameIDColl != null && pNameIDColl.Count() > 0)
                        {
                            oldCusDataForm.P_NAME_ID_COLL.AddRange(pNameIDColl.ToArray());
                        }
                        oldCusDataForm.ParentID = PARENT_PARENT_ID;
                        oldCusDataForm.P_PARENT_ID_COLL = new NewBisPASvcRef.P_PARENT_ID_COLLECTION();
                        if (pParentIDColl != null && pParentIDColl.Count() > 0)
                        {
                            oldCusDataForm.P_PARENT_ID_COLL.AddRange(pParentIDColl.ToArray());
                        }

                        oldCusDataForm.ShowDialog();
                        if (oldCusDataForm.P_NAME_ID_OBJ != null && oldCusDataForm.P_NAME_ID_OBJ.NAME_ID != null)
                        {
                            PARENT_PA_NAME_ID = oldCusDataForm.P_NAME_ID_OBJ.NAME_ID;
                            cmbDataParent.BackColor = Color.FromArgb(bgRed[0], bgRed[1], bgRed[2]);
                            cmbDataParent.SelectedValue = "1";
                            ChkOldParentDataWithNewCusParent(oldCusDataForm.P_NAME_ID_OBJ);
                        }
                        else
                        {
                            PARENT_PA_NAME_ID = null;
                            cmbDataParent.BackColor = Color.White;
                            cmbDataParent.SelectedValue = "2";

                            if (oldCusDataForm.P_PARENT_ID_OBJ != null && oldCusDataForm.P_PARENT_ID_OBJ.PARENT_ID != null)
                            {
                                ChkOldParentDataWithParentNewCusData(oldCusDataForm.P_PARENT_ID_OBJ);
                            }
                        }

                        if (oldCusDataForm.P_PARENT_ID_OBJ != null && oldCusDataForm.P_PARENT_ID_OBJ.PARENT_ID != null)
                        {
                            PARENT_PARENT_ID = oldCusDataForm.P_PARENT_ID_OBJ.PARENT_ID;
                        }
                        else
                        {
                            PARENT_PARENT_ID = null;
                        }
                    }
                    else
                    {
                        PARENT_PA_NAME_ID = null;
                        PARENT_ID = null;
                        cmbDataParent.BackColor = Color.White;
                        cmbDataParent.SelectedValue = "2";
                    }
                    if (txtParentBirthDt.Text.Trim() != "")
                    {
                        Int64? ageCal = null;
                        using (NewBisPASvcRef.NewBisPASvcClient clientAge = new NewBisPASvcRef.NewBisPASvcClient())
                        {
                            DateTime? dateCal = null;
                            dateCal = Utility.StringToDateTime(txtAppSignDt.Text, "BU");
                            if (PAR_PA_APPLICATION_ID != null && PAR_PA_APPLICATION_ID.APP_ID != null)
                            {
                                if (PAR_PA_APPLICATION_ID.APPLICATION_Collection != null && PAR_PA_APPLICATION_ID.APPLICATION_Collection.Count() > 0)
                                {
                                    foreach (NewBisPASvcRef.U_APPLICATION obj in PAR_PA_APPLICATION_ID.APPLICATION_Collection)
                                    {
                                        if (obj.LIFE_ID != null && obj.LIFE_ID.UAPP_ID != null)
                                        {
                                            dateCal = obj.LIFE_ID.ISU_DT;
                                        }
                                    }
                                }
                            }
                            if (cmbChannelType.SelectedValue == null || cmbChannelType.SelectedValue.ToString() == "")
                            {
                                cmbChannelType.Focus();
                                throw new Exception("กรุณาระบุช่องทางการขายที่ท่านต้องการ");
                            }
                            String channelType = cmbChannelType.SelectedValue.ToString();
                            DateTime? birthDateCal = Utility.StringToDateTime(txtParentBirthDt.Text, "BU");
                            ageCal = clientAge.GetAge(out pr, birthDateCal, dateCal, dateCal, channelType);
                            if (pr.Successed == false)
                            {
                                throw new Exception(pr.ErrorMessage);
                            }
                        }

                        txtParentStAge.Text = ageCal == null ? "" : ageCal.Value.ToString();
                    }
                    else
                    {
                        txtParentStAge.Text = "";
                    }

                    if (PARENT_PA_NAME_ID == null)
                    {
                        cmbParentAdrType.SelectedValue = "2";
                        cmbParentAdrType.BackColor = Color.White;
                        PARENT_ADDRESS_ID = null;
                        SetColorItemBeginParentAddress();
                    }
                    else
                    {
                        ShowParentAddressDialog();
                    }

                    if (PARENT_PA_NAME_ID == null && IsChannelTypeFreePolicy(cmbChannelType.SelectedValue.ToString()))
                    {
                        var client = new NewBisPASvcRef.NewBisPASvcClient();
                        NewBisPASvcRef.LBDU_CUSTOMER dataObject;

                        var custypeOfLBDU = cmbCardType.SelectedValue.ToString() == "1" ? "CI" : "PP";
                        var pr2 = client.GetLBDU_CUSTOMER(out dataObject, custypeOfLBDU, txtIdcardNo.Text.Trim());
                        if (!pr2.Successed)
                        {
                            throw new Exception("Error GetLBDU_CUSTOMER : " + pr2.ErrorMessage);
                        }

                        if (dataObject != null)
                        {
                            AddressOfLBDUOfParent.Visible = true;
                            AddressOfLBDUOfParent.Text = dataObject.ADDR + " " + (!string.IsNullOrEmpty(dataObject.STREET) ? "ถ." + dataObject.STREET : dataObject.STREET) +
                                                                   " " + (!string.IsNullOrEmpty(dataObject.SUBDIST) ? " แขวง" + dataObject.SUBDIST : dataObject.SUBDIST) +
                                                                   " " + (!string.IsNullOrEmpty(dataObject.DISTRICT) ? " แขวง" + dataObject.DISTRICT : dataObject.DISTRICT) + " " + dataObject.PROVINCE + " " + dataObject.POST;
                            txtParentBirthDt.Text = dataObject.DOB != null ? Utility.dateTimeToString(dataObject.DOB.Value, "dd/MM/yyyy", "BU") : "";
                            txtParentPrename.Text = dataObject.PRENAME;
                            txtParentName.Text = dataObject.NAME;
                            txtParentSurname.Text = dataObject.SURNAME;
                            cmbParentGender.SelectedValue = dataObject.SEX != null ? dataObject.SEX.ToString() : "";
                            txtParentEmail.Text = dataObject.EMAIL;
                            cmbParentNationality.SelectedValue = dataObject.NATIONALITY;
                            txtParentMbPhone.Text = dataObject.TELNOM;
                            txtParentRoad.Text = dataObject.STREET;
                            cmbParentProvince.SelectedValue = dataObject.PROVINCE == "กรุงเทพมหานคร" ? "กรุงเทพฯ" : dataObject.PROVINCE;
                            var districtData = ((NewBisPASvcRef.GET_LIST_COLLECTION)cmbAmphur.DataSource);
                            if (districtData != null && districtData.Any())
                            {
                                var hasDistrict = districtData.Where(item => item.DESCRIPTION == dataObject.DISTRICT).Any();
                                if (hasDistrict)
                                {
                                    cmbParentAmphur.SelectedValue = dataObject.DISTRICT;
                                    var subdistrictData = ((NewBisPASvcRef.GET_LIST_COLLECTION)cmbTambol.DataSource);
                                    if (subdistrictData != null && subdistrictData.Any())
                                    {
                                        var hasSubDistrict = subdistrictData.Where(item => item.DESCRIPTION == dataObject.SUBDIST).Any();
                                        if (hasSubDistrict)
                                        {
                                            cmbParentTambol.SelectedValue = dataObject.SUBDIST;
                                        }
                                    }
                                }
                            }
                            if (string.IsNullOrEmpty(txtParentEmail.Text))
                            {
                                txtParentEmail.Text = dataObject.EMAIL;
                            }
                            if (string.IsNullOrEmpty(cmbParentNationality.SelectedValue.ToString()))
                            {
                                cmbParentNationality.SelectedValue = dataObject.NATIONALITY;
                            }
                            if (string.IsNullOrEmpty(txtParentMbPhone.Text))
                            {
                                txtParentMbPhone.Text = dataObject.TELNOM;
                            }
                            if (string.IsNullOrEmpty(txtParentRoad.Text))
                            {
                                txtParentRoad.Text = dataObject.STREET;
                            }
                            if (string.IsNullOrEmpty(txtParentPhoneNumber.Text))
                            {
                                txtParentPhoneNumber.Text = dataObject.TELNOR;
                            }
                            txtParentZipcode.Text = dataObject.POST;
                            cmbParentAdrType.SelectedValue = "2";
                            cmbDataParent.SelectedValue = "2";
                            cmbParentAddressType.SelectedValue = "3";
                        }
                        else
                        {
                            AddressOfLBDUOfParent.Visible = false;
                            AddressOfLBDUOfParent.Text = "";

                        }
                    }

                }
                catch (Exception ex)
                {
                    SetMsgException(ex);
                }
                finally
                {
                    this.Cursor = Cursors.Default;
                }
            }
        }

        private void txtParentIdcardNo_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtParentBirthDt_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyValue == 13)
            {
                try
                {
                    this.Cursor = Cursors.WaitCursor;
                    VerifyFlag = true;

                    //if (cmbCardType.SelectedValue.ToString() == "")
                    //{
                    //    cmbCardType.Focus();
                    //    throw new Exception("ระบุประเภทของบัตร");
                    //}

                    //if (txtIdcardNo.Text.Trim() == "")
                    //{
                    //    txtIdcardNo.Focus();
                    //    throw new Exception("ระบุเลขบัตรประชาชนหรือหนังสือเดินทาง");
                    //}

                    if (txtParentIdcardNo.Text.Trim() != "" && cmbParentCardType.SelectedValue.ToString() == "")
                    {
                        cmbParentCardType.Focus();
                        throw new Exception("ระบุประเภทของบัตร");
                    }

                    if (cmbParentCardType.SelectedValue.ToString() == "1" && txtParentIdcardNo.Text.Trim() != "")
                    {
                        if (Utility.ValidateIDCard(txtParentIdcardNo.Text) == false)
                        {
                            txtParentIdcardNo.Focus();
                            throw new Exception("รูปแบบของเลขที่บัตรประชาชนไม่ถูกต้อง");
                        }
                    }

                    NewBisPASvcRef.ProcessResult pr = new NewBisPASvcRef.ProcessResult();
                    NewBisPASvcRef.P_NAME_ID_COLLECTION pNameIDColl = new NewBisPASvcRef.P_NAME_ID_COLLECTION();
                    NewBisPASvcRef.P_PARENT_ID_COLLECTION pParentIDColl = new NewBisPASvcRef.P_PARENT_ID_COLLECTION();
                    DateTime? customerBirthDate = Utility.StringToDateTime(txtParentBirthDt.Text, "BU");
                    using (NewBisPASvcRef.NewBisPASvcClient client = new NewBisPASvcRef.NewBisPASvcClient())
                    {
                        pr = client.GetPNameIDByCustomerData(out pNameIDColl, out pParentIDColl, txtParentIdcardNo.Text.Trim(), txtParentName.Text.Trim(), txtParentSurname.Text.Trim(), cmbParentGender.SelectedValue.ToString(), customerBirthDate);
                        if (pr.Successed == false)
                        {
                            throw new Exception(pr.ErrorMessage);
                        }

                        PAR_PARENT_P_NAME_ID_COLL = new NewBisPASvcRef.P_NAME_ID_COLLECTION();
                        if (pNameIDColl != null && pNameIDColl.Count() > 0)
                        {
                            PAR_PARENT_P_NAME_ID_COLL.AddRange(pNameIDColl.ToArray());
                        }

                        PAR_PARENT_P_PARENT_ID_COLL = new NewBisPASvcRef.P_PARENT_ID_COLLECTION();
                        if (pParentIDColl != null && pParentIDColl.Count() > 0)
                        {
                            PAR_PARENT_P_PARENT_ID_COLL.AddRange(pParentIDColl.ToArray());
                        }

                    }

                    SetColorItemBeginParentName();

                    if ((pNameIDColl != null && pNameIDColl.Count() > 0) || (pParentIDColl != null && pParentIDColl.Count() > 0))
                    {
                        OldCustomerDataForm oldCusDataForm = new OldCustomerDataForm();
                        oldCusDataForm.NameID = PARENT_PA_NAME_ID;
                        oldCusDataForm.P_NAME_ID_COLL = new NewBisPASvcRef.P_NAME_ID_COLLECTION();
                        if (pNameIDColl != null && pNameIDColl.Count() > 0)
                        {
                            oldCusDataForm.P_NAME_ID_COLL.AddRange(pNameIDColl.ToArray());
                        }
                        oldCusDataForm.ParentID = PARENT_PARENT_ID;
                        oldCusDataForm.P_PARENT_ID_COLL = new NewBisPASvcRef.P_PARENT_ID_COLLECTION();
                        if (pParentIDColl != null && pParentIDColl.Count() > 0)
                        {
                            oldCusDataForm.P_PARENT_ID_COLL.AddRange(pParentIDColl.ToArray());
                        }

                        oldCusDataForm.ShowDialog();
                        if (oldCusDataForm.P_NAME_ID_OBJ != null && oldCusDataForm.P_NAME_ID_OBJ.NAME_ID != null)
                        {
                            PARENT_PA_NAME_ID = oldCusDataForm.P_NAME_ID_OBJ.NAME_ID;
                            cmbDataParent.BackColor = Color.FromArgb(bgRed[0], bgRed[1], bgRed[2]);
                            cmbDataParent.SelectedValue = "1";
                            ChkOldParentDataWithNewCusParent(oldCusDataForm.P_NAME_ID_OBJ);
                        }
                        else
                        {
                            PARENT_PA_NAME_ID = null;
                            cmbDataParent.BackColor = Color.White;
                            cmbDataParent.SelectedValue = "2";

                            if (oldCusDataForm.P_PARENT_ID_OBJ != null && oldCusDataForm.P_PARENT_ID_OBJ.PARENT_ID != null)
                            {
                                ChkOldParentDataWithParentNewCusData(oldCusDataForm.P_PARENT_ID_OBJ);
                            }
                        }

                        if (oldCusDataForm.P_PARENT_ID_OBJ != null && oldCusDataForm.P_PARENT_ID_OBJ.PARENT_ID != null)
                        {
                            PARENT_PARENT_ID = oldCusDataForm.P_PARENT_ID_OBJ.PARENT_ID;
                        }
                        else
                        {
                            PARENT_PARENT_ID = null;
                        }
                    }
                    else
                    {
                        PARENT_PA_NAME_ID = null;
                        PARENT_PARENT_ID = null;
                        cmbDataCus.BackColor = Color.White;
                        cmbDataCus.SelectedValue = "2";
                    }

                    if (txtParentBirthDt.Text.Trim() != "")
                    {
                        Int64? ageCal = null;
                        using (NewBisPASvcRef.NewBisPASvcClient clientAge = new NewBisPASvcRef.NewBisPASvcClient())
                        {
                            DateTime? dateCal = null;
                            dateCal = Utility.StringToDateTime(txtAppSignDt.Text, "BU");
                            if (PAR_PA_APPLICATION_ID != null && PAR_PA_APPLICATION_ID.APP_ID != null)
                            {
                                if (PAR_PA_APPLICATION_ID.APPLICATION_Collection != null && PAR_PA_APPLICATION_ID.APPLICATION_Collection.Count() > 0)
                                {
                                    foreach (NewBisPASvcRef.U_APPLICATION obj in PAR_PA_APPLICATION_ID.APPLICATION_Collection)
                                    {
                                        if (obj.LIFE_ID != null && obj.LIFE_ID.UAPP_ID != null)
                                        {
                                            dateCal = obj.LIFE_ID.ISU_DT;
                                        }
                                    }
                                }
                            }
                            if (cmbChannelType.SelectedValue == null || cmbChannelType.SelectedValue.ToString() == "")
                            {
                                cmbChannelType.Focus();
                                throw new Exception("กรุณาระบุช่องทางการขายที่ท่านต้องการ");
                            }
                            String channelType = cmbChannelType.SelectedValue.ToString();
                            DateTime? birthDateCal = Utility.StringToDateTime(txtParentBirthDt.Text, "BU");
                            ageCal = clientAge.GetAge(out pr, birthDateCal, dateCal, dateCal, channelType);
                            if (pr.Successed == false)
                            {
                                throw new Exception(pr.ErrorMessage);
                            }
                        }

                        txtParentStAge.Text = ageCal == null ? "" : ageCal.Value.ToString();
                    }
                    else
                    {
                        txtParentStAge.Text = "";
                    }

                    if (PARENT_PA_NAME_ID == null)
                    {
                        cmbAdrType.SelectedValue = "2";
                        cmbAdrType.BackColor = Color.White;
                        ADDRESS_ID = null;
                        SetColorItemBeginParentAddress();
                    }
                    else
                    {
                        SetColorItemBeginParentAddress();

                        //   ShowAddressDialog();
                    }

                    if (PARENT_PA_NAME_ID == null && IsChannelTypeFreePolicy(cmbChannelType.SelectedValue.ToString()))
                    {
                        var client = new NewBisPASvcRef.NewBisPASvcClient();
                        NewBisPASvcRef.LBDU_CUSTOMER dataObject;

                        var custypeOfLBDU = cmbCardType.SelectedValue.ToString() == "1" ? "CI" : "PP";
                        var pr2 = client.GetLBDU_CUSTOMER(out dataObject, custypeOfLBDU, txtParentIdcardNo.Text.Trim());
                        if (!pr2.Successed)
                        {
                            throw new Exception("Error GetLBDU_CUSTOMER : " + pr2.ErrorMessage);
                        }

                        if (dataObject != null)
                        {
                            AddressOfLBDUOfParent.Visible = true;
                            AddressOfLBDUOfParent.Text = dataObject.ADDR + " " + (!string.IsNullOrEmpty(dataObject.STREET) ? "ถ." + dataObject.STREET : dataObject.STREET) +
                                                                   " " + (!string.IsNullOrEmpty(dataObject.SUBDIST) ? " แขวง" + dataObject.SUBDIST : dataObject.SUBDIST) +
                                                                   " " + (!string.IsNullOrEmpty(dataObject.DISTRICT) ? " แขวง" + dataObject.DISTRICT : dataObject.DISTRICT) + " " + dataObject.PROVINCE + " " + dataObject.POST;
                            txtParentBirthDt.Text = dataObject.DOB != null ? Utility.dateTimeToString(dataObject.DOB.Value, "dd/MM/yyyy", "BU") : "";
                            txtParentPrename.Text = dataObject.PRENAME;
                            txtParentName.Text = dataObject.NAME;
                            txtParentSurname.Text = dataObject.SURNAME;
                            cmbParentGender.SelectedValue = dataObject.SEX != null ? dataObject.SEX.ToString() : "";
                            txtParentEmail.Text = dataObject.EMAIL;
                            cmbParentNationality.SelectedValue = dataObject.NATIONALITY;
                            txtParentMbPhone.Text = dataObject.TELNOM;
                            txtParentRoad.Text = dataObject.STREET;
                            cmbParentProvince.SelectedValue = dataObject.PROVINCE == "กรุงเทพมหานคร" ? "กรุงเทพฯ" : dataObject.PROVINCE;
                            var districtData = ((NewBisPASvcRef.GET_LIST_COLLECTION)cmbAmphur.DataSource);
                            if (districtData != null && districtData.Any())
                            {
                                var hasDistrict = districtData.Where(item => item.DESCRIPTION == dataObject.DISTRICT).Any();
                                if (hasDistrict)
                                {
                                    cmbParentAmphur.SelectedValue = dataObject.DISTRICT;
                                    var subdistrictData = ((NewBisPASvcRef.GET_LIST_COLLECTION)cmbTambol.DataSource);
                                    if (subdistrictData != null && subdistrictData.Any())
                                    {
                                        var hasSubDistrict = subdistrictData.Where(item => item.DESCRIPTION == dataObject.SUBDIST).Any();
                                        if (hasSubDistrict)
                                        {
                                            cmbParentTambol.SelectedValue = dataObject.SUBDIST;
                                        }
                                    }
                                }
                            }
                            txtParentZipcode.Text = dataObject.POST;
                            if (string.IsNullOrEmpty(txtParentEmail.Text))
                            {
                                txtParentEmail.Text = dataObject.EMAIL;
                            }
                            if (string.IsNullOrEmpty(cmbParentNationality.SelectedValue.ToString()))
                            {
                                cmbParentNationality.SelectedValue = dataObject.NATIONALITY;
                            }
                            if (string.IsNullOrEmpty(txtParentMbPhone.Text))
                            {
                                txtParentMbPhone.Text = dataObject.TELNOM;
                            }
                            if (string.IsNullOrEmpty(txtParentRoad.Text))
                            {
                                txtParentRoad.Text = dataObject.STREET;
                            }
                            if (string.IsNullOrEmpty(txtParentPhoneNumber.Text))
                            {
                                txtParentPhoneNumber.Text = dataObject.TELNOR;
                            }
                            cmbParentAdrType.SelectedValue = "2";
                            cmbDataParent.SelectedValue = "2";
                            cmbParentAddressType.SelectedValue = "3";
                        }
                        else
                        {
                            AddressOfLBDUOfParent.Visible = false;
                            AddressOfLBDUOfParent.Text = "";

                        }
                    }
                }
                catch (Exception ex)
                {
                    SetMsgException(ex);
                }
                finally
                {
                    this.Cursor = Cursors.Default;
                }
            }

        }

        private void txtParentOcpClass_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyValue == 13)
                {
                    if (txtParentOcpType.Text != "" && txtParentOcpClass.Text != "")
                    {
                        if (PAR_OCCUPATION_COLL != null && PAR_OCCUPATION_COLL.Count() > 0)
                        {
                            var GetData = from getData in PAR_OCCUPATION_COLL
                                          where getData.OCP_TYPE == txtParentOcpType.Text
                                          && getData.OCP_CLASS == txtParentOcpClass.Text
                                          select getData;
                            if (GetData != null && GetData.Count() > 0)
                            {
                                cmbParentbocpDesc.SelectedValue = GetData.ToArray()[0].CODE;
                            }
                            else
                            {
                                txtParentOcpType.Focus();
                                cmbParentbocpDesc.SelectedValue = "";
                                throw new Exception("ไม่พบข้อมูลชั้นอาชีพ");
                            }
                        }
                        else
                        {
                            txtParentOcpType.Focus();
                            throw new Exception("ไม่พบข้อมูลชั้นอาชีพ");
                        }
                    }
                    else
                    {
                        txtParentOcpType.Focus();
                        throw new Exception("กรุณาระบุ รหัสของชั้นอาชีพให้ครบ");
                    }
                }

            }
            catch (Exception ex)
            {
                SetMsgException(ex);
            }
        }

        private void cmbParentbocpDesc_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbParentbocpDesc.SelectedValue != null)
                {
                    if (cmbParentbocpDesc.SelectedValue.ToString() != "")
                    {
                        String code = cmbParentbocpDesc.SelectedValue.ToString();
                        String[] val = code.Split('/');

                        if (val != null && val.Count() > 1)
                        {
                            txtParentOcpType.Text = val[0] == null ? "" : val[0].ToString();
                            txtParentOcpClass.Text = val[1] == null ? "" : val[1].ToString();
                        }
                        else
                        {
                            txtParentOcpType.Text = "";
                            txtParentOcpClass.Text = "";
                        }
                    }

                }
                else
                {
                    txtParentOcpType.Focus();
                    txtParentOcpType.Text = "";
                    txtParentOcpClass.Text = "";
                    throw new Exception("ไม่พบข้อมูลชั้นอาชีพ");
                }
            }
            catch (Exception ex)
            {
                SetMsgException(ex);
            }
        }

        private void btnPayMentSave_Click(object sender, EventArgs e)
        {

            try
            {
                if (cmbSummryStatus.SelectedValue != null)
                {
                    var status = new string[] { "IF", "CC", "DC", "PP", "EX", "NT" };
                    if (status.Contains(cmbSummryStatus.SelectedValue.ToString()))
                    {
                        Decimal returnPremium = 0;
                        returnPremium = txtPayMentReturnPrem.Text == "" ? 0 : Convert.ToDecimal(txtPayMentReturnPrem.Text.Trim().Replace(",", ""));
                        String channelType = cmbChannelType.SelectedValue.ToString();
                        String WorkGroup = cmbWorkGroup.SelectedValue.ToString();

                        bool chkWaitPayAppr = false;
                        bool chkHaveAppr = false;
                        if (pPosID != null && pPosID.POS_ID != null)
                        {
                            NewBISSvcRef.ProcessResult prApproved = new NewBISSvcRef.ProcessResult();
                            using (NewBISSvcRef.NewBISSvcClient clientApproved = new NewBISSvcRef.NewBISSvcClient())
                            {
                                chkWaitPayAppr = clientApproved.GetAcWaitPayApprove(out prApproved, pPosID.POS_ID);
                                if (prApproved.Successed == false)
                                {
                                    throw new Exception(prApproved.ErrorMessage);
                                }
                            }


                            if (pPosID.POS_APPROVE_Collection != null && pPosID.POS_APPROVE_Collection.Count() > 0)
                            {
                                chkHaveAppr = true;
                            }
                        }

                        if (chkWaitPayAppr == true)
                        {
                            throw new Exception("ไม่สามารถบันทึกข้อมูลได้เนื่องจากข้อมูลมีการอนุมัติการจ่ายเงินเกินจากบัญชีแล้ว ต้องติดต่อทางบัญชีเพื่อทำการยกเลิกการจ่าย");
                        }

                        //if (chkHaveAppr == true && ckbPaymentTmn.Checked == false)
                        //{
                        //    if (PK_OPERATION == ckbPayMentOperation.Checked)
                        //    {
                        //        throw new Exception("ไม่สามารถบันทึกข้อมูลได้เนื่องจากข้อมูลมีการอนุมัติจากฝ่ายประกันชีวิตแล้ว จะบันทึกได้ต้องทำการยกเลิกข้อมูลก่อน");
                        //    }
                        //}
                        if (chkHaveAppr == true && ckbPaymentTmn.Checked == false)
                        {
                            throw new Exception("ไม่สามารถบันทึกข้อมูลได้เนื่องจากข้อมูลมีการอนุมัติจากฝ่ายประกันชีวิตแล้ว จะบันทึกได้ต้องทำการยกเลิกข้อมูลก่อน");
                        }

                        if (((channelType == "PO" || channelType == "PN") && returnPremium >= 100))
                        {

                            if (pPosID.POS_ID == null)
                            {

                                string certNo = "", policyHolding = "";

                                certNo = txtCertNo.Text;
                                policyHolding = txtPolicyHolding.Text;
                                pPosID.POS_ID = null;
                                pPosID.JOB_CODE = SystemConstant.JobCodePayOptionForNewBis;
                                pPosID.SUBJOB_CODE = WorkGroup == "BNC" ? SystemConstant.SubJobCodePayOptionForNewBisBANC : SystemConstant.SubJobCodePayOptionForNewBisOther;
                                pPosID.CHANNEL_TYPE = channelType;
                                pPosID.DEPART = WorkGroup;
                                pPosID.POLICY = certNo;
                                pPosID.CERT_NO = policyHolding;// == "" ? null : Convert.ToInt64(txtPolicy.Text).ToString();


                                Decimal? policyID = null;
                                if (txtCertNo.Text != "")
                                {
                                    NewBISSvcRef.ProcessResult pr = new NewBISSvcRef.ProcessResult();
                                    using (NewBISSvcRef.NewBISSvcClient client = new NewBISSvcRef.NewBISSvcClient())
                                    {
                                        policyID = client.GetPolicyIDByChannelType(out pr, channelType, certNo, policyHolding);
                                    }
                                }


                                pPosID.POLICY_ID = policyID == null ? (long?)null : Convert.ToInt64(policyID);
                                pPosID.APP_NO = txtAppNo.Text;
                                pPosID.TMN_FLG = ckbPaymentTmn.Checked == true ? 'Y' : 'N';
                                pPosID.UPD_ID = UserID;
                                pPosID.UPD_DT = DateTime.Now;
                            }
                            else
                            {
                                pPosID.UPD_ID = UserID;
                                pPosID.TMN_FLG = ckbPaymentTmn.Checked == true ? 'Y' : 'N';
                            }
                            pPosID.P_POS_SUBJOB = new NewBisPASvcRef.P_POS_SUBJOB()
                            {
                                POS_ID = pPosID.POS_ID,
                                SUBJOB_CODE = pPosID.SUBJOB_CODE,
                                TMN_FLG = 'N',
                                UPD_DT = pPosID.UPD_DT,
                                UPD_ID = pPosID.UPD_ID
                            };

                            pPosID.POS_PAYOPTION_Collection = new NewBisPASvcRef.P_POS_PAYOPTION_Collection();
                            Decimal allPay = 0;
                            dgvPayMent.EndEdit();
                            foreach (DataGridViewRow dr in dgvPayMent.Rows)
                            {
                                if (dr.IsNewRow == false)
                                {
                                    long? posPayOptionID = dr.Cells["payMentID"].Value == null ? (long?)null : Convert.ToInt64(dr.Cells["payMentID"].Value);
                                    String tmnType = dr.Cells["payMentTmn"].Value == null ? "N" : dr.Cells["payMentTmn"].Value.ToString();
                                    String optType = dr.Cells["payMentOpt"].Value == null ? "" : dr.Cells["payMentOpt"].Value.ToString();
                                    String bankCode = dr.Cells["payMentBankCode"].Value == null ? "" : dr.Cells["payMentBankCode"].Value.ToString();
                                    String branchCode = dr.Cells["payMentBranch"].Value == null ? "" : dr.Cells["payMentBranch"].Value.ToString();
                                    String acountNo = dr.Cells["payMentAccountNo"].Value == null ? "" : dr.Cells["payMentAccountNo"].Value.ToString();

                                    String payStr = dr.Cells["payMentPay"].Value == null ? "" : dr.Cells["payMentPay"].Value.ToString();
                                    Decimal pay = payStr == "" ? 0 : Convert.ToDecimal(payStr.Replace(",", ""));
                                    String payName = dr.Cells["payMentPayName"].Value == null ? "" : dr.Cells["payMentPayName"].Value.ToString();

                                    if (optType == "")
                                    {
                                        pPosID.POS_PAYOPTION_Collection = new NewBisPASvcRef.P_POS_PAYOPTION_Collection();
                                        throw new Exception("กรุณาระบุประเภทการจ่ายเงินที่ท่านต้องการ ในแถวที่ " + (dr.Index + 1).ToString());
                                    }

                                    if (pay == 0)
                                    {
                                        pPosID.POS_PAYOPTION_Collection = new NewBisPASvcRef.P_POS_PAYOPTION_Collection();
                                        throw new Exception("กรุณาระบุจำนวนเงิน ในแถวที่ " + (dr.Index + 1).ToString());
                                    }

                                    if (payName == "")
                                    {
                                        pPosID.POS_PAYOPTION_Collection = new NewBisPASvcRef.P_POS_PAYOPTION_Collection();
                                        throw new Exception("กรุณาระบุรายละเอียด ในแถวที่ " + (dr.Index + 1).ToString());
                                    }

                                    //if (payName != "" && payName.Length > 50)
                                    //{
                                    //    pPosID.POS_PAYOPTION_Collection = new NewBISSvcRef.P_POS_PAYOPTION_Collection();
                                    //    throw new Exception("รายละเอียดมีขนาดมากกว่า 50 ตัวอักษร ในหน้าบัญชี Smart ในส่วนอนุมัติเงินเกิน ในแถวที่ " + (dr.Index + 1).ToString());
                                    //}

                                    if (optType == "TF" || optType == "CQ" || optType == "DF")
                                    {
                                        if (bankCode == "")
                                        {
                                            pPosID.POS_PAYOPTION_Collection = new NewBisPASvcRef.P_POS_PAYOPTION_Collection();
                                            throw new Exception("กรุณาระบุรหัสธนาคาร  ในแถวที่ " + (dr.Index + 1).ToString());
                                        }

                                        if (branchCode == "")
                                        {
                                            pPosID.POS_PAYOPTION_Collection = new NewBisPASvcRef.P_POS_PAYOPTION_Collection();
                                            throw new Exception("กรุณาระบุรหัสสาขาธนาคาร  ในแถวที่ " + (dr.Index + 1).ToString());
                                        }

                                        if (branchCode != "" && branchCode.Length < 4)
                                        {
                                            pPosID.POS_PAYOPTION_Collection = new NewBisPASvcRef.P_POS_PAYOPTION_Collection();
                                            throw new Exception("กรุณาระบุรหัสสาขาธนาคารให้มี 4 ตัวอักษร  ในแถวที่ " + (dr.Index + 1).ToString());
                                        }
                                    }

                                    if (optType == "TF" || optType == "CC")
                                    {
                                        if (acountNo == "")
                                        {
                                            pPosID.POS_PAYOPTION_Collection = new NewBisPASvcRef.P_POS_PAYOPTION_Collection();
                                            throw new Exception("กรุณาระบุเลขที่บัญชี หรือ เลขที่บัตรเครดิต ในแถวที่ " + (dr.Index + 1).ToString());
                                        }
                                    }

                                    if (acountNo != "" && acountNo.Length < 10)
                                    {
                                        pPosID.POS_PAYOPTION_Collection = new NewBisPASvcRef.P_POS_PAYOPTION_Collection();
                                        throw new Exception("เลขที่บัญชีมีค่าน้อยกว่า 10 ตัวอักษร ในแถวที่ " + (dr.Index + 1).ToString());
                                    }

                                    var pPosPayOptionObj = new NewBisPASvcRef.P_POS_PAYOPTION();
                                    pPosPayOptionObj.POS_ID = pPosID.POS_ID;
                                    pPosPayOptionObj.POS_PAYOPTION_ID = posPayOptionID;
                                    pPosPayOptionObj.OPT = optType;
                                    pPosPayOptionObj.BANK = bankCode;
                                    pPosPayOptionObj.BRANCH = branchCode;
                                    pPosPayOptionObj.ACCOUNT_NO = acountNo;
                                    pPosPayOptionObj.PAY = pay;
                                    pPosPayOptionObj.PAY_NAME = payName;
                                    pPosPayOptionObj.PAY_DT = null;
                                    pPosPayOptionObj.UPD_ID = UserID;
                                    pPosPayOptionObj.UPD_DT = DateTime.Now;
                                    pPosPayOptionObj.TMN_FLG = Convert.ToChar(tmnType);

                                    if (tmnType == "N")
                                    {
                                        allPay = allPay + pay;
                                    }

                                    pPosPayOptionObj.POS_PAYOPERATION = new NewBisPASvcRef.P_POS_PAYOPERATION();
                                    pPosPayOptionObj.POS_PAYOPERATION.POS_PAYOPTION_ID = pPosPayOptionObj.POS_PAYOPTION_ID;
                                    if (ckbPayMentOperation.Checked == true)
                                    {
                                        pPosPayOptionObj.POS_PAYOPERATION.OPERATION = 'Y';
                                    }
                                    else
                                    {
                                        pPosPayOptionObj.POS_PAYOPERATION.OPERATION = 'N';
                                    }

                                    pPosID.POS_PAYOPTION_Collection.Add(pPosPayOptionObj);

                                }
                            }

                            if (returnPremium != allPay)
                            {
                                pPosID.POS_PAYOPTION_Collection = new NewBisPASvcRef.P_POS_PAYOPTION_Collection();
                                throw new Exception("ยอดรวมเงินจ่ายในรายการไม่เท่ากับจำนวนเงินเกิน ยอดเงินในรายการ : " + allPay.ToString("n2") + "  จำนวนเงินเกิน : " + returnPremium.ToString("n2"));
                            }

                            var prSave = new NewBisPASvcRef.ProcessResult();
                            NewBisPASvcRef.W_SUMMARY[] wSummaryArr;
                            using (var clientSave = new NewBisPASvcRef.NewBisPASvcClient())
                            {
                                if (cmbSummryStatus.SelectedValue.ToString() == "IF")
                                {
                                    prSave = clientSave.GetWSummaryForPayment(out wSummaryArr, cmbSummryStatus.SelectedValue.ToString(), PAR_U_APPLICATION_NAME.UAPP_ID);
                                    if (prSave.Successed == false)
                                    {
                                        throw new Exception(prSave.ErrorMessage);
                                    }

                                    if (wSummaryArr != null && wSummaryArr.Count() > 0)
                                    {
                                        NewBisPASvcRef.W_SUMMARY wSummaryObj = wSummaryArr.ToArray()[0];

                                        long[] summaryIDs = { wSummaryObj.SUMMARY_ID.Value };
                                        NewBisPASvcRef.W_SUMMARY_LETTER[] wSummaryLetterArr;
                                        prSave = clientSave.GetCompositeDataW_SUMMARY_LETTER(out wSummaryLetterArr, summaryIDs, null);

                                        if (!(wSummaryLetterArr != null && wSummaryLetterArr.Count() > 0))
                                        {
                                            var uLetterIDObj = new NewBisPASvcRef.U_LETTER_ID();
                                            uLetterIDObj.ULETTER_ID = null;
                                            uLetterIDObj.UAPP_ID = PAR_U_APPLICATION_NAME.UAPP_ID;
                                            uLetterIDObj.STATUS = wSummaryObj.STATUS;
                                            uLetterIDObj.SUBSTATUS = wSummaryObj.SUBSTATUS;
                                            uLetterIDObj.STATUS_CAUSE = null;
                                            uLetterIDObj.REFERENCE = null;
                                            uLetterIDObj.LETTER_TYPE = 'I';
                                            uLetterIDObj.LETTER_CODE = null;
                                            uLetterIDObj.SIGNATURE_ID = null;
                                            uLetterIDObj.TMN = 'N';
                                            uLetterIDObj.FSYSTEM_DT = DateTime.Now;
                                            uLetterIDObj.UPD_ID = wSummaryObj.UPD_ID;
                                            uLetterIDObj.PRINT_FLG = 'N';
                                            uLetterIDObj.LETTER_DT = DateTime.Now;

                                            uLetterIDObj.RETURN_PRM = new NewBisPASvcRef.U_RETURN_PRM();
                                            uLetterIDObj.RETURN_PRM.ULETTER_ID = uLetterIDObj.ULETTER_ID;
                                            uLetterIDObj.RETURN_PRM.PAY_OPTION = null;
                                            uLetterIDObj.RETURN_PRM.RETURN_PREMIUM = returnPremium;
                                            uLetterIDObj.RETURN_PRM.FEE = null;
                                            uLetterIDObj.RETURN_PRM.APPROVE_ID = wSummaryObj.UPD_ID;
                                            uLetterIDObj.RETURN_PRM.APPROVE_DT = uLetterIDObj.FSYSTEM_DT;
                                            uLetterIDObj.RETURN_PRM.UPD_ID = UserID;

                                            var wSumLetter = new NewBisPASvcRef.W_SUMMARY_LETTER();
                                            wSumLetter.SUMMARY_ID = wSummaryObj.SUMMARY_ID;
                                            wSumLetter.ULETTER_ID = uLetterIDObj.ULETTER_ID;
                                            uLetterIDObj.SUMMARY_LETTER_Collection = new NewBisPASvcRef.W_SUMMARY_LETTER_Collection();
                                            uLetterIDObj.SUMMARY_LETTER_Collection.Add(wSumLetter);

                                            prSave = clientSave.EditU_LETTER_ID(ref uLetterIDObj);

                                            if (prSave.Successed == false)
                                            {
                                                throw new Exception(prSave.ErrorMessage);
                                            }

                                        }
                                    }
                                }

                                prSave = clientSave.EditPPosID(ref pPosID);
                                if (prSave.Successed == false)
                                {
                                    throw new Exception(prSave.ErrorMessage);
                                }

                                pPosID = new NewBisPASvcRef.P_POS_ID();

                                prSave = clientSave.GetPPosID(out pPosID, txtAppNo.Text, channelType);
                                if (prSave.Successed == false)
                                {
                                    throw new Exception(prSave.ErrorMessage);
                                }
                            }

                            ckbPayMentOperation.Checked = false;
                            bool chkOperation = false;
                            if (pPosID != null && pPosID.POS_ID != null)
                            {
                                if (pPosID.POS_PAYOPTION_Collection != null && pPosID.POS_PAYOPTION_Collection.Count() > 0)
                                {
                                    dgvPayMent.Rows.Clear();
                                    int i = 0;
                                    foreach (NewBisPASvcRef.P_POS_PAYOPTION obj in pPosID.POS_PAYOPTION_Collection)
                                    {
                                        DataGridViewRow dr = (DataGridViewRow)dgvPayMent.Rows[i].Clone();
                                        dr.Cells[1].Value = obj.OPT;
                                        dr.Cells[2].Value = obj.BANK;
                                        dr.Cells[3].Value = obj.BRANCH;
                                        dr.Cells[4].Value = obj.ACCOUNT_NO;
                                        dr.Cells[5].Value = obj.PAY_NAME;
                                        dr.Cells[6].Value = obj.PAY;
                                        dr.Cells[7].Value = obj.TMN_FLG == null ? "N" : obj.TMN_FLG.Value.ToString();
                                        dr.Cells[9].Value = obj.PAY_DT == null ? "" : Utility.dateTimeToString(obj.PAY_DT.Value, "dd/MM/yyyy", "BU");
                                        dr.Cells[10].Value = obj.UPD_ID;
                                        dr.Cells[11].Value = obj.UPD_DT == null ? "" : Utility.dateTimeToString(obj.UPD_DT.Value, "dd/MM/yyyy", "BU");
                                        dr.Cells[12].Value = obj.POS_PAYOPTION_ID;
                                        dgvPayMent.Rows.Add(dr);
                                        i = i + 1;

                                        if (obj.POS_PAYOPERATION != null && obj.POS_PAYOPERATION.POS_PAYOPTION_ID != null)
                                        {
                                            if (obj.POS_PAYOPERATION.OPERATION == 'Y')
                                            {
                                                chkOperation = true;
                                            }
                                        }
                                    }

                                    ckbPayMentOperation.Checked = chkOperation;
                                }
                            }
                            else
                            {
                                dgvPayMent.Rows.Clear();
                            }

                            if (!chkOperation)
                            {
                                btnPayMentSave.Enabled = true;
                            }
                            else
                            {
                                btnPayMentSave.Enabled = false;
                            }
                            ckbPaymentTmn.Checked = false;
                            MessageBox.Show("บันทึกข้อมูลเรียบร้ยแล้ว");

                        }
                        else
                        {
                            pPosID.POS_PAYOPTION_Collection = new NewBisPASvcRef.P_POS_PAYOPTION_Collection();
                            throw new Exception("เงินเกินไม่ถึงสำหรับการทำเงินคืน");
                        }
                    }
                    else
                    {
                        pPosID.POS_PAYOPTION_Collection = new NewBisPASvcRef.P_POS_PAYOPTION_Collection();
                        throw new Exception("ไม่สามารถใช้งานได้ต้องเป็นสถานะ IF,CC,DC,PP,EX,NT");
                    }
                }


            }
            catch (Exception ex)
            {
                SetMsgException(ex);
            }
        }


        private bool GenPaymentOfReturnPremium(out String msgError)
        {
            msgError = "";
            bool chkError = false;
            int chkData = 0;
            foreach (DataGridViewRow dr in dgvPayMent.Rows)
            {

                if (dr.IsNewRow == false)
                {
                    string tmnType = dr.Cells["payMentTmn"].Value == null ? "N" : dr.Cells["payMentTmn"].Value.ToString();
                    if (tmnType == "N")
                    {
                        chkData++;
                    }
                }
            }

            if (chkData == 0)
            {
                if (cmbSummryStatus.SelectedValue != null)
                {
                    var status = new string[] { "IF", "CC", "DC", "PP", "EX", "NT" };
                    if (status.Contains(cmbSummryStatus.SelectedValue.ToString()))
                    //  if ((cmbSummryStatus.SelectedValue.ToString() == "IF" || cmbStatus.SelectedValue.ToString() == "CC" || cmbStatus.SelectedValue.ToString() == "DC" || cmbStatus.SelectedValue.ToString() == "PP" || cmbStatus.SelectedValue.ToString() == "EX" || cmbStatus.SelectedValue.ToString() == "NT") || (cmbSStatus.SelectedValue.ToString() == "IF" || cmbSStatus.SelectedValue.ToString() == "CC" || cmbSStatus.SelectedValue.ToString() == "DC" || cmbSStatus.SelectedValue.ToString() == "PP" || cmbSStatus.SelectedValue.ToString() == "EX" || cmbSStatus.SelectedValue.ToString() == "NT"))
                    {
                        Decimal returnPremium = 0;
                        returnPremium = txtPayMentReturnPrem.Text == "" ? 0 : Convert.ToDecimal(txtPayMentReturnPrem.Text.Trim().Replace(",", ""));
                        String accountNo = "";
                        String bankCode = "";
                        String branchCode = "";
                        String accountName = "";
                        String channelType = cmbChannelType.SelectedValue.ToString();
                        if ((channelType == "PO" || channelType == "PN") && returnPremium >= 100)
                        {
                            NewBISSvcRef.CS_SUSPENSE_COLLECTION suspenseReturnColl = new NewBISSvcRef.CS_SUSPENSE_COLLECTION();
                            NewBISSvcRef.ProcessResult pr = new NewBISSvcRef.ProcessResult();
                            using (NewBISSvcRef.NewBISSvcClient client = new NewBISSvcRef.NewBISSvcClient())
                            {
                                suspenseReturnColl = client.GetSuspenseOfReturn(out pr, txtAppNo.Text);
                                if (pr.Successed == false)
                                {
                                    throw new Exception(pr.ErrorMessage);
                                }

                                if (!(string.IsNullOrEmpty(txtTmpAccountNo.Text)))
                                {
                                    accountNo = txtTmpAccountNo.Text.Trim();
                                    bankCode = txtTmpBankCode.Text.Trim();
                                    branchCode = txtTmpBranchCode.Text.Trim();
                                    accountName = txtTmpAccountName.Text.Trim();
                                }
                                else
                                {
                                    if (txtCertNo.Text == "")
                                    {
                                        if (chbSmartIDTmn.Checked == false)
                                        {
                                            accountNo = txtSmartAccNo.Text.Trim();
                                            bankCode = txtSmartBankCode.Text.Trim();
                                            branchCode = txtSmartBranchCode.Text.Trim();
                                            accountName = txtSmartAccName.Text.Trim();
                                        }
                                    }
                                    else
                                    {

                                    }
                                }
                            }



                            if (suspenseReturnColl != null && suspenseReturnColl.Count() > 0)
                            {
                                String payNameCus = "";
                                if (String.IsNullOrEmpty(txtParentName.Text))
                                {
                                    payNameCus = txtPrename.Text + txtName.Text + "  " + txtSurname.Text;
                                }
                                else
                                {
                                    payNameCus = txtParentPrename.Text + txtParentName.Text + " " + txtParentSurname.Text;
                                }



                                NewBISSvcRef.P_POS_PAYOPTION_Collection payOptionColl = new NewBISSvcRef.P_POS_PAYOPTION_Collection();
                                foreach (NewBISSvcRef.CS_SUSPENSE suspenseObj in suspenseReturnColl)
                                {
                                    Decimal calPremium = suspenseObj.CSP_CALPREM == null ? 0 : suspenseObj.CSP_CALPREM.Value;
                                    NewBISSvcRef.P_POS_PAYOPTION payOptionObj = new NewBISSvcRef.P_POS_PAYOPTION();

                                    //if (cmbSStatusCause.SelectedValue != null && cmbSStatusCause.SelectedValue.ToString() == "DC006")
                                    //{
                                    //    payOptionObj.OPT = "BR";
                                    //    payOptionObj.BANK = "";
                                    //    payOptionObj.BRANCH = "";
                                    //    payOptionObj.ACCOUNT_NO = "";
                                    //    payOptionObj.PAY = returnPremium;
                                    //    payOptionObj.PAY_NAME = payNameCus;
                                    //    payOptionObj.TMN_FLG = 'N';
                                    //    returnPremium = 0;
                                    //    payOptionColl.Add(payOptionObj);
                                    //    break;
                                    //}

                                    //if (PAR_REMARK_FLAG == "N")
                                    //{
                                    //    payOptionObj.OPT = "BR";
                                    //    payOptionObj.BANK = "";
                                    //    payOptionObj.BRANCH = "";
                                    //    payOptionObj.ACCOUNT_NO = "";
                                    //    payOptionObj.PAY = returnPremium;
                                    //    payOptionObj.PAY_NAME = payNameCus;
                                    //    payOptionObj.TMN_FLG = 'N';
                                    //    returnPremium = 0;
                                    //    payOptionColl.Add(payOptionObj);
                                    //    break;
                                    //}

                                    if (suspenseObj.CSP_OPTION == "08" || suspenseObj.CSP_OPTION == "RE" || suspenseObj.CSP_OPTION == "EX")
                                    {
                                        if (returnPremium >= calPremium)
                                        {
                                            payOptionObj.OPT = "CC";
                                            payOptionObj.BANK = null;
                                            payOptionObj.BRANCH = null;
                                            payOptionObj.ACCOUNT_NO = suspenseObj.CSP_CARDNO;
                                            payOptionObj.PAY = calPremium;
                                            payOptionObj.PAY_NAME = payNameCus;
                                            payOptionObj.TMN_FLG = 'N';
                                            returnPremium = returnPremium - calPremium;
                                            payOptionColl.Add(payOptionObj);
                                        }
                                        else if (calPremium > returnPremium)
                                        {
                                            payOptionObj.OPT = "CC";
                                            payOptionObj.BANK = null;
                                            payOptionObj.BRANCH = null;
                                            payOptionObj.ACCOUNT_NO = suspenseObj.CSP_CARDNO;
                                            payOptionObj.PAY = returnPremium;
                                            payOptionObj.PAY_NAME = payNameCus;
                                            payOptionObj.TMN_FLG = 'N';
                                            returnPremium = 0;
                                            payOptionColl.Add(payOptionObj);
                                            break;
                                        }
                                    }
                                    else if (returnPremium > 0)
                                    {


                                        if (channelType == "PN" || channelType == "PO")
                                        {
                                            if (accountNo != null && accountNo != "")
                                            {
                                                payOptionObj.OPT = "TF";
                                                payOptionObj.BANK = bankCode;
                                                payOptionObj.BRANCH = branchCode;
                                                payOptionObj.ACCOUNT_NO = accountNo;
                                                payOptionObj.PAY = returnPremium;
                                                payOptionObj.PAY_NAME = accountName;
                                                payOptionObj.TMN_FLG = 'N';
                                                returnPremium = 0;
                                                payOptionColl.Add(payOptionObj);
                                                break;
                                            }
                                            else if (txtZipcode.Text.Substring(0, 2) == "10" || txtZipcode.Text.Substring(0, 2) == "11" || txtZipcode.Text.Substring(0, 2) == "12")
                                            {
                                                payOptionObj.OPT = "DF";
                                                payOptionObj.BANK = "002";
                                                payOptionObj.BRANCH = "0118";
                                                payOptionObj.ACCOUNT_NO = null;
                                                payOptionObj.PAY = returnPremium;
                                                payOptionObj.PAY_NAME = payNameCus;
                                                payOptionObj.TMN_FLG = 'N';
                                                returnPremium = 0;
                                                payOptionColl.Add(payOptionObj);
                                                break;
                                            }
                                            else
                                            {
                                                NewBISSvcRef.POSTCODE_BANK postBank = new NewBISSvcRef.POSTCODE_BANK();
                                                using (NewBISSvcRef.NewBISSvcClient client = new NewBISSvcRef.NewBISSvcClient())
                                                {
                                                    postBank = client.GetPayBankByPost(out pr, txtZipcode.Text);
                                                    if (postBank != null)
                                                    {
                                                        payOptionObj.BANK = postBank.PCBK_BANK;
                                                        payOptionObj.BRANCH = postBank.PCBK_BRANCH;
                                                        payOptionObj.OPT = postBank.PCBK_OPTION == "CQ" ? "DF" : postBank.PCBK_OPTION;
                                                    }
                                                    else
                                                    {
                                                        string subZip_Code = "";
                                                        subZip_Code = txtZipcode.Text.Substring(0, 2).PadRight(5, '0');
                                                        postBank = client.GetPayBankByPost(out pr, subZip_Code);
                                                        if (postBank != null)
                                                        {
                                                            payOptionObj.BANK = postBank.PCBK_BANK;
                                                            payOptionObj.BRANCH = postBank.PCBK_BRANCH;
                                                            payOptionObj.OPT = postBank.PCBK_OPTION == "CQ" ? "DF" : postBank.PCBK_OPTION;
                                                        }
                                                    }
                                                }


                                                //payOptionObj.OPT = "DF";
                                                payOptionObj.ACCOUNT_NO = null;
                                                payOptionObj.PAY = returnPremium;
                                                payOptionObj.PAY_NAME = payNameCus;
                                                payOptionObj.TMN_FLG = 'N';
                                                returnPremium = 0;
                                                payOptionColl.Add(payOptionObj);
                                                break;
                                            }
                                        }
                                    }
                                }

                                if (payOptionColl != null && payOptionColl.Count() > 0)
                                {
                                    int i = 0;
                                    foreach (NewBISSvcRef.P_POS_PAYOPTION obj in payOptionColl)
                                    {
                                        DataGridViewRow dr = (DataGridViewRow)dgvPayMent.Rows[i].Clone();
                                        dr.Cells[1].Value = obj.OPT;
                                        dr.Cells[2].Value = obj.BANK;
                                        dr.Cells[3].Value = obj.BRANCH;
                                        dr.Cells[4].Value = obj.ACCOUNT_NO;
                                        dr.Cells[5].Value = obj.PAY_NAME;
                                        dr.Cells[6].Value = obj.PAY;
                                        dr.Cells[7].Value = "N";
                                        dr.Cells[8].Value = obj.TMN_FLG == null ? "N" : obj.TMN_FLG.Value.ToString();
                                        dgvPayMent.Rows.Add(dr);
                                        i = i + 1;
                                    }
                                }
                            }
                        }
                        else
                        {
                            msgError = "เงินเกินไม่ถึงสำหรับการทำเงินคืน";
                            chkError = true;
                        }
                    }
                    else
                    {
                        msgError = "ไม่สามารถใช้งานได้ต้องเป็นสถานะ IF,CC,DC,PP,EX,NT";
                        chkError = true;
                    }
                }
            }
            else
            {
                msgError = "มีข้อมูลในตารางการจ่ายเงินเกินแล้วไม่สามารถคำนวนการจ่ายเงินเกินอัตโนมัติได้";
                chkError = true;
            }
            return chkError;
        }


        private void PaymentReturnPremium()
        {
            if (cmbSummryStatus.SelectedValue != null)
            {
                if (PAR_END_PROCESS != 'Y')
                {
                    var status = new string[] { "IF", "CC", "DC", "PP", "EX", "NT" };
                    if (status.Contains(cmbSummryStatus.SelectedValue.ToString()))
                    //    if ((cmbSummryStatus.SelectedValue.ToString() == "IF" || cmbStatus.SelectedValue.ToString() == "CC" || cmbStatus.SelectedValue.ToString() == "DC" || cmbStatus.SelectedValue.ToString() == "PP" || cmbStatus.SelectedValue.ToString() == "EX" || cmbStatus.SelectedValue.ToString() == "NT") || (cmbSStatus.SelectedValue.ToString() == "IF" || cmbSStatus.SelectedValue.ToString() == "CC" || cmbSStatus.SelectedValue.ToString() == "DC" || cmbSStatus.SelectedValue.ToString() == "PP" || cmbSStatus.SelectedValue.ToString() == "EX" || cmbSStatus.SelectedValue.ToString() == "NT"))
                    {
                        String channelType = cmbChannelType.SelectedValue.ToString();
                        String WorkGroup = cmbWorkGroup.SelectedValue.ToString();
                        Decimal returnPremium = 0;
                        returnPremium = txtPayMentReturnPrem.Text == "" ? 0 : Convert.ToDecimal(txtPayMentReturnPrem.Text.Trim().Replace(",", ""));
                        if ((channelType == "PO" || channelType == "PN") && returnPremium >= 100)
                        {
                            if (pPosID.POS_ID == null)
                            {
                                pPosID.POS_ID = null;
                                pPosID.JOB_CODE = SystemConstant.JobCodePayOptionForNewBis;
                                pPosID.SUBJOB_CODE = WorkGroup == "BNC" ? SystemConstant.SubJobCodePayOptionForNewBisBANC : SystemConstant.SubJobCodePayOptionForNewBisOther;
                                pPosID.SUBJOB_CODE = null;
                                pPosID.CHANNEL_TYPE = channelType;
                                pPosID.DEPART = WorkGroup;
                                pPosID.POLICY = txtCertNo.Text;
                                //  pPosID.CERT_NO = txtPolicy.Text == "" ? null : Convert.ToInt64(txtPolicy.Text).ToString();


                                Decimal? policyID = null;
                                if (txtCertNo.Text != "")
                                {
                                    NewBISSvcRef.ProcessResult pr = new NewBISSvcRef.ProcessResult();
                                    using (NewBISSvcRef.NewBISSvcClient client = new NewBISSvcRef.NewBISSvcClient())
                                    {
                                        policyID = client.GetPolicyIDByChannelType(out pr, channelType, txtCertNo.Text, txtPolicyHolding.Text);
                                        if (pr.Successed == false)
                                        {
                                            throw new Exception(pr.ErrorMessage);
                                        }
                                    }
                                }

                                pPosID.POLICY_ID = policyID == null ? (long?)null : Convert.ToInt64(policyID);
                                pPosID.APP_NO = txtAppNo.Text;
                                pPosID.TMN_FLG = ckbPaymentTmn.Checked == true ? 'Y' : 'N';
                                pPosID.UPD_ID = UserID;
                                pPosID.UPD_DT = DateTime.Now;
                            }
                            else
                            {
                                pPosID.UPD_ID = UserID;
                                pPosID.TMN_FLG = ckbPaymentTmn.Checked == true ? 'Y' : 'N';
                            }
                            pPosID.P_POS_SUBJOB = new NewBisPASvcRef.P_POS_SUBJOB()
                            {
                                POS_ID = pPosID.POS_ID,
                                SUBJOB_CODE = pPosID.SUBJOB_CODE,
                                TMN_FLG = 'N',
                                UPD_DT = pPosID.UPD_DT,
                                UPD_ID = pPosID.UPD_ID
                            };
                            pPosID.POS_PAYOPTION_Collection = new NewBisPASvcRef.P_POS_PAYOPTION_Collection();
                            Decimal allPay = 0;
                            dgvPayMent.EndEdit();
                            foreach (DataGridViewRow dr in dgvPayMent.Rows)
                            {
                                if (dr.IsNewRow == false)
                                {
                                    long? posPayOptionID = dr.Cells["payMentID"].Value == null ? (long?)null : Convert.ToInt64(dr.Cells["payMentID"].Value);
                                    String tmnType = dr.Cells["payMentTmn"].Value == null ? "N" : dr.Cells["payMentTmn"].Value.ToString();
                                    String optType = dr.Cells["payMentOpt"].Value == null ? "" : dr.Cells["payMentOpt"].Value.ToString();
                                    String bankCode = dr.Cells["payMentBankCode"].Value == null ? "" : dr.Cells["payMentBankCode"].Value.ToString();
                                    String branchCode = dr.Cells["payMentBranch"].Value == null ? "" : dr.Cells["payMentBranch"].Value.ToString();
                                    String acountNo = dr.Cells["payMentAccountNo"].Value == null ? "" : dr.Cells["payMentAccountNo"].Value.ToString();

                                    String payStr = dr.Cells["payMentPay"].Value == null ? "" : dr.Cells["payMentPay"].Value.ToString();
                                    Decimal pay = payStr == "" ? 0 : Convert.ToDecimal(payStr.Replace(",", ""));
                                    String payName = dr.Cells["payMentPayName"].Value == null ? "" : dr.Cells["payMentPayName"].Value.ToString();

                                    if (optType == "")
                                    {
                                        pPosID.POS_PAYOPTION_Collection = new NewBisPASvcRef.P_POS_PAYOPTION_Collection();
                                        throw new Exception("กรุณาระบุประเภทการจ่ายเงินที่ท่านต้องการ ในหน้าบัญชี Smart ในส่วนอนุมัติเงินเกิน ในแถวที่ " + (dr.Index + 1).ToString());
                                    }

                                    if (pay == 0)
                                    {
                                        pPosID.POS_PAYOPTION_Collection = new NewBisPASvcRef.P_POS_PAYOPTION_Collection();
                                        throw new Exception("กรุณาระบุจำนวนเงิน ในหน้าบัญชี Smart ในส่วนอนุมัติเงินเกิน ในแถวที่ " + (dr.Index + 1).ToString());
                                    }

                                    if (payName == "")
                                    {
                                        pPosID.POS_PAYOPTION_Collection = new NewBisPASvcRef.P_POS_PAYOPTION_Collection();
                                        throw new Exception("กรุณาระบุรายละเอียด ในหน้าบัญชี Smart ในส่วนอนุมัติเงินเกิน ในแถวที่ " + (dr.Index + 1).ToString());
                                    }

                                    //if (payName != "" && payName.Length > 50)
                                    //{
                                    //    pPosID.POS_PAYOPTION_Collection = new NewBISSvcRef.P_POS_PAYOPTION_Collection();
                                    //    throw new Exception("รายละเอียดมีขนาดมากกว่า 50 ตัวอักษร ในหน้าบัญชี Smart ในส่วนอนุมัติเงินเกิน ในแถวที่ " + (dr.Index + 1).ToString());
                                    //}

                                    if (optType == "TF" || optType == "CQ" || optType == "DF")
                                    {
                                        if (bankCode == "")
                                        {
                                            pPosID.POS_PAYOPTION_Collection = new NewBisPASvcRef.P_POS_PAYOPTION_Collection();
                                            throw new Exception("กรุณาระบุรหัสธนาคาร ในหน้าบัญชี Smart ในส่วนอนุมัติเงินเกิน ในแถวที่ " + (dr.Index + 1).ToString());
                                        }

                                        if (branchCode == "")
                                        {
                                            pPosID.POS_PAYOPTION_Collection = new NewBisPASvcRef.P_POS_PAYOPTION_Collection();
                                            throw new Exception("กรุณาระบุรหัสสาขาธนาคาร ในหน้าบัญชี Smart ในส่วนอนุมัติเงินเกิน ในแถวที่ " + (dr.Index + 1).ToString());
                                        }

                                        if (branchCode != "" && branchCode.Length < 4)
                                        {
                                            pPosID.POS_PAYOPTION_Collection = new NewBisPASvcRef.P_POS_PAYOPTION_Collection();
                                            throw new Exception("กรุณาระบุรหัสสาขาธนาคารต้องมี 4 ตัวอักษร ในหน้าบัญชี Smart ในส่วนอนุมัติเงินเกิน ในแถวที่ " + (dr.Index + 1).ToString());
                                        }
                                    }

                                    if (optType == "TF" || optType == "CC")
                                    {
                                        if (acountNo == "")
                                        {
                                            pPosID.POS_PAYOPTION_Collection = new NewBisPASvcRef.P_POS_PAYOPTION_Collection();
                                            throw new Exception("กรุณาระบุเลขที่บัญชี หรือ เลขที่บัตรเครดิต ในหน้าบัญชี Smart ในส่วนอนุมัติเงินเกิน ในแถวที่ " + (dr.Index + 1).ToString());
                                        }
                                    }

                                    if (acountNo != "" && acountNo.Length < 10)
                                    {
                                        pPosID.POS_PAYOPTION_Collection = new NewBisPASvcRef.P_POS_PAYOPTION_Collection();
                                        throw new Exception("เลขที่บัญชีมีค่าน้อยกว่า 10 ตัวอักษร ในหน้าบัญชี Smart ในส่วนอนุมัติเงินเกิน ในแถวที่ " + (dr.Index + 1).ToString());
                                    }

                                    var pPosPayOptionObj = new NewBisPASvcRef.P_POS_PAYOPTION();
                                    pPosPayOptionObj.POS_ID = pPosID.POS_ID;
                                    pPosPayOptionObj.POS_PAYOPTION_ID = posPayOptionID;
                                    pPosPayOptionObj.OPT = optType;
                                    pPosPayOptionObj.BANK = bankCode;
                                    pPosPayOptionObj.BRANCH = branchCode;
                                    pPosPayOptionObj.ACCOUNT_NO = acountNo;
                                    pPosPayOptionObj.PAY = pay;
                                    pPosPayOptionObj.PAY_NAME = payName;
                                    pPosPayOptionObj.PAY_DT = DateTime.Now;
                                    pPosPayOptionObj.UPD_ID = UserID;
                                    pPosPayOptionObj.UPD_DT = DateTime.Now;
                                    pPosPayOptionObj.TMN_FLG = Convert.ToChar(tmnType);

                                    pPosPayOptionObj.POS_PAYOPERATION = new NewBisPASvcRef.P_POS_PAYOPERATION();
                                    pPosPayOptionObj.POS_PAYOPERATION.POS_PAYOPTION_ID = pPosPayOptionObj.POS_PAYOPTION_ID;
                                    if (ckbPayMentOperation.Checked == true)
                                    {
                                        pPosPayOptionObj.POS_PAYOPERATION.OPERATION = 'Y';
                                    }
                                    else
                                    {
                                        pPosPayOptionObj.POS_PAYOPERATION.OPERATION = 'N';
                                    }

                                    if (tmnType == "N")
                                    {
                                        allPay = allPay + pay;
                                    }

                                    pPosID.POS_PAYOPTION_Collection.Add(pPosPayOptionObj);

                                }
                            }

                            if (returnPremium != allPay)
                            {
                                pPosID.POS_PAYOPTION_Collection = new NewBisPASvcRef.P_POS_PAYOPTION_Collection();
                                throw new Exception("ยอดรวมเงินจ่ายในรายการไม่เท่ากับจำนวนเงินเกิน ยอดเงินในรายการ : " + allPay.ToString("n2") + "  จำนวนเงินเกิน : " + returnPremium.ToString("n2"));
                            }
                        }
                        else
                        {
                            pPosID.POS_PAYOPTION_Collection = new NewBisPASvcRef.P_POS_PAYOPTION_Collection();
                        }
                    }
                    else
                    {
                        pPosID.POS_PAYOPTION_Collection = new NewBisPASvcRef.P_POS_PAYOPTION_Collection();
                    }
                }

                else
                {
                    pPosID.POS_PAYOPTION_Collection = new NewBisPASvcRef.P_POS_PAYOPTION_Collection();
                }
            }
            else
            {
                pPosID.POS_PAYOPTION_Collection = new NewBisPASvcRef.P_POS_PAYOPTION_Collection();
            }

        }

        private void dgvPayMent_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            try
            {
                if (e.Control is DataGridViewTextBoxEditingControl)
                {
                    if (dgvPayMent.CurrentCell.ColumnIndex == dgvPayMent.CurrentRow.Cells["payMentPayName"].ColumnIndex)
                    {
                        AutoCompleteStringCollection data = new AutoCompleteStringCollection();
                        foreach (NewBisPASvcRef.AUTB_DATADIC_DET item in PaymentDescColl)
                        {
                            data.Add(item.DESCRIPTION);
                        }

                        ((TextBox)e.Control).AutoCompleteMode = AutoCompleteMode.Suggest;
                        ((TextBox)e.Control).AutoCompleteSource = AutoCompleteSource.CustomSource;
                        ((TextBox)e.Control).AutoCompleteCustomSource = data;
                    }
                    else
                    {
                        ((TextBox)e.Control).AutoCompleteCustomSource = null;
                    }
                }
            }
            catch (Exception ex)
            {
                SetMsgException(ex);
            }
        }

        private void dgvPayMent_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                String payOptionID = dgvPayMent.Rows[e.RowIndex].Cells["payMentID"].Value == null ? "" : dgvPayMent.Rows[e.RowIndex].Cells["payMentID"].Value.ToString();
                if (dgvPayMent.Rows[e.RowIndex].Cells["payMentTmn"].ColumnIndex == e.ColumnIndex)
                {
                    dgvPayMent.EndEdit();
                    DataGridViewCheckBoxCell Check = (DataGridViewCheckBoxCell)dgvPayMent.Rows[e.RowIndex].Cells["payMentTmn"];
                    if (payOptionID == "")
                    {
                        dgvPayMent.Rows[e.RowIndex].Cells["payMentTmn"].Value = "N";
                        Check.ReadOnly = true;
                        MessageBox.Show("ไม่สามารถเลือกติกข้อมูลได้ให้ใช่ปุ่มยกเลิกแทนเพื่อลบข้อมูล");
                        return;
                    }
                    else
                    {
                        Check.ReadOnly = false;
                    }
                }
                else if (dgvPayMent.Rows[e.RowIndex].Cells["payMentCancel"].ColumnIndex == e.ColumnIndex)
                {
                    dgvPayMent.EndEdit();
                    if (payOptionID == "")
                    {
                        var result = MessageBox.Show("คุณต้องการลบข้อมูลนี้ใช่หรือไม่", "คำเตือน", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (result == DialogResult.Yes)
                        {
                            dgvPayMent.Rows.RemoveAt(e.RowIndex);
                        }
                    }
                    else
                    {
                        MessageBox.Show("ไม่สามารถใช้ปุ่มยกเลิกได้ให้ใช้ ติกยกเลิกข้อมูลแทน");
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                SetMsgException(ex);
            }
        }

        private void dgvPayMent_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == dgvPayMent.Rows[e.RowIndex].Cells["payMentOpt"].ColumnIndex)
                {
                    String payType = dgvPayMent.Rows[e.RowIndex].Cells["payMentOpt"].Value == null ? "" : dgvPayMent.Rows[e.RowIndex].Cells["payMentOpt"].Value.ToString();
                    if (payType == "")
                    {
                        dgvPayMent.Rows[e.RowIndex].Cells["payMentOpt"].Value = "";
                    }
                }

                if (e.ColumnIndex == dgvPayMent.Rows[e.RowIndex].Cells["payMentRowNo"].ColumnIndex)
                {
                    dgvPayMent.Rows[e.RowIndex].Cells["payMentRowNo"].Value = (e.RowIndex + 1).ToString();
                }

            }
            catch (Exception ex)
            {
                SetMsgException(ex);
            }
        }

        private void ckbMotercycleUsed_CheckedChanged(object sender, EventArgs e)
        {


            try
            {
                //cmbGroupOcp.SelectedValue = "";
                //cmbGroupOcpS.SelectedValue = "";
                txtOcpType.Text = "";
                txtOcpClass.Text = "";
                //txtOcpSpcType.Text = "";
                //txtOcpSpcClass.Text = "";
                var ocpTypeClassColl = new NewBisPASvcRef.OCCUPATION_DATA_COLLECTION();
                var obj = new NewBisPASvcRef.OCCUPATION_DATA();
                obj.CODE = "";
                obj.OCP_TYPE_NAME = "ระบุชั้นอาชีพ";
                ocpTypeClassColl.Add(obj);


                var GetData = from getData in PAR_OCCUPATION_COLL
                              where getData.MOTERCYCLE_USED == (ckbMotercycleUsed.Checked ? "Y" : "N")
                              orderby getData.OCP_TYPE ascending, getData.OCP_CLASS ascending
                              select getData;
                if (GetData != null && GetData.Count() > 0)
                {
                    ocpTypeClassColl.AddRange(GetData.ToArray());
                }


                if (ocpTypeClassColl != null && ocpTypeClassColl.Count() > 0)
                {
                    NewBisPASvcRef.OCCUPATION_DATA oCcupationObj = new NewBisPASvcRef.OCCUPATION_DATA();
                    oCcupationObj.CODE = "";
                    oCcupationObj.OCP_TYPE_NAME = "ระบุรายละเอียดชั้นอาชีพ";
                    ocpTypeClassColl.Add(oCcupationObj);
                    cmbocpDesc.DataSource = ocpTypeClassColl;
                    cmbocpDesc.DisplayMember = "OCP_TYPE_NAME";
                    cmbocpDesc.ValueMember = "CODE";
                    cmbocpDesc.SelectedValue = "";


                    NewBisPASvcRef.OCCUPATION_DATA[] pOccupation = new NewBisPASvcRef.OCCUPATION_DATA[PAR_OCCUPATION_COLL.Count];
                    PAR_OCCUPATION_COLL.CopyTo(pOccupation);

                    cmbParentbocpDesc.DataSource = pOccupation;
                    cmbParentbocpDesc.DisplayMember = "OCP_TYPE_NAME";
                    cmbParentbocpDesc.ValueMember = "CODE";
                    cmbParentbocpDesc.SelectedValue = "";


                    AutoCompleteStringCollection data = new AutoCompleteStringCollection();
                    foreach (NewBisPASvcRef.OCCUPATION_DATA item in PAR_OCCUPATION_COLL)
                    {
                        data.Add(item.DESCRIPTION);
                    }
                    cmbocpDesc.AutoCompleteMode = AutoCompleteMode.Suggest;
                    cmbocpDesc.AutoCompleteSource = AutoCompleteSource.CustomSource;
                    cmbocpDesc.AutoCompleteCustomSource = data;


                    cmbParentbocpDesc.AutoCompleteMode = AutoCompleteMode.Suggest;
                    cmbParentbocpDesc.AutoCompleteSource = AutoCompleteSource.CustomSource;
                    cmbParentbocpDesc.AutoCompleteCustomSource = data;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }


        }


        private void btnPayMentCalAuto_Click(object sender, EventArgs e)
        {

            if (dgvPayMent != null && dgvPayMent.Rows != null)
            {
                int tmnRowsCount = 0;
                int totalRows = 0;
                if (dgvPayMent.Rows.Count > 0)
                {
                    foreach (DataGridViewRow dr in dgvPayMent.Rows)
                    {
                        if (dr.IsNewRow == false)
                        {
                            totalRows++;
                            String tmnType = dr.Cells["payMentTmn"].Value == null ? "N" : dr.Cells["payMentTmn"].Value.ToString();
                            if (tmnType == "Y")
                            {
                                tmnRowsCount++;
                            }
                        }

                    }
                }

                if (tmnRowsCount == totalRows || totalRows == 0)
                {
                    string ChannelType = cmbChannelType.SelectedValue == null ? "" : cmbChannelType.SelectedValue.ToString();
                    var wSummarystatus = cmbSummryStatus.SelectedValue.ToString();
                    Decimal? premium = txtTotalPremium.Text == "" ? (Decimal?)null : Convert.ToDecimal(txtTotalPremium.Text.Trim().Replace(",", ""));
                    var calPremium = PAR_PREMIUM_SUSPENSE == null ? 0 : PAR_PREMIUM_SUSPENSE.Value;
                    Decimal? overPremium = (calPremium - premium);

                    SetReturnPayData(wSummarystatus, overPremium, ChannelType, calPremium, true);
                }
                else
                {

                    MessageBox.Show("ไม่สามารถคำนวณได้เนื่องจากมีรายการเงินคืน");
                }

            }
        }

        private void ckbPaymentTmn_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void ckbPayMentOperation_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void cmbParentRelation_SelectionChangeCommitted(object sender, EventArgs e)
        {

            try
            {
                if (cmbParentRelation.SelectedValue != null)
                {
                    if (cmbParentAdrType.SelectedValue.ToString() == "")// && PK_PARENT_UADDRESS_ID == null)
                    {
                        cmbParentAdrType.SelectedValue = cmbAdrType.SelectedValue;
                        txtParentAddressName.Text = txtAddressName.Text.Trim();
                        txtParentAddressNumber.Text = txtAddressNumber.Text.Trim();
                        txtParentMooh.Text = txtMooh.Text.Trim();
                        txtParentSoi.Text = txtSoi.Text.Trim();
                        txtParentRoad.Text = txtRoad.Text.Trim();

                        cmbParentProvince.Text = cmbProvince.Text;
                        cmbParentAmphur.Text = cmbAmphur.Text;
                        cmbParentTambol.Text = cmbTambol.Text;

                        txtParentZipcode.Text = txtZipcode.Text.Trim();
                    }
                }
            }
            catch (Exception ex)
            {
                SetMsgException(ex);
            }

        }

        private void CheckOldCustomer()
        {

            VerifyFlag = true;

            //if (cmbCardType.SelectedValue.ToString() == "")
            //{
            //    cmbCardType.Focus();
            //    throw new Exception("ระบุประเภทของบัตร");
            //}

            //if (txtIdcardNo.Text.Trim() == "")
            //{
            //    txtIdcardNo.Focus();
            //    throw new Exception("ระบุเลขบัตรประชาชนหรือหนังสือเดินทาง");
            //}

            if (txtIdcardNo.Text.Trim() != "" && cmbCardType.SelectedValue.ToString() == "")
            {
                cmbCardType.Focus();
                throw new Exception("ระบุประเภทของบัตร");
            }

            if (cmbCardType.SelectedValue.ToString() == "1" && txtIdcardNo.Text.Trim() != "")
            {
                if (Utility.ValidateIDCard(txtIdcardNo.Text) == false)
                {
                    txtIdcardNo.Focus();
                    throw new Exception("รูปแบบของเลขที่บัตรประชาชนไม่ถูกต้อง");
                }
            }

            //check user death
            deathAlert();

            NewBisPASvcRef.ProcessResult pr = new NewBisPASvcRef.ProcessResult();
            NewBisPASvcRef.P_NAME_ID_COLLECTION pNameIDColl = new NewBisPASvcRef.P_NAME_ID_COLLECTION();
            NewBisPASvcRef.P_PARENT_ID_COLLECTION pParentIDColl = new NewBisPASvcRef.P_PARENT_ID_COLLECTION();
            DateTime? customerBirthDate = Utility.StringToDateTime(txtBirthDt.Text, "BU");
            using (NewBisPASvcRef.NewBisPASvcClient client = new NewBisPASvcRef.NewBisPASvcClient())
            {
                pr = client.GetPNameIDByCustomerData(out pNameIDColl, out pParentIDColl, txtIdcardNo.Text.Trim(), txtName.Text.Trim(), txtSurname.Text.Trim(), cmbSex.SelectedValue.ToString(), customerBirthDate);
                if (pr.Successed == false)
                {
                    throw new Exception(pr.ErrorMessage);
                }

                PAR_P_NAME_ID_COLL = new NewBisPASvcRef.P_NAME_ID_COLLECTION();
                if (pNameIDColl != null && pNameIDColl.Count() > 0)
                {
                    PAR_P_NAME_ID_COLL.AddRange(pNameIDColl.ToArray());
                }

                PAR_P_PARENT_ID_COLL = new NewBisPASvcRef.P_PARENT_ID_COLLECTION();
                if (pParentIDColl != null && pParentIDColl.Count() > 0)
                {
                    PAR_P_PARENT_ID_COLL.AddRange(pParentIDColl.ToArray());
                }

            }

            SetColorItemBeginCustomerName();

            if ((pNameIDColl != null && pNameIDColl.Count() > 0) || (pParentIDColl != null && pParentIDColl.Count() > 0))
            {
                OldCustomerDataForm oldCusDataForm = new OldCustomerDataForm();
                oldCusDataForm.NameID = NAME_ID;
                oldCusDataForm.P_NAME_ID_COLL = new NewBisPASvcRef.P_NAME_ID_COLLECTION();
                if (pNameIDColl != null && pNameIDColl.Count() > 0)
                {
                    oldCusDataForm.P_NAME_ID_COLL.AddRange(pNameIDColl.ToArray());
                }
                oldCusDataForm.ParentID = PARENT_ID;
                oldCusDataForm.P_PARENT_ID_COLL = new NewBisPASvcRef.P_PARENT_ID_COLLECTION();
                if (pParentIDColl != null && pParentIDColl.Count() > 0)
                {
                    oldCusDataForm.P_PARENT_ID_COLL.AddRange(pParentIDColl.ToArray());
                }



                oldCusDataForm.ShowDialog();

                if (oldCusDataForm.P_NAME_ID_OBJ != null && oldCusDataForm.P_NAME_ID_OBJ.NAME_ID != null)
                {
                    NAME_ID = oldCusDataForm.P_NAME_ID_OBJ.NAME_ID;
                    cmbDataCus.BackColor = Color.FromArgb(bgRed[0], bgRed[1], bgRed[2]);
                    cmbDataCus.SelectedValue = "1";
                    ChkOldCusDataWithNewCusData(oldCusDataForm.P_NAME_ID_OBJ);
                    btnCustomerData.PerformClick();
                }
                else
                {
                    NAME_ID = null;
                    cmbDataCus.BackColor = Color.White;
                    cmbDataCus.SelectedValue = "2";

                    if (oldCusDataForm.P_PARENT_ID_OBJ != null && oldCusDataForm.P_PARENT_ID_OBJ.PARENT_ID != null)
                    {
                        ChkOldParentDataWithNewCusData(oldCusDataForm.P_PARENT_ID_OBJ);
                    }
                }

                if (oldCusDataForm.P_PARENT_ID_OBJ != null && oldCusDataForm.P_PARENT_ID_OBJ.PARENT_ID != null)
                {
                    PARENT_ID = oldCusDataForm.P_PARENT_ID_OBJ.PARENT_ID;
                }
                else
                {
                    PARENT_ID = null;
                }
            }
            else
            {
                NAME_ID = null;
                PARENT_ID = null;
                cmbDataCus.BackColor = Color.White;
                cmbDataCus.SelectedValue = "2";
            }

            if (NAME_ID == null)
            {
                cmbAdrType.SelectedValue = "2";
                cmbAdrType.BackColor = Color.White;
                ADDRESS_ID = null;
                SetColorItemBeginCustomerAddress();


                if (IsChannelTypeFreePolicy(cmbChannelType.SelectedValue.ToString()))
                {
                    var client = new NewBisPASvcRef.NewBisPASvcClient();
                    NewBisPASvcRef.LBDU_CUSTOMER dataObject;

                    var custypeOfLBDU = cmbCardType.SelectedValue.ToString() == "1" ? "CI" : "PP";
                    var pr2 = client.GetLBDU_CUSTOMER(out dataObject, custypeOfLBDU, txtIdcardNo.Text.Trim());
                    if (!pr2.Successed)
                    {
                        throw new Exception("Error GetLBDU_CUSTOMER : " + pr2.ErrorMessage);
                    }

                    if (dataObject != null)
                    {
                        AddressOfLBDU.Visible = true;
                        AddressOfLBDU.Text = dataObject.ADDR + " " + (!string.IsNullOrEmpty(dataObject.STREET) ? "ถ." + dataObject.STREET : dataObject.STREET) +
                                                               " " + (!string.IsNullOrEmpty(dataObject.SUBDIST) ? " แขวง" + dataObject.SUBDIST : dataObject.SUBDIST) +
                                                               " " + (!string.IsNullOrEmpty(dataObject.DISTRICT) ? " แขวง" + dataObject.DISTRICT : dataObject.DISTRICT) + " " + dataObject.PROVINCE + " " + dataObject.POST;
                        txtBirthDt.Text = dataObject.DOB != null ? Utility.dateTimeToString(dataObject.DOB.Value, "dd/MM/yyyy", "BU") : "";
                        txtPrename.Text = dataObject.PRENAME;
                        txtName.Text = dataObject.NAME;
                        txtSurname.Text = dataObject.SURNAME;
                        cmbSex.SelectedValue = dataObject.SEX != null ? dataObject.SEX.ToString() : "";
                        txtEmail.Text = dataObject.EMAIL;
                        cmbNationality.SelectedValue = dataObject.NATIONALITY;
                        txtMbPhone.Text = dataObject.TELNOM;
                        txtRoad.Text = dataObject.STREET;
                        cmbProvince.SelectedValue = dataObject.PROVINCE == "กรุงเทพมหานคร" ? "กรุงเทพฯ" : dataObject.PROVINCE;
                        var districtData = ((NewBisPASvcRef.GET_LIST_COLLECTION)cmbAmphur.DataSource);
                        if (districtData != null && districtData.Any())
                        {
                            var hasDistrict = districtData.Where(item => item.DESCRIPTION == dataObject.DISTRICT).Any();
                            if (hasDistrict)
                            {
                                cmbAmphur.SelectedValue = dataObject.DISTRICT;
                                var subdistrictData = ((NewBisPASvcRef.GET_LIST_COLLECTION)cmbTambol.DataSource);
                                if (subdistrictData != null && subdistrictData.Any())
                                {
                                    var hasSubDistrict = subdistrictData.Where(item => item.DESCRIPTION == dataObject.SUBDIST).Any();
                                    if (hasSubDistrict)
                                    {
                                        cmbTambol.SelectedValue = dataObject.SUBDIST;
                                    }
                                }
                            }
                        }
                        txtZipcode.Text = dataObject.POST;
                        txtPhoneNumber.Text = dataObject.TELNOR;
                        cmbAdrType.SelectedValue = "2";
                        cmbDataCus.SelectedValue = "2";
                        cmbAddressType.SelectedValue = "3";
                    }
                    else
                    {
                        AddressOfLBDU.Visible = false;
                        AddressOfLBDU.Text = "";
                    }
                }

            }
            else
            {


            }

            if (txtBirthDt.Text.Trim() != "")
            {
                Int64? ageCal = null;
                using (NewBisPASvcRef.NewBisPASvcClient clientAge = new NewBisPASvcRef.NewBisPASvcClient())
                {
                    DateTime? dateCal = null;
                    dateCal = Utility.StringToDateTime(txtAppSignDt.Text, "BU");
                    if (PAR_PA_APPLICATION_ID != null && PAR_PA_APPLICATION_ID.APP_ID != null)
                    {
                        if (PAR_PA_APPLICATION_ID.APPLICATION_Collection != null && PAR_PA_APPLICATION_ID.APPLICATION_Collection.Count() > 0)
                        {
                            foreach (NewBisPASvcRef.U_APPLICATION obj in PAR_PA_APPLICATION_ID.APPLICATION_Collection)
                            {
                                if (obj.LIFE_ID != null && obj.LIFE_ID.UAPP_ID != null)
                                {
                                    dateCal = obj.LIFE_ID.ISU_DT;
                                    if (txtPaidIsuDate.Text.Trim() != "")
                                    {
                                        dateCal = Utility.StringToDateTime(txtPaidIsuDate.Text, "BU");
                                    }
                                }
                            }
                        }
                    }
                    if (cmbChannelType.SelectedValue == null || cmbChannelType.SelectedValue.ToString() == "")
                    {
                        cmbChannelType.Focus();
                        throw new Exception("กรุณาระบุช่องทางการขายที่ท่านต้องการ");
                    }
                    String channelType = cmbChannelType.SelectedValue.ToString();
                    DateTime? birthDateCal = Utility.StringToDateTime(txtBirthDt.Text, "BU");
                    ageCal = clientAge.GetAge(out pr, birthDateCal, dateCal, dateCal, channelType);
                    if (pr.Successed == false)
                    {
                        throw new Exception(pr.ErrorMessage);
                    }
                }

                txtStAge.Text = ageCal == null ? "" : ageCal.Value.ToString();
            }
            else
            {
                txtStAge.Text = "";
            }

            if (NAME_ID != null)
            {
                ShowAddressDialog();

                if (IsChannelTypeFreePolicy(cmbChannelType.SelectedValue.ToString()))
                {
                    var client = new NewBisPASvcRef.NewBisPASvcClient();
                    NewBisPASvcRef.LBDU_CUSTOMER dataObject;

                    var custypeOfLBDU = cmbCardType.SelectedValue.ToString() == "1" ? "CI" : "PP";
                    var pr2 = client.GetLBDU_CUSTOMER(out dataObject, custypeOfLBDU, txtIdcardNo.Text.Trim());
                    if (!pr2.Successed)
                    {
                        throw new Exception("Error GetLBDU_CUSTOMER : " + pr2.ErrorMessage);
                    }

                    if (dataObject != null)
                    {
                        AddressOfLBDU.Visible = true;
                        AddressOfLBDU.Text = dataObject.ADDR + " " + (!string.IsNullOrEmpty(dataObject.STREET) ? "ถ." + dataObject.STREET : dataObject.STREET) +
                                                               " " + (!string.IsNullOrEmpty(dataObject.SUBDIST) ? " แขวง" + dataObject.SUBDIST : dataObject.SUBDIST) +
                                                               " " + (!string.IsNullOrEmpty(dataObject.DISTRICT) ? " แขวง" + dataObject.DISTRICT : dataObject.DISTRICT) + " " + dataObject.PROVINCE + " " + dataObject.POST;
                        if (string.IsNullOrEmpty(txtEmail.Text))
                        {
                            txtEmail.Text = dataObject.EMAIL;
                        }
                        if (string.IsNullOrEmpty(cmbNationality.SelectedValue.ToString()))
                        {
                            cmbNationality.SelectedValue = dataObject.NATIONALITY;
                        }
                        if (string.IsNullOrEmpty(txtMbPhone.Text))
                        {
                            txtMbPhone.Text = dataObject.TELNOM;
                        }
                        if (string.IsNullOrEmpty(txtRoad.Text))
                        {
                            txtRoad.Text = dataObject.STREET;
                        }
                        if (string.IsNullOrEmpty(cmbProvince.SelectedValue.ToString()))
                        {
                            cmbProvince.SelectedValue = dataObject.PROVINCE == "กรุงเทพมหานคร" ? "กรุงเทพฯ" : dataObject.PROVINCE;
                        }
                        if (string.IsNullOrEmpty(cmbAmphur.SelectedValue.ToString()) && string.IsNullOrEmpty(cmbTambol.SelectedValue.ToString()))
                        {
                            var districtData = ((NewBisPASvcRef.GET_LIST_COLLECTION)cmbAmphur.DataSource);
                            if (districtData != null && districtData.Any())
                            {
                                var hasDistrict = districtData.Where(item => item.DESCRIPTION == dataObject.DISTRICT).Any();
                                if (hasDistrict)
                                {
                                    cmbAmphur.SelectedValue = dataObject.DISTRICT;
                                    var subdistrictData = ((NewBisPASvcRef.GET_LIST_COLLECTION)cmbTambol.DataSource);
                                    if (subdistrictData != null && subdistrictData.Any())
                                    {
                                        var hasSubDistrict = subdistrictData.Where(item => item.DESCRIPTION == dataObject.SUBDIST).Any();
                                        if (hasSubDistrict)
                                        {
                                            cmbTambol.SelectedValue = dataObject.SUBDIST;
                                        }
                                    }
                                }
                            }

                            txtZipcode.Text = dataObject.POST;
                        }

                        if (string.IsNullOrEmpty(txtPhoneNumber.Text))
                        {
                            txtPhoneNumber.Text = dataObject.TELNOR;
                        }
                        cmbAdrType.SelectedValue = "2";
                        cmbAddressType.SelectedValue = "3";
                    }
                }
            }

        }

        private void deathAlert()
        {
            using (DeathSvcRef.IdCardClient cd = new DeathSvcRef.IdCardClient())
            {
                if (txtBirthDt.Text != "" && txtBirthDt.Text.Trim() != "")
                {
                    DeathSvcRef.POPOut p = new DeathSvcRef.POPOut();
                    DeathSvcRef.IdCardInfo idi = new DeathSvcRef.IdCardInfo();

                    idi.BirthDate = Utility.StringToDate(txtBirthDt.Text);
                    idi.Cid = txtIdcardNo.Text.Trim();

                    p = cd.CheckDeathStatus(idi);

                    if (p.dataInfoField.stCodeField == 1)
                    {
                        throw new Exception("เลขที่บัตรประชาชนมีสถานะการเสียชีวิต ไม่สามารถทำธุรกรรมใดๆได้");
                    }
                }
            }
        }

        private void InitUapplicationUpdate()
        {
            if (PAR_PA_APPLICATION_ID != null && PAR_PA_APPLICATION_ID.APPLICATION_Collection != null && PAR_PA_APPLICATION_ID.APPLICATION_Collection.Any())

                if (PAR_PA_APPLICATION_ID.APPLICATION_Collection[0].APPLICATION_UPDATE_Collection != null && PAR_PA_APPLICATION_ID.APPLICATION_Collection[0].APPLICATION_UPDATE_Collection.Count() > 0)
                {
                    var GetUserUpdate =
                        from getUserUpdate in PAR_PA_APPLICATION_ID.APPLICATION_Collection[0].APPLICATION_UPDATE_Collection
                        orderby getUserUpdate.FSYSTEM_DT ascending
                        select getUserUpdate;
                    int index = 0;

                    if (GetUserUpdate != null && GetUserUpdate.Count() > 0)
                    {
                        //NewBISSvcRef.GET_LIST_COLLECTION getUserUpdateColl = new NewBISSvcRef.GET_LIST_COLLECTION();
                        CenterSvcRef.ProcessResult pr = new CenterSvcRef.ProcessResult();
                        cmbUserUpdate.Items.Clear();
                        using (CenterSvcRef.CenterServiceClient client = new CenterSvcRef.CenterServiceClient())
                        {
                            foreach (var obj in GetUserUpdate)
                            {
                                NewBISSvcRef.GET_LIST getListObj = new NewBISSvcRef.GET_LIST();
                                getListObj.CODE = obj.UPD_ID;

                                CenterSvcRef.User oUser = new CenterSvcRef.User();
                                oUser = client.getUser(out pr, obj.UPD_ID);
                                if (pr.Successed == false)
                                {
                                    throw new Exception(pr.ErrorMessage);
                                }
                                if (oUser != null)
                                {
                                    String fDate = obj.FSYSTEM_DT == null ? "" : Utility.dateTimeToString(obj.FSYSTEM_DT.Value, "dd/MM/yyyy hh:mi:ss", "BU");
                                    getListObj.DESCRIPTION = oUser.firstName + "  " + oUser.lastName + " (  เวลา : " + fDate + " )";
                                    cmbUserUpdate.Items.Add(getListObj);
                                    index++;
                                }

                            }
                        }

                        cmbUserUpdate.DisplayMember = "DESCRIPTION";
                        cmbUserUpdate.SelectedIndex = 0;

                    }
                }
                else
                {
                    cmbUserUpdate.Items.Clear();
                    NewBISSvcRef.GET_LIST userUpdateObj = new NewBISSvcRef.GET_LIST();
                    userUpdateObj.CODE = "";
                    userUpdateObj.DESCRIPTION = "รายชื่อผู้บันทึกหรือแก้ไขข้อมูล";
                    cmbUserUpdate.Items.Add(userUpdateObj);
                    cmbUserUpdate.DisplayMember = "DESCRIPTION";
                    cmbUserUpdate.SelectedItem = userUpdateObj;
                }
        }

        private void MakeUPolPrintingException()
        {
            var appId = GetAppIdFromUapplication();
            if (appId != null)
            {
                NewBISSvcRef.U_POLPRINTING_EXCEPTION[] uPolPrintingArr;
                NewBISSvcRef.ProcessResult pr = new NewBISSvcRef.ProcessResult();

                using (NewBISSvcRef.NewBISSvcClient client = new NewBISSvcRef.NewBISSvcClient())
                {
                    pr = client.GetU_POLPRINTING_EXCEPTION(out uPolPrintingArr, new long[] { appId.Value });
                    if (pr.Successed == false)
                    {
                        throw new Exception(pr.ErrorMessage);
                    }
                }

                if (uPolPrintingArr != null && uPolPrintingArr.Any())
                {
                    var uPolPrintingObj = new NewBisPASvcRef.U_POLPRINTING_EXCEPTION();
                    uPolPrintingObj.APP_ID = appId;
                    if (ckbMedical.Checked == true)
                    {
                        uPolPrintingObj.MEDICAL = 'N';
                    }
                    else
                    {
                        uPolPrintingObj.MEDICAL = 'E';
                    }

                    if (ckbCounterOffer.Checked == true)
                    {
                        uPolPrintingObj.COUNTEROFFER = 'N';
                    }
                    else
                    {
                        uPolPrintingObj.COUNTEROFFER = 'E';
                    }
                    if (ckbchgConditionApp.Checked == true)
                    {
                        uPolPrintingObj.CHGCONDITION_APP = 'Y';
                    }
                    else
                    {
                        uPolPrintingObj.CHGCONDITION_APP = 'E';
                    }
                    if (ckbApsolutePlan.Checked == true)
                    {
                        uPolPrintingObj.APSOLUTE_PLAN = 'N';
                    }
                    else
                    {
                        uPolPrintingObj.APSOLUTE_PLAN = 'E';
                    }
                    uPolPrintingObj.UPD_DT = DateTime.Now;
                    uPolPrintingObj.UPD_ID = UserID;
                    PAR_PA_APPLICATION_ID.POLPRINTING_EXCEPTION = new NewBisPASvcRef.U_POLPRINTING_EXCEPTION();
                    PAR_PA_APPLICATION_ID.POLPRINTING_EXCEPTION = uPolPrintingObj;
                }
                else
                {
                    bool haveData = false;
                    if (ckbMedical.Checked == true)
                    {
                        haveData = true;
                    }
                    if (ckbCounterOffer.Checked == true)
                    {
                        haveData = true;
                    }

                    if (ckbchgConditionApp.Checked == true)
                    {
                        haveData = true;
                    }
                    if (ckbApsolutePlan.Checked == true)
                    {
                        haveData = true;
                    }

                    if (haveData == true)
                    {
                        var uPolPrintingObj = new NewBisPASvcRef.U_POLPRINTING_EXCEPTION();
                        uPolPrintingObj.APP_ID = appId;
                        if (ckbMedical.Checked == true)
                        {
                            uPolPrintingObj.MEDICAL = 'N';
                        }
                        else
                        {
                            uPolPrintingObj.MEDICAL = 'E';
                        }
                        if (ckbCounterOffer.Checked == true)
                        {
                            uPolPrintingObj.COUNTEROFFER = 'N';
                        }
                        else
                        {
                            uPolPrintingObj.COUNTEROFFER = 'E';
                        }
                        if (ckbchgConditionApp.Checked == true)
                        {
                            uPolPrintingObj.CHGCONDITION_APP = 'Y';
                        }
                        else
                        {
                            uPolPrintingObj.CHGCONDITION_APP = 'E';
                        }
                        if (ckbApsolutePlan.Checked == true)
                        {
                            uPolPrintingObj.APSOLUTE_PLAN = 'N';
                        }
                        else
                        {
                            uPolPrintingObj.APSOLUTE_PLAN = 'E';
                        }
                        uPolPrintingObj.UPD_DT = DateTime.Now;
                        uPolPrintingObj.UPD_ID = UserID;
                        PAR_PA_APPLICATION_ID.POLPRINTING_EXCEPTION = new NewBisPASvcRef.U_POLPRINTING_EXCEPTION();
                        PAR_PA_APPLICATION_ID.POLPRINTING_EXCEPTION = uPolPrintingObj;
                    }
                }
            }
        }


        private void InitUPolPrintingException()
        {
            if (PAR_PA_APPLICATION_ID.POLPRINTING_EXCEPTION != null && PAR_PA_APPLICATION_ID.POLPRINTING_EXCEPTION.APP_ID != null)
            {
                var uPolPrintingObj = new NewBisPASvcRef.U_POLPRINTING_EXCEPTION();
                uPolPrintingObj = PAR_PA_APPLICATION_ID.POLPRINTING_EXCEPTION;

                if (uPolPrintingObj.MEDICAL == 'N')
                {
                    ckbMedical.Checked = true;
                }
                else
                {
                    ckbMedical.Checked = false;
                }

                if (uPolPrintingObj.COUNTEROFFER == 'N')
                {
                    ckbCounterOffer.Checked = true;
                }
                else
                {
                    ckbCounterOffer.Checked = false;
                }

                if (uPolPrintingObj.CHGCONDITION_APP == 'Y')
                {
                    ckbchgConditionApp.Checked = true;
                }
                else
                {
                    ckbchgConditionApp.Checked = false;
                }

                if (uPolPrintingObj.APSOLUTE_PLAN == 'N')
                {
                    ckbApsolutePlan.Checked = true;
                }
                else
                {
                    ckbApsolutePlan.Checked = false;
                }


            }
        }
        private long? GetAppIdFromUapplication()
        {
            if (PAR_PA_APPLICATION_ID != null && PAR_PA_APPLICATION_ID.APPLICATION_Collection != null && PAR_PA_APPLICATION_ID.APPLICATION_Collection.Any())
            {
                return PAR_PA_APPLICATION_ID.APPLICATION_Collection.FirstOrDefault().APP_ID;
            }
            return null;
        }

        private void chbSmartIDTmn_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chbSmartIDTmn.Checked == true)
                {
                    cmbSmartRemark.Visible = true;
                }
                else
                {
                    cmbSmartRemark.Visible = false;
                }
            }
            catch (Exception ex)
            {
                SetMsgException(ex);
            }
        }

        private void txtSmartBankCode_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyValue == 13)
            {
                if (txtSmartBankCode.Text != "")
                {
                    txtSmartBankCode.Text = txtSmartBankCode.Text.PadLeft(3, '0');
                    NewBISSvcRef.ProcessResult pr = new NewBISSvcRef.ProcessResult();
                    String bankName = "";
                    using (NewBISSvcRef.NewBISSvcClient client = new NewBISSvcRef.NewBISSvcClient())
                    {
                        try
                        {
                            bankName = client.GetBankName(out pr, txtSmartBankCode.Text);
                        }
                        catch (Exception ex)
                        {

                            throw ex;
                        }
                    }
                    if (bankName != "")
                    {
                        txtSmartBankName.Text = bankName;
                    }
                    else
                    {
                        txtSmartBankName.Text = "";
                        MessageBox.Show("ไม่พบชื่อธนาคารที่ท่านต้องการ");
                        return;
                    }
                }
                else
                {
                    txtSmartBankName.Text = "";
                    MessageBox.Show("กรุณาระบุรหัสธนาคารที่ท่านต้องการ");
                    return;
                }
            }
        }

        private void txtSmartBranchCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                if ((txtSmartBankCode.Text != "") && (txtSmartBranchCode.Text != ""))
                {
                    txtSmartBranchCode.Text = txtSmartBranchCode.Text.PadLeft(4, '0');
                    NewBISSvcRef.ProcessResult pr = new NewBISSvcRef.ProcessResult();
                    String branchName = "";
                    using (NewBISSvcRef.NewBISSvcClient client = new NewBISSvcRef.NewBISSvcClient())
                    {
                        try
                        {
                            branchName = client.GetBranchName(out pr, txtSmartBankCode.Text, txtSmartBranchCode.Text);
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                    }
                    if (branchName != "")
                    {
                        txtSmartBranchName.Text = branchName;
                    }
                    else
                    {
                        txtSmartBranchName.Text = "";
                        MessageBox.Show("ไม่พบชื่อสาขาธนาคารที่ท่านต้องการ");
                        return;
                    }
                }
                else
                {
                    txtSmartBranchName.Text = "";
                    MessageBox.Show("กรุณาระบุรหัสธนาคารและรหัสสาขาธนาคารที่ท่านต้องการ");
                    return;
                }
            }
        }

        private void txtSmartAccNo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyValue == 13)
                {

                    if (txtSmartBankCode.Text.Trim() == "" && txtSmartBankName.Text.Trim() == "")
                    {
                        txtSmartBankCode.Focus();
                        throw new Exception("กรุณาระบุชื่อธนาคาร");
                    }

                    if (txtSmartAccNo.Text.Trim() == "")
                    {
                        txtSmartAccNo.Focus();
                        throw new Exception("กรุณาระบุเลขที่บัญชี");
                    }

                    NewBISSvcRef.ProcessResult pr = new NewBISSvcRef.ProcessResult();
                    String AccType = "";
                    using (NewBISSvcRef.NewBISSvcClient client = new NewBISSvcRef.NewBISSvcClient())
                    {
                        pr = client.GET_ACCTYPE(out AccType, txtSmartBankCode.Text.Trim(), txtSmartAccNo.Text.Trim());
                        if (pr.Successed == false)
                        {
                            throw new Exception(pr.ErrorMessage);
                        }
                    }

                    //ตรวจสอบบัญชี Smart ========================================

                    if (chbSmartIDTmn.Checked == false)
                    {
                        if (txtSmartBankCode.Text.Trim() != "" && txtSmartAccNo.Text.Trim() != "")
                        {

                            ITUtility.ProcessResult prPol = new ProcessResult();
                            using (PolicySvcRef.PolicySvcClient polClient = new PolicySvcRef.PolicySvcClient())
                            {
                                string policydup = "";
                                PolicySvcRef.P_ACCOUNT_ID[] pAccountIDArr;
                                //prPol = polClient.GetP_ACCOUNT_ID(out pAccountIDArr, txtSmartAccNo.Text.Trim(), txtSmartBankCode.Text.Trim());
                                prPol = polClient.CheckAccount(out pAccountIDArr, out policydup, txtSmartAccNo.Text.Trim(), txtSmartBankCode.Text.Trim());

                                if (pAccountIDArr != null && pAccountIDArr.Count() > 0)
                                {
                                    var PAccountIDLinq =
                                        from pAccountIDLinq in pAccountIDArr
                                        where pAccountIDLinq.TMN == 'N'
                                        select pAccountIDLinq;
                                    if (PAccountIDLinq != null && PAccountIDLinq.Count() > 0)
                                    {
                                        PolicySvcRef.P_ACCOUNT_ID pAccountIDObj = new PolicySvcRef.P_ACCOUNT_ID();
                                        pAccountIDObj = PAccountIDLinq.ToArray()[0];

                                        if (pAccountIDObj.ACC_NAME.Trim().Replace(" ", "") != txtSmartAccName.Text.Trim().Replace(" ", ""))
                                        {
                                            if (String.IsNullOrEmpty(policydup))
                                            {
                                                MessageBox.Show("ในหน้าบัญชีจ่ายผลประโยชน์ ข้อมูลชื่อบัญชีไม่ตรงกับข้อมูลเดิม ข้อมูลเดิมคือ " + pAccountIDObj.ACC_NAME);

                                            }
                                            else
                                            {
                                                MessageBox.Show("ในหน้าบัญชีจ่ายผลประโยชน์ ข้อมูลชื่อบัญชีไม่ตรงกับข้อมูล SMART เดิม ข้อมูลเดิมคือ " + pAccountIDObj.ACC_NAME + " กรมธรรม์ที่ใช้ " + policydup);

                                            }

                                        }

                                        if (!(AccType == "" || AccType == "N"))
                                        {
                                            String depositTypeName = "";
                                            if (pAccountIDObj.ACC_DEPOSIT_TYPE == 'S')
                                            {
                                                depositTypeName = "สะสมทรัพย์";
                                            }
                                            else if (pAccountIDObj.ACC_DEPOSIT_TYPE == 'C')
                                            {
                                                depositTypeName = "กระแสรายวัน";
                                            }
                                            if (pAccountIDObj.ACC_DEPOSIT_TYPE != Convert.ToChar(AccType))
                                            {
                                                MessageBox.Show("ในหน้าบัญชีจ่ายผลประโยชน์ ข้อมูลประเภทบัญชีไม่ตรงกับข้อมูลเดิม ข้อมูลเดิมคือ " + depositTypeName);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }

                    //END ตรวจสอบบัญชี Smart ====================================
                    if (AccType == "" || AccType == "N")
                    {
                        MessageBox.Show("เลขที่บัญชีนี้ไม่พบข้อมูลว่าเป็นประเภทสะสมทรัพย์ หรือ กระแสรายวัน ท่านสามารถเลือกประเภทบัญชีได้เอง");
                        cmbSmartDepositType.SelectedValue = "";
                    }
                    else
                    {
                        cmbSmartDepositType.SelectedValue = AccType;
                    }
                }
            }
            catch (Exception ex)
            {
                SetMsgException(ex);
            }
        }

        private void cmbSmartAccOwner_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbSmartAccOwner.SelectedValue.ToString() == "P")
                {
                    int? ageChild = txtStAge.Text == "" ? (int?)null : Convert.ToInt16(txtStAge.Text);
                    if (ageChild == null || ageChild > 15)
                    {
                        cmbSmartAccOwner.SelectedValue = "";
                        cmbSmartAccOwner.Focus();
                        throw new Exception("ไม่ใช่กรมธรรม์เด็กไม่สามารถเลือกเจ้าของบัญชีเป็นผู้ปกครองได้");
                    }
                }

                if (txtSmartAccName.Text.Trim() == "")
                {
                    if (cmbSmartAccOwner.SelectedValue.ToString() == "C")
                    {
                        txtSmartAccName.Text = txtPrename.Text.Trim() + " " + txtName.Text.Trim() + "  " + txtSurname.Text.Trim();
                    }
                    else if (cmbSmartAccOwner.SelectedValue.ToString() == "P")
                    {
                        txtSmartAccName.Text = txtParentPrename.Text.Trim() + " " + txtParentName.Text.Trim() + "  " + txtParentSurname.Text.Trim();
                    }
                }

            }
            catch (Exception ex)
            {
                SetMsgException(ex);
            }
        }

        private void btnSaveSmart_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtSmartBankCode.Text != "" && txtSmartBankName.Text == "")
                {
                    txtSmartBankCode.Focus();
                    throw new Exception("กรุณาระบุชื่อธนาคาร");
                }

                if (txtSmartBranchCode.Text != "" && txtSmartBranchName.Text == "")
                {
                    txtSmartBranchCode.Focus();
                    throw new Exception("กรุณาระบุชื่อสาขาธนาคาร");
                }

                bool chkSuscess = false;
                if (chbSmartIDTmn.Checked == false)
                {
                    if ((txtSmartBankCode.Text != "") && (txtSmartBankName.Text != ""))
                    {
                        if (txtSmartBranchCode.Text == "")
                        {
                            txtSmartBranchCode.Focus();
                            throw new Exception("กรุณาระบุสาขาธนาคาร");
                        }
                        if (txtSmartBranchName.Text == "")
                        {
                            txtSmartBranchName.Focus();
                            throw new Exception("กรุณาระบุชื่อธนาคาร");
                        }
                        if (txtSmartAccName.Text == "")
                        {
                            txtSmartAccName.Focus();
                            throw new Exception("กรุณาระบุชื่อบัญชี");
                        }
                        if (txtSmartAccNo.Text == "")
                        {
                            txtSmartAccNo.Focus();
                            throw new Exception("กรุณาระบุเลขที่บัญชี");
                        }
                        if (cmbSmartAccOwner.SelectedValue.ToString() == "")
                        {
                            cmbSmartAccOwner.Focus();
                            throw new Exception("กรุณาระบุเจ้าของบัญชี");
                        }
                        String SmartDepositType = cmbSmartDepositType.SelectedValue == null ? "" : cmbSmartDepositType.SelectedValue.ToString();
                        if (SmartDepositType == "")
                        {
                            cmbSmartDepositType.Focus();
                            throw new Exception("กรุณาระบุประเภทบัญชี");
                        }
                        if (cmbSmartAccOwner.SelectedValue.ToString() == "P" && txtParentIdcardNo.Text == "")
                        {
                            cmbSmartAccOwner.Focus();
                            throw new Exception("ท่านเลือกผู้ปกครองเป็นเจ้าของบัญชี กรุณาระบุข้อมูลผู้ปกครอง");
                        }
                        if (cmbSmartAccOwner.SelectedValue.ToString() == "C" && txtIdcardNo.Text == "")
                        {
                            cmbSmartAccOwner.Focus();
                            throw new Exception("ท่านเลือกผู้ปกครองเป็นเจ้าของบัญชี กรุณาระบุข้อมูลผู้เอาประกัน");
                        }

                        chkSuscess = true;
                    }
                }
                else if (chbSmartIDTmn.Checked == true)
                {
                    if (txtSmartAccName.Text == "")
                    {
                        txtSmartAccName.Focus();
                        throw new Exception("กรุณาระบุชื่อบัญชี");
                    }
                    if (cmbSmartAccOwner.SelectedValue.ToString() == "")
                    {
                        cmbSmartAccOwner.Focus();
                        throw new Exception("กรุณาระบุเจ้าของบัญชี");
                    }
                    if (cmbSmartAccOwner.SelectedValue.ToString() == "P" && txtParentIdcardNo.Text == "")
                    {
                        cmbSmartAccOwner.Focus();
                        throw new Exception("ท่านเลือกผู้ปกครองเป็นเจ้าของบัญชี กรุณาระบุข้อมูลผู้ปกครอง");
                    }
                    if (cmbSmartAccOwner.SelectedValue.ToString() == "C" && txtIdcardNo.Text == "")
                    {
                        cmbSmartAccOwner.Focus();
                        throw new Exception("ท่านเลือกผู้ปกครองเป็นเจ้าของบัญชี กรุณาระบุข้อมูลผู้เอาประกัน");
                    }
                    chkSuscess = true;
                }


                if (chkSuscess == true)
                {

                    if (PAR_PA_APPLICATION_ID != null && PAR_PA_APPLICATION_ID.APPLICATION_Collection != null && PAR_PA_APPLICATION_ID.APPLICATION_Collection.Any())
                    {
                        PAR_PA_APPLICATION_ID.APPLICATION_Collection.First().SMART_ID_Collection = new NewBisPASvcRef.U_SMART_ID_Collection();
                        NewBISSvcRef.U_SMART_ID uSmartID = new NewBISSvcRef.U_SMART_ID();
                        uSmartID.USMART_ID = PK_SMART_ID;
                        uSmartID.UAPP_ID = PAR_PA_APPLICATION_ID.APPLICATION_Collection.First().UAPP_ID;
                        uSmartID.UACCOUNT_ID = PK_SMART_ACCOUNT_ID;
                        uSmartID.DOCUMENT_DT = Utility.StringToDateTime(txtAppSignDt.Text, "BU");
                        uSmartID.RECEIVE_DT = Utility.StringToDateTime(txtAppOfcRcvDt.Text, "BU");
                        uSmartID.TRANACTION_DT = Utility.StringToDateTime(txtAppSysDt.Text, "BU");
                        uSmartID.DOCUMENT_SOURCE = "A";
                        uSmartID.FSYSTEM_DT = DateTime.Now;
                        uSmartID.UPD_DT = DateTime.Now;
                        uSmartID.UPD_ID = UserID;
                        uSmartID.ObjId = 1;

                        String remarkSmart = cmbSmartRemark.SelectedValue == null ? "" : cmbSmartRemark.SelectedValue.ToString();

                        if (chbSmartIDTmn.Checked == true)
                        {
                            uSmartID.TMN = 'Y';
                            uSmartID.SMART_ID_TMN = new NewBISSvcRef.U_SMART_ID_TMN();
                            uSmartID.SMART_ID_TMN.USMART_ID = uSmartID.USMART_ID;
                            uSmartID.SMART_ID_TMN.TMN_DT = DateTime.Now;
                            uSmartID.SMART_ID_TMN.TMN_ID = uSmartID.UPD_ID;
                            uSmartID.FOLLOW_SMART_CODE = remarkSmart == "" ? null : remarkSmart;
                        }
                        else
                        {
                            uSmartID.TMN = 'N';
                            uSmartID.FOLLOW_SMART_CODE = null;
                        }



                        NewBISSvcRef.U_ACCOUNT_ID uAccIDSmart = new NewBISSvcRef.U_ACCOUNT_ID();
                        uAccIDSmart.UACCOUNT_ID = uSmartID.UACCOUNT_ID;
                        uAccIDSmart.ACC_BANK = txtSmartBankCode.Text.Trim() == null ? null : txtSmartBankCode.Text.Trim();
                        uAccIDSmart.ACC_BRANCH = txtSmartBranchCode.Text.Trim() == null ? null : txtSmartBranchCode.Text.Trim();
                        uAccIDSmart.ACC_NO = txtSmartAccNo.Text.Trim() == null ? null : txtSmartAccNo.Text.Trim();
                        uAccIDSmart.ACC_DEPOSIT_TYPE = cmbSmartDepositType.SelectedValue.ToString() == "" ? null : cmbSmartDepositType.SelectedValue.ToString();
                        uAccIDSmart.ACC_NAME = txtSmartAccName.Text.Trim() == null ? null : txtSmartAccName.Text.Trim();
                        uAccIDSmart.ACC_NAME_TYPE = "P";
                        uAccIDSmart.APPROVE_DT = null;
                        uAccIDSmart.APPROVE_ID = null;
                        uAccIDSmart.FSYSTEM_DT = DateTime.Now;
                        uAccIDSmart.UPD_DT = DateTime.Now;
                        uAccIDSmart.UPD_ID = UserID;

                        uAccIDSmart.SMART_ID = new NewBISSvcRef.U_SMART_ID();
                        uAccIDSmart.SMART_ID = uSmartID;

                        uAccIDSmart.ACCOUNT_PERSONAL = new NewBISSvcRef.U_ACCOUNT_PERSONAL();
                        uAccIDSmart.ACCOUNT_PERSONAL.UACCOUNT_ID = uSmartID.UACCOUNT_ID;


                        if (cmbSmartAccOwner.SelectedValue.ToString() == "P")
                        {
                            uAccIDSmart.ACCOUNT_PERSONAL.PRENAME = txtParentPrename.Text.Trim() == null ? null : txtParentPrename.Text.Trim();
                            uAccIDSmart.ACCOUNT_PERSONAL.NAME = txtParentName.Text.Trim() == null ? null : txtParentName.Text.Trim();
                            uAccIDSmart.ACCOUNT_PERSONAL.SURNAME = txtParentSurname.Text.Trim() == null ? null : txtParentSurname.Text.Trim();
                            uAccIDSmart.ACCOUNT_PERSONAL.BIRTH_DT = Utility.StringToDateTime(txtParentBirthDt.Text, "BU");

                            uAccIDSmart.ACCOUNT_PERSONAL.SEX = cmbParentGender.SelectedValue.ToString() == "" ? null : cmbParentGender.SelectedValue.ToString();

                            if (cmbParentCardType.SelectedValue.ToString() == "1")
                            {
                                uAccIDSmart.ACCOUNT_PERSONAL.IDCARD_NO = txtParentIdcardNo.Text.Trim() == "" ? null : txtParentIdcardNo.Text.Trim();
                                uAccIDSmart.ACCOUNT_PERSONAL.PASSPORT = null;
                            }
                            else
                            {
                                uAccIDSmart.ACCOUNT_PERSONAL.IDCARD_NO = null;
                                uAccIDSmart.ACCOUNT_PERSONAL.PASSPORT = txtParentIdcardNo.Text.Trim() == "" ? null : txtParentIdcardNo.Text.Trim();
                            }
                        }

                        if (cmbSmartAccOwner.SelectedValue.ToString() == "C")
                        {
                            uAccIDSmart.ACCOUNT_PERSONAL.PRENAME = txtPrename.Text.Trim() == null ? null : txtPrename.Text.Trim();
                            uAccIDSmart.ACCOUNT_PERSONAL.NAME = txtName.Text.Trim() == null ? null : txtName.Text.Trim();
                            uAccIDSmart.ACCOUNT_PERSONAL.SURNAME = txtSurname.Text.Trim() == null ? null : txtSurname.Text.Trim();
                            uAccIDSmart.ACCOUNT_PERSONAL.BIRTH_DT = Utility.StringToDateTime(txtBirthDt.Text, "BU");

                            uAccIDSmart.ACCOUNT_PERSONAL.SEX = cmbSex.SelectedValue.ToString() == "" ? null : cmbSex.SelectedValue.ToString();
                            if (cmbCardType.SelectedValue.ToString() == "1")
                            {
                                uAccIDSmart.ACCOUNT_PERSONAL.IDCARD_NO = txtIdcardNo.Text.Trim() == "" ? null : txtIdcardNo.Text.Trim();
                                uAccIDSmart.ACCOUNT_PERSONAL.PASSPORT = null;
                            }
                            else
                            {
                                uAccIDSmart.ACCOUNT_PERSONAL.IDCARD_NO = null;
                                uAccIDSmart.ACCOUNT_PERSONAL.PASSPORT = txtIdcardNo.Text.Trim() == "" ? null : txtIdcardNo.Text.Trim();
                            }
                        }

                        NewBISSvcRef.APPLICATION_TIME_WORK_COLL appTimeWorkColl = new NewBISSvcRef.APPLICATION_TIME_WORK_COLL();
                        if (uAccIDSmart != null)
                        {
                            NewBISSvcRef.ProcessResult pr = new NewBISSvcRef.ProcessResult();
                            using (NewBISSvcRef.NewBISSvcClient client = new NewBISSvcRef.NewBISSvcClient())
                            {
                                pr = client.EditU_ACCOUNT_ID(ref uAccIDSmart, ref appTimeWorkColl);
                            }
                            if (pr.Successed == false)
                            {
                                throw new Exception(pr.ErrorMessage);
                            }
                            PK_SMART_ID = uAccIDSmart.SMART_ID.USMART_ID;
                            PK_SMART_ACCOUNT_ID = uAccIDSmart.UACCOUNT_ID;
                            MessageBox.Show("บันทึกข้อมูลบันชีผู้รับผลประโยชน์เรียบร้อยแล้ว");
                        }
                        else
                        {
                            MessageBox.Show("ไม่สามารถบันทึกข้อมูลได้");
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                SetMsgException(ex);
            }
        }

        private void USmartID()
        {
            if (txtSmartBankCode.Text != "" && txtSmartBankName.Text == "")
            {
                txtSmartBankCode.Focus();
                throw new Exception("กรุณาระบุชื่อธนาคาร");
            }

            if (txtSmartBranchCode.Text != "" && txtSmartBranchName.Text == "")
            {
                txtSmartBranchCode.Focus();
                throw new Exception("กรุณาระบุชื่อสาขาธนาคาร");
            }
            if (txtSmartAccNo.Text != "" && txtSmartAccNo.Text.Length < 10)
            {
                txtSmartAccNo.Focus();
                throw new Exception("เลขที่บัญชีมีค่าน้อยกว่า 10 ตัวอักษร");
            }


            bool chkSuscess = false;
            if (chbSmartIDTmn.Checked == false)
            {
                if ((txtSmartBankCode.Text != "") && (txtSmartBankName.Text != ""))
                {
                    if (txtSmartBranchCode.Text == "")
                    {
                        txtSmartBranchCode.Focus();
                        throw new Exception("กรุณาระบุสาขาธนาคาร");
                    }
                    if (txtSmartBranchName.Text == "")
                    {
                        txtSmartBranchName.Focus();
                        throw new Exception("กรุณาระบุชื่อธนาคาร");
                    }
                    if (txtSmartAccName.Text == "")
                    {
                        txtSmartAccName.Focus();
                        throw new Exception("กรุณาระบุชื่อบัญชี");
                    }
                    if (txtSmartAccNo.Text == "")
                    {
                        txtSmartAccNo.Focus();
                        throw new Exception("กรุณาระบุเลขที่บัญชี");
                    }
                    if (cmbSmartAccOwner.SelectedValue.ToString() == "")
                    {
                        cmbSmartAccOwner.Focus();
                        throw new Exception("กรุณาระบุเจ้าของบัญชี");
                    }
                    String SmartDepositType = cmbSmartDepositType.SelectedValue == null ? "" : cmbSmartDepositType.SelectedValue.ToString();
                    if (SmartDepositType == "")
                    {
                        cmbSmartDepositType.Focus();
                        throw new Exception("กรุณาระบุประเภทบัญชี");
                    }
                    if (cmbSmartAccOwner.SelectedValue.ToString() == "P" && txtParentIdcardNo.Text == "")
                    {
                        cmbSmartAccOwner.Focus();
                        throw new Exception("ท่านเลือกผู้ปกครองเป็นเจ้าของบัญชี กรุณาระบุข้อมูลผู้ปกครอง");
                    }
                    if (cmbSmartAccOwner.SelectedValue.ToString() == "C" && txtIdcardNo.Text == "")
                    {
                        cmbSmartAccOwner.Focus();
                        throw new Exception("ท่านเลือกผู้ปกครองเป็นเจ้าของบัญชี กรุณาระบุข้อมูลผู้เอาประกัน");
                    }

                    chkSuscess = true;
                }
            }
            else if (chbSmartIDTmn.Checked == true)
            {
                if (txtSmartAccName.Text == "")
                {
                    txtSmartAccName.Focus();
                    throw new Exception("กรุณาระบุชื่อบัญชี");
                }
                if (cmbSmartAccOwner.SelectedValue.ToString() == "")
                {
                    cmbSmartAccOwner.Focus();
                    throw new Exception("กรุณาระบุเจ้าของบัญชี");
                }
                if (cmbSmartAccOwner.SelectedValue.ToString() == "P" && txtParentIdcardNo.Text == "")
                {
                    cmbSmartAccOwner.Focus();
                    throw new Exception("ท่านเลือกผู้ปกครองเป็นเจ้าของบัญชี กรุณาระบุข้อมูลผู้ปกครอง");
                }
                if (cmbSmartAccOwner.SelectedValue.ToString() == "C" && txtIdcardNo.Text == "")
                {
                    cmbSmartAccOwner.Focus();
                    throw new Exception("ท่านเลือกลูกค้าเป็นเจ้าของบัญชี กรุณาระบุข้อมูลผู้เอาประกัน");
                }
                chkSuscess = true;
            }


            if (chkSuscess == true)
            {
                if (chbSmartIDApproved.Checked == false && chbSmartIDTmn.Checked == false)
                {
                    chbSmartIDApproved.Focus();
                    throw new Exception("กรุณาระบุตรวจสอบบัญชี Smart แล้ว หรือ ไม่อนุมัติ อย่างใดอย่างหนึ่งเท่านั้น");
                }

                if (chbSmartIDApproved.Checked == true && chbSmartIDTmn.Checked == true)
                {
                    chbSmartIDApproved.Focus();
                    throw new Exception("กรุณาระบุตรวจสอบบัญชี Smart แล้ว หรือ ไม่อนุมัติ อย่างใดอย่างหนึ่งเท่านั้น");
                }
                //if (txtSmartBranchCode.Text == "")
                //{
                //    txtSmartBranchCode.Focus();
                //    throw new Exception("กรุณาระบุสาขาธนาคาร");
                //}
                //if (txtSmartBranchName.Text == "")
                //{
                //    txtSmartBranchName.Focus();
                //    throw new Exception("กรุณาระบุชื่อธนาคาร");
                //}
                //if (txtSmartAccName.Text == "")
                //{
                //    txtSmartAccName.Focus();
                //    throw new Exception("กรุณาระบุชื่อบัญชี");
                //}
                //if (txtSmartAccNo.Text == "")
                //{
                //    txtSmartAccNo.Focus();
                //    throw new Exception("กรุณาระบุเลขที่บัญชี");
                //}
                //if (cmbSmartAccOwner.SelectedValue.ToString() == "")
                //{
                //    cmbSmartAccOwner.Focus();
                //    throw new Exception("กรุณาระบุเจ้าของบัญชี");
                //}
                //if (cmbSmartDepositType.SelectedValue.ToString() == "")
                //{
                //    cmbSmartDepositType.Focus();
                //    throw new Exception("กรุณาระบุประเภทบัญชี");
                //}
                //if (cmbSmartAccOwner.SelectedValue.ToString() == "P" && txtParentIdCard.Text == "")
                //{
                //    cmbSmartAccOwner.Focus();
                //    throw new Exception("ท่านเลือกผู้ปกครองเป็นเจ้าของบัญชี กรุณาระบุข้อมูลผู้ปกครอง");
                //}
                //if (cmbSmartAccOwner.SelectedValue.ToString() == "C" && txtIdCardNo.Text == "")
                //{
                //    cmbSmartAccOwner.Focus();
                //    throw new Exception("ท่านเลือกผู้ปกครองเป็นเจ้าของบัญชี กรุณาระบุข้อมูลผู้เอาประกัน");
                //}

                PAR_PA_APPLICATION_ID.APPLICATION_Collection.First().SMART_ID_Collection = new NewBisPASvcRef.U_SMART_ID_Collection();
                var uSmartID = new NewBisPASvcRef.U_SMART_ID();
                uSmartID.USMART_ID = PK_SMART_ID;
                uSmartID.UAPP_ID = PAR_PA_APPLICATION_ID.APPLICATION_Collection.First().UAPP_ID;
                uSmartID.UACCOUNT_ID = PK_SMART_ACCOUNT_ID;
                uSmartID.DOCUMENT_DT = Utility.StringToDateTime(txtAppSignDt.Text, "BU");
                uSmartID.RECEIVE_DT = Utility.StringToDateTime(txtAppOfcRcvDt.Text, "BU");
                uSmartID.TRANACTION_DT = Utility.StringToDateTime(txtAppSysDt.Text, "BU");
                uSmartID.DOCUMENT_SOURCE = "A";
                uSmartID.FSYSTEM_DT = DateTime.Now;
                uSmartID.UPD_DT = DateTime.Now;
                uSmartID.UPD_ID = UserID;
                uSmartID.ObjId = 1;
                uSmartID.APPROVE_FLG = chbSmartIDApproved.Checked == true ? 'Y' : 'N';


                String remarkSmart = cmbSmartRemark.SelectedValue == null ? "" : cmbSmartRemark.SelectedValue.ToString();

                if (chbSmartIDTmn.Checked == true)
                {
                    uSmartID.TMN = 'Y';
                    uSmartID.SMART_ID_TMN = new NewBisPASvcRef.U_SMART_ID_TMN();
                    uSmartID.SMART_ID_TMN.USMART_ID = uSmartID.USMART_ID;
                    uSmartID.SMART_ID_TMN.TMN_DT = DateTime.Now;
                    uSmartID.SMART_ID_TMN.TMN_ID = uSmartID.UPD_ID;
                    uSmartID.FOLLOW_SMART_CODE = remarkSmart == "" ? null : remarkSmart;
                }
                else
                {
                    uSmartID.TMN = 'N';
                    uSmartID.FOLLOW_SMART_CODE = null;
                }

                PAR_PA_APPLICATION_ID.APPLICATION_Collection.First().SMART_ID_Collection.Add(uSmartID);

                var uAccIDSmart = new NewBisPASvcRef.U_ACCOUNT_ID();
                uAccIDSmart.UACCOUNT_ID = uSmartID.UACCOUNT_ID;
                uAccIDSmart.ACC_BANK = txtSmartBankCode.Text.Trim() == null ? null : txtSmartBankCode.Text.Trim();
                uAccIDSmart.ACC_BRANCH = txtSmartBranchCode.Text.Trim() == null ? null : txtSmartBranchCode.Text.Trim();
                uAccIDSmart.ACC_NO = txtSmartAccNo.Text.Trim() == null ? null : txtSmartAccNo.Text.Trim();
                uAccIDSmart.ACC_DEPOSIT_TYPE = cmbSmartDepositType.SelectedValue.ToString() == "" ? null : cmbSmartDepositType.SelectedValue.ToString();
                uAccIDSmart.ACC_NAME = txtSmartAccName.Text.Trim() == null ? null : txtSmartAccName.Text.Trim();
                uAccIDSmart.ACC_NAME_TYPE = "P";
                uAccIDSmart.APPROVE_DT = null;
                uAccIDSmart.APPROVE_ID = null;
                uAccIDSmart.FSYSTEM_DT = DateTime.Now;
                uAccIDSmart.UPD_DT = DateTime.Now;
                uAccIDSmart.UPD_ID = UserID;

                uAccIDSmart.SMART_ID = new NewBisPASvcRef.U_SMART_ID();
                uAccIDSmart.SMART_ID = uSmartID;

                uAccIDSmart.ACCOUNT_PERSONAL = new NewBisPASvcRef.U_ACCOUNT_PERSONAL();
                uAccIDSmart.ACCOUNT_PERSONAL.UACCOUNT_ID = uSmartID.UACCOUNT_ID;


                if (cmbSmartAccOwner.SelectedValue.ToString() == "P")
                {
                    uAccIDSmart.ACCOUNT_PERSONAL.PRENAME = txtParentPrename.Text.Trim() == null ? null : txtParentPrename.Text.Trim();
                    uAccIDSmart.ACCOUNT_PERSONAL.NAME = txtParentName.Text.Trim() == null ? null : txtParentName.Text.Trim();
                    uAccIDSmart.ACCOUNT_PERSONAL.SURNAME = txtParentSurname.Text.Trim() == null ? null : txtParentSurname.Text.Trim();
                    uAccIDSmart.ACCOUNT_PERSONAL.BIRTH_DT = Utility.StringToDateTime(txtParentBirthDt.Text, "BU");

                    uAccIDSmart.ACCOUNT_PERSONAL.SEX = cmbParentGender.SelectedValue.ToString() == "" ? null : cmbParentGender.SelectedValue.ToString();

                    if (cmbParentCardType.SelectedValue.ToString() == "1")
                    {
                        uAccIDSmart.ACCOUNT_PERSONAL.IDCARD_NO = txtParentIdcardNo.Text.Trim() == "" ? null : txtParentIdcardNo.Text.Trim();
                        uAccIDSmart.ACCOUNT_PERSONAL.PASSPORT = null;
                    }
                    else
                    {
                        uAccIDSmart.ACCOUNT_PERSONAL.IDCARD_NO = null;
                        uAccIDSmart.ACCOUNT_PERSONAL.PASSPORT = txtParentIdcardNo.Text.Trim() == "" ? null : txtParentIdcardNo.Text.Trim();
                    }
                }

                if (cmbSmartAccOwner.SelectedValue.ToString() == "C")
                {
                    uAccIDSmart.ACCOUNT_PERSONAL.PRENAME = txtPrename.Text.Trim() == null ? null : txtPrename.Text.Trim();
                    uAccIDSmart.ACCOUNT_PERSONAL.NAME = txtName.Text.Trim() == null ? null : txtName.Text.Trim();
                    uAccIDSmart.ACCOUNT_PERSONAL.SURNAME = txtSurname.Text.Trim() == null ? null : txtSurname.Text.Trim();
                    uAccIDSmart.ACCOUNT_PERSONAL.BIRTH_DT = Utility.StringToDateTime(txtBirthDt.Text, "BU");

                    uAccIDSmart.ACCOUNT_PERSONAL.SEX = cmbSex.SelectedValue.ToString() == "" ? null : cmbSex.SelectedValue.ToString();
                    if (cmbCardType.SelectedValue.ToString() == "1")
                    {
                        uAccIDSmart.ACCOUNT_PERSONAL.IDCARD_NO = txtIdcardNo.Text.Trim() == "" ? null : txtIdcardNo.Text.Trim();
                        uAccIDSmart.ACCOUNT_PERSONAL.PASSPORT = null;
                    }
                    else
                    {
                        uAccIDSmart.ACCOUNT_PERSONAL.IDCARD_NO = null;
                        uAccIDSmart.ACCOUNT_PERSONAL.PASSPORT = txtIdcardNo.Text.Trim() == "" ? null : txtIdcardNo.Text.Trim();
                    }
                }


                uAccountIDColl.Add(uAccIDSmart);
            }
        }

        private void btntempaccount_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            try
            {
                long programGroupId;
                string StartCommand = "101";

                RegistryKey localMachineReg = Registry.LocalMachine;
                RegistryKey softwareReg = localMachineReg.OpenSubKey("Software", true);
                RegistryKey bangkokLifeAssuranceReg = softwareReg.OpenSubKey("Bangkok Life Assurance", true);
                RegistryKey blaWinAppMenuReg = bangkokLifeAssuranceReg.OpenSubKey("BlaWinAppMenu", true);

                blaWinAppMenuReg.SetValue("UserID", UserID, RegistryValueKind.String);
                blaWinAppMenuReg.SetValue("StartCommand", StartCommand, RegistryValueKind.String);
                blaWinAppMenuReg.SetValue("appNoReg", txtAppNo.Text, RegistryValueKind.String);
                blaWinAppMenuReg.SetValue("idCardReg", txtIdcardNo.Text, RegistryValueKind.String);
                blaWinAppMenuReg.SetValue("cardTypeReg", cmbCardType.SelectedValue.ToString(), RegistryValueKind.String);  //1: idcard, 2:passport

                blaWinAppMenuReg.Close();
                bangkokLifeAssuranceReg.Close();
                softwareReg.Close();
                localMachineReg.Close();

                if (long.TryParse(StartCommand, out programGroupId))
                {
                    SecuritySvcRef.ProgramGroup pg = null;
                    SecuritySvcRef.ProcessResult pr;
                    using (SecuritySvcRef.SecurityServiceClient client = new SecuritySvcRef.SecurityServiceClient())
                    {
                        pr = client.GetProgramGroupById(out pg, Convert.ToInt64(StartCommand));
                    }
                    if (pr.Successed)
                    {
                        if (pg != null)
                        {

                            string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.StartMenu);

                            System.IO.DirectoryInfo appDataDirInfo = new System.IO.DirectoryInfo(appDataPath);

                            System.IO.DirectoryInfo startMenuDir = null;

                            startMenuDir = new System.IO.DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.StartMenu));

                            System.IO.FileInfo[] files = startMenuDir.GetFiles("ระบบบริการกรมธรรม์.appref-ms", System.IO.SearchOption.AllDirectories);

                            if (files.Length == 0)
                                MessageBox.Show("กรุณาติดตั้ง" + pg.Description);
                            else
                            {
                                Process.Start(files[0].FullName);
                            }

                        }
                    }
                }
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }

        }

        private void btnAppRemark_Click(object sender, EventArgs e)
        {

            try
            {
                if (PAR_PA_APPLICATION_ID == null && PAR_PA_APPLICATION_ID.APP_ID != null)
                {
                    throw new Exception("ไม่สามารถดูข้อมูลหรือบันทึกข้อมูลได้เนื่องจากยังไม่มีข้อมูลใบคำขอ");
                }

                var formUAppRemark = new FormUAppRemark();
                formUAppRemark.USER_ID = UserID;


                if (U_APP_REMARK_COLL != null && U_APP_REMARK_COLL.Count() > 0)
                {
                    formUAppRemark.PAR_U_APP_REMARK_COLL = new NewBisPASvcRef.U_APP_REMARK_Collection();
                    formUAppRemark.PAR_U_APP_REMARK_COLL.AddRange(U_APP_REMARK_COLL);
                }
                else
                {
                    formUAppRemark.PAR_U_APP_REMARK_COLL = null;
                }

                formUAppRemark.ShowDialog();

                U_APP_REMARK_COLL = new NewBisPASvcRef.U_APP_REMARK_Collection();

                if (formUAppRemark.PAR_U_APP_REMARK_COLL != null && formUAppRemark.PAR_U_APP_REMARK_COLL.Count() > 0)
                {
                    U_APP_REMARK_COLL.AddRange(formUAppRemark.PAR_U_APP_REMARK_COLL);
                }

                if (U_APP_REMARK_COLL != null && U_APP_REMARK_COLL.Count() > 0)
                {
                    btnAppRemark.BackColor = Color.FromArgb(255, 128, 128);
                }
                else
                {
                    btnAppRemark.BackColor = Color.Transparent;
                }

            }
            catch (Exception ex)
            {
                SetMsgException(ex);
            }

        }

        private void txtBankrupCus_TextChanged(object sender, EventArgs e)
        {

        }

        private void memoMainForm1_Load(object sender, EventArgs e)
        {

        }

        private NewBisPASvcRef.U_AP_ID CreateUAPID(NewBisPASvcRef.W_SUMMARY wSummaryObj)
        {
            if (txtAPAnswerLimitDate.Text == "")
            {
                throw new Exception("กรุณาระบุวันที่จำกัดการรอคอยการตอบกลับของสถานะ AP");
            }
            var uAPIDObj = new NewBisPASvcRef.U_AP_ID();
            uAPIDObj.UAP_ID = PK_UAP_ID;
            uAPIDObj.SUMMARY_ID = wSummaryObj.SUMMARY_ID;
            if (PK_UAP_ID == null)
            {
                uAPIDObj.AP_TRN_DT = DateTime.Now;
            }
            else
            {
                uAPIDObj.AP_TRN_DT = UAP_TRN_DT;
            }

            uAPIDObj.ANSWER_LIMIT_DT = Utility.StringToDateTime(txtAPAnswerLimitDate.Text, "BU");
            uAPIDObj.UPD_ID = UserID;
            if (ckbAPTmn.Checked == true)
            {
                uAPIDObj.TMN = 'Y';
            }
            else
            {
                uAPIDObj.TMN = 'N';
            }
            return uAPIDObj;
        }

        private void cmbSummrySubStatus_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (cmbSummryStatus.SelectedValue != null)
            {
                String[] statusArr = { "AP", "CO", "MO", "MD", "IF" };

                if (statusArr.Contains(cmbSummryStatus.SelectedValue.ToString()) && (!(cmbSummryStatus.SelectedValue.ToString() == "AP" && cmbSummrySubStatus.SelectedValue.ToString() == "AS")))
                {
                    chkSms.Checked = true;
                }
                else
                {
                    chkSms.Checked = false;
                }
            }

            if (cmbSummryStatus.SelectedValue.ToString() == "AP" && cmbSummrySubStatus.SelectedValue.ToString() == "AP")
            {
                gbAPDate.Visible = true;

                String plBlock = cmbPaidPlBlock.SelectedValue == null ? "" : cmbPaidPlBlock.SelectedValue.ToString();

                if (plBlock == "H" && txtPaidPlcode.Text == "019")
                {
                    DateTime? APAnswerLimitDate = DateTime.Now.AddDays(20);
                    txtAPAnswerLimitDate.Text = Utility.dateTimeToString(APAnswerLimitDate.Value, "dd/MM/yyyy", "BU");
                }
                else
                {
                    DateTime? APAnswerLimitDate = DateTime.Now.AddDays(15);
                    txtAPAnswerLimitDate.Text = Utility.dateTimeToString(APAnswerLimitDate.Value, "dd/MM/yyyy", "BU");
                }
            }
            else
            {
                gbAPDate.Visible = false;
            }

        }

        private void txtAPAnswerLimitDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (txtAPAnswerLimitDate.Text.Trim() != "")
            {
                int? age = txtStAge.Text == "" ? (Int32?)null : Convert.ToInt32(txtStAge.Text);
                ChkDateForTextBox(txtAPAnswerLimitDate.Text, "วันที่ตอบกลับจดหมายเงินขาด", txtAPAnswerLimitDate);
                CheckAgeForAPStatus(age, txtBirthDt.Text, txtAPAnswerLimitDate.Text);
            }
        }

        private void txtAPAnswerLimitDate_Leave(object sender, EventArgs e)
        {
            if (txtAPAnswerLimitDate.Text.Trim() != "")
            {
                int? age = txtStAge.Text == "" ? (Int32?)null : Convert.ToInt32(txtStAge.Text);
                ChkDateForTextBox(txtAPAnswerLimitDate.Text, "วันที่ตอบกลับจดหมายเงินขาด", txtAPAnswerLimitDate);
                CheckAgeForAPStatus(age, txtBirthDt.Text, txtAPAnswerLimitDate.Text);
            }
        }

        private void CheckAgeForAPStatus(int? nowAge, string birthDaty, string dateInput)
        {


            #region "ตรวจสอบอายุ"
            DateTime? cusBirthDate = Utility.StringToDateTime(birthDaty, "BU");
            DateTime? dateInputDateTime = Utility.StringToDateTime(dateInput, "BU");

            Int64? age = null;
            NewBISSvcRef.ProcessResult prAge = new NewBISSvcRef.ProcessResult();
            using (NewBISSvcRef.NewBISSvcClient client = new NewBISSvcRef.NewBISSvcClient())
            {
                age = client.GetAge(out prAge, cusBirthDate.Value, DateTime.Now, DateTime.Now);
                if (prAge.Successed == false)
                {
                    throw new Exception(prAge.ErrorMessage);
                }
                if (nowAge != age)
                {
                    MessageBox.Show("เนื่องจากตอนออก AP ทำให้อายุเปลี่ยนมีผลกับเบี้ยประกัน จากเดิม อายุ : " + nowAge.ToString() + " ปี ของใหม่ อายุ : " + age.ToString() + " ปี ณ วันออก AP จึงแจ้งให้ผู้ใช้งานทราบเพื่อเปลี่ยนวันเริ่มคุ่มครอง");
                }

                if (dateInputDateTime != null)
                {
                    age = client.GetAge(out prAge, cusBirthDate.Value, dateInputDateTime.Value, dateInputDateTime.Value);
                    if (prAge.Successed == false)
                    {
                        throw new Exception(prAge.ErrorMessage);
                    }
                    if (nowAge != age)
                    {
                        MessageBox.Show("เนื่องจากระบบนำวันที่ตอบรับข้อมูลไปคำนวนกับวันเกิด ทำให้อายุลูกค้าเปลี่ยนในวันที่ตอบรับข้อมูล จากเดิมอายุ : " + nowAge.ToString() + " ปี ถ้าเป็นวันที่ตอบรับจะได้อายุ : " + age.ToString() + " ปี จึงแจ้งให้ผู้ใช้ทราบเพื่อเปลี่ยนวันที่เริ่มคุ้มครอง");
                    }
                }
            }

            #endregion

        }

        private void memoMainForm1_MouseClick(object sender, MouseEventArgs e)
        {
            //  MessageBox.Show("test");
        }

        private void cmbOcpGroup_SelectionChangeCommitted(object sender, EventArgs e)
        {

            try
            {
                if (cmbOcpGroup.SelectedValue != null)
                {
                    PrepareOcupationInfo();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }



        }


        private void PrepareOcupationInfo()
        {


            String groupCode = cmbOcpGroup.SelectedValue.ToString();
            String usedMoter = "";
            if (ckbMotercycleUsed.Checked)
            {
                usedMoter = "Y";
            }
            else
            {
                usedMoter = "N";
            }

            txtParentOcpType.Text = "";
            txtParentOcpClass.Text = "";

            var occupationColl = new NewBisPASvcRef.OCCUPATION_DATA_COLLECTION();
            var occupationArr = new NewBisPASvcRef.OCCUPATION_DATA[PAR_OCCUPATION_COLL.Count];
            PAR_OCCUPATION_COLL.CopyTo(occupationArr);
            var obj = new NewBisPASvcRef.OCCUPATION_DATA();
            obj.CODE = "";
            obj.OCP_TYPE_NAME = "ระบุชั้นอาชีพ";
            occupationColl.Add(obj);
            if (groupCode == "")
            {
                var GetData = from getData in occupationArr
                              where getData.MOTERCYCLE_USED == usedMoter
                              orderby getData.OCP_TYPE ascending, getData.OCP_CLASS ascending
                              select getData;
                if (GetData != null && GetData.Count() > 0)
                {
                    occupationColl.AddRange(GetData.ToArray());
                }
            }
            else
            {
                var GetData = from getData in occupationArr
                              where getData.MOTERCYCLE_USED == usedMoter && getData.OCP_GROUP == groupCode
                              orderby getData.OCP_TYPE ascending, getData.OCP_CLASS ascending
                              select getData;
                if (GetData != null && GetData.Count() > 0)
                {
                    occupationColl.AddRange(GetData.ToArray());
                }
            }

            if (occupationColl != null && occupationColl.Count() > 0)
            {
                cmbocpDesc.DataSource = occupationColl;
                cmbocpDesc.DisplayMember = "OCP_TYPE_NAME";
                cmbocpDesc.ValueMember = "CODE";
                cmbocpDesc.SelectedValue = "";

                AutoCompleteStringCollection data = new AutoCompleteStringCollection();
                foreach (var item in occupationColl)
                {
                    data.Add(item.OCP_TYPE_NAME);
                }
                cmbocpDesc.AutoCompleteMode = AutoCompleteMode.Suggest;
                cmbocpDesc.AutoCompleteSource = AutoCompleteSource.CustomSource;
                cmbocpDesc.AutoCompleteCustomSource = data;
            }



        }

        private void cmbOcpGroup_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                PrepareOcupationInfo();
            }
        }

        private void gbUnderwrite_Enter(object sender, EventArgs e)
        {

        }

        private void btnSaveSummaryDetail_Click(object sender, EventArgs e)
        {
            try
            {
                if (PAR_END_PROCESS == 'Y')
                {
                    if (PAR_W_SUMMARY_DETAIL_COLL != null && PAR_W_SUMMARY_DETAIL_COLL.Count() > 0)
                    {
                        if (PAR_W_SUMMARY.SUMMARY_ID == null)
                        {
                            throw new Exception("ไม่สามรถบันทึกข้อมูลได้เนื่องจากไม่มี PK_SUMMARY_ID");
                        }
                        NewBISSvcRef.W_SUMMARY_DETAIL_Collection wSummaryDetailColl = new NewBISSvcRef.W_SUMMARY_DETAIL_Collection();
                        int i = 0;
                        foreach (var obj in PAR_W_SUMMARY_DETAIL_COLL)
                        {
                            //i = i + 1;
                            if (PAR_W_SUMMARY.SUMMARY_ID == obj.SUMMARY_ID)
                            {
                                NewBISSvcRef.W_SUMMARY_DETAIL wSummaryDetailObj = new NewBISSvcRef.W_SUMMARY_DETAIL();
                                wSummaryDetailObj.SEQ = obj.SEQ;
                                wSummaryDetailObj.SUMMARY_ID = PAR_W_SUMMARY.SUMMARY_ID;
                                wSummaryDetailObj.DETAIL = obj.DETAIL;
                                wSummaryDetailColl.Add(wSummaryDetailObj);
                            }
                        }
                        if (wSummaryDetailColl != null && wSummaryDetailColl.Any())
                        {
                            NewBISSvcRef.ProcessResult pr = new NewBISSvcRef.ProcessResult();
                            using (NewBISSvcRef.NewBISSvcClient client = new NewBISSvcRef.NewBISSvcClient())
                            {
                                pr = client.RemoveWSummaryDetailAll(wSummaryDetailColl, PAR_W_SUMMARY.SUMMARY_ID);
                                if (pr.Successed == false)
                                {
                                    throw new Exception(pr.ErrorMessage);
                                }
                            }
                            MessageBox.Show("บันทึกข้อมูลรายละเอียดเรียบร้อย");
                        }
                        return;
                    }
                }
                else
                {
                    throw new Exception("ไม่สามรถบันทึกข้อมูลได้เนื่องจากงานยังไม่จบ Process");
                }
            }
            catch (Exception ex)
            {
                SetMsgException(ex);
            }
        }

        private void summaryDetailForm1_Load(object sender, EventArgs e)
        {

        }

        private void row1panel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void cmbPolicyDocmentType_SelectedIndexChanged(object sender, EventArgs e)
        {
            var policyDocmentType = cmbPolicyDocmentType.SelectedValue.ToString();
            if (!string.IsNullOrEmpty(policyDocmentType))
            {
                if (policyDocmentType.Equals("E") || policyDocmentType.Equals("A"))
                {
                    lblEpolicySendType.Enabled = true;
                    cmbEPolicySending.Enabled = true;
                }
                else
                {
                    lblEpolicySendType.Enabled = false;
                    cmbEPolicySending.Enabled = false;
                }
            }
        }



        private void cmbParentAddressType_SelectedIndexChanged(object sender, EventArgs e)
        {


            cmbParentAddressType.BackColor = Color.White;
            txtParentAddressName.BackColor = Color.White;
            txtParentAddressNumber.BackColor = Color.White;
            txtParentMooh.BackColor = Color.White;
            txtParentSoi.BackColor = Color.White;
            txtParentRoad.BackColor = Color.White;
            cmbParentProvince.BackColor = Color.White;
            cmbParentAmphur.BackColor = Color.White;
            cmbParentTambol.BackColor = Color.White;
            txtParentZipcode.BackColor = Color.White;
            txtParentPhoneNumber.BackColor = Color.White;
            txtParentEmail.BackColor = Color.White;
            txtParentMbPhone.BackColor = Color.White;


            if (cmbParentAdrType.SelectedValue.ToString() != "" && IsAutoCheckOldAddressData)
            {
                if (PARENT_PA_NAME_ID != null && PARENT_PA_NAME_ID != 0)
                {
                    NewBisPASvcRef.ProcessResult prAdr = new NewBisPASvcRef.ProcessResult();
                    NewBisPASvcRef.P_ADDRESS_ID_COLLECTION adrColl = new NewBisPASvcRef.P_ADDRESS_ID_COLLECTION();
                    NewBisPASvcRef.ProcessResult prPnameID = new NewBisPASvcRef.ProcessResult();
                    using (NewBisPASvcRef.NewBisPASvcClient client = new NewBisPASvcRef.NewBisPASvcClient())
                    {
                        try
                        {
                            prAdr = client.GetOldAddressByNameID(out adrColl, PARENT_PA_NAME_ID.Value);
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }

                    }
                    if (prAdr.Successed == true)
                    {
                        if (adrColl != null)
                        {

                            ShowOldAdrressByNameIDForm ShowOldAdrressByNameIDForm = new ShowOldAdrressByNameIDForm();
                            ShowOldAdrressByNameIDForm.P_ADDRESS_ID_COLL = new NewBisPASvcRef.P_ADDRESS_ID_COLLECTION();
                            ShowOldAdrressByNameIDForm.P_ADDRESS_ID_COLL = adrColl;
                            ShowOldAdrressByNameIDForm.ADDRESS_ID = ADDRESS_ID;
                            ShowOldAdrressByNameIDForm.ShowDialog();

                            if (ShowOldAdrressByNameIDForm.P_ADDRESS_ID_OBJ != null && ShowOldAdrressByNameIDForm.P_ADDRESS_ID_OBJ.ADDRESS_ID != null)
                            {
                                var formAddress = ShowOldAdrressByNameIDForm.P_ADDRESS_ID_OBJ;
                                cmbParentAdrType.BackColor = Color.FromArgb(255, 192, 192);
                                PARENT_ADDRESS_ID = Convert.ToInt64(formAddress.ADDRESS_ID);
                                cmbParentAdrType.SelectedValue = "1";

                                if (txtParentAddressName.Text.Trim() == "")
                                {
                                    txtParentAddressName.Text = formAddress.ADDRESS_NAME;
                                }
                                else
                                {
                                    String addressName = formAddress.ADDRESS_NAME == null ? "" : formAddress.ADDRESS_NAME.Trim();
                                    if (txtParentAddressName.Text.Trim() != addressName)
                                    {
                                        txtParentAddressName.BackColor = Color.FromArgb(255, 192, 192);
                                    }
                                    else
                                    {
                                        txtParentAddressName.BackColor = Color.White;
                                    }
                                }

                                if (txtParentAddressNumber.Text.Trim() == "")
                                {
                                    txtParentAddressNumber.Text = formAddress.ADDRESS_NUMBER;
                                }
                                else
                                {
                                    String addressNumber = formAddress.ADDRESS_NUMBER == null ? "" : formAddress.ADDRESS_NUMBER.Trim();
                                    if (txtParentAddressNumber.Text.Trim() != addressNumber)
                                    {
                                        txtParentAddressNumber.BackColor = Color.FromArgb(255, 192, 192);

                                    }
                                    else
                                    {
                                        txtParentAddressNumber.BackColor = Color.White;
                                    }
                                }

                                if (txtParentMooh.Text.Trim() == "")
                                {
                                    txtParentMooh.Text = formAddress.MOOH;
                                }
                                else
                                {
                                    String mooh = formAddress.MOOH == null ? "" : formAddress.MOOH.Trim();
                                    if (txtParentMooh.Text.Trim() != mooh)
                                    {
                                        txtParentMooh.BackColor = Color.FromArgb(255, 192, 192);
                                    }
                                    else
                                    {
                                        txtParentMooh.BackColor = Color.White;
                                    }
                                }

                                if (txtParentSoi.Text.Trim() == "")
                                {
                                    txtParentSoi.Text = formAddress.SOI;
                                }
                                else
                                {
                                    String soi = formAddress.SOI == null ? "" : formAddress.SOI.Trim();
                                    if (txtParentSoi.Text.Trim() != soi)
                                    {
                                        txtParentSoi.BackColor = Color.FromArgb(255, 192, 192);
                                    }
                                    else
                                    {
                                        txtParentSoi.BackColor = Color.White;
                                    }
                                }

                                if (txtParentRoad.Text.Trim() == "")
                                {
                                    txtParentRoad.Text = formAddress.ROAD;
                                }
                                else
                                {
                                    String road = formAddress.ROAD == null ? "" : formAddress.ROAD.Trim();
                                    if (txtParentRoad.Text.Trim() != road)
                                    {
                                        txtParentRoad.BackColor = Color.FromArgb(255, 192, 192);
                                    }
                                    else
                                    {
                                        txtParentRoad.BackColor = Color.White;
                                    }
                                }

                                String province = formAddress.PROVINCE == null ? "" : formAddress.PROVINCE.Trim();
                                if (cmbParentProvince.Text.Trim() == "ระบุจังหวัดที่ต้องการ" || cmbParentProvince.Text.Trim() == "")
                                {
                                    cmbParentProvince.Text = province;
                                }
                                else
                                {
                                    if (cmbParentProvince.Text.Trim() != province)
                                    {
                                        cmbParentProvince.BackColor = Color.FromArgb(255, 192, 192);
                                    }
                                    else
                                    {
                                        cmbParentProvince.BackColor = Color.White;
                                    }
                                }

                                String amphur = formAddress.AMPHUR == null ? "" : formAddress.AMPHUR.Trim();
                                if (cmbParentAmphur.Text.Trim() == "ระบุอำเภอที่ต้องการ" || cmbParentAmphur.Text.Trim() == "")
                                {
                                    cmbParentAmphur.Text = amphur;
                                }
                                else
                                {
                                    if (cmbParentAmphur.Text.Trim() != amphur)
                                    {
                                        cmbParentAmphur.BackColor = Color.FromArgb(255, 192, 192);
                                    }
                                    else
                                    {
                                        cmbParentAmphur.BackColor = Color.White;
                                    }
                                }

                                String tambol = formAddress.TAMBOL == null ? "" : formAddress.TAMBOL.Trim();
                                if (cmbParentTambol.Text.Trim() == "ระบุตำบลที่ต้องการ" || cmbParentTambol.Text.Trim() == "")
                                {
                                    cmbParentTambol.Text = tambol;
                                }
                                else
                                {
                                    if (cmbParentTambol.Text.Trim() != tambol)
                                    {
                                        cmbParentTambol.BackColor = Color.FromArgb(255, 192, 192);
                                    }
                                    else
                                    {
                                        cmbParentTambol.BackColor = Color.White;
                                    }
                                }

                                if (txtParentZipcode.Text.Trim() == "")
                                {
                                    txtParentZipcode.Text = formAddress.ZIP_CODE;
                                }
                                else
                                {
                                    String zipcode = formAddress.ZIP_CODE == null ? "" : formAddress.ZIP_CODE.Trim();
                                    if (txtParentZipcode.Text.Trim() != zipcode)
                                    {
                                        txtParentZipcode.BackColor = Color.FromArgb(255, 192, 192);
                                    }
                                    else
                                    {
                                        txtParentZipcode.BackColor = Color.White;
                                    }
                                }

                                if (txtParentPhoneNumber.Text.Trim() == "")
                                {
                                    txtParentPhoneNumber.Text = formAddress.PHONE_NUMBER;
                                }
                                else
                                {
                                    String phoneNumber = formAddress.PHONE_NUMBER == null ? "" : formAddress.PHONE_NUMBER.Trim();
                                    if (txtParentPhoneNumber.Text.Trim() != phoneNumber)
                                    {
                                        txtParentPhoneNumber.BackColor = Color.FromArgb(255, 192, 192);
                                    }
                                    else
                                    {
                                        txtParentPhoneNumber.BackColor = Color.White;
                                    }
                                }
                            }
                            else
                            {
                                cmbParentAdrType.BackColor = Color.White;
                                PARENT_ADDRESS_ID = 0;
                                cmbParentAdrType.SelectedValue = "2";
                            }
                        }
                    }
                }
                else
                {
                    PARENT_ADDRESS_ID = 0;
                    cmbParentAdrType.SelectedValue = "2";
                }
            }

        }

        private void txtEmail_TextChanged(object sender, EventArgs e)
        {
            var channelType = cmbChannelType.SelectedValue.ToString();
            if (channelType == "PF")
            {
                if (!string.IsNullOrEmpty(txtEmail.Text))
                {
                    cmbPolicyDocmentType.SelectedValue = "E";
                    cmbEPolicySending.SelectedValue = "C";
                }
                else
                {
                    cmbPolicyDocmentType.SelectedValue = "D";
                    cmbEPolicySending.SelectedValue = "";
                }
            }
        }

        private void txtFreePlcode2_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void lblrevenue_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (PAR_PA_APPLICATION_ID.APP_ID != null)
            {
                string url = "http://mvcsvc.bla.co.th/EDOCMaintain/View/RevenueAppLink?userID=" + UserID + "&appID=" + PAR_PA_APPLICATION_ID.APP_ID + "&source=NB";
                System.Diagnostics.Process.Start(url);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            btnHeadSave.Enabled = true;
        }


        private void SetAgentRelate()
        {

            try
            {
                if (txtSaleAgent.Text == "")
                {
                    MessageBox.Show("กรุณาระบุรหัสตัวแทนท่านต้องการ");
                    txtSaleAgent.Focus();
                    return;
                }
                else
                {
                    String workGroup = cmbWorkGroup.SelectedValue.ToString();
                    String agentCode = "";
                    String bblBranch = "";

                    if (workGroup == "BNC")
                    {
                        if (txtSaleAgent.Text.Trim().Length == 4)
                        {
                            agentCode = "";
                            bblBranch = txtSaleAgent.Text.Trim();
                        }
                        else
                        {
                            agentCode = txtSaleAgent.Text.Trim().PadLeft(8, '0');
                            bblBranch = "";
                        }
                    }
                    else
                    {
                        agentCode = txtSaleAgent.Text.Trim().PadLeft(8, '0');
                        bblBranch = "";
                    }

                    NewBisPASvcRef.ProcessResult pr = new NewBisPASvcRef.ProcessResult();
                    NewBisPASvcRef.SALE_AGENT_DATA saleAgentDataObj = new NewBisPASvcRef.SALE_AGENT_DATA();
                    using (NewBisPASvcRef.NewBisPASvcClient client = new NewBisPASvcRef.NewBisPASvcClient())
                    {
                        saleAgentDataObj = client.GetSaleAgent(out pr, agentCode, bblBranch);
                        if (pr.Successed == false)
                        {
                            throw new Exception(pr.ErrorMessage);
                        }
                    }

                    if (saleAgentDataObj != null && !string.IsNullOrEmpty(saleAgentDataObj.SALE_AGENT_CODE))
                    {
                        if (saleAgentDataObj.LICENSE_VALIDATE == "N")
                        {
                            txtSaleAgent.BackColor = Color.FromArgb(bgRed[0], bgRed[1], bgRed[2]);
                            txtSaleAgentName.BackColor = Color.FromArgb(bgRed[0], bgRed[1], bgRed[2]);
                            MessageBox.Show("เลขที่ตัวแทน " + saleAgentDataObj.SALE_AGENT_CODE.PadLeft(8, '0') + " บัตรหมดอายุ");
                        }
                        else
                        {
                            txtSaleAgent.BackColor = Color.FromArgb(bgYellow[0], bgYellow[1], bgYellow[2]);
                            txtSaleAgentName.BackColor = SystemColors.Control;
                        }

                        txtSaleAgent.Text = saleAgentDataObj.SALE_AGENT_CODE;
                        txtSaleAgentName.Text = saleAgentDataObj.SALE_AGENT_NAME;
                        txtSaleOfc.Text = saleAgentDataObj.OFFICE;
                        txtSaleAgentUpl.Text = saleAgentDataObj.SALE_AGENT_UPLINE_CODE;
                        txtSaleAgentUplName.Text = saleAgentDataObj.SALE_AGENT_UPLINE_NAME;

                        if (workGroup == "BNC")
                        {
                            txtLicenseAgent.Text = saleAgentDataObj.SALE_AGENT_UPLINE_CODE;
                            txtLicenseAgentName.Text = saleAgentDataObj.SALE_AGENT_UPLINE_NAME;
                            txtLicenseOfc.Text = saleAgentDataObj.OFFICE_UPLINE;
                        }
                        else
                        {
                            txtLicenseAgent.Text = saleAgentDataObj.SALE_AGENT_CODE;
                            txtLicenseAgentName.Text = saleAgentDataObj.SALE_AGENT_NAME;
                            txtLicenseOfc.Text = saleAgentDataObj.OFFICE;
                        }
                    }
                    else
                    {
                        txtSaleAgent.Text = "";
                        txtSaleAgentName.Text = "";
                        txtSaleAgentUpl.Text = "";
                        txtSaleAgentUplName.Text = "";
                        txtLicenseAgent.Text = "";
                        txtLicenseAgentName.Text = "";
                        txtSaleOfc.Text = "";
                        txtLicenseOfc.Text = "";
                        MessageBox.Show("ไม่พบข้อมูลตัวแทน");
                        return;
                    }

                }
            }
            catch (Exception ex)
            {
                SetMsgException(ex);
            }
        }



        private void BtnSearchPlanPaid_Click(object sender, EventArgs e)
        {
            try
            {
                String plBlock = cmbPaidPlBlock.SelectedValue == null ? "" : cmbPaidPlBlock.SelectedValue.ToString();

                if (plBlock == "")
                {
                    cmbPaidPlBlock.Focus();
                    throw new Exception("กรุณาระบุชนิดแบบประกัน");
                }
                if (txtPaidIsuDate.Text.Trim() == "")
                {
                    txtPaidIsuDate.Text = txtAppSignDt.Text.Trim();
                }
                if (cmbChannelType.SelectedValue == null || cmbChannelType.SelectedValue.ToString() == "")
                {
                    cmbChannelType.Focus();
                    throw new Exception("กรุณาระบุช่องทางการขายที่ท่านต้องการ");
                }

                DateTime? isuDate = txtPaidIsuDate.Text.Trim() == "" ? null : Utility.StringToDateTime(txtPaidIsuDate.Text.Trim(), "BU");
                string channelType = cmbChannelType.SelectedValue.ToString();

                var planSearchForm = new PlanSearchForm();
                planSearchForm.ChannelType = channelType;
                planSearchForm.IsuDate = isuDate.Value;
                planSearchForm.FreeFlag = "N";
                planSearchForm.PL_BLOCK = plBlock;
                planSearchForm.ShowDialog();
                if (planSearchForm.PlanResult != null)
                {
                    var planObj = planSearchForm.PlanResult;
                    cmbPaidCtmType.SelectedValue = planObj.CTM_TYPE;
                    cmbPaidPlBlock.SelectedValue = planObj.PL_BLOCK;
                    txtPaidPlcode.Text = planObj.PL_CODE;
                    txtPaidPlcode2.Text = planObj.PL_CODE2;
                    txtPaidPlanName.Text = planObj.BLA_TITLE;
                    cmbPaidPmode.SelectedValue = planObj.P_MODE == null ? "" : planObj.P_MODE.Value.ToString();
                    if (planObj.PL_BLOCK != "K")
                    {
                        txtPaidSumm.Text = planObj.SUM_MAX == null ? "0" : planObj.SUM_MAX.Value.ToString("n0");
                    }
                    txtPaidMarketingType.Text = planObj.MARKETING_TYPE;
                    txtPaidPolicyHolding.Text = planObj.POLICY_HOLDING;
                    txtPolicyHolding.Text = planObj.POLICY_HOLDING;
                    txtPaidSpouseFlg.Text = planObj.OPL_CODE2;
                    if (planObj.FREE_FLG != "N")
                    {
                        txtFreePlcode2.Focus();
                        throw new Exception("แบบประกันไม่ถูกต้องต้องเป็นแบบซื้อเพิ่ม");
                    }


                    var ulifePlan = PAR_PA_APPLICATION_ID?.APPLICATION_Collection?.Where(item => item.LIFE_ID != null && item.LIFE_ID.UAPP_ID != null).FirstOrDefault();
                    if (ulifePlan == null)
                    {
                        if (channelType == "KB")
                        {
                            int cusAge = txtStAge.Text.Trim() == "" ? 0 : Convert.ToInt32(txtStAge.Text.Trim());
                            FindQuestion(planObj.PL_BLOCK, planObj.PL_CODE, planObj.PL_CODE2, cusAge);
                        }
                        else if (channelType == "PD")
                        {
                            AutoPlanFreeInfo();
                        }
                    }
                    ckbLifeBuy.Checked = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void FindQuestion(string plBlock, string plCOde, string plcode2, int cusAge)
        {
            // คีย์ครั้งแรก
            var repo1 = new Repository();
            var isAduflag = IsChildApplication(cusAge) ? false : true;
            var appnameId = repo1.GetAUTB_APPNAME_BY_PLAN(plBlock, plCOde, plcode2, isAduflag, false);
            if (appnameId != null)
            {
                cmbAppNameID.SelectedValue = appnameId.Value;
            }
        }
        private void BtnSearchPlanFree_Click(object sender, EventArgs e)
        {
            try
            {
                String plBlock = cmbFreePlBlock.SelectedValue == null ? "" : cmbFreePlBlock.SelectedValue.ToString();

                if (plBlock == "")
                {
                    cmbFreePlBlock.Focus();
                    throw new Exception("กรุณาระบุชนิดแบบประกัน");
                }

                if (cmbChannelType.SelectedValue == null || cmbChannelType.SelectedValue.ToString() == "")
                {
                    cmbChannelType.Focus();
                    throw new Exception("กรุณาระบุช่องทางการขายที่ท่านต้องการ");
                }

                txtFreeIsuDate.Text = txtAppSignDt.Text.Trim();

                DateTime? isuDate = txtFreeIsuDate.Text.Trim() == "" ? null : Utility.StringToDateTime(txtFreeIsuDate.Text.Trim(), "BU");
                string channelType = cmbChannelType.SelectedValue.ToString();

                var planSearchForm = new PlanSearchForm();
                planSearchForm.ChannelType = channelType;
                planSearchForm.IsuDate = isuDate.Value;
                planSearchForm.PL_BLOCK = plBlock;
                planSearchForm.FreeFlag = "Y";
                planSearchForm.ShowDialog();
                if (planSearchForm.PlanResult != null)
                {
                    var planObj = planSearchForm.PlanResult;
                    cmbFreeCtmType.SelectedValue = planObj.CTM_TYPE;
                    cmbFreePlBlock.SelectedValue = planObj.PL_BLOCK;
                    txtFreePlcode.Text = planObj.PL_CODE;
                    txtFreePlcode2.Text = planObj.PL_CODE2;
                    txtFreePlanName.Text = planObj.BLA_TITLE;
                    cmbFreePmode.SelectedValue = planObj.P_MODE == null ? "" : planObj.P_MODE.Value.ToString();
                    txtFreeSumm.Text = planObj.SUM_MAX == null ? "0" : planObj.SUM_MAX.Value.ToString("n0");
                    txtFreeMarketingType.Text = planObj.MARKETING_TYPE;
                    txtFreePolicyHolding.Text = planObj.POLICY_HOLDING;
                    txtPolicyHolding.Text = planObj.POLICY_HOLDING;
                    txtFreeSpouseFlg.Text = planObj.OPL_CODE2;

                    if (!IsChannelTypeFreePolicy(channelType))
                    {
                        if (planObj.FREE_FLG != "Y")
                        {
                            txtFreePlcode2.Focus();
                            throw new Exception("แบบประกันไม่ถูกต้องต้องเป็นแบบแถมฟรี");
                        }
                    }
                    ckbLifeFree.Checked = true;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var url = ITUtility.Utility.AppSetting("EKYCDomainURL");
            string isEnable = chk_ekyc_pass.Checked ? "N" : "Y";
            url = url + "?userID=" + UserID + "&appno=" + txtAppNo.Text + "&enable=" + isEnable;
            Process.Start(url);
        }

        private void btn_load_ekyc_status_Click(object sender, EventArgs e)
        {
            chk_ekyc_pass.Checked = chk_ekyc_not_pass.Checked = chk_ekyc_wait_verify.Checked = chk_ekyc_none.Checked = false;
            CheckEKYC();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            btnHeadSave.Enabled = true;
        }
    }
}
