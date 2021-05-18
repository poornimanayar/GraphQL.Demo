using GraphQL.Types;
using System;
using Microsoft.Extensions.DependencyInjection;

namespace GraphQL.Demo.API.GraphQL
{
    public class ContosoUniversitySchema : Schema
    {
        public ContosoUniversitySchema(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            //operation types
            Query = serviceProvider.GetRequiredService<ContosoUniversityQuery>();
            Mutation = serviceProvider.GetRequiredService<ContosoUniversityMutation>();
            Subscription = serviceProvider.GetRequiredService<ContosoUniversitySubscription>();
        }
    }
}
