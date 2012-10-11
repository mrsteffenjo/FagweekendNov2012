using System.Collections.Generic;
using Webstep.People.Domain;

namespace Webstep.PeopleScraper
{
    
    public class Department
    {
    
        public string Name { get; set; }
    
        public string PeopleUrl { get; set; }
    
        public List<Person> People { get; set; }
    }
}
