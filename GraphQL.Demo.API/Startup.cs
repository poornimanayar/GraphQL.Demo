using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using GraphQL.Server;
using Microsoft.Extensions.Configuration;
using GraphQL.Demo.API.Models;
using Microsoft.EntityFrameworkCore;
using GraphQL.Demo.API.GraphQL;
using GraphQL.Demo.API.Repositories;
using GraphQL.Demo.API.Models.Messaging;

namespace GraphQL.Demo.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public IConfiguration Configuration { get; }

        public IWebHostEnvironment Environment { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            //register the db context
            services.AddDbContext<SchoolContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            //register IDataLoaderContextAccessor, to be injected when we need dataloader
            // services.AddSingleton<IDataLoaderContextAccessor, DataLoaderContextAccessor>();

            //register DataLoaderDocumentListener, ensures that each request will have its own context instance
            // services.AddSingleton<DataLoaderDocumentListener>();

            services.AddSingleton<IDocumentExecuter, SerialDocumentExecuter>();

            // register the services
            services.AddScoped<StudentsRepository>();
            services.AddScoped<CoursesRepository>();
            services.AddScoped<EnrollmentRepository>();

            services.AddSingleton<CourseMessageService>();

            //register the schema
            services.AddScoped<ContosoUniversitySchema>();

            //register GraphQL
            services.AddGraphQL(options =>
            {
                options.EnableMetrics = true; // keep track of execution time 
            })
            .AddGraphTypes(ServiceLifetime.Scoped)  //register the various GraphQL types, scans the assembly and adds all types that implements IGraphType
            .AddSystemTextJson() //JSON serialization & deserialization
            .AddErrorInfoProvider(opt => opt.ExposeExceptionStackTrace = true)    //detailed error messaging  
            .AddDataLoader() //data loader for dataloader operations
            .AddWebSockets();
           
            services.AddCors(options =>
                               options.AddPolicy("AllowAll", p => p.AllowAnyOrigin()
                               .AllowAnyMethod()
                               .AllowAnyHeader()));           
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors("AllowAll");

            app.UseWebSockets();

            app.UseGraphQLWebSockets<ContosoUniversitySchema>();

            // use HTTP middleware for ContosoUniversitySchema at default path /graphql. Since we have only one end point controllers can be replaced with middleware. We dont need MVC as the endpoint is created using the middleware
            app.UseGraphQL<ContosoUniversitySchema>();

            // use GraphiQL middleware at default path /ui/graphiql with default options
            app.UseGraphQLGraphiQL();

            // use GraphQL Playground middleware at default path /ui/playground with default options
            app.UseGraphQLPlayground();

            // use GraphQL Playground middleware at default path /ui/altair with default options
            app.UseGraphQLAltair();
        }
    }
}
