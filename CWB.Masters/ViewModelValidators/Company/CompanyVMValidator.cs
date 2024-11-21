using CWB.Masters.ViewModels.Company;
using CWB.Masters.ViewModelValidatorsMessage.Company;
using FluentValidation;

namespace CWB.Masters.ViewModelValidators.Company
{
    public class CompanyVMValidator : AbstractValidator<CompanyVM>
    {
        public CompanyVMValidator()
        {
            RuleFor(v => v.CompanyName)
                   .NotEmpty().WithMessage(CheckCompanyVMValidatorMessage.EmptyCompanyName);
            RuleFor(v => v.DivisionName)
                   .NotEmpty().WithMessage(CompanyVMValidatorMessage.EmptyDivisionName);
            RuleFor(v => v.TenantId)
                  .NotEmpty().WithMessage(CompanyVMValidatorMessage.EmptyTenantId);
            RuleFor(v => v.Location)
                   .NotEmpty().WithMessage(CompanyVMValidatorMessage.EmptyLocation);
        }
    }
}
