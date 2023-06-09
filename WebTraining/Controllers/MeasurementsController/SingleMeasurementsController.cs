using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebTraining.Core.DTO.MeasurementsDTO;
using WebTraining.Core.Interfaces.IMeasurements;
using WebTraining.Core.Services.MeasurementsService;
using WebTraining.DB.Models;
using WebTraining.Models.Measurements;

namespace WebTraining.Controllers.MeasurementsController
{
    public class SingleMeasurementsController : Controller
    {
        private readonly ISingleMeasurementstService singleservice;
        readonly UserManager<User> userManager;

        public SingleMeasurementsController(ISingleMeasurementstService singleservice, UserManager<User> userManager)
        {
            this.singleservice = singleservice;
            this.userManager= userManager;
        }

        public async Task<IActionResult> ButtocksViewMeasurements(string type = "Ягодицы")
        {
            User user = await GetUser();
            ButtocksMeasViewModel model = new ButtocksMeasViewModel()
            {
                Measurements = singleservice.GetNeedMeasurements(user, singleservice.GetTypeOfMuscle(type).ID).ToList()
            };
            return View(model);
        }

        public async Task<IActionResult> WaistViewMeasurements(string type = "Талия")
        {
            User user = await GetUser();
            WaistMeasViewModel model = new WaistMeasViewModel()
            {
                Measurements = singleservice.GetNeedMeasurements(user, singleservice.GetTypeOfMuscle(type).ID).ToList()
            };
            return View(model);
        }

        public async Task<IActionResult> HeightViewMeasurements(string type = "Рост")
        {
            User user = await GetUser();
            HeightMeasViewModel model = new HeightMeasViewModel()
            {
                Measurements = singleservice.GetNeedMeasurements(user, singleservice.GetTypeOfMuscle(type).ID).ToList()
            };
            return View(model);
        }

        public async Task<IActionResult> WeightViewMeasurements(string type = "Вес")
        {
            User user = await GetUser();
            WeightMeasViewModel model = new WeightMeasViewModel()
            {
                Measurements = singleservice.GetNeedMeasurements(user, singleservice.GetTypeOfMuscle(type).ID).ToList()
            };
            return View(model);
        }

        public async Task<IActionResult> BreastViewMeasurements(string type = "Грудь")
        {
            User user = await GetUser();
            BreastMeasViewModel model = new BreastMeasViewModel()
            {
                Measurements = singleservice.GetNeedMeasurements(user, singleservice.GetTypeOfMuscle(type).ID).ToList()
            };
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Add(string id)
        {
            User user = await GetUser();
            AddSingleMeasViewModel meas = new AddSingleMeasViewModel
            {
                Measurementst = new SingleMeasurementstDTO()
                {
                    Date = DateTime.Now,
                    UserId = user.Id,
                    MuscleId = singleservice.GetTypeOfMuscle(id).ID
                }
            };
            return View(meas);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddSingleMeasViewModel meas)
        {
            User user = await GetUser();
            if (ModelState.IsValid)
            {
                singleservice.AddMeasurement(meas.Measurementst, user);
                return RedirectToAction("Index", "Account");
            }
            else
            {
                return View(meas);
            }
        }

        public IActionResult Delete(int id) 
        {
            singleservice.DeleteMeasurement(id);
            return RedirectToAction("Index", "Account");
        }

        [HttpGet]
        public IActionResult Edit(int id) 
        {
            SingleMeasurementstDTO meas = singleservice.GetMeasurement(id);
            if (meas != null)
            {
                return View(meas);
            }
            return RedirectToAction("Index", "Account");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SingleMeasurementstDTO meas) 
        {
            User user = await GetUser();
            if (ModelState.IsValid)
            {
                singleservice.UpdateMeasurement(meas, user);
                return RedirectToAction("Index", "Account");
            }
            return View(meas);
        }

        private async Task<User> GetUser()
        {
            return await userManager.FindByNameAsync(User.Identity.Name);
        }
    }
}
