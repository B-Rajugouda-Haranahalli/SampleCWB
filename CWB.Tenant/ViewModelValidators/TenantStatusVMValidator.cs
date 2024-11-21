using CWB.Tenant.ViewModels;
using CWB.Tenant.ViewModelValidatorsMessage;
using FluentValidation;

namespace CWB.Tenant.ViewModelValidators
{
    public class TenantStatusVMValidator : AbstractValidator<TenantStatusVM>
    {
        public TenantStatusVMValidator()
        {
            RuleFor(t => t.TenantId)
              .NotEmpty().WithMessage(TenantStatusVMValidatorMessage.EmptyTenantId);

            RuleFor(t => t.Status)
               .NotNull().WithMessage(TenantStatusVMValidatorMessage.EmptyTenantStatus);
        }
    }
}
