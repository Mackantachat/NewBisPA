using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewBisPA.Library
{
    public class RenewalRepository
    {
        public NewBISSvcRef.CSTempPaymentInfo GetRenewalPaymentTransection(string appNo, string channelType,bool isEndProcess, NewBISSvcRef.ERenewStatusCase status)
        {


            var client = new NewBISSvcRef.NewBISSvcClient();
            NewBISSvcRef.U_RENEWAL_CASE renewCase = null;
            var pr = client.GetRenewalCase(out renewCase, appNo, status);
            if (pr.Successed && renewCase != null)
            {
                NewBISSvcRef.CSTempPaymentInfo dataObject;
                if (isEndProcess)
                {
                    pr = client.GetCSTempPaymentTrandectionForEndProcess(out dataObject, channelType, renewCase.POLICY_ID.Value);
                }
                else
                {
                    pr = client.GetCSTempPaymentInfo(out dataObject, channelType, renewCase.POLICY_ID.Value);
                }

                if (pr.Successed)
                {
                    return dataObject;
                }
                else
                {
                    throw new Exception(pr.ErrorMessage);
                }

            }
            return null;

        }
        public bool IsRenewApplication(string appNo,string channelType)
        {


            var client = new NewBISSvcRef.NewBISSvcClient();
            NewBISSvcRef.U_RENEWAL_CASE renewCase = null;
            var pr = client.GetRenewalCaseWithChannelType(out renewCase, channelType, appNo, NewBISSvcRef.ERenewStatusCase.RewnewOrAllowToGrant);
            if (pr.Successed)
            {
                if (renewCase != null)
                {
                    return true;
                }

                return false;
            }
            else
            {
                throw new Exception(pr.ErrorMessage);
            }

        }

    }
}

