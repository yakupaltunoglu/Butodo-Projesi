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
    public class TaskMessageController : Controller
    {
        private readonly ITaskTableService _taskTableService;
        private readonly ITaskMessageService _taskMessageService;
        private readonly IProjectService _projectService;
        private IValidator<TaskMessageDto> _validator;
        public TaskMessageController(NHibernate.ISession sessions, IValidator<TaskMessageDto> validator)
        {
            _taskTableService = new TaskTableService(sessions);
            _taskMessageService = new TaskMessageService(sessions);
            _validator = validator;

        }
        #region Crud
        public IActionResult Index()
        {
            var result = _taskMessageService.ListTaskMessage();
            return View(result);
        }
        public IActionResult AddorEdit(string id)
        {
            Guid personalProjectId;
            Guid.TryParse(id, out personalProjectId);
            var result = _taskMessageService.GetTaskMessage(personalProjectId);
            GetSelectListItems(result);
            return View(result);
        }

        private void GetSelectListItems(TaskMessageDto result)
        {
            result.TaskTableList = _taskTableService.ListTaskTable();
        }

        [HttpPost]
        public async Task<IActionResult> AddorEdit([Bind("Id,Name,TaskTableId")] TaskMessageDto taskMessageDto)
        {
            ValidationResult result = await _validator.ValidateAsync(taskMessageDto);

            if (result.IsValid)
            {
                _taskMessageService.SaveOrUpdateTaskMessage(taskMessageDto);
                return RedirectToAction(nameof(Index));
            }
            result.AddToModelState(this.ModelState, "");
            ViewBag.Exception = result.Errors;
            GetSelectListItems(taskMessageDto);
            return View(taskMessageDto);
        }
      
        public IActionResult Delete(Guid id)
        {
            _taskMessageService.DeleteTaskMessage(id);
            return RedirectToAction(nameof(Index));
        }   




        #endregion
    }
}
