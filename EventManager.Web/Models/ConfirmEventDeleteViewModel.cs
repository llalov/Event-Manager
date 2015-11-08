using System;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using EventManager.Models;

namespace EventManager.Web.Models
{
    public class ConfirmEventDeleteViewModel
    {

        public static Expression<Func<Event, ConfirmEventDeleteViewModel>> ViewModel
        {
            get
            {
                return e => new ConfirmEventDeleteViewModel()
                {
                    Id = e.Id,
                    Title = e.Title,
                    StartDateTime = e.StartDateTime,
                    Duration = e.Duration,
                    Author = e.Author,
                    Location = e.Location
                };
            }
        }
        public int Id { get; set; }

        public string Title { get; set; }

        [Display(Name = "Date and Time")]
        public DateTime StartDateTime { get; set; }

        public TimeSpan? Duration { get; set; }

        public virtual User Author { get; set; }

        public string Location { get; set; }
    }
}