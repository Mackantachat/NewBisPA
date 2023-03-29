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
    public partial class SummaryDetailForm : UserControl
    {
        public SummaryDetailForm()
        {
            InitializeComponent();
        }

        public TextBox txtSummaryDetailListSeq
        {
            get { return txtSeq; }
            set { txtSeq = value; }
        }
        public TextBox txtSummaryDetailListStatus
        {
            get { return txtStatusDesc; }
            set { txtStatusDesc = value; }
        }
        public TextBox txtSummaryDetailList
        {
            get { return txtDetail; }
            set { txtDetail = value; }
        }
        public Button btnSummaryDetailListEdit
        {
            get { return btnEdit; }
            set { btnEdit = value; }
        }
        public Button btnSummaryDetailListDelete
        {
            get { return btnDelete; }
            set { btnDelete = value; }
        }
        public TextBox txtSummaryUpdID
        {
            get { return txtUpdID; }
            set { txtUpdID = value; }
        }
    }
}
