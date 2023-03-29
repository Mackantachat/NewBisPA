using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NewBisPA
{
    public partial class FormUnlockApp : Form
    {

        NewBISSvcRef.ProcessResult pr = new NewBISSvcRef.ProcessResult();
        NewBISSvcRef.APP_LOCK_DETAIL[] appLockDets;

        private string _UserID;

        public string UserID
        {
            get { return _UserID; }
            set { _UserID = value; }

        }
        public FormUnlockApp(string _userid)
        {
            InitializeComponent();

            UserID = _userid;
        }

        private void FormUnlockApp_Load(object sender, EventArgs e)
        {
            bindingDatagrid();
        }

        private void bindingDatagrid()
        {
            dtgapplockdetail.DataSource = null;
            dtgapplockdetail.AutoGenerateColumns = false;

            using (NewBISSvcRef.NewBISSvcClient client = new NewBISSvcRef.NewBISSvcClient())
            {
                try
                {
                    pr = client.GetAppLockDetail(out appLockDets);
                    if (pr.Successed == true)
                    { 
                        var checkLock =
                            from x in appLockDets
                            where x.UPD_ID == UserID
                            select x;

                        if (checkLock != null && checkLock.Count() > 0)
                        {
                            btnunlockall.Enabled = true;
                        }
                        else
                        {
                            btnunlockall.Enabled = false;
                        }

                        dtgapplockdetail.DataSource = checkLock.ToArray();

                        foreach (DataGridViewRow dr in dtgapplockdetail.Rows)
                        {
                            if (dr.DataBoundItem is NewBISSvcRef.APP_LOCK_DETAIL)
                            {
                                NewBISSvcRef.APP_LOCK_DETAIL obj = (NewBISSvcRef.APP_LOCK_DETAIL)dr.DataBoundItem;
                                dr.Cells["clmappno"].Value = obj.APP_NO;
                                dr.Cells["clmusername"].Value = obj.USERNAME;
                                dr.Cells["clmdate"].Value = obj.FSYSTEM_DT;
                                dr.Cells["clmtelephone"].Value = obj.TELEPHONE;
                                dr.Cells["clmuappid"].Value = obj.UAPP_ID;
                                dr.Cells["clmupdid"].Value = obj.UPD_ID;
                                dr.Cells["clmupdateflg"].Value = obj.UPDATE_FLG; 
                            }
                        }

                        //if (dtgapplockdetail.RowCount == 0)
                        //{
                        //    MessageBox.Show("ยังไม่มีข้อมูล");
                        //}
                    }
                    else
                    {
                        MessageBox.Show(pr.ErrorMessage);
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

        private void dtgapplockdetail_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex != dtgapplockdetail.Columns["clmunlock"].Index )
            {
                return; 
            }
            else if (e.ColumnIndex == dtgapplockdetail.Columns["clmunlock"].Index)
            {
                string appno = dtgapplockdetail["clmappno", e.RowIndex].Value.ToString();
                if (MessageBox.Show("ท่านต้องการปลดล็อคใบคำขอเลขที่ " + appno + "?", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    try
                    {
                        UnlockByApp(e);
                        MessageBox.Show("ใบคำขอเลขที่ " + appno + " ถูกปลดล็อคแล้ว");
                        bindingDatagrid();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        private void UnlockByApp(DataGridViewCellEventArgs e)
        {
            using (NewBISSvcRef.NewBISSvcClient client = new NewBISSvcRef.NewBISSvcClient())
            {
                long[] uappids = { Convert.ToInt64(dtgapplockdetail["clmuappid", e.RowIndex].Value.ToString().Trim()) };
                string[] updids = { dtgapplockdetail["clmupdid", e.RowIndex].Value.ToString() };

                if (dtgapplockdetail["clmupdateflg", e.RowIndex].Value != null && dtgapplockdetail["clmupdateflg", e.RowIndex].Value.ToString() == "Y")
                {
                    client.EditLock("E", uappids, "Y", updids, DateTime.Now);
                }
                else
                {
                    client.RemoveLock(uappids, "N", updids);
                }
                 
                if (client.State != System.ServiceModel.CommunicationState.Closed)
                {
                    client.Close();
                }
            }
        }

        private void btnunlockall_Click(object sender, EventArgs e)
        {
            if (dtgapplockdetail.RowCount > 0)
            {
                try
                {
                    using (NewBISSvcRef.NewBISSvcClient client = new NewBISSvcRef.NewBISSvcClient())
                    {

                        dtgapplockdetail.EndEdit();
                        List<long> uapp_ids = new List<long>();
                        List<string> upd_ids = new List<string>();
                        foreach (DataGridViewRow row in dtgapplockdetail.Rows)
                        {
                            if (UserID == row.Cells["clmupdid"].Value.ToString())
                            {
                                uapp_ids.Add(Convert.ToInt64(row.Cells["clmuappid"].Value.ToString().Trim()));
                                upd_ids.Add(row.Cells["clmupdid"].Value.ToString().Trim());
                            }
                        }

                        client.RemoveLock(uapp_ids.ToArray(), "N", upd_ids.ToArray());
                        client.EditLock("E", uapp_ids.ToArray(), "Y", upd_ids.ToArray(), DateTime.Now);

                        MessageBox.Show("ใบคำขอทั้งหมดถูกปลดล็อคแล้ว");
                        bindingDatagrid();

                        if (client.State != System.ServiceModel.CommunicationState.Closed)
                        {
                            client.Close();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("ไม่มีใบคำขอที่ถูกล็อค");
            }
        }
    }
}
