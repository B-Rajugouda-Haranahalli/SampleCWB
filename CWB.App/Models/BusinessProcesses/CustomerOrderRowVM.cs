using CWB.CommonUtils.Common;
using System;
using System.Collections.Generic;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace CWB.App.Models.BusinessProcesses
{
    public class CustomerOrderRowVM :CustomerOrderVM //for display
    {
        
        public int Status { get; set; }
        public int Plan { get; set; }
        public int Matl { get; set; }
        public int WIP { get; set; }
        public bool Hold { get; set; }
        public bool Done { get; set; }
  
    }
}
