using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Notizen_Projekt.Models
{
    public class Message
    {

        public string Header { get; set; }

        public string Messagetext { get; set; }

        public string Solution { get; set; }

        public Message() : this("","",""){}

        public Message(string header,string messageText, string solution)
        {
            this.Header = header;
            this.Messagetext = messageText;
            this.Solution = solution;
        }
    }
}