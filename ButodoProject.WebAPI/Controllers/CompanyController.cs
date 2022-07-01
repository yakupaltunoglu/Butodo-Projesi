using ButodoProject.Core.Service;
using ButodoProject.Core.Service.Dto;
using ButodoProject.Core.Service.Interface;
using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace ButodoProject.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _companyService;
        private IValidator<CompanyDto> _validator;
        public CompanyController(IValidator<CompanyDto> validator, NHibernate.ISession sessions)
        {
            _companyService = new CompanyService(sessions);
            _validator = validator;
        }

        [HttpGet("getlist")]
        public IList<CompanyDto> GetList()
        {
            return _companyService.ListCompany();
        }

        [HttpGet("get")]
        public CompanyDto Get(string id)
        {
            Guid companyId;
            Guid.TryParse(id, out companyId);
            return _companyService.GetCompany(companyId);
        }


        [HttpPost("delete")]
        public void Delete(Guid id)
        {
           _companyService.DeleteCompany(id); ;
        }


        [HttpPost("addoredit")]
        public async Task<CompanyDto> AddorEdit([FromBody] CompanyDto companyDto)
        {
            ValidationResult result = await _validator.ValidateAsync(companyDto);

            if (result.IsValid)
            {
                _companyService.SaveOrUpdateCompany(companyDto);
                //return RedirectToAction(nameof(Index));
            }

            result.AddToModelState(this.ModelState, "");
            //companyDto.Exception = result.Errors;

            return companyDto;
        }

    }
}