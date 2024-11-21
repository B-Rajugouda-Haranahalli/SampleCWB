using CWB.Tenant.ViewModels;
using CWB.Tenant.ViewModelValidatorsMessage;
using FluentValidation;

namespace CWB.Tenant.ViewModelValidators
{
    public class TenantRequestsVMValidator : AbstractValidator<TenantRequestsVM>
    {
        public TenantRequestsVMValidator()
        {
            RuleFor(t => t.CompanyName)
              .NotEmpty().WithMessage(TenantRequestsVMValidatorMessage.EmptyTenantCompanyName)
              .MaximumLength(150).WithMessage(TenantRequestsVMValidatorMessage.MaxLengthTenantCompanyName);

            RuleFor(t => t.Email)
               .NotEmpty().WithMessage(TenantRequestsVMValidatorMessage.EmptyEmail)
               .EmailAddress().WithMessage(TenantRequestsVMValidatorMessage.ValidEmail);

            RuleFor(t => t.Phone)
              .NotEmpty().WithMessage(TenantRequestsVMValidatorMessage.EmptyPhoneNumber)
              .MaximumLength(15).WithMessage(TenantRequestsVMValidatorMessage.MaxPhoneNumberLength);

            RuleFor(t => t.CompanyInfo)
              .MaximumLength(4000).WithMessage(TenantRequestsVMValidatorMessage.MaxLengthInfo);
        }
    }
}
