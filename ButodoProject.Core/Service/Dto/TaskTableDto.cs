using ButodoProject.Core.Model.FixType;
using System;
using System.Collections.Generic;
using System.Text;

namespace ButodoProject.Core.Service.Dto
{
    public class TaskTableDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid ProjectId { get; set; }
        public Guid PersonalId { get; set; }
        public string ProjectName { get; set; }
        public string PersonalName { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public ProjectDto ProjectDto { get; set; }
        public IList<ProjectDto> ProjectList { get; set; }
        public PersonalDto PersonalDto { get; set; }
        public IList<PersonalDto> PersonalList { get; set; }


    }
}
