using ButodoProject.Core.Service.Dto;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace ButodoProject.Core.Validators.Personals
{
    public class SpendTimeValidator : AbstractValidator<SpendTimeDto>
    {
        public SpendTimeValidator()
        {
            RuleFor(p => p.Minute).NotNull().GreaterThan(-1).WithMessage("Lütfen harcanan dakika alanını boş geçmeyiniz.");
           
            RuleFor(p => p.PersonalId).NotEmpty().WithMessage("Lütfen personal seçiniz.");
            RuleFor(p => p.TaskTableId).NotEmpty().WithMessage("Lütfen görev seçiniz.");
            
        }
       
    }
}
