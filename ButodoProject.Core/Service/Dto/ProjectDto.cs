using ButodoProject.Core.Model.FixType;
using System;
using System.Collections.Generic;

namespace ButodoProject.Core.Service.Dto
{
    public class ProjectDto
    {
        public Guid Id { get; set; }

        public string ProjectName { get; set; }
        public string FullProjectName { get; set; }
        public Guid CompanyId { get; set; }
        public string CompanyName { get; set; }
        //public List<Itemlist> CompanyList { get; set; }
        public int Leftx { get; set; }
        public int Rightx { get; set; }
        public int Depth { get; set; }

        public List<TaskTableDto> TaskTableList { get; set; }
        public string Username { get; set; }

        //public List<Itemlist> PersonalList { get; set; }
        public DateTime CreatedAt { get; set; }

    }

}