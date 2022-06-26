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
    public class PersonalProjectController : Controller
    {
        private readonly IPersonalProjectService _personalProjectService;
        private readonly IPersonalService _personalService;
        private readonly IProjectService _projectService;
        public PersonalProjectController(NHibernate.ISession sessions)
        {
            _personalProjectService = new PersonalProjectService(sessions);
            _personalService = new PersonalService(sessions);
            _projectService = new ProjectService(sessions);

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
            result.PersonalList = _personalService.ListPersonal();
            result.ProjectList = _projectService.ListProject();
            return View(result);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddorEdit([Bind("Id,PersonalId,ProjectId")] PersonalProjectDto personalProjectDto)
        {
            if (ModelState.IsValid)
            {
                _personalProjectService.SaveOrUpdatePersonalProject(personalProjectDto);
                return RedirectToAction(nameof(Index));
            }
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
