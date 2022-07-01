using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ButodoProject.Core.Service.Dto
{
    public class CompanyDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
    }

}