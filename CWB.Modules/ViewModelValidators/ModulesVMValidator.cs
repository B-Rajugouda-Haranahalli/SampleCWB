using CWB.Modules.ViewModels;
using CWB.Modules.ViewModelValidatorsMessage;
using FluentValidation;

namespace CWB.Modules.ViewModelValidators
{
    public class ModulesVMValidator : AbstractValidator<ModulesVM>
    {
        public ModulesVMValidator()
        {
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage(ModuleVMValidatorMessage.EmptyModuleName) 
                .MaximumLength(250).WithMessage(ModuleVMValidatorMessage.MaxLengthModuleName);

            RuleFor(c => c.ModuleKey)
                .NotEmpty().WithMessage(ModuleVMValidatorMessage.EmptyModuleKey)
                .MaximumLength(50).WithMessage(ModuleVMValidatorMessage.MaxLengthModuleKey);
        }
    }
}
