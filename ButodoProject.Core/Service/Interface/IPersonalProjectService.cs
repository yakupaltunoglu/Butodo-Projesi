using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ButodoProject.Core.Model.FixType;
using ButodoProject.Core.Service.Dto;
using ButodoProject.Model.Domain;

namespace ButodoProject.Core.Service.Interface
{
    public interface IPersonalProjectService : IServiceBase
    {
        IList<PersonalProjectDto> ListPersonalProject();
        PersonalProjectDto GetPersonalProject(Guid id);
        void SaveOrUpdatePersonalProject(PersonalProjectDto data);
        void DeletePersonalProject(Guid id);

    }
}