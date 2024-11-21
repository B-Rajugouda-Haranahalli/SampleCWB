using CWB.Constants.Tenant;

namespace CWB.Tenant.ViewModelValidatorsMessage
{
    public static class TenantRequestApvRjtVMValidatorMessage
    {
        public static string EmptyRequestId => "Please enter your Request Id";
        public static string ValidRequestStatus => $"Please enter Valid Request Status, Allowed Status: {TenantStatus.Approve} or {TenantStatus.Reject} ";
        public static string EmptyRequestStatus => "Please enter your Request Status";
        public static string MaxLengthComment => "Comments should be less than 4000 characters";
    }
}
