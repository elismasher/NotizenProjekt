using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Notizen_Projekt.Models;

namespace Notizen_Projekt.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            List<Note> notes = new List<Note> {
                new Note("Lukas","Hallo", "asdfjklöer",DateTime.Now,DateTime.Now,Status.publicN, ColourNote.blue),
                new Note("Elias", "Überschrift", "Hier sollte der Text stehen!", new DateTime(2020,2,12), DateTime.Now, Status.privateN, ColourNote.green),
                new Note("Lukas","Spalte 3", "asdf jklö asdf jklö",new DateTime(2019,12,31),DateTime.Now,Status.friendsN, ColourNote.red),
                new Note("Lukas","abcdef", "Ich sollte in der Spalte 1 sein!",new DateTime(2012,10,14),new DateTime(2020,1,3),Status.privateN, ColourNote.yellow),
                new Note("Lukas","efdsafs", "Hier Text!?",new DateTime(2012,10,14),new DateTime(2020,1,3),Status.publicN, ColourNote.notSpecified)
            };
            return View(notes);
        }

      
    }
}