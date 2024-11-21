﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CWB.Masters.ViewModels.Machines
{
    public class McTypeDocListVM
    {
        public long McTypeDocListId { get; set; }
        public long McTypeId { get; set; }
        public long DocumentTypeId { get; set; }
        public char Mandatory { get; set; }
        public long UpdatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        public long TenantId { get; set; }
    }
}