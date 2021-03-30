using GreenDonut;
using HotChocolate.DataLoader;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TestGraphQL.Data;

namespace TestGraphQL.Resolver
{
    public class CustomerByIdDataLoader : BatchDataLoader<Guid, Customer>
    {
        private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;

        public CustomerByIdDataLoader(
           IBatchScheduler batchScheduler,
           IDbContextFactory<ApplicationDbContext> dbContextFactory)
           : base(batchScheduler)
        {
            _dbContextFactory = dbContextFactory ??
                throw new ArgumentNullException(nameof(dbContextFactory));
        }

        protected override async Task<IReadOnlyDictionary<Guid, Customer>> LoadBatchAsync(
             IReadOnlyList<Guid> keys,
             CancellationToken cancellationToken)
        {
            await using ApplicationDbContext dbContext =
                _dbContextFactory.CreateDbContext();

            var dbCustomers = await dbContext.Customer
                 .Where(s => keys.Contains(s.Id))
                 .ToDictionaryAsync(t => t.Id, cancellationToken);

            Dictionary<Guid, Customer> customerDist = new Dictionary<Guid, Customer>();
            foreach (var item in dbCustomers)
            {
                customerDist.Add(item.Key, new Customer
                {
                    Id = item.Value.Id,
                    DisplayName = item.Value.DisplayName,
                    Name = item.Value.Name,
                    MainPhone = item.Value.MainPhone,
                    WebSite = item.Value.WebSite,
                    IsDataBaseCreated = item.Value.IsDataBaseCreated,
                    DataBaseCreatedDate = item.Value.DataBaseCreatedDate
                });
            }
            return customerDist;
            //return await dbContext.Customers
            //    .Where(s => keys.Contains(s.Id))
            //    .ToDictionaryAsync(t => t.Id, cancellationToken);
        }
    }
}
