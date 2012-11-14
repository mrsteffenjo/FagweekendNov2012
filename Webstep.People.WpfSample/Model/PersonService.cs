using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Messaging;
using Newtonsoft.Json;
using RestSharp;
using Webstep.People.Domain;
using Webstep.People.WpfSample.Model.Events;

namespace Webstep.People.WpfSample.Model
{
    public class PersonService
    {
        string peopleUrl = "http://www.fagkomiteen.no/api/person";

        public void AddPerson(Person person)
        {
            var client = new RestClient(peopleUrl);
            var request = new RestRequest(Method.POST) { RequestFormat = DataFormat.Json };
            request.AddBody(person);
            client.ExecuteAsync<Person>(request, response =>
            {
                App.Current.Dispatcher.BeginInvoke(response.StatusCode == HttpStatusCode.OK
                                                       ? new Action(
                                                             () =>
                                                             Messenger.Default.Send(new PersonCreatedEvent
                                                                 {Person = response.Data}))
                                                       : new Action(
                                                             () =>
                                                             Messenger.Default.Send(new CommunicationFailedEvent()
                                                                 {Message = "Failed to add person"})));
            });
        }

        public void Delete(Person person)
        {
            var client = new RestClient(peopleUrl);
            var request = new RestRequest(Method.DELETE) { RequestFormat = DataFormat.Json };
            request.AddParameter("Id", person.Id);
            client.ExecuteAsync<Person>(request, response =>
            {
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    App.Current.Dispatcher.BeginInvoke(new Action(() => Messenger.Default.Send(new PersonDeletedEvent { Person = response.Data })));
                }
                else
                {

                    App.Current.Dispatcher.BeginInvoke(new Action(() => Messenger.Default.Send(new CommunicationFailedEvent() { Message = "Failed to delete person" })));

                }

            });
        }

        public void GetPeople()
        {
             var client = new RestClient(peopleUrl);
            var request = new RestRequest(Method.GET) {RequestFormat = DataFormat.Json};
            client.ExecuteAsync<List<Person>>(request, response =>
            {
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    //var ranking = JsonConvert.DeserializeObject<ScoreRanking>(response.Content);
                    App.Current.Dispatcher.BeginInvoke(new Action(() => Messenger.Default.Send(new PeopleRetrievedEvent { People = response.Data })));
                }
                else
                {
                    
                    App.Current.Dispatcher.BeginInvoke(new Action(() => Messenger.Default.Send(new CommunicationFailedEvent() { Message = "Failed to load persons"})));
                    
                }
                
            });
        }


    }
}
