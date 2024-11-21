using CWB.Masters.ViewModels.ItemMaster;
using CWB.Masters.ViewModelValidatorsMessage.Company;
using CWB.Masters.ViewModelValidatorsMessage.ItemMaster;
using FluentValidation;

namespace CWB.Masters.ViewModelValidators.ItemMaster
{
    public class RawMaterialDetailVMValidator : AbstractValidator<RawMaterialDetailVM>
    {
        public RawMaterialDetailVMValidator()
        {
            RuleFor(v => v.RawMaterialMadeType)
                   .NotEmpty().WithMessage(RawMaterialDetailVMValidatorMessage.EmptyRawMaterialMadeType);
            RuleFor(v => v.RawMaterialTypeId)
                   .NotEmpty().WithMessage(RawMaterialDetailVMValidatorMessage.EmptyRawMaterialTypeId);
            RuleFor(v => v.PartNo)
                  .NotEmpty().WithMessage(RawMaterialDetailVMValidatorMessage.EmptyInHousePartNo);
            RuleFor(v => v.PartDescription)
                  .NotEmpty().WithMessage(RawMaterialDetailVMValidatorMessage.EmptyPartDescription);
            RuleFor(v => v.BaseRawMaterialId)
                  .NotEmpty().WithMessage(RawMaterialDetailVMValidatorMessage.EmptyBaseRawMaterialId);
            //RuleFor(v => v.PurchaseDetailId)
              //    .NotEmpty().WithMessage(RawMaterialDetailVMValidatorMessage.EmptyPurchaseDetailId);
            RuleFor(v => v.TenantId)
                  .NotEmpty().WithMessage(RawMaterialDetailVMValidatorMessage.EmptyTenantId);
        }

    }
}
