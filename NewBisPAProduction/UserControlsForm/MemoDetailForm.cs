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
    public partial class MemoDetailForm : UserControl
    {
        public MemoDetailForm()
        {
            InitializeComponent();
        }
        public TextBox txtMoDetailListNo
        {
            get { return txtMemoDetailListNo; }
            set { txtMemoDetailListNo = value; }
        }
        public TextBox txtMoDetailListPendCode
        {
            get { return txtMemoDetailListPendCode; }
            set { txtMemoDetailListPendCode = value; }
        }
        public TextBox txtMoDetailListPendCodeDesc
        {
            get { return txtMemoDetailListPendCodeDesc; }
            set { txtMemoDetailListPendCodeDesc = value; }
        }
        public TextBox txtMoDetailListPendStatus
        {
            get { return txtMemoDetailListPendStatus; }
            set { txtMemoDetailListPendStatus = value; }
        }
        public TextBox txtMoDetailListPendStatusDate
        {
            get { return txtMemoDetailListPendStatusDate; }
            set { txtMemoDetailListPendStatusDate = value; }
        }
        public TextBox txtMoDetailListDesc
        {
            get { return txtMemoDetailListDesc; }
            set { txtMemoDetailListDesc = value; }
        }
        public TextBox txtMoDetailListDocument
        {
            get { return txtMemoDetailListDocument; }
            set { txtMemoDetailListDocument = value; }
        }
        public Button btnMoDetailListEdit
        {
            get { return btnMemoDetailListEdit; }
            set { btnMemoDetailListEdit = value; }
        }
        public Button btnMoDetailListCancel
        {
            get { return btnMemoDetailListCancel; }
            set { btnMemoDetailListCancel = value; }
        }
    }
}
