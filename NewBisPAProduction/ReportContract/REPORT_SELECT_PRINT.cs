using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewBisPA
{
    public class REPORT_SELECT_PRINT
    {
        string _appNo; 
        public string AppNo
        {
            get { return _appNo; }
            set { _appNo = value; }
        }

        long _uletterId;
        public long UletterId
        {
            get { return _uletterId; }
            set { _uletterId = value; }
        }

        long _jobId; 
        public long JobId
        {
            get { return _jobId; }
            set { _jobId = value; }
        }

        string _userId;
        public string UserId
        {
            get { return _userId; }
            set { _userId = value; }
        }

        string _approved;
        public string approved
        {
            get { return _approved; }
            set { _approved = value; }
        }

        string _printDate;
        public string printDate
        {
            get { return _printDate; }
            set { _printDate = value; }
        }

        string _channelType;
        public string channelType
        {
            get { return _channelType; }
            set { _channelType = value; }
        }

        string _policyHolding;
        public string policyHolding
        {
            get { return _policyHolding; }
            set { _policyHolding = value; }
        }

        string _choiceFlg;
        public string choiceFlg
        {
            get { return _choiceFlg; }
            set { _choiceFlg = value; }
        }

        string _office;
        public string office
        {
            get { return _office; }
            set { _office = value; }
        }

        string _sendofc;
        public string sendofc
        {
            get { return _sendofc; }
            set { _sendofc = value; }
        }

        string _plan;
        public string plan
        {
            get { return _plan; }
            set { _plan = value; }
        }
    }
    
    public class REPORT_SELECT_PRINT_Collection : List<REPORT_SELECT_PRINT>
    {
    }
}
