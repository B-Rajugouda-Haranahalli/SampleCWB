
using CWB.Masters.ViewModels.Company;
using CWB.Masters.ViewModelValidatorsMessage.Company;
using FluentValidation;

namespace CWB.Masters.Company.ViewModelValidators
{
    public class CheckDivisionVMValidator : AbstractValidator<CheckDivisionVM>
    {
        public CheckDivisionVMValidator()
        {
            //RuleFor(v => v.CompanyId)
            //  .NotEmpty().WithMessage(CheckDivisionVMValidatorMessage.EmptyCompanyId);
            //RuleFor(v => v.DivisionId)
            //  .NotEmpty().WithMessage(CheckDivisionVMValidatorMessage.EmptyDivisionId);

            RuleFor(v => v.DivisionName)
                   .NotEmpty().WithMessage(CheckDivisionVMValidatorMessage.EmptyDivisionName);

            RuleFor(v => v.TenantId)
                  .NotEmpty().WithMessage(CheckDivisionVMValidatorMessage.EmptyTenantId);
        }

    }
}
