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
    public partial class MemoMainForm : UserControl
    {
        public MemoMainForm()
        {
            InitializeComponent();
        }

        public TextBox txtMoListNO
        {
            get { return txtMemoListNO; }
            set { txtMemoListNO = value; }
        }
        public TextBox txtMoListTrnDate
        {
            get { return txtMemoListTrnDate; }
            set { txtMemoListTrnDate = value; }
        }
        public TextBox txtMoListLimitDate
        {
            get { return txtMemoListLimitDate; }
            set { txtMemoListLimitDate = value; }
        }
        public TextBox txtMoListLetter
        {
            get { return txtMemoListLetter; }
            set { txtMemoListLetter = value; }
        }
        public TextBox txtMoListProcessEndDesc
        {
            get { return txtMemoListProcessEndDesc; }
            set { txtMemoListProcessEndDesc = value; }
        }
        public TextBox txtMoListTmnDesc
        {
            get { return txtMemoListTmnDesc; }
            set { txtMemoListTmnDesc = value; }
        }
        public Button btnMoListDetail
        {
            get { return btnMemoListDetail; }
            set { btnMemoListDetail = value; }
        }
        public Button btnMoListEdit
        {
            get { return btnMemoListEdit; }
            set { btnMemoListEdit = value; }
        }
        public Button btnMoListCancel
        {
            get { return btnMemoListCancel; }
            set { btnMemoListCancel = value; }
        }


        public Button ButtonRequestDocumentFromBranch
        {
            get { return BtnRequestDocumentFromBranch; }
            set { BtnRequestDocumentFromBranch = value; }
        }

        private void btnMemoListCancel_Click(object sender, EventArgs e)
        {

        }

        private void BtnRequestDocumentFromBranch_Click(object sender, EventArgs e)
        {

        }
    }
}
