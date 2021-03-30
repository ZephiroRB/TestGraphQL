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
    public class RegionQuery
    {
        [UseApplicationDbContextAttribute]
        [UsePaging(typeof(NonNullType<RegionType>), DefaultPageSize = 50, IncludeTotalCount = true)]

        public IQueryable<Region> GetRegionsFromDQT([ScopedService] ApplicationDbContext context)
        {
            try
            {
                //LogControl.Write("First Log Called from Region Query");

                var region = (from cont in context.Region
                              select new Region
                              {
                                  Id = cont.Id,
                                  CustomerId = cont.CustomerId,
                                  Name = cont.Name,
                                  Created = cont.Created,
                                  // CreatedBy = cont.CreatedByUser,
                                  Modified = cont.Modified,
                                  //UpdatedBy = cont.UpdatedByUser


                              }).AsQueryable();
                return region;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [UseApplicationDbContextAttribute]
        [UseFiltering]
        [UseSorting]
        public Task<Region> GetRegionAsync(
        Guid id,
        RegionByIdDataLoader dataLoader,
        CancellationToken cancellationToken) =>
        dataLoader.LoadAsync(id, cancellationToken);
    }
}
