using ButodoProject.Core.Helper;
using ButodoProject.Core.Service;
using ButodoProject.Core.Service.Dto;
using ButodoProject.Core.Service.Interface;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ButodoProject.Web.Controllers
{
  
    public class AccountController : Controller
    {
        private readonly IHomeService _homeService;

        //Sample Users Data, it can be fetched with the use of any ORM
        public AccountController(NHibernate.ISession sessions)
        {
            _homeService = new HomeService(sessions);
      
        }
        public IActionResult Login(string ReturnUrl = "/")
        {
            LoginDto objLoginModel = new LoginDto();
            objLoginModel.ReturnUrl = ReturnUrl;
            return View(objLoginModel);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            if (ModelState.IsValid)
            {
                var hashPassword = CryptoHelper.EncryptByMd5(loginDto.Password);
                var user = _homeService.GetPersonal(loginDto.Username, hashPassword);

                if (user == null)
                {
                    //Add logic here to display some message to user
                    ViewBag.Message = "Invalid Credential";
                    return View(loginDto);
                }
                else
                {

                    //A claim is a statement about a subject by an issuer and
                    //represent attributes of the subject that are useful in the context of authentication and authorization operations.

                    var claims = new List<Claim>() {
                            new Claim(ClaimTypes.NameIdentifier,Convert.ToString(user.Id)),
                            new Claim(ClaimTypes.Name,Convert.ToString(user.Username)),
                            //new Claim(ClaimTypes.Role,Convert.ToString(user.PersonalType)),
                            };

                    //Initialize a new instance of the ClaimsIdentity with the claims and authentication scheme
                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    //Initialize a new instance of the ClaimsPrincipal with ClaimsIdentity
                    var principal = new ClaimsPrincipal(identity);
                    //SignInAsync is a Extension method for Sign in a principal for the specified scheme.
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                        principal, new AuthenticationProperties() { IsPersistent = loginDto.RememberMe });

                    return LocalRedirect(loginDto.ReturnUrl);
                }
            }
            return View(loginDto);
        }

        public async Task<IActionResult> LogOut()
        {
            //SignOutAsync is Extension method for SignOut
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            //Redirect to home page
            return LocalRedirect("/account/login");
        }


        public IActionResult ForgotPassword()
        {

            return View();

        }


        [HttpPost]
        public async Task<IActionResult> Send([Bind("ToEmail,Subject,Body")] MailRequestDto request)
        {
            try
            {
                request.Subject = "merhaba";
                request.Body = "dsfsdf";

                await EmailHelper.SendEmailAsync(request);
                return Ok();
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

    }
}
