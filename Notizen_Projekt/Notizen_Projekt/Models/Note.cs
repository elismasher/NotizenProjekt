using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Notizen_Projekt.Models
{
    public enum Status
    {
        privateN, publicN, friendsN
    }

    public class Note
    {
        private User _user;

        public User User
        {
            get { return this._user; }
            set
            {
                if (value != this._user)
                {
                    this._user = value;
                }
            }
        }

        public string NoteTitle { get; set; }
        public string NoteText { get; set; }

        public DateTime DateWritten { get; set; }

        public DateTime DateLastEdit { get; set; }

        public Status Status { get; set; }
        public string ColourNote { get; set; }
        public int Id { get; set; }

        public List<Tag> Tag { get; set; }

        public Note() : this(null, "", "", DateTime.Now,DateTime.Now, Status.privateN, "", 0, new List<Tag>()) { }

        public Note(User user,string noteTitle, string noteText, DateTime dateWritten, DateTime dateLastEdit, Status status, string colourNote, int id, List<Tag> tag)
        {
            this.User = user;
            this.NoteTitle = noteTitle;
            this.NoteText = noteText;
            this.DateWritten = dateWritten;
            this.DateLastEdit = dateLastEdit;
            this.Status = status;
            this.ColourNote = colourNote;
            this.Id = id;
            this.Tag = tag;
        }

    }
}