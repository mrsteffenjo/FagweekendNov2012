using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Webstep.Fagkomiteen.Web.Models;
using Webstep.People.Domain;

namespace Webstep.Fagkomiteen.Web.Controllers.Api
{
    public class MailController : ApiController
    {
        // POST api/mail
        public HttpResponseMessage Post(Email email)
        {
            try
            {
                MailSender.Send(email);
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, email);
                return response;
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.ToString());
            }
        }

    }
}
