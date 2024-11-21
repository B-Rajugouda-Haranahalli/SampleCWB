using CWB.Masters.ViewModels.OperationList;
using CWB.Masters.ViewModelValidatorsMessage.OperationList;
using FluentValidation;

namespace CWB.Masters.ViewModelValidators.OperationList
{
    public class OperationalDocumentListVMValidator : AbstractValidator<OperationalDocumentListVM>
    {
        public OperationalDocumentListVMValidator()
        {
            RuleFor(v => v.DocumentTypeId)
                   .NotEmpty().WithMessage(OperationalDocumentListVMValidatorMessage.EmptyDocumentTypeId);
            RuleFor(v => v.OperationListId)
                   .NotEmpty().WithMessage(OperationalDocumentListVMValidatorMessage.EmptyOperationListId);
            RuleFor(v => v.TenantId)
                  .NotEmpty().WithMessage(OperationalDocumentListVMValidatorMessage.EmptyTenantId);
        }
    }
}