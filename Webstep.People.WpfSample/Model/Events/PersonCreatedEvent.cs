using Webstep.People.Domain;

namespace Webstep.People.WpfSample.Model.Events
{
    public class PersonCreatedEvent
    {
        public Person Person { get; set; }
    }
}