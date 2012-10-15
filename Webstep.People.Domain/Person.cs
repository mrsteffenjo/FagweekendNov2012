using System;

namespace Webstep.People.Domain
{
    public class Person
    {
        public int Id { get; set; }
        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                NameSplit(_name);
            }
        }
        
        public string Title { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string InfoUrl { get; set; }
        public string Info { get; set; }
        public string ImageUrl { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public void NameSplit(string name)
        {
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
