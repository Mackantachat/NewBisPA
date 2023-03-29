using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NewBisPA.ReportForm
{
    public partial class frmReportMODetailDoc : Form
    {
        private string _summaryId;
        public frmReportMODetailDoc(string summaryId)
        {
            InitializeComponent();
            _summaryId = summaryId;
        }

        private void frmReportMODetailDoc_Load(object sender, EventArgs e)
        {
            dtgDocshow.DataSource = null;
            dtgDocshow.AutoGenerateColumns = false;
            NewBISSvcRef.ProcessResult pr = new NewBISSvcRef.ProcessResult();
            NewBISSvcRef.REPORT_SHOWDETAILDOC_Collection ressearchColl = new NewBISSvcRef.REPORT_SHOWDETAILDOC_Collection();
            using (NewBISSvcRef.NewBISSvcClient client = new NewBISSvcRef.NewBISSvcClient())
            {
                try
                {
                    ressearchColl = client.GetShowDetailDocColl(out pr, _summaryId);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                if (client.State != System.ServiceModel.CommunicationState.Closed)
                {
                    client.Close();
                }
            }
            if (pr.Successed == true)
            {
                dtgDocshow.DataSource = ressearchColl;
                foreach (DataGridViewRow dr in dtgDocshow.Rows)
                {
                    if (dr.DataBoundItem is NewBISSvcRef.REPORT_SHOWDETAILDOC)
                    {
                        NewBISSvcRef.REPORT_SHOWDETAILDOC obj = (NewBISSvcRef.REPORT_SHOWDETAILDOC)dr.DataBoundItem;
                        dr.Cells["clmDescription"].Value = obj.Description;
                        dr.Cells["clmPrint_seq"].Value = obj.Print_seq;
                        dr.Cells["clmPrint_seq2"].Value = obj.Print_seq2;
                        dr.Cells["clmIso_code"].Value = obj.Iso_code;

                        if (obj.Print_seq2 == 1)
                        {
                            dr.DefaultCellStyle.BackColor = System.Drawing.Color.Beige;
                        }
                        else
                        {
                            dr.DefaultCellStyle.BackColor = System.Drawing.Color.MistyRose;
                        }
                    }                    
                }
            }
            dtgDocshow.ClearSelection();
        }
    }
}
