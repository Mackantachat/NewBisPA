using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Drawing.Printing;

namespace NewBisPA
{
    public partial class XtraReport_KwanBuaLung_Close : DevExpress.XtraReports.UI.XtraReport
    {
        public XtraReport_KwanBuaLung_Close(NewBISSvcRef.REPORT_KWANBUALUNG_CLOSE[] dataObject, DateTime? ST, DateTime? EN, string TITLE, string POLICY_HOLDING)
        {
            InitializeComponent();
            this.DataSource = dataObject;
            xrLabel_Title.Text += TITLE + " (" + POLICY_HOLDING + ")";
            Detail.BeforePrint += new PrintEventHandler(Detail_BeforePrint);
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
            xrTableCell_Rownum.Text = index.ToString("N0");
            index++;
        }

    }
}
