using CWB.CompanySettings.ViewModels.DocType;
using CWB.CompanySettings.ViewModelValidatorsMessage.DocType;
using FluentValidation;

namespace CWB.CompanySettings.ViewModelValidators.DocType
{
    public class DocumentTypeVMValidator : AbstractValidator<DocumentTypeVM>
    {
        public DocumentTypeVMValidator()
        {

            RuleFor(v => v.Name)
                   .NotEmpty().WithMessage(DocumentTypeVMValidatorMessage.EmptyDocumentType);

            RuleFor(v => v.TenantId)
                  .NotEmpty().WithMessage(DocumentTypeVMValidatorMessage.EmptyTenantId);
        }

    }
}