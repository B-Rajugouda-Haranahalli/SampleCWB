using CWB.Masters.ViewModels.Machines;
using CWB.Masters.ViewModelValidatorsMessage.Machines;
using FluentValidation;

namespace CWB.Masters.ViewModelValidators.Machines
{
    public class MachineProcDocumentVMValidator : AbstractValidator<MachineProcDocumentVM>
    {
        public MachineProcDocumentVMValidator()
        {
            RuleFor(v => v.TenantId)
                  .NotEmpty().WithMessage(MachineProcDocumentVMValidatorMessage.EmptyTenantId);
            RuleFor(v => v.MachineProcDocumentTypeId)
                   .NotEmpty().WithMessage(MachineProcDocumentVMValidatorMessage.EmptyDocumentTypeId);
            RuleFor(v => v.MachineProcDocumentMachineId)
                   .NotEmpty().WithMessage(MachineProcDocumentVMValidatorMessage.EmptyMachineId);
        }

    }
}
