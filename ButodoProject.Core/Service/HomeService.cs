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
    public class HomeService : ServiceBase, IHomeService
    {

        public HomeService(ISession session) : base(session)
        {
        }
       

        #region Login Account
        public PersonalDto GetPersonal(string username, string password)
        {
            PersonalDto personalDto = null;
            var personal = CurrentSession.QueryOver<Personal>()
                .Where(x => x.IsDeleted == false)
                .Where(x => x.Username == username && x.Password == password)
                .SelectList(u => u
                    .Select(x => x.Id).WithAlias(() => personalDto.Id)
                    .Select(x => x.Name).WithAlias(() => personalDto.Name)
                    .Select(x => x.Username).WithAlias(() => personalDto.Username)
                    .Select(x => x.Password).WithAlias(() => personalDto.Password)
                )
                .TransformUsing(Transformers.AliasToBean<PersonalDto>())
                .SingleOrDefault<PersonalDto>();

            return personal;
        }



        //public void ChangeSendForgotPassword(string email)
        //{
        //    string message = "Kayıt Güncellendi";
        //    using (var transaction = CurrentSession.BeginTransaction())
        //    {
        //        var educator = CurrentSession.QueryOver<Educator>()
        //                    .Where(x => x.IsDeleted == false)
        //                    .Where(x => x.Email == email).SingleOrDefault();
        //        if (educator == null)
        //        {
        //            SetResultAsFail("Mail bulunamadı");
        //            return;
        //        }
        //        string newPassword = EmailHelper.SendNewPasswordEducatorMail(email);
        //        educator.Password = CryptoHelper.EncryptByMd5(newPassword);
        //        message += "  ve Sisteme giriş şifresi mailine gönderildi.";
        //        CurrentSession.Update(educator);
        //        transaction.Commit();
        //    }
        //    SetResultAsSuccess(message);
        //}

        #endregion

        public IList<ProjectDto> GetListProjectCategory()
        {
            ProjectDto projectDto = null;
            var projectList = CurrentSession.QueryOver<Project>()
                   .Where(x => x.IsDeleted == false)
                   .Where(x => x.Depth == 2)
                   .SelectList(u => u
                    .Select(x => x.Name).WithAlias(() => projectDto.Name)
                    .Select(x => x.FullName).WithAlias(() => projectDto.FullName)
                    .Select(x => x.Leftx).WithAlias(() => projectDto.Leftx)
                    .Select(x => x.Rightx).WithAlias(() => projectDto.Rightx)
                    .Select(x => x.Depth).WithAlias(() => projectDto.Depth)
                    .Select(x => x.Id).WithAlias(() => projectDto.Id)
                )
                .TransformUsing(Transformers.AliasToBean<ProjectDto>())
                .List<ProjectDto>();



            var projectDtos = new List<ProjectDto>();

            foreach (var item in projectList)
            {

                List<TaskTableDto> taskTableDtos = new List<TaskTableDto>();
                var result = GetListProjectTask(item.Id);
                if (result.Count > 0)
                {
                    foreach (var item2 in result)
                    {
                        taskTableDtos.Add(new TaskTableDto
                        {
                            Id = item2.Id,
                            Name = item2.Name,
                            EndDate = item2.EndDate,
                        });
                    }
                    projectDtos.Add(new ProjectDto { Name = item.Name, TaskTableList = taskTableDtos });
                }
            }

            return projectDtos;
        }

        public IList<TaskTableDto> GetListProjectTask(Guid projectId)
        {
            TaskTableDto taskTableDto = null;
            Project jProject = null;
            Personal jPersonal = null;
            var taskTableList = CurrentSession.QueryOver<TaskTable>()
                .JoinAlias(x => x.Project, () => jProject)
                .JoinAlias(x => x.Personal, () => jPersonal)
                 .Where(x => x.IsDeleted == false)
                 .Where(x => x.Project.Id == projectId)
                 .SelectList(u => u
                    .Select(x => x.EndDate).WithAlias(() => taskTableDto.EndDate)
                    .Select(x => x.CreatedAt).WithAlias(() => taskTableDto.CreatedAt)
                    .Select(x => x.Name).WithAlias(() => taskTableDto.Name)
                    .Select(x => x.Id).WithAlias(() => taskTableDto.Id)
                    .Select(x => jProject.Name).WithAlias(() => taskTableDto.ProjectName)
                    .Select(x => jPersonal.Name).WithAlias(() => taskTableDto.PersonalName)
                    .Select(x => jProject.Id).WithAlias(() => taskTableDto.ProjectId)
                    .Select(x => jPersonal.Id).WithAlias(() => taskTableDto.PersonalId)
                )
                .TransformUsing(Transformers.AliasToBean<TaskTableDto>())
                .List<TaskTableDto>();

            return taskTableList;
        }
    }
}