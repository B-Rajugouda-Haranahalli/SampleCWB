using CWB.Simulation.ViewModels;
using CWB.Simulation.ViewModelValidatorsMessage;
using FluentValidation;

namespace CWB.Simulation.ViewModelValidators
{
    public class MRBomGroupVMValidator : AbstractValidator<MRBomGroupVM>
    {
        public MRBomGroupVMValidator()
        {
            RuleFor(t => t.Name)
              .NotEmpty().WithMessage(MRBomGroupVMValidatorMessage.EmptyName);

            RuleFor(t => t.TenantId)
              .NotEmpty().WithMessage(MRBomGroupVMValidatorMessage.EmptyTenantId);
        }
    }
}
