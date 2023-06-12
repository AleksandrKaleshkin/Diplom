using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebTraining.Core.DTO;
using WebTraining.Core.Interfaces;
using WebTraining.DB.Models;
using WebTraining.Models;

namespace WebTraining.Controllers
{
    [Authorize]
    public class NotedpadController : Controller
    {
        readonly INotepadService notepadService;
        readonly UserManager<User> userManager;

        public NotedpadController(INotepadService serv, UserManager<User> userManager)
        {
            notepadService = serv;
            this.userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            User user = await GetUser();
            IEnumerable<NotepadDTO> notes=notepadService.GetUserNotes(user).ToList();
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
        public async Task<IActionResult> CreateNote()
        {
            User user = await GetUser();
            NotepadDTO note = new NotepadDTO
            {
                UserId = user.Id,
            };
            return View(note);
        }

        [HttpPost]
        public IActionResult CreateNote(NotepadDTO note)
        {
            if (ModelState.IsValid)
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

        private async Task<User> GetUser()
        {
            return await userManager.FindByNameAsync(User.Identity.Name);
        }
    }
}
