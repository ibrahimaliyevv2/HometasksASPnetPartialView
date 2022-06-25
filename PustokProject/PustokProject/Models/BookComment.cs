using System;
using System.ComponentModel.DataAnnotations;

namespace PustokProject.Models
{
	public class BookComment
	{
        public int Id { get; set; }
        public int BookId { get; set; }
        public string AppUserId { get; set; }
        [Required]
        [MaxLength(250)]
        public string Text { get; set; }
        public int Rate { get; set; }
        public DateTime CreatedAt { get; set; }

        public Book Book { get; set; }
        public AppUser AppUser { get; set; }
    }
}

