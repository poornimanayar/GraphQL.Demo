using GraphQL.Client.Abstractions;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.SystemTextJson;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace GraphQL.Demo.Blazor.App
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            var graphQlClient = new GraphQLHttpClient("https://localhost:44301/graphql", new SystemTextJsonSerializer());
            builder.Services.AddScoped<IGraphQLClient>(x => graphQlClient);


            await builder.Build().RunAsync();
        }
    }
}
