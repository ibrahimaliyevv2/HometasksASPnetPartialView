using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PustokProject.Models
{
	public class Author
	{
        public int Id { get; set; }
        [Required]
        [MaxLength(50, ErrorMessage = "Maximum length is 50 characher!")]
        public string FullName { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public string? ModifiedBy { get; set; }

        public DateTime BirthDate { get; set; }

        public List<Book> Books { get; set; }
        [ForeignKey("ModifiedBy")]
        public AppUser AppUser { get; set; }
    }
}

