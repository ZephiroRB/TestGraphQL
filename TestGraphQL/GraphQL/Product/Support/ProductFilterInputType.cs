using HotChocolate.Data.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestGraphQL.GraphQL.Product.Support
{
    public class ProductFilterInputType : FilterInputType<Models.Product>
    {
        protected override void Configure(IFilterInputTypeDescriptor<Models.Product> descriptor)
        {
            //descriptor.Ignore(t => t.Id);
        }
    }
}
