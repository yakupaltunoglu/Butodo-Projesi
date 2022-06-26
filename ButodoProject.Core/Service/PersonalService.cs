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
    public class PersonalService : ServiceBase, IPersonalService
    {

       

        public PersonalService(ISession session) : base(session)
        {
        }
        #region Crud
        public IList<PersonalTypeDto> ListPersonalType()
        {
            PersonalTypeDto personalTypeDto = null;
            var result = CurrentSession.QueryOver<PersonalType>()
                .Where(x => x.IsDeleted == false)
                .SelectList(u => u
                    .Select(x => x.Name).WithAlias(() => personalTypeDto.Name)
                    .Select(x => x.Id).WithAlias(() => personalTypeDto.Id)
                )
                .TransformUsing(Transformers.AliasToBean<PersonalTypeDto>())
                .List<PersonalTypeDto>();
            return result;
        }

        public IList<PersonalDto> ListPersonal()
        {
            PersonalDto personalDto = null;
            Company jCompany = null;
            PersonalType jPersonalType = null;
            var personalList = CurrentSession.QueryOver<Personal>()
                .JoinAlias(x => x.Company, () => jCompany)
                .JoinAlias(x => x.PersonalType, () => jPersonalType)
                .Where(x => x.IsDeleted == false)
                .SelectList(u => u
                    .Select(x => jCompany.Name).WithAlias(() => personalDto.CompanyName)
                    .Select(x => jPersonalType.Name).WithAlias(() => personalDto.PersonalTypeName)
                    .Select(x => x.Name).WithAlias(() => personalDto.Name)
                    .Select(x => x.Surname).WithAlias(() => personalDto.Surname)
                    .Select(x => x.Email).WithAlias(() => personalDto.Email)
                    .Select(x => x.Username).WithAlias(() => personalDto.Username)
                    .Select(x => x.Id).WithAlias(() => personalDto.Id)
                )
                .TransformUsing(Transformers.AliasToBean<PersonalDto>())
                .List<PersonalDto>();

            return personalList;
        }
        public void SaveOrUpdatePersonal(PersonalDto data)
        {
            using (var tran = CurrentSession.BeginTransaction())
            {
                var node = CurrentSession.QueryOver<Personal>()
                    .Where(x => x.Id == data.Id)
                    .SingleOrDefault();

                if (node == null)
                {
                    node = new Personal
                    {
                        Name = data.Name,
                        Surname = data.Surname,
                        PersonalType = CurrentSession.Load<PersonalType>(data.PersonalTypeId),
                        Company = CurrentSession.Load<Company>(data.CompanyId),
                        Password = CryptoHelper.EncryptByMd5(data.Password),
                        Username = data.Username,
                        Email=data.Email
                    };

                    CurrentSession.Save(node);
                }
                else
                {
                    node.Name = data.Name;
                    node.Surname = data.Surname;
                    node.PersonalType = CurrentSession.Load<PersonalType>(data.PersonalTypeId); 
                    node.Company = CurrentSession.Load<Company>(data.CompanyId);
                    if(data.Password != null) 
                        node.Password =  CryptoHelper.EncryptByMd5(data.Password);
                    node.Username = data.Username;
                    node.Email = data.Email;
                    node.LastUpdatedAt = DateTime.Now;
                    CurrentSession.Update(node);
                }


                tran.Commit();
            }
        }
     

        public PersonalDto GetPersonal(Guid id)
        {
           var personal =CurrentSession.QueryOver<Personal>()
                                           .Where(x => x.IsDeleted == false)
                                           .Where(x => x.Id == id).SingleOrDefault();

            return personal == null ? new PersonalDto() : new PersonalDto
            {
                Id = id,
                Name = personal.Name,
                Surname = personal.Surname,
                PersonalTypeId = personal.PersonalType.Id,
                CompanyId = personal.Company.Id,
                Email = personal.Email,
                Username = personal.Username,
            };


        }

        public void DeletePersonal(Guid id)
        {
            var node = CurrentSession.QueryOver<Personal>().Where(x => x.Id == id).SingleOrDefault();
            node.IsDeleted = true;
            node.DeletedAt = DateTime.Now;
            CurrentSession.Update(node);
            CurrentSession.Flush();
        }
        #endregion
    }
}