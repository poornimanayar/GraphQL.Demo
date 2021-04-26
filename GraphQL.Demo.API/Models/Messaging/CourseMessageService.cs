using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace GraphQL.Demo.API.Models.Messaging
{
    public class CourseMessageService
    {
        //an object which is both an observable which can be observed and an object which observes the a sequence, CourseAddedMessage

        private readonly ISubject<CourseAddedMessage> _messageStream = new ReplaySubject<CourseAddedMessage>(1); 
        public CourseAddedMessage AddCourseAddedMessage(Course course)
        {
            var message = new CourseAddedMessage
            {
                Id = course.CourseID,
                Title = course.Title
            };
            _messageStream.OnNext(message); //provides observer with the new data and sends message to the subscriber
            return message;
        }

        public IObservable<CourseAddedMessage> GetMessages()
        {
            return _messageStream.AsObservable();
        }
    }
}
