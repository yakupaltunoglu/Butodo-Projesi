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
    public class PersonalProjectController : Controller
    {
        private IValidator<PersonalProjectDto> _validator;
        private readonly IPersonalProjectService _personalProjectService;
        private readonly IPersonalService _personalService;
        private readonly IProjectService _projectService;
        public PersonalProjectController(NHibernate.ISession sessions, IValidator<PersonalProjectDto> validator)
        {
            _personalProjectService = new PersonalProjectService(sessions);
            _personalService = new PersonalService(sessions);
            _projectService = new ProjectService(sessions);
            _validator = validator;

        }
        #region Crud
        public IActionResult Index()
        {
            var result = _personalProjectService.ListPersonalProject();
            return View(result);
        }
        public IActionResult AddorEdit(string id)
        {

            Guid personalProjectId;
            Guid.TryParse(id, out personalProjectId);
            var result = _personalProjectService.GetPersonalProject(personalProjectId);
            GetSelectListItems(result);
            return View(result);
        }

        private void GetSelectListItems(PersonalProjectDto result)
        {
            result.PersonalList = _personalService.ListPersonal();
            result.ProjectList = _projectService.ListProject();
        }

        [HttpPost]
        public async Task<IActionResult> AddorEdit([Bind("Id,PersonalId,ProjectId")] PersonalProjectDto personalProjectDto)
        {
            ValidationResult result = await _validator.ValidateAsync(personalProjectDto);

            if (result.IsValid)
            {
                _personalProjectService.SaveOrUpdatePersonalProject(personalProjectDto);
                return RedirectToAction(nameof(Index));
            }

            result.AddToModelState(this.ModelState,"");
            ViewBag.Exception = result.Errors;
            GetSelectListItems(personalProjectDto);
            return View(personalProjectDto);
        }
      
        public IActionResult Delete(Guid id)
        {
            _personalProjectService.DeletePersonalProject(id);
            return RedirectToAction(nameof(Index));
        }   




        #endregion
    }
}
