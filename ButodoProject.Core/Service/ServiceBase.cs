using System.Collections.Generic;
using System.Linq;
using NHibernate;
using ButodoProject.Core.Service.Interface;

namespace ButodoProject.Core.Service
{
    public class ServiceBase : IServiceBase
    {
        public ServiceResult Result { get; set; }
        protected ISession CurrentSession;
      
        public ServiceBase(NHibernate.ISession session)
        {
            CurrentSession = session;
            Result = new ServiceResult() { Success = true };
        }

        internal void SetResultAsFail(string errorMessage, ResponseResultCode status)
        {
            this.Result = new ServiceResult() { Success = false, Message = errorMessage, StatusCode = (int)status};
        }

        internal void SetResultAsSuccess(string message, string data)
        {
            this.Result = new ServiceResult() { Message = message, Success = true, Data = data, StatusCode = (int)ResponseResultCode.Success};
        }

        internal void SetResultAsSuccess(string message)
        {
            this.Result = new ServiceResult() { Message = message, Success = true, StatusCode = (int)ResponseResultCode.Success };
        }

        internal void SetResultAsSuccess()
        {
            this.Result = new ServiceResult() { Message = "", Success = true, StatusCode = (int)ResponseResultCode.Success };
        }
    }
}
