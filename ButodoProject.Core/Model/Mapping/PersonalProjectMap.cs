using FluentNHibernate.Mapping;
using ButodoProject.Model.Domain;
using ButodoProject.Core.Model.Mapping;

namespace ButodoProject.Model.Mapping
{
    public class PersonalProjectMap : EntityBaseMap<PersonalProject>
    {
        public PersonalProjectMap()
        {
            References(x => x.Personal).Column("Personal");
            References(x => x.Project).Column("Project");
        }

    }
}
