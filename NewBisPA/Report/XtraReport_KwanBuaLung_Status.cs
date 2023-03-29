using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Drawing.Printing;
using System.Linq;
using System.Collections.Generic;

namespace NewBisPA
{
    public partial class XtraReport_KwanBuaLung_Status : DevExpress.XtraReports.UI.XtraReport
    {
        NewBISSvcRef.REPORT_KWANBUALUNG_STATUS[] DATA_REPORT = null;
        List<NewBISSvcRef.REPORT_KWANBUALUNG_SUBSTATUS> SUB_STATUSs = new List<NewBISSvcRef.REPORT_KWANBUALUNG_SUBSTATUS>();
        public XtraReport_KwanBuaLung_Status(NewBISSvcRef.REPORT_KWANBUALUNG_STATUS[] dataObject, DateTime? DT_ST, DateTime? DT_EN)
        {
            InitializeComponent();
            this.DataSource = dataObject;
            DATA_REPORT = dataObject;
            if (DT_ST != null && DT_EN != null)
            {
                xrLabel_Date_Search.Text = "ช่วงวันที่ : " + ITUtility.Utility.dateTimeToString(DT_ST.Value, "dd/MM/yyyy", "BU");
                xrLabel_Date_Search.Text += " - " + ITUtility.Utility.dateTimeToString(DT_EN.Value, "dd/MM/yyyy", "BU");
            }
            else if (DT_ST != null && DT_EN == null)
            {
                xrLabel_Date_Search.Text = "วันที่ : " + ITUtility.Utility.dateTimeToString(DT_ST.Value, "dd/MM/yyyy", "BU");
            }
            Detail.BeforePrint += new PrintEventHandler(Detail_BeforePrint);
            DetailReport_SUBSTATUS.BeforePrint += new PrintEventHandler(DetailReport_SUBSTATUS_BeforePrint);
        }
        private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            DetailReport_SUBSTATUS.Visible = false; 
            var STATUS_NO = GetCurrentColumnValue("STATUS_NO");
            NewBISSvcRef.REPORT_KWANBUALUNG_STATUS TMP = DATA_REPORT.Where(s => s.STATUS_NO == STATUS_NO).SingleOrDefault();
            if (TMP != null && TMP.REPORT_KWANBUALUNG_SUBSTATUS_Collection != null && TMP.REPORT_KWANBUALUNG_SUBSTATUS_Collection.Count() > 0)
            {
                SUB_STATUSs.AddRange(TMP.REPORT_KWANBUALUNG_SUBSTATUS_Collection);
                DetailReport_SUBSTATUS.Visible = true;  
            }

        }
        private void DetailReport_SUBSTATUS_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            DetailReport_STATUS_CAUSE.Visible = false;
            var SUBSTATUS =DetailReport_SUBSTATUS.GetCurrentColumnValue("SUBSTATUS");
            NewBISSvcRef.REPORT_KWANBUALUNG_SUBSTATUS TMP = SUB_STATUSs.Where(s => s.SUBSTATUS == SUBSTATUS).SingleOrDefault();
            if (TMP != null && TMP.REPORT_KWANBUALUNG_STATUSCAUSE_Collection != null && TMP.REPORT_KWANBUALUNG_STATUSCAUSE_Collection.Count() > 0)
            {
                DetailReport_STATUS_CAUSE.Visible = true;
            }
        }
    }
}
