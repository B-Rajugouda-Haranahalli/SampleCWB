using CWB.Simulation.ViewModels;
using CWB.Simulation.ViewModelValidatorsMessage;
using FluentValidation;

namespace CWB.Simulation.ViewModelValidators
{
    public class PlantVMValidator : AbstractValidator<PlantVM>
    {
        public PlantVMValidator()
        {
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage(PlantVMValidatorMessage.EmptyName);
        }
    }
}
