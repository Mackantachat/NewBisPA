using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewBisPA.ReportContract
{
    public class REPORT_MEMO_DETAIL
    {
        DateTime _letter_dt;
        public DateTime Letter_dt
        {
            get { return _letter_dt; }
            set { _letter_dt = value; }
        }

        string _letter_header;
        public string Letter_header
        {
            get { return _letter_header; }
            set { _letter_header = value; }
        }

        string _signature;
        public string Signature
        {
            get { return _signature; }
            set { _signature = value; }
        }

        string _position;
        public string Position
        {
            get { return _position; }
            set { _position = value; }
        }

        string _userid;
        public string Userid
        {
            get { return _userid; }
            set { _userid = value; }
        }

        string _company;
        public string Company
        {
            get { return _company; }
            set { _company = value; }
        }

        string _phone_number;
        public string Phone_number
        {
            get { return _phone_number; }
            set { _phone_number = value; }
        }

        string _agentname;
        public string Agentname
        {
            get { return _agentname; }
            set { _agentname = value; }
        }

        string _agentcode;
        public string Agentcode
        {
            get { return _agentcode; }
            set { _agentcode = value; }
        }

        string _agentuplname;
        public string Agentuplname
        {
            get { return _agentuplname; }
            set { _agentuplname = value; }
        }

        string _agentuplcode;
        public string Agentuplcode
        {
            get { return _agentuplcode; }
            set { _agentuplcode = value; }
        }

        string _sale_ofc;
        public string Sale_ofc
        {
            get { return _sale_ofc; }
            set { _sale_ofc = value; }
        }
    }

    public class REPORT_MEMO_DETAIL_Collection : List<REPORT_MEMO_DETAIL>
    {
    }
}
