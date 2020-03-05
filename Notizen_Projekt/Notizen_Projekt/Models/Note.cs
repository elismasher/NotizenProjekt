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

    public enum ColourNote
    {
        blue, red, yellow, green, grey, white, notSpecified
    }

    public class Note
    {
        public string NoteTitle { get; set; }
        public string NoteText { get; set; }

        public DateTime DateWritten { get; set; }

        public DateTime DateLastEdit { get; set; }

        public Status Status { get; set; }
        public ColourNote ColourNote { get; set; }
        public int Id { get; set; }

        public string Tag { get; set; }


        public Note() : this("", "",DateTime.Now,DateTime.Now,Status.notSpecified, ColourNote.notSpecified, 0,"") { }

        public Note(string noteTitle, string noteText, DateTime dateWritten, DateTime dateLastEdit, Status status, ColourNote colourNote, int id, string tag)
        {
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