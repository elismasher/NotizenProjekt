using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Notizen_Projekt.Models
{
    public class Tag
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public Tag() : this(0, null) { }
        public Tag(int id, string name)
        {
            ID = id;
            Name = name;
        }
    }
}