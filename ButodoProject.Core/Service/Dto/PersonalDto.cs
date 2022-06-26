using ButodoProject.Core.Model.FixType;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ButodoProject.Core.Service.Dto
{
    public class PersonalDto : ResultModelDto
    {
        public Guid Id { get; set; }


        public Guid PersonalTypeId { get; set; }


        public Guid CompanyId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string CompanyName { get; set; }
        public string PersonalTypeName { get; set; }

        public CompanyDto CompanyDto { get; set; }
        public IList<CompanyDto> CompanyList { get; set; }


        public PersonalTypeDto PersonalTypeDto { get; set; }
        public IList<PersonalTypeDto> PersonalTypeList { get; set; }
        public IList<PersonalDto> PersonalList { get; set; }

        public string Email { get; set; }


       
        public string Username { get; set; }


         

        public string Password { get; set; }

    }
}
