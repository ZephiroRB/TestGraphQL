using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestGraphQL.Comman;

namespace TestGraphQL.GraphQL.Product.Support
{
    public class AddProductPayload : Payload
    {
        public AddProductPayload(TestGraphQL.Models.Product product)
        {
            Product = product;
        }
        public AddProductPayload(TransactionMessage error)
            : base(new[] { error })
        {
        }



        public TestGraphQL.Models.Product? Product { get; }
    }
}
