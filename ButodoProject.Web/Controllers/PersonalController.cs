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
  
    public class PersonalController : Controller
    {
        private readonly IPersonalService _personalService;
        private readonly ICompanyService _companyService;
        private IValidator<PersonalDto> _validator;
        public PersonalController(IValidator<PersonalDto> validator,ISession sessions)
        {
            _personalService = new PersonalService(sessions);
            _companyService = new CompanyService(sessions);
            _validator = validator;
        }

        #region Crud
        public IActionResult Index()
        {
            //var roleType = _personalService.RoleControl(RolePageType.PersonalList);
            //if (roleType == RoleType.Blocked)
            //    return View("NotAuthorize");
            //ViewBag.RoleType = roleType;
            var result = _personalService.ListPersonal();
            return View(result);
        }


        public IActionResult AddorEdit(string id)
        {
            Guid personalId;
            Guid.TryParse(id, out personalId);
            var personalDto = _personalService.GetPersonal(personalId);
            GetSelectListItems(personalDto);

            return View(personalDto);
        }

        private void GetSelectListItems(PersonalDto personalDto)
        {
            var personalTypeList = _personalService.ListPersonalType();
            var companyList = _companyService.ListCompany();
            personalDto.PersonalTypeList = personalTypeList;
            personalDto.CompanyList = companyList;
        }

        [HttpPost]
        public async Task<IActionResult> AddorEdit([Bind("Id,Name,Surname,PersonalTypeId,CompanyId,Password,Email,Username")] PersonalDto personalDto)
        {
            ValidationResult result =await _validator.ValidateAsync(personalDto);

            if (result.IsValid)
            {
                _personalService.SaveOrUpdatePersonal(personalDto);
                return RedirectToAction(nameof(Index));
            }
            result.AddToModelState(this.ModelState,"");
            ViewBag.Exception = result.Errors;
            GetSelectListItems(personalDto);
            return View(personalDto);
        }

        public IActionResult Delete(Guid id)
        {
            _personalService.DeletePersonal(id);
            return RedirectToAction(nameof(Index));
        }

        //public IActionResult Role()
        //{
        //    var personal = User.Identity.Name;
        //    //var roleType = _personalService.RoleControl(RolePageType.PersonalList);
        //    //if (roleType == RoleType.Blocked)
        //    //    return View("NotAuthorize");
        //    //ViewBag.RoleType = roleType;
        //    return View();
        //}
        #endregion

    }
}
