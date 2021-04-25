using GraphQL.Demo.API.Models.Messaging;
using GraphQL.Types;

namespace GraphQL.Demo.API.GraphQL.SubscriptionTypes
{
    public class CourseAddedMessageType : ObjectGraphType<CourseAddedMessage>
    {
        public CourseAddedMessageType()
        {
            Field(t => t.Id).Name("Id");
            Field(t => t.Title).Name("title");
        }
    }
}
