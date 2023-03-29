using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NewBisPA.ReportForm;

namespace NewBisPA
{
    public partial class frmUPaymentAuthority : Form
    {
        public string chkUserAppr = null;
        public decimal PremiumApprove = 0;
        private string UserApprove = null;
        private decimal RequestPremium = 0;
        private string UserLogin = null;
        public bool isApproved = false;
        public frmUPaymentAuthority(string pUserAppr, decimal pAut_premium, string pUser_Login)
        {
            UserApprove = pUserAppr;
            RequestPremium = pAut_premium;
            UserLogin = pUser_Login;
            InitializeComponent();
        }

        private void frmUPaymentAuthority_Load(object sender, EventArgs e)
        {
            //txtUserAppr.Text = UserAppr_in;

            CenterSvcRef.ProcessResult pr = new CenterSvcRef.ProcessResult();
            CenterSvcRef.CenterServiceClient cn = new CenterSvcRef.CenterServiceClient();

            string OldUser;
            string UserName;

            using (CenterSvcRef.CenterServiceClient CenterClient = new CenterSvcRef.CenterServiceClient())
            {
                try
                {
                    CenterSvcRef.User UserColl = new CenterSvcRef.User();
                    UserColl = CenterClient.getUser(out pr, UserLogin);
                    if (UserColl != null)
                    {
                        OldUser = UserColl.OldUserId;
                        UserName = UserColl.firstName.ToString() + " " + UserColl.lastName.ToString();

                        userFullname.Text = UserName;
                        DataTable userlist = new DataTable();
                        userlist.Columns.Add(new DataColumn("Display", typeof(string)));
                        userlist.Columns.Add(new DataColumn("Id", typeof(string)));
                        userlist.Rows.Add(userlist.NewRow());
                        userlist.Rows[0][0] = UserName;
                        userlist.Rows[0][1] = OldUser;
                        userlist.Rows.Add(userlist.NewRow());
                        userlist.Rows[1][0] = "อรนุช  สำราญฤทธิ์";
                        userlist.Rows[1][1] = "890199";
                        userlist.Rows.Add(userlist.NewRow());

                        if (OldUser != "940869")
                        {
                            userlist.Rows[2][0] = "ดัษณี  เลิศเจริญชัยกุล";
                            userlist.Rows[2][1] = "940869";
                            userlist.Rows.Add(userlist.NewRow());
                        }


                        //cmbUserAppr.DataSource = userlist;
                        //cmbUserAppr.DisplayMember = "Display";
                        //cmbUserAppr.ValueMember = "Id";
                        //cmbUserAppr.SelectedValue = UserApprove;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                if (CenterClient.State != System.ServiceModel.CommunicationState.Closed)
                {
                    CenterClient.Close();
                }

            }
            txtPasswordAppr.Focus();
        }

        //private void frmUPaymentAuthority_Load(object sender, EventArgs e)
        //{
        //    //txtUserAppr.Text = UserAppr_in;

        //    NewBISSvcRef.ProcessResult pr = new NewBISSvcRef.ProcessResult();
        //    NewBISSvcRef.ZTB_USER[] ztbUsers = null;
        //    NewBISSvcRef.ZTB_USER ztbUser = new NewBISSvcRef.ZTB_USER();
        //    using (NewBISSvcRef.NewBISSvcClient client = new NewBISSvcRef.NewBISSvcClient())
        //    {
        //        try
        //        {
        //            string[] usid = new string[1];
        //            usid[0] = UserAppr_in;
        //            client.GetZTB_USER(out ztbUsers, usid, null);
        //            if (ztbUsers != null)
        //            {
        //                ztbUser = ztbUsers[0];
        //                UserName = ztbUser.NAME.ToString() + "  " + ztbUser.SURNAME.ToString();

        //                DataTable userlist = new DataTable();
        //                userlist.Columns.Add(new DataColumn("Display", typeof(string)));
        //                userlist.Columns.Add(new DataColumn("Id", typeof(string)));
        //                userlist.Rows.Add(userlist.NewRow());
        //                userlist.Rows[0][0] = UserName;
        //                userlist.Rows[0][1] = UserAppr_in;
        //                userlist.Rows.Add(userlist.NewRow());
        //                userlist.Rows[1][0] = "ฉันทนา  วิมุกตานนท์";
        //                userlist.Rows[1][1] = "000001";
        //                userlist.Rows.Add(userlist.NewRow());
        //                cmbUserAppr.DataSource = userlist;
        //                cmbUserAppr.DisplayMember = "Display";
        //                cmbUserAppr.ValueMember = "Id";
        //                cmbUserAppr.SelectedValue = UserAppr_in;                      
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            throw ex;
        //        }
        //        if (client.State != System.ServiceModel.CommunicationState.Closed)
        //        {
        //            client.Close();
        //        }
        //    }

        //    txtPasswordAppr.Focus();
        //}
        private void frmUPaymentAuthority_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (isApproved)
            {
                PremiumApprove = RequestPremium;
            }
            chkUserAppr = UserApprove;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {

            if (txtPasswordAppr.Text == "")
            {
                MessageBox.Show("กรุณาใส่รหัสผ่านเพื่อยืนยันสิทธิ์การอนุมัติ");
                txtPasswordAppr.Focus();
            }
            else
            {
                NewBISSvcRef.ProcessResult pr = new NewBISSvcRef.ProcessResult();
                using (NewBISSvcRef.NewBISSvcClient client = new NewBISSvcRef.NewBISSvcClient())
                {
                    try
                    {
                        var usrerApprove = new string[] { "000044", "000445", "000718", "000737", "000421" };
                        if (usrerApprove.Contains(UserLogin))
                        {


                            NewBISSvcRef.PS_AUTHORIZATION obj = new NewBISSvcRef.PS_AUTHORIZATION();
                            obj = client.GetPS_Authority(out pr, "C3", UserLogin);
                            if (obj.PAUT_PWD == txtPasswordAppr.Text.ToUpper())
                            {

                                isApproved = true;
                                chkUserAppr = UserLogin;
                                PremiumApprove = Convert.ToDecimal(obj.PAUT_AMT);
                                this.Close();
                                //  ChkAut_premium = Convert.ToDecimal(obj.PAUT_AMT);
                                //this.Close();
                                //MessageBox.Show(obj.PAUT_AMT.ToString());
                            }
                            else
                            {
                                MessageBox.Show("รหัสผ่านไม่ถูกต้อง");
                                txtPasswordAppr.Focus();
                            }
                        }
                        MessageBox.Show("ไม่มีสิทธิ์อนุมัติ");
                        this.Close();

                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    if (client.State != System.ServiceModel.CommunicationState.Closed)
                    {
                        client.Close();
                    }

                }

                //var password = new string[] { "su@78941", "nop@99999", "aew%2506kl", "ch@1111an" };
                //if (password.Contains(txtPasswordAppr.Text)
                //    )
                //{
                //    isApproved = true;
                //    chkUserAppr = UserLogin;
                //    PremiumApprove = 5000000;
                //    this.Close();
                //    //MessageBox.Show(obj.PAUT_AMT.ToString());
                //}
                //else
                //{
                //    MessageBox.Show("รหัสผ่านไม่ถูกต้อง");
                //    txtPasswordAppr.Focus();
                //}

            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
