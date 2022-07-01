using System;

namespace ButodoProject.Core.Model.FixType
{
    public enum RolePageType
    {
        None,
        CompanyList,
        CompanyAddorEdit,
        PersonalList,
        PersonalAddorEdit,
        PersonalProjectList,
        PersonalProjectAddorEdit,
        ProjectList,
        ProjectAddorEdit,
        SpendTimeList,
        SpendTimeAddorEdit,
        TaskMessageList,
        TaskMessageAddorEdit,
        TaskTableList,
        TaskTableAddorEdit,
    }

    public enum RoleType
    {
        Blocked = 0,
        Viewer = 1,
        Owner = 2
    }

}