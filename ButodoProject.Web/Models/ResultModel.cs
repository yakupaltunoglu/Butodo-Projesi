using ButodoProject.Core.Service.Dto;
using System;
using System.Collections.Generic;

namespace ButodoProject.Web.Models
{
    public class ResultModel
    {
        public List<Exception> Exception { get; set; }
        public PersonalDto PersonalDto { get; set; }
       
    }
}
