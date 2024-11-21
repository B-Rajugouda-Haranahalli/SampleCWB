using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CWB.App.Models.CoSettings
{
    public class CountryVM
    {
        public long CountryId { get; set; }
        public string Name { get; set; }
        public long TenantId { get; set; }
    }
}
