using ButodoProject.Core.Service.Dto;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace ButodoProject.Core.Validators.Personals
{
    public class ProjectValidator : AbstractValidator<ProjectDto>
    {
        public ProjectValidator()
        {
            RuleFor(p => p.Name).NotNull().WithMessage("Lütfen proje adı alanını boş geçmeyiniz.");
            RuleFor(p => p.Leftx).NotNull().GreaterThan(-1).WithMessage("Lütfen leftx alanını boş geçmeyiniz.");
            RuleFor(p => p.Rightx).NotNull().GreaterThan(-1).WithMessage("Lütfen rightx alanını boş geçmeyiniz.");
            RuleFor(p => p.Depth).NotNull().GreaterThan(-1).WithMessage("Lütfen depth alanını boş geçmeyiniz.");
            RuleFor(p => p.CompanyId).NotEmpty().WithMessage("Lütfen şirket seçiniz.");
        }
        
    }
}
