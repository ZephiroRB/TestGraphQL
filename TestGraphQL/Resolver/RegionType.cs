using HotChocolate;
using HotChocolate.Resolvers;
using HotChocolate.Types;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TestGraphQL.Data;

namespace TestGraphQL.Resolver
{
    public class RegionType : ObjectType<Region>
    {
        protected override void Configure(IObjectTypeDescriptor<Region> descriptor)
        {
            // descriptor.Field(c => c.Id).Ignore();
            descriptor
                 .ImplementsNode()
                 .IdField(t => t.CustomerId)
                 .ResolveNode((ctx, id) => ctx.DataLoader<RegionByIdDataLoader>().LoadAsync(id, ctx.RequestAborted));

            descriptor
                .Field(t => t.Customer)
                .ResolveWith<CustomerResolvers>(t => t.GetCustomerAsync(default!, default!, default!, default))
                .UseDbContext<ApplicationDbContext>()
                //.UsePaging<NonNullType<SessionType>>()
                .Name("Customer");

            //descriptor
            //    .Field(t => t.Name)
            //    .UseUpperCase();
        }

        private class CustomerResolvers
        {
            public async Task<IEnumerable<Customer>> GetCustomerAsync(
                Region Region,
                [ScopedService] ApplicationDbContext dbContext,
                CustomerByIdDataLoader sessionById,
                CancellationToken cancellationToken)
            {
                Guid[] sessionIds = await dbContext.Customer
                   .Where(s => s.Id == Region.CustomerId)
                   .Select(s => s.Id)
                   .ToArrayAsync();

                return await sessionById.LoadAsync(sessionIds, cancellationToken);
            }
        }
    }
}
