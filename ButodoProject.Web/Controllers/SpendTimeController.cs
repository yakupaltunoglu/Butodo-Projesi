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
    public class SpendTimeController : Controller
    {
        private readonly ITaskTableService _taskTableService;
        private readonly IPersonalService _personalService;
        private readonly ISpendTimeService _spendTimeService;
        private IValidator<SpendTimeDto> _validator;
        public SpendTimeController(NHibernate.ISession sessions, IValidator<SpendTimeDto> validator)
        {
            _taskTableService = new TaskTableService(sessions);
            _personalService = new PersonalService(sessions);
            _spendTimeService = new SpendTimeService(sessions);
            _validator = validator;

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
            GetSelectListItems(result);
            return View(result);
        }

        private void GetSelectListItems(SpendTimeDto result)
        {
            result.PersonalList = _personalService.ListPersonal();
            result.TaskTableList = _taskTableService.ListTaskTable();
        }

        [HttpPost]
        public async Task<IActionResult> AddorEdit([Bind("Id,PersonalId,TaskTableId,Minute")] SpendTimeDto spendTimeDto)
        {
            ValidationResult result = await _validator.ValidateAsync(spendTimeDto);

            if (result.IsValid)
            {
                _spendTimeService.SaveOrUpdateSpendTime(spendTimeDto);
                return RedirectToAction(nameof(Index));
            }

            result.AddToModelState(this.ModelState, "");
            ViewBag.Exception = result.Errors;
            GetSelectListItems(spendTimeDto);
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
