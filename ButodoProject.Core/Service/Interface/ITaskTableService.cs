using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ButodoProject.Core.Model.FixType;
using ButodoProject.Core.Service.Dto;
using ButodoProject.Model.Domain;

namespace ButodoProject.Core.Service.Interface
{
    public interface ITaskTableService : IServiceBase
    {
        IList<TaskTableDto> ListTaskTable();
        TaskTableDto GetTaskTable(Guid id);
        void SaveOrUpdateTaskTable(TaskTableDto data);
        void DeleteTaskTable(Guid id);

    }
}