using AutoMapper;
using System.ComponentModel.DataAnnotations;
using WebTraining.Core.DTO;
using WebTraining.Core.DTO.MeasurementsDTO;
using WebTraining.Core.Interfaces;
using WebTraining.DB.Interfaces;
using WebTraining.DB.Models;

namespace WebTraining.Core.Services
{
    public class NotepadService : INotepadService
    {
        IUnitOfWork Database { get; set; }

        public NotepadService(IUnitOfWork unit)
        {
            Database = unit;
        }

        public void AddNote(NotepadDTO note)
        {
            Notepad notepad = new Notepad
            {
                DateNote = note.DateNote.ToUniversalTime(),
                Description = note.Description,
                UserId = note.UserId,
            };
            Database.Notepads.Create(notepad);
            Database.Save();
        }

        public void DeleteNote(int id)
        {
            if (id!= 0)
            {
                Database.Notepads.Delete(id);
                Database.Save();
            }
        }

        public void Dispose()
        {
            Database.Dispose();
        }

        public NotepadDTO GetNote(int id)
        {
            if (id == 0)
            {
                throw new ValidationException();
            }
            var note = Database.Notepads.Get(id);
            if (note == null)
            {
                throw new ValidationException();
            }
            return new NotepadDTO
            {
                DateNote = note.DateNote,
                Description = note.Description
            };            
        }

        public IEnumerable<NotepadDTO> GetNotes()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Notepad, NotepadDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<Notepad>, List<NotepadDTO>>(Database.Notepads.GetAll());
        }

        public IEnumerable<NotepadDTO> GetNeedNotes(User user)
        {
            IEnumerable<NotepadDTO> neednotes = GetNotes().Where(x => x.UserId == user.Id);
            return neednotes;
        }

        public void UpdateNote(NotepadDTO noteDTO)
        {
            var note = Database.Notepads.Get(noteDTO.ID);
            note.DateNote = TimeZoneInfo.ConvertTimeToUtc(DateTime.Now);
            note.Description = noteDTO.Description;
            Database.Notepads.Update(note);
            Database.Save();
        }
    }
}
