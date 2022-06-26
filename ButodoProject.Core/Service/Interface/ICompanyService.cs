using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ButodoProject.Core.Model.FixType;
using ButodoProject.Core.Service.Dto;
using ButodoProject.Model.Domain;

namespace ButodoProject.Core.Service.Interface
{
    public interface ICompanyService : IServiceBase
    {
        IList<CompanyDto> ListCompany();
        CompanyDto GetCompany(Guid id);
        void SaveOrUpdateCompany(CompanyDto data);
        void DeleteCompany(Guid id);

    }
}