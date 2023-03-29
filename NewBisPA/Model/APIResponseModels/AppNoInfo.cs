using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewBisPA.Model.APIResponseModels
{
    public class AppNoInfo
    {
        public string Policy { get; set; }
        public long? PolicyId { get; set; }
        public string ChannelType { get; set; }
        public string AppNo { get; set; }
        public DateTime? AppDate { get; set; }
        public string PlanCode { get; set; }
        public string PlanDesc { get; set; }
        public RiskInfo[] RiskInfos { get; set; }
    }
}
