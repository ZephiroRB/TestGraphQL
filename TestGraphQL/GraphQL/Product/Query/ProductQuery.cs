using HotChocolate.Data;
using HotChocolate.Types;
using System;
using HotChocolate;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestGraphQL.Extensions;
using TestGraphQL.Data;
using TestGraphQL.GraphQL.Product.Support;
using FizzWare.NBuilder;
using Microsoft.AspNetCore.Authorization;

namespace TestGraphQL.GraphQL.Product.Query
{
    [ExtendObjectType(Name = "Query")]
    [Authorize]
    public class ProductQuery
    {
        [UseApplicationDbContextAttribute]
        [UsePaging(typeof(NonNullType<ProductType>), DefaultPageSize = 50, IncludeTotalCount = true)]
        [UseFiltering(typeof(ProductFilterInputType))]
        [UseSorting]
        public IQueryable<Models.Product> GetRegions([ScopedService] NakodaAgenciesDbContext context)
        {
            try
            {

                return context.Products.AsQueryable();
                //return null;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
