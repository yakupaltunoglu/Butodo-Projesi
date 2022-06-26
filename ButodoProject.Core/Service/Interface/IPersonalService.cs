using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ButodoProject.Core.Model.FixType;
using ButodoProject.Core.Service.Dto;
using ButodoProject.Model.Domain;

namespace ButodoProject.Core.Service.Interface
{
    public interface IPersonalService : IServiceBase
    {
        IList<PersonalDto> ListPersonal();
        void SaveOrUpdatePersonal(PersonalDto data);
        void DeletePersonal(Guid id);

        PersonalDto GetPersonal(Guid id);
        IList<PersonalTypeDto> ListPersonalType();
    }
}