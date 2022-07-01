using ButodoProject.Core.Model.FixType;
using System;
using System.Collections.Generic;
using System.Text;

namespace ButodoProject.Core.Service.Dto
{
    public class PersonalRoleDto
    {
        public Guid Id { get; set; }

        public IList<PersonalDto> PersonalListt { get; set; }
        public Guid PersonalId { get; set; }
        public string PersonalName { get; set; }
  
        public IList<RoleDto> RoleTypes { get; set; }

        public string CompanyList { get; set; }
        public string CompanyAddorEdit { get; set; }
        public string PersonalList { get; set; }
        public string PersonalAddorEdit { get; set; }
        public string PersonalProjectList { get; set; }
        public string PersonalProjectAddorEdit { get; set; }
        public string ProjectList { get; set; }
        public string ProjectAddorEdit { get; set; }
        public string SpendTimeList { get; set; }
        public string SpendTimeAddorEdit { get; set; }
        public string TaskMessageList { get; set; }
        public string TaskMessageAddorEdit { get; set; }
        public string TaskTableList { get; set; }
        public string TaskTableAddorEdit { get; set; }

    }

    public class RoleDto
    {
        public Guid PersonalId { get; set; }
        public string RolePageType { get; set; }
        public string RoleType { get; set; }
    }
}
