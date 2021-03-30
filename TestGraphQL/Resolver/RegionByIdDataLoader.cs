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
    public class RegionByIdDataLoader : BatchDataLoader<Guid, Region>
    {
        private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;

        public RegionByIdDataLoader(
           IBatchScheduler batchScheduler,
           IDbContextFactory<ApplicationDbContext> dbContextFactory)
           : base(batchScheduler)
        {
            _dbContextFactory = dbContextFactory ??
                throw new ArgumentNullException(nameof(dbContextFactory));
        }

        protected override async Task<IReadOnlyDictionary<Guid, Region>> LoadBatchAsync(
             IReadOnlyList<Guid> keys,
             CancellationToken cancellationToken)
        {
            try
            {
                await using ApplicationDbContext dbContext =
                    _dbContextFactory.CreateDbContext();

                var dbCustomers = await dbContext.Region
                     .Where(s => keys.Contains(s.Id))
                     .ToDictionaryAsync(t => t.Id, cancellationToken);

                Dictionary<Guid, Region> customerDist = new Dictionary<Guid, Region>();
                foreach (var item in dbCustomers)
                {
                    customerDist.Add(item.Key, new Region
                    {
                        Id = item.Value.Id,
                        Name = item.Value.Name,
                        CustomerId = item.Value.CustomerId
                    });
                }
                return customerDist;
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                return null;
            }
            //return await dbContext.Customers
            //    .Where(s => keys.Contains(s.Id))
            //    .ToDictionaryAsync(t => t.Id, cancellationToken);
        }
    }
}
