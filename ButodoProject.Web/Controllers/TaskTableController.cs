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
    public class TaskTableController : Controller
    {
        private readonly ITaskTableService _taskTableService;
        private readonly IPersonalService _personalService;
        private readonly IProjectService _projectService;
        public TaskTableController(NHibernate.ISession sessions)
        {
            _taskTableService = new TaskTableService(sessions);
            _personalService = new PersonalService(sessions);
            _projectService = new ProjectService(sessions);

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
            result.PersonalList = _personalService.ListPersonal();
            result.ProjectList = _projectService.ListProject();
            return View(result);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddorEdit([Bind("Id,Name,EndDate,PersonalId,ProjectId")] TaskTableDto taskTableDto)
        {
            if (ModelState.IsValid)
            {
                _taskTableService.SaveOrUpdateTaskTable(taskTableDto);
                return RedirectToAction(nameof(Index));
            }
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
