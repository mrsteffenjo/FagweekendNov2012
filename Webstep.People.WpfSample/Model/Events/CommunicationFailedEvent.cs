using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webstep.People.WpfSample.Model.Events
{
    public class CommunicationFailedEvent
    {
        public Exception Exception { get; set; }
        public string Message { get; set; }
    }
}
