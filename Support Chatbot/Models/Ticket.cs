using Microsoft.Bot.Connector;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Xml.Linq;
using System;

namespace QnABotWithMSI.Models
{
    public class Ticket
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string AssignTo { get; set; }
        public string StatusName { get; set; }
        public string ProcessFlowName { get; set; }
        public string Username { get; set; }
        public string TenantName { get; set; }
        public string PriorityName { get; set; }
        public string SeverityName { get; set; }
        public List<string> Tags { get; set; }
        public string CreationDate { get; set; }

    }
}
