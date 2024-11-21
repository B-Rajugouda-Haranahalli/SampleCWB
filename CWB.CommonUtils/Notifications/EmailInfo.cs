using System;
using System.Collections.Generic;
using System.Text;

namespace CWB.CommonUtils.Notifications
{
    public class EmailInfo
    {
        public string ToAddress { get; set; }
        public string Name { get; set; }
        public string TemplateId { get; set; }
        public object EmailObject { get; set; }
    }
}
