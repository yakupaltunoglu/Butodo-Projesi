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
    public class SpendTimeService : ServiceBase, ISpendTimeService
    {

       

        public SpendTimeService(ISession session) : base(session)
        {
        }
        #region Crud
       

        public IList<SpendTimeDto> ListSpendTime()
        {
            SpendTimeDto spendTimeDto = null;
            Personal jPersonal = null;
            TaskTable jTaskTable = null;
            var spendTimes = CurrentSession.QueryOver<SpendTime>()
                .JoinAlias(x => x.Personal, () => jPersonal)
                .JoinAlias(x => x.TaskTable, () => jTaskTable)
                .Where(x => x.IsDeleted == false)
                .SelectList(u => u
                    .Select(x => jPersonal.Name).WithAlias(() => spendTimeDto.PersonalName)
                    .Select(x => jTaskTable.Name).WithAlias(() => spendTimeDto.TaskTableName)
                    .Select(x => x.Hour).WithAlias(() => spendTimeDto.Hour)
                    .Select(x => x.Id).WithAlias(() => spendTimeDto.Id)
                )
                .TransformUsing(Transformers.AliasToBean<SpendTimeDto>())
                .List<SpendTimeDto>();

            return spendTimes;
        }
        public void SaveOrUpdateSpendTime(SpendTimeDto data)
        {
            using (var tran = CurrentSession.BeginTransaction())
            {
                var node = CurrentSession.QueryOver<SpendTime>()
                    .Where(x => x.Id == data.Id)
                    .SingleOrDefault();

                if (node == null)
                {
                    node = new SpendTime
                    {
                        Hour = data.Hour,
                        Personal = CurrentSession.Load<Personal>(data.PersonalId),
                        TaskTable = CurrentSession.Load<TaskTable>(data.TaskTableId),
                    };

                    CurrentSession.Save(node);
                }
                else
                {
                    node.Hour = data.Hour;
                    node.Personal = CurrentSession.Load<Personal>(data.PersonalId); 
                    node.TaskTable = CurrentSession.Load<TaskTable>(data.TaskTableId);
                    node.LastUpdatedAt = DateTime.Now;
                    CurrentSession.Update(node);
                }


                tran.Commit();
            }
        }
     

        public SpendTimeDto GetSpendTime(Guid id)
        {
           var spendTime =CurrentSession.QueryOver<SpendTime>()
                                           .Where(x => x.IsDeleted == false)
                                           .Where(x => x.Id == id).SingleOrDefault();

            return spendTime == null ? new SpendTimeDto() : new SpendTimeDto
            {
                Id = id,
                Hour = spendTime.Hour, 
                PersonalId = spendTime.Personal.Id,
                TaskTableId = spendTime.TaskTable.Id, 
            };


        }

        public void DeleteSpendTime(Guid id)
        {
            var node = CurrentSession.QueryOver<SpendTime>().Where(x => x.Id == id).SingleOrDefault();
            node.IsDeleted = true;
            node.DeletedAt = DateTime.Now;
            CurrentSession.Update(node);
            CurrentSession.Flush();
        }
        #endregion
    }
}