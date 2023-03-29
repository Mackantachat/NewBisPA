using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NewBisPA.UserControlsForm
{
    public partial class HistoryForm : UserControl
    {
        public HistoryForm()
        {
            InitializeComponent();
        }
        public TextBox txtHisSeq
        {
            get { return txtSeq; }
            set { txtSeq = value; }
        }
        public TextBox txtHisStatus
        {
            get { return txtStatus; }
            set { txtStatus = value; }
        }
        public TextBox txtHisSubStatus
        {
            get { return txtSubStatus; }
            set { txtSubStatus = value; }
        }
        public TextBox txtHisStatusCause
        {
            get { return txtStatusCause; }
            set { txtStatusCause = value; }
        }
        public TextBox txtHisUndID
        {
            get { return txtUndID; }
            set { txtUndID = value; }
        }
        public TextBox txtHisFSystemDate
        {
            get { return txtFSystemDate; }
            set { txtFSystemDate = value; }
        }
        public TextBox txtHisUpdID
        {
            get { return txtUpdID; }
            set { txtUpdID = value; }
        }
        public TextBox txtHisLetterNo
        {
            get { return txtLetterNo; }
            set { txtLetterNo = value; }
        }
        public Button btnHisDetail
        {
            get { return btnDetail; }
            set { btnDetail = value; }
        }
    }
}
