using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TestGraphQL.Data;
using TestGraphQL.Extensions;

namespace TestGraphQL.Resolver
{
    [ExtendObjectType(Name = "Query")]
    public class CustomerQuery
    {
        [UseApplicationDbContextAttribute]
        [UsePaging(typeof(NonNullType<CustomerType>), DefaultPageSize = 50, IncludeTotalCount = true)]
        //[UseFiltering(typeof(CustomerFilterInputType))]
        //[UseSorting]
        //[UseProjection]
        public IQueryable<Customer> GetCustomers([ScopedService] ApplicationDbContext context)
        {
            try
            {
                //logger.LogError(LogLevel.Error, "0225 ILogger: error from index page aa111");
                //logger.LogInformation("Foo called");

                var customers = (from cust in context.Customer
                                 select new Customer
                                 {
                                     Id = cust.Id,
                                     DisplayName = cust.DisplayName,
                                     MainPhone = cust.MainPhone,

                                     WebSite = cust.WebSite
                                 }).AsQueryable();
                //return customers.OrderByDescending(d => d.Created);
                return customers;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [UseApplicationDbContextAttribute]
        [UseFiltering]
        [UseSorting]
        public Task<Customer> GetCustomerAsync(
        Guid id,
        CustomerByIdDataLoader dataLoader,
        CancellationToken cancellationToken) =>
        dataLoader.LoadAsync(id, cancellationToken);
    }
}
