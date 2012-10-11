using System.Collections.Generic;
using Webstep.People.Client.Model;
using Webstep.People.Domain;

namespace Webstep.PeopleScraper.Events
{
    public class PeopleRetrievedEvent
    {
        public List<Person> People { get; set; }
    }
}
