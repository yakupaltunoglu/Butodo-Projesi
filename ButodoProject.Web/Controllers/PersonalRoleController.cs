using ButodoProject.Core.Helper;
using ButodoProject.Core.Model.FixType;
using ButodoProject.Core.Service;
using ButodoProject.Core.Service.Dto;
using ButodoProject.Core.Service.Interface;
using ButodoProject.Web.Models;
using ButodoProject.Core.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ButodoProject.Web.Controllers
{

    public class PersonalRoleController : Controller
    {
        private IValidator<PersonalRoleDto> _validator;

        private readonly IPersonalRoleService _personalRoleService;
        private readonly IPersonalService _personalService;

        public PersonalRoleController(ISession sessions, IValidator<PersonalRoleDto> validator)
        {
            _personalRoleService = new PersonalRoleService(sessions);
            _personalService = new PersonalService(sessions);
            _validator = validator;
        }

        #region Crud
        public IActionResult Index()
        {
            var result = _personalRoleService.ListPersonalRole();
            return View(result);
        }


        public IActionResult AddorEdit(string id)
        {
            Guid personalId;
            Guid.TryParse(id, out personalId);
            var personalDto = _personalRoleService.GetPersonalRole(personalId);
            GetSelectListItems(personalDto);
            return View(personalDto);
        }
        private void GetSelectListItems(PersonalRoleDto result)
        {
            result.PersonalListt = _personalService.ListPersonal();
        }


        [HttpPost]
        public async Task<IActionResult> AddorEdit([Bind("Id,PersonalId,CompanyList,CompanyAddorEdit,PersonalList,PersonalAddorEdit,PersonalProjectList,PersonalProjectAddorEdit,ProjectList,ProjectAddorEdit,SpendTimeList,SpendTimeAddorEdit,TaskMessageList,TaskMessageAddorEdit,TaskTableList,TaskTableAddorEdit")] PersonalRoleDto personalRoleDto)
        {
            ValidationResult result = await _validator.ValidateAsync(personalRoleDto);

            if (result.IsValid)
            {
                _personalRoleService.SaveOrUpdatePersonalRole(personalRoleDto);
                return RedirectToAction(nameof(Index));
            }

            result.AddToModelState(this.ModelState, "");
            ViewBag.Exception = result.Errors;
            GetSelectListItems(personalRoleDto);
            return View(personalRoleDto);

        }

        public IActionResult Delete(Guid id)
        {
            _personalRoleService.DeletePersonalRole(id);
            return RedirectToAction(nameof(Index));
        }


        #endregion

    }
}
