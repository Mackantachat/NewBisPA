using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;

namespace NewBisPA
{
    public partial class XtraReport_KwanBuaLung_Policy : DevExpress.XtraReports.UI.XtraReport
    {
        public XtraReport_KwanBuaLung_Policy(NewBISSvcRef.REPORT_KWANBUALUNG_POLICY[] dataObject, DateTime? ST, DateTime? EN, string TITLE, string POLICY_HOLDING)
        {
            InitializeComponent();
            this.DataSource = dataObject.OrderBy(s=>s.POLICY);
            xrLabel_Title.Text += TITLE + " (" + POLICY_HOLDING + ")";
            if (ST != null && EN != null)
            {
                xrLabel_Date_Search.Text = "ช่วงวันที่ : " + ITUtility.Utility.dateTimeToString(ST.Value, "dd/MM/yyyy", "BU");
                xrLabel_Date_Search.Text += " - " + ITUtility.Utility.dateTimeToString(EN.Value, "dd/MM/yyyy", "BU");
            }
            else if (ST != null && EN == null)
            {
                xrLabel_Date_Search.Text = "วันที่ : " + ITUtility.Utility.dateTimeToString(ST.Value, "dd/MM/yyyy", "BU");
            }
        }

    }
}
