using ButodoProject.Core.Service.Dto;
using FluentValidation;

namespace ButodoProject.Core.Validators
{
    public class CompanyValidator : AbstractValidator<CompanyDto>
    {

        public CompanyValidator()
        {
            RuleFor(p => p.Name).NotNull().WithMessage("Lütfen adı alanını boş geçmeyiniz.");
        }

    }
}
