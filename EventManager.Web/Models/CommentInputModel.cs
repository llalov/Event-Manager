using System;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using EventManager.Models;

namespace EventManager.Web.Models
{
    public class CommentInputModel
    { 
        [Required]
        public string Text { get; set; }
    }
}