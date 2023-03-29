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

namespace NewBisPA
{
    partial class ApplicationForm
    {
        private String _UserID;
        public String UserID
        {
            get { return _UserID; }
            set { _UserID = value; }
        }
        private int[] bgRed = { 255, 192, 192 };
        private int[] bgYellow = { 255, 255, 192 };
        private int[] bgGreen = { 192, 255, 192 };

        private long? _NAME_ID;

        public long? NAME_ID
        {
            get { return _NAME_ID; }
            set { _NAME_ID = value; }
        }

        public long? PARENT_PA_NAME_ID
        {
            get;
            set;
        }


        public long? PARENT_PARENT_ID
        {
            get;
            set;
        }

        private long? _PARENT_ID;

        public long? PARENT_ID
        {
            get { return _PARENT_ID; }
            set { _PARENT_ID = value; }
        }

        private long? _ADDRESS_ID;

        public long? ADDRESS_ID
        {
            get { return _ADDRESS_ID; }
            set { _ADDRESS_ID = value; }
        }

        public long? PARENT_ADDRESS_ID
        {
            get;
            set;
        }

        private bool _PLAN_ERROR;

        public bool PLAN_ERROR
        {
            get { return _PLAN_ERROR; }
            set { _PLAN_ERROR = value; }
        }

        //เก็บข้อมูลของ Tab ========================
        private bool _VerifyFlag;
        public bool VerifyFlag
        {
            get { return _VerifyFlag; }
            set { _VerifyFlag = value; }
        }
        private bool _ClickTabCustomerData;
        public bool ClickTabCustomerData
        {
            get { return _ClickTabCustomerData; }
            set { _ClickTabCustomerData = value; }
        }
        private bool _ClickTabPlanData;
        public bool ClickTabPlanData
        {
            get { return _ClickTabPlanData; }
            set { _ClickTabPlanData = value; }
        }
        private bool _ClickTabBenefit;
        public bool ClickTabBenefit
        {
            get { return _ClickTabBenefit; }
            set { _ClickTabBenefit = value; }
        }
        private bool _ClickTabAppName;
        public bool ClickTabAppName
        {
            get { return _ClickTabAppName; }
            set { _ClickTabAppName = value; }
        }
        private bool _ClickTabCreditCard;
        public bool ClickTabCreditCard
        {
            get { return _ClickTabCreditCard; }
            set { _ClickTabCreditCard = value; }
        }
        private bool _ClickTabUnderWriteMain;
        public bool ClickTabUnderWriteMain
        {
            get { return _ClickTabUnderWriteMain; }
            set { _ClickTabUnderWriteMain = value; }
        }
        private bool _ClickTabUnderWrite;
        public bool ClickTabUnderWrite
        {
            get { return _ClickTabUnderWrite; }
            set { _ClickTabUnderWrite = value; }
        }
        private bool _ClickTabMemo;
        public bool ClickTabMemo
        {
            get { return _ClickTabMemo; }
            set { _ClickTabMemo = value; }
        }
        private bool _ClickTabHistory;
        public bool ClickTabHistory
        {
            get { return _ClickTabHistory; }
            set { _ClickTabHistory = value; }
        }
        //จบเก็บข้อมูลของ Tab ======================
        //ตัวแปรที่มีผลต่อการคำนวนเบี้ย =================
        private String _OLD_BIRTH_DATE;
        public String OLD_BIRTH_DATE
        {
            get { return _OLD_BIRTH_DATE; }
            set { _OLD_BIRTH_DATE = value; }
        }
        private String _OLD_BIRTH_DATE_SPOUSE;
        public String OLD_BIRTH_DATE_SPOUSE
        {
            get { return _OLD_BIRTH_DATE_SPOUSE; }
            set { _OLD_BIRTH_DATE_SPOUSE = value; }
        }
        private bool _OLD_PLAN_FREE;
        public bool OLD_PLAN_FREE
        {
            get { return _OLD_PLAN_FREE; }
            set { _OLD_PLAN_FREE = value; }
        }
        private bool _OLD_PLAN_PAID;
        public bool OLD_PLAN_PAID
        {
            get { return _OLD_PLAN_PAID; }
            set { _OLD_PLAN_PAID = value; }
        }
        private bool _OLD_SPOUSE;
        public bool OLD_SPOUSE
        {
            get { return _OLD_SPOUSE; }
            set { _OLD_SPOUSE = value; }
        }
        private String _OLD_PLAN_CODE_FREE;
        public String OLD_PLAN_CODE_FREE
        {
            get { return _OLD_PLAN_CODE_FREE; }
            set { _OLD_PLAN_CODE_FREE = value; }
        }
        private String _OLD_MODE_FREE;
        public String OLD_MODE_FREE
        {
            get { return _OLD_MODE_FREE; }
            set { _OLD_MODE_FREE = value; }
        }
        private String _OLD_SUMM_FREE;
        public String OLD_SUMM_FREE
        {
            get { return _OLD_SUMM_FREE; }
            set { _OLD_SUMM_FREE = value; }
        }
        private String _OLD_ISU_DT_FREE;

        public String OLD_ISU_DT_FREE
        {
            get { return _OLD_ISU_DT_FREE; }
            set { _OLD_ISU_DT_FREE = value; }
        }
        private String _OLD_PLAN_CODE_PAID;
        public String OLD_PLAN_CODE_PAID
        {
            get { return _OLD_PLAN_CODE_PAID; }
            set { _OLD_PLAN_CODE_PAID = value; }
        }
        private String _OLD_MODE_PAID;
        public String OLD_MODE_PAID
        {
            get { return _OLD_MODE_PAID; }
            set { _OLD_MODE_PAID = value; }
        }
        private String _OLD_SUMM_PAID;
        public String OLD_SUMM_PAID
        {
            get { return _OLD_SUMM_PAID; }
            set { _OLD_SUMM_PAID = value; }
        }
        private String _OLD_ISU_DT_PAID;

        public String OLD_ISU_DT_PAID
        {
            get { return _OLD_ISU_DT_PAID; }
            set { _OLD_ISU_DT_PAID = value; }
        }

        private String _OLD_PREMIUM_PAID;

        public String OLD_PREMIUM_PAID
        {
            get { return _OLD_PREMIUM_PAID; }
            set { _OLD_PREMIUM_PAID = value; }
        }

        //จบตัวแปรที่มีผลต่อการคำนวนเบี้ย ===============

        //ตัวแปรในส่วนของ UNDERWRITE ==============
        private bool _PAR_MEMO_SAVE_SUSCESS;

        public bool PAR_MEMO_SAVE_SUSCESS
        {
            get { return _PAR_MEMO_SAVE_SUSCESS; }
            set { _PAR_MEMO_SAVE_SUSCESS = value; }
        }
        //END ตัวแปรในส่วนของ UNDERWRITE ==========

        private bool _APP_LOCK_ERROR;

        public bool APP_LOCK_ERROR
        {
            get { return _APP_LOCK_ERROR; }
            set { _APP_LOCK_ERROR = value; }
        }

        private char? _PAR_END_PROCESS;

        public char? PAR_END_PROCESS
        {
            get { return _PAR_END_PROCESS; }
            set { _PAR_END_PROCESS = value; }
        }

        //ตัวแปรเก็บข้อมูลบัญชี ==============================================
        private bool _PAR_SUSPENSE_CHECK;
        public bool PAR_SUSPENSE_CHECK
        {
            get { return _PAR_SUSPENSE_CHECK; }
            set { _PAR_SUSPENSE_CHECK = value; }
        }

        private bool _PAR_SUSPENSE_DETAIL;
        public bool PAR_SUSPENSE_DETAIL
        {
            get { return _PAR_SUSPENSE_DETAIL; }
            set { _PAR_SUSPENSE_DETAIL = value; }
        }

        private Decimal? _PAR_PREMIUM_SUSPENSE;
        public Decimal? PAR_PREMIUM_SUSPENSE
        {
            get { return _PAR_PREMIUM_SUSPENSE; }
            set { _PAR_PREMIUM_SUSPENSE = value; }
        }

        private DateTime? _PAR_PAYIN_DATE;
        public DateTime? PAR_PAYIN_DATE
        {
            get { return _PAR_PAYIN_DATE; }
            set { _PAR_PAYIN_DATE = value; }
        }

        private Decimal? _PAR_TOTAL_SUMM_FREE;
        public Decimal? PAR_TOTAL_SUMM_FREE
        {
            get { return _PAR_TOTAL_SUMM_FREE; }
            set { _PAR_TOTAL_SUMM_FREE = value; }
        }

        private Decimal? _PAR_TOTAL_SUMM_BUY;
        public Decimal? PAR_TOTAL_SUMM_BUY
        {
            get { return _PAR_TOTAL_SUMM_BUY; }
            set { _PAR_TOTAL_SUMM_BUY = value; }
        }
        private bool _PAR_CAL_SUMM;

        public bool PAR_CAL_SUMM
        {
            get { return _PAR_CAL_SUMM; }
            set { _PAR_CAL_SUMM = value; }
        }
        //END ตัวแปรเก็บข้อมูลบัญชี ==========================================

        private bool _PAR_CLICK_CUSFORM;

        public bool PAR_CLICK_CUSFORM
        {
            get { return _PAR_CLICK_CUSFORM; }
            set { _PAR_CLICK_CUSFORM = value; }
        }

        private void ApplicationBeginParameter()
        {
            try
            {
                AddressOfLBDU.Visible = false;
                AddressOfLBDUOfParent.Visible = false;
                cmbAdrType.Enabled = true;
                SuspenseePayDate = null;
                PAR_CLICK_CUSFORM = false;
                PAR_CAL_SUMM = false;
                PAR_TOTAL_SUMM_FREE = 0;
                PAR_TOTAL_SUMM_BUY = 0;
                PAR_SUSPENSE_CHECK = false;
                PAR_SUSPENSE_DETAIL = false;
                PAR_PREMIUM_SUSPENSE = null;
                PAR_PAYIN_DATE = null;

                PAR_END_PROCESS = 'N';
                APP_LOCK_ERROR = false;
                FORMAPP = "";
                cmbEPolicySending.SelectedValue = cmbPolicyDocmentType.SelectedValue = "";
                PAR_MEMO_SAVE_SUSCESS = true;
                VerifyFlag = true;
                btnHeadSave.Enabled = false;
                btnScan.Enabled = false;
                btnCustomerData.Enabled = false;
                btnHeadClear.Visible = true;
                btnHeadSave.Visible = true;
                chk_ekyc_pass.Checked = chk_ekyc_not_pass.Checked = chk_ekyc_none.Checked = chk_ekyc_wait_verify.Checked = false;
                chk_ekyc_pass.Visible = chk_ekyc_not_pass.Visible = chk_ekyc_none.Visible = chk_ekyc_wait_verify.Visible = btn_load_ekyc_status.Visible = ekycDetailExterlink.Visible =  false;

                tabMain.SelectTab("tabCustomerData");
                PAR_PA_APPLICATION_ID = new NewBisPASvcRef.PA_APPLICATION_ID();
                PAR_W_UNDERWRITE_ID_COLL = new NewBisPASvcRef.W_UNDERWRITE_ID_Collection();
                PAR_W_UNDERWRITE_ID_TMP_COLL = new NewBisPASvcRef.W_UNDERWRITE_ID_Collection();
                PAR_PLAN_FREE = new NewBisPASvcRef.U_LIFE_ID();
                PAR_PLAN_PAID = new NewBisPASvcRef.U_LIFE_ID();
                PAR_U_ADDRESS_ID_COLL = null;
                PAR_U_SPOUSE_ID_COLL = null;
                PAR_BENEFIT_ID_COLL = new NewBisPASvcRef.U_BENEFIT_ID_COLLECTION();
                PAR_BENEFIT_ID_COLL_TMP = new NewBisPASvcRef.U_BENEFIT_ID_COLLECTION();
                PAR_BENEFIT_ID_OBJ = new NewBisPASvcRef.U_BENEFIT_ID();
                PAR_W_SUMMARY = new NewBisPASvcRef.W_SUMMARY();
                PAR_U_MEMO_ID_COLL_TMP = new NewBisPASvcRef.U_MEMO_ID_Collection();
                PAR_U_MEMO_ID = new NewBisPASvcRef.U_MEMO_ID();
                PAR_U_MEMO_DETAIL = new NewBisPASvcRef.U_MEMO_DETAIL();
                PAR_U_CAL_ERROR = new NewBisPASvcRef.U_CAL_ERROR();
                PAR_U_APPLICATION_NAME = new NewBisPASvcRef.U_APPLICATION_NAME();
                PAR_W_SUMMARY_DETAIL_COLL = new NewBisPASvcRef.W_SUMMARY_DETAIL_Collection();

                NewBisPASvcRef.OCCUPATION_DATA oCcupationObj = new NewBisPASvcRef.OCCUPATION_DATA();
                oCcupationObj.CODE = "";
                oCcupationObj.OCP_TYPE_NAME = "ระบุรายละเอียดชั้นอาชีพ";
                PAR_OCCUPATION_COLL.Add(oCcupationObj);
                cmbocpDesc.DataSource = PAR_OCCUPATION_COLL;
                cmbocpDesc.DisplayMember = "OCP_TYPE_NAME";
                cmbocpDesc.ValueMember = "CODE";
                cmbocpDesc.SelectedValue = "";

                NewBisPASvcRef.OCCUPATION_DATA[] pOccupation = new NewBisPASvcRef.OCCUPATION_DATA[PAR_OCCUPATION_COLL.Count];
                PAR_OCCUPATION_COLL.CopyTo(pOccupation);

                cmbParentbocpDesc.DataSource = pOccupation;
                cmbParentbocpDesc.DisplayMember = "OCP_TYPE_NAME";
                cmbParentbocpDesc.ValueMember = "CODE";
                cmbParentbocpDesc.SelectedValue = "";



                U_APP_REMARK_COLL = new NewBisPASvcRef.U_APP_REMARK_Collection();
                btnAppRemark.BackColor = Color.Transparent;
                ckbMedical.Checked = false;
                ckbCounterOffer.Checked = false;
                ckbchgConditionApp.Checked = false;    
                ckbchgConditionApp2.Checked = false; 
                ckbApsolutePlan.Checked = false;


                SetPlanParametersOld();
                TabMainParameter();
                MainParameter();
                HeadBeginParameter();
                CustomerParameter();
                ParentParameter();
                UnderWriteParameter();
                AddressParameter();
                PlanPDParameter();
                CreditCardParameter();
                ClearControlsBenefitAdd();
                panelBenefitList.Controls.Clear();
                PlanOldParameter();
                MemoParameter();
                panelHistory.Controls.Clear();

                dgvPayMent.Rows.Clear();
                txtPayMentReturnPrem.Text = "";
                txtPayMentPremium.Text = "";
                txtPayMentSuspense.Text = "";
            }
            catch (Exception ex)
            {
                SetMsgException(ex);
            }
            
        }
        private void MainParameter()
        {
            NAME_ID = null;
            PARENT_ID = null;
            ADDRESS_ID = null;
            PLAN_ERROR = false;
            PARENT_PA_NAME_ID = PARENT_PARENT_ID = PARENT_ADDRESS_ID =  null;
        }
        private void HeadBeginParameter()
        {
            txtAppNo.Focus();
            //cmbChannelType.SelectedValue = "PD";
            cmbChannelType.Enabled = true;
            cmbWorkGroup.Enabled = true;
            cmbAppNameID.Enabled = true;

            if (PAR_AUTB_CHANNEL_BLOCK_COLLECTION != null && PAR_AUTB_CHANNEL_BLOCK_COLLECTION.Count() > 0)
            {
                //String channelType = "";
                //channelType = cmbChannelType.SelectedValue == null ? "PD" : cmbChannelType.SelectedValue.ToString();

                var WorkGroupLinq = from workGroupLinq in PAR_AUTB_CHANNEL_BLOCK_COLLECTION
                                    where workGroupLinq.CHANNEL_TYPE == cmbChannelType.SelectedValue.ToString()
                                    select workGroupLinq;
                if (WorkGroupLinq != null && WorkGroupLinq.Count() > 0)
                {
                    String workGroupCode = (from getData in WorkGroupLinq select getData.WORK_GROUP).First();
                        
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
                    cmbWorkGroup.SelectedValue = workGroupCode;
                }
            }

            if (cmbChannelType.SelectedValue == null || cmbChannelType.SelectedValue.ToString() == "")
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
            else
            {
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

            txtCertNo.Text = "";
            txtCustomerName.Text = "";
            txtSaleAgent.Text = "";
            txtSaleAgentName.Text = "";
            txtSaleAgent.BackColor = Color.FromArgb(bgYellow[0], bgYellow[1], bgYellow[2]);
            txtSaleAgentName.BackColor = SystemColors.Control;
            txtSaleAgentUpl.Text = "";
            txtSaleAgentUplName.Text = "";
            txtLicenseAgent.Text = "";
            txtLicenseAgentName.Text = "";
            txtAppOfc.Text = "";
            cmbSendTo.SelectedValue = "";
            txtSendOfc.Text = "";
            lblSendAt.Visible = false;
            txtSendOfc.Visible = false;
            cmbDocumentFlag.SelectedValue = "";
            cmbUnderWriteFlag.SelectedValue = "";
            cmbUnderWrite.SelectedValue = "C";

            if (PAR_AUTB_STATUS_COLLECTION != null && PAR_AUTB_STATUS_COLLECTION.Count() > 0)
            {
                var StatusLinq = from statusLinq in PAR_AUTB_STATUS_COLLECTION
                                 where statusLinq.STATUS == "WT"
                                 select statusLinq;
                txtStatus.Text = StatusLinq.First().DESCRIPTION;
            }

            DateControlsBegin();

            SetControlByChannelType(cmbChannelType.SelectedValue.ToString());

            txtBankrupCus.Visible = false;
            txtRemarkCus.Visible = false;
            txtBankrupSpouse.Visible = false;
            txtRemarkSpouse.Visible = false;
            txtBankrupCus.Text = "";
            txtRemarkCus.Text = "";
            txtBankrupSpouse.Text = "";
            txtRemarkSpouse.Text = "";
            txtPolicyHolding.Text = "";
        }
        private void DateControlsBegin()
        {
            txtAppOfcRcvDt.Text = Utility.dateTimeToString(DateTime.Now, "dd/MM/yyyy hh:mi:ss", "BU");
            txtAppHoRcvDt.Text = Utility.dateTimeToString(DateTime.Now, "dd/MM/yyyy hh:mi:ss", "BU");
            txtAppDt.Text = "";
            txtAppSignDt.Text = Utility.dateTimeToString(DateTime.Now, "dd/MM/yyyy", "BU");
            txtAppSysDt.Text = Utility.dateTimeToString(DateTime.Now, "dd/MM/yyyy hh:mi:ss", "BU");
            txtStrDt.Text = "";
            txtApprovedDt.Text = "";
            txtPolicyDt.Text = "";
            txtInstallDt.Text = "";
            txtMktDate.Text = "";
        }
        private void CustomerParameter()
        {
            ckbIdcardDoc.Checked = false;
            ckbAddressDoc.Checked = false;
            cmbCardType.SelectedValue = "1";
            txtIdcardNo.Text = "";
            cmbDataCus.SelectedValue = "2";
            txtPrename.Text = "";
            txtName.Text = "";
            txtSurname.Text = "";
            cmbSex.SelectedValue = "";
            txtBirthDt.Text = "";
            txtStAge.Text = "";
            cmbNationality.SelectedValue = "TH";
            cmbReligion.SelectedValue = "";
            cmbMaritalStatus.SelectedValue = "";
            txtMbPhone.Text = "";
            txtEmail.Text = "";
            txtIdcardNo.BackColor = Color.FromArgb(bgYellow[0], bgYellow[1], bgYellow[2]);
            cmbDataCus.BackColor = Color.White;
            txtPrename.BackColor = Color.White;
            txtName.BackColor = Color.White;
            txtSurname.BackColor = Color.White;
            cmbSex.BackColor = Color.White;
            txtBirthDt.BackColor = Color.FromArgb(bgYellow[0], bgYellow[1], bgYellow[2]);
            cmbNationality.BackColor = Color.White;
            txtEmail.BackColor = Color.White;
            txtMbPhone.BackColor = Color.White;

            txtHeight.Text = "";
            txtWeight.Text = "";
            txtBmi.Text = "";
            txtBmi.BackColor = Color.FromArgb(192, 255, 192);
            ckbMotercycleUsed.Checked = false;
            txtOcpType.Text = "";
            txtOcpClass.Text = "";
            cmbocpDesc.SelectedValue = "";
            cmbPosition.SelectedValue = "";
            cmbBusinessType.SelectedValue = "";
            txtWorkType.Text = "";
            txtIncomePerYear.Text = "";

            txtBankCode.Text = "002";
            txtBankName.Text = "ธนาคารกรุงเทพ จำกัด (มหาชน)";
            txtBranchCode.Text = "";
            txtBranchName.Text = "";
            txtAccName.Text = "";
            txtAccNo.Text = "";
            cmbDepositType.SelectedValue = "";
        }

        private void ParentParameter()
        {
            ckbParentIdcardDoc.Checked = false;
            ckbParentAddressDoc.Checked = false;
            cmbParentCardType.SelectedValue = "1";
            txtParentIdcardNo.Text = "";
            cmbDataParent.SelectedValue = "2";
            txtParentPrename.Text = "";
            txtParentName.Text = "";
            txtParentSurname.Text = "";
            cmbParentGender.SelectedValue = "";
            txtParentBirthDt.Text = "";
            txtParentStAge.Text = "";
            cmbParentNationality.SelectedValue = "TH";
            cmbParentReligion.SelectedValue = "";
            cmbParentMaritalStatus.SelectedValue = "";
            txtParentMbPhone.Text = "";
            txtParentEmail.Text = "";
            txtParentIdcardNo.BackColor = Color.FromArgb(bgYellow[0], bgYellow[1], bgYellow[2]);
            cmbDataParent.BackColor = Color.White;
            txtParentPrename.BackColor = Color.White;
            txtParentName.BackColor = Color.White;
            txtParentSurname.BackColor = Color.White;
            cmbParentGender.BackColor = Color.White;
            txtParentBirthDt.BackColor = Color.FromArgb(bgYellow[0], bgYellow[1], bgYellow[2]);
            cmbParentNationality.BackColor = Color.White;
            txtParentEmail.BackColor = Color.White;
            txtParentMbPhone.BackColor = Color.White;

            txtParentHeight.Text = "";
            txtParentWeight.Text = "";

            ckbParentMotercycleUsed.Checked = false;
            txtParentOcpType.Text = "";
            txtParentOcpClass.Text = "";
            cmbParentbocpDesc.SelectedValue = "";
            cmbParentPosition.SelectedValue = "";
            cmbParentBusinessType.SelectedValue = "";
            txtParentWorkType.Text = "";
            txtParentIncomePerYear.Text = "";
        }
        private void AddressParameter()
        {
            cmbAddressType.SelectedValue = "";
            txtAddressName.Text = "";
            txtAddressNumber.Text = "";
            txtMooh.Text = "";
            txtSoi.Text = "";
            txtRoad.Text = "";
            cmbProvince.SelectedValue = "";
            cmbAmphur.SelectedValue = "";
            cmbTambol.SelectedValue = "";
            txtZipcode.Text = "";
            txtPhoneNumber.Text = "";
            cmbAdrType.SelectedValue = "2";

            cmbAdrType.BackColor = Color.White;
            txtAddressName.BackColor = Color.White;
            txtAddressNumber.BackColor = Color.White;
            txtMooh.BackColor = Color.White;
            txtSoi.BackColor = Color.White;
            txtRoad.BackColor = Color.White;
            cmbProvince.BackColor = Color.White;
            cmbAmphur.BackColor = Color.White;
            cmbTambol.BackColor = Color.White;
            txtZipcode.BackColor = Color.White;
            txtPhoneNumber.BackColor = Color.White;
        }
        private void PlanPDParameter()
        {
            cmbFreeCtmType.SelectedValue = "G";
            //cmbFreePlBlock.SelectedValue = "Q";
            txtFreePlcode.Text = "";
            txtFreePlcode2.Text = "";
            txtFreePlanName.Text = "";
            cmbFreePmode.SelectedValue = "";
            txtFreeSumm.Text = "";
            txtFreeIsuDate.Text = "";
            txtFreePolYr.Text = "";
            txtFreePolLt.Text = "";
            txtFreePayTerm.Text = "";
            txtFreeLastPayDate.Text = "";
            txtFreeAssTerm.Text = "";
            txtFreeAssDate.Text = "";
            txtFreeMatTerm.Text = "";
            txtFreeMatDate.Text = "";
            txtFreeNxtDueDate.Text = "";
            txtFreePremium.Text = "0";
            txtFreeSingle.Text = "";
            txtFreeProtect.Text = "";
            txtFreeMedical.Text = "";
            txtFreeStandard.Text = "";
            txtFreeReinsure.Text = "";
            txtFreeExcludeTpd.Text = "";
            txtFreeDepositInst.Text = "";
            txtFreePensionDate.Text = "";
            txtFreePolicyHolding.Text = "";
            txtFreeMarketingType.Text = "";
            txtFreeSpouseFlg.Text = "";
            if (cmbChannelType.SelectedValue.ToString() == "PF")
            {
                ckbLifeFree.Checked = true;
            }
            else
            {
                ckbLifeFree.Checked = false;
            }
            if (cmbChannelType.SelectedValue.ToString() == "PD" || cmbChannelType.SelectedValue.ToString() == "PF")
            {
                ckbLifeBuy.Checked = false;
                ckbLifeBuy.Enabled = true;

            }
            else
            {
                ckbLifeBuy.Checked = true;
                ckbLifeBuy.Enabled = false;
            }

            if (cmbChannelType.SelectedValue.ToString() == "KB")
            {
                txtPaidPremium.ReadOnly = false;
            }
            else
            {
                txtPaidPremium.ReadOnly = true;
            }
            
            cmbPaidCtmType.SelectedValue = "G";
            //cmbPaidPlBlock.SelectedValue = "Q";
            txtPaidPlcode.Text = "";
            txtPaidPlcode2.Text = "";
            txtPaidPlanName.Text = "";
            cmbPaidPmode.SelectedValue = "";
            txtPaidSumm.Text = "";
            txtPaidIsuDate.Text = "";
            txtPaidPolYr.Text = "";
            txtPaidPolLt.Text = "";
            txtPaidPayTerm.Text = "";
            txtPaidLastPayDate.Text = "";
            txtPaidAssTerm.Text = "";
            txtPaidAssDate.Text = "";
            txtPaidMatTerm.Text = "";
            txtPaidMatDate.Text = "";
            txtPaidNxtDueDate.Text = "";
            txtPaidPremium.Text = "0";
            txtPaidSingle.Text = "";
            txtPaidProtect.Text = "";
            txtPaidMedical.Text = "";
            txtPaidStandard.Text = "";
            txtPaidReinsure.Text = "";
            txtPaidExcludeTpd.Text = "";
            txtPaidDepositInst.Text = "";
            txtPaidPensionDate.Text = "";
            txtPaidPolicyHolding.Text = "";
            txtPaidMarketingType.Text = "";
            txtPaidSpouseFlg.Text = "";

            ckbSpouse.Checked = false;
            cmbSpCardType.SelectedValue = "";
            txtSpIDcardNo.Text = "";
            txtSpPrename.Text = "";
            txtSpName.Text = "";
            txtSpSurname.Text = "";
            cmbSpSex.SelectedValue = "";
            txtSpBirthDate.Text = "";
            txtSpAge.Text = "";
            cmbSpNationality.SelectedValue = "TH";
            txtSpMbPhone.Text = "";
            txtSpAssTerm.Text = "";
            txtSpAssDate.Text = "";

            txtTotalPremium.Text = "";
            txtSuspensePremium.Text = "";
            txtOverPremium.Text = "";
        }
        private void PlanOldParameter()
        {
            OLD_PLAN_FREE = false;
            OLD_PLAN_CODE_FREE = "";
            OLD_MODE_FREE = "";
            OLD_SUMM_FREE = "";
            OLD_ISU_DT_FREE = "";

            OLD_PLAN_PAID = false;
            OLD_PLAN_CODE_PAID = "";
            OLD_MODE_PAID = "";
            OLD_SUMM_PAID = "";
            OLD_ISU_DT_PAID = "";
            OLD_PREMIUM_PAID = "";

            OLD_SPOUSE = false;
            OLD_BIRTH_DATE_SPOUSE = "";
        }
        private void CreditCardParameter()
        {
            cmbPayOption.SelectedValue = "RE";
            txtCardNo1.Text = "";
            txtCardNo2.Text = "";
            txtCardNo3.Text = "";
            txtCardNo4.Text = "";
            txtCreditCardName.Text = "";
            txtCreditCardTypeName.Text = "";
            txtFinCode.Text = "";
            txtCreditCardType.Text = "";
            txtExpireMM.Text = "";
            txtExpireYY.Text = "";
        }
        private void UnderWriteParameter()
        {
            SetEnableStatusOfUnderwriterControl(true);
            cmbSummryStatus.SelectedValue = "";
            GetControlsUnderStatus(cmbSummryStatus.SelectedValue.ToString(), "");
            lblPPUntil.Visible = false;
            cmbPPUntil.Visible = false;
            cmbPPUntil.DataSource = null;
            cmbUnderWriteID.SelectedValue = UserID;

            txtTotalSumm.Text = "";
            txtNowSumm.Text = "";
            txtNowPremium.Text = "";
            txtNowSuspensePremium.Text = "";
            txtSuspenseDate.Text = "";

            txtTotalSummOld.Text = "";
            txtTotalSummFree.Text = "";
            txtNowSummFree.Text = "";
            txtTotalSummOldFree.Text = "";

            txtSummaryDetailSeq.Text = "";
            txtSummaryDetail.Text = "";
            btnSummaryDetail.Text = "เพิ่มข้อมูล";
            panelSummaryDetail.Controls.Clear();
        }
        private void MemoParameter()
        {
            txtMemoIDNO.Text = "";
            txtMemoIDTrnDate.Text = "";
            txtMemoIDLimitDate.Text = "";
            cmbMemoIDEndProcess.SelectedValue = "";
            ckbMemoIDTmn.Checked = false;
            btnMemoIDSave.Text = "เพิ่มข้อมูล";
            panelMemoList.Controls.Clear();

            gbMemoDetail.Enabled = false;
            txtMemoDetailNO.Text = "";
            txtMemoDetailPendCode.Text = "";
            cmbMemoDetailPendCodeDesc.SelectedValue = "";
            cmbMemoDetailPendStatus.SelectedValue = "";
            txtMemoDetailPendStatusDate.Text = "";
            btnMemoDetailSave.Text = "เพิ่มข้อมูล";
            txtMemoDescription.Text = "";
            panelMemoDetailList.Controls.Clear();
            panelMemoDocument.Controls.Clear();

            DateTime? AnswerLimitDate = DateTime.Now.AddDays(20);
            txtMemoIDLimitDate.Text = Utility.dateTimeToString(AnswerLimitDate.Value, "dd/MM/yyyy", "BU");
            cmbMemoIDEndProcess.SelectedValue = "N";

        }
        private void TabMainParameter()
        {
            ClickTabCustomerData = false;
            ClickTabPlanData = false;
            ClickTabBenefit = false;
            ClickTabAppName = false;
            ClickTabCreditCard = false;
            ClickTabUnderWriteMain = false;
            ClickTabUnderWrite = false;
            ClickTabMemo = false;
            ClickTabHistory = false;
        }
        private void SetPlanParametersOld()
        {
            OLD_BIRTH_DATE = "";
            OLD_PLAN_FREE = false;
            OLD_PLAN_PAID = false;
            OLD_SPOUSE = false;
            OLD_PLAN_CODE_FREE = "";
            OLD_MODE_FREE = "";
            OLD_SUMM_FREE = "";
            OLD_ISU_DT_FREE = "";
            OLD_PLAN_CODE_PAID = "";
            OLD_MODE_PAID = "";
            OLD_SUMM_PAID = "";
            OLD_ISU_DT_PAID = "";
            OLD_BIRTH_DATE_SPOUSE = "";
        }
        private void SetColorItemBeginCustomerName()
        {
            txtIdcardNo.BackColor = Color.FromArgb(bgYellow[0], bgYellow[1], bgYellow[2]);
            cmbDataCus.BackColor = Color.White;
            txtPrename.BackColor = Color.White;
            txtName.BackColor = Color.White;
            txtSurname.BackColor = Color.White;
            cmbSex.BackColor = Color.White;
            txtBirthDt.BackColor = Color.FromArgb(bgYellow[0], bgYellow[1], bgYellow[2]);
            cmbNationality.BackColor = Color.White;
            txtEmail.BackColor = Color.White;
            txtMbPhone.BackColor = Color.White;
        }

        private void SetColorItemBeginParentName()
        {
            txtParentIdcardNo.BackColor = Color.FromArgb(bgYellow[0], bgYellow[1], bgYellow[2]);
            cmbDataParent.BackColor = Color.White;
            txtParentPrename.BackColor = Color.White;
            txtParentName.BackColor = Color.White;
            txtParentSurname.BackColor = Color.White;
            cmbParentGender.BackColor = Color.White;
            txtParentBirthDt.BackColor = Color.FromArgb(bgYellow[0], bgYellow[1], bgYellow[2]);
            cmbParentNationality.BackColor = Color.White;
            txtParentEmail.BackColor = Color.White;
            txtParentMbPhone.BackColor = Color.White;
        }
        private void SetColorItemBeginCustomerAddress()
        {
            txtAddressName.BackColor = Color.White;
            txtAddressNumber.BackColor = Color.White;
            txtMooh.BackColor = Color.White;
            txtSoi.BackColor = Color.White;
            txtRoad.BackColor = Color.White;
            cmbProvince.BackColor = Color.White;
            cmbAmphur.BackColor = Color.White;
            cmbTambol.BackColor = Color.White;
            txtZipcode.BackColor = Color.White;
            txtPhoneNumber.BackColor = Color.White;
        }

        private void SetColorItemBeginParentAddress()
        {
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
        }
        private void SetControlByChannelType(String channelType)
        {
            if (channelType == "PD" || channelType == "PF")
            {
                gbAccount.Visible = false;
                gbPlanFree.Enabled = true;
                pnlSpouse.Enabled = true;
                ckbSpouse.Enabled = true;
                ckbLifeBuy.Checked = false;
                ckbLifeBuy.Enabled = true;

                label131.Visible = true;
                label132.Visible = true;
                label133.Visible = true;
                txtTotalSummFree.Visible = true;
                txtNowSummFree.Visible = true;
                txtTotalSummOldFree.Visible = true;
                if (channelType == "PD")
                {
                    gbCreditCard.Visible = true;
                }
                else
                {
                    gbCreditCard.Visible = false;
                }
            }
            else if (channelType == "PO" || channelType == "PN")
            {
                gbAccount.Visible = false;
                gbPlanFree.Enabled = false;
                pnlSpouse.Enabled = false;
                gbCreditCard.Visible = false;
                ckbSpouse.Enabled = false;
                ckbLifeBuy.Checked = true;
                ckbLifeBuy.Enabled = false;

                label131.Visible = false;
                label132.Visible = false;
                label133.Visible = false;
                txtTotalSummFree.Visible = false;
                txtNowSummFree.Visible = false;
                txtTotalSummOldFree.Visible = false;
            }
            else if (channelType == "KB")
            {
                gbAccount.Visible = true;
                gbPlanFree.Enabled = false;
                pnlSpouse.Enabled = false;
                gbCreditCard.Visible = false;
                ckbSpouse.Enabled = false;
                ckbLifeBuy.Checked = true;
                ckbLifeBuy.Enabled = false;

                label131.Visible = false;
                label132.Visible = false;
                label133.Visible = false;
                txtTotalSummFree.Visible = false;
                txtNowSummFree.Visible = false;
                txtTotalSummOldFree.Visible = false;
            }
        }
    }
}
