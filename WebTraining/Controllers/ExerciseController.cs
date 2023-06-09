using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebTraining.Core.DTO;
using WebTraining.Core.Interfaces;
using WebTraining.Core.Models;
using WebTraining.Models;


namespace WebTraining.Controllers
{
    [Authorize]
    public class ExerciseController : Controller
    {
        readonly IExerciseService exerciseService;
        readonly IWebHostEnvironment _appEnvironment;
        public ExerciseController(IExerciseService serv, IWebHostEnvironment appEnvironment)
        {
            exerciseService= serv;
            _appEnvironment= appEnvironment;            
        }

        public IActionResult Index()
        {
            ExerciseViewModel viewModel = new ExerciseViewModel()
            {
                Exercises = exerciseService.GetExercises().ToList(),
                Image=exerciseService.GetImages().ToList()
            };
            return View(viewModel);
        }

        public IActionResult Exercise(int id)
        {
            ExerciseEViewModel model = new ExerciseEViewModel
            {
                Exercise = exerciseService.GetExercise(id),
                Image = exerciseService.GetImageExercises(exerciseService.GetExercise(id)).ToList()
            };
            return View(model);
        }

        
        [HttpGet]
        [Authorize(Roles = "coach, admin")]
        public IActionResult CreateExercise()
        {
            AddEditExerciseViewModel exercise = new AddEditExerciseViewModel
            {
                TypeOfMysc = new TypeOfMyscList(exerciseService.GetTypeOfMuscles().ToList()),
                ExerciseDTO = new ExerciseDTO() { }               
            };
            return View(exercise);
        }

        [HttpPost]
        [Authorize(Roles = "coach, admin")]
        public IActionResult CreateExercise(AddEditExerciseViewModel exercise, IFormFileCollection uploadedNameImage)
        {
            if (ModelState.IsValid)
            {        
                exerciseService.AddExercise(exercise.ExerciseDTO);                
                AddEditPicture(exerciseService.GetExercises().Last(), uploadedNameImage);
                return RedirectToAction("Index");
            }
            else
            {
                exercise.TypeOfMysc = new TypeOfMyscList(exerciseService.GetTypeOfMuscles().ToList());
                return View(exercise);
            }
        }
        [HttpGet]
        [Authorize(Roles = "coach, admin")]
        public IActionResult Delete(int id)
        {
            ExerciseDTO exercise = exerciseService.GetExercise(id);
            if (exercise!=null)
            {
                DeletePicture(exercise);
                exerciseService.DeleteExercise(id);
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize(Roles = "coach, admin")]
        public IActionResult EditExercise(int id) 
        {
            var exercise = exerciseService.GetExercise(id);
            if (exercise != null)
            {
                AddEditExerciseViewModel model = new AddEditExerciseViewModel
                {
                    TypeOfMysc = new TypeOfMyscList(exerciseService.GetTypeOfMuscles().ToList()),
                    ExerciseDTO = exercise

                };
                return View(model);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        [Authorize(Roles = "coach, admin")]
        public IActionResult EditExercise(AddEditExerciseViewModel exercise, IFormFileCollection uploadedNameImage, int type)
        {
            if (ModelState.IsValid)
            {
                exerciseService.UpdateExercise(exercise.ExerciseDTO);
                return RedirectToAction("Index");
            }
            else
            {
                exercise.TypeOfMysc = new TypeOfMyscList(exerciseService.GetTypeOfMuscles().ToList());
                return View(exercise);
            }
        }

        private void AddEditPicture (ExerciseDTO exercise, IFormFileCollection uploadedNameImage)
        {
            int numImage = 1;
            foreach (var fileimage in uploadedNameImage)
            {
                string PathImage = "/files/image/ExerciseImage/" + fileimage.FileName;
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + PathImage, FileMode.Create))
                {
                    fileimage.CopyTo(fileStream);
                }
                ImageExerciseDTO imageExercise = new ImageExerciseDTO
                {
                    NameImage = fileimage.FileName,
                    PathImage = PathImage,
                    ExerciseID = exercise.ID
                };
                exerciseService.AddPicture(imageExercise);

                numImage++;
            }
        }

        private void DeletePicture(ExerciseDTO exercise)
        {
            var allimage = exerciseService.GetImages();
            var imageExercises = exerciseService.GetImageExercises(exercise);       
            foreach (var item in imageExercises)
            {
                var allneedimage = allimage.Where(x=>x.PathImage==item.PathImage).Where(x=>x.ExerciseID!=item.ExerciseID).ToList();
                item.PathImage = _appEnvironment.WebRootPath + item.PathImage;
                if (item.PathImage != _appEnvironment.WebRootPath&&allneedimage.Count==0 )
                {
                    System.IO.File.Delete(item.PathImage);
                    exerciseService.DeleteImage(item.ID);
                }
            }
        }
    }
}
