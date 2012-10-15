using System;
using System.Collections.Generic;
using System.Linq;
using GalaSoft.MvvmLight.Messaging;
using HtmlAgilityPack;
using Webstep.People.Client.Model;
using Webstep.People.Domain;
using Webstep.PeopleScraper.Events;

namespace Webstep.PeopleScraper
{
    public class PersonService
    {
        private static int idGenerator;
        public PersonService()
        {
            Persons = new List<Person>();
            Messenger.Default.Register(this, new Action<PeopleRetrievedEvent>((e) =>
            {
                foreach (var person in e.People)
                {
                    Persons.Add(person);
                }
            }));


        }

        public static int GetId()
        {
            idGenerator += 1;
            return idGenerator;
        }

        public List<Person> Persons { get; set; }

        public Person Get(int id)
        {
            return Persons.First(p => p.Id == id);
        }

        public void DownloadPeople(string url)
        {
            var web = new HtmlWeb();
            var doc = web.Load(url);

            // Lets get our li nodes where 
            var vcards = doc.DocumentNode.Descendants("li").
                Where(x => x.Attributes.Contains("class") && x.Attributes["class"].Value.Contains("vcard")).ToList();
            var persons = PersonParser.Parse(vcards);
            Messenger.Default.Send(new PeopleRetrievedEvent { People = persons });
        }

        public void DownloadPersonInfo(Person person)
        {
            var web = new HtmlWeb();
            var doc = web.Load(person.InfoUrl);

            // Get info entry
            var result = doc.DocumentNode.Descendants("div").First(x => x.Attributes.Contains("class") && x.Attributes["class"].Value.Contains("entry"));
            var info = result.InnerText.Replace("\t", String.Empty).Replace("\r", String.Empty).Replace("Last ned vCard", "");
            person.Info = info;
            Messenger.Default.Send(new PersonInfoRetrievedEvent()
                {
                    Person = person,
                    Info = info
                });
        }
    }
}
