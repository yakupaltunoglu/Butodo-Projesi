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
    public class PersonalProjectService : ServiceBase, IPersonalProjectService
    {
        public PersonalProjectService(ISession session) : base(session)
        {
        }
        #region Crud
        public IList<PersonalProjectDto> ListPersonalProject()
        {
            Personal jPersonal = null;
            Project jProject = null;

            PersonalProjectDto personalProjectDto = null;
            var personalProjectDtos = CurrentSession.QueryOver<PersonalProject>()
                .JoinAlias(x => x.Personal, () => jPersonal)
                .JoinAlias(x => x.Project, () => jProject)
                .Where(x => x.IsDeleted == false)
                .SelectList(u => u
                    .Select(x => jPersonal.Name).WithAlias(() => personalProjectDto.PersonalName)
                    .Select(x => jProject.Name).WithAlias(() => personalProjectDto.ProjectName)
                    .Select(x => x.Id).WithAlias(() => personalProjectDto.Id)
                    .Select(x => x.CreatedAt).WithAlias(() => personalProjectDto.CreatedAt)
                )
                .TransformUsing(Transformers.AliasToBean<PersonalProjectDto>())
                .List<PersonalProjectDto>().OrderByDescending(x => x.CreatedAt).ToList();
            return personalProjectDtos;
        }

        public void SaveOrUpdatePersonalProject(PersonalProjectDto data)
        {
            using (var tran = CurrentSession.BeginTransaction())
            {
                var node = CurrentSession.QueryOver<PersonalProject>()
                    .Where(x => x.Id == data.Id)
                    .SingleOrDefault();

                if (node == null)
                {
                    node = new PersonalProject
                    {
                         Project = CurrentSession.Load<Project>(data.ProjectId),
                         Personal = CurrentSession.Load<Personal>(data.PersonalId)
                    };

                    CurrentSession.Save(node);
                }
                else
                {
                    node.Project = CurrentSession.Load<Project>(data.ProjectId);
                    node.Personal = CurrentSession.Load<Personal>(data.PersonalId);
                    node.LastUpdatedAt = DateTime.Now;
                    CurrentSession.Update(node);
                }



                tran.Commit();
            }
        }
        public PersonalProjectDto GetPersonalProject(Guid id)
        {
            var personalProject = CurrentSession.QueryOver<PersonalProject>()
                           .Where(x => x.IsDeleted == false)
                           .Where(x => x.Id == id)
                           .SingleOrDefault();
            return personalProject == null ? new PersonalProjectDto() : new PersonalProjectDto
            {
                Id = id,
                ProjectId=personalProject.Project.Id,
                PersonalId=personalProject.Personal.Id
            };
        }
        public void DeletePersonalProject(Guid id)
        {
            var node = CurrentSession.QueryOver<PersonalProject>().Where(x => x.Id == id).SingleOrDefault();
            node.IsDeleted = true;
            node.DeletedAt = DateTime.Now;
            CurrentSession.Update(node);
            CurrentSession.Flush();
        }

        #endregion

    }
}