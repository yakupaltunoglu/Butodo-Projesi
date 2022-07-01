using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using ButodoProject.Core.Service.Dto;
using ButodoProject.Core.Service.Interface;
using ButodoProject.Core.Service;
using ButodoProject.Core.Model.FixType;
using ButodoProject.Core.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using FluentValidation;
using FluentValidation.Results;
using FluentValidation.AspNetCore;

namespace ButodoProject.Web.Controllers
{
    //[Authorize]
    public class ProjectController : Controller
    {
        private readonly IProjectService _projectService;
        private IValidator<ProjectDto> _validator;
        private readonly ICompanyService _companyService;
        public ProjectController(IValidator<ProjectDto> validator, NHibernate.ISession sessions)
        {
            _projectService = new ProjectService(sessions);
            _companyService = new CompanyService(sessions);
            _validator = validator;
        }
        #region Crud
        public IActionResult Index()
        {
            var result = _projectService.ListProject();
            return View(result);
        }
        public IActionResult AddorEdit(string id)
        {
            Guid projectId;
            Guid.TryParse(id, out projectId);
            var result = _projectService.GetProject(projectId);
            GetSelectListItems(result);
            return View(result);
        }
        private void GetSelectListItems(ProjectDto projectDto)
        {
            var companyList = _companyService.ListCompany();
            projectDto.CompanyList = companyList;
        }

        [HttpPost]
        public async Task<IActionResult> AddorEdit([Bind("Id,Name,Leftx,Rightx,Depth,CompanyId")] ProjectDto projectDto)
        {
            ValidationResult result = await _validator.ValidateAsync(projectDto);

            if (result.IsValid)
            {
                _projectService.SaveOrUpdateProject(projectDto);
                return RedirectToAction(nameof(Index));
            }
            result.AddToModelState(this.ModelState, "");
            ViewBag.Exception = result.Errors;
            GetSelectListItems(projectDto);
            return View(projectDto);
        }
      
        public IActionResult Delete(Guid id)
        {
            _projectService.DeleteProject(id);
            return RedirectToAction(nameof(Index));
        }   




        #endregion
    }
}
