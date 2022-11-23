using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainScheduler.Model.Interfaces;
using TrainScheduler.Model.ViewModels;
using TrainScheduler.Core.Helpers;
using TrainScheduler.App.Constants;

namespace TrainScheduler.App.Controllers
{
    public class ScheduleController : Controller
    {
        private readonly IScheduleService _scheduleService;
        private readonly IDestinationService _destinationService;

        public ScheduleController(
            IScheduleService scheduleService,
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

        [HttpGet]
        public async Task<IActionResult> Schedule()
        {
            var model = new ScheduleModel()
            {
                Date = DateTime.Now,
                Schedules = await _scheduleService.GetByDateAsync(DateTime.Now)
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Schedule(DateTime date)
        {
            var model = new ScheduleModel()
            {
                Date = date,
                Schedules = await _scheduleService.GetByDateAsync(date)
            };

            return View(model);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> BookTicket([FromBody] BookTicketModel model)
        {
            if (HttpContext.Session.TryGet<List<int>>(CacheConstants.BookedSchedules, out var bookedSchedules))
            {
                bookedSchedules.Add(model.ScheduleId);
            }
            else
            {
                bookedSchedules = new List<int>() { model.ScheduleId };
            }

            var scheduleTicketsInCart = bookedSchedules.Where(s => s == model.ScheduleId).Count();

            var havAvailableTickets = await _scheduleService.HasAvaiableTickets(model.ScheduleId, scheduleTicketsInCart);

            if (havAvailableTickets)
            {
                HttpContext.Session.Set(CacheConstants.BookedSchedules, bookedSchedules);
                return Ok(bookedSchedules.Count);
            }

            return Ok(-1);
        }


        [Authorize]
        [HttpPost]
        public IActionResult RemoveTicket([FromBody] BookTicketModel model)
        {
            if (HttpContext.Session.TryGet<List<int>>(CacheConstants.BookedSchedules, out var bookedSchedules))
            {
                bookedSchedules.Remove(model.ScheduleId);
            }
            else
            {
                bookedSchedules = new List<int>();
            }

            HttpContext.Session.Set(CacheConstants.BookedSchedules, bookedSchedules);
            return Ok(bookedSchedules.Count);
        }
    }
}
