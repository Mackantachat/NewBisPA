using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ITUtility;
using NewBisPA.Library;

namespace NewBisPA
{
    public partial class FormCsSuspenseDetail : Form
    {
        private String _CHANNEL_TYPE;

        public String CHANNEL_TYPE
        {
            get { return _CHANNEL_TYPE; }
            set { _CHANNEL_TYPE = value; }
        }
        private String _APP_NO;

        public String APP_NO
        {
            get { return _APP_NO; }
            set { _APP_NO = value; }
        }

        private Decimal? _PREMIUM;

        public Decimal? PREMIUM
        {
            get { return _PREMIUM; }
            set { _PREMIUM = value; }
        }

        private String _STATUS;

        public String STATUS
        {
            get { return _STATUS; }
            set { _STATUS = value; }
        }

        private bool _IsEndProcess;

        public bool IsEndProcess
        {
            get { return _IsEndProcess; }
            set { _IsEndProcess = value; }
        }


        public FormCsSuspenseDetail()
        {
            InitializeComponent();
        }

        private void FormCsSuspenseDetail_Load(object sender, EventArgs e)
        {
            try
            {
                dgvCsSuspense.AutoGenerateColumns = false;
                dgvCsSuspense.RowHeadersVisible = false;
                dgvCsSuspense.DataSource = null;
                NewBISSvcRef.ProcessResult pr = new NewBISSvcRef.ProcessResult();
                NewBISSvcRef.CS_SUSPENSE_COLLECTION suspenseColl = new NewBISSvcRef.CS_SUSPENSE_COLLECTION();
                Decimal sumSuspense = 0;


                var renewRepository = new RenewalRepository();
                var renewInfo = renewRepository.GetRenewalPaymentTransection(APP_NO, CHANNEL_TYPE, IsEndProcess, NewBISSvcRef.ERenewStatusCase.RewnewOrAllowToGrant);
                if (renewInfo != null)
                {

                    sumSuspense = renewInfo.PremiumReceive;

                    if (renewInfo.Transections != null && renewInfo.Transections.Any())
                    {
                        var tempListSuspense = new NewBISSvcRef.CS_SUSPENSE_COLLECTION();
                        tempListSuspense.AddRange(renewInfo.Transections.Select(tran => new NewBISSvcRef.CS_SUSPENSE
                        {
                            CSP_SP_NO = tran.CTPA_ID.Value.ToString(),
                            CSP_CALPREM = tran.AMOUNT,
                            CSP_PAYIN_DATE = tran.PAY_DT,
                            CSP_OPTION = tran.PAY_OPTION,
                            CSP_DISABLED = tran.TMN_FLG == null ? "N" : tran.TMN_FLG.ToString(),
                            CSP_CLEARING_STATUS = "Y",
                            CSP_FEE = tran.PREMIUM_FEE
                        }).ToList());

                        suspenseColl = tempListSuspense;
                    }
                }
                else
                {
                    using (NewBISSvcRef.NewBISSvcClient client = new NewBISSvcRef.NewBISSvcClient())
                    {
                        suspenseColl = client.GetCsSuupenseAllByAppNo(out pr, APP_NO, CHANNEL_TYPE);
                        //suspenseColl = client.GetCsSuupenseAllByAppNo(out pr, "B043696", "OD");
                        if (pr.Successed == false)
                        {
                            throw new Exception(pr.ErrorMessage);
                        }
                        sumSuspense = client.GetCalPremiumByAppNo(out pr, APP_NO, CHANNEL_TYPE, PREMIUM, STATUS);
                        //sumSuspense = client.GetCalPremiumByAppNo(out pr, "B043696", "OD", PREMIUM, STATUS);
                        if (pr.Successed == false)
                        {
                            throw new Exception(pr.ErrorMessage);
                        }
                    }
                }

                txtTotalSuspense.Text = sumSuspense.ToString("n2");

                if (suspenseColl != null && suspenseColl.Count() > 0)
                {
                    dgvCsSuspense.DataSource = suspenseColl;
                    int i = 0;
                    foreach (DataGridViewRow dr in dgvCsSuspense.Rows)
                    {
                        if (dr.DataBoundItem is NewBISSvcRef.CS_SUSPENSE)
                        {
                            NewBISSvcRef.CS_SUSPENSE obj = (NewBISSvcRef.CS_SUSPENSE)dr.DataBoundItem;
                            i = i + 1;
                            dr.Cells["CSSNo"].Value = i.ToString();
                            dr.Cells["CSSSuspenseNo"].Value = obj.CSP_SP_NO;
                            dr.Cells["CSSSuspensePrem"].Value = obj.CSP_CALPREM == null ? "0" : obj.CSP_CALPREM.Value.ToString("n2");
                            dr.Cells["CSSSuspenseDate"].Value = obj.CSP_PAYIN_DATE == null ? "" : Utility.dateTimeToString(obj.CSP_PAYIN_DATE.Value, "dd/MM/yyyy", "BU");
                            dr.Cells["CSSSuspensePayBy"].Value = obj.CSP_OPTION;
                            if (obj.CSP_DISABLED == "Y")
                            {
                                dr.Cells["CSSSuspenseCancel"].Value = "ยกเลิก";
                            }
                            else
                            {
                                dr.Cells["CSSSuspenseCancel"].Value = "ใช้งาน";
                            }

                            if (obj.CSP_CLEARING_STATUS == "Y")
                            {
                                dr.Cells["CSSSuspenseClearing"].Value = "Yes";
                            }
                            else if (obj.CSP_CLEARING_STATUS == "N")
                            {
                                dr.Cells["CSSSuspenseClearing"].Value = "No";
                            }
                            else if (obj.CSP_CLEARING_STATUS == "W")
                            {
                                dr.Cells["CSSSuspenseClearing"].Value = "Wait";
                            }
                            dr.Cells["CSSSuspenseFee"].Value = obj.CSP_FEE == null ? "0" : obj.CSP_FEE.Value.ToString("n2");

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
