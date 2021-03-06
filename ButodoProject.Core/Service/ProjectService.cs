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
    public class ProjectService : ServiceBase, IProjectService
    {
        public ProjectService(ISession session) : base(session)
        {
        }
        #region Crud
        public IList<ProjectDto> ListProject()
        {
            Company jCompany = null;

            ProjectDto projectDto = null;
            var projectList = CurrentSession.QueryOver<Project>()
                .JoinAlias(x => x.Company, () => jCompany)
                .Where(x => x.IsDeleted == false)
                .SelectList(u => u
                    .Select(x => jCompany.Name).WithAlias(() => projectDto.CompanyName)
                    .Select(x => x.Id).WithAlias(() => projectDto.Id)
                    .Select(x => x.FullName).WithAlias(() => projectDto.FullName)
                    .Select(x => x.Name).WithAlias(() => projectDto.Name)
                    .Select(x => x.Leftx).WithAlias(() => projectDto.Leftx)
                    .Select(x => x.Rightx).WithAlias(() => projectDto.Rightx)
                    .Select(x => x.Depth).WithAlias(() => projectDto.Depth)
                    .Select(x => x.CreatedAt).WithAlias(() => projectDto.CreatedAt)
                )
                .TransformUsing(Transformers.AliasToBean<ProjectDto>())
                .List<ProjectDto>().OrderByDescending(x => x.CreatedAt).ToList();
            return projectList;
        }

        public void SaveOrUpdateProject(ProjectDto data)
        {
            using (var tran = CurrentSession.BeginTransaction())
            {
                var node = CurrentSession.QueryOver<Project>()
                    .Where(x => x.Id == data.Id)
                    .SingleOrDefault();

                if (node == null)
                {
                    node = new Project
                    {
                        Name = data.Name,
                        FullName = data.FullName,
                        Leftx = data.Leftx,
                        Rightx = data.Rightx,
                        Depth = data.Depth,
                        Company = CurrentSession.Load<Company>(data.CompanyId),
                    };

                    CurrentSession.Save(node);
                }
                else
                {

                    node.Name = data.Name;
                    node.FullName = data.FullName;
                    node.Leftx = data.Leftx;
                    node.Rightx = data.Rightx;
                    node.Depth = data.Depth;
                    node.Company = CurrentSession.Load<Company>(data.CompanyId);
                    node.LastUpdatedAt = DateTime.Now;
                    CurrentSession.Update(node);
                }



                tran.Commit();
            }
        }
        public ProjectDto GetProject(Guid id)
        {
            var project = CurrentSession.QueryOver<Project>()
                           .Where(x => x.IsDeleted == false)
                           .Where(x => x.Id == id)
                           .SingleOrDefault();

            return project == null ? new ProjectDto() : new ProjectDto
            {
                Id = id,
                Name = project.Name,
                FullName = project.FullName,
                Leftx=project.Leftx,
                Rightx=project.Rightx,
                Depth=project.Depth,
                CompanyId= project.Company.Id

            };
        }
        public void DeleteProject(Guid id)
        {
            var node = CurrentSession.QueryOver<Project>().Where(x => x.Id == id).SingleOrDefault();
            node.IsDeleted = true;
            node.DeletedAt = DateTime.Now;
            CurrentSession.Update(node);
            CurrentSession.Flush();
        }

        #endregion

    }
}