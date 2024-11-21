using CWB.Constants.Tenant;
using CWB.Tenant.ViewModels;
using CWB.Tenant.ViewModelValidatorsMessage;
using FluentValidation;

namespace CWB.Tenant.ViewModelValidators
{
    public class TenantRequestApvRjtVMValidator : AbstractValidator<TenantRequestApproveRejectVM>
    {
        public TenantRequestApvRjtVMValidator()
        {
            RuleFor(t => t.TenantRequestId)
              .NotEmpty().WithMessage(TenantRequestApvRjtVMValidatorMessage.EmptyRequestId);

            RuleFor(t => t.Status)
               .NotEmpty().WithMessage(TenantRequestApvRjtVMValidatorMessage.EmptyRequestStatus)
               .Must(s => s == TenantStatus.Approve || s == TenantStatus.Reject).WithMessage(TenantRequestApvRjtVMValidatorMessage.ValidRequestStatus);

            RuleFor(t => t.Comments)
              .MaximumLength(4000).WithMessage(TenantRequestApvRjtVMValidatorMessage.MaxLengthComment);
        }
    }
}
