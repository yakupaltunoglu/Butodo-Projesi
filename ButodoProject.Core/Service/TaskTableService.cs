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
    public class TaskTableService : ServiceBase, ITaskTableService
    {
        public TaskTableService(ISession session) : base(session)
        {
        }
        #region Crud
        public IList<TaskTableDto> ListTaskTable()
        {
            Personal jPersonal = null;
            Project jProject = null;

            TaskTableDto taskTableDto = null;
            var taskTableDtos = CurrentSession.QueryOver<TaskTable>()
                .JoinAlias(x => x.Personal, () => jPersonal)
                .JoinAlias(x => x.Project, () => jProject)
                .Where(x => x.IsDeleted == false)
                .SelectList(u => u
                    .Select(x => jPersonal.Name).WithAlias(() => taskTableDto.PersonalName)
                    .Select(x => jProject.ProjectName).WithAlias(() => taskTableDto.ProjectName)
                    .Select(x => x.Id).WithAlias(() => taskTableDto.Id)
                    .Select(x => x.Name).WithAlias(() => taskTableDto.Name)
                    .Select(x => x.EndDate).WithAlias(() => taskTableDto.EndDate)
                    .Select(x => x.CreatedAt).WithAlias(() => taskTableDto.CreatedAt)
                )
                .TransformUsing(Transformers.AliasToBean<TaskTableDto>())
                .List<TaskTableDto>().OrderByDescending(x => x.CreatedAt).ToList();
            return taskTableDtos;
        }

        public void SaveOrUpdateTaskTable(TaskTableDto data)
        {
            using (var tran = CurrentSession.BeginTransaction())
            {
                var node = CurrentSession.QueryOver<TaskTable>()
                    .Where(x => x.Id == data.Id)
                    .SingleOrDefault();

                if (node == null)
                {
                    node = new TaskTable
                    {
                        Project = CurrentSession.Load<Project>(data.ProjectId),
                        Personal = CurrentSession.Load<Personal>(data.PersonalId),
                        EndDate = data.EndDate,
                        Name = data.Name,

                    };

                    CurrentSession.Save(node);
                }
                else
                {
                    node.Project = CurrentSession.Load<Project>(data.ProjectId);
                    node.Personal = CurrentSession.Load<Personal>(data.PersonalId);
                    node.LastUpdatedAt = DateTime.Now;
                    node.EndDate = data.EndDate;
                    node.Name = data.Name;
                    CurrentSession.Update(node);
                }

                tran.Commit();
            }
        }
        public TaskTableDto GetTaskTable(Guid id)
        {
            var taskTable = CurrentSession.QueryOver<TaskTable>()
                           .Where(x => x.IsDeleted == false)
                           .Where(x => x.Id == id)
                           .SingleOrDefault();
            if (taskTable == null)
            {
                return new TaskTableDto()
                {
                    EndDate = DateTime.Now,
                };
            }
            else
            {
                return new TaskTableDto
                {
                    Id = id,
                    ProjectId = taskTable.Project.Id,
                    PersonalId = taskTable.Personal.Id,
                    Name = taskTable.Name,
                    EndDate = taskTable.EndDate,
                };
            }

        }
        public void DeleteTaskTable(Guid id)
        {
            var node = CurrentSession.QueryOver<TaskTable>().Where(x => x.Id == id).SingleOrDefault();
            node.IsDeleted = true;
            node.DeletedAt = DateTime.Now;
            CurrentSession.Update(node);
            CurrentSession.Flush();
        }

        #endregion

    }
}