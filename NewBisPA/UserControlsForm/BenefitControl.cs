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
    public partial class BenefitControl : UserControl
    {
        public BenefitControl()
        {
            InitializeComponent();
        }

        public TextBox txtBnfNo
        {
            get { return txtBenefitNo; }
            set { txtBenefitNo = value; }
        }
        public TextBox txtBnfType
        {
            get { return txtBenefitType; }
            set { txtBenefitType = value; }
        }
        public TextBox txtSpFlg
        {
            get { return txtSpouseFlg; }
            set { txtSpouseFlg = value; }
        }
        public TextBox txtBnfPrename
        {
            get { return txtBenefitPrename; }
            set { txtBenefitPrename = value; }
        }
        public TextBox txtBnfName
        {
            get { return txtBenefitName; }
            set { txtBenefitName = value; }
        }
        public TextBox txtBnfSurname
        {
            get { return txtBenefitSurname; }
            set { txtBenefitSurname = value; }
        }
        public TextBox txtBnfRelation
        {
            get { return txtBenefitRelation; }
            set { txtBenefitRelation = value; }
        }
        public TextBox txtBnfGainPercent
        {
            get { return txtBenefitGainPercent; }
            set { txtBenefitGainPercent = value; }
        }
        public TextBox txtBnfClear
        {
            get { return txtBenefitClear; }
            set { txtBenefitClear = value; }
        }
        public TextBox txtBnfStatus
        {
            get { return txtBenefitStatus; }
            set { txtBenefitStatus = value; }
        }
        public TextBox txtBnfMessage
        {
            get { return txtBenefitMessage; }
            set { txtBenefitMessage = value; }
        }
        public Button btnBnfEdit
        {
            get { return btnBenefitEdit; }
            set { btnBenefitEdit = value; }
        }
        public TextBox txtBnfID
        {
            get { return txtBenefitID; }
            set { txtBenefitID = value; }
        }
        public TextBox txtBnfTypeCode
        {
            get { return txtBenefitTypeCode; }
            set { txtBenefitTypeCode = value; }
        }
        public TextBox txtBnfClearFlag
        {
            get { return txtBenefitClearFlag; }
            set { txtBenefitClearFlag = value; }
        }
        public TextBox txtBnfTmn
        {
            get { return txtBenefitTmn; }
            set { txtBenefitTmn = value; }
        }
    }
}
