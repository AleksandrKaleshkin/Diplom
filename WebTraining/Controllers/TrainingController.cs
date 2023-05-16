using Microsoft.AspNetCore.Mvc;
using WebTraining.Core.DTO;
using WebTraining.Core.Interfaces;
using WebTraining.Models;

namespace WebTraining.Controllers
{
    public class TrainingController : Controller
    {
        readonly ITrainingService trainingService;
        readonly ITrainingExerciseService trainingExerciseService;

        public TrainingController(ITrainingService serv, ITrainingExerciseService service)
        {
            trainingService = serv;
            trainingExerciseService = service;
        }

        public IActionResult Index()
        {
            IEnumerable<TrainingDTO> trainings = trainingService.GetTrainings().ToList();
            TrainingViewModel training = new TrainingViewModel()
            {
                Trainings = trainings
            };
            return View(training);
        }

        [HttpGet]              
        public IActionResult CreateTraining() 
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateTraining(TrainingDTO training)
        {
            if (ModelState.IsValid !=null)
            {
                trainingService.AddTraing(training);
                return RedirectToAction("Index");
            }
            else
            {
                return View(training);
            }
        }

        [HttpGet]
        public IActionResult DeleteTraining(int id) 
        {
            trainingService.DeleteTraining(id);
            return RedirectToAction("Index"); 
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
            if (id!= null)
            {
                return View(exerciseViewModel);
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult AddExercise(int id) 
        {
            TrainingExerciseDTO exercise = trainingExerciseService.GetExercise(id);
            return View(exercise); 
        }

        [HttpPost]
        public IActionResult AddExercise(TrainingExerciseDTO exercise) 
        {
            if (ModelState.IsValid != null)
            {
                trainingExerciseService.AddExercise(exercise);
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");


        }

        [HttpGet]
        public IActionResult EditExercise(int id)
        {
            TrainingExerciseDTO exercise = trainingExerciseService.GetNeedExercise(id);
            return View(exercise);  

        }

        [HttpPost]
        public IActionResult EditExercise(TrainingExerciseDTO exercise)
        {
            trainingExerciseService.UpdateExercise(exercise);
            return RedirectToAction("Index");

        }

        [HttpGet]
        public IActionResult DeleteExercise(int id)
        {
            trainingExerciseService.DeleteExercise(id);
            return RedirectToAction("Index");
        }
    }
}
