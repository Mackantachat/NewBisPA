using NewBisPA.Library.Resource;
using NewBisPA.Model.APIResponseModels;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewBisPA.Library
{
    public class SMSProvideService
    {
        public string GetSMSWorkCode(string workGroup)
        {
            if (WorkGroupConstant.Bancassurance.Equals(workGroup))
            {
                return ITUtility.Utility.AppSettings("SMS_WORKCODE_BNC");
            }
            else
            {
                return ITUtility.Utility.AppSettings("SMS_WORKCODE_OD");
            }
        }

        public void SendSMSV2(string message, string mobilePhone, string workCode, string referenceCode, string referenceNo, string receiveCode, string receiveNo, string userId)
        {

            var smsURL = ITUtility.Utility.AppSettings("SMSAPI_URL");
            var appCode = ITUtility.Utility.AppSettings("ApplicationCode");
            var restClient = new RestClient(smsURL);
            var restRequet = new RestRequest("api/SMS/SendSMS", Method.POST);
            restRequet.RequestFormat = DataFormat.Json;
            restRequet.AddJsonBody(new
            {
                phoneNumber = mobilePhone,
                workCode = workCode,
                message = message,
                application = appCode,
                updid = userId,
                referenceCode = referenceCode,
                referenceNo = referenceNo,
                receiveCode = receiveCode,
                receiveNo = receiveNo
            });
            var response = restClient.Execute(restRequet);
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new Exception($"SendSMSService Error StatusCode {response.ResponseStatus} ,  Message : {response.Content} ");
            }
            var responseData = JsonConvert.DeserializeObject<SMSResponse>(response.Content);
            if (!responseData.processResult.successed)
            {
                throw new Exception($"SendSMSService Error : {responseData.processResult.errorMessage}");
            }
        }



    }
}
