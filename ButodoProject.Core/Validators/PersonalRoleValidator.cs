using ButodoProject.Core.Service.Dto;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace ButodoProject.Core.Validators.Personals
{
    public class PersonalRoleValidator : AbstractValidator<PersonalRoleDto>
    {
        public PersonalRoleValidator()
        {
            RuleFor(p => p.PersonalId).NotEmpty().WithMessage("Lütfen personel seçiniz.");
        }
        
    }
}
