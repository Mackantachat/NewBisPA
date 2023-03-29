using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewBisPA.Model.APIResponseModels
{
    public class CheckAmloData
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public string RiskDesc { get; set; }
        public string RiskCode { get; set; }
    }
}
