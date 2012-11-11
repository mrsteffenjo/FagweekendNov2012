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

        public void GetPeopleRest()
        {
             var client = new RestClient(peopleUrl);
            var request = new RestRequest(Method.GET) {RequestFormat = DataFormat.Json};
            //request.AddBody(score);
            client.ExecuteAsync<List<Person>>(request, response =>
            {
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    //var ranking = JsonConvert.DeserializeObject<ScoreRanking>(response.Content);
                    App.Current.Dispatcher.BeginInvoke(new Action(() => Messenger.Default.Send(new PeopleRetrievedEvent { People = response.Data })));
                }
                else
                {
                    
                    App.Current.Dispatcher.BeginInvoke(new Action(() => Messenger.Default.Send(new CommunicationFailedEvent() { Message = "Failed to loat persons"})));
                    
                }
                
            });
        }
    }
}
