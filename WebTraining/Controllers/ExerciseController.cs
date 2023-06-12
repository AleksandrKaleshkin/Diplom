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
            exerciseService = serv;
            _appEnvironment = appEnvironment;
        }

        public IActionResult Index()
        {
            ExerciseViewModel viewModel = new ExerciseViewModel()
            {
                Exercises = exerciseService.GetExercises().ToList(),
                Image = exerciseService.GetImages().ToList()
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
        public async Task<IActionResult> CreateExercise(AddEditExerciseViewModel exercise, IFormFileCollection uploadedNameImage)
        {
            if (ModelState.IsValid)
            {
                if (uploadedNameImage.Count == 3)
                {
                    exerciseService.AddExercise(exercise.ExerciseDTO);
                    await AddImage(exercise.ExerciseDTO, uploadedNameImage);
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError("ImageExercise", "Не хватает картинки. Должно быть загружено 3 картинки");
            }
            exercise.TypeOfMysc = new TypeOfMyscList(exerciseService.GetTypeOfMuscles().ToList());
            return View(exercise);

        }
        [HttpGet]
        [Authorize(Roles = "coach, admin")]

        public IActionResult Delete(int id)
        {
            ExerciseDTO exercise = exerciseService.GetExercise(id);
            if (exercise != null)
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
                    ImageExercise = exerciseService.GetImageExercises(exercise).ToList(),
                    TypeOfMysc = new TypeOfMyscList(exerciseService.GetTypeOfMuscles().ToList()),
                    ExerciseDTO = exercise
                };
                return View(model);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        [Authorize(Roles = "coach, admin")]
        public async Task<IActionResult> EditExerciseAsync(AddEditExerciseViewModel exercise, IFormFile? uploadedNameImage1, IFormFile? uploadedNameImage2, IFormFile? uploadedNameImage3)
        {
            if (ModelState.IsValid)
            {
                exercise.ImageExercise = exerciseService.GetImageExercises(exercise.ExerciseDTO);
                exerciseService.UpdateExercise(exercise.ExerciseDTO);
                await EditImageAsync(exercise.ExerciseDTO, uploadedNameImage1, uploadedNameImage2, uploadedNameImage3);
                return RedirectToAction("Index");
            }
            else
            {
                exercise.TypeOfMysc = new TypeOfMyscList(exerciseService.GetTypeOfMuscles().ToList());
                return View(exercise);
            }
        }

        private async Task EditImageAsync(ExerciseDTO exercise, IFormFile uploadedNameImage1, IFormFile uploadedNameImage2, IFormFile uploadedNameImage3)
        {
            List<ImageExerciseDTO> image = new List<ImageExerciseDTO>(3);
            image.AddRange(exerciseService.GetImageExercises(exercise));
            if (uploadedNameImage1 != null)
            {
                string path = "/Files/" + uploadedNameImage1.FileName;
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await uploadedNameImage1.CopyToAsync(fileStream);
                }
                if (image[0]!=null)
                {
                    image[0].PathImage= path;
                    image[0].NameImage= uploadedNameImage1.FileName;
                    exerciseService.UpdatePicture(image[0]);
                }
                else
                {
                    ImageExerciseDTO newimage = new ImageExerciseDTO()
                    {
                        PathImage = path,
                        ExerciseID = exercise.ID,
                        NameImage = uploadedNameImage1.FileName
                    };
                    exerciseService.AddPicture(newimage);
                }
            }
            if (uploadedNameImage2 != null)
            {
                string path = "/Files/" + uploadedNameImage2.FileName;
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await uploadedNameImage2.CopyToAsync(fileStream);
                }
                if (image[1] != null)
                {

                    image[1].PathImage = path;
                    image[1].NameImage = uploadedNameImage2.FileName;
                    exerciseService.UpdatePicture(image[1]);
                }
                else
                {
                    ImageExerciseDTO newimage = new ImageExerciseDTO()
                    {
                        PathImage = path,
                        ExerciseID = exercise.ID,
                        NameImage = uploadedNameImage2.FileName
                    };
                    exerciseService.AddPicture(newimage);
                }
            }
            if (uploadedNameImage3 != null)
            {
                string path = "/Files/" + uploadedNameImage3.FileName;
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await uploadedNameImage3.CopyToAsync(fileStream);
                }
                if (image.Count==3)
                {
                    image[2].PathImage = path;
                    image[2].NameImage = uploadedNameImage3.FileName;
                    exerciseService.UpdatePicture(image[2]);
                }
                else
                {
                    ImageExerciseDTO newimage = new ImageExerciseDTO()
                    {
                        PathImage = path,
                        ExerciseID = exercise.ID,
                        NameImage = uploadedNameImage3.FileName
                    };
                    exerciseService.AddPicture(newimage);
                }
            }
        }

        private async Task AddImage(ExerciseDTO exercise, IFormFileCollection uploadedNameImage)
        {
            foreach (var item in uploadedNameImage)
            {
                string path = "/Files/" + item.FileName;
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await item.CopyToAsync(fileStream);
                }
                ImageExerciseDTO newimage = new ImageExerciseDTO()
                {
                    ExerciseID = exerciseService.GetExercises().Last().ID,
                    PathImage = path,
                    NameImage = item.FileName
                };
                exerciseService.AddPicture(newimage);
            }
        }

        private void DeletePicture(ExerciseDTO exercise)
        {
            var image = exerciseService.GetImageExercises(exercise);

            foreach (var item in image)
            {
                item.PathImage = _appEnvironment.WebRootPath + item.PathImage;
                if (item.PathImage != _appEnvironment.WebRootPath)
                {
                    System.IO.File.Delete(item.PathImage);
                    exerciseService.DeleteImage(item.ID);
                }
            }
        }
    }
}



