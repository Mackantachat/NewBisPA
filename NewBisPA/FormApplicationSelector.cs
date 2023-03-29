using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ITUtility;
using System.Collections;
using Microsoft.Win32;
using PresentationControls;
using System.Threading;
using System.IO;

namespace NewBisPA
{
    public partial class FormApplicationSelector : Form 
    {
        private ApplicationForm _FormApplication;

        public ApplicationForm FormApplication
        {
            get { return _FormApplication; }
            set { _FormApplication = value; }
        }

        private string _UserID;

        public string UserID
        {
            get { return _UserID; }
            set { _UserID = value; }
        }

        private string _FORMAPP;

        public string FORMAPP
        {
            get { return _FORMAPP; }
            set { _FORMAPP = value; }
        }

        private int? _IND_ROW_CLICK;

        public int? IND_ROW_CLICK
        {
            get { return _IND_ROW_CLICK; }
            set { _IND_ROW_CLICK = value; }
        }

        private long _APPID;

        public long APPID
        {
            get { return _APPID; }
            set { _APPID = value; }
        } 

        private string _flgChk;

        public string flgChk
        {
            get { return _flgChk; }
            set { _flgChk = value; }
        }
        private string dirPath = @"C:\WINDOWS\Temp\NewbizReport";
        NewBISSvcRef.APP_IN_PROCESS[] appInProcFilterLockedItem;
        NewBISSvcRef.ProcessResult pr = new NewBISSvcRef.ProcessResult();
        NewBISSvcRef.ProcessResult pr1 = new NewBISSvcRef.ProcessResult();
        NewBISSvcRef.ProcessResult pr2 = new NewBISSvcRef.ProcessResult();
        NewBISSvcRef.ProcessResult pr3 = new NewBISSvcRef.ProcessResult();
        NewBISSvcRef.ProcessResult pr4 = new NewBISSvcRef.ProcessResult();
        NewBISSvcRef.ProcessResult prOffice = new NewBISSvcRef.ProcessResult();
        NewBISSvcRef.ProcessResult prSmart = new NewBISSvcRef.ProcessResult();

        NewBISSvcRef.APP_IN_PROCESS_COLLECION appInProcs = new NewBISSvcRef.APP_IN_PROCESS_COLLECION();
        NewBISSvcRef.AUTB_DATADIC_DET_COLLECTION autbDataDicDets = new NewBISSvcRef.AUTB_DATADIC_DET_COLLECTION();
        NewBISSvcRef.AUTB_CHANNEL_COLLECTION autbChannels = new NewBISSvcRef.AUTB_CHANNEL_COLLECTION();
        NewBISSvcRef.AUTB_STATUS_COLLECTION autbStatuses = new NewBISSvcRef.AUTB_STATUS_COLLECTION();
        NewBISSvcRef.AUTB_SUBSTATUS_COLLECTION autbSubStatuses = new NewBISSvcRef.AUTB_SUBSTATUS_COLLECTION();
        NewBISSvcRef.LIFE_PLAN_COLLECTION lifePlans = new NewBISSvcRef.LIFE_PLAN_COLLECTION();
        NewBISSvcRef.RIDER_PLAN_COLLECTION riderPlans = new NewBISSvcRef.RIDER_PLAN_COLLECTION();
        NewBISSvcRef.AUTB_UNDERWRITER autbUnderwriter = new NewBISSvcRef.AUTB_UNDERWRITER();
        NewBISSvcRef.AUTB_UNDERWRITER_COLLECTION autbUnderwriters = new NewBISSvcRef.AUTB_UNDERWRITER_COLLECTION();
        NewBISSvcRef.U_APPLICATION_LOCK uApplicationUpdate = new NewBISSvcRef.U_APPLICATION_LOCK();
        NewBISSvcRef.U_APPLICATION_LOCK[] uApplicationUpdates;
        NewBISSvcRef.ZTB_OFFICE[] ztbOffices;
        NewBISSvcRef.AUTB_FOLLOW_SMART[] autbFollowSmarts = null;
        NewBISSvcRef.AUTB_FOLLOW_SMART autbFollowSmart = new NewBISSvcRef.AUTB_FOLLOW_SMART();
        NewBISSvcRef.AUTB_FOLLOW_SMART_Collection autbFollowSmartColl = new NewBISSvcRef.AUTB_FOLLOW_SMART_Collection();
        NewBISSvcRef.ProcessResult prTel = new NewBISSvcRef.ProcessResult();
        NewBISSvcRef.ZTB_MARKETING_TYPE[] ztbMarketingTypes = null;

        public FormApplicationSelector() {
            InitializeComponent();
        }

        public FormApplicationSelector(string _user_id)
        {
            InitializeComponent();
            UserID = _user_id;
        }

        private void FormApplicationSelector_Load(object sender, EventArgs e)
        {
            FormApplication = new ApplicationForm();
            bindingControl();
            this.cmbstatus.SelectedIndexChanged += new System.EventHandler(this.cmbstatus_SelectedIndexChanged);
            this.cmbchannel.SelectedIndexChanged += new System.EventHandler(this.cmbchannel_SelectedIndexChanged);
            this.cmbworkgroup.SelectedIndexChanged += new System.EventHandler(this.cmbworkgroup_SelectedIndexChanged);
            getRemainWork();
            flgChk = "1";
            displayUser(); 
        }

        private void displayUser()
        {
            CenterSvcRef.ProcessResult prCenter = new CenterSvcRef.ProcessResult();
            CenterSvcRef.User userObj = new CenterSvcRef.User();
            using (CenterSvcRef.CenterServiceClient clientCenter = new CenterSvcRef.CenterServiceClient())
            {
                userObj = clientCenter.getUser(out prCenter, UserID);
                if (prCenter.Successed == false)
                {
                    throw new Exception(prCenter.ErrorMessage);
                }
            }

            if (userObj != null && userObj.UserID != null)
            {
                toolStripLabelUser.Text = "ชื่อผู้ใช้งาน : คุณ" + userObj.firstName + "  " + userObj.lastName + "  สังกัด : " + userObj.OrgDescTH + "  เวลา : " + DateTime.Now.ToString();
            }
        }

        private void bindingControl()
        { 
            using (NewBISSvcRef.NewBISSvcClient client = new NewBISSvcRef.NewBISSvcClient())
            { 
                try
                {
                    //txtdatebegin.Text = DateTime.Now.Day.ToString() + "/" + DateTime.Now.Month.ToString() + "/" + DateTime.Now.ToString("yyyy", System.Globalization.CultureInfo.GetCultureInfo("th-TH"));
                    //txtdateend.Text = DateTime.Now.Day.ToString() + "/" + DateTime.Now.Month.ToString() + "/" + DateTime.Now.ToString("yyyy", System.Globalization.CultureInfo.GetCultureInfo("th-TH"));
                    //txthrbegin.Text = "00"; //DateTime.Now.Hour.ToString().Length == 1 ? "0" + DateTime.Now.Hour.ToString() : DateTime.Now.Hour.ToString();
                    //txthrend.Text = DateTime.Now.Hour.ToString().Length == 1 ? "0" + DateTime.Now.Hour.ToString() : DateTime.Now.Hour.ToString();
                    //txtminbegin.Text = "00"; //DateTime.Now.Minute.ToString().Length == 1 ? "0" + DateTime.Now.Minute.ToString() : DateTime.Now.Minute.ToString();
                    //txtminend.Text = DateTime.Now.Minute.ToString().Length == 1 ? "0" + DateTime.Now.Minute.ToString() : DateTime.Now.Minute.ToString();

                    //txtsddoc.Text = DateTime.Now.Day.ToString() + "/" + DateTime.Now.Month.ToString() + "/" + DateTime.Now.ToString("yyyy", System.Globalization.CultureInfo.GetCultureInfo("th-TH"));
                    //txteddoc.Text = DateTime.Now.Day.ToString() + "/" + DateTime.Now.Month.ToString() + "/" + DateTime.Now.ToString("yyyy", System.Globalization.CultureInfo.GetCultureInfo("th-TH"));
                    //txtshdoc.Text = "00"; // DateTime.Now.Hour.ToString().Length == 1 ? "0" + DateTime.Now.Hour.ToString() : DateTime.Now.Hour.ToString();
                    //txtehdoc.Text = DateTime.Now.Hour.ToString().Length == 1 ? "0" + DateTime.Now.Hour.ToString() : DateTime.Now.Hour.ToString(); ;
                    //txtsmdoc.Text = "00"; //DateTime.Now.Minute.ToString().Length == 1 ? "0" + DateTime.Now.Minute.ToString() : DateTime.Now.Minute.ToString();
                    //txtemdoc.Text = DateTime.Now.Minute.ToString().Length == 1 ? "0" + DateTime.Now.Minute.ToString() : DateTime.Now.Minute.ToString();

                    //txtstatusdtbegin.Text = DateTime.Now.Day.ToString() + "/" + DateTime.Now.Month.ToString() + "/" + DateTime.Now.ToString("yyyy", System.Globalization.CultureInfo.GetCultureInfo("th-TH"));
                    //txtstatusdtend.Text = DateTime.Now.Day.ToString() + "/" + DateTime.Now.Month.ToString() + "/" + DateTime.Now.ToString("yyyy", System.Globalization.CultureInfo.GetCultureInfo("th-TH"));
                    //txtstatushrbegin.Text = "00";
                    //txtstatushrend.Text = DateTime.Now.Hour.ToString().Length == 1 ? "0" + DateTime.Now.Hour.ToString() : DateTime.Now.Hour.ToString(); ;
                    //txtstatusmnbegin.Text = "00"; 
                    //txtstatusmnend.Text = DateTime.Now.Minute.ToString().Length == 1 ? "0" + DateTime.Now.Minute.ToString() : DateTime.Now.Minute.ToString();

                    txtsddoc.Enabled = false;
                    txteddoc.Enabled = false;
                    txtshdoc.Enabled = false;
                    txtehdoc.Enabled = false;
                    txtsmdoc.Enabled = false;
                    txtemdoc.Enabled = false;

                    //workgroup
                    autbDataDicDets = client.GetWorkGroup(out pr1, "OD");
                    if (pr1.Successed == true)
                    {
                        if (autbDataDicDets != null)
                        {
                            NewBISSvcRef.AUTB_DATADIC_DET blank = new NewBISSvcRef.AUTB_DATADIC_DET();
                            blank.DESCRIPTION = "ทั้งหมด";
                            blank.CODE = "";
                            autbDataDicDets.Add(blank);

                            cmbworkgroup.DataSource = autbDataDicDets;
                            cmbworkgroup.DisplayMember = "DESCRIPTION";
                            cmbworkgroup.ValueMember = "CODE";
                            cmbworkgroup.SelectedValue = "";
                        }
                    }

                    autbChannels = client.GetAutbChannel(out pr2);
                    if (pr2.Successed == true)
                    {
                        if (autbChannels != null)
                        {
                            //NewBISSvcRef.AUTB_CHANNEL blank = new NewBISSvcRef.AUTB_CHANNEL();
                            //blank.DESCRIPTION = "ทั้งหมด";
                            //blank.CHANNEL_TYPE = "";
                            //autbChannels.Add(blank);

                            cmbchannel.DataSource = autbChannels;
                            cmbchannel.DisplayMember = "DESCRIPTION";
                            cmbchannel.ValueMember = "CHANNEL_TYPE";
                            cmbchannel.SelectedValue = "OD";
                        }
                    }

                    autbStatuses = client.GetStatusS(out pr3);

                    if (pr3.Successed == true)
                    {
                        if (autbStatuses != null)
                        {
                            NewBISSvcRef.AUTB_STATUS blank = new NewBISSvcRef.AUTB_STATUS();
                            blank.STATUS_DESCRIPTION = "ทั้งหมด";
                            blank.STATUS = "ทั้งหมด";
                            blank.APPLICATION_END = 'N';
                            autbStatuses.Add(blank);

                            var FilterStatus = from x in autbStatuses
                                               where x.APPLICATION_END == 'N'
                                               select x;

                            NewBISSvcRef.AUTB_STATUS[] autbStatusesFilter = FilterStatus.ToArray();

                            //  cmbstatus.DataSource = autbStatuses;
                            cmbstatus.DataSource = autbStatusesFilter;
                            cmbstatus.DisplayMember = "STATUS_DESCRIPTION";
                            cmbstatus.ValueMember = "STATUS";
                            cmbstatus.SelectedValue = "ทั้งหมด";
                        }
                    }

                    autbSubStatuses = client.GetSubStatusByStatus(out pr4, cmbstatus.SelectedValue.ToString());
                    if (pr4.Successed == true)
                    {
                        if (autbSubStatuses != null)
                        {
                            if (autbSubStatuses.Count > 1)
                            {
                                NewBISSvcRef.AUTB_SUBSTATUS blank = new NewBISSvcRef.AUTB_SUBSTATUS();
                                blank.SUBSTATUS_DESCRIPTION = "ทั้งหมด";
                                blank.SUBSTATUS = "ทั้งหมด";
                                autbSubStatuses.Add(blank);
                            }

                            cmbsubstatus.DataSource = autbSubStatuses;
                            cmbsubstatus.DisplayMember = "SUBSTATUS_DESCRIPTION";
                            cmbsubstatus.ValueMember = "SUBSTATUS";
                            if (autbSubStatuses.Count > 1)
                            {
                                cmbsubstatus.SelectedValue = "ทั้งหมด";
                            }
                            else if (autbSubStatuses.Count == 1)
                            {
                                cmbsubstatus.SelectedIndex = 0;
                            }

                            if (client.State != System.ServiceModel.CommunicationState.Closed)
                            {
                                client.Close();
                            }
                        }
                    }

                    //แบบประกัน
                    DataTable undType = new DataTable();
                    undType.Columns.Add(new DataColumn("Display", typeof(string)));
                    undType.Columns.Add(new DataColumn("Id", typeof(string)));
                    undType.Rows.Add(undType.NewRow());
                    undType.Rows.Add(undType.NewRow());
                    undType.Rows.Add(undType.NewRow());
                    undType.Rows.Add(undType.NewRow());
                    undType.Rows[0][0] = "Medical";
                    undType.Rows[0][1] = "M";
                    undType.Rows[1][0] = "NonMedical";
                    undType.Rows[1][1] = "N";
                    undType.Rows[2][0] = "CleanCase";
                    undType.Rows[2][1] = "C";
                    undType.Rows[3][0] = "ทั้งหมด";
                    undType.Rows[3][1] = "";

                    cmbundtype.DataSource = undType;
                    cmbundtype.DisplayMember = "Display";
                    cmbundtype.ValueMember = "Id";
                    cmbundtype.SelectedValue = "";

                    //suspense
                    DataTable suspense = new DataTable();
                    suspense.Columns.Add(new DataColumn("Display", typeof(string)));
                    suspense.Columns.Add(new DataColumn("Id", typeof(string)));
                    suspense.Rows.Add(suspense.NewRow());
                    suspense.Rows.Add(suspense.NewRow());
                    suspense.Rows.Add(suspense.NewRow());
                    suspense.Rows.Add(suspense.NewRow());
                    suspense.Rows[0][0] = "ทั้งหมด";
                    suspense.Rows[0][1] = "";
                    suspense.Rows[1][0] = "ไม่ชำระเบี้ย";
                    suspense.Rows[1][1] = "N";
                    suspense.Rows[2][0] = "ชำระเบี้ยขาด";
                    suspense.Rows[2][1] = "W";
                    suspense.Rows[3][0] = "ชำระเบี้ยครบ";
                    suspense.Rows[3][1] = "Y";
                    

                    cmbsuspense.DataSource = suspense;
                    cmbsuspense.DisplayMember = "Display";
                    cmbsuspense.ValueMember = "Id";
                    cmbsuspense.SelectedValue = "";

                    //มีเอกสารสแกนเข้า dms เพิ่มเติม
                    DataTable dmsScan = new DataTable();
                    dmsScan.Columns.Add(new DataColumn("Display", typeof(string)));
                    dmsScan.Columns.Add(new DataColumn("Id", typeof(string)));
                    dmsScan.Rows.Add(dmsScan.NewRow());
                    dmsScan.Rows.Add(dmsScan.NewRow());
                    dmsScan.Rows.Add(dmsScan.NewRow());
                    dmsScan.Rows[0][0] = "ไม่ต้องมีเอกสารการพิจารณา";//"ไม่มีเงื่อนไขในการ scan เข้าระบบ";
                    dmsScan.Rows[0][1] = "";
                    dmsScan.Rows[1][0] = "มีเอกสารการพิจารณาแล้ว";
                    dmsScan.Rows[1][1] = "Y";
                    dmsScan.Rows[2][0] = "รอเอกสารการพิจารณา";
                    dmsScan.Rows[2][1] = "N";

                    cmbdmsscan.DataSource = dmsScan;
                    cmbdmsscan.DisplayMember = "Display";
                    cmbdmsscan.ValueMember = "Id";
                    cmbdmsscan.SelectedValue = "";

                    //cmbsmart
                    DataTable smart = new DataTable();
                    smart.Columns.Add(new DataColumn("Display", typeof(string)));
                    smart.Columns.Add(new DataColumn("Id", typeof(string)));
                    smart.Rows.Add(smart.NewRow());
                    smart.Rows.Add(smart.NewRow());
                    smart.Rows.Add(smart.NewRow());
                    smart.Rows[0][0] = "อนุมัติ";
                    smart.Rows[0][1] = "Y";
                    smart.Rows[1][0] = "ไม่อนุมัติ";
                    smart.Rows[1][1] = "N";
                    smart.Rows[2][0] = "ทั้งหมด";
                    smart.Rows[2][1] = "";

                    cmbsmart.DataSource = smart;
                    cmbsmart.DisplayMember = "Display";
                    cmbsmart.ValueMember = "Id";
                    cmbsmart.SelectedValue = "";

                    //cmbreasonsmart
                    prSmart = client.GetAUTB_FOLLOW_SMART(out autbFollowSmarts, null);
                    if (prSmart.Successed == true)
                    {
                        if (autbFollowSmarts != null && autbFollowSmarts.Count() > 1)
                        {
                            autbFollowSmartColl.AddRange(autbFollowSmarts.ToArray());
                            NewBISSvcRef.AUTB_FOLLOW_SMART blank = new NewBISSvcRef.AUTB_FOLLOW_SMART();
                            blank.DESCRIPTION = "ทั้งหมด";
                            blank.FOLLOW_SMART_CODE = "";
                            autbFollowSmartColl.Add(blank);

                            cmbsmartreason.DataSource = autbFollowSmartColl;
                            cmbsmartreason.DisplayMember = "DESCRIPTION";
                            cmbsmartreason.ValueMember = "FOLLOW_SMART_CODE";
                            cmbsmartreason.SelectedValue = "";

                            //if (client.State != System.ServiceModel.CommunicationState.Closed)
                            //{
                            //    client.Close();
                            //}
                        }
                    }

                    //cmbremark
                    DataTable remark = new DataTable();
                    remark.Columns.Add(new DataColumn("Display", typeof(string)));
                    remark.Columns.Add(new DataColumn("Id", typeof(string)));
                    remark.Rows.Add(remark.NewRow());
                    remark.Rows.Add(remark.NewRow());
                    remark.Rows.Add(remark.NewRow());
                    remark.Rows[0][0] = "ทั้งหมด";
                    remark.Rows[0][1] = "";
                    remark.Rows[1][0] = "clear ปัญหาแล้ว";
                    remark.Rows[1][1] = "Y";
                    remark.Rows[2][0] = "ยังไม่ clear ปัญหา";
                    remark.Rows[2][1] = "N";

                    cmbremark.DataSource = remark;
                    cmbremark.DisplayMember = "Display";
                    cmbremark.ValueMember = "Id";
                    cmbremark.SelectedValue = "";
                     
                    //ใบคำขอยังไม่ได้ดำเนินการ
                    cmbappwait.Enabled = false;
                    txtdateappwait.Text = DateTime.Now.Day.ToString() + "/" + DateTime.Now.Month.ToString() + "/" + DateTime.Now.ToString("yyyy", System.Globalization.CultureInfo.GetCultureInfo("th-TH"));
                    txtdateappwait.Enabled = false;
                    //แบบประกัน
                    bindingPlan(client);

                    //สัญญาพิเศษ
                    bindingRiderPlan(client);

                    //ผู้พิจรณารับประกัน
                    try
                    {
                        autbUnderwriters = client.GetUnderWrite(out pr, autbUnderwriter);
                    }
                    catch (Exception ex)
                    {
                        //MessageBox.Show(ex.Message);
                        MessageBox.Show(new Form() { TopMost = true }, ex.Message);
                    }
                    if (pr.Successed == true)
                    {
                        if (autbUnderwriters != null)
                        {
                            NewBISSvcRef.AUTB_UNDERWRITER blank = new NewBISSvcRef.AUTB_UNDERWRITER();
                            blank.UND_NAME = "ทั้งหมด";
                            blank.UND_ID = "";
                            autbUnderwriters.Add(blank);

                            cmbunderwrite.DataSource = autbUnderwriters;
                            cmbunderwrite.DisplayMember = "UND_NAME";
                            cmbunderwrite.ValueMember = "UND_ID";
                            cmbunderwrite.SelectedValue = "";
                        }
                    }

                    //รหัสผู้ส่งต่อ
                    bindingUnderwriterForward(client);
                    cmbappforward.Enabled = false;

                    //ขั้นตอนการพิจารณา
                    bindingUnderwriteStep();
                    cmbundstep.Enabled = false;

                    //กลุ่มใบคำขอ
                    bindingAppGroup();
                    cmbappgroup.Enabled = false;

                    //cmbappwait
                    DataTable nonUnd = new DataTable();
                    nonUnd.Columns.Add(new DataColumn("Display", typeof(string)));
                    nonUnd.Columns.Add(new DataColumn("Id", typeof(string)));
                    nonUnd.Rows.Add(nonUnd.NewRow());
                    nonUnd.Rows.Add(nonUnd.NewRow());
                    nonUnd.Rows.Add(nonUnd.NewRow());
                    nonUnd.Rows.Add(nonUnd.NewRow());
                    nonUnd.Rows.Add(nonUnd.NewRow());
                    nonUnd.Rows[0][0] = "CO";
                    nonUnd.Rows[0][1] = "CO";
                    nonUnd.Rows[1][0] = "MO";
                    nonUnd.Rows[1][1] = "MO";
                    nonUnd.Rows[2][0] = "MD";
                    nonUnd.Rows[2][1] = "MD";
                    nonUnd.Rows[3][0] = "AP";
                    nonUnd.Rows[3][1] = "AP";
                    nonUnd.Rows[4][0] = "ทั้งหมด";
                    nonUnd.Rows[4][1] = "";

                    cmbappwait.DataSource = nonUnd;
                    cmbappwait.DisplayMember = "Display";
                    cmbappwait.ValueMember = "Id";
                    cmbappwait.SelectedValue = "";

                    //ประเภทใบคำขอ
                    DataTable type = new DataTable();
                    type.Columns.Add(new DataColumn("Display", typeof(string)));
                    type.Columns.Add(new DataColumn("Id", typeof(string)));
                    type.Rows.Add(type.NewRow());
                    type.Rows.Add(type.NewRow());
                    type.Rows.Add(type.NewRow());
                    type.Rows[0][0] = "NewBis";
                    type.Rows[0][1] = "Y";
                    type.Rows[1][0] = "ISIS";
                    type.Rows[1][1] = "N";
                    type.Rows[2][0] = "ทั้งหมด";
                    type.Rows[2][1] = "";

                    cmbapptype.DataSource = type;
                    cmbapptype.DisplayMember = "Display";
                    cmbapptype.ValueMember = "Id";
                    cmbapptype.SelectedValue = "";
                    
                    //สาขาส่งใบคำขอ
                    prOffice = client.GetZTB_OFFICE(out ztbOffices, null);
                    if (prOffice.Successed == true)
                    {
                        if (ztbOffices != null && ztbOffices.Length > 0)
                        {
                            NewBISSvcRef.ZTB_OFFICE[] office = new NewBISSvcRef.ZTB_OFFICE[ztbOffices.Length];
                           // ztbOffices.CopyTo(office);
                            office = ztbOffices;
                            AutoCompleteStringCollection datatranOffice = new AutoCompleteStringCollection();
                            foreach (NewBISSvcRef.ZTB_OFFICE o in office)
                            {
                                datatranOffice.Add(o.OFFICE);
                            }
                            txtappofc.AutoCompleteMode = AutoCompleteMode.Suggest;
                            txtappofc.AutoCompleteSource = AutoCompleteSource.CustomSource;
                            txtappofc.AutoCompleteCustomSource = datatranOffice; 
                        }
                    }
                    else
                    {
                       // MessageBox.Show(prOffice.ErrorMessage);
                        MessageBox.Show(new Form() { TopMost = true }, prOffice.ErrorMessage);
                    }

                   
                    List<NewBISSvcRef.ZTB_MARKETING_TYPE> mar = new List<NewBISSvcRef.ZTB_MARKETING_TYPE>();

                    prTel = client.GetZTB_MARKETING_TYPE(out ztbMarketingTypes, null, null);
                    if (prTel.Successed == true)
                    {
                        if (ztbMarketingTypes != null && ztbMarketingTypes.Count() > 1)
                        {
                            mar.AddRange(ztbMarketingTypes);
                            NewBISSvcRef.ZTB_MARKETING_TYPE blank = new NewBISSvcRef.ZTB_MARKETING_TYPE();
                            blank.DESCRIPTION = "ทั้งหมด";
                            blank.MARKETING_TYPE = "";
                            mar.Add(blank);

                            cmbmarketingtype.DataSource = mar;
                            cmbmarketingtype.DisplayMember = "DESCRIPTION";
                            cmbmarketingtype.ValueMember = "MARKETING_TYPE";
                            cmbmarketingtype.SelectedValue = ""; 
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                if (client.State != System.ServiceModel.CommunicationState.Closed)
                {
                    client.Close();
                }
            }
        }

        private void bindingAppGroup()
        {
            DataTable appGroup = new DataTable();
            appGroup.Columns.Add(new DataColumn("Display", typeof(string)));
            appGroup.Columns.Add(new DataColumn("Id", typeof(string)));
            appGroup.Rows.Add(appGroup.NewRow());
            appGroup.Rows.Add(appGroup.NewRow());
            appGroup.Rows.Add(appGroup.NewRow());
            appGroup.Rows[0][0] = "ใบคำขอที่ออกกรมธรรม์แล้ว";
            appGroup.Rows[0][1] = "Y";  
            appGroup.Rows[1][0] = "ใบคำขอที่ยังไม่ออกกรมธรรม์";
            appGroup.Rows[1][1] = "N";
            appGroup.Rows[2][0] = "ทั้งหมด";
            appGroup.Rows[2][1] = "";

            cmbappgroup.DataSource = appGroup;
            cmbappgroup.DisplayMember = "Display";
            cmbappgroup.ValueMember = "Id";
            cmbappgroup.SelectedValue = "";
        }

        private void bindingUnderwriteStep()
        {
            DataTable undStep = new DataTable();
            undStep.Columns.Add(new DataColumn("Display", typeof(string)));
            undStep.Columns.Add(new DataColumn("Id", typeof(string)));
            undStep.Rows.Add(undStep.NewRow());
            undStep.Rows.Add(undStep.NewRow());
            undStep.Rows.Add(undStep.NewRow());
            undStep.Rows.Add(undStep.NewRow());
            undStep.Rows[0][0] = "ผู้รับช่วงต่อรับการพิจารณาเสร็จแล้ว";
            undStep.Rows[0][1] = "Y";
            undStep.Rows[1][0] = "กำลังพิจารณารับประกัน";
            undStep.Rows[1][1] = "W";
            undStep.Rows[2][0] = "ยังไม่ได้พิจรณา";
            undStep.Rows[2][1] = "N";
            undStep.Rows[3][0] = "ทั้งหมด";
            undStep.Rows[3][1] = "";

            cmbundstep.DataSource = undStep;
            cmbundstep.DisplayMember = "Display";
            cmbundstep.ValueMember = "Id";
            cmbundstep.SelectedValue = "";
        }

        private void bindingUnderwriterForward(NewBISSvcRef.NewBISSvcClient client)
        {
            autbUnderwriters = client.GetUnderWrite(out pr, autbUnderwriter);
            if (pr.Successed == true)
            {
                if (autbUnderwriters != null)
                {
                    NewBISSvcRef.AUTB_UNDERWRITER blank = new NewBISSvcRef.AUTB_UNDERWRITER();
                    blank.UND_NAME = "ทั้งหมด";
                    blank.UND_ID = "";
                    autbUnderwriters.Add(blank);

                    cmbappforward.DataSource = autbUnderwriters;
                    cmbappforward.DisplayMember = "UND_NAME";
                    cmbappforward.ValueMember = "UND_ID";
                    cmbappforward.SelectedValue = "";
                }
            }
        }

        private void bindingPlan(NewBISSvcRef.NewBISSvcClient client)
        {
            string channel = "";

            if (cmbchannel.SelectedValue == null)
            {
                channel = "";
            }
            else
            {
                channel = cmbchannel.SelectedValue.ToString();
            }

            lifePlans = client.GetLifePlan(out pr, cmbworkgroup.SelectedValue.ToString(), channel);
            if (pr.Successed == true)
            {
                if (lifePlans != null)
                {
                    DataTable DT = new DataTable();
                    DT.Columns.AddRange(
                        new DataColumn[]
                {
                    new DataColumn("DETAIL", typeof(string)),
                    new DataColumn("KEY", typeof(string)),               
                });
                    foreach (NewBISSvcRef.LIFE_PLAN life in lifePlans)
                    {
                        DT.Rows.Add(life.DETAIL, life.KEY);
                    }

                    cmbplan.DataSource =
                        new ListSelectionWrapper<DataRow>(
                            DT.Rows,
                            "DETAIL"
                            );
                    cmbplan.DisplayMemberSingleItem = "Name";
                    cmbplan.DisplayMember = "NameConcatenated";
                    cmbplan.ValueMember = "Selected";

                    if (cmbplan.Text == "")
                        cmbplan.Text = "ทั้งหมด";
                }
            }
        }

        private void bindingRiderPlan(NewBISSvcRef.NewBISSvcClient client)
        {
        //    string channel = "";

        //    if (cmbchannel.SelectedValue == null)
        //    {
        //        channel = "";
        //    }
        //    else
        //    {
        //        channel = cmbchannel.SelectedValue.ToString();
        //    }

        //    riderPlans = client.GetLifePlan(out pr, cmbworkgroup.SelectedValue.ToString(), channel);
            riderPlans = client.GetRiderPlan(out pr);
            if (pr.Successed == true)
            {
                if (riderPlans != null)
                {
                    DataTable DT = new DataTable();
                    DT.Columns.AddRange(
                        new DataColumn[]
                {
                    new DataColumn("DETAIL", typeof(string)),
                    new DataColumn("KEY", typeof(string)),               
                });
                    foreach (NewBISSvcRef.RIDER_PLAN rider in riderPlans)
                    {
                        DT.Rows.Add(rider.DETAIL, rider.KEY);
                    }

                    cmbrider.DataSource =
                        new ListSelectionWrapper<DataRow>(
                            DT.Rows,
                            "DETAIL"
                            );
                    cmbrider.DisplayMemberSingleItem = "Name";
                    cmbrider.DisplayMember = "NameConcatenated";
                    cmbrider.ValueMember = "Selected";

                    if (cmbrider.Text == "")
                        cmbrider.Text = "ทั้งหมด";
                }
            }
        }

        private void cmbstatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                cmbsubstatus.DataSource = null;
                cmbsubstatus.Items.Clear();

                NewBISSvcRef.NewBISSvcClient client = new NewBISSvcRef.NewBISSvcClient();
                autbSubStatuses = client.GetSubStatusByStatus(out pr4, cmbstatus.SelectedValue.ToString());
                if (pr4.Successed == true)
                {
                    if (autbSubStatuses != null)
                    {
                        if (autbSubStatuses.Count > 1)
                        {
                            NewBISSvcRef.AUTB_SUBSTATUS blank = new NewBISSvcRef.AUTB_SUBSTATUS();
                            blank.SUBSTATUS_DESCRIPTION = "ทั้งหมด";
                            blank.SUBSTATUS = "ทั้งหมด";
                            autbSubStatuses.Add(blank);
                        }

                        cmbsubstatus.DataSource = autbSubStatuses;
                        cmbsubstatus.DisplayMember = "SUBSTATUS_DESCRIPTION";
                        cmbsubstatus.ValueMember = "SUBSTATUS";
                        if (autbSubStatuses.Count > 1)
                        {
                            cmbsubstatus.SelectedValue = "ทั้งหมด";
                        }
                        else if (autbSubStatuses.Count == 1)
                        {
                            cmbsubstatus.SelectedIndex = 0;
                        }

                        if (client.State != System.ServiceModel.CommunicationState.Closed)
                        {
                            client.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                MessageBox.Show(new Form() { TopMost = true }, ex.Message);
            }
        }

        private void chknotund_ChecfkedChanged(object sender, EventArgs e)
        {
            if (chknotund.Checked == true)
            {
                chkdocret.Checked = false;
                chkdocnoret.Checked = false;
                chkreceive.Checked = false;
                cmbappwait.Enabled = true;
                txtdateappwait.Enabled = true;
                //ใบคำขอยังไม่ดำเนินการ 
                DataTable nonUnd = new DataTable();
                nonUnd.Columns.Add(new DataColumn("Display", typeof(string)));
                nonUnd.Columns.Add(new DataColumn("Id", typeof(string)));
                nonUnd.Rows.Add(nonUnd.NewRow());
                nonUnd.Rows.Add(nonUnd.NewRow());
                nonUnd.Rows.Add(nonUnd.NewRow());
                nonUnd.Rows.Add(nonUnd.NewRow());
                nonUnd.Rows.Add(nonUnd.NewRow());
                nonUnd.Rows[0][0] = "CO";
                nonUnd.Rows[0][1] = "CO";
                nonUnd.Rows[1][0] = "MO";
                nonUnd.Rows[1][1] = "MO";
                nonUnd.Rows[2][0] = "MD";
                nonUnd.Rows[2][1] = "MD";
                nonUnd.Rows[3][0] = "AP";
                nonUnd.Rows[3][1] = "AP";
                nonUnd.Rows[4][0] = "ทั้งหมด";
                nonUnd.Rows[4][1] = "";

                cmbappwait.DataSource = nonUnd;
                cmbappwait.DisplayMember = "Display";
                cmbappwait.ValueMember = "Id";
                cmbappwait.SelectedValue = "";

                cmbstatus.SelectedValue = "ทั้งหมด";
                cmbstatus.Enabled = false;
                cmbsubstatus.Enabled = false;
            }
            else
            {
                cmbappwait.Enabled = false;
                txtdateappwait.Text = DateTime.Now.Day.ToString() + "/" + DateTime.Now.Month.ToString() + "/" + DateTime.Now.ToString("yyyy", System.Globalization.CultureInfo.GetCultureInfo("th-TH"));
                txtdateappwait.Enabled = false;
                cmbappwait.SelectedValue = "";
                cmbstatus.Enabled = true;
                cmbsubstatus.Enabled = true;
            }
        }

        private void cmbchannel_SelectedIndexChanged(object sender, EventArgs e)
        {
            NewBISSvcRef.NewBISSvcClient client = new NewBISSvcRef.NewBISSvcClient();
            bindingPlan(client);
           // bindingRiderPlan(client);
            if (client.State != System.ServiceModel.CommunicationState.Closed)
            {
                client.Close();
            }
        }

        private void cmbworkgroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            NewBISSvcRef.NewBISSvcClient client = new NewBISSvcRef.NewBISSvcClient();
            bindingPlan(client);
          //  bindingRiderPlan(client);
            if (client.State != System.ServiceModel.CommunicationState.Closed)
            {
                client.Close();
            }
        }

        private void dtgCallReport_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            NewBISSvcRef.ProcessResult prx = new NewBISSvcRef.ProcessResult();
            NewBISSvcRef.ProcessResult prchan = new NewBISSvcRef.ProcessResult();
            NewBISSvcRef.U_APPLICATION_LOCK ual = new NewBISSvcRef.U_APPLICATION_LOCK();
            NewBISSvcRef.U_APPLICATION_ID[] uapplies = null;
            ual = null;

            if (e.RowIndex < 0 || e.ColumnIndex != dtgCallReport.Columns["clmlinkappno"].Index && e.ColumnIndex != dtgCallReport.Columns["clmlock"].Index)
            {
                return;
            }
            else if (e.ColumnIndex == dtgCallReport.Columns["clmlinkappno"].Index)
            { 
                if (dtgCallReport["clmlock", e.RowIndex].Value == "Y")
                {
                    APPID = Convert.ToInt64(dtgCallReport["clmappid", e.RowIndex].Value);

                    NewBISSvcRef.NewBISSvcClient ccc = new NewBISSvcRef.NewBISSvcClient();

                    ual = ccc.CheckDupLock(out prx, APPID);

                    if (ual != null && ual.TRANSACTION_FLG == 'U')
                    {
                        MessageBox.Show("ใบคำขอนี้กำลังถูกพิจารณาโดบ คุณ" + getUserName(UserID));
                    }
                    else
                    {
                        FORMAPP = "APPSELECT";
                       // string channel = cmbchannel.SelectedValue.ToString();
                       
                        string appno = dtgCallReport["clmlinkappno", e.RowIndex].Value.ToString(); 
                        long[] appids = { APPID };
                        long? uappid =  Convert.ToInt64(dtgCallReport["clmuappid", e.RowIndex].Value.ToString()) ;

                        prchan = ccc.GetU_APPLICATION_ID(out uapplies, appids, appno, uappid, null);
                        if (prchan.Successed == false)
                        {
                            throw new Exception(prchan.ErrorMessage);
                        }

                        string channel = uapplies[0].CHANNEL_TYPE;
                        FormApplication.Show();
                        FormApplication.SelectAppFromMenu("SELECT", this, null, appno, channel);//(FORMAPP, APPID, this);
                        IND_ROW_CLICK = e.RowIndex;
                        this.VisibleChanged += new System.EventHandler(this.FormApplicationSelector_VisibleChanged);
                    } 
                }
                else
                {
                    MessageBox.Show("โปรด Lock ใบคำขอก่อนทำการพิจารณา");
                }
            } 
        }

        private void searchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UnlockTransaction();

            DateTime? sd = Utility.StringToDateTime(txtdatebegin.Text, "BU");
            DateTime? ed = Utility.StringToDateTime(txtdateend.Text, "BU");
            DateTime? taw = Utility.StringToDateTime(txtdateappwait.Text, "BU");

            DateTime? sdoc = Utility.StringToDateTime(txtsddoc.Text, "BU");
            DateTime? edoc = Utility.StringToDateTime(txteddoc.Text, "BU");

            DateTime? sStatusdt = Utility.StringToDateTime(txtstatusdtbegin.Text, "BU");
            DateTime? eStatusdt = Utility.StringToDateTime(txtstatusdtend.Text, "BU");

            if ((txtsumsource.Text == "" && txtsumdest.Text != "") || (txtsumsource.Text != "" && txtsumdest.Text == ""))
            {
                MessageBox.Show("ท่านกรอกช่วงทุนประกันไม่ครบถ้วน");
                txtsumsource.Focus();
            }
            else if ((txtsumsource.Text != "" && txtsumdest.Text != "") && (Convert.ToInt64(txtsumsource.Text) > Convert.ToInt64(txtsumdest.Text)))
            {
                MessageBox.Show("ทุนประกันเริ่มต้นต้องมีค่าน้อยกว่า");
                txtsumsource.Focus();
            }
            else if ((txtcalpremsrc.Text == "" && txtcalpremdes.Text != "") || (txtcalpremsrc.Text != "" && txtcalpremdes.Text == ""))
            {
                MessageBox.Show("ท่านกรอกเงินในบัญชีพักไม่ครบถ้วน");
                txtcalpremsrc.Focus();
            }
            else if ((txtcalpremsrc.Text != "" && txtcalpremdes.Text != "") && (Convert.ToInt64(txtcalpremsrc.Text) > Convert.ToInt64(txtcalpremdes.Text)))
            {
                MessageBox.Show("เงินในบัญชีพักเริ่มต้นต้องมีค่าน้อยกว่า");
                txtcalpremsrc.Focus();
            }
            else if ((txtdatebegin.Text == "" && txtdateend.Text != "") || (txtdatebegin.Text != "" && txtdateend.Text == ""))
            {
                MessageBox.Show("ท่านกรอกวันที่บันทึกใบคำขอไม่ครบถ้วน");
                txtdatebegin.Focus();
            }
            else if ((txtdatebegin.Text == "" || txtdateend.Text == "") && (txthrbegin.Text != "" || txthrend.Text != "" || txtminbegin.Text != "" || txtminend.Text != ""))
            {
                MessageBox.Show("โปรดกรอกวันที่บันทึกใบคำขอเริ่มต้นให้ครบถ้วนก่อนกรอกเวลา");
                txtdatebegin.Focus();
            }
            else if ((txtdatebegin.Text != "" && txtdateend.Text != "") && sd == null)
            {
                MessageBox.Show("รูปแบบวันที่บันทึกใบคำขอไม่ถูกต้อง");
                txtdatebegin.Focus();
            }
            else if ((txtdatebegin.Text != "" && txtdateend.Text != "") && ed == null)
            {
                MessageBox.Show("รูปแบบวันที่บันทึกใบคำขอไม่ถูกต้อง");
                txtdateend.Focus();
            }
            else if ((txtdatebegin.Text != "" && txtdateend.Text != "") && (sd != null && ed != null) && (DateTime.Compare((DateTime)sd, (DateTime)ed) > 0))
            {
                MessageBox.Show("วันที่บันทึกใบคำขอเริ่มต้นต้องมาก่อน");
                txtdatebegin.Focus();
            }
            else if ((txtsddoc.Text != "" && txteddoc.Text != "") && sdoc == null)
            {
                MessageBox.Show("รูปแบบวันที่ไม่ถูกต้อง");
                txtsddoc.Focus();
            }
            else if ((txtsddoc.Text != "" && txteddoc.Text != "") && edoc == null)
            {
                MessageBox.Show("รูปแบบวันที่ไม่ถูกต้อง");
                txteddoc.Focus();
            }
            else if ((txtsddoc.Text != "" && txteddoc.Text != "") && (sdoc != null && edoc != null) && (DateTime.Compare((DateTime)sdoc, (DateTime)edoc) > 0))
            {
                MessageBox.Show("วันที่เริ่มต้นต้องมาก่อน");
                txtsddoc.Focus();
            }
            else if (chknotund.Checked == true && taw == null) //taw
            {
                MessageBox.Show("โปรดกรอกวันที่รับค่าให้สมบูรณ์");
                txtdateappwait.Focus();
            }
            else if ((txtstatusdtbegin.Text == "" && txtstatusdtend.Text != "") || (txtstatusdtbegin.Text != "" && txtstatusdtend.Text == ""))
            {
                MessageBox.Show("ท่านกรอกวันที่บันทึกสถานะไม่ครบถ้วน");
                txtstatusdtbegin.Focus();
            }
            else if ((txtstatusdtbegin.Text == "" || txtstatusdtend.Text == "") && (txtstatushrbegin.Text != "" || txtstatushrend.Text != "" || txtstatusmnbegin.Text != "" || txtstatusmnend.Text != ""))
            {
                MessageBox.Show("โปรดกรอกวันที่บันทึกสถานะเริ่มต้นให้ครบถ้วนก่อนกรอกเวลา");
                txtstatusdtbegin.Focus();
            }
            else if ((txtstatusdtbegin.Text != "" && txtstatusdtend.Text != "") && sStatusdt == null)
            {
                MessageBox.Show("รูปแบบวันที่บันทึกสถานะไม่ถูกต้อง");
                txtstatusdtbegin.Focus();
            }
            else if ((txtstatusdtbegin.Text != "" && txtstatusdtend.Text != "") && eStatusdt == null)
            {
                MessageBox.Show("รูปแบบวันที่บันทึกสถานะไม่ถูกต้อง");
                txtstatusdtend.Focus();
            }
            else if ((txtstatusdtbegin.Text != "" && txtstatusdtend.Text != "") && (sStatusdt != null && eStatusdt != null) && (DateTime.Compare((DateTime)sStatusdt, (DateTime)eStatusdt) > 0))
            {
                MessageBox.Show("วันที่บันทึกสถานะเริ่มต้นต้องมาก่อน");
                txtstatusdtbegin.Focus();
            }
            else if (((txthrbegin.Text != "" && txthrend.Text != "" && txtminbegin.Text != "" && txtminend.Text != "") || (txthrbegin.Text == "" && txthrend.Text == "" && txtminbegin.Text == "" && txtminend.Text == "")) && ((txtshdoc.Text != "" && txtehdoc.Text != "" && txtsmdoc.Text != "" && txtemdoc.Text != "") || (txtshdoc.Text == "" && txtehdoc.Text == "" && txtsmdoc.Text == "" && txtemdoc.Text == "")) && ((txtstatushrbegin.Text != "" && txtstatushrend.Text != "" && txtstatusmnbegin.Text != "" && txtstatusmnend.Text != "") || (txtstatushrbegin.Text == "" && txtstatushrend.Text == "" && txtstatusmnbegin.Text == "" && txtstatusmnend.Text == "")))
            {
                GetData();
            } 
            else if (txthrbegin.Text == "" || txthrend.Text == "" || txtminbegin.Text == "" || txtminend.Text == "")
            {
                MessageBox.Show("ท่านกรอกเวลาที่บันทึกใบคำขอไม่ครบถ้วน");
                txthrbegin.Focus();
            }
            else if (txtshdoc.Text == "" || txtehdoc.Text == "" || txtsmdoc.Text == "" || txtemdoc.Text == "")
            {
                MessageBox.Show("ท่านกรอกเวลาไม่ครบถ้วน");
                txtshdoc.Focus();
            }
            else if (txtstatushrbegin.Text == "" || txtstatushrend.Text == "" || txtstatusmnbegin.Text == "" || txtstatusmnend.Text == "")
            {
                MessageBox.Show("ท่านกรอกเวลาไม่ครบถ้วน");
                txtstatushrbegin.Focus();
            }
        }

        private void UnlockTransaction()
        {
            this.Cursor = Cursors.WaitCursor;
            NewBISSvcRef.ProcessResult prx = new NewBISSvcRef.ProcessResult();
            NewBISSvcRef.U_APPLICATION_LOCK ual = new NewBISSvcRef.U_APPLICATION_LOCK();
            ual = null;

            try
            {
                using (NewBISSvcRef.NewBISSvcClient c = new NewBISSvcRef.NewBISSvcClient())
                {
                    if (dtgCallReport.RowCount > 0)
                    {           //delete row ที่ user ไม่มีการ update
                        dtgCallReport.EndEdit();
                        List<long> uapp_ids = new List<long>(); 
                        foreach (DataGridViewRow row in dtgCallReport.Rows)
                        {
                            ual = c.CheckDupLock(out prx, (long?)row.Cells["clmappid"].Value);

                            if (ual != null && ual.TRANSACTION_FLG == 'U')
                            { 
                              //ไมปลด
                            }
                            else
                            {
                                uapp_ids.Add((long)row.Cells["clmuappid"].Value);
                            }

                            #region "old code"              
                            //long[] uappid = { (long)row.Cells["clmuappid"].Value };
                            //string[] updid = { UserID };
                            //pr = c.GetU_APPLICATION_LOCK(out uApplicationUpdates, uappid, (DateTime[])null, updid);  //เอาค่าใน dtg ที่ค้างไป get ดูว่ามีเหลือเปล่า

                            //if (pr.Successed == true)
                            //{
                            //    if (uApplicationUpdates != null)
                            //    {
                            //        for (int i = 0; i < uApplicationUpdates.Length; i++)
                            //        {
                            //            uApplicationUpdate = uApplicationUpdates[i];         //ถ้ามี เอาค่าที่ get มาไปเป็น key ลบ 
                            //            if (uApplicationUpdate.UPDATE_FLG == 'Y')
                            //            {
                            //                uApplicationUpdate.TRANSACTION_FLG = 'E';
                            //                c.EditU_APPLICATION_LOCK(ref uApplicationUpdate);  //update ที่มีแก้ไข
                            //            }
                            //            else
                            //            {
                            //                c.RemoveU_APPLICATION_LOCK(uApplicationUpdate);  //ลบที่ไม่มีแก้ไข
                            //            }
                            //        }
                            //    }
                            //}
#endregion
                        }
                        string[] upd_ids = { UserID };
                        if (uapp_ids != null && uapp_ids.Count() > 0)
                        {
                            c.EditLock("E", uapp_ids.ToArray(), "Y", upd_ids.ToArray(), DateTime.Now);
                            c.RemoveLock(uapp_ids.ToArray(), "N", upd_ids);
                        }
                        chkall.Checked = false;
                    }
                    if (c.State != System.ServiceModel.CommunicationState.Closed)
                    {
                        c.Close();
                    }
                }
            }
            finally
            {
                this.Cursor = Cursors.Default; 
            }
        }

        private void GetData()
        {
            using (NewBISSvcRef.NewBISSvcClient client = new NewBISSvcRef.NewBISSvcClient())
            {
                this.Cursor = Cursors.WaitCursor; 

                try
                {
                    dtgCallReport.DataSource = null;
                    dtgCallReport.AutoGenerateColumns = false;

                    #region "prepaire data"

                    string appwait = cmbappwait.SelectedValue.ToString();
                    string forwardid = cmbappforward.SelectedValue.ToString();
                    string receive_case = cmbundstep.SelectedValue.ToString();

                    if (chknotund.Checked == false)
                    {
                        appwait = "";
                    }
                    else if (chknotund.Checked == true && cmbappwait.SelectedValue.ToString() == "")
                    {
                        appwait = "ALL";
                    }

                    DateTime dateAppwait = (DateTime)Utility.StringToDateTime(txtdateappwait.Text, "BU");

                    if (chkforward.Checked == false)
                    {
                        forwardid = "";
                        receive_case = "";
                    }
                    else if (chkforward.Checked == true)
                    {
                        if (cmbappforward.SelectedValue.ToString() == "")
                        {
                            forwardid = "ALL";
                        }
                        if (cmbundstep.SelectedValue.ToString() == "")
                        {
                            receive_case = "ALL";
                        }
                    }

                    List<string> lifes = new List<string>();
                    string[] lifesArray = new string[100];

                    if (cmbplan.Text != "" && cmbplan.Text != "ทั้งหมด")
                    {
                        string[] temp = cmbplan.Text.Split('&');
                        foreach (string life in temp)
                        {
                            lifes.Add(life.Replace("\"", "").Trim().Substring(0, 8));
                        }
                        lifesArray = lifes.ToArray();
                    }
                    else
                    {
                        lifes = null;
                        lifesArray = null;
                    }

                    List<string> riders = new List<string>();
                    string[] ridersArray = new string[100];

                    if (cmbrider.Text != "" && cmbrider.Text != "ทั้งหมด")
                    {
                        string[] temp = cmbrider.Text.Split('&');
                        foreach (string rider in temp)
                        {
                            riders.Add(rider.Replace("\"", "").Trim().Substring(0, 8));
                        }
                        ridersArray = riders.ToArray();
                    }
                    else
                    {
                        riders = null;
                        ridersArray = null;
                    }

                    string substatus = "";

                    if (cmbsubstatus.SelectedValue != null)
                    {
                        substatus = cmbsubstatus.SelectedValue.ToString().Trim().Replace("ทั้งหมด", "");
                    }

                    //วันที่บันทึกใบคำขอ
                    DateTime? sDateTime = null;
                    DateTime? eDateTime = null;

                    if (txtdatebegin.Text != "" && txtdateend.Text != "")
                    { 
                        DateTime startDateTime = (DateTime)Utility.StringToDateTime(txtdatebegin.Text, "BU");
                        DateTime endDateTime = (DateTime)Utility.StringToDateTime(txtdateend.Text, "BU");

                        if (txthrbegin.Text != "" && txthrend.Text != "" && txtminbegin.Text != "" && txtminend.Text != "")
                        {
                            if (txthrbegin.Text == "24" && txtminbegin.Text =="00")
                            {
                                txthrbegin.Text = "23";
                                txtminbegin.Text = "59";
                            }

                            if (txthrend.Text == "24" && txtminend.Text == "00")
                            {
                                txthrend.Text = "23";
                                txtminend.Text = "59";
                            } 

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

                    
                    //ช่วงวันที่ตอบกลับกรณีขอเอกสารเพิ่มเติม

                      DateTime? sDocDateTime = null;
                      DateTime? eDocDateTime = null;

                    if (chkdocret.Checked == true)
                    { 
                        if (txtsddoc.Text != "" && txteddoc.Text != "")
                        {
                            DateTime startDocDateTime = (DateTime)Utility.StringToDateTime(txtsddoc.Text, "BU");
                            DateTime enddocDateTime = (DateTime)Utility.StringToDateTime(txteddoc.Text, "BU");

                            if (txtshdoc.Text != "" && txtehdoc.Text != "" && txtsmdoc.Text != "" && txtemdoc.Text != "")
                            {
                                if (txtshdoc.Text == "24" && txtsmdoc.Text == "00")
                                {
                                    txtshdoc.Text = "23";
                                    txtsmdoc.Text = "59";
                                }

                                if (txtehdoc.Text == "24" && txtemdoc.Text == "00")
                                {
                                    txtehdoc.Text = "23";
                                    txtemdoc.Text = "59";
                                } 

                                startDocDateTime = startDocDateTime.ChangeTime(Convert.ToInt32(txtshdoc.Text), Convert.ToInt32(txtsmdoc.Text), 0, 0);
                                enddocDateTime = enddocDateTime.ChangeTime(Convert.ToInt32(txtehdoc.Text), Convert.ToInt32(txtemdoc.Text), 0, 0);
                            }
                            else if (txtshdoc.Text == "" && txtehdoc.Text == "" && txtsmdoc.Text == "" && txtemdoc.Text == "")
                            {
                                startDocDateTime = startDocDateTime.ChangeTime(0, 0, 0, 0);
                                enddocDateTime = enddocDateTime.ChangeTime(0, 0, 0, 0);
                            } 
                            sDocDateTime = startDocDateTime;
                            eDocDateTime = enddocDateTime;
                        }
                    }

                    //วันที่บันทึกสถานะ
                    DateTime? sStatusDateTime = null;
                    DateTime? eStatusDateTime = null;

                    if (txtstatusdtbegin.Text != "" && txtstatusdtend.Text != "")
                    {
                        DateTime beginStatusDateTime = (DateTime)Utility.StringToDateTime(txtstatusdtbegin.Text, "BU");
                        DateTime endStatusDateTime = (DateTime)Utility.StringToDateTime(txtstatusdtend.Text, "BU");

                        if (txtstatushrbegin.Text != "" && txtstatushrend.Text != "" && txtstatusmnbegin.Text != "" && txtstatusmnend.Text != "")
                        {
                            if (txtstatushrbegin.Text == "24" && txtstatusmnbegin.Text == "00")
                            {
                                txtstatushrbegin.Text = "23";
                                txtstatusmnbegin.Text = "59";
                            }

                            if (txtstatushrend.Text == "24" && txtstatusmnend.Text == "00")
                            {
                                txtstatushrend.Text = "23";
                                txtstatusmnend.Text = "59";
                            } 

                            beginStatusDateTime = beginStatusDateTime.ChangeTime(Convert.ToInt32(txtstatushrbegin.Text), Convert.ToInt32(txtstatusmnbegin.Text), 0, 0);
                            endStatusDateTime = endStatusDateTime.ChangeTime(Convert.ToInt32(txtstatushrend.Text), Convert.ToInt32(txtstatusmnend.Text), 0, 0);
                        }
                        else if (txtstatushrbegin.Text == "" && txtstatushrend.Text == "" && txtstatusmnbegin.Text == "" && txtstatusmnend.Text == "")
                        {
                            beginStatusDateTime = beginStatusDateTime.ChangeTime(0, 0, 0, 0);
                            endStatusDateTime = endStatusDateTime.ChangeTime(0, 0, 0, 0);
                        }
                        sStatusDateTime = beginStatusDateTime;
                        eStatusDateTime = endStatusDateTime;
                    }    

                    //มีการตอบกลับ
                    string appreceive = "";

                    if (chkreceive.Checked == true)
                    {
                        appreceive = "Y";
                    }
                    else
                    {
                        appreceive = "N";
                    }

                    string appsource = txtappsource.Text.Trim();
                    string appdest = txtappdest.Text.Trim();

                    if (appsource == "" && appdest != "")
                    {
                        appsource = appdest;
                    }
                    if (appsource != "" && appdest == "")
                    {
                        appdest = appsource;
                    }

                    string etcDoc = "";
                    if (chketc.Checked == true)
                    {
                        etcDoc = "Y";
                    }
                    else
                    {
                        etcDoc = "N";
                    }

                    string smartStatus = cmbsmart.SelectedValue.ToString();
                    string reasonSmart = cmbsmartreason.SelectedValue == null || cmbsmartreason.SelectedValue == "" ? "" : cmbsmartreason.SelectedValue.ToString();

                    string renewalApp = "N";
                    if (chkRenewal.Checked == true)
                    {
                        renewalApp = "Y";
                    }

                    #endregion

                    string assignment = "";

                    appInProcs = client.GetAppInProcess(out pr, appsource, appdest, txtname.Text.Trim(), txtsurname.Text.Trim(), txtidcard.Text.Trim(), cmbchannel.SelectedValue.ToString().Trim(), cmbworkgroup.SelectedValue.ToString().Trim(), txtappofc.Text.Trim(), cmbstatus.SelectedValue.ToString().Trim().Replace("ทั้งหมด", ""), substatus, txtcalpremsrc.Text.Trim(), txtcalpremdes.Text.Trim(), appwait, dateAppwait, cmbunderwrite.SelectedValue.ToString().Trim(), forwardid, receive_case, cmbundtype.SelectedValue.ToString().Trim(), txtsumsource.Text.Trim(), txtsumdest.Text.Trim(), lifesArray, ridersArray, sDateTime, eDateTime, txtregister.Text.Trim(), UserID, appreceive, chkdocnoret.Checked, "",sDocDateTime, eDocDateTime, "N", sStatusDateTime, eStatusDateTime, cmbapptype.SelectedValue.ToString(), cmbappgroup.SelectedValue.ToString(), cmbdmsscan.SelectedValue.ToString(), cmbremark.SelectedValue.ToString(), cmbsuspense.SelectedValue.ToString(), assignment,txtparentname.Text,txtparentsurname.Text,etcDoc,smartStatus,reasonSmart,cmbmarketingtype.SelectedValue.ToString(),renewalApp,"");

                    decimal sumPrem = 0;
                    decimal sumSumm = 0;

                    string listAppLock = "";
                    string checkDup = "";
                    if (pr.Successed == true)
                    {
                        if (appInProcs != null)
                        {
                            if (dtgCallReport != null)
                            {
                                //วนหา uapp_id ที่ถูก lock มา show user
                                //List<long> uapps = new List<long>();
                                //List<long> appLocks = new List<long>();
                                //foreach (NewBISSvcRef.APP_IN_PROCESS aip in appInProcs)
                                //{
                                //    uapps.Add((long)aip.UAPP_ID); 
                                //}
                                //string[] user = { UserID };

                                //pr = client.GetU_APPLICATION_LOCK(out uApplicationUpdates, uapps.ToArray(), null, null);
                                //    if (pr.Successed == true)
                                //    {
                                //        if (uApplicationUpdates != null)
                                //        {
                                //            for (int i = 0; i < uApplicationUpdates.Length; i++)
                                //            {
                                //                uApplicationUpdate = uApplicationUpdates[i];
                                //                if ((uApplicationUpdate.TRANSACTION_FLG == 'P' || uApplicationUpdate.TRANSACTION_FLG == 'U') && uApplicationUpdate.UPD_ID != UserID)  //ถ้า lock และ เป็น user อื่น, ใช้ != E ไม่ได้ มี null มาด้วย 
                                //                { 
                                //                    appLocks.Add((long)uApplicationUpdate.UAPP_ID); 
                                //                }
                                //            }
                                //        }
                                //    } 

                                //filter user lock ออก 
                                    //var FilterLockedItem = from x in appInProcs
                                    //    where x.UAPP_ID.Value != null && !(appLocks.Contains(x.UAPP_ID.Value))
                                    //    select x; 

                                //appInProcFilterLockedItem = FilterLockedItem.ToArray();

                                var appInProcessDistinct =
                                    (from x in appInProcs
                                     orderby x.APP_OFCRCV_DT,x.NEWBIS_DT //APP_OFCRCV_DT, NEWBIS_DT
                                     select x).Distinct();

                                NewBISSvcRef.APP_IN_PROCESS_COLLECION temp = new NewBISSvcRef.APP_IN_PROCESS_COLLECION();
                                string chkAppDup = "";
                                foreach (NewBISSvcRef.APP_IN_PROCESS x in appInProcessDistinct)
                                {
                                    if (x.APP_NO != chkAppDup)
                                    {
                                        chkAppDup = x.APP_NO;
                                        temp.Add(x);
                                    }
                                }
                                appInProcs = temp;

                                dtgCallReport.DataSource = appInProcs;//appInProcFilterLockedItem;
                                bindingDataGrid(client, ref sumPrem, ref sumSumm);
                            }
                        }

                        if (dtgCallReport.RowCount == 0)
                        {
                            //if (listAppLock.Trim() == "")
                          //  {
                               // MessageBox.Show("ไม่พบข้อมูลตามเงื่อนไขที่ท่านเลือก");
                                MessageBox.Show(new Form() { TopMost = true }, "ไม่พบข้อมูลตามเงื่อนไขที่ท่านเลือก");
                                chkall.Checked = false;
                           // }
                            txtsumm.Text = "";
                            txtsumapp.Text = "";
                            txtpremium.Text = "";
                        }
                    }
                    else
                    {
                       // MessageBox.Show(pr.ErrorMessage);
                        MessageBox.Show(new Form() { TopMost = true }, pr.ErrorMessage);
                    }

                }
                catch (Exception ex)
                {
                  //  MessageBox.Show(ex.Message);
                    MessageBox.Show(new Form() { TopMost = true }, ex.Message);
                }
                finally
                {
                    this.Cursor = Cursors.Default; 
                }
                if (client.State != System.ServiceModel.CommunicationState.Closed)
                {
                    client.Close();
                }
            }
        }

        private void bindingDataGrid(NewBISSvcRef.NewBISSvcClient client, ref decimal sumPrem, ref decimal sumSumm)
        {
            List<string> listCountAppNo = new List<string>();
            listCountAppNo.Clear();
            string checkAppnoDup = "";
            foreach (DataGridViewRow dr in dtgCallReport.Rows)
            {
                if (dr.DataBoundItem is NewBISSvcRef.APP_IN_PROCESS)
                {
                    NewBISSvcRef.APP_IN_PROCESS obj = (NewBISSvcRef.APP_IN_PROCESS)dr.DataBoundItem;
                    dr.Cells["clmlock"].Value = "N";
                    dr.Cells["clmlinkappno"].Value = obj.APP_NO;
                    dr.Cells["clmappno"].Value = obj.APP_NO;//obj.status.ToString() == "ยกเลิก" ? "N" : "Y";
                    dr.Cells["clmisis"].Value = obj.ISIS_STATUS;
                    dr.Cells["clmname"].Value = obj.CUSTNAME;
                    dr.Cells["clmsex"].Value = obj.SEX == null? "": obj.SEX.ToString() == "M" ? "ช" : "ญ"; //obj.SEX; 
                    dr.Cells["clmbirthdt"].Value = obj.BIRTH_DT==null || obj.BIRTH_DT == DateTime.MinValue ? "":String.Format("{0:dd/MM/yyyy}", obj.BIRTH_DT);
                    dr.Cells["clmpassport"].Value = obj.IDCARD; 
                    dr.Cells["clmnewbisdt"].Value = obj.NEWBIS_DT == null ? (DateTime?)null : obj.NEWBIS_DT;
                    dr.Cells["clmplan"].Value = obj.PLAN;
                    dr.Cells["clmsumm"].Value = obj.SUMM == 0 ? "0" : string.Format("{0:#,#}", obj.SUMM);
                    dr.Cells["clmpremium"].Value = obj.SUMM_PREMIUM == 0 ? "0" : string.Format("{0:#,#}", obj.SUMM_PREMIUM);
                    dr.Cells["clmstatus"].Value = obj.STATUS;
                    dr.Cells["clmsubstatus"].Value = obj.SUBSTATUS;
                    dr.Cells["clmmode"].Value = obj.P_MODE;
                    dr.Cells["clmisudt"].Value = obj.ISU_DT == null ? "" : String.Format("{0:dd/MM/yyyy}", obj.ISU_DT); // String.Format("{0:dd/MM/yyyy}", obj.ISU_DT);
                    dr.Cells["clmunderwriting"].Value = obj.UNDERWRITE_TYPE;
                    dr.Cells["clmcalprem"].Value = obj.CALPREM == 0 ? "0" : string.Format("{0:#,#}", obj.CALPREM);
                    dr.Cells["clmagent"].Value = obj.SALE_AGENT;
                    dr.Cells["clmunderwriter"].Value = obj.UND_ID;
                    dr.Cells["clmuappid"].Value = obj.UAPP_ID;
                    dr.Cells["clmappid"].Value = obj.APP_ID;
                    dr.Cells["clmclear"].Value = "Y";
                    dr.Cells["clmappofcdt"].Value = obj.APP_OFCRCV_DT == null ? (DateTime?)null : obj.APP_OFCRCV_DT;
                    dr.Cells["clmsmart"].Value = obj.FOLLOW_REASON;
                    dr.Cells["clmreceivedt"].Value = obj.LAST_RECEIVE_DT=="" || obj.LAST_RECEIVE_DT==null? "": obj.LAST_RECEIVE_DT + " " + obj.MEMO_REMAIN;
                    listCountAppNo.Add(obj.APP_NO); // for count appno

                    if (checkAppnoDup != obj.APP_NO)
                    {
                        checkAppnoDup = obj.APP_NO;
                        sumPrem += (decimal)obj.SUMM_PREMIUM;
                        sumSumm += (decimal)obj.SUMM;
                    }

                    //long[] uappid = { (long)obj.UAPP_ID };
                    //string[] user = { UserID };
                    //pr4 = client.GetU_APPLICATION_LOCK(out uApplicationUpdates, uappid, null, user);

                    //if (pr4.Successed == true)
                    //{
                    //    if (uApplicationUpdates != null)  //check เผื่อๆ แต่ไม่น่าเข้าอยู่แล้ว มักจะมาเป็น y
                    //    {
                    //        for (int i = 0; i < uApplicationUpdates.Length; i++)
                    //        {
                    //            uApplicationUpdate = uApplicationUpdates[i];
                    //            if (uApplicationUpdate.UPDATE_FLG == 'N' && uApplicationUpdate.TRANSACTION_FLG == 'E')
                    //            {
                    //                uApplicationUpdate.UAPP_ID = obj.UAPP_ID;
                    //                uApplicationUpdate.FSYSTEM_DT = DateTime.Now;
                    //                uApplicationUpdate.UPD_ID = UserID;
                    //                uApplicationUpdate.TRANSACTION_FLG = 'P';
                    //                uApplicationUpdate.UPDATE_FLG = 'N';
                    //                uApplicationUpdate.UPDATE_DT = DateTime.Now;
                    //                client.EditU_APPLICATION_LOCK(ref uApplicationUpdate); 
                    //            }
                    //            else if (uApplicationUpdate.UPDATE_FLG == 'Y' && uApplicationUpdate.TRANSACTION_FLG == 'E')  //เจอ แต่เป็นแถวที่เคย update มาแล้ว ก็ add ใหม่ สถานะ N
                    //            {
                    //                uApplicationUpdate.UAPP_ID = obj.UAPP_ID;
                    //                uApplicationUpdate.FSYSTEM_DT = DateTime.Now;
                    //                uApplicationUpdate.UPD_ID = UserID;
                    //                uApplicationUpdate.TRANSACTION_FLG = 'P';
                    //                uApplicationUpdate.UPDATE_FLG = 'N';
                    //                uApplicationUpdate.UPDATE_DT = DateTime.Now;
                    //                client.AddU_APPLICATION_LOCK(ref uApplicationUpdate); 
                    //            }
                    //        }
                    //    }
                    //    else   //กรณี record ใหม่ lock โลด
                    //    {
                    //        uApplicationUpdate.UAPP_ID = obj.UAPP_ID;
                    //        uApplicationUpdate.FSYSTEM_DT = DateTime.Now;
                    //        uApplicationUpdate.UPD_ID = UserID;
                    //        uApplicationUpdate.TRANSACTION_FLG = 'P';
                    //        uApplicationUpdate.UPDATE_FLG = 'N';
                    //        uApplicationUpdate.UPDATE_DT = DateTime.Now;
                    //        client.AddU_APPLICATION_LOCK(ref uApplicationUpdate); 
                    //    }
                    //}
                    //else
                    //{
                    //    MessageBox.Show(pr.ErrorMessage);
                    //}
                  //  chkall.Checked = true;
                }
            }
            //lblcount.Visible = false;
            //lblcount.Text = "จำนวน " + dtgCallReport.RowCount + " Records";
            txtsumm.Text = string.Format("{0:#,#}", sumSumm);
            txtpremium.Text = string.Format("{0:#,#}", sumPrem);
            // txtsumapp.Text = string.Format("{0:#,#}", dtgCallReport.RowCount);
            List<string> listAppnoDistinct = new List<string>();
            listAppnoDistinct = listCountAppNo.Distinct().ToList();
            txtsumapp.Text = listAppnoDistinct.Count().ToString();// +" ค่าเดิม " + string.Format("{0:#,#}", dtgCallReport.RowCount);
        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                if (dtgCallReport.RowCount > 0)
                {
                    string condition = getCondition();
                    string userName = getUserName(UserID);

                    ReportForm.frmReportAppList repAppList = new ReportForm.frmReportAppList(appInProcs.ToArray(), condition, userName, txtsumapp.Text, txtsumm.Text, txtpremium.Text);
                    repAppList.Show();
                }
                else
                {
                    MessageBox.Show(new Form() { TopMost = true }, "ไม่มีข้อมูลที่จะต้องพิมพ์");
                }
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private string getUserName(string _userid)
        {
            CenterSvcRef.User user;
            string userName = "";
            CenterSvcRef.ProcessResult prcenter = new CenterSvcRef.ProcessResult();
            using (CenterSvcRef.CenterServiceClient client = new CenterSvcRef.CenterServiceClient())
            {
                user = client.getUser(out prcenter, _userid);

                if (client.State != System.ServiceModel.CommunicationState.Closed)
                {
                    client.Close();
                }
            }
            if (user != null)
                userName = user.firstName + "  " + user.lastName;
            return userName;
        }

        private string getCondition()
        {
            string condition = "";
            if (txtappsource.Text != "" && txtappdest.Text != "")
            {
                if (txtappsource.Text == txtappdest.Text)
                {
                    condition = "เลขที่ใบคำขอ : " + txtappsource.Text + "\r\n";
                }
                else
                {
                    condition = "เลขที่ใบคำขอ : " + txtappsource.Text + " - " + txtappdest.Text + "\r\n";
                }
            }

            if (txtname.Text != "" && txtsurname.Text != "")
            {
                condition += "ชื่อ " + txtname.Text + " สกุล  " + txtsurname.Text + "\r\n";
            }
            else if (txtname.Text != "" && txtsurname.Text == "")
            {
                condition += "ชื่อ " + txtname.Text + " สกุล -" + "\r\n";
            }
            else if (txtname.Text == "" && txtsurname.Text != "")
            {
                condition += "ชื่อ - สกุล " + txtsurname.Text + "\r\n";
            }

            if (txtidcard.Text != "")
            {
                condition += "บัตรประชาชน : " + txtidcard.Text + "\r\n";
            }

            if (cmbchannel.SelectedValue.ToString() != "")
            {
                condition += "ช่องทางการขาย : " + cmbchannel.Text + "\r\n";
            }

            if (cmbworkgroup.SelectedValue.ToString() != "")
            {
                condition += "กลุ่มปฏิบัติงาน  : " + cmbworkgroup.Text + "\r\n";
            }

            if (txtappofc.Text != "")
            {
                condition += "สาขาส่งใบคำขอ : " + txtappofc.Text + "\r\n";
            }

            if (cmbstatus.SelectedValue.ToString() != "ทั้งหมด")
            {
                condition += "สถานะ : " + cmbstatus.SelectedValue.ToString();
                if (cmbsubstatus.SelectedValue.ToString() != "ทั้งหมด")
                {
                    condition += " สถานะย่อย : " + cmbsubstatus.SelectedValue.ToString() + "\r\n";
                }
                else
                {
                    condition += "\r\n";
                }
            }

            if (txtcalpremsrc.Text != "" && txtcalpremdes.Text != "")
            {
                if (txtcalpremsrc.Text == txtcalpremdes.Text)
                {
                    condition = "เงินในบัญชีพัก : " + txtcalpremsrc.Text + "\r\n";
                }
                else
                {
                    condition += "ช่วงเงินในบัญชีพัก : " + txtcalpremsrc.Text + "-" + txtcalpremdes.Text + "\r\n";
                }
            }

            if (cmbunderwrite.SelectedValue.ToString() != "")
            {
                condition += "ผู้พิจารณารับประกัน : " + cmbunderwrite.SelectedValue.ToString() + "\r\n";
            }

            if (cmbundtype.SelectedValue.ToString() != "")
            {
                condition += "ประเภทการพิจารณา : " + cmbundtype.Text + "\r\n";
            }

            if (txtsumsource.Text != "" && txtsumdest.Text != "")
            {
                if (txtsumsource.Text == txtsumdest.Text)
                {
                    condition += "ทุนประกัน : " + txtsumsource.Text + "\r\n";
                }
                else
                {
                    condition += "ช่วงทุนประกัน : " + txtsumsource.Text + "-" + txtsumdest.Text + "\r\n";
                }
            }

            if (cmbplan.Text != "ทั้งหมด")
            {
                condition += "แบบประกัน : " + cmbplan.Text.Replace('&', ',').Replace("\"", "") + "\r\n";
            }

            if (cmbrider.Text != "ทั้งหมด")
            {
                condition += "สัญญาพิเศษ : " + cmbrider.Text + "\r\n";
            }

            if (txtdatebegin.Text != "" && txtdateend.Text != "")
            {
                condition += "ช่วงวันที่บันทึกใบคำขอ : " + String.Format("{0:dd/MM/yyyy}", txtdatebegin.Text) + " " + txthrbegin.Text + ":" + txtminbegin.Text + " - " + String.Format("{0:dd/MM/yyyy}", txtdateend.Text) + " " + txthrend.Text + ":" + txtminend.Text + "\r\n";
            }

            if (txtregister.Text != "")
            {
                condition += "รหัสผู้บันทึกใบคำขอ : " + txtregister.Text + "\r\n";
            }

            if (chknotund.Checked == true)
            {
                if (cmbappwait.SelectedValue.ToString() == "")
                {
                    condition += "เป็นใบคำขอที่ยังไม่ดำเนินการตามที่ขอ วันที่รับค่า " + txtdateappwait.Text + "\r\n";
                }
                else
                {
                    condition += "เป็นใบคำขอที่ยังไม่ดำเนินการตามที่ขอ สถานะ : " + cmbappwait.SelectedValue.ToString() + " วันที่รับค่า " + txtdateappwait.Text + "\r\n";
                }
            }

            if (chkforward.Checked == true)
            {
                if (cmbappforward.SelectedValue.ToString() == "" && cmbundstep.SelectedValue.ToString() == "")
                {
                    condition += "เป็นใบคำขอที่ถูกส่งต่อการพิจารณา \r\n";
                }
                else if (cmbappforward.SelectedValue.ToString() != "" && cmbundstep.SelectedValue.ToString() != "")
                {
                    condition += "เป็นใบคำขอที่ถูกส่งต่อการพิจารณา รหัสผู้ถูกส่งต่อ : " + cmbappforward.SelectedValue.ToString() + " ขั้นตอนการพิจารณา : " + cmbundstep.Text + "\r\n";
                }
                else if (cmbappforward.SelectedValue.ToString() != "" && cmbundstep.SelectedValue.ToString() == "")
                {
                    condition += "เป็นใบคำขอที่ถูกส่งต่อการพิจารณา รหัสผู้ถูกส่งต่อ : " + cmbappforward.SelectedValue.ToString() + "\r\n";
                }
                else if (cmbappforward.SelectedValue.ToString() == "" && cmbundstep.SelectedValue.ToString() != "")
                {
                    condition += "เป็นใบคำขอที่ถูกส่งต่อการพิจารณา ขั้นตอนการพิจารณา : " + cmbundstep.Text + "\r\n";
                }
            }

            if (chkdocnoret.Checked == true)
            {
                condition += "เป็นใบคำขอที่มีการขอเอกสารเพิ่มเติม แต่ยังไม่มีการตอบกลับ \r\n";
            }
            else if (chkdocret.Checked == true)
            {
                condition += "เป็นใบคำขอที่มีการขอเอกสารเพิ่มเติม มีการตอบกลับในช่วงวันที่ : " + String.Format("{0:dd/MM/yyyy}", txtsddoc.Text) + " " + txtshdoc.Text + ":" + txtsmdoc.Text + " - " + String.Format("{0:dd/MM/yyyy}", txteddoc.Text) + " " + txtehdoc.Text + ":" + txtemdoc.Text + "\r\n";
            }

            if (cmbsuspense.SelectedValue == "N")
            {
                condition += "ยังไม่ชำระเบี้ย" + "\r\n";
            }
            else if (cmbsuspense.SelectedValue == "Y")
            {
                condition += "ชำระเบี้ยครบ" + "\r\n";
            }
            else if (cmbsuspense.SelectedValue == "W")
            {
                condition += "ชำระเบี้ยขาด" + "\r\n";
            }

            if (chketc.Checked == true)
            { 
                condition += "เป็นใบคำขอที่มีแสกนเอกสารอื่น ๆ แสกนเข้ามาเพิ่ม \r\n";
            }

            if(cmbsmart.SelectedValue != "")
            {
                if(cmbsmart.SelectedValue == "Y") //อนุมัติ
                {
                    condition += "มีบัญชีสมาร์ทที่ได้รับการอนุมัติ \r\n";
                }
                else if(cmbsmart.SelectedValue == "N")
                {
                    condition += "มีบัญชีสมาร์ทที่ไม่ได้รับการอนุมัติ ";
                     if(cmbsmartreason.SelectedValue != "")
                        {
                            condition += "ด้วยสาเหตุ " + cmbsmartreason.Text + "\r\n";
                        }   
                }
            }
            return condition;
        }

        private void newJobToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("ท่านต้องการล้างใบคำขอและเงื่อนไขที่เลือกไว้?", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
            { 
                    UnlockTransaction();
                    // lblcount.Visible = false;
                    //txtdatebegin.Text = DateTime.Now.Day.ToString() + "/" + DateTime.Now.Month.ToString() + "/" + DateTime.Now.ToString("yyyy", System.Globalization.CultureInfo.GetCultureInfo("th-TH"));
                    //txtdateend.Text = DateTime.Now.Day.ToString() + "/" + DateTime.Now.Month.ToString() + "/" + DateTime.Now.ToString("yyyy", System.Globalization.CultureInfo.GetCultureInfo("th-TH"));
                    //txthrbegin.Text = "00"; //DateTime.Now.Hour.ToString().Length == 1 ? "0" + DateTime.Now.Hour.ToString() : DateTime.Now.Hour.ToString();
                    //txthrend.Text = DateTime.Now.Hour.ToString().Length == 1 ? "0" + DateTime.Now.Hour.ToString() : DateTime.Now.Hour.ToString();
                    //txtminbegin.Text = "00"; //DateTime.Now.Minute.ToString().Length == 1 ? "0" + DateTime.Now.Minute.ToString() : DateTime.Now.Minute.ToString();
                    //txtminend.Text = DateTime.Now.Minute.ToString().Length == 1 ? "0" + DateTime.Now.Minute.ToString() : DateTime.Now.Minute.ToString();
                 
                    //txtstatusdtbegin.Text = DateTime.Now.Day.ToString() + "/" + DateTime.Now.Month.ToString() + "/" + DateTime.Now.ToString("yyyy", System.Globalization.CultureInfo.GetCultureInfo("th-TH"));
                    //txtstatusdtend.Text = DateTime.Now.Day.ToString() + "/" + DateTime.Now.Month.ToString() + "/" + DateTime.Now.ToString("yyyy", System.Globalization.CultureInfo.GetCultureInfo("th-TH"));
                    //txtstatushrbegin.Text = "00";
                    //txtstatushrend.Text = DateTime.Now.Hour.ToString().Length == 1 ? "0" + DateTime.Now.Hour.ToString() : DateTime.Now.Hour.ToString(); ;
                    //txtstatusmnbegin.Text = "00";
                    //txtstatusmnend.Text = DateTime.Now.Minute.ToString().Length == 1 ? "0" + DateTime.Now.Minute.ToString() : DateTime.Now.Minute.ToString();
                 
                    txtdatebegin.Text = ""; 
                    txtdateend.Text = ""; 
                    txthrbegin.Text = ""; 
                    txthrend.Text = ""; 
                    txtminbegin.Text = ""; 
                    txtminend.Text = ""; 

                    txtstatusdtbegin.Text = ""; 
                    txtstatusdtend.Text = ""; 
                    txtstatushrbegin.Text = ""; 
                    txtstatushrend.Text = ""; 
                    txtstatusmnbegin.Text = "";
                    txtstatusmnend.Text = ""; 

                    txtappsource.Text = "";
                    txtappdest.Text = "";
                    txtname.Text = "";
                    txtsurname.Text = "";
                    txtidcard.Text = "";
                    cmbchannel.SelectedValue = "OD";
                    cmbworkgroup.SelectedValue = "";
                    txtappofc.Text = "";
                    cmbstatus.SelectedValue = "ทั้งหมด";
                    cmbsubstatus.SelectedValue = "ทั้งหมด";
                    txtcalpremsrc.Text = "";
                    txtcalpremdes.Text = "";
                    cmbunderwrite.SelectedValue = "";
                    cmbundtype.SelectedValue = "";
                    txtsumsource.Text = "";
                    txtsumdest.Text = "";
                    cmbplan.SelectedValue = "";
                    cmbrider.SelectedValue = "";
                    txtregister.Text = "";
                    chknotund.Checked = false;
                    chkforward.Checked = false;
                    cmbappforward.SelectedValue = "";
                    cmbundstep.SelectedValue = "";
                    txtsumm.Text = "";
                    txtpremium.Text = "";
                    dtgCallReport.DataSource = null;
                    txtsumapp.Text = "";
                    txtappsource.Focus();
                    cmbappwait.SelectedValue = "";
                    txtdateappwait.Text = txtdateappwait.Text = DateTime.Now.Day.ToString() + "/" + DateTime.Now.Month.ToString() + "/" + DateTime.Now.ToString("yyyy", System.Globalization.CultureInfo.GetCultureInfo("th-TH"));
                    chkall.Checked = false;
                    cmbsuspense.SelectedValue = "";
                    cmbremark.SelectedValue = "";
                    cmbdmsscan.SelectedValue = "";
                    txtparentname.Text = "";
                    txtparentsurname.Text = "";

                    //chkass.Checked = false;
                    chketc.Checked = false;

                    cmbsmart.SelectedValue = "";
                    pnlsmart.Visible = false;

                    getRemainWork();
                    flgChk = "1";
            }
        }

        private void chkforward_CheckedChanged(object sender, EventArgs e)
        {
            if (chkforward.Checked == true)
            {
                cmbappforward.Enabled = true;
                cmbundstep.Enabled = true;
                cmbappgroup.Enabled = true;

                //cmbstatus.SelectedValue = "ทั้งหมด";
                //cmbstatus.Enabled = false;
                //cmbsubstatus.Enabled = false;
                cmbstatus.Enabled = true;
                cmbsubstatus.Enabled = true;

                using (NewBISSvcRef.NewBISSvcClient client = new NewBISSvcRef.NewBISSvcClient())
                {
                    bindingUnderwriterForward(client);
                    bindingUnderwriteStep();
                    bindingAppGroup();

                    if (client.State != System.ServiceModel.CommunicationState.Closed)
                    {
                        client.Close();
                    }
                }
            }
            else
            {
                cmbappforward.Enabled = false;
                cmbappforward.SelectedValue = "";
                cmbundstep.Enabled = false;
                cmbundstep.SelectedValue = "";
                cmbappgroup.Enabled = false;
                cmbappgroup.SelectedValue = "";

                cmbstatus.Enabled = true;
                cmbsubstatus.Enabled = true;
            }
        }

        private void txtcalpremsrc_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter && txtcalpremsrc.Text.Trim() != "")
            {
                txtcalpremdes.Focus();
            }
            else if ((e.KeyChar < '0' || e.KeyChar > '9') && (e.KeyChar != '\b'))
            {
                e.Handled = true;
                return;
            }
        }

        private void txtcalpremdes_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < '0' || e.KeyChar > '9') && (e.KeyChar != '\b'))
            {
                e.Handled = true;
                return;
            }
        }

        private void txtsumsource_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter && txtsumsource.Text.Trim() != "")
            {
                txtsumdest.Focus();
            }
            else if ((e.KeyChar < '0' || e.KeyChar > '9') && (e.KeyChar != '\b'))
            {
                e.Handled = true;
                return;
            }
        }

        private void txtsumdest_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < '0' || e.KeyChar > '9') && (e.KeyChar != '\b'))
            {
                e.Handled = true;
                return;
            }
        }

        private void txtidcard_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < '0' || e.KeyChar > '9') && (e.KeyChar != '\b'))
            {
                e.Handled = true;
                return;
            }
        }

        private void getRemainWork()
        {
            try
            {
                List<long> uappIDArray = new List<long>();
                uappIDArray.Clear();
                dtgCallReport.DataSource = null;
                dtgCallReport.AutoGenerateColumns = false;

                using (NewBISSvcRef.NewBISSvcClient client = new NewBISSvcRef.NewBISSvcClient())
                {
                    string[] userids = { UserID };
                    //pr = client.GetU_APPLICATION_LOCK(out uApplicationUpdates, null, null, userids);
                    pr = client.GetUapplicationLock(out uApplicationUpdates, userids);
                    if (pr.Successed == true)
                    {
                        if (uApplicationUpdates != null)
                        {
                            for (int i = 0; i < uApplicationUpdates.Length; i++)
                            {
                                uApplicationUpdate = uApplicationUpdates[i];
                                if (uApplicationUpdate.TRANSACTION_FLG != 'E')
                                {
                                    uappIDArray.Add((long)uApplicationUpdate.UAPP_ID);
                                }
                            }
                        }
                    }

                    if (uappIDArray.Count != 0)
                    {
                        long[] uappid = uappIDArray.ToArray();
                        appInProcs = client.GetAppInProcessByUappID(out pr, uappid, UserID);

                        decimal sumPrem = 0;
                        decimal sumSumm = 0;
                      
                        if (pr.Successed == true)
                        { 
                            if (appInProcs != null && appInProcs.Count() > 0)
                            {
                                var appInProcessDistinct =
                                 (from x in appInProcs
                                  orderby x.APP_OFCRCV_DT, x.NEWBIS_DT //APP_OFCRCV_DT, NEWBIS_DT
                                  select x).Distinct();

                                NewBISSvcRef.APP_IN_PROCESS_COLLECION temp = new NewBISSvcRef.APP_IN_PROCESS_COLLECION();
                                string chkAppDup = "";
                                foreach (NewBISSvcRef.APP_IN_PROCESS x in appInProcessDistinct)
                                {
                                    if (x.APP_NO != chkAppDup)
                                    {
                                        chkAppDup = x.APP_NO;
                                        temp.Add(x);
                                    }
                                }
                                appInProcs = temp;
                                dtgCallReport.DataSource = appInProcs;
                                bindingDataGrid(client, ref sumPrem, ref sumSumm);
                                chkall.Checked = true;
                            } 
                        }
                        else
                        {
                           // MessageBox.Show(pr.ErrorMessage);
                            MessageBox.Show(new Form() { TopMost = true }, pr.ErrorMessage);
                        }
                    }

                    if (client.State != System.ServiceModel.CommunicationState.Closed)
                    {
                        client.Close();
                    }
                }
            }
            catch (Exception ex)
            {
               // throw ex;
                MessageBox.Show(ex.ToString());
            }
        }

        private void FormApplicationSelector_FormClosing(object sender, FormClosingEventArgs e)
        {
            UnlockTransaction();

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
                    if (filePath.StartsWith("C:\\WINDOWS\\Temp\\NewbizReport\\reportAppListFinalPDF"))
                    {
                        File.Delete(filePath);
                    }
                }
            }
            catch (Exception ex)
            { }
        }

        private void cmbrider_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbrider.Text == "")
                cmbrider.Text = "ทั้งหมด";
        }

        private void cmbplan_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbplan.Text == "")
                cmbplan.Text = "ทั้งหมด";
        }

        private void txtregister_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < '0' || e.KeyChar > '9') && (e.KeyChar != '\b'))
            {
                e.Handled = true;
                return;
            }
        }

        private void dtgCallReport_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex != dtgCallReport.Columns["clmlinkappno"].Index && e.ColumnIndex != dtgCallReport.Columns["clmlock"].Index)
            {
                return;
            }
            else if (e.ColumnIndex == dtgCallReport.Columns["clmlock"].Index)
            {
                NewBISSvcRef.U_APPLICATION_LOCK ual = new NewBISSvcRef.U_APPLICATION_LOCK();
                NewBISSvcRef.ProcessResult prX = new NewBISSvcRef.ProcessResult();
                ual = null;
                DataGridViewCheckBoxCell ch1 = new DataGridViewCheckBoxCell();
                ch1 = (DataGridViewCheckBoxCell)dtgCallReport.Rows[dtgCallReport.CurrentRow.Index].Cells[0];

                if (ch1.Value == null)
                    ch1.Value = "T";
                switch (ch1.Value.ToString())
                {
                    case "Y":  //unchecked
                        ch1.Value = "N";
                        {
                            using (NewBISSvcRef.NewBISSvcClient client = new NewBISSvcRef.NewBISSvcClient())
                            { 
                                long[] uappids = { (long)dtgCallReport["clmuappid", e.RowIndex].Value };
                                string[] updids = { UserID };

                                ual = client.CheckDupLock(out prX, (long?)dtgCallReport["clmappid", e.RowIndex].Value);

                                if (ual != null && ual.TRANSACTION_FLG == 'U')
                                {
                                    dtgCallReport["clmclear", e.RowIndex].Value = "N"; 
                                    MessageBox.Show("ใบคำขอนี้กำลังถูกพิจารณาโดบ คุณ" + getUserName(UserID));
                                }
                                else
                                {
                                    client.EditLock("E", uappids, "Y", updids, DateTime.Now); //แก้บน service ให้ edit 2 uapp_id แล้ว
                                    client.RemoveLock(uappids, "N", updids);
                                }

                                if (client.State != System.ServiceModel.CommunicationState.Closed)
                                {
                                    client.Close();
                                }
                            }
                        } 
                        break;
                    case "N":  //checked
                        ch1.Value = "Y";
                        
                        {
                            using (NewBISSvcRef.NewBISSvcClient client = new NewBISSvcRef.NewBISSvcClient())
                            {
                                string appLockDetail = "";
                                long[] appLock = null;
                                long[] uappids = { (long)dtgCallReport["clmuappid", e.RowIndex].Value };
                                client.AddLock(out appLockDetail, out appLock, uappids, 'N', UserID, 'P');

                                if (appLock != null)
                                {
                                    dtgCallReport["clmclear", e.RowIndex].Value = "N"; 
                                    MessageBox.Show(new Form() { TopMost = true }, appLockDetail);
                                } 
                                if (client.State != System.ServiceModel.CommunicationState.Closed)
                                {
                                    client.Close();
                                }
                            }
                        }
                        break;
                }
                 
                int cntCheck = 0;

                if (dtgCallReport.RowCount > 0)
                {
                    dtgCallReport.EndEdit();

                    foreach (DataGridViewRow row in dtgCallReport.Rows)
                    {
                        if (row.Cells["clmlock"].Value == "Y") //check
                        {
                            cntCheck++;
                        }
                    }

                    if (cntCheck == dtgCallReport.RowCount)
                    {
                        chkall.Checked = true;
                    }
                    else
                    { 
                        flgChk = "0";
                        chkall.Checked = false;
                    }
                }
            }
        }

        private void txtdatebegin_Leave(object sender, EventArgs e)
        {
            ChkDateForTextBox(txtdatebegin.Text, "วันที่บันทึกใบคำขอ", txtdatebegin);
        }

        private void ChkDateForTextBox(String strDate, String msg, Control textBox)
        {
            if (strDate != "")
            {
                DateTime? d = Utility.StringToDateTime(strDate, "BU");
                if (d == null)
                {
                    textBox.Text = "";
                    //MessageBox.Show("รูปแบบวันที่ " + msg + " ไม่ถูกต้อง"); 
                    MessageBox.Show(new Form() { TopMost = true }, "รูปแบบวันที่ " + msg + " ไม่ถูกต้อง");
                    textBox.Focus();
                    return;
                }
            }
        }

        private void txtdateend_Leave(object sender, EventArgs e)
        {
            ChkDateForTextBox(txtdateend.Text, "วันที่บันทึกใบคำขอ", txtdateend);
        }

        private void txtminbegin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < '0' || e.KeyChar > '9') && (e.KeyChar != '\b'))
            {
                e.Handled = true;
                return;
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

        private void txthrbegin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < '0' || e.KeyChar > '9') && (e.KeyChar != '\b'))
            {
                e.Handled = true;
                return;
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

        private void txthrbegin_Leave(object sender, EventArgs e)
        {
            if (txthrbegin.Text != "")
            {
                if (Convert.ToInt32(txthrbegin.Text) > 24)
                {
                    // MessageBox.Show("กรอกเวลาไม่ถูกต้อง");
                    MessageBox.Show(new Form() { TopMost = true }, "กรอกเวลาไม่ถูกต้อง");
                    txthrbegin.Focus();
                }
            }
        }

        private void txthrend_Leave(object sender, EventArgs e)
        {
            if (txthrend.Text != "")
            {
                if (Convert.ToInt32(txthrend.Text) > 24)
                {
                    // MessageBox.Show("กรอกเวลาไม่ถูกต้อง");
                    MessageBox.Show(new Form() { TopMost = true }, "กรอกเวลาไม่ถูกต้อง");
                    txthrend.Focus();
                }
            }
        }

        private void txtminbegin_Leave(object sender, EventArgs e)
        {
            if (txtminbegin.Text != "")
            {
                if (Convert.ToInt32(txtminbegin.Text) > 60)
                {
                    // MessageBox.Show("กรอกเวลาไม่ถูกต้อง");
                    MessageBox.Show(new Form() { TopMost = true }, "กรอกเวลาไม่ถูกต้อง");
                    txtminbegin.Focus();
                }
            }
        }

        private void txtminend_Leave(object sender, EventArgs e)
        {
            if (txtminend.Text != "")
            {
                if (Convert.ToInt32(txtminend.Text) > 60)
                {
                   // MessageBox.Show("กรอกเวลาไม่ถูกต้อง");
                    MessageBox.Show(new Form() { TopMost = true }, "กรอกเวลาไม่ถูกต้อง");
                    txtminend.Focus();
                }
            }
        }

        private void txtdateappwait_Leave(object sender, EventArgs e)
        {
            ChkDateForTextBox(txtdateappwait.Text, "วันที่รับค่า", txtdateappwait);
        }

        private void chkreceive_CheckedChanged(object sender, EventArgs e)
        {
            if (chkreceive.Checked == true)
            {
                chkdocret.Checked = false;
                chkdocnoret.Checked = false;
                chknotund.Checked = false;
                //cmbstatus.SelectedValue = "ทั้งหมด";
                //cmbstatus.Enabled = false;
                //cmbsubstatus.Enabled = false;
                cmbstatus.Enabled = true;
                cmbsubstatus.Enabled = true;
            }
            else
            {
                cmbstatus.Enabled = true;
                cmbsubstatus.Enabled = true;
            }
        }

        private void chkall_CheckedChanged(object sender, EventArgs e)
        {
            if (chkall.Checked == true)
            { 
                    LockAllTransaction(); 
            }
            else
            {
                if (flgChk != "0") // 0 = check ผ่าน child
                {
                    flgChk = "1"; //check ผ่าน header checkbox
                    if (flgChk == "1")
                    {
                        UnlockAllTransaction();
                    }
                }

                flgChk = "1";
            }
        }

        private void UnlockAllTransaction()
        {
            NewBISSvcRef.ProcessResult prx = new NewBISSvcRef.ProcessResult();
            NewBISSvcRef.U_APPLICATION_LOCK ual = new NewBISSvcRef.U_APPLICATION_LOCK();
            ual = null;

            using (NewBISSvcRef.NewBISSvcClient c = new NewBISSvcRef.NewBISSvcClient())
            { 
                if (dtgCallReport.RowCount > 0)
                {
                    dtgCallReport.EndEdit();
                    List<long> uapp_ids = new List<long>(); 
                    foreach (DataGridViewRow row in dtgCallReport.Rows)
                    {
                        ual = c.CheckDupLock(out prx, (long?)row.Cells["clmappid"].Value);

                        if (ual != null && ual.TRANSACTION_FLG == 'U')
                        { }
                        else
                        {
                            uapp_ids.Add((long)row.Cells["clmuappid"].Value);
                            row.Cells["clmlock"].Value = "N";
                        }
                        
                    }
                    string[] upd_ids = { UserID };
                    chkall.Checked = false;
                    if (uapp_ids != null && uapp_ids.Count() > 0)
                    {
                        c.EditLock("E", uapp_ids.ToArray(), "Y", upd_ids.ToArray(), DateTime.Now);
                        c.RemoveLock(uapp_ids.ToArray(), "N", upd_ids);
                    }
                }

                if (c.State != System.ServiceModel.CommunicationState.Closed)
                {
                    c.Close();
                } 
            }
        }

        private void LockAllTransaction()
        {
            using (NewBISSvcRef.NewBISSvcClient client = new NewBISSvcRef.NewBISSvcClient())
            {
                //loop datagrid for check & lock all (if check ก็ข้ามไปแสดงว่ามัน lock อย่แล้ว)
                if (dtgCallReport.RowCount > 0)
                {           //delete row ที่ user ไม่มีการ update
                    dtgCallReport.EndEdit();
                    List<long> uappids = new List<long>();
                    foreach (DataGridViewRow row in dtgCallReport.Rows)
                    {
                        if (row.Cells["clmlock"].Value == "N")
                        {
                            row.Cells["clmlock"].Value = "Y";
                            uappids.Add((long)row.Cells["clmuappid"].Value);
                            //uApplicationUpdate.UAPP_ID = (long)row.Cells["clmuappid"].Value; 
                            //uApplicationUpdate.FSYSTEM_DT = DateTime.Now;
                            //uApplicationUpdate.UPD_ID = UserID;
                            //uApplicationUpdate.TRANSACTION_FLG = 'P';
                            //uApplicationUpdate.UPDATE_FLG = 'N';
                            //uApplicationUpdate.UPDATE_DT = DateTime.Now;
                            //client.AddU_APPLICATION_LOCK(ref uApplicationUpdate);
                        }
                    }
                    string appLockDetail = "";
                    long[] appLock = null;
                    client.AddLock(out appLockDetail, out appLock, uappids.ToArray(), 'N', UserID, 'P');

                    if (appLock != null)
                    {
                        foreach (DataGridViewRow row in dtgCallReport.Rows)
                        {
                            if (appLock.Contains((long)row.Cells["clmuappid"].Value))
                            {
                                row.Cells["clmlock"].Value = "N";
                            }
                        } 
                        MessageBox.Show(new Form() { TopMost = true }, "ใบคำขอต่อไปนี้ไม่สามารถ Lock ได้ " + "\r\n" + appLockDetail);
                    } 
                }
                if (client.State != System.ServiceModel.CommunicationState.Closed)
                {
                    client.Close();
                }
            }
        }

        private void chkdocnoret_CheckedChanged(object sender, EventArgs e)
        {
            if (chkdocnoret.Checked == true)
            {
                //disable ใบคำขอที่ยังไม่ดำเนินการตามที่ขอ - ใบคำขอที่มีการตอบรับและต้องมีการพิจารณารับประกัน
                chkreceive.Checked = false;
                chknotund.Checked = false;

                //มีการตอบกลับ
                chkdocret.Checked = false;

                //txtsddoc.Text = DateTime.Now.Day.ToString() + "/" + DateTime.Now.Month.ToString() + "/" + DateTime.Now.ToString("yyyy", System.Globalization.CultureInfo.GetCultureInfo("th-TH"));
                //txteddoc.Text = DateTime.Now.Day.ToString() + "/" + DateTime.Now.Month.ToString() + "/" + DateTime.Now.ToString("yyyy", System.Globalization.CultureInfo.GetCultureInfo("th-TH"));
                //txtshdoc.Text = DateTime.Now.Hour.ToString().Length == 1 ? "0" + DateTime.Now.Hour.ToString() : DateTime.Now.Hour.ToString();
                //txtehdoc.Text = DateTime.Now.Hour.ToString().Length == 1 ? "0" + DateTime.Now.Hour.ToString() : DateTime.Now.Hour.ToString(); ;
                //txtsmdoc.Text = DateTime.Now.Minute.ToString().Length == 1 ? "0" + DateTime.Now.Minute.ToString() : DateTime.Now.Minute.ToString();
                //txtemdoc.Text = DateTime.Now.Minute.ToString().Length == 1 ? "0" + DateTime.Now.Minute.ToString() : DateTime.Now.Minute.ToString();

                //txtsddoc.Enabled = false;
                //txteddoc.Enabled = false;
                //txtshdoc.Enabled = false;
                //txtehdoc.Enabled = false;
                //txtsmdoc.Enabled = false;
                //txtemdoc.Enabled = false; 
            } 
        }

        private void chkdocret_CheckedChanged(object sender, EventArgs e)
        {
            if (chkdocret.Checked == true)
            {
                //disable ใบคำขอที่ยังไม่ดำเนินการตามที่ขอ - ใบคำขอที่มีการตอบรับและต้องมีการพิจารณารับประกัน
                chkreceive.Checked = false;
                chknotund.Checked = false;

                chkdocnoret.Checked = false;

                txtsddoc.Text = DateTime.Now.Day.ToString() + "/" + DateTime.Now.Month.ToString() + "/" + DateTime.Now.ToString("yyyy", System.Globalization.CultureInfo.GetCultureInfo("th-TH"));
                txteddoc.Text = DateTime.Now.Day.ToString() + "/" + DateTime.Now.Month.ToString() + "/" + DateTime.Now.ToString("yyyy", System.Globalization.CultureInfo.GetCultureInfo("th-TH"));
                txtshdoc.Text = "00"; //DateTime.Now.Hour.ToString().Length == 1 ? "0" + DateTime.Now.Hour.ToString() : DateTime.Now.Hour.ToString();
                txtehdoc.Text = DateTime.Now.Hour.ToString().Length == 1 ? "0" + DateTime.Now.Hour.ToString() : DateTime.Now.Hour.ToString(); ;
                txtsmdoc.Text = "00"; //DateTime.Now.Minute.ToString().Length == 1 ? "0" + DateTime.Now.Minute.ToString() : DateTime.Now.Minute.ToString();
                txtemdoc.Text = DateTime.Now.Minute.ToString().Length == 1 ? "0" + DateTime.Now.Minute.ToString() : DateTime.Now.Minute.ToString();

                txtsddoc.Enabled = true;
                txteddoc.Enabled = true;
                txtshdoc.Enabled = true;
                txtehdoc.Enabled = true;
                txtsmdoc.Enabled = true;
                txtemdoc.Enabled = true;
            }
            else
            {
                txtsddoc.Text = DateTime.Now.Day.ToString() + "/" + DateTime.Now.Month.ToString() + "/" + DateTime.Now.ToString("yyyy", System.Globalization.CultureInfo.GetCultureInfo("th-TH"));
                txteddoc.Text = DateTime.Now.Day.ToString() + "/" + DateTime.Now.Month.ToString() + "/" + DateTime.Now.ToString("yyyy", System.Globalization.CultureInfo.GetCultureInfo("th-TH"));
                txtshdoc.Text = "00"; //DateTime.Now.Hour.ToString().Length == 1 ? "0" + DateTime.Now.Hour.ToString() : DateTime.Now.Hour.ToString();
                txtehdoc.Text = DateTime.Now.Hour.ToString().Length == 1 ? "0" + DateTime.Now.Hour.ToString() : DateTime.Now.Hour.ToString(); ;
                txtsmdoc.Text = "00"; //DateTime.Now.Minute.ToString().Length == 1 ? "0" + DateTime.Now.Minute.ToString() : DateTime.Now.Minute.ToString();
                txtemdoc.Text = DateTime.Now.Minute.ToString().Length == 1 ? "0" + DateTime.Now.Minute.ToString() : DateTime.Now.Minute.ToString();

                txtsddoc.Enabled = false;
                txteddoc.Enabled = false;
                txtshdoc.Enabled = false;
                txtehdoc.Enabled = false;
                txtsmdoc.Enabled = false;
                txtemdoc.Enabled = false;
            }
        }

        private void txtsddoc_Leave(object sender, EventArgs e)
        {
            ChkDateForTextBox(txtsddoc.Text, "วันที่ตอบกลับกรณีมีการขอเอกสารเพิ่มเติม", txtsddoc);
        }

        private void txteddoc_Leave(object sender, EventArgs e)
        {
            ChkDateForTextBox(txteddoc.Text, "วันที่ตอบกลับกรณีมีการขอเอกสารเพิ่มเติม", txteddoc);
        }

        private void txtshdoc_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < '0' || e.KeyChar > '9') && (e.KeyChar != '\b'))
            {
                e.Handled = true;
                return;
            }
        }

        private void txtehdoc_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < '0' || e.KeyChar > '9') && (e.KeyChar != '\b'))
            {
                e.Handled = true;
                return;
            }
        }

        private void txtsmdoc_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < '0' || e.KeyChar > '9') && (e.KeyChar != '\b'))
            {
                e.Handled = true;
                return;
            }
        }

        private void txtemdoc_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < '0' || e.KeyChar > '9') && (e.KeyChar != '\b'))
            {
                e.Handled = true;
                return;
            }
        }

        private void txtshdoc_Leave(object sender, EventArgs e)
        {
            if (txtshdoc.Text != "")
            {
                if (Convert.ToInt32(txtshdoc.Text) > 24)
                {
                    //  MessageBox.Show("กรอกเวลาไม่ถูกต้อง");
                    MessageBox.Show(new Form() { TopMost = true }, "กรอกเวลาไม่ถูกต้อง");
                    txtshdoc.Focus();
                }
            }
        }

        private void txtsmdoc_Leave(object sender, EventArgs e)
        {
            if (txtsmdoc.Text != "")
            {
                if (Convert.ToInt32(txtsmdoc.Text) > 60)
                {
                    //  MessageBox.Show("กรอกเวลาไม่ถูกต้อง");
                    MessageBox.Show(new Form() { TopMost = true }, "กรอกเวลาไม่ถูกต้อง");
                    txtsmdoc.Focus();
                }
            }
        }

        private void txtehdoc_Leave(object sender, EventArgs e)
        {
            if (txtehdoc.Text != "")
            {
                if (Convert.ToInt32(txtehdoc.Text) > 24)
                {
                    //  MessageBox.Show("กรอกเวลาไม่ถูกต้อง");
                    MessageBox.Show(new Form() { TopMost = true }, "กรอกเวลาไม่ถูกต้อง");
                    txtehdoc.Focus();
                }
            }
        }

        private void txtemdoc_Leave(object sender, EventArgs e)
        {
            if (txtemdoc.Text != "")
            {
                if (Convert.ToInt32(txtemdoc.Text) > 60)
                {
                    //  MessageBox.Show("กรอกเวลาไม่ถูกต้อง");
                    MessageBox.Show(new Form() { TopMost = true }, "กรอกเวลาไม่ถูกต้อง");
                    txtemdoc.Focus();
                }
            }
        }

        private void txtstatusdtbegin_Leave(object sender, EventArgs e)
        {
            ChkDateForTextBox(txtstatusdtbegin.Text, "วันที่บันทึกสถานะ", txtstatusdtbegin);
        }

        private void txtstatusdtend_Leave(object sender, EventArgs e)
        {
            ChkDateForTextBox(txtstatusdtend.Text, "วันที่บันทึกสถานะ", txtstatusdtend);
        }

        private void txtstatushrbegin_Leave(object sender, EventArgs e)
        {
            if (txtstatushrbegin.Text != "")
            {
                if (Convert.ToInt32(txtstatushrbegin.Text) > 24)
                {
                    //  MessageBox.Show("กรอกเวลาไม่ถูกต้อง");
                    MessageBox.Show(new Form() { TopMost = true }, "กรอกเวลาไม่ถูกต้อง");
                    txtstatushrbegin.Focus();
                }
            }
        }

        private void txtstatusmnbegin_Leave(object sender, EventArgs e)
        {
            if (txtstatusmnbegin.Text != "")
            {
                if (Convert.ToInt32(txtstatusmnbegin.Text) > 60)
                {
                    //  MessageBox.Show("กรอกเวลาไม่ถูกต้อง");
                    MessageBox.Show(new Form() { TopMost = true }, "กรอกเวลาไม่ถูกต้อง");
                    txtstatusmnbegin.Focus();
                }
            }
        }

        private void txtstatushrend_Leave(object sender, EventArgs e)
        {
            if (txtstatushrend.Text != "")
            {
                if (Convert.ToInt32(txtstatushrend.Text) > 24)
                {
                    //  MessageBox.Show("กรอกเวลาไม่ถูกต้อง");
                    MessageBox.Show(new Form() { TopMost = true }, "กรอกเวลาไม่ถูกต้อง");
                    txtstatushrend.Focus();
                }
            }
        }

        private void txtstatusmnend_Leave(object sender, EventArgs e)
        {
            if (txtstatusmnend.Text != "")
            {
                if (Convert.ToInt32(txtstatusmnend.Text) > 60)
                {
                  //  MessageBox.Show("กรอกเวลาไม่ถูกต้อง");
                    MessageBox.Show(new Form() { TopMost = true }, "กรอกเวลาไม่ถูกต้อง");
                    txtstatusmnend.Focus();
                }
            }
        }

        private void txtstatushrbegin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < '0' || e.KeyChar > '9') && (e.KeyChar != '\b'))
            {
                e.Handled = true;
                return;
            }
        }

        private void txtstatusmnbegin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < '0' || e.KeyChar > '9') && (e.KeyChar != '\b'))
            {
                e.Handled = true;
                return;
            }
        }

        private void txtstatushrend_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < '0' || e.KeyChar > '9') && (e.KeyChar != '\b'))
            {
                e.Handled = true;
                return;
            }
        }

        private void txtstatusmnend_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < '0' || e.KeyChar > '9') && (e.KeyChar != '\b'))
            {
                e.Handled = true;
                return;
            }
        }

        private void dtgCallReport_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
          if (e.RowIndex < 0 || e.ColumnIndex != dtgCallReport.Columns["clmlock"].Index)
            {
                return;
            }
          else if (e.ColumnIndex == dtgCallReport.Columns["clmlock"].Index)
          {
              if (dtgCallReport["clmclear", e.RowIndex].Value.ToString() == "N") //boy
              {
                  if (dtgCallReport["clmlock", e.RowIndex].Value == "Y")
                  {
                      dtgCallReport["clmlock", e.RowIndex].Value = "N";
                      dtgCallReport["clmclear", e.RowIndex].Value = "Y";
                  }
                  else if (dtgCallReport["clmlock", e.RowIndex].Value == "N")
                  {
                      dtgCallReport["clmlock", e.RowIndex].Value = "Y";
                      dtgCallReport["clmclear", e.RowIndex].Value = "Y";
                      flgChk = "0";
                      chkall.Checked = true;
                  } 
              }
          }
        }

        private void setFormatDate(KeyEventArgs e, TextBox t)
        {
            if ((t.Text.Length < 8 || t.Text.Length > 8) && !(t.Text.Contains("/")))
            {
               // MessageBox.Show("");
                MessageBox.Show(new Form() { TopMost = true }, "โปรดกรอกวันที่ให้ตรงตามรูปแบบ");
            }
            else if (!t.Text.Contains("/"))
            {
                t.Text = t.Text.Substring(0, 2) + "/" + t.Text.Substring(2, 2) + "/" + t.Text.Substring(4, 4);
            }  
        }

        private void txtdatebegin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13 && txtdatebegin.Text != "")
            {
                setFormatDate(e, txtdatebegin);
                txthrbegin.Focus();
            }
        }

        private void txtdateend_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13 && txtdateend.Text != "")
            {
                setFormatDate(e, txtdateend);
                txthrend.Focus();
            }
        }

        private void txtstatusdtbegin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13 && txtstatusdtbegin.Text != "")
            {
                setFormatDate(e, txtstatusdtbegin);
                txtstatushrbegin.Focus();
            }
        }

        private void txtstatusdtend_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13 && txtstatusdtend.Text != "")
            {
                setFormatDate(e, txtstatusdtend);
                txtstatushrend.Focus();
            }
        }

        private void txtsddoc_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13 && txtsddoc.Text != "")
            {
                setFormatDate(e, txtsddoc);
                txtshdoc.Focus();
            }
        }

        private void txteddoc_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13 && txteddoc.Text != "")
            {
                setFormatDate(e, txteddoc);
                txtehdoc.Focus();
            }
        }

        private void FormApplicationSelector_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible == true && IND_ROW_CLICK != null)
            {
                using (NewBISSvcRef.NewBISSvcClient client = new NewBISSvcRef.NewBISSvcClient())
                {
                    long[] uappids = { (long)dtgCallReport["clmuappid", (int)IND_ROW_CLICK].Value };
                    string[] userid = { UserID };
                    pr2 = client.GetLastU_APPLICATION_LOCK(out uApplicationUpdate, (long)dtgCallReport["clmuappid", (int)IND_ROW_CLICK].Value);
                    if (pr2.Successed == true)
                    {
                        if (uApplicationUpdate != null)
                        {
                            if (uApplicationUpdate.UPDATE_FLG == 'Y' && (uApplicationUpdate.TRANSACTION_FLG == 'P' || uApplicationUpdate.TRANSACTION_FLG == 'U'))
                            {
                                dtgCallReport.Rows[(int)IND_ROW_CLICK].DefaultCellStyle.BackColor = Color.PeachPuff;
                                client.EditLock("E", uappids, "Y", userid, DateTime.Now);
                                dtgCallReport["clmlock", (int)IND_ROW_CLICK].Value = "N";
                            }
                            else if (uApplicationUpdate.UPDATE_FLG == 'N' && (uApplicationUpdate.TRANSACTION_FLG == 'P' || uApplicationUpdate.TRANSACTION_FLG == 'U'))
                            {
                                client.RemoveLock(uappids, "N", userid);
                                dtgCallReport["clmlock", (int)IND_ROW_CLICK].Value = "N";
                            }
                        }
                    }

                    if (client.State != System.ServiceModel.CommunicationState.Closed)
                    {
                        client.Close();
                    }
                }

                int cntCheck = 0;

                if (dtgCallReport.RowCount > 0)
                {
                    dtgCallReport.EndEdit();

                    foreach (DataGridViewRow row in dtgCallReport.Rows)
                    {
                        if (row.Cells["clmlock"].Value == "Y") //check
                        {
                            cntCheck++;
                        }
                    }

                    if (cntCheck == dtgCallReport.RowCount)
                    {
                        chkall.Checked = true;
                    }
                    else
                    {
                        flgChk = "0";
                        chkall.Checked = false;
                    }
                }
               // long[] uappids = { (long)dtgCallReport["clmuappid", e.RowIndex].Value };
               // string[] updids = { UserID };
                //client.EditLock("E", uappids, "Y", updids, DateTime.Now);
                //client.RemoveLock(uappids, "N", updids); 
                //UnlockAllTransaction();
                //dtgCallReport["clmlock", (int)IND_ROW_CLICK].Value  = "N";
            }
        }

        private void cmbsmart_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cmbsmart.SelectedValue == "N")
            {
                pnlsmart.Visible = true;
            }
            else
            {
                pnlsmart.Visible = false;
            }

        }

        private void unlockmenu_Click(object sender, EventArgs e)
        {
            FormUnlockApp unlock = new FormUnlockApp(UserID);
            unlock.Show(this);
        }

        private void cmbworkgroup_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cmbchannel.SelectedValue.ToString() == "OD" && (cmbworkgroup.SelectedValue.ToString() == "OD1" || cmbworkgroup.SelectedValue.ToString() == "OD2"))
            {
                cmbmarketingtype.DataSource = null;

                List<NewBISSvcRef.ZTB_MARKETING_TYPE> fill = new List<NewBISSvcRef.ZTB_MARKETING_TYPE>();
                fill.Clear();

                var filter = from x in ztbMarketingTypes
                             where x.MARKETING_TYPE == "AGT" || x.MARKETING_TYPE  == "TEL"
                             select x;

                fill.AddRange(filter.ToArray());
                NewBISSvcRef.ZTB_MARKETING_TYPE blank = new NewBISSvcRef.ZTB_MARKETING_TYPE();
                blank.DESCRIPTION = "ทั้งหมด";
                blank.MARKETING_TYPE = "";
                fill.Add(blank);

                cmbmarketingtype.DataSource = fill;
                cmbmarketingtype.DisplayMember = "DESCRIPTION";
                cmbmarketingtype.ValueMember = "MARKETING_TYPE";
                cmbmarketingtype.SelectedValue = "AGT";
            }
            else if(cmbchannel.SelectedValue.ToString() == "OD" && (cmbworkgroup.SelectedValue.ToString() == "BNC"))
            {
                cmbmarketingtype.DataSource = null;
                //cmbmarketingtype.SelectedValue = "BNC";
                List<NewBISSvcRef.ZTB_MARKETING_TYPE> fill = new List<NewBISSvcRef.ZTB_MARKETING_TYPE>();
                fill.Clear();

                var filter = from x in ztbMarketingTypes
                             where x.MARKETING_TYPE == "BNC"
                             select x;

                fill.AddRange(filter.ToArray());
                NewBISSvcRef.ZTB_MARKETING_TYPE blank = new NewBISSvcRef.ZTB_MARKETING_TYPE();
                blank.DESCRIPTION = "ทั้งหมด";
                blank.MARKETING_TYPE = "";
                fill.Add(blank);

                cmbmarketingtype.DataSource = fill;
                cmbmarketingtype.DisplayMember = "DESCRIPTION";
                cmbmarketingtype.ValueMember = "MARKETING_TYPE";
                cmbmarketingtype.SelectedValue = "BNC";
            }
            else if (cmbchannel.SelectedValue.ToString() == "OD" && cmbworkgroup.SelectedValue.ToString() == "SAL")
            {
                cmbmarketingtype.DataSource = null;
                //cmbmarketingtype.SelectedValue = "TEL";
                List<NewBISSvcRef.ZTB_MARKETING_TYPE> fill = new List<NewBISSvcRef.ZTB_MARKETING_TYPE>();
                fill.Clear();

                var filter = from x in ztbMarketingTypes
                             where x.MARKETING_TYPE == "AGT" //|| x.MARKETING_TYPE == "TEL"
                             select x;

                fill.AddRange(filter.ToArray());
                NewBISSvcRef.ZTB_MARKETING_TYPE blank = new NewBISSvcRef.ZTB_MARKETING_TYPE();
                blank.DESCRIPTION = "ทั้งหมด";
                blank.MARKETING_TYPE = "";
                fill.Add(blank);

                cmbmarketingtype.DataSource = fill;
                cmbmarketingtype.DisplayMember = "DESCRIPTION";
                cmbmarketingtype.ValueMember = "MARKETING_TYPE";
                cmbmarketingtype.SelectedValue = "";
            }
            else
            {
                cmbmarketingtype.DataSource = null;
                List<NewBISSvcRef.ZTB_MARKETING_TYPE> fill = new List<NewBISSvcRef.ZTB_MARKETING_TYPE>();

                fill.AddRange(ztbMarketingTypes);

                NewBISSvcRef.ZTB_MARKETING_TYPE blank = new NewBISSvcRef.ZTB_MARKETING_TYPE();
                blank.DESCRIPTION = "ทั้งหมด";
                blank.MARKETING_TYPE = "";
                fill.Add(blank);

                cmbmarketingtype.DataSource = fill;
                cmbmarketingtype.DisplayMember = "DESCRIPTION";
                cmbmarketingtype.ValueMember = "MARKETING_TYPE";
                cmbmarketingtype.SelectedValue = "";
            }
        } 
    }

    public static class DateModule  //Extension Method for changedate ind datetime variable
    { 
        public static DateTime ChangeTime(this DateTime dateTime, int hours, int minutes, int seconds, int milliseconds)
        {
            return new DateTime(
                dateTime.Year,
                dateTime.Month,
                dateTime.Day,
                hours,
                minutes,
                seconds,
                milliseconds,
                dateTime.Kind);
        }
    }

}
