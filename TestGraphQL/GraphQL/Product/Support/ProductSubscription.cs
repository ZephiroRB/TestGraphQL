using HotChocolate;
using HotChocolate.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestGraphQL.GraphQL.Product.Support
{
    [ExtendObjectType(Name = "Subscription")]
    public class ProductSubscription
    {
        [Subscribe]
        [Topic]
        public Models.Product OnProductAdded([EventMessage] Models.Product product)
        {
            return product;
        }
        [Subscribe]
        [Topic]
        public Models.Product OnProductUpdated([EventMessage] Models.Product product)
        {
            return product;
        }

        [Subscribe]
        [Topic]
        public Models.Product OnProductDeleted([EventMessage] Models.Product product)
        {
            return product;
        }
    }
}
