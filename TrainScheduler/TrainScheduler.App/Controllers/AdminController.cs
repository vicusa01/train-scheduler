using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainScheduler.Model.Interfaces;
using TrainScheduler.Model.ViewModels;

namespace TrainScheduler.App.Controllers
{
    public class AdminController : Controller
    {
        private readonly IStopService _stopService;
        private readonly ITrainService _trainService;

        public AdminController(
            IStopService stopService,
            ITrainService trainService)
        {
            _stopService = stopService ?? throw new ArgumentNullException(nameof(stopService));
            _trainService = trainService ?? throw new ArgumentNullException(nameof(trainService));
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        #region Stops

        [HttpGet]
        public async Task<IActionResult> Stops()
        {
            var stops = await _stopService.GetAllAsync();
            return View(stops);
        }

        [HttpGet]
        public IActionResult CreateStop()
        {          
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateStop(CreateStopModel model)
        {
            if (ModelState.IsValid)
            {
                await _stopService.CreateAsync(model);
                return RedirectToAction("Stops");
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteStop(int id)
        {
            await _stopService.DeleteAsync(id);
            return RedirectToAction("Stops");
        }

        [HttpGet]
        public async Task<IActionResult> EditStop(int id)
        {
            var stop = await _stopService.GetByIdAsync(id);
            if (stop == null)
            {
                return NotFound();
            }

            var model = new UpdateStopModel() { Id = stop.Id, Name = stop.Name };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditStop(UpdateStopModel model)
        {
            if (ModelState.IsValid)
            {
                await _stopService.UpdateAsync(model);
                return RedirectToAction("Stops");
            }
            return View(model);
        }

        #endregion

        [HttpGet]
        public async Task<IActionResult> Trains()
        {
            var trains = await _trainService.GetAllAsync();
            return View(trains);
        }

        [HttpGet]
        public IActionResult CreateTrain()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateTrain(CreateTrainModel model)
        {
            if (ModelState.IsValid)
            {
                await _trainService.CreateAsync(model);
                return RedirectToAction("Trains");
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteTrain(int id)
        {
            await _trainService.DeleteAsync(id);
            return RedirectToAction("Trains");
        }

        [HttpGet]
        public async Task<IActionResult> EditTrain(int id)
        {
            var train = await _trainService.GetByIdAsync(id);
            if (train == null)
            {
                return NotFound();
            }

            var model = new UpdateTrainModel() { Id = train.Id, Number = train.Number, TrainType = train.TrainType, Seats = train.Seats };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditTrain(UpdateTrainModel model)
        {
            if (ModelState.IsValid)
            {
                await _trainService.UpdateAsync(model);
                return RedirectToAction("Trains");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Destinations()
        {
            return View();
        }
    }
}
