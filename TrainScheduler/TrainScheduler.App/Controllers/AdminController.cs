﻿using Microsoft.AspNetCore.Mvc;
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
        private readonly IDestinationService _destinationService;
        private readonly IScheduleService _scheduleService;

        public AdminController(
            IStopService stopService,
            ITrainService trainService,
            IDestinationService destinationService,
            IScheduleService scheduleService)
        {
            _stopService = stopService ?? throw new ArgumentNullException(nameof(stopService));
            _trainService = trainService ?? throw new ArgumentNullException(nameof(trainService));
            _destinationService = destinationService ?? throw new ArgumentNullException(nameof(destinationService));
            _scheduleService = scheduleService ?? throw new ArgumentNullException(nameof(scheduleService));
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

        #region Trains

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

        #endregion

        #region Destinations

        [HttpGet]
        public async Task<IActionResult> Destinations()
        {
            var destinations = await _destinationService.GetAllAsync();
            return View(destinations);
        }

        [HttpGet]
        public async Task<IActionResult> CreateDestination()
        {
            var model = new CreateDestinationModel()
            {
                Stops = await _stopService.GetAllAsync(),
                Trains = await _trainService.GetAllAsync()
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateDestination(CreateDestinationModel model)
        {
            if (ModelState.IsValid)
            {
                await _destinationService.CreateAsync(model);
                return RedirectToAction("Destinations");
            }

            model.Stops = await _stopService.GetAllAsync();
            model.Trains = await _trainService.GetAllAsync();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteDestination(int id)
        {
            await _destinationService.DeleteAsync(id);
            return RedirectToAction("Destinations");
        }

        [HttpGet]
        public async Task<IActionResult> EditDestination(int id)
        {
            var destination = await _destinationService.GetByIdAsync(id);
            if (destination == null)
            {
                return NotFound();
            }

            var model = new UpdateDestinationModel() 
            { 
                Id = destination.Id, 
                Name = destination.Name,
                DepartureId = destination.DepartureId,
                ArrivalId = destination.ArrivalId,
                TrainId = destination.TrainId,
                Stops = await _stopService.GetAllAsync(),
                Trains = await _trainService.GetAllAsync()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditDestination(UpdateDestinationModel model)
        {
            if (ModelState.IsValid)
            {
                await _destinationService.UpdateAsync(model);
                return RedirectToAction("Destinations");
            }

            model.Stops = await _stopService.GetAllAsync();
            model.Trains = await _trainService.GetAllAsync();

            return View(model);
        }

        #endregion

        #region Schedules

        [HttpGet]
        public async Task<IActionResult> Schedules()
        {
            var schedules = await _scheduleService.GetAllAsync();
            return View(schedules);
        }

        [HttpGet]
        public async Task<IActionResult> CreateSchedule()
        {
            var model = new CreateScheduleModel()
            {
                Destinations = await _destinationService.GetAllAsync(), 
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSchedule(CreateScheduleModel model)
        {
            if (ModelState.IsValid)
            {
                await _scheduleService.CreateAsync(model);
                return RedirectToAction("Schedules");
            }

            model.Destinations = await _destinationService.GetAllAsync();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteSchedule(int id)
        {
            await _scheduleService.DeleteAsync(id);
            return RedirectToAction("Schedules");
        }

        [HttpGet]
        public async Task<IActionResult> EditSchedule(int id)
        {
            var schedule = await _scheduleService.GetByIdAsync(id);
            if (schedule == null)
            {
                return NotFound();
            }

            var model = new UpdateScheduleModel()
            {
                Id = schedule.Id,  
                DestinationId = schedule.DestinationId,
                ArrivalTime = schedule.ArrivalTime,
                DepartureTime = schedule.DepartureTime,
                Destinations = await _destinationService.GetAllAsync()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditSchedule(UpdateScheduleModel model)
        {
            if (ModelState.IsValid)
            {
                await _scheduleService.UpdateAsync(model);
                return RedirectToAction("Schedules");
            }

            model.Destinations = await _destinationService.GetAllAsync();
            return View(model);
        }

        #endregion
    }
}
