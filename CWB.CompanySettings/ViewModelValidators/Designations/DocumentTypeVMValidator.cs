using CWB.CompanySettings.ViewModels.Designations;
using CWB.CompanySettings.ViewModelValidatorsMessage.Designations;
using FluentValidation;

namespace CWB.CompanySettings.ViewModelValidators.Designations
{
    public class DesignationVMValidator : AbstractValidator<DesignationVM>
    {
        public DesignationVMValidator()
        {

            RuleFor(v => v.Name)
                   .NotEmpty().WithMessage(DesignationVMValidatorMessage.EmptyDesignation);

            RuleFor(v => v.TenantId)
                  .NotEmpty().WithMessage(DesignationVMValidatorMessage.EmptyTenantId);
        }

    }
}