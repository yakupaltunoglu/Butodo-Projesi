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
    public class CompanyController : Controller
    {
        private readonly ICompanyService _companyService;
        public CompanyController(NHibernate.ISession sessions)
        {
            _companyService = new CompanyService(sessions);

        }
        #region Crud
        public IActionResult Index()
        {
            var result = _companyService.ListCompany();
            return View(result);
        }
        public IActionResult AddorEdit(string id)
        {
            Guid companyId;
            Guid.TryParse(id, out companyId);
            return View(_companyService.GetCompany(companyId));
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddorEdit([Bind("Id,Name")] CompanyDto companyDto)
        {
            if (ModelState.IsValid)
            {
                _companyService.SaveOrUpdateCompany(companyDto);
                return RedirectToAction(nameof(Index));
            }
            return View(companyDto);
        }
      
        public IActionResult Delete(Guid id)
        {
            _companyService.DeleteCompany(id);
            return RedirectToAction(nameof(Index));
        }
        #endregion
    }
}
