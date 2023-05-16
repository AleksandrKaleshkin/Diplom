using Microsoft.AspNetCore.Mvc;
using WebTraining.Core.DTO;
using WebTraining.Core.Interfaces;
using WebTraining.Core.Models;
using WebTraining.DB.Models;
using WebTraining.Models;

namespace WebTraining.Controllers
{
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
            IEnumerable<ExerciseDTO> exercise = exerciseService.GetExercises().ToList();
            ExerciseViewModel viewModel = new ExerciseViewModel()
            {
                Exercises = exercise
            };
            return View(viewModel);
        }

        
        [HttpGet]
        public IActionResult CreateExercise()
        {
            IEnumerable<TypeOfMuscle> type = exerciseService.GetTypeOfMuscles();
            AddEditExerciseViewModel exercise = new AddEditExerciseViewModel
            {
                TypeOfMysc = new TypeOfMyscList(type.ToList()),
                ExerciseDTO = new ExerciseDTO() { }
                
            };
            return View(exercise);
        }

        [HttpPost]
        public IActionResult CreateExercise(AddEditExerciseViewModel exercise, IFormFileCollection uploadedNameImage, int type)
        {
            if (uploadedNameImage != null )
            {
                int num = 1;

                foreach (var fileimage in uploadedNameImage)
                {
                    string PathImage = "/files/image/" + fileimage.FileName;

                    using (var fileStream = new FileStream(_appEnvironment.WebRootPath + PathImage, FileMode.Create))
                    {
                        fileimage.CopyTo(fileStream);
                    }
                    if (num==1)
                    {
                        exercise.ExerciseDTO.NameImage1 = fileimage.Name;
                        exercise.ExerciseDTO.PathImage1 = PathImage;
                    }
                    else if (num==2)
                    {
                        exercise.ExerciseDTO.NameImage2 = fileimage.Name;
                        exercise.ExerciseDTO.PathImage2 = PathImage;
                    }
                    else if (num == 3)
                    {
                        exercise.ExerciseDTO.NameImage3 = fileimage.Name;
                        exercise.ExerciseDTO.PathImage3 = PathImage;
                    }
                    num++;
                }
                
                exerciseService.AddExercise(exercise.ExerciseDTO, type);
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
            exerciseService.DeleteExercise(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult EditExercise(int id) 
        {
            if (exerciseService.GetExercise(id) != null)
            {
                return View(exerciseService.GetExercise(id));
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult EditExercise(ExerciseDTO exercise, IFormFileCollection uploadedNameImage)
        {
            if (uploadedNameImage != null)
            {
                int numImage = 1;

                foreach (var fileimage in uploadedNameImage)
                {
                    string PathImage = "/files/image/" + fileimage.FileName;

                    using (var fileStream = new FileStream(_appEnvironment.WebRootPath + PathImage, FileMode.Create))
                    {
                        fileimage.CopyTo(fileStream);
                    }
                    if (numImage == 1)
                    {
                        exercise.NameImage1 = fileimage.Name;
                        exercise.PathImage1 = PathImage;
                    }
                    else if (numImage == 2)
                    {
                        exercise.NameImage2 = fileimage.Name;
                        exercise.PathImage2 = PathImage;
                    }
                    else if (numImage == 3)
                    {
                        exercise.NameImage3 = fileimage.Name;
                        exercise.PathImage3 = PathImage;
                    }
                    numImage++;
                }

                exerciseService.UpdateExercise(exercise);
                return RedirectToAction("Index");
            }
            else
            {
                return View(exercise);
            }
        }
    }
}
