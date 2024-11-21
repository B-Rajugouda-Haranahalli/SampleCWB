using System.ComponentModel;

namespace CWB.Masters.MastersUtils
{
    public enum MRItemType
    {
        [Description("Consumable Item")]
        ConsumableItem,
        [Description("Durable Item")]
        DurableItem,
        [Description("Assembly")]
        Assembly,
        [Description("Support Equipment")]
        SupportEquipment
    }
}
