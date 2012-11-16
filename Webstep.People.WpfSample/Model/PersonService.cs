using System;
using System.Collections.Generic;
using System.Net;
using GalaSoft.MvvmLight.Messaging;
using RestSharp;
using Webstep.People.Domain;
using Webstep.People.WpfSample.Model.Events;

namespace Webstep.People.WpfSample.Model
{
    public class PersonService
    {
        private const string PeopleUrl = "http://www.fagkomiteen.no/api/person";
<<<<<<< HEAD
        private readonly RestClient _client;
=======
        private  RestClient _client;
>>>>>>> A lot of stuff

        public PersonService()
        {
             _client = new RestClient(PeopleUrl);
        }

        public void AddPerson(Person person)
        {
            var request = new RestRequest(Method.POST) { RequestFormat = DataFormat.Json };
            request.AddBody(person);
            _client.ExecuteAsync<Person>(request, response =>
            {
                if (response.StatusCode == HttpStatusCode.Created)
                {
                    App.Current.Dispatcher.BeginInvoke(new Action(() => Messenger.Default.Send(new PersonCreatedEvent{Person = response.Data})));    
                }
                else
                {
                    App.Current.Dispatcher.BeginInvoke(new Action(() => Messenger.Default.Send(new CommunicationFailedEvent() { Message = "Failed to Add person" })));
                }
<<<<<<< HEAD
=======
            });
        }

        public void UpdatePerson(Person person)
        {
            var request = new RestRequest(Method.PUT) { RequestFormat = DataFormat.Json };
            request.AddBody(person);
            var url = PeopleUrl + "/" + person.Id;
            _client = new RestClient(url);
            _client.ExecuteAsync<Person>(request, response =>
            {
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    App.Current.Dispatcher.BeginInvoke(new Action(() => Messenger.Default.Send(new PersonUpdatedEvent { Person = response.Data })));
                }
                else
                {
                    App.Current.Dispatcher.BeginInvoke(new Action(() => Messenger.Default.Send(new CommunicationFailedEvent { Message = "Failed to update person" })));
                }
>>>>>>> A lot of stuff
            });
        }

        public void Delete(Person person)
        {
            var request = new RestRequest(Method.DELETE) { RequestFormat = DataFormat.Json };
            request.AddParameter("Id", person.Id);
            _client.ExecuteAsync<Person>(request, response =>
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
            var request = new RestRequest(Method.GET) {RequestFormat = DataFormat.Json};
            _client.ExecuteAsync<List<Person>>(request, response =>
            {
                if (response.StatusCode == HttpStatusCode.OK)
                {
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
