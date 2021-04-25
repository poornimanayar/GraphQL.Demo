﻿using GraphQL.Demo.API.GraphQL.SubscriptionTypes;
using GraphQL.Demo.API.Models.Messaging;
using GraphQL.Resolvers;
using GraphQL.Types;

namespace GraphQL.Demo.API.GraphQL
{
    //operation type in GraphQL
    public class ContosoUniversitySubscription : ObjectGraphType
    {
        public ContosoUniversitySubscription(CourseMessageService courseMessageService)
        {
            // specify fields and resolve
            AddField(new EventStreamFieldType
            {
                Name = "courseAdded",
                Type = typeof(CourseAddedMessageType), //graph type for the field
                Resolver = new FuncFieldResolver<CourseAddedMessage>(c=>c.Source as CourseAddedMessage), 
                Subscriber = new EventStreamResolver<CourseAddedMessage>(c=>courseMessageService.GetMessages()) // observes or subscribe to observable
            });
        }
    }
}