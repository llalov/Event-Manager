using System;
using System.Linq.Expressions;
using EventManager.Models;

namespace EventManager.Web.Models
{
    public class EventViewModel
    {

        public static Expression<Func<Event, EventViewModel>> ViewModel
        {
            get
            {
                return e => new EventViewModel()
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
     
        public DateTime StartDateTime { get; set; }

        public TimeSpan? Duration { get; set; }

        public virtual User Author { get; set; }

        public string Location { get; set; }

    }
}