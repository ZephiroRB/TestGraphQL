using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TestGraphQL.Resolver
{
    public class Customer
    {
        public Guid Id { get; set; }

        public string? Name { get; set; }
        public string? DisplayName { get; set; }
        public string? MainPhone { get; set; }
        public string? WebSite { get; set; }
        public bool? IsDataBaseCreated { get; set; }
        public DateTime? DataBaseCreatedDate { get; set; }


        [ForeignKey("Id")]
        public virtual Region Region { get; set; }

    }
}
