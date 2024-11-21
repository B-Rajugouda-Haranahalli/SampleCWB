using CWB.Masters.ViewModels.Machines;
using CWB.Masters.ViewModelValidatorsMessage.Machines;
using FluentValidation;

namespace CWB.Masters.ViewModelValidators.Machines
{
    public class MachineVMValidator : AbstractValidator<MachineVM>
    {
        public MachineVMValidator()
        {
            RuleFor(v => v.MachinePlantId)
                   .NotEmpty().WithMessage(MachineVMValidatorMessage.EmptyPlantId);
            RuleFor(v => v.MachineDepartmentId)
                  .NotEmpty().WithMessage(MachineVMValidatorMessage.EmptyDepartmentId);
            RuleFor(v => v.TenantId)
                  .NotEmpty().WithMessage(MachineVMValidatorMessage.EmptyTenantId);
            RuleFor(v => v.MachineMachineName)
                  .NotEmpty().WithMessage(MachineVMValidatorMessage.EmptyMachineName);
            RuleFor(v => v.MachineMachineSlNo)
                  .NotEmpty().WithMessage(MachineVMValidatorMessage.EmptySlNo);
            RuleFor(v => v.MachineMachineManufacturer)
                  .NotEmpty().WithMessage(MachineVMValidatorMessage.EmptyManufacturer);
            RuleFor(v => v.MachineOperationListId)
                  .NotEmpty().WithMessage(MachineVMValidatorMessage.EmptyOperationListId);
            RuleFor(v => v.MachineMachineTypeId)
                  .NotEmpty().WithMessage(MachineVMValidatorMessage.EmptyMachineTypeId);
        }
    }
}