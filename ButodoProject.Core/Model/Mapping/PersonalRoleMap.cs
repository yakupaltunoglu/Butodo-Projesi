using ButodoProject.Model.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace ButodoProject.Core.Model.Mapping
{
    public class PersonalRoleMap : EntityBaseMap<PersonalRole>
    {
        public PersonalRoleMap()
        {
            Map(x => x.RoleType);
            Map(x => x.RolePageType);
            References(x => x.Personal).Column("Personal");
            
        }
    }
}
