using System;
using System.Web.Mvc;
using EventManager.Models;
using EventManager.Web.Extensions;
using EventManager.Web.Models;
using Microsoft.AspNet.Identity;

namespace EventManager.Web.Controllers
{
    public class CommentsController : BaseController
    {
        public ActionResult Create()
        {
            return this.PartialView("_CreateCommentPartial");
        }

    }
}