using System;
using System.ComponentModel.DataAnnotations;

namespace EventManager.Models
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Text { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public string AuthorId { get; set; }

        public virtual User Author { get; set; }

        public int EventId { get; set; }

        [Required]
        public virtual Event Event { get; set; }
    }
}
