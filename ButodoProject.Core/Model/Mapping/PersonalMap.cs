using ButodoProject.Model.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace ButodoProject.Core.Model.Mapping
{
    public class PersonalMap : EntityBaseMap<Personal>
    {
        public PersonalMap()
        {
            Map(x => x.Name);
            Map(x => x.Surname);
            Map(x => x.Email);
            References(x => x.Company).Column("Company");
            References(x => x.PersonalType).Column("PersonalType");
            Map(x => x.Username);
            Map(x => x.Password);
        }
    }
}
