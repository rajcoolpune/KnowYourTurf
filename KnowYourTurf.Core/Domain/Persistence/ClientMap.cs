using FluentNHibernate.Mapping;

namespace KnowYourTurf.Core.Domain.Persistence
{
    public class ClientMap : DomainEntityMap<Client>
    {
        public ClientMap()
        {
            Map(x => x.Name);
            Map(x => x.ZipCode);
            Map(x => x.TaxRate);
            Map(x => x.Address);
            Map(x => x.Address2);
            Map(x => x.City);
            Map(x => x.State);
            Map(x => x.Notes);
            Map(x => x.NumberOfSites);
            HasMany(x => x.Sites).Access.CamelCaseField(Prefix.Underscore).LazyLoad().Cascade.SaveUpdate();
            HasMany(x => x.UserRoles).Access.CamelCaseField(Prefix.Underscore).LazyLoad().Cascade.SaveUpdate();
        }
    }

    public class SiteMap : DomainEntityMap<Site>
    {
        public SiteMap()
        {
            Map(x => x.Name);
            Map(x => x.SiteOperation);
            Map(x => x.Description);
            HasMany(x => x.Fields).Access.CamelCaseField(Prefix.Underscore).LazyLoad().Cascade.SaveUpdate();
        }
    }
}