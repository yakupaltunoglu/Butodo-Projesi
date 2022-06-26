using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ButodoProject.Core.Service.Dto
{
    public class PersonalTypeDto
    {
        public Guid Id { get; set; }


        [Required(ErrorMessage = "Bu alan gerekli")]
        public string Name { get; set; }
        
    }

   
    

}