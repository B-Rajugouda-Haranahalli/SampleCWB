using CWB.Simulation.SimulationUtils;
using CWB.Simulation.ViewModels;
using CWB.Simulation.ViewModelValidatorsMessage;
using FluentValidation;

namespace CWB.Simulation.ViewModelValidators
{
    public class MRBomVMValidator : AbstractValidator<MRBomVM>
    {
        public MRBomVMValidator()
        {
            RuleFor(t => t.ItemDescription)
              .NotEmpty().WithMessage(MRBomVMValidatorMessage.EmptyItemDescription);

            RuleFor(t => t.SupplierId)
               .NotEmpty().WithMessage(MRBomVMValidatorMessage.EmptySupplierId);

            RuleFor(t => t.Cost)
              .NotEmpty().WithMessage(MRBomVMValidatorMessage.EmptyCost);

            RuleFor(t => t.QuantityOnHand)
               .NotEmpty().WithMessage(MRBomVMValidatorMessage.EmptyQuantityOnHand);

            RuleFor(t => t.MRBomGroupId)
               .NotEmpty().WithMessage(MRBomVMValidatorMessage.EmptyMRBomGroupId);

            RuleFor(t => t.TenantId)
               .NotEmpty().WithMessage(MRBomVMValidatorMessage.EmptyTenantId);

            RuleFor(t => t.ItemType)
               .NotEmpty().WithMessage(MRBomVMValidatorMessage.EmptyItemType)
               .Must(s => s == MRBomItemType.Consumable || s == MRBomItemType.Durable || s == MRBomItemType.SupportEquipment).WithMessage(MRBomVMValidatorMessage.ValidItemType);

            RuleFor(t => t.UoM)
               .NotEmpty().WithMessage(MRBomVMValidatorMessage.EmptyUoM)
               .Must(s => s == MRBomUoM.Nos || s == MRBomUoM.Litres || s == MRBomUoM.Kgs).WithMessage(MRBomVMValidatorMessage.ValidUoM);

            RuleFor(t => t.ConsumptionType)
               .NotEmpty().WithMessage(MRBomVMValidatorMessage.EmptyConsumptionType)
               .Must(s => s == MRBomConsumptionType.ConsumptionPerPart || s == MRBomConsumptionType.LifeForPart).WithMessage(MRBomVMValidatorMessage.ValidConsumptionType);

        }
    }
}
