using CWB.Masters.Domain;
using CWB.Masters.ViewModels.ItemMaster;
using CWB.Masters.ViewModelValidatorsMessage.Company;
using CWB.Masters.ViewModelValidatorsMessage.ItemMaster;
using FluentValidation;

namespace CWB.Masters.ViewModelValidators.ItemMaster
{
    public class BoughtOutFinishDetailVMValidator : AbstractValidator<BoughtOutFinishDetailVM>
    {
        public BoughtOutFinishDetailVMValidator()
        {
            RuleFor(v => v.BoughtOutFinishMadeType)
                   .NotEmpty().WithMessage(BoughtOutFinishDetailVMValidatorMessage.EmptyBoughtOutFinishMadeType);
            RuleFor(v => v.PartDescription)
                   .NotEmpty().WithMessage(BoughtOutFinishDetailVMValidatorMessage.EmptyPartDescription);
           // RuleFor(v => v.Supplier)
             //     .NotEmpty().WithMessage(BoughtOutFinishDetailVMValidatorMessage.EmptySupplier);
            RuleFor(v => v.TenantId)
                  .NotEmpty().WithMessage(BoughtOutFinishDetailVMValidatorMessage.EmptyTenantId);
        }

    }
}
