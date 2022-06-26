using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ButodoProject.Core.Model.FixType;
using ButodoProject.Core.Service.Dto;
using ButodoProject.Model.Domain;

namespace ButodoProject.Core.Service.Interface
{
    public interface IProjectService : IServiceBase
    {
        IList<ProjectDto> ListProject();
        ProjectDto GetProject(Guid id);
        void SaveOrUpdateProject(ProjectDto data);
        void DeleteProject(Guid id);

    }
}