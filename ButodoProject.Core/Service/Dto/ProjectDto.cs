using ButodoProject.Core.Model.FixType;
using System;
using System.Collections.Generic;

namespace ButodoProject.Core.Service.Dto
{
    public class ProjectDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
        public string FullName { get; set; }
 
        public int Leftx { get; set; }
        public int Rightx { get; set; }
        public int Depth { get; set; }

        public string Username { get; set; }
        public Guid CompanyId { get; set; }
        public string CompanyName { get; set; }
        public IList<CompanyDto> CompanyList { get; set; }
        public IList<TaskTableDto> TaskTableList { get; set; }

        public DateTime CreatedAt { get; set; }

    }

}