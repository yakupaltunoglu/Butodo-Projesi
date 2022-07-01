using ButodoProject.Core.Service.Dto;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace ButodoProject.Core.Validators.Personals
{
    public class TaskMessageValidator : AbstractValidator<TaskMessageDto>
    {
        public TaskMessageValidator()
        {
            RuleFor(p => p.Name).NotNull().WithMessage("Lütfen mesaj alanını boş geçmeyiniz.");
            RuleFor(p => p.TaskTableId).NotEmpty().WithMessage("Lütfen görev seçiniz.");
        }
       
    }
}
