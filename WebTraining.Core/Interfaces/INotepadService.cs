using WebTraining.Core.DTO;
using WebTraining.DB.Models;

namespace WebTraining.Core.Interfaces
{
    public interface INotepadService
    {
        NotepadDTO GetNote(int id);
        void DeleteNote(int id);
        void AddNote(NotepadDTO note);
        IEnumerable<NotepadDTO> GetNotes();
        void UpdateNote(NotepadDTO note);
        void Dispose();
    }
}
