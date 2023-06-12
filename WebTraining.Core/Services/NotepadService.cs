using AutoMapper;
using System.ComponentModel.DataAnnotations;
using WebTraining.Core.DTO;
using WebTraining.Core.Interfaces;
using WebTraining.DB.Interfaces;
using WebTraining.DB.Models;

namespace WebTraining.Core.Services
{
    public class NotepadService : INotepadService
    {
        private readonly INotepadRepository<Notepad> service;
        private readonly IMapper mapper;

        public NotepadService(INotepadRepository<Notepad> service, IMapper mapper)
        {
            this.mapper = mapper;
            this.service = service;
        }

        public void AddNote(NotepadDTO note)
        {
            Notepad notepad = new Notepad
            {
                DateNote = DateTime.UtcNow,
                Description = note.Description,
                UserId = note.UserId,
            };
            service.Create(notepad);
        }

        public void DeleteNote(int id)
        {
            if (id!= 0)
            {
                service.Delete(id);
            }
        }

        public NotepadDTO GetNote(int id)
        {
            if (id == 0)
            {
                throw new ValidationException();
            }
            var note = service.Get(id);
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
            var note_list = service.GetAll();
            return mapper.Map<IEnumerable<NotepadDTO>>(note_list);
        }

        public IEnumerable<NotepadDTO> GetNeedNotes(User user)
        {
            IEnumerable<NotepadDTO> neednotes = GetNotes().Where(x => x.UserId == user.Id);
            foreach (var item in neednotes)
            {
                item.DateNote= item.DateNote.ToLocalTime();
            }
            return neednotes;
        }

        public void UpdateNote(NotepadDTO noteDTO)
        {
            var note = service.Get(noteDTO.ID);
            note.DateNote = DateTime.UtcNow;
            note.Description = noteDTO.Description;
            service.Update(note);
        }
    }
}
