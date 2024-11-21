using CWB.Tenant.ViewModels;
using CWB.Tenant.ViewModelValidatorsMessage;
using FluentValidation;

namespace CWB.Tenant.ViewModelValidators
{
    public class TenantVMValidator : AbstractValidator<TenantVM>
    {
        public TenantVMValidator()
        {
            RuleFor(t => t.CompanyName)
             .NotEmpty().WithMessage(TenantVMValidatorMessage.EmptyTenantCompanyName)
             .MaximumLength(150).WithMessage(TenantVMValidatorMessage.MaxLengthTenantCompanyName);

            RuleFor(t => t.Email)
               .NotEmpty().WithMessage(TenantVMValidatorMessage.EmptyEmail)
               .EmailAddress().WithMessage(TenantVMValidatorMessage.ValidEmail);

            RuleFor(t => t.Phone)
              .NotEmpty().WithMessage(TenantVMValidatorMessage.EmptyPhoneNumber)
              .MaximumLength(15).WithMessage(TenantVMValidatorMessage.MaxPhoneNumberLength);

            RuleFor(t => t.CompanyInfo)
              .MaximumLength(4000).WithMessage(TenantVMValidatorMessage.MaxLengthInfo);
        }
    }
}
