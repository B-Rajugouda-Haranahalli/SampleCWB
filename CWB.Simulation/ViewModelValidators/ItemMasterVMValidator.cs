using CWB.Simulation.SimulationUtils;
using CWB.Simulation.ViewModels;
using CWB.Simulation.ViewModelValidatorsMessage;
using FluentValidation;
using System;

namespace CWB.Simulation.ViewModelValidators
{
    public class ItemMasterVMValidator : AbstractValidator<ItemMasterVM>
    {
        public ItemMasterVMValidator()
        {
            RuleFor(t => t.CustomerId)
              .NotEmpty().WithMessage(ItemMasterVMValidatorMessage.EmptyCustomerId);

            RuleFor(t => t.PartNo)
              .NotEmpty().WithMessage(ItemMasterVMValidatorMessage.EmptyPartNo);

            RuleFor(t => t.RevNo)
              .NotEmpty().WithMessage(ItemMasterVMValidatorMessage.EmptyRevNo);

            RuleFor(t => t.RevDate)
              .NotEmpty()
              .Must(date => date != default(DateTime)).WithMessage(ItemMasterVMValidatorMessage.EmptyRevDate);

            RuleFor(t => t.PartDescription)
              .NotEmpty().WithMessage(ItemMasterVMValidatorMessage.EmptyPartDescription);

            RuleFor(t => t.PartAssembly)
               .NotEmpty().WithMessage(ItemMasterVMValidatorMessage.EmptyPartAssembly)
               .Must(s => s == PartAssembly.Assembly || s == PartAssembly.Part).WithMessage(ItemMasterVMValidatorMessage.ValidPartAssembly);

            RuleFor(t => t.MakeBOF)
               .NotEmpty().WithMessage(ItemMasterVMValidatorMessage.EmptyMakeBOF)
               .Must(s => s == MakeBOF.BOF || s == MakeBOF.Make).WithMessage(ItemMasterVMValidatorMessage.ValidMakeBOF);
        }
    }
}
