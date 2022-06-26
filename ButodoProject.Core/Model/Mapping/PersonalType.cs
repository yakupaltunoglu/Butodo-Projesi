using ButodoProject.Model.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace ButodoProject.Core.Model.Mapping
{
    public class PersonalTypeMap : EntityBaseMap<PersonalType>
    {
        public PersonalTypeMap()
        {
            Map(x => x.Name);
        }
    }
}
