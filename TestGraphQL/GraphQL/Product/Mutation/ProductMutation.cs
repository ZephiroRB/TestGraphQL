using FizzWare.NBuilder;
using HotChocolate;
using HotChocolate.Subscriptions;
using HotChocolate.Types;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TestGraphQL.Data;
using TestGraphQL.Enum;
using TestGraphQL.Extensions;
using TestGraphQL.GraphQL.Product.Support;
using TestGraphQL.Token;

namespace TestGraphQL.GraphQL.Product.Mutation
{
    [ExtendObjectType(Name = "Mutation")]
    public class ProductMutation
    {
        [UseApplicationDbContextAttribute]
        public async Task<AddProductPayload> AddProductAsync(
        AddProductInput input,
        [ScopedService] NakodaAgenciesDbContext context,
        [Service] ITopicEventSender eventSender,
        CancellationToken cancellationToken
        )
        {
            try
            {

                //List<TestGraphQL.Models.Product> _address = Builder<TestGraphQL.Models.Product>.CreateListOfSize(10000).All()
                //  .With(c => c.ProductName = Faker.Address.StreetName())

                //  ).Build().ToList();

                var region = new TestGraphQL.Models.Product
                {

                    ProductName = input.ProductName,
                    Active = true,
                    CreatedDate = DateTime.Now
                };
                context.Products.AddRange(region);


                await context.SaveChangesAsync(cancellationToken);

                await eventSender.SendAsync(nameof(ProductSubscription.OnProductAdded), region, cancellationToken);

                var _regionResponse = (from _region in context.Products
                                       where _region.ProductId.Equals(region.ProductId)
                                       select new Models.Product
                                       {
                                           ProductId = _region.ProductId ?? default(Guid),
                                           ProductName = _region.ProductName,
                                           Active = _region.Active,
                                           CreatedDate = _region.CreatedDate

                                       }).FirstOrDefault();

                return new AddProductPayload(_regionResponse);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [UseApplicationDbContextAttribute]
        public async Task<UpdateProductPayload> UpdateProductAsync(
       UpdateProductInput input,
       [ScopedService] NakodaAgenciesDbContext context,
        [Service] ITopicEventSender eventSender,
        CancellationToken cancellationToken
       )
        {
            try
            {
                var region = context.Products.Where(c => c.ProductId.Equals(input.ProductId)).FirstOrDefault();
                if (region == null)
                {
                    throw new Exception("No Region found for" + input.ProductId);
                }
                if (!string.IsNullOrEmpty(input.ProductName))
                    region.ProductName = input.ProductName;

                if (!string.IsNullOrEmpty(Convert.ToString(input.Active)))
                    region.Active = input.Active;

                context.Products.Update(region);

                await context.SaveChangesAsync(cancellationToken);

                await eventSender.SendAsync(nameof(ProductSubscription.OnProductUpdated), region, cancellationToken);

                var regionResponse = (from _region in context.Products
                                      where _region.ProductId.Equals(region.ProductId)
                                      select new Models.Product
                                      {
                                          ProductId = _region.ProductId ?? default(Guid),
                                          ProductName = _region.ProductName,
                                          Active = _region.Active,
                                          CreatedDate = _region.CreatedDate
                                      }).FirstOrDefault();

                return new UpdateProductPayload(regionResponse);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private List<User> Users = new List<User>
        {
            new User{
                Id = 1,
                FirstName = "Naveen",
                LastName = "Bommidi",
                Email = "naveen@gmail.com",
                Password="1234",
                PhoneNumber="8888899999"
            },
            new User{
                Id = 2,
                FirstName = "Hemanth",
                LastName = "Kumar",
                Email = "hemanth@gmail.com",
                Password = "abcd",
                PhoneNumber = "2222299999"
            }
        };
        public string UserLogin([Service] IOptions<TokenSettings> tokenSettings, LoginInput login)
        {
            var currentUser = Users.Where(_ => _.Email.ToLower() == login.Email.ToLower() &&
            _.Password == login.Password).FirstOrDefault();

            if (currentUser != null)
            {
                var securitykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenSettings.Value.Key));
                var credentials = new SigningCredentials(securitykey, SecurityAlgorithms.HmacSha256);

                var jwtToken = new JwtSecurityToken(
                    issuer: tokenSettings.Value.Issuer,
                    audience: tokenSettings.Value.Audience,
                    expires: DateTime.Now.AddMinutes(20),
                    signingCredentials: credentials
                );

                return new JwtSecurityTokenHandler().WriteToken(jwtToken);

            }
            return "";
        }
    }
}
