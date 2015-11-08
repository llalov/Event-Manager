﻿using System;
using System.Linq;
using System.Web.Mvc;
using EventManager.Models;
using EventManager.Web.Extensions;
using EventManager.Web.Models;
using Microsoft.AspNet.Identity;
/*using Events.Web.Extensions;*/

namespace EventManager.Web.Controllers
{
    [Authorize]
    public class EventsController : BaseController
    {
        // GET: Events/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EventInputModel model)
        {
            if (model != null && this.ModelState.IsValid)
            {
                var newEvent = new Event()
                {
                    AuthorId = this.User.Identity.GetUserId(),
                    Title = model.Title,
                    StartDateTime = model.StartDateTime,
                    Duration = model.Duration,
                    Description = model.Description,
                    Location = model.Location,
                    isPublic = model.IsPublic
                };
                this.db.Events.Add(newEvent);
                this.db.SaveChanges();
                this.AddNotification("Event created.", NotificationType.INFO);
                //Display notification message "Event created"
                return this.RedirectToAction("My");
            }
            return this.View(model);

        }
        //GET: Events/My
        public ActionResult My()
        {
            string currentUserId = User.Identity.GetUserId();
            var events = this.db.Events
                .Where(e => e.AuthorId == currentUserId)
                .OrderBy(e => e.StartDateTime)
                .Select(EventViewModel.ViewModel);

            var upcomingEvents = events.Where(e => e.StartDateTime > DateTime.Now);
            var passedEvents = events.Where(e => e.StartDateTime <= DateTime.Now);

            return View(new UpcomingPassedEventsViewModel()
            {
                UpcomingEvents = upcomingEvents,
                PassedEvents = passedEvents
            });
        }

        private Event LoadEvent(int id)
        {
            var currentUserId = this.User.Identity.GetUserId();
            var isAdmin = this.IsAdmin();

            var eventToEdit = this.db
                .Events
                .Where(e => e.Id == id)
                .FirstOrDefault(e => e.AuthorId == currentUserId || isAdmin);
            return eventToEdit;
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var eventToEdit = this.LoadEvent(id);
            if (eventToEdit == null)
            {
                this.AddNotification("Cannot edit event #" + id, NotificationType.ERROR);
                return this.RedirectToAction("My");
            }
            var model = EventInputModel.CreateFromEvent(eventToEdit);
            return this.View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, EventInputModel model)
        {
            var eventToEdit = this.LoadEvent(id);
            if (eventToEdit == null)
            {
                this.AddNotification("Cannot edit event #" + id, NotificationType.ERROR);
                return this.RedirectToAction("My");
            }
            if (model != null && this.ModelState.IsValid)
            {
                eventToEdit.Title = model.Title;
                eventToEdit.StartDateTime = model.StartDateTime;
                eventToEdit.Duration = model.Duration;
                eventToEdit.Description = model.Description;
                eventToEdit.Location = model.Location;
                eventToEdit.isPublic = model.IsPublic;

                this.db.SaveChanges();
                this.AddNotification("Event edited.", NotificationType.INFO);
                return this.RedirectToAction("My");
            }
            return this.View(model);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var eventToDelete = this.db.Events
                .Where(e => e.Id == id)
                .Select(ConfirmEventDeleteViewModel.ViewModel).FirstOrDefault();

            return View(eventToDelete);
        }

        [HttpPost]
        public ActionResult DeleteEvent(int id)
        {
            var eventToDelete = this.db.Events.FirstOrDefault(e => e.Id == id);
            this.db.Events.Remove(eventToDelete);
            this.db.SaveChanges();
            this.AddNotification("Event deleted.", NotificationType.INFO);
            return this.RedirectToAction("My");
        }
    }
}