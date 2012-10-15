using System;
using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;
using Webstep.People.Domain;
using Webstep.PeopleScraper;

namespace Webstep.People.Client.Model
{
    public class PersonParser
    {
        private static Person Parse(HtmlNode personNode)
        {
            var person = new Person { Id = PersonService.GetId() };
            
            person.ImageUrl = personNode.Descendants("img").Select(x => x.GetAttributeValue("src", "")).First();
            var title = personNode.Descendants("em").First(node => node.Attributes.Contains("class") && node.Attributes["class"].Value == "title").
                    InnerText;
            person.Title = Clean(title);
                

            var name = personNode.Descendants("a").First();
            person.Name = name.InnerText;
            person.InfoUrl = name.GetAttributeValue("href", "");

            var email = personNode.Descendants("a").First(x => x.Attributes["href"].Value.Contains("mailto"));

            person.Email =
                email.Attributes["href"].Value.Replace("mailto:", ""); 

            var phone =
                personNode.Descendants("span").First(n => n.Attributes.Contains("class") && n.Attributes["class"].Value.Contains("value"));
            person.Phone = phone.InnerText;

            return person;

        }

        private static string Clean(string target)
        {
            return target.Replace("\t", "").Replace("\r", "").Replace("\n", "");
        }

        public static List<Person> Parse(List<HtmlNode> vcards)
        {
            return vcards.Select(Parse).ToList();
        }
    }
}
