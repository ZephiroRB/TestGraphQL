using FizzWare.NBuilder;
using HotChocolate;
using HotChocolate.Subscriptions;
using HotChocolate.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TestGraphQL.Data;
using TestGraphQL.Enum;
using TestGraphQL.Extensions;
using TestGraphQL.GraphQL.Product.Support;

namespace TestGraphQL.GraphQL.Product.Mutation
{
    [ExtendObjectType(Name = "Mutation")]
    public class ProductMutation
    {
        [UseApplicationDbContextAttribute]
        public async Task<AddProductPayload> AddProductAsync(
        AddProductInput input,
        [ScopedService] NakodaAgenciesDbContext context,
        [Service] ITopicEventSender eventSender,
        CancellationToken cancellationToken
        )
        {
            try
            {

                //List<TestGraphQL.Models.Product> _address = Builder<TestGraphQL.Models.Product>.CreateListOfSize(10000).All()
                //  .With(c => c.ProductName = Faker.Address.StreetName())

                //  ).Build().ToList();

                var region = new TestGraphQL.Models.Product
                {

                    ProductName = input.ProductName,
                    Active = true,
                    CreatedDate = DateTime.Now
                };
                context.Products.AddRange(region);


                await context.SaveChangesAsync(cancellationToken);

                await eventSender.SendAsync(nameof(ProductSubscription.OnProductAdded), region, cancellationToken);

                var _regionResponse = (from _region in context.Products
                                       where _region.ProductId.Equals(region.ProductId)
                                       select new Models.Product
                                       {
                                           ProductId = _region.ProductId ?? default(Guid),
                                           ProductName = _region.ProductName,
                                           Active = _region.Active,
                                           CreatedDate = _region.CreatedDate
                                           
                                       }).FirstOrDefault();

                return new AddProductPayload(_regionResponse);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [UseApplicationDbContextAttribute]
        public async Task<UpdateProductPayload> UpdateProductAsync(
       UpdateProductInput input,
       [ScopedService] NakodaAgenciesDbContext context,
        [Service] ITopicEventSender eventSender,
        CancellationToken cancellationToken
       )
        {
            try
            {
                var region = context.Products.Where(c => c.ProductId.Equals(input.ProductId)).FirstOrDefault();
                if (region == null)
                {
                    throw new Exception("No Region found for" + input.ProductId);
                }
                if (!string.IsNullOrEmpty(input.ProductName))
                    region.ProductName = input.ProductName;

                if (!string.IsNullOrEmpty(Convert.ToString(input.Active)))
                    region.Active = input.Active;

                context.Products.Update(region);

                await context.SaveChangesAsync(cancellationToken);

                await eventSender.SendAsync(nameof(ProductSubscription.OnProductUpdated), region, cancellationToken);

                var regionResponse = (from _region in context.Products
                                      where _region.ProductId.Equals(region.ProductId)
                                      select new Models.Product
                                      {
                                          ProductId = _region.ProductId ?? default(Guid),
                                          ProductName = _region.ProductName,
                                          Active = _region.Active,
                                          CreatedDate = _region.CreatedDate
                                      }).FirstOrDefault();

                return new UpdateProductPayload(regionResponse);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
