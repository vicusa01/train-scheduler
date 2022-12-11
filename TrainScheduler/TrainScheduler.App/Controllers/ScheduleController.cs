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
        private readonly IStopService _stopService;

        public ScheduleController(
            IScheduleService scheduleService,
            IStopService stopService)
        {
            _scheduleService = scheduleService ?? throw new ArgumentNullException(nameof(scheduleService));
            _stopService = stopService ?? throw new ArgumentNullException(nameof(stopService));
        }

        [HttpGet]
        public async Task<IActionResult> AvailableSeats()
        {
            var model = new AvailableSeatsModel()
            {
                Stops = await _stopService.GetAllAsync(),
                Date = DateTime.Now
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AvailableSeats(int departureStopId, int arrivalStopId, DateTime date)
        {
            var model = new AvailableSeatsModel()
            {
                DepartureStopId = departureStopId,
                ArrivalStopId = arrivalStopId,
                Date = date,
                Stops = await _stopService.GetAllAsync(),
                AvailableSeats = await _scheduleService.GetAvailableSeatsAsync(departureStopId, arrivalStopId, date)
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
