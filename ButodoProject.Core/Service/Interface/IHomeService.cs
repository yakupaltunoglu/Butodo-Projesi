using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ButodoProject.Core.Model.FixType;
using ButodoProject.Core.Service.Dto;
using ButodoProject.Model.Domain;

namespace ButodoProject.Core.Service.Interface
{
    public interface IHomeService : IServiceBase
    {
        #region Login Account
      
        PersonalDto GetPersonal(string username, string password);
        //void ChangeSendForgotPassword(string email);
        #endregion
        IList<ProjectDto> GetListProjectCategory();
        IList<TaskTableDto> GetListProjectTask(Guid projectId);
    }
}