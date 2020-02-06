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
                new Note("Lukas","Hallo",DateTime.Now,DateTime.Now,Status.publicN),
                new Note("Lukas","Spalte 2",new DateTime(2019,12,31),DateTime.Now,Status.friendsN),
                new Note("Lukas","abcdef",new DateTime(2012,10,14),new DateTime(2020,1,3),Status.privateN),
                new Note("Lukas","efdsafs",new DateTime(2012,10,14),new DateTime(2020,1,3),Status.publicN)
            };
            return View(notes);
        }

      
    }
}