using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Notizen_Projekt.Models
{
    public enum Status
    {
        privateN, publicN, friendsN, notSpecified 
    }

    public class Note
    {
        private string _user;


        public string User 
        { 
            get { return this._user; }
            set 
            { if (value != this._user ) 
                {
                    this._user = value;
                } 
            }
        }

        public string NoteText { get; set; }

        public DateTime DateWritten { get; set; }

        public DateTime DateLastEdit { get; set; }

        public Status Status { get; set; }

        public Note() : this("", "",DateTime.Now,DateTime.Now,Status.notSpecified) { }

        public Note(string user,string noteText, DateTime dateWritten, DateTime dateLastEdit, Status status)
        {
            this.User = user;
            this.NoteText = noteText;
            this.DateWritten = dateWritten;
            this.DateLastEdit = dateLastEdit;
            this.Status = status;
        }

    }
}