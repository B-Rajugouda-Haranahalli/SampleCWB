using System.ComponentModel;

namespace CWB.Masters.MastersUtils
{
    public enum CompanyType
    {

        [Description("Customer")]
        Customer,
        [Description("Supplier")]
        Supplier,
        [Description("Both")]
        Both
    }
}
