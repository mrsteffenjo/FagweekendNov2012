using System.Collections.Generic;
using Webstep.People.Domain;

namespace Webstep.People.WpfSample.Model.Events
{
    public class PeopleRetrievedEvent
    {
        public List<Person> People{ get; set; }
    }
}
