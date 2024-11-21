﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CWB.App.Models.Machine
{
    public class McSlNoDocListVM
    {
        public long McSlNoDocListId { get; set; }
        public long McId { get; set; }
        public long DocumentTypeId { get; set; }
        public char Mandatory { get; set; }
        public long UpdatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        public long TenantId { get; set; }
        public string DocumentTypeName { get; set; } = string.Empty;
    }
}
