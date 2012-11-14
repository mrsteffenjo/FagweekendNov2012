using Webstep.People.Domain;

namespace Webstep.People.WpfSample.Model.Events
{
    public class PersonDeletedEvent
    {
        public Person Person { get; set; }
    }
}