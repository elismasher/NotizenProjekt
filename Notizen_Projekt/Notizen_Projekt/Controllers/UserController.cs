using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Notizen_Projekt.Models;

using Notizen_Projekt.Models.db;

namespace Notizen_Projekt.Controllers
{
    public class UserController : Controller
    {
        private IRepositoryUser rep;

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            if (Session["loggedinUser"] != null)
            {
                return RedirectToAction("index", "home");
            }

            return View(new UserLogin());
        }

        [HttpPost]
        public ActionResult Login(UserLogin user)
        {
            User userFromDB;

            rep = new RepositoryUser();
            rep.Open();
            userFromDB = rep.Login(user);
            rep.Close();

            if (userFromDB == null)
            {
                ModelState.AddModelError("Username", "Benutzername oder Passwort stimmen nicht überein!");
                return View(user);
            }
            else
            {
                Session["loggedinUser"] = userFromDB;
                return RedirectToAction("index", "home");
            }
        }

        public ActionResult Logout()
        {
            if (Session["loggedinUser"] != null)
            {

                ViewBag.Name = ((User)Session["loggedinUser"]).Username;

                Session["loggedinUser"] = null;
            }

            // Response.AddHeader("REFRESH", "5;URL=/User/Login");
            
            return View();
        }

        [HttpGet]
        public ActionResult Registration()
        {
            return View(new User());
        }

        [HttpPost]
        public ActionResult Registration(User user)
        {
            if (user == null)
            {
                return RedirectToAction("Registration");
            }

            CheckUserData(user);

            if (!ModelState.IsValid)
            {
                return View(user);
            }
            else
            {
                rep = new RepositoryUser();

                rep.Open();
                if (rep.Insert(user))
                {
                    rep.Close();
                    return View("Message", new Message("Registrierung", "Ihre Daten wurde erfolgreich abgespeichert!"));
                }
                else
                {
                    rep.Close();
                    return View("Message", new Message("Registrierung", "Ihre Daten konnten nicht abgespeichert werden!"));
                }
            }
        }

        private void CheckUserData(User user)
        {
            if (user == null)
            {
                return;
            }

            if (string.IsNullOrEmpty(user.Firstname.Trim()))
            {
                ModelState.AddModelError("Firstname", "Vorname muss eingetragen werden");
            }
            if (string.IsNullOrEmpty(user.Lastname.Trim()))
            {
                ModelState.AddModelError("Lastname", "Nachname muss eingetragen werden");
            }
            if (string.IsNullOrEmpty(user.Username.Trim()))
            {
                ModelState.AddModelError("Username", "Ein Username muss verwendet werden");
            }

            if (!CheckPassword(user.Password))
            {
              ModelState.AddModelError("Password", "Passwort muss mindestens 8 Zeichen lang sein und mind. 1 Großbuchstaben und mind. 1 Zahl enthalten.");
            }

            if (user.Password != user.PasswordConfirm)
            {
                ModelState.AddModelError("PasswordConfirm", "Es muss 2mal das gleiche Passwort eingegeben werden.");
            }

            if (!EmailContainsAddSign(user.Email, 1))
            {
                ModelState.AddModelError("Email", "Email muss @ Zeichen enthalten.");
            }

            if (!EmailContainsAddSign(user.Email, 1))
            {
                ModelState.AddModelError("Email", "Email muss . Zeichen enthalten.");
            }

        }

        private bool CheckPassword(string password)
        {
            if (password.Length < 8)
            {
                return false;
            }

            if (!PWContainsNumber(password, 1))
            {
                return false;
            }
            if (!PWContainsUppercaseCharacter(password, 1))
            {
                return false;
            }

            return true;
        }

       private bool PWContainsUppercaseCharacter(string text,int minCount)
       {
               int count = 0;
               foreach (char c in text)
                {
                    if (char.IsUpper(c))
                    {
                        count++;
                    }
                }

           return count >= minCount;
       }

        private bool PWContainsNumber(string text, int minCount)
        {
            int count = 0;
            foreach (char c in text)
            {
                if (char.IsNumber(c))
                {
                    count++;
                }
            }
            return count >= minCount;
        }

        private bool EmailContainsAddSign(string text, int minCount)
        {
            string allowedChars = "@";
            int count = 0;
            foreach (char c in text)
            {
                if (allowedChars.Contains(c))
                {
                    count++;
                }
            }

            return count >= minCount;
        }

        private bool EmailContainsDot(string text, int minCount)
        {
            string allowedChars = ".";
            int count = 0;
            foreach (char c in text)
            {

                if (allowedChars.Contains(c))
                {
                    count++;
                }
            }
            return count >= minCount;
        }
    }
}