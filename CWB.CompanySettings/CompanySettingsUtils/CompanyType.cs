using System.ComponentModel;

namespace CWB.CompanySettings.CompanySettingsUtils
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
