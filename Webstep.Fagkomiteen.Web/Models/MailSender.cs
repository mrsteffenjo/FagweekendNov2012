using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using Webstep.People.Domain;

namespace Webstep.Fagkomiteen.Web.Models
{
    public class MailSender
    {
        public static void Send(Email email)
        {
            var message = new MailMessage();
            message.To.Add(email.To);
            message.Subject = email.Subject;
            message.From = new MailAddress("robot@fagkomiteen.no");
            message.Body = email.Body;


            var smtp = new SmtpClient("31.24.130.183", 25);
            // var smtp = new SmtpClient("smtp.altibox.no", 25);

            smtp.Send(message);
        }
    }
}