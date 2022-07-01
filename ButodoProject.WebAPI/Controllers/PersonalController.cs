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
    public class PersonalController : Controller
    {
        private readonly IPersonalService _personalService;
        private IValidator<PersonalDto> _validator;
        public PersonalController(IValidator<PersonalDto> validator, NHibernate.ISession sessions)
        {
            _personalService = new PersonalService(sessions);
            _validator = validator;
        }

        [HttpGet("getlist")]
        public IList<PersonalDto> GetList()
        {
            return _personalService.ListPersonal();
        }

        [HttpGet("get")]
        public PersonalDto Get(string id)
        {
            Guid personalId;
            Guid.TryParse(id, out personalId);
            return _personalService.GetPersonal(personalId);
        }


        [HttpPost("delete")]
        public void Delete(Guid id)
        {
            _personalService.DeletePersonal(id); ;
        }


        [HttpPost("addoredit")]
        public async Task<PersonalDto> AddorEdit([FromBody] PersonalDto personalDto)
        {
            ValidationResult result = await _validator.ValidateAsync(personalDto);

            if (result.IsValid)
            {
                _personalService.SaveOrUpdatePersonal(personalDto);
                //return RedirectToAction(nameof(Index));
            }

            result.AddToModelState(this.ModelState, "");
            //companyDto.Exception = result.Errors;

            return personalDto;
        }
    }
}
