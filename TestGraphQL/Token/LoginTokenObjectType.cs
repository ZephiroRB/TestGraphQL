using HotChocolate.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestGraphQL.Token
{
    [ExtendObjectType(Name = "Mutation")]
    public class LoginTokenObjectType : ObjectType<LoginToken>
    {
        protected override void Configure(IObjectTypeDescriptor<LoginToken> descriptor)
        {
            descriptor.Field(_ => _.UserLogin(default, default))
            .Name("UserLogin")
            .Type<StringType>()
            .Argument("login", a => a.Type<LoginInputObjectType>());
        }
    }
}
