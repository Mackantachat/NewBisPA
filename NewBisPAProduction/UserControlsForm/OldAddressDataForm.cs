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
    public partial class OldAddressDataForm : UserControl
    {
        public TextBox oldAddressAddressID
        {
            get { return txtAddressID; }
            set { txtAddressID = value; }
        }
        public CheckBox oldAddressSel
        {
            get { return chkOldAddressSel; }
            set { chkOldAddressSel = value; }
        }
        public Label oldAddressNo
        {
            get { return lblOldAddressNo; }
            set { lblOldAddressNo = value; }
        }
        public TextBox oldAddressName
        {
            get { return txtOldAddressName; }
            set { txtOldAddressName = value; }
        }
        public TextBox oldAddressNumber
        {
            get { return txtOldAddressNumber; }
            set { txtOldAddressNumber = value; }
        }
        public TextBox oldAddressMooh
        {
            get { return txtOldMooh; }
            set { txtOldMooh = value; }
        }
        public TextBox oldAddressSoi
        {
            get { return txtOldSoi; }
            set { txtOldSoi = value; }
        }
        public TextBox oldAddressRoad
        {
            get { return txtOldRoad; }
            set { txtOldRoad = value; }
        }
        public TextBox oldAddressTambol
        {
            get { return txtOldTambol; }
            set { txtOldTambol = value; }
        }
        public TextBox oldAddressAmphur
        {
            get { return txtOldAmphur; }
            set { txtOldAmphur = value; }
        }
        public TextBox oldAddressProvince
        {
            get { return txtOldProvince; }
            set { txtOldProvince = value; }
        }
        public TextBox oldAddressZipcode
        {
            get { return txtOldZipcode; }
            set { txtOldZipcode = value; }
        }
        public TextBox oldAddressPhoneNumber
        {
            get { return txtOldPhoneNumber; }
            set { txtOldPhoneNumber = value; }
        }

        public OldAddressDataForm()
        {
            InitializeComponent();
        }

    }
}
