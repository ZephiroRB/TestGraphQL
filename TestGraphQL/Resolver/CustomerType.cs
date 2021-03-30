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
    public class CustomerType : ObjectType<Customer>
    {
        protected override void Configure(IObjectTypeDescriptor<Customer> descriptor)
        {
            // descriptor.Field(c => c.Id).Ignore();
            descriptor
                 .ImplementsNode()
                 .IdField(t => t.Id)
                 .ResolveNode((ctx, id) => ctx.DataLoader<CustomerByIdDataLoader>().LoadAsync(id, ctx.RequestAborted));

            descriptor
                .Field(t => t.Region)
                .ResolveWith<CustomerResolvers>(t => t.GetRegionAsync(default!, default!, default!, default))
                .UseDbContext<ApplicationDbContext>()
                //.UsePaging<NonNullType<SessionType>>()
                .Name("Region");

            //descriptor
            //    .Field(t => t.Name)
            //    .UseUpperCase();
        }

        private class CustomerResolvers
        {
            public async Task<IEnumerable<Region>> GetRegionAsync(
                Customer customer,
                [ScopedService] ApplicationDbContext dbContext,
                RegionByIdDataLoader sessionById,
                CancellationToken cancellationToken)
            {
                Guid[] sessionIds = await dbContext.Region
                   .Where(s => s.CustomerId == customer.Id)
                   .Select(s => s.Id)
                   .ToArrayAsync();

                return await sessionById.LoadAsync(sessionIds, cancellationToken);
            }
        }
    }
}
