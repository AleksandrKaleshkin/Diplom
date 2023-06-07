using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebTraining.Core.DTO;
using WebTraining.Core.Interfaces;
using WebTraining.Core.Models;
using WebTraining.DB.Models;
using WebTraining.Models;

namespace WebTraining.Controllers
{
    [Authorize]
    public class TrainingController : Controller
    {
        readonly ITrainingService trainingService;
        readonly ITrainingExerciseService trainingExerciseService;
        readonly UserManager<User> userManager;


        public TrainingController(ITrainingService serv, ITrainingExerciseService service, UserManager<User> userManager)
        {
            trainingService = serv;
            trainingExerciseService = service;
            this.userManager = userManager;
        }


        public async Task<IActionResult> Index()
        {
            User user = await GetUser();
            TrainingViewModel training = new TrainingViewModel()
            {
                Trainings = trainingService.GetNeedTraining(user).ToList()
            };
            return View(training);
        }

        public IActionResult AllTraining()
        {
            TrainingViewModel training = new TrainingViewModel()
            {
                Trainings = trainingService.GetTrainings().ToList()
            };
            return View(training);
        }


        [HttpGet]

        public IActionResult CreateTraining() 
        {
            AddEditTrainingViewModel model = new AddEditTrainingViewModel
            {
                TrainingDTO = new TrainingDTO{DateTraining= DateTime.Now},
                Users = new UserList(trainingService.GetAllUsers().ToList())
                
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTraining(AddEditTrainingViewModel training)
        {
            if (ModelState.IsValid)
            {
                trainingService.AddTraing(training.TrainingDTO);
                return RedirectToAction("AllTraining");
            }
            else
            {
                training.Users = new UserList(trainingService.GetAllUsers().ToList());
                return View(training);
            }

        }
        [HttpGet]
        public IActionResult EditTraining(int id)
        {
            AddEditTrainingViewModel model = new AddEditTrainingViewModel
            {
                TrainingDTO = trainingService.GetTraining(id),
                Users = new UserList(trainingService.GetAllUsers().ToList())
               

            };
            return View(model);
        }

        [HttpPost]
        public IActionResult EditTraining(AddEditTrainingViewModel training)
        {
            if (ModelState.IsValid)
            {
                trainingService.UpdateTraining(training.TrainingDTO);
                return RedirectToAction("AllTraining");
            }
            else
            {
                training.Users = new UserList(trainingService.GetAllUsers().ToList());
                return View(training);
            }
        }


        [HttpGet]
        public IActionResult DeleteTraining(int id) 
        {
            trainingService.DeleteTraining(id);
            return RedirectToAction("AllTraining"); 
        }

        [HttpGet]
        public IActionResult ViewExercises(int id)
        {
            List<TrainingExerciseDTO> exercise = trainingExerciseService.GetNeedExercises(id).ToList();
            TrainingExerciseViewModel exerciseViewModel = new TrainingExerciseViewModel()
            {
                
                ExerciseTraining = exercise,
                TrainingId= id,
  
                
            };
            if (id!= 0)
            {
                return View(exerciseViewModel);
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult AddExercise(int id) 
        
        {
            AddEditTrainingExerciseViewModel model = new AddEditTrainingExerciseViewModel
            {
                TrainingExercises = trainingExerciseService.GetExercise(id),
                ExerciseList = new ExerciseList(trainingExerciseService.GetExerciseList().ToList())
            };
            
            return View(model); 
        }

        [HttpPost]
        public IActionResult AddExercise(AddEditTrainingExerciseViewModel model) 
        {
            if (ModelState.IsValid)
            {
                trainingExerciseService.AddExercise(model.TrainingExercises);
                return RedirectToAction("ViewExercises", new { id = model.TrainingExercises.TrainingId });
            }
            else
            {
                model.ExerciseList = new ExerciseList(trainingExerciseService.GetExerciseList().ToList());
                return View(model);
            };

        }

        [HttpGet]
        public IActionResult EditExercise(int id)
        {
            AddEditTrainingExerciseViewModel model = new AddEditTrainingExerciseViewModel
            {
                TrainingExercises = trainingExerciseService.GetNeedExercise(id),
                ExerciseList = new ExerciseList(trainingExerciseService.GetExerciseList().ToList())
            };
            return View(model);  

        }

        [HttpPost]
        public IActionResult EditExercise(AddEditTrainingExerciseViewModel model)
        {
            if (ModelState.IsValid)
            {
                trainingExerciseService.UpdateExercise(model.TrainingExercises);
                return RedirectToAction("ViewExercises", new { id = model.TrainingExercises.TrainingId });
            }
            else
            {
                model.ExerciseList = new ExerciseList(trainingExerciseService.GetExerciseList().ToList());
                return View(model);
            }

        }


        public IActionResult DeleteExercise(int id, int age)
        {
            trainingExerciseService.DeleteExercise(id);
            return RedirectToAction("ViewExercises", new { id = age });
        }

        public async Task<User> GetUser ()
        {
            return await userManager.FindByNameAsync(User.Identity.Name);
        }


    }
}
