using ButodoProject.Core.Service.Dto;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace ButodoProject.Core.Validators.Personals
{
    public class PersonalProjectValidator : AbstractValidator<PersonalProjectDto>
    {
        public PersonalProjectValidator()
        {
            RuleFor(p => p.PersonalId).NotEmpty().WithMessage("Lütfen personel seçiniz.");
            RuleFor(p => p.ProjectId).NotEmpty().WithMessage("Lütfen proje seçiniz.");
        }
    }
}
