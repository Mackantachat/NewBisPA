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
namespace NewBisPA
{
    partial class ApplicationForm
    {
        NewBisPASvcRef.AUTB_CHANNEL_BLOCK_COLLECTION PAR_AUTB_CHANNEL_BLOCK_COLLECTION = new NewBisPASvcRef.AUTB_CHANNEL_BLOCK_COLLECTION();
        NewBisPASvcRef.AUTB_APPNAME_ID_COLLECTION PAR_AUTB_APPNAME_ID_COLLECTION = new NewBisPASvcRef.AUTB_APPNAME_ID_COLLECTION();
        NewBisPASvcRef.AUTB_DATADIC_DET_COLLECTION PAR_AUTB_DATADIC_DET_COLLECTION = new NewBisPASvcRef.AUTB_DATADIC_DET_COLLECTION();
        NewBisPASvcRef.AUTB_STATUS_COLLECTION PAR_AUTB_STATUS_COLLECTION = new NewBisPASvcRef.AUTB_STATUS_COLLECTION();
        NewBisPASvcRef.AUTB_SUBSTATUS_COLLECTION PAR_AUTB_SUBSTATUS_COLLECTION = new NewBisPASvcRef.AUTB_SUBSTATUS_COLLECTION();
        NewBisPASvcRef.AUTB_STATUS_CAUSE_COLLECTION PAR_STATUS_CAUSE_COLL = new NewBisPASvcRef.AUTB_STATUS_CAUSE_COLLECTION();
        NewBisPASvcRef.AUTB_LETTER_SCRIPT_COLLECTION PAR_LETTER_SCRIPT_COLL = new NewBisPASvcRef.AUTB_LETTER_SCRIPT_COLLECTION();
        NewBisPASvcRef.AUTB_PRENAME_COLLECTION PAR_AUTB_PRENAME_COLLECTION = new NewBisPASvcRef.AUTB_PRENAME_COLLECTION();
        NewBisPASvcRef.GET_LIST_COLLECTION PAR_COUNTRY_COLLECTION = new NewBisPASvcRef.GET_LIST_COLLECTION();
        NewBisPASvcRef.ZTB_POST_SUBDISTRICT_COLLECTION PAR_ZTB_POST_SUBDISTRICT_COLLECTION = new NewBisPASvcRef.ZTB_POST_SUBDISTRICT_COLLECTION();
        CenterSvcRef.User PAR_USER = new CenterSvcRef.User();
        CenterSvcRef.ProcessResult prUser = new CenterSvcRef.ProcessResult();
        NewBisPASvcRef.AUTB_DATADIC_DET[] benefitTypeArr;
        NewBisPASvcRef.AUTB_DATADIC_DET[] benefitClearArr;
        NewBisPASvcRef.AUTB_DATADIC_DET[] spouseFlgArr;
        NewBisPASvcRef.AUTB_RELATION_COLLECTION PAR_RELATION_COLL = new NewBisPASvcRef.AUTB_RELATION_COLLECTION();
        NewBisPASvcRef.AUTB_UNDERWRITER_COLLECTION PAR_UNDERWRITER_COLL = new NewBisPASvcRef.AUTB_UNDERWRITER_COLLECTION();
        NewBisPASvcRef.AUTB_PEND_REQM_COLLECTION PAR_PEND_REQM_COLL = new NewBisPASvcRef.AUTB_PEND_REQM_COLLECTION();
        NewBisPASvcRef.AUTB_DOCUMENT_PEND_CODE_COLL PAR_DOCUMENT_PEND_CODE_COLL = new NewBisPASvcRef.AUTB_DOCUMENT_PEND_CODE_COLL();
        NewBisPASvcRef.OCCUPATION_DATA_COLLECTION PAR_OCCUPATION_COLL = new NewBisPASvcRef.OCCUPATION_DATA_COLLECTION();
        NewBisPASvcRef.GET_LIST_COLLECTION PAR_POSITION_COLL = new NewBisPASvcRef.GET_LIST_COLLECTION();
        NewBisPASvcRef.GET_LIST_COLLECTION PAR_BUSSINESS_TYPE_COLL = new NewBisPASvcRef.GET_LIST_COLLECTION();
        NewBisPASvcRef.ZTB_PM_POLICY_Collection PAR_ZTB_PM_POLICY_COLL = new NewBisPASvcRef.ZTB_PM_POLICY_Collection();


        private void CreateBaseControlsDat()
        {
            try
            {
                using (CenterSvcRef.CenterServiceClient clientUser = new CenterSvcRef.CenterServiceClient())
                {
                    PAR_USER = clientUser.getUser(out prUser, UserID);
                    if (prUser.Successed == false)
                    {
                        throw new Exception(prUser.ErrorMessage);
                    }
                }

                NewBisPASvcRef.ProcessResult pr = new NewBisPASvcRef.ProcessResult();
                NewBisPASvcRef.AUTB_CHANNEL_COLLECTION channelTypeColl = new NewBisPASvcRef.AUTB_CHANNEL_COLLECTION();
                using (NewBisPASvcRef.NewBisPASvcClient client = new NewBisPASvcRef.NewBisPASvcClient())
                {
                    pr = client.GetBaseControlsData(out channelTypeColl, out PAR_AUTB_CHANNEL_BLOCK_COLLECTION, out PAR_AUTB_APPNAME_ID_COLLECTION, out PAR_AUTB_DATADIC_DET_COLLECTION
                        , out PAR_AUTB_STATUS_COLLECTION, out PAR_AUTB_SUBSTATUS_COLLECTION, out PAR_AUTB_PRENAME_COLLECTION, out PAR_COUNTRY_COLLECTION, out PAR_ZTB_POST_SUBDISTRICT_COLLECTION
                        , out PAR_RELATION_COLL, out PAR_UNDERWRITER_COLL, out PAR_STATUS_CAUSE_COLL, out PAR_LETTER_SCRIPT_COLL, out PAR_PEND_REQM_COLL, out PAR_DOCUMENT_PEND_CODE_COLL
                        , out PAR_OCCUPATION_COLL, out PAR_POSITION_COLL, out PAR_BUSSINESS_TYPE_COLL, out PAR_ZTB_PM_POLICY_COLL);
                    if (pr.Successed == false)
                    {
                        throw new Exception(pr.ErrorMessage);
                    }
                }

                var ptbOptPayColl = new List<NewBISSvcRef.PTB_OPTPAY>(); ;
                using (var newbusClient = new NewBISSvcRef.NewBISSvcClient())
                {
                    NewBISSvcRef.PTB_OPTPAY[] ptbOptPays;
                    var newbisPr = newbusClient.GetPTB_OPTPAY(out ptbOptPays, null);

                    if (newbisPr.Successed)
                    {
                        if (ptbOptPays != null && ptbOptPays.Any())
                        {
                            ptbOptPayColl.AddRange(ptbOptPays);
                        }
                    }
                }

                if (ptbOptPayColl != null && ptbOptPayColl.Count() > 0)
                {
                    NewBISSvcRef.PTB_OPTPAY ptbOptPayObj = new NewBISSvcRef.PTB_OPTPAY();
                    ptbOptPayObj.CODE = "";
                    ptbOptPayObj.DESCRIPTION = "ระบุวิธีการจ่าย";
                    ptbOptPayColl.Add(ptbOptPayObj);
                    ((DataGridViewComboBoxColumn)dgvPayMent.Columns["payMentOpt"]).DataSource = ptbOptPayColl;
                    ((DataGridViewComboBoxColumn)dgvPayMent.Columns["payMentOpt"]).DisplayMember = "DESCRIPTION";
                    ((DataGridViewComboBoxColumn)dgvPayMent.Columns["payMentOpt"]).ValueMember = "CODE";

                }


                NewBisPASvcRef.GET_LIST[] payOptionColl = new NewBisPASvcRef.GET_LIST[3];
                payOptionColl[0] = new NewBisPASvcRef.GET_LIST();
                payOptionColl[0].CODE = "";
                payOptionColl[0].DESCRIPTION = "ระบุประเภทการชำระ";
                payOptionColl[1] = new NewBisPASvcRef.GET_LIST();
                payOptionColl[1].CODE = "08";
                payOptionColl[1].DESCRIPTION = "บัตรเครดิต";
                payOptionColl[2] = new NewBisPASvcRef.GET_LIST();
                payOptionColl[2].CODE = "RE";
                payOptionColl[2].DESCRIPTION = "Recurring";
                cmbCreditPayOption.DataSource = payOptionColl;
                cmbCreditPayOption.DisplayMember = "DESCRIPTION";
                cmbCreditPayOption.ValueMember = "CODE";
                cmbCreditPayOption.SelectedValue = "";


                if (channelTypeColl != null && channelTypeColl.Count() > 0)
                {
                    var channelCodes = new string[] { "PD", "PO", "KB", "PN" ,  "PF" };
                    var ChannelTypeLinq = from channelTypeLinq in channelTypeColl
                                          where channelCodes.Contains(channelTypeLinq.CHANNEL_TYPE)
                                          select channelTypeLinq;
                    if (ChannelTypeLinq != null && ChannelTypeLinq.Count() > 0)
                    {
                        NewBisPASvcRef.AUTB_CHANNEL_COLLECTION channelTypes = new NewBisPASvcRef.AUTB_CHANNEL_COLLECTION();
                        NewBisPASvcRef.AUTB_CHANNEL channelObj = new NewBisPASvcRef.AUTB_CHANNEL();
                        channelObj.CHANNEL_TYPE = "";
                        channelObj.DESCRIPTION = "กรุณาระบุช่องทางการขายที่ท่านต้องการ";
                        channelTypes.Add(channelObj);
                        channelTypes.AddRange(ChannelTypeLinq.ToArray());
                        cmbChannelType.DataSource = channelTypes;
                        cmbChannelType.DisplayMember = "DESCRIPTION";
                        cmbChannelType.ValueMember = "CHANNEL_TYPE";
                        cmbChannelType.SelectedValue = "PD";

                        SetControlByChannelType("PD");

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
                                cmbWorkGroup.SelectedValue = "BNC";
                            }
                        }

                        if (PAR_RELATION_COLL != null && PAR_RELATION_COLL.Any())
                        {
                            NewBisPASvcRef.AUTB_RELATION_COLLECTION relationColl = new NewBisPASvcRef.AUTB_RELATION_COLLECTION();
                            NewBisPASvcRef.AUTB_RELATION relationObj = new NewBisPASvcRef.AUTB_RELATION();
                            relationObj.RELATION = "ระบุความสัมพันธ์";
                            relationObj.DESCRIPTION = "ระบุความสัมพันธ์";
                            relationColl.Add(relationObj);
                            relationColl.AddRange(PAR_RELATION_COLL);
                            cmbParentRelation.DataSource = relationColl;
                            cmbParentRelation.DisplayMember = "RELATION";
                            cmbParentRelation.ValueMember = "RELATION";
                            cmbParentRelation.SelectedValue = "ระบุความสัมพันธ์";

                            AutoCompleteStringCollection data = new AutoCompleteStringCollection();
                            foreach (NewBisPASvcRef.AUTB_RELATION item in relationColl)
                            {
                                data.Add(item.RELATION);
                            }
                            cmbParentRelation.AutoCompleteMode = AutoCompleteMode.Suggest;
                            cmbParentRelation.AutoCompleteSource = AutoCompleteSource.CustomSource;
                            cmbParentRelation.AutoCompleteCustomSource = data;

                        }

                        if (PAR_AUTB_APPNAME_ID_COLLECTION != null && PAR_AUTB_APPNAME_ID_COLLECTION.Count() > 0)
                        {
                            if (cmbChannelType.SelectedValue == null || cmbChannelType.SelectedValue.ToString() == "")
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
                            else
                            {
                                var GetData = from getData in PAR_AUTB_APPNAME_ID_COLLECTION
                                              where getData.CHANNEL_TYPE == cmbChannelType.SelectedValue.ToString()
                                              select getData;
                                if (GetData != null && GetData.Count() > 0)
                                {
                                    var appNameInfo = GetData.Where(item => item.DEFAULT_FLG == "Y").FirstOrDefault();
                                    NewBisPASvcRef.AUTB_APPNAME_ID_COLLECTION appNameIDs = new NewBisPASvcRef.AUTB_APPNAME_ID_COLLECTION();
                                    NewBisPASvcRef.AUTB_APPNAME_ID appNameIDObj = new NewBisPASvcRef.AUTB_APPNAME_ID();
                                    appNameIDObj.APPNAME_ID = 0;
                                    appNameIDObj.TITLE = "กรุณาระบุเอกสารชุดใบคำขอที่ต้องการ";
                                    appNameIDObj.CHANNEL_TYPE = "";
                                    appNameIDObj.WORK_GROUP = "";
                                    appNameIDs.Add(appNameIDObj);
                                    appNameIDs.AddRange(GetData);
                                    cmbAppNameID.DataSource = appNameIDs;
                                    cmbAppNameID.DisplayMember = "TITLE";
                                    cmbAppNameID.ValueMember = "APPNAME_ID";
                                    if (appNameInfo != null)
                                    {
                                        cmbAppNameID.SelectedValue = appNameInfo.APPNAME_ID.Value;
                                    }
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

                if (PAR_AUTB_DATADIC_DET_COLLECTION != null && PAR_AUTB_DATADIC_DET_COLLECTION.Count() > 0)
                {
                    var SendToLinq = from sendToLinq in PAR_AUTB_DATADIC_DET_COLLECTION
                                     where sendToLinq.COL_NAME == "SEND_TO"
                                     select sendToLinq;
                    if (SendToLinq != null && SendToLinq.Count() > 0)
                    {
                        NewBisPASvcRef.AUTB_DATADIC_DET_COLLECTION sendToS = new NewBisPASvcRef.AUTB_DATADIC_DET_COLLECTION();
                        NewBisPASvcRef.AUTB_DATADIC_DET sendToObj = new NewBisPASvcRef.AUTB_DATADIC_DET();
                        sendToObj.CODE = "";
                        sendToObj.DESCRIPTION = "ระบุวิธีการส่ง กธ.";
                        sendToS.Add(sendToObj);
                        sendToS.AddRange(SendToLinq.ToArray());
                        cmbSendTo.DataSource = sendToS;
                        cmbSendTo.DisplayMember = "DESCRIPTION";
                        cmbSendTo.ValueMember = "CODE";
                        cmbSendTo.SelectedValue = "";
                    }

                    var DocumentFlgLinq = from documentFlgLinq in PAR_AUTB_DATADIC_DET_COLLECTION
                                          where documentFlgLinq.COL_NAME == "DOCUMENT_FLG"
                                          select documentFlgLinq;
                    if (DocumentFlgLinq != null && DocumentFlgLinq.Count() > 0)
                    {
                        NewBisPASvcRef.AUTB_DATADIC_DET_COLLECTION ducumentFlgS = new NewBisPASvcRef.AUTB_DATADIC_DET_COLLECTION();
                        NewBisPASvcRef.AUTB_DATADIC_DET ducumentFlgObj = new NewBisPASvcRef.AUTB_DATADIC_DET();
                        ducumentFlgObj.CODE = "";
                        ducumentFlgObj.DESCRIPTION = "ระบุสถานะเอกสาร";
                        ducumentFlgS.Add(ducumentFlgObj);
                        ducumentFlgS.AddRange(DocumentFlgLinq.ToArray());
                        cmbDocumentFlag.DataSource = ducumentFlgS;
                        cmbDocumentFlag.DisplayMember = "DESCRIPTION";
                        cmbDocumentFlag.ValueMember = "CODE";
                        cmbDocumentFlag.SelectedValue = "";
                    }

                    var UnderWriteFlgLinq = from underWriteFlgLinq in PAR_AUTB_DATADIC_DET_COLLECTION
                                            where underWriteFlgLinq.COL_NAME == "UNDERWRITING_FLG"
                                            select underWriteFlgLinq;
                    if (UnderWriteFlgLinq != null && UnderWriteFlgLinq.Count() > 0)
                    {
                        NewBisPASvcRef.AUTB_DATADIC_DET_COLLECTION underWriteFlgS = new NewBisPASvcRef.AUTB_DATADIC_DET_COLLECTION();
                        NewBisPASvcRef.AUTB_DATADIC_DET underWriteFlgObj = new NewBisPASvcRef.AUTB_DATADIC_DET();
                        underWriteFlgObj.CODE = "";
                        underWriteFlgObj.DESCRIPTION = "ระบุสถานะการพิจารณา";
                        underWriteFlgS.Add(underWriteFlgObj);
                        underWriteFlgS.AddRange(UnderWriteFlgLinq.ToArray());
                        cmbUnderWriteFlag.DataSource = underWriteFlgS;
                        cmbUnderWriteFlag.DisplayMember = "DESCRIPTION";
                        cmbUnderWriteFlag.ValueMember = "CODE";
                        cmbUnderWriteFlag.SelectedValue = "";
                    }

                    var CustomerTypeLinq = from customerTypeLinq in PAR_AUTB_DATADIC_DET_COLLECTION
                                           where customerTypeLinq.COL_NAME == "CUSTOMER_TYPE"
                                           select customerTypeLinq;
                    if (CustomerTypeLinq != null && CustomerTypeLinq.Count() > 0)
                    {
                        NewBisPASvcRef.AUTB_DATADIC_DET_COLLECTION customerTypeS = new NewBisPASvcRef.AUTB_DATADIC_DET_COLLECTION();
                        NewBisPASvcRef.AUTB_DATADIC_DET customerTypeObj = new NewBisPASvcRef.AUTB_DATADIC_DET();
                        customerTypeObj.CODE = "";
                        customerTypeObj.DESCRIPTION = "ระบุผู้ที่จะพิจารณา";
                        customerTypeS.Add(customerTypeObj);
                        customerTypeS.AddRange(CustomerTypeLinq.ToArray());
                        cmbUnderWrite.DataSource = customerTypeS;
                        cmbUnderWrite.DisplayMember = "DESCRIPTION";
                        cmbUnderWrite.ValueMember = "CODE";
                        cmbUnderWrite.SelectedValue = "C";
                    }


                    var PaymentDescLinq =
                        from paymentDescLinq in PAR_AUTB_DATADIC_DET_COLLECTION
                        where paymentDescLinq.COL_NAME == "PAYMENT_DETAIL"
                        orderby paymentDescLinq.CODE ascending
                        select paymentDescLinq;
                    if (PaymentDescLinq != null && PaymentDescLinq.Count() > 0)
                    {
                        PaymentDescColl = new NewBisPASvcRef.AUTB_DATADIC_DET_COLLECTION();
                        PaymentDescColl.AddRange(PaymentDescLinq.ToArray());
                    }
                }

                if (PAR_AUTB_STATUS_COLLECTION != null && PAR_AUTB_STATUS_COLLECTION.Count() > 0)
                {
                    var StatusLinq = from statusLinq in PAR_AUTB_STATUS_COLLECTION
                                     where statusLinq.STATUS == "WT"
                                     select statusLinq;
                    txtStatus.Text = StatusLinq.First().DESCRIPTION;
                }


                var ePolicySendTypeData = from documentFlgLinq in PAR_AUTB_DATADIC_DET_COLLECTION
                                      where documentFlgLinq.COL_NAME == "EPOLICY_SEND_TYPE"
                                      select documentFlgLinq;
                if (ePolicySendTypeData != null && ePolicySendTypeData.Count() > 0)
                {
                    NewBisPASvcRef.AUTB_DATADIC_DET_COLLECTION ducumentFlgS = new NewBisPASvcRef.AUTB_DATADIC_DET_COLLECTION();
                    NewBisPASvcRef.AUTB_DATADIC_DET ducumentFlgObj = new NewBisPASvcRef.AUTB_DATADIC_DET();
                    ducumentFlgObj.CODE = "";
                    ducumentFlgObj.DESCRIPTION = "ระบุการส่ง กธ.";
                    ducumentFlgS.Add(ducumentFlgObj);
                    ducumentFlgS.AddRange(ePolicySendTypeData.ToArray());
                    cmbEPolicySending.DataSource = ducumentFlgS;
                    cmbEPolicySending.DisplayMember = "DESCRIPTION";
                    cmbEPolicySending.ValueMember = "CODE";
                    cmbEPolicySending.SelectedValue = "";
                }

                var policyDocTypeData = from documentFlgLinq in PAR_AUTB_DATADIC_DET_COLLECTION
                                          where documentFlgLinq.COL_NAME == "POLICY_DOC_TYPE"
                                          select documentFlgLinq;
                if (policyDocTypeData != null && policyDocTypeData.Count() > 0)
                {
                    NewBisPASvcRef.AUTB_DATADIC_DET_COLLECTION ducumentFlgS = new NewBisPASvcRef.AUTB_DATADIC_DET_COLLECTION();
                    NewBisPASvcRef.AUTB_DATADIC_DET ducumentFlgObj = new NewBisPASvcRef.AUTB_DATADIC_DET();
                    ducumentFlgObj.CODE = "";
                    ducumentFlgObj.DESCRIPTION = "ระบุประเภทชุด กธ.";
                    ducumentFlgS.Add(ducumentFlgObj);
                    ducumentFlgS.AddRange(policyDocTypeData.ToArray());
                    cmbPolicyDocmentType.DataSource = ducumentFlgS;
                    cmbPolicyDocmentType.DisplayMember = "DESCRIPTION";
                    cmbPolicyDocmentType.ValueMember = "CODE";
                    cmbPolicyDocmentType.SelectedValue = "";
                }



                DateControlsBegin();
                CreateBaseControlsCustomerPage();
                CreateBaseControlsPlanPage();
                CreateBaseControlsBenefitPage();
                CreateBaseControlsCreditCard();
                CreateBaseControlsUnderWrite();
                CreateBaseControlsMemoPage();
                txtAppNo.Focus();
            }
            catch (Exception ex)
            {
                SetMsgException(ex);
            }

        }
        private void CreateBaseControlsCustomerPage()
        {
            NewBisPASvcRef.AUTB_DATADIC_DET_COLLECTION cardTypeS = new NewBisPASvcRef.AUTB_DATADIC_DET_COLLECTION();
            NewBisPASvcRef.AUTB_DATADIC_DET cardType1Obj = new NewBisPASvcRef.AUTB_DATADIC_DET();
            cardType1Obj.CODE = "";
            cardType1Obj.DESCRIPTION = "ระบุประเภทบัตร";
            cardTypeS.Add(cardType1Obj);
            NewBisPASvcRef.AUTB_DATADIC_DET cardType2Obj = new NewBisPASvcRef.AUTB_DATADIC_DET();
            cardType2Obj.CODE = "1";
            cardType2Obj.DESCRIPTION = "บัตรประชาชน";
            cardTypeS.Add(cardType2Obj);
            NewBisPASvcRef.AUTB_DATADIC_DET cardType3Obj = new NewBisPASvcRef.AUTB_DATADIC_DET();
            cardType3Obj.CODE = "2";
            cardType3Obj.DESCRIPTION = "หนังสือเดินทาง";
            cardTypeS.Add(cardType3Obj);
            cmbCardType.DataSource = cardTypeS;
            cmbCardType.DisplayMember = "DESCRIPTION";
            cmbCardType.ValueMember = "CODE";
            cmbCardType.SelectedValue = "1";

            NewBisPASvcRef.AUTB_DATADIC_DET[] paCardTypeS = new NewBisPASvcRef.AUTB_DATADIC_DET[cardTypeS.Count];
            cardTypeS.CopyTo(paCardTypeS);
            cmbParentCardType.DataSource = paCardTypeS;
            cmbParentCardType.DisplayMember = "DESCRIPTION";
            cmbParentCardType.ValueMember = "CODE";
            cmbParentCardType.SelectedValue = "1";


            NewBisPASvcRef.AUTB_DATADIC_DET[] spCardTypeArr = new NewBisPASvcRef.AUTB_DATADIC_DET[cardTypeS.Count()];
            cardTypeS.CopyTo(spCardTypeArr);
            cmbSpCardType.DataSource = spCardTypeArr;
            cmbSpCardType.DisplayMember = "DESCRIPTION";
            cmbSpCardType.ValueMember = "CODE";
            cmbSpCardType.SelectedValue = "1";





            NewBisPASvcRef.AUTB_DATADIC_DET[] dataCusArr = new NewBisPASvcRef.AUTB_DATADIC_DET[3];
            dataCusArr[0] = new NewBisPASvcRef.AUTB_DATADIC_DET();
            dataCusArr[0].CODE = "";
            dataCusArr[0].DESCRIPTION = "ระบุข้อมูลลูกค้า";
            dataCusArr[1] = new NewBisPASvcRef.AUTB_DATADIC_DET();
            dataCusArr[1].CODE = "1";
            dataCusArr[1].DESCRIPTION = "ลูกค้าเก่า";
            dataCusArr[2] = new NewBisPASvcRef.AUTB_DATADIC_DET();
            dataCusArr[2].CODE = "2";
            dataCusArr[2].DESCRIPTION = "ลูกค้าใหม่";
            cmbDataCus.DataSource = dataCusArr;
            cmbDataCus.DisplayMember = "DESCRIPTION";
            cmbDataCus.ValueMember = "CODE";
            cmbDataCus.SelectedValue = "2";


            NewBisPASvcRef.AUTB_DATADIC_DET[] paDataCusArr = new NewBisPASvcRef.AUTB_DATADIC_DET[dataCusArr.Count()];
            paDataCusArr = dataCusArr.Clone() as NewBisPASvcRef.AUTB_DATADIC_DET[];
            cmbDataParent.DataSource = paDataCusArr;
            cmbDataParent.DisplayMember = "DESCRIPTION";
            cmbDataParent.ValueMember = "CODE";
            cmbDataParent.SelectedValue = "2";


            if (PAR_AUTB_DATADIC_DET_COLLECTION != null && PAR_AUTB_DATADIC_DET_COLLECTION.Count() > 0)
            {
                var SexLinq = from sexLinq in PAR_AUTB_DATADIC_DET_COLLECTION
                              where sexLinq.COL_NAME == "SEX"
                              select sexLinq;

                if (SexLinq != null && SexLinq.Count() > 0)
                {
                    NewBisPASvcRef.AUTB_DATADIC_DET_COLLECTION sexS = new NewBisPASvcRef.AUTB_DATADIC_DET_COLLECTION();
                    NewBisPASvcRef.AUTB_DATADIC_DET sexObj = new NewBisPASvcRef.AUTB_DATADIC_DET();
                    sexObj.CODE = "";
                    sexObj.DESCRIPTION = "ระบุเพศ";
                    sexS.Add(sexObj);
                    sexS.AddRange(SexLinq.ToArray());
                    cmbSex.DataSource = sexS;
                    cmbSex.DisplayMember = "DESCRIPTION";
                    cmbSex.ValueMember = "CODE";
                    cmbSex.SelectedValue = "";

                    NewBisPASvcRef.AUTB_DATADIC_DET[] pSexArr = new NewBisPASvcRef.AUTB_DATADIC_DET[sexS.Count()];
                    sexS.CopyTo(pSexArr);
                    cmbParentGender.DataSource = pSexArr;
                    cmbParentGender.DisplayMember = "DESCRIPTION";
                    cmbParentGender.ValueMember = "CODE";
                    cmbParentGender.SelectedValue = "";

                    NewBisPASvcRef.AUTB_DATADIC_DET[] spSexArr = new NewBisPASvcRef.AUTB_DATADIC_DET[sexS.Count()];
                    sexS.CopyTo(spSexArr);
                    cmbSpSex.DataSource = spSexArr;
                    cmbSpSex.DisplayMember = "DESCRIPTION";
                    cmbSpSex.ValueMember = "CODE";
                    cmbSpSex.SelectedValue = "";
                }

                var ReligionLinq = from religionLinq in PAR_AUTB_DATADIC_DET_COLLECTION
                                   where religionLinq.COL_NAME == "RELIGION"
                                   select religionLinq;
                if (ReligionLinq != null && ReligionLinq.Count() > 0)
                {
                    NewBisPASvcRef.AUTB_DATADIC_DET_COLLECTION religionS = new NewBisPASvcRef.AUTB_DATADIC_DET_COLLECTION();
                    NewBisPASvcRef.AUTB_DATADIC_DET religionObj = new NewBisPASvcRef.AUTB_DATADIC_DET();
                    religionObj.CODE = "";
                    religionObj.DESCRIPTION = "ระบุศาสนา";
                    religionS.Add(religionObj);
                    religionS.AddRange(ReligionLinq.ToArray());
                    cmbReligion.DataSource = religionS;
                    cmbReligion.DisplayMember = "DESCRIPTION";
                    cmbReligion.ValueMember = "CODE";
                    cmbReligion.SelectedValue = "";

                    NewBisPASvcRef.AUTB_DATADIC_DET[] pReligionS = new NewBisPASvcRef.AUTB_DATADIC_DET[religionS.Count()];
                    religionS.CopyTo(pReligionS);
                    cmbParentReligion.DataSource = pReligionS;
                    cmbParentReligion.DisplayMember = "DESCRIPTION";
                    cmbParentReligion.ValueMember = "CODE";
                    cmbParentReligion.SelectedValue = "";
                }

                var MaritalLinq = from maritalLinq in PAR_AUTB_DATADIC_DET_COLLECTION
                                  where maritalLinq.COL_NAME == "MARITAL"
                                  select maritalLinq;
                if (MaritalLinq != null && MaritalLinq.Count() > 0)
                {
                    NewBisPASvcRef.AUTB_DATADIC_DET_COLLECTION maritalS = new NewBisPASvcRef.AUTB_DATADIC_DET_COLLECTION();
                    NewBisPASvcRef.AUTB_DATADIC_DET maritalObj = new NewBisPASvcRef.AUTB_DATADIC_DET();
                    maritalObj.CODE = "";
                    maritalObj.DESCRIPTION = "ระบุสถานภาพการสมรส";
                    maritalS.Add(maritalObj);
                    maritalS.AddRange(MaritalLinq.ToArray());
                    cmbMaritalStatus.DataSource = maritalS;
                    cmbMaritalStatus.DisplayMember = "DESCRIPTION";
                    cmbMaritalStatus.ValueMember = "CODE";
                    cmbMaritalStatus.SelectedValue = "";



                    NewBisPASvcRef.AUTB_DATADIC_DET[] pMaritalS = new NewBisPASvcRef.AUTB_DATADIC_DET[maritalS.Count()];
                    maritalS.CopyTo(pMaritalS);
                    cmbParentMaritalStatus.DataSource = pMaritalS;
                    cmbParentMaritalStatus.DisplayMember = "DESCRIPTION";
                    cmbParentMaritalStatus.ValueMember = "CODE";
                    cmbParentMaritalStatus.SelectedValue = "";
                }

                var DepositTypeLinq = from depositTypeLinq in PAR_AUTB_DATADIC_DET_COLLECTION
                                      where depositTypeLinq.COL_NAME == "ACC_DEPOSIT_TYPE"
                                      && depositTypeLinq.CODE != "C"
                                      select depositTypeLinq;
                if (DepositTypeLinq != null && DepositTypeLinq.Count() > 0)
                {
                    NewBisPASvcRef.AUTB_DATADIC_DET_COLLECTION depositTypeS = new NewBisPASvcRef.AUTB_DATADIC_DET_COLLECTION();
                    NewBisPASvcRef.AUTB_DATADIC_DET depositTypeObj = new NewBisPASvcRef.AUTB_DATADIC_DET();
                    depositTypeObj.CODE = "";
                    depositTypeObj.DESCRIPTION = "ระบุประเภทบัญชี";
                    depositTypeS.Add(depositTypeObj);
                    depositTypeS.AddRange(DepositTypeLinq.ToArray());
                    cmbDepositType.DataSource = depositTypeS;
                    cmbDepositType.DisplayMember = "DESCRIPTION";
                    cmbDepositType.ValueMember = "CODE";
                    cmbDepositType.SelectedValue = "";

                    txtBankCode.Text = "002";
                    txtBankName.Text = "ธนาคารกรุงเทพ จำกัด (มหาชน)";
                }

            }

            if (PAR_AUTB_PRENAME_COLLECTION != null && PAR_AUTB_PRENAME_COLLECTION.Count() > 0)
            {
                AutoCompleteStringCollection data = new AutoCompleteStringCollection();
                foreach (NewBisPASvcRef.AUTB_PRENAME item in PAR_AUTB_PRENAME_COLLECTION)
                {
                    data.Add(item.DESC_ABBR);
                }
                txtPrename.AutoCompleteMode = AutoCompleteMode.Suggest;
                txtPrename.AutoCompleteSource = AutoCompleteSource.CustomSource;
                txtPrename.AutoCompleteCustomSource = data;

                txtParentPrename.AutoCompleteMode = AutoCompleteMode.Suggest;
                txtParentPrename.AutoCompleteSource = AutoCompleteSource.CustomSource;
                txtParentPrename.AutoCompleteCustomSource = data;

                txtSpPrename.AutoCompleteMode = AutoCompleteMode.Suggest;
                txtSpPrename.AutoCompleteSource = AutoCompleteSource.CustomSource;
                txtSpPrename.AutoCompleteCustomSource = data;

                txtBenefitPrename.AutoCompleteMode = AutoCompleteMode.Suggest;
                txtBenefitPrename.AutoCompleteSource = AutoCompleteSource.CustomSource;
                txtBenefitPrename.AutoCompleteCustomSource = data;
            }


            if (PAR_COUNTRY_COLLECTION != null && PAR_COUNTRY_COLLECTION.Count() > 0)
            {
                NewBisPASvcRef.GET_LIST countryObj = new NewBisPASvcRef.GET_LIST();
                countryObj.CODE = "";
                countryObj.DESCRIPTION = "ระบุสัญชาติ";
                PAR_COUNTRY_COLLECTION.Add(countryObj);
                cmbNationality.DataSource = PAR_COUNTRY_COLLECTION;
                cmbNationality.DisplayMember = "DESCRIPTION";
                cmbNationality.ValueMember = "CODE";
                cmbNationality.SelectedValue = "";

                NewBisPASvcRef.GET_LIST[] pCountry = new NewBisPASvcRef.GET_LIST[PAR_COUNTRY_COLLECTION.Count];
                PAR_COUNTRY_COLLECTION.CopyTo(pCountry);

                cmbParentNationality.DataSource = pCountry;
                cmbParentNationality.DisplayMember = "DESCRIPTION";
                cmbParentNationality.ValueMember = "CODE";

                cmbNationality.SelectedValue = "";
                AutoCompleteStringCollection data = new AutoCompleteStringCollection();
                foreach (NewBisPASvcRef.GET_LIST item in PAR_COUNTRY_COLLECTION)
                {
                    data.Add(item.DESCRIPTION);
                }
                cmbNationality.AutoCompleteMode = AutoCompleteMode.Suggest;
                cmbNationality.AutoCompleteSource = AutoCompleteSource.CustomSource;
                cmbNationality.AutoCompleteCustomSource = data;

                cmbParentNationality.AutoCompleteMode = AutoCompleteMode.Suggest;
                cmbParentNationality.AutoCompleteSource = AutoCompleteSource.CustomSource;
                cmbParentNationality.AutoCompleteCustomSource = data;

                NewBisPASvcRef.GET_LIST[] spCountryArr = new NewBisPASvcRef.GET_LIST[PAR_COUNTRY_COLLECTION.Count()];
                PAR_COUNTRY_COLLECTION.CopyTo(spCountryArr);
                cmbSpNationality.DataSource = spCountryArr;
                cmbSpNationality.DisplayMember = "DESCRIPTION";
                cmbSpNationality.ValueMember = "CODE";
                cmbSpNationality.SelectedValue = "";
                AutoCompleteStringCollection dataSp = new AutoCompleteStringCollection();
                foreach (NewBisPASvcRef.GET_LIST item in spCountryArr)
                {
                    dataSp.Add(item.DESCRIPTION);
                }
                cmbSpNationality.AutoCompleteMode = AutoCompleteMode.Suggest;
                cmbSpNationality.AutoCompleteSource = AutoCompleteSource.CustomSource;
                cmbSpNationality.AutoCompleteCustomSource = dataSp;
            }

            NewBisPASvcRef.AUTB_DATADIC_DET_COLLECTION addressTypeS = new NewBisPASvcRef.AUTB_DATADIC_DET_COLLECTION();
            NewBisPASvcRef.AUTB_DATADIC_DET objAdrType = new NewBisPASvcRef.AUTB_DATADIC_DET();
            objAdrType.CODE = "";
            objAdrType.DESCRIPTION = "ระบุประเภทที่อยู่";
            addressTypeS.Add(objAdrType);
            NewBisPASvcRef.AUTB_DATADIC_DET objAdrType0 = new NewBisPASvcRef.AUTB_DATADIC_DET();
            objAdrType0.CODE = "0";
            objAdrType0.DESCRIPTION = "ที่อยู่ปัจจุบัน";
            addressTypeS.Add(objAdrType0);
            NewBisPASvcRef.AUTB_DATADIC_DET objAdrType1 = new NewBisPASvcRef.AUTB_DATADIC_DET();
            objAdrType1.CODE = "1";
            objAdrType1.DESCRIPTION = "ที่อยู่ที่ทำงาน";
            addressTypeS.Add(objAdrType1);
            NewBisPASvcRef.AUTB_DATADIC_DET objAdrType2 = new NewBisPASvcRef.AUTB_DATADIC_DET();
            objAdrType2.CODE = "2";
            objAdrType2.DESCRIPTION = "ทะเบียนบ้าน";
            addressTypeS.Add(objAdrType2);
            NewBisPASvcRef.AUTB_DATADIC_DET objAdrType3 = new NewBisPASvcRef.AUTB_DATADIC_DET();
            objAdrType3.CODE = "3";
            objAdrType3.DESCRIPTION = "อื่นๆ";
            addressTypeS.Add(objAdrType3);

            cmbAddressType.DataSource = addressTypeS;
            cmbAddressType.DisplayMember = "DESCRIPTION";
            cmbAddressType.ValueMember = "CODE";
            cmbAddressType.SelectedValue = "";


            NewBisPASvcRef.AUTB_DATADIC_DET[] pAddressTypeS = new NewBisPASvcRef.AUTB_DATADIC_DET[addressTypeS.Count()];
            addressTypeS.CopyTo(pAddressTypeS);

            cmbParentAddressType.DataSource = pAddressTypeS;
            cmbParentAddressType.DisplayMember = "DESCRIPTION";
            cmbParentAddressType.ValueMember = "CODE";
            cmbParentAddressType.SelectedValue = "";

            NewBisPASvcRef.AUTB_DATADIC_DET[] oAddressTypeArr = new NewBisPASvcRef.AUTB_DATADIC_DET[3];
            oAddressTypeArr[0] = new NewBisPASvcRef.AUTB_DATADIC_DET();
            oAddressTypeArr[0].CODE = "";
            oAddressTypeArr[0].DESCRIPTION = "ระบุข้อมูลที่อยู่";
            oAddressTypeArr[1] = new NewBisPASvcRef.AUTB_DATADIC_DET();
            oAddressTypeArr[1].CODE = "1";
            oAddressTypeArr[1].DESCRIPTION = "ที่อยู่เก่า";
            oAddressTypeArr[2] = new NewBisPASvcRef.AUTB_DATADIC_DET();
            oAddressTypeArr[2].CODE = "2";
            oAddressTypeArr[2].DESCRIPTION = "ที่อยู่ใหม่";
            cmbAdrType.DataSource = oAddressTypeArr;
            cmbAdrType.DisplayMember = "DESCRIPTION";
            cmbAdrType.ValueMember = "CODE";
            cmbAdrType.SelectedValue = "2";

            NewBisPASvcRef.AUTB_DATADIC_DET[] pOAddressTypeArr = new NewBisPASvcRef.AUTB_DATADIC_DET[oAddressTypeArr.Count()];
            pOAddressTypeArr = oAddressTypeArr.Clone() as NewBisPASvcRef.AUTB_DATADIC_DET[];

            cmbParentAdrType.DataSource = pOAddressTypeArr;
            cmbParentAdrType.DisplayMember = "DESCRIPTION";
            cmbParentAdrType.ValueMember = "CODE";
            cmbParentAdrType.SelectedValue = "2";

            if (PAR_ZTB_POST_SUBDISTRICT_COLLECTION != null && PAR_ZTB_POST_SUBDISTRICT_COLLECTION.Count() > 0)
            {
                var ProvinceLinq = (from provinceLinq in PAR_ZTB_POST_SUBDISTRICT_COLLECTION orderby provinceLinq.PROVINCE ascending select provinceLinq.PROVINCE).Distinct();
                if (ProvinceLinq != null && ProvinceLinq.Count() > 0)
                {
                    NewBisPASvcRef.GET_LIST_COLLECTION provinceS = new NewBisPASvcRef.GET_LIST_COLLECTION();
                    NewBisPASvcRef.GET_LIST provinceObj = new NewBisPASvcRef.GET_LIST();
                    provinceObj.CODE = "";
                    provinceObj.DESCRIPTION = "ระบุจังหวัดที่ต้องการ";
                    provinceS.Add(provinceObj);

                    foreach (var item in ProvinceLinq)
                    {
                        NewBisPASvcRef.GET_LIST obj = new NewBisPASvcRef.GET_LIST();
                        obj.CODE = item.ToString();
                        obj.DESCRIPTION = item.ToString();
                        provinceS.Add(obj);
                    }
                    cmbProvince.DataSource = provinceS;
                    cmbProvince.DisplayMember = "DESCRIPTION";
                    cmbProvince.ValueMember = "CODE";
                    cmbProvince.SelectedValue = "";

                    NewBisPASvcRef.GET_LIST[] pProvinceS = new NewBisPASvcRef.GET_LIST[provinceS.Count];
                    provinceS.CopyTo(pProvinceS);
                    cmbParentProvince.DataSource = pProvinceS;
                    cmbParentProvince.DisplayMember = "DESCRIPTION";
                    cmbParentProvince.ValueMember = "CODE";
                    cmbParentProvince.SelectedValue = "";

                    AutoCompleteStringCollection data = new AutoCompleteStringCollection();
                    foreach (NewBisPASvcRef.GET_LIST item in provinceS)
                    {
                        data.Add(item.DESCRIPTION);
                    }


                    cmbProvince.AutoCompleteMode = AutoCompleteMode.Suggest;
                    cmbProvince.AutoCompleteSource = AutoCompleteSource.CustomSource;
                    cmbProvince.AutoCompleteCustomSource = data;


                    cmbParentProvince.AutoCompleteMode = AutoCompleteMode.Suggest;
                    cmbParentProvince.AutoCompleteSource = AutoCompleteSource.CustomSource;
                    cmbParentProvince.AutoCompleteCustomSource = data;

                }
            }

            if (PAR_OCCUPATION_COLL != null && PAR_OCCUPATION_COLL.Count() > 0)
            {




                /*กลุ่มอาชีพ*/
                var ocpGroupColl = new NewBisPASvcRef.OCCUPATION_DATA_COLLECTION();
               var  ocpGroupObj = new NewBisPASvcRef.OCCUPATION_DATA();
                ocpGroupObj.OCP_GROUP = "";
                ocpGroupObj.GROUP_NAME = "ระบุกลุ่มอาชีพ";
                ocpGroupColl.Add(ocpGroupObj);

                var OcpGroupInfo = from getData in PAR_OCCUPATION_COLL
                                  orderby getData.OCP_GROUP ascending
                                  group getData by getData.OCP_GROUP into g
                                  select g;

                foreach (var ocpGroup in OcpGroupInfo)
                {
                    var obj = new NewBisPASvcRef.OCCUPATION_DATA();
                    obj.OCP_GROUP = ocpGroup.First().OCP_GROUP;
                    obj.GROUP_NAME = ocpGroup.First().GROUP_NAME;
                    ocpGroupColl.Add(obj);
                }

                if (ocpGroupColl != null && ocpGroupColl.Count() > 0)
                {
                    cmbOcpGroup.DataSource = ocpGroupColl;
                    cmbOcpGroup.DisplayMember = "GROUP_NAME";
                    cmbOcpGroup.ValueMember = "OCP_GROUP";
                    cmbOcpGroup.SelectedValue = "";

                    AutoCompleteStringCollection dataOcp = new AutoCompleteStringCollection();
                    foreach(var item in ocpGroupColl)
                    {
                        dataOcp.Add(item.GROUP_NAME);
                    }
                    cmbOcpGroup.AutoCompleteMode = AutoCompleteMode.Suggest;
                    cmbOcpGroup.AutoCompleteSource = AutoCompleteSource.CustomSource;
                    cmbOcpGroup.AutoCompleteCustomSource = dataOcp;


                }
                NewBisPASvcRef.OCCUPATION_DATA oCcupationObj = new NewBisPASvcRef.OCCUPATION_DATA();
                oCcupationObj.CODE = "";
                oCcupationObj.OCP_TYPE_NAME = "ระบุรายละเอียดชั้นอาชีพ";
                PAR_OCCUPATION_COLL.Add(oCcupationObj);
                cmbocpDesc.DataSource = PAR_OCCUPATION_COLL;
                cmbocpDesc.DisplayMember = "OCP_TYPE_NAME";
                cmbocpDesc.ValueMember = "CODE";
                cmbocpDesc.SelectedValue = "";

                NewBisPASvcRef.OCCUPATION_DATA[] pOccupation = new NewBisPASvcRef.OCCUPATION_DATA[PAR_OCCUPATION_COLL.Count];
                PAR_OCCUPATION_COLL.CopyTo(pOccupation);

                cmbParentbocpDesc.DataSource = pOccupation;
                cmbParentbocpDesc.DisplayMember = "OCP_TYPE_NAME";
                cmbParentbocpDesc.ValueMember = "CODE";
                cmbParentbocpDesc.SelectedValue = "";


                AutoCompleteStringCollection data = new AutoCompleteStringCollection();
                foreach (NewBisPASvcRef.OCCUPATION_DATA item in PAR_OCCUPATION_COLL)
                {
                    data.Add(item.DESCRIPTION);
                }
                cmbocpDesc.AutoCompleteMode = AutoCompleteMode.Suggest;
                cmbocpDesc.AutoCompleteSource = AutoCompleteSource.CustomSource;
                cmbocpDesc.AutoCompleteCustomSource = data;


                cmbParentbocpDesc.AutoCompleteMode = AutoCompleteMode.Suggest;
                cmbParentbocpDesc.AutoCompleteSource = AutoCompleteSource.CustomSource;
                cmbParentbocpDesc.AutoCompleteCustomSource = data;
            }

            if (PAR_POSITION_COLL != null && PAR_POSITION_COLL.Count() > 0)
            {
                NewBisPASvcRef.GET_LIST positionObj = new NewBisPASvcRef.GET_LIST();
                positionObj.CODE = "";
                positionObj.DESCRIPTION = "ระบุตำแหน่ง";
                PAR_POSITION_COLL.Add(positionObj);
                cmbPosition.DataSource = PAR_POSITION_COLL;
                cmbPosition.DisplayMember = "DESCRIPTION";
                cmbPosition.ValueMember = "CODE";
                cmbPosition.SelectedValue = "";

                NewBisPASvcRef.GET_LIST[] pPositiob = new NewBisPASvcRef.GET_LIST[PAR_POSITION_COLL.Count];
                PAR_POSITION_COLL.CopyTo(pPositiob);
                cmbParentPosition.DataSource = pPositiob;
                cmbParentPosition.DisplayMember = "DESCRIPTION";
                cmbParentPosition.ValueMember = "CODE";
                cmbParentPosition.SelectedValue = "";

                AutoCompleteStringCollection data = new AutoCompleteStringCollection();
                foreach (NewBisPASvcRef.GET_LIST item in PAR_POSITION_COLL)
                {
                    data.Add(item.DESCRIPTION);
                }
                cmbPosition.AutoCompleteMode = AutoCompleteMode.Suggest;
                cmbPosition.AutoCompleteSource = AutoCompleteSource.CustomSource;
                cmbPosition.AutoCompleteCustomSource = data;

                cmbParentPosition.AutoCompleteMode = AutoCompleteMode.Suggest;
                cmbParentPosition.AutoCompleteSource = AutoCompleteSource.CustomSource;
                cmbParentPosition.AutoCompleteCustomSource = data;
            }

            if (PAR_BUSSINESS_TYPE_COLL != null && PAR_BUSSINESS_TYPE_COLL.Count() > 0)
            {
                NewBisPASvcRef.GET_LIST businessObj = new NewBisPASvcRef.GET_LIST();
                businessObj.CODE = "";
                businessObj.DESCRIPTION = "ระบุลักษณะธุรกิจ";
                PAR_BUSSINESS_TYPE_COLL.Add(businessObj);
                cmbBusinessType.DataSource = PAR_BUSSINESS_TYPE_COLL;
                cmbBusinessType.DisplayMember = "DESCRIPTION";
                cmbBusinessType.ValueMember = "CODE";
                cmbBusinessType.SelectedValue = "";

                NewBisPASvcRef.GET_LIST[] pBusiness = new NewBisPASvcRef.GET_LIST[PAR_BUSSINESS_TYPE_COLL.Count];
                PAR_BUSSINESS_TYPE_COLL.CopyTo(pBusiness);
                cmbParentBusinessType.DataSource = pBusiness;
                cmbParentBusinessType.DisplayMember = "DESCRIPTION";
                cmbParentBusinessType.ValueMember = "CODE";
                cmbParentBusinessType.SelectedValue = "";

                AutoCompleteStringCollection data = new AutoCompleteStringCollection();
                foreach (NewBisPASvcRef.GET_LIST item in PAR_BUSSINESS_TYPE_COLL)
                {
                    data.Add(item.DESCRIPTION);
                }
                cmbBusinessType.AutoCompleteMode = AutoCompleteMode.Suggest;
                cmbBusinessType.AutoCompleteSource = AutoCompleteSource.CustomSource;
                cmbBusinessType.AutoCompleteCustomSource = data;

                cmbParentBusinessType.AutoCompleteMode = AutoCompleteMode.Suggest;
                cmbParentBusinessType.AutoCompleteSource = AutoCompleteSource.CustomSource;
                cmbParentBusinessType.AutoCompleteCustomSource = data;
            }
        }
        private void CreateBaseControlsPlanPage()
        {
            if (PAR_AUTB_DATADIC_DET_COLLECTION != null && PAR_AUTB_DATADIC_DET_COLLECTION.Count() > 0)
            {
                var CtmTypeLing = from ctmTypeLing in PAR_AUTB_DATADIC_DET_COLLECTION
                                  where ctmTypeLing.COL_NAME == "CTM_TYPE"
                                  select ctmTypeLing;
                if (CtmTypeLing != null && CtmTypeLing.Count() > 0)
                {
                    NewBisPASvcRef.AUTB_DATADIC_DET_COLLECTION freeCtmTypeS = new NewBisPASvcRef.AUTB_DATADIC_DET_COLLECTION();
                    freeCtmTypeS.AddRange(CtmTypeLing.ToArray());
                    cmbFreeCtmType.DataSource = freeCtmTypeS;
                    cmbFreeCtmType.DisplayMember = "DESCRIPTION";
                    cmbFreeCtmType.ValueMember = "CODE";
                    cmbFreeCtmType.SelectedValue = "G";

                    NewBisPASvcRef.AUTB_DATADIC_DET[] paidCtmTypeS = new NewBisPASvcRef.AUTB_DATADIC_DET[freeCtmTypeS.Count()];
                    freeCtmTypeS.CopyTo(paidCtmTypeS);
                    cmbPaidCtmType.DataSource = paidCtmTypeS;
                    cmbPaidCtmType.DisplayMember = "DESCRIPTION";
                    cmbPaidCtmType.ValueMember = "CODE";
                    cmbPaidCtmType.SelectedValue = "G";

                }

                var PmodeLinq = from pmodeLinq in PAR_AUTB_DATADIC_DET_COLLECTION
                                where pmodeLinq.COL_NAME == "P_MODE"
                                select pmodeLinq;
                if (PmodeLinq != null && PmodeLinq.Count() > 0)
                {
                    NewBisPASvcRef.AUTB_DATADIC_DET_COLLECTION pmodeS = new NewBisPASvcRef.AUTB_DATADIC_DET_COLLECTION();
                    NewBisPASvcRef.AUTB_DATADIC_DET pmodeObj = new NewBisPASvcRef.AUTB_DATADIC_DET();
                    pmodeObj.CODE = "";
                    pmodeObj.DESCRIPTION = "ระบุงวดการชำระเบี้ย";
                    pmodeS.Add(pmodeObj);
                    pmodeS.AddRange(PmodeLinq.ToArray());
                    cmbFreePmode.DataSource = pmodeS;
                    cmbFreePmode.DisplayMember = "DESCRIPTION";
                    cmbFreePmode.ValueMember = "CODE";
                    cmbFreePmode.SelectedValue = "";

                    NewBisPASvcRef.AUTB_DATADIC_DET[] paidPmodeS = new NewBisPASvcRef.AUTB_DATADIC_DET[pmodeS.Count()];
                    pmodeS.CopyTo(paidPmodeS);
                    cmbPaidPmode.DataSource = paidPmodeS;
                    cmbPaidPmode.DisplayMember = "DESCRIPTION";
                    cmbPaidPmode.ValueMember = "CODE";
                    cmbPaidPmode.SelectedValue = "";

                }
            }

            if (PAR_AUTB_CHANNEL_BLOCK_COLLECTION != null && PAR_AUTB_CHANNEL_BLOCK_COLLECTION.Count() > 0)
            {
                var PlBlockLinq = from plBlockLinq in PAR_AUTB_CHANNEL_BLOCK_COLLECTION
                                  where plBlockLinq.CHANNEL_TYPE == cmbChannelType.SelectedValue.ToString()
                                  && plBlockLinq.WORK_GROUP == cmbWorkGroup.SelectedValue.ToString()
                                  select plBlockLinq;
                if (PlBlockLinq != null && PlBlockLinq.Count() > 0)
                {
                    NewBisPASvcRef.AUTB_CHANNEL_BLOCK_COLLECTION plBlockS = new NewBisPASvcRef.AUTB_CHANNEL_BLOCK_COLLECTION();
                    plBlockS.AddRange(PlBlockLinq.ToArray());
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


        }
        private void CreateBaseControlsBenefitPage()
        {
            if (PAR_RELATION_COLL != null && PAR_RELATION_COLL.Count() > 0)
            {
                var GetData = from getData in PAR_RELATION_COLL
                              orderby getData.RELATION_CODE ascending
                              select getData;
                if (GetData != null && GetData.Count() > 0)
                {
                    AutoCompleteStringCollection data = new AutoCompleteStringCollection();
                    foreach (NewBisPASvcRef.AUTB_RELATION item in GetData)
                    {
                        data.Add(item.RELATION);
                    }
                    txtBenefitRelaton.AutoCompleteMode = AutoCompleteMode.Suggest;
                    txtBenefitRelaton.AutoCompleteSource = AutoCompleteSource.CustomSource;
                    txtBenefitRelaton.AutoCompleteCustomSource = data;
                }
            }

            benefitTypeArr = new NewBisPASvcRef.AUTB_DATADIC_DET[2];
            benefitTypeArr[0] = new NewBisPASvcRef.AUTB_DATADIC_DET();
            benefitTypeArr[0].CODE = "1";
            benefitTypeArr[0].DESCRIPTION = "บุคคล";
            benefitTypeArr[1] = new NewBisPASvcRef.AUTB_DATADIC_DET();
            benefitTypeArr[1].CODE = "2";
            benefitTypeArr[1].DESCRIPTION = "ข้อความ";
            cmbBenefitType.DataSource = benefitTypeArr;
            cmbBenefitType.DisplayMember = "DESCRIPTION";
            cmbBenefitType.ValueMember = "CODE";
            cmbBenefitType.SelectedValue = "1";
            SetItemBnfPerson();

            benefitClearArr = new NewBisPASvcRef.AUTB_DATADIC_DET[2];
            benefitClearArr[0] = new NewBisPASvcRef.AUTB_DATADIC_DET();
            benefitClearArr[0].CODE = "Y";
            benefitClearArr[0].DESCRIPTION = "CLEAR";
            benefitClearArr[1] = new NewBisPASvcRef.AUTB_DATADIC_DET();
            benefitClearArr[1].CODE = "N";
            benefitClearArr[1].DESCRIPTION = "NOT CLEAR";
            cmbBenefitClearFlag.DataSource = benefitClearArr;
            cmbBenefitClearFlag.DisplayMember = "DESCRIPTION";
            cmbBenefitClearFlag.ValueMember = "CODE";
            cmbBenefitClearFlag.SelectedValue = "Y";

            spouseFlgArr = new NewBisPASvcRef.AUTB_DATADIC_DET[2];
            spouseFlgArr[0] = new NewBisPASvcRef.AUTB_DATADIC_DET();
            spouseFlgArr[0].CODE = "";
            spouseFlgArr[0].DESCRIPTION = "ผู้เอาประกัน";
            spouseFlgArr[1] = new NewBisPASvcRef.AUTB_DATADIC_DET();
            spouseFlgArr[1].CODE = "Y";
            spouseFlgArr[1].DESCRIPTION = "คู่สมรส";
            cmbSpouseFlg.DataSource = spouseFlgArr;
            cmbSpouseFlg.DisplayMember = "DESCRIPTION";
            cmbSpouseFlg.ValueMember = "CODE";
            cmbSpouseFlg.SelectedValue = "";

        }
        private void CreateBaseControlsCreditCard()
        {
            NewBisPASvcRef.GET_LIST[] payOptionArr = new NewBisPASvcRef.GET_LIST[3];
            payOptionArr[0] = new NewBisPASvcRef.GET_LIST();
            payOptionArr[0].CODE = "";
            payOptionArr[0].DESCRIPTION = "ระบุประเภทการชำระ";
            payOptionArr[1] = new NewBisPASvcRef.GET_LIST();
            payOptionArr[1].CODE = "08";
            payOptionArr[1].DESCRIPTION = "บัตรเครดิต";
            payOptionArr[2] = new NewBisPASvcRef.GET_LIST();
            payOptionArr[2].CODE = "RE";
            payOptionArr[2].DESCRIPTION = "Recurring";
            cmbPayOption.DataSource = payOptionArr;
            cmbPayOption.DisplayMember = "DESCRIPTION";
            cmbPayOption.ValueMember = "CODE";
            cmbPayOption.SelectedValue = "RE";
        }
        private void CreateBaseControlsUnderWrite()
        {
            if (PAR_UNDERWRITER_COLL != null && PAR_UNDERWRITER_COLL.Count() > 0)
            {
                cmbUnderWriteID.DataSource = PAR_UNDERWRITER_COLL;
                cmbUnderWriteID.DisplayMember = "UND_NAME";
                cmbUnderWriteID.ValueMember = "UND_ID";
                cmbUnderWriteID.SelectedValue = UserID;
            }
            if (PAR_AUTB_STATUS_COLLECTION != null && PAR_AUTB_STATUS_COLLECTION.Count() > 0)
            {
                NewBisPASvcRef.AUTB_STATUS_COLLECTION statusColl = new NewBisPASvcRef.AUTB_STATUS_COLLECTION();
                NewBisPASvcRef.AUTB_STATUS statusObj = new NewBisPASvcRef.AUTB_STATUS();
                statusObj.STATUS = "";
                statusObj.STATUS_DESCRIPTION = "เลือกสถานะที่ต้องการ";
                statusColl.Add(statusObj);
                statusColl.AddRange(PAR_AUTB_STATUS_COLLECTION.ToArray());
                cmbSummryStatus.DataSource = statusColl;
                cmbSummryStatus.DisplayMember = "STATUS_DESCRIPTION";
                cmbSummryStatus.ValueMember = "STATUS";
                cmbSummryStatus.SelectedValue = "";
                GetControlsUnderStatus("", "");
            }

            NewBisPASvcRef.AUTB_DATADIC_DET[] mibArr = new NewBisPASvcRef.AUTB_DATADIC_DET[3];
            mibArr[0] = new NewBisPASvcRef.AUTB_DATADIC_DET();
            mibArr[0].CODE = "";
            mibArr[0].DESCRIPTION = "เลือกประเภทลูกค้าเพื่อทำการบันทึก MIB";
            mibArr[1] = new NewBisPASvcRef.AUTB_DATADIC_DET();
            mibArr[1].CODE = "C";
            mibArr[1].DESCRIPTION = "ผู้เอาประกัน";
            mibArr[2] = new NewBisPASvcRef.AUTB_DATADIC_DET();
            mibArr[2].CODE = "S";
            mibArr[2].DESCRIPTION = "คู่สมรส";
            cmbMIB.DataSource = mibArr;
            cmbMIB.DisplayMember = "DESCRIPTION";
            cmbMIB.ValueMember = "CODE";
            cmbMIB.SelectedValue = "";
        }
        private void CreateBaseControlsMemoPage()
        {
            if (PAR_AUTB_DATADIC_DET_COLLECTION != null && PAR_AUTB_DATADIC_DET_COLLECTION.Count() > 0)
            {
                var GetMemoProcessEnd = from getMemoProcessEnd in PAR_AUTB_DATADIC_DET_COLLECTION
                                        where getMemoProcessEnd.COL_NAME == "END_PROCESS"
                                        select getMemoProcessEnd;
                if (GetMemoProcessEnd != null && GetMemoProcessEnd.Count() > 0)
                {
                    NewBisPASvcRef.AUTB_DATADIC_DET_COLLECTION memoProcessEndS = new NewBisPASvcRef.AUTB_DATADIC_DET_COLLECTION();
                    NewBisPASvcRef.AUTB_DATADIC_DET memoProcessEnd = new NewBisPASvcRef.AUTB_DATADIC_DET();
                    memoProcessEnd.CODE = "";
                    memoProcessEnd.DESCRIPTION = "ระบุขั้นตอนดำเนินการ";
                    memoProcessEndS.Add(memoProcessEnd);
                    memoProcessEndS.AddRange(GetMemoProcessEnd.ToArray());
                    cmbMemoIDEndProcess.DataSource = memoProcessEndS;
                    cmbMemoIDEndProcess.DisplayMember = "DESCRIPTION";
                    cmbMemoIDEndProcess.ValueMember = "CODE";
                    cmbMemoIDEndProcess.SelectedValue = "";
                }

                var GetPendStatus = from getPendStatus in PAR_AUTB_DATADIC_DET_COLLECTION
                                    where getPendStatus.COL_NAME == "PEND_STATUS"
                                    select getPendStatus;
                if (GetPendStatus != null && GetPendStatus.Count() > 0)
                {
                    NewBisPASvcRef.AUTB_DATADIC_DET_COLLECTION pendStatusS = new NewBisPASvcRef.AUTB_DATADIC_DET_COLLECTION();
                    NewBisPASvcRef.AUTB_DATADIC_DET pendStatusObj = new NewBisPASvcRef.AUTB_DATADIC_DET();
                    pendStatusObj.CODE = "";
                    pendStatusObj.DESCRIPTION = "ระบุสถานะเอกสาร";
                    pendStatusS.Add(pendStatusObj);
                    pendStatusS.AddRange(GetPendStatus.ToArray());
                    cmbMemoDetailPendStatus.DataSource = pendStatusS;
                    cmbMemoDetailPendStatus.DisplayMember = "DESCRIPTION";
                    cmbMemoDetailPendStatus.ValueMember = "CODE";
                    cmbMemoDetailPendStatus.SelectedValue = "W";
                }
            }
            if (PAR_PEND_REQM_COLL != null && PAR_PEND_REQM_COLL.Count() > 0)
            {
                var GetData = from getData in PAR_PEND_REQM_COLL
                              orderby getData.DESCRIPTION ascending
                              select getData;
                if (GetData != null && GetData.Count() > 0)
                {
                    NewBisPASvcRef.AUTB_PEND_REQM_COLLECTION pendReqmS = new NewBisPASvcRef.AUTB_PEND_REQM_COLLECTION();
                    NewBisPASvcRef.AUTB_PEND_REQM pendReqmObj = new NewBisPASvcRef.AUTB_PEND_REQM();
                    pendReqmObj.PEND_CODE = "";
                    pendReqmObj.DESCRIPTION = "ระบุรายละเอียดของรหัสการขอเอกสารเพิ่ม";
                    pendReqmS.Add(pendReqmObj);
                    pendReqmS.AddRange(GetData.ToArray());
                    cmbMemoDetailPendCodeDesc.DataSource = pendReqmS;
                    cmbMemoDetailPendCodeDesc.DisplayMember = "DESCRIPTION";
                    cmbMemoDetailPendCodeDesc.ValueMember = "PEND_CODE";
                    cmbMemoDetailPendCodeDesc.SelectedValue = "";
                }
            }
        }
        private void SetMsgException(Exception ex)
        {
            MessageBox.Show(ex.ToString());
            return;
        }
        private void ChkDateForTextBox(String strDate, String msg, Control textBox)
        {
            if (strDate != "" && cmbChannelType.SelectedValue != "PD")
            {
                DateTime? d = Utility.StringToDateTime(strDate, "BU");
                if (d == null)
                {
                    textBox.Text = "";
                    MessageBox.Show("รูปแบบวันที่ " + msg + " ไม่ถูกต้อง");
                    textBox.Focus();
                    return;
                }
            }
        }
        private void ChkIntForTextBox(String strInt, String msg, Control textBox)
        {
            if (strInt != "")
            {
                Decimal i = 0; //กำหนดชนิดของข้อมูลเป็น int เพื่อเปรียบเทียบ กับค่าที่ใส่เข้ามาใน TextBox ต่างๆ
                if (!Decimal.TryParse(strInt, out i))//ตรวจสอบค่าที่ใส่ให้กับ textbox ว่าตรงกับข้อมูลของตัวแปร i (int)หรือไม่ ถ้าไม่ตรงก็ให้แสดงข้อความออกมา
                {
                    textBox.Text = "";
                    MessageBox.Show("กรุณาระบุ " + msg + " เป็นตัวเลข");
                    textBox.Focus();
                    return;
                }

            }
        }
        private void GetControlsUnderStatus(String status, String subStatus)
        {
            if (subStatus == "")
            {
                if (status == "AP")
                {
                    subStatus = "AS";
                }
                else
                {
                    subStatus = status;
                }

            }

            if (PAR_AUTB_SUBSTATUS_COLLECTION != null && PAR_AUTB_SUBSTATUS_COLLECTION.Count() > 0)
            {

                var GetSubStatus = from getSubStatus in PAR_AUTB_SUBSTATUS_COLLECTION
                                   where getSubStatus.STATUS == status
                                   select getSubStatus;
                if (GetSubStatus != null && GetSubStatus.Count() > 0)
                {
                    NewBisPASvcRef.AUTB_SUBSTATUS_COLLECTION subStatusColl = new NewBisPASvcRef.AUTB_SUBSTATUS_COLLECTION();
                    NewBisPASvcRef.AUTB_SUBSTATUS subStatusObj = new NewBisPASvcRef.AUTB_SUBSTATUS();
                    subStatusObj.SUBSTATUS = "";
                    subStatusObj.SUBSTATUS_DESCRIPTION = "เลือกสถานะย่อยที่ต้องการ";
                    subStatusColl.Add(subStatusObj);
                    subStatusColl.AddRange(GetSubStatus.ToArray());

                    /*เพิ่มสถานะย่อยของสถานะ IF ของงาน PA บัตรเครดิต*/
                    if (status == "IF")
                    {
                        NewBisPASvcRef.AUTB_SUBSTATUS subStatusIFObj = new NewBisPASvcRef.AUTB_SUBSTATUS();
                        subStatusIFObj.SUBSTATUS = "CC";
                        subStatusIFObj.SUBSTATUS_DESCRIPTION = "(CC) ออกกรมธรรม์โดยยกเลิกกรมธรรม์แถมฟรีของ PA บัตรเครดิต";
                        subStatusColl.Add(subStatusIFObj);
                    }
                    /*เพิ่มสถานะย่อยของสถานะ IF ของงาน PA บัตรเครดิต*/

                    cmbSummrySubStatus.DataSource = subStatusColl;
                    cmbSummrySubStatus.DisplayMember = "SUBSTATUS_DESCRIPTION";
                    cmbSummrySubStatus.ValueMember = "SUBSTATUS";
                    cmbSummrySubStatus.SelectedValue = subStatus;
                    if (cmbSummrySubStatus.SelectedValue == null)
                    {
                        cmbSummrySubStatus.SelectedValue = "";
                    }
                }
                else
                {
                    NewBisPASvcRef.AUTB_SUBSTATUS_COLLECTION subStatusColl = new NewBisPASvcRef.AUTB_SUBSTATUS_COLLECTION();
                    NewBisPASvcRef.AUTB_SUBSTATUS subStatusObj = new NewBisPASvcRef.AUTB_SUBSTATUS();
                    subStatusObj.SUBSTATUS = "";
                    subStatusObj.SUBSTATUS_DESCRIPTION = "เลือกสถานะย่อยที่ต้องการ";
                    subStatusColl.Add(subStatusObj);
                    cmbSummrySubStatus.DataSource = subStatusColl;
                    cmbSummrySubStatus.DisplayMember = "SUBSTATUS_DESCRIPTION";
                    cmbSummrySubStatus.ValueMember = "SUBSTATUS";
                    cmbSummrySubStatus.SelectedValue = subStatus;

                }
            }

            if (PAR_STATUS_CAUSE_COLL != null && PAR_STATUS_CAUSE_COLL.Count() > 0)
            {
                var GetStausCause = from getStausCause in PAR_STATUS_CAUSE_COLL
                                    where getStausCause.STATUS == status
                                    select getStausCause;
                if (GetStausCause != null && GetStausCause.Count() > 0)
                {
                    lblStatusCause.Visible = true;
                    cmbSummaryStatusCause.Visible = true;
                    NewBisPASvcRef.AUTB_STATUS_CAUSE_COLLECTION statusCauseColl = new NewBisPASvcRef.AUTB_STATUS_CAUSE_COLLECTION();
                    NewBisPASvcRef.AUTB_STATUS_CAUSE statusCauseObj = new NewBisPASvcRef.AUTB_STATUS_CAUSE();
                    statusCauseObj.STATUS_CAUSE = "";
                    statusCauseObj.DESCRIPTION = "เลือกสาเหตุของสถานะ";
                    statusCauseColl.Add(statusCauseObj);
                    statusCauseColl.AddRange(GetStausCause);
                    cmbSummaryStatusCause.DataSource = statusCauseColl;
                    cmbSummaryStatusCause.DisplayMember = "DESCRIPTION";
                    cmbSummaryStatusCause.ValueMember = "STATUS_CAUSE";
                    cmbSummaryStatusCause.SelectedValue = "";
                }
                else
                {
                    //lblStatusCause.Visible = false;
                    //cmbSummaryStatusCause.Visible = false;
                    //NewBisPASvcRef.AUTB_STATUS_CAUSE_COLLECTION statusCauseColl = new NewBisPASvcRef.AUTB_STATUS_CAUSE_COLLECTION();
                    //NewBisPASvcRef.AUTB_STATUS_CAUSE statusCauseObj = new NewBisPASvcRef.AUTB_STATUS_CAUSE();
                    //statusCauseObj.STATUS_CAUSE = "";
                    //statusCauseObj.DESCRIPTION = "เลือกสาเหตุของสถานะ";
                    //statusCauseColl.Add(statusCauseObj);
                    //cmbSummaryStatusCause.DataSource = statusCauseColl;
                    //cmbSummaryStatusCause.DisplayMember = "DESCRIPTION";
                    //cmbSummaryStatusCause.ValueMember = "STATUS_CAUSE";
                    //cmbSummaryStatusCause.SelectedValue = "";
                    lblStatusCause.Visible = false;
                    cmbSummaryStatusCause.Visible = false;
                    cmbSummaryStatusCause.DataSource = null;
                }
            }

            lblPPUntil.Visible = false;
            cmbPPUntil.Visible = false;
            cmbPPUntil.DataSource = null;
        }
        private void getRegistryValue()
        {
            RegistryKey localMachineReg = Registry.LocalMachine;

            RegistryKey softwareReg = localMachineReg.OpenSubKey("Software", true);

            RegistryKey bangkokLifeAssuranceReg = softwareReg.OpenSubKey("Bangkok Life Assurance", true);
            if (bangkokLifeAssuranceReg == null)
            {
                if (bangkokLifeAssuranceReg == null)
                {
                    RegistryKey a = softwareReg.OpenSubKey("Wow6432Node", true);

                    bangkokLifeAssuranceReg = a.OpenSubKey("Bangkok Life Assurance", true);
                    if (bangkokLifeAssuranceReg == null)
                        throw new Exception("กรุณาเข้าใช้ระบบผ่านทางเมนูระบบงาน !");
                }
            }


            RegistryKey blaWinAppMenuReg = bangkokLifeAssuranceReg.OpenSubKey("BlaWinAppMenu", true);
            if (blaWinAppMenuReg == null)
                throw new Exception("กรุณาเข้าใช้ระบบผ่านทางเมนูระบบงาน !");

            UserID = (string)blaWinAppMenuReg.GetValue("UserID");
            if (UserID == null)
                throw new Exception("กรุณาเข้าใช้ระบบผ่านทางเมนูระบบงาน !");

            //Password = (string)blaWinAppMenuReg.GetValue("Password");
            //if (Password == null)
            //    throw new Exception("กรุณาเข้าใช้ระบบผ่านทางเมนูระบบงาน !");
            //MenuLoginDate = Convert.ToDateTime((string)blaWinAppMenuReg.GetValue("LoginDate"));
            //if (MenuLoginDate == new DateTime())
            //    throw new Exception("กรุณาเข้าใช้ระบบผ่านทางเมนูระบบงาน !");
            //StartCommand = (string)blaWinAppMenuReg.GetValue("StartCommand");

            blaWinAppMenuReg.Close();
            bangkokLifeAssuranceReg.Close();
            softwareReg.Close();
            localMachineReg.Close();

        }
    }
}
