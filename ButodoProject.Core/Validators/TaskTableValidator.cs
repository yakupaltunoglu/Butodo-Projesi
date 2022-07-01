using ButodoProject.Core.Service.Dto;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace ButodoProject.Core.Validators.Personals
{
    public class TaskTableValidator : AbstractValidator<TaskTableDto>
    {
        public TaskTableValidator()
        {
            RuleFor(p => p.Name).NotNull().WithMessage("Lütfen görev adı alanını boş geçmeyiniz.");
            RuleFor(p => p.EndDate).Must(BeAValidDate).WithMessage("Lütfen termin tarihini boş geçmeyiniz.");
            RuleFor(p => p.PersonalId).NotEmpty().WithMessage("Lütfen personal seçiniz.");
            RuleFor(p => p.ProjectId).NotEmpty().WithMessage("Lütfen proje seçiniz.");
            
        }
        private bool BeAValidDate(DateTime date)
        {
            return !date.Equals(default(DateTime));
        }
    }
}
