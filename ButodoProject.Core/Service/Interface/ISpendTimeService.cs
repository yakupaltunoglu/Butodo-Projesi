using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ButodoProject.Core.Model.FixType;
using ButodoProject.Core.Service.Dto;
using ButodoProject.Model.Domain;

namespace ButodoProject.Core.Service.Interface
{
    public interface ISpendTimeService : IServiceBase
    {
        IList<SpendTimeDto> ListSpendTime();
        SpendTimeDto GetSpendTime(Guid id);
        void SaveOrUpdateSpendTime(SpendTimeDto data);
        void DeleteSpendTime(Guid id);

    }
}