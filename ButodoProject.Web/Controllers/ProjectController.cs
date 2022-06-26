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
    public class ProjectController : Controller
    {
        private readonly IProjectService _projectService;
        public ProjectController(NHibernate.ISession sessions)
        {
            _projectService = new ProjectService(sessions);

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
            return View(_projectService.GetProject(projectId));
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddorEdit([Bind("Id,Name")] ProjectDto projectDto)
        {
            if (ModelState.IsValid)
            {
                _projectService.SaveOrUpdateProject(projectDto);
                return RedirectToAction(nameof(Index));
            }
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
