using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewBisPA.Library.Resource
{
    public static class SystemConstant
    {

        public const string ReturnPremiumPayAccountNoForRenewApp = "21220001";
        public const string JobCodePayOptionForNewBis = "J70";
        public const string SubJobCodePayOptionForNewBisBANC = "B41";
        public const string SubJobCodePayOptionForNewBisOther = "B40";
        public const char YesDBFlag = 'Y';


        public const string SMSDataTypeForCustomer = "C";
        public const string SMSDataTypeForAgent = "A";

        public const string SMSReferanceByAppNo = "APP";
        public const string SMSReferanceByPolicyId = "POLICY_ID";




        public const string SMSServiceReceiveCode_Customer = "CUS";
        public const string SMSServiceReceiveCode_Agent = "AGT";

        public const string SMSServiceReferance_App = "APP";
        public const string SMSServiceReferance_PolicyId = "POL";

    }
}