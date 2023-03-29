using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Collections;
using ITUtility;
using NewBisPA.Library.Resource;

namespace NewBisPA
{
    partial class ApplicationForm
    {
        private void SearchData()
        {
            string channelType = cmbChannelType.SelectedValue == null ? "" : cmbChannelType.SelectedValue.ToString();
            string WorkGroup = cmbWorkGroup.SelectedValue == null ? "" : cmbWorkGroup.SelectedValue.ToString();

            NewBisPASvcRef.ProcessResult pr = new NewBisPASvcRef.ProcessResult();
            PAR_PA_APPLICATION_ID = new NewBisPASvcRef.PA_APPLICATION_ID();
            PAR_U_ADDRESS_ID_COLL = null;
            PAR_U_SPOUSE_ID_COLL = null;
            PAR_BENEFIT_ID_COLL = new NewBisPASvcRef.U_BENEFIT_ID_COLLECTION();
            PAR_P_NAME_ID_COLL = new NewBisPASvcRef.P_NAME_ID_COLLECTION();
            PAR_P_PARENT_ID_COLL = new NewBisPASvcRef.P_PARENT_ID_COLLECTION();
            PAR_ADDRESS_ID_COLL = new NewBisPASvcRef.P_ADDRESS_ID_COLLECTION();
            PAR_W_UNDERWRITE_ID_COLL = new NewBisPASvcRef.W_UNDERWRITE_ID_Collection();
            PAR_U_APPLICATION_LOCK = new NewBisPASvcRef.U_APPLICATION_LOCK();
            PAR_CHECK_DATA_CUSTOMER = new NewBisPASvcRef.CHECK_DATA_CUSTOMER();
            PAR_CHECK_DATA_OTHER = new NewBisPASvcRef.CHECK_DATA_CUSTOMER();
            using (NewBisPASvcRef.NewBisPASvcClient client = new NewBisPASvcRef.NewBisPASvcClient())
            {
                pr = client.GetUApplicationIDOfPA(ref PAR_PA_APPLICATION_ID, ref PAR_U_ADDRESS_ID_COLL, ref PAR_U_SPOUSE_ID_COLL, ref PAR_BENEFIT_ID_COLL, ref PAR_P_NAME_ID_COLL, ref PAR_P_PARENT_ID_COLL, ref PAR_ADDRESS_ID_COLL, ref PAR_W_UNDERWRITE_ID_COLL, ref PAR_U_ACCOUNT_ID_COLL, ref PAR_U_APPLICATION_LOCK, ref PAR_CHECK_DATA_CUSTOMER, ref PAR_CHECK_DATA_OTHER, txtAppNo.Text.Trim(), channelType);
                if (pr.Successed == false)
                {
                    throw new Exception(pr.ErrorMessage);
                }
            }

            if (PAR_PA_APPLICATION_ID != null && PAR_PA_APPLICATION_ID.APP_ID != null)
            {

                var newbisFlag = PAR_PA_APPLICATION_ID.APP_ISIS?.NEWBIS ?? 'N';
                GetUApplicationID(PAR_PA_APPLICATION_ID);
                GetOldDataPNameIDEventSearch(PAR_PA_APPLICATION_ID);

                BankruptSvcRef.BankruptCollection bankruptColl = new BankruptSvcRef.BankruptCollection();

                using (BankruptSvcRef.BankruptServiceContractClient bankruptCollClient = new BankruptSvcRef.BankruptServiceContractClient())
                {
                    if (txtName.Text.Trim() != "" && txtSurname.Text.Trim() != "")
                    {
                        bankruptColl = bankruptCollClient.LoadBankrupt(txtIdcardNo.Text.Trim(), txtName.Text.Trim(), txtSurname.Text.Trim());

                        if (bankruptColl != null && bankruptColl.Count() > 0)
                        {
                            PAR_CHECK_DATA_CUSTOMER.BANKRUPT_DATA = "Y";
                        }
                        else
                        {
                            PAR_CHECK_DATA_CUSTOMER.BANKRUPT_DATA = "N";
                        }

                        if (PAR_U_SPOUSE_ID_COLL != null && PAR_U_SPOUSE_ID_COLL.Count() > 0)
                        {
                            BankruptSvcRef.BankruptCollection bankruptSpouseColl = new BankruptSvcRef.BankruptCollection();
                            bankruptSpouseColl = bankruptCollClient.LoadBankrupt(PAR_U_SPOUSE_ID_COLL[0].IDCARD_NO, PAR_U_SPOUSE_ID_COLL[0].NAME, PAR_U_SPOUSE_ID_COLL[0].SURNAME);
                            if (bankruptSpouseColl != null && bankruptSpouseColl.Count() > 0)
                            {
                                PAR_CHECK_DATA_OTHER.BANKRUPT_DATA = "Y";
                            }
                            else
                            {
                                PAR_CHECK_DATA_OTHER.BANKRUPT_DATA = "N";
                            }
                        }
                    }
                }
                string[] documentTracking = null;
                if (PAR_PA_APPLICATION_ID.DocumentTrackings != null && PAR_PA_APPLICATION_ID.DocumentTrackings.Any())
                {
                    documentTracking = PAR_PA_APPLICATION_ID.DocumentTrackings.Where(item => item.STATUS == SystemConstant.YesDBFlag).Select(item => item.DOC_CODE).ToArray();

                }
                txtBankrupCus.Visible = false;
                txtRemarkCus.Visible = false;
                txtBankrupSpouse.Visible = false;
                txtRemarkSpouse.Visible = false;
                txtBankrupCus.Text = "";
                txtRemarkCus.Text = "";
                txtBankrupSpouse.Text = "";
                txtRemarkSpouse.Text = "";

                int chkDataOfCustomer = 0;
                if (PAR_CHECK_DATA_CUSTOMER.CUSTOMER_DATA == "Y")
                {
                    chkDataOfCustomer = chkDataOfCustomer + 1;
                }
                if (PAR_CHECK_DATA_CUSTOMER.MIB_DATA == "Y")
                {
                    chkDataOfCustomer = chkDataOfCustomer + 1;
                }
                if (PAR_CHECK_DATA_CUSTOMER.P_AML_CTF_DATA == "Y")
                {
                    chkDataOfCustomer = chkDataOfCustomer + 1;
                }
                if (PAR_CHECK_DATA_CUSTOMER.BANKRUPT_DATA == "Y")
                {
                    chkDataOfCustomer = chkDataOfCustomer + 1;
                    txtBankrupCus.Visible = true;
                    txtBankrupCus.Text = "มีข้อมูลล้มละลายผู้เอาประกัน";
                }
                if (PAR_CHECK_DATA_CUSTOMER.REMARK_DATA == "Y")
                {
                    chkDataOfCustomer = chkDataOfCustomer + 1;
                    txtRemarkCus.Visible = true;
                    txtRemarkCus.Text = "ไม่รับชำระเบี้ยผู้เอาประกัน";
                }
                if (PAR_CHECK_DATA_CUSTOMER.RECEIVE_PREMIUM_DATA == "Y")
                {
                    chkDataOfCustomer = chkDataOfCustomer + 1;
                }
                if (PAR_CHECK_DATA_OTHER.CUSTOMER_DATA == "Y")
                {
                    chkDataOfCustomer = chkDataOfCustomer + 1;
                }
                if (PAR_CHECK_DATA_OTHER.MIB_DATA == "Y")
                {
                    chkDataOfCustomer = chkDataOfCustomer + 1;
                }
                if (PAR_CHECK_DATA_OTHER.P_AML_CTF_DATA == "Y")
                {
                    chkDataOfCustomer = chkDataOfCustomer + 1;
                }
                if (PAR_CHECK_DATA_OTHER.BANKRUPT_DATA == "Y")
                {
                    chkDataOfCustomer = chkDataOfCustomer + 1;
                    txtBankrupSpouse.Visible = true;
                    txtBankrupSpouse.Text = "มีข้อมูลล้มละลายคู่สมรส";
                }
                if (PAR_CHECK_DATA_OTHER.REMARK_DATA == "Y")
                {
                    chkDataOfCustomer = chkDataOfCustomer + 1;
                }
                if (PAR_CHECK_DATA_OTHER.RECEIVE_PREMIUM_DATA == "Y")
                {
                    chkDataOfCustomer = chkDataOfCustomer + 1;
                    txtRemarkSpouse.Visible = true;
                    txtRemarkSpouse.Text = "ไม่รับชำระเบี้ยคู่สมรส";
                }

                if (chkDataOfCustomer > 0)
                {
                    btnCustomerData.ForeColor = Color.Red;
                    btnCustomerData.Font = new Font("Tahoma", 9, FontStyle.Bold);
                }
                else
                {
                    btnCustomerData.ForeColor = Color.Black;
                    btnCustomerData.Font = new Font("Tahoma", 9, FontStyle.Regular);
                }

                //เช็ค EKYC
                var ekycChannelList = new string[] { "PN", "PO" };
                if (ekycChannelList.Contains(channelType) && WorkGroup == "TEL")
                {
                    chk_ekyc_pass.Visible = chk_ekyc_not_pass.Visible = chk_ekyc_none.Visible = chk_ekyc_wait_verify.Visible = ekycDetailExterlink.Visible =  true;
                    CheckEKYC();
                }
            }
            if (!string.IsNullOrEmpty(txtAppNo.Text))
            {
                // init data
                InitUapplicationUpdate();
                InitUPolPrintingException();
                initPaymentReturnPremium();
            }

            if(channelType == "KB" && PAR_PA_APPLICATION_ID.APP_ID == null)
            {
                txtAppOfc.Text = "สนญ"; // default
            }

            if (PAR_PA_APPLICATION_ID.APP_REMARK_Collection != null && PAR_PA_APPLICATION_ID.APP_REMARK_Collection.Any())
            {
                U_APP_REMARK_COLL = new NewBisPASvcRef.U_APP_REMARK_Collection();
                U_APP_REMARK_COLL.AddRange(PAR_PA_APPLICATION_ID.APP_REMARK_Collection);
                btnAppRemark.BackColor = Color.FromArgb(255, 128, 128);
            }
            else
            {
                btnAppRemark.BackColor = Color.Transparent;
            }
        }


        private void CheckEKYC()
        {
            try
            {
                string appno = "";
                string result = "";
                appno = txtAppNo.Text;
                ImproveNewBisSvcRef.ProcessResult prPlan = new ImproveNewBisSvcRef.ProcessResult();
                using (ImproveNewBisSvcRef.ImproveNewBisSvcClient client = new ImproveNewBisSvcRef.ImproveNewBisSvcClient())
                {
                    result = client.GetEkyc(appno);
                    if (result == "Y")
                    {
                        chk_ekyc_pass.Checked = true;
                    }
                    else if (result == "N")
                    {
                        chk_ekyc_not_pass.Checked = true;
                    }
                    else if (result == "W")
                    {
                        chk_ekyc_wait_verify.Checked = true;
                    }
                    else
                    {
                        chk_ekyc_none.Checked = true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void initPaymentReturnPremium()
        {

            string ChannelType = cmbChannelType.SelectedValue.ToString();
            gbPayMentReturnPrm.Visible = true;
            btnPayMentSave.Visible = true;


            using (var clientPayMent = new NewBisPASvcRef.NewBisPASvcClient())
            {
                var prPayMent = clientPayMent.GetReturnPremiumPayoption(out pPosID, txtAppNo.Text, ChannelType);
                if (prPayMent.Successed == false)
                {
                    throw new Exception(prPayMent.ErrorMessage);
                }
            }

            if (pPosID != null && pPosID.POS_ID != null)
            {
                bool chkOperation = false;
                ckbPaymentTmn.Checked = pPosID.TMN_FLG == 'Y' ? true : false;
                if (pPosID.POS_PAYOPTION_Collection != null && pPosID.POS_PAYOPTION_Collection.Count() > 0)
                {
                    dgvPayMent.Rows.Clear();
                    int i = 0;
                    foreach (var obj in pPosID.POS_PAYOPTION_Collection)
                    {
                        DataGridViewRow dr = (DataGridViewRow)dgvPayMent.Rows[i].Clone();
                        dr.Cells[1].Value = obj.OPT;
                        dr.Cells[2].Value = obj.BANK;
                        dr.Cells[3].Value = obj.BRANCH;
                        dr.Cells[4].Value = obj.ACCOUNT_NO;
                        dr.Cells[5].Value = obj.PAY_NAME;
                        dr.Cells[6].Value = obj.PAY;

                        if (pPosID.POS_APPROVE_Collection != null && pPosID.POS_APPROVE_Collection.Count() > 0)
                        {
                            dr.Cells[7].Value = "Y";
                        }
                        else
                        {
                            dr.Cells[7].Value = "N";
                        }

                        dr.Cells[8].Value = obj.TMN_FLG == null ? "N" : obj.TMN_FLG.Value.ToString();
                        dr.Cells[10].Value = obj.PAY_DT == null ? "" : Utility.dateTimeToString(obj.PAY_DT.Value, "dd/MM/yyyy", "BU");
                        dr.Cells[11].Value = obj.UPD_ID;
                        dr.Cells[12].Value = obj.UPD_DT == null ? "" : Utility.dateTimeToString(obj.UPD_DT.Value, "dd/MM/yyyy", "BU");
                        dr.Cells[13].Value = obj.POS_PAYOPTION_ID;
                        dgvPayMent.Rows.Add(dr);
                        i = i + 1;
                        if (obj.POS_PAYOPERATION != null && obj.POS_PAYOPERATION.POS_PAYOPTION_ID != null)
                        {
                            if (obj.POS_PAYOPERATION.OPERATION == 'Y')
                            {
                                chkOperation = true;
                            }
                        }
                    }

                    ckbPayMentOperation.Checked = chkOperation;
                    // PK_OPERATION = chkOperation;
                }
            }
            else
            {
                pPosID = new NewBisPASvcRef.P_POS_ID();
            }




        }

        #region "APPLICATION"
        private void GetUApplicationID(NewBisPASvcRef.PA_APPLICATION_ID objectDetail)
        {
            if (objectDetail.APPLICATION_Collection != null && objectDetail.APPLICATION_Collection.Count() > 0)
            {
                NewBisPASvcRef.U_APPLICATION objectData = new NewBisPASvcRef.U_APPLICATION();
                objectData = objectDetail.APPLICATION_Collection[0];
                GetUApplication(objectData);
                if (objectDetail.APPLICATION_Collection != null && objectDetail.APPLICATION_Collection.Any())
                {
                    GetPlanParametersOld(objectDetail.APPLICATION_Collection);
                }
            }
            if (objectDetail.APP_ISIS != null && objectDetail.APP_ISIS.APP_ID != null)
            {
                if (objectDetail.APP_ISIS.NEWBIS != 'Y')
                {
                    PLAN_ERROR = true;

                    #region "ส่งค่าให้สถานะให้ ISIS ครั้งแรกในการเปิดโปรแกรม"
                    try
                    {
                        using (ReceiptSvcRef.ServiceClient clientIsis = new ReceiptSvcRef.ServiceClient())
                        {
                            String detail = "";
                            detail = "สถานะเดิม: WT สถานะย่อยเดิม: WT สถานะใหม่: WT  สถานะย่อยใหม่: WT";
                            clientIsis.SendStatusToIsis(txtAppNo.Text, objectDetail.APP_ISIS.GUID, "WT", "WT", detail, DateTime.Now, 0, 0);
                        }
                    }
                    catch (Exception ex)
                    {

                        throw ex;
                    }

                    #endregion
                }
            }
            if (objectDetail.CAL_ERROR != null && objectDetail.CAL_ERROR.U_CAL_ERROR_ID != null)
            {
                PAR_U_CAL_ERROR = new NewBisPASvcRef.U_CAL_ERROR();
                PAR_U_CAL_ERROR = objectDetail.CAL_ERROR;
            }

        }
        private void GetULifeIDFree(NewBisPASvcRef.U_LIFE_ID objectDetail)
        {
            ckbLifeFree.Checked = true;
            cmbFreeCtmType.SelectedValue = objectDetail.CTM_TYPE == null ? "" : objectDetail.CTM_TYPE;
            cmbFreePlBlock.SelectedValue = objectDetail.PL_BLOCK == null ? "" : objectDetail.PL_BLOCK;
            txtFreePlcode.Text = objectDetail.PL_CODE;
            txtFreePlcode2.Text = objectDetail.PL_CODE2;
            txtFreePlanName.Text = objectDetail.BLA_TITLE;
            cmbFreePmode.SelectedValue = objectDetail.P_MODE == null ? "" : objectDetail.P_MODE.Value.ToString();
            txtFreeSumm.Text = objectDetail.SUMM == null ? "" : objectDetail.SUMM.Value.ToString("n0");
            txtFreeIsuDate.Text = objectDetail.ISU_DT == null ? "" : Utility.dateTimeToString(objectDetail.ISU_DT.Value, "dd/MM/yyyy", "BU");
            txtFreePolYr.Text = objectDetail.POL_YR == null ? "" : objectDetail.POL_YR.Value.ToString();
            txtFreePolLt.Text = objectDetail.POL_LT == null ? "" : objectDetail.POL_LT.Value.ToString();
            txtFreePayTerm.Text = objectDetail.PAY_TERM == null ? "" : objectDetail.PAY_TERM.Value.ToString();
            txtFreeLastPayDate.Text = objectDetail.LASTPAY_DT == null ? "" : Utility.dateTimeToString(objectDetail.LASTPAY_DT.Value, "dd/MM/yyyy", "BU");
            txtFreeAssTerm.Text = objectDetail.ASS_TERM == null ? "" : objectDetail.ASS_TERM.Value.ToString();
            txtFreeAssDate.Text = objectDetail.ASS_DT == null ? "" : Utility.dateTimeToString(objectDetail.ASS_DT.Value, "dd/MM/yyyy", "BU");
            txtFreeMatTerm.Text = objectDetail.MAT_TERM == null ? "" : objectDetail.MAT_TERM.Value.ToString();
            txtFreeMatDate.Text = objectDetail.MAT_DT == null ? "" : Utility.dateTimeToString(objectDetail.MAT_DT.Value, "dd/MM/yyyy", "BU");
            txtFreeNxtDueDate.Text = objectDetail.NXTDUE_DT == null ? "" : Utility.dateTimeToString(objectDetail.NXTDUE_DT.Value, "dd/MM/yyyy", "BU");
            txtFreePremium.Text = objectDetail.PREMIUM == null ? "0" : objectDetail.PREMIUM.Value.ToString("n0");
            txtFreeSingle.Text = objectDetail.SINGLE;
            txtFreeProtect.Text = objectDetail.PROTECT;
            txtFreeMedical.Text = objectDetail.MEDICAL;
            txtFreeStandard.Text = objectDetail.STANDARD;
            txtFreeReinsure.Text = objectDetail.REINSURE;
            txtFreeExcludeTpd.Text = objectDetail.EXCLUDE_TPD == null ? "" : objectDetail.EXCLUDE_TPD.Value.ToString();
            txtFreeDepositInst.Text = objectDetail.DEPOSIT_INST == null ? "" : objectDetail.DEPOSIT_INST.Value.ToString();
            txtFreePensionDate.Text = objectDetail.PENSION_DT == null ? "" : Utility.dateTimeToString(objectDetail.PENSION_DT.Value, "dd/MM/yyyy", "BU");
            txtFreePolicyHolding.Text = objectDetail.POLICY_HOLDING;
            txtFreeMarketingType.Text = objectDetail.MARKETING_TYPE;
            txtFreeSpouseFlg.Text = objectDetail.SPOUSE_FLG;
            PAR_PLAN_FREE = objectDetail;
            txtPolicyHolding.Text = objectDetail.POLICY_HOLDING;

        }
        private void GetULifeIDPaid(NewBisPASvcRef.U_LIFE_ID objectDetail)
        {
            ckbLifeBuy.Checked = true;
            cmbPaidCtmType.SelectedValue = objectDetail.CTM_TYPE == null ? "" : objectDetail.CTM_TYPE;
            cmbPaidPlBlock.SelectedValue = objectDetail.PL_BLOCK == null ? "" : objectDetail.PL_BLOCK;
            txtPaidPlcode.Text = objectDetail.PL_CODE;
            txtPaidPlcode2.Text = objectDetail.PL_CODE2;
            txtPaidPlanName.Text = objectDetail.BLA_TITLE;
            cmbPaidPmode.SelectedValue = objectDetail.P_MODE == null ? "" : objectDetail.P_MODE.Value.ToString();
            txtPaidSumm.Text = objectDetail.SUMM == null ? "" : objectDetail.SUMM.Value.ToString("n0");
            txtPaidIsuDate.Text = objectDetail.ISU_DT == null ? "" : Utility.dateTimeToString(objectDetail.ISU_DT.Value, "dd/MM/yyyy", "BU");
            txtPaidPolYr.Text = objectDetail.POL_YR == null ? "" : objectDetail.POL_YR.Value.ToString();
            txtPaidPolLt.Text = objectDetail.POL_LT == null ? "" : objectDetail.POL_LT.Value.ToString();
            txtPaidPayTerm.Text = objectDetail.PAY_TERM == null ? "" : objectDetail.PAY_TERM.Value.ToString();
            txtPaidLastPayDate.Text = objectDetail.LASTPAY_DT == null ? "" : Utility.dateTimeToString(objectDetail.LASTPAY_DT.Value, "dd/MM/yyyy", "BU");
            txtPaidAssTerm.Text = objectDetail.ASS_TERM == null ? "" : objectDetail.ASS_TERM.Value.ToString();
            txtPaidAssDate.Text = objectDetail.ASS_DT == null ? "" : Utility.dateTimeToString(objectDetail.ASS_DT.Value, "dd/MM/yyyy", "BU");
            txtPaidMatTerm.Text = objectDetail.MAT_TERM == null ? "" : objectDetail.MAT_TERM.Value.ToString();
            txtPaidMatDate.Text = objectDetail.MAT_DT == null ? "" : Utility.dateTimeToString(objectDetail.MAT_DT.Value, "dd/MM/yyyy", "BU");
            txtPaidNxtDueDate.Text = objectDetail.NXTDUE_DT == null ? "" : Utility.dateTimeToString(objectDetail.NXTDUE_DT.Value, "dd/MM/yyyy", "BU");
            txtPaidPremium.Text = objectDetail.PREMIUM == null ? "0" : objectDetail.PREMIUM.Value.ToString("n0");
            txtPaidSingle.Text = objectDetail.SINGLE;
            txtPaidProtect.Text = objectDetail.PROTECT;
            txtPaidMedical.Text = objectDetail.MEDICAL;
            txtPaidStandard.Text = objectDetail.STANDARD;
            txtPaidReinsure.Text = objectDetail.REINSURE;
            txtPaidExcludeTpd.Text = objectDetail.EXCLUDE_TPD == null ? "" : objectDetail.EXCLUDE_TPD.Value.ToString();
            txtPaidDepositInst.Text = objectDetail.DEPOSIT_INST == null ? "" : objectDetail.DEPOSIT_INST.Value.ToString();
            txtPaidPensionDate.Text = objectDetail.PENSION_DT == null ? "" : Utility.dateTimeToString(objectDetail.PENSION_DT.Value, "dd/MM/yyyy", "BU");
            txtPaidPolicyHolding.Text = objectDetail.POLICY_HOLDING;
            txtPaidMarketingType.Text = objectDetail.MARKETING_TYPE;
            txtPaidSpouseFlg.Text = objectDetail.SPOUSE_FLG;
            PAR_PLAN_PAID = objectDetail;
            txtPolicyHolding.Text = objectDetail.POLICY_HOLDING;
        }


        private void GetUSpouseID(NewBisPASvcRef.U_APP_SPOUSE_Collection objColl, NewBisPASvcRef.U_SPOUSE_ID[] objArr)
        {
            if (objColl != null && objColl.Count() > 0)
            {
                txtSpAssTerm.Text = objColl[0].ASS_TERM == null ? "" : objColl[0].ASS_TERM.Value.ToString();
                txtSpAssDate.Text = objColl[0].ASS_DT == null ? "" : Utility.dateTimeToString(objColl[0].ASS_DT.Value, "dd/MM/yyyy", "BU");

                if (objArr != null && objArr.Count() > 0)
                {
                    NewBisPASvcRef.U_SPOUSE_ID obj = new NewBisPASvcRef.U_SPOUSE_ID();
                    obj = objArr.ToArray()[0];
                    ckbSpouse.Checked = true;
                    if ((!(String.IsNullOrEmpty(obj.IDCARD_NO))) && (String.IsNullOrEmpty(obj.PASSPORT)))
                    {
                        txtSpIDcardNo.Text = obj.IDCARD_NO;
                        cmbSpCardType.SelectedValue = "1";
                    }
                    else if ((!(String.IsNullOrEmpty(obj.PASSPORT))) && (String.IsNullOrEmpty(obj.IDCARD_NO)))
                    {
                        txtSpIDcardNo.Text = obj.PASSPORT;
                        cmbSpCardType.SelectedValue = "2";
                    }
                    else
                    {
                        txtSpIDcardNo.Text = "";
                        cmbSpCardType.SelectedValue = "";
                    }

                    txtSpPrename.Text = obj.PRENAME;
                    txtSpName.Text = obj.NAME;
                    txtSpSurname.Text = obj.SURNAME;
                    cmbSpSex.SelectedValue = obj.SEX == null ? "" : obj.SEX.Value.ToString();
                    txtSpBirthDate.Text = obj.BIRTH_DT == null ? "" : Utility.dateTimeToString(obj.BIRTH_DT.Value, "dd/MM/yyyy", "BU");
                    txtSpAge.Text = obj.APP_SPOUSE_Collection[0].ST_AGE == null ? "" : obj.APP_SPOUSE_Collection[0].ST_AGE.Value.ToString();
                    cmbSpNationality.SelectedValue = obj.NATIONALITY == null ? "" : obj.NATIONALITY;
                    txtSpMbPhone.Text = obj.MB_PHONE;

                }
            }
        }
        private void GetUApplication(NewBisPASvcRef.U_APPLICATION objectDetail)
        {
            PAR_END_PROCESS = objectDetail.END_PROCESS;
            txtAppOfcRcvDt.Text = objectDetail.APP_OFCRCV_DT == null ? Utility.dateTimeToString(DateTime.Now, "dd/MM/yyyy hh:mi:ss", "BU") : Utility.dateTimeToString(objectDetail.APP_OFCRCV_DT.Value, "dd/MM/yyyy hh:mi:ss", "BU");
            txtAppHoRcvDt.Text = objectDetail.APP_HORCV_DT == null ? Utility.dateTimeToString(DateTime.Now, "dd/MM/yyyy hh:mi:ss", "BU") : Utility.dateTimeToString(objectDetail.APP_HORCV_DT.Value, "dd/MM/yyyy hh:mi:ss", "BU");
            txtAppOfc.Text = objectDetail.APP_OFC == null || objectDetail.APP_OFC == "" ? "สนญ" : objectDetail.APP_OFC;
            txtAppDt.Text = objectDetail.APP_DT == null ? "" : Utility.dateTimeToString(objectDetail.APP_DT.Value, "dd/MM/yyyy", "BU");
            txtAppSignDt.Text = objectDetail.APP_SIGN_DT == null ? Utility.dateTimeToString(DateTime.Now, "dd/MM/yyyy", "BU") : Utility.dateTimeToString(objectDetail.APP_SIGN_DT.Value, "dd/MM/yyyy", "BU");
            cmbDocumentFlag.SelectedValue = objectDetail.DOCUMENT_FLG == null ? "" : objectDetail.DOCUMENT_FLG.Value.ToString();
            cmbUnderWriteFlag.SelectedValue = objectDetail.UNDERWRITING_FLG == null ? "" : objectDetail.UNDERWRITING_FLG.Value.ToString();
            cmbUnderWrite.SelectedValue = "C";
            if (objectDetail.APPSYS_DT != null)
            {
                txtAppSysDt.Text = Utility.dateTimeToString(objectDetail.APPSYS_DT.Value, "dd/MM/yyyy hh:mi:ss", "BU");
            }

            if (objectDetail.NAME_ID_Collection != null && objectDetail.NAME_ID_Collection.Count() > 0)
            {
                //NewBisPASvcRef.U_NAME_ID uNameID = new NewBisPASvcRef.U_NAME_ID();
                // uNameID = objectDetail.NAME_ID_Collection[0];

                var uNameCusInfo = objectDetail.NAME_ID_Collection.Where(item => item.CUSTOMER_TYPE.Equals("C")).FirstOrDefault();
                if (uNameCusInfo != null)
                {
                    GetUNameID(uNameCusInfo);
                }

                var uNameParentInfo = objectDetail.NAME_ID_Collection.Where(item => item.CUSTOMER_TYPE.Equals("P")).FirstOrDefault();
                if (uNameParentInfo != null)
                {
                    GetUParentNameID(uNameParentInfo);
                }
            }
            else
            {
                NewBisPASvcRef.NewBisPASvcClient nbpc = new NewBisPASvcRef.NewBisPASvcClient();
                NewBisPASvcRef.U_REGISTER_ID[] uri = null;
                nbpc.GetDataU_REGISTER_ID(out uri, txtAppNo.Text);
                if (uri != null && uri.Any())
                {
                    var reigsterIngo = uri[0];
                    if (reigsterIngo.IDCARD_NO != null)
                    {
                        cmbCardType.SelectedValue = "1";
                        txtIdcardNo.Text = reigsterIngo.IDCARD_NO;
                    }
                    else
                    {
                        cmbCardType.SelectedValue = "2";
                        txtIdcardNo.Text = reigsterIngo.PASSPORT;
                    }
                    txtPrename.Text = reigsterIngo.PRENAME;
                    txtName.Text = reigsterIngo.NAME;
                    txtSurname.Text = reigsterIngo.SURNAME;
                }
                txtSaleAgent.Text = "00900000";
                SetAgentRelate();
            }

            if (objectDetail.POLICY_SENDING != null && objectDetail.POLICY_SENDING.UAPP_ID != null)
            {
                GetUPolicySending(objectDetail.POLICY_SENDING);
            }

            if (objectDetail.LIFE_ID != null)
            {
                if (objectDetail.LIFE_ID.AGENT != null && objectDetail.LIFE_ID.AGENT.UAPP_ID != null)
                {
                    txtPolicyHolding.Text = objectDetail.LIFE_ID.POLICY_HOLDING;
                    GetUAgent(objectDetail.LIFE_ID.AGENT);
                }
            }
            else
            {
                //PF
                if (cmbChannelType.SelectedValue.ToString() == "PF" && txtSaleAgent.Text.Trim() == "")
                {
                    txtSaleAgent.Text = "00900000";
                    txtSaleAgentName.Text = "บริษัท กรุงเทพประกันชีวิต จำกัด (มหาชน)   (สนญ) (บัตรหมด: 20/03/9999)";

                    txtLicenseAgent.Text = "00900000";
                    txtLicenseAgentName.Text = "บริษัท กรุงเทพประกันชีวิต จำกัด (มหาชน)   (สนญ) (บัตรหมด: 20/03/9999)";
                }
            }

            if (objectDetail.STATUS_ID_Collection != null && objectDetail.STATUS_ID_Collection.Count() > 0)
            {
                GetUStatusID(objectDetail.STATUS_ID_Collection[0]);
            }

            if (objectDetail.APPROVED_Collection != null && objectDetail.APPROVED_Collection.Count() > 0)
            {
                GetUApproved(objectDetail.APPROVED_Collection[0]);
            }

            if (objectDetail.APPLICATION_NAME_Collection != null && objectDetail.APPLICATION_NAME_Collection.Count() > 0)
            {
                PAR_U_APPLICATION_NAME = new NewBisPASvcRef.U_APPLICATION_NAME();
                PAR_U_APPLICATION_NAME = objectDetail.APPLICATION_NAME_Collection[0];

                cmbAppNameID.SelectedValue = Convert.ToInt64(PAR_U_APPLICATION_NAME.APPNAME_ID.Value);
            }
            else
            {
                PAR_U_APPLICATION_NAME = new NewBisPASvcRef.U_APPLICATION_NAME();
                long? appNameID = null;
                if (PAR_PA_APPLICATION_ID.CHANNEL_TYPE == "PD")
                {
                    appNameID = 18;
                    cmbAppNameID.SelectedValue = appNameID;
                }
                if (PAR_PA_APPLICATION_ID.CHANNEL_TYPE == "PF")
                {
                    appNameID = 27;
                    cmbAppNameID.SelectedValue = appNameID;
                }
            }



            if (PAR_U_ACCOUNT_ID_COLL != null && PAR_U_ACCOUNT_ID_COLL.Count() > 0)
            {
                if (PAR_U_ACCOUNT_ID_COLL[0] != null && PAR_U_ACCOUNT_ID_COLL[0].UACCOUNT_ID != null)
                {
                    txtBranchCode.Text = PAR_U_ACCOUNT_ID_COLL[0].ACC_BRANCH;
                    txtBranchName.Text = PAR_U_ACCOUNT_ID_COLL[0].BRANCH_NAME;
                    txtAccName.Text = PAR_U_ACCOUNT_ID_COLL[0].ACC_NAME;
                    txtAccNo.Text = PAR_U_ACCOUNT_ID_COLL[0].ACC_NO;
                    cmbDepositType.SelectedValue = PAR_U_ACCOUNT_ID_COLL[0].ACC_DEPOSIT_TYPE;
                }

            }

            if (objectDetail.END_PROCESS == 'Y')
            {
                btnHeadSave.Enabled = false;
                //gbUnderwrite.Enabled = false;
                SetEnableStatusOfUnderwriterControl(false);
            }
        }
        private void GetUNameID(NewBisPASvcRef.U_NAME_ID objectDetail)
        {
            if ((!(String.IsNullOrEmpty(objectDetail.IDCARD_NO))) && (String.IsNullOrEmpty(objectDetail.PASSPORT)))
            {
                txtIdcardNo.Text = objectDetail.IDCARD_NO;
                cmbCardType.SelectedValue = "1";
            }
            else if ((!(String.IsNullOrEmpty(objectDetail.PASSPORT))) && (String.IsNullOrEmpty(objectDetail.IDCARD_NO)))
            {
                txtIdcardNo.Text = objectDetail.PASSPORT;
                cmbCardType.SelectedValue = "2";
            }
            else
            {
                txtIdcardNo.Text = "";
                cmbCardType.SelectedValue = "";
            }

            if (objectDetail.NAME_ID != null)
            {
                NAME_ID = objectDetail.NAME_ID;
                cmbDataCus.SelectedValue = "1";
                cmbDataCus.BackColor = Color.FromArgb(bgRed[0], bgRed[1], bgRed[2]);
            }
            else
            {
                NAME_ID = null;
                cmbDataCus.SelectedValue = "2";
                cmbDataCus.BackColor = Color.White;
            }

            if (objectDetail.PARENT_ID != null)
            {
                PARENT_ID = objectDetail.PARENT_ID;
            }
            else
            {
                PARENT_ID = null;
            }

            cmbUnderWrite.SelectedValue = objectDetail.CUSTOMER_TYPE == null ? "" : objectDetail.CUSTOMER_TYPE;
            txtPrename.Text = objectDetail.PRENAME;
            txtName.Text = objectDetail.NAME;
            txtSurname.Text = objectDetail.SURNAME;
            txtBirthDt.Text = Utility.dateTimeToString(objectDetail.BIRTH_DT.Value, "dd/MM/yyyy", "BU");
            cmbSex.SelectedValue = objectDetail.SEX == null ? "" : objectDetail.SEX.Value.ToString();
            txtMbPhone.Text = objectDetail.MB_PHONE;
            cmbNationality.SelectedValue = objectDetail.NATIONALITY == null ? "" : objectDetail.NATIONALITY;
            txtStAge.Text = objectDetail.ST_AGE == null ? "" : objectDetail.ST_AGE.Value.ToString();
            cmbMaritalStatus.SelectedValue = objectDetail.MARITAL_STATUS == null ? "" : objectDetail.MARITAL_STATUS.Value.ToString();
            cmbReligion.SelectedValue = objectDetail.RELIGION == null ? "" : objectDetail.RELIGION.Value.ToString();

            txtCustomerName.Text = objectDetail.PRENAME + objectDetail.NAME + "  " + objectDetail.SURNAME;

            if (objectDetail.APP_ADDRESS != null && objectDetail.APP_ADDRESS.UNAME_ID != null)
            {
                if (PAR_U_ADDRESS_ID_COLL != null && PAR_U_ADDRESS_ID_COLL.Count() > 0)
                {
                    var GetAddress = from getAddress in PAR_U_ADDRESS_ID_COLL
                                     where getAddress.UADDRESS_ID == objectDetail.APP_ADDRESS.UADDRESS_ID
                                     select getAddress;
                    if (GetAddress != null && GetAddress.Count() > 0)
                    {
                        NewBisPASvcRef.U_ADDRESS_ID uAddressIDObj = new NewBisPASvcRef.U_ADDRESS_ID();
                        uAddressIDObj = GetAddress.ToArray()[0];
                        PAR_U_ADDRESS_ID_OLD = uAddressIDObj;
                        if (PAR_U_ADDRESS_ID_OLD.OADDRESS != null && PAR_U_ADDRESS_ID_OLD.OADDRESS.UADDRESS_ID != null)
                        {
                            PAR_U_OADDRESS = PAR_U_ADDRESS_ID_OLD.OADDRESS;
                        }
                        else
                        {
                            PAR_U_OADDRESS = null;
                        }
                        GetUAddressID(uAddressIDObj);
                    }
                }

            }

            if (objectDetail.NAME_DOCUMENT_Collection != null && objectDetail.NAME_DOCUMENT_Collection.Count() > 0)
            {
                var GetCardDoc = from getCardDoc in objectDetail.NAME_DOCUMENT_Collection
                                 where getCardDoc.DOC_CODE == "000000000005"
                                 select getCardDoc;
                if (GetCardDoc != null && GetCardDoc.Count() > 0)
                {
                    ckbIdcardDoc.Checked = true;
                }
                else
                {
                    ckbIdcardDoc.Checked = false;
                }

                var GetAddressDoc = from getAddressDoc in objectDetail.NAME_DOCUMENT_Collection
                                    where getAddressDoc.DOC_CODE == "000000000132"
                                    select getAddressDoc;
                if (GetAddressDoc != null && GetAddressDoc.Count() > 0)
                {
                    ckbAddressDoc.Checked = true;
                }
                else
                {
                    ckbAddressDoc.Checked = false;
                }
            }
            if (objectDetail.EMAIL_ID != null && objectDetail.EMAIL_ID.UNAME_ID != null)
            {
                txtEmail.Text = objectDetail.EMAIL_ID.EMAIL;
            }
            else
            {
                txtEmail.Text = "";
            }

            if (cmbDataCus.SelectedValue.ToString() == "1" && NAME_ID != null)
            {
                if (PAR_P_NAME_ID_COLL != null && PAR_P_NAME_ID_COLL.Count() > 0)
                {
                    var GetData = from getData in PAR_P_NAME_ID_COLL
                                  where getData.NAME_ID == NAME_ID
                                  select getData;
                    if (GetData != null && GetData.Count() > 0)
                    {
                        NewBisPASvcRef.P_NAME_ID pNameIDObj = new NewBisPASvcRef.P_NAME_ID();
                        pNameIDObj = GetData.ToArray()[0];
                        ChkOldCusDataWithNewCusData(pNameIDObj);
                    }

                }
            }

            if (objectDetail.OCCUPATION != null && objectDetail.OCCUPATION.UNAME_ID != null)
            {
                GetUOccupation(objectDetail.OCCUPATION);
            }
        }

        private void GetUParentNameID(NewBisPASvcRef.U_NAME_ID objectDetail)
        {
            if ((!(String.IsNullOrEmpty(objectDetail.IDCARD_NO))) && (String.IsNullOrEmpty(objectDetail.PASSPORT)))
            {
                txtParentIdcardNo.Text = objectDetail.IDCARD_NO;
                cmbParentCardType.SelectedValue = "1";
            }
            else if ((!(String.IsNullOrEmpty(objectDetail.PASSPORT))) && (String.IsNullOrEmpty(objectDetail.IDCARD_NO)))
            {
                txtParentIdcardNo.Text = objectDetail.PASSPORT;
                cmbParentCardType.SelectedValue = "2";
            }
            else
            {
                txtParentIdcardNo.Text = "";
                cmbParentCardType.SelectedValue = "";
            }

            if (objectDetail.NAME_ID != null)
            {
                PARENT_PA_NAME_ID = objectDetail.NAME_ID;
                cmbDataParent.SelectedValue = "1";
                cmbDataParent.BackColor = Color.FromArgb(bgRed[0], bgRed[1], bgRed[2]);
            }
            else
            {
                PARENT_PA_NAME_ID = null;
                cmbDataParent.SelectedValue = "2";
                cmbDataParent.BackColor = Color.White;
            }

            if (objectDetail.PARENT_ID != null)
            {
                PARENT_ID = objectDetail.PARENT_ID;
            }
            else
            {
                PARENT_ID = null;
            }

            // cmbUnderWrite.SelectedValue = objectDetail.CUSTOMER_TYPE == null ? "" : objectDetail.CUSTOMER_TYPE;
            txtParentPrename.Text = objectDetail.PRENAME;
            txtParentName.Text = objectDetail.NAME;
            txtParentSurname.Text = objectDetail.SURNAME;
            txtParentBirthDt.Text = Utility.dateTimeToString(objectDetail.BIRTH_DT.Value, "dd/MM/yyyy", "BU");
            cmbParentGender.SelectedValue = objectDetail.SEX == null ? "" : objectDetail.SEX.Value.ToString();
            txtParentMbPhone.Text = objectDetail.MB_PHONE;
            cmbParentNationality.SelectedValue = objectDetail.NATIONALITY == null ? "" : objectDetail.NATIONALITY;
            txtParentStAge.Text = objectDetail.ST_AGE == null ? "" : objectDetail.ST_AGE.Value.ToString();
            cmbParentMaritalStatus.SelectedValue = objectDetail.MARITAL_STATUS == null ? "" : objectDetail.MARITAL_STATUS.Value.ToString();
            cmbParentReligion.SelectedValue = objectDetail.RELIGION == null ? "" : objectDetail.RELIGION.Value.ToString();

            // txtCustomerName.Text = objectDetail.PRENAME + objectDetail.NAME + "  " + objectDetail.SURNAME;

            if (objectDetail.APP_ADDRESS != null && objectDetail.APP_ADDRESS.UNAME_ID != null)
            {
                if (PAR_U_ADDRESS_ID_COLL != null && PAR_U_ADDRESS_ID_COLL.Count() > 0)
                {
                    var GetAddress = from getAddress in PAR_U_ADDRESS_ID_COLL
                                     where getAddress.UADDRESS_ID == objectDetail.APP_ADDRESS.UADDRESS_ID
                                     select getAddress;
                    if (GetAddress != null && GetAddress.Count() > 0)
                    {
                        NewBisPASvcRef.U_ADDRESS_ID uAddressIDObj = new NewBisPASvcRef.U_ADDRESS_ID();
                        uAddressIDObj = GetAddress.ToArray()[0];
                        PAR_U_ADDRESS_ID_OLD = uAddressIDObj;
                        if (PAR_U_ADDRESS_ID_OLD.OADDRESS != null && PAR_U_ADDRESS_ID_OLD.OADDRESS.UADDRESS_ID != null)
                        {
                            PAR_U_OADDRESS = PAR_U_ADDRESS_ID_OLD.OADDRESS;
                        }
                        else
                        {
                            PAR_U_OADDRESS = null;
                        }
                        GetUParentAddressID(uAddressIDObj);
                    }
                }

            }

            if (objectDetail.NAME_DOCUMENT_Collection != null && objectDetail.NAME_DOCUMENT_Collection.Count() > 0)
            {
                var GetCardDoc = from getCardDoc in objectDetail.NAME_DOCUMENT_Collection
                                 where getCardDoc.DOC_CODE == "000000000005"
                                 select getCardDoc;
                if (GetCardDoc != null && GetCardDoc.Count() > 0)
                {
                    ckbParentIdcardDoc.Checked = true;
                }
                else
                {
                    ckbParentIdcardDoc.Checked = false;
                }

                var GetAddressDoc = from getAddressDoc in objectDetail.NAME_DOCUMENT_Collection
                                    where getAddressDoc.DOC_CODE == "000000000132"
                                    select getAddressDoc;
                if (GetAddressDoc != null && GetAddressDoc.Count() > 0)
                {
                    ckbParentAddressDoc.Checked = true;
                }
                else
                {
                    ckbParentAddressDoc.Checked = false;
                }
            }
            if (objectDetail.EMAIL_ID != null && objectDetail.EMAIL_ID.UNAME_ID != null)
            {
                txtParentEmail.Text = objectDetail.EMAIL_ID.EMAIL;
            }
            else
            {
                txtParentEmail.Text = "";
            }

            if (cmbDataParent.SelectedValue.ToString() == "1" && NAME_ID != null)
            {
                if (PAR_P_NAME_ID_COLL != null && PAR_P_NAME_ID_COLL.Count() > 0)
                {
                    var GetData = from getData in PAR_P_NAME_ID_COLL
                                  where getData.NAME_ID == NAME_ID
                                  select getData;
                    if (GetData != null && GetData.Count() > 0)
                    {
                        NewBisPASvcRef.P_NAME_ID pNameIDObj = new NewBisPASvcRef.P_NAME_ID();
                        pNameIDObj = GetData.ToArray()[0];
                        ChkOldCusDataWithNewCusData(pNameIDObj);
                    }

                }
            }

            if (objectDetail.OCCUPATION != null && objectDetail.OCCUPATION.UNAME_ID != null)
            {
                GetUParentOccupation(objectDetail.OCCUPATION);
            }
        }

        private void GetUOccupation(NewBisPASvcRef.U_OCCUPATION objectDetail)
        {
            txtHeight.Text = (objectDetail.HEIGHT == null || objectDetail.HEIGHT == 0 ? "" : objectDetail.HEIGHT.Value.ToString());
            txtWeight.Text = (objectDetail.WEIGHT == null || objectDetail.WEIGHT == 0 ? "" : objectDetail.WEIGHT.ToString());
            ckbMotercycleUsed.Checked = objectDetail.MOTERCYCLE_USED == 'Y' ? true : false;
            txtOcpType.Text = objectDetail.REGULAR_OCP_TYPE;
            txtOcpClass.Text = objectDetail.REGULAR_OCP_CLASS == null ? "" : objectDetail.REGULAR_OCP_CLASS.Value.ToString();

            if (PAR_OCCUPATION_COLL != null && PAR_OCCUPATION_COLL.Count() > 0)
            {
                var GetData = from getData in PAR_OCCUPATION_COLL
                              where getData.OCP_TYPE == txtOcpType.Text
                              && getData.OCP_CLASS == txtOcpClass.Text
                              select getData;
                if (GetData != null && GetData.Count() > 0)
                {
                    cmbocpDesc.SelectedValue = GetData.ToArray()[0].CODE;
                    cmbOcpGroup.SelectedValue = GetData.ToArray()[0].OCP_GROUP;
                }
                else
                {
                    cmbocpDesc.SelectedValue = "";
                }
            }

            cmbPosition.SelectedValue = objectDetail.REGULAR_POSITION == null ? "" : objectDetail.REGULAR_POSITION;
            cmbBusinessType.SelectedValue = objectDetail.REGULAR_BUSINESS_TYPE == null ? "" : objectDetail.REGULAR_BUSINESS_TYPE;
            txtWorkType.Text = objectDetail.REGULAR_WORK_TYPE;
            txtIncomePerYear.Text = objectDetail.REGULAR_INCOME_PERYR == null ? "" : objectDetail.REGULAR_INCOME_PERYR.Value.ToString();

            if (objectDetail.BMI != null && objectDetail.BMI != 0)
            {

                txtBmi.Text = objectDetail.BMI.Value.ToString("n2");

                decimal ageCustomer = txtStAge.Text == "" ? 0 : Convert.ToDecimal(txtStAge.Text.Replace(",", ""));
                if (ageCustomer < 12)
                {
                    if (objectDetail.BMI.Value >= 13 && objectDetail.BMI.Value <= 21)
                    {
                        txtBmi.BackColor = Color.FromArgb(192, 255, 192);
                    }
                    else
                    {
                        txtBmi.BackColor = Color.FromArgb(255, 192, 192);
                    }
                }
                else
                {
                    if (objectDetail.BMI.Value >= 17 && objectDetail.BMI.Value <= 32)
                    {
                        txtBmi.BackColor = Color.FromArgb(192, 255, 192);
                    }
                    else
                    {
                        txtBmi.BackColor = Color.FromArgb(255, 192, 192);
                    }
                }
            }
            else
            {
                txtBmi.Text = "";
                txtBmi.BackColor = Color.FromArgb(192, 255, 192);
            }
        }

        private void GetUParentOccupation(NewBisPASvcRef.U_OCCUPATION objectDetail)
        {
            txtParentHeight.Text = objectDetail.HEIGHT == null ? "" : objectDetail.HEIGHT.Value.ToString();
            txtParentWeight.Text = objectDetail.WEIGHT == null ? "" : objectDetail.WEIGHT.ToString();
            ckbParentMotercycleUsed.Checked = objectDetail.MOTERCYCLE_USED == 'Y' ? true : false;
            txtParentOcpType.Text = objectDetail.REGULAR_OCP_TYPE;
            txtParentOcpClass.Text = objectDetail.REGULAR_OCP_CLASS == null ? "" : objectDetail.REGULAR_OCP_CLASS.Value.ToString();

            if (PAR_OCCUPATION_COLL != null && PAR_OCCUPATION_COLL.Count() > 0)
            {
                var GetData = from getData in PAR_OCCUPATION_COLL
                              where getData.OCP_TYPE == txtParentOcpType.Text
                              && getData.OCP_CLASS == txtParentOcpClass.Text
                              select getData;
                if (GetData != null && GetData.Count() > 0)
                {
                    cmbParentbocpDesc.SelectedValue = GetData.ToArray()[0].CODE;
                }
                else
                {
                    cmbParentbocpDesc.SelectedValue = "";
                }
            }

            cmbParentPosition.SelectedValue = objectDetail.REGULAR_POSITION == null ? "" : objectDetail.REGULAR_POSITION;
            cmbParentBusinessType.SelectedValue = objectDetail.REGULAR_BUSINESS_TYPE == null ? "" : objectDetail.REGULAR_BUSINESS_TYPE;
            txtParentWorkType.Text = objectDetail.REGULAR_WORK_TYPE;
            txtParentIncomePerYear.Text = objectDetail.REGULAR_INCOME_PERYR == null ? "" : objectDetail.REGULAR_INCOME_PERYR.Value.ToString();
        }

        private void GetUAddressID(NewBisPASvcRef.U_ADDRESS_ID objectDetail)
        {
            cmbAddressType.SelectedValue = objectDetail.ADDRESS_TYPE == null ? "" : objectDetail.ADDRESS_TYPE.Value.ToString();
            txtAddressName.Text = objectDetail.ADDRESS_NAME;
            txtAddressNumber.Text = objectDetail.ADDRESS_NUMBER;
            txtMooh.Text = objectDetail.MOOH;
            txtSoi.Text = objectDetail.SOI;
            txtRoad.Text = objectDetail.ROAD;
            cmbProvince.Text = objectDetail.PROVINCE == "" || objectDetail.PROVINCE == null ? "ระบุจังหวัดที่ต้องการ" : objectDetail.PROVINCE;
            cmbAmphur.Text = objectDetail.AMPHUR == "" || objectDetail.AMPHUR == null ? "ระบุอำเภอที่ต้องการ" : objectDetail.AMPHUR;
            cmbTambol.Text = objectDetail.TAMBOL == "" || objectDetail.TAMBOL == null ? "ระบุตำบลที่ต้องการ" : objectDetail.TAMBOL;
            txtZipcode.Text = objectDetail.ZIP_CODE;
            txtPhoneNumber.Text = objectDetail.PHONE_NUMBER;
            if (objectDetail.ADDRESS_ID != null)
            {
                ADDRESS_ID = objectDetail.ADDRESS_ID;
                cmbAdrType.SelectedValue = "1";
                cmbAdrType.BackColor = Color.FromArgb(bgRed[0], bgRed[1], bgRed[2]);

                if (PAR_ADDRESS_ID_COLL != null && PAR_ADDRESS_ID_COLL.Count() > 0)
                {
                    var GetData = from getData in PAR_ADDRESS_ID_COLL
                                  where getData.ADDRESS_ID == ADDRESS_ID
                                  select getData;
                    if (GetData != null && GetData.Count() > 0)
                    {
                        NewBisPASvcRef.P_ADDRESS_ID pAddressIDObj = new NewBisPASvcRef.P_ADDRESS_ID();
                        pAddressIDObj = GetData.ToArray()[0];
                        string channelType = cmbChannelType.SelectedValue.ToString();

                        if (channelType == "PO" || channelType == "PN")
                        {
                            cmbAdrType.Enabled = true;
                            CompareAndSetAddressInfo(pAddressIDObj);
                        }
                        else
                        {
                            ChkOldAddressWithNewAddress(pAddressIDObj);
                        }
                    }
                }
            }
            else
            {
                ADDRESS_ID = null;
                cmbAdrType.SelectedValue = "2";
                cmbAdrType.BackColor = Color.White;
            }

        }

        private void GetUParentAddressID(NewBisPASvcRef.U_ADDRESS_ID objectDetail)
        {
            cmbParentAddressType.SelectedValue = objectDetail.ADDRESS_TYPE == null ? "" : objectDetail.ADDRESS_TYPE.Value.ToString();
            txtParentAddressName.Text = objectDetail.ADDRESS_NAME;
            txtParentAddressNumber.Text = objectDetail.ADDRESS_NUMBER;
            txtParentMooh.Text = objectDetail.MOOH;
            txtParentSoi.Text = objectDetail.SOI;
            txtParentRoad.Text = objectDetail.ROAD;
            cmbParentProvince.Text = objectDetail.PROVINCE == "" || objectDetail.PROVINCE == null ? "ระบุจังหวัดที่ต้องการ" : objectDetail.PROVINCE;
            cmbParentAmphur.Text = objectDetail.AMPHUR == "" || objectDetail.AMPHUR == null ? "ระบุอำเภอที่ต้องการ" : objectDetail.AMPHUR;
            cmbParentTambol.Text = objectDetail.TAMBOL == "" || objectDetail.TAMBOL == null ? "ระบุตำบลที่ต้องการ" : objectDetail.TAMBOL;
            txtParentZipcode.Text = objectDetail.ZIP_CODE;
            txtParentPhoneNumber.Text = objectDetail.PHONE_NUMBER;
            if (objectDetail.ADDRESS_ID != null)
            {
                ADDRESS_ID = objectDetail.ADDRESS_ID;
                cmbParentAdrType.SelectedValue = "1";
                cmbParentAdrType.BackColor = Color.FromArgb(bgRed[0], bgRed[1], bgRed[2]);

                if (PAR_ADDRESS_ID_COLL != null && PAR_ADDRESS_ID_COLL.Count() > 0)
                {
                    var GetData = from getData in PAR_ADDRESS_ID_COLL
                                  where getData.ADDRESS_ID == ADDRESS_ID
                                  select getData;
                    if (GetData != null && GetData.Count() > 0)
                    {
                        NewBisPASvcRef.P_ADDRESS_ID pAddressIDObj = new NewBisPASvcRef.P_ADDRESS_ID();
                        pAddressIDObj = GetData.ToArray()[0];
                        ChkOldAddressWithNewAddress(pAddressIDObj);
                    }
                }
            }
            else
            {
                ADDRESS_ID = null;
                cmbParentAdrType.SelectedValue = "2";
                cmbParentAdrType.BackColor = Color.White;
            }

        }



        private void GetUPolicySending(NewBisPASvcRef.U_POLICY_SENDING objectDetail)
        {
            cmbSendTo.SelectedValue = objectDetail.SEND_TO;
            lblSendAt.Visible = false;
            txtSendOfc.Visible = false;
            if (objectDetail.SEND_TO == "O")
            {
                lblSendAt.Visible = true;
                txtSendOfc.Visible = true;
                txtSendOfc.Text = objectDetail.OFFICE;
            }
            string channelType = cmbChannelType.SelectedValue == null ? "" : cmbChannelType.SelectedValue.ToString();
            if (channelType == "PF")
            {
                if (objectDetail.EPOLICY_FLG == 'Y' || objectDetail.DPOLICY_FLG == 'Y')
                {

                    if ((objectDetail.EPOLICY_FLG == 'Y' && objectDetail.DPOLICY_FLG == 'Y'))
                    {
                        cmbPolicyDocmentType.SelectedValue = "A"; // ALL = documeny + e policy
                    }
                    else if (objectDetail.EPOLICY_FLG == 'Y')
                    {
                        cmbPolicyDocmentType.SelectedValue = "E";
                    }
                    else if (objectDetail.DPOLICY_FLG == 'Y')
                    {
                        cmbPolicyDocmentType.SelectedValue = "D";
                    }

                    cmbEPolicySending.SelectedValue = objectDetail.EPOLICY_SEND_TO == null ? "" : objectDetail.EPOLICY_SEND_TO.Value.ToString();
                    lblEpolicySendType.Enabled = cmbEPolicySending.Enabled = true;
                }
                else
                {
                    cmbPolicyDocmentType.SelectedValue = "D";
                    lblEpolicySendType.Enabled = cmbEPolicySending.Enabled = false;
                }
            }
        }
        private void GetUAgent(NewBisPASvcRef.U_AGENT objectDetail)
        {
            txtSaleAgent.Text = objectDetail.SALE_AGENT;

            txtSaleOfc.Text = objectDetail.SALE_OFC;
            txtSaleAgentUpl.Text = objectDetail.SALE_AGENT_UPL;

            txtLicenseAgent.Text = objectDetail.LICENSE_AGENT;
            txtLicenseOfc.Text = objectDetail.LICENSE_OFC;

            if (objectDetail.SALE_AGENT_DATA != null && (!String.IsNullOrEmpty(objectDetail.SALE_AGENT_DATA.SALE_AGENT_NAME)))
            {
                txtSaleAgentName.Text = objectDetail.SALE_AGENT_DATA.SALE_AGENT_NAME;
                txtSaleAgentUplName.Text = objectDetail.SALE_AGENT_DATA.SALE_AGENT_UPLINE_NAME;
                if (objectDetail.SALE_AGENT_DATA.LICENSE_VALIDATE == "N")
                {
                    txtSaleAgent.BackColor = Color.FromArgb(bgRed[0], bgRed[1], bgRed[2]);
                    txtSaleAgentName.BackColor = Color.FromArgb(bgRed[0], bgRed[1], bgRed[2]);
                }
                else
                {
                    txtSaleAgent.BackColor = Color.FromArgb(bgYellow[0], bgYellow[1], bgYellow[2]);
                    txtSaleAgentName.BackColor = SystemColors.Control;
                }
            }
            else
            {
                txtSaleAgentName.Text = "";
                txtSaleAgentUplName.Text = "";
                txtSaleAgent.BackColor = Color.FromArgb(bgYellow[0], bgYellow[1], bgYellow[2]);
                txtSaleAgentName.BackColor = SystemColors.Control;
            }

            if (cmbWorkGroup.SelectedValue.ToString() == "BNC")
            {
                txtLicenseAgentName.Text = txtSaleAgentUplName.Text;
            }
            else
            {
                txtLicenseAgentName.Text = txtSaleAgentName.Text;
            }



            txtStrDt.Text = objectDetail.STR_DT == null ? "" : Utility.dateTimeToString(objectDetail.STR_DT.Value, "dd/MM/yyyy", "BU");
            txtMktDate.Text = objectDetail.MARKETING_DT == null ? "" : Utility.dateTimeToString(objectDetail.MARKETING_DT.Value, "dd/MM/yyyy", "BU");

        }
        private void GetUStatusID(NewBisPASvcRef.U_STATUS_ID objectDetail)
        {
            var GetData = from getData in PAR_AUTB_STATUS_COLLECTION
                          where getData.STATUS == objectDetail.STATUS
                          select getData;
            if (GetData != null && GetData.Count() > 0)
            {
                txtStatus.Text = GetData.ToArray()[0].DESCRIPTION;
            }
            else
            {
                txtStatus.Text = "";
            }
        }
        private void GetPlanParametersOld(NewBisPASvcRef.U_APPLICATION_Collection objectDetail)
        {
            SetPlanParametersOld();
            var tempUNameId = objectDetail[0].NAME_ID_Collection;
            if (tempUNameId != null && tempUNameId.Any())
            {
                NewBisPASvcRef.U_NAME_ID uNameIDObj = tempUNameId.First();
                OLD_BIRTH_DATE = uNameIDObj.BIRTH_DT == null ? "" : Utility.dateTimeToString(uNameIDObj.BIRTH_DT.Value, "dd/MM/yyyy", "BU");
                string channelType = cmbChannelType.SelectedValue == null ? "" : cmbChannelType.SelectedValue.ToString();
                foreach (NewBisPASvcRef.U_APPLICATION obj in objectDetail)
                {
                    if (obj.LIFE_ID != null && obj.LIFE_ID.UAPP_ID != null)
                    {
                        if (obj.LIFE_ID.FREE_FLG == "Y" || IsChannelTypeFreePolicy(channelType))
                        {
                            OLD_PLAN_FREE = true;
                            OLD_PLAN_CODE_FREE = obj.LIFE_ID.PL_CODE + obj.LIFE_ID.PL_CODE2;
                            OLD_MODE_FREE = obj.LIFE_ID.P_MODE == null ? "" : obj.LIFE_ID.P_MODE.Value.ToString();
                            OLD_SUMM_FREE = obj.LIFE_ID.SUMM == null ? "" : obj.LIFE_ID.SUMM.Value.ToString();
                            OLD_ISU_DT_FREE = obj.LIFE_ID.ISU_DT == null ? "" : Utility.dateTimeToString(obj.LIFE_ID.ISU_DT.Value, "dd/MM/yyyy", "BU");
                        }
                        else if (obj.LIFE_ID.FREE_FLG == "N")
                        {
                            OLD_PLAN_PAID = true;
                            OLD_PLAN_CODE_PAID = obj.LIFE_ID.PL_CODE + obj.LIFE_ID.PL_CODE2;
                            OLD_MODE_PAID = obj.LIFE_ID.P_MODE == null ? "" : obj.LIFE_ID.P_MODE.Value.ToString();
                            OLD_SUMM_PAID = obj.LIFE_ID.SUMM == null ? "" : obj.LIFE_ID.SUMM.Value.ToString();
                            OLD_ISU_DT_PAID = obj.LIFE_ID.ISU_DT == null ? "" : Utility.dateTimeToString(obj.LIFE_ID.ISU_DT.Value, "dd/MM/yyyy", "BU");
                            OLD_PREMIUM_PAID = obj.LIFE_ID.PREMIUM == null ? "" : obj.LIFE_ID.PREMIUM.Value.ToString();
                            if (obj.LIFE_ID.APP_SPOUSE_Collection != null && obj.LIFE_ID.APP_SPOUSE_Collection.Count() > 0)
                            {
                                OLD_SPOUSE = true;
                                if (PAR_U_SPOUSE_ID_COLL != null && PAR_U_SPOUSE_ID_COLL.Count() > 0)
                                {
                                    NewBisPASvcRef.U_SPOUSE_ID uSpouseIDObj = new NewBisPASvcRef.U_SPOUSE_ID();
                                    uSpouseIDObj = PAR_U_SPOUSE_ID_COLL.ToArray()[0];
                                    OLD_BIRTH_DATE_SPOUSE = uSpouseIDObj.BIRTH_DT == null ? "" : Utility.dateTimeToString(uSpouseIDObj.BIRTH_DT.Value, "dd/MM/yyyy", "BU");
                                }
                            }
                        }
                    }
                }
            }
        }
        private void GetUCreditCard(NewBisPASvcRef.U_CREDIT_CARD objectDetail)
        {
            cmbPayOption.SelectedValue = objectDetail.PAY_OPTION == null ? "RE" : objectDetail.PAY_OPTION;
            txtCardNo1.Text = objectDetail.CARD_NO.Substring(0, 4);
            txtCardNo2.Text = objectDetail.CARD_NO.Substring(4, 4);
            txtCardNo3.Text = objectDetail.CARD_NO.Substring(8, 4);
            txtCardNo4.Text = objectDetail.CARD_NO.Substring(12, 4);
            if (objectDetail.CDC_DESCRIPTION_OBJ != null)
            {
                txtCreditCardName.Text = objectDetail.CDC_DESCRIPTION_OBJ.CCDC_THAI_DESC;
                txtCreditCardTypeName.Text = objectDetail.CDC_DESCRIPTION_OBJ.CARD_TYPE_NAME;
            }
            txtCreditCardType.Text = objectDetail.CARD_TYPE;
            txtFinCode.Text = objectDetail.FIN_CODE;
            txtExpireMM.Text = objectDetail.EXPIRE_MM == null ? "" : objectDetail.EXPIRE_MM.Value.ToString();
            txtExpireYY.Text = objectDetail.EXPIRE_Y4 == null ? "" : objectDetail.EXPIRE_Y4.Value.ToString();
        }
        private void GetOldDataPNameIDEventSearch(NewBisPASvcRef.PA_APPLICATION_ID objectDetail)
        {
            if (objectDetail.APP_ISIS != null && objectDetail.APP_ISIS.APP_ID != null)
            {
                if (objectDetail.APP_ISIS.NEWBIS == 'N')
                {
                    VerifyFlag = true;
                    if ((PAR_P_NAME_ID_COLL != null && PAR_P_NAME_ID_COLL.Count() > 0) || (PAR_P_PARENT_ID_COLL != null && PAR_P_PARENT_ID_COLL.Count() > 0))
                    {
                        NewBisPASvcRef.P_NAME_ID_COLLECTION pNameIDColl = new NewBisPASvcRef.P_NAME_ID_COLLECTION();
                        if (PAR_P_NAME_ID_COLL != null && PAR_P_NAME_ID_COLL.Count() > 0)
                        {
                            pNameIDColl.AddRange(PAR_P_NAME_ID_COLL.ToArray());
                        }
                        NewBisPASvcRef.P_PARENT_ID_COLLECTION pParentIDColl = new NewBisPASvcRef.P_PARENT_ID_COLLECTION();
                        if (PAR_P_PARENT_ID_COLL != null && PAR_P_PARENT_ID_COLL.Count() > 0)
                        {
                            pParentIDColl.AddRange(PAR_P_PARENT_ID_COLL.ToArray());
                        }

                        SetColorItemBeginCustomerName();

                        if ((pNameIDColl != null && pNameIDColl.Count() > 0) || (pParentIDColl != null && pParentIDColl.Count() > 0))
                        {

                            OldCustomerDataForm oldCusDataForm = new OldCustomerDataForm();
                            oldCusDataForm.NameID = NAME_ID;
                            oldCusDataForm.P_NAME_ID_COLL = new NewBisPASvcRef.P_NAME_ID_COLLECTION();
                            if (pNameIDColl != null && pNameIDColl.Count() > 0)
                            {
                                oldCusDataForm.P_NAME_ID_COLL.AddRange(pNameIDColl.ToArray());
                            }
                            oldCusDataForm.ParentID = PARENT_ID;
                            oldCusDataForm.P_PARENT_ID_COLL = new NewBisPASvcRef.P_PARENT_ID_COLLECTION();
                            if (pParentIDColl != null && pParentIDColl.Count() > 0)
                            {
                                oldCusDataForm.P_PARENT_ID_COLL.AddRange(pParentIDColl.ToArray());
                            }



                            oldCusDataForm.ShowDialog();

                            if (oldCusDataForm.P_NAME_ID_OBJ != null && oldCusDataForm.P_NAME_ID_OBJ.NAME_ID != null)
                            {
                                NAME_ID = oldCusDataForm.P_NAME_ID_OBJ.NAME_ID;
                                cmbDataCus.BackColor = Color.FromArgb(bgRed[0], bgRed[1], bgRed[2]);
                                cmbDataCus.SelectedValue = "1";
                                ChkOldCusDataWithNewCusData(oldCusDataForm.P_NAME_ID_OBJ);
                                btnCustomerData.PerformClick();

                            }
                            else
                            {
                                NAME_ID = null;
                                cmbDataCus.BackColor = Color.White;
                                cmbDataCus.SelectedValue = "2";

                                if (oldCusDataForm.P_PARENT_ID_OBJ != null && oldCusDataForm.P_PARENT_ID_OBJ.PARENT_ID != null)
                                {
                                    ChkOldParentDataWithNewCusData(oldCusDataForm.P_PARENT_ID_OBJ);
                                }
                            }

                            if (oldCusDataForm.P_PARENT_ID_OBJ != null && oldCusDataForm.P_PARENT_ID_OBJ.PARENT_ID != null)
                            {
                                PARENT_ID = oldCusDataForm.P_PARENT_ID_OBJ.PARENT_ID;
                            }
                            else
                            {
                                PARENT_ID = null;
                            }
                        }
                        else
                        {
                            NAME_ID = null;
                            PARENT_ID = null;
                            cmbDataCus.BackColor = Color.White;
                            cmbDataCus.SelectedValue = "2";
                            SetColorItemBeginCustomerAddress();
                        }

                        if (txtBirthDt.Text.Trim() != "")
                        {
                            Int64? ageCal = null;
                            NewBisPASvcRef.ProcessResult pr = new NewBisPASvcRef.ProcessResult();
                            using (NewBisPASvcRef.NewBisPASvcClient clientAge = new NewBisPASvcRef.NewBisPASvcClient())
                            {
                                DateTime? dateCal = null;
                                dateCal = Utility.StringToDateTime(txtAppSignDt.Text, "BU");
                                if (PAR_PA_APPLICATION_ID != null && PAR_PA_APPLICATION_ID.APP_ID != null)
                                {
                                    if (PAR_PA_APPLICATION_ID.APPLICATION_Collection != null && PAR_PA_APPLICATION_ID.APPLICATION_Collection.Count() > 0)
                                    {
                                        foreach (NewBisPASvcRef.U_APPLICATION obj in PAR_PA_APPLICATION_ID.APPLICATION_Collection)
                                        {
                                            if (obj.LIFE_ID != null && obj.LIFE_ID.UAPP_ID != null)
                                            {
                                                dateCal = obj.LIFE_ID.ISU_DT;
                                            }
                                        }
                                    }
                                }
                                if (cmbChannelType.SelectedValue == null || cmbChannelType.SelectedValue.ToString() == "")
                                {
                                    cmbChannelType.Focus();
                                    throw new Exception("กรุณาระบุช่องทางการขายที่ท่านต้องการ");
                                }
                                String channelType = cmbChannelType.SelectedValue.ToString();
                                DateTime? birthDateCal = Utility.StringToDateTime(txtBirthDt.Text, "BU");
                                ageCal = clientAge.GetAge(out pr, birthDateCal, dateCal, dateCal, channelType);
                                if (pr.Successed == false)
                                {
                                    throw new Exception(pr.ErrorMessage);
                                }
                            }

                            txtStAge.Text = ageCal == null ? "" : ageCal.Value.ToString();
                        }
                        else
                        {
                            txtStAge.Text = "";
                        }

                        if (NAME_ID == null)
                        {
                            cmbAdrType.SelectedValue = "2";
                            cmbAdrType.BackColor = Color.White;
                            ADDRESS_ID = null;
                        }
                        //Show ข้อมูลที่อยู่เก่าของลูกค้า ======================================================
                        ShowAddressDialog();
                        //END Show ข้อมูลที่อยู่เก่าของลูกค้า ==================================================
                    }
                }
            }
        }
        private void GetUApproved(NewBisPASvcRef.U_APPROVED objectDetail)
        {
            txtCertNo.Text = objectDetail.POLICY;
            txtApprovedDt.Text = objectDetail.APPROVE_DT == null ? "" : Utility.dateTimeToString(objectDetail.APPROVE_DT.Value, "dd/MM/yyyy", "BU");
            txtPolicyDt.Text = objectDetail.POLICY_DT == null ? "" : Utility.dateTimeToString(objectDetail.POLICY_DT.Value, "dd/MM/yyyy", "BU");
            txtInstallDt.Text = objectDetail.INSTALL_DT == null ? "" : Utility.dateTimeToString(objectDetail.INSTALL_DT.Value, "dd/MM/yyyy", "BU");
            if (objectDetail.DMS_CHANGE == 'Y')
            {
                chbDmsChange.Checked = true;
            }
            else
            {
                chbDmsChange.Checked = false;
            }
        }

        private void ShowAddressDialog()
        {
            if (NAME_ID != null)
            {
                if (cmbAddressType.SelectedValue != null)
                {
                    if (cmbAddressType.SelectedValue.ToString() != "" && ADDRESS_ID == null)
                    {

                        SetColorItemBeginCustomerAddress();

                        NewBisPASvcRef.ProcessResult pr = new NewBisPASvcRef.ProcessResult();
                        NewBisPASvcRef.P_ADDRESS_ID_COLLECTION addressIDColl = new NewBisPASvcRef.P_ADDRESS_ID_COLLECTION();
                        using (NewBisPASvcRef.NewBisPASvcClient client = new NewBisPASvcRef.NewBisPASvcClient())
                        {
                            pr = client.GetOldAddressByNameID(out addressIDColl, NAME_ID);
                            if (pr.Successed == false)
                            {
                                throw new Exception(pr.ErrorMessage);
                            }
                        }

                        if (addressIDColl != null && addressIDColl.Count() > 0)
                        {
                            ShowOldAdrressByNameIDForm ShowOldAdrressByNameIDForm = new ShowOldAdrressByNameIDForm();
                            ShowOldAdrressByNameIDForm.P_ADDRESS_ID_COLL = new NewBisPASvcRef.P_ADDRESS_ID_COLLECTION();
                            ShowOldAdrressByNameIDForm.P_ADDRESS_ID_COLL = addressIDColl;
                            ShowOldAdrressByNameIDForm.ADDRESS_ID = ADDRESS_ID;
                            ShowOldAdrressByNameIDForm.ShowDialog();

                            if (ShowOldAdrressByNameIDForm.P_ADDRESS_ID_OBJ != null && ShowOldAdrressByNameIDForm.P_ADDRESS_ID_OBJ.ADDRESS_ID != null)
                            {
                                ADDRESS_ID = ShowOldAdrressByNameIDForm.P_ADDRESS_ID_OBJ.ADDRESS_ID;
                                cmbAdrType.SelectedValue = "1";
                                cmbAdrType.BackColor = Color.FromArgb(bgRed[0], bgRed[1], bgRed[2]);
                                ChkOldAddressWithNewAddress(ShowOldAdrressByNameIDForm.P_ADDRESS_ID_OBJ);
                            }
                            else
                            {
                                ADDRESS_ID = null;
                                cmbAdrType.SelectedValue = "2";
                                cmbAdrType.BackColor = Color.White;
                            }

                        }
                    }
                }
            }
        }

        private void ShowParentAddressDialog()
        {
            if (NAME_ID != null)
            {
                if (cmbAddressType.SelectedValue != null)
                {
                    if (cmbAddressType.SelectedValue.ToString() != "" && ADDRESS_ID == null)
                    {

                        SetColorItemBeginCustomerAddress();

                        NewBisPASvcRef.ProcessResult pr = new NewBisPASvcRef.ProcessResult();
                        NewBisPASvcRef.P_ADDRESS_ID_COLLECTION addressIDColl = new NewBisPASvcRef.P_ADDRESS_ID_COLLECTION();
                        using (NewBisPASvcRef.NewBisPASvcClient client = new NewBisPASvcRef.NewBisPASvcClient())
                        {
                            pr = client.GetOldAddressByNameID(out addressIDColl, NAME_ID);
                            if (pr.Successed == false)
                            {
                                throw new Exception(pr.ErrorMessage);
                            }
                        }

                        if (addressIDColl != null && addressIDColl.Count() > 0)
                        {
                            ShowOldAdrressByNameIDForm ShowOldAdrressByNameIDForm = new ShowOldAdrressByNameIDForm();
                            ShowOldAdrressByNameIDForm.P_ADDRESS_ID_COLL = new NewBisPASvcRef.P_ADDRESS_ID_COLLECTION();
                            ShowOldAdrressByNameIDForm.P_ADDRESS_ID_COLL = addressIDColl;
                            ShowOldAdrressByNameIDForm.ADDRESS_ID = ADDRESS_ID;
                            ShowOldAdrressByNameIDForm.ShowDialog();

                            if (ShowOldAdrressByNameIDForm.P_ADDRESS_ID_OBJ != null && ShowOldAdrressByNameIDForm.P_ADDRESS_ID_OBJ.ADDRESS_ID != null)
                            {
                                ADDRESS_ID = ShowOldAdrressByNameIDForm.P_ADDRESS_ID_OBJ.ADDRESS_ID;
                                cmbAdrType.SelectedValue = "1";
                                cmbAdrType.BackColor = Color.FromArgb(bgRed[0], bgRed[1], bgRed[2]);
                                ChkOldAddressWithNewAddress(ShowOldAdrressByNameIDForm.P_ADDRESS_ID_OBJ);
                            }
                            else
                            {
                                ADDRESS_ID = null;
                                cmbAdrType.SelectedValue = "2";
                                cmbAdrType.BackColor = Color.White;
                            }

                        }
                    }
                }
            }
        }

        #endregion
        #region "UNDERWRITE"
        private void GetWUnderwriteID(NewBisPASvcRef.W_UNDERWRITE_ID objectDetail)
        {
            if (objectDetail.SUBUNDERWRITE_ID_Collection != null && objectDetail.SUBUNDERWRITE_ID_Collection.Count() > 0)
            {
                NewBisPASvcRef.W_SUBUNDERWRITE_ID wSubUnderwriteIDObj = new NewBisPASvcRef.W_SUBUNDERWRITE_ID();
                wSubUnderwriteIDObj = objectDetail.SUBUNDERWRITE_ID_Collection[0];
                GetWSubUnderwriteID(wSubUnderwriteIDObj);
            }
        }
        private void GetWSubUnderwriteID(NewBisPASvcRef.W_SUBUNDERWRITE_ID objectDetail)
        {
            cmbUnderWrite.SelectedValue = objectDetail.CUSTOMER_TYPE == null ? "" : objectDetail.CUSTOMER_TYPE;

            if (objectDetail.SUMMARY_Collection != null && objectDetail.SUMMARY_Collection.Count() > 0)
            {
                NewBisPASvcRef.W_SUMMARY wSummaryObj = new NewBisPASvcRef.W_SUMMARY();
                wSummaryObj = objectDetail.SUMMARY_Collection[0];
                GetWSummary(wSummaryObj);
            }
        }
        private void GetWSummary(NewBisPASvcRef.W_SUMMARY objectDetail)
        {
            PAR_W_SUMMARY = ITUtility.GenericConverter<NewBisPASvcRef.W_SUMMARY, NewBisPASvcRef.W_SUMMARY>.Convert(objectDetail);
            //PAR_W_SUMMARY = objectDetail;
            cmbSummryStatus.SelectedValue = objectDetail.STATUS == null ? "" : objectDetail.STATUS;
            String subStatus = objectDetail.SUBSTATUS == null ? "" : objectDetail.SUBSTATUS;
            GetControlsUnderStatus(cmbSummryStatus.SelectedValue.ToString(), subStatus);

            if (!(String.IsNullOrEmpty(objectDetail.STATUS_CAUSE)))
            {
                cmbSummaryStatusCause.SelectedValue = objectDetail.STATUS_CAUSE == null ? "" : objectDetail.STATUS_CAUSE;
            }

            if (objectDetail.SUMMARY_UNDERWRITER_Collection != null && objectDetail.SUMMARY_UNDERWRITER_Collection.Count() > 0)
            {
                NewBisPASvcRef.W_SUMMARY_UNDERWRITER wSummaryUnderwriterObj = new NewBisPASvcRef.W_SUMMARY_UNDERWRITER();
                wSummaryUnderwriterObj = objectDetail.SUMMARY_UNDERWRITER_Collection[0];

                String[] statusEnd = { "IF", "CC", "DC", "EX", "NT", "PP" };
                if (statusEnd.Contains(cmbSummryStatus.SelectedValue.ToString()))
                {
                    cmbUnderWriteID.SelectedValue = wSummaryUnderwriterObj.UND_ID == null ? "" : wSummaryUnderwriterObj.UND_ID;
                }
                else
                {
                    cmbUnderWriteID.SelectedValue = UserID;
                }
            }

            PAR_U_MEMO_ID_COLL_TMP = new NewBisPASvcRef.U_MEMO_ID_Collection();
            if (objectDetail.MEMO_ID_Collection != null && objectDetail.MEMO_ID_Collection.Count() > 0)
            {
                PAR_U_MEMO_ID_COLL_TMP.AddRange(objectDetail.MEMO_ID_Collection.ToArray());
            }

            PAR_W_SUMMARY_DETAIL_COLL = new NewBisPASvcRef.W_SUMMARY_DETAIL_Collection();
            if (objectDetail.SUMMARY_DETAIL_Collection != null && objectDetail.SUMMARY_DETAIL_Collection.Count() > 0)
            {
                PAR_W_SUMMARY_DETAIL_COLL.AddRange(objectDetail.SUMMARY_DETAIL_Collection);
            }
        }
        #endregion


        public void SelectAppFromMenu(String FromAction, FormApplicationSelector FormNameSelect, FormApplicationViewer FormNameView, String appNo, String channelType)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                ApplicationBeginParameter();
                btnHeadClear.Visible = false;


                FORMAPP = FromAction;
                if (FromAction == "SELECT")
                {
                    ApplicationSelectorForm = FormNameSelect;
                    ApplicationSelectorForm.Hide();
                }
                else if (FromAction == "VIEW")
                {
                    ApplicationViewerForm = FormNameView;
                    ApplicationViewerForm.Hide();
                }

                txtAppNo.Text = appNo.ToUpper();
                cmbChannelType.SelectedValue = channelType;
                SetChannelFromApp();

                //Set Parameter For Search ==============================================
                txtAppNo.ReadOnly = true;
                txtCertNo.ReadOnly = true;
                txtPolicyHolding.ReadOnly = true;
                btnHeadSave.Enabled = true;
                btnScan.Enabled = true;
                btnCustomerData.Enabled = true;
                cmbChannelType.Enabled = false;
                cmbWorkGroup.Enabled = false;
                cmbAppNameID.Enabled = false;
                U_APP_REMARK_COLL = new NewBisPASvcRef.U_APP_REMARK_Collection();
                btnAppRemark.BackColor = Color.Transparent;
                //END Set Parameter For Search ===========================================

                SearchData();
                // GetOldDataPNameIDEventSearch(PAR_PA_APPLICATION_ID);

                APP_LOCK_ERROR = false;

                if (FromAction == "SELECT")
                {
                    if (PAR_U_APPLICATION_LOCK != null && PAR_U_APPLICATION_LOCK.UAPP_ID != null)
                    {
                        if (PAR_U_APPLICATION_LOCK.TRANSACTION_FLG == 'P')
                        {
                            NewBisPASvcRef.ProcessResult pr = new NewBisPASvcRef.ProcessResult();
                            using (NewBisPASvcRef.NewBisPASvcClient client = new NewBisPASvcRef.NewBisPASvcClient())
                            {
                                PAR_U_APPLICATION_LOCK.TRANSACTION_FLG = 'P';
                                pr = client.AdjustUApplicationLock(ref PAR_U_APPLICATION_LOCK, "UPDATE");
                                if (pr.Successed == false)
                                {
                                    throw new Exception(pr.ErrorMessage);
                                }
                            }
                        }
                        else if (PAR_U_APPLICATION_LOCK.TRANSACTION_FLG == 'U')
                        {
                            APP_LOCK_ERROR = true;
                            throw new Exception("เลที่ใบคำขอ " + txtAppNo.Text + " กำลังพิจารณาโดย รหัสพนักงาน " + PAR_U_APPLICATION_LOCK.UPD_ID);
                        }
                        else
                        {
                            APP_LOCK_ERROR = true;
                            throw new Exception("เกิดข้อผิดพลาดในการ Lock ข้อมูลของเลขที่ใบคำ " + txtAppNo.Text + " กรุณาติดต่อ IT Tel.8518");
                        }
                    }
                    else
                    {
                        APP_LOCK_ERROR = true;
                        throw new Exception("เกิดข้อผิดพลาดในการ Lock ข้อมูลของเลขที่ใบคำ " + txtAppNo.Text + " กรุณาติดต่อ IT Tel.8518");
                    }
                }
                else if (FromAction == "VIEW")
                {
                    btnHeadSave.Visible = false;
                }

            }
            catch (Exception ex)
            {
                btnHeadSave.Visible = false;
                SetMsgException(ex);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }

        }

        private void SetChannelFromApp()
        {
            SetControlByChannelType(cmbChannelType.SelectedValue.ToString());
            if (PAR_AUTB_CHANNEL_BLOCK_COLLECTION != null && PAR_AUTB_CHANNEL_BLOCK_COLLECTION.Count() > 0)
            {
                var WorkGroupLinq = from workGroupLinq in PAR_AUTB_CHANNEL_BLOCK_COLLECTION
                                    where workGroupLinq.CHANNEL_TYPE == cmbChannelType.SelectedValue.ToString()
                                    select workGroupLinq;
                if (WorkGroupLinq != null && WorkGroupLinq.Count() > 0)
                {

                    NewBisPASvcRef.AUTB_CHANNEL_BLOCK_COLLECTION channelBlocks = new NewBisPASvcRef.AUTB_CHANNEL_BLOCK_COLLECTION();
                    NewBisPASvcRef.AUTB_CHANNEL_BLOCK channelBlockObj = new NewBisPASvcRef.AUTB_CHANNEL_BLOCK();
                    channelBlockObj.WORK_GROUP = "";
                    channelBlockObj.PL_BLOCK = "";
                    channelBlockObj.CHANNEL_TYPE = "";
                    channelBlockObj.WORK_GROUP_NAME = "กรุณาระบุกลุ่มงานที่ต้องการ";
                    channelBlocks.Add(channelBlockObj);
                    channelBlocks.AddRange(WorkGroupLinq.ToArray());
                    cmbWorkGroup.DataSource = channelBlocks;
                    cmbWorkGroup.DisplayMember = "WORK_GROUP_NAME";
                    cmbWorkGroup.ValueMember = "WORK_GROUP";
                    cmbWorkGroup.SelectedValue = WorkGroupLinq.ToArray()[0].WORK_GROUP;

                    NewBisPASvcRef.AUTB_CHANNEL_BLOCK_COLLECTION plBlockS = new NewBisPASvcRef.AUTB_CHANNEL_BLOCK_COLLECTION();
                    plBlockS.AddRange(WorkGroupLinq.ToArray());
                    cmbFreePlBlock.DataSource = plBlockS;
                    cmbFreePlBlock.DisplayMember = "PL_BLOCK";
                    cmbFreePlBlock.ValueMember = "PL_BLOCK";
                    cmbFreePlBlock.SelectedIndex = 0;

                    NewBisPASvcRef.AUTB_CHANNEL_BLOCK[] paidPlBlockS = new NewBisPASvcRef.AUTB_CHANNEL_BLOCK[plBlockS.Count()];
                    plBlockS.CopyTo(paidPlBlockS);
                    cmbPaidPlBlock.DataSource = plBlockS;
                    cmbPaidPlBlock.DisplayMember = "PL_BLOCK";
                    cmbPaidPlBlock.ValueMember = "PL_BLOCK";
                    cmbPaidPlBlock.SelectedIndex = 0;
                }
            }

            if (PAR_AUTB_APPNAME_ID_COLLECTION != null && PAR_AUTB_APPNAME_ID_COLLECTION.Count() > 0)
            {
                var GetAppName = from getAppName in PAR_AUTB_APPNAME_ID_COLLECTION
                                 where getAppName.CHANNEL_TYPE == cmbChannelType.SelectedValue.ToString()
                                 select getAppName;
                if (GetAppName != null && GetAppName.Count() > 0)
                {
                    NewBisPASvcRef.AUTB_APPNAME_ID_COLLECTION appNameIDs = new NewBisPASvcRef.AUTB_APPNAME_ID_COLLECTION();
                    NewBisPASvcRef.AUTB_APPNAME_ID appNameIDObj = new NewBisPASvcRef.AUTB_APPNAME_ID();
                    appNameIDObj.APPNAME_ID = 0;
                    appNameIDObj.TITLE = "กรุณาระบุเอกสารชุดใบคำขอที่ต้องการ";
                    appNameIDObj.CHANNEL_TYPE = "";
                    appNameIDObj.WORK_GROUP = "";
                    appNameIDs.Add(appNameIDObj);
                    appNameIDs.AddRange(GetAppName.ToArray());
                    cmbAppNameID.DataSource = appNameIDs;
                    cmbAppNameID.DisplayMember = "TITLE";
                    cmbAppNameID.ValueMember = "APPNAME_ID";
                    cmbAppNameID.SelectedValue = GetAppName.ToArray()[0].APPNAME_ID;
                }
                else
                {
                    NewBisPASvcRef.AUTB_APPNAME_ID_COLLECTION appNameIDs = new NewBisPASvcRef.AUTB_APPNAME_ID_COLLECTION();
                    NewBisPASvcRef.AUTB_APPNAME_ID appNameIDObj = new NewBisPASvcRef.AUTB_APPNAME_ID();
                    appNameIDObj.APPNAME_ID = 0;
                    appNameIDObj.TITLE = "กรุณาระบุเอกสารชุดใบคำขอที่ต้องการ";
                    appNameIDObj.CHANNEL_TYPE = "";
                    appNameIDObj.WORK_GROUP = "";
                    appNameIDs.Add(appNameIDObj);
                    cmbAppNameID.DataSource = appNameIDs;
                    cmbAppNameID.DisplayMember = "TITLE";
                    cmbAppNameID.ValueMember = "APPNAME_ID";
                }
            }
        }



    }

}
