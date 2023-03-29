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
    public partial class PlanSearchForm : Form
    {
        public PlanSearchForm()
        {
            InitializeComponent();
        }

        public string PL_BLOCK { get; set; }

        public string ChannelType { get; set; }

        public string FreeFlag { get; set; }


        public DateTime? IsuDate { get; set; }

        private NewBisPASvcRef.ZTB_PLAN_PM_COLLECTION _PlanSearchList { get; set; }

        public NewBisPASvcRef.ZTB_PLAN_PM PlanResult { get; set; }


        private void btnSearch_Click(object sender, EventArgs e)
        {
            dgvPlanSearch.AutoGenerateColumns = false;
            dgvPlanSearch.RowHeadersVisible = false;
            dgvPlanSearch.DataSource = null;

            NewBisPASvcRef.ZTB_PLAN_PM_COLLECTION planColl = null;
            using (var client = new NewBisPASvcRef.NewBisPASvcClient())
            {
                try
                {
                    var pr = client.GetZtbPlanOfPMByKeyword(out planColl, PL_BLOCK, IsuDate, TxtKeyword.Text.Trim(), ChannelType);
                    if (!pr.Successed)
                    {
                        MessageBox.Show("GetZtbPlanOfPMByKeyword error :" + pr.ErrorMessage);
                    }
                    if (!string.IsNullOrEmpty(FreeFlag))
                    {
                        var data  = planColl.Where(item => item.FREE_FLG == FreeFlag).OrderBy(item => item.PL_BLOCK + item.PL_CODE + item.PL_CODE2).ToArray();
                        planColl = new NewBisPASvcRef.ZTB_PLAN_PM_COLLECTION();
                        planColl.AddRange(data);
                    }
                    else
                    {
                        var data = planColl.OrderBy(item => item.PL_BLOCK + item.PL_CODE + item.PL_CODE2);
                        planColl = new NewBisPASvcRef.ZTB_PLAN_PM_COLLECTION();
                        planColl.AddRange(data);
                    }
                    _PlanSearchList = planColl;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("GetZtbPlanOfPMByKeyword error :" + ex.Message);

                }
            }
            if (_PlanSearchList != null && _PlanSearchList.Any())
            {
                dgvPlanSearch.DataSource = _PlanSearchList;
                int rowNo = 0;
                foreach (DataGridViewRow dr in dgvPlanSearch.Rows)
                {
                    rowNo = rowNo + 1;
                    dr.Cells["planRowNo"].Value = rowNo.ToString();
                }
            }
            else
            {
                MessageBox.Show("ไม่พบชื่อแบบประกันที่ท่านต้องการ");
                return;
            }
        }

        private void dgvPlanSearch_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if ((e.RowIndex > -1) && (e.ColumnIndex == 1))
            {
                try
                {
                    NewBisPASvcRef.ZTB_PLAN_PM planObj = new NewBisPASvcRef.ZTB_PLAN_PM();
                    PlanResult = _PlanSearchList[e.RowIndex];
                    Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }
            }
        }

        private void PlanSearchForm_Load(object sender, EventArgs e)
        {
            dgvPlanSearch.AutoGenerateColumns = false;
            dgvPlanSearch.RowHeadersVisible = false;
            dgvPlanSearch.DataSource = null;
            TxtKeyword.Text = "";
            TxtKeyword.Focus();
        }
    }
}
