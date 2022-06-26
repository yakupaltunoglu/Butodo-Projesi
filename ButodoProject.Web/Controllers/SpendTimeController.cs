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
    public class SpendTimeController : Controller
    {
        private readonly ITaskTableService _taskTableService;
        private readonly IPersonalService _personalService;
        private readonly ISpendTimeService _spendTimeService;
        public SpendTimeController(NHibernate.ISession sessions)
        {
            _taskTableService = new TaskTableService(sessions);
            _personalService = new PersonalService(sessions);
            _spendTimeService = new SpendTimeService(sessions);

        }
        #region Crud
        public IActionResult Index()
        {
            var result = _spendTimeService.ListSpendTime();
            return View(result);
        }
        public IActionResult AddorEdit(string id)
        {
            Guid spendTimeId;
            Guid.TryParse(id, out spendTimeId);
            var result = _spendTimeService.GetSpendTime(spendTimeId);
            result.PersonalList = _personalService.ListPersonal();
            result.TaskTableList = _taskTableService.ListTaskTable();
            return View(result);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddorEdit([Bind("Id,PersonalId,TaskTableId")] SpendTimeDto spendTimeDto,double Hour)
        {
            if (ModelState.IsValid)
            {
                spendTimeDto.Hour = Hour;
                _spendTimeService.SaveOrUpdateSpendTime(spendTimeDto);
                return RedirectToAction(nameof(Index));
            }
            return View(spendTimeDto);
        }
      
        public IActionResult Delete(Guid id)
        {
            _spendTimeService.DeleteSpendTime(id);
            return RedirectToAction(nameof(Index));
        }   




        #endregion
    }
}
