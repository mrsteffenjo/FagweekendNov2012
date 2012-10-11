using Webstep.People.Domain;

namespace Webstep.PeopleScraper.Events
{
    public class PersonInfoRetrievedEvent
    {
        public Person Person { get; set; }
        public string Info { get; set; }
    }
}
