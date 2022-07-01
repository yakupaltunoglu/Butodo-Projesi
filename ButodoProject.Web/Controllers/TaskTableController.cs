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
using FluentValidation.Results;
using FluentValidation;
using FluentValidation.AspNetCore;

namespace ButodoProject.Web.Controllers
{
    //[Authorize]
    public class TaskTableController : Controller
    {
        private readonly ITaskTableService _taskTableService;
        private readonly IPersonalService _personalService;
        private readonly IProjectService _projectService;
        private IValidator<TaskTableDto> _validator;

        public TaskTableController(NHibernate.ISession sessions, IValidator<TaskTableDto> validator)
        {
            _taskTableService = new TaskTableService(sessions);
            _personalService = new PersonalService(sessions);
            _projectService = new ProjectService(sessions);
            _validator = validator;

        }
        #region Crud
        public IActionResult Index()
        {
            var result = _taskTableService.ListTaskTable();
            return View(result);
        }
        public IActionResult AddorEdit(string id)
        {
            Guid personalProjectId;
            Guid.TryParse(id, out personalProjectId);
            var result = _taskTableService.GetTaskTable(personalProjectId);
            GetSelectListItems(result);
            return View(result);
        }

        private void GetSelectListItems(TaskTableDto result)
        {
            result.PersonalList = _personalService.ListPersonal();
            result.ProjectList = _projectService.ListProject();
        }

        [HttpPost]
        public async Task<IActionResult> AddorEdit([Bind("Id,Name,EndDate,PersonalId,ProjectId")] TaskTableDto taskTableDto)
        {
            ValidationResult result = await _validator.ValidateAsync(taskTableDto);

            if (result.IsValid)
            {
                _taskTableService.SaveOrUpdateTaskTable(taskTableDto);
                return RedirectToAction(nameof(Index));
            }
            result.AddToModelState(this.ModelState,"");



            ViewBag.Exception = result.Errors;



            GetSelectListItems(taskTableDto);
            return View(taskTableDto);
        }
      
        public IActionResult Delete(Guid id)
        {
            _taskTableService.DeleteTaskTable(id);
            return RedirectToAction(nameof(Index));
        }   




        #endregion
    }
}
