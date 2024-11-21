using CWB.Simulation.ViewModels;
using CWB.Simulation.ViewModelValidatorsMessage;
using FluentValidation;

namespace CWB.Simulation.ViewModelValidators
{
    public class VendorVMValidator : AbstractValidator<VendorVM>
    {
        public VendorVMValidator()
        {
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage(VendorVMValidatorMessage.EmptyName);

            RuleFor(c => c.TenantId)
                .NotEmpty().WithMessage(VendorVMValidatorMessage.EmptyTenantId);

        }
    }
}
