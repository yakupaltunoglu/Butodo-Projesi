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

namespace ButodoProject.Web.Controllers
{
    //[Authorize]
    public class TaskMessageController : Controller
    {
        private readonly ITaskTableService _taskTableService;
        private readonly ITaskMessageService _taskMessageService;
        private readonly IProjectService _projectService;
        public TaskMessageController(NHibernate.ISession sessions)
        {
            _taskTableService = new TaskTableService(sessions);
            _taskMessageService = new TaskMessageService(sessions);

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
            result.TaskTableList = _taskTableService.ListTaskTable();
            return View(result);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddorEdit([Bind("Id,Name,TaskTableId")] TaskMessageDto taskMessageDto)
        {
            if (ModelState.IsValid)
            {
                _taskMessageService.SaveOrUpdateTaskMessage(taskMessageDto);
                return RedirectToAction(nameof(Index));
            }
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
