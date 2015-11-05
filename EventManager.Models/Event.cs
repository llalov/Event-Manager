using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EventManager.Models
{
    public class Event
    {
        public Event()
        {
            this.isPublic = true;
            this.StartDateTime = DateTime.Now;
            this.Comments = new HashSet<Comment>();
        }
        
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Title { get; set; }

        [Required]
        public DateTime StartDateTime { get; set; }

        public TimeSpan? Duration { get; set; }

        public string AuthorId { get; set; }

        public virtual User Author { get; set; }

        public string Description { get; set; }

        [MaxLength(200)]
        public string Location { get; set; }

        public bool isPublic { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
    }
}
