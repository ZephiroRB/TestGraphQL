using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;
using TestGraphQL.Data;
using HotChocolate.AspNetCore;
using HotChocolate.AspNetCore.Playground;
using HotChocolate.Types;
using Microsoft.EntityFrameworkCore;
using TestGraphQL.GraphQL.Product.Query;
using TestGraphQL.GraphQL.Product.Mutation;
using TestGraphQL.GraphQL.Product.Support;
using HotChocolate;
using TestGraphQL.Models;
using TestGraphQL.GraphQL;
using TestGraphQL.Resolver;
//using TestGraphQL.GraphQL.Product.Mutation;
//using TestGraphQL.GraphQL.Product.Support;

namespace TestGraphQL
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();

            services.AddPooledDbContextFactory<NakodaAgenciesDbContext>(opt =>
            //opt.UseLazyLoadingProxies()
            opt.UseSqlServer
           (this.Configuration.GetConnectionString("DqtConStr")));

            services.AddPooledDbContextFactory<ApplicationDbContext>(opt =>
            //opt.UseLazyLoadingProxies()
            opt.UseSqlServer
           (this.Configuration.GetConnectionString("ConStr")));



            services.AddSingleton<IAuthorService, InMemoryAuthorService>();
            services.AddSingleton<IBookService, InMemoryBookService>();

            services
               .AddGraphQLServer()
               .AddFiltering()
               .AddSorting()
               .AddType(new UuidType(defaultFormat: 'D'))
               .AddQueryType(d => d.Name("Query"))
               .AddType<ProductQuery>()
               .AddType<BookQuery>()
               .AddType<RegionQuery>()
                .AddType<CustomerQuery>()
             .AddMutationType(d => d.Name("Mutation"))
                .AddType<ProductMutation>()
                .AddSubscriptionType(d => d.Name("Subscription"))
                .AddType<ProductSubscription>()
                .AddType<AuthorType>()
                .AddType<BookType>()
                .AddType<RegionType>()
                .AddType<CustomerType>()
                .AddFiltering()
                .AddSorting()
                .AddInMemorySubscriptions();
            //.AddDataLoader<CustomerByIdDataLoader>();

            //services.AddSwaggerGen(c =>
            //{
            //    c.SwaggerDoc("v1", new OpenApiInfo { Title = "TestGraphQL", Version = "v1" });
            //});
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.UsePlayground(new PlaygroundOptions
                {
                    QueryPath = "/api",
                    Path = "/playground"
                });
            }

            app.UseCors(o => o
                 .AllowAnyHeader()
                 .AllowAnyMethod()
                 .AllowAnyOrigin());

            app.UseGraphQL("/api");

            app.UseRouting();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGraphQL();

                endpoints.MapGet("/", context =>
                {
                    context.Response.Redirect("/graphql");
                    return Task.CompletedTask;
                });
            });
        }
    }
}
