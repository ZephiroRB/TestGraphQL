using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TestGraphQL.Resolver
{
    public class Region
    {
        public string Name { get; set; }

        public Guid CustomerId { get; set; }

        [ForeignKey("CustomerId")]
        public virtual Customer Customer { get; set; }

        public Guid Id { get; set; }

        public Guid CreatedBy { get; set; }

        public DateTime Created { get; set; }


        public Guid? UpdatedBy { get; set; }

        public DateTime? Modified { get; set; }


    }
}
