using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Notizen_Projekt.Models;

using Notizen_Projekt.Models.db;

namespace Notizen_Projekt.Controllers
{
    public class HomeController : Controller
    {
        private IRepositoryNote rep;

        [HttpGet]
        public ActionResult Index()
        {
            List<Note> notes;
            User currentUserLoggedIn;
            currentUserLoggedIn = (User)Session["loggedinUser"];

            rep = new RepositoryNote();
            rep.Open();
            notes = rep.GetAllNotes(currentUserLoggedIn);
            ViewBag.AllTags = rep.GetAllTags(currentUserLoggedIn.ID);
            rep.Close();

            return View(notes);
        }

        [HttpPost]
        public ActionResult Index(Note newNote, List<int> tags)
        {
            if (newNote == null)
            {
                return RedirectToAction("Index");
            }

            if (!ModelState.IsValid)
            {
                return View(newNote);
            }
            else
            {
                rep = new RepositoryNote();

                newNote.User = (User)Session["loggedinUser"];

                rep.Open();
                rep.Insert(newNote);
                rep.AddTagsForNote(rep.GetLatestNoteId(), tags);
                rep.Close();
                return RedirectToAction("Index");
            }
        }


        public ActionResult RemoveNote(int noteId)
        {
            if (Session["loggedInUser"] == null) // Überprüft ob ein User angemeldet ist
            {
                return RedirectToAction("Login", "User");
            }

            rep = new RepositoryNote();

            rep.Open();

            // Überprüft User ob ihmn die Notiz wirklich gehört
            if (!rep.CheckUserIdToNoteId(noteId, (User)Session["loggedInUser"]))
            {
                TempData["message"] = "Sie sind nicht der Inhaber dieser Notiz somit wurde sie auch nicht entfernt!";
                return RedirectToAction("Index");
            }

            rep.Delete(noteId);
            rep.Close();
            return RedirectToAction("Index");
        }
    }
}