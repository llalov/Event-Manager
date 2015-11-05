using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EventManager.Web.Models;

namespace EventManager.Web.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            var events = this.db
                .Events
                .OrderBy(e => e.StartDateTime)
                .Where(e => e.isPublic)
                .Select(e => new EventViewModel()
                {
                    Id = e.Id,
                    Title = e.Title,
                    StartDateTime = e.StartDateTime,
                    Duration = e.Duration,
                    Author = e.Author,
                    Location = e.Location
                });

            var upcomingEvents = events.Where(e => e.StartDateTime > DateTime.Now);

            var passedEvents = events.Where(e => e.StartDateTime <= DateTime.Now);

            return View(new UpcomingPassedEventsViewModel()
            {
                UpcomingEvents = upcomingEvents,
                PassedEvents = passedEvents
            });
        }
    }
}