using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ButodoProject.Core.Model.FixType;
using ButodoProject.Core.Service.Dto;
using ButodoProject.Model.Domain;

namespace ButodoProject.Core.Service.Interface
{
    public interface IPersonalRoleService : IServiceBase
    {
        IList<PersonalRoleDto> ListPersonalRole();
        //PersonalRoleDto GetPersonalRole(Guid id);

        PersonalRoleDto GetPersonalRole(Guid id);
        void SaveOrUpdatePersonalRole(PersonalRoleDto data);
        void DeletePersonalRole(Guid id);

    }
}