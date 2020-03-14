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

    public enum ColourNote
    {
        blue, red, yellow, green, grey, white, notSpecified
    }

    public class Note
    {
        private string _user;

        public string User
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
        public ColourNote ColourNote { get; set; }
        public int Id { get; set; }

        public List<string> Tag { get; set; }

        public Note() : this("", "", "", DateTime.Now,DateTime.Now, Status.privateN, ColourNote.notSpecified, 0, new List<string>()) { }

        public Note(string user,string noteTitle, string noteText, DateTime dateWritten, DateTime dateLastEdit, Status status, ColourNote colourNote, int id, List<string> tag)
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