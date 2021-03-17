using HotChocolate.Types;
using HotChocolate.Types.Descriptors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using TestGraphQL.Data;

namespace TestGraphQL.Extensions
{
    public class UseApplicationDbContextAttribute : ObjectFieldDescriptorAttribute
    {
        public override void OnConfigure(
            IDescriptorContext context,
            IObjectFieldDescriptor descriptor,
            MemberInfo member)
        {
            descriptor.UseDbContext<NakodaAgenciesDbContext>();
        }
    }
}
