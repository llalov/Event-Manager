using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EventManager.Models;

namespace EventManager.Web.Models
{
    public class EventViewModel
    {
        public int Id { get; set; }
        
        public string Title { get; set; }
     
        public DateTime StartDateTime { get; set; }

        public TimeSpan? Duration { get; set; }

        public virtual User Author { get; set; }

        public string Location { get; set; }

    }
}