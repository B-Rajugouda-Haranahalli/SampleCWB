
using CWB.Masters.ViewModels.Company;
using CWB.Masters.ViewModelValidatorsMessage.Company;
using FluentValidation;

namespace CWB.Masters.Company.ViewModelValidators
{
    public class CheckCompanyVMValidator : AbstractValidator<CheckCompanyVM>
    {
        public CheckCompanyVMValidator()
        {
            //RuleFor(v => v.CompanyId)
            //  .NotEmpty().WithMessage(CheckCompanyVMValidatorMessage.EmptyCompanyId);

            RuleFor(v => v.CompanyName)
                   .NotEmpty().WithMessage(CheckCompanyVMValidatorMessage.EmptyCompanyName);

            RuleFor(v => v.TenantId)
                  .NotEmpty().WithMessage(CheckCompanyVMValidatorMessage.EmptyTenantId);
        }

    }
}
