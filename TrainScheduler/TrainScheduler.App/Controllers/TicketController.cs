﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainScheduler.Model.Interfaces;
using TrainScheduler.Model.ViewModels;

namespace TrainScheduler.App.Controllers
{
    public class TicketController : Controller
    {
        private readonly ITicketService _ticketService;
        private readonly IAccountService _accountService;
        private readonly IDestinationService _destinationService;

        public TicketController(
            ITicketService ticketService,
            IAccountService accountService,
            IDestinationService destinationService)
        {
            _ticketService = ticketService ?? throw new ArgumentNullException(nameof(ticketService));
            _accountService = accountService ?? throw new ArgumentNullException(nameof(accountService));
            _destinationService = destinationService ?? throw new ArgumentNullException(nameof(destinationService));
        }

        [HttpGet]
        public async Task<IActionResult> AdminBooking()
        {
            var model = new AdminBookingModel()
            {
                Users = await _accountService.GetAllUsersAsync()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AdminBooking(string userId, string fio)
        {
            var model = new AdminBookingModel()
            {
                UserId = userId,
                FIO = fio,
                Users = await _accountService.GetAllUsersAsync(),
                Tickets = await _ticketService.FindTicketsAsync(userId, fio)
            };

            return View(model);
        }

        [HttpGet]
        public IActionResult TicketsReport()
        {
            var model = new TicketsReportModel()
            {
                From = DateTime.Now,
                To = DateTime.Now.AddDays(1)
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> TicketsReport(DateTime from, DateTime to)
        {
            var model = new TicketsReportModel()
            {
                From = from,
                To = to,
                Report = await _ticketService.GetTicketsReportAsync(from, to)
            };

            return View(model);
        }
    }
}
