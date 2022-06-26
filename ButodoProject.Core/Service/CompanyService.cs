using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using FluentNHibernate;
using Microsoft.Extensions.Options;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Transform;
using NHibernate.Util;
using ButodoProject.Core.Helper;
using ButodoProject.Core.Model.Domain;
using ButodoProject.Core.Model.FixType;
using ButodoProject.Core.Service;
using ButodoProject.Core.Service.Dto;
using ButodoProject.Core.Service.Interface;
using ButodoProject.Model.Domain;
using System.Threading.Tasks;

namespace ButodoProject.Core.Service
{
    public class CompanyService : ServiceBase, ICompanyService
    {
      

        public CompanyService(ISession session) : base(session)
        {
        }
        #region Crud
        public IList<CompanyDto> ListCompany()
        {
            CompanyDto companyDto = null;
            var companyList = CurrentSession.QueryOver<Company>()
                .Where(x => x.IsDeleted == false)
                .SelectList(u => u
                    .Select(x => x.Id).WithAlias(() => companyDto.Id)
                    .Select(x => x.Name).WithAlias(() => companyDto.Name)
                    .Select(x => x.CreatedAt).WithAlias(() => companyDto.CreatedAt)
                )
                .TransformUsing(Transformers.AliasToBean<CompanyDto>())
                .List<CompanyDto>().OrderByDescending(x => x.CreatedAt).ToList();

            return companyList;
        }
     
        public void SaveOrUpdateCompany(CompanyDto data)
        {
            using (var tran = CurrentSession.BeginTransaction())
            {
                var node = CurrentSession.QueryOver<Company>()
                    .Where(x => x.Id == data.Id)
                    .SingleOrDefault();

                if (node == null)
                {
                    node = new Company
                    {
                        Name = data.Name,
                    };

                    CurrentSession.Save(node);
                }
                else
                {
                    node.Name = data.Name;
                    node.LastUpdatedAt = DateTime.Now;
                    CurrentSession.Update(node);
                }



                tran.Commit();
            }
        }

        public CompanyDto GetCompany(Guid id)
        {
            var company = CurrentSession.QueryOver<Company>()
                           .Where(x => x.IsDeleted == false)
                           .Where(x => x.Id == id)
                           .SingleOrDefault();

            return company == null ? new CompanyDto() : new CompanyDto
            {
                Id = id,
                Name = company.Name,
            };
        }
        public void DeleteCompany(Guid id)
        {
            var node = CurrentSession.QueryOver<Company>().Where(x => x.Id == id).SingleOrDefault();
            node.IsDeleted = true;
            node.DeletedAt = DateTime.Now;
            CurrentSession.Update(node);
            CurrentSession.Flush();
        }

        #endregion
    }
}