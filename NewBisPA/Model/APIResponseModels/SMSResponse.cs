using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewBisPA.Model.APIResponseModels
{
    public class SMSResult
    {
        public bool successed { get; set; }
        public string errorMessage { get; set; }
        public int result { get; set; }
    }

    public class SMSResponse
    {
        public SMSResult processResult { get; set; }
    }
}
