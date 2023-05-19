using Microsoft.AspNetCore.Mvc;
using WebTraining.Core.DTO;
using WebTraining.Core.Interfaces;
using WebTraining.Models;

namespace WebTraining.Controllers
{
    public class NotedpadController : Controller
    {
        readonly INotepadService notepadService;

        public NotedpadController(INotepadService serv)
        {
            notepadService = serv;
        }
        public IActionResult Index()
        {
            IEnumerable<NotepadDTO> notes=notepadService.GetNotes().ToList();
            NoteViewModel noteView = new NoteViewModel()
            {
                Exercises = notes
            };
            return View(noteView);
        }

        [HttpGet]
        public IActionResult DeleteNote(int id) 
        {
            notepadService.DeleteNote(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult CreateNote()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateNote(NotepadDTO note)
        {
            if (ModelState.IsValid != false)
            {

                notepadService.AddNote(note);
                return RedirectToAction("Index");
            }
            else
            {
                return View(note);
            }
        }

        [HttpGet]
        public IActionResult EditNote(int id) 
        {
            NotepadDTO note = notepadService.GetNote(id);
            if (note!=null)
            {
                return View(note);
            }
            return RedirectToAction("Index");            
        }

        [HttpPost]
        public IActionResult EditNote(NotepadDTO note)
        {
            notepadService.UpdateNote(note);
            return RedirectToAction("Index");
        }
    }
}
