using ButodoProject.Core.Service.Dto;
using ButodoProject.Core.Service.Interface;
using ButodoProject.Model.Domain;
using NHibernate;
using NHibernate.Transform;
using System;
using System.Collections.Generic;


namespace ButodoProject.Core.Service
{
    public class PersonalRoleService : ServiceBase, IPersonalRoleService
    {
        public PersonalRoleService(ISession session) : base(session)
        {
        }



        public PersonalRoleDto GetPersonalRole(Guid id)
        {
            var personalRole = CurrentSession.QueryOver<PersonalRole>()
                                           .Where(x => x.IsDeleted == false)
                                           .Where(x => x.Personal.Id == id).Take(1).SingleOrDefault();
            RoleDto roleDto = null;

            var roleTypes = CurrentSession.QueryOver<PersonalRole>()
                .Where(x => x.IsDeleted == false).Where(x => x.Personal.Id == id)
                .SelectList(u => u.Select(x => x.RolePageType).WithAlias(() => roleDto.RolePageType)
                .Select(x => x.RoleType).WithAlias(() => roleDto.RoleType)
                .Select(x => x.Personal.Id).WithAlias(() => roleDto.PersonalId))
                .TransformUsing(Transformers.AliasToBean<RoleDto>())
                .List<RoleDto>();

            return personalRole == null ? new PersonalRoleDto() : new PersonalRoleDto
            {
                Id = id,
                PersonalId = personalRole.Personal.Id,
                RoleTypes = roleTypes
            };
        }

        public IList<PersonalRoleDto> ListPersonalRole()
        {
            Personal jPersonal = null;
            PersonalRoleDto personalRoleDto = null;
            var personalRoleList = CurrentSession.QueryOver<PersonalRole>()
                .JoinAlias(x => x.Personal, () => jPersonal)
                .Where(x => x.IsDeleted == false)
                .SelectList(u => u
                    .SelectGroup(x => jPersonal.Name).WithAlias(() => personalRoleDto.PersonalName)
                    .SelectGroup(x => jPersonal.Id).WithAlias(() => personalRoleDto.PersonalId)
                )
                .TransformUsing(Transformers.AliasToBean<PersonalRoleDto>())
                .List<PersonalRoleDto>();

            return personalRoleList;
        }

        public void SaveOrUpdatePersonalRole(PersonalRoleDto data)
        {
            using (var tran = CurrentSession.BeginTransaction())
            {
                List<PersonalRole> list = new List<PersonalRole>();
                var node = CurrentSession.QueryOver<PersonalRole>()
                    .Where(x => x.IsDeleted == false)
                    .Where(x => x.Personal.Id == data.PersonalId)
                    .List();

                if (node.Count < 1)
                {
                    AddPersonalRole(data, list);
                }
                else
                {
                    foreach (var item in node)
                    {
                        CurrentSession.Delete(item);
                    }

                    AddPersonalRole(data, list);

                }
                CurrentSession.Flush();
                tran.Commit();
            }
        }

        private void AddPersonalRole(PersonalRoleDto data, List<PersonalRole> list)
        {
            CurrentSession.Save(new PersonalRole
            {
                Personal = CurrentSession.Load<Personal>(data.PersonalId),
                RolePageType = "CompanyList",
                RoleType = data.CompanyList,
            });
            CurrentSession.Save(new PersonalRole
            {
                Personal = CurrentSession.Load<Personal>(data.PersonalId),
                RolePageType = "CompanyAddorEdit",
                RoleType = data.CompanyAddorEdit,
            });
            CurrentSession.Save(new PersonalRole
            {
                Personal = CurrentSession.Load<Personal>(data.PersonalId),
                RolePageType = "PersonalList",
                RoleType = data.PersonalList,
            });
            CurrentSession.Save(new PersonalRole
            {
                Personal = CurrentSession.Load<Personal>(data.PersonalId),
                RolePageType = "PersonalAddorEdit",
                RoleType = data.PersonalAddorEdit,
            });
            CurrentSession.Save(new PersonalRole
            {
                Personal = CurrentSession.Load<Personal>(data.PersonalId),
                RolePageType = "PersonalProjectList",
                RoleType = data.PersonalProjectList,
            });
            CurrentSession.Save(new PersonalRole
            {
                Personal = CurrentSession.Load<Personal>(data.PersonalId),
                RolePageType = "PersonalProjectAddorEdit",
                RoleType = data.PersonalProjectAddorEdit,
            });
            CurrentSession.Save(new PersonalRole
            {
                Personal = CurrentSession.Load<Personal>(data.PersonalId),
                RolePageType = "ProjectList",
                RoleType = data.ProjectList,
            });
            CurrentSession.Save(new PersonalRole
            {
                Personal = CurrentSession.Load<Personal>(data.PersonalId),
                RolePageType = "ProjectAddorEdit",
                RoleType = data.ProjectAddorEdit,
            });
            CurrentSession.Save(new PersonalRole
            {
                Personal = CurrentSession.Load<Personal>(data.PersonalId),
                RolePageType = "SpendTimeList",
                RoleType = data.SpendTimeList,
            });
            CurrentSession.Save(new PersonalRole
            {
                Personal = CurrentSession.Load<Personal>(data.PersonalId),
                RolePageType = "SpendTimeAddorEdit",
                RoleType = data.SpendTimeAddorEdit,
            });
            CurrentSession.Save(new PersonalRole
            {
                Personal = CurrentSession.Load<Personal>(data.PersonalId),
                RolePageType = "TaskMessageList",
                RoleType = data.TaskMessageList,
            });
            CurrentSession.Save(new PersonalRole
            {
                Personal = CurrentSession.Load<Personal>(data.PersonalId),
                RolePageType = "TaskMessageAddorEdit",
                RoleType = data.TaskMessageAddorEdit,
            });
            CurrentSession.Save(new PersonalRole
            {
                Personal = CurrentSession.Load<Personal>(data.PersonalId),
                RolePageType = "TaskTableList",
                RoleType = data.TaskTableList,
            });
            CurrentSession.Save(new PersonalRole
            {
                Personal = CurrentSession.Load<Personal>(data.PersonalId),
                RolePageType = "TaskTableAddorEdit",
                RoleType = data.TaskTableAddorEdit,
            });
        }
        public void DeletePersonalRole(Guid id)
        {
            var node = CurrentSession.QueryOver<PersonalRole>().Where(x => x.IsDeleted == false).Where(x => x.Personal.Id == id).List();
            foreach (var item in node)
            {
                item.IsDeleted = true;
                item.DeletedAt = DateTime.Now;
                CurrentSession.Update(item);
            }

            CurrentSession.Flush();
        }

    }
}
