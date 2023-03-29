using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraReports.UI;
using System.IO;
using System.Threading;

namespace NewBisPA
{
    public partial class FormKwanbualung_Report : Form
    {
        private NewBISSvcRef.KWBL_PLAN[] KWBL_PLANs = null;
        private NewBISSvcRef.KWBL_BRANCH[] KWBL_BRANCHs = null;
        private Initial_Form Initial = new Initial_Form();
        private bool Done = false;

        public FormKwanbualung_Report()
        {
            InitializeComponent();

        }

        private void FormKwanbualung_Report_Load(object sender, EventArgs e)
        {
            try
            {
                this.Hide();
                Thread thread = new Thread(new ThreadStart(this.ShowInitial_Form));
                thread.IsBackground = true;
                thread.Start();
                Txt_STATUS_DT_ST.Focus();
                GetData_POLCIY_HOLDING_POLICY();
                GetData_BBL_BRANCH();
                this.Show();
                this.Activate();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Code: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                this.Dispose();
            }
            finally
            {
                Done = true;
            }
        }

        private void ShowInitial_Form()
        {
            Initial.Show();
            Initial.Activate();
            while (!Done)
            {
                Application.DoEvents();
            }
            Initial.Close();
            this.Initial.Dispose();
        }

        private void TabControl_REPORT_SelectedIndexChanged(object sender, EventArgs e)
        {
            #region "REGION"
            if (TabControl_REPORT.SelectedIndex == 1 && DD_POLICY_HOLDING.DataSource == null && KWBL_PLANs != null)
            {
                this.Cursor = Cursors.WaitCursor;
                DD_POLICY_HOLDING.DataSource = KWBL_PLANs.OrderBy(s => s.POLICY_HOLDING).ToArray();
                DD_POLICY_HOLDING.ValueMember = "POLICY_HOLDING";
                DD_POLICY_HOLDING.DisplayMember = "TITLE";
                this.Cursor = Cursors.Default;
            }
            #endregion "REGION"
            #region "POLICY"
            else if (TabControl_REPORT.SelectedIndex == 2 && DD_POLICY_HOLDING_CER.DataSource == null &&
                     DD_BRANCH_CER.DataSource == null && KWBL_PLANs != null && KWBL_BRANCHs != null)
            {
                this.Cursor = Cursors.WaitCursor;
                DD_POLICY_HOLDING_CER.DataSource = KWBL_PLANs.OrderBy(s => s.POLICY_HOLDING).ToArray();
                DD_POLICY_HOLDING_CER.ValueMember = "POLICY_HOLDING";
                DD_POLICY_HOLDING_CER.DisplayMember = "TITLE";
                DD_BRANCH_CER.DataSource = KWBL_BRANCHs.OrderBy(s => s.BRANCH_TITLE).ToArray();
                DD_BRANCH_CER.ValueMember = "BBL_CODE";
                DD_BRANCH_CER.DisplayMember = "BRANCH_TITLE";
                AutoCompleteStringCollection Data = new AutoCompleteStringCollection();
                string[] BRANCH_NAME = KWBL_BRANCHs.Select(s => s.BRANCH_TITLE).OrderBy(s => s).ToArray();
                Data.AddRange(BRANCH_NAME);
                DD_BRANCH_CER.AutoCompleteMode = AutoCompleteMode.Suggest;
                DD_BRANCH_CER.AutoCompleteSource = AutoCompleteSource.ListItems;
                DD_BRANCH_CER.AutoCompleteCustomSource = Data;
                this.Cursor = Cursors.Default;
                Initial.Close();
            }
            #endregion "POLICY"
            #region "CANCEL"
            else if (TabControl_REPORT.SelectedIndex == 3 && DD_POLICY_HOLDING_CANCEL.DataSource == null &&
                     DD_BRANCH_CANCEL.DataSource == null && KWBL_PLANs != null && KWBL_BRANCHs != null)
            {
                this.Cursor = Cursors.WaitCursor;
                DD_POLICY_HOLDING_CANCEL.DataSource = KWBL_PLANs.OrderBy(s => s.POLICY_HOLDING).ToArray();
                DD_POLICY_HOLDING_CANCEL.ValueMember = "POLICY_HOLDING";
                DD_POLICY_HOLDING_CANCEL.DisplayMember = "TITLE";
                DD_BRANCH_CANCEL.DataSource = KWBL_BRANCHs.OrderBy(s => s.BRANCH_TITLE).ToArray();
                DD_BRANCH_CANCEL.ValueMember = "BBL_CODE";
                DD_BRANCH_CANCEL.DisplayMember = "BRANCH_TITLE";
                AutoCompleteStringCollection Data = new AutoCompleteStringCollection();
                string[] BRANCH_NAME = KWBL_BRANCHs.Select(s => s.BRANCH_TITLE).OrderBy(s => s).ToArray();
                Data.AddRange(BRANCH_NAME);
                DD_BRANCH_CANCEL.AutoCompleteMode = AutoCompleteMode.Suggest;
                DD_BRANCH_CANCEL.AutoCompleteSource = AutoCompleteSource.ListItems;
                DD_BRANCH_CANCEL.AutoCompleteCustomSource = Data;
                this.Cursor = Cursors.Default;
            }
            #endregion "CANCEL"
            #region "CLOSE"
            else if (TabControl_REPORT.SelectedIndex == 4 && DD_POLICY_HOLDING_CLOSE.DataSource == null && KWBL_PLANs != null)
            {
                this.Cursor = Cursors.WaitCursor;
                DD_POLICY_HOLDING_CLOSE.DataSource = KWBL_PLANs.OrderBy(s => s.POLICY_HOLDING).ToArray();
                DD_POLICY_HOLDING_CLOSE.ValueMember = "POLICY_HOLDING";
                DD_POLICY_HOLDING_CLOSE.DisplayMember = "TITLE";
                this.Cursor = Cursors.Default;
            }
            #endregion "CLOSE"
            #region "EXPORT FILE"
            else if (TabControl_REPORT.SelectedIndex == 5 && DD_POLICY_HOLDING_EXPORT.DataSource == null && KWBL_PLANs != null)
            {
                this.Cursor = Cursors.WaitCursor;
                #region "POLICY HOLDING"
                DD_POLICY_HOLDING_EXPORT.DataSource = KWBL_PLANs.OrderBy(s => s.POLICY_HOLDING).ToArray();
                DD_POLICY_HOLDING_EXPORT.ValueMember = "POLICY_HOLDING";
                DD_POLICY_HOLDING_EXPORT.DisplayMember = "TITLE";
                #endregion "POLICY HOLDING"
                #region "STATUS"
                DataTable STATUS = new DataTable();
                STATUS.Columns.Add("STATUS");
                STATUS.Columns.Add("VALUE");
                STATUS.Rows.Add("1. ปฏิเสธรับประกัน", "DECLINED");
                STATUS.Rows.Add("2. สิ้นสุดความคุ้มครองอายุครบ 60,65 ปี", "MATURITY");
                STATUS.Rows.Add("3. เรียกร้องค่าสินไหมแล้ว", "DEATH");
                DD_STATUS.DataSource = STATUS;
                DD_STATUS.ValueMember = "VALUE";
                DD_STATUS.DisplayMember = "STATUS";
                #endregion "STATUS"
                this.Cursor = Cursors.Default;
            }
            #endregion "EXPORT FILE"
        }

        private void GetData_POLCIY_HOLDING_POLICY()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                ITUtility.ProcessResult pr = new ITUtility.ProcessResult();
                NewBISSvcRef.KWBL_PLAN[] TMP_KWBL_PLANs = null;
                using (NewBISSvcRef.NewBISSvcClient client = new NewBISSvcRef.NewBISSvcClient())
                {
                    pr = client.GetDataKWANBUALUNG_PLAN(out TMP_KWBL_PLANs);
                    client.Close();
                }
                if (pr.Successed)
                {
                    KWBL_PLANs = TMP_KWBL_PLANs;
                }
                else
                {
                    MessageBox.Show(pr.ErrorMessage, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void GetData_BBL_BRANCH()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                ITUtility.ProcessResult pr = new ITUtility.ProcessResult();
                NewBISSvcRef.KWBL_BRANCH[] TMP_KWBL_BRANCHs = null;
                using (NewBISSvcRef.NewBISSvcClient client = new NewBISSvcRef.NewBISSvcClient())
                {
                    pr = client.GetDataBBL_BRANCH(out TMP_KWBL_BRANCHs);
                    client.Close();
                }
                if (pr.Successed)
                {
                    KWBL_BRANCHs = TMP_KWBL_BRANCHs;
                }
                else
                {
                    MessageBox.Show(pr.ErrorMessage, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        #region "REPORT [STATUS]"

        private void Search_Data_Status()
        {
            try
            {
                if (Txt_STATUS_DT_ST.MaskCompleted || (Txt_STATUS_DT_ST.MaskCompleted && Txt_STATUS_DT_EN.MaskCompleted))
                {
                    this.Cursor = Cursors.WaitCursor;
                    #region "MODIFY DATETIME"
                    DateTime? ST = null;
                    DateTime? EN = null;
                    try
                    {
                        if (Txt_STATUS_DT_ST.MaskCompleted && Txt_STATUS_DT_EN.MaskCompleted)
                        {
                            ST = Convert.ToDateTime(Txt_STATUS_DT_ST.Text);
                            EN = Convert.ToDateTime(Txt_STATUS_DT_EN.Text);
                        }
                        else if (Txt_STATUS_DT_ST.MaskCompleted && !Txt_STATUS_DT_EN.MaskCompleted)
                        {
                            ST = Convert.ToDateTime(Txt_STATUS_DT_ST.Text);
                        }
                    }
                    catch
                    {
                        throw new Exception("Date Input Invalid!");
                    }
                    #endregion "MODIFY DATETIME"
                    #region "CALL WEB SERVICE"
                    ITUtility.ProcessResult pr = new ITUtility.ProcessResult();
                    NewBISSvcRef.REPORT_KWANBUALUNG_STATUS[] REPORT_DATA = null;
                    using (NewBISSvcRef.NewBISSvcClient client = new NewBISSvcRef.NewBISSvcClient())
                    {
                        pr = client.GetDataREPORT_KWANBUALUNG_STATUS(out REPORT_DATA, ST, EN);
                    }
                    #endregion "CALL WEB SERVICE"
                    #region "CREATE REPORT/SHOW ERROR"
                    if (pr.Successed)
                    {
                        if (REPORT_DATA != null)
                        {
                            XtraReport_KwanBuaLung_Status Report = new XtraReport_KwanBuaLung_Status(REPORT_DATA, ST, EN);
                            ReportPrintTool Tool = new ReportPrintTool(Report);
                            Tool.ShowPreview();
                        }
                        else
                        {
                            MessageBox.Show("ไม่พบข้อมูลที่ค้นหา !", "No Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        MessageBox.Show(pr.ErrorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    #endregion "CREATE REPORT/SHOW ERROR"
                }
                else
                {
                    Txt_STATUS_DT_ST.Focus();
                    MessageBox.Show("กรุณาตรวจสอบเงื่อนไขการค้นหา", "Check Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                Txt_STATUS_DT_ST.Clear();
                Txt_STATUS_DT_EN.Clear();
                Txt_STATUS_DT_ST.Focus();
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void Txt_STATUS_DT_ST_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Search_Data_Status();
            }
        }

        private void Txt_STATUS_DT_EN_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Search_Data_Status();
            }
        }

        private void Button_Search_Status_Click(object sender, EventArgs e)
        {
            Search_Data_Status();
        }

        private void Button_Clear_Click(object sender, EventArgs e)
        {
            Txt_STATUS_DT_ST.Clear();
            Txt_STATUS_DT_EN.Clear();
            Txt_STATUS_DT_ST.Focus();
        }

        #endregion "REPORT [STATUS]"

        #region "REPORT [REGION]"

        private void Search_Data_Region()
        {
            try
            {
                if (Txt_REGION_ST.MaskCompleted || (Txt_REGION_ST.MaskCompleted && Txt_REGION_EN.MaskCompleted))
                {
                    this.Cursor = Cursors.WaitCursor;
                    #region "MODIFY DATETIME"
                    DateTime? ST = null;
                    DateTime? EN = null;
                    try
                    {
                        if (Txt_REGION_ST.MaskCompleted && Txt_REGION_EN.MaskCompleted)
                        {
                            ST = Convert.ToDateTime(Txt_REGION_ST.Text);
                            EN = Convert.ToDateTime(Txt_REGION_EN.Text);
                        }
                        else if (Txt_REGION_ST.MaskCompleted && !Txt_REGION_EN.MaskCompleted)
                        {
                            ST = Convert.ToDateTime(Txt_REGION_ST.Text);
                        }
                    }
                    catch
                    {
                        throw new Exception("Date Input Invalid!");
                    }
                    #endregion "MODIFY DATETIME"
                    #region "CALL WEB SERVICE"
                    ITUtility.ProcessResult pr = new ITUtility.ProcessResult();
                    NewBISSvcRef.REPORT_KWANBUALUNG_REGION[] REPORT_KWANBUALUNG_REGIONs = null;
                    using (NewBISSvcRef.NewBISSvcClient client = new NewBISSvcRef.NewBISSvcClient())
                    {
                        pr = client.GetDataREPORT_KWANBUALUNG_REGION(out REPORT_KWANBUALUNG_REGIONs, DD_POLICY_HOLDING.SelectedValue.ToString(), ST, EN);
                        client.Close();
                    }
                    #endregion "CALL WEB SERVICE"
                    #region "CREATE REPORT / SHOW ERROR"
                    if (pr.Successed)
                    {
                        if (REPORT_KWANBUALUNG_REGIONs != null)
                        {
                            string TITLE = DD_POLICY_HOLDING.Text;
                            string[] TMP_TITLE = TITLE.Split(':');
                            string POLICY_HOLDING = TMP_TITLE[0].Trim();
                            TITLE = TMP_TITLE[1].Trim();
                            XtraReport_KwanBuaLung_Region Report = new XtraReport_KwanBuaLung_Region(REPORT_KWANBUALUNG_REGIONs, ST, EN, TITLE, POLICY_HOLDING);
                            ReportPrintTool Tool = new ReportPrintTool(Report);
                            Tool.ShowPreview();
                        }
                        else
                        {
                            MessageBox.Show("ไม่พบข้อมูลที่ค้นหา !", "No Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        MessageBox.Show(pr.ErrorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    #endregion "CREATE REPORT / SHOW ERROR"
                }
                else
                {
                    Txt_REGION_ST.Focus();
                    MessageBox.Show("กรุณาตรวจสอบเงื่อนไขการค้นหา", "Check Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void Button_REGION_SEARCH_Click(object sender, EventArgs e)
        {
            Search_Data_Region();
        }

        private void Txt_REGION_ST_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Search_Data_Region();
            }
        }

        private void Txt_REGION_EN_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Search_Data_Region();
            }
        }

        private void Button_REGION_CLEAR_Click(object sender, EventArgs e)
        {
            Txt_REGION_ST.Clear();
            Txt_REGION_EN.Clear();
            Txt_REGION_ST.Focus();
            DD_POLICY_HOLDING.SelectedIndex = 0;
        }

        #endregion "REPORT [REGION]"

        #region "REPORT [POLICY]"

        private void Search_Data_Policy()
        {
            try
            {
                if ((Txt_CER_DT_ST.MaskCompleted || (Txt_CER_DT_ST.MaskCompleted && Txt_CER_DT_EN.MaskCompleted)) && (DD_BRANCH_CER.SelectedValue != null || DD_BRANCH_CER.SelectedIndex == 0))
                {
                    this.Cursor = Cursors.WaitCursor;
                    #region "MODIFY DATETIME"
                    DateTime? ST = null;
                    DateTime? EN = null;
                    try
                    {
                        if (Txt_CER_DT_ST.MaskCompleted && Txt_CER_DT_EN.MaskCompleted)
                        {
                            ST = Convert.ToDateTime(Txt_CER_DT_ST.Text);
                            EN = Convert.ToDateTime(Txt_CER_DT_EN.Text);
                        }
                        else if (Txt_CER_DT_ST.MaskCompleted && !Txt_CER_DT_EN.MaskCompleted)
                        {
                            ST = Convert.ToDateTime(Txt_CER_DT_ST.Text);
                        }
                    }
                    catch
                    {
                        throw new Exception("Date Input Invalid!");
                    }
                    #endregion "MODIFY DATETIME"
                    #region "CALL WEB SERVICE"
                    ITUtility.ProcessResult pr = new ITUtility.ProcessResult();
                    NewBISSvcRef.REPORT_KWANBUALUNG_POLICY[] REPORT_KWANBUALUNG_POLICYs = null;
                    using (NewBISSvcRef.NewBISSvcClient client = new NewBISSvcRef.NewBISSvcClient())
                    {
                        string BRANCH_CODE = null;
                        if (DD_BRANCH_CER.SelectedValue != null)
                        {
                            BRANCH_CODE = DD_BRANCH_CER.SelectedValue.ToString();
                        }
                        pr = client.GetDataREPORT_KWANBUALUNG_POLICY(out REPORT_KWANBUALUNG_POLICYs, DD_POLICY_HOLDING_CER.SelectedValue.ToString(), BRANCH_CODE, ST, EN);
                        client.Close();
                    }
                    #endregion "CALL WEB SERVICE"
                    #region "CREATE REPORT / SHOW ERROR"
                    if (pr.Successed)
                    {
                        if (REPORT_KWANBUALUNG_POLICYs != null)
                        {
                            string TITLE = DD_POLICY_HOLDING_CER.Text;
                            string[] TMP_TITLE = TITLE.Split(':');
                            string POLICY_HOLDING = TMP_TITLE[0].Trim();
                            TITLE = TMP_TITLE[1].Trim();
                            XtraReport_KwanBuaLung_Policy Report = new XtraReport_KwanBuaLung_Policy(REPORT_KWANBUALUNG_POLICYs, ST, EN, TITLE, POLICY_HOLDING);
                            ReportPrintTool Tool = new ReportPrintTool(Report);
                            Tool.ShowPreview();
                        }
                        else
                        {
                            MessageBox.Show("ไม่พบข้อมูลที่ค้นหา !", "No Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        MessageBox.Show(pr.ErrorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    #endregion "CREATE REPORT / SHOW ERROR"
                }
                else
                {
                    DD_POLICY_HOLDING.Focus();
                    MessageBox.Show("กรุณาตรวจสอบเงื่อนไขการค้นหา", "Check Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void Button_CER_SEARCH_Click(object sender, EventArgs e)
        {
            Search_Data_Policy();
        }

        private void Button_CER_CLEAR_Click(object sender, EventArgs e)
        {
            DD_BRANCH_CER.SelectedIndex = 0;
            DD_POLICY_HOLDING_CER.SelectedIndex = 0;
            Txt_CER_DT_ST.Clear();
            Txt_CER_DT_EN.Clear();
            DD_POLICY_HOLDING_CER.Focus();
        }

        private void Txt_CER_DT_ST_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Search_Data_Policy();
            }
        }

        private void Txt_CER_DT_EN_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Search_Data_Policy();
            }
        }

        #endregion "REPORT [POLICY]"

        #region "REPORT [CANCEL]"

        private void Search_Data_Cancel()
        {
            try
            {
                if ((Txt_CANCEL_ST.MaskCompleted || (Txt_CANCEL_ST.MaskCompleted && Txt_CANCEL_EN.MaskCompleted)) && (DD_BRANCH_CANCEL.SelectedValue != null || DD_BRANCH_CANCEL.SelectedIndex == 0))
                {
                    this.Cursor = Cursors.WaitCursor;
                    #region "MODIFY DATETIME"
                    DateTime? ST = null;
                    DateTime? EN = null;
                    try
                    {
                        if (Txt_CANCEL_ST.MaskCompleted && Txt_CANCEL_EN.MaskCompleted)
                        {
                            ST = Convert.ToDateTime(Txt_CANCEL_ST.Text);
                            EN = Convert.ToDateTime(Txt_CANCEL_EN.Text);
                        }
                        else if (Txt_CANCEL_ST.MaskCompleted && !Txt_CANCEL_EN.MaskCompleted)
                        {
                            ST = Convert.ToDateTime(Txt_CANCEL_ST.Text);
                        }
                    }
                    catch
                    {
                        throw new Exception("Date Input Invalid!");
                    }
                    #endregion "MODIFY DATETIME"
                    #region "CALL WEB SERVICE"
                    ITUtility.ProcessResult pr = new ITUtility.ProcessResult();
                    NewBISSvcRef.REPORT_KWANBUALUNG_CANCEL[] REPORT_KWANBUALUNG_CANCELs = null;
                    using (NewBISSvcRef.NewBISSvcClient client = new NewBISSvcRef.NewBISSvcClient())
                    {
                        string BRANCH_CODE = null;
                        if (DD_BRANCH_CER.SelectedValue != null)
                        {
                            BRANCH_CODE = DD_BRANCH_CANCEL.SelectedValue.ToString();
                        }
                        pr = client.GetDataREPORT_KWANBUALUNG_CANCEL(out REPORT_KWANBUALUNG_CANCELs, DD_POLICY_HOLDING_CANCEL.SelectedValue.ToString(), BRANCH_CODE, ST, EN);
                        client.Close();
                    }
                    #endregion "CALL WEB SERVICE"
                    #region "CREATE REPORT / SHOW ERROR"
                    if (pr.Successed)
                    {
                        if (REPORT_KWANBUALUNG_CANCELs != null)
                        {
                            string TITLE = DD_POLICY_HOLDING_CANCEL.Text;
                            string[] TMP_TITLE = TITLE.Split(':');
                            string POLICY_HOLDING = TMP_TITLE[0].Trim();
                            TITLE = TMP_TITLE[1].Trim();
                            XtraReport_KwanBuaLung_Cancel Report = new XtraReport_KwanBuaLung_Cancel(REPORT_KWANBUALUNG_CANCELs, ST, EN, TITLE, POLICY_HOLDING);
                            ReportPrintTool Tool = new ReportPrintTool(Report);
                            Tool.ShowPreview();
                        }
                        else
                        {
                            MessageBox.Show("ไม่พบข้อมูลที่ค้นหา !", "No Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        MessageBox.Show(pr.ErrorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    #endregion "CREATE REPORT / SHOW ERROR"
                }
                else
                {
                    DD_POLICY_HOLDING.Focus();
                    MessageBox.Show("กรุณาตรวจสอบเงื่อนไขการค้นหา", "Check Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void Button_SEARCH_CLEAR_Click(object sender, EventArgs e)
        {
            Search_Data_Cancel();
        }

        private void Button_CANCEL_CLEAR_Click(object sender, EventArgs e)
        {
            DD_BRANCH_CANCEL.SelectedIndex = 0;
            DD_POLICY_HOLDING_CANCEL.SelectedIndex = 0;
            Txt_CANCEL_ST.Clear();
            Txt_CANCEL_EN.Clear();
            DD_POLICY_HOLDING_CANCEL.Focus();
        }

        private void Txt_CANCEL_ST_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Search_Data_Cancel();
            }
        }

        private void Txt_CANCEL_EN_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Search_Data_Cancel();
            }
        }

        #endregion "REPORT [CANCEL]"

        #region "REPORT [CLOSE]"

        private void Search_Data_Close()
        {
            try
            {
                if (Txt_CLOSE_ST.MaskCompleted || (Txt_CLOSE_ST.MaskCompleted && Txt_CLOSE_EN.MaskCompleted))
                {
                    this.Cursor = Cursors.WaitCursor;
                    #region "MODIFY DATETIME"
                    DateTime? ST = null;
                    DateTime? EN = null;
                    try
                    {
                        if (Txt_CLOSE_ST.MaskCompleted && Txt_CLOSE_EN.MaskCompleted)
                        {
                            ST = Convert.ToDateTime(Txt_CLOSE_ST.Text);
                            EN = Convert.ToDateTime(Txt_CLOSE_EN.Text);
                        }
                        else if (Txt_CLOSE_ST.MaskCompleted && !Txt_CLOSE_EN.MaskCompleted)
                        {
                            ST = Convert.ToDateTime(Txt_CLOSE_ST.Text);
                        }
                    }
                    catch
                    {
                        throw new Exception("Date Input Invalid!");
                    }
                    #endregion "MODIFY DATETIME"
                    #region "CALL WEB SERVICE"
                    ITUtility.ProcessResult pr = new ITUtility.ProcessResult();
                    NewBISSvcRef.REPORT_KWANBUALUNG_CLOSE[] REPORT_KWANBUALUNG_CLOSEs = null;
                    using (NewBISSvcRef.NewBISSvcClient client = new NewBISSvcRef.NewBISSvcClient())
                    {
                        pr = client.GetDataREPORT_KWANBUALUNG_CLOSE(out REPORT_KWANBUALUNG_CLOSEs, DD_POLICY_HOLDING_CLOSE.SelectedValue.ToString(), ST, EN);
                        client.Close();
                    }
                    #endregion "CALL WEB SERVICE"
                    #region "CREATE REPORT / SHOW ERROR"
                    if (pr.Successed)
                    {
                        if (REPORT_KWANBUALUNG_CLOSEs != null)
                        {
                            REPORT_KWANBUALUNG_CLOSEs = REPORT_KWANBUALUNG_CLOSEs.OrderBy(s => s.BBL_BRANCH_CODE).ToArray() ;
                            string TITLE = DD_POLICY_HOLDING_CLOSE.Text;
                            string[] TMP_TITLE = TITLE.Split(':');
                            string POLICY_HOLDING = TMP_TITLE[0].Trim();
                            TITLE = TMP_TITLE[1].Trim();
                            XtraReport_KwanBuaLung_Close Report = new XtraReport_KwanBuaLung_Close(REPORT_KWANBUALUNG_CLOSEs, ST, EN, TITLE, POLICY_HOLDING);
                            ReportPrintTool Tool = new ReportPrintTool(Report);
                            Tool.ShowPreview();
                        }
                        else
                        {
                            MessageBox.Show("ไม่พบข้อมูลที่ค้นหา !", "No Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        MessageBox.Show(pr.ErrorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    #endregion "CREATE REPORT / SHOW ERROR"
                }
                else
                {
                    Txt_CLOSE_ST.Focus();
                    MessageBox.Show("กรุณาตรวจสอบเงื่อนไขการค้นหา", "Check Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void Button_CLOSE_SEARCH_Click(object sender, EventArgs e)
        {
            Search_Data_Close();
        }

        private void Button_CLOSE_CLEAR_Click(object sender, EventArgs e)
        {
            DD_POLICY_HOLDING_CLOSE.SelectedIndex = 0;
            Txt_CLOSE_ST.Clear();
            Txt_CLOSE_EN.Clear();
            DD_POLICY_HOLDING_CLOSE.Focus();
        }

        private void Txt_CLOSE_ST_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Search_Data_Close();
            }
        }

        private void Txt_CLOSE_EN_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Search_Data_Close();
            }
        }

        #endregion "REPORT [CLOSE]"

        #region "REPORT [EXPORT TEXT FILE]"

        private void Search_Data_Export_File()
        {
            try
            {
                if (Txt_Export_STATUS_ST.MaskCompleted || (Txt_Export_STATUS_ST.MaskCompleted && Txt_Export_STATUS_EN.MaskCompleted))
                {
                    this.Cursor = Cursors.WaitCursor;
                    #region "MODIFY DATETIME"
                    DateTime? ST = null;
                    DateTime? EN = null;
                    try
                    {
                        if (Txt_Export_STATUS_ST.MaskCompleted && Txt_Export_STATUS_EN.MaskCompleted)
                        {
                            ST = Convert.ToDateTime(Txt_Export_STATUS_ST.Text);
                            EN = Convert.ToDateTime(Txt_Export_STATUS_EN.Text);
                        }
                        else if (Txt_Export_STATUS_ST.MaskCompleted && !Txt_Export_STATUS_EN.MaskCompleted)
                        {
                            ST = Convert.ToDateTime(Txt_Export_STATUS_ST.Text);
                        }
                    }
                    catch
                    {
                        throw new Exception("Date Input Invalid!");
                    }
                    #endregion "MODIFY DATETIME"
                    #region "CALL WEB SERVICE"
                    ITUtility.ProcessResult pr = new ITUtility.ProcessResult();
                    string[] dataObject = null;
                    using (NewBISSvcRef.NewBISSvcClient client = new NewBISSvcRef.NewBISSvcClient())
                    {
                        string POLICY_HOLDING = DD_POLICY_HOLDING_EXPORT.SelectedValue.ToString();
                        string STATUS = DD_STATUS.SelectedValue.ToString();
                        pr = client.GetDataTXT_FILE_BY_STATUS(out dataObject, POLICY_HOLDING, STATUS, ST, EN);
                        client.Close();
                    }
                    #endregion "CALL WEB SERVICE"
                    #region "WRITE FILE / SHOW ERROR"
                    if (pr.Successed)
                    {
                        if (dataObject != null && dataObject.Count() > 0)
                        {
                            string Now = ITUtility.Utility.dateTimeToString(DateTime.Now, "dd-MM-yyyy (hh-mm-ss)", "BU");
                            string FOLDER_NAME = DD_STATUS.Text;
                            string STATUS_NAME = DD_STATUS.Text.Substring(3);
                            string MAIN_PATH = "C:\\KwanBuaLung_Export_File";
                            string SUB_PATH = MAIN_PATH + "\\" + FOLDER_NAME;
                            #region "CREATE FOLDER"
                            if (!Directory.Exists(MAIN_PATH))
                            {
                                System.IO.Directory.CreateDirectory(MAIN_PATH);
                            }
                            #endregion "CREATE FOLDER"
                            if (Directory.Exists(MAIN_PATH))
                            {
                                #region "VREATE SUB FOLDER"
                                if (!Directory.Exists(SUB_PATH))
                                {
                                    System.IO.Directory.CreateDirectory(SUB_PATH);
                                }
                                #endregion "VREATE SUB FOLDER"
                                if (Directory.Exists(SUB_PATH))
                                {
                                    #region "WRITE FILE"
                                    string PATH = SUB_PATH + "\\" + Now + "-" + STATUS_NAME + ".txt";
                                    using (StreamWriter sw = new StreamWriter(PATH, false, Encoding.Default))
                                    {
                                        foreach (string i in dataObject)
                                        {
                                            sw.WriteLine(i);
                                        }
                                        sw.Flush();
                                        sw.Close();
                                    }
                                    #endregion "WRITE FILE"
                                    #region "OPEN FILE?"
                                    DialogResult Result = MessageBox.Show("Export File สำเร็จ จำนวน " + dataObject.Count().ToString("N0") + " รายการ !\n" + PATH + "\nต้องการเปิดไฟล์ตอนนี้หรือไม่ ?", "ExportFile Successful", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                                    if (Result == DialogResult.Yes)
                                    {
                                        System.Diagnostics.Process.Start(PATH);
                                    }
                                    #endregion "OPEN FILE?"
                                }
                                else
                                {
                                    MessageBox.Show("สร้าง " + SUB_PATH + "  ไม่สำเร็จ !", "Not Create Directory", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                            else
                            {
                                MessageBox.Show("สร้าง " + MAIN_PATH + " ไม่สำเร็จ !", "Not Create Directory", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else
                        {
                            MessageBox.Show("ไม่พบข้อมูลที่ค้นหา !", "No Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        MessageBox.Show(pr.ErrorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    #endregion "WRITE FILE / SHOW ERROR"
                }
                else
                {
                    DD_POLICY_HOLDING_EXPORT.Focus();
                    MessageBox.Show("กรุณาตรวจสอบเงื่อนไขการค้นหา", "Check Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void button_Export_Search_Click(object sender, EventArgs e)
        {
            Search_Data_Export_File();
        }

        private void button_Export_Clear_Click(object sender, EventArgs e)
        {
            DD_POLICY_HOLDING_EXPORT.SelectedIndex = 0;
            Txt_Export_STATUS_ST.Clear();
            Txt_Export_STATUS_EN.Clear();
            DD_STATUS.SelectedIndex = 0;
            DD_POLICY_HOLDING_EXPORT.Focus();
        }

        private void Txt_Export_STATUS_ST_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                Search_Data_Export_File();
            }
        }

        private void Txt_Export_STATUS_EN_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Search_Data_Export_File();
            }
        }

        #endregion "REPORT [EXPORT TEXT FILE]"


    }
}
