using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ITUtility;
using Microsoft.Win32;
using System.Threading;
using System.Diagnostics;
using System.Security.Cryptography;
using System.IO;
using Newtonsoft.Json;

namespace NewBisPA
{
    public partial class FormCustomerData : Form
    {
        private long? _UAPP_ID;

        public long? UAPP_ID
        {
            get { return _UAPP_ID; }
            set { _UAPP_ID = value; }
        }


        private String _CUSTOMER_TYPE;

        public String CUSTOMER_TYPE
        {
            get { return _CUSTOMER_TYPE; }
            set { _CUSTOMER_TYPE = value; }
        }
        private NewBISSvcRef.W_MIB_Collection _W_MIB_COLL;

        public NewBISSvcRef.W_MIB_Collection W_MIB_COLL
        {
            get { return _W_MIB_COLL; }
            set { _W_MIB_COLL = value; }
        }

        private int HAVE_CUSPRODUCT;
        private int HAVE_CUSPRODUCTOTH;
        private int HAVE_PARENTPRODUCT;
        private int HAVE_PARENTPRODUCTOTH;
        private int HAVE_MIB;
        private int HAVE_AMLCTF;
        private int HAVE_BANKRUPT;
        private int HAVE_REMARK;

        private int _CHK_HAVE_DATA;

        public int CHK_HAVE_DATA
        {
            get { return _CHK_HAVE_DATA; }
            set { _CHK_HAVE_DATA = value; }
        }

        private String _APP_NO;

        public String APP_NO
        {
            get { return _APP_NO; }
            set { _APP_NO = value; }
        }
        private String _USERID;

        public String USERID
        {
            get { return _USERID; }
            set { _USERID = value; }
        }
        private String _FromControl;

        public String FromControl
        {
            get { return _FromControl; }
            set { _FromControl = value; }
        }
        private NewBISSvcRef.U_NAME_ID _CUSTOMER_DATA;

        public NewBISSvcRef.U_NAME_ID CUSTOMER_DATA
        {
            get { return _CUSTOMER_DATA; }
            set { _CUSTOMER_DATA = value; }
        }

        private NewBISSvcRef.U_NAME_ID _PARENT_DATA;

        public NewBISSvcRef.U_NAME_ID PARENT_DATA
        {
            get { return _PARENT_DATA; }
            set { _PARENT_DATA = value; }
        }

        private bool _CLICK_FORM;

        public bool CLICK_FORM
        {
            get { return _CLICK_FORM; }
            set { _CLICK_FORM = value; }
        }

        public FormCustomerData()
        {
            InitializeComponent();
        }
        private String _PWD;

        public String PWD
        {
            get { return _PWD; }
            set { _PWD = value; }
        }


        private void FormCustomerData_Load(object sender, EventArgs e)
        {


            dgvCustomerOrdinary.AutoGenerateColumns = false;
            dgvCustomerOrdinary.RowHeadersVisible = false;
            dgvCustomerOrdinary.DataSource = null;

            dgvCustomerBancassurance.AutoGenerateColumns = false;
            dgvCustomerBancassurance.RowHeadersVisible = false;
            dgvCustomerBancassurance.DataSource = null;

            dgvCustomerGroupMortgage.AutoGenerateColumns = false;
            dgvCustomerGroupMortgage.RowHeadersVisible = false;
            dgvCustomerGroupMortgage.DataSource = null;

            dataGridCustomerHL.AutoGenerateColumns = false;
            dataGridCustomerHL.RowHeadersVisible = false;
            dataGridCustomerHL.DataSource = null;

            dataGridCustomerPD.AutoGenerateColumns = false;
            dataGridCustomerPD.RowHeadersVisible = false;
            dataGridCustomerPD.DataSource = null;

            dataGridCustomerPO.AutoGenerateColumns = false;
            dataGridCustomerPO.RowHeadersVisible = false;
            dataGridCustomerPO.DataSource = null;

            dataGridCustomerKB.AutoGenerateColumns = false;
            dataGridCustomerKB.RowHeadersVisible = false;
            dataGridCustomerKB.DataSource = null;

            dgvParentOrdinary.AutoGenerateColumns = false;
            dgvParentOrdinary.RowHeadersVisible = false;
            dgvParentOrdinary.DataSource = null;

            dgvParentBancassurance.AutoGenerateColumns = false;
            dgvParentBancassurance.RowHeadersVisible = false;
            dgvParentBancassurance.DataSource = null;

            dgvParentGroupMortgage.AutoGenerateColumns = false;
            dgvParentGroupMortgage.RowHeadersVisible = false;
            dgvParentGroupMortgage.DataSource = null;

            dgvParentHL.AutoGenerateColumns = false;
            dgvParentHL.RowHeadersVisible = false;
            dgvParentHL.DataSource = null;

            dgvParentPD.AutoGenerateColumns = false;
            dgvParentPD.RowHeadersVisible = false;
            dgvParentPD.DataSource = null;

            dgvParentPO.AutoGenerateColumns = false;
            dgvParentPO.RowHeadersVisible = false;
            dgvParentPO.DataSource = null;

            dgvParentKB.AutoGenerateColumns = false;
            dgvParentKB.RowHeadersVisible = false;
            dgvParentKB.DataSource = null;



            dgvCustomerMIB.AutoGenerateColumns = false;
            dgvCustomerMIB.RowHeadersVisible = false;
            dgvCustomerMIB.DataSource = null;

            dgvParentMIB.AutoGenerateColumns = false;
            dgvParentMIB.RowHeadersVisible = false;
            dgvParentMIB.DataSource = null;


            dgvCustomerAmlCtf.AutoGenerateColumns = false;
            dgvCustomerAmlCtf.RowHeadersVisible = false;
            dgvCustomerAmlCtf.DataSource = null;

            dgvParentAmlCtf.AutoGenerateColumns = false;
            dgvParentAmlCtf.RowHeadersVisible = false;
            dgvParentAmlCtf.DataSource = null;

            dgvCustomerBankrupt.AutoGenerateColumns = false;
            dgvCustomerBankrupt.RowHeadersVisible = false;
            dgvCustomerBankrupt.DataSource = null;

            dgvParentBankrupt.AutoGenerateColumns = false;
            dgvParentBankrupt.RowHeadersVisible = false;
            dgvParentBankrupt.DataSource = null;

            dgvCustomerRemark.AutoGenerateColumns = false;
            dgvCustomerRemark.RowHeadersVisible = false;
            dgvCustomerRemark.DataSource = null;

            dgvParentRemark.AutoGenerateColumns = false;
            dgvParentRemark.RowHeadersVisible = false;

            dgvParentRemark.DataSource = null;
            GetCustomerData();
        }

        public void GetCustomerData()
        {


            try
            {
                //if (CLICK_FORM == false)
                //{
                CLICK_FORM = true;

                W_MIB_COLL = null;
                CHK_HAVE_DATA = 0;
                HAVE_CUSPRODUCT = 0;
                HAVE_PARENTPRODUCT = 0;
                HAVE_MIB = 0;
                HAVE_AMLCTF = 0;
                HAVE_BANKRUPT = 0;
                HAVE_REMARK = 0;
                HAVE_CUSPRODUCTOTH = 0;
                HAVE_PARENTPRODUCTOTH = 0;
                RegistryKey localMachineReg = Registry.LocalMachine;

                RegistryKey softwareReg = localMachineReg.OpenSubKey("Software", true);

                RegistryKey bangkokLifeAssuranceReg = softwareReg.OpenSubKey("Bangkok Life Assurance", true);

                if (bangkokLifeAssuranceReg == null)
                {
                    RegistryKey a = softwareReg.OpenSubKey("Wow6432Node", true);

                    bangkokLifeAssuranceReg = a.OpenSubKey("Bangkok Life Assurance", true);
                    if (bangkokLifeAssuranceReg == null)
                        throw new Exception("กรุณาเข้าใช้ระบบผ่านทางเมนูระบบงาน !");
                }

                RegistryKey blaWinAppMenuReg = bangkokLifeAssuranceReg.OpenSubKey("BlaWinAppMenu", true);

                PWD = "";
                PWD = (string)blaWinAppMenuReg.GetValue("Password");
                //set ค่าเริ่มต้น ======================================================

                //จบ set ค่าเริ่มต้น ===================================================


                tabCustomerData.SelectTab("tabCustomerProduct");
                tabCustomerData.SelectTab("tabCustomerProductOther");
                SelectCustomerProductAll();

                tabCustomerData.SelectTab("tabParentProduct");
                tabCustomerData.SelectTab("tabParentProductOther");
                SelectParentProductAll();

                tabCustomerData.SelectTab("tabMIB");
                //ส่วนของ ข้อมูล MIB ของผู้เอาประกัน ==============================================
                SelectCustomerMIB();
                //จบส่วนของ ข้อมูล MIB ของผู้เอาประกัน ============================================
                //ส่วนของ ข้อมูล MIB ของผู้ปกครอง ===============================================
                SelectParentMIB();
                //จบส่วนของ ข้อมูล MIB ของผู้ปกครอง =============================================
                tabCustomerData.SelectTab("tabAmlCtf");
                //ส่วนของข้อมูลการฟอกเงิน ผู้เอาประกัน ==============================================
                SelectCustomerAmlCtf();
                //จบส่วนของข้อมูลการฟอกเงิน ผู้เอาประกัน ============================================
                //ส่วนของข้อมูลการฟอกเงิน ผู้ปกครอง ===============================================
                SelectParentAmlCtf();
                //จบส่วนของข้อมูลการฟอกเงิน ผู้ปกครอง =============================================
                tabCustomerData.SelectTab("tabBankrupt");
                //ส่วนของข้อมูลล้มละลายของลูกค้า ==================================================
                SelectCustomerBankrupt();
                //จบส่วนของข้อมูลล้มละลายของลูกค้า ================================================
                //ส่วนของข้อมูลล้มละลายของผู้ปกครอง ===============================================
                SelectParentBankrupt();
                //จบส่วนของข้อมูลล้มละลายของผู้ปกครอง =============================================
                tabCustomerData.SelectTab("tabRemark");
                //ส่วนของข้อมูลหมายเหตุของลูกค้า ==================================================
                SelectCustomerRemark();
                //จบส่วนของข้อมูลหมายเหตุของลูกค้า ==================================================
                //ส่วนของข้อมูลหมายเหตุของผู้ปกครอง ==================================================
                SelectParentRemark();
                //จบส่วนของข้อมูลหมายเหตุของผู้ปกครอง ==================================================

                if (HAVE_CUSPRODUCT == 0)
                {
                    tabCustomerData.TabPages.Remove(tabCustomerProduct);
                }

                if (HAVE_CUSPRODUCTOTH == 0)
                {
                    tabCustomerData.TabPages.Remove(tabCustomerProductOther);
                }

                if (HAVE_PARENTPRODUCT == 0)
                {
                    tabCustomerData.TabPages.Remove(tabParentProduct);
                }

                if (HAVE_PARENTPRODUCTOTH == 0)
                {
                    tabCustomerData.TabPages.Remove(tabParentProductOther);
                }

                if (HAVE_MIB == 0)
                {
                    tabCustomerData.TabPages.Remove(tabMIB);
                }

                if (HAVE_AMLCTF == 0)
                {
                    tabCustomerData.TabPages.Remove(tabAmlCtf);
                }

                if (HAVE_BANKRUPT == 0)
                {
                    tabCustomerData.TabPages.Remove(tabBankrupt);
                }

                if (HAVE_REMARK == 0)
                {
                    tabCustomerData.TabPages.Remove(tabRemark);
                }

                if (CHK_HAVE_DATA == 0)
                {
                    if (FromControl == "btnCustomerData")
                    {
                        MessageBox.Show("ไม่พบข้อมูล");
                    }
                    this.Close();
                }
                else
                {
                    //if (FromControl == "btnCustomerData")
                    //{
                    //    tabCustomerData.SelectTab(0);
                    //}
                    //else if (FromControl == "txtIdCardNo")
                    //{
                    //    tabCustomerData.SelectTab("tabCustomerProduct");
                    //}
                    tabCustomerData.SelectTab(0);
                }
            }


            //}
            catch (Exception ex)
            {
                CLICK_FORM = false;
                SetMsgException(ex);
            }
        }


        private void SelectCustomerRemark()
        {
            if (CUSTOMER_DATA != null)
            {
                if (!(CUSTOMER_DATA.IDCARD_NO == null && CUSTOMER_DATA.PASSPORT == null && CUSTOMER_DATA.NAME == null && CUSTOMER_DATA.SURNAME == null && CUSTOMER_DATA.SEX == null && CUSTOMER_DATA.BIRTH_DT == null))
                {
                    String cardType = "1";
                    String cardNo = "";
                    DateTime birthDate = CUSTOMER_DATA.BIRTH_DT == null ? DateTime.Now : CUSTOMER_DATA.BIRTH_DT.Value;
                    String sex = CUSTOMER_DATA.SEX == null ? "" : CUSTOMER_DATA.SEX.Value.ToString();
                    if (!(String.IsNullOrEmpty(CUSTOMER_DATA.IDCARD_NO)))
                    {
                        cardType = "1";
                        cardNo = CUSTOMER_DATA.IDCARD_NO;
                    }

                    if (!(String.IsNullOrEmpty(CUSTOMER_DATA.PASSPORT)))
                    {
                        cardType = "2";
                        cardNo = CUSTOMER_DATA.PASSPORT;
                    }

                    NewBISSvcRef.ProcessResult pr = new NewBISSvcRef.ProcessResult();
                    NewBISSvcRef.P_REMARK_COLLECTION pRemarkColl = new NewBISSvcRef.P_REMARK_COLLECTION();
                    using (NewBISSvcRef.NewBISSvcClient client = new NewBISSvcRef.NewBISSvcClient())
                    {
                        pRemarkColl = client.GetPRemakByCustomerData(out pr, cardType, cardNo, CUSTOMER_DATA.NAME, CUSTOMER_DATA.SURNAME);
                        if (pr.Successed == false)
                        {
                            throw new Exception(pr.ErrorMessage);
                        }
                    }

                    if (pRemarkColl != null && pRemarkColl.Count() > 0)
                    {
                        CHK_HAVE_DATA = CHK_HAVE_DATA + 1;
                        HAVE_REMARK = HAVE_REMARK + 1;

                        dgvCustomerRemark.DataSource = pRemarkColl;
                        int i = 0;
                        foreach (DataGridViewRow dr in dgvCustomerRemark.Rows)
                        {
                            if (dr.DataBoundItem is NewBISSvcRef.P_REMARK)
                            {
                                i = i + 1;
                                NewBISSvcRef.P_REMARK obj = (NewBISSvcRef.P_REMARK)dr.DataBoundItem;
                                dr.Cells["CRMNo"].Value = i.ToString();
                                dr.Cells["CRMPolicy"].Value = obj.POLICY;
                                dr.Cells["CRMCustName"].Value = obj.CUSTOMER_NAME;
                                dr.Cells["CRMSex"].Value = obj.SEX;
                                dr.Cells["CRMBirthDate"].Value = obj.BIRTH_DT == null ? "" : Utility.dateTimeToString(obj.BIRTH_DT.Value, "dd/MM/yyyy", "BU"); ;
                                dr.Cells["CRMIDcard"].Value = obj.IDCARD_NO;
                                dr.Cells["CRMRemarFlg"].Value = obj.REMARK_FLG;
                                dr.Cells["CRMOrderPerson"].Value = obj.ORDER_PERSON;
                                dr.Cells["CRMProcess"].Value = obj.PROCESS_FLG;
                                dr.Cells["CRMRemark"].Value = obj.REMARK;
                                dr.Cells["CRMPayMentBenefit"].Value = obj.PAYMENT_BENEFIT;
                                dr.Cells["CRMReceivePremium"].Value = obj.RECEIVE_PREMIUM;
                                dr.Cells["CRMRemarkTrnDate"].Value = obj.REMARK_TRN_DT == null ? "" : Utility.dateTimeToString(obj.REMARK_TRN_DT.Value, "dd/MM/yyyy", "BU");
                                dr.Cells["CRMAuditDate"].Value = obj.AUDIT_DT == null ? "" : Utility.dateTimeToString(obj.AUDIT_DT.Value, "dd/MM/yyyy", "BU");
                                dr.Cells["CRMAuditRef"].Value = obj.AUDIT_REF;
                                dr.Cells["CRMAuditRcvDate"].Value = obj.AUDIT_RCV_DT == null ? "" : Utility.dateTimeToString(obj.AUDIT_RCV_DT.Value, "dd/MM/yyyy", "BU");
                            }
                        }
                    }
                }
            }
        }

        private void SelectParentRemark()
        {
            if (PARENT_DATA != null)
            {
                if (!(PARENT_DATA.IDCARD_NO == null && PARENT_DATA.PASSPORT == null && PARENT_DATA.NAME == null && PARENT_DATA.SURNAME == null && PARENT_DATA.SEX == null && PARENT_DATA.BIRTH_DT == null))
                {
                    String cardType = "1";
                    String cardNo = "";
                    DateTime birthDate = PARENT_DATA.BIRTH_DT == null ? DateTime.Now : PARENT_DATA.BIRTH_DT.Value;
                    String sex = PARENT_DATA.SEX == null ? "" : PARENT_DATA.SEX.Value.ToString();
                    if (!(String.IsNullOrEmpty(PARENT_DATA.IDCARD_NO)))
                    {
                        cardType = "1";
                        cardNo = PARENT_DATA.IDCARD_NO;
                    }

                    if (!(String.IsNullOrEmpty(PARENT_DATA.PASSPORT)))
                    {
                        cardType = "2";
                        cardNo = PARENT_DATA.PASSPORT;
                    }

                    NewBISSvcRef.ProcessResult pr = new NewBISSvcRef.ProcessResult();
                    NewBISSvcRef.P_REMARK_COLLECTION pRemarkColl = new NewBISSvcRef.P_REMARK_COLLECTION();
                    using (NewBISSvcRef.NewBISSvcClient client = new NewBISSvcRef.NewBISSvcClient())
                    {
                        pRemarkColl = client.GetPRemakByCustomerData(out pr, cardType, cardNo, PARENT_DATA.NAME, PARENT_DATA.SURNAME);
                        if (pr.Successed == false)
                        {
                            throw new Exception(pr.ErrorMessage);
                        }
                    }

                    if (pRemarkColl != null && pRemarkColl.Count() > 0)
                    {
                        CHK_HAVE_DATA = CHK_HAVE_DATA + 1;
                        HAVE_REMARK = HAVE_REMARK + 1;

                        dgvParentRemark.DataSource = pRemarkColl;
                        int i = 0;
                        foreach (DataGridViewRow dr in dgvParentRemark.Rows)
                        {
                            if (dr.DataBoundItem is NewBISSvcRef.P_REMARK)
                            {
                                i = i + 1;
                                NewBISSvcRef.P_REMARK obj = (NewBISSvcRef.P_REMARK)dr.DataBoundItem;
                                dr.Cells["PRMNo"].Value = i.ToString();
                                dr.Cells["PRMPolicy"].Value = obj.POLICY;
                                dr.Cells["PRMCustName"].Value = obj.CUSTOMER_NAME;
                                dr.Cells["PRMSex"].Value = obj.SEX;
                                dr.Cells["PRMBirthDate"].Value = obj.BIRTH_DT == null ? "" : Utility.dateTimeToString(obj.BIRTH_DT.Value, "dd/MM/yyyy", "BU"); ;
                                dr.Cells["PRMIDcard"].Value = obj.IDCARD_NO;
                                dr.Cells["PRMRemarFlg"].Value = obj.REMARK_FLG;
                                dr.Cells["PRMOrderPerson"].Value = obj.ORDER_PERSON;
                                dr.Cells["PRMProcess"].Value = obj.PROCESS_FLG;
                                dr.Cells["PRMRemark"].Value = obj.REMARK;
                                dr.Cells["PRMPayMentBenefit"].Value = obj.PAYMENT_BENEFIT;
                                dr.Cells["PRMReceivePremium"].Value = obj.RECEIVE_PREMIUM;
                                dr.Cells["PRMRemarkTrnDate"].Value = obj.REMARK_TRN_DT == null ? "" : Utility.dateTimeToString(obj.REMARK_TRN_DT.Value, "dd/MM/yyyy", "BU");
                                dr.Cells["PRMAuditDate"].Value = obj.AUDIT_DT == null ? "" : Utility.dateTimeToString(obj.AUDIT_DT.Value, "dd/MM/yyyy", "BU");
                                dr.Cells["PRMAuditRef"].Value = obj.AUDIT_REF;
                                dr.Cells["PRMAuditRcvDate"].Value = obj.AUDIT_RCV_DT == null ? "" : Utility.dateTimeToString(obj.AUDIT_RCV_DT.Value, "dd/MM/yyyy", "BU");
                            }
                        }
                    }
                }
            }
        }

        private void SelectCustomerBankrupt()
        {
            if (CUSTOMER_DATA != null)
            {
                if (!(CUSTOMER_DATA.IDCARD_NO == null && CUSTOMER_DATA.PASSPORT == null && CUSTOMER_DATA.NAME == null && CUSTOMER_DATA.SURNAME == null && CUSTOMER_DATA.SEX == null && CUSTOMER_DATA.BIRTH_DT == null))
                {
                    String cardType = "1";
                    String cardNo = "";
                    DateTime birthDate = CUSTOMER_DATA.BIRTH_DT == null ? DateTime.Now : CUSTOMER_DATA.BIRTH_DT.Value;
                    String sex = CUSTOMER_DATA.SEX == null ? "" : CUSTOMER_DATA.SEX.Value.ToString();

                    if (!(String.IsNullOrEmpty(CUSTOMER_DATA.IDCARD_NO)))
                    {
                        cardType = "1";
                        cardNo = CUSTOMER_DATA.IDCARD_NO;
                    }

                    if (!(String.IsNullOrEmpty(CUSTOMER_DATA.PASSPORT)))
                    {
                        cardType = "2";
                        cardNo = CUSTOMER_DATA.PASSPORT;
                    }

                    BankruptSvcRef.BankruptCollection bankruptColl = new BankruptSvcRef.BankruptCollection();
                    using (BankruptSvcRef.BankruptServiceContractClient client = new BankruptSvcRef.BankruptServiceContractClient())
                    {
                        bankruptColl = client.LoadBankrupt(cardNo, CUSTOMER_DATA.NAME, CUSTOMER_DATA.SURNAME);
                    }

                    if (bankruptColl != null && bankruptColl.Count() > 0)
                    {
                        CHK_HAVE_DATA = CHK_HAVE_DATA + 1;
                        HAVE_BANKRUPT = HAVE_BANKRUPT + 1;

                        dgvCustomerBankrupt.DataSource = bankruptColl;
                        int i = 0;
                        foreach (DataGridViewRow dr in dgvCustomerBankrupt.Rows)
                        {
                            if (dr.DataBoundItem is BankruptSvcRef.Bankrupt)
                            {
                                i = i + 1;
                                BankruptSvcRef.Bankrupt obj = (BankruptSvcRef.Bankrupt)dr.DataBoundItem;
                                dr.Cells["CBRNo"].Value = i.ToString();
                                dr.Cells["CBRFullName"].Value = obj.FullName;
                                dr.Cells["CBRJudgMentDate"].Value = obj.JudgmentDate == null ? "" : Utility.dateTimeToString(obj.JudgmentDate, "dd/MM/yyyy", "BU");
                                dr.Cells["CBRCreatedOn"].Value = obj.CreatedOn == null ? "" : Utility.dateTimeToString(obj.CreatedOn, "dd/MM/yyyy", "BU");
                                dr.Cells["CBRCreatedBy"].Value = obj.CreatedBy;
                                dr.Cells["CBRCreatedNote"].Value = obj.CreatedNote;
                                dr.Cells["CBRRegisterCode"].Value = obj.RegisterCode;
                                if (obj.RegisterType == 0)
                                {
                                    dr.Cells["CBRRegisterType"].Value = "บุคคล";
                                }
                                else if (obj.RegisterType == 1)
                                {
                                    dr.Cells["CBRRegisterType"].Value = "บริษัท";
                                }
                                dr.Cells["CBRKeyCreatedOn"].Value = obj.KeyCreatedOn == null ? "" : Utility.dateTimeToString(obj.KeyCreatedOn, "dd/MM/yyyy", "BU");
                                dr.Cells["CBRKeyCreatedBy"].Value = obj.KeyCreatedBy;
                                dr.Cells["CBRKeyCreatedNote"].Value = obj.KeyCreatedNote;
                                dr.Cells["CBRDetail"].Value = obj.BankruptStatus.Detail;
                            }
                        }
                    }
                }
            }
        }

        private void SelectParentBankrupt()
        {
            if (PARENT_DATA != null)
            {
                if (!(PARENT_DATA.IDCARD_NO == null && PARENT_DATA.PASSPORT == null && PARENT_DATA.NAME == null && PARENT_DATA.SURNAME == null && PARENT_DATA.SEX == null && PARENT_DATA.BIRTH_DT == null))
                {
                    String cardType = "1";
                    String cardNo = "";
                    DateTime birthDate = PARENT_DATA.BIRTH_DT == null ? DateTime.Now : PARENT_DATA.BIRTH_DT.Value;
                    String sex = PARENT_DATA.SEX == null ? "" : PARENT_DATA.SEX.Value.ToString();

                    if (!(String.IsNullOrEmpty(PARENT_DATA.IDCARD_NO)))
                    {
                        cardType = "1";
                        cardNo = PARENT_DATA.IDCARD_NO;
                    }

                    if (!(String.IsNullOrEmpty(PARENT_DATA.PASSPORT)))
                    {
                        cardType = "2";
                        cardNo = PARENT_DATA.PASSPORT;
                    }

                    BankruptSvcRef.BankruptCollection bankruptColl = new BankruptSvcRef.BankruptCollection();
                    using (BankruptSvcRef.BankruptServiceContractClient client = new BankruptSvcRef.BankruptServiceContractClient())
                    {
                        bankruptColl = client.LoadBankrupt(cardNo, PARENT_DATA.NAME, PARENT_DATA.SURNAME);
                    }

                    if (bankruptColl != null && bankruptColl.Count() > 0)
                    {
                        CHK_HAVE_DATA = CHK_HAVE_DATA + 1;
                        HAVE_BANKRUPT = HAVE_BANKRUPT + 1;

                        dgvParentBankrupt.DataSource = bankruptColl;
                        int i = 0;
                        foreach (DataGridViewRow dr in dgvParentBankrupt.Rows)
                        {
                            if (dr.DataBoundItem is BankruptSvcRef.Bankrupt)
                            {
                                i = i + 1;
                                BankruptSvcRef.Bankrupt obj = (BankruptSvcRef.Bankrupt)dr.DataBoundItem;
                                dr.Cells["PBRNo"].Value = i.ToString();
                                dr.Cells["PBRFullName"].Value = obj.FullName;
                                dr.Cells["PBRJudgMentDate"].Value = obj.JudgmentDate == null ? "" : Utility.dateTimeToString(obj.JudgmentDate, "dd/MM/yyyy", "BU");
                                dr.Cells["PBRCreatedOn"].Value = obj.CreatedOn == null ? "" : Utility.dateTimeToString(obj.CreatedOn, "dd/MM/yyyy", "BU");
                                dr.Cells["PBRCreatedBy"].Value = obj.CreatedBy;
                                dr.Cells["PBRCreatedNote"].Value = obj.CreatedNote;
                                dr.Cells["PBRRegisterCode"].Value = obj.RegisterCode;
                                if (obj.RegisterType == 0)
                                {
                                    dr.Cells["PBRRegisterType"].Value = "บุคคล";
                                }
                                else if (obj.RegisterType == 1)
                                {
                                    dr.Cells["PBRRegisterType"].Value = "บริษัท";
                                }
                                dr.Cells["PBRKeyCreatedOn"].Value = obj.KeyCreatedOn == null ? "" : Utility.dateTimeToString(obj.KeyCreatedOn, "dd/MM/yyyy", "BU");
                                dr.Cells["PBRKeyCreatedBy"].Value = obj.KeyCreatedBy;
                                dr.Cells["PBRKeyCreatedNote"].Value = obj.KeyCreatedNote;
                                dr.Cells["PBRDetail"].Value = obj.BankruptStatus.Detail;
                            }
                        }
                    }
                }
            }
        }

        private void SelectCustomerAmlCtf()
        {
            if (CUSTOMER_DATA != null)
            {
                if (!(CUSTOMER_DATA.IDCARD_NO == null && CUSTOMER_DATA.PASSPORT == null && CUSTOMER_DATA.NAME == null && CUSTOMER_DATA.SURNAME == null && CUSTOMER_DATA.SEX == null && CUSTOMER_DATA.BIRTH_DT == null))
                {
                    String cardType = "1";
                    String cardNo = "";
                    DateTime birthDate = CUSTOMER_DATA.BIRTH_DT == null ? DateTime.Now : CUSTOMER_DATA.BIRTH_DT.Value;
                    String sex = CUSTOMER_DATA.SEX == null ? "" : CUSTOMER_DATA.SEX.Value.ToString();

                    if (!(String.IsNullOrEmpty(CUSTOMER_DATA.IDCARD_NO)))
                    {
                        cardType = "1";
                        cardNo = CUSTOMER_DATA.IDCARD_NO;
                    }

                    if (!(String.IsNullOrEmpty(CUSTOMER_DATA.PASSPORT)))
                    {
                        cardType = "2";
                        cardNo = CUSTOMER_DATA.PASSPORT;
                    }

                    NewBISSvcRef.ProcessResult pr = new NewBISSvcRef.ProcessResult();
                    NewBISSvcRef.P_AML_CTF_COLL amlCtfColl = new NewBISSvcRef.P_AML_CTF_COLL();
                    using (NewBISSvcRef.NewBISSvcClient client = new NewBISSvcRef.NewBISSvcClient())
                    {
                        amlCtfColl = client.GetPAmlCtfColl(out pr, cardType, cardNo, CUSTOMER_DATA.NAME, CUSTOMER_DATA.SURNAME);
                        if (pr.Successed == false)
                        {
                            throw new Exception(pr.ErrorMessage);
                        }
                    }

                    if (amlCtfColl != null && amlCtfColl.Count() > 0)
                    {
                        CHK_HAVE_DATA = CHK_HAVE_DATA + 1;
                        HAVE_AMLCTF = HAVE_AMLCTF + 1;

                        dgvCustomerAmlCtf.DataSource = amlCtfColl;
                        int i = 0;
                        foreach (DataGridViewRow dr in dgvCustomerAmlCtf.Rows)
                        {
                            if (dr.DataBoundItem is NewBISSvcRef.P_AML_CTF)
                            {
                                i = i + 1;
                                NewBISSvcRef.P_AML_CTF obj = (NewBISSvcRef.P_AML_CTF)dr.DataBoundItem;
                                dr.Cells["CACNo"].Value = i.ToString();
                                dr.Cells["CACChannel"].Value = obj.CHANNEL_TYPE;
                                dr.Cells["CACAppNo"].Value = obj.APP_NO;
                                dr.Cells["CACPolicy"].Value = obj.POLICY;
                                dr.Cells["CACPolYrLt"].Value = obj.POL_YR + "/" + obj.POL_LT;
                                dr.Cells["CACPMode"].Value = obj.P_MODE;
                                dr.Cells["CACCustFlg"].Value = obj.CUST_FLG;
                                dr.Cells["CACName"].Value = obj.NAME;
                                dr.Cells["CACIDCard"].Value = obj.IDCARD_NO;
                                dr.Cells["CACBirthDate"].Value = obj.BIRTH_DT == null ? "" : Utility.dateTimeToString(obj.BIRTH_DT.Value, "dd/MM/yyyy", "BU");
                                dr.Cells["CACSumm"].Value = obj.SUMM == null ? "0" : obj.SUMM.Value.ToString("n0");
                                dr.Cells["CACBscPrm"].Value = obj.BSC_PRM == null ? "0" : obj.BSC_PRM.Value.ToString("n0");
                                dr.Cells["CACRdrPrm"].Value = obj.RDR_PRM == null ? "0" : obj.RDR_PRM.Value.ToString("n0");
                                dr.Cells["CACAmloDate"].Value = obj.AMLO_DT == null ? "" : Utility.dateTimeToString(obj.AMLO_DT.Value, "dd/MM/yyyy", "BU");
                                dr.Cells["CACBnfPay"].Value = obj.BNF_PAY == null ? "0" : obj.BNF_PAY.Value.ToString("n0");
                                dr.Cells["CACUpdDate"].Value = obj.UPD_DT == null ? "" : Utility.dateTimeToString(obj.UPD_DT.Value, "dd/MM/yyyy", "BU");
                                dr.Cells["CACRiskClass"].Value = obj.RISK_CLASS;
                                dr.Cells["CACAmloSend"].Value = obj.AMLO_SEND;
                                dr.Cells["CACDoubtCause"].Value = obj.DOUBT_CAUSE;

                            }
                        }
                    }
                }
            }
        }

        private void SelectParentAmlCtf()
        {
            if (PARENT_DATA != null)
            {
                if (!(PARENT_DATA.IDCARD_NO == null && PARENT_DATA.PASSPORT == null && PARENT_DATA.NAME == null && PARENT_DATA.SURNAME == null && PARENT_DATA.SEX == null && PARENT_DATA.BIRTH_DT == null))
                {
                    String cardType = "1";
                    String cardNo = "";
                    DateTime birthDate = PARENT_DATA.BIRTH_DT == null ? DateTime.Now : PARENT_DATA.BIRTH_DT.Value;
                    String sex = PARENT_DATA.SEX == null ? "" : PARENT_DATA.SEX.Value.ToString();

                    if (!(String.IsNullOrEmpty(PARENT_DATA.IDCARD_NO)))
                    {
                        cardType = "1";
                        cardNo = PARENT_DATA.IDCARD_NO;
                    }

                    if (!(String.IsNullOrEmpty(PARENT_DATA.PASSPORT)))
                    {
                        cardType = "2";
                        cardNo = PARENT_DATA.PASSPORT;
                    }

                    NewBISSvcRef.ProcessResult pr = new NewBISSvcRef.ProcessResult();
                    NewBISSvcRef.P_AML_CTF_COLL amlCtfColl = new NewBISSvcRef.P_AML_CTF_COLL();
                    using (NewBISSvcRef.NewBISSvcClient client = new NewBISSvcRef.NewBISSvcClient())
                    {
                        amlCtfColl = client.GetPAmlCtfColl(out pr, cardType, cardNo, PARENT_DATA.NAME, PARENT_DATA.SURNAME);
                        if (pr.Successed == false)
                        {
                            throw new Exception(pr.ErrorMessage);
                        }
                    }

                    if (amlCtfColl != null && amlCtfColl.Count() > 0)
                    {
                        CHK_HAVE_DATA = CHK_HAVE_DATA + 1;
                        HAVE_AMLCTF = HAVE_AMLCTF + 1;

                        dgvParentAmlCtf.DataSource = amlCtfColl;
                        int i = 0;
                        foreach (DataGridViewRow dr in dgvParentAmlCtf.Rows)
                        {
                            if (dr.DataBoundItem is NewBISSvcRef.P_AML_CTF)
                            {
                                i = i + 1;
                                NewBISSvcRef.P_AML_CTF obj = (NewBISSvcRef.P_AML_CTF)dr.DataBoundItem;
                                dr.Cells["PACNo"].Value = i.ToString();
                                dr.Cells["PACChannel"].Value = obj.CHANNEL_TYPE;
                                dr.Cells["PACAppNo"].Value = obj.APP_NO;
                                dr.Cells["PACPolicy"].Value = obj.POLICY;
                                dr.Cells["PACPolYrLt"].Value = obj.POL_YR + "/" + obj.POL_LT;
                                dr.Cells["PACPMode"].Value = obj.P_MODE;
                                dr.Cells["PACCustFlg"].Value = obj.CUST_FLG;
                                dr.Cells["PACName"].Value = obj.NAME;
                                dr.Cells["PACIDCard"].Value = obj.IDCARD_NO;
                                dr.Cells["PACBirthDate"].Value = obj.BIRTH_DT == null ? "" : Utility.dateTimeToString(obj.BIRTH_DT.Value, "dd/MM/yyyy", "BU");
                                dr.Cells["PACSumm"].Value = obj.SUMM == null ? "0" : obj.SUMM.Value.ToString("n0");
                                dr.Cells["PACBscPrm"].Value = obj.BSC_PRM == null ? "0" : obj.BSC_PRM.Value.ToString("n0");
                                dr.Cells["PACRdrPrm"].Value = obj.RDR_PRM == null ? "0" : obj.RDR_PRM.Value.ToString("n0");
                                dr.Cells["PACAmloDate"].Value = obj.AMLO_DT == null ? "" : Utility.dateTimeToString(obj.AMLO_DT.Value, "dd/MM/yyyy", "BU");
                                dr.Cells["PACBnfPay"].Value = obj.BNF_PAY == null ? "0" : obj.BNF_PAY.Value.ToString("n0");
                                dr.Cells["PACUpdDate"].Value = obj.UPD_DT == null ? "" : Utility.dateTimeToString(obj.UPD_DT.Value, "dd/MM/yyyy", "BU");
                                dr.Cells["PACRiskClass"].Value = obj.RISK_CLASS;
                                dr.Cells["PACAmloSend"].Value = obj.AMLO_SEND;
                                dr.Cells["PACDoubtCause"].Value = obj.DOUBT_CAUSE;

                            }
                        }
                    }
                }
            }
        }

        private void SelectCustomerMIB()
        {
            if (CUSTOMER_DATA != null)
            {
                if (!(CUSTOMER_DATA.IDCARD_NO == null && CUSTOMER_DATA.PASSPORT == null && CUSTOMER_DATA.NAME == null && CUSTOMER_DATA.SURNAME == null && CUSTOMER_DATA.SEX == null && CUSTOMER_DATA.BIRTH_DT == null))
                {
                    String cardType = "1";
                    String cardNo = "";
                    DateTime birthDate = CUSTOMER_DATA.BIRTH_DT == null ? DateTime.Now : CUSTOMER_DATA.BIRTH_DT.Value;
                    String sex = CUSTOMER_DATA.SEX == null ? "" : CUSTOMER_DATA.SEX.Value.ToString();

                    if (!(String.IsNullOrEmpty(CUSTOMER_DATA.IDCARD_NO)))
                    {
                        cardType = "1";
                        cardNo = CUSTOMER_DATA.IDCARD_NO;
                    }

                    if (!(String.IsNullOrEmpty(CUSTOMER_DATA.PASSPORT)))
                    {
                        cardType = "2";
                        cardNo = CUSTOMER_DATA.PASSPORT;
                    }

                    NewBISSvcRef.ProcessResult pr = new NewBISSvcRef.ProcessResult();
                    NewBISSvcRef.P_YELLOW_CARD_COLLECTION blackListColl = new NewBISSvcRef.P_YELLOW_CARD_COLLECTION();
                    using (NewBISSvcRef.NewBISSvcClient client = new NewBISSvcRef.NewBISSvcClient())
                    {
                        blackListColl = client.GetBlackList(out pr, CUSTOMER_DATA.NAME, CUSTOMER_DATA.SURNAME, cardNo, sex, birthDate);
                        if (pr.Successed == false)
                        {
                            throw new Exception(pr.ErrorMessage);
                        }
                    }

                    if (blackListColl != null && blackListColl.Count() > 0)
                    {
                        CHK_HAVE_DATA = CHK_HAVE_DATA + 1;
                        HAVE_MIB = HAVE_MIB + 1;

                        dgvCustomerMIB.DataSource = blackListColl;
                        int i = 0;
                        foreach (DataGridViewRow dr in dgvCustomerMIB.Rows)
                        {
                            if (dr.DataBoundItem is NewBISSvcRef.P_YELLOW_CARD)
                            {
                                i = i + 1;
                                NewBISSvcRef.P_YELLOW_CARD obj = (NewBISSvcRef.P_YELLOW_CARD)dr.DataBoundItem;
                                dr.Cells["CMName"].Value = obj.PRENAME + obj.NAME + "  " + obj.SURNAME;
                                dr.Cells["CMIdCardNo"].Value = obj.IDCARD_NO;
                                dr.Cells["CMCompanyCode"].Value = obj.COMPANY_CODE;
                                dr.Cells["CMReference"].Value = obj.REFERENCE;
                                dr.Cells["CMResult"].Value = obj.RESULT;
                                dr.Cells["CMCause"].Value = obj.RESULT_CAUSE;
                                dr.Cells["CMNo"].Value = i.ToString();
                                dr.Cells["CMCode"].Value = obj.COMPANY_CODE + "/" + obj.CARD_RUN + "/" + obj.CARD_SEQ;
                                dr.Cells["CMBirthDate"].Value = obj.BIRTH_DT == null ? "" : Utility.dateTimeToString(obj.BIRTH_DT.Value, "dd/MM/yyyy", "BU");
                                if (obj.SEX == "M")
                                {
                                    dr.Cells["CMSex"].Value = "ชาย";
                                }
                                else
                                {
                                    dr.Cells["CMSex"].Value = "หญิง";
                                }
                            }
                        }
                    }
                }
            }
        }

        private void SelectParentMIB()
        {
            if (CUSTOMER_DATA != null)
            {
                if (!(PARENT_DATA.IDCARD_NO == null && PARENT_DATA.PASSPORT == null && PARENT_DATA.NAME == null && PARENT_DATA.SURNAME == null && PARENT_DATA.SEX == null && PARENT_DATA.BIRTH_DT == null))
                {
                    String cardType = "1";
                    String cardNo = "";
                    DateTime birthDate = PARENT_DATA.BIRTH_DT == null ? DateTime.Now : PARENT_DATA.BIRTH_DT.Value;
                    String sex = PARENT_DATA.SEX == null ? "" : PARENT_DATA.SEX.Value.ToString();

                    if (!(String.IsNullOrEmpty(PARENT_DATA.IDCARD_NO)))
                    {
                        cardType = "1";
                        cardNo = PARENT_DATA.IDCARD_NO;
                    }

                    if (!(String.IsNullOrEmpty(PARENT_DATA.PASSPORT)))
                    {
                        cardType = "2";
                        cardNo = PARENT_DATA.PASSPORT;
                    }

                    NewBISSvcRef.ProcessResult pr = new NewBISSvcRef.ProcessResult();
                    NewBISSvcRef.P_YELLOW_CARD_COLLECTION blackListColl = new NewBISSvcRef.P_YELLOW_CARD_COLLECTION();
                    using (NewBISSvcRef.NewBISSvcClient client = new NewBISSvcRef.NewBISSvcClient())
                    {
                        blackListColl = client.GetBlackList(out pr, PARENT_DATA.NAME, PARENT_DATA.SURNAME, cardNo, sex, birthDate);
                        if (pr.Successed == false)
                        {
                            throw new Exception(pr.ErrorMessage);
                        }
                    }

                    if (blackListColl != null && blackListColl.Count() > 0)
                    {
                        CHK_HAVE_DATA = CHK_HAVE_DATA + 1;
                        HAVE_MIB = HAVE_MIB + 1;

                        dgvParentMIB.DataSource = blackListColl;
                        int i = 0;
                        foreach (DataGridViewRow dr in dgvParentMIB.Rows)
                        {
                            if (dr.DataBoundItem is NewBISSvcRef.P_YELLOW_CARD)
                            {
                                i = i + 1;
                                NewBISSvcRef.P_YELLOW_CARD obj = (NewBISSvcRef.P_YELLOW_CARD)dr.DataBoundItem;
                                dr.Cells["PMName"].Value = obj.PRENAME + obj.NAME + "  " + obj.SURNAME;
                                dr.Cells["PMIdCardNo"].Value = obj.IDCARD_NO;
                                dr.Cells["PMCompanyCode"].Value = obj.COMPANY_CODE;
                                dr.Cells["PMReference"].Value = obj.REFERENCE;
                                dr.Cells["PMResult"].Value = obj.RESULT;
                                dr.Cells["PMCause"].Value = obj.RESULT_CAUSE;
                                dr.Cells["PMNo"].Value = i.ToString();
                                dr.Cells["PMCode"].Value = obj.COMPANY_CODE + "/" + obj.CARD_RUN + "/" + obj.CARD_SEQ;
                                dr.Cells["PMBirthDate"].Value = obj.BIRTH_DT == null ? "" : Utility.dateTimeToString(obj.BIRTH_DT.Value, "dd/MM/yyyy", "BU");
                                if (obj.SEX == "M")
                                {
                                    dr.Cells["PMSex"].Value = "ชาย";
                                }
                                else
                                {
                                    dr.Cells["PMSex"].Value = "หญิง";
                                }
                            }
                        }
                    }
                }
            }
        }

        private void SelectCustomerProductAll()
        {
            if (CUSTOMER_DATA != null)
            {
                if (!(CUSTOMER_DATA.IDCARD_NO == null && CUSTOMER_DATA.PASSPORT == null && CUSTOMER_DATA.NAME == null && CUSTOMER_DATA.SURNAME == null && CUSTOMER_DATA.SEX == null && CUSTOMER_DATA.BIRTH_DT == null))
                {

                    NewBisPASvcRef.ProcessResult pr = new NewBisPASvcRef.ProcessResult();
                    NewBisPASvcRef.POLICY_ALL_OF_SUMM_COLLECTION customerDataColl = new NewBisPASvcRef.POLICY_ALL_OF_SUMM_COLLECTION();
                    String idcard = "";
                    if (!(String.IsNullOrEmpty(CUSTOMER_DATA.IDCARD_NO)))
                    {
                        idcard = CUSTOMER_DATA.IDCARD_NO;
                    }
                    if (!(String.IsNullOrEmpty(CUSTOMER_DATA.PASSPORT)))
                    {
                        idcard = CUSTOMER_DATA.PASSPORT;
                    }
                    String sex = CUSTOMER_DATA.SEX == null ? "" : CUSTOMER_DATA.SEX.Value.ToString();
                    using (NewBisPASvcRef.NewBisPASvcClient client = new NewBisPASvcRef.NewBisPASvcClient())
                    {
                        pr = client.GetPolicyAndAppAllChannel(out customerDataColl, idcard, CUSTOMER_DATA.NAME, CUSTOMER_DATA.SURNAME, sex, CUSTOMER_DATA.BIRTH_DT, APP_NO);
                        if (pr.Successed == false)
                        {
                            throw new Exception(pr.ErrorMessage);
                        }
                    }

                    if (customerDataColl != null && customerDataColl.Count() > 0)
                    {
                        CHK_HAVE_DATA = CHK_HAVE_DATA + 1;
                        SelectProductOfCustomer1(customerDataColl);
                        SelectProductOfCustomer2(customerDataColl);
                    }
                }
            }
        }

        private void SelectProductOfCustomer1(NewBisPASvcRef.POLICY_ALL_OF_SUMM_COLLECTION customerDataColl)
        {
            Int64 totalSummCustomer1 = 0;
            var GetDataOD = from getData in customerDataColl
                            where getData.CHANNEL_TYPE == "OD"
                            && getData.BANC_TYPE == "N"
                            orderby getData.POLICY ascending, getData.CUSTOMER_TYPE descending
                            select getData;
            if (GetDataOD != null && GetDataOD.Count() > 0)
            {
                HAVE_CUSPRODUCT = HAVE_CUSPRODUCT + 1;

                dgvCustomerOrdinary.DataSource = GetDataOD.ToArray();
                int i = 0;
                String productType = "";
                Int64 totalSummOfOD = 0;
                foreach (DataGridViewRow dr in dgvCustomerOrdinary.Rows)
                {
                    if (dr.DataBoundItem is NewBisPASvcRef.POLICY_ALL_OF_SUMM)
                    {
                        i = i + 1;
                        NewBisPASvcRef.POLICY_ALL_OF_SUMM obj = (NewBisPASvcRef.POLICY_ALL_OF_SUMM)dr.DataBoundItem;
                        dr.Cells["CPONo"].Value = i.ToString("n0");

                        if (obj.CUSTOMER_TYPE == "C" && obj.PRODUCT_TYPE == "POL" && obj.PLAN_TYPE == "L")
                        {
                            productType = "P";
                        }
                        else if (obj.CUSTOMER_TYPE == "C" && obj.PRODUCT_TYPE == "POL" && obj.PLAN_TYPE == "R")
                        {
                            productType = "P/R";
                        }
                        else if (obj.CUSTOMER_TYPE == "P" && obj.PRODUCT_TYPE == "POL" && obj.PLAN_TYPE == "R")
                        {
                            productType = "P/R (Parent)";
                        }
                        else if (obj.CUSTOMER_TYPE == "C" && obj.PRODUCT_TYPE == "APP" && obj.PLAN_TYPE == "L")
                        {
                            productType = "A";
                        }
                        else if (obj.CUSTOMER_TYPE == "C" && obj.PRODUCT_TYPE == "APP" && obj.PLAN_TYPE == "R")
                        {
                            productType = "A/R";
                        }
                        else if (obj.CUSTOMER_TYPE == "P" && obj.PRODUCT_TYPE == "APP" && obj.PLAN_TYPE == "R")
                        {
                            productType = "A/R (Parent)";
                        }
                        dr.Cells["CPOType"].Value = productType;
                        dr.Cells["CPOPolicyNo"].Value = obj.POLICY;
                        dr.Cells["CPOPlan"].Value = obj.PL_BLOCK + obj.PL_CODE + "/" + obj.PL_CODE2 + " " + obj.PLAN_NAME;
                        dr.Cells["CPOSumm"].Value = obj.SUMM == null ? "" : obj.SUMM.Value.ToString("n0");
                        dr.Cells["CPOInfSumm"].Value = obj.INFSUMM == null ? "" : obj.INFSUMM.Value.ToString("n0");
                        dr.Cells["CPOStatus"].Value = obj.STATUS_DESC;
                        dr.Cells["CPOStandard"].Value = obj.STANDARD_DESC;
                        dr.Cells["CPOIsuDate"].Value = obj.ISU_DT == null ? "" : Utility.dateTimeToString(obj.ISU_DT.Value, "dd/MM/yyyy", "BU");
                        dr.Cells["CPOClaim"].Value = obj.CLAIM;
                        dr.Cells["CPOExclude"].Value = obj.EXCLUDE;
                        //dr.Cells["CPOProductType"].Value = obj.PRODUCT_TYPE;
                        //dr.Cells["CPOPolicyID"].Value = obj.POLICY_ID;
                        if (obj.PRODUCT_TYPE == "POL")
                        {
                            if (obj.PLAN_TYPE == "L")
                            {
                                if ((obj.STATUS == "10") || (obj.STATUS == "11") || (obj.STATUS == "12") || (obj.STATUS == "13"))
                                {
                                    totalSummOfOD = totalSummOfOD + Convert.ToInt64(obj.INFSUMM);
                                }
                            }
                            if (obj.PLAN_TYPE == "R")
                            {
                                if ((obj.STATUS == "WC") || (obj.STATUS == "FA") || (obj.STATUS == "FC") || (obj.STATUS == "FR"))
                                {
                                    totalSummOfOD = totalSummOfOD + Convert.ToInt64(obj.INFSUMM);
                                }
                            }
                        }
                        else
                        {
                            totalSummOfOD = totalSummOfOD + Convert.ToInt64(obj.INFSUMM);
                        }
                    }
                }
                totalSummCustomer1 = totalSummCustomer1 + totalSummOfOD;

                txtSumSummOfOrdinary.Text = totalSummOfOD.ToString("n0");

            }

            var GetDataBnc = from getData in customerDataColl
                             where getData.CHANNEL_TYPE == "OD"
                             && getData.BANC_TYPE == "Y"
                             orderby getData.POLICY ascending, getData.CUSTOMER_TYPE descending
                             select getData;
            if (GetDataBnc != null && GetDataBnc.Count() > 0)
            {
                HAVE_CUSPRODUCT = HAVE_CUSPRODUCT + 1;

                dgvCustomerBancassurance.DataSource = GetDataBnc.ToArray();
                int i = 0;
                String productType = "";
                Int64 totalSummOfBnc = 0;
                foreach (DataGridViewRow dr in dgvCustomerBancassurance.Rows)
                {
                    if (dr.DataBoundItem is NewBisPASvcRef.POLICY_ALL_OF_SUMM)
                    {
                        i = i + 1;
                        NewBisPASvcRef.POLICY_ALL_OF_SUMM obj = (NewBisPASvcRef.POLICY_ALL_OF_SUMM)dr.DataBoundItem;
                        dr.Cells["CPBNo"].Value = i.ToString("n0");

                        if (obj.CUSTOMER_TYPE == "C" && obj.PRODUCT_TYPE == "POL" && obj.PLAN_TYPE == "L")
                        {
                            productType = "P";
                        }
                        else if (obj.CUSTOMER_TYPE == "C" && obj.PRODUCT_TYPE == "POL" && obj.PLAN_TYPE == "R")
                        {
                            productType = "P/R";
                        }
                        else if (obj.CUSTOMER_TYPE == "P" && obj.PRODUCT_TYPE == "POL" && obj.PLAN_TYPE == "R")
                        {
                            productType = "P/R (Parent)";
                        }
                        else if (obj.CUSTOMER_TYPE == "C" && obj.PRODUCT_TYPE == "APP" && obj.PLAN_TYPE == "L")
                        {
                            productType = "A";
                        }
                        else if (obj.CUSTOMER_TYPE == "C" && obj.PRODUCT_TYPE == "APP" && obj.PLAN_TYPE == "R")
                        {
                            productType = "A/R";
                        }
                        else if (obj.CUSTOMER_TYPE == "P" && obj.PRODUCT_TYPE == "APP" && obj.PLAN_TYPE == "R")
                        {
                            productType = "A/R (Parent)";
                        }
                        dr.Cells["CPBType"].Value = productType;
                        dr.Cells["CPBPolicyNo"].Value = obj.POLICY;
                        dr.Cells["CPBPlan"].Value = obj.PL_BLOCK + obj.PL_CODE + "/" + obj.PL_CODE2 + " " + obj.PLAN_NAME;
                        dr.Cells["CPBSumm"].Value = obj.SUMM == null ? "" : obj.SUMM.Value.ToString("n0");
                        dr.Cells["CPBInfSumm"].Value = obj.INFSUMM == null ? "" : obj.INFSUMM.Value.ToString("n0");
                        dr.Cells["CPBStatus"].Value = obj.STATUS_DESC;
                        dr.Cells["CPBStandard"].Value = obj.STANDARD_DESC;
                        dr.Cells["CPBIsuDate"].Value = obj.ISU_DT == null ? "" : Utility.dateTimeToString(obj.ISU_DT.Value, "dd/MM/yyyy", "BU");
                        dr.Cells["CPBClaim"].Value = obj.CLAIM;
                        dr.Cells["CPBExclude"].Value = obj.EXCLUDE;
                        dr.Cells["CPBProductType"].Value = obj.PRODUCT_TYPE;
                        dr.Cells["CPBPolicyID"].Value = obj.POLICY_ID == null ? "" : obj.POLICY_ID.Value.ToString();
                        if (obj.PRODUCT_TYPE == "POL")
                        {
                            if (obj.PLAN_TYPE == "L")
                            {
                                if ((obj.STATUS == "10") || (obj.STATUS == "11") || (obj.STATUS == "12") || (obj.STATUS == "13"))
                                {
                                    totalSummOfBnc = totalSummOfBnc + Convert.ToInt64(obj.INFSUMM);
                                }
                            }
                            if (obj.PLAN_TYPE == "R")
                            {
                                if ((obj.STATUS == "WC") || (obj.STATUS == "FA") || (obj.STATUS == "FC") || (obj.STATUS == "FR"))
                                {
                                    totalSummOfBnc = totalSummOfBnc + Convert.ToInt64(obj.INFSUMM);
                                }
                            }
                        }
                        else
                        {
                            totalSummOfBnc = totalSummOfBnc + Convert.ToInt64(obj.INFSUMM);
                        }

                    }
                }

                totalSummCustomer1 = totalSummCustomer1 + totalSummOfBnc;
                txtSumSummOfBancassurance.Text = totalSummOfBnc.ToString("n0");
            }

            var GetDataGM = from getData in customerDataColl
                            where getData.CHANNEL_TYPE == "GM"
                            orderby getData.POLICY ascending, getData.CUSTOMER_TYPE descending
                            select getData;

            if (GetDataGM != null && GetDataGM.Count() > 0)
            {
                HAVE_CUSPRODUCT = HAVE_CUSPRODUCT + 1;

                dgvCustomerGroupMortgage.DataSource = GetDataGM.ToArray();
                int i = 0;
                String productType = "";
                Int64 totalSummOfGM = 0;
                foreach (DataGridViewRow dr in dgvCustomerGroupMortgage.Rows)
                {
                    if (dr.DataBoundItem is NewBisPASvcRef.POLICY_ALL_OF_SUMM)
                    {
                        i = i + 1;
                        NewBisPASvcRef.POLICY_ALL_OF_SUMM obj = (NewBisPASvcRef.POLICY_ALL_OF_SUMM)dr.DataBoundItem;
                        dr.Cells["GMCNo"].Value = i.ToString("n0");

                        if (obj.CUSTOMER_TYPE == "C" && obj.PRODUCT_TYPE == "POL" && obj.PLAN_TYPE == "L")
                        {
                            productType = "P";
                        }
                        else if (obj.CUSTOMER_TYPE == "C" && obj.PRODUCT_TYPE == "POL" && obj.PLAN_TYPE == "R")
                        {
                            productType = "P/R";
                        }
                        else if (obj.CUSTOMER_TYPE == "P" && obj.PRODUCT_TYPE == "POL" && obj.PLAN_TYPE == "R")
                        {
                            productType = "P/R (Parent)";
                        }
                        else if (obj.CUSTOMER_TYPE == "C" && obj.PRODUCT_TYPE == "APP" && obj.PLAN_TYPE == "L")
                        {
                            productType = "A";
                        }
                        else if (obj.CUSTOMER_TYPE == "C" && obj.PRODUCT_TYPE == "APP" && obj.PLAN_TYPE == "R")
                        {
                            productType = "A/R";
                        }
                        else if (obj.CUSTOMER_TYPE == "P" && obj.PRODUCT_TYPE == "APP" && obj.PLAN_TYPE == "R")
                        {
                            productType = "A/R (Parent)";
                        }
                        dr.Cells["GMCType"].Value = productType;
                        dr.Cells["GMCPolicyNo"].Value = obj.POLICY;
                        dr.Cells["GMCPlan"].Value = obj.PL_BLOCK + obj.PL_CODE + "/" + obj.PL_CODE2 + " " + obj.PLAN_NAME;
                        dr.Cells["GMCSumm"].Value = obj.SUMM == null ? "" : obj.SUMM.Value.ToString("n0");
                        dr.Cells["GMCInfSumm"].Value = obj.INFSUMM == null ? "" : obj.INFSUMM.Value.ToString("n0");
                        dr.Cells["GMCStatus"].Value = obj.STATUS_DESC;
                        dr.Cells["GMCStandard"].Value = obj.STANDARD_DESC;
                        dr.Cells["GMCIsuDate"].Value = obj.ISU_DT == null ? "" : Utility.dateTimeToString(obj.ISU_DT.Value, "dd/MM/yyyy", "BU");
                        dr.Cells["GMCClaim"].Value = obj.CLAIM;
                        dr.Cells["GMCExclude"].Value = obj.EXCLUDE;
                        dr.Cells["GMCProductType"].Value = obj.PRODUCT_TYPE;
                        dr.Cells["GMCPolicyID"].Value = obj.POLICY_ID == null ? "" : obj.POLICY_ID.Value.ToString();
                        if (obj.PRODUCT_TYPE == "POL")
                        {
                            if (obj.PLAN_TYPE == "L")
                            {
                                if ((obj.STATUS == "10") || (obj.STATUS == "11") || (obj.STATUS == "12") || (obj.STATUS == "13"))
                                {
                                    totalSummOfGM = totalSummOfGM + Convert.ToInt64(obj.INFSUMM);
                                }
                            }
                            if (obj.PLAN_TYPE == "R")
                            {
                                if ((obj.STATUS == "WC") || (obj.STATUS == "FA") || (obj.STATUS == "FC") || (obj.STATUS == "FR"))
                                {
                                    totalSummOfGM = totalSummOfGM + Convert.ToInt64(obj.INFSUMM);
                                }
                            }
                        }
                        else
                        {
                            totalSummOfGM = totalSummOfGM + Convert.ToInt64(obj.INFSUMM);
                        }

                    }
                }

                totalSummCustomer1 = totalSummCustomer1 + totalSummOfGM;
                txtSumSummOfGroupMortgage.Text = totalSummOfGM.ToString("n0");

            }

            var GetDataHL = from getData in customerDataColl
                            where getData.CHANNEL_TYPE == "HL"
                            orderby getData.POLICY ascending, getData.CUSTOMER_TYPE descending
                            select getData;

            if (GetDataHL != null && GetDataHL.Count() > 0)
            {
                HAVE_CUSPRODUCT = HAVE_CUSPRODUCT + 1;

                dataGridCustomerHL.DataSource = GetDataHL.ToArray();
                int i = 0;
                String productType = "";
                Int64 totalSummOfHL = 0;
                foreach (DataGridViewRow dr in dataGridCustomerHL.Rows)
                {
                    if (dr.DataBoundItem is NewBisPASvcRef.POLICY_ALL_OF_SUMM)
                    {
                        i = i + 1;
                        NewBisPASvcRef.POLICY_ALL_OF_SUMM obj = (NewBisPASvcRef.POLICY_ALL_OF_SUMM)dr.DataBoundItem;
                        dr.Cells["HLCNo"].Value = i.ToString("n0");

                        if (obj.CUSTOMER_TYPE == "C" && obj.PRODUCT_TYPE == "POL" && obj.PLAN_TYPE == "L")
                        {
                            productType = "P";
                        }
                        else if (obj.CUSTOMER_TYPE == "C" && obj.PRODUCT_TYPE == "POL" && obj.PLAN_TYPE == "R")
                        {
                            productType = "P/R";
                        }
                        else if (obj.CUSTOMER_TYPE == "P" && obj.PRODUCT_TYPE == "POL" && obj.PLAN_TYPE == "R")
                        {
                            productType = "P/R (Parent)";
                        }
                        else if (obj.CUSTOMER_TYPE == "C" && obj.PRODUCT_TYPE == "APP" && obj.PLAN_TYPE == "L")
                        {
                            productType = "A";
                        }
                        else if (obj.CUSTOMER_TYPE == "C" && obj.PRODUCT_TYPE == "APP" && obj.PLAN_TYPE == "R")
                        {
                            productType = "A/R";
                        }
                        else if (obj.CUSTOMER_TYPE == "P" && obj.PRODUCT_TYPE == "APP" && obj.PLAN_TYPE == "R")
                        {
                            productType = "A/R (Parent)";
                        }
                        dr.Cells["HLCType"].Value = productType;
                        dr.Cells["HLCPolicyNo"].Value = obj.POLICY;
                        dr.Cells["HLCPlan"].Value = obj.PL_BLOCK + obj.PL_CODE + "/" + obj.PL_CODE2 + " " + obj.PLAN_NAME;
                        dr.Cells["HLCSumm"].Value = obj.SUMM == null ? "" : obj.SUMM.Value.ToString("n0");
                        dr.Cells["HLCInfSumm"].Value = obj.INFSUMM == null ? "" : obj.INFSUMM.Value.ToString("n0");
                        dr.Cells["HLCStatus"].Value = obj.STATUS_DESC;
                        dr.Cells["HLCStandard"].Value = obj.STANDARD_DESC;
                        dr.Cells["HLCIsuDate"].Value = obj.ISU_DT == null ? "" : Utility.dateTimeToString(obj.ISU_DT.Value, "dd/MM/yyyy", "BU");
                        dr.Cells["HLCClaim"].Value = obj.CLAIM;
                        dr.Cells["HLCExclude"].Value = obj.EXCLUDE;
                        dr.Cells["HLCProductType"].Value = obj.PRODUCT_TYPE;
                        dr.Cells["HLCPolicyID"].Value = obj.POLICY_ID == null ? "" : obj.POLICY_ID.Value.ToString();
                        if (obj.PRODUCT_TYPE == "POL")
                        {
                            if (obj.PLAN_TYPE == "L")
                            {
                                if ((obj.STATUS == "10") || (obj.STATUS == "11") || (obj.STATUS == "12") || (obj.STATUS == "13"))
                                {
                                    totalSummOfHL = totalSummOfHL + Convert.ToInt64(obj.INFSUMM);
                                }
                            }
                            if (obj.PLAN_TYPE == "R")
                            {
                                if ((obj.STATUS == "WC") || (obj.STATUS == "FA") || (obj.STATUS == "FC") || (obj.STATUS == "FR"))
                                {
                                    totalSummOfHL = totalSummOfHL + Convert.ToInt64(obj.INFSUMM);
                                }
                            }
                        }
                        else
                        {
                            totalSummOfHL = totalSummOfHL + Convert.ToInt64(obj.INFSUMM);
                        }
                    }
                }

                totalSummCustomer1 = totalSummCustomer1 + totalSummOfHL;
                txtSumSummOfHL.Text = totalSummOfHL.ToString("n0");

            }

            txtTotalSumm.Text = totalSummCustomer1.ToString("n0");
        }
        private void SelectProductOfCustomer2(NewBisPASvcRef.POLICY_ALL_OF_SUMM_COLLECTION customerDataColl)
        {
            Int64 totalSummCustomer1 = 0;
            var GetDataPD = from getData in customerDataColl
                            where getData.CHANNEL_TYPE == "PD" || getData.CHANNEL_TYPE == "PF"
                            orderby getData.POLICY ascending, getData.CUSTOMER_TYPE descending
                            select getData;
            if (GetDataPD != null && GetDataPD.Count() > 0)
            {
                HAVE_CUSPRODUCTOTH = HAVE_CUSPRODUCTOTH + 1;
                dataGridCustomerPD.DataSource = GetDataPD.ToArray();
                int i = 0;
                String productType = "";
                Int64 totalSummOfPD = 0;
                foreach (DataGridViewRow dr in dataGridCustomerPD.Rows)
                {
                    if (dr.DataBoundItem is NewBisPASvcRef.POLICY_ALL_OF_SUMM)
                    {
                        i = i + 1;
                        NewBisPASvcRef.POLICY_ALL_OF_SUMM obj = (NewBisPASvcRef.POLICY_ALL_OF_SUMM)dr.DataBoundItem;
                        dr.Cells["PDCNo"].Value = i.ToString("n0");

                        if (obj.CUSTOMER_TYPE == "C" && obj.PRODUCT_TYPE == "POL" && obj.PLAN_TYPE == "L")
                        {
                            productType = "P";
                        }
                        else if (obj.CUSTOMER_TYPE == "C" && obj.PRODUCT_TYPE == "POL" && obj.PLAN_TYPE == "R")
                        {
                            productType = "P/R";
                        }
                        else if (obj.CUSTOMER_TYPE == "P" && obj.PRODUCT_TYPE == "POL" && obj.PLAN_TYPE == "R")
                        {
                            productType = "P/R (Parent)";
                        }
                        else if (obj.CUSTOMER_TYPE == "C" && obj.PRODUCT_TYPE == "APP" && obj.PLAN_TYPE == "L")
                        {
                            productType = "A";
                        }
                        else if (obj.CUSTOMER_TYPE == "C" && obj.PRODUCT_TYPE == "APP" && obj.PLAN_TYPE == "R")
                        {
                            productType = "A/R";
                        }
                        else if (obj.CUSTOMER_TYPE == "P" && obj.PRODUCT_TYPE == "APP" && obj.PLAN_TYPE == "R")
                        {
                            productType = "A/R (Parent)";
                        }
                        dr.Cells["PDCType"].Value = productType;
                        dr.Cells["PDCPolicyNo"].Value = obj.POLICY;
                        dr.Cells["PDCPlan"].Value = obj.PL_BLOCK + obj.PL_CODE + "/" + obj.PL_CODE2 + " " + obj.PLAN_NAME;
                        dr.Cells["PDCSumm"].Value = obj.SUMM == null ? "" : obj.SUMM.Value.ToString("n0");
                        dr.Cells["PDCInfSumm"].Value = obj.INFSUMM == null ? "" : obj.INFSUMM.Value.ToString("n0");
                        dr.Cells["PDCStatus"].Value = obj.STATUS_DESC;
                        dr.Cells["PDCStandard"].Value = obj.STANDARD_DESC;
                        dr.Cells["PDCIsuDate"].Value = obj.ISU_DT == null ? "" : Utility.dateTimeToString(obj.ISU_DT.Value, "dd/MM/yyyy", "BU");
                        dr.Cells["PDCClaim"].Value = obj.CLAIM;
                        dr.Cells["PDCExclude"].Value = obj.EXCLUDE;
                        dr.Cells["PDCProductType"].Value = obj.PRODUCT_TYPE;
                        dr.Cells["PDCPolicyID"].Value = obj.POLICY_ID == null ? "" : obj.POLICY_ID.Value.ToString();
                        //dr.Cells["CPOProductType"].Value = obj.PRODUCT_TYPE;
                        //dr.Cells["CPOPolicyID"].Value = obj.POLICY_ID;
                        if (obj.PRODUCT_TYPE == "POL")
                        {
                            if (obj.PLAN_TYPE == "L")
                            {
                                if ((obj.STATUS == "10") || (obj.STATUS == "11") || (obj.STATUS == "12") || (obj.STATUS == "13"))
                                {
                                    totalSummOfPD = totalSummOfPD + Convert.ToInt64(obj.INFSUMM);
                                }
                            }
                            if (obj.PLAN_TYPE == "R")
                            {
                                if ((obj.STATUS == "WC") || (obj.STATUS == "FA") || (obj.STATUS == "FC") || (obj.STATUS == "FR"))
                                {
                                    totalSummOfPD = totalSummOfPD + Convert.ToInt64(obj.INFSUMM);
                                }
                            }
                        }
                        else
                        {
                            totalSummOfPD = totalSummOfPD + Convert.ToInt64(obj.INFSUMM);
                        }

                    }
                }

                totalSummCustomer1 = totalSummCustomer1 + totalSummOfPD;
                txtTotalSummPD.Text = totalSummOfPD.ToString("n0");
            }

            var GetDataPO = from getData in customerDataColl
                            where getData.CHANNEL_TYPE == "PO" || getData.CHANNEL_TYPE == "PN"
                            orderby getData.POLICY ascending, getData.CUSTOMER_TYPE descending
                            select getData;
            if (GetDataPO != null && GetDataPO.Count() > 0)
            {
                HAVE_CUSPRODUCTOTH = HAVE_CUSPRODUCTOTH + 1;
                dataGridCustomerPO.DataSource = GetDataPO.ToArray();
                int i = 0;
                String productType = "";
                Int64 totalSummOfPO = 0;
                foreach (DataGridViewRow dr in dataGridCustomerPO.Rows)
                {
                    if (dr.DataBoundItem is NewBisPASvcRef.POLICY_ALL_OF_SUMM)
                    {
                        i = i + 1;
                        NewBisPASvcRef.POLICY_ALL_OF_SUMM obj = (NewBisPASvcRef.POLICY_ALL_OF_SUMM)dr.DataBoundItem;
                        dr.Cells["POCNo"].Value = i.ToString("n0");

                        if (obj.CUSTOMER_TYPE == "C" && obj.PRODUCT_TYPE == "POL" && obj.PLAN_TYPE == "L")
                        {
                            productType = "P";
                        }
                        else if (obj.CUSTOMER_TYPE == "C" && obj.PRODUCT_TYPE == "POL" && obj.PLAN_TYPE == "R")
                        {
                            productType = "P/R";
                        }
                        else if (obj.CUSTOMER_TYPE == "P" && obj.PRODUCT_TYPE == "POL" && obj.PLAN_TYPE == "R")
                        {
                            productType = "P/R (Parent)";
                        }
                        else if (obj.CUSTOMER_TYPE == "C" && obj.PRODUCT_TYPE == "APP" && obj.PLAN_TYPE == "L")
                        {
                            productType = "A";
                        }
                        else if (obj.CUSTOMER_TYPE == "C" && obj.PRODUCT_TYPE == "APP" && obj.PLAN_TYPE == "R")
                        {
                            productType = "A/R";
                        }
                        else if (obj.CUSTOMER_TYPE == "P" && obj.PRODUCT_TYPE == "APP" && obj.PLAN_TYPE == "R")
                        {
                            productType = "A/R (Parent)";
                        }
                        dr.Cells["POCType"].Value = productType;
                        dr.Cells["POCPolicyNo"].Value = obj.POLICY;
                        dr.Cells["POCPlan"].Value = obj.PL_BLOCK + obj.PL_CODE + "/" + obj.PL_CODE2 + " " + obj.PLAN_NAME;
                        dr.Cells["POCSumm"].Value = obj.SUMM == null ? "" : obj.SUMM.Value.ToString("n0");
                        dr.Cells["POCInfSumm"].Value = obj.INFSUMM == null ? "" : obj.INFSUMM.Value.ToString("n0");
                        dr.Cells["POCStatus"].Value = obj.STATUS_DESC;
                        dr.Cells["POCStandard"].Value = obj.STANDARD_DESC;
                        dr.Cells["POCIsuDate"].Value = obj.ISU_DT == null ? "" : Utility.dateTimeToString(obj.ISU_DT.Value, "dd/MM/yyyy", "BU");
                        dr.Cells["POCClaim"].Value = obj.CLAIM;
                        dr.Cells["POCExclude"].Value = obj.EXCLUDE;
                        dr.Cells["POCProductType"].Value = obj.PRODUCT_TYPE;
                        dr.Cells["POCPolicyID"].Value = obj.POLICY_ID == null ? "" : obj.POLICY_ID.Value.ToString();

                        if (obj.PRODUCT_TYPE == "POL")
                        {
                            if (obj.PLAN_TYPE == "L")
                            {
                                if ((obj.STATUS == "10") || (obj.STATUS == "11") || (obj.STATUS == "12") || (obj.STATUS == "13"))
                                {
                                    totalSummOfPO = totalSummOfPO + Convert.ToInt64(obj.INFSUMM);
                                }
                            }
                            if (obj.PLAN_TYPE == "R")
                            {
                                if ((obj.STATUS == "WC") || (obj.STATUS == "FA") || (obj.STATUS == "FC") || (obj.STATUS == "FR"))
                                {
                                    totalSummOfPO = totalSummOfPO + Convert.ToInt64(obj.INFSUMM);
                                }
                            }
                        }
                        else
                        {
                            totalSummOfPO = totalSummOfPO + Convert.ToInt64(obj.INFSUMM);
                        }

                    }
                }

                totalSummCustomer1 = totalSummCustomer1 + totalSummOfPO;
                txtTotalSummPO.Text = totalSummOfPO.ToString("n0");
            }

            var GetDataKB = from getData in customerDataColl
                            where getData.CHANNEL_TYPE == "KB"
                            orderby getData.POLICY ascending, getData.CUSTOMER_TYPE descending
                            select getData;
            if (GetDataKB != null && GetDataKB.Count() > 0)
            {
                HAVE_CUSPRODUCTOTH = HAVE_CUSPRODUCTOTH + 1;
                dataGridCustomerKB.DataSource = GetDataKB.ToArray();
                int i = 0;
                String productType = "";
                Int64 totalSummOfKB = 0;
                foreach (DataGridViewRow dr in dataGridCustomerKB.Rows)
                {
                    if (dr.DataBoundItem is NewBisPASvcRef.POLICY_ALL_OF_SUMM)
                    {
                        i = i + 1;
                        NewBisPASvcRef.POLICY_ALL_OF_SUMM obj = (NewBisPASvcRef.POLICY_ALL_OF_SUMM)dr.DataBoundItem;
                        dr.Cells["KBCNo"].Value = i.ToString("n0");

                        if (obj.CUSTOMER_TYPE == "C" && obj.PRODUCT_TYPE == "POL" && obj.PLAN_TYPE == "L")
                        {
                            productType = "P";
                        }
                        else if (obj.CUSTOMER_TYPE == "C" && obj.PRODUCT_TYPE == "POL" && obj.PLAN_TYPE == "R")
                        {
                            productType = "P/R";
                        }
                        else if (obj.CUSTOMER_TYPE == "P" && obj.PRODUCT_TYPE == "POL" && obj.PLAN_TYPE == "R")
                        {
                            productType = "P/R (Parent)";
                        }
                        else if (obj.CUSTOMER_TYPE == "C" && obj.PRODUCT_TYPE == "APP" && obj.PLAN_TYPE == "L")
                        {
                            productType = "A";
                        }
                        else if (obj.CUSTOMER_TYPE == "C" && obj.PRODUCT_TYPE == "APP" && obj.PLAN_TYPE == "R")
                        {
                            productType = "A/R";
                        }
                        else if (obj.CUSTOMER_TYPE == "P" && obj.PRODUCT_TYPE == "APP" && obj.PLAN_TYPE == "R")
                        {
                            productType = "A/R (Parent)";
                        }
                        dr.Cells["KBCType"].Value = productType;
                        dr.Cells["KBCPolicyNo"].Value = obj.POLICY;
                        dr.Cells["KBCPlan"].Value = obj.PL_BLOCK + obj.PL_CODE + "/" + obj.PL_CODE2 + " " + obj.PLAN_NAME;
                        dr.Cells["KBCSumm"].Value = obj.SUMM == null ? "" : obj.SUMM.Value.ToString("n0");
                        dr.Cells["KBCInfSumm"].Value = obj.INFSUMM == null ? "" : obj.INFSUMM.Value.ToString("n0");
                        dr.Cells["KBCStatus"].Value = obj.STATUS_DESC;
                        dr.Cells["KBCStandard"].Value = obj.STANDARD_DESC;
                        dr.Cells["KBCIsuDate"].Value = obj.ISU_DT == null ? "" : Utility.dateTimeToString(obj.ISU_DT.Value, "dd/MM/yyyy", "BU");
                        dr.Cells["KBCClaim"].Value = obj.CLAIM;
                        dr.Cells["KBCExclude"].Value = obj.EXCLUDE;
                        dr.Cells["KBCProductType"].Value = obj.PRODUCT_TYPE;
                        dr.Cells["KBCPolicyID"].Value = obj.POLICY_ID == null ? "" : obj.POLICY_ID.Value.ToString();

                        if (obj.PRODUCT_TYPE == "POL")
                        {
                            if (obj.PLAN_TYPE == "L")
                            {
                                if ((obj.STATUS == "10") || (obj.STATUS == "11") || (obj.STATUS == "12") || (obj.STATUS == "13"))
                                {
                                    totalSummOfKB = totalSummOfKB + Convert.ToInt64(obj.INFSUMM);
                                }
                            }
                            if (obj.PLAN_TYPE == "R")
                            {
                                if ((obj.STATUS == "WC") || (obj.STATUS == "FA") || (obj.STATUS == "FC") || (obj.STATUS == "FR"))
                                {
                                    totalSummOfKB = totalSummOfKB + Convert.ToInt64(obj.INFSUMM);
                                }
                            }
                        }
                        else
                        {
                            totalSummOfKB = totalSummOfKB + Convert.ToInt64(obj.INFSUMM);
                        }
                    }
                }

                totalSummCustomer1 = totalSummCustomer1 + totalSummOfKB;
                txtTotalSummKB.Text = totalSummOfKB.ToString("n0");
            }

            txtTotalAllSummOth.Text = totalSummCustomer1.ToString("n0");
        }

        private void SelectParentProductAll()
        {
            if (PARENT_DATA != null)
            {
                if (!(PARENT_DATA.IDCARD_NO == null && PARENT_DATA.PASSPORT == null && PARENT_DATA.NAME == null && PARENT_DATA.SURNAME == null && PARENT_DATA.SEX == null && PARENT_DATA.BIRTH_DT == null))
                {

                    NewBisPASvcRef.ProcessResult pr = new NewBisPASvcRef.ProcessResult();
                    NewBisPASvcRef.POLICY_ALL_OF_SUMM_COLLECTION customerDataColl = new NewBisPASvcRef.POLICY_ALL_OF_SUMM_COLLECTION();
                    String idcard = "";
                    if (!(String.IsNullOrEmpty(PARENT_DATA.IDCARD_NO)))
                    {
                        idcard = PARENT_DATA.IDCARD_NO;
                    }
                    if (!(String.IsNullOrEmpty(PARENT_DATA.PASSPORT)))
                    {
                        idcard = PARENT_DATA.PASSPORT;
                    }
                    String sex = PARENT_DATA.SEX == null ? "" : PARENT_DATA.SEX.Value.ToString();
                    using (NewBisPASvcRef.NewBisPASvcClient client = new NewBisPASvcRef.NewBisPASvcClient())
                    {
                        pr = client.GetPolicyAndAppAllChannel(out customerDataColl, idcard, PARENT_DATA.NAME, PARENT_DATA.SURNAME, sex, PARENT_DATA.BIRTH_DT, APP_NO);
                        if (pr.Successed == false)
                        {
                            throw new Exception(pr.ErrorMessage);
                        }
                    }

                    if (customerDataColl != null && customerDataColl.Count() > 0)
                    {
                        CHK_HAVE_DATA = CHK_HAVE_DATA + 1;
                        SelectProductOfParent1(customerDataColl);
                        SelectProductOfParent2(customerDataColl);
                    }

                }
            }
        }

        private void SelectProductOfParent1(NewBisPASvcRef.POLICY_ALL_OF_SUMM_COLLECTION customerDataColl)
        {
            Int64 totalSummCustomer1 = 0;
            var GetDataOD = from getData in customerDataColl
                            where getData.CHANNEL_TYPE == "OD"
                            && getData.BANC_TYPE == "N"
                            orderby getData.POLICY ascending, getData.CUSTOMER_TYPE descending
                            select getData;
            if (GetDataOD != null && GetDataOD.Count() > 0)
            {
                HAVE_PARENTPRODUCT = HAVE_PARENTPRODUCT + 1;

                dgvParentOrdinary.DataSource = GetDataOD.ToArray();
                int i = 0;
                String productType = "";
                Int64 totalSummOfOD = 0;
                foreach (DataGridViewRow dr in dgvParentOrdinary.Rows)
                {
                    if (dr.DataBoundItem is NewBisPASvcRef.POLICY_ALL_OF_SUMM)
                    {
                        i = i + 1;
                        NewBisPASvcRef.POLICY_ALL_OF_SUMM obj = (NewBisPASvcRef.POLICY_ALL_OF_SUMM)dr.DataBoundItem;
                        dr.Cells["PPONo"].Value = i.ToString("n0");

                        if (obj.CUSTOMER_TYPE == "C" && obj.PRODUCT_TYPE == "POL" && obj.PLAN_TYPE == "L")
                        {
                            productType = "P";
                        }
                        else if (obj.CUSTOMER_TYPE == "C" && obj.PRODUCT_TYPE == "POL" && obj.PLAN_TYPE == "R")
                        {
                            productType = "P/R";
                        }
                        else if (obj.CUSTOMER_TYPE == "P" && obj.PRODUCT_TYPE == "POL" && obj.PLAN_TYPE == "R")
                        {
                            productType = "P/R (Parent)";
                        }
                        else if (obj.CUSTOMER_TYPE == "C" && obj.PRODUCT_TYPE == "APP" && obj.PLAN_TYPE == "L")
                        {
                            productType = "A";
                        }
                        else if (obj.CUSTOMER_TYPE == "C" && obj.PRODUCT_TYPE == "APP" && obj.PLAN_TYPE == "R")
                        {
                            productType = "A/R";
                        }
                        else if (obj.CUSTOMER_TYPE == "P" && obj.PRODUCT_TYPE == "APP" && obj.PLAN_TYPE == "R")
                        {
                            productType = "A/R (Parent)";
                        }
                        dr.Cells["PPOType"].Value = productType;
                        dr.Cells["PPOPolicyNo"].Value = obj.POLICY;
                        dr.Cells["PPOPlan"].Value = obj.PL_BLOCK + obj.PL_CODE + "/" + obj.PL_CODE2 + " " + obj.PLAN_NAME;
                        dr.Cells["PPOSumm"].Value = obj.SUMM == null ? "" : obj.SUMM.Value.ToString("n0");
                        dr.Cells["PPOInfSumm"].Value = obj.INFSUMM == null ? "" : obj.INFSUMM.Value.ToString("n0");
                        dr.Cells["PPOStatus"].Value = obj.STATUS_DESC;
                        dr.Cells["PPOStandard"].Value = obj.STANDARD_DESC;
                        dr.Cells["PPOIsuDate"].Value = obj.ISU_DT == null ? "" : Utility.dateTimeToString(obj.ISU_DT.Value, "dd/MM/yyyy", "BU");
                        dr.Cells["PPOClaim"].Value = obj.CLAIM;
                        dr.Cells["PPOExclude"].Value = obj.EXCLUDE;

                        if (obj.PRODUCT_TYPE == "POL")
                        {
                            if (obj.PLAN_TYPE == "L")
                            {
                                if ((obj.STATUS == "10") || (obj.STATUS == "11") || (obj.STATUS == "12") || (obj.STATUS == "13"))
                                {
                                    totalSummOfOD = totalSummOfOD + Convert.ToInt64(obj.INFSUMM);
                                }
                            }
                            if (obj.PLAN_TYPE == "R")
                            {
                                if ((obj.STATUS == "WC") || (obj.STATUS == "FA") || (obj.STATUS == "FC") || (obj.STATUS == "FR"))
                                {
                                    totalSummOfOD = totalSummOfOD + Convert.ToInt64(obj.INFSUMM);
                                }
                            }
                        }
                        else
                        {
                            totalSummOfOD = totalSummOfOD + Convert.ToInt64(obj.INFSUMM);
                        }
                    }
                }
                totalSummCustomer1 = totalSummCustomer1 + totalSummOfOD;

                txtParentOrdinarySumSumm.Text = totalSummOfOD.ToString("n0");

            }

            var GetDataBnc = from getData in customerDataColl
                             where getData.CHANNEL_TYPE == "OD"
                             && getData.BANC_TYPE == "Y"
                             orderby getData.POLICY ascending, getData.CUSTOMER_TYPE descending
                             select getData;
            if (GetDataBnc != null && GetDataBnc.Count() > 0)
            {
                HAVE_PARENTPRODUCT = HAVE_PARENTPRODUCT + 1;

                dgvParentBancassurance.DataSource = GetDataBnc.ToArray();
                int i = 0;
                String productType = "";
                Int64 totalSummOfBnc = 0;
                foreach (DataGridViewRow dr in dgvParentBancassurance.Rows)
                {
                    if (dr.DataBoundItem is NewBisPASvcRef.POLICY_ALL_OF_SUMM)
                    {
                        i = i + 1;
                        NewBisPASvcRef.POLICY_ALL_OF_SUMM obj = (NewBisPASvcRef.POLICY_ALL_OF_SUMM)dr.DataBoundItem;
                        dr.Cells["PPBNo"].Value = i.ToString("n0");

                        if (obj.CUSTOMER_TYPE == "C" && obj.PRODUCT_TYPE == "POL" && obj.PLAN_TYPE == "L")
                        {
                            productType = "P";
                        }
                        else if (obj.CUSTOMER_TYPE == "C" && obj.PRODUCT_TYPE == "POL" && obj.PLAN_TYPE == "R")
                        {
                            productType = "P/R";
                        }
                        else if (obj.CUSTOMER_TYPE == "P" && obj.PRODUCT_TYPE == "POL" && obj.PLAN_TYPE == "R")
                        {
                            productType = "P/R (Parent)";
                        }
                        else if (obj.CUSTOMER_TYPE == "C" && obj.PRODUCT_TYPE == "APP" && obj.PLAN_TYPE == "L")
                        {
                            productType = "A";
                        }
                        else if (obj.CUSTOMER_TYPE == "C" && obj.PRODUCT_TYPE == "APP" && obj.PLAN_TYPE == "R")
                        {
                            productType = "A/R";
                        }
                        else if (obj.CUSTOMER_TYPE == "P" && obj.PRODUCT_TYPE == "APP" && obj.PLAN_TYPE == "R")
                        {
                            productType = "A/R (Parent)";
                        }
                        dr.Cells["PPBType"].Value = productType;
                        dr.Cells["PPBPolicyNo"].Value = obj.POLICY;
                        dr.Cells["PPBPlan"].Value = obj.PL_BLOCK + obj.PL_CODE + "/" + obj.PL_CODE2 + " " + obj.PLAN_NAME;
                        dr.Cells["PPBSumm"].Value = obj.SUMM == null ? "" : obj.SUMM.Value.ToString("n0");
                        dr.Cells["PPBInfSumm"].Value = obj.INFSUMM == null ? "" : obj.INFSUMM.Value.ToString("n0");
                        dr.Cells["PPBStatus"].Value = obj.STATUS_DESC;
                        dr.Cells["PPBStandard"].Value = obj.STANDARD_DESC;
                        dr.Cells["PPBIsuDate"].Value = obj.ISU_DT == null ? "" : Utility.dateTimeToString(obj.ISU_DT.Value, "dd/MM/yyyy", "BU");
                        dr.Cells["PPBClaim"].Value = obj.CLAIM;
                        dr.Cells["PPBExclude"].Value = obj.EXCLUDE;

                        if (obj.PRODUCT_TYPE == "POL")
                        {
                            if (obj.PLAN_TYPE == "L")
                            {
                                if ((obj.STATUS == "10") || (obj.STATUS == "11") || (obj.STATUS == "12") || (obj.STATUS == "13"))
                                {
                                    totalSummOfBnc = totalSummOfBnc + Convert.ToInt64(obj.INFSUMM);
                                }
                            }
                            if (obj.PLAN_TYPE == "R")
                            {
                                if ((obj.STATUS == "WC") || (obj.STATUS == "FA") || (obj.STATUS == "FC") || (obj.STATUS == "FR"))
                                {
                                    totalSummOfBnc = totalSummOfBnc + Convert.ToInt64(obj.INFSUMM);
                                }
                            }
                        }
                        else
                        {
                            totalSummOfBnc = totalSummOfBnc + Convert.ToInt64(obj.INFSUMM);
                        }

                    }
                }

                totalSummCustomer1 = totalSummCustomer1 + totalSummOfBnc;
                txtParentBancassuranceSumSumm.Text = totalSummOfBnc.ToString("n0");
            }

            var GetDataGM = from getData in customerDataColl
                            where getData.CHANNEL_TYPE == "GM"
                            orderby getData.POLICY ascending, getData.CUSTOMER_TYPE descending
                            select getData;

            if (GetDataGM != null && GetDataGM.Count() > 0)
            {
                HAVE_PARENTPRODUCT = HAVE_PARENTPRODUCT + 1;

                dgvParentGroupMortgage.DataSource = GetDataGM.ToArray();
                int i = 0;
                String productType = "";
                Int64 totalSummOfGM = 0;
                foreach (DataGridViewRow dr in dgvParentGroupMortgage.Rows)
                {
                    if (dr.DataBoundItem is NewBisPASvcRef.POLICY_ALL_OF_SUMM)
                    {
                        i = i + 1;
                        NewBisPASvcRef.POLICY_ALL_OF_SUMM obj = (NewBisPASvcRef.POLICY_ALL_OF_SUMM)dr.DataBoundItem;
                        dr.Cells["GMPNo"].Value = i.ToString("n0");

                        if (obj.CUSTOMER_TYPE == "C" && obj.PRODUCT_TYPE == "POL" && obj.PLAN_TYPE == "L")
                        {
                            productType = "P";
                        }
                        else if (obj.CUSTOMER_TYPE == "C" && obj.PRODUCT_TYPE == "POL" && obj.PLAN_TYPE == "R")
                        {
                            productType = "P/R";
                        }
                        else if (obj.CUSTOMER_TYPE == "P" && obj.PRODUCT_TYPE == "POL" && obj.PLAN_TYPE == "R")
                        {
                            productType = "P/R (Parent)";
                        }
                        else if (obj.CUSTOMER_TYPE == "C" && obj.PRODUCT_TYPE == "APP" && obj.PLAN_TYPE == "L")
                        {
                            productType = "A";
                        }
                        else if (obj.CUSTOMER_TYPE == "C" && obj.PRODUCT_TYPE == "APP" && obj.PLAN_TYPE == "R")
                        {
                            productType = "A/R";
                        }
                        else if (obj.CUSTOMER_TYPE == "P" && obj.PRODUCT_TYPE == "APP" && obj.PLAN_TYPE == "R")
                        {
                            productType = "A/R (Parent)";
                        }
                        dr.Cells["GMPType"].Value = productType;
                        dr.Cells["GMPPolicyNo"].Value = obj.POLICY;
                        dr.Cells["GMPPlan"].Value = obj.PL_BLOCK + obj.PL_CODE + "/" + obj.PL_CODE2 + " " + obj.PLAN_NAME;
                        dr.Cells["GMPSumm"].Value = obj.SUMM == null ? "" : obj.SUMM.Value.ToString("n0");
                        dr.Cells["GMPInfSumm"].Value = obj.INFSUMM == null ? "" : obj.INFSUMM.Value.ToString("n0");
                        dr.Cells["GMPStatus"].Value = obj.STATUS_DESC;
                        dr.Cells["GMPStandard"].Value = obj.STANDARD_DESC;
                        dr.Cells["GMPIsuDate"].Value = obj.ISU_DT == null ? "" : Utility.dateTimeToString(obj.ISU_DT.Value, "dd/MM/yyyy", "BU");
                        dr.Cells["GMPClaim"].Value = obj.CLAIM;
                        dr.Cells["GMPExclude"].Value = obj.EXCLUDE;

                        if (obj.PRODUCT_TYPE == "POL")
                        {
                            if (obj.PLAN_TYPE == "L")
                            {
                                if ((obj.STATUS == "10") || (obj.STATUS == "11") || (obj.STATUS == "12") || (obj.STATUS == "13"))
                                {
                                    totalSummOfGM = totalSummOfGM + Convert.ToInt64(obj.INFSUMM);
                                }
                            }
                            if (obj.PLAN_TYPE == "R")
                            {
                                if ((obj.STATUS == "WC") || (obj.STATUS == "FA") || (obj.STATUS == "FC") || (obj.STATUS == "FR"))
                                {
                                    totalSummOfGM = totalSummOfGM + Convert.ToInt64(obj.INFSUMM);
                                }
                            }
                        }
                        else
                        {
                            totalSummOfGM = totalSummOfGM + Convert.ToInt64(obj.INFSUMM);
                        }

                    }
                }

                totalSummCustomer1 = totalSummCustomer1 + totalSummOfGM;
                txtParentGroupMortgageSumSumm.Text = totalSummOfGM.ToString("n0");

            }

            var GetDataHL = from getData in customerDataColl
                            where getData.CHANNEL_TYPE == "HL"
                            orderby getData.POLICY ascending, getData.CUSTOMER_TYPE descending
                            select getData;

            if (GetDataHL != null && GetDataHL.Count() > 0)
            {
                HAVE_PARENTPRODUCT = HAVE_PARENTPRODUCT + 1;

                dgvParentHL.DataSource = GetDataHL.ToArray();
                int i = 0;
                String productType = "";
                Int64 totalSummOfHL = 0;
                foreach (DataGridViewRow dr in dgvParentHL.Rows)
                {
                    if (dr.DataBoundItem is NewBisPASvcRef.POLICY_ALL_OF_SUMM)
                    {
                        i = i + 1;
                        NewBisPASvcRef.POLICY_ALL_OF_SUMM obj = (NewBisPASvcRef.POLICY_ALL_OF_SUMM)dr.DataBoundItem;
                        dr.Cells["HLPNo"].Value = i.ToString("n0");

                        if (obj.CUSTOMER_TYPE == "C" && obj.PRODUCT_TYPE == "POL" && obj.PLAN_TYPE == "L")
                        {
                            productType = "P";
                        }
                        else if (obj.CUSTOMER_TYPE == "C" && obj.PRODUCT_TYPE == "POL" && obj.PLAN_TYPE == "R")
                        {
                            productType = "P/R";
                        }
                        else if (obj.CUSTOMER_TYPE == "P" && obj.PRODUCT_TYPE == "POL" && obj.PLAN_TYPE == "R")
                        {
                            productType = "P/R (Parent)";
                        }
                        else if (obj.CUSTOMER_TYPE == "C" && obj.PRODUCT_TYPE == "APP" && obj.PLAN_TYPE == "L")
                        {
                            productType = "A";
                        }
                        else if (obj.CUSTOMER_TYPE == "C" && obj.PRODUCT_TYPE == "APP" && obj.PLAN_TYPE == "R")
                        {
                            productType = "A/R";
                        }
                        else if (obj.CUSTOMER_TYPE == "P" && obj.PRODUCT_TYPE == "APP" && obj.PLAN_TYPE == "R")
                        {
                            productType = "A/R (Parent)";
                        }
                        dr.Cells["HLPType"].Value = productType;
                        dr.Cells["HLPPolicyNo"].Value = obj.POLICY;
                        dr.Cells["HLPPlan"].Value = obj.PL_BLOCK + obj.PL_CODE + "/" + obj.PL_CODE2 + " " + obj.PLAN_NAME;
                        dr.Cells["HLPSumm"].Value = obj.SUMM == null ? "" : obj.SUMM.Value.ToString("n0");
                        dr.Cells["HLPInfSumm"].Value = obj.INFSUMM == null ? "" : obj.INFSUMM.Value.ToString("n0");
                        dr.Cells["HLPStatus"].Value = obj.STATUS_DESC;
                        dr.Cells["HLPStandard"].Value = obj.STANDARD_DESC;
                        dr.Cells["HLPIsuDate"].Value = obj.ISU_DT == null ? "" : Utility.dateTimeToString(obj.ISU_DT.Value, "dd/MM/yyyy", "BU");
                        dr.Cells["HLPClaim"].Value = obj.CLAIM;
                        dr.Cells["HLPExclude"].Value = obj.EXCLUDE;

                        if (obj.PRODUCT_TYPE == "POL")
                        {
                            if (obj.PLAN_TYPE == "L")
                            {
                                if ((obj.STATUS == "10") || (obj.STATUS == "11") || (obj.STATUS == "12") || (obj.STATUS == "13"))
                                {
                                    totalSummOfHL = totalSummOfHL + Convert.ToInt64(obj.INFSUMM);
                                }
                            }
                            if (obj.PLAN_TYPE == "R")
                            {
                                if ((obj.STATUS == "WC") || (obj.STATUS == "FA") || (obj.STATUS == "FC") || (obj.STATUS == "FR"))
                                {
                                    totalSummOfHL = totalSummOfHL + Convert.ToInt64(obj.INFSUMM);
                                }
                            }
                        }
                        else
                        {
                            totalSummOfHL = totalSummOfHL + Convert.ToInt64(obj.INFSUMM);
                        }

                    }
                }

                totalSummCustomer1 = totalSummCustomer1 + totalSummOfHL;
                txtParentHLSumSumm.Text = totalSummOfHL.ToString("n0");

            }

            txtParentTotalSumm.Text = totalSummCustomer1.ToString("n0");
        }
        private void SelectProductOfParent2(NewBisPASvcRef.POLICY_ALL_OF_SUMM_COLLECTION customerDataColl)
        {
            Int64 totalSummCustomer1 = 0;
            var GetDataPD = from getData in customerDataColl
                            where getData.CHANNEL_TYPE == "PD" || getData.CHANNEL_TYPE == "PF"
                            orderby getData.POLICY ascending, getData.CUSTOMER_TYPE descending
                            select getData;
            if (GetDataPD != null && GetDataPD.Count() > 0)
            {

                HAVE_PARENTPRODUCTOTH = HAVE_PARENTPRODUCTOTH + 1;
                dgvParentPD.DataSource = GetDataPD.ToArray();
                int i = 0;
                String productType = "";
                Int64 totalSummOfPD = 0;
                foreach (DataGridViewRow dr in dgvParentPD.Rows)
                {
                    if (dr.DataBoundItem is NewBisPASvcRef.POLICY_ALL_OF_SUMM)
                    {
                        i = i + 1;
                        NewBisPASvcRef.POLICY_ALL_OF_SUMM obj = (NewBisPASvcRef.POLICY_ALL_OF_SUMM)dr.DataBoundItem;
                        dr.Cells["PDPNo"].Value = i.ToString("n0");

                        if (obj.CUSTOMER_TYPE == "C" && obj.PRODUCT_TYPE == "POL" && obj.PLAN_TYPE == "L")
                        {
                            productType = "P";
                        }
                        else if (obj.CUSTOMER_TYPE == "C" && obj.PRODUCT_TYPE == "POL" && obj.PLAN_TYPE == "R")
                        {
                            productType = "P/R";
                        }
                        else if (obj.CUSTOMER_TYPE == "P" && obj.PRODUCT_TYPE == "POL" && obj.PLAN_TYPE == "R")
                        {
                            productType = "P/R (Parent)";
                        }
                        else if (obj.CUSTOMER_TYPE == "C" && obj.PRODUCT_TYPE == "APP" && obj.PLAN_TYPE == "L")
                        {
                            productType = "A";
                        }
                        else if (obj.CUSTOMER_TYPE == "C" && obj.PRODUCT_TYPE == "APP" && obj.PLAN_TYPE == "R")
                        {
                            productType = "A/R";
                        }
                        else if (obj.CUSTOMER_TYPE == "P" && obj.PRODUCT_TYPE == "APP" && obj.PLAN_TYPE == "R")
                        {
                            productType = "A/R (Parent)";
                        }
                        dr.Cells["PDPType"].Value = productType;
                        dr.Cells["PDPPolicyNo"].Value = obj.POLICY;
                        dr.Cells["PDPPlan"].Value = obj.PL_BLOCK + obj.PL_CODE + "/" + obj.PL_CODE2 + " " + obj.PLAN_NAME;
                        dr.Cells["PDPSumm"].Value = obj.SUMM == null ? "" : obj.SUMM.Value.ToString("n0");
                        dr.Cells["PDPInfSumm"].Value = obj.INFSUMM == null ? "" : obj.INFSUMM.Value.ToString("n0");
                        dr.Cells["PDPStatus"].Value = obj.STATUS_DESC;
                        dr.Cells["PDPStandard"].Value = obj.STANDARD_DESC;
                        dr.Cells["PDPIsuDate"].Value = obj.ISU_DT == null ? "" : Utility.dateTimeToString(obj.ISU_DT.Value, "dd/MM/yyyy", "BU");
                        dr.Cells["PDPClaim"].Value = obj.CLAIM;
                        dr.Cells["PDPExclude"].Value = obj.EXCLUDE;

                        if (obj.PRODUCT_TYPE == "POL")
                        {
                            if (obj.PLAN_TYPE == "L")
                            {
                                if ((obj.STATUS == "10") || (obj.STATUS == "11") || (obj.STATUS == "12") || (obj.STATUS == "13"))
                                {
                                    totalSummOfPD = totalSummOfPD + Convert.ToInt64(obj.INFSUMM);
                                }
                            }
                            if (obj.PLAN_TYPE == "R")
                            {
                                if ((obj.STATUS == "WC") || (obj.STATUS == "FA") || (obj.STATUS == "FC") || (obj.STATUS == "FR"))
                                {
                                    totalSummOfPD = totalSummOfPD + Convert.ToInt64(obj.INFSUMM);
                                }
                            }
                        }
                        else
                        {
                            totalSummOfPD = totalSummOfPD + Convert.ToInt64(obj.INFSUMM);
                        }
                    }
                }

                totalSummCustomer1 = totalSummCustomer1 + totalSummOfPD;
                txtTotalSummParentPD.Text = totalSummOfPD.ToString("n0");
            }

            var GetDataPO = from getData in customerDataColl
                            where getData.CHANNEL_TYPE == "PO" || getData.CHANNEL_TYPE == "PN"
                            orderby getData.POLICY ascending, getData.CUSTOMER_TYPE descending
                            select getData;
            if (GetDataPO != null && GetDataPO.Count() > 0)
            {
                HAVE_PARENTPRODUCTOTH = HAVE_PARENTPRODUCTOTH + 1;
                dgvParentPO.DataSource = GetDataPO.ToArray();
                int i = 0;
                String productType = "";
                Int64 totalSummOfPO = 0;
                foreach (DataGridViewRow dr in dgvParentPO.Rows)
                {
                    if (dr.DataBoundItem is NewBisPASvcRef.POLICY_ALL_OF_SUMM)
                    {
                        i = i + 1;
                        NewBisPASvcRef.POLICY_ALL_OF_SUMM obj = (NewBisPASvcRef.POLICY_ALL_OF_SUMM)dr.DataBoundItem;
                        dr.Cells["POPNo"].Value = i.ToString("n0");

                        if (obj.CUSTOMER_TYPE == "C" && obj.PRODUCT_TYPE == "POL" && obj.PLAN_TYPE == "L")
                        {
                            productType = "P";
                        }
                        else if (obj.CUSTOMER_TYPE == "C" && obj.PRODUCT_TYPE == "POL" && obj.PLAN_TYPE == "R")
                        {
                            productType = "P/R";
                        }
                        else if (obj.CUSTOMER_TYPE == "P" && obj.PRODUCT_TYPE == "POL" && obj.PLAN_TYPE == "R")
                        {
                            productType = "P/R (Parent)";
                        }
                        else if (obj.CUSTOMER_TYPE == "C" && obj.PRODUCT_TYPE == "APP" && obj.PLAN_TYPE == "L")
                        {
                            productType = "A";
                        }
                        else if (obj.CUSTOMER_TYPE == "C" && obj.PRODUCT_TYPE == "APP" && obj.PLAN_TYPE == "R")
                        {
                            productType = "A/R";
                        }
                        else if (obj.CUSTOMER_TYPE == "P" && obj.PRODUCT_TYPE == "APP" && obj.PLAN_TYPE == "R")
                        {
                            productType = "A/R (Parent)";
                        }
                        dr.Cells["POPType"].Value = productType;
                        dr.Cells["POPPolicyNo"].Value = obj.POLICY;
                        dr.Cells["POPPlan"].Value = obj.PL_BLOCK + obj.PL_CODE + "/" + obj.PL_CODE2 + " " + obj.PLAN_NAME;
                        dr.Cells["POPSumm"].Value = obj.SUMM == null ? "" : obj.SUMM.Value.ToString("n0");
                        dr.Cells["POPInfSumm"].Value = obj.INFSUMM == null ? "" : obj.INFSUMM.Value.ToString("n0");
                        dr.Cells["POPStatus"].Value = obj.STATUS_DESC;
                        dr.Cells["POPStandard"].Value = obj.STANDARD_DESC;
                        dr.Cells["POPIsuDate"].Value = obj.ISU_DT == null ? "" : Utility.dateTimeToString(obj.ISU_DT.Value, "dd/MM/yyyy", "BU");
                        dr.Cells["POPClaim"].Value = obj.CLAIM;
                        dr.Cells["POPExclude"].Value = obj.EXCLUDE;

                        if (obj.PRODUCT_TYPE == "POL")
                        {
                            if (obj.PLAN_TYPE == "L")
                            {
                                if ((obj.STATUS == "10") || (obj.STATUS == "11") || (obj.STATUS == "12") || (obj.STATUS == "13"))
                                {
                                    totalSummOfPO = totalSummOfPO + Convert.ToInt64(obj.INFSUMM);
                                }
                            }
                            if (obj.PLAN_TYPE == "R")
                            {
                                if ((obj.STATUS == "WC") || (obj.STATUS == "FA") || (obj.STATUS == "FC") || (obj.STATUS == "FR"))
                                {
                                    totalSummOfPO = totalSummOfPO + Convert.ToInt64(obj.INFSUMM);
                                }
                            }
                        }
                        else
                        {
                            totalSummOfPO = totalSummOfPO + Convert.ToInt64(obj.INFSUMM);
                        }
                    }
                }

                totalSummCustomer1 = totalSummCustomer1 + totalSummOfPO;
                txtTotalSummParentPO.Text = totalSummOfPO.ToString("n0");
            }

            var GetDataKB = from getData in customerDataColl
                            where getData.CHANNEL_TYPE == "KB"
                            orderby getData.POLICY ascending, getData.CUSTOMER_TYPE descending
                            select getData;
            if (GetDataKB != null && GetDataKB.Count() > 0)
            {
                HAVE_PARENTPRODUCTOTH = HAVE_PARENTPRODUCTOTH + 1;
                dgvParentKB.DataSource = GetDataKB.ToArray();
                int i = 0;
                String productType = "";
                Int64 totalSummOfKB = 0;
                foreach (DataGridViewRow dr in dgvParentKB.Rows)
                {
                    if (dr.DataBoundItem is NewBisPASvcRef.POLICY_ALL_OF_SUMM)
                    {
                        i = i + 1;
                        NewBisPASvcRef.POLICY_ALL_OF_SUMM obj = (NewBisPASvcRef.POLICY_ALL_OF_SUMM)dr.DataBoundItem;
                        dr.Cells["KBPNo"].Value = i.ToString("n0");

                        if (obj.CUSTOMER_TYPE == "C" && obj.PRODUCT_TYPE == "POL" && obj.PLAN_TYPE == "L")
                        {
                            productType = "P";
                        }
                        else if (obj.CUSTOMER_TYPE == "C" && obj.PRODUCT_TYPE == "POL" && obj.PLAN_TYPE == "R")
                        {
                            productType = "P/R";
                        }
                        else if (obj.CUSTOMER_TYPE == "P" && obj.PRODUCT_TYPE == "POL" && obj.PLAN_TYPE == "R")
                        {
                            productType = "P/R (Parent)";
                        }
                        else if (obj.CUSTOMER_TYPE == "C" && obj.PRODUCT_TYPE == "APP" && obj.PLAN_TYPE == "L")
                        {
                            productType = "A";
                        }
                        else if (obj.CUSTOMER_TYPE == "C" && obj.PRODUCT_TYPE == "APP" && obj.PLAN_TYPE == "R")
                        {
                            productType = "A/R";
                        }
                        else if (obj.CUSTOMER_TYPE == "P" && obj.PRODUCT_TYPE == "APP" && obj.PLAN_TYPE == "R")
                        {
                            productType = "A/R (Parent)";
                        }
                        dr.Cells["KBPType"].Value = productType;
                        dr.Cells["KBPPolicyNo"].Value = obj.POLICY;
                        dr.Cells["KBPPlan"].Value = obj.PL_BLOCK + obj.PL_CODE + "/" + obj.PL_CODE2 + " " + obj.PLAN_NAME;
                        dr.Cells["KBPSumm"].Value = obj.SUMM == null ? "" : obj.SUMM.Value.ToString("n0");
                        dr.Cells["KBPInfSumm"].Value = obj.INFSUMM == null ? "" : obj.INFSUMM.Value.ToString("n0");
                        dr.Cells["KBPStatus"].Value = obj.STATUS_DESC;
                        dr.Cells["KBPStandard"].Value = obj.STANDARD_DESC;
                        dr.Cells["KBPIsuDate"].Value = obj.ISU_DT == null ? "" : Utility.dateTimeToString(obj.ISU_DT.Value, "dd/MM/yyyy", "BU");
                        dr.Cells["KBPClaim"].Value = obj.CLAIM;
                        dr.Cells["KBPExclude"].Value = obj.EXCLUDE;

                        if (obj.PRODUCT_TYPE == "POL")
                        {
                            if (obj.PLAN_TYPE == "L")
                            {
                                if ((obj.STATUS == "10") || (obj.STATUS == "11") || (obj.STATUS == "12") || (obj.STATUS == "13"))
                                {
                                    totalSummOfKB = totalSummOfKB + Convert.ToInt64(obj.INFSUMM);
                                }
                            }
                            if (obj.PLAN_TYPE == "R")
                            {
                                if ((obj.STATUS == "WC") || (obj.STATUS == "FA") || (obj.STATUS == "FC") || (obj.STATUS == "FR"))
                                {
                                    totalSummOfKB = totalSummOfKB + Convert.ToInt64(obj.INFSUMM);
                                }
                            }
                        }
                        else
                        {
                            totalSummOfKB = totalSummOfKB + Convert.ToInt64(obj.INFSUMM);
                        }

                    }
                }

                totalSummCustomer1 = totalSummCustomer1 + totalSummOfKB;
                txtTotalSummParentKB.Text = totalSummOfKB.ToString("n0");
            }

            txtTotalSummParentOth.Text = totalSummCustomer1.ToString("n0");
        }

        private void SetMsgException(Exception ex)
        {
            MessageBox.Show(ex.Message);
            return;
        }

        private void dgvCustomerMIB_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1 && dgvCustomerMIB.Rows[e.RowIndex].Cells["CMCode"].ColumnIndex == e.ColumnIndex)
            {
                String customerCode = dgvCustomerMIB.Rows[e.RowIndex].Cells["CMCode"].Value == null ? "" : dgvCustomerMIB.Rows[e.RowIndex].Cells["CMCode"].Value.ToString();
                if (customerCode != "")
                {
                    String companyCode = dgvCustomerMIB.Rows[e.RowIndex].Cells["CMCOMPANY_CODE"].Value == null ? "" : dgvCustomerMIB.Rows[e.RowIndex].Cells["CMCOMPANY_CODE"].Value.ToString();
                    String cardRun = dgvCustomerMIB.Rows[e.RowIndex].Cells["CMCARD_RUN"].Value == null ? "" : dgvCustomerMIB.Rows[e.RowIndex].Cells["CMCARD_RUN"].Value.ToString();
                    String cardSeq = dgvCustomerMIB.Rows[e.RowIndex].Cells["CMCARD_SEQ"].Value == null ? "" : dgvCustomerMIB.Rows[e.RowIndex].Cells["CMCARD_SEQ"].Value.ToString();
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
                        RegistryKey localMachineReg = Registry.LocalMachine;
                        RegistryKey softwareReg = localMachineReg.OpenSubKey("Software", true);
                        RegistryKey bangkokLifeAssuranceReg = softwareReg.OpenSubKey("Bangkok Life Assurance", true);
                        RegistryKey blaWinAppMenuReg = bangkokLifeAssuranceReg.OpenSubKey("BlaWinAppMenu", true);

                        blaWinAppMenuReg.SetValue("UserID", USERID, RegistryValueKind.String);
                        blaWinAppMenuReg.SetValue("FormAction", "FORMSEARCH", RegistryValueKind.String);

                        blaWinAppMenuReg.SetValue("CompanyCode", companyCode, RegistryValueKind.String);
                        blaWinAppMenuReg.SetValue("CardRun", cardRun, RegistryValueKind.String);
                        blaWinAppMenuReg.SetValue("CardSeq", cardSeq, RegistryValueKind.String);

                        blaWinAppMenuReg.Close();
                        bangkokLifeAssuranceReg.Close();
                        softwareReg.Close();
                        localMachineReg.Close();
                        Process.Start(files[0].FullName);
                    }
                }
            }
        }

        private void FormCustomerData_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                //if (CUSTOMER_TYPE == "C")
                //{
                //    W_MIB_COLL = new NewBISSvcRef.W_MIB_Collection();
                //    dgvCustomerMIB.EndEdit();
                //    String chkTrue = "";
                //    foreach (DataGridViewRow dr in dgvCustomerMIB.Rows)
                //    {
                //        if (dr.IsNewRow == false)
                //        {
                //            if (dr.DataBoundItem is NewBISSvcRef.P_YELLOW_CARD)
                //            {
                //                NewBISSvcRef.P_YELLOW_CARD obj = (NewBISSvcRef.P_YELLOW_CARD)dr.DataBoundItem;
                //                chkTrue = dr.Cells["CMSel"].Value == null ? "" : dr.Cells["CMSel"].Value.ToString();
                //                if (chkTrue == "Y")
                //                {
                //                    NewBISSvcRef.W_MIB wMIBObj = new NewBISSvcRef.W_MIB();
                //                    wMIBObj.MIB_ID = null;
                //                    wMIBObj.SUBUNDERWRITE_ID = null;
                //                    wMIBObj.COMPANY_CODE = obj.COMPANY_CODE;
                //                    wMIBObj.CARD_RUN = obj.CARD_RUN;
                //                    wMIBObj.CARD_SEQ = obj.CARD_SEQ;
                //                    wMIBObj.CHECK_TRUE = chkTrue == "" ? 'N' : Convert.ToChar(chkTrue);
                //                    wMIBObj.REFERENCE = obj.REFERENCE;
                //                    wMIBObj.RESULT = obj.RESULT;
                //                    wMIBObj.RESULT_CAUSE = obj.RESULT_CAUSE;
                //                    wMIBObj.UND_ID = USERID;
                //                    wMIBObj.FSYSTEM = DateTime.Now;
                //                    wMIBObj.UPD_ID = USERID;
                //                    W_MIB_COLL.Add(wMIBObj);
                //                }


                //            }

                //        }
                //    }
                //}
                //else if (CUSTOMER_TYPE == "P")
                //{
                //    W_MIB_COLL = new NewBISSvcRef.W_MIB_Collection();
                //    dgvParentMIB.EndEdit();
                //    String chkTrue = "";
                //    foreach (DataGridViewRow dr in dgvParentMIB.Rows)
                //    {
                //        if (dr.IsNewRow == false)
                //        {
                //            if (dr.DataBoundItem is NewBISSvcRef.P_YELLOW_CARD)
                //            {
                //                NewBISSvcRef.P_YELLOW_CARD obj = (NewBISSvcRef.P_YELLOW_CARD)dr.DataBoundItem;
                //                chkTrue = dr.Cells["PMSel"].Value == null ? "" : dr.Cells["PMSel"].Value.ToString();
                //                if (chkTrue == "Y")
                //                {
                //                    NewBISSvcRef.W_MIB wMIBObj = new NewBISSvcRef.W_MIB();
                //                    wMIBObj.MIB_ID = null;
                //                    wMIBObj.SUBUNDERWRITE_ID = null;
                //                    wMIBObj.COMPANY_CODE = obj.COMPANY_CODE;
                //                    wMIBObj.CARD_RUN = obj.CARD_RUN;
                //                    wMIBObj.CARD_SEQ = obj.CARD_SEQ;
                //                    wMIBObj.CHECK_TRUE = chkTrue == "" ? 'N' : Convert.ToChar(chkTrue);
                //                    wMIBObj.REFERENCE = obj.REFERENCE;
                //                    wMIBObj.RESULT = obj.RESULT;
                //                    wMIBObj.RESULT_CAUSE = obj.RESULT_CAUSE;
                //                    wMIBObj.UND_ID = USERID;
                //                    wMIBObj.FSYSTEM = DateTime.Now;
                //                    wMIBObj.UPD_ID = USERID;
                //                    W_MIB_COLL.Add(wMIBObj);
                //                }

                //            }

                //        }
                //    }
                //}
            }
            catch (Exception ex)
            {
                SetMsgException(ex);
            }
        }

        private void dgvCustomerOrdinary_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {



            try
            {
                String policyID = "";
                String policyNo = "";
                String productType = "";
                String claim = "";


                policyID = dgvCustomerOrdinary.Rows[e.RowIndex].Cells["CPOPolicyID"].Value == null ? "" : dgvCustomerOrdinary.Rows[e.RowIndex].Cells["CPOPolicyID"].Value.ToString();
                policyNo = dgvCustomerOrdinary.Rows[e.RowIndex].Cells["CPOPolicyNo"].Value == null ? "" : dgvCustomerOrdinary.Rows[e.RowIndex].Cells["CPOPolicyNo"].Value.ToString();
        //   productType = dgvCustomerOrdinary.Rows[e.RowIndex].Cells["CPOProductType"].Value == null ? "" : dgvCustomerOrdinary.Rows[e.RowIndex].Cells["CPOProductType"].Value.ToString();
                claim = dgvCustomerOrdinary.Rows[e.RowIndex].Cells["CPOClaim"].Value == null ? "" : dgvCustomerOrdinary.Rows[e.RowIndex].Cells["CPOClaim"].Value.ToString();


                if ((e.RowIndex > -1) && (dgvCustomerOrdinary.Rows[e.RowIndex].IsNewRow == false) && (e.ColumnIndex == dgvCustomerOrdinary.Rows[e.RowIndex].Cells["CPOPolicyNo"].ColumnIndex) && (policyNo != ""))
                {
                    if (productType == "POL")
                    {
                        System.Diagnostics.Process.Start("http://mvcsvc.bla.co.th/CallCenter/?POLICY_ID=" + policyID + "&TYPE=P&USERID=" + USERID + " ");
                    }
                    else if (productType == "APP")
                    {
                        System.Diagnostics.Process.Start("http://mvcsvc.bla.co.th/CallCenter/?POLICY_ID=" + policyNo + "&TYPE=A&USERID=" + USERID + " ");
                    }
                }
                else if ((e.RowIndex > -1) && (dgvCustomerOrdinary.Rows[e.RowIndex].IsNewRow == false) && (e.ColumnIndex == dgvCustomerOrdinary.Rows[e.RowIndex].Cells["CPOClaim"].ColumnIndex) && (claim == "Y"))
                {
                    if (productType == "POL")
                    {
                        System.Diagnostics.Process.Start("http://brn.bla.co.th/forms/frmservlet?config=claim&POLICYNO=" + policyNo + " ");
                    }
                }
                else if ((e.RowIndex > -1) && (dgvCustomerOrdinary.Rows[e.RowIndex].IsNewRow == false) && (e.ColumnIndex == dgvCustomerOrdinary.Rows[e.RowIndex].Cells["CPODocument"].ColumnIndex))
                {
                    if (productType == "POL")
                    {
                        var Data = new { id = policyID, type = "policy", user = USERID, pass = PWD };
                        string json = JsonConvert.SerializeObject(Data);
                        string Encrypt_Text = EncryptText(json, "BangkokLifeIT2014");
                        Encrypt_Text = System.Web.HttpUtility.UrlEncode(Encrypt_Text);
                        Process.Start("http://mvcsvc.bla.co.th/DMSContent/home/index?input=" + Encrypt_Text);
                    }
                    else if (productType == "APP")
                    {
                        var Data = new { id = policyNo, type = "APP", user = USERID, pass = PWD };
                        string json = JsonConvert.SerializeObject(Data);
                        string Encrypt_Text = EncryptText(json, "BangkokLifeIT2014");
                        Encrypt_Text = System.Web.HttpUtility.UrlEncode(Encrypt_Text);
                        Process.Start("http://mvcsvc.bla.co.th/DMSContent/home/index?input=" + Encrypt_Text);
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }


        }
        private string EncryptText(string input, string password)
        {
            // Get the bytes of the string
            byte[] bytesToBeEncrypted = Encoding.UTF8.GetBytes(input);
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);

            // Hash the password with SHA256
            passwordBytes = SHA256.Create().ComputeHash(passwordBytes);

            byte[] bytesEncrypted = AES_Encrypt(bytesToBeEncrypted, passwordBytes);

            string result = Convert.ToBase64String(bytesEncrypted);

            return result;
        }
        public byte[] AES_Encrypt(byte[] bytesToBeEncrypted, byte[] passwordBytes)
        {
            byte[] encryptedBytes = null;

            // Set your salt here, change it to meet your flavor:
            // The salt bytes must be at least 8 bytes.
            byte[] saltBytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };

            using (MemoryStream ms = new MemoryStream())
            {
                using (RijndaelManaged AES = new RijndaelManaged())
                {
                    AES.KeySize = 256;
                    AES.BlockSize = 128;

                    var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);
                    AES.Key = key.GetBytes(AES.KeySize / 8);
                    AES.IV = key.GetBytes(AES.BlockSize / 8);

                    AES.Mode = CipherMode.CBC;

                    using (var cs = new CryptoStream(ms, AES.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(bytesToBeEncrypted, 0, bytesToBeEncrypted.Length);
                        cs.Close();
                    }
                    encryptedBytes = ms.ToArray();
                }
            }

            return encryptedBytes;
        }

        private void dgvCustomerBancassurance_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                String policyID = "";
                String policyNo = "";
                String productType = "";
                String claim = "";


                policyID = dgvCustomerBancassurance.Rows[e.RowIndex].Cells["CPBPolicyID"].Value == null ? "" : dgvCustomerBancassurance.Rows[e.RowIndex].Cells["CPBPolicyID"].Value.ToString();
                policyNo = dgvCustomerBancassurance.Rows[e.RowIndex].Cells["CPBPolicyNo"].Value == null ? "" : dgvCustomerBancassurance.Rows[e.RowIndex].Cells["CPBPolicyNo"].Value.ToString();
                productType = dgvCustomerBancassurance.Rows[e.RowIndex].Cells["CPBProductType"].Value == null ? "" : dgvCustomerBancassurance.Rows[e.RowIndex].Cells["CPBProductType"].Value.ToString();
                claim = dgvCustomerBancassurance.Rows[e.RowIndex].Cells["CPBClaim"].Value == null ? "" : dgvCustomerBancassurance.Rows[e.RowIndex].Cells["CPBClaim"].Value.ToString();

                if ((e.RowIndex > -1) && (dgvCustomerBancassurance.Rows[e.RowIndex].IsNewRow == false) && (e.ColumnIndex == dgvCustomerBancassurance.Rows[e.RowIndex].Cells["CPBPolicyNo"].ColumnIndex) && (policyNo != ""))
                {
                    if (productType == "POL")
                    {
                        System.Diagnostics.Process.Start("http://mvcsvc.bla.co.th/CallCenter/?POLICY_ID=" + policyID + "&TYPE=P&USERID=" + USERID + " ");
                    }
                    else if (productType == "APP")
                    {
                        System.Diagnostics.Process.Start("http://mvcsvc.bla.co.th/CallCenter/?POLICY_ID=" + policyNo + "&TYPE=A&USERID=" + USERID + " ");
                    }
                }
                else if ((e.RowIndex > -1) && (dgvCustomerBancassurance.Rows[e.RowIndex].IsNewRow == false) && (e.ColumnIndex == dgvCustomerBancassurance.Rows[e.RowIndex].Cells["CPBClaim"].ColumnIndex) && (claim == "Y"))
                {
                    if (productType == "POL")
                    {
                        System.Diagnostics.Process.Start("http://brn.bla.co.th/forms/frmservlet?config=claim&POLICYNO=" + policyNo + " ");
                    }
                }
                else if ((e.RowIndex > -1) && (dgvCustomerBancassurance.Rows[e.RowIndex].IsNewRow == false) && (e.ColumnIndex == dgvCustomerBancassurance.Rows[e.RowIndex].Cells["CPBDocument"].ColumnIndex))
                {
                    if (productType == "POL")
                    {
                        var Data = new { id = policyID, type = "policy", user = USERID, pass = PWD };
                        string json = JsonConvert.SerializeObject(Data);
                        string Encrypt_Text = EncryptText(json, "BangkokLifeIT2014");
                        Encrypt_Text = System.Web.HttpUtility.UrlEncode(Encrypt_Text);
                        Process.Start("http://mvcsvc.bla.co.th/DMSContent/home/index?input=" + Encrypt_Text);
                    }
                    else if (productType == "APP")
                    {
                        var Data = new { id = policyNo, type = "APP", user = USERID, pass = PWD };
                        string json = JsonConvert.SerializeObject(Data);
                        string Encrypt_Text = EncryptText(json, "BangkokLifeIT2014");
                        Encrypt_Text = System.Web.HttpUtility.UrlEncode(Encrypt_Text);
                        Process.Start("http://mvcsvc.bla.co.th/DMSContent/home/index?input=" + Encrypt_Text);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void dgvCustomerGroupMortgage_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                String policyID = "";
                String policyNo = "";
                String productType = "";
                String claim = "";

                policyID = dgvCustomerGroupMortgage.Rows[e.RowIndex].Cells["GMCPolicyID"].Value == null ? "" : dgvCustomerGroupMortgage.Rows[e.RowIndex].Cells["GMCPolicyID"].Value.ToString();
                policyNo = dgvCustomerGroupMortgage.Rows[e.RowIndex].Cells["GMCPolicyNo"].Value == null ? "" : dgvCustomerGroupMortgage.Rows[e.RowIndex].Cells["GMCPolicyNo"].Value.ToString();
                productType = dgvCustomerGroupMortgage.Rows[e.RowIndex].Cells["GMCProductType"].Value == null ? "" : dgvCustomerGroupMortgage.Rows[e.RowIndex].Cells["GMCProductType"].Value.ToString();
                claim = dgvCustomerGroupMortgage.Rows[e.RowIndex].Cells["GMCClaim"].Value == null ? "" : dgvCustomerGroupMortgage.Rows[e.RowIndex].Cells["GMCClaim"].Value.ToString();


                if ((e.RowIndex > -1) && (dgvCustomerGroupMortgage.Rows[e.RowIndex].IsNewRow == false) && (e.ColumnIndex == dgvCustomerGroupMortgage.Rows[e.RowIndex].Cells["GMCPolicyNo"].ColumnIndex) && (policyNo != ""))
                {
                    if (productType == "POL")
                    {
                        System.Diagnostics.Process.Start("http://mvcsvc.bla.co.th/CallCenter/?POLICY_ID=" + policyID + "&TYPE=P&USERID=" + USERID + " ");
                    }
                    else if (productType == "APP")
                    {
                        System.Diagnostics.Process.Start("http://mvcsvc.bla.co.th/CallCenter/?POLICY_ID=" + policyNo + "&TYPE=A&USERID=" + USERID + " ");
                    }
                }
                //else if ((e.RowIndex > -1) && (dgvCustomerGroupMortgage.Rows[e.RowIndex].IsNewRow == false) && (e.ColumnIndex == dgvCustomerGroupMortgage.Rows[e.RowIndex].Cells["GMCClaim"].ColumnIndex) && (claim == "Y"))
                //{
                //    if (productType == "POL")
                //    {
                //        System.Diagnostics.Process.Start("http://orclapps5.bla.co.th:7778/forms/frmservlet?config=claim&POLICYNO=" + policyNo + " ");
                //    }
                //}
                else if ((e.RowIndex > -1) && (dgvCustomerGroupMortgage.Rows[e.RowIndex].IsNewRow == false) && (e.ColumnIndex == dgvCustomerGroupMortgage.Rows[e.RowIndex].Cells["GMCDocument"].ColumnIndex))
                {
                    if (productType == "POL")
                    {
                        var Data = new { id = policyID, type = "policy", user = USERID, pass = PWD };
                        string json = JsonConvert.SerializeObject(Data);
                        string Encrypt_Text = EncryptText(json, "BangkokLifeIT2014");
                        Encrypt_Text = System.Web.HttpUtility.UrlEncode(Encrypt_Text);
                        Process.Start("http://mvcsvc.bla.co.th/DMSContent/home/index?input=" + Encrypt_Text);
                    }
                    else if (productType == "APP")
                    {
                        var Data = new { id = policyNo, type = "APP", user = USERID, pass = PWD };
                        string json = JsonConvert.SerializeObject(Data);
                        string Encrypt_Text = EncryptText(json, "BangkokLifeIT2014");
                        Encrypt_Text = System.Web.HttpUtility.UrlEncode(Encrypt_Text);
                        Process.Start("http://mvcsvc.bla.co.th/DMSContent/home/index?input=" + Encrypt_Text);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void dataGridCustomerHL_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                String policyID = "";
                String policyNo = "";
                String productType = "";
                String claim = "";

                policyID = dataGridCustomerHL.Rows[e.RowIndex].Cells["HLCPolicyID"].Value == null ? "" : dataGridCustomerHL.Rows[e.RowIndex].Cells["HLCPolicyID"].Value.ToString();
                policyNo = dataGridCustomerHL.Rows[e.RowIndex].Cells["HLCPolicyNo"].Value == null ? "" : dataGridCustomerHL.Rows[e.RowIndex].Cells["HLCPolicyNo"].Value.ToString();
                productType = dataGridCustomerHL.Rows[e.RowIndex].Cells["HLCProductType"].Value == null ? "" : dataGridCustomerHL.Rows[e.RowIndex].Cells["HLCProductType"].Value.ToString();
                claim = dataGridCustomerHL.Rows[e.RowIndex].Cells["HLCClaim"].Value == null ? "" : dataGridCustomerHL.Rows[e.RowIndex].Cells["HLCClaim"].Value.ToString();


                if ((e.RowIndex > -1) && (dataGridCustomerHL.Rows[e.RowIndex].IsNewRow == false) && (e.ColumnIndex == dataGridCustomerHL.Rows[e.RowIndex].Cells["HLCPolicyNo"].ColumnIndex) && (policyNo != ""))
                {
                    if (productType == "POL")
                    {
                        System.Diagnostics.Process.Start("http://mvcsvc.bla.co.th/CallCenter/?POLICY_ID=" + policyID + "&TYPE=P&USERID=" + USERID + " ");
                    }
                    else if (productType == "APP")
                    {
                        System.Diagnostics.Process.Start("http://mvcsvc.bla.co.th/CallCenter/?POLICY_ID=" + policyNo + "&TYPE=A&USERID=" + USERID + " ");
                    }
                }
                //else if ((e.RowIndex > -1) && (dataGridCustomerHL.Rows[e.RowIndex].IsNewRow == false) && (e.ColumnIndex == dataGridCustomerHL.Rows[e.RowIndex].Cells["HLCClaim"].ColumnIndex) && (claim == "Y"))
                //{
                //    if (productType == "POL")
                //    {
                //        System.Diagnostics.Process.Start("http://orclapps5.bla.co.th:7778/forms/frmservlet?config=claim&POLICYNO=" + policyNo + " ");
                //    }
                //}
                else if ((e.RowIndex > -1) && (dataGridCustomerHL.Rows[e.RowIndex].IsNewRow == false) && (e.ColumnIndex == dataGridCustomerHL.Rows[e.RowIndex].Cells["HLCDocument"].ColumnIndex))
                {
                    if (productType == "POL")
                    {
                        var Data = new { id = policyID, type = "policy", user = USERID, pass = PWD };
                        string json = JsonConvert.SerializeObject(Data);
                        string Encrypt_Text = EncryptText(json, "BangkokLifeIT2014");
                        Encrypt_Text = System.Web.HttpUtility.UrlEncode(Encrypt_Text);
                        Process.Start("http://mvcsvc.bla.co.th/DMSContent/home/index?input=" + Encrypt_Text);
                    }
                    else if (productType == "APP")
                    {
                        var Data = new { id = policyNo, type = "APP", user = USERID, pass = PWD };
                        string json = JsonConvert.SerializeObject(Data);
                        string Encrypt_Text = EncryptText(json, "BangkokLifeIT2014");
                        Encrypt_Text = System.Web.HttpUtility.UrlEncode(Encrypt_Text);
                        Process.Start("http://mvcsvc.bla.co.th/DMSContent/home/index?input=" + Encrypt_Text);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void dataGridCustomerPD_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                String policyID = "";
                String policyNo = "";
                String productType = "";
                String claim = "";

                policyID = dataGridCustomerPD.Rows[e.RowIndex].Cells["PDCPolicyID"].Value == null ? "" : dataGridCustomerPD.Rows[e.RowIndex].Cells["PDCPolicyID"].Value.ToString();
                policyNo = dataGridCustomerPD.Rows[e.RowIndex].Cells["PDCPolicyNo"].Value == null ? "" : dataGridCustomerPD.Rows[e.RowIndex].Cells["PDCPolicyNo"].Value.ToString();
                productType = dataGridCustomerPD.Rows[e.RowIndex].Cells["PDCProductType"].Value == null ? "" : dataGridCustomerPD.Rows[e.RowIndex].Cells["PDCProductType"].Value.ToString();
                claim = dataGridCustomerPD.Rows[e.RowIndex].Cells["PDCClaim"].Value == null ? "" : dataGridCustomerPD.Rows[e.RowIndex].Cells["PDCClaim"].Value.ToString();


                if ((e.RowIndex > -1) && (dataGridCustomerPD.Rows[e.RowIndex].IsNewRow == false) && (e.ColumnIndex == dataGridCustomerPD.Rows[e.RowIndex].Cells["PDCPolicyNo"].ColumnIndex) && (policyNo != ""))
                {
                    if (productType == "POL")
                    {
                        System.Diagnostics.Process.Start("http://mvcsvc.bla.co.th/CallCenter/?POLICY_ID=" + policyID + "&TYPE=P&USERID=" + USERID + " ");
                    }
                    else if (productType == "APP")
                    {
                        System.Diagnostics.Process.Start("http://mvcsvc.bla.co.th/CallCenter/?POLICY_ID=" + policyNo + "&TYPE=A&USERID=" + USERID + " ");
                    }
                }
                //else if ((e.RowIndex > -1) && (dataGridCustomerPD.Rows[e.RowIndex].IsNewRow == false) && (e.ColumnIndex == dataGridCustomerPD.Rows[e.RowIndex].Cells["PDCClaim"].ColumnIndex) && (claim == "Y"))
                //{
                //    if (productType == "POL")
                //    {
                //        System.Diagnostics.Process.Start("http://orclapps5.bla.co.th:7778/forms/frmservlet?config=claim&POLICYNO=" + policyNo + " ");
                //    }
                //}
                else if ((e.RowIndex > -1) && (dataGridCustomerPD.Rows[e.RowIndex].IsNewRow == false) && (e.ColumnIndex == dataGridCustomerPD.Rows[e.RowIndex].Cells["PDCDocument"].ColumnIndex))
                {
                    if (productType == "POL")
                    {
                        var Data = new { id = policyID, type = "policy", user = USERID, pass = PWD };
                        string json = JsonConvert.SerializeObject(Data);
                        string Encrypt_Text = EncryptText(json, "BangkokLifeIT2014");
                        Encrypt_Text = System.Web.HttpUtility.UrlEncode(Encrypt_Text);
                        Process.Start("http://mvcsvc.bla.co.th/DMSContent/home/index?input=" + Encrypt_Text);
                    }
                    else if (productType == "APP")
                    {
                        var Data = new { id = policyNo, type = "APP", user = USERID, pass = PWD };
                        string json = JsonConvert.SerializeObject(Data);
                        string Encrypt_Text = EncryptText(json, "BangkokLifeIT2014");
                        Encrypt_Text = System.Web.HttpUtility.UrlEncode(Encrypt_Text);
                        Process.Start("http://mvcsvc.bla.co.th/DMSContent/home/index?input=" + Encrypt_Text);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void dataGridCustomerPO_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                String policyID = "";
                String policyNo = "";
                String productType = "";
                String claim = "";

                policyID = dataGridCustomerPO.Rows[e.RowIndex].Cells["POCPolicyID"].Value == null ? "" : dataGridCustomerPO.Rows[e.RowIndex].Cells["POCPolicyID"].Value.ToString();
                policyNo = dataGridCustomerPO.Rows[e.RowIndex].Cells["POCPolicyNo"].Value == null ? "" : dataGridCustomerPO.Rows[e.RowIndex].Cells["POCPolicyNo"].Value.ToString();
                productType = dataGridCustomerPO.Rows[e.RowIndex].Cells["POCProductType"].Value == null ? "" : dataGridCustomerPO.Rows[e.RowIndex].Cells["POCProductType"].Value.ToString();
                claim = dataGridCustomerPO.Rows[e.RowIndex].Cells["POCClaim"].Value == null ? "" : dataGridCustomerPO.Rows[e.RowIndex].Cells["POCClaim"].Value.ToString();


                if ((e.RowIndex > -1) && (dataGridCustomerPO.Rows[e.RowIndex].IsNewRow == false) && (e.ColumnIndex == dataGridCustomerPO.Rows[e.RowIndex].Cells["POCPolicyNo"].ColumnIndex) && (policyNo != ""))
                {
                    if (productType == "POL")
                    {
                        System.Diagnostics.Process.Start("http://mvcsvc.bla.co.th/CallCenter/?POLICY_ID=" + policyID + "&TYPE=P&USERID=" + USERID + " ");
                    }
                    else if (productType == "APP")
                    {
                        System.Diagnostics.Process.Start("http://mvcsvc.bla.co.th/CallCenter/?POLICY_ID=" + policyNo + "&TYPE=A&USERID=" + USERID + " ");
                    }
                }
                //else if ((e.RowIndex > -1) && (dataGridCustomerPO.Rows[e.RowIndex].IsNewRow == false) && (e.ColumnIndex == dataGridCustomerPO.Rows[e.RowIndex].Cells["POCClaim"].ColumnIndex) && (claim == "Y"))
                //{
                //    if (productType == "POL")
                //    {
                //        System.Diagnostics.Process.Start("http://orclapps5.bla.co.th:7778/forms/frmservlet?config=claim&POLICYNO=" + policyNo + " ");
                //    }
                //}
                else if ((e.RowIndex > -1) && (dataGridCustomerPO.Rows[e.RowIndex].IsNewRow == false) && (e.ColumnIndex == dataGridCustomerPO.Rows[e.RowIndex].Cells["POCDocument"].ColumnIndex))
                {
                    if (productType == "POL")
                    {
                        var Data = new { id = policyID, type = "policy", user = USERID, pass = PWD };
                        string json = JsonConvert.SerializeObject(Data);
                        string Encrypt_Text = EncryptText(json, "BangkokLifeIT2014");
                        Encrypt_Text = System.Web.HttpUtility.UrlEncode(Encrypt_Text);
                        Process.Start("http://mvcsvc.bla.co.th/DMSContent/home/index?input=" + Encrypt_Text);
                    }
                    else if (productType == "APP")
                    {
                        var Data = new { id = policyNo, type = "APP", user = USERID, pass = PWD };
                        string json = JsonConvert.SerializeObject(Data);
                        string Encrypt_Text = EncryptText(json, "BangkokLifeIT2014");
                        Encrypt_Text = System.Web.HttpUtility.UrlEncode(Encrypt_Text);
                        Process.Start("http://mvcsvc.bla.co.th/DMSContent/home/index?input=" + Encrypt_Text);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void dataGridCustomerKB_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                String policyID = "";
                String policyNo = "";
                String productType = "";
                String claim = "";

                policyID = dataGridCustomerKB.Rows[e.RowIndex].Cells["KBCPolicyID"].Value == null ? "" : dataGridCustomerKB.Rows[e.RowIndex].Cells["KBCPolicyID"].Value.ToString();
                policyNo = dataGridCustomerKB.Rows[e.RowIndex].Cells["KBCPolicyNo"].Value == null ? "" : dataGridCustomerKB.Rows[e.RowIndex].Cells["KBCPolicyNo"].Value.ToString();
                productType = dataGridCustomerKB.Rows[e.RowIndex].Cells["KBCProductType"].Value == null ? "" : dataGridCustomerKB.Rows[e.RowIndex].Cells["KBCProductType"].Value.ToString();
                claim = dataGridCustomerKB.Rows[e.RowIndex].Cells["KBCClaim"].Value == null ? "" : dataGridCustomerKB.Rows[e.RowIndex].Cells["KBCClaim"].Value.ToString();


                if ((e.RowIndex > -1) && (dataGridCustomerKB.Rows[e.RowIndex].IsNewRow == false) && (e.ColumnIndex == dataGridCustomerKB.Rows[e.RowIndex].Cells["KBCPolicyNo"].ColumnIndex) && (policyNo != ""))
                {
                    if (productType == "POL")
                    {
                        System.Diagnostics.Process.Start("http://mvcsvc.bla.co.th/CallCenter/?POLICY_ID=" + policyID + "&TYPE=P&USERID=" + USERID + " ");
                    }
                    else if (productType == "APP")
                    {
                        System.Diagnostics.Process.Start("http://mvcsvc.bla.co.th/CallCenter/?POLICY_ID=" + policyNo + "&TYPE=A&USERID=" + USERID + " ");
                    }
                }
                //else if ((e.RowIndex > -1) && (dataGridCustomerKB.Rows[e.RowIndex].IsNewRow == false) && (e.ColumnIndex == dataGridCustomerKB.Rows[e.RowIndex].Cells["KBCClaim"].ColumnIndex) && (claim == "Y"))
                //{
                //    if (productType == "POL")
                //    {
                //        System.Diagnostics.Process.Start("http://orclapps5.bla.co.th:7778/forms/frmservlet?config=claim&POLICYNO=" + policyNo + " ");
                //    }
                //}
                else if ((e.RowIndex > -1) && (dataGridCustomerKB.Rows[e.RowIndex].IsNewRow == false) && (e.ColumnIndex == dataGridCustomerKB.Rows[e.RowIndex].Cells["KBCDocument"].ColumnIndex))
                {
                    if (productType == "POL")
                    {
                        var Data = new { id = policyID, type = "policy", user = USERID, pass = PWD };
                        string json = JsonConvert.SerializeObject(Data);
                        string Encrypt_Text = EncryptText(json, "BangkokLifeIT2014");
                        Encrypt_Text = System.Web.HttpUtility.UrlEncode(Encrypt_Text);
                        Process.Start("http://mvcsvc.bla.co.th/DMSContent/home/index?input=" + Encrypt_Text);
                    }
                    else if (productType == "APP")
                    {
                        var Data = new { id = policyNo, type = "APP", user = USERID, pass = PWD };
                        string json = JsonConvert.SerializeObject(Data);
                        string Encrypt_Text = EncryptText(json, "BangkokLifeIT2014");
                        Encrypt_Text = System.Web.HttpUtility.UrlEncode(Encrypt_Text);
                        Process.Start("http://mvcsvc.bla.co.th/DMSContent/home/index?input=" + Encrypt_Text);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void dgvParentOrdinary_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                String policyID = "";
                String policyNo = "";
                String productType = "";
                String claim = "";

                policyID = dgvParentOrdinary.Rows[e.RowIndex].Cells["PPOPolicyID"].Value == null ? "" : dgvParentOrdinary.Rows[e.RowIndex].Cells["PPOPolicyID"].Value.ToString();
                policyNo = dgvParentOrdinary.Rows[e.RowIndex].Cells["PPOPolicyNo"].Value == null ? "" : dgvParentOrdinary.Rows[e.RowIndex].Cells["PPOPolicyNo"].Value.ToString();
                productType = dgvParentOrdinary.Rows[e.RowIndex].Cells["PPOProductType"].Value == null ? "" : dgvParentOrdinary.Rows[e.RowIndex].Cells["PPOProductType"].Value.ToString();
                claim = dgvParentOrdinary.Rows[e.RowIndex].Cells["PPOClaim"].Value == null ? "" : dgvParentOrdinary.Rows[e.RowIndex].Cells["PPOClaim"].Value.ToString();


                if ((e.RowIndex > -1) && (dgvParentOrdinary.Rows[e.RowIndex].IsNewRow == false) && (e.ColumnIndex == dgvParentOrdinary.Rows[e.RowIndex].Cells["PPOPolicyNo"].ColumnIndex) && (policyNo != ""))
                {
                    if (productType == "POL")
                    {
                        System.Diagnostics.Process.Start("http://mvcsvc.bla.co.th/CallCenter/?POLICY_ID=" + policyID + "&TYPE=P&USERID=" + USERID + " ");
                    }
                    else if (productType == "APP")
                    {
                        System.Diagnostics.Process.Start("http://mvcsvc.bla.co.th/CallCenter/?POLICY_ID=" + policyNo + "&TYPE=A&USERID=" + USERID + " ");
                    }
                }
                else if ((e.RowIndex > -1) && (dgvParentOrdinary.Rows[e.RowIndex].IsNewRow == false) && (e.ColumnIndex == dgvParentOrdinary.Rows[e.RowIndex].Cells["PPOClaim"].ColumnIndex) && (claim == "Y"))
                {
                    if (productType == "POL")
                    {
                        System.Diagnostics.Process.Start("http://brn.bla.co.th/forms/frmservlet?config=claim&POLICYNO=" + policyNo + " ");
                    }
                }
                else if ((e.RowIndex > -1) && (dgvParentOrdinary.Rows[e.RowIndex].IsNewRow == false) && (e.ColumnIndex == dgvParentOrdinary.Rows[e.RowIndex].Cells["PPODocument"].ColumnIndex))
                {
                    if (productType == "POL")
                    {
                        var Data = new { id = policyID, type = "policy", user = USERID, pass = PWD };
                        string json = JsonConvert.SerializeObject(Data);
                        string Encrypt_Text = EncryptText(json, "BangkokLifeIT2014");
                        Encrypt_Text = System.Web.HttpUtility.UrlEncode(Encrypt_Text);
                        Process.Start("http://mvcsvc.bla.co.th/DMSContent/home/index?input=" + Encrypt_Text);
                    }
                    else if (productType == "APP")
                    {
                        var Data = new { id = policyNo, type = "APP", user = USERID, pass = PWD };
                        string json = JsonConvert.SerializeObject(Data);
                        string Encrypt_Text = EncryptText(json, "BangkokLifeIT2014");
                        Encrypt_Text = System.Web.HttpUtility.UrlEncode(Encrypt_Text);
                        Process.Start("http://mvcsvc.bla.co.th/DMSContent/home/index?input=" + Encrypt_Text);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void dgvParentBancassurance_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvParentGroupMortgage_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                String policyID = "";
                String policyNo = "";
                String productType = "";
                String claim = "";

                policyID = dgvParentGroupMortgage.Rows[e.RowIndex].Cells["GMPPolicyID"].Value == null ? "" : dgvParentGroupMortgage.Rows[e.RowIndex].Cells["GMPPolicyID"].Value.ToString();
                policyNo = dgvParentGroupMortgage.Rows[e.RowIndex].Cells["GMPPolicyNo"].Value == null ? "" : dgvParentGroupMortgage.Rows[e.RowIndex].Cells["GMPPolicyNo"].Value.ToString();
                productType = dgvParentGroupMortgage.Rows[e.RowIndex].Cells["GMPProductType"].Value == null ? "" : dgvParentGroupMortgage.Rows[e.RowIndex].Cells["GMPProductType"].Value.ToString();
                claim = dgvParentGroupMortgage.Rows[e.RowIndex].Cells["GMPClaim"].Value == null ? "" : dgvParentGroupMortgage.Rows[e.RowIndex].Cells["GMPClaim"].Value.ToString();


                if ((e.RowIndex > -1) && (dgvParentGroupMortgage.Rows[e.RowIndex].IsNewRow == false) && (e.ColumnIndex == dgvParentGroupMortgage.Rows[e.RowIndex].Cells["GMPPolicyNo"].ColumnIndex) && (policyNo != ""))
                {
                    if (productType == "POL")
                    {
                        System.Diagnostics.Process.Start("http://mvcsvc.bla.co.th/CallCenter/?POLICY_ID=" + policyID + "&TYPE=P&USERID=" + USERID + " ");
                    }
                    else if (productType == "APP")
                    {
                        System.Diagnostics.Process.Start("http://mvcsvc.bla.co.th/CallCenter/?POLICY_ID=" + policyNo + "&TYPE=A&USERID=" + USERID + " ");
                    }
                }
                //else if ((e.RowIndex > -1) && (dgvParentGroupMortgage.Rows[e.RowIndex].IsNewRow == false) && (e.ColumnIndex == dgvParentGroupMortgage.Rows[e.RowIndex].Cells["GMPClaim"].ColumnIndex) && (claim == "Y"))
                //{
                //    if (productType == "POL")
                //    {
                //        System.Diagnostics.Process.Start("http://orclapps5.bla.co.th:7778/forms/frmservlet?config=claim&POLICYNO=" + policyNo + " ");
                //    }
                //}
                else if ((e.RowIndex > -1) && (dgvParentGroupMortgage.Rows[e.RowIndex].IsNewRow == false) && (e.ColumnIndex == dgvParentGroupMortgage.Rows[e.RowIndex].Cells["GMPDocument"].ColumnIndex))
                {
                    if (productType == "POL")
                    {
                        var Data = new { id = policyID, type = "policy", user = USERID, pass = PWD };
                        string json = JsonConvert.SerializeObject(Data);
                        string Encrypt_Text = EncryptText(json, "BangkokLifeIT2014");
                        Encrypt_Text = System.Web.HttpUtility.UrlEncode(Encrypt_Text);
                        Process.Start("http://mvcsvc.bla.co.th/DMSContent/home/index?input=" + Encrypt_Text);
                    }
                    else if (productType == "APP")
                    {
                        var Data = new { id = policyNo, type = "APP", user = USERID, pass = PWD };
                        string json = JsonConvert.SerializeObject(Data);
                        string Encrypt_Text = EncryptText(json, "BangkokLifeIT2014");
                        Encrypt_Text = System.Web.HttpUtility.UrlEncode(Encrypt_Text);
                        Process.Start("http://mvcsvc.bla.co.th/DMSContent/home/index?input=" + Encrypt_Text);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void dgvParentHL_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                String policyID = "";
                String policyNo = "";
                String productType = "";
                String claim = "";

                policyID = dgvParentHL.Rows[e.RowIndex].Cells["HLPPolicyID"].Value == null ? "" : dgvParentHL.Rows[e.RowIndex].Cells["HLPPolicyID"].Value.ToString();
                policyNo = dgvParentHL.Rows[e.RowIndex].Cells["HLPPolicyNo"].Value == null ? "" : dgvParentHL.Rows[e.RowIndex].Cells["HLPPolicyNo"].Value.ToString();
                productType = dgvParentHL.Rows[e.RowIndex].Cells["HLPProductType"].Value == null ? "" : dgvParentHL.Rows[e.RowIndex].Cells["HLPProductType"].Value.ToString();
                claim = dgvParentHL.Rows[e.RowIndex].Cells["HLPClaim"].Value == null ? "" : dgvParentHL.Rows[e.RowIndex].Cells["HLPClaim"].Value.ToString();


                if ((e.RowIndex > -1) && (dgvParentHL.Rows[e.RowIndex].IsNewRow == false) && (e.ColumnIndex == dgvParentHL.Rows[e.RowIndex].Cells["HLPPolicyNo"].ColumnIndex) && (policyNo != ""))
                {
                    if (productType == "POL")
                    {
                        System.Diagnostics.Process.Start("http://mvcsvc.bla.co.th/CallCenter/?POLICY_ID=" + policyID + "&TYPE=P&USERID=" + USERID + " ");
                    }
                    else if (productType == "APP")
                    {
                        System.Diagnostics.Process.Start("http://mvcsvc.bla.co.th/CallCenter/?POLICY_ID=" + policyNo + "&TYPE=A&USERID=" + USERID + " ");
                    }
                }
                //else if ((e.RowIndex > -1) && (dgvParentHL.Rows[e.RowIndex].IsNewRow == false) && (e.ColumnIndex == dgvParentHL.Rows[e.RowIndex].Cells["HLPClaim"].ColumnIndex) && (claim == "Y"))
                //{
                //    if (productType == "POL")
                //    {
                //        System.Diagnostics.Process.Start("http://orclapps5.bla.co.th:7778/forms/frmservlet?config=claim&POLICYNO=" + policyNo + " ");
                //    }
                //}
                else if ((e.RowIndex > -1) && (dgvParentHL.Rows[e.RowIndex].IsNewRow == false) && (e.ColumnIndex == dgvParentHL.Rows[e.RowIndex].Cells["HLPDocument"].ColumnIndex))
                {
                    if (productType == "POL")
                    {
                        var Data = new { id = policyID, type = "policy", user = USERID, pass = PWD };
                        string json = JsonConvert.SerializeObject(Data);
                        string Encrypt_Text = EncryptText(json, "BangkokLifeIT2014");
                        Encrypt_Text = System.Web.HttpUtility.UrlEncode(Encrypt_Text);
                        Process.Start("http://mvcsvc.bla.co.th/DMSContent/home/index?input=" + Encrypt_Text);
                    }
                    else if (productType == "APP")
                    {
                        var Data = new { id = policyNo, type = "APP", user = USERID, pass = PWD };
                        string json = JsonConvert.SerializeObject(Data);
                        string Encrypt_Text = EncryptText(json, "BangkokLifeIT2014");
                        Encrypt_Text = System.Web.HttpUtility.UrlEncode(Encrypt_Text);
                        Process.Start("http://mvcsvc.bla.co.th/DMSContent/home/index?input=" + Encrypt_Text);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void dgvParentPD_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                String policyID = "";
                String policyNo = "";
                String productType = "";
                String claim = "";

                policyID = dgvParentPD.Rows[e.RowIndex].Cells["PDPPolicyID"].Value == null ? "" : dgvParentPD.Rows[e.RowIndex].Cells["PDPPolicyID"].Value.ToString();
                policyNo = dgvParentPD.Rows[e.RowIndex].Cells["PDPPolicyNo"].Value == null ? "" : dgvParentPD.Rows[e.RowIndex].Cells["PDPPolicyNo"].Value.ToString();
                productType = dgvParentPD.Rows[e.RowIndex].Cells["PDPProductType"].Value == null ? "" : dgvParentPD.Rows[e.RowIndex].Cells["PDPProductType"].Value.ToString();
                claim = dgvParentPD.Rows[e.RowIndex].Cells["PDPClaim"].Value == null ? "" : dgvParentPD.Rows[e.RowIndex].Cells["PDPClaim"].Value.ToString();


                if ((e.RowIndex > -1) && (dgvParentPD.Rows[e.RowIndex].IsNewRow == false) && (e.ColumnIndex == dgvParentPD.Rows[e.RowIndex].Cells["PDPPolicyNo"].ColumnIndex) && (policyNo != ""))
                {
                    if (productType == "POL")
                    {
                        System.Diagnostics.Process.Start("http://mvcsvc.bla.co.th/CallCenter/?POLICY_ID=" + policyID + "&TYPE=P&USERID=" + USERID + " ");
                    }
                    else if (productType == "APP")
                    {
                        System.Diagnostics.Process.Start("http://mvcsvc.bla.co.th/CallCenter/?POLICY_ID=" + policyNo + "&TYPE=A&USERID=" + USERID + " ");
                    }
                }
                //else if ((e.RowIndex > -1) && (dgvParentPD.Rows[e.RowIndex].IsNewRow == false) && (e.ColumnIndex == dgvParentPD.Rows[e.RowIndex].Cells["PDPClaim"].ColumnIndex) && (claim == "Y"))
                //{
                //    if (productType == "POL")
                //    {
                //        System.Diagnostics.Process.Start("http://orclapps5.bla.co.th:7778/forms/frmservlet?config=claim&POLICYNO=" + policyNo + " ");
                //    }
                //}
                else if ((e.RowIndex > -1) && (dgvParentPD.Rows[e.RowIndex].IsNewRow == false) && (e.ColumnIndex == dgvParentPD.Rows[e.RowIndex].Cells["PDPDocument"].ColumnIndex))
                {
                    if (productType == "POL")
                    {
                        var Data = new { id = policyID, type = "policy", user = USERID, pass = PWD };
                        string json = JsonConvert.SerializeObject(Data);
                        string Encrypt_Text = EncryptText(json, "BangkokLifeIT2014");
                        Encrypt_Text = System.Web.HttpUtility.UrlEncode(Encrypt_Text);
                        Process.Start("http://mvcsvc.bla.co.th/DMSContent/home/index?input=" + Encrypt_Text);
                    }
                    else if (productType == "APP")
                    {
                        var Data = new { id = policyNo, type = "APP", user = USERID, pass = PWD };
                        string json = JsonConvert.SerializeObject(Data);
                        string Encrypt_Text = EncryptText(json, "BangkokLifeIT2014");
                        Encrypt_Text = System.Web.HttpUtility.UrlEncode(Encrypt_Text);
                        Process.Start("http://mvcsvc.bla.co.th/DMSContent/home/index?input=" + Encrypt_Text);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void dgvParentPO_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                String policyID = "";
                String policyNo = "";
                String productType = "";
                String claim = "";

                policyID = dgvParentPO.Rows[e.RowIndex].Cells["POPPolicyID"].Value == null ? "" : dgvParentPO.Rows[e.RowIndex].Cells["POPPolicyID"].Value.ToString();
                policyNo = dgvParentPO.Rows[e.RowIndex].Cells["POPPolicyNo"].Value == null ? "" : dgvParentPO.Rows[e.RowIndex].Cells["POPPolicyNo"].Value.ToString();
                productType = dgvParentPO.Rows[e.RowIndex].Cells["POPProductType"].Value == null ? "" : dgvParentPO.Rows[e.RowIndex].Cells["POPProductType"].Value.ToString();
                claim = dgvParentPO.Rows[e.RowIndex].Cells["POPClaim"].Value == null ? "" : dgvParentPO.Rows[e.RowIndex].Cells["POPClaim"].Value.ToString();


                if ((e.RowIndex > -1) && (dgvParentPO.Rows[e.RowIndex].IsNewRow == false) && (e.ColumnIndex == dgvParentPO.Rows[e.RowIndex].Cells["POPPolicyNo"].ColumnIndex) && (policyNo != ""))
                {
                    if (productType == "POL")
                    {
                        System.Diagnostics.Process.Start("http://mvcsvc.bla.co.th/CallCenter/?POLICY_ID=" + policyID + "&TYPE=P&USERID=" + USERID + " ");
                    }
                    else if (productType == "APP")
                    {
                        System.Diagnostics.Process.Start("http://mvcsvc.bla.co.th/CallCenter/?POLICY_ID=" + policyNo + "&TYPE=A&USERID=" + USERID + " ");
                    }
                }
                //else if ((e.RowIndex > -1) && (dgvParentPO.Rows[e.RowIndex].IsNewRow == false) && (e.ColumnIndex == dgvParentPO.Rows[e.RowIndex].Cells["POPClaim"].ColumnIndex) && (claim == "Y"))
                //{
                //    if (productType == "POL")
                //    {
                //        System.Diagnostics.Process.Start("http://orclapps5.bla.co.th:7778/forms/frmservlet?config=claim&POLICYNO=" + policyNo + " ");
                //    }
                //}
                else if ((e.RowIndex > -1) && (dgvParentPO.Rows[e.RowIndex].IsNewRow == false) && (e.ColumnIndex == dgvParentPO.Rows[e.RowIndex].Cells["POPDocument"].ColumnIndex))
                {
                    if (productType == "POL")
                    {
                        var Data = new { id = policyID, type = "policy", user = USERID, pass = PWD };
                        string json = JsonConvert.SerializeObject(Data);
                        string Encrypt_Text = EncryptText(json, "BangkokLifeIT2014");
                        Encrypt_Text = System.Web.HttpUtility.UrlEncode(Encrypt_Text);
                        Process.Start("http://mvcsvc.bla.co.th/DMSContent/home/index?input=" + Encrypt_Text);
                    }
                    else if (productType == "APP")
                    {
                        var Data = new { id = policyNo, type = "APP", user = USERID, pass = PWD };
                        string json = JsonConvert.SerializeObject(Data);
                        string Encrypt_Text = EncryptText(json, "BangkokLifeIT2014");
                        Encrypt_Text = System.Web.HttpUtility.UrlEncode(Encrypt_Text);
                        Process.Start("http://mvcsvc.bla.co.th/DMSContent/home/index?input=" + Encrypt_Text);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void dgvParentKB_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                String policyID = "";
                String policyNo = "";
                String productType = "";
                String claim = "";

                policyID = dgvParentKB.Rows[e.RowIndex].Cells["KBPPolicyID"].Value == null ? "" : dgvParentKB.Rows[e.RowIndex].Cells["KBPPolicyID"].Value.ToString();
                policyNo = dgvParentKB.Rows[e.RowIndex].Cells["KBPPolicyNo"].Value == null ? "" : dgvParentKB.Rows[e.RowIndex].Cells["KBPPolicyNo"].Value.ToString();
                productType = dgvParentKB.Rows[e.RowIndex].Cells["KBPProductType"].Value == null ? "" : dgvParentKB.Rows[e.RowIndex].Cells["KBPProductType"].Value.ToString();
                claim = dgvParentKB.Rows[e.RowIndex].Cells["KBPClaim"].Value == null ? "" : dgvParentKB.Rows[e.RowIndex].Cells["KBPClaim"].Value.ToString();


                if ((e.RowIndex > -1) && (dgvParentKB.Rows[e.RowIndex].IsNewRow == false) && (e.ColumnIndex == dgvParentKB.Rows[e.RowIndex].Cells["KBPPolicyNo"].ColumnIndex) && (policyNo != ""))
                {
                    if (productType == "POL")
                    {
                        System.Diagnostics.Process.Start("http://mvcsvc.bla.co.th/CallCenter/?POLICY_ID=" + policyID + "&TYPE=P&USERID=" + USERID + " ");
                    }
                    else if (productType == "APP")
                    {
                        System.Diagnostics.Process.Start("http://mvcsvc.bla.co.th/CallCenter/?POLICY_ID=" + policyNo + "&TYPE=A&USERID=" + USERID + " ");
                    }
                }
                //else if ((e.RowIndex > -1) && (dgvParentKB.Rows[e.RowIndex].IsNewRow == false) && (e.ColumnIndex == dgvParentKB.Rows[e.RowIndex].Cells["KBPClaim"].ColumnIndex) && (claim == "Y"))
                //{
                //    if (productType == "POL")
                //    {
                //        System.Diagnostics.Process.Start("http://orclapps5.bla.co.th:7778/forms/frmservlet?config=claim&POLICYNO=" + policyNo + " ");
                //    }
                //}
                else if ((e.RowIndex > -1) && (dgvParentKB.Rows[e.RowIndex].IsNewRow == false) && (e.ColumnIndex == dgvParentKB.Rows[e.RowIndex].Cells["KBPDocument"].ColumnIndex))
                {
                    if (productType == "POL")
                    {
                        var Data = new { id = policyID, type = "policy", user = USERID, pass = PWD };
                        string json = JsonConvert.SerializeObject(Data);
                        string Encrypt_Text = EncryptText(json, "BangkokLifeIT2014");
                        Encrypt_Text = System.Web.HttpUtility.UrlEncode(Encrypt_Text);
                        Process.Start("http://mvcsvc.bla.co.th/DMSContent/home/index?input=" + Encrypt_Text);
                    }
                    else if (productType == "APP")
                    {
                        var Data = new { id = policyNo, type = "APP", user = USERID, pass = PWD };
                        string json = JsonConvert.SerializeObject(Data);
                        string Encrypt_Text = EncryptText(json, "BangkokLifeIT2014");
                        Encrypt_Text = System.Web.HttpUtility.UrlEncode(Encrypt_Text);
                        Process.Start("http://mvcsvc.bla.co.th/DMSContent/home/index?input=" + Encrypt_Text);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
