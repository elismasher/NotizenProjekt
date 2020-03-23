using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notizen_Projekt.Models.db
{
    interface IRepositoryNote
    {
        void Open();
        void Close();

        bool Insert(Note newNote);
        bool AddTagsForNote(int nodeId, List<int> tags);
        bool Delete(int id);
        bool UpdateNoteData(int id, Note newNoteData);
        Note GetNote(int idNote);
        List<Note> GetAllNotes(User user);
        int GetLatestNoteId();

        List<Tag> GetAllTags(int userId);
        List<Tag> GetAllTagsByNoteId(int noteId);
        bool CheckUserIdToNoteId(int noteId, User user);
    }
}
