﻿using System;
using System.Linq;
using System.Web.Mvc;
using EventManager.Models;
using EventManager.Web.Extensions;
using EventManager.Web.Models;
using Microsoft.AspNet.Identity;

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
                .Select(EventViewModel.ViewModel);

            var upcomingEvents = events.Where(e => e.StartDateTime > DateTime.Now);

            var passedEvents = events.Where(e => e.StartDateTime <= DateTime.Now);

            return View(new UpcomingPassedEventsViewModel()
            {
                UpcomingEvents = upcomingEvents,
                PassedEvents = passedEvents
            });
        }

        public ActionResult EventDetailsById(int id)
        {
            var currentUserId = this.User.Identity.GetUserId();
            var isAdmin = this.IsAdmin();
            var eventDetails = this.db.Events
                .Where(e => e.Id == id)
                .Where(e => e.isPublic || isAdmin || (e.AuthorId != null && e.AuthorId == currentUserId))
                .Select(EventDetailsViewModel.ViewModel)
                .FirstOrDefault();

            var isOwner = (eventDetails != null && eventDetails.AuthorId != null &&
                           eventDetails.AuthorId == currentUserId);
            this.ViewBag.CanEdit = isOwner || isAdmin;

            return this.PartialView("_EventDetailsPartial", eventDetails);
        }

        /*public ActionResult AddCommentById(int id)
        {
            var currentUserId = this.User.Identity.GetUserId();

        } */
        
       
    }
}