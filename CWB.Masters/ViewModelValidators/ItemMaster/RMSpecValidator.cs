using CWB.Masters.ViewModels.ItemMaster;
using CWB.Masters.ViewModelValidatorsMessage.Company;
using CWB.Masters.ViewModelValidatorsMessage.ItemMaster;
using FluentValidation;

namespace CWB.Masters.ViewModelValidators.ItemMaster
{
    public class RMSpecValidator : AbstractValidator<RawMaterialSepcVM>
    {
        public RMSpecValidator()
        {
            RuleFor(v => v.Name)
                   .NotEmpty().WithMessage(NameValidatorMessage.EmptyName);
        }

    }
}
