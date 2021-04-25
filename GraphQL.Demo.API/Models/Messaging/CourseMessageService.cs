using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace GraphQL.Demo.API.Models.Messaging
{
    public class CourseMessageService
    {
        private readonly ISubject<CourseAddedMessage> _messageStream = new ReplaySubject<CourseAddedMessage>(1); //an observable and an observing sequence 

        public CourseAddedMessage AddCourseAddedMessage(Course course)
        {
            var message = new CourseAddedMessage
            {
                Id = course.CourseID,
                Title = course.Title
            };
            _messageStream.OnNext(message); //provides observer with the new data
            return message;
        }

        public IObservable<CourseAddedMessage> GetMessages()
        {
            return _messageStream.AsObservable();
        }
    }
}
