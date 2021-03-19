using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestGraphQL.Enum;

namespace TestGraphQL.GraphQL.Product.Support
{

    public record UpdateProductInput(
    Guid ProductId,
string? ProductName,
bool? Active

);


}
