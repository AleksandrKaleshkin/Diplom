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
                Exercises = exerciseService.GetExercises().ToList()
        };
            return View(viewModel);
        }

        public IActionResult Exercise(int id)
        {
            ExerciseDTO exercise = exerciseService.GetExercise(id);
            return View(exercise);
        }

        
        [HttpGet]
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
        public IActionResult CreateExercise(AddEditExerciseViewModel exercise, IFormFileCollection uploadedNameImage)
        {
            if (uploadedNameImage.Count != 0 )
            {
                AddEditPicture(exercise, uploadedNameImage);            
                exerciseService.AddExercise(exercise.ExerciseDTO);
                return RedirectToAction("Index");
            }
            else
            {
                return View(exercise);
            }
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            ExerciseDTO exercise = exerciseService.GetExercise(id);
            string path = _appEnvironment.WebRootPath + exercise.PathImage1;
            string path2 = _appEnvironment.WebRootPath + exercise.PathImage2;
            string path3 = _appEnvironment.WebRootPath + exercise.PathImage3;
            System.IO.File.Delete(path);
            System.IO.File.Delete(path2);
            System.IO.File.Delete(path3);
            exerciseService.DeleteExercise(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult EditExercise(int id) 
        {
            if (exerciseService.GetExercise(id) != null)
            {
                AddEditExerciseViewModel exercise = new AddEditExerciseViewModel
                {
                    TypeOfMysc = new TypeOfMyscList(exerciseService.GetTypeOfMuscles().ToList()),
                    ExerciseDTO = exerciseService.GetExercise(id)

                };
                return View(exercise);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult EditExercise(AddEditExerciseViewModel exercise, IFormFileCollection uploadedNameImage, int type)
        {
            if (uploadedNameImage != null)
            {
                AddEditPicture(exercise, uploadedNameImage);
                exerciseService.UpdateExercise(exercise.ExerciseDTO,type);
                return RedirectToAction("Index");
            }
            else
            {
                return View(exercise);
            }
        }

        public void AddEditPicture (AddEditExerciseViewModel exercise, IFormFileCollection uploadedNameImage)
        {
            int numImage = 1;
            foreach (var fileimage in uploadedNameImage)
            {
                string PathImage = "/files/image/ExerciseImage/" + fileimage.FileName;
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + PathImage, FileMode.Create))
                {
                    fileimage.CopyTo(fileStream);
                }
                if (numImage == 1)
                {
                    exercise.ExerciseDTO.NameImage1 = fileimage.FileName;
                    exercise.ExerciseDTO.PathImage1 = PathImage;
                }
                else if (numImage == 2)
                {
                    exercise.ExerciseDTO.NameImage2 = fileimage.FileName;
                    exercise.ExerciseDTO.PathImage2 = PathImage;
                }
                else if (numImage == 3)
                {
                    exercise.ExerciseDTO.NameImage3 = fileimage.FileName;
                    exercise.ExerciseDTO.PathImage3 = PathImage;
                }
                numImage++;
            }
        }
    }
}
