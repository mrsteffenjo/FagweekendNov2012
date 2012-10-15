using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Web;
using Newtonsoft.Json;
using Webstep.People.Domain;

namespace Webstep.People.WebClient.Models
{
    public class WebstepContext : DbContext
    {
        public DbSet<Person> Persons { get; set; }
    }

    public class DemoDataContextDbInitializer : DropCreateDatabaseIfModelChanges<WebstepContext>
    {
        protected override void Seed(WebstepContext context)
        {
            // Seed data from json file
            string path = HttpContext.Current.Server.MapPath("~/App_Data/Webstep-People.json");
            var persons = JsonConvert.DeserializeObject<List<Person>>(File.ReadAllText(path));
            foreach (var person in persons)
            {
                context.Persons.Add(person);
            }
        }
    } 
}