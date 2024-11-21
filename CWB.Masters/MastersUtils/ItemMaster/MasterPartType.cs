using System.ComponentModel;

namespace CWB.Masters.MastersUtils.ItemMaster
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
