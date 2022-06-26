using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using NHibernate;
using ButodoProject.Core.Service.Interface;
using Microsoft.Extensions.Options;
using ButodoProject.Core.Service.Dto;

namespace ButodoProject.Core.Service
{
    public class ManagerService : ServiceBase, IManagerService
    {


       

        public ManagerService(ISession session) : base(session)
        {
        }

    }
}