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
    public partial class FormConcatOldAddress : Form
    {
        public FormConcatOldAddress()
        {
            InitializeComponent();
        }

        private bool _VERIFY_FLG;

        public bool VERIFY_FLG
        {
            get { return _VERIFY_FLG; }
            set { _VERIFY_FLG = value; }
        }

        private NewBisPASvcRef.U_OADDRESS _OADDRESS;

        public NewBisPASvcRef.U_OADDRESS OADDRESS
        {
            get { return _OADDRESS; }
            set { _OADDRESS = value; }
        }

        private void FormConcatOldAddress_Load(object sender, EventArgs e)
        {
            try
            {
                VERIFY_FLG = false;
                txtAddress1.Text = OADDRESS.ADDRESS1;
                txtAddress2.Text = OADDRESS.ADDRESS2;

                if (txtAddress1.Text.Length > 55)
                {
                    txtAddress1.BackColor = Color.FromArgb(255, 192, 192);
                }
                else
                {
                    txtAddress1.BackColor = Color.White;
                }

                if (txtAddress2.Text.Length > 45)
                {
                    txtAddress2.BackColor = Color.FromArgb(255, 192, 192);
                }
                else
                {
                    txtAddress2.BackColor = Color.White;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void btnAddress_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtAddress1.Text.Length > 55)
                {
                    txtAddress1.BackColor = Color.FromArgb(255, 192, 192);
                }
                else
                {
                    txtAddress1.BackColor = Color.White;
                }

                if (txtAddress2.Text.Length > 45)
                {
                    txtAddress2.BackColor = Color.FromArgb(255, 192, 192);
                }
                else
                {
                    txtAddress2.BackColor = Color.White;
                }

                int adrLen1 = 0;
                int adrLen2 = 0;

                adrLen1 = txtAddress1.Text.Length;
                adrLen2 = txtAddress2.Text.Length;

                if (adrLen1 <= 55 && adrLen2 <= 45)
                {
                    VERIFY_FLG = true;
                    OADDRESS = new NewBisPASvcRef.U_OADDRESS();
                    OADDRESS.ADDRESS1 = txtAddress1.Text;
                    OADDRESS.ADDRESS2 = txtAddress2.Text;
                    MessageBox.Show("ข้อมูลที่อยู่ถูกต้อง");
                }
                else
                {
                    VERIFY_FLG = false;
                    throw new Exception("ข้อมูลที่อยู่ยังมีความยาวเกินอยู่  ที่อยู่ 1 มีความยาว " + adrLen1.ToString() + " ตัวอักษร  ที่อยู่ 2 มีความยาว " + adrLen2.ToString() + " ตัวอักษร");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void FormConcatOldAddress_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (VERIFY_FLG == true)
                {
                    e.Cancel = false;
                }
                else
                {
                    e.Cancel = true;
                    throw new Exception("กรุณาคลิกปุ่มตรวจสอบเพื่อทำการตรวจสอบข้อมูลก่อน");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void txtAddress1_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (txtAddress1.Text != "")
                {
                    if (txtAddress1.Text.Length > 55)
                    {
                        VERIFY_FLG = false;
                        txtAddress1.BackColor = Color.FromArgb(255, 192, 192);
                    }
                    else
                    {
                        txtAddress1.BackColor = Color.White;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void txtAddress2_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (txtAddress2.Text != "")
                {
                    if (txtAddress2.Text.Length > 45)
                    {
                        VERIFY_FLG = false;
                        txtAddress2.BackColor = Color.FromArgb(255, 192, 192);
                    }
                    else
                    {
                        txtAddress2.BackColor = Color.White;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }


    }
}
