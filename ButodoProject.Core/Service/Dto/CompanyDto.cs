using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ButodoProject.Core.Service.Dto
{
    public class CompanyDto
    {
        public Guid Id { get; set; }


        [Required(ErrorMessage = "Bu alan gerekli")]
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        
    }

    public class CompanyDtoIndex
    {
        public CompanyDto CompanyDto { get; set; }

        public IEnumerable<CompanyDto> CompanyDtoList { get; set; }

    }

}