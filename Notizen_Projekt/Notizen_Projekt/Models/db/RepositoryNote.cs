using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Web;

namespace Notizen_Projekt.Models.db
{
    public class RepositoryNote : IRepositoryNote
    {
        // hier eigenen Login verwenden (für Datenbank)
        private string _connectionString = "Server=localhost; Database=db_NotizenProjekt; Uid=root; Pwd=Klexi2408;";
        private MySqlConnection _connection;

        public void Open()
        {
            if (this._connection == null)
            {
                this._connection = new MySqlConnection(this._connectionString);
            }
            if (this._connection.State != ConnectionState.Open)
            {
                this._connection.Open();
            }
        }

        public void Close()
        {
            if ((this._connection != null) && (this._connection.State != ConnectionState.Closed))
            {
                this._connection.Close();
            }
        }

        public bool Insert(Note newNote)
        {
            if(newNote == null)
            {
                return false;
            }

            DbCommand cmdInsert = this._connection.CreateCommand();
            cmdInsert.CommandText = "INSERT INTO notes(noteTitle, noteText, dateLastEdit, statusNote, colourNote, idUser) VALUES(@noteTitle, @noteText, now(), @noteStatus, @noteColour, @userId);";

            DbParameter paramTitle = cmdInsert.CreateParameter();
            paramTitle.ParameterName = "noteTitle";
            paramTitle.Value = newNote.NoteTitle;
            paramTitle.DbType = DbType.String;

            DbParameter paramText = cmdInsert.CreateParameter();
            paramText.ParameterName = "noteText";
            paramText.Value = newNote.NoteText;
            paramText.DbType = DbType.String;

            DbParameter paramStatus = cmdInsert.CreateParameter();
            paramStatus.ParameterName = "noteStatus";
            paramStatus.Value = newNote.Status;
            paramStatus.DbType = DbType.Int32;

            DbParameter paramColour = cmdInsert.CreateParameter();
            paramColour.ParameterName = "noteColour";
            paramColour.Value = newNote.ColourNote;
            paramColour.DbType = DbType.Int32;

            DbParameter paramUserID = cmdInsert.CreateParameter();
            paramUserID.ParameterName = "userId";
            paramUserID.Value = newNote.User.ID;
            paramUserID.DbType = DbType.Int32;

            cmdInsert.Parameters.Add(paramTitle);
            cmdInsert.Parameters.Add(paramText);
            cmdInsert.Parameters.Add(paramStatus);
            cmdInsert.Parameters.Add(paramColour);
            cmdInsert.Parameters.Add(paramUserID);

            return cmdInsert.ExecuteNonQuery() == 1;

        }

        public bool AddTagsForNote(int nodeId, List<int> tags)
        {
            DbCommand cmd = _connection.CreateCommand();

            cmd.CommandText = "INSERT INTO NM_noteTag VALUES (@nodeId, @tagId);";

            DbParameter paramNodeId = cmd.CreateParameter();
            paramNodeId.ParameterName = "nodeId";
            paramNodeId.Value = nodeId;
            paramNodeId.DbType = DbType.Int32;


            DbParameter paramTagId = cmd.CreateParameter();
            paramTagId.ParameterName = "tagId";
            paramTagId.DbType = DbType.Int32;

            bool ret = true;

            if(tags != null)
            {
                foreach (var tag in tags)
                {
                    paramTagId.Value = tag;
                    cmd.Parameters.Clear();

                    cmd.Parameters.Add(paramNodeId);
                    cmd.Parameters.Add(paramTagId);

                    ret = ret && cmd.ExecuteNonQuery() == 1;
                }
            }
            

            return ret;
        }

        public bool Delete(int id)
        {
            DbCommand cmdDel = this._connection.CreateCommand();
            cmdDel.CommandText = "DELETE FROM notes WHERE id=@noteId";

            DbParameter paramId = cmdDel.CreateParameter();
            paramId.ParameterName = "noteId";
            paramId.Value = id;
            paramId.DbType = DbType.Int32;

            cmdDel.Parameters.Add(paramId);

            return cmdDel.ExecuteNonQuery() == 1;
        }

        public List<Note> GetAllNotes(User user)
        {
            List<Note> notes = new List<Note>();
            int idUser = user.ID;

            DbCommand cmdGetNote = this._connection.CreateCommand();
            cmdGetNote.CommandText = "SELECT * FROM notes WHERE idUser=@userId";

            DbParameter paramId = cmdGetNote.CreateParameter();
            paramId.ParameterName = "userId";
            paramId.Value = idUser;
            paramId.DbType = DbType.Int32;

            cmdGetNote.Parameters.Add(paramId);

            using (DbDataReader reader = cmdGetNote.ExecuteReader())
            {
                if (!reader.HasRows)
                {
                    return null;
                }

                while (reader.Read())
                {
                    notes.Add(
                        new Note
                        {
                            User = user,
                            NoteTitle = Convert.ToString(reader["noteTitle"]),
                            NoteText = Convert.ToString(reader["noteText"]),
                            DateWritten = Convert.ToDateTime(reader["dateWritten"]),
                            DateLastEdit = Convert.ToDateTime(reader["dateLastEdit"]),
                            Status = (Status)Convert.ToInt32(reader["statusNote"]),
                            ColourNote = (ColourNote)Convert.ToInt32(reader["colourNote"]),
                            Id = Convert.ToInt32(reader["id"])
                        }
                    );
                }
            }

            foreach (var note in notes)
            {
                note.Tag = GetAllTagsByNoteId(note.Id);
            }

            return notes;
        }

        public Note GetNote(int idNote)
        {
            DbCommand cmdGetNote = this._connection.CreateCommand();
            cmdGetNote.CommandText = "SELECT * FROM notes WHERE idNote=@id";

            DbParameter paramId = cmdGetNote.CreateParameter();
            paramId.ParameterName = "id";
            paramId.Value = idNote;
            paramId.DbType = DbType.Int32;

            cmdGetNote.Parameters.Add(paramId);

            using (DbDataReader reader = cmdGetNote.ExecuteReader())
            {
                if (!reader.HasRows)
                {
                    return null;
                }


                reader.Read();
                return new Note
                {
                    User = new RepositoryUser().GetUser(Convert.ToInt32(reader["idUser"])),
                    NoteTitle = Convert.ToString(reader["noteTitle"]),
                    NoteText = Convert.ToString(reader["noteText"]),
                    DateWritten = Convert.ToDateTime(reader["dateWritten"]),
                    DateLastEdit = Convert.ToDateTime(reader["dateLastEdit"]),
                    Status = (Status)Convert.ToInt32(reader["statusNote"]),
                    ColourNote = (ColourNote)Convert.ToInt32(reader["colourNote"]),
                    Id = Convert.ToInt32(reader["id"])
                };
            }
        }

        public bool UpdateNoteData(int id, Note newNoteData)
        {
            throw new NotImplementedException();
        }

        public List<Tag> GetAllTags(int userId)
        {
            DbCommand cmdGetAllTags = _connection.CreateCommand();
            cmdGetAllTags.CommandText = "SELECT * FROM tags WHERE userId = @userId;";

            DbParameter paramUserId = cmdGetAllTags.CreateParameter();
            paramUserId.ParameterName = "userId";
            paramUserId.Value = userId;
            paramUserId.DbType = DbType.Int32;

            cmdGetAllTags.Parameters.Add(paramUserId);

            List<Tag> ret = new List<Tag>();

            using (DbDataReader reader = cmdGetAllTags.ExecuteReader())
            {
                while (reader.Read())
                {
                    ret.Add(new Tag
                    {
                        ID = Convert.ToInt32(reader["id"]),
                        Name = Convert.ToString(reader["tagName"])
                    });
                }
            }

            return ret;
        }

        public List<Tag> GetAllTagsByNoteId(int noteId)
        {
            DbCommand cmdGetAllTags = _connection.CreateCommand();
            cmdGetAllTags.CommandText = "SELECT * FROM tags t JOIN NM_noteTag nT ON t.id = nT.idTag WHERE nT.idNote = @noteId;";

            DbParameter paramNoteId = cmdGetAllTags.CreateParameter();
            paramNoteId.ParameterName = "noteId";
            paramNoteId.Value = noteId;
            paramNoteId.DbType = DbType.Int32;

            cmdGetAllTags.Parameters.Add(paramNoteId);

            List<Tag> ret = new List<Tag>();

            using (DbDataReader reader = cmdGetAllTags.ExecuteReader())
            {
                while (reader.Read())
                {
                    ret.Add(new Tag
                    {
                        ID = Convert.ToInt32(reader["id"]),
                        Name = Convert.ToString(reader["tagName"])
                    });
                }
            }

            return ret;
        }

        public int GetLatestNoteId()
        {
            DbCommand cmd = _connection.CreateCommand();
            cmd.CommandText = "SELECT MAX(id) AS maxId FROM notes;";

            int ret;
            using (DbDataReader reader = cmd.ExecuteReader())
            {
                reader.Read();
                ret = Convert.ToInt32(reader["maxId"]);
            }

            return ret;
        }
    }
}