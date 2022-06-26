using ButodoProject.Core.Model.Domain;

namespace ButodoProject.Core.Model.Mapping
{
    public class CustomerMap : EntityBaseMap<Customer>
    {
        public CustomerMap()
        {
            Map(x => x.FirstName).Length(64);
            Map(x => x.LastName).Length(64);
            Map(x => x.Password).Length(128);
            Map(x => x.Mobile).Length(32);
            Map(x => x.Email).Length(128);
        }
    }
}