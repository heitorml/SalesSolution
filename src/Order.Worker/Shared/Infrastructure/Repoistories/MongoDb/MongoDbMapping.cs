using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using Orders.Worker.Entities;
using System.Diagnostics.CodeAnalysis;

namespace Orders.Worker.Shared.Infrastructure.MongoDb
{
    [ExcludeFromCodeCoverage]
    public static class MongoDbMapping
    {
        public static void RegisterMappings()
        {
            if (!BsonClassMap.IsClassMapRegistered(typeof(Order)))
            {
                BsonClassMap.RegisterClassMap<Order>(cm =>
                {
                    cm.AutoMap();
                    cm.MapIdMember(c => c.Id)
                      .SetIdGenerator(StringObjectIdGenerator.Instance);

                    cm.MapMember(c => c.Items).SetElementName("items");
                    cm.MapMember(c => c.Resale).SetElementName("resale");
                    cm.MapMember(c => c.Price).SetElementName("price");
                    cm.MapMember(c => c.Status).SetElementName("status");
                    cm.MapMember(c => c.CreatAt).SetElementName("creatAt");
                });
            }

            if (!BsonClassMap.IsClassMapRegistered(typeof(Resale)))
            {
                BsonClassMap.RegisterClassMap<Resale>(cm =>
                {
                    cm.AutoMap();
                    cm.MapIdMember(c => c.Id)
                      .SetIdGenerator(StringObjectIdGenerator.Instance);
                    cm.MapMember(e => e.Name).SetElementName("name");
                    cm.MapMember(e => e.FantasyName).SetElementName("fantasyName");
                    cm.MapMember(e => e.Address).SetElementName("address");
                    cm.MapMember(e => e.Active).SetElementName("active");
                    cm.MapMember(e => e.ContactName).SetElementName("contactName");
                    cm.MapMember(e => e.Cnpj).SetElementName("cnpj");
                    cm.MapMember(e => e.CreateAt).SetElementName("createAt");
                    cm.MapMember(e => e.Email).SetElementName("email");
                    cm.MapMember(e => e.Phone).SetElementName("phone");
                    cm.MapMember(e => e.UpdadeAt).SetElementName("updadeAt");

                });
            }

            if (!BsonClassMap.IsClassMapRegistered(typeof(Address)))
            {
                BsonClassMap.RegisterClassMap<Address>(cm =>
                {
                    cm.AutoMap();
                    cm.MapMember(e => e.Name).SetElementName("name");
                    cm.MapMember(e => e.ZipCode).SetElementName("zipCode");
                    cm.MapMember(e => e.City).SetElementName("city");
                });
            }

            if (!BsonClassMap.IsClassMapRegistered(typeof(OrderItems)))
            {
                BsonClassMap.RegisterClassMap<OrderItems>(cm =>
                {
                    cm.AutoMap();
                    cm.MapMember(e => e.Name).SetElementName("name");
                    cm.MapMember(e => e.Price).SetElementName("price");
                    cm.MapMember(e => e.Quantity).SetElementName("quantity");
                    cm.MapMember(e => e.Description).SetElementName("description");
                });
            }
        }
    }
}
