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
using ButodoProject.Web.Models;
using ButodoProject.Core.Validators;

namespace ButodoProject.Web.Controllers
{
  
    public class HomeController : Controller
    {
        private readonly IHomeService _homeService;
        
        public HomeController( NHibernate.ISession sessions)
        {
            _homeService = new HomeService(sessions);
        }
        
        private string GetCurrentUserId()
        {
            return User.Claims.Where(x => x.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").SingleOrDefault().Value;
        }
        private string GetCurrentUsername()
        {
            return User.Claims.Where(x => x.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").SingleOrDefault().Value;
        }


        [Authorize]
        public IActionResult Index()
        {
            var result = _homeService.GetListProjectCategory();
            return View(result);
        }






     


    }
}
