using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Drawing.Printing;

namespace NewBisPA
{
    public partial class XtraReport_KwanBuaLung_Region : DevExpress.XtraReports.UI.XtraReport
    {
        public XtraReport_KwanBuaLung_Region(NewBISSvcRef.REPORT_KWANBUALUNG_REGION[] dataObject, DateTime? ST, DateTime? EN, string TITEL, string POLICY_HOLDING)
        {
            InitializeComponent();
            this.DataSource = dataObject;
            Detail.BeforePrint += new PrintEventHandler(Detail_BeforePrint);
            xrLabel_Title.Text += TITEL + " (" + POLICY_HOLDING + ")";
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
        int index = 1;
        private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrLabel_Index.Text = index.ToString("N0");
            index++;
        }
    }
}
