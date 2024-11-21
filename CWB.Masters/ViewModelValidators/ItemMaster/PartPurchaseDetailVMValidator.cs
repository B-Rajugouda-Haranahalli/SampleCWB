using CWB.Masters.ViewModels.ItemMaster;
using CWB.Masters.ViewModelValidatorsMessage.Company;
using CWB.Masters.ViewModelValidatorsMessage.ItemMaster;
using FluentValidation;

namespace CWB.Masters.ViewModelValidators.ItemMaster
{
    public class PartPurchaseDetailVMValidator : AbstractValidator<PartPurchaseDetailsVM>
    {
        public PartPurchaseDetailVMValidator()
        {
            RuleFor(v => v.PSupplierPartNo)
                   .NotEmpty().WithMessage(PartPurchaseValidatorMessage.EmptySupplierPartNo);
            RuleFor(v => v.PSupplierId)
                   .NotEmpty().WithMessage(PartPurchaseValidatorMessage.EmptySupplierId);
            RuleFor(v => v.ShareOfBusiness)
                  .NotEmpty().WithMessage(PartPurchaseValidatorMessage.EmptyShareOfBusiness);
            RuleFor(v => v.MinimumOrderQuantity)
                  .NotEmpty().WithMessage(PartPurchaseValidatorMessage.EmptyMinimumOrderQuantity);
            RuleFor(v => v.Price)
                  .NotEmpty().WithMessage(PartPurchaseValidatorMessage.EmptyPrice);
            RuleFor(v => v.LeadTimeInDays)
                  .NotEmpty().WithMessage(PartPurchaseValidatorMessage.EmptyLeadTimeInDays);
            RuleFor(v => v.PPartId)
                  .NotEmpty().WithMessage(PartPurchaseValidatorMessage.EmptyMasterPartId);
        }

    }
}
