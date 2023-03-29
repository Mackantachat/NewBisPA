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
using NewBisPA.Model;
using NewBisPA.Library;
using System.Net.Mime;
using NewBisPA.Library.Resource;

namespace NewBisPA
{
    partial class ApplicationForm
    {
        private void EditData()
        {
            ObjectData();
        }
        private void ObjectData()
        {
            ObjUApplicationID();
            CutCaseApplication();
            CheckIsuDateBeforeSave();
            SaveData();
        }

        private void ObjUApplicationID()
        {
            PAR_PA_APPLICATION_ID.CHANNEL_TYPE = cmbChannelType.SelectedValue == null ? "" : cmbChannelType.SelectedValue.ToString();
            PAR_PA_APPLICATION_ID.WORK_GROUP = cmbWorkGroup.SelectedValue == null ? "" : cmbWorkGroup.SelectedValue.ToString();
            PAR_PA_APPLICATION_ID.APP_NO = txtAppNo.Text.Trim();
            PAR_PA_APPLICATION_ID.REGISTER_ID = UserID;
            PAR_PA_APPLICATION_ID.CAL_ERROR = new NewBisPASvcRef.U_CAL_ERROR();
            PAR_PA_APPLICATION_ID.CAL_ERROR = PAR_U_CAL_ERROR;
            ObjUApplication();
            ObjUBenefitID();
            MakeAppRemark();
            // USmartID();
            PaymentReturnPremium();
            ValidatePlanData(PAR_PA_APPLICATION_ID.APPLICATION_Collection);
            MakeUPolPrintingException();
            String spouseFlg = "";
            if (PAR_PA_APPLICATION_ID.APPLICATION_Collection != null && PAR_PA_APPLICATION_ID.APPLICATION_Collection.Count() > 0)
            {
                foreach (NewBisPASvcRef.U_APPLICATION uAppSpouse in PAR_PA_APPLICATION_ID.APPLICATION_Collection)
                {
                    if (uAppSpouse.LIFE_ID.SPOUSE_FLG == "T")
                    {
                        spouseFlg = "Y";
                        break;
                    }
                }
            }

            ValidateBenefitData(PAR_BENEFIT_ID_COLL, spouseFlg);
            ValidateUApplicationName(PAR_U_APPLICATION_NAME);
            //บันทึกข้อมูลลง Object U_ADDRESS_ID ====================================================================================
            if (PAR_PA_APPLICATION_ID.APPLICATION_Collection != null && PAR_PA_APPLICATION_ID.APPLICATION_Collection.Count() > 0)
            {

                var uAddressIDArr = new List<NewBisPASvcRef.U_ADDRESS_ID>();
                NewBisPASvcRef.U_ACCOUNT_ID[] uAccountIDArr = new NewBisPASvcRef.U_ACCOUNT_ID[PAR_PA_APPLICATION_ID.APPLICATION_Collection.Count()];

                var uPAAppIndex = 0;
                for (int i = 0; i < PAR_PA_APPLICATION_ID.APPLICATION_Collection.Count(); i++)
                {
                    if (PAR_PA_APPLICATION_ID.APPLICATION_Collection[i] != null)
                    {
                        NewBisPASvcRef.U_APPLICATION uApplicationObj = PAR_PA_APPLICATION_ID.APPLICATION_Collection[i];
                        // START U_NAME_ID ==============================================================================
                        #region "U_NAME_ID"
                        if (uApplicationObj.NAME_ID_Collection != null && uApplicationObj.NAME_ID_Collection.Count() > 0)
                        {


                            foreach (NewBisPASvcRef.U_NAME_ID uNameIDObj in uApplicationObj.NAME_ID_Collection)
                            {

                                if (uNameIDObj.CUSTOMER_TYPE == "C")
                                {
                                    //ADD U_NAME_ID =======================================================================================
                                    NewBisPASvcRef.U_NAME_ID uNameIDNew = new NewBisPASvcRef.U_NAME_ID();
                                    uNameIDNew = uNameIDObj;
                                    ObjUNameID(ref uNameIDNew);
                                    //END ADD U_NAME_ID ====================================================================================

                                }
                                else if (uNameIDObj.CUSTOMER_TYPE == "P")
                                {
                                    //ADD U_NAME_ID =======================================================================================
                                    NewBisPASvcRef.U_NAME_ID uParentNameIDNew = new NewBisPASvcRef.U_NAME_ID();
                                    uParentNameIDNew = uNameIDObj;
                                    ObjUParentNameID(ref uParentNameIDNew);
                                    //END ADD U_NAME_ID ====================================================================================

                                }




                                //ADD APP_ADDRESS ======================================================================================
                                if (uNameIDObj.APP_ADDRESS != null)
                                {
                                    NewBisPASvcRef.U_ADDRESS_ID uAddressIDObj = new NewBisPASvcRef.U_ADDRESS_ID();

                                    if (uNameIDObj.CUSTOMER_TYPE == "C")
                                    {
                                        ObjUAddressID(ref uAddressIDObj);
                                    }
                                    else if (uNameIDObj.CUSTOMER_TYPE == "P")
                                    {
                                        ObjUParentAddressID(ref uAddressIDObj);

                                    }


                                    uAddressIDObj.APP_ADDRESS = new NewBisPASvcRef.U_APP_ADDRESS();
                                    uAddressIDObj.APP_ADDRESS = uNameIDObj.APP_ADDRESS;

                                    uAddressIDObj.ObjId = uNameIDObj.ObjId;
                                    uAddressIDObj.UADDRESS_ID = uNameIDObj.APP_ADDRESS.UADDRESS_ID;

                                    //  uAddressIDArr[uPAAppIndex++] = new NewBisPASvcRef.U_ADDRESS_ID();
                                    uAddressIDArr.Add(uAddressIDObj);
                                }
                                //END ADD APP_ADDRESS ===================================================================================

                                //ADD U_NAME_DOCUMENT ====================================================================================
                                uNameIDObj.NAME_DOCUMENT_Collection = new NewBisPASvcRef.U_NAME_DOCUMENT_Collection();
                                if (ckbIdcardDoc.Checked == true)
                                {
                                    NewBisPASvcRef.U_NAME_DOCUMENT obj = new NewBisPASvcRef.U_NAME_DOCUMENT();
                                    obj.UNAME_ID = uNameIDObj.UNAME_ID;
                                    obj.DOC_CODE = "000000000005";
                                    uNameIDObj.NAME_DOCUMENT_Collection.Add(obj);
                                }
                                if (ckbAddressDoc.Checked == true)
                                {
                                    NewBisPASvcRef.U_NAME_DOCUMENT obj = new NewBisPASvcRef.U_NAME_DOCUMENT();
                                    obj.UNAME_ID = uNameIDObj.UNAME_ID;
                                    obj.DOC_CODE = "000000000132";
                                    uNameIDObj.NAME_DOCUMENT_Collection.Add(obj);
                                }
                                //END ADD U_NAME_DOCUMENT ================================================================================

                                //ADD U_EMAIL_ID =========================================================================================
                                if (txtEmail.Text.Trim() != "")
                                {
                                    uNameIDObj.EMAIL_ID = new NewBisPASvcRef.U_EMAIL_ID();
                                    uNameIDObj.EMAIL_ID.UNAME_ID = uNameIDObj.UNAME_ID;
                                    uNameIDObj.EMAIL_ID.EMAIL = txtEmail.Text.Trim();
                                }
                                //END ADD U_EMAIL_ID =====================================================================================

                                //ADD U_NAME_CODE =========================================================================================
                                if (uNameIDObj.NAME_ID != null)
                                {
                                    uNameIDObj.NAME_CODE = new NewBisPASvcRef.U_NAME_CODE();
                                    uNameIDObj.NAME_CODE.UNAME_ID = uNameIDObj.UNAME_ID;
                                    uNameIDObj.NAME_CODE.NAMECODE = uNameIDObj.NAME_ID.Value.ToString().PadLeft(12, '0');
                                }
                                //END ADD U_NAME_CODE =====================================================================================
                                //uPAAppIndex++;
                            }
                        }
                        #endregion
                        // END U_NAME_ID ==============================================================================

                        //ADD U_APPLICATION_UPDATE ==============================================================================
                        #region "U_APPLICATION_UPDATE"
                        NewBisPASvcRef.U_APPLICATION_UPDATE uApplicationUpdateObj = new NewBisPASvcRef.U_APPLICATION_UPDATE();
                        uApplicationUpdateObj.UAPP_ID = uApplicationObj.UAPP_ID;
                        ObjUApplicationUpdate(ref uApplicationUpdateObj);
                        uApplicationObj.APPLICATION_UPDATE_Collection = new NewBisPASvcRef.U_APPLICATION_UPDATE_Collection();
                        uApplicationObj.APPLICATION_UPDATE_Collection.Add(uApplicationUpdateObj);
                        #endregion
                        //END ADD U_APPLICATION_UPDATE ==========================================================================

                        //ADD U_APPLICATION_NAME ================================================================================
                        #region "U_APPLICATION_NAME"
                        if (PAR_U_APPLICATION_NAME != null && PAR_U_APPLICATION_NAME.APPNAME_ID != null)
                        {
                            NewBisPASvcRef.U_APPLICATION_NAME uApplicationNameObj = new NewBisPASvcRef.U_APPLICATION_NAME();
                            uApplicationNameObj = GenericConverter<NewBisPASvcRef.U_APPLICATION_NAME, NewBisPASvcRef.U_APPLICATION_NAME>.Convert(PAR_U_APPLICATION_NAME);

                            if (uApplicationObj.APPLICATION_NAME_Collection != null && uApplicationObj.APPLICATION_NAME_Collection.Count() > 0)
                            {
                                uApplicationNameObj.UAPP_ID = uApplicationObj.UAPP_ID;
                                uApplicationNameObj.UAPPNAME_ID = uApplicationObj.APPLICATION_NAME_Collection[0].UAPPNAME_ID;
                            }
                            else
                            {
                                uApplicationNameObj.UAPP_ID = uApplicationObj.UAPP_ID;
                                uApplicationNameObj.UAPPNAME_ID = null;
                            }
                            uApplicationNameObj.TMN = 'N';
                            uApplicationObj.APPLICATION_NAME_Collection = new NewBisPASvcRef.U_APPLICATION_NAME_Collection();
                            uApplicationObj.APPLICATION_NAME_Collection.Add(uApplicationNameObj);

                        }
                        #endregion
                        //END ADD U_APPLICATION_NAME ============================================================================

                        //เก็บค่า U_POLICY_SENDING =================================================================================
                        #region "U_POLICY_SENDING"
                        NewBisPASvcRef.U_POLICY_SENDING uPolicySendingObj = new NewBisPASvcRef.U_POLICY_SENDING();
                        uPolicySendingObj.UAPP_ID = uApplicationObj.UAPP_ID;
                        ObjUPolicySending(ref uPolicySendingObj);
                        uApplicationObj.POLICY_SENDING = new NewBisPASvcRef.U_POLICY_SENDING();
                        uApplicationObj.POLICY_SENDING = uPolicySendingObj;

                        if (cmbChannelType.SelectedValue.ToString() == "PF")
                        {

                            string policyDocumentType = cmbPolicyDocmentType.SelectedValue == null ? "" : cmbPolicyDocmentType.SelectedValue.ToString();
                            string ePolicySendingType = cmbEPolicySending.SelectedValue == null ? "" : cmbEPolicySending.SelectedValue.ToString();

                            if (!string.IsNullOrEmpty(policyDocumentType))
                            {
                                if (policyDocumentType == "D")
                                {
                                    uApplicationObj.POLICY_SENDING.DPOLICY_FLG = 'Y';
                                    uApplicationObj.POLICY_SENDING.EPOLICY_FLG = 'N';
                                }
                                else
                                {
                                    if (!string.IsNullOrEmpty(ePolicySendingType))
                                    {
                                        if (policyDocumentType == "E")
                                        {
                                            uApplicationObj.POLICY_SENDING.DPOLICY_FLG = 'N';
                                            uApplicationObj.POLICY_SENDING.EPOLICY_FLG = 'Y';
                                            uApplicationObj.POLICY_SENDING.EPOLICY_SEND_TO = Convert.ToChar(ePolicySendingType);

                                        }
                                        else if (policyDocumentType == "A")
                                        {
                                            uApplicationObj.POLICY_SENDING.DPOLICY_FLG = 'Y';
                                            uApplicationObj.POLICY_SENDING.EPOLICY_FLG = 'Y';
                                            uApplicationObj.POLICY_SENDING.EPOLICY_SEND_TO = Convert.ToChar(ePolicySendingType);
                                        }
                                    }
                                }
                            }

                        }

                        #endregion
                        //END เก็บค่า U_POLICY_SENDING =============================================================================

                        //เก็บค่า U_AGENT ==========================================================================================
                        #region "U_AGENT"
                        if (cmbChannelType.SelectedValue.ToString() != "PD" && cmbChannelType.SelectedValue.ToString() != "PF")
                        {
                            if (uApplicationObj.LIFE_ID != null && (!String.IsNullOrEmpty(uApplicationObj.LIFE_ID.PL_CODE)))
                            {
                                NewBisPASvcRef.U_AGENT uAgentObj = new NewBisPASvcRef.U_AGENT();
                                uAgentObj.UAPP_ID = uApplicationObj.UAPP_ID;
                                uAgentObj.MARKETING_DT = txtMktDate.Text.Trim() == "" ? (DateTime?)null : Utility.StringToDateTime(txtMktDate.Text.Trim(), "BU");
                                uAgentObj.MARKETING_TYPE = uApplicationObj.LIFE_ID.MARKETING_TYPE;
                                ObjUAgent(ref uAgentObj);
                                uApplicationObj.LIFE_ID.AGENT = new NewBisPASvcRef.U_AGENT();
                                uApplicationObj.LIFE_ID.AGENT = uAgentObj;
                            }
                        }
                        else
                        {
                            if (uApplicationObj.LIFE_ID != null && (!String.IsNullOrEmpty(uApplicationObj.LIFE_ID.PL_CODE)))
                            {
                                if (uApplicationObj.LIFE_ID.AGENT != null && (!String.IsNullOrEmpty(uApplicationObj.LIFE_ID.AGENT.SALE_AGENT)))
                                {
                                    NewBisPASvcRef.U_AGENT uAgentObj = new NewBisPASvcRef.U_AGENT();
                                    uAgentObj.UAPP_ID = uApplicationObj.UAPP_ID;
                                    uAgentObj.MARKETING_DT = txtMktDate.Text.Trim() == "" ? (DateTime?)null : Utility.StringToDateTime(txtMktDate.Text.Trim(), "BU");
                                    uAgentObj.MARKETING_TYPE = "CRD";
                                    ObjUAgent(ref uAgentObj);
                                    uApplicationObj.LIFE_ID.AGENT = new NewBisPASvcRef.U_AGENT();
                                    uApplicationObj.LIFE_ID.AGENT = uAgentObj;
                                }
                                else
                                {
                                    NewBisPASvcRef.U_AGENT uAgentObj = new NewBisPASvcRef.U_AGENT();
                                    uAgentObj.UAPP_ID = uApplicationObj.UAPP_ID;
                                    uAgentObj.MARKETING_DT = txtMktDate.Text.Trim() == "" ? (DateTime?)null : Utility.StringToDateTime(txtMktDate.Text.Trim(), "BU");
                                    uAgentObj.MARKETING_TYPE = "CRD";
                                    uAgentObj.SALE_AGENT = "00900000";
                                    uAgentObj.SALE_AGENT_UPL = "00900000";
                                    uAgentObj.LICENSE_AGENT = "00900000";
                                    uAgentObj.STR_DT = txtStrDt.Text.Trim() == "" ? (DateTime?)null : Utility.StringToDateTime(txtStrDt.Text.Trim(), "BU");
                                    uAgentObj.SALE_OFC = "สนญ";
                                    uAgentObj.LICENSE_OFC = "สนญ";
                                    uAgentObj.PAY_COM_FLG = "N";
                                    uApplicationObj.LIFE_ID.AGENT = new NewBisPASvcRef.U_AGENT();
                                    uApplicationObj.LIFE_ID.AGENT = uAgentObj;

                                    txtSaleAgent.Text = "00900000";
                                    txtSaleAgentUpl.Text = "00900000";
                                    txtSaleAgentUpl.Text = "00900000";
                                    txtSaleOfc.Text = "สนญ";
                                    txtLicenseOfc.Text = "สนญ";

                                }
                            }
                        }
                        #endregion
                        //END เก็บค่า U_AGENT ======================================================================================

                        //เก็บค่า U_CREDIT_CARD ====================================================================================
                        #region "U_CREDIT_CARD"

                        if (cmbChannelType.SelectedValue.ToString() == "PD")
                        {
                            if (ClickTabPlanData == true)
                            {
                                NewBisPASvcRef.U_CREDIT_CARD uCreditCardObj = new NewBisPASvcRef.U_CREDIT_CARD();
                                uCreditCardObj.UAPP_ID = uApplicationObj.UAPP_ID;
                                uCreditCardObj.SEQ = 1;
                                ObjCreditCard(ref uCreditCardObj);
                                uApplicationObj.CREDIT_CARD_Collection = new NewBisPASvcRef.U_CREDIT_CARD_Collection();
                                uApplicationObj.CREDIT_CARD_Collection.Add(uCreditCardObj);
                            }
                        }

                        #endregion
                        //END เก็บค่า U_CREDIT_CARD ================================================================================

                        //เก็บค่า U_SMART_ID =======================================================================================
                        #region "U_SMART_ID"
                        if (cmbChannelType.SelectedValue.ToString() == "KB")
                        {
                            NewBisPASvcRef.U_SMART_ID uSmartIDObj = new NewBisPASvcRef.U_SMART_ID();
                            if (uApplicationObj.SMART_ID_Collection != null && uApplicationObj.SMART_ID_Collection.Count() > 0)
                            {
                                uSmartIDObj = uApplicationObj.SMART_ID_Collection[0];
                                uSmartIDObj.ObjId = i;
                            }
                            else
                            {
                                uSmartIDObj.ObjId = i;
                                uSmartIDObj.UAPP_ID = uApplicationObj.UAPP_ID;
                                uSmartIDObj.UACCOUNT_ID = null;
                                uSmartIDObj.DOCUMENT_DT = txtAppSignDt.Text.Trim() == "" ? (DateTime?)null : Utility.StringToDateTime(txtAppSignDt.Text.Trim(), "BU");
                                uSmartIDObj.RECEIVE_DT = uSmartIDObj.DOCUMENT_DT;
                                uSmartIDObj.TRANACTION_DT = DateTime.Now;
                                uSmartIDObj.DOCUMENT_SOURCE = "A";
                                uSmartIDObj.FSYSTEM_DT = DateTime.Now;
                                uSmartIDObj.UPD_DT = DateTime.Now;
                                uSmartIDObj.UPD_ID = UserID;
                                uSmartIDObj.TMN = 'N';
                            }

                            uAccountIDArr[i] = new NewBisPASvcRef.U_ACCOUNT_ID();
                            uAccountIDArr[i].UACCOUNT_ID = uSmartIDObj.UACCOUNT_ID;
                            uAccountIDArr[i].ACC_BANK = txtBankCode.Text.Trim();
                            uAccountIDArr[i].ACC_BRANCH = txtBranchCode.Text.Trim();
                            uAccountIDArr[i].ACC_NO = txtAccNo.Text.Trim();
                            uAccountIDArr[i].ACC_DEPOSIT_TYPE = cmbDepositType.SelectedValue == null ? "" : cmbDepositType.SelectedValue.ToString();
                            uAccountIDArr[i].ACC_NAME = txtAccName.Text.Trim();
                            uAccountIDArr[i].ACC_NAME_TYPE = "P";
                            uAccountIDArr[i].APPROVE_DT = uSmartIDObj.RECEIVE_DT;
                            uAccountIDArr[i].APPROVE_ID = uSmartIDObj.UPD_ID;
                            uAccountIDArr[i].FSYSTEM_DT = uSmartIDObj.FSYSTEM_DT;
                            uAccountIDArr[i].UPD_DT = uSmartIDObj.UPD_DT;
                            uAccountIDArr[i].UPD_ID = uSmartIDObj.UPD_ID;

                            uAccountIDArr[i].SMART_ID = new NewBisPASvcRef.U_SMART_ID();
                            uAccountIDArr[i].SMART_ID = uSmartIDObj;




                            uApplicationObj.SMART_ID_Collection = new NewBisPASvcRef.U_SMART_ID_Collection();
                            uApplicationObj.SMART_ID_Collection.Add(uSmartIDObj);
                        }
                        #endregion
                        //END เก็บค่า U_SMART_ID ===================================================================================
                    }
                }

                #region "เก็บค่า U_ADDRESS_ID"
                if (uAddressIDArr != null && uAddressIDArr.Count() > 0)
                {

                    objUOaddress(ref PAR_U_OADDRESS, uAddressIDArr.ToArray()[0]);

                    PAR_U_ADDRESS_ID_COLL = new NewBisPASvcRef.U_ADDRESS_ID[uAddressIDArr.Count()];
                    for (int i = 0; i < uAddressIDArr.Count(); i++)
                    {
                        PAR_U_ADDRESS_ID_COLL[i] = new NewBisPASvcRef.U_ADDRESS_ID();
                        uAddressIDArr[i].OADDRESS = PAR_U_OADDRESS;
                        PAR_U_ADDRESS_ID_COLL[i] = uAddressIDArr[i];
                    }
                }
                #endregion

                #region "เก็บค่า U_ACCOUNT_ID"
                if (cmbChannelType.SelectedValue.ToString() == "KB")
                {
                    if (uAccountIDArr != null && uAccountIDArr.Count() > 0)
                    {
                        PAR_U_ACCOUNT_ID_COLL = new NewBisPASvcRef.U_ACCOUNT_ID[uAccountIDArr.Count()];
                        for (int i = 0; i < uAccountIDArr.Count(); i++)
                        {
                            PAR_U_ACCOUNT_ID_COLL[i] = new NewBisPASvcRef.U_ACCOUNT_ID();
                            PAR_U_ACCOUNT_ID_COLL[i] = uAccountIDArr[i];
                        }
                    }
                    else
                    {
                        tabMain.SelectTab("tabCustomerData");
                        throw new Exception("กรุณาระบุข้อมูลบัญชีของผู้เอาประกัน");
                    }
                }
                #endregion

            }

            //END บันทึกข้อมูลลง Object U_ADDRESS_ID ================================================================================

            if (cmbChannelType.SelectedValue.ToString() == "PD")
            {
                ValidateCreditCardData(PAR_PA_APPLICATION_ID.APPLICATION_Collection);
            }

            //บันทึกข้อมูลส่วนการพิจารณา ===============================================================================================
            #region "ADD _UNDERWRITE"


            if (cmbSummryStatus.SelectedValue.ToString() == "")
            {

                cmbSummryStatus.SelectedValue = "WT";
                GetControlsUnderStatus("WT", "WT");
                //tabMain.SelectTab("tabUnderWriteData");
                // tabUnderWriteMain.SelectTab("tabUnderWrite");
                //throw new Exception("กรุณาระบุสถานะกรมธรรม์ก่อนบันทึกข้อมูล");
            }

            if (PAR_PA_APPLICATION_ID.APP_ID != null && PAR_PA_APPLICATION_ID.APP_ISIS != null && PAR_PA_APPLICATION_ID.APP_ISIS.APP_ID != null)
            {
                if (PAR_PA_APPLICATION_ID.APP_ISIS.NEWBIS == 'N' && cmbSummryStatus.SelectedValue.ToString() == "")
                {

                    cmbSummryStatus.SelectedValue = "WT";
                    GetControlsUnderStatus("WT", "WT");
                    //tabMain.SelectTab("tabUnderWriteData");
                    //tabUnderWriteMain.SelectTab("tabUnderWrite");
                    //throw new Exception("กรุณาระบุสถานะกรมธรรม์ก่อนบันทึกข้อมูล");
                }
            }

            if (cmbSummryStatus.SelectedValue.ToString() != "")
            {
                PAR_W_UNDERWRITE_ID_TMP_COLL = new NewBisPASvcRef.W_UNDERWRITE_ID_Collection();
                if (PAR_PA_APPLICATION_ID.APPLICATION_Collection != null && PAR_PA_APPLICATION_ID.APPLICATION_Collection.Count() > 0)
                {
                    for (int i = 0; i < PAR_PA_APPLICATION_ID.APPLICATION_Collection.Count(); i++)
                    {
                        NewBisPASvcRef.U_APPLICATION uApplicationObj = new NewBisPASvcRef.U_APPLICATION();
                        uApplicationObj = PAR_PA_APPLICATION_ID.APPLICATION_Collection[i];
                        long? underwriteID = null;
                        if (uApplicationObj.UNDERWRITE_APPLICATION != null && uApplicationObj.UNDERWRITE_APPLICATION.UNDERWRITE_ID != null)
                        {
                            underwriteID = uApplicationObj.UNDERWRITE_APPLICATION.UNDERWRITE_ID;
                        }

                        NewBisPASvcRef.W_UNDERWRITE_ID wUnderwriteIDObj = new NewBisPASvcRef.W_UNDERWRITE_ID();

                        var GetData = from getData in PAR_W_UNDERWRITE_ID_COLL
                                      where getData.UNDERWRITE_ID == underwriteID
                                      select getData;

                        if (GetData != null && GetData.Count() > 0)
                        {
                            wUnderwriteIDObj = GetData.ToArray()[0];
                        }

                        //Add W_UNDERWRITE_ID ======================================================
                        objWUnderwriteID(ref wUnderwriteIDObj);
                        //END Add W_UNDERWRITE_ID ==================================================

                        //ADD W_UNDERWRITE_APPLICATION =============================================
                        NewBisPASvcRef.W_UNDERWRITE_APPLICATION wUnderwriteApplication = new NewBisPASvcRef.W_UNDERWRITE_APPLICATION();
                        if (wUnderwriteIDObj.UNDERWRITE_APPLICATION_Collection != null && wUnderwriteIDObj.UNDERWRITE_APPLICATION_Collection.Count() > 0)
                        {
                            wUnderwriteApplication = wUnderwriteIDObj.UNDERWRITE_APPLICATION_Collection[0];
                        }
                        else
                        {
                            wUnderwriteApplication.UAPP_ID = uApplicationObj.UAPP_ID;
                        }
                        wUnderwriteIDObj.UNDERWRITE_APPLICATION_Collection = new NewBisPASvcRef.W_UNDERWRITE_APPLICATION_Collection();
                        wUnderwriteIDObj.UNDERWRITE_APPLICATION_Collection.Add(wUnderwriteApplication);
                        //END ADD W_UNDERWRITE_APPLICATION =========================================

                        //Add W_SUBUNDERWRITE_ID ===================================================
                        NewBisPASvcRef.W_SUBUNDERWRITE_ID wSubUnderwriteIDobj = new NewBisPASvcRef.W_SUBUNDERWRITE_ID();
                        if (wUnderwriteIDObj.SUBUNDERWRITE_ID_Collection != null && wUnderwriteIDObj.SUBUNDERWRITE_ID_Collection.Count() > 0)
                        {
                            wSubUnderwriteIDobj = wUnderwriteIDObj.SUBUNDERWRITE_ID_Collection[0];
                        }

                        objWSuBUnderwriteID(ref wSubUnderwriteIDobj);

                        //Add W_SUMMARY_ID =========================================================
                        NewBisPASvcRef.W_SUMMARY wSummaryObj = new NewBisPASvcRef.W_SUMMARY();
                        if (wSubUnderwriteIDobj.SUMMARY_Collection != null && wSubUnderwriteIDobj.SUMMARY_Collection.Count() > 0)
                        {
                            wSummaryObj = wSubUnderwriteIDobj.SUMMARY_Collection[0];


                        }

                        if (cmbSummryStatus.SelectedValue.ToString() == "AP" && cmbSummrySubStatus.SelectedValue.ToString() == "AP")
                        {
                            //U_AP_ID =====================================================================
                            var uAPObj = CreateUAPID(wSummaryObj);

                            wSummaryObj.AP_ID_Collection = new NewBisPASvcRef.U_AP_ID_Collection();
                            wSummaryObj.AP_ID_Collection.Add(uAPObj);
                            //END U_AP_ID =================================================================
                        }

                        objWSummary(ref wSummaryObj);

                        //เก็บค่าลง U_APPROVED ของสถานะที่ END PROCESS ===================================


                        String[] statusEnd = { "IF", "CC", "DC", "EX", "NT", "PP" };

                        if (statusEnd.Contains(wSummaryObj.STATUS))
                        {
                            NewBisPASvcRef.U_APPROVED uApprovedObj = new NewBisPASvcRef.U_APPROVED();
                            uApprovedObj.UAPP_ID = uApplicationObj.UAPP_ID;
                            objUApporved(ref uApprovedObj, wSummaryObj.STATUS);

                            NewBisPASvcRef.U_POLICYHOLDING uPolicyHoldingObj = new NewBisPASvcRef.U_POLICYHOLDING();
                            uPolicyHoldingObj.UAPP_ID = uApprovedObj.UAPP_ID;
                            uPolicyHoldingObj.POLICY_HOLDING = uApplicationObj.LIFE_ID.POLICY_HOLDING;
                            uApprovedObj.POLICYHOLDING_Collection = new NewBisPASvcRef.U_POLICYHOLDING_Collection();
                            uApprovedObj.POLICYHOLDING_Collection.Add(uPolicyHoldingObj);
                            if (chbDmsChange.Checked == true)
                            {
                                uApprovedObj.DMS_CHANGE = 'Y';
                                uApprovedObj.U_DMS_CHANGE = new NewBisPASvcRef.U_DMS_CHANGE();
                                uApprovedObj.U_DMS_CHANGE.UAPP_ID = uApprovedObj.UAPP_ID;
                                uApprovedObj.U_DMS_CHANGE.FSYSTEM_DT = DateTime.Now;
                            }
                            else
                            {
                                uApprovedObj.DMS_CHANGE = 'N';
                            }
                            PAR_PA_APPLICATION_ID.APPLICATION_Collection[i].APPROVED_Collection = new NewBisPASvcRef.U_APPROVED_Collection();
                            PAR_PA_APPLICATION_ID.APPLICATION_Collection[i].APPROVED_Collection.Add(uApprovedObj);

                            if (cmbChannelType.SelectedValue.ToString() != "PD" && cmbChannelType.SelectedValue.ToString() != "PF")
                            {
                                PAR_PA_APPLICATION_ID.APPLICATION_Collection[i].LIFE_ID.AGENT.STR_DT = Utility.StringToDateTime(txtStrDt.Text.Trim(), "BU");
                                PAR_PA_APPLICATION_ID.APPLICATION_Collection[i].LIFE_ID.AGENT.MARKETING_DT = Utility.StringToDateTime(txtMktDate.Text.Trim(), "BU");
                            }


                            PAR_PA_APPLICATION_ID.APPLICATION_Collection[i].END_PROCESS = 'Y';
                            wUnderwriteIDObj.PROCESS_FLG = 'Y';
                        }

                        //เก็บค่าลง U_APPROVED ของสถานะที่ END PROCESS ===================================

                        //Add W_SUMMARY_UNDERWRITE =================================================
                        NewBisPASvcRef.W_SUMMARY_UNDERWRITER wSummaryUnderwriterObj = new NewBisPASvcRef.W_SUMMARY_UNDERWRITER();
                        wSummaryUnderwriterObj.SUMMARY_ID = wSummaryObj.SUMMARY_ID;
                        objWSummaryUnderwriter(ref wSummaryUnderwriterObj);
                        wSummaryObj.SUMMARY_UNDERWRITER_Collection = new NewBisPASvcRef.W_SUMMARY_UNDERWRITER_Collection();
                        wSummaryObj.SUMMARY_UNDERWRITER_Collection.Add(wSummaryUnderwriterObj);
                        //END Add W_SUMMARY_UNDERWRITE =============================================

                        //Add W_SUMMARY_DETAIL =====================================================
                        wSummaryObj.SUMMARY_DETAIL_Collection = new NewBisPASvcRef.W_SUMMARY_DETAIL_Collection();
                        if (PAR_W_SUMMARY_DETAIL_COLL != null && PAR_W_SUMMARY_DETAIL_COLL.Count() > 0)
                        {
                            var GetSummaryDetail = from getData in PAR_W_SUMMARY_DETAIL_COLL
                                                   select getData;
                            if (GetSummaryDetail != null && GetSummaryDetail.Count() > 0)
                            {
                                NewBisPASvcRef.W_SUMMARY_DETAIL_Collection wSummaryDetailColl = new NewBisPASvcRef.W_SUMMARY_DETAIL_Collection();
                                wSummaryDetailColl.AddRange(GetSummaryDetail.ToArray());
                                //foreach (NewBisPASvcRef.W_SUMMARY_DETAIL  wSummaryDetailObj in wSummaryDetailColl)
                                //{
                                //    wSummaryDetailObj.SUMMARY_ID = wSummaryObj.SUMMARY_ID;
                                //}                                
                                wSummaryObj.SUMMARY_DETAIL_Collection.AddRange(wSummaryDetailColl.ToArray());
                            }
                        }
                        //END Add W_SUMMARY_DETAIL =================================================

                        wSubUnderwriteIDobj.SUMMARY_Collection = new NewBisPASvcRef.W_SUMMARY_Collection();
                        wSubUnderwriteIDobj.SUMMARY_Collection.Add(wSummaryObj);
                        //END Add W_SUMMARY_ID =====================================================


                        //// ทำเงินเกิน 
                        //String[] overPremstatusEnd = { "CC", "DC", "EX", "NT", "PP" };
                        //if (overPremstatusEnd.Contains(wSummaryObj.STATUS))
                        //{

                        //    var client = new NewBISSvcRef.NewBISSvcClient();
                        //    NewBISSvcRef.U_LETTER_ID[] letterInfo;
                        //    var letterPr = client.GetULetterByUappId(out letterInfo, null, new long[] { uApplicationObj.UAPP_ID == null ? 0 : uApplicationObj.UAPP_ID.Value });

                        //    long? uletterId = null;
                        //    if (letterPr.Successed)
                        //    {
                        //        if (letterInfo != null && letterInfo.Any())
                        //        {
                        //            uletterId = letterInfo.FirstOrDefault().ULETTER_ID.Value;
                        //        }
                        //    }

                        //    NewBisPASvcRef.U_LETTER_ID uLetterIDObj = new NewBisPASvcRef.U_LETTER_ID();
                        //    uLetterIDObj.ULETTER_ID = uletterId;
                        //    uLetterIDObj.UAPP_ID = uApplicationObj.UAPP_ID;
                        //    uLetterIDObj.STATUS = wSummaryObj.STATUS;
                        //    uLetterIDObj.SUBSTATUS = wSummaryObj.SUBSTATUS;
                        //    //  uLetterIDObj.STATUS_DT = wSummaryObj.FSYSTEM_DT;
                        //    uLetterIDObj.STATUS_CAUSE = wSummaryObj.STATUS_CAUSE;
                        //    uLetterIDObj.LETTER_TYPE = 'I';

                        //    //==================================================================================================
                        //    NewBISSvcRef.ProcessResult prLetterCode = new NewBISSvcRef.ProcessResult();
                        //    NewBISSvcRef.AUTB_LETTER_ID_Collection autbLetterIDColl = new NewBISSvcRef.AUTB_LETTER_ID_Collection();
                        //    ;
                        //    autbLetterIDColl = client.GetAutbLetterIDBySubStatus(out prLetterCode, wSummaryObj.STATUS);
                        //    if (prLetterCode.Successed == false)
                        //    {
                        //        throw new Exception(prLetterCode.ErrorMessage);
                        //    }
                        //    if (autbLetterIDColl != null && autbLetterIDColl.Count() > 0)
                        //    {
                        //        uLetterIDObj.LETTER_CODE = autbLetterIDColl[0].LETTER_CODE;
                        //    }
                        //    else
                        //    {
                        //        uLetterIDObj.LETTER_CODE = null;
                        //    }
                        //    //==================================================================================================
                        //    uLetterIDObj.SIGNATURE_ID = null;
                        //    uLetterIDObj.TMN = 'N';
                        //    // uLetterIDObj.LETTER_DT = wSummaryObj.FSYSTEM_DT;
                        //    //uLetterIDObj.FSYSTEM_DT = wSummaryObj.FSYSTEM_DT;
                        //    uLetterIDObj.UPD_ID = UserID;
                        //    uLetterIDObj.PRINT_FLG = 'N';
                        //    //W_SUMMARY_LETTER ===================================================================================

                        //    NewBisPASvcRef.W_SUMMARY_LETTER wSumLetter = new NewBisPASvcRef.W_SUMMARY_LETTER();
                        //    wSumLetter.SUMMARY_ID = wSummaryObj.SUMMARY_ID;
                        //    wSumLetter.ULETTER_ID = uLetterIDObj.ULETTER_ID;
                        //    uLetterIDObj.SUMMARY_LETTER_Collection = new NewBisPASvcRef.W_SUMMARY_LETTER_Collection();
                        //    uLetterIDObj.SUMMARY_LETTER_Collection.Add(wSumLetter);

                        //    //END W_SUMMARY_LETTER ===============================================================================

                        //    //U_LETTER_SENDTO ====================================================================================
                        //    if (wSummaryObj.STATUS == "DC" && wSummaryObj.SUBSTATUS == "DC" && wSummaryObj.STATUS_CAUSE == "DC006")
                        //    {
                        //        uLetterIDObj.LETTER_SENDTO_Collection = new NewBisPASvcRef.U_LETTER_SENDTO_Collection();
                        //        NewBisPASvcRef.U_LETTER_SENDTO objDC = new NewBisPASvcRef.U_LETTER_SENDTO();
                        //        objDC.ULETTER_ID = uLetterIDObj.ULETTER_ID;
                        //        objDC.SEND_LETTER_TO = "C";
                        //        objDC.SEND_BY = "CS";
                        //        uLetterIDObj.LETTER_SENDTO_Collection.Add(objDC);
                        //    }
                        //    //else
                        //    //{
                        //    //    if (PAR_U_LETTER_SENDTO_COLL != null && PAR_U_LETTER_SENDTO_COLL.Count() > 0)
                        //    //    {
                        //    //        uLetterIDObj.LETTER_SENDTO_Collection = new NewBISSvcRef.U_LETTER_SENDTO_Collection();
                        //    //        foreach (NewBISSvcRef.U_LETTER_SENDTO obj in PAR_U_LETTER_SENDTO_COLL)
                        //    //        {
                        //    //            obj.ULETTER_ID = uLetterIDObj.ULETTER_ID;
                        //    //            uLetterIDObj.LETTER_SENDTO_Collection.Add(obj);
                        //    //        }
                        //    //    }
                        //    //}


                        //    //END U_LETTER_SENDTO =================================================================================

                        //    //U_RETURN_PRM =======================================================================================

                        //    NewBISSvcRef.ProcessResult prCalPrem = new NewBISSvcRef.ProcessResult();
                        //    decimal suspensePremium = 0;
                        //    String ChannelType = cmbChannelType.SelectedValue.ToString();
                        //    Decimal? premium = txtTotalPremium.Text == "" ? (Decimal?)null : Convert.ToDecimal(txtTotalPremium.Text.Trim().Replace(",", ""));
                        //    //suspensePremium = client.GetCalPremiumByAppNo(out prCalPrem, txtAppNo.Text.Trim(), ChannelType, premium);
                        //    //suspensePremium = client.GetReturnPremium(out prCalPrem, txtAppNo.Text.Trim(), ChannelType);
                        //    //suspensePremium = client.GetCalPremiumByAppNoCancel(out prCalPrem, txtAppNo.Text.Trim(), ChannelType, premium, wSummaryObj.STATUS);


                        //    suspensePremium = PAR_PREMIUM_SUSPENSE == null ? 0 : PAR_PREMIUM_SUSPENSE.Value;
                        //    //if (prCalPrem.Successed == false)
                        //    //{
                        //    //    throw new Exception(prCalPrem.ErrorMessage);
                        //    //}

                        //    //if (suspensePremium > 0 && wSummaryObj.STATUS != "AP")
                        //    if (wSummaryObj.STATUS != "AP")
                        //    {

                        //        NewBisPASvcRef.U_RETURN_PRM uReturnPrmObj = new NewBisPASvcRef.U_RETURN_PRM();
                        //        uReturnPrmObj.ULETTER_ID = uLetterIDObj.ULETTER_ID;
                        //        uReturnPrmObj.RETURN_PREMIUM = suspensePremium;
                        //        var wSumUnderWrite =
                        //            (from sumUnderWrite in wSummaryObj.SUMMARY_UNDERWRITER_Collection
                        //             orderby sumUnderWrite.FSYSTEM_DT descending
                        //             select sumUnderWrite).First();

                        //        uReturnPrmObj.APPROVE_ID = wSumUnderWrite.UND_ID;
                        //        uReturnPrmObj.APPROVE_DT = wSumUnderWrite.FSYSTEM_DT;
                        //        uReturnPrmObj.UPD_ID = UserID;

                        //        //U_RETURN_PRMNEWAPP =================================================================================
                        //        //if (PAR_U_RETURN_PRMNEWAPP != null && PAR_U_RETURN_PRMNEWAPP.NEW_APP_NO != null)
                        //        //{
                        //        //    PAR_U_RETURN_PRMNEWAPP.ULETTER_ID = uReturnPrmObj.ULETTER_ID;
                        //        //    uReturnPrmObj.RETURN_PRMNEWAPP = new NewBISSvcRef.U_RETURN_PRMNEWAPP();
                        //        //    uReturnPrmObj.RETURN_PRMNEWAPP = PAR_U_RETURN_PRMNEWAPP;
                        //        //}
                        //        //END U_RETURN_PRMNEWAPP =============================================================================

                        //        uLetterIDObj.RETURN_PRM = uReturnPrmObj;
                        //    }

                        //    ULetterIDObj = uLetterIDObj;
                        //    //END U_RETURN_PRM ===================================================================================

                        //    ////U_LACKPREMIUM ======================================================================================
                        //    //Decimal overPremium = 0;
                        //    //Decimal totalPremium = 0;
                        //    //totalPremium = txtTotalPremium.Text == "" ? 0 : Convert.ToDecimal(txtTotalPremium.Text.Trim().Replace(",", ""));
                        //    //overPremium = suspensePremium - totalPremium;
                        //    //if (overPremium < 0 && wSummaryObj.STATUS == "AP" && PK_LACKPRM_ID == null)
                        //    //{
                        //    //    NewBISSvcRef.U_LACKPREMIUM uLackPremium = new NewBISSvcRef.U_LACKPREMIUM();
                        //    //    uLackPremium.ULETTER_ID = uLetterIDObj.ULETTER_ID;
                        //    //    uLackPremium.LACKPRM_ID = PK_LACKPRM_ID;
                        //    //    uLackPremium.PREMIUM = totalPremium;
                        //    //    uLackPremium.SUSPENSE_PREMIUM = suspensePremium;
                        //    //    uLetterIDObj.LACKPREMIUM_Collection = new NewBISSvcRef.U_LACKPREMIUM_Collection();
                        //    //    uLetterIDObj.LACKPREMIUM_Collection.Add(uLackPremium);
                        //    //}

                        //    ////END U_LACKPREMIUM ==================================================================================

                        //    //บันทึกข้อมูลจดหมายที่มีสถานะปฏิเสธ ===========================================================================
                        //    // prLetterCode = client.EditU_LETTER_ID(ref uLetterIDObj);
                        //    //if (prLetterCode.Successed == false)
                        //    //{
                        //    //    //btnHeadSave.Enabled = false;
                        //    //    //  btnUnderWrite.Enabled = false;
                        //    //    throw new Exception(prLetterCode.ErrorMessage);
                        //    //}
                        //    //จบบันทึกข้อมูลจดหมายที่มีสถานะปฏิเสธ =========================================================================
                        //}
                        //else if (wSummaryObj.STATUS == "IF")
                        //{

                        //    Decimal overPremium = 0;
                        //    Decimal totalPremium = 0;
                        //    totalPremium = txtTotalPremium.Text == "" ? 0 : Convert.ToDecimal(txtTotalPremium.Text.Trim().Replace(",", ""));
                        //    NewBisPASvcRef.ProcessResult prCalPrem = new NewBisPASvcRef.ProcessResult();
                        //    decimal suspensePremium = 0;
                        //    String ChannelType = cmbChannelType.SelectedValue.ToString();
                        //    Decimal? premium = txtTotalPremium.Text == "" ? (Decimal?)null : Convert.ToDecimal(txtTotalPremium.Text.Trim().Replace(",", ""));
                        //    String statusCode = cmbSummryStatus.SelectedValue == null ? "WT" : cmbSummryStatus.SelectedValue.ToString();
                        //    //suspensePremium = client.GetCalPremiumByAppNo(out prCalPrem, txtAppNo.Text.Trim(), ChannelType, premium, statusCode);
                        //    suspensePremium = PAR_PREMIUM_SUSPENSE == null ? 0 : PAR_PREMIUM_SUSPENSE.Value;
                        //    //if (prCalPrem.Successed == false)
                        //    //{
                        //    //    throw new Exception(prCalPrem.ErrorMessage);
                        //    //}

                        //    overPremium = suspensePremium - totalPremium;

                        //    if (overPremium > 0)
                        //    {
                        //       var uLetterIDObj = new NewBisPASvcRef.U_LETTER_ID();
                        //        uLetterIDObj.ULETTER_ID = null;
                        //        uLetterIDObj.UAPP_ID = uApplicationObj.UAPP_ID;
                        //        uLetterIDObj.STATUS = "IF";
                        //        uLetterIDObj.SUBSTATUS = "IF";
                        //        uLetterIDObj.STATUS_CAUSE = null;
                        //        uLetterIDObj.REFERENCE = null;
                        //        uLetterIDObj.LETTER_TYPE = 'I';
                        //        uLetterIDObj.LETTER_CODE = null;
                        //        uLetterIDObj.SIGNATURE_ID = null;
                        //        uLetterIDObj.TMN = 'N';
                        //        uLetterIDObj.FSYSTEM_DT = DateTime.Now;
                        //        uLetterIDObj.UPD_ID = wSummaryObj.UPD_ID;
                        //        uLetterIDObj.PRINT_FLG = 'N';
                        //        uLetterIDObj.LETTER_DT = DateTime.Now;

                        //        uLetterIDObj.RETURN_PRM = new NewBisPASvcRef.U_RETURN_PRM();
                        //        uLetterIDObj.RETURN_PRM.ULETTER_ID = uLetterIDObj.ULETTER_ID;
                        //        uLetterIDObj.RETURN_PRM.PAY_OPTION = null;
                        //        uLetterIDObj.RETURN_PRM.RETURN_PREMIUM = overPremium;
                        //        uLetterIDObj.RETURN_PRM.FEE = null;
                        //        uLetterIDObj.RETURN_PRM.APPROVE_ID = wSummaryObj.UPD_ID;
                        //        uLetterIDObj.RETURN_PRM.APPROVE_DT = uLetterIDObj.FSYSTEM_DT;
                        //        uLetterIDObj.RETURN_PRM.UPD_ID = UserID;



                        //        NewBisPASvcRef.W_SUMMARY_LETTER wSumLetter = new NewBisPASvcRef.W_SUMMARY_LETTER();
                        //        wSumLetter.SUMMARY_ID = wSummaryObj.SUMMARY_ID;
                        //        wSumLetter.ULETTER_ID = uLetterIDObj.ULETTER_ID;
                        //        uLetterIDObj.SUMMARY_LETTER_Collection = new NewBisPASvcRef.W_SUMMARY_LETTER_Collection();
                        //        uLetterIDObj.SUMMARY_LETTER_Collection.Add(wSumLetter);

                        //        ULetterIDObj = uLetterIDObj;

                        //    }
                        //    else if (overPremium < 0)
                        //    {
                        //       var uLetterIDObj = new NewBisPASvcRef.U_LETTER_ID();
                        //        uLetterIDObj.ULETTER_ID = null;
                        //        uLetterIDObj.UAPP_ID = uApplicationObj.UAPP_ID;
                        //        uLetterIDObj.STATUS = "IF";
                        //        uLetterIDObj.SUBSTATUS = "IF";
                        //        uLetterIDObj.STATUS_CAUSE = null;
                        //        uLetterIDObj.REFERENCE = null;
                        //        uLetterIDObj.LETTER_TYPE = 'I';
                        //        uLetterIDObj.LETTER_CODE = null;
                        //        uLetterIDObj.SIGNATURE_ID = null;
                        //        uLetterIDObj.TMN = 'N';
                        //        uLetterIDObj.FSYSTEM_DT = DateTime.Now;
                        //        uLetterIDObj.UPD_ID = wSummaryObj.UPD_ID;
                        //        uLetterIDObj.PRINT_FLG = 'N';
                        //        uLetterIDObj.LETTER_DT = DateTime.Now;


                        //        var uLackPremium = new NewBisPASvcRef.U_LACKPREMIUM();
                        //        uLackPremium.ULETTER_ID = uLetterIDObj.ULETTER_ID;
                        //        uLackPremium.LACKPRM_ID = null; ;
                        //        uLackPremium.PREMIUM = totalPremium;
                        //        uLackPremium.SUSPENSE_PREMIUM = suspensePremium;
                        //        uLetterIDObj.LACKPREMIUM_Collection = new NewBisPASvcRef.U_LACKPREMIUM_Collection();
                        //        uLetterIDObj.LACKPREMIUM_Collection.Add(uLackPremium);


                        //       var wSumLetter = new NewBisPASvcRef.W_SUMMARY_LETTER();
                        //        wSumLetter.SUMMARY_ID = wSummaryObj.SUMMARY_ID;
                        //        wSumLetter.ULETTER_ID = uLetterIDObj.ULETTER_ID;
                        //        uLetterIDObj.SUMMARY_LETTER_Collection = new NewBisPASvcRef.W_SUMMARY_LETTER_Collection();
                        //        uLetterIDObj.SUMMARY_LETTER_Collection.Add(wSumLetter);

                        //        ULetterIDObj = uLetterIDObj;
                        //    }

                        //}

                        wUnderwriteIDObj.SUBUNDERWRITE_ID_Collection = new NewBisPASvcRef.W_SUBUNDERWRITE_ID_Collection();
                        wUnderwriteIDObj.SUBUNDERWRITE_ID_Collection.Add(wSubUnderwriteIDobj);
                        PAR_W_UNDERWRITE_ID_TMP_COLL.Add(wUnderwriteIDObj);
                        //END Add W_SUBUNDERWRITE_ID ================================================


                    }
                }

                if (PAR_W_UNDERWRITE_ID_TMP_COLL != null && PAR_W_UNDERWRITE_ID_TMP_COLL.Count() > 0)
                {
                    PAR_W_UNDERWRITE_ID_COLL = new NewBisPASvcRef.W_UNDERWRITE_ID_Collection();
                    PAR_W_UNDERWRITE_ID_COLL.AddRange(PAR_W_UNDERWRITE_ID_TMP_COLL.ToArray());
                }
            }



            //บันทึกรายละเอียด Memo ======================================================

            if (PAR_U_MEMO_ID_COLL_TMP != null && PAR_U_MEMO_ID_COLL_TMP.Count() > 0)
            {
                if (PAR_W_UNDERWRITE_ID_COLL != null && PAR_W_UNDERWRITE_ID_COLL.Count() > 0)
                {
                    foreach (NewBisPASvcRef.W_UNDERWRITE_ID wUnderwriteIDObj in PAR_W_UNDERWRITE_ID_COLL)
                    {
                        if (wUnderwriteIDObj.SUBUNDERWRITE_ID_Collection != null && wUnderwriteIDObj.SUBUNDERWRITE_ID_Collection.Count() > 0)
                        {
                            foreach (NewBisPASvcRef.W_SUBUNDERWRITE_ID wSubUnderwriteIDObj in wUnderwriteIDObj.SUBUNDERWRITE_ID_Collection)
                            {
                                if (wSubUnderwriteIDObj.SUMMARY_Collection != null && wSubUnderwriteIDObj.SUMMARY_Collection.Count() > 0)
                                {

                                    foreach (NewBisPASvcRef.W_SUMMARY wSummaryObj in wSubUnderwriteIDObj.SUMMARY_Collection)
                                    {
                                        NewBisPASvcRef.U_MEMO_ID_Collection uMemoColl = new NewBisPASvcRef.U_MEMO_ID_Collection();

                                        foreach (NewBisPASvcRef.U_MEMO_ID uMemoIDObj in PAR_U_MEMO_ID_COLL_TMP)
                                        {

                                            NewBisPASvcRef.U_MEMO_ID uMemoIDTmp = new NewBisPASvcRef.U_MEMO_ID();

                                            uMemoIDTmp = GenericConverter<NewBisPASvcRef.U_MEMO_ID, NewBisPASvcRef.U_MEMO_ID>.Convert(uMemoIDObj);

                                            if (wSummaryObj.MEMO_ID_Collection != null && wSummaryObj.MEMO_ID_Collection.Count() > 0)
                                            {
                                                var GetData = from getData in wSummaryObj.MEMO_ID_Collection
                                                              where getData.MEMO_TRN_DT == uMemoIDObj.MEMO_TRN_DT
                                                              select getData;

                                                if (GetData != null && GetData.Count() > 0)
                                                {
                                                    uMemoIDTmp.UMEMO_ID = GetData.ToArray()[0].UMEMO_ID;
                                                    uMemoIDTmp.SUMMARY_ID = GetData.ToArray()[0].SUMMARY_ID;
                                                }
                                                else
                                                {
                                                    uMemoIDTmp.SUMMARY_ID = wSummaryObj.MEMO_ID_Collection[0].SUMMARY_ID;
                                                    uMemoIDTmp.UMEMO_ID = null;
                                                }
                                            }
                                            else
                                            {
                                                uMemoIDTmp.SUMMARY_ID = null;
                                                uMemoIDTmp.UMEMO_ID = null;
                                            }

                                            ValidateUMemoIDData(uMemoIDObj, wSummaryObj.STATUS);
                                            uMemoColl.Add(uMemoIDTmp);
                                        }

                                        wSummaryObj.MEMO_ID_Collection = new NewBisPASvcRef.U_MEMO_ID_Collection();
                                        wSummaryObj.MEMO_ID_Collection.AddRange(uMemoColl.ToArray());
                                    }
                                }
                            }
                        }
                    }
                }
            }
            //END บันทึกรายละเอียด Memo ==================================================
            #endregion

            ValidateWUnderwriteIDData(PAR_W_UNDERWRITE_ID_COLL);

            //throw new Exception("55555");
            //END บันทึกข้อมูลส่วนการพิจารณา ===========================================================================================

        }
        private void ObjUApplication()
        {

            NewBisPASvcRef.U_APPLICATION_Collection uApplicationColl = new NewBisPASvcRef.U_APPLICATION_Collection();
            string channelType = cmbChannelType.SelectedValue == null ? "" : cmbChannelType.SelectedValue.ToString();
            //บันทึกครั้งแรกยังไม่มีค่าใน U_APPLICATION ===============================================================================
            #region "PLAN FREE"
            if (PAR_PLAN_FREE != null && (!(String.IsNullOrEmpty(PAR_PLAN_FREE.PL_CODE))))
            {
                //Get ค่า U_APPLICATION เดิมว่ามีหรือป่าว ===================================================================================
                NewBisPASvcRef.U_APPLICATION uApplicationFree = new NewBisPASvcRef.U_APPLICATION();
                long? UAPP_ID_FREE = null;
                String planIDFree = PAR_PLAN_FREE.PL_BLOCK + PAR_PLAN_FREE.PL_CODE + PAR_PLAN_FREE.PL_CODE2;

                if (PAR_PA_APPLICATION_ID.APPLICATION_Collection != null && PAR_PA_APPLICATION_ID.APPLICATION_Collection.Count() > 0)
                {
                    foreach (NewBisPASvcRef.U_APPLICATION item in PAR_PA_APPLICATION_ID.APPLICATION_Collection)
                    {
                        if (item.LIFE_ID != null)
                        {
                            if (item.LIFE_ID.FREE_FLG == "Y" || IsChannelTypeFreePolicy(channelType))
                            {
                                uApplicationFree = item;
                                UAPP_ID_FREE = item.LIFE_ID.UAPP_ID;
                                break;
                            }
                        }
                    }
                }
                //END Get ค่า U_APPLICATION เดิมว่ามีหรือป่าว ================================================================================
                NewBisPASvcRef.U_APPLICATION uApplication = new NewBisPASvcRef.U_APPLICATION();
                uApplication.APP_ID = PAR_PA_APPLICATION_ID.APP_ID;
                uApplication.OLD_POL = NAME_ID == null ? 'N' : 'Y';
                uApplication.APP_DT = txtAppDt.Text.Trim() == "" ? (DateTime?)null : Utility.StringToDateTime(txtAppDt.Text.Trim(), "BU");
                uApplication.APPSYS_DT = txtAppSysDt.Text.Trim() == "" ? (DateTime?)null : Utility.StringToDateTime(txtAppSysDt.Text.Trim(), "BU");
                uApplication.APP_OFC = txtAppOfc.Text.Trim();
                uApplication.APP_OFCRCV_DT = txtAppOfcRcvDt.Text.Trim() == "" ? (DateTime?)null : Utility.StringToDateTime(txtAppOfcRcvDt.Text.Trim(), "BU");
                uApplication.APP_HORCV_DT = txtAppHoRcvDt.Text.Trim() == "" ? (DateTime?)null : Utility.StringToDateTime(txtAppHoRcvDt.Text.Trim(), "BU");
                uApplication.APP_SIGN_DT = txtAppSignDt.Text.Trim() == "" ? (DateTime?)null : Utility.StringToDateTime(txtAppSignDt.Text.Trim(), "BU");
                uApplication.END_PROCESS = 'N';
                uApplication.ENTRY_OFC = uApplication.APP_OFC;
                uApplication.UNDERWRITING_FLG = cmbUnderWriteFlag.SelectedValue == null || cmbUnderWriteFlag.SelectedValue.ToString() == "" ? (char?)null : Convert.ToChar(cmbUnderWriteFlag.SelectedValue.ToString());
                uApplication.DOCUMENT_FLG = cmbDocumentFlag.SelectedValue == null || cmbDocumentFlag.SelectedValue.ToString() == "" ? (char?)null : Convert.ToChar(cmbDocumentFlag.SelectedValue.ToString());
                uApplication.UAPP_ID = UAPP_ID_FREE;
                //เก็บต่า Object ของแบบประกัน ===============================================
                uApplication.LIFE_ID = new NewBisPASvcRef.U_LIFE_ID();
                uApplication.LIFE_ID = PAR_PLAN_FREE;
                uApplication.LIFE_ID.UAPP_ID = uApplication.UAPP_ID;
                uApplication.LIFE_ID.ObjId = 1;

                //เก็บต่า Object ของ U_NAME_ID =============================================
                //Get ค่า U_NAME_ID เดิมว่ามีหรือป่าว ===================================================================================
                long? UNAME_ID_FREE = null;
                NewBisPASvcRef.U_APP_ADDRESS uAppAddressFree = new NewBisPASvcRef.U_APP_ADDRESS();

                long? PARENT_UNAME_ID_FREE = null;
                NewBisPASvcRef.U_APP_ADDRESS uParentAppAddressFree = new NewBisPASvcRef.U_APP_ADDRESS();

                if (uApplicationFree != null && uApplicationFree.UAPP_ID != null)
                {
                    if (uApplicationFree.NAME_ID_Collection != null && uApplicationFree.NAME_ID_Collection.Count() > 0)
                    {
                        var assured = uApplicationFree.NAME_ID_Collection.Where(item => item.CUSTOMER_TYPE.Equals("C")).FirstOrDefault();
                        UNAME_ID_FREE = assured.UNAME_ID;
                        uAppAddressFree = assured.APP_ADDRESS;

                        var parent = uApplicationFree.NAME_ID_Collection.Where(item => item.CUSTOMER_TYPE.Equals("P")).FirstOrDefault();
                        if (parent != null)
                        {
                            PARENT_UNAME_ID_FREE = parent.UNAME_ID;
                            uParentAppAddressFree = parent.APP_ADDRESS;
                        }
                    }
                }

                //END Get ค่า U_NAME_ID เดิมว่ามีหรือป่าว ===============================================================================
                uApplication.NAME_ID_Collection = new NewBisPASvcRef.U_NAME_ID_Collection();
                NewBisPASvcRef.U_NAME_ID uNameIDObj = new NewBisPASvcRef.U_NAME_ID();
                uNameIDObj.UAPP_ID = uApplication.UAPP_ID;
                uNameIDObj.UNAME_ID = UNAME_ID_FREE;
                uNameIDObj.ObjId = 1;
                ObjUNameID(ref uNameIDObj);
                uNameIDObj.APP_ADDRESS = new NewBisPASvcRef.U_APP_ADDRESS();
                if (uAppAddressFree != null && uAppAddressFree.UNAME_ID != null)
                {
                    uNameIDObj.APP_ADDRESS = uAppAddressFree;
                }
                else
                {
                    uNameIDObj.APP_ADDRESS.CONTACT_ADR = "Y";
                }
                uApplication.NAME_ID_Collection.Add(uNameIDObj);
                int? cusAge = txtStAge.Text.Trim() == "" ? (int?)null : Convert.ToInt16(txtStAge.Text.Trim());
                if (IsChildApplication(cusAge))
                {
                    var uParantNameIDObj = new NewBisPASvcRef.U_NAME_ID();
                    uParantNameIDObj.UAPP_ID = uApplication.UAPP_ID;
                    uParantNameIDObj.UNAME_ID = PARENT_UNAME_ID_FREE;  // default
                    uParantNameIDObj.ObjId = 2;
                    ObjUParentNameID(ref uParantNameIDObj);
                    uParantNameIDObj.APP_ADDRESS = new NewBisPASvcRef.U_APP_ADDRESS();
                    if (uAppAddressFree != null && uAppAddressFree.UNAME_ID != null)
                    {
                        uParantNameIDObj.APP_ADDRESS = uParentAppAddressFree;
                    }
                    else
                    {
                        uParantNameIDObj.APP_ADDRESS.CONTACT_ADR = "Y";
                    }
                    uApplication.NAME_ID_Collection.Add(uParantNameIDObj);

                }


                //เก็บต่า Object ของ U_NAME_ID =============================================

                //เก็บค่า W_UNDERWRITE_APPLICATION =========================================
                if (uApplicationFree != null && uApplicationFree.UAPP_ID != null)
                {
                    if (uApplicationFree.UNDERWRITE_APPLICATION != null && uApplicationFree.UNDERWRITE_APPLICATION.UAPP_ID != null)
                    {
                        uApplication.UNDERWRITE_APPLICATION = new NewBisPASvcRef.W_UNDERWRITE_APPLICATION();
                        uApplication.UNDERWRITE_APPLICATION = uApplicationFree.UNDERWRITE_APPLICATION;
                    }
                }
                //END เก็บค่า W_UNDERWRITE_APPLICATION =====================================

                //เก็บค่า U_APPLICATION_NAME ===============================================
                if (uApplicationFree.APPLICATION_NAME_Collection != null && uApplicationFree.APPLICATION_NAME_Collection.Count() > 0)
                {
                    uApplication.APPLICATION_NAME_Collection = new NewBisPASvcRef.U_APPLICATION_NAME_Collection();
                    uApplication.APPLICATION_NAME_Collection.AddRange(uApplicationFree.APPLICATION_NAME_Collection.ToArray());
                }
                //END เก็บค่า U_APPLICATION_NAME ===========================================

                uApplicationColl.Add(uApplication);

            }
            #endregion

            #region "PLAN PAID"
            if (PAR_PLAN_PAID != null && (!(String.IsNullOrEmpty(PAR_PLAN_PAID.PL_CODE))))
            {
                //Get ค่า U_APPLICATION เดิมว่ามีหรือป่าว ===================================================================================
                NewBisPASvcRef.U_APPLICATION uApplicationPaid = new NewBisPASvcRef.U_APPLICATION();
                long? UAPP_ID_PAID = null;
                String planIDPaid = PAR_PLAN_PAID.PL_BLOCK + PAR_PLAN_PAID.PL_CODE + PAR_PLAN_PAID.PL_CODE2;

                if (PAR_PA_APPLICATION_ID.APPLICATION_Collection != null && PAR_PA_APPLICATION_ID.APPLICATION_Collection.Count() > 0)
                {
                    foreach (NewBisPASvcRef.U_APPLICATION item in PAR_PA_APPLICATION_ID.APPLICATION_Collection)
                    {
                        if (item.LIFE_ID != null)
                        {
                            if (item.LIFE_ID.FREE_FLG == "N")
                            {
                                uApplicationPaid = item;
                                UAPP_ID_PAID = item.LIFE_ID.UAPP_ID;
                                break;
                            }
                        }
                    }
                }
                //END Get ค่า U_APPLICATION เดิมว่ามีหรือป่าว ===============================================================================

                NewBisPASvcRef.U_APPLICATION uApplication = new NewBisPASvcRef.U_APPLICATION();
                uApplication.APP_ID = PAR_PA_APPLICATION_ID.APP_ID;
                uApplication.OLD_POL = NAME_ID == null ? 'N' : 'Y';
                uApplication.APP_DT = txtAppDt.Text.Trim() == "" ? (DateTime?)null : Utility.StringToDateTime(txtAppDt.Text.Trim(), "BU");
                uApplication.APPSYS_DT = txtAppSysDt.Text.Trim() == "" ? (DateTime?)null : Utility.StringToDateTime(txtAppSysDt.Text.Trim(), "BU");
                uApplication.APP_OFC = txtAppOfc.Text.Trim();
                uApplication.APP_OFCRCV_DT = txtAppOfcRcvDt.Text.Trim() == "" ? (DateTime?)null : Utility.StringToDateTime(txtAppOfcRcvDt.Text.Trim(), "BU");
                uApplication.APP_HORCV_DT = txtAppHoRcvDt.Text.Trim() == "" ? (DateTime?)null : Utility.StringToDateTime(txtAppHoRcvDt.Text.Trim(), "BU");
                uApplication.APP_SIGN_DT = txtAppSignDt.Text.Trim() == "" ? (DateTime?)null : Utility.StringToDateTime(txtAppSignDt.Text.Trim(), "BU");
                uApplication.END_PROCESS = 'N';
                uApplication.ENTRY_OFC = uApplication.APP_OFC;
                uApplication.UNDERWRITING_FLG = cmbUnderWriteFlag.SelectedValue == null || cmbUnderWriteFlag.SelectedValue.ToString() == "" ? (char?)null : Convert.ToChar(cmbUnderWriteFlag.SelectedValue.ToString());
                uApplication.DOCUMENT_FLG = cmbDocumentFlag.SelectedValue == null || cmbDocumentFlag.SelectedValue.ToString() == "" ? (char?)null : Convert.ToChar(cmbDocumentFlag.SelectedValue.ToString());
                uApplication.UAPP_ID = UAPP_ID_PAID;

                //เก็บต่า Object ของแบบประกัน ===============================================
                uApplication.LIFE_ID = new NewBisPASvcRef.U_LIFE_ID();
                uApplication.LIFE_ID = PAR_PLAN_PAID;
                uApplication.LIFE_ID.UAPP_ID = uApplication.UAPP_ID;
                uApplication.LIFE_ID.ObjId = 1;

                long? SPOUSE_ID = null;
                if (uApplicationPaid != null && uApplicationPaid.UAPP_ID != null)
                {
                    if (uApplicationPaid.LIFE_ID != null && uApplicationPaid.LIFE_ID.UAPP_ID != null)
                    {
                        if (uApplicationPaid.LIFE_ID.APP_SPOUSE_Collection != null && uApplicationPaid.LIFE_ID.APP_SPOUSE_Collection.Count() > 0)
                        {
                            SPOUSE_ID = uApplicationPaid.LIFE_ID.APP_SPOUSE_Collection[0].SPOUSE_ID;
                        }
                    }
                }
                //เก็บต่า Object ของ  U_APP_SPOUSE ===============================================

                if (ckbSpouse.Checked == true && PAR_PLAN_PAID.SPOUSE_FLG == "T")
                {

                    NewBisPASvcRef.U_APP_SPOUSE uAppSpouseObj = new NewBisPASvcRef.U_APP_SPOUSE();
                    uAppSpouseObj.UAPP_ID = uApplication.LIFE_ID.UAPP_ID;
                    uAppSpouseObj.SPOUSE_ID = SPOUSE_ID;
                    uAppSpouseObj.ST_AGE = txtSpAge.Text == "" ? (int?)null : Convert.ToInt16(txtSpAge.Text);
                    uAppSpouseObj.ASS_TERM = txtSpAssTerm.Text.Trim() == "" ? (decimal?)null : Convert.ToDecimal(txtSpAssTerm.Text.Trim());
                    uAppSpouseObj.ISU_DT = uApplication.LIFE_ID.ISU_DT;
                    uAppSpouseObj.ASS_DT = txtSpAssDate.Text.Trim() == "" ? (DateTime?)null : Utility.StringToDateTime(txtSpAssDate.Text.Trim(), "BU");
                    uAppSpouseObj.TMN = 'N';
                    uAppSpouseObj.TMN_DT = null;
                    uAppSpouseObj.TMN_ID = null;
                    uApplication.LIFE_ID.APP_SPOUSE_Collection = new NewBisPASvcRef.U_APP_SPOUSE_Collection();
                    uApplication.LIFE_ID.APP_SPOUSE_Collection.Add(uAppSpouseObj);
                }
                else
                {
                    if (SPOUSE_ID != null)
                    {
                        NewBisPASvcRef.U_APP_SPOUSE uAppSpouseTmnObl = new NewBisPASvcRef.U_APP_SPOUSE();
                        foreach (NewBisPASvcRef.U_APP_SPOUSE obj in uApplicationPaid.LIFE_ID.APP_SPOUSE_Collection)
                        {
                            obj.UAPP_ID = uApplication.LIFE_ID.UAPP_ID;
                            obj.SPOUSE_ID = SPOUSE_ID;
                            obj.TMN = 'Y';
                            obj.TMN_DT = DateTime.Now;
                            obj.TMN_ID = UserID;
                            uAppSpouseTmnObl = obj;

                        }
                        uApplication.LIFE_ID.APP_SPOUSE_Collection = new NewBisPASvcRef.U_APP_SPOUSE_Collection();
                        uApplication.LIFE_ID.APP_SPOUSE_Collection.Add(uAppSpouseTmnObl);
                    }
                }

                if (uApplication.LIFE_ID.APP_SPOUSE_Collection != null && uApplication.LIFE_ID.APP_SPOUSE_Collection.Count() > 0)
                {
                    if (PAR_U_SPOUSE_ID_COLL == null)
                    {
                        PAR_U_SPOUSE_ID_COLL = new NewBisPASvcRef.U_SPOUSE_ID[uApplication.LIFE_ID.APP_SPOUSE_Collection.Count()];
                    }


                    for (int i = 0; i < uApplication.LIFE_ID.APP_SPOUSE_Collection.Count(); i++)
                    {

                        if (PAR_U_SPOUSE_ID_COLL[i] == null)
                        {
                            PAR_U_SPOUSE_ID_COLL[i] = new NewBisPASvcRef.U_SPOUSE_ID();
                        }
                        ObjUSpouseID(ref PAR_U_SPOUSE_ID_COLL[i]);
                        PAR_U_SPOUSE_ID_COLL[i].SPOUSE_ID = uApplication.LIFE_ID.APP_SPOUSE_Collection[0].SPOUSE_ID;
                        PAR_U_SPOUSE_ID_COLL[i].ObjId = uApplication.LIFE_ID.ObjId;
                        PAR_U_SPOUSE_ID_COLL[i].APP_SPOUSE_Collection = new NewBisPASvcRef.U_APP_SPOUSE_Collection();
                        PAR_U_SPOUSE_ID_COLL[i].APP_SPOUSE_Collection.Add(uApplication.LIFE_ID.APP_SPOUSE_Collection[0]);
                    }
                }

                //END เก็บต่า Object ของ  U_APP_SPOUSE ===============================================

                //END เก็บต่า Object ของแบบประกัน ===========================================

                //เก็บต่า Object ของ U_NAME_ID =============================================
                //Get ค่า U_NAME_ID เดิมว่ามีหรือป่าว ===================================================================================


                uApplication.NAME_ID_Collection = new NewBisPASvcRef.U_NAME_ID_Collection();
                NewBisPASvcRef.U_NAME_ID uParantNameIDObj = null;
                NewBisPASvcRef.U_NAME_ID uNameIDObj = new NewBisPASvcRef.U_NAME_ID();
                uNameIDObj.UAPP_ID = uApplication.UAPP_ID;
                uNameIDObj.UNAME_ID = null;  // default
                uNameIDObj.ObjId = 2;
                ObjUNameID(ref uNameIDObj);
                uNameIDObj.APP_ADDRESS = new NewBisPASvcRef.U_APP_ADDRESS() { CONTACT_ADR = "Y" }; // default




                int? cusAge = txtStAge.Text.Trim() == "" ? (int?)null : Convert.ToInt16(txtStAge.Text.Trim());
                if (IsChildApplication(cusAge))
                {
                    uParantNameIDObj = new NewBisPASvcRef.U_NAME_ID();
                    uParantNameIDObj.UAPP_ID = uApplication.UAPP_ID;
                    uParantNameIDObj.UNAME_ID = null;  // default
                    uParantNameIDObj.ObjId = 2;
                    ObjUParentNameID(ref uParantNameIDObj);
                    uParantNameIDObj.APP_ADDRESS = new NewBisPASvcRef.U_APP_ADDRESS() { CONTACT_ADR = "Y" }; // default
                }


                NewBisPASvcRef.U_APP_ADDRESS uAppAddressPaid = new NewBisPASvcRef.U_APP_ADDRESS();
                if (uApplicationPaid != null && uApplicationPaid.UAPP_ID != null)
                {
                    if (uApplicationPaid.NAME_ID_Collection != null && uApplicationPaid.NAME_ID_Collection.Any())
                    {
                        var uNameCusInfo = uApplicationPaid.NAME_ID_Collection.Where(item => item.CUSTOMER_TYPE.Equals("C")).FirstOrDefault();
                        if (uNameCusInfo != null)
                        {
                            uNameIDObj.UNAME_ID = uNameCusInfo.UNAME_ID;
                            uNameIDObj.APP_ADDRESS = uNameCusInfo.APP_ADDRESS;
                        }
                        if (IsChildApplication(cusAge))
                        {
                            var uNameParentInfo = uApplicationPaid.NAME_ID_Collection.Where(item => item.CUSTOMER_TYPE.Equals("P")).FirstOrDefault();
                            if (uNameParentInfo != null)
                            {
                                uParantNameIDObj.UNAME_ID = uNameParentInfo.UNAME_ID;
                                uParantNameIDObj.APP_ADDRESS = uNameParentInfo.APP_ADDRESS;
                            }
                        }
                    }
                }
                uApplication.NAME_ID_Collection.Add(uNameIDObj);
                if (uParantNameIDObj != null && IsChildApplication(cusAge))
                {
                    uApplication.NAME_ID_Collection.Add(uParantNameIDObj);
                }




                //END Get ค่า U_NAME_ID เดิมว่ามีหรือป่าว ===============================================================================

                //END เก็บต่า Object ของ U_NAME_ID =========================================

                //เก็บต่า Object ของ U_SMART_ID ============================================
                if (uApplicationPaid.SMART_ID_Collection != null && uApplicationPaid.SMART_ID_Collection.Count() > 0)
                {
                    uApplication.SMART_ID_Collection = new NewBisPASvcRef.U_SMART_ID_Collection();
                    uApplication.SMART_ID_Collection.AddRange(uApplicationPaid.SMART_ID_Collection.ToArray());
                }
                //END เก็บต่า Object ของ U_SMART_ID ========================================

                //เก็บค่า W_UNDERWRITE_APPLICATION =========================================
                if (uApplicationPaid != null && uApplicationPaid.UAPP_ID != null)
                {
                    if (uApplicationPaid.UNDERWRITE_APPLICATION != null && uApplicationPaid.UNDERWRITE_APPLICATION.UAPP_ID != null)
                    {
                        uApplication.UNDERWRITE_APPLICATION = new NewBisPASvcRef.W_UNDERWRITE_APPLICATION();
                        uApplication.UNDERWRITE_APPLICATION = uApplicationPaid.UNDERWRITE_APPLICATION;
                    }
                }
                //END เก็บค่า W_UNDERWRITE_APPLICATION =====================================

                //เก็บค่า U_APPLICATION_NAME ===============================================
                if (uApplicationPaid.APPLICATION_NAME_Collection != null && uApplicationPaid.APPLICATION_NAME_Collection.Count() > 0)
                {
                    uApplication.APPLICATION_NAME_Collection = new NewBisPASvcRef.U_APPLICATION_NAME_Collection();
                    uApplication.APPLICATION_NAME_Collection.AddRange(uApplicationPaid.APPLICATION_NAME_Collection.ToArray());
                }


                //END เก็บค่า U_APPLICATION_NAME ===========================================

                uApplicationColl.Add(uApplication);

            }



            #endregion

            if (uApplicationColl != null && uApplicationColl.Count() > 0)
            {
                PAR_PA_APPLICATION_ID.APPLICATION_Collection = new NewBisPASvcRef.U_APPLICATION_Collection();
                PAR_PA_APPLICATION_ID.APPLICATION_Collection.AddRange(uApplicationColl);
            }

            //END บันทึกครั้งแรกยังไม่มีค่าใน U_APPLICATION ===========================================================================



        }
        private void ObjUNameID(ref NewBisPASvcRef.U_NAME_ID uNameIDObj)
        {

            uNameIDObj.CUSTOMER_TYPE = "C";
            uNameIDObj.PRENAME = txtPrename.Text.Trim();
            uNameIDObj.NAME = txtName.Text.Trim();
            uNameIDObj.SURNAME = txtSurname.Text.Trim();
            if (cmbCardType.SelectedValue.ToString() == "1")
            {
                uNameIDObj.IDCARD_NO = txtIdcardNo.Text.Trim();
            }
            else if (cmbCardType.SelectedValue.ToString() == "2")
            {
                uNameIDObj.PASSPORT = txtIdcardNo.Text.Trim();
            }
            uNameIDObj.BIRTH_DT = txtBirthDt.Text.Trim() == "" ? (DateTime?)null : Utility.StringToDateTime(txtBirthDt.Text, "BU");
            uNameIDObj.SEX = cmbSex.SelectedValue == null || cmbSex.SelectedValue.ToString() == "" ? (char?)null : Convert.ToChar(cmbSex.SelectedValue.ToString());
            uNameIDObj.MB_PHONE = txtMbPhone.Text.Trim();
            uNameIDObj.NATIONALITY = cmbNationality.SelectedValue == null ? "" : cmbNationality.SelectedValue.ToString();
            uNameIDObj.ST_AGE = txtStAge.Text.Trim() == "" ? (int?)null : Convert.ToInt16(txtStAge.Text.Trim());
            uNameIDObj.MARITAL_STATUS = cmbMaritalStatus.SelectedValue == null || cmbMaritalStatus.SelectedValue.ToString() == "" ? (char?)null : Convert.ToChar(cmbMaritalStatus.SelectedValue.ToString());
            uNameIDObj.RELIGION = cmbReligion.SelectedValue == null || cmbReligion.SelectedValue.ToString() == "" ? (char?)null : Convert.ToChar(cmbReligion.SelectedValue.ToString());
            if (cmbDataCus.SelectedValue.ToString() == "1")
            {
                uNameIDObj.NAME_ID = NAME_ID;
                uNameIDObj.ONAME_ID = NAME_ID;
            }
            else
            {
                uNameIDObj.NAME_ID = null;
                uNameIDObj.ONAME_ID = null;
            }

            uNameIDObj.PARENT_ID = PARENT_ID;
            uNameIDObj.OPARENT_ID = PARENT_ID;

            //เก็บต่า Object ของ U_EMAIL_ID =============================================
            uNameIDObj.EMAIL_ID = new NewBisPASvcRef.U_EMAIL_ID();
            uNameIDObj.EMAIL_ID.UNAME_ID = uNameIDObj.UNAME_ID;
            uNameIDObj.EMAIL_ID.EMAIL = txtEmail.Text.Trim();
            //END เก็บต่า Object ของ U_EMAIL_ID =========================================

            //เก็บค่า Object ของ U_OCCUPATION ===========================================
            uNameIDObj.OCCUPATION = new NewBisPASvcRef.U_OCCUPATION();
            uNameIDObj.OCCUPATION.UNAME_ID = uNameIDObj.UNAME_ID;
            uNameIDObj.OCCUPATION.REGULAR_OCP_TYPE = txtOcpType.Text.Trim();
            uNameIDObj.OCCUPATION.REGULAR_OCP_CLASS = txtOcpClass.Text.Trim() == "" ? (char?)null : Convert.ToChar(txtOcpClass.Text.Trim());
            uNameIDObj.OCCUPATION.REGULAR_POSITION = cmbPosition.SelectedValue == null || cmbPosition.SelectedValue.ToString() == "" ? "" : cmbPosition.SelectedValue.ToString();
            uNameIDObj.OCCUPATION.REGULAR_WORK_TYPE = txtWorkType.Text.Trim();
            uNameIDObj.OCCUPATION.REGULAR_BUSINESS_TYPE = cmbBusinessType.SelectedValue == null || cmbBusinessType.SelectedValue.ToString() == "" ? "" : cmbBusinessType.SelectedValue.ToString();
            uNameIDObj.OCCUPATION.REGULAR_INCOME_PERYR = txtIncomePerYear.Text.Trim() == "" ? (long?)null : Convert.ToInt64(txtIncomePerYear.Text.Trim());
            uNameIDObj.OCCUPATION.HEIGHT = txtHeight.Text.Trim() == "" ? (int?)null : Convert.ToInt16(txtHeight.Text.Trim());
            uNameIDObj.OCCUPATION.WEIGHT = txtWeight.Text.Trim() == "" ? (int?)null : Convert.ToInt16(txtWeight.Text.Trim());
            uNameIDObj.OCCUPATION.MOTERCYCLE_USED = ckbMotercycleUsed.Checked == true ? 'Y' : 'N';



            //END เก็บค่า Object ของ U_OCCUPATION =======================================
        }

        private void ObjUParentNameID(ref NewBisPASvcRef.U_NAME_ID uNameIDObj)
        {

            uNameIDObj.CUSTOMER_TYPE = "P";
            uNameIDObj.PRENAME = txtParentPrename.Text.Trim();
            uNameIDObj.NAME = txtParentName.Text.Trim();
            uNameIDObj.SURNAME = txtParentSurname.Text.Trim();
            if (cmbParentCardType.SelectedValue.ToString() == "1")
            {
                uNameIDObj.IDCARD_NO = txtParentIdcardNo.Text.Trim();
            }
            else if (cmbParentCardType.SelectedValue.ToString() == "2")
            {
                uNameIDObj.PASSPORT = txtParentIdcardNo.Text.Trim();
            }
            uNameIDObj.BIRTH_DT = txtParentBirthDt.Text.Trim() == "" ? (DateTime?)null : Utility.StringToDateTime(txtParentBirthDt.Text, "BU");
            uNameIDObj.SEX = cmbParentGender.SelectedValue == null || cmbParentGender.SelectedValue.ToString() == "" ? (char?)null : Convert.ToChar(cmbParentGender.SelectedValue.ToString());
            uNameIDObj.MB_PHONE = txtParentMbPhone.Text.Trim();
            uNameIDObj.NATIONALITY = cmbParentNationality.SelectedValue == null ? "" : cmbParentNationality.SelectedValue.ToString();
            uNameIDObj.ST_AGE = txtParentStAge.Text.Trim() == "" ? (int?)null : Convert.ToInt16(txtParentStAge.Text.Trim());
            uNameIDObj.MARITAL_STATUS = cmbParentMaritalStatus.SelectedValue == null || cmbParentMaritalStatus.SelectedValue.ToString() == "" ? (char?)null : Convert.ToChar(cmbParentMaritalStatus.SelectedValue.ToString());
            uNameIDObj.RELIGION = cmbParentReligion.SelectedValue == null || cmbParentReligion.SelectedValue.ToString() == "" ? (char?)null : Convert.ToChar(cmbParentReligion.SelectedValue.ToString());
            if (cmbDataParent.SelectedValue.ToString() == "1")
            {
                uNameIDObj.NAME_ID = PARENT_PA_NAME_ID;
                uNameIDObj.ONAME_ID = PARENT_PA_NAME_ID;
            }
            else
            {
                uNameIDObj.NAME_ID = null;
                uNameIDObj.ONAME_ID = null;
            }

            uNameIDObj.PARENT_ID = PARENT_PARENT_ID;
            uNameIDObj.OPARENT_ID = PARENT_PARENT_ID;

            //เก็บต่า Object ของ U_EMAIL_ID =============================================
            uNameIDObj.EMAIL_ID = new NewBisPASvcRef.U_EMAIL_ID();
            uNameIDObj.EMAIL_ID.UNAME_ID = uNameIDObj.UNAME_ID;
            uNameIDObj.EMAIL_ID.EMAIL = txtParentEmail.Text.Trim();
            //END เก็บต่า Object ของ U_EMAIL_ID =========================================

            //เก็บค่า Object ของ U_OCCUPATION ===========================================
            uNameIDObj.OCCUPATION = new NewBisPASvcRef.U_OCCUPATION();
            uNameIDObj.OCCUPATION.UNAME_ID = uNameIDObj.UNAME_ID;
            uNameIDObj.OCCUPATION.REGULAR_OCP_TYPE = txtParentOcpType.Text.Trim();
            uNameIDObj.OCCUPATION.REGULAR_OCP_CLASS = txtParentOcpClass.Text.Trim() == "" ? (char?)null : Convert.ToChar(txtParentOcpClass.Text.Trim());
            uNameIDObj.OCCUPATION.REGULAR_POSITION = cmbParentPosition.SelectedValue == null || cmbParentPosition.SelectedValue.ToString() == "" ? "" : cmbParentPosition.SelectedValue.ToString();
            uNameIDObj.OCCUPATION.REGULAR_WORK_TYPE = txtParentWorkType.Text.Trim();
            uNameIDObj.OCCUPATION.REGULAR_BUSINESS_TYPE = cmbParentBusinessType.SelectedValue == null || cmbParentBusinessType.SelectedValue.ToString() == "" ? "" : cmbParentBusinessType.SelectedValue.ToString();
            uNameIDObj.OCCUPATION.REGULAR_INCOME_PERYR = txtParentIncomePerYear.Text.Trim() == "" ? (long?)null : Convert.ToInt64(txtParentIncomePerYear.Text.Trim());
            uNameIDObj.OCCUPATION.HEIGHT = txtParentHeight.Text.Trim() == "" ? (int?)null : Convert.ToInt16(txtParentHeight.Text.Trim());
            uNameIDObj.OCCUPATION.WEIGHT = txtParentWeight.Text.Trim() == "" ? (int?)null : Convert.ToInt16(txtParentWeight.Text.Trim());
            uNameIDObj.OCCUPATION.MOTERCYCLE_USED = ckbParentMotercycleUsed.Checked == true ? 'Y' : 'N';



            //END เก็บค่า Object ของ U_OCCUPATION =======================================
        }

        private void ObjUAppAddress(ref NewBisPASvcRef.U_APP_ADDRESS uAppAddressObj)
        {
            uAppAddressObj.CONTACT_ADR = "Y";
        }
        private void ObjUAddressID(ref NewBisPASvcRef.U_ADDRESS_ID uAddressIDObj)
        {
            if (cmbAdrType.SelectedValue.ToString() == "1")
            {
                uAddressIDObj.ADDRESS_ID = ADDRESS_ID;
            }
            else
            {
                uAddressIDObj.ADDRESS_ID = null;
            }
            uAddressIDObj.ADDRESS_TYPE = cmbAddressType.SelectedValue == null || cmbAddressType.SelectedValue.ToString() == "" ? (int?)null : Convert.ToInt16(cmbAddressType.SelectedValue.ToString());
            uAddressIDObj.ADDRESS_NUMBER = txtAddressNumber.Text.Trim();
            uAddressIDObj.ADDRESS_NAME = txtAddressName.Text.Trim();
            uAddressIDObj.MOOH = txtMooh.Text.Trim();
            uAddressIDObj.SOI = txtSoi.Text.Trim();
            uAddressIDObj.ROAD = txtRoad.Text.Trim();
            uAddressIDObj.TAMBOL = cmbTambol.Text.Trim() == "ระบุตำบลที่ต้องการ" ? "" : cmbTambol.Text.Trim();
            uAddressIDObj.AMPHUR = cmbAmphur.Text.Trim() == "ระบุอำเภอที่ต้องการ" ? "" : cmbAmphur.Text.Trim();
            uAddressIDObj.PROVINCE = cmbProvince.Text.Trim() == "ระบุจังหวัดที่ต้องการ" ? "" : cmbProvince.Text.Trim();
            uAddressIDObj.ZIP_CODE = txtZipcode.Text.Trim();
            uAddressIDObj.PHONE_NUMBER = txtPhoneNumber.Text.Trim();
            uAddressIDObj.OADDRESS_TYPE = cmbAdrType.SelectedValue == null ? "" : cmbAdrType.SelectedValue.ToString();
            uAddressIDObj.UPD_ID = UserID;
            uAddressIDObj.UPD_DT = DateTime.Now;
            uAddressIDObj.FSYSTEM_DT = DateTime.Now;


        }

        private void ObjUParentAddressID(ref NewBisPASvcRef.U_ADDRESS_ID uAddressIDObj)
        {
            if (cmbParentAdrType.SelectedValue.ToString() == "1")
            {
                uAddressIDObj.ADDRESS_ID = PARENT_ADDRESS_ID;
            }
            else
            {
                uAddressIDObj.ADDRESS_ID = null;
            }
            uAddressIDObj.ADDRESS_TYPE = cmbParentAddressType.SelectedValue == null || cmbParentAddressType.SelectedValue.ToString() == "" ? (int?)null : Convert.ToInt16(cmbParentAddressType.SelectedValue.ToString());
            uAddressIDObj.ADDRESS_NUMBER = txtParentAddressNumber.Text.Trim();
            uAddressIDObj.ADDRESS_NAME = txtParentAddressName.Text.Trim();
            uAddressIDObj.MOOH = txtParentMooh.Text.Trim();
            uAddressIDObj.SOI = txtParentSoi.Text.Trim();
            uAddressIDObj.ROAD = txtParentRoad.Text.Trim();
            uAddressIDObj.TAMBOL = cmbParentTambol.Text.Trim() == "ระบุตำบลที่ต้องการ" ? "" : cmbParentTambol.Text.Trim();
            uAddressIDObj.AMPHUR = cmbParentAmphur.Text.Trim() == "ระบุอำเภอที่ต้องการ" ? "" : cmbParentAmphur.Text.Trim();
            uAddressIDObj.PROVINCE = cmbParentProvince.Text.Trim() == "ระบุจังหวัดที่ต้องการ" ? "" : cmbParentProvince.Text.Trim();
            uAddressIDObj.ZIP_CODE = txtParentZipcode.Text.Trim();
            uAddressIDObj.PHONE_NUMBER = txtParentPhoneNumber.Text.Trim();
            uAddressIDObj.OADDRESS_TYPE = cmbParentAdrType.SelectedValue == null ? "" : cmbParentAdrType.SelectedValue.ToString();
            uAddressIDObj.UPD_ID = UserID;
            uAddressIDObj.UPD_DT = DateTime.Now;
            uAddressIDObj.FSYSTEM_DT = DateTime.Now;


        }
        private void ObjUSpouseID(ref NewBisPASvcRef.U_SPOUSE_ID objDetail)
        {
            objDetail.PRENAME = txtSpPrename.Text.Trim();
            objDetail.NAME = txtSpName.Text.Trim();
            objDetail.SURNAME = txtSpSurname.Text.Trim();
            if (cmbSpCardType.SelectedValue.ToString() == "1")
            {
                objDetail.IDCARD_NO = txtSpIDcardNo.Text.Trim();
                objDetail.PASSPORT = "";
            }
            else if (cmbSpCardType.SelectedValue.ToString() == "2")
            {
                objDetail.IDCARD_NO = "";
                objDetail.PASSPORT = txtSpIDcardNo.Text.Trim();
            }
            objDetail.BIRTH_DT = txtSpBirthDate.Text == "" ? (DateTime?)null : Utility.StringToDateTime(txtSpBirthDate.Text, "BU");
            objDetail.SEX = cmbSpSex.SelectedValue == null || cmbSpSex.SelectedValue.ToString() == "" ? (char?)null : Convert.ToChar(cmbSpSex.SelectedValue.ToString());
            objDetail.MB_PHONE = txtSpMbPhone.Text;
            objDetail.NATIONALITY = cmbSpNationality.SelectedValue == null ? "" : cmbSpNationality.SelectedValue.ToString();
        }
        private void ObjUApplicationUpdate(ref NewBisPASvcRef.U_APPLICATION_UPDATE objDetail)
        {
            objDetail.FSYSTEM_DT = DateTime.Now;
            objDetail.UPD_ID = UserID;
        }
        private void ObjUPolicySending(ref NewBisPASvcRef.U_POLICY_SENDING objDetail)
        {
            objDetail.SEND_TO = cmbSendTo.SelectedValue == null ? "" : cmbSendTo.SelectedValue.ToString();
            objDetail.OFFICE = txtSendOfc.Text.Trim();
            objDetail.BBL_CODE = txtSaleAgent.Text.Trim();
        }
        private void ObjUAgent(ref NewBisPASvcRef.U_AGENT objDetail)
        {
            objDetail.SALE_AGENT = txtSaleAgent.Text.Trim();
            objDetail.SALE_AGENT_UPL = txtSaleAgentUpl.Text.Trim();
            objDetail.LICENSE_AGENT = txtLicenseAgent.Text.Trim();
            objDetail.STR_DT = txtStrDt.Text.Trim() == "" ? (DateTime?)null : Utility.StringToDateTime(txtStrDt.Text.Trim(), "BU");
            objDetail.SALE_OFC = txtSaleOfc.Text.Trim();
            objDetail.LICENSE_OFC = txtLicenseOfc.Text.Trim();
            objDetail.PAY_COM_FLG = "N";

        }
        private void ObjCreditCard(ref NewBisPASvcRef.U_CREDIT_CARD objDetail)
        {
            if (txtExpireYY.Text.Trim() != "")
            {
                Decimal i = 0;
                if (!Decimal.TryParse(txtExpireYY.Text.Trim(), out i))
                {
                    tabMain.SelectTab("tabPlanData");
                    txtExpireYY.Focus();
                    throw new Exception("กรุณาระบุ ปีหมดอายุ เป็นตัวเลข");

                }
            }
            if (txtExpireMM.Text.Trim() != "")
            {
                Decimal i = 0;
                if (!Decimal.TryParse(txtExpireMM.Text.Trim(), out i))
                {
                    tabMain.SelectTab("tabPlanData");
                    txtExpireMM.Focus();
                    throw new Exception("กรุณาระบุ เดือนหมดอายุ เป็นตัวเลข");

                }
            }
            objDetail.PAY_OPTION = cmbPayOption.SelectedValue == null ? "" : cmbPayOption.SelectedValue.ToString();
            objDetail.FIN_CODE = txtFinCode.Text.Trim();
            objDetail.CARD_TYPE = txtCreditCardType.Text.Trim();
            objDetail.CARD_NO = txtCardNo1.Text.Trim() + txtCardNo2.Text.Trim() + txtCardNo3.Text.Trim() + txtCardNo4.Text.Trim();
            objDetail.EXPIRE_Y4 = txtExpireYY.Text.Trim() == "" ? (int?)null : Convert.ToInt16(txtExpireYY.Text.Trim());
            objDetail.EXPIRE_MM = txtExpireMM.Text.Trim() == "" ? (int?)null : Convert.ToInt16(txtExpireMM.Text.Trim());
            objDetail.CARD_AUTHORIZE = "";
        }
        private void ObjUBenefitID()
        {
            if (PAR_BENEFIT_ID_COLL_TMP != null && PAR_BENEFIT_ID_COLL_TMP.Count() > 0)
            {
                var GetData = from getData in PAR_BENEFIT_ID_COLL_TMP
                              where getData.TMN == 'N'
                              orderby getData.APP_BENEFIT.SEQ ascending
                              select getData;
                if (GetData != null && GetData.Count() > 0)
                {
                    PAR_BENEFIT_ID_COLL = new NewBisPASvcRef.U_BENEFIT_ID_COLLECTION();
                    int? i = 0;
                    foreach (NewBisPASvcRef.U_BENEFIT_ID uBenefitIDObj in GetData)
                    {
                        i = i + 1;
                        uBenefitIDObj.APP_BENEFIT.SEQ = i;
                        PAR_BENEFIT_ID_COLL.Add(uBenefitIDObj);
                    }
                }
            }
        }
        private void objWUnderwriteID(ref NewBisPASvcRef.W_UNDERWRITE_ID objDetail)
        {
            objDetail.PROCESS_FLG = 'P';
            objDetail.END_DT = null;
            objDetail.UPD_DT = DateTime.Now;
            objDetail.UPD_ID = UserID;
            objDetail.UNDERWRITE_DMS = null;
            objDetail.UNDERWRITE_DMS_DT = null;
            objDetail.UNDERWRITE_DMS_ID = null;
            objDetail.CONTENT_ID = null;
        }
        private void objWSuBUnderwriteID(ref NewBisPASvcRef.W_SUBUNDERWRITE_ID objDetail)
        {
            objDetail.CUSTOMER_TYPE = "C";
            objDetail.CLAIM = 'N';
        }
        private void objWSummary(ref NewBisPASvcRef.W_SUMMARY objDetail)
        {
            objDetail.STANDARD_TYPE = "SM";
            objDetail.STATUS = cmbSummryStatus.SelectedValue == null ? "" : cmbSummryStatus.SelectedValue.ToString();
            objDetail.SUBSTATUS = cmbSummrySubStatus.SelectedValue == null ? "" : cmbSummrySubStatus.SelectedValue.ToString();
            objDetail.STATUS_CAUSE = cmbSummaryStatusCause.SelectedValue == null ? "" : cmbSummaryStatusCause.SelectedValue.ToString();
            objDetail.FORWARD = 'N';
            objDetail.SEND_TOPOLICY = 'N';
            objDetail.TMN = 'N';
            objDetail.UPD_ID = UserID;

            String statusCause = objDetail.STATUS_CAUSE;
            if (objDetail.STATUS == "PP")
            {
                var GetData = from getData in PAR_LETTER_SCRIPT_COLL
                              where getData.STATUS_CAUSE == statusCause
                              select getData;
                if (GetData != null && GetData.Count() > 0)
                {
                    objDetail.POSTPONE = new NewBisPASvcRef.W_POSTPONE();
                    objDetail.POSTPONE.SUMMARY_ID = objDetail.SUMMARY_ID;
                    objDetail.POSTPONE.PP_UNTIL = cmbPPUntil.Text;
                }
            }
        }
        private void objWSummaryUnderwriter(ref NewBisPASvcRef.W_SUMMARY_UNDERWRITER objDetail)
        {
            objDetail.UND_ID = cmbUnderWriteID.SelectedValue == null ? "" : cmbUnderWriteID.SelectedValue.ToString();
        }
        private void objUApporved(ref NewBisPASvcRef.U_APPROVED objDetail, String statusCode)
        {
            objDetail.APPROVE_DT = Utility.StringToDateTime(txtApprovedDt.Text.Trim(), "BU");
            objDetail.INSTALL_DT = Utility.StringToDateTime(txtInstallDt.Text.Trim(), "BU");
            objDetail.INSTALL_ID = UserID;
            objDetail.DMS_CHANGE = 'N';
            if (statusCode == "IF")
            {
                objDetail.POLICY_DT = Utility.StringToDateTime(txtPolicyDt.Text.Trim(), "BU");
            }
        }
        private void objUOaddress(ref NewBisPASvcRef.U_OADDRESS objDetail, NewBisPASvcRef.U_ADDRESS_ID uAddressID)
        {
            bool ChkOAddress = false;

            if (objDetail == null)
            {
                ChkOAddress = true;
            }
            else
            {
                String addressOld = PAR_U_ADDRESS_ID_OLD.ADDRESS_NUMBER + PAR_U_ADDRESS_ID_OLD.ADDRESS_NAME + PAR_U_ADDRESS_ID_OLD.MOOH + PAR_U_ADDRESS_ID_OLD.SOI + PAR_U_ADDRESS_ID_OLD.ROAD + PAR_U_ADDRESS_ID_OLD.TAMBOL + PAR_U_ADDRESS_ID_OLD.AMPHUR + PAR_U_ADDRESS_ID_OLD.PROVINCE;
                String addressNew = uAddressID.ADDRESS_NUMBER + uAddressID.ADDRESS_NAME + uAddressID.MOOH + uAddressID.SOI + uAddressID.ROAD + uAddressID.TAMBOL + uAddressID.AMPHUR + uAddressID.PROVINCE;

                if (addressOld.Trim().Replace(" ", "") != addressNew.Trim().Replace(" ", ""))
                {
                    ChkOAddress = true;
                }
            }

            if (ChkOAddress == true)
            {
                Boolean varidateAddressLength = true;
                String addressLine1 = "";
                String addressLine2 = "";
                NewBisPASvcRef.ProcessResult pr = new NewBisPASvcRef.ProcessResult();
                using (NewBisPASvcRef.NewBisPASvcClient client = new NewBisPASvcRef.NewBisPASvcClient())
                {
                    varidateAddressLength = client.ValidateAddressLengthPA(out pr, uAddressID);
                    if (pr.Successed == false)
                    {
                        throw new Exception(pr.ErrorMessage);
                    }
                    pr = client.ConcatAddressPA(out addressLine1, out addressLine2, uAddressID);
                    if (pr.Successed == false)
                    {
                        throw new Exception(pr.ErrorMessage);
                    }
                }

                if (varidateAddressLength == false)
                {
                    FormConcatOldAddress formConcatOldAddress = new FormConcatOldAddress();
                    formConcatOldAddress.OADDRESS = new NewBisPASvcRef.U_OADDRESS();
                    formConcatOldAddress.OADDRESS.ADDRESS1 = addressLine1;
                    formConcatOldAddress.OADDRESS.ADDRESS2 = addressLine2;
                    formConcatOldAddress.ShowDialog();
                    objDetail = new NewBisPASvcRef.U_OADDRESS();
                    objDetail.UADDRESS_ID = null;
                    objDetail.ADDRESS1 = formConcatOldAddress.OADDRESS.ADDRESS1;
                    objDetail.ADDRESS2 = formConcatOldAddress.OADDRESS.ADDRESS2;
                }
                else
                {
                    objDetail = new NewBisPASvcRef.U_OADDRESS();
                    objDetail.UADDRESS_ID = null;
                    objDetail.ADDRESS1 = addressLine1;
                    objDetail.ADDRESS2 = addressLine2;
                }
            }

            if (string.IsNullOrEmpty(objDetail.ADDRESS1))
            {
                throw new Exception("ไม่มีข้อมูลใน ADDRESS1 เพื่อลงตาราง OADDRESS กรุณาติดต่อ IT เบอร์ 8518");
            }
            else
            {
                if (objDetail.ADDRESS1.Length > 55)
                {
                    throw new Exception("ADDRESS1 มีค่ามากกว่า 55 ตัวอักษร");
                }
            }
            if (string.IsNullOrEmpty(objDetail.ADDRESS2))
            {
                throw new Exception("ไม่มีข้อมูลใน ADDRESS2 เพื่อลงตาราง OADDRESS กรุณาติดต่อ IT เบอร์ 8518");
            }
            else
            {
                if (objDetail.ADDRESS2.Length > 45)
                {
                    throw new Exception("ADDRESS2 มีค่ามากกว่า 45 ตัวอักษร");
                }
            }

        }
        private void CutCaseApplication()
        {
            if (cmbUnderWriteFlag.SelectedValue.ToString() == "" && (cmbChannelType.SelectedValue.ToString() == "PO" || cmbChannelType.SelectedValue.ToString() == "PN"))
            {
                if (PAR_PA_APPLICATION_ID.APPLICATION_Collection != null && PAR_PA_APPLICATION_ID.APPLICATION_Collection.Count() > 0)
                {
                    String bankrupt = "N";
                    String receivePremium = "N";
                    String channelType = cmbChannelType.SelectedValue.ToString();  // cmbUnderWriteFlag.SelectedValue == null ? "" : cmbUnderWriteFlag.SelectedValue.ToString();
                    String policyHolder = "";
                    String oldPolicy = cmbDataCus.SelectedValue.ToString() == "1" ? "Y" : null;
                    String agentCode = txtSaleAgent.Text.Trim();
                    bool haveMed = false;
                    bool haveMIB = false;
                    if (PAR_CHECK_DATA_CUSTOMER != null)
                    {
                        if (PAR_CHECK_DATA_CUSTOMER.MIB_DATA == "Y")
                        {
                            haveMIB = true;
                        }
                        if (PAR_CHECK_DATA_CUSTOMER.BANKRUPT_DATA == "Y")
                        {
                            bankrupt = "Y";
                        }
                        if (PAR_CHECK_DATA_CUSTOMER.RECEIVE_PREMIUM_DATA == "Y")
                        {
                            receivePremium = "Y";
                        }
                    }
                    String nationality = cmbNationality.SelectedValue.ToString();
                    decimal? age = txtStAge.Text == "" ? (decimal?)null : Convert.ToDecimal(txtStAge.Text);
                    decimal? height = txtHeight.Text == "" ? (decimal?)null : Convert.ToDecimal(txtHeight.Text);
                    decimal? weight = txtWeight.Text == "" ? (decimal?)null : Convert.ToDecimal(txtWeight.Text);
                    bool questionClean = true;

                    NewBisPASvcRef.U_APPLICATION_NAME uApplicationNameObj = new NewBisPASvcRef.U_APPLICATION_NAME();
                    if (PAR_PA_APPLICATION_ID.APPLICATION_Collection[0].APPLICATION_NAME_Collection != null && PAR_PA_APPLICATION_ID.APPLICATION_Collection[0].APPLICATION_NAME_Collection.Count() > 0)
                    {
                        uApplicationNameObj = PAR_PA_APPLICATION_ID.APPLICATION_Collection[0].APPLICATION_NAME_Collection[0];
                        if (uApplicationNameObj.APPLICATION_QUESTIONAIRE_Collection != null && uApplicationNameObj.APPLICATION_QUESTIONAIRE_Collection.Count() > 0)
                        {
                            foreach (NewBisPASvcRef.U_APPLICATION_QUESTIONAIRE obj in uApplicationNameObj.APPLICATION_QUESTIONAIRE_Collection)
                            {
                                if (obj.CLEAN_FLG == 'N')
                                {
                                    questionClean = false;
                                    break;
                                }
                            }
                        }
                    }

                    bool benefitClean = true;
                    if (PAR_BENEFIT_ID_COLL != null && PAR_BENEFIT_ID_COLL.Count() > 0)
                    {
                        foreach (NewBisPASvcRef.U_BENEFIT_ID obj in PAR_BENEFIT_ID_COLL)
                        {
                            if (obj.CLEAR_FLG == 'N')
                            {
                                benefitClean = false;
                                break;
                            }
                        }
                    }

                    bool suspenseClean = true;
                    if (PAR_PREMIUM_SUSPENSE == null || PAR_PREMIUM_SUSPENSE == 0)
                    {
                        suspenseClean = false;
                    }

                    bool documentClean = true;
                    if (cmbDocumentFlag.SelectedValue.ToString() != "Y")
                    {
                        documentClean = false;
                    }

                    char? underwriteFlag = null;

                    if (bankrupt == "Y" || receivePremium == "Y")
                    {
                        underwriteFlag = 'M';
                    }
                    else
                    {

                        string xocpGrp = "";// txtXocpGrp.Text;
                        string xocpType = "";// txtXocpType.Text;

                        string plBlock = ""; //; cmbPlBlock.SelectedValue == null ? "" : cmbPlBlock.SelectedValue.ToString();
                        string plCode = ""; // txtPlCode.Text.Trim();
                        string plCode2 = "";// txtPlCode2.Text.Trim();
                        decimal? summ = null;// Convert.ToDecimal((txtSumm.Text.Trim().Replace(",", "")));

                        if (channelType.Equals("PN") || channelType.Equals("PO"))
                        {
                            using (var client = new NewBisPASvcRef.NewBisPASvcClient())
                            {
                                var pr = client.FFD_UNDERWRITING_FLG_ALL(out underwriteFlag, new NewBisPASvcRef.UnderwriteFlagParams()
                                {
                                    V_CHANNEL_TYPE = channelType,
                                    V_OPOLICY_HOLDER = policyHolder,
                                    V_OPOLICY = oldPolicy,
                                    V_AGENTCODE = agentCode,
                                    V_HAVEMED = haveMed,
                                    V_HAVEMIB = haveMIB,
                                    V_NATIONALITY = nationality,
                                    V_ST_AGE = age,
                                    V_HEIGHT = height,
                                    V_WEIGHT = weight,
                                    V_QUESTION_CLEAN = questionClean,
                                    V_RELBNF_CLEAN = benefitClean,
                                    V_SUSPENSE_CLEAN = suspenseClean,
                                    V_DOCUMENT_CLEAN = documentClean,
                                    V_ID_CARD = txtIdcardNo.Text.Trim(),
                                    PL_BLOCK = plBlock,
                                    PL_CODE = plCode,
                                    PL_CODE2 = plCode2,
                                    SUMM = summ,
                                    V_XOCP_GRP = xocpGrp,
                                    V_XOCP_TYPE = xocpType
                                });

                                if (pr.Successed == false)
                                {
                                    throw new Exception(pr.ErrorMessage);
                                }

                            }

                        }
                        else
                        {
                            ITUtility.ProcessResult pr = new ITUtility.ProcessResult();
                            using (PolicySvcRef.PolicySvcClient client = new PolicySvcRef.PolicySvcClient())
                            {


                                pr = client.FFD_UNDERWRITING_FLG_ALL(out underwriteFlag, channelType, policyHolder, oldPolicy, agentCode, haveMed, haveMIB, nationality, age, height, weight, questionClean, benefitClean, suspenseClean, documentClean, txtIdcardNo.Text.Trim(), xocpGrp, xocpType, plBlock, plCode, plCode2, summ);
                                if (pr.Successed == false)
                                {
                                    throw new Exception(pr.ErrorMessage);
                                }

                            }
                        }
                    }

                    if (underwriteFlag == null)
                    {
                        throw new Exception("ไม่สามารถ Cut CAse ได้ ติดต่อเจ้าหน้าที่ IT 8518");
                    }


                    if (PAR_PA_APPLICATION_ID.APPLICATION_Collection != null && PAR_PA_APPLICATION_ID.APPLICATION_Collection.Any())
                    {
                        foreach (NewBisPASvcRef.U_APPLICATION obj in PAR_PA_APPLICATION_ID.APPLICATION_Collection)
                        {
                            obj.UNDERWRITING_FLG = underwriteFlag;
                        }
                    }
                    cmbUnderWriteFlag.SelectedValue = underwriteFlag.Value.ToString();
                }
            }
            else if (cmbUnderWriteFlag.SelectedValue.ToString() == "" && cmbChannelType.SelectedValue.ToString() == "KB")
            {
                if (PAR_PA_APPLICATION_ID.APPLICATION_Collection != null && PAR_PA_APPLICATION_ID.APPLICATION_Collection.Count() > 0)
                {
                    String bankrupt = "N";
                    String receivePremium = "N";
                    String channelType = cmbUnderWriteFlag.SelectedValue == null ? "" : cmbUnderWriteFlag.SelectedValue.ToString();
                    String policyHolder = "";
                    String oldPolicy = cmbDataCus.SelectedValue.ToString() == "1" ? "Y" : null;
                    String agentCode = txtSaleAgent.Text.Trim();
                    bool haveMed = false;
                    bool haveMIB = false;
                    if (PAR_CHECK_DATA_CUSTOMER != null)
                    {
                        if (PAR_CHECK_DATA_CUSTOMER.MIB_DATA == "Y")
                        {
                            haveMIB = true;
                        }
                        if (PAR_CHECK_DATA_CUSTOMER.BANKRUPT_DATA == "Y")
                        {
                            bankrupt = "Y";
                        }
                        if (PAR_CHECK_DATA_CUSTOMER.RECEIVE_PREMIUM_DATA == "Y")
                        {
                            receivePremium = "Y";
                        }
                    }
                    String nationality = cmbNationality.SelectedValue.ToString();
                    decimal? age = txtStAge.Text == "" ? (decimal?)null : Convert.ToDecimal(txtStAge.Text);
                    decimal? height = txtHeight.Text == "" ? (decimal?)null : Convert.ToDecimal(txtHeight.Text);
                    decimal? weight = txtWeight.Text == "" ? (decimal?)null : Convert.ToDecimal(txtWeight.Text);
                    bool questionClean = true;

                    NewBisPASvcRef.U_APPLICATION_NAME uApplicationNameObj = new NewBisPASvcRef.U_APPLICATION_NAME();
                    if (PAR_PA_APPLICATION_ID.APPLICATION_Collection[0].APPLICATION_NAME_Collection != null && PAR_PA_APPLICATION_ID.APPLICATION_Collection[0].APPLICATION_NAME_Collection.Count() > 0)
                    {
                        uApplicationNameObj = PAR_PA_APPLICATION_ID.APPLICATION_Collection[0].APPLICATION_NAME_Collection[0];
                        if (uApplicationNameObj.APPLICATION_QUESTIONAIRE_Collection != null && uApplicationNameObj.APPLICATION_QUESTIONAIRE_Collection.Count() > 0)
                        {
                            foreach (NewBisPASvcRef.U_APPLICATION_QUESTIONAIRE obj in uApplicationNameObj.APPLICATION_QUESTIONAIRE_Collection)
                            {
                                if (obj.CLEAN_FLG == 'N')
                                {
                                    questionClean = false;
                                    break;
                                }
                            }
                        }
                    }

                    bool benefitClean = true;
                    if (PAR_BENEFIT_ID_COLL != null && PAR_BENEFIT_ID_COLL.Count() > 0)
                    {
                        foreach (NewBisPASvcRef.U_BENEFIT_ID obj in PAR_BENEFIT_ID_COLL)
                        {
                            if (obj.CLEAR_FLG == 'N')
                            {
                                benefitClean = false;
                                break;
                            }
                        }
                    }

                    bool suspenseClean = true;
                    if (PAR_PREMIUM_SUSPENSE == null || PAR_PREMIUM_SUSPENSE == 0)
                    {
                        suspenseClean = false;
                    }

                    bool documentClean = true;
                    if (cmbDocumentFlag.SelectedValue.ToString() != "Y")
                    {
                        documentClean = false;
                    }

                    char? underwriteFlag = null;

                    if (bankrupt == "Y" || receivePremium == "Y")
                    {
                        underwriteFlag = 'M';
                    }
                    else
                    {
                        ITUtility.ProcessResult pr = new ITUtility.ProcessResult();
                        using (PolicySvcRef.PolicySvcClient client = new PolicySvcRef.PolicySvcClient())
                        {
                            string xocpGrp = "";// txtXocpGrp.Text;
                            string xocpType = "";// txtXocpType.Text;

                            string plBlock = "";// cmbPlBlock.SelectedValue == null ? "" : cmbPlBlock.SelectedValue.ToString();
                            string plCode = ""; // txtPlCode.Text.Trim();
                            string plCode2 = "";// txtPlCode2.Text.Trim();
                            decimal? summ = null;// Convert.ToDecimal((txtSumm.Text.Trim().Replace(",", "")));
                            pr = client.FFD_UNDERWRITING_FLG_ALL(out underwriteFlag, channelType, policyHolder, oldPolicy, agentCode, haveMed, haveMIB, nationality, age, height, weight, questionClean, benefitClean, suspenseClean, documentClean, txtIdcardNo.Text.Trim(), xocpGrp, xocpType, plBlock, plCode, plCode2, summ);
                            if (pr.Successed == false)
                            {
                                throw new Exception(pr.ErrorMessage);
                            }
                        }
                    }

                    if (underwriteFlag == null)
                    {
                        throw new Exception("ไม่สามารถ Cut CAse ได้ ติดต่อเจ้าหน้าที่ IT 8518");
                    }

                    foreach (NewBisPASvcRef.U_APPLICATION obj in PAR_PA_APPLICATION_ID.APPLICATION_Collection)
                    {
                        obj.UNDERWRITING_FLG = underwriteFlag;
                    }

                    cmbUnderWriteFlag.SelectedValue = underwriteFlag.Value.ToString();
                }
            }
            #region "old code"
            //else if (cmbUnderWriteFlag.SelectedValue.ToString() == "" && cmbChannelType.SelectedValue.ToString() == "KB")
            //{
            //    char? underwriteFlag = null;
            //    int chkMed = 0;
            //    if (PAR_CHECK_DATA_CUSTOMER != null)
            //    {
            //        if (PAR_CHECK_DATA_CUSTOMER.MIB_DATA == "Y")
            //        {
            //            chkMed = chkMed + 1;
            //        }
            //        if (PAR_CHECK_DATA_CUSTOMER.BANKRUPT_DATA == "Y")
            //        {
            //            chkMed = chkMed + 1;
            //        }
            //        if (PAR_CHECK_DATA_CUSTOMER.RECEIVE_PREMIUM_DATA == "Y")
            //        {
            //            chkMed = chkMed + 1;
            //        }
            //    }

            //    underwriteFlag = 'C';

            //    if (chkMed > 0)
            //    {
            //        underwriteFlag = 'M';
            //    }
            //    else
            //    {
            //        bool benefitClean = true;
            //        if (PAR_BENEFIT_ID_COLL != null && PAR_BENEFIT_ID_COLL.Count() > 0)
            //        {
            //            foreach (NewBisPASvcRef.U_BENEFIT_ID obj in PAR_BENEFIT_ID_COLL)
            //            {
            //                if (obj.CLEAR_FLG == 'N')
            //                {
            //                    benefitClean = false;
            //                    break;
            //                }
            //            }
            //        }

            //        if (benefitClean == false)
            //        {
            //            underwriteFlag = 'N';
            //        }
            //    }
            //    foreach (NewBisPASvcRef.U_APPLICATION obj in PAR_PA_APPLICATION_ID.APPLICATION_Collection)
            //    {
            //        obj.UNDERWRITING_FLG = underwriteFlag;
            //    }
            //    cmbUnderWriteFlag.SelectedValue = underwriteFlag.Value.ToString();

            //}
            #endregion
            else if (cmbUnderWriteFlag.SelectedValue.ToString() == "" && (cmbChannelType.SelectedValue.ToString() == "PD" || cmbChannelType.SelectedValue.ToString() == "PF"))
            {
                char? underwriteFlag = null;
                int chkMed = 0;
                if (PAR_CHECK_DATA_CUSTOMER != null)
                {
                    if (PAR_CHECK_DATA_CUSTOMER.MIB_DATA == "Y")
                    {
                        chkMed = chkMed + 1;
                    }
                    if (PAR_CHECK_DATA_CUSTOMER.BANKRUPT_DATA == "Y")
                    {
                        chkMed = chkMed + 1;
                    }
                    if (PAR_CHECK_DATA_CUSTOMER.RECEIVE_PREMIUM_DATA == "Y")
                    {
                        chkMed = chkMed + 1;
                    }
                    if (PAR_CHECK_DATA_OTHER.MIB_DATA == "Y")
                    {
                        chkMed = chkMed + 1;
                    }
                    if (PAR_CHECK_DATA_OTHER.BANKRUPT_DATA == "Y")
                    {
                        chkMed = chkMed + 1;
                    }
                    if (PAR_CHECK_DATA_OTHER.RECEIVE_PREMIUM_DATA == "Y")
                    {
                        chkMed = chkMed + 1;
                    }
                }

                underwriteFlag = 'C';

                if (chkMed > 0)
                {
                    underwriteFlag = 'M';
                }
                //else
                //{
                //    bool benefitClean = true;
                //    if (PAR_BENEFIT_ID_COLL != null && PAR_BENEFIT_ID_COLL.Count() > 0)
                //    {
                //        foreach (NewBisPASvcRef.U_BENEFIT_ID obj in PAR_BENEFIT_ID_COLL)
                //        {
                //            if (obj.CLEAR_FLG == 'N')
                //            {
                //                benefitClean = false;
                //                break;
                //            }
                //        }
                //    }

                //    if (benefitClean == false)
                //    {
                //        underwriteFlag = 'N';
                //    }
                //}
                if (PAR_PA_APPLICATION_ID.APPLICATION_Collection != null && PAR_PA_APPLICATION_ID.APPLICATION_Collection.Any())
                {
                    foreach (NewBisPASvcRef.U_APPLICATION obj in PAR_PA_APPLICATION_ID.APPLICATION_Collection)
                    {
                        obj.UNDERWRITING_FLG = underwriteFlag;
                    }
                }
                cmbUnderWriteFlag.SelectedValue = underwriteFlag.Value.ToString();
            }
        }
        private void SaveData()
        {

            var statusOld = PAR_W_SUMMARY != null ? PAR_W_SUMMARY.STATUS_CAUSE ?? "" : "";
            var subStatusOld = PAR_W_SUMMARY != null ? PAR_W_SUMMARY.SUBSTATUS ?? "" : "";

            if (PAR_PA_APPLICATION_ID != null && (!(String.IsNullOrEmpty(PAR_PA_APPLICATION_ID.APP_NO))))
            {
                btnHeadSave.Enabled = false;
                NewBisPASvcRef.ProcessResult pr = new NewBisPASvcRef.ProcessResult();

                using (NewBisPASvcRef.NewBisPASvcClient client = new NewBisPASvcRef.NewBisPASvcClient())
                {
                    if (PAR_END_PROCESS != 'Y')
                    {
                        pr = client.AddUpdateUApplicationIDWithRegisPayoption(ref PAR_PA_APPLICATION_ID, ref PAR_U_ADDRESS_ID_COLL, ref PAR_U_SPOUSE_ID_COLL, ref PAR_BENEFIT_ID_COLL, ref PAR_W_UNDERWRITE_ID_COLL, ref PAR_U_ACCOUNT_ID_COLL, PAR_PREMIUM_SUSPENSE, pPosID, UserID);
                        if (pr.Successed == false)
                        {
                            throw new Exception(pr.ErrorMessage);
                        }


                    }

                }

                var repo = new RenewalRepository();
                if (PAR_END_PROCESS != 'Y')
                {

                    if (!repo.IsRenewApplication(PAR_PA_APPLICATION_ID.APP_NO, PAR_PA_APPLICATION_ID.CHANNEL_TYPE))
                    {

                        //ส่งข้อมูลให้ระบบ ISIS ================================================================================
                        SendStatusToIsis();
                        //END ส่งข้อมูลให้ระบบ ISIS ============================================================================

                        //ออกใบเสร็จ =========================================================================================
                        #region "ออกใบเสร็จ"
                        if (PAR_PA_APPLICATION_ID != null && PAR_PA_APPLICATION_ID.APP_ID != null)
                        {


                            if (PAR_PA_APPLICATION_ID.APP_ISIS != null && PAR_PA_APPLICATION_ID.APP_ISIS.APP_ID != null)
                            {
                                NewBisPASvcRef.U_APP_ISIS uAppIsisTmp = new NewBisPASvcRef.U_APP_ISIS();
                                uAppIsisTmp = PAR_PA_APPLICATION_ID.APP_ISIS;

                                if (!(String.IsNullOrEmpty(uAppIsisTmp.GUID)))
                                {
                                    if (PAR_PA_APPLICATION_ID.APPLICATION_Collection != null && PAR_PA_APPLICATION_ID.APPLICATION_Collection.Count() > 0)
                                    {
                                        foreach (NewBisPASvcRef.U_APPLICATION uApplictionObj in PAR_PA_APPLICATION_ID.APPLICATION_Collection)
                                        {
                                            if (uApplictionObj.LIFE_ID != null && uApplictionObj.LIFE_ID.UAPP_ID != null)
                                            {
                                                if (uApplictionObj.LIFE_ID.FREE_FLG == "N")
                                                {
                                                    if (uApplictionObj.APPROVED_Collection != null && uApplictionObj.APPROVED_Collection.Count() > 0)
                                                    {
                                                        NewBisPASvcRef.U_APPROVED uApprovedObj = new NewBisPASvcRef.U_APPROVED();
                                                        uApprovedObj = uApplictionObj.APPROVED_Collection[0];
                                                        String policyHolding = "";
                                                        if (PAR_PA_APPLICATION_ID.CHANNEL_TYPE == "KB" || PAR_PA_APPLICATION_ID.CHANNEL_TYPE == "PD" || PAR_PA_APPLICATION_ID.CHANNEL_TYPE == "PF")
                                                        {
                                                            if (uApprovedObj.POLICYHOLDING_Collection != null && uApprovedObj.POLICYHOLDING_Collection.Count() > 0)
                                                            {
                                                                NewBisPASvcRef.U_POLICYHOLDING uPolicyHoldingTmp = new NewBisPASvcRef.U_POLICYHOLDING();
                                                                uPolicyHoldingTmp = uApprovedObj.POLICYHOLDING_Collection[0];
                                                                if (String.IsNullOrEmpty(uPolicyHoldingTmp.POLICY_HOLDING))
                                                                {
                                                                    throw new Exception("ไม่มีข้อมูล U_POLICYHOLDING ของส่วนการออกใบเสร็จ");
                                                                }
                                                                policyHolding = uPolicyHoldingTmp.POLICY_HOLDING;
                                                            }
                                                        }

                                                        if (!(String.IsNullOrEmpty(uApprovedObj.POLICY)))
                                                        {
                                                            ReceiptSvcRef.ServiceClient repClient = new ReceiptSvcRef.ServiceClient();
                                                            repClient.GenerateReceipt(uAppIsisTmp.GUID, PAR_PA_APPLICATION_ID.CHANNEL_TYPE, uApprovedObj.POLICY, policyHolding, uApplictionObj.LIFE_ID.ISU_DT, uApplictionObj.UAPP_ID, UserID);
                                                        }

                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }


                        }
                        #endregion
                        //END ออกใบเสร็จ =====================================================================================
                    }

                }

                string summaryStatusAfterSave = cmbSummryStatus.SelectedValue.ToString();
                string summarySubStatusAfterSave = cmbSummrySubStatus.SelectedValue.ToString();


                PAR_CLICK_CUSFORM = false;
                PAR_CAL_SUMM = false;
                PAR_TOTAL_SUMM_FREE = 0;
                PAR_TOTAL_SUMM_BUY = 0;

                PAR_PLAN_FREE = new NewBisPASvcRef.U_LIFE_ID();
                PAR_PLAN_PAID = new NewBisPASvcRef.U_LIFE_ID();


                cmbSummryStatus.SelectedValue = "";
                PAR_W_SUMMARY = new NewBisPASvcRef.W_SUMMARY();
                PAR_U_MEMO_ID_COLL_TMP = new NewBisPASvcRef.U_MEMO_ID_Collection();

                TabMainParameter();
                tabMain.SelectTab("tabCustomerData");
                btnHeadSave.Enabled = true;
                GetPlanParametersOld(PAR_PA_APPLICATION_ID.APPLICATION_Collection);

                SetColorItemBeginCustomerName();
                SetColorItemBeginCustomerAddress();

                if (PAR_U_ADDRESS_ID_COLL != null && PAR_U_ADDRESS_ID_COLL.Count() > 0)
                {
                    PAR_U_ADDRESS_ID_OLD = PAR_U_ADDRESS_ID_COLL[0];
                    PAR_U_OADDRESS = new NewBisPASvcRef.U_OADDRESS();
                    PAR_U_OADDRESS = PAR_U_ADDRESS_ID_OLD.OADDRESS;
                }

                if (NAME_ID != null)
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

                            cmbDataCus.SelectedValue = "1";
                            cmbDataCus.BackColor = Color.FromArgb(bgRed[0], bgRed[1], bgRed[2]);

                        }

                    }
                }
                if (ADDRESS_ID != null)
                {
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

                //UpDate ค่าต่างหลังการบันทึก ==============================================
                //Get U_CAL_ERROR =======================================================================
                if (PAR_PA_APPLICATION_ID != null && PAR_PA_APPLICATION_ID.APP_ID != null)
                {
                    if (PAR_PA_APPLICATION_ID.CAL_ERROR != null && PAR_PA_APPLICATION_ID.CAL_ERROR.APP_ID != null)
                    {
                        PAR_U_CAL_ERROR = new NewBisPASvcRef.U_CAL_ERROR();
                        PAR_U_CAL_ERROR = PAR_PA_APPLICATION_ID.CAL_ERROR;
                    }
                }
                //END Get U_CAL_ERROR ===================================================================
                if (PAR_PA_APPLICATION_ID.APPLICATION_Collection != null && PAR_PA_APPLICATION_ID.APPLICATION_Collection.Count() > 0)
                {
                    GetDataAfterSave(PAR_PA_APPLICATION_ID.APPLICATION_Collection[0]);


                }
                //END UpDate ค่าต่างหลังการบันทึก ==========================================



                //#region SendImageISISToDMBSNB

                //string[] endProcessStatusApp = { "IF", "CC", "DC", "EX", "NT", "PP" };
                //if (endProcessStatusApp.Contains(summaryStatusAfterSave))
                //{
                //    try
                //    {
                //        long appId = 0, uappId = 0;
                //        string channelType1 = "", policyId = "";
                //        string appNo = txtAppNo.Text.Trim();
                //        string policy = "", policyHolding = "";
                //        try
                //        {
                //            appId = PAR_PA_APPLICATION_ID.APPLICATION_Collection.First().APP_ID ?? 0;
                //            uappId = PAR_PA_APPLICATION_ID.APPLICATION_Collection.First().UAPP_ID ?? 0;
                //            channelType1 = PAR_PA_APPLICATION_ID.CHANNEL_TYPE;
                //            var policObj = PAR_PA_APPLICATION_ID.APPLICATION_Collection.First().APPROVED_Collection;
                //            if (policObj != null && policObj.Any())
                //            {
                //                var policyInfo = policObj.First();
                //                policyId = policyInfo.POLICY_ID.Value.ToString();
                //                policy = policyInfo.POLICY;
                //                if (policyInfo.POLICYHOLDING_Collection != null && policyInfo.POLICYHOLDING_Collection.Any())
                //                {
                //                    policyHolding = policyInfo.POLICYHOLDING_Collection.First().POLICY_HOLDING;
                //                }
                //            }
                //        }
                //        catch (Exception e)
                //        {
                //            sendMailDMSNB(e.Message + "ไม่สามารถ อัพโหลดไฟล์ไปยัง DMNB --> ม่สามารถดึงข้อมูล policy ได้");
                //        }
                //        var isSendToEasy = chbDmsChange.Checked ? false : true;
                //        GetImageISISAndUpload2DMBSNB(uappId, appId, channelType1, policyId, summaryStatusAfterSave, summarySubStatusAfterSave, appNo, policy, policyHolding, isSendToEasy);
                //    }
                //    catch (Exception e)
                //    {
                //        //  MessageBox.Show("ทำการบันทึกข้อมูล NewBIs เรียบร้อย แต่ไม่สามารถ อัพโหลดไฟล์ไปยัง DMNB ได้");
                //        sendMailDMSNB(PAR_PA_APPLICATION_ID.APP_NO + e.Message);
                //    }
                //}

                //#endregion

                string channelType = cmbChannelType.SelectedValue.ToString();
                #region GetImageISISAndUpload2DMBSNB
                var StatusCode = summaryStatusAfterSave;
                string[] endProcessStatusApp = { "IF", "CC", "DC", "EX", "NT", "PP" };
                if (endProcessStatusApp.Contains(StatusCode))
                {
                    try
                    {
                        string policy = "", policyHolding = "", policyId = "";
                        long appNo = 0;
                        //var channelType = "", policyId = "", policyHolding = "";
                        string subStatus = cmbSummryStatus.SelectedValue == null || cmbSummryStatus.SelectedValue.ToString() == "" ? "WT" : cmbSummryStatus.SelectedValue.ToString();
                        try
                        {
                            var policObj = PAR_PA_APPLICATION_ID.APPLICATION_Collection.First().APPROVED_Collection;
                            if (policObj != null && policObj.Any())
                            {
                                var policyInfo = policObj.First();
                                policyId = policyInfo.POLICY_ID.Value.ToString();
                                policy = policyInfo.POLICY;
                                if (policyInfo.POLICYHOLDING_Collection != null && policyInfo.POLICYHOLDING_Collection.Any())
                                {
                                    policyHolding = policyInfo.POLICYHOLDING_Collection.First().POLICY_HOLDING;
                                }
                            }
                        }
                        catch (Exception e)
                        {
                            sendMailDMSNB(e.Message + "ไม่สามารถ อัพโหลดไฟล์ไปยัง DMNB --> ม่สามารถดึงข้อมูล policy ได้");
                        }
                        //  GetImageISISAndUpload2DMBSNB(appNo, channelType, policyId, StatusCode, subStatus);
                        var isUploadToEDM = false;
                        var refType = "";
                        var appId = PAR_PA_APPLICATION_ID.APP_ID.Value;
                        var uappId = PAR_PA_APPLICATION_ID.APPLICATION_Collection.First().UAPP_ID.Value;
                        var appNoParam = PAR_PA_APPLICATION_ID.APP_NO;
                        var appRepo = new NewBisRepository();
                        try
                        {
                            refType = "RENEW"; // ทำการเพิ่มใหม่ทังหมด
                            #region " Set upload to param"
                            var isChangeDMS = chbDmsChange.Checked;
                            var isUseISISFlag = false; //ITUtility.Utility.AppSettings(NewBIS.Library.SystemConst.AppSettingConst.UseISISFlag) == "Y" ? true : false;
                            if (!string.IsNullOrEmpty(policyId) && !isChangeDMS && !isUseISISFlag)
                            {
                                isUploadToEDM = true;
                            }
                            #endregion
                            var documentIndex = 1;
                            var imgeDoc = appRepo.GetApplicationDocument(appId, appNoParam, channelType, _UserID);
                            if (imgeDoc != null && imgeDoc.ImageApp != null && imgeDoc.ImageApp.DataFiles != null && imgeDoc.ImageApp.DataFiles.Any())
                            {
                                refType = "GUID"; // ทำการเพิ่มใบคำขอหลักใหม่
                                long? imageIdEasy = null;


                                if (isUseISISFlag)
                                {
                                    #region " Upload to target IMS,EDM -> OLD Tag Info"
                                    foreach (var fileItem in imgeDoc.ImageApp.DataFiles)
                                    {
                                        try
                                        {
                                            if (fileItem != null)
                                            {
                                                appRepo.UploadImageToDMSNB(out imageIdEasy, fileItem.FileByte, fileItem.ContentType, appNoParam, channelType, policyId, policy, policyHolding, uappId, appId, documentIndex, StatusCode, isUploadToEDM, _UserID);
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            #region Add Log
                                            appRepo.WriteErrorLogToDMSNB(new NewBisOnlineWcfRef.U_DMSNB_LOG()
                                            {
                                                APP_ID = appId,
                                                REF_ID = refType,
                                                REF_TYPE = refType,
                                                SEND_AMOUNT = 1,
                                                SEND_FLG = 'N',
                                                STATUS = StatusCode,
                                                SUB_STATUS = subStatus,
                                                EXCEPTION_MSG = "imgeDoc.ImageApp.DataFiles" + ex.Message,
                                                CHANNEL_TYPE = channelType,
                                                FILE_SEQ = 0,
                                                POLICY_ID = policyId,
                                                SEND_EDM_FLG = isUploadToEDM ? "Y" : "N"
                                            });
                                            #endregion
                                        }
                                    }
                                    if (imgeDoc.ImageAppAditonal != null && imgeDoc.ImageAppAditonal.Any())
                                    {
                                        refType = "CONTENT_ID"; // ทำการเพิ่มเอกสารเพิ่มเติม
                                        foreach (var item in imgeDoc.ImageAppAditonal)
                                        {
                                            if (item.DataFile != null && item.DataFile.FileByte != null && item.DataFile.FileByte.Any())
                                            {
                                                try
                                                {
                                                    documentIndex++;
                                                    appRepo.UploadImageToDMSNB(out imageIdEasy, item.DataFile.FileByte, item.DataFile.ContentType, appNoParam, channelType, policyId, policy, policyHolding, uappId, appId, documentIndex, item.StatusType, isUploadToEDM, _UserID);
                                                }
                                                catch (Exception ex)
                                                {

                                                    #region Add Log
                                                    appRepo.WriteErrorLogToDMSNB(new NewBisOnlineWcfRef.U_DMSNB_LOG()
                                                    {
                                                        APP_ID = appId,
                                                        REF_ID = refType,
                                                        REF_TYPE = refType,
                                                        SEND_AMOUNT = 1,
                                                        SEND_FLG = 'N',
                                                        STATUS = StatusCode,
                                                        SUB_STATUS = subStatus,
                                                        EXCEPTION_MSG = "imgeDoc.ImageAppAditonal " + ex.Message,
                                                        CHANNEL_TYPE = channelType,
                                                        FILE_SEQ = 0,
                                                        POLICY_ID = policyId,
                                                        SEND_EDM_FLG = isUploadToEDM ? "Y" : "N"
                                                    });
                                                    #endregion
                                                }

                                            }
                                        }
                                    }
                                    #endregion
                                }
                                else
                                {
                                    #region " Upload to target  IMD,EDM New Tag Info"
                                    foreach (var fileItem in imgeDoc.ImageApp.DataFiles)
                                    {
                                        if (fileItem != null)
                                        {
                                            try
                                            {
                                                if (imgeDoc.ImageApp.ImageId != null)
                                                {
                                                    if (!chbDmsChange.Checked || StatusCode != "IF")
                                                    {
                                                        appRepo.UploadFlagEDM(imgeDoc.ImageApp.ImageId.Value);
                                                    }
                                                }
                                                else
                                                {
                                                    appRepo.UploadImageToIMS_EDM(out imageIdEasy, fileItem.FileByte, fileItem.ContentType, appNoParam, channelType, policyId, policy, policyHolding, uappId, appId, isUploadToEDM, _UserID);
                                                }
                                            }
                                            catch (Exception ex)
                                            {

                                                #region Add Log
                                                appRepo.WriteErrorLogToDMSNB(new NewBisOnlineWcfRef.U_DMSNB_LOG()
                                                {
                                                    APP_ID = appId,
                                                    REF_ID = refType,
                                                    REF_TYPE = refType,
                                                    SEND_AMOUNT = 1,
                                                    SEND_FLG = 'N',
                                                    STATUS = StatusCode,
                                                    SUB_STATUS = subStatus,
                                                    EXCEPTION_MSG =  "imgeDoc.ImageApp.DataFiles" + ex.Message,
                                                    CHANNEL_TYPE = channelType,
                                                    FILE_SEQ = 0,
                                                    POLICY_ID = policyId,
                                                    SEND_EDM_FLG = isUploadToEDM ? "Y" : "N"
                                                });
                                                #endregion
                                            }
                                        }
                                    }
                                    if (imgeDoc.ImageAppAditonal != null && imgeDoc.ImageAppAditonal.Any())
                                    {
                                        refType = "CONTENT_ID"; // ทำการเพิ่มเอกสารเพิ่มเติม
                                        foreach (var item in imgeDoc.ImageAppAditonal)
                                        {
                                            try
                                            {
                                                if (item.ImageId != null)
                                                {
                                                    if (!chbDmsChange.Checked || StatusCode != "IF")
                                                    {
                                                        appRepo.UploadFlagEDM(item.ImageId.Value);
                                                    }
                                                }
                                                else if (item.DataFile != null && item.DataFile.FileByte != null && item.DataFile.FileByte.Any())
                                                {
                                                    documentIndex++;
                                                    appRepo.UploadImageToIMS_EDM(out imageIdEasy, item.DataFile.FileByte, item.DataFile.ContentType, appNoParam, channelType, policyId, policy, policyHolding, uappId, appId, isUploadToEDM, _UserID);
                                                }
                                            }
                                            catch (Exception ex)
                                            {

                                                #region Add Log
                                                appRepo.WriteErrorLogToDMSNB(new NewBisOnlineWcfRef.U_DMSNB_LOG()
                                                {
                                                    APP_ID = appId,
                                                    REF_ID = refType,
                                                    REF_TYPE = refType,
                                                    SEND_AMOUNT = 1,
                                                    SEND_FLG = 'N',
                                                    STATUS = StatusCode,
                                                    SUB_STATUS = subStatus,
                                                    EXCEPTION_MSG = "imgeDoc.ImageAppAditonal " + ex.Message,
                                                    CHANNEL_TYPE = channelType,
                                                    FILE_SEQ = 0,
                                                    POLICY_ID = policyId,
                                                    SEND_EDM_FLG = isUploadToEDM ? "Y" : "N"
                                                });
                                                #endregion
                                            }
                                        }
                                    }
                                    #endregion
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            #region Add Log
                            var tt = new NewBisOnlineWcfRef.U_DMSNB_LOG()
                            {
                                APP_ID = appId,
                                REF_ID = refType,
                                REF_TYPE =  "RENEW",
                                SEND_AMOUNT = 1,
                                SEND_FLG = 'N',
                                STATUS = StatusCode,
                                SUB_STATUS = subStatus,
                                EXCEPTION_MSG = "Upload Image Error :" + ex.Message,
                                CHANNEL_TYPE = channelType,
                                FILE_SEQ = 0,
                                POLICY_ID = policyId,
                                SEND_EDM_FLG = isUploadToEDM ? "Y" : "N"
                            };
                            appRepo.WriteErrorLogToDMSNB(tt);
                            #endregion
                        }

                    }
                    catch (Exception e)
                    {
                        MessageBox.Show("ทำการบันทึกข้อมูล NewBIs เรียบร้อย แต่ไม่สามารถ อัพโหลดไฟล์ไปยัง DMNB ได้");
                        sendMailDMSNB(PAR_PA_APPLICATION_ID.APP_NO + e.Message);
                    }
                }
                #endregion

                //#endregion
                if (channelType == "PO" || channelType == "PN")
                {
                    #region "ส่ง SMS"

                    try
                    {

                        var statusNow = summaryStatusAfterSave;
                        var subStatusNow = cmbSummrySubStatus.SelectedValue == null || cmbSummrySubStatus.SelectedValue.ToString() == "" ? "WT" : cmbSummrySubStatus.SelectedValue.ToString();
                        String policyIF = "";
                        long? policyIDIF = null;

                        if (PAR_PA_APPLICATION_ID != null && PAR_PA_APPLICATION_ID.APPLICATION_Collection != null && PAR_PA_APPLICATION_ID.APPLICATION_Collection.Any() && chkSms.Checked == true)
                        {
                            if (statusNow == "IF")
                            {

                                if (PAR_PA_APPLICATION_ID.APP_ID != null)
                                {
                                    if (PAR_PA_APPLICATION_ID.APPLICATION_Collection.First().UAPP_ID != null)
                                    {
                                        if (PAR_PA_APPLICATION_ID.APPLICATION_Collection.First().APPROVED_Collection != null && PAR_PA_APPLICATION_ID.APPLICATION_Collection.First().APPROVED_Collection.Any())
                                        {
                                            policyIF = PAR_PA_APPLICATION_ID.APPLICATION_Collection.First().APPROVED_Collection[0].POLICY;
                                            policyIDIF = PAR_PA_APPLICATION_ID.APPLICATION_Collection.First().APPROVED_Collection[0].POLICY_ID;
                                        }
                                    }
                                }

                                if (policyIF == "")
                                {

                                    throw new Exception("ไม่สามารถส่ง SMS ได้เนื่องจากสถานะ IF แต่ไม่มีเลขที่กรมธรรม์");
                                }
                            }
                        }
                        var ChannelType = cmbChannelType.SelectedValue.ToString();
                        var workGroup = cmbWorkGroup.SelectedValue.ToString();
                        if ((statusOld + subStatusOld) != (statusNow + subStatusNow))
                        {
                            String customerName = "(" + txtName.Text.Trim() + " " + (txtSurname.Text.Trim() == "" ? "" : txtSurname.Text.Trim().Substring(0, 1)) + ".)";
                            String[] statusSms = { "AP", "MO", "MD", "CO", "IF" };
                            if (statusSms.Contains(statusNow) && (!(statusNow == "AP" && subStatusNow == "AS")))
                            {

                                NewBISSvcRef.SMS_MESSAGE_Collection smsMsgColl = new NewBISSvcRef.SMS_MESSAGE_Collection();
                                NewBISSvcRef.SMS_DATA objData = new NewBISSvcRef.SMS_DATA();
                                NewBISSvcRef.ProcessResult prOD = new NewBISSvcRef.ProcessResult();
                                using (NewBISSvcRef.NewBISSvcClient clientOD = new NewBISSvcRef.NewBISSvcClient())
                                {
                                    String planID = cmbPaidPlBlock.SelectedValue.ToString() + txtPaidPlcode.Text.Trim() + txtPaidPlcode2.Text.Trim();

                                    objData = clientOD.GetSmsData(out prOD, txtSaleAgent.Text.Trim(), workGroup, planID);

                                    if (prOD.Successed == false)
                                    {
                                        throw new Exception(prOD.ErrorMessage + "อยู่ในส่วนของ ฟังก์ชั่น Get ข้อมูลตัวแทน ชื่อฟังก์ขั่น GetSmsData");
                                    }
                                }

                                if (objData.MARKETING_TYPE != "WEB")
                                {

                                    if (objData != null && !String.IsNullOrEmpty(objData.AGENTCODE))
                                    {
                                        if (!workGroup.Equals("TEL"))
                                        {
                                            // objData.MOBILE_PHONE = "0859818332"; //hardcode
                                            if (!String.IsNullOrEmpty(objData.MOBILE_PHONE))
                                            {
                                                // throw new Exception("ไม่สามารถส่ง SMS ได้เนื่องจากตัวแทนไม่มีหมายเลขโทรศัพท์มือถือ");

                                                #region " Get ข้อความ"

                                                String msg = "";

                                                #region "สถานะ AP"
                                                if (statusNow == "AP")
                                                {
                                                    Decimal lackPremium = 0;
                                                    lackPremium = txtOverPremium.Text.Trim() == "" ? 0 : Convert.ToDecimal(txtOverPremium.Text.Trim().Replace(",", ""));
                                                    if (lackPremium < 0)
                                                    {
                                                        msg = "รอชำระเบี้ยจำนวน " + (lackPremium * -1).ToString("n0") + " บาท";
                                                    }
                                                }
                                                #endregion

                                                #region "สถานะ CO"
                                                if (statusNow == "CO")
                                                {
                                                    try
                                                    {
                                                        String msgLackPremium = "";
                                                        Decimal lackPremium = 0;
                                                        lackPremium = txtOverPremium.Text.Trim() == "" ? 0 : Convert.ToDecimal(txtOverPremium.Text.Trim().Replace(",", "").Replace("+", "").Replace("-", ""));
                                                        if (lackPremium < 0)
                                                        {
                                                            msgLackPremium = "/ชำระเบี้ยเพิ่มเติม" + (lackPremium * -1).ToString("n0") + "บาท";
                                                        }

                                                        //Decimal efRate = txtEFPremium.Text == "" ? 0 : Convert.ToDecimal(txtEFPremium.Text.Trim().Replace(",", ""));
                                                        //Decimal emRate = txtEMPremium.Text == "" ? 0 : Convert.ToDecimal(txtEMPremium.Text.Trim().Replace(",", ""));
                                                        //Decimal xocpRate = txtXocpPremium.Text == "" ? 0 : Convert.ToDecimal(txtXocpPremium.Text.Trim().Replace(",", ""));

                                                        //if (efRate > 0 || emRate > 0)
                                                        //{
                                                        //    msg = "เพิ่มเบี้ยพิเศษจากสุขภาพ" + msgLackPremium;
                                                        //}
                                                        //else if (xocpRate > 0)
                                                        //{
                                                        //    msg = "เพิ่มเบี้ยพิเศษจากอาชีพ" + msgLackPremium;
                                                        //}
                                                        //else
                                                        //{
                                                        //    msg = "เสนอเงื่อนไขใหม่" + msgLackPremium;
                                                        //}
                                                    }
                                                    catch (Exception e)
                                                    {
                                                        throw new Exception(e.Message + " : txtOverPremium:" + txtOverPremium.Text);// " : txtEFPremium:" + txtEFPremium.Text + "  txtXocpPremium :" + txtXocpPremium.Text);
                                                    }

                                                }
                                                #endregion

                                                #region "สถานะ MO,MD"
                                                List<String> smsMoList = new List<String>();

                                                if (statusNow == "MO" || statusNow == "MD")
                                                {
                                                    NewBISSvcRef.ProcessResult prMo = new NewBISSvcRef.ProcessResult();
                                                    NewBISSvcRef.SMS_MEMO_STATUS_COLLECTION smsMemoColl = new NewBISSvcRef.SMS_MEMO_STATUS_COLLECTION();
                                                    using (NewBISSvcRef.NewBISSvcClient clientMo = new NewBISSvcRef.NewBISSvcClient())
                                                    {
                                                        smsMemoColl = clientMo.GetSmsMemoStatus(out prMo, txtAppNo.Text.Trim(), statusNow);
                                                        if (prMo.Successed == false)
                                                        {
                                                            throw new Exception(prMo.ErrorMessage + " ไม่สามารถ Get ค่าของ Memoได้ ที่ฟังก์ชั่น ชื่อ GetSmsMemoStatus");
                                                        }
                                                    }

                                                    if (smsMemoColl != null && smsMemoColl.Count() > 0)
                                                    {
                                                        String smsMo = "";
                                                        int totalMemo = smsMemoColl.Count();
                                                        int i = 0;
                                                        int smsNo = 0;
                                                        foreach (NewBISSvcRef.SMS_MEMO_STATUS smsMemoObj in smsMemoColl)
                                                        {
                                                            i = i + 1;
                                                            smsNo = smsNo + 1;

                                                            if (smsNo == 1)
                                                            {
                                                                smsMo = smsMo + smsMemoObj.SMS_DESC;
                                                                if (i == totalMemo)
                                                                {
                                                                    smsMoList.Add(smsMo);
                                                                }
                                                            }

                                                            if (smsNo == 2)
                                                            {
                                                                if (String.IsNullOrEmpty(smsMemoObj.SMS_DESC))
                                                                {
                                                                    smsMoList.Add(smsMo);
                                                                }
                                                                else
                                                                {
                                                                    smsMo = smsMo + "/" + smsMemoObj.SMS_DESC;
                                                                    smsMoList.Add(smsMo);
                                                                    smsMo = "";
                                                                    smsNo = 0;
                                                                }
                                                            }
                                                        }
                                                    }

                                                }
                                                #endregion

                                                #region "สถานะ IF"
                                                if (statusNow == "IF")
                                                {
                                                    //if (ChannelType == "GM" || ChannelType == "HL")
                                                    //{
                                                    //    msg = "ออกกรมธรรม์เลขที่ " + txtPolicyHolder.Text + "/" + policyIF;
                                                    //}
                                                    //else
                                                    //{
                                                    msg = "ออกกรมธรรม์เลขที่ " + policyIF;
                                                    //}
                                                }
                                                #endregion

                                                #endregion

                                                #region " SET ข้อความ SMS ส่งงาน MarketType ที่ไม่ใช่ BNC"

                                                if (objData.MARKETING_TYPE == "AGT")
                                                {
                                                    if (statusNow == "AP" || statusNow == "CO" || statusNow == "IF")
                                                    {
                                                        if (!String.IsNullOrEmpty(msg))
                                                        {
                                                            if (objData.UNIT == "N")
                                                            {
                                                                if (!String.IsNullOrEmpty(objData.MOBILE_PHONE))
                                                                {
                                                                    NewBISSvcRef.SMS_MESSAGE smsMsgObj1 = new NewBISSvcRef.SMS_MESSAGE();
                                                                    smsMsgObj1.MB_PHONE = objData.MOBILE_PHONE;
                                                                    smsMsgObj1.MSG_SMS = "ใบคำขอ" + txtAppNo.Text.Trim() + customerName + msg;
                                                                    smsMsgObj1.DATA_TYPE = "A";
                                                                    smsMsgColl.Add(smsMsgObj1);
                                                                }

                                                                if (!String.IsNullOrEmpty(objData.UPLINE_MOBILE_PHONE))
                                                                {
                                                                    NewBISSvcRef.SMS_MESSAGE smsMsgObj2 = new NewBISSvcRef.SMS_MESSAGE();
                                                                    smsMsgObj2.MB_PHONE = objData.UPLINE_MOBILE_PHONE;
                                                                    smsMsgObj2.MSG_SMS = "(ตท." + objData.NAME + " " + (string.IsNullOrEmpty(objData.SURNAME) ? "" : objData.SURNAME.Substring(0, 1)) + ".)ใบคำขอ" + txtAppNo.Text.Trim() + customerName + msg;
                                                                    smsMsgObj2.DATA_TYPE = "A";
                                                                    smsMsgColl.Add(smsMsgObj2);
                                                                }

                                                            }
                                                            else if (objData.UNIT == "Y")
                                                            {
                                                                if (!String.IsNullOrEmpty(objData.MOBILE_PHONE))
                                                                {
                                                                    NewBISSvcRef.SMS_MESSAGE smsMsgObj1 = new NewBISSvcRef.SMS_MESSAGE();
                                                                    smsMsgObj1.MB_PHONE = objData.MOBILE_PHONE;
                                                                    smsMsgObj1.MSG_SMS = "ใบคำขอ" + txtAppNo.Text.Trim() + customerName + msg;
                                                                    smsMsgObj1.DATA_TYPE = "A";
                                                                    smsMsgColl.Add(smsMsgObj1);
                                                                }
                                                            }

                                                        }
                                                    }
                                                    else if (statusNow == "MO" || statusNow == "MD")
                                                    {
                                                        if (smsMoList != null && smsMoList.Count() > 0)
                                                        {
                                                            foreach (String msgMo in smsMoList)
                                                            {
                                                                if (objData.UNIT == "N")
                                                                {
                                                                    if (!String.IsNullOrEmpty(objData.MOBILE_PHONE))
                                                                    {
                                                                        NewBISSvcRef.SMS_MESSAGE smsMsgObj1 = new NewBISSvcRef.SMS_MESSAGE();
                                                                        smsMsgObj1.MB_PHONE = objData.MOBILE_PHONE;
                                                                        smsMsgObj1.MSG_SMS = "ใบคำขอ" + txtAppNo.Text.Trim() + customerName + msgMo;
                                                                        smsMsgObj1.DATA_TYPE = "A";
                                                                        smsMsgColl.Add(smsMsgObj1);
                                                                    }

                                                                    if (!String.IsNullOrEmpty(objData.UPLINE_MOBILE_PHONE))
                                                                    {
                                                                        NewBISSvcRef.SMS_MESSAGE smsMsgObj2 = new NewBISSvcRef.SMS_MESSAGE();
                                                                        smsMsgObj2.MB_PHONE = objData.UPLINE_MOBILE_PHONE;
                                                                        smsMsgObj2.MSG_SMS = "(ตท." + objData.NAME + " " + (string.IsNullOrEmpty(objData.SURNAME) ? "" : objData.SURNAME.Substring(0, 1)) + ".)ใบคำขอ" + txtAppNo.Text.Trim() + customerName + msgMo;
                                                                        smsMsgObj2.DATA_TYPE = "A";
                                                                        smsMsgColl.Add(smsMsgObj2);
                                                                    }

                                                                }
                                                                else if (objData.UNIT == "Y")
                                                                {
                                                                    if (!String.IsNullOrEmpty(objData.MOBILE_PHONE))
                                                                    {
                                                                        NewBISSvcRef.SMS_MESSAGE smsMsgObj1 = new NewBISSvcRef.SMS_MESSAGE();
                                                                        smsMsgObj1.MB_PHONE = objData.MOBILE_PHONE;
                                                                        smsMsgObj1.MSG_SMS = "ใบคำขอ" + txtAppNo.Text.Trim() + customerName + msgMo;
                                                                        smsMsgObj1.DATA_TYPE = "A";
                                                                        smsMsgColl.Add(smsMsgObj1);
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                                #endregion

                                                #region " SET ข้อความ SMS ส่งงาน MarketType ที่เป็น BNC"
                                                if (objData.MARKETING_TYPE == "BNC")
                                                {
                                                    if (statusNow == "AP" || statusNow == "CO" || statusNow == "IF")
                                                    {
                                                        if (!String.IsNullOrEmpty(msg))
                                                        {
                                                            if (!String.IsNullOrEmpty(objData.MOBILE_PHONE))
                                                            {
                                                                NewBISSvcRef.SMS_MESSAGE smsMsgObj1 = new NewBISSvcRef.SMS_MESSAGE();
                                                                smsMsgObj1.MB_PHONE = objData.MOBILE_PHONE;
                                                                smsMsgObj1.MSG_SMS = "สาขา" + objData.BRANCH_CODE + " ใบคำขอ" + txtAppNo.Text.Trim() + customerName + msg;
                                                                smsMsgObj1.DATA_TYPE = "A";
                                                                smsMsgColl.Add(smsMsgObj1);
                                                            }
                                                        }
                                                    }
                                                    else if (statusNow == "MO" || statusNow == "MD")
                                                    {
                                                        if (smsMoList != null && smsMoList.Count() > 0)
                                                        {
                                                            foreach (String msgMo in smsMoList)
                                                            {
                                                                if (!String.IsNullOrEmpty(objData.MOBILE_PHONE))
                                                                {
                                                                    NewBISSvcRef.SMS_MESSAGE smsMsgObj1 = new NewBISSvcRef.SMS_MESSAGE();
                                                                    smsMsgObj1.MB_PHONE = objData.MOBILE_PHONE;
                                                                    smsMsgObj1.MSG_SMS = "สาขา" + objData.BRANCH_CODE + " ใบคำขอ" + txtAppNo.Text.Trim() + customerName + msgMo;
                                                                    smsMsgObj1.DATA_TYPE = "A";
                                                                    smsMsgColl.Add(smsMsgObj1);
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                                #endregion
                                            }
                                        }

                                        #region "เก็บค่าส่ง SMS ให้ลูกค้ากรณีออก IF ได้ทุก MarketType"
                                        if (statusNow == "IF")
                                        {
                                            if (txtMbPhone.Text.Trim() != "")
                                            {
                                                NewBISSvcRef.SMS_MESSAGE smsMsgCustomer = new NewBISSvcRef.SMS_MESSAGE();
                                                smsMsgCustomer.MB_PHONE = txtMbPhone.Text.Trim();
                                                //if (ChannelType == "GM" || ChannelType == "HL")
                                                //{
                                                //    smsMsgCustomer.MSG_SMS = "กรุงเทพประกันชีวิตได้ให้ความคุ้มครองท่านแล้วตั้งแต่วันที่ " + txtPlanIsuDate.Text + " และจะจัดส่งกรมธรรม์เลขที่ " + txtPolicyHolder.Text + "/" + policyIF + " ให้ท่านต่อไป";
                                                //}
                                                // else
                                                //{
                                                smsMsgCustomer.MSG_SMS = "กรุงเทพประกันชีวิตได้ให้ความคุ้มครองท่านแล้วตั้งแต่วันที่ " + txtPaidIsuDate.Text + " และจะจัดส่งกรมธรรม์เลขที่ " + policyIF + " ให้ท่านต่อไป";
                                                //}
                                                smsMsgCustomer.DATA_TYPE = "C";
                                                smsMsgColl.Add(smsMsgCustomer);
                                            }
                                            else
                                            {
                                                //  MessageBox.Show("ไม่สามารถส่ง SMS ได้เนื่องจากไม่มีเบอร์มือถือลูกค้า");
                                                // throw new Exception("ไม่สามารถส่ง SMS ได้เนื่องจากไม่มีเบอร์มือถือลูกค้า");
                                            }
                                        }
                                        #endregion


                                        if (smsMsgColl != null && smsMsgColl.Count() > 0)
                                        {
                                            var isSendMessageSuccess = false;

                                            #region "ส่ง SMS ของอั๋นน้อย"
                                            using (smsSvcRef.Sms_SvcSoapClient smsClient = new smsSvcRef.Sms_SvcSoapClient())
                                            {
                                                foreach (NewBISSvcRef.SMS_MESSAGE obj in smsMsgColl)
                                                {

                                                    try
                                                    {
                                                        var smsService = new SMSProvideService();
                                                        var workCode = smsService.GetSMSWorkCode(workGroup);
                                                        var mobilephone = obj.MB_PHONE;
                                                        var referenceCode = SystemConstant.SMSReferanceByAppNo;
                                                        var referenceNo = PAR_PA_APPLICATION_ID.APP_NO;
                                                        string receiveCode = "", receiveNo = "";
                                                        if (obj.DATA_TYPE == SystemConstant.SMSDataTypeForCustomer)
                                                        {
                                                            receiveCode = SystemConstant.SMSServiceReceiveCode_Customer;
                                                            receiveNo = txtIdcardNo.Text.Trim();
                                                        }
                                                        else if (obj.DATA_TYPE == SystemConstant.SMSDataTypeForAgent)
                                                        {
                                                            receiveCode = SystemConstant.SMSServiceReceiveCode_Agent;
                                                            receiveNo = txtSaleAgent.Text;
                                                        }

                                                        if (workGroup == "BNC")
                                                        {
                                                            mobilephone = "0894785545"; // พี่ตู่
                                                        }
                                                        else
                                                        {
                                                            mobilephone = "0984156692";  // 0984156692 พี่ตา
                                                        }
                                                        smsService.SendSMSV2(obj.MSG_SMS, mobilephone, workCode, referenceCode, referenceNo, receiveCode, receiveNo, _UserID);

                                                    }
                                                    catch (Exception e)
                                                    {
                                                        MessageBox.Show("ไม่สามารถส่ง Message ได้เนื่องจาก " + " : " + e.Message);
                                                        isSendMessageSuccess = false;
                                                        NewBISSvcRef.ProcessResult prError = new NewBISSvcRef.ProcessResult();
                                                        NewBISSvcRef.U_SEND_SMS_ERROR uSendSmsErrorObj = new NewBISSvcRef.U_SEND_SMS_ERROR();
                                                        using (NewBISSvcRef.NewBISSvcClient clientErr = new NewBISSvcRef.NewBISSvcClient())
                                                        {
                                                            uSendSmsErrorObj.U_SENDSMS_ERR_ID = null;
                                                            uSendSmsErrorObj.APP_NO = txtAppNo.Text.Trim();
                                                            uSendSmsErrorObj.APP_REFERENCE_TYPE = 2;
                                                            uSendSmsErrorObj.STATUS = statusNow;
                                                            uSendSmsErrorObj.SUBSTATUS = subStatusNow;
                                                            uSendSmsErrorObj.ZTB_SMS_TYPE_ID = 5;
                                                            uSendSmsErrorObj.MESSAGE = obj.MSG_SMS;
                                                            uSendSmsErrorObj.SEND_DT = DateTime.Now;
                                                            if (obj.DATA_TYPE == "A")
                                                            {

                                                                uSendSmsErrorObj.AGENT_RECIPIENT_REF = objData.AGENTCODE;
                                                                uSendSmsErrorObj.AGENT_RECIPIENT_TYPE = 5;
                                                                uSendSmsErrorObj.AGENT_MOBILE_NO = obj.MB_PHONE;
                                                                uSendSmsErrorObj.AGENT_NAME = objData.NAME;
                                                                uSendSmsErrorObj.AGENT_SURNAME = objData.SURNAME;
                                                            }

                                                            if (obj.DATA_TYPE == "C")
                                                            {

                                                                var cusInfo = (from getData in PAR_PA_APPLICATION_ID.APPLICATION_Collection.First().NAME_ID_Collection
                                                                               where getData.CUSTOMER_TYPE == "C"
                                                                               select getData).First();
                                                                uSendSmsErrorObj.CUSTOMER_RECIPIENT_TYPE = 4;
                                                                uSendSmsErrorObj.CUSTOMER_RECIPIENT_REF = cusInfo.IDCARD_NO;
                                                                uSendSmsErrorObj.CUSTOMER_NAME = cusInfo.NAME;
                                                                uSendSmsErrorObj.CUSTOMER_SURNAME = cusInfo.SURNAME;
                                                                uSendSmsErrorObj.CUSTOMER_IDCARD_NO = cusInfo.IDCARD_NO;
                                                                uSendSmsErrorObj.CUSTOMER_BIRTH_DT = cusInfo.BIRTH_DT;
                                                            }

                                                            uSendSmsErrorObj.ERROR_MESSAGE = "ไม่สามารถส่ง Message ได้เนื่องจาก " + " : " + e.Message;
                                                            uSendSmsErrorObj.SEND_FLG = 'N';
                                                            uSendSmsErrorObj.SEND_FLG_DATE = null;
                                                            uSendSmsErrorObj.SEND_AMOUNT = 1;
                                                            uSendSmsErrorObj.AGENT_MOBILE_NO = objData.MOBILE_PHONE;
                                                            clientErr.AddU_SEND_SMS_ERROR(ref uSendSmsErrorObj);
                                                            //throw new Exception("ไม่สามารถส่ง SMS " + sms.ERR_CODE);

                                                        }
                                                    }
                                                }

                                            }
                                            #endregion

                                            if (isSendMessageSuccess)
                                            {
                                                IsSendSMS.Checked = true;
                                                IsSendSMS.Enabled = false;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        // throw new Exception("ไม่สามารถส่ง SMS ได้เนื่องจากไม่มีข้อมูลตัวแทน");
                                    }
                                }

                            }
                        }



                    }

                    catch (Exception ex)
                    {
                        LetterSvcRef.LetterServiceClient c = new LetterSvcRef.LetterServiceClient();
                        string err = "ไม่สามารถส่ง SMS ได้ เลขที่ใบคำขอ " + txtAppNo.Text + " มีสถานะเป็น " + cmbSummryStatus.SelectedValue.ToString() + " มีข้อผิดพลาด คือ " + ex.Message;
                        sendMailSMS(err);
                        var statusNow = cmbSummryStatus.SelectedValue == null || cmbSummryStatus.SelectedValue.ToString() == "" ? "WT" : cmbSummryStatus.SelectedValue.ToString();
                        var subStatusNow = cmbSummrySubStatus.SelectedValue == null || cmbSummrySubStatus.SelectedValue.ToString() == "" ? "WT" : cmbSummrySubStatus.SelectedValue.ToString();
                        using (NewBISSvcRef.NewBISSvcClient clientErr = new NewBISSvcRef.NewBISSvcClient())
                        {
                            NewBISSvcRef.ProcessResult prError = new NewBISSvcRef.ProcessResult();
                            NewBISSvcRef.U_SEND_SMS_ERROR uSendSmsErrorObj = new NewBISSvcRef.U_SEND_SMS_ERROR();
                            uSendSmsErrorObj.U_SENDSMS_ERR_ID = null;
                            uSendSmsErrorObj.APP_NO = txtAppNo.Text.Trim();
                            uSendSmsErrorObj.STATUS = summaryStatusAfterSave;
                            uSendSmsErrorObj.SUBSTATUS = summarySubStatusAfterSave;
                            uSendSmsErrorObj.MESSAGE = ex.Message;
                            clientErr.AddU_SEND_SMS_ERROR(ref uSendSmsErrorObj);
                        }
                    }

                    #endregion

                }
                if (summaryStatusAfterSave == "IF")
                {
                    string msg = "\nบันทึกข้อมูลเรียบร้อยแล้วครับ\nใบคำข้อเลขที่ : " + txtAppNo.Text.Trim() + " " + "ได้ออกเป็นกรมธรรม์เรียบร้อยแล้ว\n" + "เลขที่กรมธรรม์ : " + txtCertNo.Text + "\n" + "วันที่ใบเสร็จ : " + txtPolicyDt.Text.Trim();
                    MessageBox.Show(msg);
                }
                else
                {
                    MessageBox.Show("บันทึกข้อมูลเรียบร้อยแล้วครับ");
                }

                if (FORMAPP == "SELECT")
                {
                    this.Hide();
                    ApplicationSelectorForm.Show();
                }
                else
                {
                    btnHeadClear.PerformClick();


                }
            }
        }

        private void sendMailSMS(String errorMsg)
        {
            try
            {
                LetterSvcRef.LetterServiceClient x = new LetterSvcRef.LetterServiceClient();
                LetterSvcRef.Mail mail = new LetterSvcRef.Mail();
                //LetterSvcRef prMail = new LetterSvcRef.ProcessResult();

                mail.From = "system@bangkoklife.com";
                mail.Subject = "แจ้งความผิดพลาดกรณีไม่สามารถ SMS ได้";
                string[] emails = { "naratip.cha@bla.co.th" };
                //mail.IsBodyHtml = true;
                //mail.Encoding = MailEncoding.Windows874;
                mail.To = emails;
                mail.AttachFileCollection = null;
                mail.Body = errorMsg;
                var prMail = x.SendMail(mail);
            }
            catch (Exception)
            {

            }

        }
        private void CheckIsuDateBeforeSave()
        {
            if (PAR_W_UNDERWRITE_ID_COLL != null && PAR_W_UNDERWRITE_ID_COLL.Count() > 0)
            {
                if (PAR_W_UNDERWRITE_ID_COLL[0].SUBUNDERWRITE_ID_Collection != null && PAR_W_UNDERWRITE_ID_COLL[0].SUBUNDERWRITE_ID_Collection.Count() > 0)
                {
                    NewBisPASvcRef.W_SUMMARY wSummaryObj = new NewBisPASvcRef.W_SUMMARY();
                    wSummaryObj = PAR_W_UNDERWRITE_ID_COLL[0].SUBUNDERWRITE_ID_Collection[0].SUMMARY_Collection[0];
                    String StatusCode = "";
                    StatusCode = wSummaryObj.STATUS;
                    if (StatusCode == "IF")
                    {
                        #region "ตรวจสอบข้อมูลลูกค้าก่อนออก IF"
                        if (cmbDataCus.SelectedValue.ToString() == "1")
                        {
                            if (NAME_ID == null)
                            {
                                throw new Exception("เลือกข้อมูลลูกค้าเป็นข้อมูลเก่าแต่ไม่พบข้อมูล NAME_ID");
                            }
                            else
                            {
                                if (PAR_P_NAME_ID_COLL != null && PAR_P_NAME_ID_COLL.Count() > 0)
                                {
                                    if (NAME_ID != null)
                                    {
                                        NewBisPASvcRef.P_NAME_ID oldPNameIDObj = new NewBisPASvcRef.P_NAME_ID();
                                        var GetData = from getData in PAR_P_NAME_ID_COLL
                                                      where getData.NAME_ID == NAME_ID
                                                      select getData;
                                        if (GetData != null && GetData.Count() > 0)
                                        {
                                            oldPNameIDObj = GetData.ToArray()[0];

                                            String idCardNoOld = "";
                                            String sexOld = "";
                                            String birthDateOld = "";

                                            if (cmbCardType.SelectedValue.ToString() == "1")
                                            {
                                                idCardNoOld = oldPNameIDObj.IDCARD_NO == null ? "" : oldPNameIDObj.IDCARD_NO;
                                            }
                                            else if (cmbCardType.SelectedValue.ToString() == "2")
                                            {
                                                idCardNoOld = oldPNameIDObj.PASSPORT == null ? "" : oldPNameIDObj.PASSPORT;
                                            }

                                            sexOld = oldPNameIDObj.SEX == null ? "" : oldPNameIDObj.SEX;
                                            birthDateOld = oldPNameIDObj.BIRTH_DT == null ? "" : Utility.dateTimeToString(oldPNameIDObj.BIRTH_DT.Value, "dd/MM/yyyy", "BU");

                                            if (idCardNoOld != "")
                                            {
                                                if (txtIdcardNo.Text.Trim() != idCardNoOld.Trim())
                                                {
                                                    throw new Exception("เลขที่บัตรประชาชนของลูกค้าไม่ตรงกับเลขที่บัตรประชาชนเก่าของลูกค้า เลขที่เก่าคือ " + idCardNoOld.Trim());
                                                }
                                            }

                                            if (cmbSex.SelectedValue.ToString() != sexOld)
                                            {
                                                throw new Exception("เพศลูกค้าไม่ตรงกับเพศเก่าของลูกค้า");
                                            }

                                            if (txtBirthDt.Text.Trim() != birthDateOld)
                                            {
                                                throw new Exception("วันเกิดลูกค้าไม่ตรงกับวันเกิดเก่าของลูกค้า");
                                            }

                                            String prename = oldPNameIDObj.PRENAME == null ? "" : oldPNameIDObj.PRENAME;
                                            String name = oldPNameIDObj.NAME == null ? "" : oldPNameIDObj.NAME;
                                            String surname = oldPNameIDObj.SURNAME == null ? "" : oldPNameIDObj.SURNAME;
                                            String oldName = prename.Trim() + name.Trim() + surname.Trim();
                                            String newName = txtPrename.Text.Trim() + txtName.Text.Trim() + txtSurname.Text.Trim();

                                            if (oldName != newName)
                                            {
                                                var result = MessageBox.Show("ข้อมูลชื่อนามสกุลของลูกค้าไม่ตรงกับข้อมูลเดิม ถ้าต้องการแก้ไขและสลักหลังข้อมูลลูกค้าอัตโนมัติให้กดปุ่ม YES ถ้าไม่กดปุ่ม NO " + " \n " + "ข้อมูลเดิม : " + prename + " " + name + "  " + surname + " \n " + "ข้อมูลใหม่ : " + txtPrename.Text.Trim() + " " + txtName.Text.Trim() + "  " + txtSurname.Text.Trim() + ")", "คำเตือน", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                                                if (result == DialogResult.No)
                                                {
                                                    throw new Exception("ท่านเลือกปุ่ม NO จึงไม่สามารถบันทึกข้อมูลได้เนื่องจากข้อมูลเดิมลูกค้าไม่ตรงกับข้อมูลใหม่");
                                                }
                                            }
                                        }
                                        else
                                        {
                                            throw new Exception("ไม่พบข้อมูลลูกค้าเก่าของลูกค้า");
                                        }
                                    }
                                    else
                                    {
                                        throw new Exception("ข้อมูลลูกค้าเป็นลูกค้าเก่าของบริษัท แต่ท่านไม่เลือกลูกค้าเป็นลูกค้าเก่า");
                                    }
                                }
                            }
                        }
                        #endregion
                    }

                    if (StatusCode == "IF" || StatusCode == "AP" || StatusCode == "CO")
                    {
                        if (PAR_PA_APPLICATION_ID != null)
                        {
                            if (PAR_PA_APPLICATION_ID.APPLICATION_Collection != null && PAR_PA_APPLICATION_ID.APPLICATION_Collection.Count() > 0)
                            {
                                foreach (NewBisPASvcRef.U_APPLICATION uApplication in PAR_PA_APPLICATION_ID.APPLICATION_Collection)
                                {
                                    if (uApplication.LIFE_ID != null && (!String.IsNullOrEmpty(uApplication.LIFE_ID.PL_CODE)))
                                    {
                                        NewBisPASvcRef.U_LIFE_ID uLifeID = new NewBisPASvcRef.U_LIFE_ID();
                                        uLifeID = uApplication.LIFE_ID;
                                        if (uLifeID.ISU_DT != null)
                                        {
                                            String channelType = cmbChannelType.SelectedValue.ToString();
                                            String workGroup = cmbWorkGroup.SelectedValue.ToString();
                                            DateTime isuDateVal = uLifeID.ISU_DT.Value;
                                            int amountDay = 0;
                                            amountDay = (DateTime.Now - isuDateVal).Days;

                                            var channelTypeList = new string[] { "PO", "PN" };
                                            if (amountDay < 0 && !channelTypeList.Contains(channelType))
                                            {
                                                txtName.Focus();
                                                throw new Exception("วันเริ่มคุ้มครองมากกว่าวันที่ปัจจุบัน");
                                            }
                                            else if (channelTypeList.Contains(channelType))
                                            {
                                                if (amountDay < -30)
                                                {
                                                    txtName.Focus();
                                                    throw new Exception("วันเริ่มคุ้มครองล่วงหน้าเกิน 30 วัน");
                                                }
                                            }

                                            if (workGroup == "BNC")
                                            {
                                                if (amountDay > 90 && uLifeID.FREE_FLG != "Y")
                                                {
                                                    txtName.Focus();
                                                    throw new Exception("วันเริ่มคุ้มครองย้อนหลังเกิน 90 วัน");
                                                }
                                            }
                                            else
                                            {
                                                if (!(channelType.Contains(channelType)))
                                                {
                                                    if (amountDay > 30)
                                                    {
                                                        txtName.Focus();
                                                        throw new Exception("วันเริ่มคุ้มครองย้อนหลังเกิน 30 วัน");
                                                    }
                                                }
                                            }
                                        }
                                    }

                                }
                            }
                        }
                    }
                }
            }
        }
        private void SendStatusToIsis()
        {
            if (PAR_PA_APPLICATION_ID.APP_ISIS != null && PAR_PA_APPLICATION_ID.APP_ISIS.APP_ID != null && (!string.IsNullOrEmpty(PAR_PA_APPLICATION_ID.APP_ISIS.GUID)))
            {
                if (PAR_W_UNDERWRITE_ID_COLL != null && PAR_W_UNDERWRITE_ID_COLL.Count() > 0)
                {
                    if (PAR_W_UNDERWRITE_ID_COLL[0].SUBUNDERWRITE_ID_Collection != null && PAR_W_UNDERWRITE_ID_COLL[0].SUBUNDERWRITE_ID_Collection.Count() > 0)
                    {
                        NewBisPASvcRef.W_SUMMARY wSummaryObj = new NewBisPASvcRef.W_SUMMARY();
                        wSummaryObj = PAR_W_UNDERWRITE_ID_COLL[0].SUBUNDERWRITE_ID_Collection[0].SUMMARY_Collection[0];

                        bool sendIsis = false;

                        if (ClickTabUnderWriteMain == true)
                        {
                            if (PAR_W_SUMMARY != null && PAR_W_SUMMARY.SUMMARY_ID != null)
                            {
                                String oldStatus = PAR_W_SUMMARY.STATUS + PAR_W_SUMMARY.SUBSTATUS;
                                String newStatus = wSummaryObj.STATUS + wSummaryObj.SUBSTATUS;
                                if (oldStatus != newStatus)
                                {
                                    sendIsis = true;
                                }
                                else
                                {
                                    if (wSummaryObj.STATUS == "MO" || wSummaryObj.STATUS == "MD")
                                    {
                                        if (PAR_U_MEMO_ID_COLL_TMP != null && PAR_U_MEMO_ID_COLL_TMP.Count() > 0)
                                        {
                                            foreach (NewBisPASvcRef.U_MEMO_ID obj in PAR_U_MEMO_ID_COLL_TMP)
                                            {
                                                if (!(obj.U_LETTER_ID != null && obj.U_LETTER_ID.ULETTER_ID != null))
                                                {
                                                    sendIsis = true;
                                                    break;
                                                }
                                            }
                                        }
                                    }

                                }
                            }
                            else
                            {
                                sendIsis = true;
                            }
                        }

                        if (sendIsis == true)
                        {
                            if (wSummaryObj.STATUS != "MO" && wSummaryObj.STATUS != "MD")
                            {
                                String detail = "";
                                detail = "สถานะเดิม: " + PAR_W_SUMMARY.STATUS + " สถานะย่อยเดิม: " + PAR_W_SUMMARY.SUBSTATUS + " สถานะใหม่: " + wSummaryObj.STATUS + "  สถานะย่อยใหม่: " + wSummaryObj.SUBSTATUS;
                                //using (IsisAppTransportService.AppTransportServiceContractClient clientIsis = new IsisAppTransportService.AppTransportServiceContractClient())
                                //{
                                //    clientIsis.ChangeAppFormStatus(PAR_PA_APPLICATION_ID.APP_ISIS.GUID, wSummaryObj.STATUS, wSummaryObj.SUBSTATUS, detail, DateTime.Now, 0, 0);
                                //}
                                using (ReceiptSvcRef.ServiceClient clientIsis = new ReceiptSvcRef.ServiceClient())
                                {
                                    clientIsis.SendStatusToIsis(txtAppNo.Text, PAR_PA_APPLICATION_ID.APP_ISIS.GUID, wSummaryObj.STATUS, wSummaryObj.SUBSTATUS, detail, DateTime.Now, 0, 0);
                                }
                            }
                            else
                            {
                                if (PAR_U_MEMO_ID_COLL_TMP != null && PAR_U_MEMO_ID_COLL_TMP.Count() > 0)
                                {
                                    NewBISSvcRef.ProcessResult pr1 = new NewBISSvcRef.ProcessResult();
                                    using (NewBISSvcRef.NewBISSvcClient client = new NewBISSvcRef.NewBISSvcClient())
                                    {
                                        foreach (NewBisPASvcRef.U_MEMO_ID obj in PAR_U_MEMO_ID_COLL_TMP)
                                        {
                                            if (!(obj.U_LETTER_ID != null && obj.U_LETTER_ID.ULETTER_ID != null))
                                            {
                                                var GetData = from getData in wSummaryObj.MEMO_ID_Collection
                                                              where
                                                              getData.TMN == 'N'
                                                              && getData.MEMO_TRN_DT.Value.Day == obj.MEMO_TRN_DT.Value.Day
                                                              && getData.MEMO_TRN_DT.Value.Month == obj.MEMO_TRN_DT.Value.Month
                                                              && getData.MEMO_TRN_DT.Value.Year == obj.MEMO_TRN_DT.Value.Year
                                                              && getData.MEMO_TRN_DT.Value.Hour == obj.MEMO_TRN_DT.Value.Hour
                                                              && getData.MEMO_TRN_DT.Value.Minute == obj.MEMO_TRN_DT.Value.Minute
                                                              && getData.MEMO_TRN_DT.Value.Second == obj.MEMO_TRN_DT.Value.Second
                                                              select getData;

                                                if (GetData != null && GetData.Count() > 0)
                                                {
                                                    NewBisPASvcRef.U_LETTER_ID uLetterIDObj = new NewBisPASvcRef.U_LETTER_ID();
                                                    uLetterIDObj = GetData.First().U_LETTER_ID;

                                                    String guID = "";
                                                    int gracePeriod = 0;
                                                    long? isisDocID = null;
                                                    double? diffDate = null;
                                                    String detail = "";
                                                    pr1 = client.RequestDocFromISIS(out guID, out isisDocID, out detail, PAR_PA_APPLICATION_ID.APP_ID.Value, wSummaryObj.STATUS, wSummaryObj.SUBSTATUS, obj.ANSWER_LIMIT_DT.Value, uLetterIDObj.STATUS_DT.Value, obj.UPD_ID);
                                                    if (pr1.Successed == false)
                                                    {
                                                        throw new Exception(pr1.ErrorMessage);
                                                    }

                                                    if (guID != null && guID != "")
                                                    {
                                                        try
                                                        {
                                                            diffDate = Utility.differenceDay(DateTime.Now, obj.ANSWER_LIMIT_DT);
                                                            gracePeriod = diffDate == null ? 0 : Convert.ToInt16(Math.Ceiling(diffDate.Value));
                                                            //using (IsisAppTransportService.AppTransportServiceContractClient clientIsis = new IsisAppTransportService.AppTransportServiceContractClient())
                                                            //{
                                                            //    clientIsis.ChangeAppFormStatus(guID, wSummaryObj.STATUS, wSummaryObj.SUBSTATUS, detail, DateTime.Now, gracePeriod, isisDocID.Value);

                                                            //}
                                                            using (ReceiptSvcRef.ServiceClient clientIsis = new ReceiptSvcRef.ServiceClient())
                                                            {
                                                                clientIsis.SendStatusToIsis(txtAppNo.Text, guID, wSummaryObj.STATUS, wSummaryObj.SUBSTATUS, detail, DateTime.Now, gracePeriod, isisDocID.Value);
                                                            }
                                                        }
                                                        catch (Exception ex)
                                                        {

                                                            throw new Exception("Error Function clientIsis.ChangeAppFormStatus LN:13196  " + ex);
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }

                                }
                            }
                        }

                    }
                }
            }
        }

        private void SetEnableStatusOfUnderwriterControl(bool enable = true)
        {
            txtUWIsuDate.Enabled = gbAPDate.Enabled = cmbUnderWriteID.Enabled = cmbSummryStatus.Enabled = cmbSummrySubStatus.Enabled = chkSms.Enabled = IsSendSMS.Enabled =
            PolprinteingExcepGroup.Enabled = cmbSummaryStatusCause.Enabled = cmbPPUntil.Enabled = chbDmsChange.Enabled = enable;

        }
        private void GetDataAfterSave(NewBisPASvcRef.U_APPLICATION objDetail)
        {
            cmbUnderWriteFlag.SelectedValue = objDetail.UNDERWRITING_FLG == null ? "" : objDetail.UNDERWRITING_FLG.Value.ToString();
            //Get U_APPROVED =========================================================================
            if (objDetail.END_PROCESS == 'Y')
            {
                btnHeadSave.Enabled = false;
                SetEnableStatusOfUnderwriterControl(false);
                //gbUnderwrite.Enabled = false;
            }

            if (objDetail.APPROVED_Collection != null && objDetail.APPROVED_Collection.Count() > 0)
            {
                GetUApproved(objDetail.APPROVED_Collection[0]);
            }

            //END Get U_APPROVED =====================================================================
            //Get Status =============================================================================
            if (objDetail.STATUS_ID_Collection != null && objDetail.STATUS_ID_Collection.Count() > 0)
            {
                NewBisPASvcRef.U_STATUS_ID uStatusIDObj = new NewBisPASvcRef.U_STATUS_ID();
                uStatusIDObj = objDetail.STATUS_ID_Collection[0];
                var GetStatus = from getStatus in PAR_AUTB_STATUS_COLLECTION
                                where getStatus.STATUS == uStatusIDObj.STATUS
                                select getStatus;
                if (GetStatus != null && GetStatus.Count() > 0)
                {
                    txtStatus.Text = GetStatus.ToArray()[0].DESCRIPTION;
                }
            }
            //END Get Status =========================================================================

            //Get U_AGENT ============================================================================
            if (objDetail.LIFE_ID != null && objDetail.LIFE_ID.UAPP_ID != null)
            {
                if (objDetail.LIFE_ID.AGENT != null && objDetail.LIFE_ID.AGENT.UAPP_ID != null)
                {
                    NewBisPASvcRef.U_AGENT uAgentObj = new NewBisPASvcRef.U_AGENT();
                    uAgentObj = objDetail.LIFE_ID.AGENT;
                    txtStrDt.Text = uAgentObj.STR_DT == null ? "" : Utility.dateTimeToString(uAgentObj.STR_DT.Value, "dd/MM/yyyy", "BU");
                    txtMktDate.Text = uAgentObj.MARKETING_DT == null ? "" : Utility.dateTimeToString(uAgentObj.MARKETING_DT.Value, "dd/MM/yyyy", "BU");
                }
            }

            //Get U_APPLICATION_NAME =================================================================
            if (objDetail.APPLICATION_NAME_Collection != null && objDetail.APPLICATION_NAME_Collection.Count() > 0)
            {
                PAR_U_APPLICATION_NAME = new NewBisPASvcRef.U_APPLICATION_NAME();
                PAR_U_APPLICATION_NAME = objDetail.APPLICATION_NAME_Collection[0];
            }
            //END Get U_APPLICATION_NAME =============================================================
            //END Get U_AGENT ========================================================================
        }


        private void MakeAppRemark()
        {
            if (PAR_PA_APPLICATION_ID != null && PAR_PA_APPLICATION_ID.APP_ID != null)
            {
                if (U_APP_REMARK_COLL != null && U_APP_REMARK_COLL.Count() > 0)
                {
                    foreach (NewBisPASvcRef.U_APP_REMARK obj in U_APP_REMARK_COLL)
                    {
                        obj.APP_ID = PAR_PA_APPLICATION_ID.APP_ID;
                    }
                    PAR_PA_APPLICATION_ID.APP_REMARK_Collection = new NewBisPASvcRef.U_APP_REMARK_Collection();
                    PAR_PA_APPLICATION_ID.APP_REMARK_Collection.AddRange(U_APP_REMARK_COLL);
                }
            }

        }


        private void sendMailDMSNB(String errorMsg)
        {
            try
            {
                LetterSvcRef.LetterServiceClient x = new LetterSvcRef.LetterServiceClient();
                LetterSvcRef.Mail mail = new LetterSvcRef.Mail();
                //LetterSvcRef prMail = new LetterSvcRef.ProcessResult();



                mail.From = "system@bangkoklife.com";
                mail.Subject = "แจ้งความผิดพลาดกรณีไม่สามารถ  เอกสารได้";
                string[] emails = { "naratip.cha@bla.co.th" };
                //mail.IsBodyHtml = true;
                //mail.Encoding = MailEncoding.Windows874;
                mail.To = emails;
                mail.AttachFileCollection = null;
                mail.Body = errorMsg;
                var prMail = x.SendMail(mail);
            }
            catch (Exception)
            {

            }

        }

        private void GetImageISISAndUpload2DMBSNB(long uappId, long appId, string channelType, string policyId, string statusCode, string subStatus, string appNo, string policy, string policyHolding, bool isSendToEasy)
        {
            var refType = "";
            var refID = "";
            try
            {
                refType = "RENEW"; // ทำการเพิ่มใหม่ทังหมด
                var documentIndex = 1;
                var imafeRepo = new Repository();
                var imgeDoc = imafeRepo.GetApplicationDocument(appId, appNo, channelType, UserID);
                if (imgeDoc.ImageApp != null && imgeDoc.ImageApp.DataFiles != null && imgeDoc.ImageApp.DataFiles.Any())
                {
                    long? imageIdEasy = null;
                    refType = "GUID"; // ทำการเพิ่มใบคำขอหลักใหม่
                    refID = imgeDoc.ImageApp.GuIdKey;
                    foreach (var fileItem in imgeDoc.ImageApp.DataFiles)
                    {
                        NewUploadImageToDMSNB(out imageIdEasy, fileItem.FileByte, fileItem.ContentType, appNo, channelType, policyId, policy, policyHolding, uappId, appId, documentIndex, subStatus, UserID);
                    }
                    if (imgeDoc.ImageAppAditonal != null && imgeDoc.ImageAppAditonal.Any())
                    {
                        refType = "CONTENT_ID"; // ทำการเพิ่มเอกสารเพิ่มเติม
                        foreach (var item in imgeDoc.ImageAppAditonal)
                        {
                            refID = item.ConntentIdKey; ;
                            if (item.DataFile != null && item.DataFile.FileByte != null && item.DataFile.FileByte.Any())
                            {
                                documentIndex++;
                                NewUploadImageToDMSNB(out imageIdEasy, item.DataFile.FileByte, item.DataFile.ContentType, appNo, channelType, policyId, policy, policyHolding, uappId, appId, documentIndex, item.StatusType, UserID);
                            }
                        }

                    }
                }
            }
            catch (Exception e)
            {
                #region Add Log
                WriteErrorLogToDMSNB(new NewBISSvcRef.U_DMSNB_LOG()
                {
                    APP_ID = appId,
                    REF_ID = refID,
                    REF_TYPE = refType,
                    SEND_AMOUNT = 1,
                    SEND_FLG = 'N',
                    STATUS = statusCode,
                    SUB_STATUS = subStatus,
                    EXCEPTION_MSG = "Can't not get image from isis",
                    CHANNEL_TYPE = channelType,
                    FILE_SEQ = 0,
                    POLICY_ID = policyId
                });
                #endregion
                throw new Exception("ทำการบันทึกข้อมูล NewBIs เรียบร้อย แต่ไม่สามารถ อัพโหลดไฟล์ไปยัง IMS ได้");
            }
        }


        //private ImageApplicationDocument GetDocumentImageFromIsis(long appId)
        //{
        //    var iamgeDoc = new ImageApplicationDocument();
        //    #region "ค้นหาใบคำขอชุดแรก"
        //    using (var client = new NewBISSvcRef.NewBISSvcClient())
        //    {


        //        try
        //        {

        //            NewBISSvcRef.ProcessResult pr = new NewBISSvcRef.ProcessResult();
        //            NewBISSvcRef.U_APP_ISIS[] uAppISIS;
        //            long[] app_ids = { appId };
        //            pr = client.GetU_APP_ISIS(out uAppISIS, app_ids);
        //            if (uAppISIS != null)
        //            {
        //                foreach (NewBISSvcRef.U_APP_ISIS uapp in uAppISIS)
        //                {
        //                    if (uapp.GUID != null)
        //                    {
        //                        using (var clientISIS = new IsisAppTransportService.AppTransportServiceContractClient())
        //                        {
        //                            try
        //                            {
        //                                iamgeDoc.ImageApp = new ImageApplication()
        //                                {

        //                                    GuIdKey = uapp.GUID,
        //                                    ImageByte = clientISIS.GetAppFormContentByID(uapp.GUID)
        //                                };


        //                            }
        //                            catch (Exception ex)
        //                            {
        //                                throw ex;
        //                            }
        //                            finally
        //                            {
        //                                if (clientISIS.State != System.ServiceModel.CommunicationState.Closed)
        //                                {
        //                                    clientISIS.Close();
        //                                }
        //                            }
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            throw ex;
        //        }
        //        finally
        //        {
        //            if (client.State != System.ServiceModel.CommunicationState.Closed)
        //            {
        //                client.Close();
        //            }
        //        }
        //    }
        //    #endregion

        //    #region "ค้นหาเอกสารเพิ่มเติม"

        //    using (var client = new NewBISSvcRef.NewBISSvcClient())
        //    {
        //        try
        //        {
        //            NewBISSvcRef.ProcessResult pr = new NewBISSvcRef.ProcessResult();
        //            NewBISSvcRef.U_APP_ISIS_DOCUMENT[] uAppISISDocuments;
        //            long[] app_ids = { appId };
        //            pr = client.GetU_APP_ISIS_DOCUMENT(out uAppISISDocuments, null, app_ids);
        //            if (uAppISISDocuments != null)
        //            {
        //                iamgeDoc.ImageAppAditonal = new List<ImageAddtionalApplication>();
        //                foreach (NewBISSvcRef.U_APP_ISIS_DOCUMENT uapp in uAppISISDocuments)
        //                {
        //                    if (uapp.CONTENT_ID != null)
        //                    {
        //                        using (var clientISIS = new IsisAppTransportService.AppTransportServiceContractClient())
        //                        {
        //                            try
        //                            {
        //                                var addtional = new ImageAddtionalApplication() { ConntentIdKey = uapp.CONTENT_ID };
        //                                addtional. = clientISIS.GetMoreContentByID(uapp.CONTENT_ID);
        //                                addtional.StatusType = uapp.STATUS;
        //                                addtional.SubStatusType = uapp.SUBSTATUS;
        //                                iamgeDoc.ImageAppAditonal.Add(addtional);

        //                            }
        //                            catch (Exception ex)
        //                            {
        //                                throw ex;
        //                            }
        //                            finally
        //                            {
        //                                if (clientISIS.State != System.ServiceModel.CommunicationState.Closed)
        //                                {
        //                                    clientISIS.Close();
        //                                }
        //                            }
        //                        }
        //                    }
        //                }

        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            throw ex;
        //        }
        //        finally
        //        {
        //            if (client.State != System.ServiceModel.CommunicationState.Closed)
        //            {
        //                client.Close();
        //            }
        //        }
        //    }
        //    return iamgeDoc;
        //    #endregion


        //}



        private void WriteErrorLogToDMSNB(NewBISSvcRef.U_DMSNB_LOG obj)
        {
            using (var client = new NewBISSvcRef.NewBISSvcClient())
            {
                var pr = client.AddU_DMSNB_LOG(obj);
                if (!pr.Successed)
                {

                    sendMailDMSNB("AddU_DMSNB_LOG Error :" + pr.ErrorMessage);

                }
            }
        }


        public long NewUploadImageToDMSNB(out long? imageEasy, byte[] imageApp, string contentType, string appNo, string channelType, string policyId, string policy, string policyHolding, long uappId, long appId, long seqNo, string documentStatus, string userId)
        {
            var client = new ImageManagementSvcRef.ImagemanagementSvcClient();


            var fileName = "";

            if (contentType == MediaTypeNames.Application.Pdf)
            {
                fileName = (!string.IsNullOrEmpty(policyId) ? "P" + policyId : "A" + appId.ToString()) + "-NEWBIS.PDF";
            }
            else if (contentType == MediaTypeNames.Image.Tiff)
            {
                fileName = (!string.IsNullOrEmpty(policyId) ? "P" + policyId : "A" + appId.ToString()) + "-NEWBIS.TIFF";
            }

            var dataFile = new ITUtility.DataFile()
            {
                ContentType = contentType,
                FileData = imageApp,
                FileName = fileName
            };
            var tag = new List<ImageManagementSvcRef.TAG_INFO>();
            var infoCollection = new ImageManagementSvcRef.TAG_INFO_Collection();

            ImageManagementSvcRef.TAG_DETAIL tagInfo;
            var pr = client.GET_TAG_DETAIL_WITH_APP_NEWBIS(out tagInfo, channelType, documentStatus);
            if (!pr.Successed)
            {
                throw new Exception("GET_TAG_DETAIL_WITH_APP_NEWBIS Error :" + pr.ErrorMessage);
            }

            if (tagInfo == null)
            {
                throw new Exception("GET_TAG_DETAIL_WITH_APP_NEWBIS tagInfo is null :");

            }
            //     var tagTypeId = GetTagTypeId(channelType, documentStatus);
            pr = client.GET_TAG(out infoCollection, tagInfo.APP_IMG_TYPE_ID.Value);
            if (!pr.Successed)
            {
                throw new Exception("GET_TAG error :" + pr.ErrorMessage);

            }
            if (infoCollection != null && infoCollection.Any())
            {
                foreach (var item in infoCollection)
                {
                    if (item.TagName.Equals("POLICY_ID"))
                    {
                        item.TagData = policyId;
                    }
                    if (item.TagName.Equals("APP_ID"))
                    {
                        item.TagData = appId.ToString();
                    }
                    if (item.TagName.Equals("SEQ_NO"))
                    {
                        item.TagData = seqNo.ToString();
                    }
                    if (item.TagName.Equals("PARENT_TYPE"))
                    {
                        item.TagData = "APP";
                    }
                }

                long? imageIms = 0;
                imageEasy = 0;
                var applicationId = ITUtility.Utility.AppSettings("ApplicationId");
                var pathId = ITUtility.Utility.AppSettings("ImsPathIdOfFromAppSetting");
                //  var pr = client.UPLOAD_FILE_BY_TAG(out image, dataFile, userId, null, infoCollection.ToArray(), pathId, applicationId);
                var enumContractTo = !string.IsNullOrEmpty(policyId) ? ImageManagementSvcRef.EnumContractTo.EASY : ImageManagementSvcRef.EnumContractTo.IMS;
                string certNo = "", policyNo = "";
                if (!string.IsNullOrEmpty(policyHolding))
                {
                    certNo = policy;
                    policyNo = policyHolding;
                }
                else
                {
                    policyNo = policy;
                }

                ImageManagementSvcRef.UploadeFiles objectEDM = new ImageManagementSvcRef.UploadeFiles
                {
                    APP_NO = appNo,
                    ChannelType = channelType,
                    policy_id = policyId,
                    UserID = "000000",// userId,
                    cert_no = certNo,
                    policy_no = policyNo,
                    UAPP_ID = uappId
                };
                pr = client.UPLOAD_FILE_BY_TAG__NEWBIS(out imageIms, out imageEasy, dataFile, infoCollection.ToArray(), objectEDM, pathId, applicationId, enumContractTo);
                if (pr.Successed)
                {
                    imageIms = imageIms ?? 0;
                    return imageIms.Value;
                }
                throw new Exception("ImageSystem ->" + pr.ErrorMessage);
            }
            throw new Exception("ImageSystem -> Tag-Info not found from channeltype  " + channelType);
        }

        public long UploadImageToDMSNB(byte[] imageApp, string channelType, string policyId, long appId, long seqNo, string documentStatus, string appNo, string policy, string policyHolding, string policy_id, bool isSendToEasy)
        {
            var client = new ImageManagementSvcRef.ImagemanagementSvcClient();

            var dataFile = new ITUtility.DataFile()
            {

                ContentType = "image/tiff",
                FileData = imageApp,
                FileName = (!string.IsNullOrEmpty(policyId) ? "P" + policyId : "A" + appId.ToString()) + "-NEWBIS.TIFF",


            };
            var tag = new List<ImageManagementSvcRef.TAG_INFO>();
            var infoCollection = new ImageManagementSvcRef.TAG_INFO_Collection();
            var tagKey = client.GET_TAG(out infoCollection, GetTagTypeId(channelType, documentStatus));

            if (infoCollection != null && infoCollection.Any())
            {

                foreach (var item in infoCollection)
                {
                    if (item.TagName.Equals("POLICY_ID"))
                    {
                        item.TagData = policyId;
                    }
                    if (item.TagName.Equals("APP_ID"))
                    {
                        item.TagData = appId.ToString();
                    }
                    if (item.TagName.Equals("SEQ_NO"))
                    {
                        item.TagData = seqNo.ToString();
                    }
                    if (item.TagName.Equals("PARENT_TYPE"))
                    {
                        item.TagData = "APP";
                    }
                }

                long? image = 0; long? image2 = 0;
                var enumContrct = isSendToEasy ? ImageManagementSvcRef.EnumContractTo.EASY : ImageManagementSvcRef.EnumContractTo.IMS;
                ImageManagementSvcRef.UploadeFiles ObjectEDM = new ImageManagementSvcRef.UploadeFiles()
                {
                    UserID = UserID,
                    APP_NO = appNo,
                    ChannelType = channelType,
                    policy_id = policy_id,
                    cert_no = !string.IsNullOrEmpty(policyHolding) ? policy : "",
                    policy_no = !string.IsNullOrEmpty(policyHolding) ? policyHolding : policy,
                };
                var pr = client.UPLOAD_FILE_BY_TAG__NEWBIS(out image, out image2, dataFile, infoCollection.ToArray(), null, "1", "40", enumContrct);

                long imageId = image ?? 0;
                if (pr.Successed)
                {
                    return imageId;
                }
                throw new Exception("ImageSystem ->" + pr.ErrorMessage);
            }
            throw new Exception("ImageSystem -> Tag-Info not found from channeltype  " + channelType);
        }

        private long GetTagTypeId(string channelType, string status)
        {

            if (!status.Equals("CO"))
            {
                switch (channelType.ToUpper())
                {

                    case "PM": return 65;
                    case "CL": return 69;
                    case "OM": return 73;
                    case "MC": return 77;
                    case "PN": return 81;
                    case "HL": return 85;
                    case "KB": return 41;
                    case "GM": return 45;
                    case "PD": return 49;
                    case "PF": return 49;
                    case "PO": return 53;
                    case "GD": return 57;
                    case "DM": return 61;
                    default:
                        return 0;
                }
            }
            else
            {
                switch (channelType.ToUpper())
                {
                    case "PM": return 67;
                    case "CL": return 71;
                    case "OM": return 75;
                    case "MC": return 79;
                    case "PN": return 83;
                    case "HL": return 87;
                    case "KB": return 43;
                    case "GM": return 47;
                    case "PD": return 51;
                    case "PF": return 51;
                    case "PO": return 55;
                    case "GD": return 59;
                    case "DM": return 63;
                    default:
                        return 0;
                }
            }


        }
    }
}
