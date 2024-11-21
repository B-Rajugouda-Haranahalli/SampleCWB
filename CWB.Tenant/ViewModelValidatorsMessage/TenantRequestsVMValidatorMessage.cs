namespace CWB.Tenant.ViewModelValidatorsMessage
{
    public static class TenantRequestsVMValidatorMessage
    {
        public static string EmptyTenantCompanyName => "Please enter your Tenant Company Name";
        public static string MaxLengthTenantCompanyName => "The Tenant Company Name should be less than 150 characters";
        public static string EmptyEmail => "Please enter your Email";
        public static string ValidEmail => "Please enter Valid Email";
        public static string EmptyPhoneNumber => "Please enter your Phone Number";
        public static string MaxPhoneNumberLength => "The Phone Number sheould be less than 15 characters";
        public static string MaxLengthInfo => "The Company Info should be less than 4000 characters";
    }
}
