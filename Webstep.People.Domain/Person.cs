using System;

namespace Webstep.People.Domain
{
    public class Person
    {
        public static Person Create()
        {
            var person = new Person
                {
                    FirstName = "Senior",
                    LastName = "Konsulent",
                    Email = "seniorkonsulent@webstep.no",
                    Title = "Seniorkonsulent",
                    ImageUrl = "http://www.webstep.no/wp-content/uploads/2010/02/Ikon_bildeerstatt8-110x150.png"
                };
            return person;

        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string InfoUrl { get; set; }
        public string Info { get; set; }
        public string ImageUrl { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public void SplitName(string name)
        {
            if (string.IsNullOrEmpty(name)) return;
            if (name.Length > 0)
            {
                // Check for a comma
                if (name.IndexOf(",") > 0)
                {
                    LastName = name.Substring(0, name.IndexOf(",")).Trim();
                    FirstName = name.Substring(name.IndexOf(",") + 1).Trim();
                }
                else if (name.IndexOf(" ") > 0)
                {
                    FirstName = name.Substring(0, name.IndexOf(" ")).Trim();
                    LastName = name.Substring(name.IndexOf(" ") + 1).Trim();
                }
            }
        }
    }
}
