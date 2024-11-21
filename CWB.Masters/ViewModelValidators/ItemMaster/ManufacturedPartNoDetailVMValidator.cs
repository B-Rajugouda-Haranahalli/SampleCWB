using CWB.Masters.ViewModels.ItemMaster;
using CWB.Masters.ViewModelValidatorsMessage.Company;
using CWB.Masters.ViewModelValidatorsMessage.ItemMaster;
using FluentValidation;

namespace CWB.Masters.ViewModelValidators.ItemMaster
{
    public class ManufacturedPartNoDetailVMValidator : AbstractValidator<ManufacturedPartNoDetailVM>
    {
        public ManufacturedPartNoDetailVMValidator()
        {
            RuleFor(v => v.ManufacturedPartType)
                   .NotEmpty().WithMessage(ManufacturedPartNoDetailVMValidatorMessage.EmptyManufacturedPartType);
          /*  RuleFor(v => v.CompanyName)
                   .NotEmpty().WithMessage(ManufacturedPartNoDetailVMValidatorMessage.EmptyCompanyName);
            RuleFor(v => v.PartNumber)
                  .NotEmpty().WithMessage(ManufacturedPartNoDetailVMValidatorMessage.EmptyPartNumber);*/
            RuleFor(v => v.PartDescription)
                  .NotEmpty().WithMessage(ManufacturedPartNoDetailVMValidatorMessage.EmptyPartDescription);
            RuleFor(v => v.RevNo)
                  .NotEmpty().WithMessage(ManufacturedPartNoDetailVMValidatorMessage.EmptyRevNo);
            //RuleFor(v => v.Quantity)
            //      .NotEmpty().WithMessage(ManufacturedPartNoDetailVMValidatorMessage.EmptyQuantity);
            RuleFor(v => v.TenantId)
                  .NotEmpty().WithMessage(ManufacturedPartNoDetailVMValidatorMessage.EmptyTenantId);
        }

    }
}
