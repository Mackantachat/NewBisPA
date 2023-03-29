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
        private void ValidateParameter()
        {
            if (VerifyFlag == false)
            {
                tabMain.SelectTab("tabCustomerData");
                txtIdcardNo.Focus();
                throw new Exception("กรุณาทำการตรวจสอบข้อมูลลูกค้าก่อนที่จะทำการบันทึกข้อมูล ตรวจสอบโดยกด Enter ที่ช่องบัตรประชาชน");
            }
            ValidateHeader();
            ValidateCustomerData();
            ValidateOccupation();
            ValidateAddressData();

            if (cmbChannelType.SelectedValue.ToString() == "KB")
            {
                ValidateAccount();
            }

            if (PAR_PA_APPLICATION_ID.APP_ID == null)
            {
                if (PLAN_ERROR == true)
                {
                    tabMain.SelectTab("tabPlanData");
                    btnCalPremium.Focus();
                    throw new Exception("กรุณาทำการคำนวนเบี้ยประกันก่อนทำการบันทึกข้อมูล");
                }

                if (cmbChannelType.SelectedValue.ToString() == "PD")
                {

                    if (string.IsNullOrEmpty(cmbPayOption.SelectedValue.ToString()))
                    {
                        cmbPayOption.Focus();
                        throw new Exception("กรุณาระบุประเภทการชำระบัตร");
                    }
                    if (string.IsNullOrEmpty(txtCardNo1.Text)  || txtCardNo1.Text.Length != 4)
                    {
                        cmbPayOption.Focus();
                        throw new Exception("กรุณาระบุเลขที่บัตรเครดิตช่องที่ 1 ให้ครบถ้วนและถูกต้อง");
                    }
                    if (string.IsNullOrEmpty(txtCardNo2.Text) || txtCardNo2.Text.Length != 4)
                    {
                        cmbPayOption.Focus();
                        throw new Exception("กรุณาระบุเลขที่บัตรเครดิตช่องที่ 2 ให้ครบถ้วนและถูกต้อง");
                    }
                    if (string.IsNullOrEmpty(txtCardNo3.Text) || txtCardNo3.Text.Length != 4)
                    {
                        cmbPayOption.Focus();
                        throw new Exception("กรุณาระบุเลขที่บัตรเครดิตช่องที่ 3 ให้ครบถ้วนและถูกต้อง");
                    }
                    if (string.IsNullOrEmpty(txtCardNo4.Text) || txtCardNo4.Text.Length != 4)
                    {
                        cmbPayOption.Focus();
                        throw new Exception("กรุณาระบุเลขที่บัตรเครดิตช่องที่ 4 ให้ครบถ้วนและถูกต้อง");
                    }
                }
            }
            else
            {
                if (PLAN_ERROR == true)
                {
                    tabMain.SelectTab("tabPlanData");
                    btnCalPremium.Focus();
                    throw new Exception("กรุณาทำการคำนวนเบี้ยประกันก่อนทำการบันทึกข้อมูล");
                }
                if (txtBirthDt.Text != OLD_BIRTH_DATE)
                {
                    tabMain.SelectTab("tabPlanData");
                    btnCalPremium.Focus();
                    throw new Exception("เนื่องจากมีการเปลี่ยนแปลงวันเกิดของผู้เอาประกัน กรุณาทำการคำนวนเบี้ยประกันก่อนทำการบันทึกข้อมูล");
                }
                if (ClickTabPlanData == true)
                {
                    if (cmbChannelType.SelectedValue.ToString() == "PD" || cmbChannelType.SelectedValue.ToString() == "PF")
                    {
                        if (ckbLifeFree.Checked != OLD_PLAN_FREE)
                        {
                            tabMain.SelectTab("tabPlanData");
                            btnCalPremium.Focus();
                            throw new Exception("เนื่องจากมีการเปลี่ยนแปลงของแบบประกันแถมฟรี กรุณาทำการคำนวนเบี้ยประกันก่อนทำการบันทึกข้อมูล");
                        }
                        if (ckbLifeBuy.Checked != OLD_PLAN_PAID)
                        {
                            tabMain.SelectTab("tabPlanData");
                            btnCalPremium.Focus();
                            throw new Exception("เนื่องจากมีการเปลี่ยนแปลงของแบบประกันแบบซื้อ กรุณาทำการคำนวนเบี้ยประกันก่อนทำการบันทึกข้อมูล");
                        }
                        if (ckbSpouse.Checked != OLD_SPOUSE)
                        {
                            tabMain.SelectTab("tabPlanData");
                            btnCalPremium.Focus();
                            throw new Exception("เนื่องจากมีการเปลี่ยนแปลงของคู่สมรส กรุณาทำการคำนวนเบี้ยประกันก่อนทำการบันทึกข้อมูล");
                        }
                        String planCodeFree = txtFreePlcode.Text.Trim() + txtFreePlcode2.Text.Trim();

                        if (planCodeFree != OLD_PLAN_CODE_FREE)
                        {
                            tabMain.SelectTab("tabPlanData");
                            btnCalPremium.Focus();
                            throw new Exception("เนื่องจากมีการเปลี่ยนแปลงรหัสของแบบประกันแถมฟรี กรุณาทำการคำนวนเบี้ยประกันก่อนทำการบันทึกข้อมูล");
                        }
                        if (cmbFreePmode.SelectedValue.ToString() != OLD_MODE_FREE)
                        {
                            tabMain.SelectTab("tabPlanData");
                            btnCalPremium.Focus();
                            throw new Exception("เนื่องจากมีการเปลี่ยนแปลงงวดการชำระของแบบประกันแถมฟรี กรุณาทำการคำนวนเบี้ยประกันก่อนทำการบันทึกข้อมูล");
                        }
                        if (txtFreeSumm.Text.Trim().Replace(",", "") != OLD_SUMM_FREE.Replace(",", ""))
                        {
                            tabMain.SelectTab("tabPlanData");
                            btnCalPremium.Focus();
                            throw new Exception("เนื่องจากมีการเปลี่ยนแปลงทุนประกันของแบบประกันแถมฟรี กรุณาทำการคำนวนเบี้ยประกันก่อนทำการบันทึกข้อมูล");
                        }
                        if (txtFreeIsuDate.Text.Trim() != OLD_ISU_DT_FREE)
                        {
                            tabMain.SelectTab("tabPlanData");
                            btnCalPremium.Focus();
                            throw new Exception("เนื่องจากมีการเปลี่ยนแปลงวันที่คุ้มครองของแบบประกันแถมฟรี กรุณาทำการคำนวนเบี้ยประกันก่อนทำการบันทึกข้อมูล");
                        }
                        if (txtSpBirthDate.Text.Trim() != OLD_BIRTH_DATE_SPOUSE)
                        {
                            tabMain.SelectTab("tabPlanData");
                            btnCalPremium.Focus();
                            throw new Exception("เนื่องจากมีการเปลี่ยนแปลงข้อมูลวันเกิดของคู่สมรส กรุณาทำการคำนวนเบี้ยประกันก่อนทำการบันทึกข้อมูล");
                        }
                        if (ckbLifeFree.Checked == true && ckbLifeBuy.Checked == true)
                        {
                            if (txtFreeIsuDate.Text.Trim() != txtPaidIsuDate.Text.Trim())
                            {
                                tabMain.SelectTab("tabPlanData");
                                btnCalPremium.Focus();
                                throw new Exception("วันเริ่มคุ้มครองของแบบประกันฟรีไม่ตรงกับแบบซื้อ");
                            }
                        }
                    }

                    String planCodePaid = txtPaidPlcode.Text.Trim() + txtPaidPlcode2.Text.Trim();

                    if (planCodePaid != OLD_PLAN_CODE_PAID)
                    {
                        tabMain.SelectTab("tabPlanData");
                        btnCalPremium.Focus();
                        throw new Exception("เนื่องจากมีการเปลี่ยนแปลงรหัสของแบบประกันแบบซื้อ กรุณาทำการคำนวนเบี้ยประกันก่อนทำการบันทึกข้อมูล");
                    }
                    if (cmbPaidPmode.SelectedValue.ToString() != OLD_MODE_PAID)
                    {
                        tabMain.SelectTab("tabPlanData");
                        btnCalPremium.Focus();
                        throw new Exception("เนื่องจากมีการเปลี่ยนแปลงงวดการชำระของแบบประกันแบบซื้อ กรุณาทำการคำนวนเบี้ยประกันก่อนทำการบันทึกข้อมูล");
                    }
                    if (txtPaidSumm.Text.Trim().Replace(",", "") != OLD_SUMM_PAID.Replace(",", ""))
                    {
                        tabMain.SelectTab("tabPlanData");
                        btnCalPremium.Focus();
                        throw new Exception("เนื่องจากมีการเปลี่ยนแปลงทุนประกันของแบบประกันแบบซื้อ กรุณาทำการคำนวนเบี้ยประกันก่อนทำการบันทึกข้อมูล");
                    }
                    if (txtPaidIsuDate.Text.Trim() != OLD_ISU_DT_PAID)
                    {
                        tabMain.SelectTab("tabPlanData");
                        btnCalPremium.Focus();
                        throw new Exception("เนื่องจากมีการเปลี่ยนแปลงวันที่คุ้มครองของแบบประกันแบบซื้อ กรุณาทำการคำนวนเบี้ยประกันก่อนทำการบันทึกข้อมูล");
                    }

                }

            }

            if (PAR_P_NAME_ID_COLL != null && PAR_P_NAME_ID_COLL.Count() > 0)
            {
                if (PAR_P_NAME_ID_COLL.Count() > 1)
                {
                    tabMain.SelectTab("tabCustomerData");
                    txtIdcardNo.Focus();
                    throw new Exception("ไม่สามารถบันทึกข้อมูลได้เนื่องจากลูกค้ามีข้อมูลเดิมมากกว่า 1 ข้อมูล กรุณาติดต่อ IT Tel.8518");
                }
                else
                {
                    if (cmbDataCus.SelectedValue.ToString() == "2")
                    {
                        tabMain.SelectTab("tabCustomerData");
                        txtIdcardNo.Focus();
                        throw new Exception("ไม่สามารถบันทึกข้อมูลได้เนื่องจากลูกค้ามีข้อมูลเดิมแต่ท่านไม่เลือกลูกค้าเป็นลูกค้าเก่า กรุณาตรวจสอบข้อมูล");
                    }
                    if (NAME_ID == null)
                    {
                        tabMain.SelectTab("tabCustomerData");
                        txtIdcardNo.Focus();
                        throw new Exception("ไม่สามารถบันทึกข้อมูลได้เนื่องจากลูกค้ามีข้อมูลเดิมแต่ท่านไม่เลือกลูกค้าเป็นลูกค้าเก่า กรุณาตรวจสอบข้อมูล");
                    }
                }
            }

            if (PAR_P_PARENT_ID_COLL != null && PAR_P_PARENT_ID_COLL.Count() > 0)
            {
                if (PARENT_ID == null)
                {
                    tabMain.SelectTab("tabCustomerData");
                    txtIdcardNo.Focus();
                    throw new Exception("ไม่สามารถบันทึกข้อมูลได้เนื่องจากลูกค้ามีข้อมูลเดิมของผู้ปกครองแต่ท่านไม่เลือกข้อมูลผู้ปกครองเป็นลูกค้าเก่า กรุณาตรวจสอบข้อมูล");
                }
            }
        }
        private void ValidateHeader()
        {
            if (cmbChannelType.SelectedValue.ToString() == "")
            {
                cmbChannelType.Focus();
                throw new Exception("ระบุ ช่องทางการขาย ที่ท่านต้องการ");
            }
            if (cmbWorkGroup.SelectedValue.ToString() == "")
            {
                cmbWorkGroup.Focus();
                throw new Exception("ระบุ กลุ่มงาน ที่ท่านต้องการ");
            }
            if (txtAppNo.Text.Trim() == "")
            {
                txtAppNo.Focus();
                throw new Exception("ระบุเลขที่ใบคำขอที่ท่านต้องการ");
            }

            if (cmbChannelType.SelectedValue.ToString() != "PD" && cmbChannelType.SelectedValue.ToString() != "PF")
            {
                if (txtSaleAgent.Text.Trim() == "" || txtSaleAgentName.Text.Trim() == "")
                {
                    txtSaleAgent.Focus();
                    throw new Exception("ระบุ รหัสตัวแทน ที่ท่านต้องการ");
                }
                //if (txtSaleAgentUpl.Text.Trim() == "" || txtSaleAgentUplName.Text.Trim() == "")
                //{
                //    txtSaleAgentUpl.Focus();
                //    throw new Exception("ระบุ รหัสหน่วยตัวแทน ที่ท่านต้องการ");
                //}
                if (txtLicenseAgent.Text.Trim() == "" || txtLicenseAgentName.Text.Trim() == "")
                {
                    txtLicenseAgent.Focus();
                    throw new Exception("ระบุ รหัสตัวแทนผ่านบัตร ที่ท่านต้องการ");
                }
            }

            if (txtAppOfcRcvDt.Text.Trim() == "")
            {
                txtAppOfcRcvDt.Focus();
                throw new Exception("ระบุ วันที่สาขาลงทะเบียน ที่ท่านต้องการ");
            }
            if (txtAppHoRcvDt.Text.Trim() == "")
            {
                txtAppHoRcvDt.Focus();
                throw new Exception("ระบุ วันที่สนญ.รับใบคำขอ ที่ท่านต้องการ");
            }
            if (txtAppOfc.Text.Trim() == "")
            {
                txtAppOfc.Focus();
                throw new Exception("ระบุ รหัสสาขาส่งคำขอ ที่ท่านต้องการ");
            }
            if (txtAppDt.Text.Trim() == "")
            {
                txtAppDt.Focus();
                throw new Exception("ระบุ วันที่ใบคำขอประจำวันที่ ที่ท่านต้องการ");
            }
            if (txtAppSignDt.Text.Trim() == "")
            {
                txtAppSignDt.Focus();
                throw new Exception("ระบุ วันที่วันที่ลงนาม ที่ท่านต้องการ");
            }
            if (cmbSendTo.SelectedValue.ToString() == "")
            {
                cmbSendTo.Focus();
                throw new Exception("ระบุ วิธีส่งกรมธรรม์ ที่ท่านต้องการ");
            }
            if (cmbSendTo.SelectedValue.ToString() == "O" && txtSendOfc.Text.Trim() == "")
            {
                txtSendOfc.Focus();
                throw new Exception("ระบุ รหัสสาขาที่ต้องการส่งกรมธรรม์ ที่ท่านต้องการ");
            }
            if (cmbDocumentFlag.SelectedValue.ToString() == "")
            {
                cmbDocumentFlag.Focus();
                throw new Exception("ระบุ สถานะเอกสาร ที่ท่านต้องการ");
            }
            if (cmbUnderWrite.SelectedValue.ToString() == "")
            {
                cmbUnderWrite.Focus();
                throw new Exception("ระบุ เลือกผู้ที่จะพิจารณา ที่ท่านต้องการ");
            }
            if (txtAppSysDt.Text.Trim() == "")
            {
                txtAppSysDt.Focus();
                throw new Exception("ระบุ วันที่บันทึกใบคำขอ ที่ท่านต้องการ");
            }

            var GetData = from getData in PAR_AUTB_APPNAME_ID_COLLECTION
                          where getData.CHANNEL_TYPE == cmbChannelType.SelectedValue.ToString()
                          select getData;
            if (GetData != null && GetData.Count() > 0)
            {
                if (cmbAppNameID.SelectedValue != null && cmbAppNameID.SelectedValue.ToString() == "0")
                {
                    cmbAppNameID.Focus();
                    throw new Exception("กรุณาระบุเอกสารชุดใบคำขอที่ต้องการ");
                }
            }

            if (cmbChannelType.SelectedValue.ToString() == "PF")
            {

                string policyDocumentType = cmbPolicyDocmentType.SelectedValue == null ? "" : cmbPolicyDocmentType.SelectedValue.ToString();
                string ePolicySendingType = cmbEPolicySending.SelectedValue == null ? "" : cmbEPolicySending.SelectedValue.ToString();

                if (!string.IsNullOrEmpty(policyDocumentType))
                {
                    if (policyDocumentType != "D")

                        if (string.IsNullOrEmpty(ePolicySendingType))
                        {
                            cmbEPolicySending.Focus();
                            throw new Exception("กรุณาเลือกวิธีส่ง กธ ePolicy");
                        }
                }
                else
                {
                    cmbPolicyDocmentType.Focus();
                    throw new Exception("กรุณาเลือกประเภทชุดกรมธรรม์");
                }
            }

        }
        private void ValidateCustomerData()
        {
            //if (ckbIdcardDoc.Checked == false && ckbAddressDoc.Checked == false)
            //{
            //    ckbIdcardDoc.Focus();
            //    throw new Exception("ระบุ เอกสารแนบ ที่ท่านต้องการ");
            //}
            //if (cmbCardType.SelectedValue.ToString() == "")
            //{
            //    cmbCardType.Focus();
            //    throw new Exception("ระบุ ประเภทบัตร ที่ท่านต้องการ");
            //}
            //if (txtIdcardNo.Text.Trim() == "")
            //{
            //    txtIdcardNo.Focus();
            //    throw new Exception("ระบุ เลขทีบัตร ที่ท่านต้องการ");
            //}
            if (txtIdcardNo.Text.Trim() != "" && cmbCardType.SelectedValue.ToString() == "")
            {
                cmbCardType.Focus();
                throw new Exception("ระบุประเภทของบัตร");
            }
            if (cmbCardType.SelectedValue.ToString() == "1" && txtIdcardNo.Text.Trim() != "")
            {
                if (Utility.ValidateIDCard(txtIdcardNo.Text) == false)
                {
                    txtIdcardNo.Focus();
                    throw new Exception("รูปแบบของเลขที่บัตรประชาชนไม่ถูกต้อง");
                }
            }
            if (cmbDataCus.SelectedValue.ToString() == "")
            {
                cmbDataCus.Focus();
                throw new Exception("ระบุ ข้อมูลลูกค้า ที่ท่านต้องการ");
            }
            if (txtPrename.Text.Trim() == "")
            {
                txtPrename.Focus();
                throw new Exception("ระบุ คำนำหน้าชื่อ ที่ท่านต้องการ");
            }
            if (txtName.Text.Trim() == "")
            {
                txtName.Focus();
                throw new Exception("ระบุ ชื่อลูกค้า ที่ท่านต้องการ");
            }
            if (cmbSex.SelectedValue.ToString() == "")
            {
                cmbSex.Focus();
                throw new Exception("ระบุ เพศของลูกค้า ที่ท่านต้องการ");
            }
            if (txtBirthDt.Text.Trim() == "")
            {
                txtBirthDt.Focus();
                throw new Exception("ระบุ วันเกิดของลูกค้า ที่ท่านต้องการ");
            }
            if (txtBirthDt.Text.Trim() != "")
            {
                DateTime? d = Utility.StringToDateTime(txtBirthDt.Text.Trim(), "BU");
                if (d == null)
                {
                    txtBirthDt.Focus();
                    MessageBox.Show("รูปแบบวันที่ ของวันเกิด ไม่ถูกต้อง");

                }
            }
            if (txtStAge.Text.Trim() == "")
            {
                txtStAge.Focus();
                throw new Exception("ระบุ อายุของลูกค้า ที่ท่านต้องการ");
            }
            if (cmbNationality.SelectedValue.ToString() == "")
            {
                cmbNationality.Focus();
                throw new Exception("ระบุ สัญชาติของลูกค้า ที่ท่านต้องการ");
            }
        }
        private void ValidateOccupation()
        {
            if (cmbChannelType.SelectedValue.ToString() == "PO" || cmbChannelType.SelectedValue.ToString() == "PN")
            {
                if (txtOcpType.Text.Trim() == "")
                {
                    txtOcpType.Focus();
                    throw new Exception("ระบุ ประเภทอาชีพ ที่ท่านต้องการ");
                }

                if (txtOcpClass.Text.Trim() == "")
                {
                    txtOcpClass.Focus();
                    throw new Exception("ระบุ ชั้นอาชีพ ที่ท่านต้องการ");
                }

                if (cmbocpDesc.SelectedValue == null)
                {
                    cmbocpDesc.Focus();
                    throw new Exception("ระบุ รายละเอียดอาชีพ ที่ท่านต้องการ");
                }

                if (cmbocpDesc.SelectedValue.ToString() == "")
                {
                    cmbocpDesc.Focus();
                    throw new Exception("ระบุ รายละเอียดอาชีพ ที่ท่านต้องการ");
                }

                if (cmbocpDesc.Text == "")
                {
                    cmbocpDesc.Focus();
                    throw new Exception("ระบุ รายละเอียดอาชีพ ที่ท่านต้องการ");
                }

                //if (cmbPosition.SelectedValue == null)
                //{
                //    cmbPosition.Focus();
                //    throw new Exception("ระบุ ตำแหน่ง ที่ท่านต้องการ");
                //}

                //if (cmbBusinessType.SelectedValue == null)
                //{
                //    cmbBusinessType.Focus();
                //    throw new Exception("ระบุ ลักษณะธุรกิจ ที่ท่านต้องการ");
                //}

                //if (cmbBusinessType.SelectedValue == null)
                //{
                //    cmbBusinessType.Focus();
                //    throw new Exception("ระบุ ลักษณะธุรกิจ ที่ท่านต้องการ");
                //}


                //if (cmbChannelType.SelectedValue.ToString() != "PN")
                //{
                //    if (txtHeight.Text.Trim() == "")
                //    {
                //        txtHeight.Focus();
                //        throw new Exception("ระบุ ส่วนสูง ที่ท่านต้องการ");
                //    }

                //    if (txtWeight.Text.Trim() == "")
                //    {
                //        txtWeight.Focus();
                //        throw new Exception("ระบุ น้ำหนัก ที่ท่านต้องการ");
                //    }

                //    if (txtBmi.Text.Trim() == "")
                //    {
                //        txtWeight.Focus();
                //        throw new Exception("ระบุ ค่า BMI ที่ท่านต้องการ");
                //    }
                //}



            }
            else
            {
                if (txtOcpType.Text.Trim() != "" && txtOcpClass.Text.Trim() != "")
                {
                    if (txtOcpType.Text.Trim() == "")
                    {
                        txtOcpType.Focus();
                        throw new Exception("ระบุ ประเภทอาชีพ ที่ท่านต้องการ");
                    }

                    if (txtOcpClass.Text.Trim() == "")
                    {
                        txtOcpClass.Focus();
                        throw new Exception("ระบุ ชั้นอาชีพ ที่ท่านต้องการ");
                    }

                    if (cmbocpDesc.SelectedValue == null)
                    {
                        cmbocpDesc.Focus();
                        throw new Exception("ระบุ รายละเอียดอาชีพ ที่ท่านต้องการ");
                    }

                    if (cmbocpDesc.SelectedValue.ToString() == "")
                    {
                        cmbocpDesc.Focus();
                        throw new Exception("ระบุ รายละเอียดอาชีพ ที่ท่านต้องการ");
                    }

                    if (cmbocpDesc.Text == "")
                    {
                        cmbocpDesc.Focus();
                        throw new Exception("ระบุ รายละเอียดอาชีพ ที่ท่านต้องการ");
                    }

                    //if (cmbPosition.SelectedValue == null)
                    //{
                    //    cmbPosition.Focus();
                    //    throw new Exception("ระบุ ตำแหน่ง ที่ท่านต้องการ");
                    //}

                    //if (cmbBusinessType.SelectedValue == null)
                    //{
                    //    cmbBusinessType.Focus();
                    //    throw new Exception("ระบุ ลักษณะธุรกิจ ที่ท่านต้องการ");
                    //}

                    //if (cmbBusinessType.SelectedValue == null)
                    //{
                    //    cmbBusinessType.Focus();
                    //    throw new Exception("ระบุ ลักษณะธุรกิจ ที่ท่านต้องการ");
                    //}

                    if (cmbChannelType.SelectedValue.ToString() != "KB")
                    {
                        if (txtHeight.Text.Trim() == "")
                        {
                            txtHeight.Focus();
                            throw new Exception("ระบุ ส่วนสูง ที่ท่านต้องการ");
                        }

                        if (txtWeight.Text.Trim() == "")
                        {
                            txtWeight.Focus();
                            throw new Exception("ระบุ น้ำหนัก ที่ท่านต้องการ");
                        }

                        if (txtBmi.Text.Trim() == "")
                        {
                            txtWeight.Focus();
                            throw new Exception("ระบุ ค่า BMI ที่ท่านต้องการ");
                        }
                    }


                }
            }


        }
        private void ValidateAddressData()
        {
            if (cmbAddressType.SelectedValue.ToString() == "")
            {
                cmbAddressType.Focus();
                throw new Exception("ระบุ ประเภทที่อยู่ที่ติดต่อ ที่ท่านต้องการ");
            }
            if (txtAddressName.Text.Trim() == "" && txtAddressNumber.Text.Trim() == "" && txtMooh.Text.Trim() == "" && txtSoi.Text.Trim() == "" && txtRoad.Text.Trim() == "")
            {
                txtAddressName.Focus();
                throw new Exception("ระบุ รายละเอียดของที่อยู่ ที่ท่านต้องการ");
            }
            if (cmbProvince.Text.Trim() == "" || cmbProvince.Text.Trim() == "ระบุจังหวัดที่ต้องการ")
            {
                cmbProvince.Focus();
                throw new Exception("ระบุ จังหวัด ที่ท่านต้องการ");
            }
            if (cmbAmphur.Text.Trim() == "" || cmbAmphur.Text.Trim() == "ระบุอำเภอที่ต้องการ")
            {
                cmbAmphur.Focus();
                throw new Exception("ระบุ อำเภอ ที่ท่านต้องการ");
            }
            if (cmbTambol.Text.Trim() == "" || cmbTambol.Text.Trim() == "ระบุตำบลที่ต้องการ")
            {
                cmbTambol.Focus();
                throw new Exception("ระบุ ตำบล ที่ท่านต้องการ");
            }
            if (txtZipcode.Text.Trim() == "")
            {
                txtZipcode.Focus();
                throw new Exception("ระบุ รหัสไปรษณีย์ ที่ท่านต้องการ");
            }
            if (txtZipcode.Text.Trim() != "")
            {
                Decimal i = 0;
                if (!Decimal.TryParse(txtZipcode.Text.Trim(), out i))
                {
                    txtZipcode.Focus();
                    throw new Exception("กรุณาระบุ รหัสไปรษณีย์ เป็นตัวเลข");

                }
            }
            if (cmbAdrType.SelectedValue.ToString() == "")
            {
                cmbAdrType.Focus();
                throw new Exception("ระบุ ข้อมูลที่อยู่เดิมหรือที่อยู่ใหม่ ที่ท่านต้องการ");
            }
        }


        private void ValidateParentData()
        {
            //if (ckbIdcardDoc.Checked == false && ckbAddressDoc.Checked == false)
            //{
            //    ckbIdcardDoc.Focus();
            //    throw new Exception("ระบุ เอกสารแนบ ที่ท่านต้องการ");
            //}
            //if (cmbCardType.SelectedValue.ToString() == "")
            //{
            //    cmbCardType.Focus();
            //    throw new Exception("ระบุ ประเภทบัตร ที่ท่านต้องการ");
            //}
            //if (txtIdcardNo.Text.Trim() == "")
            //{
            //    txtIdcardNo.Focus();
            //    throw new Exception("ระบุ เลขทีบัตร ที่ท่านต้องการ");
            //}
            if (txtParentIdcardNo.Text.Trim() != "" && cmbParentCardType.SelectedValue.ToString() == "")
            {
                cmbParentCardType.Focus();
                throw new Exception("ระบุประเภทของบัตร");
            }
            if (cmbParentCardType.SelectedValue.ToString() == "1" && txtParentIdcardNo.Text.Trim() != "")
            {
                if (Utility.ValidateIDCard(txtParentIdcardNo.Text) == false)
                {
                    txtParentIdcardNo.Focus();
                    throw new Exception("รูปแบบของเลขที่บัตรประชาชนไม่ถูกต้อง");
                }
            }
            if (cmbDataParent.SelectedValue.ToString() == "")
            {
                cmbDataParent.Focus();
                throw new Exception("ระบุ ข้อมูลลูกค้า ที่ท่านต้องการ");
            }
            if (txtParentPrename.Text.Trim() == "")
            {
                txtParentPrename.Focus();
                throw new Exception("ระบุ คำนำหน้าชื่อ ที่ท่านต้องการ");
            }
            if (txtParentName.Text.Trim() == "")
            {
                txtParentName.Focus();
                throw new Exception("ระบุ ชื่อลูกค้า ที่ท่านต้องการ");
            }
            if (cmbParentGender.SelectedValue.ToString() == "")
            {
                cmbParentGender.Focus();
                throw new Exception("ระบุ เพศของลูกค้า ที่ท่านต้องการ");
            }
            if (txtParentBirthDt.Text.Trim() == "")
            {
                txtParentBirthDt.Focus();
                throw new Exception("ระบุ วันเกิดของลูกค้า ที่ท่านต้องการ");
            }
            if (txtParentBirthDt.Text.Trim() != "")
            {
                DateTime? d = Utility.StringToDateTime(txtParentBirthDt.Text.Trim(), "BU");
                if (d == null)
                {
                    txtParentBirthDt.Focus();
                    MessageBox.Show("รูปแบบวันที่ ของวันเกิด ไม่ถูกต้อง");

                }
            }
            if (txtParentStAge.Text.Trim() == "")
            {
                txtParentStAge.Focus();
                throw new Exception("ระบุ อายุของลูกค้า ที่ท่านต้องการ");
            }
            if (cmbParentNationality.SelectedValue.ToString() == "")
            {
                cmbParentNationality.Focus();
                throw new Exception("ระบุ สัญชาติของลูกค้า ที่ท่านต้องการ");
            }
        }
        private void ValidateParentOccupation()
        {
            if (cmbChannelType.SelectedValue.ToString() == "PO" || cmbChannelType.SelectedValue.ToString() == "PN")
            {
                if (txtParentOcpType.Text.Trim() == "")
                {
                    txtParentOcpType.Focus();
                    throw new Exception("ระบุ ประเภทอาชีพ ที่ท่านต้องการ");
                }

                if (txtParentOcpClass.Text.Trim() == "")
                {
                    txtParentOcpClass.Focus();
                    throw new Exception("ระบุ ชั้นอาชีพ ที่ท่านต้องการ");
                }

                if (cmbParentbocpDesc.SelectedValue == null)
                {
                    cmbParentbocpDesc.Focus();
                    throw new Exception("ระบุ รายละเอียดอาชีพ ที่ท่านต้องการ");
                }



                //if (cmbPosition.SelectedValue == null)
                //{
                //    cmbPosition.Focus();
                //    throw new Exception("ระบุ ตำแหน่ง ที่ท่านต้องการ");
                //}

                //if (cmbBusinessType.SelectedValue == null)
                //{
                //    cmbBusinessType.Focus();
                //    throw new Exception("ระบุ ลักษณะธุรกิจ ที่ท่านต้องการ");
                //}

                //if (cmbBusinessType.SelectedValue == null)
                //{
                //    cmbBusinessType.Focus();
                //    throw new Exception("ระบุ ลักษณะธุรกิจ ที่ท่านต้องการ");
                //}


                //if (txtParentHeight.Text.Trim() == "")
                //{
                //    txtParentHeight.Focus();
                //    throw new Exception("ระบุ ส่วนสูง ที่ท่านต้องการ");
                //}

                //if (txtParentWeight.Text.Trim() == "")
                //{
                //    txtParentWeight.Focus();
                //    throw new Exception("ระบุ น้ำหนัก ที่ท่านต้องการ");
                //}


            }
            else
            {
                if (txtParentOcpType.Text.Trim() != "" && txtParentOcpClass.Text.Trim() != "")
                {
                    if (txtParentOcpType.Text.Trim() == "")
                    {
                        txtParentOcpType.Focus();
                        throw new Exception("ระบุ ประเภทอาชีพ ที่ท่านต้องการ");
                    }

                    if (txtParentOcpClass.Text.Trim() == "")
                    {
                        txtParentOcpClass.Focus();
                        throw new Exception("ระบุ ชั้นอาชีพ ที่ท่านต้องการ");
                    }

                    if (cmbParentbocpDesc.SelectedValue == null)
                    {
                        cmbParentbocpDesc.Focus();
                        throw new Exception("ระบุ รายละเอียดอาชีพ ที่ท่านต้องการ");
                    }

                    if (cmbocpDesc.SelectedValue.ToString() == "")
                    {
                        cmbocpDesc.Focus();
                        throw new Exception("ระบุ รายละเอียดอาชีพ ที่ท่านต้องการ");
                    }

                    if (cmbocpDesc.Text == "")
                    {
                        cmbocpDesc.Focus();
                        throw new Exception("ระบุ รายละเอียดอาชีพ ที่ท่านต้องการ");
                    }

                    //if (cmbPosition.SelectedValue == null)
                    //{
                    //    cmbPosition.Focus();
                    //    throw new Exception("ระบุ ตำแหน่ง ที่ท่านต้องการ");
                    //}

                    //if (cmbBusinessType.SelectedValue == null)
                    //{
                    //    cmbBusinessType.Focus();
                    //    throw new Exception("ระบุ ลักษณะธุรกิจ ที่ท่านต้องการ");
                    //}

                    //if (cmbBusinessType.SelectedValue == null)
                    //{
                    //    cmbBusinessType.Focus();
                    //    throw new Exception("ระบุ ลักษณะธุรกิจ ที่ท่านต้องการ");
                    //}

                    if (cmbChannelType.SelectedValue.ToString() != "KB")
                    {
                        if (txtHeight.Text.Trim() == "")
                        {
                            txtHeight.Focus();
                            throw new Exception("ระบุ ส่วนสูง ที่ท่านต้องการ");
                        }

                        if (txtWeight.Text.Trim() == "")
                        {
                            txtWeight.Focus();
                            throw new Exception("ระบุ น้ำหนัก ที่ท่านต้องการ");
                        }

                        if (txtBmi.Text.Trim() == "")
                        {
                            txtWeight.Focus();
                            throw new Exception("ระบุ ค่า BMI ที่ท่านต้องการ");
                        }
                    }


                }
            }


        }
        private void ValidateParentAddressData()
        {
            if (cmbAddressType.SelectedValue.ToString() == "")
            {
                cmbAddressType.Focus();
                throw new Exception("ระบุ ประเภทที่อยู่ที่ติดต่อ ที่ท่านต้องการ");
            }
            if (txtAddressName.Text.Trim() == "" && txtAddressNumber.Text.Trim() == "" && txtMooh.Text.Trim() == "" && txtSoi.Text.Trim() == "" && txtRoad.Text.Trim() == "")
            {
                txtAddressName.Focus();
                throw new Exception("ระบุ รายละเอียดของที่อยู่ ที่ท่านต้องการ");
            }
            if (cmbProvince.Text.Trim() == "" || cmbProvince.Text.Trim() == "ระบุจังหวัดที่ต้องการ")
            {
                cmbProvince.Focus();
                throw new Exception("ระบุ จังหวัด ที่ท่านต้องการ");
            }
            if (cmbAmphur.Text.Trim() == "" || cmbAmphur.Text.Trim() == "ระบุอำเภอที่ต้องการ")
            {
                cmbAmphur.Focus();
                throw new Exception("ระบุ อำเภอ ที่ท่านต้องการ");
            }
            if (cmbTambol.Text.Trim() == "" || cmbTambol.Text.Trim() == "ระบุตำบลที่ต้องการ")
            {
                cmbTambol.Focus();
                throw new Exception("ระบุ ตำบล ที่ท่านต้องการ");
            }
            if (txtZipcode.Text.Trim() == "")
            {
                txtZipcode.Focus();
                throw new Exception("ระบุ รหัสไปรษณีย์ ที่ท่านต้องการ");
            }
            if (txtZipcode.Text.Trim() != "")
            {
                Decimal i = 0;
                if (!Decimal.TryParse(txtZipcode.Text.Trim(), out i))
                {
                    txtZipcode.Focus();
                    throw new Exception("กรุณาระบุ รหัสไปรษณีย์ เป็นตัวเลข");

                }
            }
            if (cmbAdrType.SelectedValue.ToString() == "")
            {
                cmbAdrType.Focus();
                throw new Exception("ระบุ ข้อมูลที่อยู่เดิมหรือที่อยู่ใหม่ ที่ท่านต้องการ");
            }
        }




        private void ValidateAccount()
        {
            if (txtBankCode.Text.Trim() == "")
            {
                tabMain.SelectTab("tabCustomerData");
                txtBankCode.Focus();
                throw new Exception("ระบุ Code ธนานาคาร ที่ท่านต้องการ");
            }

            if (txtBankName.Text.Trim() == "")
            {
                tabMain.SelectTab("tabCustomerData");
                txtBankName.Focus();
                throw new Exception("ระบุ ชื่อธนาคาร ที่ท่านต้องการ");
            }

            if (txtBranchCode.Text.Trim() == "")
            {
                tabMain.SelectTab("tabCustomerData");
                txtBranchCode.Focus();
                throw new Exception("ระบุ Code ของสาขาธนาคารกรุงเทพ ที่ท่านต้องการ");
            }

            if (txtBranchName.Text.Trim() == "")
            {
                tabMain.SelectTab("tabCustomerData");
                txtBranchName.Focus();
                throw new Exception("ระบุ ชื่อของสาขาธนาคารกรุงเทพ ที่ท่านต้องการ");
            }

            if (txtAccName.Text.Trim() == "")
            {
                tabMain.SelectTab("tabCustomerData");
                txtAccName.Focus();
                throw new Exception("ระบุ ชื่อเจ้าของบัญชี ที่ท่านต้องการ");
            }

            if (txtAccNo.Text.Trim() == "")
            {
                tabMain.SelectTab("tabCustomerData");
                txtAccName.Focus();
                throw new Exception("ระบุ เลขที่บัญชี ที่ท่านต้องการ");
            }

            if (cmbDepositType.SelectedValue == null || cmbDepositType.SelectedValue.ToString() == "")
            {
                tabMain.SelectTab("tabCustomerData");
                cmbDepositType.Focus();
                throw new Exception("ระบุ ประเภทบัญชี ที่ท่านต้องการ");
            }
        }

        private void ValidatePlanData(NewBisPASvcRef.U_APPLICATION_Collection objDetail)
        {
            if (PLAN_ERROR == true)
            {
                tabMain.SelectTab("tabPlanData");
                ckbLifeFree.Focus();
                throw new Exception("ไม่สามารถบันทึกข้อมูลได้ เนื่องจากคำนวนเบี้ยประกันไม่สำเร็จ");
            }
            int chkPlanData = 0;
            if (objDetail != null && objDetail.Count() > 0)
            {
                foreach (NewBisPASvcRef.U_APPLICATION uApplicationObj in objDetail)
                {
                    if (uApplicationObj.LIFE_ID != null && (!String.IsNullOrEmpty(uApplicationObj.LIFE_ID.PL_CODE)))
                    {
                        chkPlanData = chkPlanData + 1;
                        NewBisPASvcRef.U_LIFE_ID uLifeIDObj = new NewBisPASvcRef.U_LIFE_ID();
                        uLifeIDObj = uApplicationObj.LIFE_ID;
                        if (uLifeIDObj.FREE_FLG == "Y" || cmbChannelType.SelectedValue.ToString() == "PF")
                        {
                            ValidatePlanFree(uLifeIDObj);
                        }
                        else if (uLifeIDObj.FREE_FLG == "N")
                        {
                            ValidatePlanPaid(uLifeIDObj);
                        }
                    }
                }
            }
            if (objDetail.Any(item => item.UAPP_ID > 0) &&  chkPlanData == 0)
            {
                tabMain.SelectTab("tabPlanData");
                ckbLifeFree.Focus();
                throw new Exception("ไม่มีข้อมูลแบบประกัน");
            }
        }
        private void ValidatePlanFree(NewBisPASvcRef.U_LIFE_ID objDetail)
        {
            if (String.IsNullOrEmpty(objDetail.CTM_TYPE))
            {
                tabMain.SelectTab("tabPlanData");
                cmbFreeCtmType.Focus();
                throw new Exception("ระบุ ประเภทลูกค้า ของแบบประกันแถมฟรี");
            }
            if (String.IsNullOrEmpty(objDetail.PL_BLOCK))
            {
                tabMain.SelectTab("tabPlanData");
                cmbFreePlBlock.Focus();
                throw new Exception("ระบุ ชนิดแบบประกัน ของแบบประกันแถมฟรี");
            }
            if (String.IsNullOrEmpty(objDetail.PL_CODE))
            {
                tabMain.SelectTab("tabPlanData");
                txtFreePlcode.Focus();
                throw new Exception("ระบุ รหัสแบบประกัน ของแบบประกันแถมฟรี");
            }
            if (String.IsNullOrEmpty(objDetail.PL_CODE2))
            {
                tabMain.SelectTab("tabPlanData");
                txtFreePlcode2.Focus();
                throw new Exception("ระบุ รหัสแบบประกัน ของแบบประกันแถมฟรี");
            }
            if (String.IsNullOrEmpty(objDetail.BLA_TITLE))
            {
                tabMain.SelectTab("tabPlanData");
                txtFreePlcode2.Focus();
                throw new Exception("ระบุ ชื่อแบบประกัน ของแบบประกันแถมฟรี");
            }
            if (objDetail.P_MODE == null)
            {
                tabMain.SelectTab("tabPlanData");
                cmbFreePmode.Focus();
                throw new Exception("ระบุ งาดการชำระเบี้ย ของแบบประกันแถมฟรี");
            }
            if (objDetail.SUMM == null)
            {
                tabMain.SelectTab("tabPlanData");
                txtFreeSumm.Focus();
                throw new Exception("ระบุ ทุนประกัน ของแบบประกันแถมฟรี");
            }
            if (objDetail.ISU_DT == null)
            {
                tabMain.SelectTab("tabPlanData");
                txtFreeIsuDate.Focus();
                throw new Exception("ระบุ วันที่เริ่มคุ้มครอง ของแบบประกันแถมฟรี");
            }
            if (objDetail.POL_YR == null)
            {
                tabMain.SelectTab("tabPlanData");
                txtFreePolYr.Focus();
                throw new Exception("ระบุ ปี ของแบบประกันแถมฟรี");
            }
            if (objDetail.POL_LT == null)
            {
                tabMain.SelectTab("tabPlanData");
                txtFreePolLt.Focus();
                throw new Exception("ระบุ งวด ของแบบประกันแถมฟรี");
            }
            if (objDetail.PAY_TERM == null)
            {
                tabMain.SelectTab("tabPlanData");
                txtFreePayTerm.Focus();
                throw new Exception("ระบุ ระยะชำระเบี้ย ของแบบประกันแถมฟรี");
            }
            if (objDetail.ASS_TERM == null)
            {
                tabMain.SelectTab("tabPlanData");
                txtFreeAssTerm.Focus();
                throw new Exception("ระบุ ระยะคุ้มครอง ของแบบประกันแถมฟรี");
            }
            if (objDetail.ASS_DT == null)
            {
                tabMain.SelectTab("tabPlanData");
                txtFreeAssDate.Focus();
                throw new Exception("ระบุ วันที่สิ้นสุดคุ้มครอง ของแบบประกันแถมฟรี");
            }
            if (objDetail.MAT_TERM == null)
            {
                tabMain.SelectTab("tabPlanData");
                txtFreeMatTerm.Focus();
                throw new Exception("ระบุ ระยะสัญญา ของแบบประกันแถมฟรี");
            }
            if (objDetail.MAT_DT == null)
            {
                tabMain.SelectTab("tabPlanData");
                txtFreeMatDate.Focus();
                throw new Exception("ระบุ วันที่ครบสัญญา ของแบบประกันแถมฟรี");
            }
            if (objDetail.PREMIUM == null)
            {
                tabMain.SelectTab("tabPlanData");
                txtFreePremium.Focus();
                throw new Exception("ระบุ เบี้ยประกัน ของแบบประกันแถมฟรี");
            }
            if (String.IsNullOrEmpty(objDetail.SINGLE))
            {
                tabMain.SelectTab("tabPlanData");
                txtFreePlcode.Focus();
                throw new Exception("ระบุ ชำระเบี้ยครั้งเดียว ของแบบประกันแถมฟรี");
            }
            if (String.IsNullOrEmpty(objDetail.PROTECT))
            {
                tabMain.SelectTab("tabPlanData");
                txtFreePlcode.Focus();
                throw new Exception("ระบุ เอกสิทธิ์ ของแบบประกันแถมฟรี");
            }
            if (String.IsNullOrEmpty(objDetail.MEDICAL))
            {
                tabMain.SelectTab("tabPlanData");
                txtFreePlcode.Focus();
                throw new Exception("ระบุ ตรวจสุขภาพ ของแบบประกันแถมฟรี");
            }
            if (String.IsNullOrEmpty(objDetail.STANDARD))
            {
                tabMain.SelectTab("tabPlanData");
                txtFreePlcode.Focus();
                throw new Exception("ระบุ รับประกันปกติ ของแบบประกันแถมฟรี");
            }
            if (String.IsNullOrEmpty(objDetail.REINSURE))
            {
                tabMain.SelectTab("tabPlanData");
                txtFreePlcode.Focus();
                throw new Exception("ระบุ รับประกันต่อ ของแบบประกันแถมฟรี");
            }
            if (objDetail.EXCLUDE_TPD == null)
            {
                tabMain.SelectTab("tabPlanData");
                txtFreePlcode.Focus();
                throw new Exception("ระบุ ทพ. ของแบบประกันแถมฟรี");
            }
            if (objDetail.DEPOSIT_INST == null)
            {
                tabMain.SelectTab("tabPlanData");
                txtFreePlcode.Focus();
                throw new Exception("ระบุ อัตราดอกเบี้ยเงินฝาก ของแบบประกันแถมฟรี");
            }
            if (String.IsNullOrEmpty(objDetail.POLICY_HOLDING))
            {
                tabMain.SelectTab("tabPlanData");
                txtFreePlcode.Focus();
                throw new Exception("ระบุ Policy Holding ของแบบประกันแถมฟรี");
            }
            if (String.IsNullOrEmpty(objDetail.MARKETING_TYPE))
            {
                tabMain.SelectTab("tabPlanData");
                txtFreePlcode.Focus();
                throw new Exception("ระบุ Marketing Type ของแบบประกันแถมฟรี");
            }
        }
        private void ValidatePlanPaid(NewBisPASvcRef.U_LIFE_ID objDetail)
        {
            if (String.IsNullOrEmpty(objDetail.CTM_TYPE))
            {
                tabMain.SelectTab("tabPlanData");
                cmbPaidCtmType.Focus();
                throw new Exception("ระบุ ประเภทลูกค้า ของแบบประกันซื้อเพิ่ม");
            }
            if (String.IsNullOrEmpty(objDetail.PL_BLOCK))
            {
                tabMain.SelectTab("tabPlanData");
                cmbPaidPlBlock.Focus();
                throw new Exception("ระบุ ชนิดแบบประกัน ของแบบประกันซื้อเพิ่ม");
            }
            if (String.IsNullOrEmpty(objDetail.PL_CODE))
            {
                tabMain.SelectTab("tabPlanData");
                txtPaidPlcode.Focus();
                throw new Exception("ระบุ รหัสแบบประกัน ของแบบประกันซื้อเพิ่ม");
            }
            if (String.IsNullOrEmpty(objDetail.PL_CODE2))
            {
                tabMain.SelectTab("tabPlanData");
                txtPaidPlcode2.Focus();
                throw new Exception("ระบุ รหัสแบบประกัน ของแบบประกันซื้อเพิ่ม");
            }
            if (String.IsNullOrEmpty(objDetail.BLA_TITLE))
            {
                tabMain.SelectTab("tabPlanData");
                txtPaidPlcode2.Focus();
                throw new Exception("ระบุ ชื่อแบบประกัน ของแบบประกันซื้อเพิ่ม");
            }

            if (objDetail.PL_BLOCK != "K")
            {
                if (objDetail.P_MODE == null)
                {
                    tabMain.SelectTab("tabPlanData");
                    cmbPaidPmode.Focus();
                    throw new Exception("ระบุ งาดการชำระเบี้ย ของแบบประกันซื้อเพิ่ม");
                }
                if (objDetail.NXTDUE_DT == null)
                {
                    tabMain.SelectTab("tabPlanData");
                    txtPaidNxtDueDate.Focus();
                    throw new Exception("ระบุ วันที่ชำระเบี้ยงวดถัดไป ของแบบประกันซื้อเพิ่ม");
                }
                if (objDetail.SUMM == null || objDetail.SUMM == 0)
                {
                    tabMain.SelectTab("tabPlanData");
                    txtPaidSumm.Focus();
                    throw new Exception("ระบุ ทุนประกัน ของแบบประกันซื้อเพิ่ม");
                }
                if (objDetail.PREMIUM == null || objDetail.PREMIUM == 0)
                {
                    tabMain.SelectTab("tabPlanData");
                    txtPaidPremium.Focus();
                    throw new Exception("ระบุ เบี้ยประกัน ของแบบประกันซื้อเพิ่ม");
                }
            }


            if (objDetail.ISU_DT == null)
            {
                tabMain.SelectTab("tabPlanData");
                txtPaidIsuDate.Focus();
                throw new Exception("ระบุ วันที่เริ่มคุ้มครอง ของแบบประกันซื้อเพิ่ม");
            }
            if (objDetail.POL_YR == null)
            {
                tabMain.SelectTab("tabPlanData");
                txtPaidPolYr.Focus();
                throw new Exception("ระบุ ปี ของแบบประกันซื้อเพิ่ม");
            }
            if (objDetail.POL_LT == null)
            {
                tabMain.SelectTab("tabPlanData");
                txtPaidPolLt.Focus();
                throw new Exception("ระบุ งวด ของแบบประกันซื้อเพิ่ม");
            }
            if (objDetail.PAY_TERM == null)
            {
                tabMain.SelectTab("tabPlanData");
                txtPaidPayTerm.Focus();
                throw new Exception("ระบุ ระยะชำระเบี้ย ของแบบประกันซื้อเพิ่ม");
            }
            if (objDetail.LASTPAY_DT == null)
            {
                tabMain.SelectTab("tabPlanData");
                txtPaidLastPayDate.Focus();
                throw new Exception("ระบุ วันที่ชำระครั้งสุดท้าย ของแบบประกันซื้อเพิ่ม");
            }
            if (objDetail.ASS_TERM == null)
            {
                tabMain.SelectTab("tabPlanData");
                txtPaidAssTerm.Focus();
                throw new Exception("ระบุ ระยะคุ้มครอง ของแบบประกันซื้อเพิ่ม");
            }
            if (objDetail.ASS_DT == null)
            {
                tabMain.SelectTab("tabPlanData");
                txtPaidAssDate.Focus();
                throw new Exception("ระบุ วันที่สิ้นสุดคุ้มครอง ของแบบประกันซื้อเพิ่ม");
            }
            if (objDetail.MAT_TERM == null)
            {
                tabMain.SelectTab("tabPlanData");
                txtPaidMatTerm.Focus();
                throw new Exception("ระบุ ระยะสัญญา ของแบบประกันซื้อเพิ่ม");
            }
            if (objDetail.MAT_DT == null)
            {
                tabMain.SelectTab("tabPlanData");
                txtPaidMatDate.Focus();
                throw new Exception("ระบุ วันที่ครบสัญญา ของแบบประกันซื้อเพิ่ม");
            }

            if (String.IsNullOrEmpty(objDetail.SINGLE))
            {
                tabMain.SelectTab("tabPlanData");
                txtPaidPlcode.Focus();
                throw new Exception("ระบุ ชำระเบี้ยครั้งเดียว ของแบบประกันซื้อเพิ่ม");
            }
            if (String.IsNullOrEmpty(objDetail.PROTECT))
            {
                tabMain.SelectTab("tabPlanData");
                txtPaidPlcode.Focus();
                throw new Exception("ระบุ เอกสิทธิ์ ของแบบประกันซื้อเพิ่ม");
            }
            if (String.IsNullOrEmpty(objDetail.MEDICAL))
            {
                tabMain.SelectTab("tabPlanData");
                txtPaidPlcode.Focus();
                throw new Exception("ระบุ ตรวจสุขภาพ ของแบบประกันซื้อเพิ่ม");
            }
            if (String.IsNullOrEmpty(objDetail.STANDARD))
            {
                tabMain.SelectTab("tabPlanData");
                txtPaidPlcode.Focus();
                throw new Exception("ระบุ รับประกันปกติ ของแบบประกันซื้อเพิ่ม");
            }
            if (String.IsNullOrEmpty(objDetail.REINSURE))
            {
                tabMain.SelectTab("tabPlanData");
                txtPaidPlcode.Focus();
                throw new Exception("ระบุ รับประกันต่อ ของแบบประกันซื้อเพิ่ม");
            }
            if (objDetail.EXCLUDE_TPD == null)
            {
                tabMain.SelectTab("tabPlanData");
                txtPaidPlcode.Focus();
                throw new Exception("ระบุ ทพ. ของแบบประกันซื้อเพิ่ม");
            }
            if (objDetail.DEPOSIT_INST == null)
            {
                tabMain.SelectTab("tabPlanData");
                txtPaidPlcode.Focus();
                throw new Exception("ระบุ อัตราดอกเบี้ยเงินฝาก ของแบบประกันซื้อเพิ่ม");
            }
            if (cmbChannelType.SelectedValue.ToString() == "PD")
            {
                if (String.IsNullOrEmpty(objDetail.POLICY_HOLDING))
                {
                    tabMain.SelectTab("tabPlanData");
                    txtPaidPlcode.Focus();
                    throw new Exception("ระบุ Policy Holding ของแบบประกันซื้อเพิ่ม");
                }
            }

            if (String.IsNullOrEmpty(objDetail.MARKETING_TYPE))
            {
                tabMain.SelectTab("tabPlanData");
                txtPaidPlcode.Focus();
                throw new Exception("ระบุ Marketing Type ของแบบประกันซื้อเพิ่ม");
            }

            if (objDetail.SPOUSE_FLG == "T")
            {
                if (objDetail.APP_SPOUSE_Collection != null && objDetail.APP_SPOUSE_Collection.Count() > 0)
                {
                    if (objDetail.APP_SPOUSE_Collection[0].TMN == 'N')
                    {
                        if (objDetail.APP_SPOUSE_Collection[0].ST_AGE == null)
                        {
                            tabMain.SelectTab("tabPlanData");
                            txtSpAge.Focus();
                            throw new Exception("ระบุ อายุ ของคู่สมรส");
                        }

                        ValidateSpouseData(PAR_U_SPOUSE_ID_COLL);
                    }

                }
                else
                {
                    tabMain.SelectTab("tabPlanData");
                    txtSpAge.Focus();
                    throw new Exception("เป็นแบบประกันที่มีคู่สมรสแต่ไม่มีข้อมูลของคู่สมรส");
                }
            }


        }
        private void ValidateSpouseData(NewBisPASvcRef.U_SPOUSE_ID[] objDetail)
        {
            if (objDetail != null && objDetail.Count() > 0)
            {
                NewBisPASvcRef.U_SPOUSE_ID obj = new NewBisPASvcRef.U_SPOUSE_ID();
                obj = objDetail.ToArray()[0];

                //if (String.IsNullOrEmpty(obj.IDCARD_NO) && String.IsNullOrEmpty(obj.PASSPORT))
                //{
                //    tabMain.SelectTab("tabPlanData");
                //    txtSpIDcardNo.Focus();
                //    throw new Exception("ระบุ ข้อมูลบัตรประชาชนหรือหนังสือเดินทาง ของคู่สมรส");
                //}

                if (!String.IsNullOrEmpty(obj.IDCARD_NO))
                {
                    if (Utility.ValidateIDCard(obj.IDCARD_NO) == false)
                    {
                        tabMain.SelectTab("tabPlanData");
                        txtSpIDcardNo.Focus();
                        throw new Exception("รูปแบบของเลขที่บัตรประชาชนของคู่สมรสไม่ถูกต้อง");
                    }
                }
                if (String.IsNullOrEmpty(obj.PRENAME))
                {
                    tabMain.SelectTab("tabPlanData");
                    txtSpPrename.Focus();
                    throw new Exception("ระบุ คำนำหน้าชื่อ ของคู่สมรส");
                }
                if (String.IsNullOrEmpty(obj.NAME))
                {
                    tabMain.SelectTab("tabPlanData");
                    txtSpName.Focus();
                    throw new Exception("ระบุ ชื่อ ของคู่สมรส");
                }
                if (obj.SEX == null)
                {
                    tabMain.SelectTab("tabPlanData");
                    cmbSpSex.Focus();
                    throw new Exception("ระบุ เพศ ของคู่สมรส");
                }
                if (obj.BIRTH_DT == null)
                {
                    tabMain.SelectTab("tabPlanData");
                    txtSpBirthDate.Focus();
                    throw new Exception("ระบุ วันเกิด ของคู่สมรส");
                }
                if (String.IsNullOrEmpty(obj.NATIONALITY))
                {
                    tabMain.SelectTab("tabPlanData");
                    cmbSpNationality.Focus();
                    throw new Exception("ระบุ สัญชาติ ของคู่สมรส");
                }
            }
        }
        private void ValidateBenefitData(NewBisPASvcRef.U_BENEFIT_ID_COLLECTION objDetail, String spouseFlg)
        {
            Boolean chkPerson = false;
            Boolean chkPersonSpouse = false;

            if (!(objDetail != null && objDetail.Count() > 0))
            {
                tabMain.SelectTab("tabBenefit");
                cmbBenefitType.Focus();
                throw new Exception("ระบุ ข้อมูลผู้รับผลประโยชน์");
            }
            if (objDetail != null && objDetail.Count() > 0)
            {
                var GetDataCus = from getData in objDetail
                                 where getData.SPOUSE_FLG != 'Y'
                                 && getData.TMN == 'N'
                                 select getData;
                if (GetDataCus != null && GetDataCus.Count() > 0)
                {
                    Decimal? totalGainPercent = 0;
                    foreach (NewBisPASvcRef.U_BENEFIT_ID obj in GetDataCus.ToArray())
                    {
                        if (obj.BENEFIT_PERSON != null && obj.BENEFIT_PERSON.GAIN_PERCENT != null)
                        {
                            chkPerson = true;
                            totalGainPercent = totalGainPercent + obj.BENEFIT_PERSON.GAIN_PERCENT.Value;
                        }
                    }

                    if (totalGainPercent != 100 && chkPerson == true)
                    {
                        tabMain.SelectTab("tabBenefit");
                        cmbBenefitType.Focus();
                        throw new Exception("ผลรวมของส่วนแบ่งผลประโยชน์ของผู้เอาประกันต้องเท่ากับ 100 %");
                    }


                }

                var GetDataSp = from getData in objDetail
                                where getData.SPOUSE_FLG == 'Y'
                                && getData.TMN == 'N'
                                select getData;

                if (spouseFlg == "Y")
                {
                    if (GetDataSp != null && GetDataSp.Count() > 0)
                    {
                        Decimal? totalGainPercent = 0;
                        foreach (NewBisPASvcRef.U_BENEFIT_ID obj in GetDataSp.ToArray())
                        {
                            if (obj.BENEFIT_PERSON != null && obj.BENEFIT_PERSON.GAIN_PERCENT != null)
                            {
                                chkPersonSpouse = true;
                                totalGainPercent = totalGainPercent + obj.BENEFIT_PERSON.GAIN_PERCENT.Value;
                            }
                        }

                        if (totalGainPercent != 100 && chkPersonSpouse == true)
                        {
                            tabMain.SelectTab("tabBenefit");
                            cmbBenefitType.Focus();
                            throw new Exception("ผลรวมของส่วนแบ่งผลประโยชน์ของคู่สมรสต้องเท่ากับ 100 %");
                        }
                    }
                    else
                    {
                        tabMain.SelectTab("tabBenefit");
                        cmbBenefitType.Focus();
                        throw new Exception("เป็นแบบประกันที่มีคู่สมรสกรุณาระบุผู้รับผลประโยชน์ของคู่สมรสให้ถูกต้อง");
                    }
                }
                else
                {
                    if (GetDataSp != null && GetDataSp.Count() > 0)
                    {
                        tabMain.SelectTab("tabBenefit");
                        cmbBenefitType.Focus();
                        throw new Exception("เป็นแบบประกันที่ไม่มีคู่สมรสไม่ต้องระบุผู้รับประโยชน์ของคู่สมรส");
                    }
                }



            }
        }
        private void ValidateCreditCardData(NewBisPASvcRef.U_APPLICATION_Collection objDetail)
        {
            if (objDetail != null && objDetail.Count() > 0)
            {
                if (objDetail[0].CREDIT_CARD_Collection != null && objDetail[0].CREDIT_CARD_Collection.Count() > 0)
                {
                    NewBisPASvcRef.U_CREDIT_CARD uCreditCardObj = new NewBisPASvcRef.U_CREDIT_CARD();
                    uCreditCardObj = objDetail[0].CREDIT_CARD_Collection[0];

                    if (cmbSummryStatus.SelectedValue != null)
                    {
                        if (cmbSummryStatus.SelectedValue.ToString() == "IF")
                        {
                            if (String.IsNullOrEmpty(uCreditCardObj.PAY_OPTION))
                            {
                                tabMain.SelectTab("tabPlanData");
                                cmbPayOption.Focus();
                                throw new Exception("ระบุ ประเภทการชำระบัตร ของบัตรเครดิต");
                            }
                            if (String.IsNullOrEmpty(uCreditCardObj.FIN_CODE))
                            {
                                tabMain.SelectTab("tabPlanData");
                                txtCardNo1.Focus();
                                throw new Exception("ระบุ บัตรของธนาคาร ของบัตรเครดิต");
                            }
                            if (string.IsNullOrEmpty(uCreditCardObj.CARD_TYPE))
                            {
                                tabMain.SelectTab("tabPlanData");
                                txtCardNo1.Focus();
                                throw new Exception("ระบุ ประเภทบัตร ของบัตรเครดิต");
                            }
                            if (string.IsNullOrEmpty(uCreditCardObj.CARD_NO))
                            {
                                tabMain.SelectTab("tabPlanData");
                                txtCardNo1.Focus();
                                throw new Exception("ระบุ เลขที่บัตร ของบัตรเครดิต");
                            }
                            else
                            {
                                if (uCreditCardObj.CARD_NO.Length < 16)
                                {
                                    tabMain.SelectTab("tabPlanData");
                                    txtCardNo1.Focus();
                                    throw new Exception("ระบุ เลขที่บัตรเครดิตต้องเป็น 16 หลัก");
                                }

                                Decimal i = 0;
                                if (!Decimal.TryParse(uCreditCardObj.CARD_NO, out i))
                                {
                                    tabMain.SelectTab("tabPlanData");
                                    txtCardNo1.Focus();
                                    throw new Exception("กรุณาระบุ เลขที่บัตรเครดิต เป็นตัวเลข");

                                }
                            }

                            if (uCreditCardObj.EXPIRE_MM != null)
                            {
                                Decimal i = 0;
                                if (!Decimal.TryParse(uCreditCardObj.EXPIRE_MM.Value.ToString(), out i))
                                {
                                    tabMain.SelectTab("tabPlanData");
                                    txtExpireMM.Focus();
                                    throw new Exception("กรุณาระบุ เดือนหมดอายุ เป็นตัวเลข");

                                }

                                if (uCreditCardObj.EXPIRE_MM.Value == 0 || uCreditCardObj.EXPIRE_MM.Value > 12)
                                {
                                    tabMain.SelectTab("tabPlanData");
                                    txtExpireMM.Focus();
                                    throw new Exception("กรุณาระบุ เดือนหมดอายุ ต้องอยู่ในช่วง 1 -12");
                                }
                            }

                            if (uCreditCardObj.EXPIRE_Y4 != null)
                            {
                                Decimal i = 0;
                                if (!Decimal.TryParse(uCreditCardObj.EXPIRE_Y4.Value.ToString(), out i))
                                {
                                    tabMain.SelectTab("tabPlanData");
                                    txtExpireYY.Focus();
                                    throw new Exception("กรุณาระบุ ปีหมดอายุ เป็นตัวเลข");

                                }
                            }

                            if (cmbSummryStatus.SelectedValue != null)
                            {
                                if (cmbSummryStatus.SelectedValue.ToString() == "IF")
                                {
                                    String chkCard1 = "";
                                    chkCard1 = txtCardNo1.Text;

                                    String[] cardUse = { "4546", "4730", "5444", "4505", "6223", "5237", "5229", "8803" };

                                    if (!cardUse.Contains(chkCard1))
                                    {
                                        throw new Exception("ไม่สามารถบันทึกข้อมูลออกกรมธรรม์ได้เนื่องจาก เลขที่บัตร 4 ตัว หน้าไม่อยู่ในกลุ่ม 4546,4730,5444,4505,6223,5237,5229,8803");
                                    }
                                }
                            }
                        }
                    }



                }
                else
                {
                    tabMain.SelectTab("tabPlanData");
                    txtCardNo1.Focus();
                    throw new Exception("ไม่สามารถบันทึกข้อมูลได้ เนื่องจากไม่มีข้อมูลบัตรเครดิต");
                }
            }

        }
        private void ValidateWUnderwriteIDData(NewBisPASvcRef.W_UNDERWRITE_ID_Collection objDetail)
        {
            if (objDetail != null && objDetail.Count() > 0)
            {
                NewBisPASvcRef.W_UNDERWRITE_ID wUnderwriteIDObj = new NewBisPASvcRef.W_UNDERWRITE_ID();
                wUnderwriteIDObj = objDetail[0];
                if (wUnderwriteIDObj.SUBUNDERWRITE_ID_Collection != null && wUnderwriteIDObj.SUBUNDERWRITE_ID_Collection.Count() > 0)
                {
                    NewBisPASvcRef.W_SUBUNDERWRITE_ID wSubUnderwriteIDObj = new NewBisPASvcRef.W_SUBUNDERWRITE_ID();
                    wSubUnderwriteIDObj = wUnderwriteIDObj.SUBUNDERWRITE_ID_Collection[0];
                    if (wSubUnderwriteIDObj.SUMMARY_Collection != null && wSubUnderwriteIDObj.SUMMARY_Collection.Count() > 0)
                    {
                        NewBisPASvcRef.W_SUMMARY wSummaryObj = new NewBisPASvcRef.W_SUMMARY();
                        wSummaryObj = wSubUnderwriteIDObj.SUMMARY_Collection[0];

                        if (String.IsNullOrEmpty(wSummaryObj.STATUS))
                        {

                            tabMain.SelectTab("tabUnderWriteData");
                            tabUnderWriteMain.SelectTab("tabUnderWrite");
                            cmbSummryStatus.Focus();
                            throw new Exception("กรุณาระบุสถานะของใบคำขอ");
                        }

                        if (String.IsNullOrEmpty(wSummaryObj.SUBSTATUS))
                        {
                            tabMain.SelectTab("tabUnderWriteData");
                            tabUnderWriteMain.SelectTab("tabUnderWrite");
                            cmbSummrySubStatus.Focus();
                            throw new Exception("กรุณาระบุสถานะย่อยของใบคำขอ");
                        }

                        var GetStatusCause = from getStatusCause in PAR_STATUS_CAUSE_COLL
                                             where getStatusCause.STATUS == wSummaryObj.STATUS
                                             select getStatusCause;
                        if (GetStatusCause != null && GetStatusCause.Count() > 0)
                        {
                            if (String.IsNullOrEmpty(wSummaryObj.STATUS_CAUSE))
                            {
                                tabMain.SelectTab("tabUnderWriteData");
                                tabUnderWriteMain.SelectTab("tabUnderWrite");
                                cmbSummaryStatusCause.Focus();
                                throw new Exception("กรุณาระบุสาเหตุของสถานะของใบคำขอ");
                            }
                        }

                        if (String.IsNullOrEmpty(wSummaryObj.SUMMARY_UNDERWRITER_Collection[0].UND_ID))
                        {
                            tabMain.SelectTab("tabUnderWriteData");
                            tabUnderWriteMain.SelectTab("tabUnderWrite");
                            cmbUnderWriteID.Focus();
                            throw new Exception("กรุณาระบุชื่อผู้พิจารณาของใบคำขอ");
                        }

                        if (wSummaryObj.STATUS == "MO" || wSummaryObj.STATUS == "MD")
                        {
                            int ChkMemoDetail = 0;
                            foreach (NewBisPASvcRef.W_UNDERWRITE_ID obj in objDetail)
                            {
                                foreach (NewBisPASvcRef.W_SUBUNDERWRITE_ID obj1 in obj.SUBUNDERWRITE_ID_Collection)
                                {
                                    foreach (NewBisPASvcRef.W_SUMMARY obj2 in obj1.SUMMARY_Collection)
                                    {
                                        if (obj2.MEMO_ID_Collection != null && obj2.MEMO_ID_Collection.Count() > 0)
                                        {
                                            ChkMemoDetail = ChkMemoDetail + 1;
                                        }
                                    }
                                }
                            }

                            if (ChkMemoDetail == 0)
                            {
                                tabMain.SelectTab("tabUnderWriteData");
                                tabUnderWriteMain.SelectTab("tabMemo");
                                txtMemoIDLimitDate.Focus();
                                throw new Exception("กรุณาระบุรายละเอียดของการขอเอกสารเพิ่ม");
                            }
                        }
                        else if (wSummaryObj.STATUS == "IF" && (cmbChannelType.SelectedValue.ToString() == "PD" || cmbChannelType.SelectedValue.ToString() == "PF"))
                        {
                            bool errorFree = false;
                            if (PAR_U_CAL_ERROR != null && PAR_U_CAL_ERROR.ERROR_DT != null)
                            {
                                if (PAR_U_CAL_ERROR.TMN == 'N')
                                {
                                    String strError = "";
                                    if (PAR_U_CAL_ERROR.U_CAL_ERROR_DETAIL_Coll != null && PAR_U_CAL_ERROR.U_CAL_ERROR_DETAIL_Coll.Count() > 0)
                                    {
                                        if (PAR_U_CAL_ERROR.U_CAL_ERROR_DETAIL_Coll.Count() == 1)
                                        {
                                            bool haveError = false;
                                            var GetDataErrorSummFree = from getData in PAR_U_CAL_ERROR.U_CAL_ERROR_DETAIL_Coll
                                                                       where getData.ERROR_CODE == "ER003"
                                                                       select getData;
                                            if (GetDataErrorSummFree != null && GetDataErrorSummFree.Count() > 0 && wSummaryObj.SUBSTATUS == "IF")
                                            {
                                                haveError = true;
                                            }
                                            else if (GetDataErrorSummFree != null && GetDataErrorSummFree.Count() > 0 && wSummaryObj.SUBSTATUS == "CC")
                                            {
                                                errorFree = true;
                                                haveError = false;
                                            }
                                            else
                                            {
                                                haveError = true;
                                            }

                                            if (haveError == true)
                                            {
                                                int i = 0;
                                                foreach (NewBisPASvcRef.U_CAL_ERROR_DETAIL obj in PAR_U_CAL_ERROR.U_CAL_ERROR_DETAIL_Coll)
                                                {
                                                    var GetData = from getData in PAR_AUTB_DATADIC_DET_COLLECTION
                                                                  where getData.COL_NAME == "ERROR_CODE"
                                                                  && getData.CODE == obj.ERROR_CODE
                                                                  select getData;
                                                    if (GetData != null && GetData.Count() > 0)
                                                    {
                                                        i = i + 1;
                                                        strError = strError + i.ToString() + ". " + GetData.ToArray()[0].DESCRIPTION + "\n";
                                                    }
                                                }
                                                throw new Exception("มีข้อผิดพลาดในการคำนวนเบี้ยประกันมีรายละเอียดดังนี้\n" + strError);
                                            }

                                        }
                                        else
                                        {
                                            int i = 0;
                                            foreach (NewBisPASvcRef.U_CAL_ERROR_DETAIL obj in PAR_U_CAL_ERROR.U_CAL_ERROR_DETAIL_Coll)
                                            {
                                                var GetData = from getData in PAR_AUTB_DATADIC_DET_COLLECTION
                                                              where getData.COL_NAME == "ERROR_CODE"
                                                              && getData.CODE == obj.ERROR_CODE
                                                              select getData;
                                                if (GetData != null && GetData.Count() > 0)
                                                {
                                                    i = i + 1;
                                                    strError = strError + i.ToString() + ". " + GetData.ToArray()[0].DESCRIPTION + "\n";
                                                }
                                            }
                                            throw new Exception("มีข้อผิดพลาดในการคำนวนเบี้ยประกันมีรายละเอียดดังนี้\n" + strError);
                                        }

                                    }

                                }
                            }


                            Decimal summFree = txtNowSummFree.Text.Trim() == "" ? 0 : Convert.ToDecimal(txtNowSummFree.Text.Trim().Replace(",", ""));
                            Decimal summFreeTotal = txtTotalSummFree.Text.Trim() == "" ? 0 : Convert.ToDecimal(txtTotalSummFree.Text.Trim().Replace(",", ""));
                            string channelType = cmbChannelType.SelectedValue == null ? "" : cmbChannelType.SelectedValue.ToString();
                            if (!IsChannelTypeFreePolicy(channelType))
                            {
                                if (summFree > 0 && summFreeTotal > 300000 && errorFree == false)
                                {
                                    throw new Exception("ไม่สามารถออกกรมธรรม์ได้ เนื่องจากแบบประกันแถมมีทุนรวมมากกว่า 300,000");
                                }
                            }


                        }

                        //ตรวจสอบว่ามี W_SUUMARY_DETAIL ปัจจุบันหรือไม่ ===============================================
                        foreach (NewBisPASvcRef.W_SUMMARY summaryObj in wSubUnderwriteIDObj.SUMMARY_Collection)
                        {
                            if (summaryObj.SUMMARY_DETAIL_Collection != null && summaryObj.SUMMARY_DETAIL_Collection.Count() > 0)
                            {
                                var GetData = from getData in summaryObj.SUMMARY_DETAIL_Collection
                                              where getData.SUMMARY_ID == null
                                              select getData;
                                if (!(GetData != null && GetData.Count() > 0))
                                {
                                    summaryObj.SUMMARY_DETAIL_Collection = new NewBisPASvcRef.W_SUMMARY_DETAIL_Collection();
                                }
                            }
                            else
                            {
                                summaryObj.SUMMARY_DETAIL_Collection = new NewBisPASvcRef.W_SUMMARY_DETAIL_Collection();
                            }
                        }
                        //END ตรวจสอบว่ามี W_SUUMARY_DETAIL ปัจจุบันหรือไม่ ===========================================
                    }
                }
            }
        }
        private void ValidateUMemoIDData(NewBisPASvcRef.U_MEMO_ID objDetail, String statusCode)
        {
            if (statusCode != "MO" && statusCode != "MD")
            {
                if (objDetail.END_PROCESS == 'N' && objDetail.TMN == 'N')
                {
                    tabMain.SelectTab("tabUnderWriteData");
                    tabUnderWriteMain.SelectTab("tabMemo");
                    txtMemoIDLimitDate.Focus();
                    throw new Exception("ไม่สามารถเปลี่ยนสถานะใบคำขอได้เนื่อง หัวข้อหลัก " + objDetail.SEQ.ToString() + " ยังสถานะ อยู่ในขั้นตอนการดำเนินงาน อยู่");
                }
            }

            if (objDetail.MEMO_DETAIL_Collection != null && objDetail.MEMO_DETAIL_Collection.Count() > 0)
            {
                bool detailSuscess = true;
                foreach (NewBisPASvcRef.U_MEMO_DETAIL uMemoDetailObj in objDetail.MEMO_DETAIL_Collection)
                {
                    if (uMemoDetailObj.PEND_STATUS == "W")
                    {
                        detailSuscess = false;
                    }
                }

                if (objDetail.END_PROCESS == 'Y' && detailSuscess == false)
                {
                    tabMain.SelectTab("tabUnderWriteData");
                    tabUnderWriteMain.SelectTab("tabMemo");
                    txtMemoIDLimitDate.Focus();
                    throw new Exception("สถานะของรายละเอียดย่อยของเอกสารเพิ่มหัวข้อลำดับที่ " + objDetail.SEQ.ToString() + " ยังมีสถานะรอเอกสารอยู่");
                }
            }
            else
            {
                tabMain.SelectTab("tabUnderWriteData");
                tabUnderWriteMain.SelectTab("tabMemo");
                txtMemoIDLimitDate.Focus();
                throw new Exception("กรุณาระบุรายละเอียดของหัวข้อย่อยในหัวข้อหลัก ลำดับที่ " + objDetail.SEQ.ToString());
            }
        }
        private void ValidateUApplicationName(NewBisPASvcRef.U_APPLICATION_NAME objDetail)
        {
            var GetData = from getData in PAR_AUTB_APPNAME_ID_COLLECTION
                          where getData.CHANNEL_TYPE == cmbChannelType.SelectedValue.ToString()
                          select getData;
            if (GetData != null && GetData.Count() > 0)
            {
                if (!(objDetail != null && objDetail.APPNAME_ID != 0))
                {
                    tabMain.SelectTab("tabCustomerData");
                    throw new Exception("ไม่พบข้อมูลของเอกสารชุดใบคำขอ");
                }
            }
        }
    }
}
