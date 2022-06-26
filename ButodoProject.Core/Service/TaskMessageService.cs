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
    public class TaskMessageService : ServiceBase, ITaskMessageService
    {
        public TaskMessageService(ISession session) : base(session)
        {
        }
        #region Crud
        public IList<TaskMessageDto> ListTaskMessage()
        {
            TaskTable jTaskTable = null;

            TaskMessageDto taskTableDto = null;
            var taskMessageDtos = CurrentSession.QueryOver<TaskMessage>()
                .JoinAlias(x => x.TaskTable, () => jTaskTable)
                .Where(x => x.IsDeleted == false)
                .SelectList(u => u
                    .Select(x => jTaskTable.Name).WithAlias(() => taskTableDto.TaskTableName)
                    .Select(x => x.Id).WithAlias(() => taskTableDto.Id)
                    .Select(x => x.Name).WithAlias(() => taskTableDto.Name)
                     .Select(x => x.CreatedAt).WithAlias(() => taskTableDto.CreatedAt)
                )
                .TransformUsing(Transformers.AliasToBean<TaskMessageDto>())
                .List<TaskMessageDto>().OrderByDescending(x => x.CreatedAt).ToList();
            return taskMessageDtos;
        }

        public void SaveOrUpdateTaskMessage(TaskMessageDto data)
        {
            using (var tran = CurrentSession.BeginTransaction())
            {
                var node = CurrentSession.QueryOver<TaskMessage>()
                    .Where(x => x.Id == data.Id)
                    .SingleOrDefault();

                if (node == null)
                {
                    node = new TaskMessage
                    {
                        TaskTable = CurrentSession.Load<TaskTable>(data.TaskTableId),
                        Name = data.Name,

                    };

                    CurrentSession.Save(node);
                }
                else
                {
                    node.TaskTable = CurrentSession.Load<TaskTable>(data.TaskTableId);
                    node.Name = data.Name;
                    node.LastUpdatedAt = DateTime.Now;
                    CurrentSession.Update(node);
                }

                tran.Commit();
            }
        }
        public TaskMessageDto GetTaskMessage(Guid id)
        {
            var taskMessage = CurrentSession.QueryOver<TaskMessage>()
                           .Where(x => x.IsDeleted == false)
                           .Where(x => x.Id == id)
                           .SingleOrDefault();
           
            
                return taskMessage == null ? new TaskMessageDto() : new TaskMessageDto
                {
                    Id = id,
                    Name = taskMessage.Name,
                    TaskTableId = taskMessage.TaskTable.Id,
                };

        }
        public void DeleteTaskMessage(Guid id)
        {
            var node = CurrentSession.QueryOver<TaskMessage>().Where(x => x.Id == id).SingleOrDefault();
            node.IsDeleted = true;
            node.DeletedAt = DateTime.Now;
            CurrentSession.Update(node);
            CurrentSession.Flush();
        }

        #endregion

    }
}