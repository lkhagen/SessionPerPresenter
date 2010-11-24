using DataObjects.Entities;
using FluentNHibernate.Mapping;

namespace DataObjects.Mapping
{
    public class OrderMap : ClassMap<Order>
    {
        public OrderMap()
        {
            Id(x => x.OrderId);

            Map(x => x.OrderDate);
            Map(x => x.Comment);

            References(x => x.Customer);
        }
    }
}
