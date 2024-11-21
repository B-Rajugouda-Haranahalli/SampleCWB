using CWB.Simulation.ViewModels;
using CWB.Simulation.ViewModelValidatorsMessage;
using FluentValidation;

namespace CWB.Simulation.ViewModelValidators
{
    public class MachineVMValidator : AbstractValidator<MachineVM>
    {
        public MachineVMValidator()
        {
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage(MachineVMValidatorMessage.EmptyName);
        }
    }
}
