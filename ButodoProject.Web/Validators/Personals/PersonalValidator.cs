using ButodoProject.Core.Service.Dto;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace ButodoProject.Web.Validators.Personals
{
    public class PersonalValidator:AbstractValidator<PersonalDto>
    {
        public PersonalValidator()
        {
            RuleFor(p => p.Name).NotNull().WithMessage("Lütfen personal adını boş geçmeyiniz.");
            RuleFor(p => p.Surname).NotNull().WithMessage("Lütfen personal soyadını boş geçmeyiniz.");
        }
    }
}
