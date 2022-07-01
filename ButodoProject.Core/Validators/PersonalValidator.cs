using ButodoProject.Core.Service.Dto;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace ButodoProject.Core.Validators.Personals
{
    public class PersonalValidator:AbstractValidator<PersonalDto>
    {
        public PersonalValidator()
        {
            RuleFor(p => p.Name).NotNull().WithMessage("Lütfen adı alanını boş geçmeyiniz.");
            RuleFor(p => p.Surname).NotNull().WithMessage("Lütfen soyadı alanını boş geçmeyiniz.");
            RuleFor(p => p.Email).NotNull().WithMessage("Lütfen email alanını boş geçmeyiniz.");
            RuleFor(p => p.Username).NotNull().WithMessage("Lütfen kullanıcı adı alanını boş geçmeyiniz.");
            //RuleFor(p => p.Password).NotNull().WithMessage("Lütfen parola alanını boş geçmeyiniz.");
            RuleFor(p => p.PersonalTypeId).NotEmpty().WithMessage("Lütfen personel tipi alanını boş geçmeyiniz.");
            RuleFor(p => p.CompanyId).NotEmpty().WithMessage("Lütfen şirket alanını boş geçmeyiniz.");
        }
    }
}
