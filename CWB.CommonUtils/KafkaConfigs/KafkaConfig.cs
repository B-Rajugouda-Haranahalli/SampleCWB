using System;
using System.Collections.Generic;
using System.Text;

namespace CWB.CommonUtils.KafkaConfigs
{
    public class KafkaConfig
    {
        public string GroupId { get; set; }
        public string Server { get; set; }
        public string Topic { get; set; }

    }
}
