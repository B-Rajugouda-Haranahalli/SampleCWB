using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace CWB.App.Models.ItemMaster
{
    public enum MasterPartType
    {
        [Description("Manufactured Part")]
        ManufacturedPart,
        [Description("Bill Of Material")]
        BOM,
        [Description("Bought Out Finish")]
        BOF,
        [Description("Raw Material")]
        RawMaterial

    }
}
