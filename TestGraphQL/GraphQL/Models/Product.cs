using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestGraphQL.GraphQL.Models
{
    public class Product
    {
        public Guid ProductId { get; set; }

        public string ProductName { get; set; }
    }
}
