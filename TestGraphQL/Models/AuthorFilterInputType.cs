using HotChocolate.Data.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestGraphQL.Models
{
    public class AuthorFilterInputType : FilterInputType<Models.Author>
    {
        protected override void Configure(IFilterInputTypeDescriptor<Models.Author> descriptor)
        {
            //descriptor.Ignore(t => t.Id);
        }
    }
}
