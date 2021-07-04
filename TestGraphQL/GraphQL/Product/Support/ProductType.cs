using HotChocolate.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestGraphQL.GraphQL.Product.Support
{
    public class ProductType : ObjectType<Models.Product>
    {
        protected override void Configure(IObjectTypeDescriptor<Models.Product> descriptor)
        {
            //descriptor.Field(c => c.Id).Ignore();

            //descriptor
            //   .Field(t => t.Sessions)
            //   .ResolveWith<TrackResolvers>(t => t.GetSessionsAsync(default!, default!, default!, default))
            //   .UseDbContext<ApplicationDbContext>()
            //   .UsePaging<NonNullType<SessionType>>()
            //   .Name("sessions");
            
        }
    }
}
