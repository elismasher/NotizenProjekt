using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Notizen_Projekt.Models;

namespace Notizen_Projekt.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
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
            if(user == null)
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
                return View("Message", new Message("Registrierung", "Registrierung erfolgreich"));
            }
        }

        private void CheckUserData(User user)
        {
            if(user == null)
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

        }

        private bool CheckPassword(string password)
        {
            if (password.Length >= 8)
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
               foreach(char c in text)
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



    }
}