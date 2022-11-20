using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainScheduler.Model.Interfaces;
using TrainScheduler.Model.ViewModels;

namespace TrainScheduler.App.Controllers
{
    public class ScheduleController : Controller
    {
        private readonly IScheduleService _scheduleService;
        private readonly IDestinationService _destinationService;

        public ScheduleController(
            IScheduleService scheduleService,
            IAccountService accountService,
            IDestinationService destinationService)
        {
            _scheduleService = scheduleService ?? throw new ArgumentNullException(nameof(scheduleService));
            _destinationService = destinationService ?? throw new ArgumentNullException(nameof(destinationService));
        }

        [HttpGet]
        public async Task<IActionResult> AvailableSeats()
        {
            var model = new AvailableSeatsModel()
            {
                Destinations = await _destinationService.GetAllAsync(),
                Date = DateTime.Now
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AvailableSeats(int destinationId, DateTime date)
        {
            var model = new AvailableSeatsModel()
            {
                DestinationId = destinationId,
                Date = date,
                Destinations = await _destinationService.GetAllAsync(),
                AvailableSeats = await _scheduleService.GetAvailableSeatsAsync(destinationId, date)
            };

            return View(model);
        }
    }
}
