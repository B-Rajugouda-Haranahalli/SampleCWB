using CWB.Masters.ViewModels.OperationList;
using CWB.Masters.ViewModelValidatorsMessage.Company;
using FluentValidation;

namespace CWB.Masters.OperationList.ViewModelValidators
{
    public class CheckOperationVMValidator : AbstractValidator<CheckOperationVM>
    {
        public CheckOperationVMValidator()
        {
            //RuleFor(v => v.CompanyId)
            //  .NotEmpty().WithMessage(CheckCompanyVMValidatorMessage.EmptyCompanyId);

            RuleFor(v => v.Operation)
                   .NotEmpty().WithMessage(CheckOperationVMValidatorMessage.EmptyOperation);

            RuleFor(v => v.TenantId)
                  .NotEmpty().WithMessage(CheckOperationVMValidatorMessage.EmptyTenantId);
        }

    }
}
