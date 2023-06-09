using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebTraining.Core.DTO.MeasurementsDTO;
using WebTraining.Core.Interfaces.IMeasurements;
using WebTraining.DB.Models;
using WebTraining.Models.Measurements;

namespace WebTraining.Controllers.MeasurementsController
{
    [Authorize]
    public class DoubleMeasurementsController : Controller
    {
        private readonly IDoubleMeasurementsService doubleMeasurementsService;
        readonly UserManager<User> userManager;


        public DoubleMeasurementsController(IDoubleMeasurementsService doubleMeasurementsService, UserManager<User> userManager)
        {
            this.doubleMeasurementsService = doubleMeasurementsService;
            this.userManager = userManager;
        }

        public async Task<IActionResult> ShinsViewMeasurements(string type = "Голень")
        {
            User user = await GetUser();
            ShinsMeasViewModel model = new ShinsMeasViewModel()
            {
                Measurements = doubleMeasurementsService.GetNeedMeasurements(user, doubleMeasurementsService.GetTypeOfMuscle(type).ID).ToList()
            };
            return View(model);
        }

        public async Task<IActionResult> ForearmViewMeasurements(string type = "Предплечье")
        {
            User user = await GetUser();
            ForearmMeasViewModel model = new ForearmMeasViewModel()
            {
                Measurements = doubleMeasurementsService.GetNeedMeasurements(user, doubleMeasurementsService.GetTypeOfMuscle(type).ID).ToList()
            };
            return View(model);
        }

        public async Task<IActionResult> LegsViewMeasurements(string type = "Ноги")
        {
            User user = await GetUser();
            LegsMeasViewModel model = new LegsMeasViewModel()
            {
                Measurements = doubleMeasurementsService.GetNeedMeasurements(user, doubleMeasurementsService.GetTypeOfMuscle(type).ID).ToList()
            };
            return View(model);
        }


        public async Task<IActionResult> ViewBitcepsMeasurements(string type="Битцепс")        
        {
            User user = await GetUser();
            BitcepsMeasurementsViewModel model = new BitcepsMeasurementsViewModel()
            {             
                Measurements = doubleMeasurementsService.GetNeedMeasurements(user, doubleMeasurementsService.GetTypeOfMuscle(type).ID).ToList()

            };
            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> Add(string id)
        {
            User user = await GetUser();
            AddDoubleMeasViewModel meas = new AddDoubleMeasViewModel
            {
                Measurementst= new DoubleMeasurementsDTO() 
                {
                    Date = DateTime.Now,
                    UserId = user.Id,
                    MuscleId = doubleMeasurementsService.GetTypeOfMuscle(id).ID
                }
            };
            return View(meas);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddDoubleMeasViewModel meas)
        {
            User user = await GetUser();
            if (ModelState.IsValid)
            {
                doubleMeasurementsService.AddMeasurement(meas.Measurementst, user);
                return RedirectToAction("Index", "Account");
            }
            else
            {
                return View(meas);
            }
        }

        public IActionResult Delete(int id) 
        {
            doubleMeasurementsService.DeleteMeasurement(id);
            return RedirectToAction("Index", "Account");
        }

        [HttpGet]
        public IActionResult Edit(int id) 
        {
            DoubleMeasurementsDTO meas = doubleMeasurementsService.GetMeasurement(id);
            if (meas != null)
            {
                return View(meas);
            }
            return RedirectToAction("Index", "Account");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(DoubleMeasurementsDTO meas) 
        {
            User user = await GetUser();
            if (ModelState.IsValid)
            {
                doubleMeasurementsService.UpdateMeasurement(meas, user);
                return RedirectToAction("Index", "Account");
            }
            return View (meas);
        }

        private async Task<User> GetUser()
        {
            return await userManager.FindByNameAsync(User.Identity.Name);
        }
    }
}
