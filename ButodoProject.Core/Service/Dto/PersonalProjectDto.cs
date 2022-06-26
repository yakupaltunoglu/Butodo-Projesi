using ButodoProject.Core.Model.FixType;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ButodoProject.Core.Service.Dto
{
    public class PersonalProjectDto
    {
        public Guid Id { get; set; }
        public ProjectDto ProjectDto { get; set; }
        public IList<ProjectDto> ProjectList { get; set; }
        public PersonalDto PersonalDto { get; set; }
        public IList<PersonalDto> PersonalList { get; set; }
        public DateTime CreatedAt { get; set; }
        public string PersonalName { get; set; }
        public string ProjectName { get; set; }

        public Guid PersonalId { get; set; }
        public Guid ProjectId { get; set; }

    }
}
