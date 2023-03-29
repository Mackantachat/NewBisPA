using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NewBisPA
{
    public partial class ShowOldAdrressByNameIDForm : Form
    {

        private NewBisPASvcRef.P_ADDRESS_ID_COLLECTION _P_ADDRESS_ID_COLL;

        public NewBisPASvcRef.P_ADDRESS_ID_COLLECTION P_ADDRESS_ID_COLL
        {
            get { return _P_ADDRESS_ID_COLL; }
            set { _P_ADDRESS_ID_COLL = value; }
        }

        private NewBisPASvcRef.P_ADDRESS_ID _P_ADDRESS_ID_OBJ;

        public NewBisPASvcRef.P_ADDRESS_ID P_ADDRESS_ID_OBJ
        {
            get { return _P_ADDRESS_ID_OBJ; }
            set { _P_ADDRESS_ID_OBJ = value; }
        }

        private long? _ADDRESS_ID;

        public long? ADDRESS_ID
        {
            get { return _ADDRESS_ID; }
            set { _ADDRESS_ID = value; }
        }

        public ShowOldAdrressByNameIDForm()
        {
            InitializeComponent();
        }

        private void ShowOldAdrressByNameIDForm_Load(object sender, EventArgs e)
        {
            try
            {
                if (P_ADDRESS_ID_COLL != null && P_ADDRESS_ID_COLL.Count() > 0)
                {
                    while (panelOldAddress.Controls.Count > 0)
                    {
                        panelOldAddress.Controls.RemoveAt(0);
                    }

                    int i = 0;
                    foreach (NewBisPASvcRef.P_ADDRESS_ID obj in P_ADDRESS_ID_COLL)
                    {
                        UserControlsForm.OldAddressDataForm oldAddressDataFormList = new UserControlsForm.OldAddressDataForm();
                        oldAddressDataFormList.Name = "oldAddressDataFormList";
                        oldAddressDataFormList.Location = new Point(0, 0 + (i * 145));
                        oldAddressDataFormList.Size = new Size(730, 140);
                        oldAddressDataFormList.Tag = obj;

                        oldAddressDataFormList.oldAddressAddressID.Text = obj.ADDRESS_ID == null ? "" : obj.ADDRESS_ID.Value.ToString();
                        if (ADDRESS_ID == obj.ADDRESS_ID)
                        {
                            oldAddressDataFormList.oldAddressSel.Checked = true;
                        }
                        else
                        {
                            oldAddressDataFormList.oldAddressSel.Checked = false;
                        }

                        oldAddressDataFormList.oldAddressNo.Text = "รายการที่ " + (i + 1).ToString();

                        oldAddressDataFormList.oldAddressName.Text = obj.ADDRESS_NAME;
                        oldAddressDataFormList.oldAddressNumber.Text = obj.ADDRESS_NUMBER;
                        oldAddressDataFormList.oldAddressMooh.Text = obj.MOOH;
                        oldAddressDataFormList.oldAddressSoi.Text = obj.SOI;
                        oldAddressDataFormList.oldAddressRoad.Text = obj.ROAD;
                        oldAddressDataFormList.oldAddressTambol.Text = obj.TAMBOL;
                        oldAddressDataFormList.oldAddressAmphur.Text = obj.AMPHUR;
                        oldAddressDataFormList.oldAddressProvince.Text = obj.PROVINCE;
                        oldAddressDataFormList.oldAddressZipcode.Text = obj.ZIP_CODE;
                        oldAddressDataFormList.oldAddressPhoneNumber.Text = obj.PHONE_NUMBER;
                        oldAddressDataFormList.oldAddressSel.Tag = obj;
                        //oldAddressDataFormList.oldAddressSel.CheckChanged += new EventHandler(oldAddressSel_CheckChanged);
                        oldAddressDataFormList.oldAddressSel.Click += new EventHandler(oldAddressSel_Click);

                        panelOldAddress.Controls.Add(oldAddressDataFormList);

                        i = i + 1;
                    }

                    lblTotalAmount.Text = "จำนวนรายการทั้งหมด " + i.ToString() + " รายการ";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        void oldAddressSel_Click(object sender, EventArgs e)
        {
            try
            {
                CheckBox chkSel = (CheckBox)sender;

                NewBisPASvcRef.P_ADDRESS_ID obj = new NewBisPASvcRef.P_ADDRESS_ID();
                obj = (NewBisPASvcRef.P_ADDRESS_ID)chkSel.Tag;

                foreach (Control ctl in panelOldAddress.Controls)
                {
                    if (ctl.Name == "oldAddressDataFormList")
                    {
                        UserControlsForm.OldAddressDataForm oldAddressDataFormList = (UserControlsForm.OldAddressDataForm)ctl;
                        NewBisPASvcRef.P_ADDRESS_ID objCtl = new NewBisPASvcRef.P_ADDRESS_ID();
                        objCtl = (NewBisPASvcRef.P_ADDRESS_ID)oldAddressDataFormList.Tag;
                        if (objCtl.ADDRESS_ID != obj.ADDRESS_ID)
                        {
                            oldAddressDataFormList.oldAddressSel.Checked = false;
                        }

                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }

        }

        private void ShowOldAdrressByNameIDForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                P_ADDRESS_ID_OBJ = new NewBisPASvcRef.P_ADDRESS_ID();
                foreach (Control ctl in panelOldAddress.Controls)
                {
                    if (ctl.Name == "oldAddressDataFormList")
                    {
                        UserControlsForm.OldAddressDataForm oldAddressDataFormList = (UserControlsForm.OldAddressDataForm)ctl;
                        NewBisPASvcRef.P_ADDRESS_ID objCtl = new NewBisPASvcRef.P_ADDRESS_ID();
                        objCtl = (NewBisPASvcRef.P_ADDRESS_ID)oldAddressDataFormList.Tag;

                        if (oldAddressDataFormList.oldAddressSel.Checked == true)
                        {
                            P_ADDRESS_ID_OBJ = objCtl;
                            break;
                        }
                    }
                }
                e.Cancel = false;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }
    }
}
